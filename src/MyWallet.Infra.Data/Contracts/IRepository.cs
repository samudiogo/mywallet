using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace MyWallet.Infra.Data.Contracts
{
    public interface IRepository<TEntity> : IDisposable where TEntity : class
    {
        void Add(TEntity obj);
        TEntity GetById(Guid id);
        void Update(TEntity obj);
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);
        
    }
}
