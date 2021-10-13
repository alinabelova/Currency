using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GPN.CR.TEST.Bussines.Models.DTO;

namespace GPN.CR.TEST.Bussines.Interfaces
{
    public interface ICurrencyDailyService
    {
        Task UpdateCurrencies(List<CurrencyDailyDTO> currencyDailyDtos);
        IEnumerable<CurrencyDailyDTO> GetCurrencyByDateAndCode(string code, DateTime date);
    }
}
