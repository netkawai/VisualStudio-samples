using Shiny;
using Shiny.BluetoothLE;

namespace simpleView.BleClient;


public class MyBleDelegate : BleDelegate
{


    public override Task OnAdapterStateChanged(AccessState state)
        => Task.CompletedTask;


    public override Task OnConnected(IPeripheral peripheral)
        => Task.CompletedTask;
}