using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MailboxLoadBalance.Logging.SoftDeletedMailboxRemoval
{
	// Token: 0x020000B0 RID: 176
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class SoftDeletedMailboxRemovalLog : ObjectLog<SoftDeletedMailboxRemovalLogEntry>
	{
		// Token: 0x060005FB RID: 1531 RVA: 0x0000FB5A File Offset: 0x0000DD5A
		private SoftDeletedMailboxRemovalLog(ObjectLogConfiguration configuration) : base(new SoftDeletedMailboxRemovalLogEntrySchema.SoftDeletedMailboxRemovalLogEntryLogSchema(), configuration)
		{
		}

		// Token: 0x060005FC RID: 1532 RVA: 0x0000FB68 File Offset: 0x0000DD68
		public static object CreateWithConfig(ObjectLogConfiguration getLogConfig)
		{
			return new SoftDeletedMailboxRemovalLog(getLogConfig);
		}
	}
}
