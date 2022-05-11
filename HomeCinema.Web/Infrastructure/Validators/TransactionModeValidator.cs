using FluentValidation;
using HomeCinema.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HomeCinema.Web.Infrastructure.Validators
{
    public class TransactionModeValidator : AbstractValidator<TransactionModeViewModel>
    {
        public TransactionModeValidator()
        {
            //RuleFor(validator => validator.ID).NotEmpty()
            //    .WithMessage("Transaction Mode ID should not be empty");
            
        RuleFor(validator => validator.TransactionModeName).NotEmpty()
                .WithMessage("Transaction Mode Name should not be empty");
        RuleFor(validator => validator.TransactionModeDescription).NotEmpty()
                .WithMessage("Transaction Mode Description should not be empty");

        }
    }
}