using System;
using System.Diagnostics;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Servicelets.ExchangeCertificate.Messages
{
	// Token: 0x02000007 RID: 7
	public static class MSExchangeExchangeCertificateEventLogConstants
	{
		// Token: 0x0400000A RID: 10
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_PermanentException = new ExEventLog.EventTuple(3221227473U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x02000008 RID: 8
		private enum Category : short
		{
			// Token: 0x0400000C RID: 12
			General = 1
		}

		// Token: 0x02000009 RID: 9
		internal enum Message : uint
		{
			// Token: 0x0400000E RID: 14
			PermanentException = 3221227473U
		}
	}
}
