using System;
using Microsoft.Exchange.InfoWorker.Common.Availability.Proxy;
using Microsoft.Exchange.SoapWebClient.EWS;

namespace Microsoft.Exchange.InfoWorker.Common.MessageTracking
{
	// Token: 0x02000284 RID: 644
	internal class FindMessageTrackingReportRequestTypeWrapper
	{
		// Token: 0x1700049D RID: 1181
		// (get) Token: 0x0600125B RID: 4699 RVA: 0x00055509 File Offset: 0x00053709
		internal Microsoft.Exchange.SoapWebClient.EWS.FindMessageTrackingReportRequestType WrappedRequest
		{
			get
			{
				return this.request;
			}
		}

		// Token: 0x0600125C RID: 4700 RVA: 0x00055511 File Offset: 0x00053711
		internal FindMessageTrackingReportRequestTypeWrapper(Microsoft.Exchange.SoapWebClient.EWS.FindMessageTrackingReportRequestType request)
		{
			this.request = request;
		}

		// Token: 0x0600125D RID: 4701 RVA: 0x00055520 File Offset: 0x00053720
		internal Microsoft.Exchange.SoapWebClient.EWS.FindMessageTrackingReportRequestType PrepareEWSRequest(int version)
		{
			VersionConverter.Convert(this.request, version);
			return this.request;
		}

		// Token: 0x0600125E RID: 4702 RVA: 0x00055534 File Offset: 0x00053734
		internal Microsoft.Exchange.InfoWorker.Common.Availability.Proxy.FindMessageTrackingReportRequestType PrepareRDRequest(int version)
		{
			VersionConverter.Convert(this.request, version);
			return MessageConverter.CopyEWSTypeToDispatcherType(this.request);
		}

		// Token: 0x04000C06 RID: 3078
		private Microsoft.Exchange.SoapWebClient.EWS.FindMessageTrackingReportRequestType request;
	}
}
