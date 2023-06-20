using Fochso.Models.Class;
using System.ComponentModel.DataAnnotations;

namespace Fochso.Models.Student;

public class CreateStudentViewModel
{
    [Required(ErrorMessage = "Student name is required")]
    public string Name { get; set; }
    public string Class { get; set; }
    public int ClassId { get; set; }
}
