using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000C06 RID: 3078
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class ApplyConversationActionJsonResponse : BaseJsonResponse
	{
		// Token: 0x04002F5D RID: 12125
		[DataMember(IsRequired = true, Order = 0)]
		public ApplyConversationActionResponse Body;
	}
}
