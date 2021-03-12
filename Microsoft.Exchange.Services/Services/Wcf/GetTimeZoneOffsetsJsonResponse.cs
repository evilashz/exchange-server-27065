using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000C59 RID: 3161
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class GetTimeZoneOffsetsJsonResponse : BaseJsonResponse
	{
		// Token: 0x04002FAD RID: 12205
		[DataMember(IsRequired = true, Order = 0)]
		public GetTimeZoneOffsetsResponseMessage Body;
	}
}
