using System;
using System.ServiceModel;

namespace Microsoft.Exchange.Net.XropService
{
	// Token: 0x02000B9E RID: 2974
	[CLSCompliant(false)]
	[MessageContract]
	public sealed class DisconnectRequestMessage
	{
		// Token: 0x17000FB2 RID: 4018
		// (get) Token: 0x06003FD8 RID: 16344 RVA: 0x000A8587 File Offset: 0x000A6787
		// (set) Token: 0x06003FD9 RID: 16345 RVA: 0x000A858F File Offset: 0x000A678F
		[MessageBodyMember]
		public DisconnectRequest Request { get; set; }
	}
}
