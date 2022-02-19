using Healty.Core;
using Healty.Core.Repositories;
using Healty.Persistence.Repositories;

using System.Collections.Generic;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Web;
using System.Linq.Dynamic;
namespace Healty.Persistence
{

    public class BaseRepository<T> : IBaseRepository<T> where T : class, IEntityBase, new()
    {
        private readonly ApplicationDbContext _context;

        public BaseRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<DataTableHeader<T>> GetDataTable(Expression<Func<T, bool>> criteria, int? take = null, int? skip = null,
                OrderByDirection orderByDirection = OrderByDirection.OrderByAsc,
                string SortColumnAndDirection = null,
                params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _context.Set<T>();
            query = includeProperties.Aggregate
                (query, (current, includeProperty) => current.Include(includeProperty));

            query = criteria == null ? query : query.Where(criteria);

            if (SortColumnAndDirection != null)
            {
                try
                {
                    query = query.OrderBy(SortColumnAndDirection);
                }
                catch (Exception ex)
                {
                    query = query.OrderBy("Id");
                }
            }

            var dataTableHeader = new DataTableHeader<T>();
            try
            {
                dataTableHeader.Count = query.Count();
            }
            catch (Exception ex)
            {
            }

            if (skip.HasValue) query = query.Skip(skip.Value);
            if (take.HasValue) query = query.Take(take.Value);

            //return await query.ToListAsync();


            try
            {
                dataTableHeader.DataList = await query.ToListAsync();
            }
            catch (Exception ex)
            {
            }


            return dataTableHeader;

        }

        public T Add(T entity)
        {
            _context.Set<T>().Add(entity);
            // await _context.SaveChangesAsync();
            return entity;
        }

        public IEnumerable<T> AddRangeAsync(IEnumerable<T> entities)
        {
            _context.Set<T>().AddRange(entities);
            //await _context.SaveChangesAsync();
            return entities;
        }

        public void Attach(T entity)
        {
            throw new NotImplementedException();
        }

        public int Count()
        {
            return _context.Set<T>().Count();
        }

        public int Count(Expression<Func<T, bool>> criteria)
        {
            return _context.Set<T>().Count(criteria);
        }

        public void Delete(T entityToDelete)
        {
            var dbSet = _context.Set<T>();
            if (_context.Entry(entityToDelete).State == EntityState.Detached)
            {
                dbSet.Attach(entityToDelete);
            }
            dbSet.Remove(entityToDelete);

        }
        public void DeleteRange(IEnumerable<T> entities)
        {
            _context.Set<T>().RemoveRange(entities);
        }

        public virtual void Update(T entityToUpdate)
        {
            _context.Set<T>().Attach(entityToUpdate);
            _context.Entry(entityToUpdate).State = EntityState.Modified;
        }

        /*public async Task DeleteAsync(int id)
        {
            var entity = await _context.Set<T>().FirstOrDefaultAsync(n => n.Id == id);
            EntityEntry entityEntry = _context.Entry<T>(entity);

            entityEntry.State = EntityState.Deleted;

            //await _context.SaveChangesAsync();
        }*/

        public T Find(Expression<Func<T, bool>> criteria, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _context.Set<T>();
            query = includeProperties.Aggregate
                (query, (current, includeProperty) => current.Include(includeProperty));
            return query.FirstOrDefault(criteria);
        }

        public IEnumerable<T> FindAll(Expression<Func<T, bool>> criteria, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _context.Set<T>();
            query = includeProperties.Aggregate
                (query, (current, includeProperty) => current.Include(includeProperty));
            return query.Where(criteria).ToList();
        }

        public async Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> criteria, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _context.Set<T>();
            query = includeProperties.Aggregate
                (query, (current, includeProperty) => current.Include(includeProperty));
            return await query.Where(criteria).ToListAsync();
        }

        public async Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> criteria, int? take = null, int? skip = null, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _context.Set<T>();
            query = includeProperties.Aggregate
                (query, (current, includeProperty) => current.Include(includeProperty));

