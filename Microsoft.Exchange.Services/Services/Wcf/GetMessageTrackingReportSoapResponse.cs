using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000D1F RID: 3359
	[MessageContract(IsWrapped = false)]
	public class GetMessageTrackingReportSoapResponse : BaseSoapResponse
	{
		// Token: 0x040030B1 RID: 12465
		[MessageBodyMember(Name = "GetMessageTrackingReportResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public GetMessageTrackingReportResponseMessage Body;
	}
}
