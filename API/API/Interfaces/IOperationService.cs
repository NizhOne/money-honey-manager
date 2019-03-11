using System;
using System.Collections.Generic;
using API.Models;
using API.Models.Domain;
using API.Models.Dto;

namespace API.Interfaces
{
    public interface IOperationService
    {
        Guid AddOperation(OperationDto operation, ApplicationUser user);
        IEnumerable<OperationDto> GetOperations(OperationsRequest request, ApplicationUser user);
    }
}