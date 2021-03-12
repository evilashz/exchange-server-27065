using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Wcf.Types;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core.Wrappers
{
	// Token: 0x0200025C RID: 604
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class AddBuddyRequestWrapper
	{
		// Token: 0x17000559 RID: 1369
		// (get) Token: 0x060016CF RID: 5839 RVA: 0x000536AF File Offset: 0x000518AF
		// (set) Token: 0x060016D0 RID: 5840 RVA: 0x000536B7 File Offset: 0x000518B7
		[DataMember(Name = "buddy")]
		public Buddy Buddy { get; set; }
	}
}
