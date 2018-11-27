using System;

namespace Recall
{
    public class Receiver
    {
        public bool bankPayment;
        public bool paypalPayment;

        public Receiver(bool bankPayment, bool paypalPayment)
        {
            this.bankPayment = bankPayment;
            this.paypalPayment = paypalPayment;
        }
    }

    public abstract class PaymentHandler
    {
        protected PaymentHandler successor;

        public void SetSuccessor(PaymentHandler successor)
        {
            this.successor = successor;
        }

        public virtual void Handle(Receiver receiver)
        {
            successor?.Handle(receiver);
        }
    }

    public class BankPaymentHandler : PaymentHandler
    {
        public override void Handle(Receiver receiver)
        {
            if (receiver.bankPayment)
            {
                Console.WriteLine("BankPayment ok");
            }
            else
            {
                base.Handle(receiver); // successor?.Handle(receiver);
            }
        }
    }

    public class PayPalPaymentHandler : PaymentHandler
    {
        public override void Handle(Receiver receiver)
        {
            if (receiver.paypalPayment)
            {
                Console.WriteLine("PayPalPayment ok");
            }
            else
            {
                successor?.Handle(receiver);
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Receiver receiver = new Receiver(true, false);

            PaymentHandler bankP = new BankPaymentHandler();
            PaymentHandler paypP = new PayPalPaymentHandler();

            bankP.SetSuccessor(paypP);
            bankP.Handle(receiver);
        }
    }
}
