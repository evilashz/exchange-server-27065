using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.InfoWorker.Availability;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000C0D RID: 3085
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class GetUserAvailabilityJsonRequest : BaseJsonRequest
	{
		// Token: 0x04002F64 RID: 12132
		[DataMember(IsRequired = true, Order = 0)]
		public GetUserAvailabilityRequest Body;
	}
}
