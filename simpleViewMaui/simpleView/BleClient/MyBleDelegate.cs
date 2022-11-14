using Shiny;
using Shiny.BluetoothLE;
using simpleView.Infrastructure;

namespace simpleView.BleClient;


public class MyBleDelegate : BleDelegate
{
    readonly SampleSqliteConnection conn;
    public MyBleDelegate(SampleSqliteConnection conn)
    {
        this.conn = conn;
    }

    public override Task OnAdapterStateChanged(AccessState state)
        => Task.CompletedTask;
    /*
        => this.conn.Log(
            "BLE",
            "Adapter Status",
            $"New Status: {state}"
        );
    */

    public override Task OnConnected(IPeripheral peripheral)
        => Task.CompletedTask;
    /*
        => this.conn.Log(
            "BLE",
            "Peripheral Connected",
            peripheral.Name
        );
    */
}