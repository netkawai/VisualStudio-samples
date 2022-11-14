using Prism.DryIoc;
using Shiny;
using simpleView.Infrastructure;

namespace simpleView;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
            .UseShiny() // THIS IS REQUIRED FOR SHINY ON MAUI
            .ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			}).UsePrism(
                new DryIocContainerExtension(),
                prism => prism.OnAppStart("MainPage")
            );

        RegisterServices(builder.Services);
        RegisterShinyServices(builder.Services);
        RegisterRoutes(builder.Services);

        return builder.Build();
	}

    static void RegisterShinyServices(IServiceCollection s)
    {

        // shiny.core
        s.AddConnectivity();
        s.AddBattery();

        // shiny.bluetoothle & shiny.bluetoothle.hosting
        s.AddBluetoothLE<BleClient.MyBleDelegate>();

    }


    static void RegisterServices(IServiceCollection s)
    {
        //builder.Logging.AddProvider(new SqliteLoggerProvider(LogLevel.Trace));
        s.AddScoped<BaseServices>();
        s.AddSingleton(TextToSpeech.Default);
        s.AddSingleton(FilePicker.Default);
    }


    static void RegisterRoutes(IServiceCollection s)
    {
        // ble client
        s.RegisterForNavigation<BleClient.ScanPage, BleClient.ScanViewModel>("BleScan");
        s.RegisterForNavigation<BleClient.PeripheralPage, BleClient.PeripheralViewModel>("BlePeripheral");
        s.RegisterForNavigation<BleClient.ServicePage, BleClient.ServiceViewModel>("BlePeripheralService");
        s.RegisterForNavigation<BleClient.CharacteristicPage, BleClient.CharacteristicViewModel>("BlePeripheralCharacteristic");
        s.RegisterForNavigation<BleManaged.ManagedScanPage, BleManaged.ManagedScanViewModel>("BleManagedScan");
        s.RegisterForNavigation<BleManaged.ManagedPeripheralPage, BleManaged.ManagedPeripheralViewModel>("BleManagedPeripheral");


        // ble perf testing
        s.RegisterForNavigation<BlePerf.ClientPage, BlePerf.ClientViewModel>("BlePerfClient");


        s.RegisterForNavigation<MainPage, MainViewModel>();
    }

}

