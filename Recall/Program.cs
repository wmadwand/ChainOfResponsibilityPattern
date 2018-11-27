using System;

namespace Recall
{
    public class Receiver
    {
        public bool bankPayment;
        public bool paypalPayment;
        private int _moneyTotal;

        public Receiver(bool bankPayment, bool paypalPayment)
        {
            this.bankPayment = bankPayment;
            this.paypalPayment = paypalPayment;
        }

        public void GetMoney(int count)
        {
            _moneyTotal += count;
            Console.WriteLine(_moneyTotal);
        }
    }

    public abstract class PaymentHandler
    {
        protected PaymentHandler successor;

        public void SetSuccessor(PaymentHandler successor)
        {
            this.successor = successor;
        }

        public virtual void Handle(Receiver receiver, int money)
        {
            successor?.Handle(receiver, money);
        }
    }

    public class BankPaymentHandler : PaymentHandler
    {
        public override void Handle(Receiver receiver, int money)
        {
            if (receiver.bankPayment)
            {
                Console.WriteLine("BankPayment ok");
                receiver.GetMoney(money);
            }
            else
            {
                base.Handle(receiver, money); // successor?.Handle(receiver);
            }
        }
    }

    public class PayPalPaymentHandler : PaymentHandler
    {
        public override void Handle(Receiver receiver, int money)
        {
            if (receiver.paypalPayment)
            {
                Console.WriteLine("PayPalPayment ok");
                receiver.GetMoney(money);
            }
            else
            {
                successor?.Handle(receiver, money);
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Receiver receiver = new Receiver(false, true);

            PaymentHandler bankP = new BankPaymentHandler();
            PaymentHandler paypP = new PayPalPaymentHandler();

            bankP.SetSuccessor(paypP);
            bankP.Handle(receiver, 500);
        }
    }
}
