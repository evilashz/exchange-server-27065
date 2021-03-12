using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core.Wrappers
{
	// Token: 0x020002BF RID: 703
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class SetPresenceRequestWrapper
	{
		// Token: 0x170005E0 RID: 1504
		// (get) Token: 0x06001840 RID: 6208 RVA: 0x000542BE File Offset: 0x000524BE
		// (set) Token: 0x06001841 RID: 6209 RVA: 0x000542C6 File Offset: 0x000524C6
		[DataMember(Name = "presenceSetting")]
		public InstantMessagePresence PresenceSetting { get; set; }
	}
}
