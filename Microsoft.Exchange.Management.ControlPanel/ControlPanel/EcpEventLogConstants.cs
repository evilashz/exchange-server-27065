using System;
using System.Diagnostics;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200001D RID: 29
	public static class EcpEventLogConstants
	{
		// Token: 0x040019C3 RID: 6595
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_EcpStarted = new ExEventLog.EventTuple(1073741825U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040019C4 RID: 6596
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_EcpDisposed = new ExEventLog.EventTuple(1073741826U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040019C5 RID: 6597
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_EcpWebServiceStarted = new ExEventLog.EventTuple(1073741827U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040019C6 RID: 6598
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_RequestFailed = new ExEventLog.EventTuple(3221225476U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040019C7 RID: 6599
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_WebServiceFailed = new ExEventLog.EventTuple(3221225477U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040019C8 RID: 6600
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_ADTransientFailure = new ExEventLog.EventTuple(3221225478U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040019C9 RID: 6601
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_EcpWebServiceRequestStarted = new ExEventLog.EventTuple(1073741831U, 2, EventLogEntryType.Information, ExEventLog.EventLevel.Medium, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040019CA RID: 6602
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_EcpWebServiceRequestCompleted = new ExEventLog.EventTuple(1073741832U, 2, EventLogEntryType.Information, ExEventLog.EventLevel.Medium, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040019CB RID: 6603
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_EcpPowerShellInvoked = new ExEventLog.EventTuple(1073741833U, 2, EventLogEntryType.Information, ExEventLog.EventLevel.Medium, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040019CC RID: 6604
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_EcpPowerShellCompleted = new ExEventLog.EventTuple(1073741834U, 2, EventLogEntryType.Information, ExEventLog.EventLevel.Medium, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040019CD RID: 6605
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_EcpApplicationRequestStarted = new ExEventLog.EventTuple(1073741835U, 2, EventLogEntryType.Information, ExEventLog.EventLevel.Medium, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040019CE RID: 6606
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_EcpApplicationRequestEnded = new ExEventLog.EventTuple(1073741836U, 2, EventLogEntryType.Information, ExEventLog.EventLevel.Medium, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040019CF RID: 6607
		[EventLogPeriod(Period = "LogOneTime")]
		public static readonly ExEventLog.EventTuple Tuple_EcpPerformanceConsoleEnabled = new ExEventLog.EventTuple(2147483661U, 2, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogOneTime);

		// Token: 0x040019D0 RID: 6608
		[EventLogPeriod(Period = "LogOneTime")]
		public static readonly ExEventLog.EventTuple Tuple_EcpPerformanceIisLogEnabled = new ExEventLog.EventTuple(2147483662U, 2, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogOneTime);

		// Token: 0x040019D1 RID: 6609
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_EcpPerformanceRecord = new ExEventLog.EventTuple(1073741839U, 2, EventLogEntryType.Information, ExEventLog.EventLevel.Expert, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040019D2 RID: 6610
		[EventLogPeriod(Period = "LogOneTime")]
		public static readonly ExEventLog.EventTuple Tuple_EcpPerformanceEventLogMediumEnabled = new ExEventLog.EventTuple(2147483664U, 2, EventLogEntryType.Warning, ExEventLog.EventLevel.Medium, ExEventLog.EventPeriod.LogOneTime);

		// Token: 0x040019D3 RID: 6611
		[EventLogPeriod(Period = "LogOneTime")]
		public static readonly ExEventLog.EventTuple Tuple_EcpPerformanceEventLogHighEnabled = new ExEventLog.EventTuple(2147483665U, 2, EventLogEntryType.Warning, ExEventLog.EventLevel.High, ExEventLog.EventPeriod.LogOneTime);

		// Token: 0x040019D4 RID: 6612
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_EcpRedirectCasServer = new ExEventLog.EventTuple(1073741842U, 3, EventLogEntryType.Information, ExEventLog.EventLevel.Low, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040019D5 RID: 6613
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_EcpRedirectCantFindCasServer = new ExEventLog.EventTuple(2147483667U, 3, EventLogEntryType.Warning, ExEventLog.EventLevel.Low, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040019D6 RID: 6614
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_EcpRedirectCantFindUserMailbox = new ExEventLog.EventTuple(1073741844U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Low, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040019D7 RID: 6615
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_ScriptRequestFailed = new ExEventLog.EventTuple(3221225493U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040019D8 RID: 6616
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_EcpProxyCantFindCasServer = new ExEventLog.EventTuple(2147483670U, 4, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040019D9 RID: 6617
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_OutboundProxySessionInitialize = new ExEventLog.EventTuple(1073741847U, 4, EventLogEntryType.Information, ExEventLog.EventLevel.Low, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040019DA RID: 6618
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_InboundProxySessionInitialize = new ExEventLog.EventTuple(1073741848U, 4, EventLogEntryType.Information, ExEventLog.EventLevel.Low, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040019DB RID: 6619
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_StandardSessionInitialize = new ExEventLog.EventTuple(1073741849U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Low, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040019DC RID: 6620
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_ProxyErrorSslTrustFailure = new ExEventLog.EventTuple(3221225498U, 4, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040019DD RID: 6621
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_ProxyErrorUnauthorized = new ExEventLog.EventTuple(3221225499U, 4, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040019DE RID: 6622
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_ProxyErrorForbidden = new ExEventLog.EventTuple(3221225500U, 4, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040019DF RID: 6623
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_ProxyErrorServiceUnavailable = new ExEventLog.EventTuple(3221225501U, 4, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040019E0 RID: 6624
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ProxyRequestFailed = new ExEventLog.EventTuple(3221225502U, 4, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040019E1 RID: 6625
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_UnableToDetectRbacRoleViaCmdlet = new ExEventLog.EventTuple(3221225503U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040019E2 RID: 6626
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_EsoStandardSessionInitialize = new ExEventLog.EventTuple(1073741856U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Low, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040019E3 RID: 6627
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_PowershellExceptionTranslated = new ExEventLog.EventTuple(1073741857U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040019E4 RID: 6628
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_ProxyErrorCASCompatibility = new ExEventLog.EventTuple(3221225506U, 4, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040019E5 RID: 6629
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_UploadRequestStarted = new ExEventLog.EventTuple(1073741859U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Low, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040019E6 RID: 6630
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_UploadRequestCompleted = new ExEventLog.EventTuple(1073741860U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Low, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040019E7 RID: 6631
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_NewCanaryIssued = new ExEventLog.EventTuple(1073741861U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040019E8 RID: 6632
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_InvalidCanaryDetected = new ExEventLog.EventTuple(3221225510U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040019E9 RID: 6633
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ResetCanaryInCookie = new ExEventLog.EventTuple(2147483687U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040019EA RID: 6634
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_InvalidCanaryInCookieDetected = new ExEventLog.EventTuple(3221225512U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040019EB RID: 6635
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_InvalidClientTokenDetected = new ExEventLog.EventTuple(3221225513U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040019EC RID: 6636
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_MissingRequiredParameterDetected = new ExEventLog.EventTuple(3221225514U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040019ED RID: 6637
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_BadLinkedInConfiguration = new ExEventLog.EventTuple(3221225515U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040019EE RID: 6638
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_LinkedInAuthorizationError = new ExEventLog.EventTuple(3221225516U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040019EF RID: 6639
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_LoadReportingschemaFailed = new ExEventLog.EventTuple(3221225517U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040019F0 RID: 6640
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_BadFacebookConfiguration = new ExEventLog.EventTuple(3221225518U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040019F1 RID: 6641
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_AsyncWebRequestStarted = new ExEventLog.EventTuple(1073741871U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040019F2 RID: 6642
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_AsyncWebRequestEnded = new ExEventLog.EventTuple(1073741872U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040019F3 RID: 6643
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_AsyncWebRequestFailed = new ExEventLog.EventTuple(3221225521U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040019F4 RID: 6644
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_AsyncWebRequestFailedInCancel = new ExEventLog.EventTuple(3221225522U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040019F5 RID: 6645
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_Office365ShellServiceFailed = new ExEventLog.EventTuple(3221225523U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040019F6 RID: 6646
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_Office365NavBarLoadConfigFailed = new ExEventLog.EventTuple(3221225524U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040019F7 RID: 6647
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_TooManyAsyncTaskInServer = new ExEventLog.EventTuple(1073741877U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040019F8 RID: 6648
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_TooManyAsyncTaskFromCurrentUser = new ExEventLog.EventTuple(1073741878U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040019F9 RID: 6649
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_LanguagePackIsNotInstalled = new ExEventLog.EventTuple(2147483703U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040019FA RID: 6650
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_ConfigurableValueOutOfRange = new ExEventLog.EventTuple(2147483704U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040019FB RID: 6651
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_ActivityContextError = new ExEventLog.EventTuple(3221225529U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040019FC RID: 6652
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_Office365NavBarCallServiceTimeout = new ExEventLog.EventTuple(3221225530U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0200001E RID: 30
		private enum Category : short
		{
			// Token: 0x040019FE RID: 6654
			General = 1,
			// Token: 0x040019FF RID: 6655
			Performance,
			// Token: 0x04001A00 RID: 6656
			Redirect,
			// Token: 0x04001A01 RID: 6657
			Proxy
		}

		// Token: 0x0200001F RID: 31
		internal enum Message : uint
		{
			// Token: 0x04001A03 RID: 6659
			EcpStarted = 1073741825U,
			// Token: 0x04001A04 RID: 6660
			EcpDisposed,
			// Token: 0x04001A05 RID: 6661
			EcpWebServiceStarted,
			// Token: 0x04001A06 RID: 6662
			RequestFailed = 3221225476U,
			// Token: 0x04001A07 RID: 6663
			WebServiceFailed,
			// Token: 0x04001A08 RID: 6664
			ADTransientFailure,
			// Token: 0x04001A09 RID: 6665
			EcpWebServiceRequestStarted = 1073741831U,
			// Token: 0x04001A0A RID: 6666
			EcpWebServiceRequestCompleted,
			// Token: 0x04001A0B RID: 6667
			EcpPowerShellInvoked,
			// Token: 0x04001A0C RID: 6668
			EcpPowerShellCompleted,
			// Token: 0x04001A0D RID: 6669
			EcpApplicationRequestStarted,
			// Token: 0x04001A0E RID: 6670
			EcpApplicationRequestEnded,
			// Token: 0x04001A0F RID: 6671
			EcpPerformanceConsoleEnabled = 2147483661U,
			// Token: 0x04001A10 RID: 6672
			EcpPerformanceIisLogEnabled,
			// Token: 0x04001A11 RID: 6673
			EcpPerformanceRecord = 1073741839U,
			// Token: 0x04001A12 RID: 6674
			EcpPerformanceEventLogMediumEnabled = 2147483664U,
			// Token: 0x04001A13 RID: 6675
			EcpPerformanceEventLogHighEnabled,
			// Token: 0x04001A14 RID: 6676
			EcpRedirectCasServer = 1073741842U,
			// Token: 0x04001A15 RID: 6677
			EcpRedirectCantFindCasServer = 2147483667U,
			// Token: 0x04001A16 RID: 6678
			EcpRedirectCantFindUserMailbox = 1073741844U,
			// Token: 0x04001A17 RID: 6679
			ScriptRequestFailed = 3221225493U,
			// Token: 0x04001A18 RID: 6680
			EcpProxyCantFindCasServer = 2147483670U,
			// Token: 0x04001A19 RID: 6681
			OutboundProxySessionInitialize = 1073741847U,
			// Token: 0x04001A1A RID: 6682
			InboundProxySessionInitialize,
			// Token: 0x04001A1B RID: 6683
			StandardSessionInitialize,
			// Token: 0x04001A1C RID: 6684
			ProxyErrorSslTrustFailure = 3221225498U,
			// Token: 0x04001A1D RID: 6685
			ProxyErrorUnauthorized,
			// Token: 0x04001A1E RID: 6686
			ProxyErrorForbidden,
			// Token: 0x04001A1F RID: 6687
			ProxyErrorServiceUnavailable,
			// Token: 0x04001A20 RID: 6688
			ProxyRequestFailed,
			// Token: 0x04001A21 RID: 6689
			UnableToDetectRbacRoleViaCmdlet,
			// Token: 0x04001A22 RID: 6690
			EsoStandardSessionInitialize = 1073741856U,
			// Token: 0x04001A23 RID: 6691
			PowershellExceptionTranslated,
			// Token: 0x04001A24 RID: 6692
			ProxyErrorCASCompatibility = 3221225506U,
			// Token: 0x04001A25 RID: 6693
			UploadRequestStarted = 1073741859U,
			// Token: 0x04001A26 RID: 6694
			UploadRequestCompleted,
			// Token: 0x04001A27 RID: 6695
			NewCanaryIssued,
			// Token: 0x04001A28 RID: 6696
			InvalidCanaryDetected = 3221225510U,
			// Token: 0x04001A29 RID: 6697
			ResetCanaryInCookie = 2147483687U,
			// Token: 0x04001A2A RID: 6698
			InvalidCanaryInCookieDetected = 3221225512U,
			// Token: 0x04001A2B RID: 6699
			InvalidClientTokenDetected,
			// Token: 0x04001A2C RID: 6700
			MissingRequiredParameterDetected,
			// Token: 0x04001A2D RID: 6701
			BadLinkedInConfiguration,
			// Token: 0x04001A2E RID: 6702
			LinkedInAuthorizationError,
			// Token: 0x04001A2F RID: 6703
			LoadReportingschemaFailed,
			// Token: 0x04001A30 RID: 6704
			BadFacebookConfiguration,
			// Token: 0x04001A31 RID: 6705
			AsyncWebRequestStarted = 1073741871U,
			// Token: 0x04001A32 RID: 6706
			AsyncWebRequestEnded,
			// Token: 0x04001A33 RID: 6707
			AsyncWebRequestFailed = 3221225521U,
			// Token: 0x04001A34 RID: 6708
			AsyncWebRequestFailedInCancel,
			// Token: 0x04001A35 RID: 6709
			Office365ShellServiceFailed,
			// Token: 0x04001A36 RID: 6710
			Office365NavBarLoadConfigFailed,
			// Token: 0x04001A37 RID: 6711
			TooManyAsyncTaskInServer = 1073741877U,
			// Token: 0x04001A38 RID: 6712
			TooManyAsyncTaskFromCurrentUser,
			// Token: 0x04001A39 RID: 6713
			LanguagePackIsNotInstalled = 2147483703U,
			// Token: 0x04001A3A RID: 6714
			ConfigurableValueOutOfRange,
			// Token: 0x04001A3B RID: 6715
			ActivityContextError = 3221225529U,
			// Token: 0x04001A3C RID: 6716
			Office365NavBarCallServiceTimeout
		}
	}
}
