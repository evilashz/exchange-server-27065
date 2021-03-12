using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;

namespace Microsoft.Exchange.Management.ReportingWebService
{
	// Token: 0x02000026 RID: 38
	public class QueryValidationInspector : IDispatchMessageInspector
	{
		// Token: 0x060000CC RID: 204 RVA: 0x0000402C File Offset: 0x0000222C
		public object AfterReceiveRequest(ref Message request, IClientChannel channel, InstanceContext instanceContext)
		{
			if (request.Properties.ContainsKey("UriTemplateMatchResults"))
			{
				HttpRequestMessageProperty httpRequestMessageProperty = (HttpRequestMessageProperty)request.Properties[HttpRequestMessageProperty.Name];
				UriTemplateMatch uriTemplateMatch = (UriTemplateMatch)request.Properties["UriTemplateMatchResults"];
				string text = uriTemplateMatch.QueryParameters["$orderby"];
				if (!string.IsNullOrEmpty(text) && text.IndexOf('\'') >= 0)
				{
					ServiceDiagnostics.ThrowError(ReportingErrorCode.InvalidQueryException, "Single quotation marks is not supported in $orderby.");
				}
			}
			return null;
		}

		// Token: 0x060000CD RID: 205 RVA: 0x000040AB File Offset: 0x000022AB
		public void BeforeSendReply(ref Message reply, object correlationState)
		{
		}

		// Token: 0x04000053 RID: 83
		private const string OrderbyQueryParameter = "$orderby";

		// Token: 0x04000054 RID: 84
		private const string UriTemplateMatchResultsQuery = "UriTemplateMatchResults";
	}
}
