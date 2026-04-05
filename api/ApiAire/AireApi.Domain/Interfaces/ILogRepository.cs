using AireApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AireApi.Domain.Interfaces
{
    public interface ILogRepository
    {
        Task<IEnumerable<Log>> GetAllAsync();
        Task<int> CreateAsync(Log log);
    }
}
