
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace INNOVATEQ.DAL
{
    public interface IRepositoryBase<T>
    {
        IQueryable<T> FindAll();
        T FindById(int Id);
        IQueryable<T> Find(Expression<Func<T, bool>> expression);
        bool Any(Expression<Func<T, bool>> expression);
        T Create(T entity);
        Task<T> CreateAsync(T entity); 
        List<T> CreateList(List<T> entityLst);
        T Update(T entity);
        List<T> Update(List<T> entitylst);
        void Delete(T entity);

        IQueryable<T> FilterPaginationList(int skip, int take, string orderBy, bool? desc, Expression<Func<T, bool>> expression);
    }

}
