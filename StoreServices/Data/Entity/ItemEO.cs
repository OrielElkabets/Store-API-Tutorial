using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreServices.Data.Entity
{
    [Table("Items")]
    public class ItemEO
    {
        [Key]
        public int? Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public double? Price { get; set; }

        [Column("Qty")]
        public int? Quantity { get; set; }
    }
}
