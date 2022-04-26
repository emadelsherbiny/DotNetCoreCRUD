using AutoMapper;
using INNOVATEQ.API.Helper;
using INNOVATEQ.DAL;
using INNOVATEQ.DAL.Repository;
using INNOVATEQ.DATA.DTO;
using INNOVATEQ.DATA.Models;
using INNOVATEQ.DATA.ViewModel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace INNOVATEQ.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : MainController<IUserRepo, User, UserViewModel>
    {
        private readonly IUserRepo _IUserRepo; 
        private readonly IMapper _mapper;
        private IWebHostEnvironment _Environment;
        private readonly IOptions<AppSettings> _AppSettings;
        public UserController(IMapper mapper, IUserRepo IUserRepo, IWebHostEnvironment iWebHostEnvironment,
           IOptions<AppSettings> AppSettings)
            : base(IUserRepo, mapper)
        {
            _mapper = mapper;
            _IUserRepo = IUserRepo;
            _Environment = iWebHostEnvironment;
            _AppSettings = AppSettings;
        }
        [HttpPost("Add")]
        public async Task<IActionResult> Add([FromForm] UserDto model )
        {
            
            var user = _mapper.Map<User>(model);
            user.ImagePath = SaveFile(model.Image); 
            return await base.AddAsync(user);
        }

        [Route("Get/{id}")]
        [HttpGet]
        public override IActionResult Get([FromRoute] int id)
        {

            var entityResult =  _mapper.Map<UserViewModel>(_IRepo.FindById(id))  ;
            entityResult.Url = _AppSettings.Value.APIUrl  ;
             
            return Ok(_mapper.Map<UserViewModel>(entityResult));
        }
        [NonAction]
        public string SaveFile(IFormFile postedFile)
        {
             
            string contentPath = this._Environment.ContentRootPath;

            string path = Path.Combine(contentPath, "uplods");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string fileName = Path.GetFileName(postedFile.FileName);
            using (FileStream stream = new FileStream(Path.Combine(path, fileName), FileMode.Create))
            {
                postedFile.CopyTo(stream);
            }

            return fileName;
        }



        [NonAction]
        public IActionResult Getimg(string Imgpath)
        {
              
            string contentPath = this._Environment.ContentRootPath;

            string path = Path.Combine(contentPath, Imgpath);
            Byte[] b = System.IO.File.ReadAllBytes(path);          
            return File(b, "image/jpg");
        }
    }


}
