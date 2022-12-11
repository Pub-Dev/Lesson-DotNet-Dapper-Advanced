namespace Lesson_DotNet_Dapper.Entities;

internal class User
{
    public int UserId { get; set; }
    public string? Name { get; set; }
    public string? Email { get; set; }
    public int StatusId { get; set; }
    public Status? Status { get; set; }
    public DateTime CreateDate { get; set; }

    public override string ToString()
    {
        return $@"
UserId = {UserId}
Name = {Name}
Email = {Email}
StatusId = {{{Status}
}}
CreateDate = {CreateDate}";
    }
}
