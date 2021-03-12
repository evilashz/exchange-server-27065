using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000D1E RID: 3358
	[MessageContract(IsWrapped = false)]
	public class GetMessageTrackingReportSoapRequest : BaseSoapRequest
	{
		// Token: 0x040030B0 RID: 12464
		[MessageBodyMember(Name = "GetMessageTrackingReport", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public GetMessageTrackingReportRequest Body;
	}
}
