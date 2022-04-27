using AutoMapper;
using HomeCinema.Data.Infrastructure;
using HomeCinema.Data.Repositories;
using HomeCinema.Entities;
using HomeCinema.Web.Infrastructure.Core;
using HomeCinema.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using HomeCinema.Web.Infrastructure.Extensions;
using HomeCinema.Data.Extensions;

namespace HomeCinema.Web.Controllers
{
    //[Authorize(Roles = "Admin")]
    [RoutePrefix("api/plot")]
    public class PlotController : ApiControllerBase
    {
        private readonly IEntityBaseRepository<Plot> _plotRepository;

        public PlotController(IEntityBaseRepository<Plot> plotRepository,
            IEntityBaseRepository<Error> _errorsRepository, IUnitOfWork _unitOfWork)
            : base(_errorsRepository, _unitOfWork)
        {
            _plotRepository = plotRepository;
        }

        

        [HttpPost]
        [Route("plot")]
        public HttpResponseMessage Plot(HttpRequestMessage request, PlotViewModel plotViewModel)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                if (!ModelState.IsValid)
                {
                    response = request.CreateResponse(HttpStatusCode.BadRequest,
                        ModelState.Keys.SelectMany(k => ModelState[k].Errors)
                              .Select(m => m.ErrorMessage).ToArray());
                }
                else
                {
                    if (_plotRepository.PlotExists(plotViewModel.ID))
                    {
                        ModelState.AddModelError("Invalid plot", "Plot Number already exists");
                        response = request.CreateResponse(HttpStatusCode.BadRequest,
                        ModelState.Keys.SelectMany(k => ModelState[k].Errors)
                              .Select(m => m.ErrorMessage).ToArray());
                    }
                    else
                    {
                        Plot newPlot = new Plot();
                        newPlot.UpdatePlot(plotViewModel);
                        _plotRepository.Add(newPlot);
                        _unitOfWork.Commit();
                        // Update view model
                        plotViewModel = Mapper.Map<Plot, PlotViewModel>(newPlot);
                        response = request.CreateResponse<PlotViewModel>(HttpStatusCode.Created, plotViewModel);

                    }
                }

                return response;
            });
        }
    }
}