using ThankYou.DB.Domain;

namespace ThankYou.ViewModels
{
    public class PayViewModel
    {
        private string paymentMethod = "card";

        public Tip Tip { get; set; } = new Tip();

        public string PaymentMethod
        {
            get { return paymentMethod; }
            set 
            {
                if (!value.Equals("spb") && !value.Equals("card"))
                    throw new ArgumentException("Incorrect string. " +
                        "The value must be either \"СБП\" or \"Банковская карта\"");

                paymentMethod = value; 
            }
        }

        public PayViewModel() { }

        public PayViewModel(Tip tip) { Tip = tip; }
    }
}
