using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeCinema.Entities
{
    
    public class TransactionModeEntity : IEntityBase
    {
        public TransactionModeEntity()
        {
            
        }
        public int ID { get; set; }             
        public string TransactionModeName { get; set; }
        public string TransactionModeDescription { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public bool DeleteFlag { get; set; }
        public byte[] RowVersion { get; }
  
    }
}
