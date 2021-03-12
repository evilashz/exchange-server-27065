using System;
using Microsoft.Exchange.Cluster.Shared;

namespace Microsoft.Exchange.Cluster.ActiveManagerServer
{
	// Token: 0x0200008C RID: 140
	internal class AmEvtSystemFailoverOnReplayDown : AmEvtBase
	{
		// Token: 0x0600054A RID: 1354 RVA: 0x0001AF29 File Offset: 0x00019129
		internal AmEvtSystemFailoverOnReplayDown(AmServerName serverName)
		{
			this.ServerName = serverName;
		}

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x0600054B RID: 1355 RVA: 0x0001AF38 File Offset: 0x00019138
		// (set) Token: 0x0600054C RID: 1356 RVA: 0x0001AF40 File Offset: 0x00019140
		internal AmServerName ServerName { get; private set; }
	}
}
