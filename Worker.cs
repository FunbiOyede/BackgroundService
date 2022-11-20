using Microsoft.Extensions.DependencyInjection;

namespace WorldCupWorkerService;

//A background process is a computer process that runs behind the scenes
// (i.e., in the background) and without
// user intervention.[1]
// Typical tasks for these processes include logging, system monitoring, scheduling

// A Background Service is a service that runs only when the app
// is running so it’ll get terminated when the app is terminated.
public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly IServiceProvider _serviceProvider;

    private int _executionCount = 0;

    //control if the service is running or not
    public bool IsEnabled { get; set; }
    public Worker(ILogger<Worker> logger, IServiceProvider serviceProvider)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
       
    }

    public override async Task StartAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Timed Hosted Service has started running.");

        await ExecuteAsync(stoppingToken);

        //_logger.LogInformation("I am leaving the start operation.");
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {

                 using var scope = _serviceProvider.CreateScope();
                    //A way to inject service in Background Service
                    var wordlCupCountriesServices = scope.ServiceProvider.GetService<IWorldCupWorkerService>();
                    ArgumentNullException.ThrowIfNull(wordlCupCountriesServices);



                   
                    _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

                    await Task.Delay(2000, stoppingToken);

                    _executionCount++;
                    wordlCupCountriesServices.AddCountries(_executionCount);


                    _logger.LogInformation(
                        $"Executed Count: {_executionCount}");
               
                
            }
            catch (Exception ex)
            {

                _logger.LogInformation("Background Service Failed to run",ex.Message );
            }
          
        }
    }


    public override async Task StopAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Timed Hosted Service want's to stop running.");

        await Task.Delay(1000, stoppingToken);

        _logger.LogInformation("I am stopping the operation.");
    }

}
