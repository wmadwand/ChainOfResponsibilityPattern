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
        public PaymentHandler succesor;

        public abstract void Handle(Receiver receiver);
    }

    public class BankPaymentHandler : PaymentHandler
    {
        public override void Handle(Receiver receiver)
        {
            if (receiver.bankPayment)
            {
                Console.WriteLine("bankPayment ok");
            }
            else if (succesor != null)
            {
                succesor.Handle(receiver);
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
                succesor?.Handle(receiver);
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
                succesor?.Handle(receiver);
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
            Receiver receiver = new Receiver(false, false, false);

            PaymentHandler bankPayment = new BankPaymentHandler();
            PaymentHandler cashPayment = new CashPaymentHandler();
            PaymentHandler payPalPayment = new PayPalPaymentHandler();
            PaymentHandler noPayment = new NoPaymentHandler();

            payPalPayment.succesor = bankPayment;
            bankPayment.succesor = cashPayment;
            cashPayment.succesor = noPayment;

            payPalPayment.Handle(receiver);
        }
    }
}
