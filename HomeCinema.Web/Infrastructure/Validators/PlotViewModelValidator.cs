using FluentValidation;
using HomeCinema.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HomeCinema.Web.Infrastructure.Validators
{    
    public class PlotViewModelValidator : AbstractValidator<PlotViewModel>
    {
        public PlotViewModelValidator()
        {
            RuleFor(r => r.ID).NotEmpty()
                .WithMessage("Plot ID should not be empty");

            RuleFor(r => r.PlotArea).NotEmpty()
                .WithMessage("Plot Area should not be empty");
            
        }
    }
}