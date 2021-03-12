using System;
using System.Diagnostics;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Servicelets.CertificateDeployment.Messages
{
	// Token: 0x02000003 RID: 3
	public static class MSExchangeCertificateDeploymentEventLogConstants
	{
		// Token: 0x0400000A RID: 10
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_PermanentException = new ExEventLog.EventTuple(3221227473U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400000B RID: 11
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_NotificationException = new ExEventLog.EventTuple(2147485650U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400000C RID: 12
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_NeedCertificate = new ExEventLog.EventTuple(1073743827U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400000D RID: 13
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_InstalledCertificate = new ExEventLog.EventTuple(1073743828U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400000E RID: 14
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_CertificateNotFound = new ExEventLog.EventTuple(2147485653U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400000F RID: 15
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_TransientException = new ExEventLog.EventTuple(2147485654U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000010 RID: 16
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_EnableNetworkServiceException = new ExEventLog.EventTuple(3221227479U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000011 RID: 17
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_CannotFindLocalSite = new ExEventLog.EventTuple(3221227480U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000012 RID: 18
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_CertificateNearingExpiry = new ExEventLog.EventTuple(3221227481U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000013 RID: 19
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_CertificateExpired = new ExEventLog.EventTuple(3221227482U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x02000004 RID: 4
		private enum Category : short
		{
			// Token: 0x04000015 RID: 21
			General = 1
		}

		// Token: 0x02000005 RID: 5
		internal enum Message : uint
		{
			// Token: 0x04000017 RID: 23
			PermanentException = 3221227473U,
			// Token: 0x04000018 RID: 24
			NotificationException = 2147485650U,
			// Token: 0x04000019 RID: 25
			NeedCertificate = 1073743827U,
			// Token: 0x0400001A RID: 26
			InstalledCertificate,
			// Token: 0x0400001B RID: 27
			CertificateNotFound = 2147485653U,
			// Token: 0x0400001C RID: 28
			TransientException,
			// Token: 0x0400001D RID: 29
			EnableNetworkServiceException = 3221227479U,
			// Token: 0x0400001E RID: 30
			CannotFindLocalSite,
			// Token: 0x0400001F RID: 31
			CertificateNearingExpiry,
			// Token: 0x04000020 RID: 32
			CertificateExpired
		}
	}
}
