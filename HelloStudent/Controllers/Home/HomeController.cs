using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain;
using HelloStudent.Core;
using HelloStudent.Models;

namespace HelloStudent.Controllers.Home
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CallOrder(string name, string phone)
        {
            var em = new EmailMessage
            {
                Message = string.Format("<b>Имя:</b> {0}<br/>" +
                                        "<b>Тел.:</b> {1}<br/>",
                                        name,
                                        phone),
                DisplayNameFrom = "HelloStudent.ru",
                From = "student.hello@yandex.ru",
                Subject = "Просьба позвонить",
                To = "kurator@list.ru"
            };

            EmailService.SendMessage(em,
                    "student.hello@yandex.ru",
                    "123456aaa111",
                    "smtp.yandex.ru",
                    587,
                    true);

            return new EmptyResult();
        }

        public ActionResult TusurOrder()
        {
            var model = new TusurOrderModel
                {
                    WorkTypesList = new List<SelectListItem>
                        {
                            new SelectListItem
                                {
                                    Text = "Экзамен",
                                    Value = "Экзамен"
                                },
                            new SelectListItem
                                {
                                    Text = "Диплом",
                                    Value = "Диплом"
                                },
                            new SelectListItem
                                {
                                    Text = "Курсовой",
                                    Value = "Курсовой"
                                },
                            new SelectListItem
                                {
                                    Text = "Лабораторная",
                                    Value = "Лабораторная"
                                },
                            new SelectListItem
                                {
                                    Text = "Контрольная",
                                    Value = "Контрольная"
                                },
                            new SelectListItem
                                {
                                    Text = "Текстовая",
                                    Value = "Текстовая"
                                },
                            new SelectListItem
                                {
                                    Text = "Компьютерная контрольная",
                                    Value = "Компьютерная контрольная"
                                }
                        },
                    UrgencyList = new List<SelectListItem>
                        {
                            new SelectListItem
                                {
                                    Text = "Менее 3 дней",
                                    Value = "Менее 3 дней"
                                },
                            new SelectListItem
                                {
                                    Text = "Менее 7 дней",
                                    Value = "Менее 7 дней"
                                },
                            new SelectListItem
                                {
                                    Text = "Менее 14 дней",
                                    Value = "Менее 14 дней"
                                },
                            new SelectListItem
                                {
                                    Text = "более 28 дней",
                                    Value = "более 28 дней"
                                }
                        }
                };

            return View(model);
        }

        [HttpPost]
        public ActionResult TusurOrderFinish(TusurOrderModel model)
        {
            var em = new EmailMessage
                {
                    Message = string.Format("<b>ФИО:</b> {0}<br/>" +
                                        "<b>Email:</b> {1}<br/>" +
                                        "<b>Тел.:</b> {2}<br/>" +
                                        "<b>Шифр:</b> {3}<br/>" +
                                        "<b>Предмет:</b> {4}<br/>" +
                                        "<b>Вид работ:</b> {5}<br/>" +
                                        "<b>Срочность:</b> {6}<br/>" +
                                        "<b>Дополнительно:</b> {7}<br/>",
                                        model.Fio,
                                        model.Email,
                                        model.Phone,
                                        model.Cipher,
                                        model.Subject,
                                        model.Type,
                                        model.Urgency,
                                        model.Description),
                    DisplayNameFrom = "HelloStudent.ru",
                    From = "student.hello@yandex.ru",
                    Subject = "Заказ работы",
                    To = "kurator@list.ru"
                };

            EmailService.SendMessage(em,
                    "student.hello@yandex.ru",
                    "123456aaa111",
                    "smtp.yandex.ru",
                    587,
                    true);

            return RedirectToAction("TusurOrderFinish");
        }

        public ActionResult Papers()
        {
            var types = new DBService().GetTypes();
            var model = new TypesListModel
                {
                    TypesList = types.Select(x => new TypeModel
                        {
                            TypId = x.Id,
                            TypePaperName = x.Name,
                            SubjectsList = new DBService().GetSubjectsByType(x.Id).Select(s => new SubjectModel
                                {
                                    SubjectName = s.Name,
                                    SubjectId = s.Id,
                                    TypeId = x.Id
                                }).ToList()
                        }).ToList()
                };

            return View(model);
        }

        public ActionResult GetPapersList(int typeId, int subjectId)
        {
            var papers = new DBService().GetPapersByTypeAndSubject(typeId, subjectId);
            var model = new PapersListModel
                {
                    PapersList = papers.Select(x => new PaperModel
                        {
                            PaperId = x.Id,
                            PaperName = x.Name
                        }).ToList()
                };

            return PartialView(model);
        }

        public ActionResult AddPaperForm()
        {
            var types = new DBService().GetTypes();
            var subjects = new DBService().GetSubjects();

            var model = new PaperModel
            {
                TypesList = types.Select(x => new SelectListItem
                    {
                        Text = x.Name,
                        Value = x.Id.ToString()
                    }).ToList(),
                SubjectsList = subjects.Select(x => new SelectListItem
                    {
                        Text = x.Name,
                        Value = x.Id.ToString()
                    }).ToList()
            };

            return View(model);
        }

        public ActionResult AddPaperFinish(PaperModel model)
        {
            var paper = new Paper
                {
                    Name = model.PaperName,
                    Description = model.Description,
                    Type = model.TypeId,
                    Subject = model.SubjectId,
                    FileName = model.File.FileName
                };

            if (paper.Type == 0)
            {
                var type = new PaperType
                    {
                        Name = model.Type
                    };

                new DBService().AddType(type);

                paper.Type = type.Id;
            }

            if (paper.Subject == 0)
            {
                var subject = new Subject
                {
                    Name = model.Subject
                };

                new DBService().AddSubject(subject);

                paper.Subject = subject.Id;
            }

            new DBService().AddPaper(paper);

            string nameAndLocation = "~/Files/" + model.File.FileName;
            model.File.SaveAs(Server.MapPath(nameAndLocation));

            return View();
        }

        public ActionResult Order()
        {
            return View();
        }
    }
}