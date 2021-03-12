using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000BFB RID: 3067
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class FindConversationJsonRequest : BaseJsonRequest
	{
		// Token: 0x04002F52 RID: 12114
		[DataMember(IsRequired = true, Order = 0)]
		public FindConversationRequest Body;
	}
}
