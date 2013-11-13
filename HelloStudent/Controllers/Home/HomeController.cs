using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
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
                                    Text = "1 день",
                                    Value = "1 день"
                                },
                            new SelectListItem
                                {
                                    Text = "3 дня",
                                    Value = "3 дня"
                                },
                            new SelectListItem
                                {
                                    Text = "5 дней",
                                    Value = "5 дней"
                                },
                            new SelectListItem
                                {
                                    Text = "более 5 дней",
                                    Value = "более 5 дней"
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
    }
}
