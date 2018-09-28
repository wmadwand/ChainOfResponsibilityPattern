using System;

namespace ChainOfResponsibility1
{
    public class Receiver
    {
        public bool bankPayment;
        public bool cashPayment;
        public bool payPalPayment;

        public Receiver(bool bankPayment, bool cashPayment, bool payPalPayment)
        {
            this.bankPayment = bankPayment;
            this.cashPayment = cashPayment;
            this.payPalPayment = payPalPayment;
        }
    }

    public abstract class PaymentHandler
    {
        public PaymentHandler successor;

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
                Console.WriteLine("bankPayment ok");
            }
            else
            {
                base.Handle(receiver);
            }
        }
    }

    public class CashPaymentHandler : PaymentHandler
    {
        public override void Handle(Receiver receiver)
        {
            if (receiver.cashPayment)
            {
                Console.WriteLine("cashPayment ok");
            }
            else
            {
                base.Handle(receiver);
            }
        }
    }

    public class PayPalPaymentHandler : PaymentHandler
    {
        public override void Handle(Receiver receiver)
        {
            if (receiver.payPalPayment)
            {
                Console.WriteLine("payPalPayment ok");
            }
            else
            {
                successor?.Handle(receiver);
            }
        }
    }

    public class NoPaymentHandler : PaymentHandler
    {
        public override void Handle(Receiver receiver)
        {
            Console.WriteLine("Any payment is impossible");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Receiver receiver = new Receiver(false, true, false);

            PaymentHandler bankPayment = new BankPaymentHandler();
            PaymentHandler cashPayment = new CashPaymentHandler();
            PaymentHandler payPalPayment = new PayPalPaymentHandler();
            PaymentHandler noPayment = new NoPaymentHandler();

            payPalPayment.successor = bankPayment;
            bankPayment.successor = cashPayment;
            cashPayment.successor = noPayment;

            payPalPayment.Handle(receiver);
        }
    }
}
