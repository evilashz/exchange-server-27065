using System;
using Microsoft.Exchange.Cluster.ClusApi;
using Microsoft.Exchange.Cluster.Shared;

namespace Microsoft.Exchange.Cluster.ActiveManagerServer
{
	// Token: 0x02000078 RID: 120
	internal class AmEvtNodeStateChanged : AmEvtBase
	{
		// Token: 0x06000503 RID: 1283 RVA: 0x0001AA1D File Offset: 0x00018C1D
		internal AmEvtNodeStateChanged(AmServerName nodeName, AmNodeState nodeState)
		{
			this.NodeName = nodeName;
			this.State = nodeState;
		}

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x06000504 RID: 1284 RVA: 0x0001AA33 File Offset: 0x00018C33
		// (set) Token: 0x06000505 RID: 1285 RVA: 0x0001AA3B File Offset: 0x00018C3B
		internal AmServerName NodeName { get; set; }

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x06000506 RID: 1286 RVA: 0x0001AA44 File Offset: 0x00018C44
		// (set) Token: 0x06000507 RID: 1287 RVA: 0x0001AA4C File Offset: 0x00018C4C
		internal AmNodeState State { get; set; }

		// Token: 0x06000508 RID: 1288 RVA: 0x0001AA55 File Offset: 0x00018C55
		public override string ToString()
		{
			return string.Format("{0}: Params: (NodeName={1}, NodeState={2})", base.GetType().Name, this.NodeName, this.State);
		}
	}
}
