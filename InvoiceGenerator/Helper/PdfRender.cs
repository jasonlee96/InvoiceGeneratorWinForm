using PuppeteerSharp;
using PuppeteerSharp.Media;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceGenerator.Helper
{
    public class PdfRender
    {
        public async Task<string> PdfSharpConvert(string html, string invoiceId)
        {
            await new BrowserFetcher().DownloadAsync(BrowserFetcher.DefaultRevision);
            var browser = await Puppeteer.LaunchAsync(new LaunchOptions
            {
                Headless = true
            });
            var page = await browser.NewPageAsync();
            await page.EmulateMediaTypeAsync(MediaType.Screen);
            await page.SetContentAsync(html);
            var pdfContent = await page.PdfStreamAsync(new PdfOptions
            {
                Format = PaperFormat.A4,
                PrintBackground = true
            });

            string outputPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Invoices");

            Directory.CreateDirectory(outputPath);

            outputPath = Path.Combine(outputPath, "Invoice_"+invoiceId+".pdf");

            using (FileStream file = new FileStream(outputPath, FileMode.Create, System.IO.FileAccess.Write))
            {
                byte[] bytes = new byte[pdfContent.Length];
                pdfContent.Read(bytes, 0, (int)pdfContent.Length);
                file.Write(bytes, 0, bytes.Length);
                pdfContent.Close();
            }
            return outputPath;
        }
    }
}
