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

        //private void CreateDatabaseIfNotExists

        public DocumentStoreHolder(IOptions<RavenSettings> ravenSettings, ILogger<DocumentStoreHolder> logger)
        {
            this._logger = logger;
            var settings = ravenSettings.Value;
            Store = new DocumentStore()
            {
                Urls = new[] { settings.Url },
                Database = settings.Database
            };

            Store.Initialize();
            CreateDatabaseIfNotExists();



        }
        private void CreateDatabaseIfNotExists()
        {
            var database = Store.Database;
            // Only RavenDB.Client 4.0.0-rc-40023 not 4.0.0 has the admin variable
            // either I need a replacement or I need the same client
            // I might need to rewrite this to keep it up to date or just keep copies of this dependency
            var dbRecord = Store.Admin.Server.Send(new GetDatabaseRecordOperation(database));

        }
    }
}
