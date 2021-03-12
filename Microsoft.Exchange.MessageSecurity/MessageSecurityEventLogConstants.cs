using System;
using System.Diagnostics;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MessageSecurity
{
	// Token: 0x02000007 RID: 7
	internal static class MessageSecurityEventLogConstants
	{
		// Token: 0x04000010 RID: 16
		public const string EventSource = "MSExchange Message Security";

		// Token: 0x04000011 RID: 17
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ReadServerConfigUnavail = new ExEventLog.EventTuple(3221488616U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000012 RID: 18
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ReadServerConfigFailed = new ExEventLog.EventTuple(3221488617U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000013 RID: 19
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_TlsCertificateNotFound = new ExEventLog.EventTuple(3221488619U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000014 RID: 20
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_LocalServerNotFound = new ExEventLog.EventTuple(3221488620U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000015 RID: 21
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_CouldNotDecryptPassword = new ExEventLog.EventTuple(3221488621U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000016 RID: 22
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_CredentialsRenewalFailure = new ExEventLog.EventTuple(3221488622U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000017 RID: 23
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_FailedReadingAdamSslPort = new ExEventLog.EventTuple(3221488623U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000018 RID: 24
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_CredentialsRenewalSuccess = new ExEventLog.EventTuple(1074004976U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000019 RID: 25
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_SubscriptionExpired = new ExEventLog.EventTuple(3221488625U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0400001A RID: 26
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_LeaseAlert = new ExEventLog.EventTuple(3221488626U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0400001B RID: 27
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_LocalServerNotFoundNoException = new ExEventLog.EventTuple(3221488627U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x02000008 RID: 8
		private enum Category : short
		{
			// Token: 0x0400001D RID: 29
			EdgeCredentialService = 1
		}

		// Token: 0x02000009 RID: 9
		internal enum Message : uint
		{
			// Token: 0x0400001F RID: 31
			ReadServerConfigUnavail = 3221488616U,
			// Token: 0x04000020 RID: 32
			ReadServerConfigFailed,
			// Token: 0x04000021 RID: 33
			TlsCertificateNotFound = 3221488619U,
			// Token: 0x04000022 RID: 34
			LocalServerNotFound,
			// Token: 0x04000023 RID: 35
			CouldNotDecryptPassword,
			// Token: 0x04000024 RID: 36
			CredentialsRenewalFailure,
			// Token: 0x04000025 RID: 37
			FailedReadingAdamSslPort,
			// Token: 0x04000026 RID: 38
			CredentialsRenewalSuccess = 1074004976U,
			// Token: 0x04000027 RID: 39
			SubscriptionExpired = 3221488625U,
			// Token: 0x04000028 RID: 40
			LeaseAlert,
			// Token: 0x04000029 RID: 41
			LocalServerNotFoundNoException
		}
	}
}
