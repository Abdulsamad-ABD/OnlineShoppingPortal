using API.DTOs;
using Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class BuggyController : BaseApiController
{
    [HttpGet("unauthorized")]
    public IActionResult GetUnauthorized() // 401
    {
        return Unauthorized();
    }

    [HttpGet("badrequest")]
    public IActionResult GetBadRequest() // 400
    {
        return BadRequest("Not a good request");
    }

    [HttpGet("notfound")]
    public IActionResult GetNotFound() //404
    {
        return NotFound();
    }

    [HttpGet("internalerror")]
    public IActionResult GetInternalError() // 500
    {
        throw new Exception("This is a test exception.");
    }

    [HttpPost("validationerror")]
    public IActionResult GetValidationError(CreateProductDto product) //400 bad request
    {
        return Ok();
    }
}