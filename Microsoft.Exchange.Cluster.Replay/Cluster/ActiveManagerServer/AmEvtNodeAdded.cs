using System;
using Microsoft.Exchange.Cluster.Shared;

namespace Microsoft.Exchange.Cluster.ActiveManagerServer
{
	// Token: 0x02000079 RID: 121
	internal class AmEvtNodeAdded : AmEvtBase
	{
		// Token: 0x06000509 RID: 1289 RVA: 0x0001AA7D File Offset: 0x00018C7D
		internal AmEvtNodeAdded(AmServerName nodeName)
		{
			this.NodeName = nodeName;
		}

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x0600050A RID: 1290 RVA: 0x0001AA8C File Offset: 0x00018C8C
		// (set) Token: 0x0600050B RID: 1291 RVA: 0x0001AA94 File Offset: 0x00018C94
		internal AmServerName NodeName { get; set; }

		// Token: 0x0600050C RID: 1292 RVA: 0x0001AA9D File Offset: 0x00018C9D
		public override string ToString()
		{
			return string.Format("{0}: Params: (NodeName={1})", base.GetType().Name, this.NodeName);
		}
	}
}
