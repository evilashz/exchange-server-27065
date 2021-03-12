using System;

namespace Microsoft.Exchange.HttpProxy
{
	// Token: 0x0200002E RID: 46
	internal class DatabaseWithForest
	{
		// Token: 0x06000152 RID: 338 RVA: 0x00007C5B File Offset: 0x00005E5B
		public DatabaseWithForest(Guid database, string resourceForest, Guid initiatingRequestId)
		{
			this.Database = database;
			this.ResourceForest = resourceForest;
			this.InitiatingRequestId = initiatingRequestId;
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x06000153 RID: 339 RVA: 0x00007C78 File Offset: 0x00005E78
		// (set) Token: 0x06000154 RID: 340 RVA: 0x00007C80 File Offset: 0x00005E80
		public Guid Database { get; set; }

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x06000155 RID: 341 RVA: 0x00007C89 File Offset: 0x00005E89
		// (set) Token: 0x06000156 RID: 342 RVA: 0x00007C91 File Offset: 0x00005E91
		public string ResourceForest { get; set; }

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x06000157 RID: 343 RVA: 0x00007C9A File Offset: 0x00005E9A
		// (set) Token: 0x06000158 RID: 344 RVA: 0x00007CA2 File Offset: 0x00005EA2
		public Guid InitiatingRequestId { get; set; }
	}
}
