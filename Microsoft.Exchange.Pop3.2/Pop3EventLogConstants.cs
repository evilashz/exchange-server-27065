using System;
using System.Diagnostics;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Pop3
{
	// Token: 0x02000024 RID: 36
	public static class Pop3EventLogConstants
	{
		// Token: 0x040000E3 RID: 227
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_Pop3StartSuccess = new ExEventLog.EventTuple(263144U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040000E4 RID: 228
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_Pop3StopSuccess = new ExEventLog.EventTuple(263145U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040000E5 RID: 229
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_NoConfigurationFound = new ExEventLog.EventTuple(3221488619U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040000E6 RID: 230
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_BasicOverPlainText = new ExEventLog.EventTuple(2147746796U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040000E7 RID: 231
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_UserDisabled = new ExEventLog.EventTuple(2147746797U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040000E8 RID: 232
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_OwaServerNotFound = new ExEventLog.EventTuple(2147746798U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040000E9 RID: 233
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_OwaServerInvalid = new ExEventLog.EventTuple(2147746799U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040000EA RID: 234
		[EventLogPeriod(Period = "LogOneTime")]
		public static readonly ExEventLog.EventTuple Tuple_UserExceededNumberOfConnections = new ExEventLog.EventTuple(2147746801U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogOneTime);

		// Token: 0x040000EB RID: 235
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_CreateMailboxLoggerFailed = new ExEventLog.EventTuple(3221488626U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040000EC RID: 236
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_OnlineValueChanged = new ExEventLog.EventTuple(263155U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040000ED RID: 237
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_MaxConnectionCountExceeded = new ExEventLog.EventTuple(2147746893U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040000EE RID: 238
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_SslConnectionNotStarted = new ExEventLog.EventTuple(3221488718U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040000EF RID: 239
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_MaxConnectionsFromSingleIpExceeded = new ExEventLog.EventTuple(2147746895U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040000F0 RID: 240
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_PortBusy = new ExEventLog.EventTuple(3221489616U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040000F1 RID: 241
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_AdUserNotFound = new ExEventLog.EventTuple(2147747797U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040000F2 RID: 242
		[EventLogPeriod(Period = "LogOneTime")]
		public static readonly ExEventLog.EventTuple Tuple_SslCertificateNotFound = new ExEventLog.EventTuple(3221489623U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogOneTime);

		// Token: 0x040000F3 RID: 243
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ProcessNotResponding = new ExEventLog.EventTuple(3221489624U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040000F4 RID: 244
		[EventLogPeriod(Period = "LogOneTime")]
		public static readonly ExEventLog.EventTuple Tuple_ControlAddressInUse = new ExEventLog.EventTuple(3221489625U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogOneTime);

		// Token: 0x040000F5 RID: 245
		[EventLogPeriod(Period = "LogOneTime")]
		public static readonly ExEventLog.EventTuple Tuple_ControlAddressDenied = new ExEventLog.EventTuple(3221489626U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogOneTime);

		// Token: 0x040000F6 RID: 246
		[EventLogPeriod(Period = "LogOneTime")]
		public static readonly ExEventLog.EventTuple Tuple_SpnRegisterFailure = new ExEventLog.EventTuple(2147747803U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogOneTime);

		// Token: 0x040000F7 RID: 247
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_NoDefaultAcceptedDomainFound = new ExEventLog.EventTuple(3221489629U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040000F8 RID: 248
		[EventLogPeriod(Period = "LogOneTime")]
		public static readonly ExEventLog.EventTuple Tuple_InvalidDatacenterProxyKey = new ExEventLog.EventTuple(2147747806U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogOneTime);

		// Token: 0x040000F9 RID: 249
		[EventLogPeriod(Period = "LogOneTime")]
		public static readonly ExEventLog.EventTuple Tuple_NoPerfCounterTimer = new ExEventLog.EventTuple(2147747807U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogOneTime);

		// Token: 0x040000FA RID: 250
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_BadPasswordCodePage = new ExEventLog.EventTuple(3221489632U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040000FB RID: 251
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_LrsListError = new ExEventLog.EventTuple(3221489723U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040000FC RID: 252
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_LrsPartnerResolutionWarning = new ExEventLog.EventTuple(2147747900U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x02000025 RID: 37
		private enum Category : short
		{
			// Token: 0x040000FE RID: 254
			General = 1
		}

		// Token: 0x02000026 RID: 38
		internal enum Message : uint
		{
			// Token: 0x04000100 RID: 256
			Pop3StartSuccess = 263144U,
			// Token: 0x04000101 RID: 257
			Pop3StopSuccess,
			// Token: 0x04000102 RID: 258
			NoConfigurationFound = 3221488619U,
			// Token: 0x04000103 RID: 259
			BasicOverPlainText = 2147746796U,
			// Token: 0x04000104 RID: 260
			UserDisabled,
			// Token: 0x04000105 RID: 261
			OwaServerNotFound,
			// Token: 0x04000106 RID: 262
			OwaServerInvalid,
			// Token: 0x04000107 RID: 263
			UserExceededNumberOfConnections = 2147746801U,
			// Token: 0x04000108 RID: 264
			CreateMailboxLoggerFailed = 3221488626U,
			// Token: 0x04000109 RID: 265
			OnlineValueChanged = 263155U,
			// Token: 0x0400010A RID: 266
			MaxConnectionCountExceeded = 2147746893U,
			// Token: 0x0400010B RID: 267
			SslConnectionNotStarted = 3221488718U,
			// Token: 0x0400010C RID: 268
			MaxConnectionsFromSingleIpExceeded = 2147746895U,
			// Token: 0x0400010D RID: 269
			PortBusy = 3221489616U,
			// Token: 0x0400010E RID: 270
			AdUserNotFound = 2147747797U,
			// Token: 0x0400010F RID: 271
			SslCertificateNotFound = 3221489623U,
			// Token: 0x04000110 RID: 272
			ProcessNotResponding,
			// Token: 0x04000111 RID: 273
			ControlAddressInUse,
			// Token: 0x04000112 RID: 274
			ControlAddressDenied,
			// Token: 0x04000113 RID: 275
			SpnRegisterFailure = 2147747803U,
			// Token: 0x04000114 RID: 276
			NoDefaultAcceptedDomainFound = 3221489629U,
			// Token: 0x04000115 RID: 277
			InvalidDatacenterProxyKey = 2147747806U,
			// Token: 0x04000116 RID: 278
			NoPerfCounterTimer,
			// Token: 0x04000117 RID: 279
			BadPasswordCodePage = 3221489632U,
			// Token: 0x04000118 RID: 280
			LrsListError = 3221489723U,
			// Token: 0x04000119 RID: 281
			LrsPartnerResolutionWarning = 2147747900U
		}
	}
}
