using System;
using Microsoft.Exchange.Cluster.Shared;

namespace Microsoft.Exchange.Cluster.ActiveManagerServer
{
	// Token: 0x02000081 RID: 129
	internal class AmEvtStoreServiceStarted : AmEvtBase
	{
		// Token: 0x06000523 RID: 1315 RVA: 0x0001AC14 File Offset: 0x00018E14
		internal AmEvtStoreServiceStarted(AmServerName nodeName)
		{
			this.NodeName = nodeName;
		}

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x06000524 RID: 1316 RVA: 0x0001AC23 File Offset: 0x00018E23
		// (set) Token: 0x06000525 RID: 1317 RVA: 0x0001AC2B File Offset: 0x00018E2B
		internal AmServerName NodeName { get; set; }

		// Token: 0x06000526 RID: 1318 RVA: 0x0001AC34 File Offset: 0x00018E34
		public override string ToString()
		{
			return string.Format("{0}: Params: (NodeName={1})", base.GetType().Name, this.NodeName);
		}
	}
}
