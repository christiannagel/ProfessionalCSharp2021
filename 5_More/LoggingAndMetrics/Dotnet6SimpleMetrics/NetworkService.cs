using Microsoft.Extensions.Logging;

using System.Diagnostics;
using System.Diagnostics.Metrics;

namespace MetricsSample;

sealed class NetworkService : IDisposable
{
    private readonly ILogger<NetworkService> _logger;
    private readonly HttpClient _httpClient;
    private static Meter s_meter = new("Wrox.ProCSharp.MetricsSample", "v1.0");
    private static Counter<int> s_requestCounter;
    private static Counter<int> s_errorCounter;
    private static Histogram<long> s_duration;

    public NetworkService(
        HttpClient httpClient,
        ILogger<NetworkService> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
        _logger.LogTrace("ILogger injected into {0}", nameof(NetworkService));
    }

    static NetworkService()
    {
        s_requestCounter = s_meter.CreateCounter<int>("total-requests", "count", "total requests");
        s_errorCounter = s_meter.CreateCounter<int>("total-errors", "count", "total errors");
        s_duration = s_meter.CreateHistogram<long>("request-duration", "milliseconds", "request duration");
    }

    public void Dispose()
    {
        s_meter.Dispose();
    }

    public async Task NetworkRequestSampleAsync(Uri requestUri)
    {
        s_requestCounter.Add(1);
        Stopwatch? stopwatch = null;
        if (s_duration.Enabled)
        {
            stopwatch = Stopwatch.StartNew();
        }
        try
        {
            _logger.LogInformation(LoggingEvents.Networking, "NetworkRequestSampleAsync started with uri {0}", requestUri.AbsoluteUri);

            string result = await _httpClient.GetStringAsync(requestUri);
            Console.WriteLine($"{result[..50]}");
            _logger.LogInformation(LoggingEvents.Networking, "NetworkRequestSampleAsync completed, received {0} characters", result.Length);
        }
        catch (HttpRequestException ex)
        {
            _logger.LogError(LoggingEvents.Networking, ex, "Error in NetworkRequestSampleAsync, error message: {0}, HResult: {1}", ex.Message, ex.HResult);
            s_errorCounter.Add(1);
        }
        finally
        {
            if (stopwatch is not null)
            {
                stopwatch.Stop();
                s_duration.Record(
                    stopwatch.ElapsedMilliseconds, 
                    tag: KeyValuePair.Create<string, object?>("Uri", requestUri));
            }
        }
    }
}
