
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Linq.Dynamic.Core;
using INNOVATEQ.DATA.Models;
using INNOVATEQ.DATA.Context;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace INNOVATEQ.DAL
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class, IBaseEntity
    {
        protected INNOVATEQDBContext RepositoryContext { get; set; }

        public RepositoryBase(INNOVATEQDBContext repositoryContext)
        {
            this.RepositoryContext = repositoryContext;
        }

        public IQueryable<T> FindAll()
        {
            return this.RepositoryContext.Set<T>().AsNoTracking();
        }

        public IQueryable<T> Find(Expression<Func<T, bool>> expression)
        {
            return this.RepositoryContext.Set<T>().Where(expression).AsNoTracking();
        }

        public bool Any(Expression<Func<T, bool>> expression)
        {
            return this.RepositoryContext.Set<T>().Any(expression);
        }

        public T Create(T entity)
        {
            this.RepositoryContext.Set<T>().Add(entity);
            this.RepositoryContext.SaveChanges();
            return entity;
        }
        public async Task<T> CreateAsync(T entity)
        {
            await  this.RepositoryContext.Set<T>().AddAsync(entity);
            await this.RepositoryContext.SaveChangesAsync();
            return entity;
        }
        public T FindById(int Id)
        {
            return this.RepositoryContext.Set<T>().SingleOrDefault(x => x.Id == Id);
        }


        public List<T> CreateList(List<T> entityLst)
        {
            foreach (var item in entityLst)
            {
                this.RepositoryContext.Set<T>().Add(item);
            }

            this.RepositoryContext.SaveChanges();
            return entityLst;
        }

        public T Update(T entity)
        {
            this.RepositoryContext.Set<T>().Update(entity);
            this.RepositoryContext.SaveChanges();
            return entity;
        }

        public List<T> Update(List<T> entitylst)
        {
            foreach (var entity in entitylst)
            {
                this.RepositoryContext.Set<T>().Update(entity);
            }

            this.RepositoryContext.SaveChanges();
            return entitylst;
        }

        public void Delete(T entity)
        {
            this.RepositoryContext.Set<T>().Remove(entity);
            this.RepositoryContext.SaveChanges();
        }



        public IQueryable<T> FilterPaginationList(int skip, int take, string orderBy, bool? desc, Expression<Func<T, bool>> expression)
        {
            //var param = orderBy;
            //var propertyInfo = typeof(T).GetProperty(param);

            if (desc.Value)
            {
                orderBy = orderBy + " DESC";
            }
            else
            {
                orderBy = orderBy + " ASC";

            }

            var result = this.RepositoryContext.Set<T>();
            if (expression != null)
            {
                return result.Where(expression).OrderBy(orderBy).Skip(skip).Take(take).AsNoTracking();
            }
            return result.OrderBy(orderBy).Skip(skip).Take(take).AsNoTracking();
        }

        public string GetReflectedPropertyValue(object subject, string field)
        {
            object reflectedValue = subject.GetType().GetProperty(field).GetValue(subject, null);
            return reflectedValue != null ? reflectedValue.ToString() : "";
        }


        //public   IEnumerable<T> GetWithRawSql(string query,
        //params object[] parameters)
        //{
        //    return dbSet.SqlQuery(query, parameters).ToList();
        //}
    }

}
