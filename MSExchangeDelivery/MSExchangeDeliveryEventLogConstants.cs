using System;
using System.Diagnostics;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MailboxTransport.Delivery
{
	// Token: 0x0200000B RID: 11
	internal static class MSExchangeDeliveryEventLogConstants
	{
		// Token: 0x04000033 RID: 51
		public const string EventSource = "MSExchangeDelivery";

		// Token: 0x04000034 RID: 52
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_DeliveryServiceStartSuccess = new ExEventLog.EventTuple(263144U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000035 RID: 53
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_DeliveryServiceStopSuccess = new ExEventLog.EventTuple(263145U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000036 RID: 54
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_DeliveryServiceStartFailure = new ExEventLog.EventTuple(3221488618U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000037 RID: 55
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_DeliveryServiceStopFailure = new ExEventLog.EventTuple(3221488619U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000038 RID: 56
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_DeliveryPoisonMessage = new ExEventLog.EventTuple(3221488647U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0200000C RID: 12
		private enum Category : short
		{
			// Token: 0x0400003A RID: 58
			MSExchangeDelivery = 1
		}

		// Token: 0x0200000D RID: 13
		internal enum Message : uint
		{
			// Token: 0x0400003C RID: 60
			DeliveryServiceStartSuccess = 263144U,
			// Token: 0x0400003D RID: 61
			DeliveryServiceStopSuccess,
			// Token: 0x0400003E RID: 62
			DeliveryServiceStartFailure = 3221488618U,
			// Token: 0x0400003F RID: 63
			DeliveryServiceStopFailure,
			// Token: 0x04000040 RID: 64
			DeliveryPoisonMessage = 3221488647U
		}
	}
}
