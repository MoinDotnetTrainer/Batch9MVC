using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAppMVCBatch9.Models
{
    public class RegisterModel
    {

        [Key]  // pk
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]  // idetity
        public int ID { get; set; } // pk identity

        [Required(ErrorMessage = "Plz Enter ur Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Plz Enter ur EmailID")]
        [RegularExpression("[a-z0-9._%+-]+@[a-z0-9.-]+\\.[a-z]{2,4}", ErrorMessage = "Please enter correct email")]
        public string EmailID { get; set; }
        public string Password { get; set; }
        public DateTime Dob { get; set; }
        public string Mobile { get; set; }
        public string Gender { get; set; }
        public string Dept { get; set; }
        public int Salary { get; set; }
        public bool Status { get; set; }
    }
}
