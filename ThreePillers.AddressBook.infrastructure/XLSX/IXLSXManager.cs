using ClosedXML.Excel;

namespace ThreePillers.AddressBook.Application.Abstractions.XLSX;

internal sealed class XLSXManager : IXLSXManager
{
    public IFormFile GenerateXLSX(
        List<AddressBookEntry> addressBookEntries,
        CancellationToken cancellationToken = default
    )
    {
        using var workbook = new XLWorkbook();
        var worksheet = workbook.Worksheets.Add("Address_Book_Entries");

        worksheet.Cell(1, 1).Value = "Id";
        worksheet.Cell(1, 2).Value = "Full Name";
        worksheet.Cell(1, 3).Value = "Email";
        worksheet.Cell(1, 4).Value = "Mobile Number";
        worksheet.Cell(1, 5).Value = "Date of Birth";
        worksheet.Cell(1, 6).Value = "Address";
        worksheet.Cell(1, 7).Value = "Age";
        worksheet.Cell(1, 8).Value = "Department";
        worksheet.Cell(1, 9).Value = "Job";

        for (int i = 0; i < addressBookEntries.Count(); i++)
        {
            var entry = addressBookEntries[i];
            worksheet.Cell(i + 2, 1).Value = entry.Id;
            worksheet.Cell(i + 2, 2).Value = entry.FullName;
            worksheet.Cell(i + 2, 3).Value = entry.Email;
            worksheet.Cell(i + 2, 4).Value = entry.Phone;
            worksheet.Cell(i + 2, 5).Value = entry.DateOfBirth.ToString("yyyy-MM-dd");
            worksheet.Cell(i + 2, 6).Value = entry.Address;
            worksheet.Cell(i + 2, 7).Value = entry.Age;
            worksheet.Cell(i + 2, 8).Value = entry.Department.Name;
            worksheet.Cell(i + 2, 9).Value = entry.Job.Title;
        }

        worksheet.Columns().AdjustToContents();
        var stream = new MemoryStream();
        workbook.SaveAs(stream);
        stream.Position = 0;
        var fileName = $"Entries_{Guid.NewGuid()}";

        return new FormFile(stream, 0, stream.Length, fileName, $"{fileName}.xlsx")
        {
            Headers = new HeaderDictionary(),
            ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
        };
    }
}
