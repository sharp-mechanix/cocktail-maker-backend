using System;
using System.Threading;
using System.Threading.Tasks;
using CocktailMaker.Grabber.Interfaces;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace CocktailMaker.Grabber
{
    /// <summary>
    ///     Grabbing runner
    /// </summary>
    public class Worker : BackgroundService
    {
        private readonly IGrabberService _grabber;
        private readonly ILogger<Worker> _logger;

        public Worker(IGrabberService grabber, ILogger<Worker> logger)
        {
            _grabber = grabber;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

                await _grabber.GetCocktailDataAsync(stoppingToken);

                await Task.CompletedTask;
            }
        }
    }
}

