using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000C00 RID: 3072
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class AddAggregatedAccountJsonResponse : BaseJsonResponse
	{
		// Token: 0x04002F57 RID: 12119
		[DataMember(IsRequired = true, Order = 0)]
		public AddAggregatedAccountResponseMessage Body;
	}
}
