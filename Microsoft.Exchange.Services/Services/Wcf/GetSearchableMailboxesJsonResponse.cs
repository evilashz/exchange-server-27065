using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000C18 RID: 3096
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class GetSearchableMailboxesJsonResponse : BaseJsonResponse
	{
		// Token: 0x04002F6F RID: 12143
		[DataMember(IsRequired = true, Order = 0)]
		public GetSearchableMailboxesResponse Body;
	}
}
