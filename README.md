# MongoDB-Entity-Framework-Core-Provider
To use MongoDB with Entity Framework Core (EF Core) in an ASP.NET Core application, you can leverage the MongoDB.EntityFrameworkCore provider. This package integrates MongoDB as a backend database while using familiar EF Core APIs.

NuGet : https://www.nuget.org/packages/MongoDB.EntityFrameworkCore

Using static methods for mapping minimal API endpoints in ASP.NET Core has several advantages. Here’s a detailed explanation of why static methods are useful in this context:

1. No Instance Required
Static methods belong to the type itself rather than to instances of the type. This means you don't need to create an instance of the endpoint class to map the routes. This simplifies the code and reduces the overhead associated with object instantiation.

2. Simplifies Endpoint Registration
By using static methods, you can directly call the methods without needing to manage the lifecycle of the endpoint class. This is particularly useful for setting up minimal APIs where you want to keep the setup code concise and straightforward.

3. Thread Safety
Static methods are inherently thread-safe when they do not modify any shared state. Since the methods used for mapping endpoints typically only configure routing and do not maintain any state, they are safe to use in a multi-threaded environment like a web server.

4. Clear Separation of Concerns
Using static methods can help to clearly separate the endpoint definitions from other application logic. This can make your code easier to understand and maintain, as the endpoint definitions are isolated in a dedicated static class.
