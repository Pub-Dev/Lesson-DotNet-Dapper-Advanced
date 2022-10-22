namespace Lesson_DotNet_Dapper.Entities;

internal class Status
{
    public int StatusId { get; set; }
    public string? Name { get; set; }
    public DateTime CreateDate { get; set; }

    public override string ToString()
    {
        return $@"
    StatusId = {StatusId}
    Name = {Name}
    CreateDate = {CreateDate}";
    }
}
