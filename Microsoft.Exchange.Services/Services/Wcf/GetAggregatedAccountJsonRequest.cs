using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000BFD RID: 3069
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class GetAggregatedAccountJsonRequest : BaseJsonRequest
	{
		// Token: 0x04002F54 RID: 12116
		[DataMember(IsRequired = true, Order = 0)]
		public GetAggregatedAccountRequest Body;
	}
}
