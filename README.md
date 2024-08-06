# MongoDB-Entity-Framework-Core-Provider
To use MongoDB with Entity Framework Core (EF Core) in an ASP.NET Core application, you can leverage the MongoDB.EntityFrameworkCore provider. This package integrates MongoDB as a backend database while using familiar EF Core APIs.

NuGet : https://www.nuget.org/packages/MongoDB.EntityFrameworkCore

The UnitOfWork pattern is designed to maintain a list of objects affected by a business transaction and coordinate the writing out of changes and the resolution of concurrency problems. The way you've implemented the UnitOfWork class can work, but there are a few considerations regarding object-oriented programming (OOP) principles and the traditional intent of the UnitOfWork pattern.

## ![Example Image](https://github.com/raihannishat/MongoDB-Entity-Framework-Core-Provider/blob/main/example-endpoints.png)

## OOP Principles and Considerations
### [1] Dependency Injection:
Using IServiceProvider to resolve repositories dynamically can be seen as a pragmatic approach to avoid manually managing repository instances.
However, it can introduce a hidden dependency and reduce the clarity of the dependencies required by the UnitOfWork.

### [2] Single Responsibility Principle (SRP):
The UnitOfWork class should manage the database context and coordinate transactions. By resolving repositories dynamically, it’s slightly taking on additional responsibility related to dependency resolution.

### [3] Explicit Dependencies Principle:
Explicitly injecting dependencies (repositories) into the UnitOfWork constructor is generally preferred. This approach makes the class's dependencies clear and improves testability.

## Unit of Work Pattern Rules
The traditional UnitOfWork pattern involves:
Keeping track of changes to objects and coordinating the persistence of those changes.
Managing the lifecycle of repositories.

## Minimal Api
Using static methods for mapping minimal API endpoints in ASP.NET Core has several advantages. Here’s a detailed explanation of why static methods are useful in this context:

### [1] No Instance Required
Static methods belong to the type itself rather than to instances of the type. This means you don't need to create an instance of the endpoint class to map the routes. This simplifies the code and reduces the overhead associated with object instantiation.

### [2] Simplifies Endpoint Registration
By using static methods, you can directly call the methods without needing to manage the lifecycle of the endpoint class. This is particularly useful for setting up minimal APIs where you want to keep the setup code concise and straightforward.

### [3] Thread Safety
Static methods are inherently thread-safe when they do not modify any shared state. Since the methods used for mapping endpoints typically only configure routing and do not maintain any state, they are safe to use in a multi-threaded environment like a web server.

### 4. Clear Separation of Concerns
Using static methods can help to clearly separate the endpoint definitions from other application logic. This can make your code easier to understand and maintain, as the endpoint definitions are isolated in a dedicated static class.
