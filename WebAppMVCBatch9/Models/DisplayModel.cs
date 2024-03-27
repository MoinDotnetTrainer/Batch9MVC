using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebAppMVCBatch9.Models
{
    public class DisplayModel
    {
        public int ID { get; set; } // pk identity
        public string Name { get; set; }
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
