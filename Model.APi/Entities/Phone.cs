using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.APi.Entities
{
    public class Phone
    {
        public int Id { get; set; }
        public string Phones { get; set; } = string.Empty;
        public string Label { get; set; } = string.Empty;
        public Contacts Contacts { get; set; } = null!;
        public int ContactsId { get; set; }
    }
}
