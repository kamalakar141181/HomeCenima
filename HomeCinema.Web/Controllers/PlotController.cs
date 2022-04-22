using AutoMapper;
using HomeCinema.Data.Infrastructure;
using HomeCinema.Data.Repositories;
using HomeCinema.Entities;
using HomeCinema.Web.Infrastructure.Core;
using HomeCinema.Web.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
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
        [Route("update")]
        public HttpResponseMessage Update(HttpRequestMessage request, PlotViewModel plot)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                if (!ModelState.IsValid)
                {
                    response = request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    var plotDb = _plotRepository.GetSingle(plot.ID);
                    if (plotDb == null)
                        response = request.CreateErrorResponse(HttpStatusCode.NotFound, "Invalid plot.");
                    else
                    {
                        plotDb.UpdatePlot(plot);                        
                        _plotRepository.Edit(plotDb);

                        _unitOfWork.Commit();
                        response = request.CreateResponse<PlotViewModel>(HttpStatusCode.OK, plot);
                    }
                }

                return response;
            });
        }

        [HttpPost]
        [Route("plot")]
        public HttpResponseMessage plotCreation(HttpRequestMessage request, PlotViewModel plot)
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
                    if (_plotRepository.PlotExists(12))
                    {
                        ModelState.AddModelError("Invalid plot", "Plot Number already exists");
                        response = request.CreateResponse(HttpStatusCode.BadRequest,
                        ModelState.Keys.SelectMany(k => ModelState[k].Errors)
                              .Select(m => m.ErrorMessage).ToArray());
                    }                    
                }

                return response;
            });
        }
    }
}