using System;
using System.IO;
using System.Net;
using System.Web;
using System.Xml;

namespace Microsoft.Exchange.Autodiscover.WCF
{
	// Token: 0x02000069 RID: 105
	public class LegacyHttpHandler : AutodiscoverDiscoveryHttpHandler
	{
		// Token: 0x060002DD RID: 733 RVA: 0x00013380 File Offset: 0x00011580
		internal override void GenerateResponse(HttpContext context)
		{
			HttpRequest request = context.Request;
			HttpResponse response = context.Response;
			base.AddEndpointEnabledHeaders(response);
			HttpStatusCode statusCode = request.IsAuthenticated ? HttpStatusCode.OK : HttpStatusCode.Unauthorized;
			response.StatusCode = (int)statusCode;
			this.GenerateErrorResponse(context);
		}

		// Token: 0x060002DE RID: 734 RVA: 0x000133C8 File Offset: 0x000115C8
		private void GenerateErrorResponse(HttpContext context)
		{
			HttpResponse response = context.Response;
			response.ContentType = "text/xml; charset=utf-8";
			using (TextWriter output = response.Output)
			{
				XmlWriter xmlFragment = XmlWriter.Create(output, new XmlWriterSettings
				{
					Indent = true,
					IndentChars = "  ",
					ConformanceLevel = ConformanceLevel.Document
				});
				bool useClientCertificateAuthentication = false;
				RequestData requestData = new RequestData(null, useClientCertificateAuthentication, CallerRequestedCapabilities.GetInstance(context));
				Common.GenerateErrorResponseDontLog(xmlFragment, "http://schemas.microsoft.com/exchange/autodiscover/responseschema/2006", "600", Strings.InvalidRequest, string.Empty, requestData, base.GetType().AssemblyQualifiedName);
			}
		}
	}
}
