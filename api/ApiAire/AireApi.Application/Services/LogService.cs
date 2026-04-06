using AireApi.Domain.Entities;
using AireApi.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AireApi.Application.Services
{
    public class LogService : ILogService
    {
        private readonly ILogRepository _repo;

        public LogService(ILogRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<Log>> GetAllAsync()
            => await _repo.GetAllAsync();

        public async Task<int> CreateAsync(Log log)
            => await _repo.CreateAsync(log);
    }
}
