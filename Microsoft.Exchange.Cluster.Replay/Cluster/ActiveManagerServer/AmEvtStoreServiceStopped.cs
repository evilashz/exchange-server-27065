using System;
using Microsoft.Exchange.Cluster.Shared;

namespace Microsoft.Exchange.Cluster.ActiveManagerServer
{
	// Token: 0x02000082 RID: 130
	internal class AmEvtStoreServiceStopped : AmEvtBase
	{
		// Token: 0x06000527 RID: 1319 RVA: 0x0001AC51 File Offset: 0x00018E51
		internal AmEvtStoreServiceStopped(AmServerName nodeName)
		{
			this.NodeName = nodeName;
			this.IsGracefulStop = true;
		}

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x06000528 RID: 1320 RVA: 0x0001AC67 File Offset: 0x00018E67
		// (set) Token: 0x06000529 RID: 1321 RVA: 0x0001AC6F File Offset: 0x00018E6F
		internal AmServerName NodeName { get; set; }

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x0600052A RID: 1322 RVA: 0x0001AC78 File Offset: 0x00018E78
		// (set) Token: 0x0600052B RID: 1323 RVA: 0x0001AC80 File Offset: 0x00018E80
		internal bool IsGracefulStop { get; set; }

		// Token: 0x0600052C RID: 1324 RVA: 0x0001AC89 File Offset: 0x00018E89
		public override string ToString()
		{
			return string.Format("{0}: Params: (NodeName={1}, IsGracefulStop={2})", base.GetType().Name, this.NodeName, this.IsGracefulStop);
		}
	}
}
