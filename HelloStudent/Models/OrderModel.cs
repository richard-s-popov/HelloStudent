using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace HelloStudent.Models
{
    public class OrderModel
    {
        public string Email { get; set; }

        public string Phone { get; set; }

        public string Description { get; set; }

        public DateTime Date { get; set; }

        public string WorkSubject { get; set; }

        public string WorkType { get; set; }

        public string Subject { get; set; }
    }
}