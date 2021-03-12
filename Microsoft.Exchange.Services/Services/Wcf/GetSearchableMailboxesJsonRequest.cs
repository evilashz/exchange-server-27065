using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000C17 RID: 3095
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class GetSearchableMailboxesJsonRequest : BaseJsonRequest
	{
		// Token: 0x04002F6E RID: 12142
		[DataMember(IsRequired = true, Order = 0)]
		public GetSearchableMailboxesRequest Body;
	}
}
