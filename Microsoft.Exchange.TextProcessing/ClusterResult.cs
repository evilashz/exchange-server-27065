using System;

namespace Microsoft.Exchange.TextProcessing
{
	// Token: 0x0200001A RID: 26
	internal class ClusterResult
	{
		// Token: 0x0600010A RID: 266 RVA: 0x0000B4FF File Offset: 0x000096FF
		public ClusterResult()
		{
			this.ActionMode = ActionEnum.BelowThreshold;
			this.Clusteroid = null;
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x0600010B RID: 267 RVA: 0x0000B515 File Offset: 0x00009715
		// (set) Token: 0x0600010C RID: 268 RVA: 0x0000B51D File Offset: 0x0000971D
		public ActionEnum ActionMode { get; set; }

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x0600010D RID: 269 RVA: 0x0000B526 File Offset: 0x00009726
		// (set) Token: 0x0600010E RID: 270 RVA: 0x0000B52E File Offset: 0x0000972E
		public LshFingerprint Clusteroid { get; set; }

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x0600010F RID: 271 RVA: 0x0000B537 File Offset: 0x00009737
		// (set) Token: 0x06000110 RID: 272 RVA: 0x0000B53F File Offset: 0x0000973F
		public ClusteringStatusEnum Status { get; set; }

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x06000111 RID: 273 RVA: 0x0000B548 File Offset: 0x00009748
		// (set) Token: 0x06000112 RID: 274 RVA: 0x0000B550 File Offset: 0x00009750
		public DateTime StartTimeStamp { get; set; }

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x06000113 RID: 275 RVA: 0x0000B559 File Offset: 0x00009759
		// (set) Token: 0x06000114 RID: 276 RVA: 0x0000B561 File Offset: 0x00009761
		public int[] PropertyValues { get; set; }
	}
}
