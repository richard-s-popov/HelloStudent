﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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