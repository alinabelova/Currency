using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GPN.CR.TEST.Data.EF;
using GPN.CR.TEST.Data.Interfaces;
using GPN.CR.TEST.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace GPN.CR.TEST.Data.Repositories
{
    public class CurrencyRepository: IRepository<Currency>
    {
        private readonly ApplicationContext _db;

        public CurrencyRepository(ApplicationContext context)
        {
            _db = context;
        }

        public async Task<List<Currency>> GetAll()
        {
            return await _db.Currencies.ToListAsync();
        }

        public async Task<Currency> Get(int id)
        {
            return await _db.Currencies.FindAsync(id);
        }

        public IEnumerable<Currency> Find(Func<Currency, bool> predicate)
        {
            return _db.Currencies.Where(predicate).ToList();
        }

        public async Task<Currency> Create(Currency item)
        {
            await _db.Currencies.AddAsync(item);
            await _db.SaveChangesAsync();
            return item;
        }

        public async Task Update(Currency item)
        {
            _db.Entry(item).State = EntityState.Modified;
            await _db.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var currency = await Get(id);
            if (currency != null)
            {
                _db.Currencies.Remove(currency);
                await _db.SaveChangesAsync();
            }
        }
    }
}
