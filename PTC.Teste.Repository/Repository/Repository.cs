using Microsoft.EntityFrameworkCore;
using PTC.Teste.Entity;
using PTC.Teste.Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;

namespace PTC.Teste.Repository.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly DbContext _dbcontext;

        private DbSet<T> DbSet
        {
            get { return _dbcontext.Set<T>(); }
        }

        public Repository(DbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public DbContext GetContext()
        {
            return _dbcontext;
        }

        public string Insert(T entity)
        {
            DbSet.Add(entity);
            return this.SaveChanges();
        }

        public string Update(T entity)
        {
            if (_dbcontext.Entry(entity).State == EntityState.Detached)
            {
                DbSet.Attach(entity);
            }
            _dbcontext.Entry<T>(entity).State = EntityState.Modified;
            return this.SaveChanges();
        }

        public string Delete(T entity)
        {
            DbSet.Remove(entity);
            return this.SaveChanges();
        }

        public string Delete(int id)
        {
            T entity = DbSet.Find(id);
            if (entity != null)
            {
                DbSet.Remove(entity);
                return this.SaveChanges();
            }
            else
                return "";
        }

        public void Cancel(T entity)
        {
            _dbcontext.Entry<T>(entity).Reload();
        }

        public string JoinEntity<TEntity>(ICollection<TEntity> list, string s) where TEntity : EntityBase
        {
            if (list != null)
            {
                DbSet.Include(typeof(TEntity).Name).Load();

                while (list.Count != 0)
                {
                    list.Remove(list.ToArray()[0]);
                }

                if (!string.IsNullOrWhiteSpace(s))
                {
                    foreach (string id in s.Split('|'))
                    {
                        if (id != "")
                        {
                            var item = _dbcontext.Set<TEntity>().Find(Convert.ToInt32(id));
                            if (item != null) list.Add(item);
                        }
                    }
                }

                return this.SaveChanges();
            }
            else
                return "";
        }

        public IQueryable<T> Filter(string condition)
        {
            return DbSet.Where(condition);
        }

        public IQueryable<T> SearchFor(Expression<Func<T, bool>> predicate)
        {
            return DbSet.Where(predicate);
        }

        public IQueryable<T> GetAll()
        {
            return DbSet;
        }

        public IQueryable<T> GetTop(int n)
        {
            return DbSet.Take(n);
        }

        public T GetById(int id)
        {
            return DbSet.Find(id);
        }

        public int GetCount()
        {
            return DbSet.Count();
        }

        public string DeleteRange(List<T> entities)
        {
            DbSet.RemoveRange(entities);
            return this.SaveChanges();
        }

        public string SaveChanges()
        {
            try
            {
                _dbcontext.SaveChanges();
                return "";
            }
            catch (Exception erro)
            {
                if (erro.InnerException != null)
                {
                    if (erro.InnerException.InnerException != null)
                        return erro.InnerException.InnerException.Message;
                    else
                        return erro.InnerException.Message;
                }
                else
                    return erro.Message;
            }
        }
    }
}
