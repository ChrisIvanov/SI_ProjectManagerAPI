using System.ComponentModel.DataAnnotations;

namespace ProjectManagementAPI.Models.Projects
{
    public class CreateModel
    {
        [Required(ErrorMessage = "This field is Required")]
        public string Title { get; set; }

        [Required(ErrorMessage = "This field is Required")]
        public string Description { get; set; }
    }
}
