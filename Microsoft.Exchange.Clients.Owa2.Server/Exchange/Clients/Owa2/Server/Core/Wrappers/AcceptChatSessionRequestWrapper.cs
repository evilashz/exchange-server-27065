using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core.Wrappers
{
	// Token: 0x02000259 RID: 601
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class AcceptChatSessionRequestWrapper
	{
		// Token: 0x17000555 RID: 1365
		// (get) Token: 0x060016C4 RID: 5828 RVA: 0x00053653 File Offset: 0x00051853
		// (set) Token: 0x060016C5 RID: 5829 RVA: 0x0005365B File Offset: 0x0005185B
		[DataMember(Name = "chatSessionId")]
		public int ChatSessionId { get; set; }
	}
}
