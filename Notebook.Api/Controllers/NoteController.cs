using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Notebook.Authentication.NewFolder;
using Notebook.Database;
using Notebook.Database.DatabaseService;
using Notebook.Database.ObjectDto;
using NoteBook.ImageService.NewFolder;
using NoteBook.ImageService.Validations;
using System;

namespace Notebook.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NoteController : Controller
    {
        private readonly IDbService _dbContext;
        private readonly IServiceImage _serviceImage;
        private readonly IJwtService _jwtService;
        private readonly IAccount _account;
        public NoteController(IDbService dbContext, IServiceImage serviceImage, IJwtService jwtService, IAccount account)
        {
            _dbContext = dbContext;
            _serviceImage = serviceImage;
            _jwtService = jwtService;
            _account = account;
        }
        [HttpGet("Log in")]
        public IActionResult LogIn([FromQuery] string name, [FromQuery] string lastname)
        {
            var user = _account.Login(name, lastname);
            if (user)
            {
                var token = _jwtService.GetJwtToken(name);               
                return Ok(new { Token = token });
            }
            return NotFound("Username or password was wrong");
        }
        [HttpPost("Create User")]
        public IActionResult Index([FromQuery] UserDto newUser)
        {
            _account.SignUpAccount(newUser.FirstName,newUser.LastName);
            return Ok();
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost("Create Record")]
        public IActionResult CreateRecord([FromQuery]RecordDto record,[FromForm] ValidatingExtension imageRequest)
        {
            byte[] imageBytes = _serviceImage.ConvertImageToBytes(imageRequest);
            _dbContext.AddRecord(record,imageBytes);
            return Ok();
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost("Create Category")]
        public IActionResult CreateCategory([FromQuery] string name,[FromQuery] Guid userid)
        {
            _dbContext.AddCategory(name,userid);
            return Ok();
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPut("Change Record")]
        public IActionResult ChangeRecord([FromQuery] RecordDto newData,[FromQuery] string id)
        {
            _dbContext.ChangeRecordCategory(Guid.Parse(id), newData);
            return Ok();
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPut("Change Category information")]
        public IActionResult ChangeInformation([FromQuery] Guid id, [FromQuery] string newName)
        {
            _dbContext.EditCategory(id, newName);
            return Ok();
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("ShowCategoryRecords")]
        public IActionResult ShowRecords([FromQuery] Guid id)
        {
            var categoryRecords = _dbContext.ShowRecordsByCategory(id);
            return Ok(categoryRecords);
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("SearchRecordById")]
        public IActionResult ShowRecord([FromQuery] Guid id)
        {
            var record = _dbContext.SearchRecordByID(id);
            return Ok(record);
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpDelete("Delete Record")]
        public IActionResult DeleteRecord([FromQuery] Guid id)
        {
            _dbContext.RemoveRecord(id);
            return Ok();
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpDelete("Delete Category")]
        public IActionResult DeleteCategory([FromQuery] Guid Id)
        {
            _dbContext.DeleteCategory(Id);
            return Ok();
        }
    }
}
