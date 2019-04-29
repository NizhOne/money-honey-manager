using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.Interfaces;
using API.Models;
using API.Models.Domain;
using API.Models.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class OperationsController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IOperationService operationService;

        public OperationsController(UserManager<ApplicationUser> userManager,
            IOperationService operationService)
        {
            this.userManager = userManager;
            this.operationService = operationService;
        }

        [HttpGet]
        public async Task<IEnumerable<OperationDto>> GetAsync([FromQuery]OperationsRequest request)
        {
            var userName = User.Identity.Name;

            var appUser = await userManager.FindByNameAsync(userName);

            return operationService.GetOperations(request, appUser);
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync([FromBody] OperationDto operation)
        {
            var appUser = await userManager.FindByNameAsync(User.Identity.Name);

            Guid operationId = this.operationService.AddOperation(operation, appUser);

            return Ok(operationId);
        }
    }
}