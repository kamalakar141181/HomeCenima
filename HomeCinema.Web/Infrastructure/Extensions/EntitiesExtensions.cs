using HomeCinema.Entities;
using HomeCinema.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HomeCinema.Web.Infrastructure.Extensions
{
    public static class EntitiesExtensions
    {
        public static void UpdateMovie(this Movie movie, MovieViewModel movieVm)
        {
            movie.Title = movieVm.Title;
            movie.Description = movieVm.Description;
            movie.GenreId = movieVm.GenreId;
            movie.Director = movieVm.Director;
            movie.Writer = movieVm.Writer;
            movie.Producer = movieVm.Producer;
            movie.Rating = movieVm.Rating;
            movie.TrailerURI = movieVm.TrailerURI;
            movie.ReleaseDate = movieVm.ReleaseDate;
        }

        public static void UpdateCustomer(this Customer customer, CustomerViewModel customerVm)
        {
            customer.FirstName = customerVm.FirstName;
            customer.LastName = customerVm.LastName;
            customer.IdentityCard = customerVm.IdentityCard;
            customer.Mobile = customerVm.Mobile;
            customer.DateOfBirth = customerVm.DateOfBirth;
            customer.Email = customerVm.Email;
            customer.UniqueKey = (customerVm.UniqueKey == null || customerVm.UniqueKey == Guid.Empty)
                ? Guid.NewGuid() : customerVm.UniqueKey;
            customer.RegistrationDate = (customer.RegistrationDate == DateTime.MinValue ? DateTime.Now : customerVm.RegistrationDate);
        }

        public static void UpdatePlot(this Plot plot, PlotViewModel plotVm)
        {
            plot.ID = plotVm.ID;
            plot.PlotID = plotVm.PlotID;
            plot.PlotArea = plotVm.PlotArea;
            plot.IsAvailable = plotVm.IsAvailable;
            plot.Rate = plotVm.Rate;
            plot.TotalAmount = plotVm.TotalAmount;
            plot.PlotDetails = plotVm.PlotDetails;
        }

        public static void UpdateTransactionMode(this TransactionModeEntity transactionModeEntity, TransactionModeViewModel transactionModeVm)
        {
            transactionModeEntity.ID = transactionModeVm.ID;
            transactionModeEntity.TransactionModeName = transactionModeVm.TransactionModeName;
            transactionModeEntity.TransactionModeDescription = transactionModeVm.TransactionModeDescription;

            transactionModeEntity.CreatedBy = transactionModeVm.CreatedBy;
            transactionModeEntity.CreatedDate = transactionModeVm.CreatedDate;
            transactionModeEntity.ModifiedBy = transactionModeVm.ModifiedBy;

            transactionModeEntity.ModifiedDate = transactionModeVm.ModifiedDate;
            transactionModeEntity.DeleteFlag = transactionModeVm.DeleteFlag;
        }
    }
}