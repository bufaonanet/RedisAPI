using Microsoft.AspNetCore.Mvc;
using RedisAPI.Data;
using RedisAPI.Models;

namespace RedisAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PlatformsController : ControllerBase
{
    private readonly IPlatformRepository _repository;

    public PlatformsController(IPlatformRepository repository)
    {
        _repository = repository;
    }

    [HttpGet("{id}", Name = "GetPlatformById")]
    public ActionResult<Platform> GetPlatformById(string id)
    {
        var platform = _repository.GetPlatformById(id);

        if (platform == null)
            return NotFound();

        return Ok(platform);
    }

    [HttpGet()]
    public ActionResult<IEnumerable<Platform>> GetAllPlatform()
    {
        return Ok(_repository.GetAllPlatform());
    }

    [HttpPost()]
    public ActionResult<Platform> CreatePlatform(Platform platform)
    {
        _repository.CreatePlatform(platform);

        return CreatedAtRoute(nameof(GetPlatformById), new { Id = platform.Id }, platform);
    }
}