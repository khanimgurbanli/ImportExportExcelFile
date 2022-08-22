using ECommerceAPI.Domain.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Domain.EmailService
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string file);

    }
}
