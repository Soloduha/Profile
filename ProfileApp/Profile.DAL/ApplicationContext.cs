namespace Profile.DAL
{
    using Microsoft.AspNet.Identity.EntityFramework;
    using Profile.DAL.Entities;
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class ApplicationContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationContext(string connectionString)
            : base("name=ApplicationContext")
        {
        }

        //DbSet for each entity type that you want to include in your model. 

        public DbSet<ClientProfile> ClientProfiles { get; set; }
    }


}