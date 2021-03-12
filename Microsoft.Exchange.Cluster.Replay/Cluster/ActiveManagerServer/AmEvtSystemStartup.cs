using System;

namespace Microsoft.Exchange.Cluster.ActiveManagerServer
{
	// Token: 0x0200007E RID: 126
	internal class AmEvtSystemStartup : AmEvtBase
	{
		// Token: 0x06000519 RID: 1305 RVA: 0x0001AB72 File Offset: 0x00018D72
		internal AmEvtSystemStartup()
		{
		}

		// Token: 0x0600051A RID: 1306 RVA: 0x0001AB7A File Offset: 0x00018D7A
		public override string ToString()
		{
			return string.Format("{0}: Params: (<None>)", base.GetType().Name);
		}
	}
}
