using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GPN.CR.TEST.Data.Models
{
    public class Currency
    {
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(10)")]
        public string ExtId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(3)")]
        public string Code { get; set; }
    }
}
