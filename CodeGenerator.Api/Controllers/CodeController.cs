using CodeGenerator.Core.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Mime;

namespace CodeGenerator.Api.Controllers;

[ApiController]
[Route("[controller]")]
[Consumes(MediaTypeNames.Application.Json)]
[Produces(MediaTypeNames.Application.Json)]
public class CodeController : ControllerBase
{
    private readonly ILogger<CodeController> _logger;
    private readonly ICodeService _service;

    public CodeController(ILogger<CodeController> logger, ICodeService service)
    {
        _logger = logger;
        _service = service;
    }

    /// <summary>
    /// Get a list of codes randomly generated
    /// </summary>
    /// <param name="count">Number of codes</param>
    /// <param name="codeLength">Length of the code</param>
    /// <param name="numbers">If the code may contains numbers</param>
    /// <param name="repeatLetters">If the code may have repeated letters</param>
    /// <returns>List of codes</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<string>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult Get(int count, int codeLength, bool numbers = true, bool repeatLetters = true)
    {
        try
        {
            return Ok(_service.GenerateCodeList(count, codeLength, numbers, repeatLetters));
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }
}

