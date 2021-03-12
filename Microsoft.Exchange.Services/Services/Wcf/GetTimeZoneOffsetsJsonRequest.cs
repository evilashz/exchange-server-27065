using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000C58 RID: 3160
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class GetTimeZoneOffsetsJsonRequest : BaseJsonRequest
	{
		// Token: 0x04002FAC RID: 12204
		[DataMember(IsRequired = true, Order = 0)]
		public GetTimeZoneOffsetsRequest Body;
	}
}
