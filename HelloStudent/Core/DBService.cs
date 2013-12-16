using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using Domain;

namespace HelloStudent.Core
{
    public class DBService
    {
        public IEnumerable<PaperType> GetTypes()
        {
            var db = new HelloStudentEntities();

            return db.PaperTypes.Select(x => x);
        }

        public SiteContent GetMainContent()
        {
            var db = new HelloStudentEntities();

            return db.SiteContents.FirstOrDefault();
        }

        public int OrderCounter()
        {
            var conf = WebConfigurationManager.OpenWebConfiguration("/");
            var count = Convert.ToInt32(conf.AppSettings.Settings["OrderCounter"].Value);

            conf.AppSettings.Settings["OrderCounter"].Value = Convert.ToString(++count);
            conf.Save();

            return count;
        }

        public void SaveMainContent(string content)
        {
            var db = new HelloStudentEntities();
            var entity = db.SiteContents.FirstOrDefault();

            if (entity != null)
            {
                entity.MainContent = content;
                db.SaveChanges();
            }
            else
            {
                db.SiteContents.Add(new SiteContent
                    {
                        MainContent = content
                    });
                db.SaveChanges();
            }
        }

        public IEnumerable<Subject> GetSubjectsByType(int typeId)
        {
            var db = new HelloStudentEntities();

            return db.Subjects.Where(x => x.Papers.Any(p => p.Type == typeId));
        }

        public IEnumerable<Subject> GetSubjects()
        {
            var db = new HelloStudentEntities();

            return db.Subjects.Select(x => x);
        }

        public IEnumerable<Paper> GetPapersByTypeAndSubject(int typeId, int subjectId)
        {
            var db = new HelloStudentEntities();

            return db.Papers.Where(x => x.Type == typeId && x.Subject == subjectId);
        }

        public Paper GetPaper(int id)
        {
            var db = new HelloStudentEntities();

            return db.Papers.FirstOrDefault(x => x.Id == id);
        }

        public void AddPaper(Paper paper)
        {
            var db = new HelloStudentEntities();

            db.Papers.Add(paper);
            db.SaveChanges();
        }

        public void AddType(PaperType type)
        {
            var db = new HelloStudentEntities();

            db.PaperTypes.Add(type);
            db.SaveChanges();
        }

        public void AddSubject(Subject subj)
        {
            var db = new HelloStudentEntities();

            db.Subjects.Add(subj);
            db.SaveChanges();
        }
    }
}