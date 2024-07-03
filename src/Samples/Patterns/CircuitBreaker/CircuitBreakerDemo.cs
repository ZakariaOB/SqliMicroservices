namespace Samples.Patterns.CircuitBreaker
{
    public class CircuitBreakerDemo
    {
        private readonly int _failureThreshold;
        private readonly TimeSpan _openTimeout;
        private int _failureCount;
        private DateTime _lastFailureTime;
        private CircuitBreakerState _state;

        public CircuitBreakerDemo(int failureThreshold, TimeSpan openTimeout)
        {
            _failureThreshold = failureThreshold;
            _openTimeout = openTimeout;
            _failureCount = 0;
            _lastFailureTime = DateTime.MinValue;
            _state = CircuitBreakerState.Closed;
        }

        public async Task<T> ExecuteAsync<T>(Func<Task<T>> action)
        {
            switch (_state)
            {
                case CircuitBreakerState.Closed:
                    return await ExecuteActionAsync(action);

                case CircuitBreakerState.Open:
                    if (DateTime.UtcNow - _lastFailureTime > _openTimeout)
                    {
                        _state = CircuitBreakerState.HalfOpen;
                        return await ExecuteActionAsync(action);
                    }
                    throw new CircuitBreakerOpenException("Circuit breaker is open");

                case CircuitBreakerState.HalfOpen:
                    return await ExecuteActionAsync(action);

                default:
                    throw new InvalidOperationException("Invalid circuit breaker state");
            }
        }

        private async Task<T> ExecuteActionAsync<T>(Func<Task<T>> action)
        {
            try
            {
                var result = await action();
                Reset();
                return result;
            }
            catch
            {
                TrackFailure();
                throw;
            }
        }

        private void TrackFailure()
        {
            _failureCount++;
            _lastFailureTime = DateTime.UtcNow;

            if (_failureCount >= _failureThreshold)
            {
                _state = CircuitBreakerState.Open;
            }
        }

        private void Reset()
        {
            _failureCount = 0;
            _state = CircuitBreakerState.Closed;
        }
    }

    public class ExternalService
    {
        private readonly CircuitBreakerDemo _circuitBreaker;

        public ExternalService(CircuitBreakerDemo circuitBreaker)
        {
            _circuitBreaker = circuitBreaker;
        }

        public async Task<string> GetDataAsync()
        {
            return await _circuitBreaker.ExecuteAsync<string>(async () =>
            {
                // Simulate a call to an external service
                await Task.Delay(100);

                // Simulate a failure
                throw new Exception("Service failure");

                // Uncomment the following line to simulate a successful call
                // return "Service data";
            });
        }
    }

}
