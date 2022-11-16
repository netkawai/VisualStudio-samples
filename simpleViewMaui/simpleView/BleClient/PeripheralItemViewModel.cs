using ReactiveUI;
using Shiny.BluetoothLE;
using simpleView.Infrastructure;

namespace simpleView.BleClient;


public class PeripheralItemViewModel : ReactiveObject
{
    public PeripheralItemViewModel(IPeripheral peripheral)
    { this.Peripheral = peripheral; }


    public override bool Equals(object obj)
        => this.Peripheral.Equals(obj);

    public IAdvertisementData AdvertisementData { get; private set; }
    public IPeripheral Peripheral { get; }
    public string Uuid => this.Peripheral.Uuid;

    public string Name { get; private set; }
    public bool IsConnected { get; private set; }
    public int Rssi { get; private set; }
    public string Connectable { get; private set; }
    public int ServiceCount { get; private set; }
    //[Reactive] public string ManufacturerData { get; private set; }
    public string LocalName { get; private set; }
    public int TxPower { get; private set; }

    public string CompanyId => AdvertisementData?.ManufacturerData?.CompanyId.ToString("X") ?? "Unknown";

    public void Update(ScanResult result)
    {
        this.Name = this.Peripheral.Name;
        this.Rssi = result.Rssi;

        AdvertisementData = result.AdvertisementData;
        this.ServiceCount = AdvertisementData.ServiceUuids?.Length ?? 0;
        this.Connectable = AdvertisementData?.IsConnectable?.ToString() ?? "Unknown";
        this.LocalName = AdvertisementData.LocalName;
        this.TxPower = AdvertisementData.TxPower ?? 0;
        this.RaisePropertyChanged(String.Empty);
            //this.ManufacturerData = ad.ManufacturerData == null
            //    ? null
            //    : BitConverter.ToString(ad.ManufacturerData);
    }
}