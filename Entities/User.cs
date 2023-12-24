using System.ComponentModel.DataAnnotations;

namespace OneCore.Entities
{
    public class User
    {
        public int UserId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public virtual List<CustomLogger>? lstCustomLogger { get; set; }
        public virtual List<Bill>? lstBill { get; set; }
        public virtual List<Document>? lstDocument { get; set; }
    }
}
