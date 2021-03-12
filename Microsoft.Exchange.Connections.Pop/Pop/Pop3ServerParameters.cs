using System;
using Microsoft.Exchange.Connections.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Connections.Pop
{
	// Token: 0x02000025 RID: 37
	[ClassAccessLevel(AccessLevel.Implementation)]
	public sealed class Pop3ServerParameters : ServerParameters
	{
		// Token: 0x06000125 RID: 293 RVA: 0x00006968 File Offset: 0x00004B68
		public Pop3ServerParameters(string server, int port) : base(server, port)
		{
		}

		// Token: 0x06000126 RID: 294 RVA: 0x00006972 File Offset: 0x00004B72
		public Pop3ServerParameters(string server) : base(server, 110)
		{
		}
	}
}
