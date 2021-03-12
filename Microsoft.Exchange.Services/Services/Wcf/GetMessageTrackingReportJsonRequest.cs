using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000BF9 RID: 3065
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class GetMessageTrackingReportJsonRequest : BaseJsonRequest
	{
		// Token: 0x04002F50 RID: 12112
		[DataMember(IsRequired = true, Order = 0)]
		public GetMessageTrackingReportRequest Body;
	}
}
