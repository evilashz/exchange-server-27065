using System;
using System.Diagnostics;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Configuration.DelegatedAuthentication.EventLog
{
	// Token: 0x02000009 RID: 9
	public static class TaskEventLogConstants
	{
		// Token: 0x0400002F RID: 47
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_DelegatedAuth_RequestNotAuthenticated = new ExEventLog.EventTuple(3221225672U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000030 RID: 48
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_DelegatedAuth_FailedToResolveCurrentUser = new ExEventLog.EventTuple(3221225673U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000031 RID: 49
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_DelegatedAuth_FailedToDecryptSecurityToken = new ExEventLog.EventTuple(3221225674U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000032 RID: 50
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_DelegatedAuth_ExpiredSecurityToken = new ExEventLog.EventTuple(3221225675U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000033 RID: 51
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_DelegatedAuth_NoGroupMembershipOnSecurityToken = new ExEventLog.EventTuple(3221225676U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000034 RID: 52
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_DelegatedAuth_CannotResolveForestRedirection = new ExEventLog.EventTuple(3221225677U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000035 RID: 53
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_DelegatedAuth_AccessDenied = new ExEventLog.EventTuple(3221225678U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000036 RID: 54
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_DelegatedAuth_FailedToResolveSecretKey = new ExEventLog.EventTuple(3221225679U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000037 RID: 55
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_DelegatedAuth_ServerError = new ExEventLog.EventTuple(3221225680U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000038 RID: 56
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_DelegatedAuth_FailedToDecodeBase64SecurityToken = new ExEventLog.EventTuple(3221225681U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000039 RID: 57
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_DelegatedAuth_FailedToResolveTargetOrganization = new ExEventLog.EventTuple(3221225682U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0400003A RID: 58
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_DelegatedAuth_DelegatedPrincipalCacheIsFull = new ExEventLog.EventTuple(3221225683U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0400003B RID: 59
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_DelegatedAuth_FailedToReadMultiple = new ExEventLog.EventTuple(3221225684U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0200000A RID: 10
		private enum Category : short
		{
			// Token: 0x0400003D RID: 61
			General = 1
		}

		// Token: 0x0200000B RID: 11
		internal enum Message : uint
		{
			// Token: 0x0400003F RID: 63
			DelegatedAuth_RequestNotAuthenticated = 3221225672U,
			// Token: 0x04000040 RID: 64
			DelegatedAuth_FailedToResolveCurrentUser,
			// Token: 0x04000041 RID: 65
			DelegatedAuth_FailedToDecryptSecurityToken,
			// Token: 0x04000042 RID: 66
			DelegatedAuth_ExpiredSecurityToken,
			// Token: 0x04000043 RID: 67
			DelegatedAuth_NoGroupMembershipOnSecurityToken,
			// Token: 0x04000044 RID: 68
			DelegatedAuth_CannotResolveForestRedirection,
			// Token: 0x04000045 RID: 69
			DelegatedAuth_AccessDenied,
			// Token: 0x04000046 RID: 70
			DelegatedAuth_FailedToResolveSecretKey,
			// Token: 0x04000047 RID: 71
			DelegatedAuth_ServerError,
			// Token: 0x04000048 RID: 72
			DelegatedAuth_FailedToDecodeBase64SecurityToken,
			// Token: 0x04000049 RID: 73
			DelegatedAuth_FailedToResolveTargetOrganization,
			// Token: 0x0400004A RID: 74
			DelegatedAuth_DelegatedPrincipalCacheIsFull,
			// Token: 0x0400004B RID: 75
			DelegatedAuth_FailedToReadMultiple
		}
	}
}
