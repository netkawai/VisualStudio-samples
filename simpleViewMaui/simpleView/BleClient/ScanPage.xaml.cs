namespace simpleView.BleClient;


public partial class ScanPage : ContentPage
{
    public ScanPage()
    {
        this.InitializeComponent();
    }

    void Entry_PropertyChanged(System.Object sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        if (BindingContext is ScanViewModel vm)
        {
            vm.UpdateFilter();
        }
    }
}