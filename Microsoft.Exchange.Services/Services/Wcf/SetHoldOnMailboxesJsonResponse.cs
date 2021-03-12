using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000C1E RID: 3102
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class SetHoldOnMailboxesJsonResponse : BaseJsonResponse
	{
		// Token: 0x04002F75 RID: 12149
		[DataMember(IsRequired = true, Order = 0)]
		public SetHoldOnMailboxesResponse Body;
	}
}
