namespace Catalog.Api.Infrastructure.Exporter
{
    public class DataExporterFactory
    {
        public virtual IDataExporter GetExporter(string exportType)
        {
            return exportType.ToLower() switch
            {
                "excel" => new ExcelDataExporter(),
                _ => null
            };
        }
    }
}