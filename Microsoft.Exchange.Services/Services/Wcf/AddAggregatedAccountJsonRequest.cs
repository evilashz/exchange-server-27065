using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000BFF RID: 3071
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class AddAggregatedAccountJsonRequest : BaseJsonRequest
	{
		// Token: 0x04002F56 RID: 12118
		[DataMember(IsRequired = true, Order = 0)]
		public AddAggregatedAccountRequest Body;
	}
}
