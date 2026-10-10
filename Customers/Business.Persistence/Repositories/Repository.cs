using Business.Core.Interfaces.Repositories;
using Business.Persistence.Data;
using Common.Wrappers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Business.Persistence.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly DbSet<TEntity> _dbSet;
        private readonly ApplicationDbContext _context;
        public Repository(ApplicationDbContext context)
        {
            _dbSet = context.Set<TEntity>();
            _context = context;
        }
                
                
        public async Task<TEntity> GetOneByAsync(Expression<Func<TEntity, bool>> filter)
        {
            IQueryable<TEntity> entity = _dbSet;
            return await entity.Where(filter).FirstOrDefaultAsync();
        }

        public async Task RemoveAsync(TEntity entity)
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<PagedResponse<IReadOnlyList<TEntity>>> GetPagedResponseAsync(int pageNumber, int pageSize, Expression<Func<TEntity, bool>> expression = null, string include = "")
        {
            IQueryable<TEntity> query = _dbSet.AsNoTracking();
            if (expression != null) query = query.Where(expression);
            query = include.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
            int totalRecords = query.Count();
            query = query.Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .AsNoTracking();

            return new PagedResponse<IReadOnlyList<TEntity>>()
            {
                TotalRecords = totalRecords,
                TotalPage = totalRecords / pageSize,
                PageSize = pageSize,
                CurrentPage = pageNumber,
                Data = await query.ToListAsync()
            };
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
        public async Task<TEntity> AddAsync(TEntity entity)
        {
            _dbSet.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
