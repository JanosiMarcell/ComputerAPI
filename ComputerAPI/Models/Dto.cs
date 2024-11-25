namespace ComputerAPI.Models
{
    public record CreateOsDto(string? Name);
    public record UpdateOsDto(string Name, string? Description); 
    public record CreateComputerDto(Guid? Id,string brand,string type,double display,int memory,DateTime CreatedTime,Guid OsId);
    public record UpdateComputerDto(string brand, string type, double display, int memory);

}
