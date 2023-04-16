using System.Text;
using UglyToad.PdfPig;
using UglyToad.PdfPig.Content;
using UglyToad.PdfPig.DocumentLayoutAnalysis.TextExtractor;

namespace ExtractTextFromPdf
{
  internal class Program
  {
    static void Main(string[] args)
    {
      if (args.Length == 0)
      {
        Console.WriteLine("Usage: ExtractTextFromPdf.exe <file.pdf> <password(optional)>");
        return;
      }

      string filename = args[0];
      if (!File.Exists(filename))
      {
        Console.WriteLine("File " + filename + " not found");
        return;
      }

      string password = args.Length == 2 ? args[1] : string.Empty;

      string errorMessage = string.Empty;
      StringBuilder stringBuilder = TextFromPdf(filename, password, out errorMessage);
      if (!string.IsNullOrEmpty(errorMessage))
      {
        Console.Write(errorMessage);
        return;
      }

      File.WriteAllText(filename.Replace(".pdf", ".txt"), stringBuilder.ToString());
      Console.WriteLine("Success. Text extracted to " + filename.Replace(".pdf", ".txt"));
    }

    private static StringBuilder TextFromPdf(string filename, string password, out string errorMessage)
    {
      errorMessage = string.Empty;
      StringBuilder stringBuilder = new StringBuilder();

      try
      {
        // PdfPig is a fully open-source Apache 2.0 licensed and .NET Standard compatible library that enables users to read and create PDFs in C#
        // https://uglytoad.github.io/PdfPig/
        // https://github.com/UglyToad/PdfPig/wiki
        PdfDocument pdfDocument = PdfDocument.Open(filename, new ParsingOptions() { Password = password });
        if (pdfDocument == null)
        {
          errorMessage = "Cannot open document " + filename;
          return stringBuilder;
        }

        foreach (Page page in pdfDocument.GetPages())
        {
          string text = ContentOrderTextExtractor.GetText(page, true);
          stringBuilder.Append(text);
          stringBuilder.Append("\n\n");
        }
      }
      catch (Exception ex)
      {
        errorMessage = ex.Message;
      }

      return stringBuilder;
    }
  }
}