            if (skip != null) query = query.Skip(skip.Value);
            if (take != null) query = query.Take(take.Value);

            return await query.Where(criteria).ToListAsync();
        }

        public async Task<IEnumerable<T>> FindAllAsync(
            Expression<Func<T, bool>> criteria, int? take = null, int? skip = null,
            OrderByDirection orderByDirection = OrderByDirection.OrderByAsc,
            Expression<Func<T, object>>[] orderBys = null,
            params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _context.Set<T>();
            query = includeProperties.Aggregate
                (query, (current, includeProperty) => current.Include(includeProperty));

            query = query.Where(criteria);

            if (skip.HasValue) query = query.Skip(skip.Value);
            if (take.HasValue) query = query.Take(take.Value);

            if (orderBys != null)
            {
                foreach (var orderBy in orderBys)
                {
                    if (orderByDirection == OrderByDirection.OrderByAsc)
                        query = query.OrderBy(orderBy);
                    else query = query.OrderByDescending(orderBy);
                }
            }
            return await query.ToListAsync();
        }

        public async Task<T> FindAsync(Expression<Func<T, bool>> criteria, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _context.Set<T>();
            query = includeProperties.Aggregate
                (query, (current, includeProperty) => current.Include(includeProperty));
            return await query.FirstOrDefaultAsync(criteria);
        }

        public IEnumerable<T> GetAll()
        //Expression<Func<T, bool>> criteria, params Expression<Func<T, object>>[] includeProperties)
        {
            // IQueryable<T> query = _context.Set<T>();
            //query = includeProperties.Aggregate
            //    (query, (current, includeProperty) => current.Include(includeProperty));
            //return query.Where(criteria).ToList();

            return _context.Set<T>().ToList();
        }

        public async Task<IEnumerable<T>> GetAllOrderTestAsync(
          params Expression<Func<T, object>>[] orderBys)

        {
            return await _context.Set<T>().ToListAsync();

        }
        public async Task<IEnumerable<T>> GetAllAsync(int? take = null, int? skip = null,
            OrderByDirection orderByDirection = OrderByDirection.OrderByAsc,
            Expression<Func<T, object>> orderBys = null,
            params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _context.Set<T>();
            query = includeProperties.Aggregate
                (query, (current, includeProperty) => current.Include(includeProperty));


            if (skip.HasValue) query = query.Skip(skip.Value);
            if (take.HasValue) query = query.Take(take.Value);

            if (orderBys != null)
            {
                //foreach (var orderBy in orderBys)
                //{
                if (orderByDirection == OrderByDirection.OrderByAsc)
                    query = query.OrderBy(orderBys);
                else query = query.OrderByDescending(orderBys);
                //}
            }
            return await query.ToListAsync();
        }

        public T GetById(int id, bool AsNoTracking = true)
        {
            if (AsNoTracking)
                return _context.Set<T>().AsNoTracking().FirstOrDefault(x => x.Id == id);

            return _context.Set<T>().FirstOrDefault(x => x.Id == id);

        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _context.Set<T>().AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }

        //public T Update(T entity)
        //{
        //    _context.Set<T>().Attach(entity);
        //    var entry = _context.Set<T>().EEntry(updatedUser);

        //    _context.Set<T>().Update(entity);
        //    return entity;
        //}


        //public async Task<IEnumerable<SelectListItem>> GetSelectListCategoriesAsync(Func<T, object> properties ,
        //    Expression<Func<T, bool>> criteria = null ) =>
        //    await _context.Set<T>().Where(criteria)
        //        .Select(c => new SelectListItem
        //        {
        //            Text = c.properties,
        //            Value = c.Id.ToString()
        //        }).ToListAsync();
    }

    public class DataTableHeader<T>
    {
        public int Count { get; set; }
        public IEnumerable<T> DataList { get; set; }
    }

    public enum OrderByDirection
    {
        OrderByAsc,
        OrderByDescending
    }
}