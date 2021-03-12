using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;

namespace Microsoft.Exchange.Management.ReportingWebService
{
	// Token: 0x0200003A RID: 58
	public class ResponseFormatInspector : IDispatchMessageInspector
	{
		// Token: 0x06000151 RID: 337 RVA: 0x00007448 File Offset: 0x00005648
		public object AfterReceiveRequest(ref Message request, IClientChannel channel, InstanceContext instanceContext)
		{
			if (request.Properties.ContainsKey("UriTemplateMatchResults"))
			{
				HttpRequestMessageProperty httpRequestMessageProperty = (HttpRequestMessageProperty)request.Properties[HttpRequestMessageProperty.Name];
				UriTemplateMatch uriTemplateMatch = (UriTemplateMatch)request.Properties["UriTemplateMatchResults"];
				string value = uriTemplateMatch.QueryParameters["$format"];
				if ("json".Equals(value, StringComparison.OrdinalIgnoreCase))
				{
					uriTemplateMatch.QueryParameters.Remove("$format");
					httpRequestMessageProperty.Headers["Accept"] = "application/json;odata=verbose";
				}
				else if ("Atom".Equals(value, StringComparison.OrdinalIgnoreCase))
				{
					uriTemplateMatch.QueryParameters.Remove("$format");
					httpRequestMessageProperty.Headers["Accept"] = "application/atom+xml";
				}
				else if (!string.IsNullOrEmpty(value))
				{
					ServiceDiagnostics.ThrowError(ReportingErrorCode.InvalidFormatQuery, Strings.InvalidFormatQuery);
				}
			}
			return null;
		}

		// Token: 0x06000152 RID: 338 RVA: 0x0000752D File Offset: 0x0000572D
		public void BeforeSendReply(ref Message reply, object correlationState)
		{
		}

		// Token: 0x040000B2 RID: 178
		private const string FormatQueryParameter = "$format";

		// Token: 0x040000B3 RID: 179
		private const string AcceptHeader = "Accept";

		// Token: 0x040000B4 RID: 180
		private const string UriTemplateMatchResultsQuery = "UriTemplateMatchResults";
	}
}
