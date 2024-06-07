using AutoMapper;
using pruebatecnica.Domain.DTOs;
using pruebatecnica.Domain.Entities;

namespace pruebatecnica.Application.Services;

public class AutoMapperService : Profile
{
    public AutoMapperService()
    {
        CreateMap<Customer, CreateCustomerDto>();

        CreateMap<Order, OrderDto>();
        CreateMap<OrderDto, Order>();

        CreateMap<OrderDetail, OrderDetailDto>();
        CreateMap<OrderDetailDto, OrderDetail>();

        CreateMap<ShoppingCart, ShoppingCartDto>();
        CreateMap<ShoppingCartDto, ShoppingCart>();

        CreateMap<Payment, PaymentDto>();
        CreateMap<PaymentDto, Payment>();
    }
}
