using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GPN.CR.TEST.Data.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<List<T>> GetAll();
        Task<T> Get(int id);
        IEnumerable<T> Find(Func<T, bool> predicate);
        Task<T> Create(T item);
        Task Update(T item);
        Task Delete(int id);
    }
}
