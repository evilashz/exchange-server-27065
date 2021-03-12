using System;
using System.Diagnostics;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.ServiceHost.Messages
{
	// Token: 0x02000019 RID: 25
	public static class MSExchangeServiceHostEventLogConstants
	{
		// Token: 0x04000055 RID: 85
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_Loading = new ExEventLog.EventTuple(1073743825U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000056 RID: 86
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ErrorLoadingNotFound = new ExEventLog.EventTuple(3221227474U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000057 RID: 87
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ServiceStarting = new ExEventLog.EventTuple(1073743827U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000058 RID: 88
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ServiceStarted = new ExEventLog.EventTuple(1073743828U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000059 RID: 89
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ServiceStopping = new ExEventLog.EventTuple(1073743833U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400005A RID: 90
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ServiceStopped = new ExEventLog.EventTuple(1073743834U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400005B RID: 91
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_UnhandledException = new ExEventLog.EventTuple(3221227483U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400005C RID: 92
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_HandlingCustomCommand = new ExEventLog.EventTuple(1073743836U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400005D RID: 93
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_CustomCommandHandled = new ExEventLog.EventTuple(1073743837U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400005E RID: 94
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_MailboxRoleInstalled = new ExEventLog.EventTuple(1073743838U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400005F RID: 95
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ClusteredMailboxRoleInstalled = new ExEventLog.EventTuple(1073743839U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000060 RID: 96
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ClientAccessRoleInstalled = new ExEventLog.EventTuple(1073743840U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000061 RID: 97
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_UnifiedMessagingRoleInstalled = new ExEventLog.EventTuple(1073743841U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000062 RID: 98
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_HubTransportRoleInstalled = new ExEventLog.EventTuple(1073743842U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000063 RID: 99
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_EdgeTransportRoleInstalled = new ExEventLog.EventTuple(1073743843U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000064 RID: 100
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_NotRunningAsLocalSystem = new ExEventLog.EventTuple(3221227492U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000065 RID: 101
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_CafeRoleInstalled = new ExEventLog.EventTuple(1073743845U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0200001A RID: 26
		private enum Category : short
		{
			// Token: 0x04000067 RID: 103
			General = 1
		}

		// Token: 0x0200001B RID: 27
		internal enum Message : uint
		{
			// Token: 0x04000069 RID: 105
			Loading = 1073743825U,
			// Token: 0x0400006A RID: 106
			ErrorLoadingNotFound = 3221227474U,
			// Token: 0x0400006B RID: 107
			ServiceStarting = 1073743827U,
			// Token: 0x0400006C RID: 108
			ServiceStarted,
			// Token: 0x0400006D RID: 109
			ServiceStopping = 1073743833U,
			// Token: 0x0400006E RID: 110
			ServiceStopped,
			// Token: 0x0400006F RID: 111
			UnhandledException = 3221227483U,
			// Token: 0x04000070 RID: 112
			HandlingCustomCommand = 1073743836U,
			// Token: 0x04000071 RID: 113
			CustomCommandHandled,
			// Token: 0x04000072 RID: 114
			MailboxRoleInstalled,
			// Token: 0x04000073 RID: 115
			ClusteredMailboxRoleInstalled,
			// Token: 0x04000074 RID: 116
			ClientAccessRoleInstalled,
			// Token: 0x04000075 RID: 117
			UnifiedMessagingRoleInstalled,
			// Token: 0x04000076 RID: 118
			HubTransportRoleInstalled,
			// Token: 0x04000077 RID: 119
			EdgeTransportRoleInstalled,
			// Token: 0x04000078 RID: 120
			NotRunningAsLocalSystem = 3221227492U,
			// Token: 0x04000079 RID: 121
			CafeRoleInstalled = 1073743845U
		}
	}
}
