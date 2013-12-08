using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
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
            var content = new DBService().GetMainContent();

            ViewBag.Content = content != null ? content.MainContent : string.Empty;

            return View();
        }

        public ActionResult CallOrder(string name, string phone, bool isTusur = false)
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
                To = isTusur ? "kurator@list.ru" : "scienceplus@yandex.ru"
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
                                    Text = "Компьютерная Контрольная Работа",
                                    Value = "Компьютерная Контрольная Работа"
                                },
                            new SelectListItem
                                {
                                    Text = "Текстовая Контрольная Работа",
                                    Value = "Текстовая Контрольная Работа"
                                },
                            new SelectListItem
                                {
                                    Text = "Лабораторная Работа",
                                    Value = "Лабораторная Работа"
                                },
                            new SelectListItem
                                {
                                    Text = "Курсовой Проект",
                                    Value = "Курсовой Проект"
                                },
                            new SelectListItem
                                {
                                    Text = "Экзамен",
                                    Value = "Экзамен"
                                },
                            new SelectListItem
                                {
                                    Text = "Преддипломная Практика",
                                    Value = "Преддипломная Практика"
                                },
                            new SelectListItem
                                {
                                    Text = "Дипломный Проект",
                                    Value = "Дипломный Проект"
                                }
                        },
                    WorkTypesListCutVersion = new List<SelectListItem>
                        {
                            new SelectListItem
                                {
                                    Text = "Компьютерная Контрольная Работа",
                                    Value = "Компьютерная Контрольная Работа"
                                },
                            new SelectListItem
                                {
                                    Text = "Текстовая Контрольная Работа",
                                    Value = "Текстовая Контрольная Работа"
                                },
                            new SelectListItem
                                {
                                    Text = "Лабораторная Работа",
                                    Value = "Лабораторная Работа"
                                },
                            new SelectListItem
                                {
                                    Text = "Курсовой Проект",
                                    Value = "Курсовой Проект"
                                },
                            new SelectListItem
                                {
                                    Text = "Экзамен",
                                    Value = "Экзамен"
                                }
                        },
                    UrgencyList = new List<SelectListItem>
                        {
                            new SelectListItem
                                {
                                    Text = "3 - 7 дней",
                                    Value = "3 - 7 дней"
                                },
                            new SelectListItem
                                {
                                    Text = "7 - 14 дней",
                                    Value = "7 - 14 дней"
                                },
                            new SelectListItem
                                {
                                    Text = "14 - 28 дней",
                                    Value = "14 - 28 дней"
                                }
                        }
                };

            return View(model);
        }

        [HttpPost]
        public ActionResult OrderFinish(OrderModel model)
        {
            return View();
        }

        public ActionResult MainPageContent(string password)
        {
            if (password == "123456aaa111")
            {
                var content = new DBService().GetMainContent();

                ViewBag.Content = content != null ? content.MainContent : string.Empty;
                ViewBag.Password = "123456aaa111";

                return View();    
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult MainPageContentFinish(string content, string password)
        {
            if (password == "123456aaa111")
            {
                new DBService().SaveMainContent(content);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult TusurOrderFinish(TusurOrderModel model)
        {
            var em = new EmailMessage
                {
                    Message = string.Format("<b>ФИО:</b> {0}<br/>" +
                                        "<b>Email:</b> {1}<br/>" +
                                        "<b>Тел.:</b> {2}<br/>" +
                                        "<b>Логин:</b> {3}<br/>" +
                                        "<b>Шифр:</b> {4}<br/>" +
                                        "<b>Предмет:</b> {5}<br/>" +
                                        "<b>Вид работ:</b> {6}<br/>" +
                                        "<b>Срочность:</b> {7}<br/>" +
                                        "<b>Дополнительно:</b> {8}<br/>",
                                        model.Fio,
                                        model.Email,
                                        model.Phone,
                                        model.Login,
                                        model.Cipher,
                                        model.Subject,
                                        model.Type,
                                        model.Urgency,
                                        model.Description),
                    DisplayNameFrom = "HelloStudent.ru",
                    From = "student.hello@yandex.ru",
                    Subject = "Заказ работы (Первый заказ)",
                    To = "kurator@list.ru"
                };

            if (!model.FirstVisit)
            {
                em = new EmailMessage
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
            }

            EmailService.SendMessage(em,
                    "student.hello@yandex.ru",
                    "123456aaa111",
                    "smtp.yandex.ru",
                    587,
                    true);

            return View("TusurOrderFinish");
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

        public ActionResult GetPaper(int id)
        {
            var paper = new DBService().GetPaper(id);
            var model = new PaperModel
                {
                    PaperName = paper.Name,
                    Type = paper.PaperType.Name,
                    Description = paper.Description,
                    FileName = paper.Name + paper.FileExtension, // todo: костыль. исправить.
                    FileLink = Url.Action("GetFile", new { id = paper.Id })
                };

            return PartialView("PaperView", model);
        }

        public ActionResult GetFile(int id)
        {
            var paper = new DBService().GetPaper(id);
            var file = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Files", paper.FileName);

            var bytes = System.IO.File.ReadAllBytes(file);

            return File(bytes, "application/zip", string.Format("{0}.{1}", paper.Name, paper.FileExtension));
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

        public ActionResult Repetitor()
        {
            return View();
        }

        [ValidateInput(false)]
        public ActionResult AddPaperFinish(PaperModel model)
        {
            var paper = new Paper
                {
                    Name = model.PaperName,
                    Description = model.Description,
                    Type = model.TypeId,
                    Subject = model.SubjectId,
                    FileName = string.Format("{0}{1}", model.PaperName.Replace(' ', '_'), Path.GetExtension(model.File.FileName)),
                    FileExtension = Path.GetExtension(model.File.FileName)
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

            string nameAndLocation = "~/Files/" + paper.FileName;
            model.File.SaveAs(Server.MapPath(nameAndLocation));

            return View();
        }

        public ActionResult Order()
        {
            return View();
        }
    }
}