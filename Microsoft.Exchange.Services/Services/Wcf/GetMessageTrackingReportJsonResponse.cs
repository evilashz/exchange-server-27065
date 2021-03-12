using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000BFA RID: 3066
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class GetMessageTrackingReportJsonResponse : BaseJsonResponse
	{
		// Token: 0x04002F51 RID: 12113
		[DataMember(IsRequired = true, Order = 0)]
		public GetMessageTrackingReportResponseMessage Body;
	}
}
