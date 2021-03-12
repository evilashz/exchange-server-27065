using System;
using System.Diagnostics;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Servicelets.SACLWatcher.Messages
{
	// Token: 0x02000003 RID: 3
	public static class SACLWatcherEventLogConstants
	{
		// Token: 0x04000008 RID: 8
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ErrorExchangeGroupNotFound = new ExEventLog.EventTuple(3221231473U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000009 RID: 9
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ErrorDomainControllerNotFound = new ExEventLog.EventTuple(3221231474U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400000A RID: 10
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ErrorOpenPolicyFailed = new ExEventLog.EventTuple(3221231475U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400000B RID: 11
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ErrorEnumerateRightsFailed = new ExEventLog.EventTuple(3221231476U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400000C RID: 12
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ErrorAddAccountRightsFailed = new ExEventLog.EventTuple(3221231477U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400000D RID: 13
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_WarningPrivilegeRemoved = new ExEventLog.EventTuple(2147489654U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400000E RID: 14
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_InfoPrivilegeRecovered = new ExEventLog.EventTuple(1073747831U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400000F RID: 15
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ErrorNoDomainController = new ExEventLog.EventTuple(3221232472U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000010 RID: 16
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ErrorNoLocalDomain = new ExEventLog.EventTuple(3221232473U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x02000004 RID: 4
		private enum Category : short
		{
			// Token: 0x04000012 RID: 18
			General = 1
		}

		// Token: 0x02000005 RID: 5
		internal enum Message : uint
		{
			// Token: 0x04000014 RID: 20
			ErrorExchangeGroupNotFound = 3221231473U,
			// Token: 0x04000015 RID: 21
			ErrorDomainControllerNotFound,
			// Token: 0x04000016 RID: 22
			ErrorOpenPolicyFailed,
			// Token: 0x04000017 RID: 23
			ErrorEnumerateRightsFailed,
			// Token: 0x04000018 RID: 24
			ErrorAddAccountRightsFailed,
			// Token: 0x04000019 RID: 25
			WarningPrivilegeRemoved = 2147489654U,
			// Token: 0x0400001A RID: 26
			InfoPrivilegeRecovered = 1073747831U,
			// Token: 0x0400001B RID: 27
			ErrorNoDomainController = 3221232472U,
			// Token: 0x0400001C RID: 28
			ErrorNoLocalDomain
		}
	}
}
