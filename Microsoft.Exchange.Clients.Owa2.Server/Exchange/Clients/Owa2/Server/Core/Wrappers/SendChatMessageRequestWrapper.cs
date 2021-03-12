using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core.Wrappers
{
	// Token: 0x020002AD RID: 685
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class SendChatMessageRequestWrapper
	{
		// Token: 0x170005CB RID: 1483
		// (get) Token: 0x06001804 RID: 6148 RVA: 0x000540C9 File Offset: 0x000522C9
		// (set) Token: 0x06001805 RID: 6149 RVA: 0x000540D1 File Offset: 0x000522D1
		[DataMember(Name = "message")]
		public ChatMessage Message { get; set; }
	}
}
