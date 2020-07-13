using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;

namespace HealthCheckSites
{
    public class GetHealthCheck : BackgroundService
    {
        private readonly ILogger _logger;
        private readonly IConfiguration _config;
        private readonly HttpClient _httpClient;
        private readonly List<string> _sites;

        public GetHealthCheck(IConfiguration config, ILogger logger)
        {
            _logger = logger;
            _config = config;
            _httpClient = new HttpClient();
            
            _sites = _config.GetSection("HealthChecks").Get<List<string>>();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            foreach (var site in _sites)
            {
                var result = await _httpClient.GetStringAsync(site);
                
                _logger.LogInformation(result);
            }
        }
    }
}
