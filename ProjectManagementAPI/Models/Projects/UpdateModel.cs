using System.ComponentModel.DataAnnotations;

namespace ProjectManagementAPI.Models.Projects
{
  public class UpdateModel : CreateModel
  {
      [Required(ErrorMessage = "This field is Required!")]
      public int Id { get; set; }
  }
}
