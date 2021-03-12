using System;
using System.ServiceModel;

namespace Microsoft.Exchange.Net.XropService
{
	// Token: 0x02000B98 RID: 2968
	[CLSCompliant(false)]
	[MessageContract]
	public sealed class ConnectResponseMessage
	{
		// Token: 0x17000F95 RID: 3989
		// (get) Token: 0x06003F98 RID: 16280 RVA: 0x000A836A File Offset: 0x000A656A
		// (set) Token: 0x06003F99 RID: 16281 RVA: 0x000A8372 File Offset: 0x000A6572
		[MessageBodyMember]
		public ConnectResponse Response { get; set; }
	}
}
