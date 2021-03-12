using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x020009BC RID: 2492
	internal class TcpNetworkAddress : NetworkAddress
	{
		// Token: 0x060058C4 RID: 22724 RVA: 0x0017280C File Offset: 0x00170A0C
		public TcpNetworkAddress(NetworkProtocol protocol, string address) : base(protocol, address)
		{
		}
	}
}
