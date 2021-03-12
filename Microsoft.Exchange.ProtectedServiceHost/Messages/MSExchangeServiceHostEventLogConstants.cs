using System;
using System.Diagnostics;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.ServiceHost.Messages
{
	// Token: 0x0200000D RID: 13
	public static class MSExchangeServiceHostEventLogConstants
	{
		// Token: 0x04000020 RID: 32
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_Loading = new ExEventLog.EventTuple(1073743825U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000021 RID: 33
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ErrorLoadingNotFound = new ExEventLog.EventTuple(3221227474U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000022 RID: 34
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ServiceStarting = new ExEventLog.EventTuple(1073743827U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000023 RID: 35
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ServiceStarted = new ExEventLog.EventTuple(1073743828U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000024 RID: 36
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ServiceStopping = new ExEventLog.EventTuple(1073743833U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000025 RID: 37
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ServiceStopped = new ExEventLog.EventTuple(1073743834U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000026 RID: 38
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_UnhandledException = new ExEventLog.EventTuple(3221227483U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000027 RID: 39
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_HandlingCustomCommand = new ExEventLog.EventTuple(1073743836U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000028 RID: 40
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_CustomCommandHandled = new ExEventLog.EventTuple(1073743837U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000029 RID: 41
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_MailboxRoleInstalled = new ExEventLog.EventTuple(1073743838U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400002A RID: 42
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ClusteredMailboxRoleInstalled = new ExEventLog.EventTuple(1073743839U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400002B RID: 43
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ClientAccessRoleInstalled = new ExEventLog.EventTuple(1073743840U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400002C RID: 44
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_UnifiedMessagingRoleInstalled = new ExEventLog.EventTuple(1073743841U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400002D RID: 45
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_HubTransportRoleInstalled = new ExEventLog.EventTuple(1073743842U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400002E RID: 46
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_EdgeTransportRoleInstalled = new ExEventLog.EventTuple(1073743843U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400002F RID: 47
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_NotRunningAsLocalSystem = new ExEventLog.EventTuple(3221227492U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000030 RID: 48
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_CafeRoleInstalled = new ExEventLog.EventTuple(1073743845U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0200000E RID: 14
		private enum Category : short
		{
			// Token: 0x04000032 RID: 50
			General = 1
		}

		// Token: 0x0200000F RID: 15
		internal enum Message : uint
		{
			// Token: 0x04000034 RID: 52
			Loading = 1073743825U,
			// Token: 0x04000035 RID: 53
			ErrorLoadingNotFound = 3221227474U,
			// Token: 0x04000036 RID: 54
			ServiceStarting = 1073743827U,
			// Token: 0x04000037 RID: 55
			ServiceStarted,
			// Token: 0x04000038 RID: 56
			ServiceStopping = 1073743833U,
			// Token: 0x04000039 RID: 57
			ServiceStopped,
			// Token: 0x0400003A RID: 58
			UnhandledException = 3221227483U,
			// Token: 0x0400003B RID: 59
			HandlingCustomCommand = 1073743836U,
			// Token: 0x0400003C RID: 60
			CustomCommandHandled,
			// Token: 0x0400003D RID: 61
			MailboxRoleInstalled,
			// Token: 0x0400003E RID: 62
			ClusteredMailboxRoleInstalled,
			// Token: 0x0400003F RID: 63
			ClientAccessRoleInstalled,
			// Token: 0x04000040 RID: 64
			UnifiedMessagingRoleInstalled,
			// Token: 0x04000041 RID: 65
			HubTransportRoleInstalled,
			// Token: 0x04000042 RID: 66
			EdgeTransportRoleInstalled,
			// Token: 0x04000043 RID: 67
			NotRunningAsLocalSystem = 3221227492U,
			// Token: 0x04000044 RID: 68
			CafeRoleInstalled = 1073743845U
		}
	}
}
