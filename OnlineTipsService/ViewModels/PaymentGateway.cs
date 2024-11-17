using Npgsql.PostgresTypes;
using OnlineTipsService.Domain;
using OnlineTipsService.App;
using System;

namespace ThankYou.ViewModels
{
    public class PaymentGateway
    {
        private readonly Tip tip;
        private bool IsAvailable { get; set; }

        public PaymentGateway(Tip tip)
        {
            this.tip = tip;
        }

        

        public string DoOperation()
        {
            var res = "Exception";
            //Имитация работы порта

            if (IsAvailable)
            {
                using (PostgresContext db = new PostgresContext())
                {
                    db.Tips.Add(tip);
                    res = "Операция прошла успешно! Спасибо за отправленные чаевые";
                }
            }

            return res;
        }
    }
}
