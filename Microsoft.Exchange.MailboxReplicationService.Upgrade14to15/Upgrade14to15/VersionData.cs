using System;

namespace Microsoft.Exchange.MailboxReplicationService.Upgrade14to15
{
	// Token: 0x020000A1 RID: 161
	public struct VersionData
	{
		// Token: 0x0600041D RID: 1053 RVA: 0x0000660A File Offset: 0x0000480A
		public void Add(int primaryCount, double primarySize, int archiveCount, double archiveSize)
		{
			this.PrimaryData.Add(primaryCount, primarySize);
			this.ArchiveData.Add(archiveCount, archiveSize);
		}

		// Token: 0x040001D2 RID: 466
		public MbxData PrimaryData;

		// Token: 0x040001D3 RID: 467
		public MbxData ArchiveData;
	}
}
