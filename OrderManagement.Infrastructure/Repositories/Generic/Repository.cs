using Microsoft.EntityFrameworkCore;
using OrderManagement.Contracts.Data.Repositories;
using OrderManagement.Contracts.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Infrastructure.Repositories.Generic
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly SouthWestTradersDbContext _context;
        private readonly DbSet<T> _dbSet;

        public Repository(SouthWestTradersDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task<T> AddAsync(T entity)
        {
            await _context.AddAsync(entity);
            return entity;
        }

        public async Task<int> CountAsync()
        {
            return await _dbSet.CountAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = _dbSet.Find(id);
            if (entity != null)
            {
                if (_context.Entry(entity).State == EntityState.Detached)
                {
                    _dbSet.Attach(entity);
                }
                _dbSet.Remove(entity);
            }
            //_dbSet.Remove(entity);
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> expression)
        {
            var entity = await _dbSet.FirstOrDefaultAsync(expression);
            return entity;
        }

        public async Task<List<T>> ListAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<List<T>> ListAsync(Expression<Func<T, bool>> expression)
        {
            return await _dbSet.Where(expression).ToListAsync();
        }

        public async Task<T> UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            return await Task.FromResult(entity);
        }
    }
}
