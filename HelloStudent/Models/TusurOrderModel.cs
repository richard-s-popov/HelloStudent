using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HelloStudent.Models
{
    public class TusurOrderModel
    {
        public string Fio { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string Cipher { get; set; }

        public string Login { get; set; }

        public string Subject { get; set; }

        public string Type { get; set; }

        public string Urgency { get; set; }

        public string Description { get; set; }

        public IEnumerable<SelectListItem> WorkTypesListCutVersion { get; set; }

        public IEnumerable<SelectListItem> WorkTypesList { get; set; }

        public IEnumerable<SelectListItem> UrgencyList { get; set; }

        public bool FirstVisit { get; set; }
    }
}