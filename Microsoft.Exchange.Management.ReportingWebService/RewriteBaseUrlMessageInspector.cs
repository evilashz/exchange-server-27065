using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using System.ServiceModel.Web;
using System.Web;

namespace Microsoft.Exchange.Management.ReportingWebService
{
	// Token: 0x02000039 RID: 57
	public class RewriteBaseUrlMessageInspector : IDispatchMessageInspector
	{
		// Token: 0x0600014E RID: 334 RVA: 0x00007334 File Offset: 0x00005534
		public object AfterReceiveRequest(ref Message request, IClientChannel channel, InstanceContext instanceContext)
		{
			if (WebOperationContext.Current != null && WebOperationContext.Current.IncomingRequest.UriTemplateMatch != null)
			{
				string text = WebOperationContext.Current.IncomingRequest.Headers["msExchProxyUri"];
				if (!string.IsNullOrEmpty(text))
				{
					Uri uri = new Uri(new Uri(text), HttpContext.Current.Request.Url.PathAndQuery);
					UriBuilder uriBuilder = new UriBuilder(WebOperationContext.Current.IncomingRequest.UriTemplateMatch.BaseUri);
					UriBuilder uriBuilder2 = new UriBuilder(WebOperationContext.Current.IncomingRequest.UriTemplateMatch.RequestUri);
					uriBuilder.Host = uri.Host;
					uriBuilder.Port = uri.Port;
					uriBuilder2.Host = uri.Host;
					uriBuilder2.Port = uri.Port;
					OperationContext.Current.IncomingMessageProperties["MicrosoftDataServicesRootUri"] = uriBuilder.Uri;
					OperationContext.Current.IncomingMessageProperties["MicrosoftDataServicesRequestUri"] = uriBuilder2.Uri;
				}
			}
			return null;
		}

		// Token: 0x0600014F RID: 335 RVA: 0x0000743D File Offset: 0x0000563D
		public void BeforeSendReply(ref Message reply, object correlationState)
		{
		}

		// Token: 0x040000AE RID: 174
		private const string FormatQueryParameter = "$format";

		// Token: 0x040000AF RID: 175
		private const string AcceptHeader = "Accept";

		// Token: 0x040000B0 RID: 176
		private const string UriTemplateMatchResultsQuery = "UriTemplateMatchResults";

		// Token: 0x040000B1 RID: 177
		private const string ProxyUri = "msExchProxyUri";
	}
}
