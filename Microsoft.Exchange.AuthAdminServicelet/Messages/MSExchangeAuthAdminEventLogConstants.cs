using System;
using System.Diagnostics;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Servicelets.AuthAdmin.Messages
{
	// Token: 0x02000009 RID: 9
	public static class MSExchangeAuthAdminEventLogConstants
	{
		// Token: 0x04000010 RID: 16
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_CriticalError = new ExEventLog.EventTuple(3221227473U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000011 RID: 17
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_Information = new ExEventLog.EventTuple(1073743826U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000012 RID: 18
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_PermanentException = new ExEventLog.EventTuple(3221227475U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000013 RID: 19
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_TransientException = new ExEventLog.EventTuple(2147485652U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000014 RID: 20
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_Warning = new ExEventLog.EventTuple(2147485653U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000015 RID: 21
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_InvalidTrustedIssuerConfiguration = new ExEventLog.EventTuple(3221227478U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000016 RID: 22
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_InvalidTrustedIssuerChanges = new ExEventLog.EventTuple(3221227479U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000017 RID: 23
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_InvalidMetadataUrl = new ExEventLog.EventTuple(3221227480U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000018 RID: 24
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_CorruptMetadata = new ExEventLog.EventTuple(3221227481U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000019 RID: 25
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_UnableToAccessMetadata = new ExEventLog.EventTuple(3221227482U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400001A RID: 26
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_InvalidCertificateList = new ExEventLog.EventTuple(3221227483U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400001B RID: 27
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_InvalidCertificates = new ExEventLog.EventTuple(3221227484U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400001C RID: 28
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_TrustedIssuerUpdated = new ExEventLog.EventTuple(1073743837U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400001D RID: 29
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_CurrentSigningUpdated = new ExEventLog.EventTuple(1073743838U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400001E RID: 30
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_AuthAdminCompleted = new ExEventLog.EventTuple(1073743839U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0200000A RID: 10
		private enum Category : short
		{
			// Token: 0x04000020 RID: 32
			General = 1
		}

		// Token: 0x0200000B RID: 11
		internal enum Message : uint
		{
			// Token: 0x04000022 RID: 34
			CriticalError = 3221227473U,
			// Token: 0x04000023 RID: 35
			Information = 1073743826U,
			// Token: 0x04000024 RID: 36
			PermanentException = 3221227475U,
			// Token: 0x04000025 RID: 37
			TransientException = 2147485652U,
			// Token: 0x04000026 RID: 38
			Warning,
			// Token: 0x04000027 RID: 39
			InvalidTrustedIssuerConfiguration = 3221227478U,
			// Token: 0x04000028 RID: 40
			InvalidTrustedIssuerChanges,
			// Token: 0x04000029 RID: 41
			InvalidMetadataUrl,
			// Token: 0x0400002A RID: 42
			CorruptMetadata,
			// Token: 0x0400002B RID: 43
			UnableToAccessMetadata,
			// Token: 0x0400002C RID: 44
			InvalidCertificateList,
			// Token: 0x0400002D RID: 45
			InvalidCertificates,
			// Token: 0x0400002E RID: 46
			TrustedIssuerUpdated = 1073743837U,
			// Token: 0x0400002F RID: 47
			CurrentSigningUpdated,
			// Token: 0x04000030 RID: 48
			AuthAdminCompleted
		}
	}
}
