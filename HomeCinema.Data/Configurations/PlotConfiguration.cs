using HomeCinema.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeCinema.Data.Configurations
{
    public class PlotConfiguration : EntityBaseConfiguration<Plot>
    {
        public PlotConfiguration()
        {
            Property(m => m.ID).IsRequired();
            Property(m => m.PlotArea).IsRequired();
            Property(m => m.IsAvailable).IsRequired();
            Property(m => m.Rate).IsRequired();
            Property(m => m.PlotDetails).HasMaxLength(200);            
        }
    }
}
