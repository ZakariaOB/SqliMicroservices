namespace Samples.Patterns.CircuitBreaker
{
    public class ExternalServiceForCircuitBreakerDemo
    {
        private readonly CircuitBreakerDemo _circuitBreaker;

        public ExternalServiceForCircuitBreakerDemo(CircuitBreakerDemo circuitBreaker)
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
