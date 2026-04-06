using AireApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AireApi.Application.Services
{
    public interface ILogService
    {
        Task<IEnumerable<Log>> GetAllAsync();
        Task<int> CreateAsync(Log log);
    }
}
