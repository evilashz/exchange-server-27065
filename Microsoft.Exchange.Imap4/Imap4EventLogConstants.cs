using System;
using System.Diagnostics;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Imap4
{
	// Token: 0x02000052 RID: 82
	public static class Imap4EventLogConstants
	{
		// Token: 0x04000279 RID: 633
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_Imap4StartSuccess = new ExEventLog.EventTuple(263144U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400027A RID: 634
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_Imap4StopSuccess = new ExEventLog.EventTuple(263145U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400027B RID: 635
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_NoConfigurationFound = new ExEventLog.EventTuple(3221488619U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400027C RID: 636
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_BasicOverPlainText = new ExEventLog.EventTuple(2147746796U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400027D RID: 637
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_UserDisabled = new ExEventLog.EventTuple(2147746797U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0400027E RID: 638
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_OwaServerNotFound = new ExEventLog.EventTuple(2147746798U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0400027F RID: 639
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_OwaServerInvalid = new ExEventLog.EventTuple(2147746799U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000280 RID: 640
		[EventLogPeriod(Period = "LogOneTime")]
		public static readonly ExEventLog.EventTuple Tuple_UserExceededNumberOfConnections = new ExEventLog.EventTuple(2147746801U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogOneTime);

		// Token: 0x04000281 RID: 641
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_CreateMailboxLoggerFailed = new ExEventLog.EventTuple(3221488626U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000282 RID: 642
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_OnlineValueChanged = new ExEventLog.EventTuple(263155U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000283 RID: 643
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_MaxConnectionCountExceeded = new ExEventLog.EventTuple(2147746893U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000284 RID: 644
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_SslConnectionNotStarted = new ExEventLog.EventTuple(3221488718U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000285 RID: 645
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_MaxConnectionsFromSingleIpExceeded = new ExEventLog.EventTuple(2147746895U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000286 RID: 646
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_PortBusy = new ExEventLog.EventTuple(3221489616U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000287 RID: 647
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_AdUserNotFound = new ExEventLog.EventTuple(2147747797U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000288 RID: 648
		[EventLogPeriod(Period = "LogOneTime")]
		public static readonly ExEventLog.EventTuple Tuple_SslCertificateNotFound = new ExEventLog.EventTuple(3221489623U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogOneTime);

		// Token: 0x04000289 RID: 649
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ProcessNotResponding = new ExEventLog.EventTuple(3221489624U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400028A RID: 650
		[EventLogPeriod(Period = "LogOneTime")]
		public static readonly ExEventLog.EventTuple Tuple_ControlAddressInUse = new ExEventLog.EventTuple(3221489625U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogOneTime);

		// Token: 0x0400028B RID: 651
		[EventLogPeriod(Period = "LogOneTime")]
		public static readonly ExEventLog.EventTuple Tuple_ControlAddressDenied = new ExEventLog.EventTuple(3221489626U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogOneTime);

		// Token: 0x0400028C RID: 652
		[EventLogPeriod(Period = "LogOneTime")]
		public static readonly ExEventLog.EventTuple Tuple_SpnRegisterFailure = new ExEventLog.EventTuple(2147747803U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogOneTime);

		// Token: 0x0400028D RID: 653
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_NoDefaultAcceptedDomainFound = new ExEventLog.EventTuple(3221489629U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400028E RID: 654
		[EventLogPeriod(Period = "LogOneTime")]
		public static readonly ExEventLog.EventTuple Tuple_InvalidDatacenterProxyKey = new ExEventLog.EventTuple(2147747806U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogOneTime);

		// Token: 0x0400028F RID: 655
		[EventLogPeriod(Period = "LogOneTime")]
		public static readonly ExEventLog.EventTuple Tuple_NoPerfCounterTimer = new ExEventLog.EventTuple(2147747807U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogOneTime);

		// Token: 0x04000290 RID: 656
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ImapUidFix = new ExEventLog.EventTuple(264247U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000291 RID: 657
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ImapUidCorruptionDiscovered = new ExEventLog.EventTuple(264248U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000292 RID: 658
		[EventLogPeriod(Period = "LogOneTime")]
		public static readonly ExEventLog.EventTuple Tuple_InvalidImapUidFixRegKeys = new ExEventLog.EventTuple(2147747897U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogOneTime);

		// Token: 0x04000293 RID: 659
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_BadPasswordCodePage = new ExEventLog.EventTuple(3221489722U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000294 RID: 660
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_LrsListError = new ExEventLog.EventTuple(3221489723U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000295 RID: 661
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_LrsPartnerResolutionWarning = new ExEventLog.EventTuple(2147747900U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x02000053 RID: 83
		private enum Category : short
		{
			// Token: 0x04000297 RID: 663
			General = 1
		}

		// Token: 0x02000054 RID: 84
		internal enum Message : uint
		{
			// Token: 0x04000299 RID: 665
			Imap4StartSuccess = 263144U,
			// Token: 0x0400029A RID: 666
			Imap4StopSuccess,
			// Token: 0x0400029B RID: 667
			NoConfigurationFound = 3221488619U,
			// Token: 0x0400029C RID: 668
			BasicOverPlainText = 2147746796U,
			// Token: 0x0400029D RID: 669
			UserDisabled,
			// Token: 0x0400029E RID: 670
			OwaServerNotFound,
			// Token: 0x0400029F RID: 671
			OwaServerInvalid,
			// Token: 0x040002A0 RID: 672
			UserExceededNumberOfConnections = 2147746801U,
			// Token: 0x040002A1 RID: 673
			CreateMailboxLoggerFailed = 3221488626U,
			// Token: 0x040002A2 RID: 674
			OnlineValueChanged = 263155U,
			// Token: 0x040002A3 RID: 675
			MaxConnectionCountExceeded = 2147746893U,
			// Token: 0x040002A4 RID: 676
			SslConnectionNotStarted = 3221488718U,
			// Token: 0x040002A5 RID: 677
			MaxConnectionsFromSingleIpExceeded = 2147746895U,
			// Token: 0x040002A6 RID: 678
			PortBusy = 3221489616U,
			// Token: 0x040002A7 RID: 679
			AdUserNotFound = 2147747797U,
			// Token: 0x040002A8 RID: 680
			SslCertificateNotFound = 3221489623U,
			// Token: 0x040002A9 RID: 681
			ProcessNotResponding,
			// Token: 0x040002AA RID: 682
			ControlAddressInUse,
			// Token: 0x040002AB RID: 683
			ControlAddressDenied,
			// Token: 0x040002AC RID: 684
			SpnRegisterFailure = 2147747803U,
			// Token: 0x040002AD RID: 685
			NoDefaultAcceptedDomainFound = 3221489629U,
			// Token: 0x040002AE RID: 686
			InvalidDatacenterProxyKey = 2147747806U,
			// Token: 0x040002AF RID: 687
			NoPerfCounterTimer,
			// Token: 0x040002B0 RID: 688
			ImapUidFix = 264247U,
			// Token: 0x040002B1 RID: 689
			ImapUidCorruptionDiscovered,
			// Token: 0x040002B2 RID: 690
			InvalidImapUidFixRegKeys = 2147747897U,
			// Token: 0x040002B3 RID: 691
			BadPasswordCodePage = 3221489722U,
			// Token: 0x040002B4 RID: 692
			LrsListError,
			// Token: 0x040002B5 RID: 693
			LrsPartnerResolutionWarning = 2147747900U
		}
	}
}
