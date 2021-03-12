using System;
using System.Diagnostics;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Servicelets.CertificateNotification.Messages
{
	// Token: 0x02000003 RID: 3
	public static class CertificateNotificationEventLogConstants
	{
		// Token: 0x04000013 RID: 19
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_TransientException = new ExEventLog.EventTuple(3221227473U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000014 RID: 20
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_OneRoundCompleted = new ExEventLog.EventTuple(1073743826U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000015 RID: 21
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_MicrosoftExchangeRecipientNotFoundException = new ExEventLog.EventTuple(3221227475U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x02000004 RID: 4
		private enum Category : short
		{
			// Token: 0x04000017 RID: 23
			General = 1
		}

		// Token: 0x02000005 RID: 5
		internal enum Message : uint
		{
			// Token: 0x04000019 RID: 25
			TransientException = 3221227473U,
			// Token: 0x0400001A RID: 26
			OneRoundCompleted = 1073743826U,
			// Token: 0x0400001B RID: 27
			MicrosoftExchangeRecipientNotFoundException = 3221227475U
		}
	}
}
