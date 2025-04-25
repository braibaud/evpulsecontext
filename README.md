# EVPulseContext

## Project Overview
**EVPulseContext** is a .NET library designed to provide a robust and efficient data access layer for applications using Entity Framework Core with PostgreSQL. It simplifies database interactions and ensures scalability and maintainability for modern .NET applications.

This library is built with the following features:
- **Entity Framework Core**: Leverages EF Core for ORM capabilities.
- **PostgreSQL Support**: Optimized for PostgreSQL databases.
- **.NET 9.0 Compatibility**: Targets the latest .NET runtime for cutting-edge performance.
- **Nullable Reference Types**: Ensures safer code with nullable reference type support.

## How to Use

### Prerequisites
- .NET 9.0 SDK or later installed on your machine.
- A PostgreSQL database instance.

### Installation
To use **EVPulseContext** in your project, add it as a dependency. If published as a NuGet package, you can install it using:

```bash
dotnet add package EVPulseContext
```

### Configuration
1. Add the necessary `DbContext` configuration in your application:
```csharp
using Microsoft.EntityFrameworkCore;
using EVPulseContext;

public class AppDbContext : EVPulseContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
}
```

2. Configure the connection string in your application settings:
```json
{
    "ConnectionStrings": 
    {
        "DefaultConnection": "Host=localhost;Database=mydb;Username=myuser;Password=mypassword"
    }
}
```

3. Register the `DbContext` in your `Startup.cs` or `Program.cs`:
```csharp
builder.Services.AddDbContext<AppDbContext>(options =>
options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));
```

### Usage
Once configured, you can use the `DbContext` to interact with your database:
```csharp
using var context = new AppDbContext(options);
var data = await context.MyEntities.ToListAsync();
```
