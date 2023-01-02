using AlkemyWallet.Core.Helper;
using AlkemyWallet.Core.Interfaces;
using AlkemyWallet.Core.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace AlkemyWallet.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [Authorize("Admin")]
        public IActionResult Get([FromQuery] int? page = 1)
        {

            PagedList<UserListDTO> pageUser = _userService.GetAllPage(page.Value);

            if (page > pageUser.TotalPages|| page < 0)
            {
                return BadRequest($"page number {page} doesn't exist");
            }
            else
            {
                var url = this.Request.Path;
                return Ok(new
                {
                    next = pageUser.HasNext ? $"{url}/{page + 1}" : "",
                    prev = (pageUser.Count > 0 && pageUser.HasPrevious) ? $"{url}/{page - 1}" : "",
                    currentPage = pageUser.CurrentPage,
                    totalPages = pageUser.TotalPages,
                    data = pageUser
                });
            }
        }


    }
}