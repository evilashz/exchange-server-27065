using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core.Wrappers
{
	// Token: 0x02000271 RID: 625
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class DeclineBuddyRequestWrapper
	{
		// Token: 0x1700057B RID: 1403
		// (get) Token: 0x06001728 RID: 5928 RVA: 0x00053999 File Offset: 0x00051B99
		// (set) Token: 0x06001729 RID: 5929 RVA: 0x000539A1 File Offset: 0x00051BA1
		[DataMember(Name = "instantMessageBuddy")]
		public InstantMessageBuddy InstantMessageBuddy { get; set; }
	}
}
