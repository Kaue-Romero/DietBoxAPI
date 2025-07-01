using DietBoxAPI.DB.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Admin")]
[ProducesResponseType(StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status401Unauthorized)]
public class AdminController : ControllerBase
{
    private readonly INutritionistService _nutritionistService;

    public AdminController(INutritionistService nutritionistService)
    {
        _nutritionistService = nutritionistService;
    }

    [HttpGet("nutritionists")]
    public ActionResult<IEnumerable<Nutritionist>> GetAll()
    {
        return Ok(_nutritionistService.GetAll());
    }

    [HttpGet("nutritionist/{id}")]
    public ActionResult<Nutritionist> GetById(int id)
    {
        var nutritionist = _nutritionistService.GetById(id);
        if (nutritionist == null) return NotFound();
        return Ok(nutritionist);
    }

    [HttpPost("nutritionist")]
    public ActionResult<Nutritionist> Create(Nutritionist nutritionist)
    {
        var created = _nutritionistService.Create(nutritionist);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("nutritionist/{id}")]
    public IActionResult Update(int id, Nutritionist nutritionist)
    {
        if (id != nutritionist.Id) return BadRequest();
        var updated = _nutritionistService.Update(id, nutritionist);
        if (!updated) return NotFound();
        return NoContent();
    }

    [HttpDelete("nutritionist/{id}")]
    public IActionResult Delete(int id)
    {
        var deleted = _nutritionistService.Delete(id);
        if (!deleted) return NotFound();
        return NoContent();
    }
}
