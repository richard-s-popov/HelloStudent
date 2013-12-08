using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace HelloStudent.Models
{
    public class OrderModel
    {
        public string Name { get; set; }

        public string City { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string Description { get; set; }

        public DateTime Date { get; set; }

        public HttpPostedFileBase File1 { get; set; }

        public HttpPostedFileBase File2 { get; set; }

        public HttpPostedFileBase File3 { get; set; }

        public string University { get; set; }

        public string WorkSubject { get; set; }

        public string WorkType { get; set; }

        public string Subject { get; set; }
    }
}