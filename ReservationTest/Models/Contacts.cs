using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ReservationTest.Models
{
    public class Contacts
    {
        [Key]
        public int ContactId { get; set; }
        
        public virtual List<PhoneNumber> PhoneNumbers { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
    }
}
