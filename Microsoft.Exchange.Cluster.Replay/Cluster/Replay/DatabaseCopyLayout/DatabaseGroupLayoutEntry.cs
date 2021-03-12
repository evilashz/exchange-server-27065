using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Cluster.Replay.DatabaseCopyLayout
{
	// Token: 0x02000173 RID: 371
	public struct DatabaseGroupLayoutEntry
	{
		// Token: 0x170003CA RID: 970
		// (get) Token: 0x06000EF1 RID: 3825 RVA: 0x0003FF47 File Offset: 0x0003E147
		// (set) Token: 0x06000EF2 RID: 3826 RVA: 0x0003FF4F File Offset: 0x0003E14F
		public string DatabaseGroupId { get; private set; }

		// Token: 0x170003CB RID: 971
		// (get) Token: 0x06000EF3 RID: 3827 RVA: 0x0003FF58 File Offset: 0x0003E158
		// (set) Token: 0x06000EF4 RID: 3828 RVA: 0x0003FF60 File Offset: 0x0003E160
		public List<string> DatabaseNameList { get; private set; }

		// Token: 0x06000EF5 RID: 3829 RVA: 0x0003FF69 File Offset: 0x0003E169
		public DatabaseGroupLayoutEntry(string databaseGroupId, List<string> databaseNameList, bool additionalCopyOnSpare = false)
		{
			this = default(DatabaseGroupLayoutEntry);
			this.DatabaseNameList = databaseNameList;
			this.DatabaseGroupId = databaseGroupId;
			this.AdditionalCopyOnSpare = additionalCopyOnSpare;
		}

		// Token: 0x04000623 RID: 1571
		public bool AdditionalCopyOnSpare;
	}
}
