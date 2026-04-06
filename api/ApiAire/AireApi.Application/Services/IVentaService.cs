using AireApi.Application.DTOs;
using AireApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AireApi.Application.Services
{
    public interface IVentaService
    {
        Task<IEnumerable<Venta>> GetAllAsync();
        Task<Venta?> GetByIdAsync(int id);
        Task<VentaResult?> RegistrarVentaAsync(RegistrarVentaDto dto);
    }
}
