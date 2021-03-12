using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core.Wrappers
{
	// Token: 0x020002A2 RID: 674
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class RemoveBuddyRequestWrapper
	{
		// Token: 0x170005BC RID: 1468
		// (get) Token: 0x060017DB RID: 6107 RVA: 0x00053F72 File Offset: 0x00052172
		// (set) Token: 0x060017DC RID: 6108 RVA: 0x00053F7A File Offset: 0x0005217A
		[DataMember(Name = "instantMessageBuddy")]
		public InstantMessageBuddy InstantMessageBuddy { get; set; }

		// Token: 0x170005BD RID: 1469
		// (get) Token: 0x060017DD RID: 6109 RVA: 0x00053F83 File Offset: 0x00052183
		// (set) Token: 0x060017DE RID: 6110 RVA: 0x00053F8B File Offset: 0x0005218B
		[DataMember(Name = "contactId")]
		public ItemId ContactId { get; set; }
	}
}
