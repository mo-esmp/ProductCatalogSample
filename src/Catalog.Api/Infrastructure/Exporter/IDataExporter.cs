using System.Collections.Generic;
using System.IO;

namespace Catalog.Api.Infrastructure.Exporter
{
    public interface IDataExporter
    {
        ExportData Export<TType>(IEnumerable<TType> list) where TType : class;
    }

    public class ExportData
    {
        public MemoryStream Stream { get; set; }

        public string ContentType { get; set; }

        public string FileName { get; set; }
    }
}