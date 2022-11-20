using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


// scoped service, see https://learn.microsoft.com/en-us/aspnet/core/fundamentals/host/hosted-services?view=aspnetcore-7.0&tabs=visual-studio
// see Consuming a scoped service in a background task in link abive
namespace WorldCupWorkerService
{
     public interface IWorldCupWorkerService
    {
       void AddCountries(int counter);
    }
    public class CountriesService : IWorldCupWorkerService  
    {
        private readonly ILogger<Worker> _logger;
        // add the number of times brazil world cup
        public Dictionary<int, string> WorldCupCountry { get; set; }


        public Random rnd = new Random();


        string[] countryNames = { "England", "France", "Germany", "Spain", "Brazil", "Argentina"};
        public CountriesService(ILogger<Worker> logger)
        {
           _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            WorldCupCountry = new();
        }
        public void AddCountries(int counter)
        {
            _logger.LogInformation("Adding World Cup Countries to list");
            var names = RandomCountries();
            WorldCupCountry.Add(counter,names);


            foreach (var item in WorldCupCountry)
            {

                _logger.LogInformation($"{item.Key}, {item.Value}!");
            }

        }


        public string RandomCountries() => countryNames[rnd.Next(0,countryNames.Length)];

       
    }
}
