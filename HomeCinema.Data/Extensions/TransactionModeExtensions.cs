using HomeCinema.Data.Repositories;
using HomeCinema.Entities;
using System;
using System.Linq;

namespace HomeCinema.Data.Extensions
{
    public static class TransactionModeExtensions
    {
        public static bool TransactionModeExists(this IEntityBaseRepository<TransactionModeEntity> transactionModeRepository, string transactionModeName)
        {
            bool _isTransactionModeExists = false;

            _isTransactionModeExists = transactionModeRepository.GetAll()
                .Any(c => c.TransactionModeName == transactionModeName);

            return _isTransactionModeExists;
        }

        

    }
}
