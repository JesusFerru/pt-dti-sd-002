﻿using Microsoft.AspNetCore.Mvc;
using pruebatecnica.Application.Interfaces;
using pruebatecnica.Domain.DTOs;
using pruebatecnica.Domain.Entities;
using pruebatecnica.Infrastructure.Repositories;

namespace pruebatecnica.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CustomerController : ControllerBase
{
    private readonly ICustomerRepository _customerRepository;

    public CustomerController(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Product>> GetProduct(int id)
    {
        try
        {
            var customer = await _customerRepository.GetCustomerById(id);
            return Ok(customer);
        }
        catch (Exception e)
        {
            return StatusCode(500, $"Internal server error: {e.Message}");
        }
    }

    [HttpPost("register")]
    public async Task<ActionResult<CreateCustomerDto>> CreateCustomer(CreateCustomerDto customerDto)
    {
        try
        {
            var createdCustomer = await _customerRepository.CreateAsync(customerDto);
            return Ok(createdCustomer);
        }
        catch (Exception e)
        {
            return StatusCode(500, $"Internal server error: {e.Message}");
        }
    }

    [HttpPost("login")]
    public async Task<ActionResult<Customer>> Login(LoginCustomerDto loginDto)
    {
        try
        {
            var customer = await _customerRepository.LoginAsync(loginDto);
            if (customer != null)
            {
                return Ok(customer);
            }
            else
            {
                return NotFound("Invalid email or password");
            }
        }
        catch (Exception e)
        {
            return StatusCode(500, $"Internal server error: {e.Message}");
        }
    }

}
