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
    [RoutePrefix("api/TransactionModeRoute")] 
    
    public class TransactionModeController : ApiControllerBase
    {
        private readonly IEntityBaseRepository<TransactionModeEntity> _transactionModeRepository;

        public TransactionModeController(IEntityBaseRepository<TransactionModeEntity> transactionModeRepository,
            IEntityBaseRepository<Error> _errorsRepository, IUnitOfWork _unitOfWork)
            : base(_errorsRepository, _unitOfWork)
        {
            _transactionModeRepository = transactionModeRepository;
        }


        [HttpGet]
        [Route("GetTransactionModes/{page:int=0}/{pageSize=4}/{filter?}")]        
        public HttpResponseMessage GetTransactionModes(HttpRequestMessage request, int? page, int? pageSize, string filter = null)
        {
            int currentPage = page.Value;
            int currentPageSize = pageSize.Value;

            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<TransactionModeEntity> transactionMode = null;
                int totaltransactionMode = new int();

                if (!string.IsNullOrEmpty(filter))
                {
                    filter = filter.Trim().ToLower();

                    transactionMode = _transactionModeRepository.FindBy(c => c.TransactionModeName.ToLower().Contains(filter))
                        .OrderBy(c => c.ID)
                        .Skip(currentPage * currentPageSize)
                        .Take(currentPageSize)
                        .ToList();

                    totaltransactionMode = _transactionModeRepository.GetAll()
                        .Where(c => c.TransactionModeName.ToLower().Contains(filter))
                        .Count();
                }
                else
                {
                    transactionMode = _transactionModeRepository.GetAll()
                        .OrderBy(c => c.ID)
                        .Skip(currentPage * currentPageSize)
                        .Take(currentPageSize)
                    .ToList();

                    totaltransactionMode = _transactionModeRepository.GetAll().Count();
                }

                IEnumerable<TransactionModeViewModel> transactionModeVM = Mapper.Map<IEnumerable<TransactionModeEntity>, IEnumerable<TransactionModeViewModel>>(transactionMode);

                PaginationSet<TransactionModeViewModel> pagedSet = new PaginationSet<TransactionModeViewModel>()
                {
                    Page = currentPage,
                    TotalCount = totaltransactionMode,
                    TotalPages = (int)Math.Ceiling((decimal)totaltransactionMode / currentPageSize),
                    Items = transactionModeVM
                };

                response = request.CreateResponse<PaginationSet<TransactionModeViewModel>>(HttpStatusCode.OK, pagedSet);

                return response;
            });
        }

        [HttpPost]
        [Route("AddTransactionMode")]
        public HttpResponseMessage AddTransactionMode(HttpRequestMessage request, TransactionModeViewModel transactionModeViewModel)
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
                    if (_transactionModeRepository.TransactionModeExists(transactionModeViewModel.TransactionModeName))
                    {
                        ModelState.AddModelError("Invalid Transaction Mode", "Transaction Mode already exists");
                        response = request.CreateResponse(HttpStatusCode.BadRequest,
                        ModelState.Keys.SelectMany(k => ModelState[k].Errors)
                              .Select(m => m.ErrorMessage).ToArray());
                    }
                    else
                    {
                        TransactionModeEntity newTransactionMode = new TransactionModeEntity();
                        newTransactionMode.UpdateTransactionMode(transactionModeViewModel);
                        _transactionModeRepository.Add(newTransactionMode);

                        _unitOfWork.Commit();

                        // Update view model                        
                        transactionModeViewModel = Mapper.Map<TransactionModeEntity, TransactionModeViewModel>(newTransactionMode);
                        response = request.CreateResponse<TransactionModeViewModel>(HttpStatusCode.Created, transactionModeViewModel);
                    }
                }

                return response;
            });
        }

        [Route("GetTransactionModeDetails/{id:int}")]
        public HttpResponseMessage GetTransactionModeDetails(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                var transactionMode = _transactionModeRepository.GetSingle(id);

                TransactionModeViewModel transactionModeVm = Mapper.Map<TransactionModeEntity, TransactionModeViewModel>(transactionMode);

                response = request.CreateResponse<TransactionModeViewModel>(HttpStatusCode.OK, transactionModeVm);

                return response;
            });
        }

        [HttpPost]
        [Route("UpdateTransactionMode")]
        public HttpResponseMessage UpdateTransactionMode(HttpRequestMessage request, TransactionModeViewModel transactionMode)
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
                    TransactionModeEntity _transactionModeEntity = _transactionModeRepository.GetSingle(transactionMode.ID);
                    _transactionModeEntity.UpdateTransactionMode(transactionMode);
                    _unitOfWork.Commit();
                    response = request.CreateResponse(HttpStatusCode.OK);
                }

                return response;
            });
        }
       
    }
}