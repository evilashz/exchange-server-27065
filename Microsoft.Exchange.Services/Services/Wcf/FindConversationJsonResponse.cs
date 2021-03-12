using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000BFC RID: 3068
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class FindConversationJsonResponse : BaseJsonResponse
	{
		// Token: 0x04002F53 RID: 12115
		[DataMember(IsRequired = true, Order = 0)]
		public FindConversationResponseMessage Body;
	}
}
