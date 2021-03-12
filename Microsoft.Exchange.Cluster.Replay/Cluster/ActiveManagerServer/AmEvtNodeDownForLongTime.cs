using System;
using Microsoft.Exchange.Cluster.Shared;

namespace Microsoft.Exchange.Cluster.ActiveManagerServer
{
	// Token: 0x0200007B RID: 123
	internal class AmEvtNodeDownForLongTime : AmEvtBase
	{
		// Token: 0x06000511 RID: 1297 RVA: 0x0001AAF7 File Offset: 0x00018CF7
		internal AmEvtNodeDownForLongTime(AmServerName nodeName)
		{
			this.NodeName = nodeName;
		}

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x06000512 RID: 1298 RVA: 0x0001AB06 File Offset: 0x00018D06
		// (set) Token: 0x06000513 RID: 1299 RVA: 0x0001AB0E File Offset: 0x00018D0E
		internal AmServerName NodeName { get; set; }

		// Token: 0x06000514 RID: 1300 RVA: 0x0001AB17 File Offset: 0x00018D17
		public override string ToString()
		{
			return string.Format("{0}: Params: (NodeName={1})", base.GetType().Name, this.NodeName);
		}
	}
}
