using System;
using System.Diagnostics;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020001AB RID: 427
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal static class StorageEventLogConstants
	{
		// Token: 0x04000B51 RID: 2897
		public const string EventSource = "MSExchange Mid-Tier Storage";

		// Token: 0x04000B52 RID: 2898
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_ExternalAuthDisabledAutoDiscover = new ExEventLog.EventTuple(3221226473U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Expert, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000B53 RID: 2899
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_AutoDiscoverFailedToAquireSecurityToken = new ExEventLog.EventTuple(3221226474U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Expert, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000B54 RID: 2900
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_AutoDiscoverFailed = new ExEventLog.EventTuple(3221226475U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Expert, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000B55 RID: 2901
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_ExternalAuthDisabledExchangePrincipal = new ExEventLog.EventTuple(3221226476U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Expert, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000B56 RID: 2902
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_XtcInvalidSmtpAddress = new ExEventLog.EventTuple(3221226477U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Expert, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000B57 RID: 2903
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_XtcOrgRelationshipMissing = new ExEventLog.EventTuple(3221226478U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Expert, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000B58 RID: 2904
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_XtcArchiveGuidMissing = new ExEventLog.EventTuple(3221226479U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Expert, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000B59 RID: 2905
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_XtcAutoDiscoverRequestFailed = new ExEventLog.EventTuple(3221226480U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Expert, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000B5A RID: 2906
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_XtcMapiError = new ExEventLog.EventTuple(3221226481U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Expert, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000B5B RID: 2907
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_ExternalAuthDisabledMailboxSession = new ExEventLog.EventTuple(3221226482U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Expert, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000B5C RID: 2908
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_ExternalArchiveDisabled = new ExEventLog.EventTuple(3221226483U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Expert, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000B5D RID: 2909
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_FederatedMailboxMisconfigured = new ExEventLog.EventTuple(3221226484U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Expert, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000B5E RID: 2910
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_AutoDiscoverFailedForSetting = new ExEventLog.EventTuple(3221226485U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Expert, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000B5F RID: 2911
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_XtcOrgRelationshipArchiveDisabled = new ExEventLog.EventTuple(3221226486U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Expert, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000B60 RID: 2912
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_XtcInvalidOrgRelationshipTargetAutodiscoverEpr = new ExEventLog.EventTuple(3221226487U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Expert, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000B61 RID: 2913
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_XtcInvalidOrgRelationshipTargetApplicationUri = new ExEventLog.EventTuple(3221226488U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Expert, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000B62 RID: 2914
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ErrorSavingMailboxAudit = new ExEventLog.EventTuple(3221227473U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000B63 RID: 2915
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ErrorSavingLastAccessTime = new ExEventLog.EventTuple(3221227474U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000B64 RID: 2916
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ErrorReadingBypassAudit = new ExEventLog.EventTuple(3221227475U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000B65 RID: 2917
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ErrorResolvingLogonUser = new ExEventLog.EventTuple(3221227476U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000B66 RID: 2918
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_ErrorOpeningParticipantSession = new ExEventLog.EventTuple(2147485653U, 2, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000B67 RID: 2919
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_ErrorBindingMessageItem = new ExEventLog.EventTuple(2147485654U, 2, EventLogEntryType.Warning, ExEventLog.EventLevel.High, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000B68 RID: 2920
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_ErrorBindingFolderForFolderBindHistory = new ExEventLog.EventTuple(2147485655U, 2, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000B69 RID: 2921
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_ErrorSavingFolderBindHistory = new ExEventLog.EventTuple(2147485656U, 2, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000B6A RID: 2922
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ErrorResolvingFromAddress = new ExEventLog.EventTuple(2147485657U, 2, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000B6B RID: 2923
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ErrorLoadAuditPolicyConfiguration = new ExEventLog.EventTuple(3221227482U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000B6C RID: 2924
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SearchObjectSaved = new ExEventLog.EventTuple(1074006969U, 3, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000B6D RID: 2925
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SearchStatusSaved = new ExEventLog.EventTuple(1074006970U, 3, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000B6E RID: 2926
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SearchStatusError = new ExEventLog.EventTuple(3221490619U, 3, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000B6F RID: 2927
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_DiscoveryAndHoldSaved = new ExEventLog.EventTuple(1074006972U, 3, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000B70 RID: 2928
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_DiscoverySearchStartRequested = new ExEventLog.EventTuple(1074006973U, 3, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000B71 RID: 2929
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_DiscoverySearchStopRequested = new ExEventLog.EventTuple(1074006974U, 3, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000B72 RID: 2930
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_DiscoverySearchRemoveRequested = new ExEventLog.EventTuple(1074006975U, 3, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000B73 RID: 2931
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_InPlaceHoldSettingsSynchronized = new ExEventLog.EventTuple(1074006976U, 3, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000B74 RID: 2932
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SynchronizeInPlaceHoldError = new ExEventLog.EventTuple(3221490625U, 3, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000B75 RID: 2933
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_DiscoverySearchRequestPickedUp = new ExEventLog.EventTuple(1074006978U, 3, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000B76 RID: 2934
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_DiscoverySearchStartRequestProcessed = new ExEventLog.EventTuple(1074006979U, 3, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000B77 RID: 2935
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_DiscoverySearchStopRequestProcessed = new ExEventLog.EventTuple(1074006980U, 3, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000B78 RID: 2936
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_DiscoverySearchRemoveRequestProcessed = new ExEventLog.EventTuple(1074006981U, 3, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000B79 RID: 2937
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_DiscoverySearchTaskError = new ExEventLog.EventTuple(3221490630U, 3, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000B7A RID: 2938
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_DiscoverySearchTaskStarted = new ExEventLog.EventTuple(1074006983U, 3, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000B7B RID: 2939
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_DiscoverySearchTaskCompleted = new ExEventLog.EventTuple(1074006984U, 3, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000B7C RID: 2940
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_DiscoverySearchServerError = new ExEventLog.EventTuple(3221490633U, 3, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000B7D RID: 2941
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_DiscoverySearchStatusChanged = new ExEventLog.EventTuple(1074006986U, 3, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000B7E RID: 2942
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_FailedToSyncDiscoveryHoldToExchangeOnline = new ExEventLog.EventTuple(3221490635U, 3, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000B7F RID: 2943
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SyncDiscoveryHoldToExchangeOnlineStart = new ExEventLog.EventTuple(1074006988U, 3, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000B80 RID: 2944
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SyncDiscoveryHoldToExchangeOnlineDetails = new ExEventLog.EventTuple(1074006989U, 3, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000B81 RID: 2945
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SingleFailureSyncDiscoveryHoldToExchangeOnline = new ExEventLog.EventTuple(3221490638U, 3, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000B82 RID: 2946
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_DiscoverySearchWorkItemQueueChanged = new ExEventLog.EventTuple(1074006991U, 3, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000B83 RID: 2947
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_DiscoverySearchWorkItemQueueNotProcessed = new ExEventLog.EventTuple(1074006992U, 3, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000B84 RID: 2948
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_DiscoverySearchRpcServerRestarted = new ExEventLog.EventTuple(1074006993U, 3, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000B85 RID: 2949
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_RecipientDLExpansionIsProcessing = new ExEventLog.EventTuple(1074007069U, 3, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000B86 RID: 2950
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_RecipientDLExpansionFailed = new ExEventLog.EventTuple(3221490718U, 3, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000B87 RID: 2951
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_RecipientDLExpansionMessageAlreadyProcessed = new ExEventLog.EventTuple(1074007071U, 3, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000B88 RID: 2952
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_RecipientDLExpansionMessageNoDLRecipients = new ExEventLog.EventTuple(1074007072U, 3, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000B89 RID: 2953
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_RecipientDLExpansionMessageNoLongerExist = new ExEventLog.EventTuple(1074007073U, 3, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000B8A RID: 2954
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_RecipientDLExpansionMismatchResults = new ExEventLog.EventTuple(3221490722U, 3, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000B8B RID: 2955
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_RecipientDLExpansionMaxNestedDLsLimit = new ExEventLog.EventTuple(3221490723U, 3, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000B8C RID: 2956
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_RecipientDLExpansionMaxRecipientsLimit = new ExEventLog.EventTuple(3221490724U, 3, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000B8D RID: 2957
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_RecipientDLExpansionSkipped = new ExEventLog.EventTuple(1074007077U, 3, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000B8E RID: 2958
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_RecipientDLExpansionUpdateItemInDumpster = new ExEventLog.EventTuple(1074007078U, 3, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000B8F RID: 2959
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_UnknownTemplateInPublishingLicense = new ExEventLog.EventTuple(3221229472U, 4, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000B90 RID: 2960
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_ErrorMultipleSaveOperationFailed = new ExEventLog.EventTuple(2147488649U, 5, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000B91 RID: 2961
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_ErrorCOWCacheWaitTimeout = new ExEventLog.EventTuple(2147488650U, 5, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000B92 RID: 2962
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_COWCalendarLoggingDisabled = new ExEventLog.EventTuple(2147488651U, 5, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000B93 RID: 2963
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_COWCalendarLoggingStopped = new ExEventLog.EventTuple(2147488652U, 5, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000B94 RID: 2964
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_ErrorDatabasePingTimedOut = new ExEventLog.EventTuple(2147489650U, 6, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000B95 RID: 2965
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_PFRuleConfigGetLocalIPFailure = new ExEventLog.EventTuple(3221494617U, 7, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000B96 RID: 2966
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_PFRuleSettingFromAddressFailure = new ExEventLog.EventTuple(2147752794U, 7, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000B97 RID: 2967
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_PopulatedServiceTopology = new ExEventLog.EventTuple(1073750824U, 9, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000B98 RID: 2968
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ErrorPopulatingServiceTopology = new ExEventLog.EventTuple(3221234473U, 9, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000B99 RID: 2969
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_RegisteredForTopologyChangedNotification = new ExEventLog.EventTuple(1073750826U, 9, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000B9A RID: 2970
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ErrorRegisteringForTopologyChangedNotification = new ExEventLog.EventTuple(3221234475U, 9, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000B9B RID: 2971
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ErrorCreateNotification = new ExEventLog.EventTuple(3221235473U, 10, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000B9C RID: 2972
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_ErrorUpdateNotification = new ExEventLog.EventTuple(3221235474U, 10, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000B9D RID: 2973
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_ErrorRemoveNotification = new ExEventLog.EventTuple(3221235475U, 10, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000B9E RID: 2974
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_ErrorDiscoverEwsUrlForMailbox = new ExEventLog.EventTuple(3221235476U, 10, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000B9F RID: 2975
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_ErrorSendNotificationEmail = new ExEventLog.EventTuple(3221235477U, 10, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000BA0 RID: 2976
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_ErrorActiveManagerClientADTimeout = new ExEventLog.EventTuple(3221235478U, 10, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000BA1 RID: 2977
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_ErrorActiveManagerClientADError = new ExEventLog.EventTuple(3221235479U, 10, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000BA2 RID: 2978
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_DkmDecryptionFailure = new ExEventLog.EventTuple(3221235480U, 4, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000BA3 RID: 2979
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_RsaCapiKeyImportFailure = new ExEventLog.EventTuple(3221235481U, 4, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000BA4 RID: 2980
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_ErrorCheckReplicationFlushedDatabaseNotFound = new ExEventLog.EventTuple(3221235482U, 10, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000BA5 RID: 2981
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_ErrorCheckReplicationFlushed = new ExEventLog.EventTuple(3221235483U, 10, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000BA6 RID: 2982
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_ErrorCheckReplicationThrottlingDatabaseNotFound = new ExEventLog.EventTuple(3221235484U, 10, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000BA7 RID: 2983
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_ErrorCheckReplicationThrottling = new ExEventLog.EventTuple(3221235485U, 10, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000BA8 RID: 2984
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_ActiveManagerClientAnotherThreadInADCall = new ExEventLog.EventTuple(1073751838U, 10, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000BA9 RID: 2985
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_ActiveManagerClientAnotherThreadInADCallTimeout = new ExEventLog.EventTuple(2147493663U, 10, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000BAA RID: 2986
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_ActiveManagerClientAnotherThreadCompleted = new ExEventLog.EventTuple(1073751840U, 10, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000BAB RID: 2987
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ActiveManagerWCFCleanup = new ExEventLog.EventTuple(1073751841U, 10, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x020001AC RID: 428
		private enum Category : short
		{
			// Token: 0x04000BAD RID: 2989
			Xtc = 1,
			// Token: 0x04000BAE RID: 2990
			Audit,
			// Token: 0x04000BAF RID: 2991
			Discovery,
			// Token: 0x04000BB0 RID: 2992
			Information_Rights_Management,
			// Token: 0x04000BB1 RID: 2993
			CopyOnWrite,
			// Token: 0x04000BB2 RID: 2994
			ResourceHealth,
			// Token: 0x04000BB3 RID: 2995
			PFRule,
			// Token: 0x04000BB4 RID: 2996
			ServiceDiscovery = 9,
			// Token: 0x04000BB5 RID: 2997
			Provider
		}

		// Token: 0x020001AD RID: 429
		internal enum Message : uint
		{
			// Token: 0x04000BB7 RID: 2999
			ExternalAuthDisabledAutoDiscover = 3221226473U,
			// Token: 0x04000BB8 RID: 3000
			AutoDiscoverFailedToAquireSecurityToken,
			// Token: 0x04000BB9 RID: 3001
			AutoDiscoverFailed,
			// Token: 0x04000BBA RID: 3002
			ExternalAuthDisabledExchangePrincipal,
			// Token: 0x04000BBB RID: 3003
			XtcInvalidSmtpAddress,
			// Token: 0x04000BBC RID: 3004
			XtcOrgRelationshipMissing,
			// Token: 0x04000BBD RID: 3005
			XtcArchiveGuidMissing,
			// Token: 0x04000BBE RID: 3006
			XtcAutoDiscoverRequestFailed,
			// Token: 0x04000BBF RID: 3007
			XtcMapiError,
			// Token: 0x04000BC0 RID: 3008
			ExternalAuthDisabledMailboxSession,
			// Token: 0x04000BC1 RID: 3009
			ExternalArchiveDisabled,
			// Token: 0x04000BC2 RID: 3010
			FederatedMailboxMisconfigured,
			// Token: 0x04000BC3 RID: 3011
			AutoDiscoverFailedForSetting,
			// Token: 0x04000BC4 RID: 3012
			XtcOrgRelationshipArchiveDisabled,
			// Token: 0x04000BC5 RID: 3013
			XtcInvalidOrgRelationshipTargetAutodiscoverEpr,
			// Token: 0x04000BC6 RID: 3014
			XtcInvalidOrgRelationshipTargetApplicationUri,
			// Token: 0x04000BC7 RID: 3015
			ErrorSavingMailboxAudit = 3221227473U,
			// Token: 0x04000BC8 RID: 3016
			ErrorSavingLastAccessTime,
			// Token: 0x04000BC9 RID: 3017
			ErrorReadingBypassAudit,
			// Token: 0x04000BCA RID: 3018
			ErrorResolvingLogonUser,
			// Token: 0x04000BCB RID: 3019
			ErrorOpeningParticipantSession = 2147485653U,
			// Token: 0x04000BCC RID: 3020
			ErrorBindingMessageItem,
			// Token: 0x04000BCD RID: 3021
			ErrorBindingFolderForFolderBindHistory,
			// Token: 0x04000BCE RID: 3022
			ErrorSavingFolderBindHistory,
			// Token: 0x04000BCF RID: 3023
			ErrorResolvingFromAddress,
			// Token: 0x04000BD0 RID: 3024
			ErrorLoadAuditPolicyConfiguration = 3221227482U,
			// Token: 0x04000BD1 RID: 3025
			SearchObjectSaved = 1074006969U,
			// Token: 0x04000BD2 RID: 3026
			SearchStatusSaved,
			// Token: 0x04000BD3 RID: 3027
			SearchStatusError = 3221490619U,
			// Token: 0x04000BD4 RID: 3028
			DiscoveryAndHoldSaved = 1074006972U,
			// Token: 0x04000BD5 RID: 3029
			DiscoverySearchStartRequested,
			// Token: 0x04000BD6 RID: 3030
			DiscoverySearchStopRequested,
			// Token: 0x04000BD7 RID: 3031
			DiscoverySearchRemoveRequested,
			// Token: 0x04000BD8 RID: 3032
			InPlaceHoldSettingsSynchronized,
			// Token: 0x04000BD9 RID: 3033
			SynchronizeInPlaceHoldError = 3221490625U,
			// Token: 0x04000BDA RID: 3034
			DiscoverySearchRequestPickedUp = 1074006978U,
			// Token: 0x04000BDB RID: 3035
			DiscoverySearchStartRequestProcessed,
			// Token: 0x04000BDC RID: 3036
			DiscoverySearchStopRequestProcessed,
			// Token: 0x04000BDD RID: 3037
			DiscoverySearchRemoveRequestProcessed,
			// Token: 0x04000BDE RID: 3038
			DiscoverySearchTaskError = 3221490630U,
			// Token: 0x04000BDF RID: 3039
			DiscoverySearchTaskStarted = 1074006983U,
			// Token: 0x04000BE0 RID: 3040
			DiscoverySearchTaskCompleted,
			// Token: 0x04000BE1 RID: 3041
			DiscoverySearchServerError = 3221490633U,
			// Token: 0x04000BE2 RID: 3042
			DiscoverySearchStatusChanged = 1074006986U,
			// Token: 0x04000BE3 RID: 3043
			FailedToSyncDiscoveryHoldToExchangeOnline = 3221490635U,
			// Token: 0x04000BE4 RID: 3044
			SyncDiscoveryHoldToExchangeOnlineStart = 1074006988U,
			// Token: 0x04000BE5 RID: 3045
			SyncDiscoveryHoldToExchangeOnlineDetails,
			// Token: 0x04000BE6 RID: 3046
			SingleFailureSyncDiscoveryHoldToExchangeOnline = 3221490638U,
			// Token: 0x04000BE7 RID: 3047
			DiscoverySearchWorkItemQueueChanged = 1074006991U,
			// Token: 0x04000BE8 RID: 3048
			DiscoverySearchWorkItemQueueNotProcessed,
			// Token: 0x04000BE9 RID: 3049
			DiscoverySearchRpcServerRestarted,
			// Token: 0x04000BEA RID: 3050
			RecipientDLExpansionIsProcessing = 1074007069U,
			// Token: 0x04000BEB RID: 3051
			RecipientDLExpansionFailed = 3221490718U,
			// Token: 0x04000BEC RID: 3052
			RecipientDLExpansionMessageAlreadyProcessed = 1074007071U,
			// Token: 0x04000BED RID: 3053
			RecipientDLExpansionMessageNoDLRecipients,
			// Token: 0x04000BEE RID: 3054
			RecipientDLExpansionMessageNoLongerExist,
			// Token: 0x04000BEF RID: 3055
			RecipientDLExpansionMismatchResults = 3221490722U,
			// Token: 0x04000BF0 RID: 3056
			RecipientDLExpansionMaxNestedDLsLimit,
			// Token: 0x04000BF1 RID: 3057
			RecipientDLExpansionMaxRecipientsLimit,
			// Token: 0x04000BF2 RID: 3058
			RecipientDLExpansionSkipped = 1074007077U,
			// Token: 0x04000BF3 RID: 3059
			RecipientDLExpansionUpdateItemInDumpster,
			// Token: 0x04000BF4 RID: 3060
			UnknownTemplateInPublishingLicense = 3221229472U,
			// Token: 0x04000BF5 RID: 3061
			ErrorMultipleSaveOperationFailed = 2147488649U,
			// Token: 0x04000BF6 RID: 3062
			ErrorCOWCacheWaitTimeout,
			// Token: 0x04000BF7 RID: 3063
			COWCalendarLoggingDisabled,
			// Token: 0x04000BF8 RID: 3064
			COWCalendarLoggingStopped,
			// Token: 0x04000BF9 RID: 3065
			ErrorDatabasePingTimedOut = 2147489650U,
			// Token: 0x04000BFA RID: 3066
			PFRuleConfigGetLocalIPFailure = 3221494617U,
			// Token: 0x04000BFB RID: 3067
			PFRuleSettingFromAddressFailure = 2147752794U,
			// Token: 0x04000BFC RID: 3068
			PopulatedServiceTopology = 1073750824U,
			// Token: 0x04000BFD RID: 3069
			ErrorPopulatingServiceTopology = 3221234473U,
			// Token: 0x04000BFE RID: 3070
			RegisteredForTopologyChangedNotification = 1073750826U,
			// Token: 0x04000BFF RID: 3071
			ErrorRegisteringForTopologyChangedNotification = 3221234475U,
			// Token: 0x04000C00 RID: 3072
			ErrorCreateNotification = 3221235473U,
			// Token: 0x04000C01 RID: 3073
			ErrorUpdateNotification,
			// Token: 0x04000C02 RID: 3074
			ErrorRemoveNotification,
			// Token: 0x04000C03 RID: 3075
			ErrorDiscoverEwsUrlForMailbox,
			// Token: 0x04000C04 RID: 3076
			ErrorSendNotificationEmail,
			// Token: 0x04000C05 RID: 3077
			ErrorActiveManagerClientADTimeout,
			// Token: 0x04000C06 RID: 3078
			ErrorActiveManagerClientADError,
			// Token: 0x04000C07 RID: 3079
			DkmDecryptionFailure,
			// Token: 0x04000C08 RID: 3080
			RsaCapiKeyImportFailure,
			// Token: 0x04000C09 RID: 3081
			ErrorCheckReplicationFlushedDatabaseNotFound,
			// Token: 0x04000C0A RID: 3082
			ErrorCheckReplicationFlushed,
			// Token: 0x04000C0B RID: 3083
			ErrorCheckReplicationThrottlingDatabaseNotFound,
			// Token: 0x04000C0C RID: 3084
			ErrorCheckReplicationThrottling,
			// Token: 0x04000C0D RID: 3085
			ActiveManagerClientAnotherThreadInADCall = 1073751838U,
			// Token: 0x04000C0E RID: 3086
			ActiveManagerClientAnotherThreadInADCallTimeout = 2147493663U,
			// Token: 0x04000C0F RID: 3087
			ActiveManagerClientAnotherThreadCompleted = 1073751840U,
			// Token: 0x04000C10 RID: 3088
			ActiveManagerWCFCleanup
		}
	}
}
