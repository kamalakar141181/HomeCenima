using HomeCinema.Web.Infrastructure.Validators;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HomeCinema.Web.Models
{
    [Bind(Exclude = "UniqueKey")]
    public class PlotViewModel : IValidatableObject
    {
        public int ID { get; set; }
        public int PlotID { get; set; }
        public decimal PlotArea { get; set; }
        public bool IsAvailable { get; set; }
        public decimal Rate { get; set; }
        public decimal TotalAmount { get; set; }
        public string PlotDetails { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var validator = new PlotViewModelValidator();
            var result = validator.Validate(this);
            return result.Errors.Select(item => new ValidationResult(item.ErrorMessage, new[] { item.PropertyName }));
        }
    }
}