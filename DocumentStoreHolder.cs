using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Raven.Client.Documents;
using KrawkowSociety.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        }
    }
}
