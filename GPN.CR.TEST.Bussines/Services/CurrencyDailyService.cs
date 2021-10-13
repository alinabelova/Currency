using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using GPN.CR.TEST.Bussines.Interfaces;
using GPN.CR.TEST.Bussines.Models.DTO;
using GPN.CR.TEST.Data.Interfaces;
using GPN.CR.TEST.Data.Models;
using Microsoft.Extensions.Logging;

namespace GPN.CR.TEST.Bussines.Services
{
    public class CurrencyDailyService: ICurrencyDailyService
    {
        private readonly IMapper _mapper;
        private readonly IRepository<CurrencyDaily> _curDailyRepo;
        private readonly IRepository<Currency> _curRepo;
        private readonly ILogger<CurrencyDailyService> _logger;

        public CurrencyDailyService(IRepository<CurrencyDaily> curDailySrv, IRepository<Currency> curSrv,
            IMapper mapper, ILogger<CurrencyDailyService> logger)
        {
            _mapper = mapper;
            _curDailyRepo = curDailySrv;
            _curRepo = curSrv;
            _logger = logger;
        }

        public async Task UpdateCurrencies(List<CurrencyDailyDTO> currencyDailyDtos)
        {
            try
            {
                var allCurrencies = await _curRepo.GetAll();
                foreach (var currencyDailyDto in currencyDailyDtos)
                {
                    var existCurrency = allCurrencies.FirstOrDefault(c => c.ExtId == currencyDailyDto.CurrencyExtId);
                    if (existCurrency == null)
                    {
                        var newCurrency = new Currency
                        {
                            ExtId = currencyDailyDto.CurrencyExtId,
                            Name = currencyDailyDto.CurrencyName,
                            Code = currencyDailyDto.CurrencyCode
                        };
                        existCurrency = await _curRepo.Create(newCurrency);
                    }

                    if (_curDailyRepo.Find(c =>
                        c.Date.Date == currencyDailyDto.Date.Date && c.Currency.Id == existCurrency.Id).Any())
                    {
                        continue;
                    }

                    var currencyDaily = _mapper.Map<CurrencyDaily>(currencyDailyDto);
                    currencyDaily.Currency = existCurrency;
                    await _curDailyRepo.Create(currencyDaily);
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Произошла ошибка в методе {nameof(UpdateCurrencies)}");
            }
        }

        public IEnumerable<CurrencyDailyDTO> GetCurrencyByDateAndCode(string code, DateTime date)
        {
            if (String.IsNullOrEmpty(code))
                throw new ArgumentException("Не заполнен параметр code");
            code = code.ToLower();
            try
            {
                var curDaily = _curDailyRepo.Find(t => t.Date.Date == date.Date && t.Currency.Code.ToLower() == code);
                return _mapper.Map<IEnumerable<CurrencyDailyDTO>>(curDaily);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Произошла ошибка в методе {nameof(GetCurrencyByDateAndCode)} ({code}, {date})");
                return new List<CurrencyDailyDTO>();
            }
        }
    }
}
