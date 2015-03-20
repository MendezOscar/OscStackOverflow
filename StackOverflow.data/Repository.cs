using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace StackOverflow.data
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        internal StackOverflowContext context;
        internal DbSet<TEntity> DbSet;
        private bool set;

        public Repository(StackOverflowContext context)
        {
            this.context = context;
            this.DbSet = context.Set<TEntity>();
            set = false;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.set)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.set = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public virtual IEnumerable<TEntity> Get(
           Expression<Func<TEntity, bool>> filter = null,
           Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
           string includeProperties = "")
        {
            IQueryable<TEntity> query = DbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                return query.ToList();
            }
        }

        public TEntity GetWithFilter(Expression<Func<TEntity, bool>> filter)
        {

            return DbSet.FirstOrDefault(filter);
        }

        public virtual TEntity GetEntityById(object entityId)
        {
            return DbSet.Find(entityId);
        }

        public virtual void InsertEntity(TEntity entity)
        {
            DbSet.Add(entity);
        }

        public virtual void Delete(object id)
        {
            TEntity entityToDelete = DbSet.Find(id);
            Delete(entityToDelete);
        }
        public virtual void Delete(TEntity entityToDelete)
        {
            if (context.Entry(entityToDelete).State == EntityState.Detached)
            {
                DbSet.Attach(entityToDelete);
            }
            DbSet.Remove(entityToDelete);
        }

        public virtual void Update(TEntity entityToUpdate)
        {
            DbSet.Attach(entityToUpdate);
            context.Entry(entityToUpdate).State = EntityState.Modified;
        }
    }
}
