using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MailboxLoadBalance.Logging.MailboxStatistics
{
	// Token: 0x020000AC RID: 172
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class MailboxStatisticsLog : ObjectLog<MailboxStatisticsLogEntry>
	{
		// Token: 0x060005F1 RID: 1521 RVA: 0x0000F967 File Offset: 0x0000DB67
		private MailboxStatisticsLog(ObjectLogConfiguration configuration) : base(new MailboxStatisticsLogEntrySchema.MailboxStatisticsLogSchema(), configuration)
		{
		}

		// Token: 0x060005F2 RID: 1522 RVA: 0x0000F975 File Offset: 0x0000DB75
		public static ObjectLog<MailboxStatisticsLogEntry> CreateWithConfig(ObjectLogConfiguration config)
		{
			return new MailboxStatisticsLog(config);
		}
	}
}
