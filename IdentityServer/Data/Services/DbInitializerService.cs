using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using Microsoft.EntityFrameworkCore;

namespace IdentityServer.Data.Services
{
    public class DbInitializerService
    {
        private readonly PersistedGrantDbContext persistedGrantDbContext;
        private readonly ConfigurationDbContext context;

        public DbInitializerService(PersistedGrantDbContext persistedGrantDbContext, ConfigurationDbContext configurationDbContext)
        {
            this.persistedGrantDbContext = persistedGrantDbContext ?? throw new ArgumentNullException(nameof(persistedGrantDbContext));
            this.context = configurationDbContext ?? throw new ArgumentNullException(nameof(configurationDbContext));
        }

        public void Initialize()
        {
            persistedGrantDbContext.Database.Migrate();
            context.Database.Migrate();
            if (!context.Clients.Any())
            {
                foreach(var client in Config.Clients)
                {
                    context.Clients.Add(client.ToEntity());
                }
                context.SaveChanges();
            }

            if (!context.IdentityResources.Any())
            {
                foreach (var resource in Config.IdentityResources)
                {
                    context.IdentityResources.Add(resource.ToEntity());
                }
                context.SaveChanges();
            }

            if (!context.ApiScopes.Any())
            {
                foreach (var scope in Config.ApiScopes)
                {
                    context.ApiScopes.Add(scope.ToEntity());
                }
                context.SaveChanges();
            }
        }
    }
}
