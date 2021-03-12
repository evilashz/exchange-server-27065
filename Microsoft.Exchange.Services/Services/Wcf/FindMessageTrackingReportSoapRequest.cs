using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000D1C RID: 3356
	[MessageContract(IsWrapped = false)]
	public class FindMessageTrackingReportSoapRequest : BaseSoapRequest
	{
		// Token: 0x040030AE RID: 12462
		[MessageBodyMember(Name = "FindMessageTrackingReport", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public FindMessageTrackingReportRequest Body;
	}
}
