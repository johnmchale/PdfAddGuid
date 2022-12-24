using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas;
using iText.Kernel.Pdf.Xobject;

namespace PdfIdAdder
{

    // Here is the complete code that demonstrates how to copy a page from one PDF document
    // to another PDF document using iText 7, and add a unique ID to the destination document:
    class Program
    {
        static void Main(string[] args)
        {
            // Open the PDF file
            PdfDocument pdf = new PdfDocument(new PdfReader("C:\\PdfDownload\\file.pdf"));

            // Create a new PDF document
            using (PdfDocument outputPdf = new PdfDocument(new PdfWriter("C:\\PdfDownload\\fileGuid.pdf")))
            {
                // Loop through the pages of the PDF
                for (int i = 1; i <= pdf.GetNumberOfPages(); i++)
                {
                    // Get the page from the input PDF
                    PdfPage page = pdf.GetPage(i);

                    // Get the size of the page
                    Rectangle pageSize = page.GetPageSize();

                    // Get the width and height of the page
                    float width = pageSize.GetWidth();
                    float height = pageSize.GetHeight();

                    // Create a new page in the output PDF with the A4 page size
                    PdfPage outputPage = outputPdf.AddNewPage(PageSize.A4);

                    // Set the media box of the output page to the size of the input page
                    outputPage.SetMediaBox(new Rectangle(0, 0, width, height));

                    // Create a content stream for the output page
                    PdfCanvas canvas = new PdfCanvas(outputPage);

                    // Copy the content of the input page to the output page
                    PdfFormXObject xobject = page.CopyAsFormXObject(outputPdf);
                    canvas.AddXObjectAt(xobject, 0, 0);
                }

                // Add a unique ID to the PDF
                outputPdf.GetDocumentInfo().SetMoreInfo("ID", Guid.NewGuid().ToString());
            }
        }
    }
}
