using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core.Wrappers
{
	// Token: 0x02000258 RID: 600
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class AcceptBuddyRequestWrapper
	{
		// Token: 0x17000553 RID: 1363
		// (get) Token: 0x060016BF RID: 5823 RVA: 0x00053629 File Offset: 0x00051829
		// (set) Token: 0x060016C0 RID: 5824 RVA: 0x00053631 File Offset: 0x00051831
		[DataMember(Name = "instantMessageBuddy")]
		public InstantMessageBuddy InstantMessageBuddy { get; set; }

		// Token: 0x17000554 RID: 1364
		// (get) Token: 0x060016C1 RID: 5825 RVA: 0x0005363A File Offset: 0x0005183A
		// (set) Token: 0x060016C2 RID: 5826 RVA: 0x00053642 File Offset: 0x00051842
		[DataMember(Name = "instantMessageGroup")]
		public InstantMessageGroup InstantMessageGroup { get; set; }
	}
}
