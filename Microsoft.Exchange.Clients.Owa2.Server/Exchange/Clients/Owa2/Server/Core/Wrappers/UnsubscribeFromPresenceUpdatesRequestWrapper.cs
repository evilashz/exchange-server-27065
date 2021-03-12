using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core.Wrappers
{
	// Token: 0x020002CB RID: 715
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class UnsubscribeFromPresenceUpdatesRequestWrapper
	{
		// Token: 0x170005F1 RID: 1521
		// (get) Token: 0x0600186E RID: 6254 RVA: 0x0005443F File Offset: 0x0005263F
		// (set) Token: 0x0600186F RID: 6255 RVA: 0x00054447 File Offset: 0x00052647
		[DataMember(Name = "sipUri")]
		public string SipUri { get; set; }
	}
}
