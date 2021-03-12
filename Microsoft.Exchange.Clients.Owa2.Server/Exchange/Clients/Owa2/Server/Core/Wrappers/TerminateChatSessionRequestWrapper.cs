using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core.Wrappers
{
	// Token: 0x020002C9 RID: 713
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class TerminateChatSessionRequestWrapper
	{
		// Token: 0x170005EE RID: 1518
		// (get) Token: 0x06001866 RID: 6246 RVA: 0x000543FC File Offset: 0x000525FC
		// (set) Token: 0x06001867 RID: 6247 RVA: 0x00054404 File Offset: 0x00052604
		[DataMember(Name = "chatSessionId")]
		public int ChatSessionId { get; set; }
	}
}
