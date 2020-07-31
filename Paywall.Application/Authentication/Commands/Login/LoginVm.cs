using System;

namespace Paywall.Application.Commands
{
    public class LoginVm
    {
        public Guid ApiKey { get; set; }
        public string DisplayName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}