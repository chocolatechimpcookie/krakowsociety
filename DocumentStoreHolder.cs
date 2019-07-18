using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Raven.Client.Documents;
using Raven.Client.Documents.Indexes;
using Raven.Client.ServerWide;
using Raven.Client.ServerWide.Operations;
using KrawkowSociety.Models;


namespace KrawkowSociety
{
    public interface IDocumentStoreHolder
    {
        IDocumentStore Store { get; }
    }

    public class DocumentStoreHolder : IDocumentStoreHolder
    {
        private readonly ILogger<DocumentStoreHolder> _logger;
        public IDocumentStore Store { get; }


        public DocumentStoreHolder(IOptions<RavenSettings> ravenSettings, ILogger<DocumentStoreHolder> logger)
        {
            this._logger = logger;
            this._logger.LogInformation("opening up document store holder");
            var settings = ravenSettings.Value;
            Store = new DocumentStore()
            {
                Urls = new[] { settings.Url },
                Database = settings.Database
            };

            Store.Initialize();
            this._logger.LogInformation("store initializes");
            CreateDatabaseIfNotExists();
            this._logger.LogInformation("create db");



        }
        private void CreateDatabaseIfNotExists()
        {
            var database = Store.Database;

            // Only RavenDB.Client 4.0.0-rc-40023 not 4.0.0 has the admin variable
            // I might need to rewrite this to keep it up to date or just keep copies of this dependency
            var dbRecord = Store.Admin.Server.Send(new GetDatabaseRecordOperation(database));

            if (dbRecord == null)
            {
                this._logger.LogInformation("RavenDB database does not exist");
                dbRecord = new DatabaseRecord(database);

                var createResult = Store.Admin.Server.Send(new CreateDatabaseOperation(dbRecord));

                if (createResult.Name != null)
                {
                    // can seed data here
                    using (var session = Store.OpenSession())
                    {
  
                        session.SaveChanges();
                    }
                }


            }

        }
    }
}
