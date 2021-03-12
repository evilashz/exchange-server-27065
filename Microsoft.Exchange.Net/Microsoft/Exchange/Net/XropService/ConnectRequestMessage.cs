using System;
using System.ServiceModel;

namespace Microsoft.Exchange.Net.XropService
{
	// Token: 0x02000B96 RID: 2966
	[CLSCompliant(false)]
	[MessageContract]
	public sealed class ConnectRequestMessage
	{
		// Token: 0x17000F86 RID: 3974
		// (get) Token: 0x06003F78 RID: 16248 RVA: 0x000A825B File Offset: 0x000A645B
		// (set) Token: 0x06003F79 RID: 16249 RVA: 0x000A8263 File Offset: 0x000A6463
		[MessageBodyMember]
		public ConnectRequest Request { get; set; }
	}
}
