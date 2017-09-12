using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MyWallet.Infra.Data.Context;
using MyWallet.Infra.Data.Contracts;

namespace MyWallet.Infra.Data.Repository.Shared
{
    public abstract class Repository<TEntity>: IRepository<TEntity> where TEntity:class
    {
        protected MyWalletContext Context;
        protected DbSet<TEntity> DbSet;

        protected Repository( MyWalletContext context)
        {
            Context = context;
            DbSet = Context.Set<TEntity>();
        }

        public void Dispose()
        {
            Context.Dispose();
            GC.SuppressFinalize(this);
        }

        public void Add(TEntity obj)
        {
            DbSet.Add(obj);
            Context.SaveChanges();
        }

        public TEntity GetById(Guid id) => DbSet.Find(id);

        public void Update(TEntity obj)
        {
            Context.Entry(obj).State = EntityState.Modified;
            Context.SaveChanges();
        }

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate) => DbSet.AsNoTracking().Where(predicate);
        
    }
}