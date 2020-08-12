# Orders Processing Service

The `Orders Processing Service` is a basic CRUD ASP.net core application that connects with MongoDB that was requested as a coding task during an interview process.
It displays a simple scenario where you can add/update/delete/retrieve orders in a business application.

`Order Model`

```json
{
  "id": "guid",
  "userId": "guid",
  "amount": "int"
}
```

`orders/by-users` endpoint returns an aggregation of the orders grouped by userId with a sum of the total amount of the orders.

```json
{
  "userId": "guid",
  "orders": [
  "guid"
  ],
  "totalAmount": "int"
},
```

## Dependencies

- Instead of running my own MongoDB instance I use a free tier on [MongoDB Cloud Atlas](https://www.mongodb.com/cloud/atlas).
- I use [AutoMapper](https://automapper.org/) to map between domain models and API models.
- I use [Moq](https://github.com/moq/moq) and [XUnit](https://xunit.github.io/) for unit testing the `OrdersService`.
- I use [Swagger](https://swagger.io/) for API documentation.

## Extra included features.

- Deployable as a [Docker](https://www.docker.com/) container.
- The default URL shows a [Swagger](https://swagger.io/) page with the API documentation.

## Possible Improvements

Due to time constraints there are some "Missing" features that I would normally implement in a "Real World" Project. Some of them are the following:

- Authentication and Authorization using something like `Identity` and `JWT`.
- Add pagination to the endpoints including limit and offset.
- Complete separation between Domain Models and Database Models.
- Generic implementation of the `Repository Pattern` when other repositories are introduced to the service.
- In Memory DB or Mock to run integration tests.
- Separate concerns between `Queries` and `Commands`.
- HealthCheck endpoint that shows Database Integrity. For that, I like this [library](https://github.com/xabaril/AspNetCore.Diagnostics.HealthChecks).
- Depending on the security needed sometimes it is good to add an extra API in between of the service and the outside world working as gateway.

## Run on docker

On a terminal with OrdersProcessingService as root run the following commands:

```bash
docker build -t orders-service .
docker run -p 8080:80 orders-service
```

## Run on dotnet

On a terminal with OrdersProcessingService.Api as root run the following commands:

```bash
dotnet restore
dotnet build
dotnet run
```
