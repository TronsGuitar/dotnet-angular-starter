namespace PersonApi.Application.DTOs;

public sealed class CreateFloorplanProjectDto
{
    public required string Name { get; set; }
    public string? Reference { get; set; }
    public string MeasurementUnit { get; set; } = "ft";
}

public sealed class FloorplanProjectDto
{
    public required Guid Id { get; set; }
    public required string Name { get; set; }
    public string? Reference { get; set; }
    public required string MeasurementUnit { get; set; }
    public List<FloorDto> Floors { get; set; } = [];
}

public sealed class FloorDto
{
    public required Guid Id { get; set; }
    public int FloorNumber { get; set; }
    public required string Name { get; set; }
    public List<LayerDto> Layers { get; set; } = [];
    public List<PlanObjectDto> Objects { get; set; } = [];
}

public sealed class LayerDto
{
    public required Guid Id { get; set; }
    public required string Type { get; set; }
    public bool Visible { get; set; } = true;
    public bool Locked { get; set; }
    public int ZIndex { get; set; }
}

public sealed class PlanObjectDto
{
    public required Guid Id { get; set; }
    public required string ObjectType { get; set; }
    public required string Label { get; set; }
    public required string GeometryJson { get; set; }
}

public sealed class UpdateFloorDto
{
    public required string Name { get; set; }
    public List<LayerDto> Layers { get; set; } = [];
    public List<PlanObjectDto> Objects { get; set; } = [];
}

public sealed class CarpetEstimateDto
{
    public required Guid ProjectId { get; set; }
    public decimal TotalSquareFeet { get; set; }
    public List<CarpetEstimateRowDto> Rows { get; set; } = [];
}

public sealed class CarpetEstimateRowDto
{
    public int FloorNumber { get; set; }
    public required string RoomLabel { get; set; }
    public decimal SquareFeet { get; set; }
}
