using Healty.Persistence;

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Healty.Core
{
    public interface IBaseRepository<T> where T : class
    {
        Task<DataTableHeader<T>> GetDataTable(Expression<Func<T, bool>> criteria, int? take = null, int? skip = null,
            OrderByDirection orderByDirection = OrderByDirection.OrderByAsc,
            string SortColumnAndDirection = null,
            params Expression<Func<T, object>>[] includeProperties);

        T GetById(int id, bool AsNoTracking = true);
        Task<T> GetByIdAsync(int id);

        T Find(Expression<Func<T, bool>> criteria, params Expression<Func<T, object>>[] includeProperties);
        Task<T> FindAsync(Expression<Func<T, bool>> criteria, params Expression<Func<T, object>>[] includeProperties);
        IEnumerable<T> FindAll(Expression<Func<T, bool>> criteria, params Expression<Func<T, object>>[] includeProperties);
        Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> criteria, int? take = null, int? skip = null,
            OrderByDirection orderByDirection = OrderByDirection.OrderByAsc,
            Expression<Func<T, object>>[] orderBys = null,
            params Expression<Func<T, object>>[] includeProperties

            );

        IEnumerable<T> GetAll();
        Task<IEnumerable<T>> GetAllAsync(int? take = null, int? skip = null,
            OrderByDirection orderByDirection = OrderByDirection.OrderByAsc,
            Expression<Func<T, object>> orderBys = null,
            params Expression<Func<T, object>>[] includeProperties);


        Task<IEnumerable<T>> GetAllOrderTestAsync(
          params Expression<Func<T, object>>[] orderBys);

        T Add(T entity);

        IEnumerable<T> AddRangeAsync(IEnumerable<T> entities);


        //T Update(T entity);
        //Task DeleteAsync(int id);
        void Delete(T entity);
        void DeleteRange(IEnumerable<T> entities);
        void Attach(T entity);
        int Count();
        int Count(Expression<Func<T, bool>> criteria);

        //Task<IEnumerable<SelectListItem>> GetSelectListCategoriesAsync(Expression<Func<T, bool>> criteria = null);

    }
}