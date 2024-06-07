# pt-dti-sd-002
Prueba técnica Junior Backend Software Engineer

## Conexión a Base de Datos MySQL

La aplicación utiliza MySQL como sistema de gestión de base de datos. Los siguientes pasos detallan cómo configurar la base de datos:

### 1. Configuración de conexión
Edite el archivo appsettings.json para incluir la cadena de conexión de SQL Server en la sección correspondiente:

```
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=fashionstore;User=;Password=;"
  },
}
```

### 2. Migraciones de Base de Datos (Más información)
Para crear migraciones y luego aplicarlas a la base de datos, puedes utilizar los siguientes comandos:

```
dotnet ef migrations add NewMigration
dotnet ef database update
```

## Arbol de directorios basado en los conceptos de Clean Architecture
```
pruebatecnica.Api
|__ Controllers
    |__ CustomerController.cs
    |__ OrderController.cs
    |__ OrderDetailController.cs
    |__ PaymentController.cs
    |__ ProductController.cs
    |__ SalesController.cs
    |__ ShoppingCartController.cs
|__ Program.cs
|__ Properties
    |__ launchSettings.json
|__ appsettings.Development.json
|__ appsettings.json
|__ pruebatecnica.Api.csproj
|__ pruebatecnica.http

pruebatecnica.Application
|__ Interfaces
    |__ ICustomerRepository.cs
    |__ IOrderDetailRepository.cs
    |__ IOrderRepository.cs
    |__ IPaymentRepository.cs
    |__ IProductRepository.cs
    |__ IShoppingCartRepository.cs
    |__ Services
        |__ IOrderService.cs
        |__ IShoppingCartService.cs
|__ Services
    |__ AutoMapperService.cs
    |__ OrderService.cs
    |__ ShoppingCartService.cs
|__ pruebatecnica.Application.csproj

pruebatecnica.Core
|__ DTOs
    |__ CreateCustomerDto.cs
    |__ LoginCustomerDto.cs
    |__ OrderDetailDto.cs
    |__ OrderDto.cs
    |__ PaymentDto.cs
    |__ ProductDto.cs
    |__ Sales
        |__ AddShoppingCartDto.cs
        |__ OrderDetailsResultDto.cs
        |__ OrderResultDto.cs
    |__ ShoppingCartDto.cs
|__ Entities
    |__ Customer.cs
    |__ Order.cs
    |__ OrderDetail.cs
    |__ Payment.cs
    |__ Product.cs
    |__ ShoppingCart.cs
|__ Enums
    |__ StatusOrder.cs
|__ GlobalUsings.cs
|__ Wrapper
    |__ ApiResponse.cs
|__ pruebatecnica.Domain.csproj

pruebatecnica.Infrastructure
|__ Data
    |__ AppDbContext.cs
    |__ Migrations
        |__ 20240607151509_newMigration.Designer.cs
        |__ 20240607151509_newMigration.cs
        |__ AppDbContextModelSnapshot.cs
|__ Repositories
    |__ CustomerRepository.cs
    |__ OrderDetailRepository.cs
    |__ OrderRepository.cs
    |__ PaymentRepository.cs
    |__ ProductRepository.cs
    |__ ShoppingCartRepository.cs
|__ pruebatecnica.Infrastructure.csproj
```