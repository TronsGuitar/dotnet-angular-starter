using PersonApi.Application.DTOs;

namespace PersonApi.Application.Interfaces;

public interface IFloorplanService
{
    FloorplanProjectDto CreateProject(CreateFloorplanProjectDto request);
    FloorplanProjectDto? GetProject(Guid projectId);
    FloorDto? GetFloor(Guid floorId);
    FloorDto? SaveFloor(Guid floorId, UpdateFloorDto request);
    FloorDto? DuplicateFloor(Guid floorId);
    CarpetEstimateDto? GetCarpetEstimate(Guid projectId);
}
