using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using GPN.CR.TEST.Bussines.Interfaces;
using GPN.CR.TEST.Host.Models;
using Microsoft.AspNetCore.Mvc;

namespace GPN.CR.TEST.Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class CurrencyController : ControllerBase
    {
        private readonly ICurrencyDailyService _curDailySrv;
        public CurrencyController(ICurrencyDailyService curDailySrv)
        {
            _curDailySrv = curDailySrv;
        }

        // GET: api/Currency
        [HttpGet("{code}")]
        public ActionResult<IEnumerable<CurrencyDailyResult>> Get(string code, DateTime date)
        {
            if (date.Date > DateTime.Now.Date || date == DateTime.MinValue)
                date = DateTime.Now;
            var currencyDailyDtos = _curDailySrv.GetCurrencyByDateAndCode(code, date);
            var viewModels = new List<CurrencyDailyResult>();
            foreach (var currencyDailyDto in currencyDailyDtos)
            {
                var viewModel = new CurrencyDailyResult();
                viewModel.code = currencyDailyDto.CurrencyCode;
                viewModel.name = currencyDailyDto.CurrencyName;
                viewModel.date = currencyDailyDto.Date.ToString("yyyy-MM-dd");
                viewModel.value = currencyDailyDto.Value.ToString(CultureInfo.CurrentCulture);
                viewModels.Add(viewModel);
            }

            if (viewModels.Any())
            {
                return viewModels;
            }
            return BadRequest("Валюта отсутствует");
        }

    }
}
