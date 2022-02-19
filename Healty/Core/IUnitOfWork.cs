using Healty.Core.Dtos;
using Healty.Core.Models;
using Healty.Core.Repositories;

using System;
using System.Threading.Tasks;

namespace Healty.Core
{
    public interface IUnitOfWork : IDisposable
    {
        IBaseRepository<Todo> Todos { get; }

        Task<int> CompleteAsync();
    }
}