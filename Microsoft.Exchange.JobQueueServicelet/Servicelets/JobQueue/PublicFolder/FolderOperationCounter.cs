using System;
using System.Globalization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Servicelets.JobQueue.PublicFolder
{
	// Token: 0x02000003 RID: 3
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class FolderOperationCounter
	{
		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600001A RID: 26 RVA: 0x00002334 File Offset: 0x00000534
		// (set) Token: 0x0600001B RID: 27 RVA: 0x0000233C File Offset: 0x0000053C
		public int Added { get; set; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600001C RID: 28 RVA: 0x00002345 File Offset: 0x00000545
		// (set) Token: 0x0600001D RID: 29 RVA: 0x0000234D File Offset: 0x0000054D
		public int Updated { get; set; }

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600001E RID: 30 RVA: 0x00002356 File Offset: 0x00000556
		// (set) Token: 0x0600001F RID: 31 RVA: 0x0000235E File Offset: 0x0000055E
		public int Deleted { get; set; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000020 RID: 32 RVA: 0x00002367 File Offset: 0x00000567
		// (set) Token: 0x06000021 RID: 33 RVA: 0x0000236F File Offset: 0x0000056F
		public int OrphanDetected { get; set; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000022 RID: 34 RVA: 0x00002378 File Offset: 0x00000578
		// (set) Token: 0x06000023 RID: 35 RVA: 0x00002380 File Offset: 0x00000580
		public int OrphanFixed { get; set; }

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000024 RID: 36 RVA: 0x00002389 File Offset: 0x00000589
		// (set) Token: 0x06000025 RID: 37 RVA: 0x00002391 File Offset: 0x00000591
		public int ParentChainMissing { get; set; }

		// Token: 0x06000026 RID: 38 RVA: 0x0000239C File Offset: 0x0000059C
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "Added={0},Updated={1},Deleted={2},OrphanDetected={3},OrphanFixed={4},ParentMissing={5}", new object[]
			{
				this.Added,
				this.Updated,
				this.Deleted,
				this.OrphanDetected,
				this.OrphanFixed,
				this.ParentChainMissing
			});
		}
	}
}
