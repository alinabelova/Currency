using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GPN.CR.TEST.Data.Models
{
    public class CurrencyDaily
    {
        public int Id { get; set; }

        [Required]
        public Currency Currency { get; set; }

        public double Value { get; set; }
        
        [Required]
        [Column(TypeName = "date")]
        public DateTime Date { get; set; }
    }
}
