namespace CQRS.Aplicacao.Util
{
    public class Pdf
    {
        public void GerarPdf()
        {
            string html = "<h1>Olá Mundo</h1><p>PDF gerado com IronPDF</p>";

            // Criar instância do renderizador
            var renderer = new ChromePdfRenderer();

            // Gerar PDF a partir do HTML
            var pdf = renderer.RenderHtmlAsPdf(html);

            // Salvar PDF no disco
            pdf.SaveAs("saida.pdf");
        }
    }
}
