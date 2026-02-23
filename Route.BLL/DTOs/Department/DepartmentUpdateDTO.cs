namespace Route.BLL.DTOs.Department
{
    public class DepartmentUpdateDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string? Description { get; set; }
        public DateTime? DateOfCreation { get; set; }
    }
}
