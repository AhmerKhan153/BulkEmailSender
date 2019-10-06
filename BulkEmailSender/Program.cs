using BulkEmailSender.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Configuration;
using Microsoft.Extensions.Hosting;

namespace BulkEmailSender
{
    class Program
    {
        static void Main(string[] args)
        {
            Sender sender = new Sender();
            sender.InitSend();
        }

    }
}
