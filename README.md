# Sqli Ecommerce

Sqli Ecommerce is a sample project used during a microservices in .NET training.

We will try to add more features to it and use it for upcoming training sessions on:
- Security in microservices projects
- Deployment using Kubernetes

## Current Solution

### Catalog Microservice

- Uses ASP.NET Core Minimal APIs, .NET 8, and C# 12.
- Implements Vertical Slice Architecture with Feature folders.
- Uses CQRS with MediatR and FluentValidation.
- Utilizes Marten for transactional document DB on PostgreSQL.
- Defines Minimal API endpoints with Carter.
- Includes logging, global exception handling, and health checks.

### Basket Microservice

- Built with ASP.NET 8 Web API, following REST principles and CRUD operations.
- Uses Redis as a distributed cache.
- Implements Proxy, Decorator, and Cache-aside patterns.
- Consumes Discount gRPC Service for calculating final prices.
- Publishes BasketCheckout Queue using MassTransit and RabbitMQ.

### Discount Microservice

- Built with ASP.NET gRPC Server.
- Provides high-performance inter-service gRPC communication.
- Uses Entity Framework Core with SQLite.
- Containerized SQLite database.

### Microservices Communication

- Synchronous gRPC and asynchronous RabbitMQ communications.
- Uses RabbitMQ Publish/Subscribe model and MassTransit abstraction.
- Publishes and subscribes to events between Basket and Ordering microservices.
- Includes a RabbitMQ EventBus.Messages library.

### Ordering Microservice

- Implements DDD, CQRS, and Clean Architecture.
- Uses MediatR, FluentValidation, and Mapster.
- Consumes RabbitMQ events with MassTransit.
- Uses SQL Server with Entity Framework Core and automatic migrations.

### Yarp API Gateway Microservice

- Develops API Gateways with Yarp Reverse Proxy.
- Configures routes, clusters, paths, transformations, and destinations.
- Implements rate limiting with FixedWindowLimiter.

### WebUI ShoppingApp Microservice

- An ASP.NET Core Web Application with Bootstrap 4 and Razor templates.
- Calls Yarp APIs using Refit HttpClientFactory.

### Docker Setup

- Containerizes all microservices and databases.
- Configures environment variables.
- Provides instructions for running the project using Docker Compose.

## Setup Instructions

### Required Tools

- Visual Studio 2022
- .NET Core 8 or later
- Docker Desktop

### Steps

1. Clone the repository.
2. Configure Docker Desktop with at least 4 GB memory and 2 CPUs.
3. Run `docker-compose` commands to start the services.
4. Access the Shopping Web UI at [https://localhost:6065](https://localhost:6065).

Use the Web project to interact with microservices via the Yarp API Gateway and monitor the RabbitMQ dashboard for queue records during basket checkouts.
