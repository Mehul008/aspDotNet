using EmployeeManagement.ExcelService;
using EmployeeManagement.Interfaces;
using EmployeeManagement.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;

[ApiController]
[Route("api/[controller]")]
public class ExportController : ControllerBase
{
    private readonly ExcelService _exportService;
    private readonly IEmployeeBasicDetails _employeeBasicDetails;
    public ExportController(ExcelService exportService, IEmployeeBasicDetails employeeBasicDetails)
    {
        _exportService = exportService;
        _employeeBasicDetails = employeeBasicDetails;
    }


}
