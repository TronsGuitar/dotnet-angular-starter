using System.Collections.Concurrent;
using PersonApi.Application.DTOs;
using PersonApi.Application.Interfaces;

namespace PersonApi.Application.Services;

public sealed class FloorplanService : IFloorplanService
{
    private static readonly ConcurrentDictionary<Guid, FloorplanProjectDto> Projects = new();

    public FloorplanProjectDto CreateProject(CreateFloorplanProjectDto request)
    {
        var project = new FloorplanProjectDto
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Reference = request.Reference,
            MeasurementUnit = request.MeasurementUnit,
            Floors =
            [
                CreateDefaultFloor(1, "First Floor")
            ]
        };

        Projects[project.Id] = project;

        return project;
    }

    public FloorplanProjectDto? GetProject(Guid projectId)
    {
        return Projects.TryGetValue(projectId, out var project) ? project : null;
    }

    public FloorDto? GetFloor(Guid floorId)
    {
        return Projects.Values.SelectMany(project => project.Floors).FirstOrDefault(floor => floor.Id == floorId);
    }

    public FloorDto? SaveFloor(Guid floorId, UpdateFloorDto request)
    {
        var floor = GetFloor(floorId);
        if (floor is null)
        {
            return null;
        }

        floor.Name = request.Name;
        floor.Layers = request.Layers;
        floor.Objects = request.Objects;

        return floor;
    }

    public FloorDto? DuplicateFloor(Guid floorId)
    {
        var project = Projects.Values.FirstOrDefault(candidate => candidate.Floors.Any(floor => floor.Id == floorId));
        var sourceFloor = project?.Floors.FirstOrDefault(floor => floor.Id == floorId);

        if (project is null || sourceFloor is null)
        {
            return null;
        }

        var duplicateFloorNumber = project.Floors.Max(floor => floor.FloorNumber) + 1;
        var clone = new FloorDto
        {
            Id = Guid.NewGuid(),
            FloorNumber = duplicateFloorNumber,
            Name = $"Floor {duplicateFloorNumber}",
            Layers = sourceFloor.Layers
                .Select(layer => new LayerDto
                {
                    Id = Guid.NewGuid(),
                    Type = layer.Type,
                    Visible = layer.Visible,
                    Locked = layer.Locked,
                    ZIndex = layer.ZIndex
                })
                .ToList(),
            Objects = sourceFloor.Objects
                .Select(planObject => new PlanObjectDto
                {
                    Id = Guid.NewGuid(),
                    ObjectType = planObject.ObjectType,
                    Label = planObject.Label,
                    GeometryJson = planObject.GeometryJson
                })
                .ToList()
        };

        project.Floors.Add(clone);

        return clone;
    }

    public CarpetEstimateDto? GetCarpetEstimate(Guid projectId)
    {
        if (!Projects.TryGetValue(projectId, out var project))
        {
            return null;
        }

        var rows = project.Floors
            .SelectMany(floor => floor.Objects
                .Where(planObject => planObject.ObjectType == "Room")
                .Select(planObject => new CarpetEstimateRowDto
                {
                    FloorNumber = floor.FloorNumber,
                    RoomLabel = planObject.Label,
                    SquareFeet = 120m
                }))
            .ToList();

        return new CarpetEstimateDto
        {
            ProjectId = project.Id,
            Rows = rows,
            TotalSquareFeet = rows.Sum(row => row.SquareFeet)
        };
    }

    private static FloorDto CreateDefaultFloor(int floorNumber, string name)
    {
        return new FloorDto
        {
            Id = Guid.NewGuid(),
            FloorNumber = floorNumber,
            Name = name,
            Layers =
            [
                new LayerDto { Id = Guid.NewGuid(), Type = "SurveyImage", ZIndex = 1 },
                new LayerDto { Id = Guid.NewGuid(), Type = "OcrAnnotations", ZIndex = 2 },
                new LayerDto { Id = Guid.NewGuid(), Type = "Walls", ZIndex = 3 },
                new LayerDto { Id = Guid.NewGuid(), Type = "Rooms", ZIndex = 4 },
                new LayerDto { Id = Guid.NewGuid(), Type = "Measurements", ZIndex = 5 }
            ]
        };
    }
}
