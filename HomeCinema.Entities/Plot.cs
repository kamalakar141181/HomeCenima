using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeCinema.Entities
{
    public class Plot : IEntityBase
    {
        public int ID { get; set; }
        public decimal PlotArea { get; set; }
        public bool IsAvailable { get; set; }
        public decimal Rate { get; set; }
        public decimal TotalAmount { get; set; }        
        public string PlotDetails { get; set; }       
    }
}
