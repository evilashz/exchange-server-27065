using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Wcf.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000C50 RID: 3152
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class GetUserAvailabilityInternalJsonResponse : BaseJsonResponse
	{
		// Token: 0x04002FA7 RID: 12199
		[DataMember(IsRequired = true, Order = 0)]
		public GetUserAvailabilityInternalResponse Body;
	}
}
