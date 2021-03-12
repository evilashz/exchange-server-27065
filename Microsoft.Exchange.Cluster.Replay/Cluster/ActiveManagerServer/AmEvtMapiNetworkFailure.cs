using System;
using Microsoft.Exchange.Cluster.Shared;

namespace Microsoft.Exchange.Cluster.ActiveManagerServer
{
	// Token: 0x0200007F RID: 127
	internal class AmEvtMapiNetworkFailure : AmEvtBase
	{
		// Token: 0x0600051B RID: 1307 RVA: 0x0001AB91 File Offset: 0x00018D91
		internal AmEvtMapiNetworkFailure(AmServerName nodeName)
		{
			this.NodeName = nodeName;
			this.DetectionTimeUtc = DateTime.UtcNow;
		}

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x0600051C RID: 1308 RVA: 0x0001ABAB File Offset: 0x00018DAB
		// (set) Token: 0x0600051D RID: 1309 RVA: 0x0001ABB3 File Offset: 0x00018DB3
		internal AmServerName NodeName { get; set; }

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x0600051E RID: 1310 RVA: 0x0001ABBC File Offset: 0x00018DBC
		// (set) Token: 0x0600051F RID: 1311 RVA: 0x0001ABC4 File Offset: 0x00018DC4
		internal DateTime DetectionTimeUtc { get; set; }

		// Token: 0x06000520 RID: 1312 RVA: 0x0001ABCD File Offset: 0x00018DCD
		public override string ToString()
		{
			return string.Format("{0}: Params: (NodeName={1},DetectedAt={2}UTC)", base.GetType().Name, this.NodeName, this.DetectionTimeUtc);
		}
	}
}
