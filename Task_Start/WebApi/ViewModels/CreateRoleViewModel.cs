using System.ComponentModel.DataAnnotations;

namespace WebApi.ViewModels
{
    public class CreateRoleViewModel
    {
        [Required]
        public string Role { get; set; }
    }
}
