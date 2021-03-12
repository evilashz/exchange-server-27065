using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000BF8 RID: 3064
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class FindMessageTrackingReportJsonResponse : BaseJsonResponse
	{
		// Token: 0x04002F4F RID: 12111
		[DataMember(IsRequired = true, Order = 0)]
		public FindMessageTrackingReportResponseMessage Body;
	}
}
