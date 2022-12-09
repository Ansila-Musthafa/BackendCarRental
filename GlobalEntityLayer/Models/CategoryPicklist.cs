using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace GlobalEntityLayer.Models
{
    public class CategoryPicklist
    {

        [Key]
        public int ID { get; set; } 
        [MaxLength(20)]
        public string Category { get; set; }

        public ICollection<CarDetails> CarDetails { get; set; }

    }
}
