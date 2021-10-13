using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using GPN.CR.TEST.Bussines.Interfaces;
using GPN.CR.TEST.Bussines.Models;
using GPN.CR.TEST.Bussines.Models.DTO;
using GPN.CR.TEST.Cron.Interfaces;
using GPN.CR.TEST.Cron.XmlModels;

namespace GPN.CR.TEST.Cron
{
    public class LoadXmlCurrencyService: ILoadCurrencyService
    {
        private readonly ICurrencyDailyService _curDailySrv;
        private readonly MainConfig _mainConfig;

        public LoadXmlCurrencyService(ICurrencyDailyService curDailySrv, MainConfig mainConfig)
        {
            _curDailySrv = curDailySrv;
            _mainConfig = mainConfig;
        }

        public async Task SaveToDb()
        {
            string url = _mainConfig.CbrUrl;
            if(String.IsNullOrEmpty(url))
                throw new ArgumentException("Не заполнено свойсвто CbrUrl в файле appsettings.json");
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            var formatInfo = new NumberFormatInfo {CurrencyDecimalSeparator = ","};

            using (var client = new HttpClient())
            {
                var content = client.GetStreamAsync(url).Result;

                XmlSerializer serializer = new XmlSerializer(typeof(ValCurs));
                var valCurs = (ValCurs)serializer.Deserialize(content);
                DateTime date = DateTime.Parse(valCurs.Date);
                var currencies = valCurs.Valute;

                var curDailyDtos = new List<CurrencyDailyDTO>();
                foreach (var currency in currencies)
                {
                    var curDaily = new CurrencyDailyDTO();
                    curDaily.Date = date;
                    curDaily.CurrencyExtId = currency.Id;
                    curDaily.CurrencyCode = currency.CharCode;
                    curDaily.CurrencyName = currency.Name;
                    double.TryParse(currency.Value, NumberStyles.Currency, formatInfo, out var valueDouble);
                    curDaily.Value = valueDouble;
                    curDailyDtos.Add(curDaily);
                }

                if (curDailyDtos.Any())
                    await _curDailySrv.UpdateCurrencies(curDailyDtos);
            }
        }
    }
}
