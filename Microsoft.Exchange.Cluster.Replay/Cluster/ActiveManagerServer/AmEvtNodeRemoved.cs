using System;
using Microsoft.Exchange.Cluster.Shared;

namespace Microsoft.Exchange.Cluster.ActiveManagerServer
{
	// Token: 0x0200007A RID: 122
	internal class AmEvtNodeRemoved : AmEvtBase
	{
		// Token: 0x0600050D RID: 1293 RVA: 0x0001AABA File Offset: 0x00018CBA
		internal AmEvtNodeRemoved(AmServerName nodeName)
		{
			this.NodeName = nodeName;
		}

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x0600050E RID: 1294 RVA: 0x0001AAC9 File Offset: 0x00018CC9
		// (set) Token: 0x0600050F RID: 1295 RVA: 0x0001AAD1 File Offset: 0x00018CD1
		internal AmServerName NodeName { get; set; }

		// Token: 0x06000510 RID: 1296 RVA: 0x0001AADA File Offset: 0x00018CDA
		public override string ToString()
		{
			return string.Format("{0}: Params: (NodeName={1})", base.GetType().Name, this.NodeName);
		}
	}
}
