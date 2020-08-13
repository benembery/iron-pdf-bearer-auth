using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IronPdf;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace pdf_generation.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PDFGenerationController : ControllerBase
    {
        private readonly ILogger<PDFGenerationController> _logger;

        public PDFGenerationController(ILogger<PDFGenerationController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Generate()
        {
            var renderOptions = new PdfPrintOptions
            {
                CssMediaType = PdfPrintOptions.PdfCssMediaType.Print,
                EnableJavaScript = true,
                RenderDelay = 500,
                MarginTop = 10,
                MarginBottom = 10,
                MarginLeft = 0,
                MarginRight = 0,
            };

            var token = "Bearer A_FAKE_TOKEN";
            var renderer = new HtmlToPdf(renderOptions);
            renderer.LoginCredentials.CustomHttpHeaders.Add("Authorization", $"{token}");
            
            var uri = new Uri("http://localhost:5101/html");
            
            try {
                var output = await renderer.RenderUrlAsPdfAsync(uri);
                return new FileStreamResult(output.Stream, "application/pdf");
            }
            catch(Exception ex) {
                _logger.LogError(ex, "Failed to generate PDF");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
