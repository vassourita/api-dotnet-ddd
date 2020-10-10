using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Api.Data.Context;
using Api.Domain.Entities;
using Api.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Api.Data.Repositories
{
    public class BaseRepository<T> : IRepository<T> where T : BaseEntity
    {
        protected readonly MyContext _Context;
        private DbSet<T> _DataSet;

        public BaseRepository(MyContext context)
        {
            _Context = context;
            _DataSet = _Context.Set<T>();
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            try
            {
                var result = await _DataSet.SingleOrDefaultAsync(x => x.Id.Equals(id));
                if (result == null)
                    return false;

                _DataSet.Remove(result);
                await _Context.SaveChangesAsync();
            }
            catch (Exception err)
            {
                throw err;
            }

            return true;
        }

        public async Task<T> InsertAsync(T item)
        {
            try
            {
                if (item.Id == Guid.Empty)
                {
                    item.Id = Guid.NewGuid();
                }
                item.CreatedAt = DateTime.UtcNow;

                _DataSet.Add(item);
                await _Context.SaveChangesAsync();
            }
            catch (Exception err)
            {
                throw err;
            }

            return item;
        }

        public async Task<bool> ExistAsync(Expression<Func<T, bool>> predicate) => await _DataSet.AnyAsync(predicate);


        public async Task<T> SelectAsync(Guid id)
        {
            try
            {
                return await _DataSet.SingleOrDefaultAsync(x => x.Id.Equals(id));
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public async Task<IEnumerable<T>> SelectAsync()
        {
            try
            {
                return await _DataSet.ToListAsync();
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public async Task<T> UpdateAsync(T item)
        {
            try
            {
                var result = await _DataSet.SingleOrDefaultAsync(x => x.Id.Equals(item.Id));
                if (result == null)
                    return null;

                item.UpdatedAt = DateTime.UtcNow;
                item.CreatedAt = result.CreatedAt;

                _Context.Entry(result).CurrentValues.SetValues(item);
                await _Context.SaveChangesAsync();
            }
            catch (Exception err)
            {
                throw err;
            }

            return item;
        }
    }
}