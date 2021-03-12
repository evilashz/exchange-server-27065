using System;
using Microsoft.Exchange.MailboxReplicationService;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x0200006B RID: 107
	internal static class MigrationFailureLog
	{
		// Token: 0x06000637 RID: 1591 RVA: 0x0001CF44 File Offset: 0x0001B144
		public static void LogFailureEvent(MigrationJob migrationObject, Exception failureException, MigrationFailureFlags failureFlags = MigrationFailureFlags.None, string failureContext = null)
		{
			if (migrationObject != null && failureException != null)
			{
				FailureEvent failureEvent = new FailureEvent(migrationObject.JobId, "MigrationJob", (int)failureFlags, failureContext);
				CommonFailureLog.LogCommonFailureEvent(failureEvent, failureException);
			}
		}

		// Token: 0x06000638 RID: 1592 RVA: 0x0001CF74 File Offset: 0x0001B174
		public static void LogFailureEvent(MigrationJobItem migrationObject, Exception failureException, MigrationFailureFlags failureFlags = MigrationFailureFlags.None, string failureContext = null)
		{
			if (migrationObject != null && failureException != null)
			{
				FailureEvent failureEvent = new FailureEvent(migrationObject.JobItemGuid, "MigrationJobItem", (int)failureFlags, failureContext);
				CommonFailureLog.LogCommonFailureEvent(failureEvent, failureException);
			}
		}
	}
}
