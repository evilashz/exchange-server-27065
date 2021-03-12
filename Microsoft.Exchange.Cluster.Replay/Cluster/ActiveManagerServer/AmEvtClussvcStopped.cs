using System;

namespace Microsoft.Exchange.Cluster.ActiveManagerServer
{
	// Token: 0x0200007D RID: 125
	internal class AmEvtClussvcStopped : AmEvtBase
	{
		// Token: 0x06000517 RID: 1303 RVA: 0x0001AB53 File Offset: 0x00018D53
		internal AmEvtClussvcStopped()
		{
		}

		// Token: 0x06000518 RID: 1304 RVA: 0x0001AB5B File Offset: 0x00018D5B
		public override string ToString()
		{
			return string.Format("{0}: Params: (<none>)", base.GetType().Name);
		}
	}
}
