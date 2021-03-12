using System;
using System.Diagnostics;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Configuration.CertificateAuthentication.EventLog
{
	// Token: 0x02000009 RID: 9
	public static class TaskEventLogConstants
	{
		// Token: 0x04000023 RID: 35
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_CertAuth_ServerError = new ExEventLog.EventTuple(3221225572U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000024 RID: 36
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_CertAuth_TransientError = new ExEventLog.EventTuple(3221225573U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000025 RID: 37
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_CertAuth_TransientRecovery = new ExEventLog.EventTuple(2147483750U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000026 RID: 38
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_CertAuth_UserNotFound = new ExEventLog.EventTuple(3221225575U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000027 RID: 39
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_CertAuth_OrganizationNotFound = new ExEventLog.EventTuple(3221225576U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0200000A RID: 10
		private enum Category : short
		{
			// Token: 0x04000029 RID: 41
			General = 1
		}

		// Token: 0x0200000B RID: 11
		internal enum Message : uint
		{
			// Token: 0x0400002B RID: 43
			CertAuth_ServerError = 3221225572U,
			// Token: 0x0400002C RID: 44
			CertAuth_TransientError,
			// Token: 0x0400002D RID: 45
			CertAuth_TransientRecovery = 2147483750U,
			// Token: 0x0400002E RID: 46
			CertAuth_UserNotFound = 3221225575U,
			// Token: 0x0400002F RID: 47
			CertAuth_OrganizationNotFound
		}
	}
}
