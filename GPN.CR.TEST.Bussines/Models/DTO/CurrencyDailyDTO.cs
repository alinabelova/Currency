using System;

namespace GPN.CR.TEST.Bussines.Models.DTO
{
    public class CurrencyDailyDTO
    {
        public int Id { get; set; }
        public string CurrencyExtId { get; set; }
        public string CurrencyName { get; set; }
        public string CurrencyCode { get; set; }
        public double Value { get; set; }
        public DateTime Date { get; set; }
    }
}
