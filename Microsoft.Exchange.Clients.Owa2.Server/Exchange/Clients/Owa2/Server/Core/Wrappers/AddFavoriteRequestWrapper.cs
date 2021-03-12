using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core.Wrappers
{
	// Token: 0x0200025E RID: 606
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class AddFavoriteRequestWrapper
	{
		// Token: 0x1700055B RID: 1371
		// (get) Token: 0x060016D5 RID: 5845 RVA: 0x000536E1 File Offset: 0x000518E1
		// (set) Token: 0x060016D6 RID: 5846 RVA: 0x000536E9 File Offset: 0x000518E9
		[DataMember(Name = "instantMessageBuddy")]
		public InstantMessageBuddy InstantMessageBuddy { get; set; }
	}
}
