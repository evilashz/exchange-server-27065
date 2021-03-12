using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000C04 RID: 3076
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class SyncConversationJsonResponse : BaseJsonResponse
	{
		// Token: 0x04002F5B RID: 12123
		[DataMember(IsRequired = true, Order = 0)]
		public SyncConversationResponseMessage Body;
	}
}
