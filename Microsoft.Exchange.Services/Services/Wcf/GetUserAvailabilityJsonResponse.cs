using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.InfoWorker.Availability;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000C0E RID: 3086
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class GetUserAvailabilityJsonResponse : BaseJsonResponse
	{
		// Token: 0x04002F65 RID: 12133
		[DataMember(IsRequired = true, Order = 0)]
		public GetUserAvailabilityResponse Body;
	}
}
