using ServiceA.Models;

namespace ServiceA.Services
{
    public class TimedHostedService : IHostedService, IDisposable
    {
        private readonly ILogger<TimedHostedService> _logger;
        private readonly IBitcoinService? _bitcoinService;
        private Timer? _timer;
        private const int TimelapseInSeconds = 5;
        private readonly SlidingBuffer<double> _buffer;
        private readonly IMemoryCacheService<double> _memoryCacheService;

        public TimedHostedService(
            ILogger<TimedHostedService> logger, 
            IServiceScopeFactory scopeFactory,
            IMemoryCacheService<double> memoryCacheService)
        {
            _logger = logger;
            _memoryCacheService = memoryCacheService;
            _buffer = new SlidingBuffer<double>(10 * 6);

            using var scope = scopeFactory.CreateScope();
            _bitcoinService = scope.ServiceProvider.GetService<IBitcoinService>();
        }

        public Task StartAsync(CancellationToken stoppingToken)
        {
            _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(TimelapseInSeconds));

            return Task.CompletedTask;
        }

        private void DoWork(object? state)
        {
            var currentValue = _bitcoinService?.GetBitcoinValue().Result;
            
            if (currentValue != null)
            {
                _buffer.Add((double) currentValue);
            }

            var avg = _buffer.Average();
            _memoryCacheService.Set("average", avg);

            _logger.LogInformation("Current: {currentValue}; Average: {0:##.##}", currentValue, avg);
        }

        public Task StopAsync(CancellationToken stoppingToken)
        {
            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
