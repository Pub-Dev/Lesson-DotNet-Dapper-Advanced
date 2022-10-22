namespace Lesson_DotNet_Dapper_Advanced.Entities;

internal class Vehicle
{
    public int VehicleId { get; set; }
    public string? Plate { get; set; }
    public VehicleColor? Color { get; set; }
    public VehicleType? Type { get; set; }
    public DateTime CreateDate { get; set; }

    public override string ToString()
    {
        return $@"VehicleId = {VehicleId}
Plate = {Plate}
CreateDate = {CreateDate}
Color = {{{Color}
}}
Type = {{{Type}
}}";
    }

    internal class VehicleColor
    {
        public int ColorId { get; set; }
        public string? Name { get; set; }
        public DateTime CreateDate { get; set; }

        public override string ToString()
        {
            return $@"
    ColorId = {ColorId}
    Name = {Name}
    CreateDate = {CreateDate}";
        }
    }

    internal class VehicleType
    {
        public int TypeId { get; set; }
        public string? Name { get; set; }
        public DateTime CreateDate { get; set; }

        public override string ToString()
        {
            return $@"
    TypeId = {TypeId}
    Name = {Name}
    CreateDate = {CreateDate}";
        }
    }
}
