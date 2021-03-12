using System;
using System.ServiceModel;

namespace Microsoft.Exchange.Net.XropService
{
	// Token: 0x02000BA0 RID: 2976
	[CLSCompliant(false)]
	[MessageContract]
	public sealed class DisconnectResponseMessage
	{
		// Token: 0x17000FB4 RID: 4020
		// (get) Token: 0x06003FDE RID: 16350 RVA: 0x000A85B9 File Offset: 0x000A67B9
		// (set) Token: 0x06003FDF RID: 16351 RVA: 0x000A85C1 File Offset: 0x000A67C1
		[MessageBodyMember]
		public DisconnectResponse Response { get; set; }
	}
}
