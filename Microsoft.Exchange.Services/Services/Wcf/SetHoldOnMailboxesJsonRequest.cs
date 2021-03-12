using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000C1D RID: 3101
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class SetHoldOnMailboxesJsonRequest : BaseJsonRequest
	{
		// Token: 0x04002F74 RID: 12148
		[DataMember(IsRequired = true, Order = 0)]
		public SetHoldOnMailboxesRequest Body;
	}
}
