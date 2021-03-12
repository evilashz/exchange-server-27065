using System;
using System.Diagnostics;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Messages
{
	// Token: 0x02000051 RID: 81
	public static class RpcClientAccessServiceEventLogConstants
	{
		// Token: 0x04000270 RID: 624
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_RpcClientAccessServiceStartPrivateSuccess = new ExEventLog.EventTuple(263144U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000271 RID: 625
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_RpcClientAccessServiceStopSuccess = new ExEventLog.EventTuple(263145U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000272 RID: 626
		[EventLogPeriod(Period = "LogOneTime")]
		public static readonly ExEventLog.EventTuple Tuple_SpnRegisterFailure = new ExEventLog.EventTuple(3221488618U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogOneTime);

		// Token: 0x04000273 RID: 627
		[EventLogPeriod(Period = "LogOneTime")]
		public static readonly ExEventLog.EventTuple Tuple_CannotStartServiceOnMailboxRole = new ExEventLog.EventTuple(263148U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogOneTime);

		// Token: 0x04000274 RID: 628
		[EventLogPeriod(Period = "LogOneTime")]
		public static readonly ExEventLog.EventTuple Tuple_DuplicateRpcEndpoint = new ExEventLog.EventTuple(3221488621U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogOneTime);

		// Token: 0x04000275 RID: 629
		[EventLogPeriod(Period = "LogOneTime")]
		public static readonly ExEventLog.EventTuple Tuple_StartingRpcClientService = new ExEventLog.EventTuple(263150U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogOneTime);

		// Token: 0x04000276 RID: 630
		[EventLogPeriod(Period = "LogOneTime")]
		public static readonly ExEventLog.EventTuple Tuple_StoppingRpcClientService = new ExEventLog.EventTuple(263151U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogOneTime);

		// Token: 0x04000277 RID: 631
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_RpcClientServiceUnexpectedExceptionOnStart = new ExEventLog.EventTuple(3221488624U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000278 RID: 632
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_RpcClientServiceUnexpectedExceptionOnStop = new ExEventLog.EventTuple(3221488625U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000279 RID: 633
		[EventLogPeriod(Period = "LogOneTime")]
		public static readonly ExEventLog.EventTuple Tuple_RpcClientServiceRemovingPrivilegeErrorOnStart = new ExEventLog.EventTuple(3221488626U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogOneTime);

		// Token: 0x0400027A RID: 634
		[EventLogPeriod(Period = "LogOneTime")]
		public static readonly ExEventLog.EventTuple Tuple_RpcClientServiceOrganizationInformationReadingFailure = new ExEventLog.EventTuple(3221488628U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogOneTime);

		// Token: 0x0400027B RID: 635
		[EventLogPeriod(Period = "LogOneTime")]
		public static readonly ExEventLog.EventTuple Tuple_ConfigurationLoadFailed = new ExEventLog.EventTuple(3221488629U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogOneTime);

		// Token: 0x0400027C RID: 636
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_UnexpectedExceptionOnConfigurationUpdate = new ExEventLog.EventTuple(3221488630U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400027D RID: 637
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_ConfigurationUpdateFailed = new ExEventLog.EventTuple(2147746807U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0400027E RID: 638
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_ConfigurationInvalidValueType = new ExEventLog.EventTuple(3221488632U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0400027F RID: 639
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_ConfigurationInvalidValue = new ExEventLog.EventTuple(3221488633U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000280 RID: 640
		[EventLogPeriod(Period = "LogOneTime")]
		public static readonly ExEventLog.EventTuple Tuple_ServiceProtocolNotEnabled = new ExEventLog.EventTuple(263162U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogOneTime);

		// Token: 0x04000281 RID: 641
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ConfigurationUpdateAfterError = new ExEventLog.EventTuple(263163U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000282 RID: 642
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_RpcClientAccessServiceStartPublicSuccess = new ExEventLog.EventTuple(263164U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000283 RID: 643
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ConfigurationUpdate = new ExEventLog.EventTuple(263165U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000284 RID: 644
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_FederatedAuthentication = new ExEventLog.EventTuple(3221488638U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000285 RID: 645
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_CannotRegisterEndPointAccessDenied = new ExEventLog.EventTuple(3221488639U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000286 RID: 646
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_WebServiceEndPointRegistered = new ExEventLog.EventTuple(263168U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000287 RID: 647
		[EventLogPeriod(Period = "LogOneTime")]
		public static readonly ExEventLog.EventTuple Tuple_RpcServiceManagerStarting = new ExEventLog.EventTuple(263169U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogOneTime);

		// Token: 0x04000288 RID: 648
		[EventLogPeriod(Period = "LogOneTime")]
		public static readonly ExEventLog.EventTuple Tuple_RpcServiceManagerStopping = new ExEventLog.EventTuple(263170U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogOneTime);

		// Token: 0x04000289 RID: 649
		[EventLogPeriod(Period = "LogOneTime")]
		public static readonly ExEventLog.EventTuple Tuple_RpcServiceManagerNoServices = new ExEventLog.EventTuple(263171U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogOneTime);

		// Token: 0x0400028A RID: 650
		[EventLogPeriod(Period = "LogOneTime")]
		public static readonly ExEventLog.EventTuple Tuple_RpcServiceManagerStartException = new ExEventLog.EventTuple(3221488644U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogOneTime);

		// Token: 0x0400028B RID: 651
		[EventLogPeriod(Period = "LogOneTime")]
		public static readonly ExEventLog.EventTuple Tuple_RpcServiceManagerStopException = new ExEventLog.EventTuple(3221488645U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogOneTime);

		// Token: 0x0400028C RID: 652
		[EventLogPeriod(Period = "LogOneTime")]
		public static readonly ExEventLog.EventTuple Tuple_RpcServiceManagerServiceLoadException = new ExEventLog.EventTuple(3221488646U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogOneTime);

		// Token: 0x0400028D RID: 653
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_RpcClientAccessServiceDeadlocked = new ExEventLog.EventTuple(3221488647U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400028E RID: 654
		[EventLogPeriod(Period = "LogOneTime")]
		public static readonly ExEventLog.EventTuple Tuple_RpcServiceManagerDuplicateRpcEndpoint = new ExEventLog.EventTuple(3221488648U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogOneTime);

		// Token: 0x0400028F RID: 655
		[EventLogPeriod(Period = "LogOneTime")]
		public static readonly ExEventLog.EventTuple Tuple_RpcServiceManagerShutdownTimeoutExceeded = new ExEventLog.EventTuple(3221488649U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogOneTime);

		// Token: 0x04000290 RID: 656
		[EventLogPeriod(Period = "LogOneTime")]
		public static readonly ExEventLog.EventTuple Tuple_StreamInsightsDataUploadFailed = new ExEventLog.EventTuple(2147484682U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogOneTime);

		// Token: 0x04000291 RID: 657
		[EventLogPeriod(Period = "LogOneTime")]
		public static readonly ExEventLog.EventTuple Tuple_StreamInsightsDataUploadExceptionThrown = new ExEventLog.EventTuple(2147484683U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogOneTime);

		// Token: 0x04000292 RID: 658
		[EventLogPeriod(Period = "LogOneTime")]
		public static readonly ExEventLog.EventTuple Tuple_StartingMSExchangeMapiMailboxAppPool = new ExEventLog.EventTuple(264144U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogOneTime);

		// Token: 0x04000293 RID: 659
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_MSExchangeMapiMailboxAppPoolStartSuccess = new ExEventLog.EventTuple(264145U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000294 RID: 660
		[EventLogPeriod(Period = "LogOneTime")]
		public static readonly ExEventLog.EventTuple Tuple_StoppingMSExchangeMapiMailboxAppPool = new ExEventLog.EventTuple(264146U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogOneTime);

		// Token: 0x04000295 RID: 661
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_MSExchangeMapiMailboxAppPoolStopSuccess = new ExEventLog.EventTuple(264147U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000296 RID: 662
		[EventLogPeriod(Period = "LogOneTime")]
		public static readonly ExEventLog.EventTuple Tuple_MapiMailboxRemovingPrivilegeErrorOnStart = new ExEventLog.EventTuple(3221489620U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogOneTime);

		// Token: 0x02000052 RID: 82
		private enum Category : short
		{
			// Token: 0x04000298 RID: 664
			General = 1
		}

		// Token: 0x02000053 RID: 83
		internal enum Message : uint
		{
			// Token: 0x0400029A RID: 666
			RpcClientAccessServiceStartPrivateSuccess = 263144U,
			// Token: 0x0400029B RID: 667
			RpcClientAccessServiceStopSuccess,
			// Token: 0x0400029C RID: 668
			SpnRegisterFailure = 3221488618U,
			// Token: 0x0400029D RID: 669
			CannotStartServiceOnMailboxRole = 263148U,
			// Token: 0x0400029E RID: 670
			DuplicateRpcEndpoint = 3221488621U,
			// Token: 0x0400029F RID: 671
			StartingRpcClientService = 263150U,
			// Token: 0x040002A0 RID: 672
			StoppingRpcClientService,
			// Token: 0x040002A1 RID: 673
			RpcClientServiceUnexpectedExceptionOnStart = 3221488624U,
			// Token: 0x040002A2 RID: 674
			RpcClientServiceUnexpectedExceptionOnStop,
			// Token: 0x040002A3 RID: 675
			RpcClientServiceRemovingPrivilegeErrorOnStart,
			// Token: 0x040002A4 RID: 676
			RpcClientServiceOrganizationInformationReadingFailure = 3221488628U,
			// Token: 0x040002A5 RID: 677
			ConfigurationLoadFailed,
			// Token: 0x040002A6 RID: 678
			UnexpectedExceptionOnConfigurationUpdate,
			// Token: 0x040002A7 RID: 679
			ConfigurationUpdateFailed = 2147746807U,
			// Token: 0x040002A8 RID: 680
			ConfigurationInvalidValueType = 3221488632U,
			// Token: 0x040002A9 RID: 681
			ConfigurationInvalidValue,
			// Token: 0x040002AA RID: 682
			ServiceProtocolNotEnabled = 263162U,
			// Token: 0x040002AB RID: 683
			ConfigurationUpdateAfterError,
			// Token: 0x040002AC RID: 684
			RpcClientAccessServiceStartPublicSuccess,
			// Token: 0x040002AD RID: 685
			ConfigurationUpdate,
			// Token: 0x040002AE RID: 686
			FederatedAuthentication = 3221488638U,
			// Token: 0x040002AF RID: 687
			CannotRegisterEndPointAccessDenied,
			// Token: 0x040002B0 RID: 688
			WebServiceEndPointRegistered = 263168U,
			// Token: 0x040002B1 RID: 689
			RpcServiceManagerStarting,
			// Token: 0x040002B2 RID: 690
			RpcServiceManagerStopping,
			// Token: 0x040002B3 RID: 691
			RpcServiceManagerNoServices,
			// Token: 0x040002B4 RID: 692
			RpcServiceManagerStartException = 3221488644U,
			// Token: 0x040002B5 RID: 693
			RpcServiceManagerStopException,
			// Token: 0x040002B6 RID: 694
			RpcServiceManagerServiceLoadException,
			// Token: 0x040002B7 RID: 695
			RpcClientAccessServiceDeadlocked,
			// Token: 0x040002B8 RID: 696
			RpcServiceManagerDuplicateRpcEndpoint,
			// Token: 0x040002B9 RID: 697
			RpcServiceManagerShutdownTimeoutExceeded,
			// Token: 0x040002BA RID: 698
			StreamInsightsDataUploadFailed = 2147484682U,
			// Token: 0x040002BB RID: 699
			StreamInsightsDataUploadExceptionThrown,
			// Token: 0x040002BC RID: 700
			StartingMSExchangeMapiMailboxAppPool = 264144U,
			// Token: 0x040002BD RID: 701
			MSExchangeMapiMailboxAppPoolStartSuccess,
			// Token: 0x040002BE RID: 702
			StoppingMSExchangeMapiMailboxAppPool,
			// Token: 0x040002BF RID: 703
			MSExchangeMapiMailboxAppPoolStopSuccess,
			// Token: 0x040002C0 RID: 704
			MapiMailboxRemovingPrivilegeErrorOnStart = 3221489620U
		}
	}
}
