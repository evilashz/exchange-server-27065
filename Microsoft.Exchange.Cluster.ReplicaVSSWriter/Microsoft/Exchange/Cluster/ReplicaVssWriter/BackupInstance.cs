using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Cluster.ReplicaVssWriter
{
	// Token: 0x0200013E RID: 318
	internal class BackupInstance
	{
		// Token: 0x060000C2 RID: 194 RVA: 0x00001A3C File Offset: 0x00000E3C
		public BackupInstance(Guid vssIdCurrentSnapshotSeId)
		{
			this.m_vssIdCurrentSnapshotSetId = vssIdCurrentSnapshotSeId;
			this.m_vssbackuptype = 0;
			this.m_fNoComponents = false;
			this.m_fFrozen = false;
			this.m_fSnapPrepared = false;
			this.m_fPostSnapshot = false;
			this.m_fBackupPrepared = false;
			this.m_StorageGroupBackups = new List<StorageGroupBackup>();
		}

		// Token: 0x04000278 RID: 632
		public Guid m_vssIdCurrentSnapshotSetId;

		// Token: 0x04000279 RID: 633
		public int m_vssbackuptype;

		// Token: 0x0400027A RID: 634
		public bool m_fNoComponents;

		// Token: 0x0400027B RID: 635
		public bool m_fFrozen;

		// Token: 0x0400027C RID: 636
		public bool m_fSnapPrepared;

		// Token: 0x0400027D RID: 637
		public bool m_fPostSnapshot;

		// Token: 0x0400027E RID: 638
		public bool m_fBackupPrepared;

		// Token: 0x0400027F RID: 639
		public List<StorageGroupBackup> m_StorageGroupBackups;
	}
}
