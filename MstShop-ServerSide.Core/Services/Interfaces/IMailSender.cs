using System;
using System.Collections.Generic;
using System.Text;

namespace MstShop_ServerSide.Core.Services.Interfaces
{
    public interface IMailSender
    {
        void Send(string to, string subject, string body);
    }
}
