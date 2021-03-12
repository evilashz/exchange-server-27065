using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;

namespace Microsoft.Exchange.Management.ReportingWebService
{
	// Token: 0x02000022 RID: 34
	public class HttpCachePolicyInspector : IDispatchMessageInspector
	{
		// Token: 0x060000B6 RID: 182 RVA: 0x00003B0C File Offset: 0x00001D0C
		public object AfterReceiveRequest(ref Message request, IClientChannel channel, InstanceContext instanceContext)
		{
			return null;
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x00003B10 File Offset: 0x00001D10
		public void BeforeSendReply(ref Message reply, object correlationState)
		{
			if (reply != null && reply.Properties.ContainsKey(HttpResponseMessageProperty.Name))
			{
				HttpResponseMessageProperty httpResponseMessageProperty = (HttpResponseMessageProperty)reply.Properties[HttpResponseMessageProperty.Name];
				httpResponseMessageProperty.Headers.Set("Cache-Control", "no-cache, no-store");
			}
		}
	}
}
