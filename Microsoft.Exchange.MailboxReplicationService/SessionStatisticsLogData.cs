using System;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200007F RID: 127
	internal class SessionStatisticsLogData
	{
		// Token: 0x06000582 RID: 1410 RVA: 0x0001F5B9 File Offset: 0x0001D7B9
		internal SessionStatisticsLogData(Guid requestGuid, SessionStatistics sessionStatistics, SessionStatistics archiveSessionStatistics)
		{
			this.RequestGuid = requestGuid;
			this.SessionStatistics = sessionStatistics;
			this.ArchiveSessionStatistics = archiveSessionStatistics;
		}

		// Token: 0x04000268 RID: 616
		public readonly SessionStatistics SessionStatistics;

		// Token: 0x04000269 RID: 617
		public readonly SessionStatistics ArchiveSessionStatistics;

		// Token: 0x0400026A RID: 618
		public readonly Guid RequestGuid;
	}
}
