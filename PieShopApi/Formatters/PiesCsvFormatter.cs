using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;
using PieShopApi.Models.Pies;
using System.Text;

namespace PieShopApi.Formatters
{
    public class PieCsvFormatter : TextOutputFormatter
    {
        public PieCsvFormatter()
        {
            SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("application/csv"));
            SupportedEncodings.Add(Encoding.UTF8);
            SupportedEncodings.Add(Encoding.Unicode);
        }
        protected override bool CanWriteType(Type? type)
            => typeof(PieDto).IsAssignableFrom(type)
                || typeof(IEnumerable<PieDto>).IsAssignableFrom(type);
        public override async Task WriteResponseBodyAsync(
        OutputFormatterWriteContext context, Encoding selectedEncoding)
        {
            var httpContext = context.HttpContext;
            var serviceProvider = httpContext.RequestServices;

            var buffer = new StringBuilder();

            if (context.Object is IEnumerable<PieDto> pies)
            {
                foreach (var pie in pies)
                {
                    FormatCsv(buffer, pie);
                }
            }
            else
            {
                FormatCsv(buffer, (PieDto)context.Object!);
            }

            await httpContext.Response.WriteAsync(buffer.ToString(), selectedEncoding);
        }
        private static void FormatCsv(StringBuilder buffer, PieDto pie)
        {
            buffer.AppendLine($"{pie.Id},{pie.Name},{pie.Description},{string.Join("|", pie.AllergyItems)}");
        }
    }

}
