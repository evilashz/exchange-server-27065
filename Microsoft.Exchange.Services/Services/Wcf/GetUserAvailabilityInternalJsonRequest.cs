using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.InfoWorker.Availability;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000C4F RID: 3151
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class GetUserAvailabilityInternalJsonRequest : BaseJsonRequest
	{
		// Token: 0x04002FA6 RID: 12198
		[DataMember(IsRequired = true, Order = 0)]
		public GetUserAvailabilityRequest Body;
	}
}
