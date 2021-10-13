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
    public class CurrencyDailyRepository: IRepository<CurrencyDaily>
    {
        private readonly ApplicationContext _db;

        public CurrencyDailyRepository(ApplicationContext context)
        {
            _db = context;
        }

        public async Task<List<CurrencyDaily>> GetAll()
        {
            return await _db.CurrencyDaily.ToListAsync();
        }

        public async Task<CurrencyDaily> Get(int id)
        {
            return await _db.CurrencyDaily.FindAsync(id);
        }

        public IEnumerable<CurrencyDaily> Find(Func<CurrencyDaily, bool> predicate)
        {
            return _db.CurrencyDaily.Include(c=>c.Currency).Where(predicate).ToList();
        }

        public async Task<CurrencyDaily> Create(CurrencyDaily item)
        {
            await _db.CurrencyDaily.AddAsync(item);
            await _db.SaveChangesAsync();
            return item;
        }

        public async Task Update(CurrencyDaily item)
        {
            _db.Entry(item).State = EntityState.Modified;
            await _db.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var currency = await Get(id);
            if (currency != null)
            {
                _db.CurrencyDaily.Remove(currency);
                await _db.SaveChangesAsync();
            }
        }
    }
}
