using System;

namespace Microsoft.Exchange.Cluster.ActiveManagerServer
{
	// Token: 0x0200007C RID: 124
	internal class AmEvtClusterStateChanged : AmEvtBase
	{
		// Token: 0x06000515 RID: 1301 RVA: 0x0001AB34 File Offset: 0x00018D34
		internal AmEvtClusterStateChanged()
		{
		}

		// Token: 0x06000516 RID: 1302 RVA: 0x0001AB3C File Offset: 0x00018D3C
		public override string ToString()
		{
			return string.Format("{0}: Params: (<none>)", base.GetType().Name);
		}
	}
}
