using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;

namespace Catalog.Api.Infrastructure.Exporter
{
    public class ExcelDataExporter : IDataExporter
    {
        public ExportData Export<TType>(IEnumerable<TType> list) where TType : class
        {
            var stream = new MemoryStream();

            using (var package = new ExcelPackage(stream))
            {
                var workSheet = package.Workbook.Worksheets.Add("Sheet1");
                workSheet.Cells.LoadFromCollection(list, true);
                package.Save();
            }
            stream.Position = 0;

            var excelName = $"{typeof(TType).Name}-{DateTime.Now:yyyyMMddHHmmssfff}.xlsx";

            return new ExportData
            {
                Stream = stream,
                ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                FileName = excelName
            };
        }
    }
}