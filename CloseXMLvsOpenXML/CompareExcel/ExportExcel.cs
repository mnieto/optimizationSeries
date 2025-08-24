using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using NPOI.XSSF.UserModel;

namespace CompareExcel
{

    [MemoryDiagnoser(false)]
    [SimpleJob(RuntimeMoniker.Net48)]
    [SimpleJob(RuntimeMoniker.Net80)]
    public class ExportExcel
    {

        private List<Person> data = new List<Person>();

        [GlobalSetup]
        public void Setup()
        {
            data = Person.BuildPeopleList();
            OfficeOpenXml.ExcelPackage.License.SetNonCommercialPersonal("Miguel");
        }

        [Benchmark]
        public void ExportToEPPLus()
        {
            using (var package = new OfficeOpenXml.ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("People");
                worksheet.Cells["A1"].LoadFromCollection(data, true);
                //package.SaveAs(new FileInfo("EPPlusExport.xlsx"));
            }
        }

        [Benchmark]
        public void ExportToClosedXML()
        {
            using (var workbook = new ClosedXML.Excel.XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("People");
                worksheet.Cell(1, 1).InsertTable(data);
                //workbook.SaveAs("ClosedXMLExport.xlsx");
            }
        }

        [Benchmark]
        public void ExportToNPOI()
        {
            var properties = typeof(Person).GetProperties();
            using (var workbook = new XSSFWorkbook())
            {
                var sheet = workbook.CreateSheet("People");
                for (int i = 0; i < data.Count; i++)
                {
                    var person = data[i];
                    var row = sheet.CreateRow(i);
                    for (int j = 0; j < properties.Length; j++)
                    {
                        var cell = row.CreateCell(j);
                        var value = properties[j].GetValue(person);
                        if (value != null)
                        {
                            cell.SetCellValue(value.ToString());
                        }
                    }
                }
                //using (var stream = new FileStream("NPOIExport.xls", FileMode.Create, FileAccess.Write))
                //{
                //    workbook.Write(stream);
                //}
            }
        }

    }
}
