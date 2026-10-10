using Common.Wrappers;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Business.Core.Interfaces.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        
        Task RemoveAsync(TEntity entity);

        Task<PagedResponse<IReadOnlyList<TEntity>>> GetPagedResponseAsync(int pageNumber, int pageSize, Expression<Func<TEntity, bool>> expression, string include = "");

        Task<TEntity> GetOneByAsync(Expression<Func<TEntity, bool>> filter);
        

        //Post
        Task<TEntity> AddAsync(TEntity entity);
        Task<TEntity> UpdateAsync(TEntity entity);
    }
}
