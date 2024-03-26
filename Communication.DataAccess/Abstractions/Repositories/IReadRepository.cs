using Communication.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Communication.DataAccess.Abstractions.Repositories
{
    public interface IReadRepository<T> : IRepository<T> where T : BaseEntity
    {
        IQueryable<T> getAll(bool tracking = true);
        IQueryable<T> getWhere(Expression<Func<T, bool>> method, bool tracking = true);
        Task<T> getSingleAsync(Expression<Func<T, bool>> method, bool tracking = true);
        Task<T> getByIdAsync(string id, bool tracking = true);
    }
}
