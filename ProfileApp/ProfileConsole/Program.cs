using Profile.DAL.Entities;
using Profile.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProfileConsole
{
    public class Program
    {
        static void Main(string[] args)
        {
            //try
            //{
                IdentityUnitOfWork a = new IdentityUnitOfWork("ApplicationContext");
                ClientProfile client = new ClientProfile();
                client.Name = "Vlad";
                client.Id = "1";
                client.Address = "Shukevicha";
                a.ClientManager.Create(client);
            //}catch(Exception a)
            //{
            //    Console.WriteLine(a.Message);
            //}
        }
    }
}
