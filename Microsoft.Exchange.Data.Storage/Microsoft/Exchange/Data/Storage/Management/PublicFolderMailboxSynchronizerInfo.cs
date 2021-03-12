using System;

namespace Microsoft.Exchange.Data.Storage.Management
{
	// Token: 0x02000A97 RID: 2711
	[Serializable]
	public class PublicFolderMailboxSynchronizerInfo : PublicFolderMailboxMonitoringInfo
	{
		// Token: 0x17001B84 RID: 7044
		// (get) Token: 0x06006335 RID: 25397 RVA: 0x001A29C3 File Offset: 0x001A0BC3
		// (set) Token: 0x06006336 RID: 25398 RVA: 0x001A29CB File Offset: 0x001A0BCB
		public int? NumberOfBatchesExecuted { get; set; }

		// Token: 0x17001B85 RID: 7045
		// (get) Token: 0x06006337 RID: 25399 RVA: 0x001A29D4 File Offset: 0x001A0BD4
		// (set) Token: 0x06006338 RID: 25400 RVA: 0x001A29DC File Offset: 0x001A0BDC
		public int? NumberOfFoldersToBeSynced { get; set; }

		// Token: 0x17001B86 RID: 7046
		// (get) Token: 0x06006339 RID: 25401 RVA: 0x001A29E5 File Offset: 0x001A0BE5
		// (set) Token: 0x0600633A RID: 25402 RVA: 0x001A29ED File Offset: 0x001A0BED
		public int? BatchSize { get; set; }

		// Token: 0x17001B87 RID: 7047
		// (get) Token: 0x0600633B RID: 25403 RVA: 0x001A29F6 File Offset: 0x001A0BF6
		// (set) Token: 0x0600633C RID: 25404 RVA: 0x001A29FE File Offset: 0x001A0BFE
		public int? NumberOfFoldersSynced { get; set; }

		// Token: 0x0400381E RID: 14366
		internal const string LastSyncCycleLogConfigurationName = "PublicFolderLastSyncCylceLog";

		// Token: 0x0400381F RID: 14367
		internal const string SyncInfoConfigurationName = "PublicFolderSyncInfo";
	}
}
