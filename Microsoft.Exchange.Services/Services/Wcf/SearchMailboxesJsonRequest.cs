using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000C19 RID: 3097
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class SearchMailboxesJsonRequest : BaseJsonRequest
	{
		// Token: 0x04002F70 RID: 12144
		[DataMember(IsRequired = true, Order = 0)]
		public SearchMailboxesRequest Body;
	}
}
