using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000C1A RID: 3098
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class SearchMailboxesJsonResponse : BaseJsonResponse
	{
		// Token: 0x04002F71 RID: 12145
		[DataMember(IsRequired = true, Order = 0)]
		public SearchMailboxesResponse Body;
	}
}
