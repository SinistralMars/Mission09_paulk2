using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Models
{
    public class EFPaymentRepository : IPaymentRepository
    {
        private BookStoreContext context;

        public EFPaymentRepository (BookStoreContext temp)
        {
            context = temp;
        }
        public IQueryable<Payment> Payments => context.Payments.Include(x => x.Lines).ThenInclude(x => x.Book);

        public void SavePayment(Payment payment)
        {
            context.AttachRange(payment.Lines.Select(x => x.Book));

            if (payment.PaymentId == 0)
            {
                context.Payments.Add(payment);
            }

            context.SaveChanges();
        }
    }
}
