# ExtractTextFromPdf
Extracts text from pdf documents

Create your own Console project in Visual Studio and simply put this code into Program.cs
I've used .NET 6.0 LTS but something more up-to-date should work.

The code extracts only the text, ignoring images.
It saves the result in a file with same name of the pdf but extension .txt

Usage: ExtractTextFromPdf.exe <file.pdf> <password(optional)> 

It uses PdfPig to open pdf documents (https://uglytoad.github.io/PdfPig/ and https://github.com/UglyToad/PdfPig/wiki)
