using System;
using System.Diagnostics;
using Microsoft.Exchange.AnchorService;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Servicelets.AuthAdmin.Messages;

namespace Microsoft.Exchange.Servicelets.AuthAdmin
{
	// Token: 0x02000006 RID: 6
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class AuthAdminLogger : AnchorLogger
	{
		// Token: 0x0600000E RID: 14 RVA: 0x00002450 File Offset: 0x00000650
		public AuthAdminLogger(string applicationName, AnchorConfig config, ExEventLog eventLog) : base(applicationName, config, eventLog)
		{
		}

		// Token: 0x0600000F RID: 15 RVA: 0x0000245B File Offset: 0x0000065B
		public void LogEventLog(ExEventLog.EventTuple eventId, params object[] args)
		{
			this.LogEventLog(eventId, null, args);
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002468 File Offset: 0x00000668
		public void LogEventLog(ExEventLog.EventTuple eventId, Exception exception, params object[] args)
		{
			MigrationEventType eventType = MigrationEventType.Information;
			EventLogEntryType entryType = eventId.EntryType;
			switch (entryType)
			{
			case EventLogEntryType.Warning:
				eventType = MigrationEventType.Warning;
				goto IL_2A;
			case (EventLogEntryType)3:
				break;
			case EventLogEntryType.Information:
				goto IL_2A;
			default:
				if (entryType == EventLogEntryType.SuccessAudit)
				{
					goto IL_2A;
				}
				break;
			}
			eventType = MigrationEventType.Error;
			IL_2A:
			string formatString = "Event " + (eventId.EventId & 65535U).ToString();
			if (exception == null)
			{
				base.EventLogger.LogEvent(eventId, string.Empty, args);
				base.Log(eventType, formatString, args);
				return;
			}
			base.EventLogger.LogEvent(eventId, string.Empty, new object[]
			{
				exception,
				args
			});
			base.Log(eventType, exception, formatString, args);
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002510 File Offset: 0x00000710
		protected override ExEventLog.EventTuple? EventIdFromLogLevel(MigrationEventType eventType)
		{
			switch (eventType)
			{
			case MigrationEventType.Error:
				return new ExEventLog.EventTuple?(MSExchangeAuthAdminEventLogConstants.Tuple_CriticalError);
			case MigrationEventType.Warning:
				return new ExEventLog.EventTuple?(MSExchangeAuthAdminEventLogConstants.Tuple_Warning);
			case MigrationEventType.Information:
				return new ExEventLog.EventTuple?(MSExchangeAuthAdminEventLogConstants.Tuple_Information);
			default:
				return null;
			}
		}
	}
}
