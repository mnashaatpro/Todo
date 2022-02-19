using Healty.Core;
using Healty.Core.Models;
using Healty.Core.Repositories;
using Healty.Persistence.Repositories;

using System.Threading.Tasks;

namespace Healty.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        //public IBookRepository Books { get; private set; }
        //public IBaseRepository<Movie> Movies { get; private set; }

        public IBaseRepository<Todo> Todos { get; private set; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Todos = new BaseRepository<Todo>(_context);
        }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}