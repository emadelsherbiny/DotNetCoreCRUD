using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using INNOVATEQ.API.Helper;
using INNOVATEQ.DAL;
using INNOVATEQ.DATA.DTO;
using INNOVATEQ.DATA.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace INNOVATEQ.API.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]
    public class MainController<TRepository, T,M> : ControllerBase
         where TRepository : IRepositoryBase<T>
         where T : class, IBaseEntity
    {
        public TRepository _IRepo;
        private readonly IMapper _mapper;
        public MainController(TRepository IRepo, IMapper mapper)
        {
            _mapper = mapper;
            _IRepo = IRepo;
           

        }
        [Route("GetAll")]
        [HttpGet]
        public virtual IActionResult GetAll()
        {
            
                var entityResult = _IRepo.FindAll().OrderByDescending(x => x.Id); 
                return Ok(new Response<List<M>>(_mapper.Map<List<M>>(entityResult.ToList()))); 

        }
         
        [HttpGet("GetAllPagination")]
        public virtual IActionResult GetAllPagination([FromQuery] PaginationFilter filter)
        {
          
                var qry = _IRepo.FindAll();
                if (filter.Desc)
                {
                    qry = qry.OrderByDescending(on => on.Id);
                }
                else
                {
                    qry = qry.OrderBy(on => on.Id);

                }
            var result = PagedList<T>.ToPagedListResult(qry, filter.PageNumber, filter.PageSize);
             
                return Ok(result);
            

        }

        [Route("Get/{id}")]
        [HttpGet]
        public virtual IActionResult Get([FromRoute] int id)
        {

            var entityResult = _IRepo.FindById(id);
            return Ok(_mapper.Map<M>(entityResult));
        }


        [NonAction]
        public   async Task<IActionResult> AddAsync(T model)
        {
           
                if (!ModelState.IsValid)
                {
                    return BadRequest(new { status = false, message = "WrongData" });
                }

                var result =await _IRepo.CreateAsync(model);
                return Ok(SuccessResultAPI(result));

           
        }
        [HttpPut("Update")]
        public IActionResult Update(T model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new { status = false, message = "WrongData" });
                }

                var result = _IRepo.Update(model);
                return Ok(SuccessResultAPI(result));

            }
            catch (Exception ex)
            {

                return BadRequest(new { status = false, message = ex.Message });
            }
        }

        [HttpDelete("Delete")]
        public IActionResult Delete(T model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new { status = false, message = "WrongData" });
                }

                  _IRepo.Delete(model);
                return Ok();

            }
            catch (Exception ex)
            {

                return BadRequest(new { status = false, message = ex.Message });
            }
        }
       
      
        [NonAction]
        public API_Result<T> SuccessResultAPI<T>(T data, string msg = null)
        {

            API_Result<T> result = new API_Result<T>()
            {
                Data = data,
                Message = msg == null ? "Success Operation" : msg,
                Status = (int)HttpStatusCode.OK
            };
            return result;
        }


        [NonAction]
        public API_Result<T> NotFoundAPI<T>(T data, string msg = null)
        {

            API_Result<T> result = new API_Result<T>()
            {
                Data = data,
                Message = msg == null ? "Not Found" : msg,
                Status = (int)HttpStatusCode.NotFound
            };
            return result;
        }
       
     

}
}
