using Common.Wrappers;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Addresses.Core.Interfaces.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<List<TEntity>> GetAll();
        Task<List<TEntity>> GetAll(Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include);
        Task<List<TEntity>> GetBy(Expression<Func<TEntity, bool>> filter);
        Task<List<TEntity>> GetBy(Expression<Func<TEntity, bool>> filter, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include);
        Task<TEntity> GetOneBy(Expression<Func<TEntity, bool>> filter, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include);
        Task<TEntity> Add(TEntity entity);
        Task<TEntity> Update(TEntity entity);

        //add
        Task<PagedResponse<IReadOnlyList<TEntity>>> GetPagedResponseAsync(int pageNumber, int pageSize, Expression<Func<TEntity, bool>> expression, string include = "");

        Task<TEntity> GetOneByAsync(Expression<Func<TEntity, bool>> filter);


        //Post
        Task<TEntity> AddAsync(TEntity entity);
        Task<TEntity> UpdateAsync(TEntity entity);
    }
}
