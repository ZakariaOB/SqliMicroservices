//The Circuit Breaker pattern is a design pattern used in software development to prevent cascading
//failures and enhance the resilience of an application. It acts as a safeguard that monitors
//the interactions between services and temporarily halts requests to a failing service, allowing the system to recover gracefully.
//Key Concepts of the Circuit Breaker Pattern
//Closed State: In this state, the circuit breaker allows requests to flow through as normal.
//If the number of failures exceeds a predefined threshold, the circuit breaker transitions to the Open state.
//Open State: In this state, the circuit breaker stops forwarding requests to the service and immediately returns
//an error response.This prevents further strain on the failing service.
//Half-Open State: After a timeout period, the circuit breaker transitions to the Half-Open state.
//In this state, it allows a limited number of test requests to determine if the service has recovered.
//If these requests succeed, the circuit breaker transitions back to the Closed state; otherwise, it returns to the Open state.

using Samples.Patterns.CircuitBreaker;

namespace Samples.Test.CircuitBreaker
{
    public class CircuitBreakerTests
    {
        [Fact]
        public async Task CircuitBreaker_ClosesAfterSuccessfulRequest()
        {
            // Arrange
            var circuitBreaker = new CircuitBreakerDemo(
            failureThreshold: 3,
                openTimeout: TimeSpan.FromSeconds(1));
            var service = new ExternalService(circuitBreaker);

            // Act and Assert
            // First three requests fail
            await Assert.ThrowsAsync<Exception>(() => service.GetDataAsync());
            await Assert.ThrowsAsync<Exception>(() => service.GetDataAsync());
            await Assert.ThrowsAsync<Exception>(() => service.GetDataAsync());

            // Circuit breaker should open now
            await Assert.ThrowsAsync<CircuitBreakerOpenException>(() => service.GetDataAsync());

            // Wait for the timeout period
            await Task.Delay(1500);

            // Next request should be attempted, and it should fail again
            await Assert.ThrowsAsync<Exception>(() => service.GetDataAsync());

            // Circuit breaker should open again
            await Assert.ThrowsAsync<CircuitBreakerOpenException>(() => service.GetDataAsync());
        }

        [Fact]
        public async Task CircuitBreaker_ResetsAfterSuccessfulRequest()
        {
            // Arrange
            var circuitBreaker = new CircuitBreakerDemo(failureThreshold: 3, openTimeout: TimeSpan.FromSeconds(1));
            var service = new ExternalService(circuitBreaker);

            // Act and Assert
            // First three requests fail
            await Assert.ThrowsAsync<Exception>(() => service.GetDataAsync());
            await Assert.ThrowsAsync<Exception>(() => service.GetDataAsync());
            await Assert.ThrowsAsync<Exception>(() => service.GetDataAsync());

            // Circuit breaker should open now
            await Assert.ThrowsAsync<CircuitBreakerOpenException>(() => service.GetDataAsync());

            // Wait for the timeout period
            await Task.Delay(1500);

            // Next request should be attempted, simulate a success
            var result = await circuitBreaker.ExecuteAsync(async () =>
            {
                await Task.Delay(100);
                return "Service data";
            });

            Assert.Equal("Service data", result);

            // Circuit breaker should reset and close now
            result = await circuitBreaker.ExecuteAsync(async () =>
            {
                await Task.Delay(100);
                return "Service data";
            });

            Assert.Equal("Service data", result);
        }
    }
}
