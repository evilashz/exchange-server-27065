using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000C03 RID: 3075
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class SyncConversationJsonRequest : BaseJsonRequest
	{
		// Token: 0x04002F5A RID: 12122
		[DataMember(IsRequired = true, Order = 0)]
		public SyncConversationRequest Body;
	}
}
