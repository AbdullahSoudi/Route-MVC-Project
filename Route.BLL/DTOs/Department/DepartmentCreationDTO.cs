using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.BLL.DTOs.Department
{
    public class DepartmentCreationDTO
    {
        [Required(ErrorMessage = "Name is required !")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Code is required !")]
        [Range(100, int.MaxValue)]
        public string Code { get; set; }
        public string? Description { get; set; }
        public DateTime? DateOfCreation { get; set; }
    }
}
