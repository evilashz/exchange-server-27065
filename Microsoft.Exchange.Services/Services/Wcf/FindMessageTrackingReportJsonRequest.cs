using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000BF7 RID: 3063
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class FindMessageTrackingReportJsonRequest : BaseJsonRequest
	{
		// Token: 0x04002F4E RID: 12110
		[DataMember(IsRequired = true, Order = 0)]
		public FindMessageTrackingReportRequest Body;
	}
}
