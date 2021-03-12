using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000C05 RID: 3077
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class ApplyConversationActionJsonRequest : BaseJsonRequest
	{
		// Token: 0x04002F5C RID: 12124
		[DataMember(IsRequired = true, Order = 0)]
		public ApplyConversationActionRequest Body;
	}
}
