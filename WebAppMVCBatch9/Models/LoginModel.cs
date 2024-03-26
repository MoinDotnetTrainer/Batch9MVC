using System.ComponentModel.DataAnnotations;

namespace WebAppMVCBatch9.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Plz Enter ur EmailID")]
        public string EmailID { get; set; }

        [Required(ErrorMessage = "Plz Enter ur Password")]
        public string Password { get; set; }
    }
}
