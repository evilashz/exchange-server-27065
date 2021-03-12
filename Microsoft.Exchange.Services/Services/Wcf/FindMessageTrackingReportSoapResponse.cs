using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000D1D RID: 3357
	[MessageContract(IsWrapped = false)]
	public class FindMessageTrackingReportSoapResponse : BaseSoapResponse
	{
		// Token: 0x040030AF RID: 12463
		[MessageBodyMember(Name = "FindMessageTrackingReportResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public FindMessageTrackingReportResponseMessage Body;
	}
}
