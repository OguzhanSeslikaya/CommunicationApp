using Communication.DataAccess.Abstractions.Repositories;
using Communication.Entity;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Communication.DataAccess.Contexts;

namespace Communication.DataAccess.Concretes.Repositories
{
    public class WriteRepository<T> : IWriteRepository<T> where T : BaseEntity
    {
        private readonly CommunicationAppDBContext _context;

        public WriteRepository(CommunicationAppDBContext context)
        {
            _context = context;
        }

        public DbSet<T> table => _context.Set<T>();

        public async Task<bool> addAsync(T model)
        {
            EntityEntry<T> entityEntry = await table.AddAsync(model);
            return entityEntry.State == EntityState.Added;
        }

        public async Task<bool> addRangeAsync(List<T> model)
        {
            await table.AddRangeAsync(model);
            return true;
        }

        public bool remove(T model)
        {
            EntityEntry<T> entityEntry = table.Remove(model);
            return entityEntry.State == EntityState.Deleted;
        }

        public async Task<bool> removeAsync(string id)
        {
            var model = await table.FirstOrDefaultAsync(d => d.id == id);
            if (model == null)
            {
                return false;
            }
            return remove(model);
        }

        public bool update(T model)
        {
            EntityEntry<T> entityEntry = table.Update(model);
            return entityEntry.State == EntityState.Modified;
        }

        public async Task<int> saveAsync()
            => await _context.SaveChangesAsync();
    }
}
