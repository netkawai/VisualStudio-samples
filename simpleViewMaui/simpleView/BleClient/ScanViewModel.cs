using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;
using System.Windows.Input;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Shiny;
using Shiny.BluetoothLE;
using simpleView.Infrastructure;

namespace simpleView.BleClient;


public class ScanViewModel : ViewModel
{
    IDisposable? scanSub;


    public ScanViewModel(BaseServices services, IBleManager bleManager) : base(services)
    {
        Peripherals.CollectionChanged += Peripherals_CollectionChanged;

        this.IsScanning = bleManager?.IsScanning ?? false;

        this.WhenAnyValueSelected(x => x.SelectedPeripheral, async x =>
        {
            this.StopScan();
            await this.Navigation.Navigate("BlePeripheral", ("Peripheral", x.Peripheral));
        });

        this.ScanToggle = ReactiveCommand.CreateFromTask(
            async () =>
            {
                if (bleManager == null)
                {
                    await this.Alert("Platform Not Supported");
                    return;
                }
                if (this.IsScanning)
                {
                    this.StopScan();
                }
                else
                {
                    this.Peripherals.Clear();
                    this.IsScanning = true;

                    this.scanSub = bleManager
                        .Scan()
                        .Buffer(TimeSpan.FromSeconds(1))
                        .Where(x => x?.Any() ?? false)
                        .SubOnMainThread(
                            results =>
                            {
                                var list = new List<PeripheralItemViewModel>();
                                foreach (var result in results)
                                {
                                    var peripheral = this.Peripherals.FirstOrDefault(x => x.Equals(result.Peripheral));
                                    if (peripheral == null)
                                        peripheral = list.FirstOrDefault(x => x.Equals(result.Peripheral));


                                    if (peripheral != null)
                                    {
                                        peripheral.Update(result);
                                    }
                                    else
                                    {
                                        peripheral = new PeripheralItemViewModel(result.Peripheral);
                                        peripheral.Update(result);
                                        list.Add(peripheral);
                                    }
                                }
                                if (list.Any())
                                {
                                    // XF is not able to deal with an observablelist/addrange properly
                                    foreach (var item in list)
                                        this.Peripherals.Add(item);
                                }
                            },
                            ex => this.Alert(ex.ToString(), "ERROR")
                        );
                }
            }
        );
    }


    public void UpdateFilter()
    {

        Application.Current.Dispatcher.DispatchAsync(() =>
        {
            FilteredPeripherals.Clear();

            foreach (var p in Peripherals)
            {
                if (IsSatisfyFilter(p))
                    FilteredPeripherals.Add(p);
            }
        });
    }

    private bool IsSatisfyFilter(PeripheralItemViewModel p)
    {
        if (!string.IsNullOrEmpty(this.CompanyId))
        {
            var companyid = ushort.Parse(CompanyId, System.Globalization.NumberStyles.AllowHexSpecifier);
            if (p.AdvertisementData?.ManufacturerData == null || p.AdvertisementData?.ManufacturerData.CompanyId != companyid)
                return false;
        }

        if (!string.IsNullOrEmpty(this.LocalName))
        {
            if (string.IsNullOrEmpty(p.LocalName) || !p.LocalName.StartsWith(LocalName))
                return false;
        }

        if (!string.IsNullOrEmpty(MacAddress))
        {
            if (string.IsNullOrEmpty(p.Uuid) || !p.Uuid.StartsWith(MacAddress))
                return false;

        }
        return true;
    }

    private void Peripherals_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
    {
        switch (e.Action)
        {
            case System.Collections.Specialized.NotifyCollectionChangedAction.Add:
                PeripheralItemViewModel p = (PeripheralItemViewModel)e.NewItems[0];
                if (IsSatisfyFilter(p))
                {
                    FilteredPeripherals.Add(p);
                }
                break;

        }
    }

    public string LocalName { get; set; }
    public string MacAddress { get; set; }
    public string CompanyId { get; set; }

    public ICommand NavToTest { get; }
    public ICommand ScanToggle { get; }

    public ObservableCollection<PeripheralItemViewModel> FilteredPeripherals { get; } = new();

    private ObservableCollection<PeripheralItemViewModel> Peripherals { get; } = new();

    [Reactive] public PeripheralItemViewModel? SelectedPeripheral { get; set; }
    [Reactive] public bool IsScanning { get; private set; }


    void StopScan()
    {
        this.scanSub?.Dispose();
        this.scanSub = null;
        this.IsScanning = false;
    }
}