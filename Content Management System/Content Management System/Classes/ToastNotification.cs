﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Notification.Wpf;

namespace Content_Management_System.Classes
{
    public class ToastNotification
    {
        public string Title { get; set; }
        public string Message { get; set; }
        public NotificationType Type { get; set; }


        public ToastNotification() 
        { 
        
        }

        public ToastNotification(string title, string message, NotificationType type)
        {
            Title = title;
            Message = message;
            Type = type;
        }
    }
}
