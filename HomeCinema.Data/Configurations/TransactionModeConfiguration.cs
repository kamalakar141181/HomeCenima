using HomeCinema.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeCinema.Data.Configurations
{
    class TransactionModeConfiguration : EntityBaseConfiguration<TransactionModeEntity>
    {
        public TransactionModeConfiguration()
        {
            Property(tmc => tmc.ID).IsRequired();
            Property(tmc => tmc.TransactionModeName).IsRequired();
            Property(tmc => tmc.TransactionModeDescription).IsRequired();
            Property(tmc => tmc.CreatedBy).IsRequired();
            Property(tmc => tmc.CreatedDate).IsRequired();
            Property(tmc => tmc.ModifiedBy).IsRequired();
            Property(tmc => tmc.ModifiedDate).IsRequired();
            Property(tmc => tmc.DeleteFlag).IsRequired();
            //Property(tmc => tmc.RowVersion).HasMaxLength(200);

        }
    }
}
