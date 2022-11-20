using WorldCupWorkerService;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {

        services.AddScoped<IWorldCupWorkerService, CountriesService>();
        //add workerClass as a hosted service
        services.AddHostedService<Worker>();

       
    })
    .Build();

await host.RunAsync();
