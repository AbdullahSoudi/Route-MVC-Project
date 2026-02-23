using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.BLL.DTOs.Department
{
    public class DepartmentDetailsDTO
    {
        public int Id { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? DateOfCreation { get; set; }
        public int LastModificationBy { get; set; }
        public DateTime? LastModificationOn { get; set; }
        public bool IsDeleted { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string? Description { get; set; }
    }
}
