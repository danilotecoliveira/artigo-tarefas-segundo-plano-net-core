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
        private readonly ILogger<GetHealthCheck> _logger;
        private readonly IConfiguration _config;
        private readonly HttpClient _httpClient;
        private readonly List<string> _sites;

        public GetHealthCheck(IConfiguration config, ILogger<GetHealthCheck> logger)
        {
            _logger = logger;
            _config = config;
            _httpClient = new HttpClient();

            _sites = _config.GetSection("HealthChecks").Get<List<string>>();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                foreach (var site in _sites)
                {
                    var result = await _httpClient.GetAsync(site);

                    if (result.IsSuccessStatusCode)
                        _logger.LogInformation($"Health check to {site} Ok");
                    else
                        _logger.LogError($"Health check error to {site}");
                }

                await Task.Delay(60000, stoppingToken);
            }
        }
    }
}
