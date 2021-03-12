using System;
using Microsoft.Exchange.InfoWorker.Common.Availability.Proxy;
using Microsoft.Exchange.SoapWebClient.EWS;

namespace Microsoft.Exchange.InfoWorker.Common.MessageTracking
{
	// Token: 0x02000285 RID: 645
	internal class GetMessageTrackingReportRequestTypeWrapper
	{
		// Token: 0x1700049E RID: 1182
		// (get) Token: 0x0600125F RID: 4703 RVA: 0x0005554D File Offset: 0x0005374D
		internal Microsoft.Exchange.SoapWebClient.EWS.GetMessageTrackingReportRequestType WrappedRequest
		{
			get
			{
				return this.request;
			}
		}

		// Token: 0x06001260 RID: 4704 RVA: 0x00055555 File Offset: 0x00053755
		internal GetMessageTrackingReportRequestTypeWrapper(Microsoft.Exchange.SoapWebClient.EWS.GetMessageTrackingReportRequestType request)
		{
			this.request = request;
		}

		// Token: 0x06001261 RID: 4705 RVA: 0x00055564 File Offset: 0x00053764
		internal Microsoft.Exchange.SoapWebClient.EWS.GetMessageTrackingReportRequestType PrepareEWSRequest(int version)
		{
			VersionConverter.Convert(this.request, version);
			return this.request;
		}

		// Token: 0x06001262 RID: 4706 RVA: 0x00055578 File Offset: 0x00053778
		internal Microsoft.Exchange.InfoWorker.Common.Availability.Proxy.GetMessageTrackingReportRequestType PrepareRDRequest(int version)
		{
			VersionConverter.Convert(this.request, version);
			return MessageConverter.CopyEWSTypeToDispatcherType(this.request);
		}

		// Token: 0x04000C07 RID: 3079
		private Microsoft.Exchange.SoapWebClient.EWS.GetMessageTrackingReportRequestType request;
	}
}
