using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core.Wrappers
{
	// Token: 0x0200025F RID: 607
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class AddImBuddyRequestWrapper
	{
		// Token: 0x1700055C RID: 1372
		// (get) Token: 0x060016D8 RID: 5848 RVA: 0x000536FA File Offset: 0x000518FA
		// (set) Token: 0x060016D9 RID: 5849 RVA: 0x00053702 File Offset: 0x00051902
		[DataMember(Name = "instantMessageBuddy")]
		public InstantMessageBuddy InstantMessageBuddy { get; set; }

		// Token: 0x1700055D RID: 1373
		// (get) Token: 0x060016DA RID: 5850 RVA: 0x0005370B File Offset: 0x0005190B
		// (set) Token: 0x060016DB RID: 5851 RVA: 0x00053713 File Offset: 0x00051913
		[DataMember(Name = "instantMessageGroup")]
		public InstantMessageGroup InstantMessageGroup { get; set; }
	}
}
