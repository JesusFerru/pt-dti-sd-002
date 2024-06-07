using AutoMapper;
using Microsoft.EntityFrameworkCore;
using pruebatecnica.Application.Interfaces;
using pruebatecnica.Domain.DTOs;
using pruebatecnica.Domain.Entities;
using pruebatecnica.Infrastructure.Data;

namespace pruebatecnica.Infrastructure.Repositories
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public PaymentRepository(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PaymentDto>> GetAsync()
        {
            try
            {
                var data = await _context.payments.ToListAsync();
                var response = data.Select(x => _mapper.Map<PaymentDto>(x)).ToList();
                return response;
            }
            catch (Exception e)
            {
                throw new Exception($"Error in GetAsync: {e.Message}");
            }
        }

        public async Task<PaymentDto> GetById(int id)
        {
            try
            {
                var data = await _context.payments
                .FirstOrDefaultAsync(x => x.PaymentId == id)
                 ?? throw new Exception($"Payment with Id '{id}' not found");

                var response = _mapper.Map<PaymentDto>(data);
                return response;
            }
            catch (Exception e)
            {
                throw new Exception($"Error in GetById: {e.Message}");
            }
        }

        public async Task<PaymentDto> CreateAsync(PaymentDto paymentDto)
        {
            try
            {
                var order = await _context.orders.FindAsync(paymentDto.OrderId)
                    ?? throw new Exception("Order not found");

                var customer = await _context.customers.FindAsync(paymentDto.CustomerId)
                    ?? throw new Exception("Customer not found");

                var data = _mapper.Map<Payment>(paymentDto);

                data.Order = order;
                data.Customer = customer;

                using (var transaction = await _context.Database.BeginTransactionAsync())
                {
                    try
                    {
                        _context.payments.Add(data);
                        await _context.SaveChangesAsync();

                        // Confirmar la transacción
                        await transaction.CommitAsync();
                    }
                    catch (Exception)
                    {
                        // Deshacer la transacción en caso de error o proceso incompleto
                        await transaction.RollbackAsync();
                        throw;
                    }
                }

                // Mapear la entidad creada de vuelta al DTO
                var response = _mapper.Map<PaymentDto>(data);
                return response;
            }
            catch (Exception e)
            {
                throw new Exception($"Error in CreateAsync: {e.Message}");
            }
        }

        public async Task<PaymentDto> UpdateAsync(int id, PaymentDto newData)
        {
            try
            {
                var data = await _context.payments.FindAsync(id)
                    ?? throw new Exception($"Payment with Id '{id}' not found");

                _mapper.Map(newData, data);

                data.Order = await _context.orders.FindAsync(newData.OrderId)
                     ?? throw new Exception("Order not found");

                data.Customer = await _context.customers.FindAsync(newData.CustomerId)
                     ?? throw new Exception("Customer not found");

                using (var transaction = await _context.Database.BeginTransactionAsync())
                {
                    try
                    {
                        //Actualizar los datos en el objeto "data" obtenido
                        _context.payments.Update(data);
                        await _context.SaveChangesAsync();
                        //Confirmar la transacción
                        await transaction.CommitAsync();
                    }
                    catch (Exception)
                    {
                        // Deshacer la transacción en caso de error o proceso incompleto
                        await transaction.RollbackAsync();
                        throw;
                    }
                }

                var response = _mapper.Map<PaymentDto>(data);
                return response;
            }
            catch (Exception e)
            {
                throw new Exception($"Error in UpdateAsync: {e.Message}");
            }
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                var data = await _context.payments.FindAsync(id)
                    ?? throw new Exception($"Payment with Id '{id}' not found");

                _context.payments.Remove(data);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw new Exception($"Error in DeleteAsync: {e.Message}");
            }
        }
    }
}
