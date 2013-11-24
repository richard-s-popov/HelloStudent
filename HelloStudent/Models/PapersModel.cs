using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HelloStudent.Models
{
    public class PapersListModel
    {
        public List<PaperModel> PapersList { get; set; }
    }

    public class SubjectsListModel
    {
        public List<SubjectModel> SubjectsList { get; set; } 
    }

    public class TypesListModel
    {
        public List<TypeModel> TypesList { get; set; } 
    }

    public class PaperModel
    {
        [DisplayName("Название работы:")]
        public string PaperName { get; set; }

        public int PaperId { get; set; }

        public int TypeId { get; set; }

        public string Type { get; set; }

        public int SubjectId { get; set; }

        public string Subject { get; set; }

        [DisplayName("Описание работы:")]
        public string Description { get; set; }

        public HttpPostedFileBase File { get; set; }

        public string FileName { get; set; }

        public string FileLink { get; set; }

        public List<SelectListItem> TypesList { get; set; }

        public List<SelectListItem> SubjectsList { get; set; } 
    }

    public class SubjectModel
    {
        public string SubjectName { get; set; }

        public int SubjectId { get; set; }

        public int TypeId { get; set; }
    }

    public class TypeModel
    {
        public string TypePaperName { get; set; }

        public int TypId { get; set; }

        public List<SubjectModel> SubjectsList { get; set; }
    }
}