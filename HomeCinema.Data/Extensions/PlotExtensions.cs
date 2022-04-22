using HomeCinema.Data.Repositories;
using HomeCinema.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeCinema.Data.Extensions
{
    public static class PlotExtensions
    {
        public static bool PlotExists(this IEntityBaseRepository<Plot> plotRepository, int plotID)
        {
            bool _plotExists = false;

            _plotExists = plotRepository.GetAll()
                .Any(p => p.ID == plotID);

            return _plotExists;
        }

        
    }
}

