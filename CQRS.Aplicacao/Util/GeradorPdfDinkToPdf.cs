using DinkToPdf;
using DinkToPdf.Contracts;

namespace CQRS.Aplicacao.Util
{
    public class GeradorPdfDinkToPdf
    {
        private readonly IConverter _converter;

        public GeradorPdfDinkToPdf(IConverter converter)
        {
            _converter = converter;
        }

        public byte[] GerarPdf(string html)
        {
            var documento = new HtmlToPdfDocument()
            {
                GlobalSettings = {
                    ColorMode = ColorMode.Color,
                    Orientation = Orientation.Portrait,
                    PaperSize = PaperKind.A4,
                    Margins = new MarginSettings { Top = 10, Bottom = 10 }
                },
                Objects = {
                    new ObjectSettings() {
                        HtmlContent = html,
                        WebSettings = { DefaultEncoding = "utf-8" }
                    }
                }
            };

            return _converter.Convert(documento);
        }
    }
}
