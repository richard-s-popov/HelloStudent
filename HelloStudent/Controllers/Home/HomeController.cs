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

        public ActionResult Order(bool? finish, int? orderNumber, string email)
        {
            var model = new OrderModel();

            if (finish != null && finish.Value && orderNumber != null)
            {
                ViewBag.Finish = true;
                model.OrderNumber = orderNumber.Value;
                model.Email = email;
            }
            else
            {
                ViewBag.Finish = false;
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult OrderFinish(OrderModel model)
        {
            var service = new DBService();
            model.OrderNumber = service.OrderCounter();

            var em = new EmailMessage
                {
                    Message = string.Format("<b>Тема работы:</b> {0}<br/>" +
                                        "<b>Тип работы:</b> {1}<br/>" +
                                        "<b>Предмет:</b> {2}<br/>" +
                                        "<b>Что требуется:</b> {3}<br/>" +
                                        "<b>Когда нужна работа:</b> {4}<br/>" +
                                        "<b>E-mail:</b> {5}<br/>" +
                                        "<b>Тел.:</b> {6}<br/>",
                                        model.WorkSubject,
                                        model.WorkType,
                                        model.Subject,
                                        model.Description,
                                        model.Date.Value.ToShortDateString(),
                                        model.Email,
                                        model.Phone),
                    DisplayNameFrom = "HelloStudent.ru",
                    From = "student.hello@yandex.ru",
                    Subject = string.Format("Заказ работы (Заявка №{0})", model.OrderNumber),
                    To = "student.hello@yandex.ru"
                };

            EmailService.SendMessage(em,
                    "student.hello@yandex.ru",
                    "123456aaa111",
                    "smtp.yandex.ru",
                    587,
                    true);

            var emToClient = new EmailMessage
                {
                    Message = string.Format("Добрый день{0}!<br/>" +
                                            "Спасибо, за интерес к проекту <b>\"Привет, студент!\"</b> Ваша заявка поступила на оценку.<br/><br/>" +
                                            "Вашей заявке присвоен номер <b>{1}</b><br/><br/>" +
                                            "Пожалуйста, проверьте данные:<br/>" +
                                            "Ваша почта <b>{2}</b><br/>" +
                                            "Ваш телефон <b>{3}</b><br/>" +
                                            "Тема: <b>{4}</b><br/>" +
                                            "Дисциплина: <b>{5}</b><br/>" +
                                            "Тип работ: <b>{6}</b><br/><br/>" +
                                            "Работа необходима Вам к <b>{7}</b><br/><br/>" +
                                            "На стоимость влияет срок сдачи, в который будет сдаваться работа. " +
                                            "Если Вы ошиблись в данных, то ответным письмом пришлите правильные.<br/><br/>" +
                                            "<b>!!! Если есть методичка к работе или любые другие дополнительные требования - " +
                                            "пожалуйста, пришлите ответным письмом.</b><br/><br/>" +
                                            "Наши специалисты произведут оценку стоимости Вашей работы.<br/><br/>" +
                                            "Как мы работаем:<br/>" +
                                            "1. Менеджер подберет специалиста, который точно сможет написать Вашу работу " +
                                            "и \"зарезервирует\" автора.<br/>" +
                                            "2. Дадим Вам ответ о стоимости, сроках и условиях сотрудничества.<br/>" +
                                            "3. Вы принимаете решение о сотрудничестве.<br/><br/>" +
                                            "Заказывая написание студенческой работы в компании <b>\"Привет, студент!\"</b>:<br/>" +
                                            "Вы будете уверены в качестве выполненной работы, т.к. у нас работают" +
                                            "квалифицированные специалисты, которые профессионально " +
                                            "занимаются написанием студенческих работ.<br/>" +
                                            "Вы в любой момент получите ответы, на все интересующие вопросы. Ваш заказ будет" +
                                            "курировать персональный менеджер. Есть возможность поэтапной сдачи работы преподавателю.<br/>" +
                                            "Мы заинтересованы в долгосрочном сотрудничестве в написании для Вас студенческих" +
                                            "работ и в Ваших рекомендациях друзьям, " +
                                            "поэтому максимально эффективно подбираем специалиста.<br/><br/>" +
                                            "<b>\"Привет, студент!\"</b> - онлайн друг современного студента<br/>" +
                                            "Сайт проекта <a href=\"www.hellostudent.ru\">www.hellostudent.ru</a><br/>" +
                                            "<b>+7-953-916-0500</b>",
                                            string.Empty,
                                            model.OrderNumber,
                                            model.Email,
                                            model.Phone,
                                            model.WorkSubject,
                                            model.Subject,
                                            model.WorkType,
                                            model.Date.Value.ToShortDateString()),
                    DisplayNameFrom = "HelloStudent.ru",
                    From = "student.hello@yandex.ru",
                    Subject = string.Format("Заказ работы (Заявка №{0})", model.OrderNumber),
                    To = model.Email
                };

            EmailService.SendMessage(emToClient,
                    "student.hello@yandex.ru",
                    "123456aaa111",
                    "smtp.yandex.ru",
                    587,
                    true);

            return RedirectToAction("Order", new {finish = true, orderNumber = model.OrderNumber, email = model.Email});
        }
    }
}