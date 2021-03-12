using System;

namespace Microsoft.Exchange.HttpProxy.Routing.Providers
{
	// Token: 0x02000012 RID: 18
	internal class User
	{
		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000040 RID: 64 RVA: 0x00002A4A File Offset: 0x00000C4A
		// (set) Token: 0x06000041 RID: 65 RVA: 0x00002A52 File Offset: 0x00000C52
		public Guid? DatabaseGuid { get; set; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000042 RID: 66 RVA: 0x00002A5B File Offset: 0x00000C5B
		// (set) Token: 0x06000043 RID: 67 RVA: 0x00002A63 File Offset: 0x00000C63
		public string DatabaseResourceForest { get; set; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000044 RID: 68 RVA: 0x00002A6C File Offset: 0x00000C6C
		// (set) Token: 0x06000045 RID: 69 RVA: 0x00002A74 File Offset: 0x00000C74
		public Guid? ArchiveDatabaseGuid { get; set; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000046 RID: 70 RVA: 0x00002A7D File Offset: 0x00000C7D
		// (set) Token: 0x06000047 RID: 71 RVA: 0x00002A85 File Offset: 0x00000C85
		public string ArchiveDatabaseResourceForest { get; set; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000048 RID: 72 RVA: 0x00002A8E File Offset: 0x00000C8E
		// (set) Token: 0x06000049 RID: 73 RVA: 0x00002A96 File Offset: 0x00000C96
		public Guid? ArchiveGuid { get; set; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600004A RID: 74 RVA: 0x00002A9F File Offset: 0x00000C9F
		// (set) Token: 0x0600004B RID: 75 RVA: 0x00002AA7 File Offset: 0x00000CA7
		public DateTime? LastModifiedTime { get; set; }
	}
}
