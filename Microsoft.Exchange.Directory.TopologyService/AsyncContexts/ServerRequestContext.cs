using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Directory.TopologyService.AsyncContexts
{
	// Token: 0x02000023 RID: 35
	[DebuggerDisplay("{PartitionFqdn} - {Role}")]
	internal class ServerRequestContext
	{
		// Token: 0x0600012C RID: 300 RVA: 0x0000A77C File Offset: 0x0000897C
		public ServerRequestContext(string partitionFqdn, IList<string> currentlyUsedServers, ADServerRole role, int serverRequested, bool forestWideAffinityRequested = false)
		{
			this.PartitionFqdn = partitionFqdn;
			this.CurrentlyUsedServers = currentlyUsedServers;
			this.Role = role;
			this.ServersRequested = serverRequested;
			this.ForestWideAffinityRequested = forestWideAffinityRequested;
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x0600012D RID: 301 RVA: 0x0000A7A9 File Offset: 0x000089A9
		// (set) Token: 0x0600012E RID: 302 RVA: 0x0000A7B1 File Offset: 0x000089B1
		public string PartitionFqdn { get; private set; }

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x0600012F RID: 303 RVA: 0x0000A7BA File Offset: 0x000089BA
		// (set) Token: 0x06000130 RID: 304 RVA: 0x0000A7C2 File Offset: 0x000089C2
		public IList<string> CurrentlyUsedServers { get; private set; }

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x06000131 RID: 305 RVA: 0x0000A7CB File Offset: 0x000089CB
		// (set) Token: 0x06000132 RID: 306 RVA: 0x0000A7D3 File Offset: 0x000089D3
		public ADServerRole Role { get; private set; }

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x06000133 RID: 307 RVA: 0x0000A7DC File Offset: 0x000089DC
		// (set) Token: 0x06000134 RID: 308 RVA: 0x0000A7E4 File Offset: 0x000089E4
		public int ServersRequested { get; private set; }

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x06000135 RID: 309 RVA: 0x0000A7ED File Offset: 0x000089ED
		// (set) Token: 0x06000136 RID: 310 RVA: 0x0000A7F5 File Offset: 0x000089F5
		public bool ForestWideAffinityRequested { get; private set; }

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x06000137 RID: 311 RVA: 0x0000A7FE File Offset: 0x000089FE
		// (set) Token: 0x06000138 RID: 312 RVA: 0x0000A806 File Offset: 0x00008A06
		public IAsyncResult AsyncResult { get; set; }
	}
}
