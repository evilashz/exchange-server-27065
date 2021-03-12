using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Connections.Common
{
	// Token: 0x0200002B RID: 43
	[ClassAccessLevel(AccessLevel.Implementation)]
	public class ServerParameters
	{
		// Token: 0x0600009D RID: 157 RVA: 0x00002AD9 File Offset: 0x00000CD9
		public ServerParameters(string server, int port)
		{
			this.Server = server;
			this.Port = port;
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x0600009E RID: 158 RVA: 0x00002AEF File Offset: 0x00000CEF
		// (set) Token: 0x0600009F RID: 159 RVA: 0x00002AF7 File Offset: 0x00000CF7
		public string Server { get; private set; }

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060000A0 RID: 160 RVA: 0x00002B00 File Offset: 0x00000D00
		// (set) Token: 0x060000A1 RID: 161 RVA: 0x00002B08 File Offset: 0x00000D08
		public int Port { get; private set; }
	}
}
