using Microsoft.AspNetCore.SignalR;
using RaduMVC.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RaduMVC.Hubs
{
    public class MessageHub : Hub, IAddMemberSubscriber
    {
        private readonly MessageService messageService;
        private IInternshipService internshipService;

        public MessageHub(MessageService messageService, IInternshipService internshipService)
        {
            this.messageService = messageService;
            this.internshipService = internshipService;
            internshipService.SubscribeToAddMember(this);
        }
        
        public async Task SendMessage(string user, string message)
        {
            messageService.AddMessage(user, message);
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }
    }
}
