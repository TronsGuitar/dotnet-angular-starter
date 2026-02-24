using Microsoft.AspNetCore.Mvc;
using PersonApi.Application.DTOs;
using PersonApi.Application.Interfaces;

namespace PersonApi.API.Controllers;

[ApiController]
[Route("api")]
public sealed class FloorplansController(IFloorplanService floorplanService) : ControllerBase
{
    [HttpPost("projects")]
    public ActionResult<FloorplanProjectDto> CreateProject([FromBody] CreateFloorplanProjectDto request)
    {
        var project = floorplanService.CreateProject(request);
        return CreatedAtAction(nameof(GetProject), new { projectId = project.Id }, project);
    }

    [HttpGet("projects/{projectId:guid}")]
    public ActionResult<FloorplanProjectDto> GetProject(Guid projectId)
    {
        var project = floorplanService.GetProject(projectId);
        return project is null ? NotFound() : Ok(project);
    }

    [HttpGet("floorplans/{floorId:guid}")]
    public ActionResult<FloorDto> GetFloor(Guid floorId)
    {
        var floor = floorplanService.GetFloor(floorId);
        return floor is null ? NotFound() : Ok(floor);
    }

    [HttpPut("floorplans/{floorId:guid}")]
    public ActionResult<FloorDto> SaveFloor(Guid floorId, [FromBody] UpdateFloorDto request)
    {
        var floor = floorplanService.SaveFloor(floorId, request);
        return floor is null ? NotFound() : Ok(floor);
    }

    [HttpPost("floorplans/{floorId:guid}/duplicate")]
    public ActionResult<FloorDto> DuplicateFloor(Guid floorId)
    {
        var duplicate = floorplanService.DuplicateFloor(floorId);
        return duplicate is null ? NotFound() : Ok(duplicate);
    }

    [HttpPost("survey-assets/{assetId:guid}/ocr")]
    public ActionResult<object> RunOcr(Guid assetId)
    {
        return Ok(new
        {
            SurveyAssetId = assetId,
            Status = "Queued",
            Message = "OCR pipeline contract ready. Integrate AI provider in infrastructure layer."
        });
    }

    [HttpPost("projects/{projectId:guid}/estimates/carpet")]
    [HttpGet("projects/{projectId:guid}/estimates/carpet")]
    public ActionResult<CarpetEstimateDto> GetCarpetEstimate(Guid projectId)
    {
        var estimate = floorplanService.GetCarpetEstimate(projectId);
        return estimate is null ? NotFound() : Ok(estimate);
    }
}
