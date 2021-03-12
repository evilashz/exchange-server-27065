using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000BFE RID: 3070
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class GetAggregatedAccountJsonResponse : BaseJsonResponse
	{
		// Token: 0x04002F55 RID: 12117
		[DataMember(IsRequired = true, Order = 0)]
		public GetAggregatedAccountResponseMessage Body;
	}
}
