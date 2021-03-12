using System;
using System.Diagnostics;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Management.EventMessages
{
	// Token: 0x02001242 RID: 4674
	internal static class ManagementEventLogConstants
	{
		// Token: 0x04006656 RID: 26198
		public const string EventSource = "MSExchange Management Application";

		// Token: 0x04006657 RID: 26199
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_RecipientsUpdateForAddressBookCancelled = new ExEventLog.EventTuple(263144U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04006658 RID: 26200
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_RecipientsUpdateForEmailAddressPolicyCancelled = new ExEventLog.EventTuple(263145U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04006659 RID: 26201
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_DoNotApplyAddressBook = new ExEventLog.EventTuple(264144U, 2, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400665A RID: 26202
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ApplyAddressBookCancelled = new ExEventLog.EventTuple(264145U, 2, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400665B RID: 26203
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_DoNotApplyEmailAddressPolicy = new ExEventLog.EventTuple(264146U, 2, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400665C RID: 26204
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ApplyEmailAddressPolicyCancelled = new ExEventLog.EventTuple(264147U, 2, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400665D RID: 26205
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_TransportServerCmdletsDeprecated = new ExEventLog.EventTuple(264148U, 2, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400665E RID: 26206
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_UpdateProxyGenerationDllFailed = new ExEventLog.EventTuple(265144U, 3, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400665F RID: 26207
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_GenerateProxyAddressFailed = new ExEventLog.EventTuple(265145U, 3, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04006660 RID: 26208
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_CopyProxyGeneratinDllFailed = new ExEventLog.EventTuple(265146U, 3, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04006661 RID: 26209
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_LoadBalancingFailedToFindDatabase = new ExEventLog.EventTuple(3221490619U, 3, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04006662 RID: 26210
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ScriptExecutionSuccessfully = new ExEventLog.EventTuple(4000U, 4, EventLogEntryType.Information, ExEventLog.EventLevel.High, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04006663 RID: 26211
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ScriptExecutionFailed = new ExEventLog.EventTuple(3221229473U, 4, EventLogEntryType.Error, ExEventLog.EventLevel.Low, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04006664 RID: 26212
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ComponentTaskExecutedSuccessfully = new ExEventLog.EventTuple(4002U, 4, EventLogEntryType.Information, ExEventLog.EventLevel.High, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04006665 RID: 26213
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ComponentTaskFailed = new ExEventLog.EventTuple(3221229475U, 4, EventLogEntryType.Error, ExEventLog.EventLevel.Low, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04006666 RID: 26214
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ExecuteTaskScriptOptic = new ExEventLog.EventTuple(4005U, 4, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04006667 RID: 26215
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_FailedToLog = new ExEventLog.EventTuple(3221230472U, 5, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04006668 RID: 26216
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_FailedToInitEwsMailer = new ExEventLog.EventTuple(3221230473U, 5, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04006669 RID: 26217
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_NoAdminAuditLogConfig = new ExEventLog.EventTuple(2147488650U, 5, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400666A RID: 26218
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_AdminLogFull = new ExEventLog.EventTuple(3221230475U, 5, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400666B RID: 26219
		[EventLogPeriod(Period = "LogOneTime")]
		public static readonly ExEventLog.EventTuple Tuple_MultipleAdminAuditLogConfig = new ExEventLog.EventTuple(3221230476U, 5, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogOneTime);

		// Token: 0x0400666C RID: 26220
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_FailedToConnectToWLCD = new ExEventLog.EventTuple(3221231472U, 6, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400666D RID: 26221
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_FailedToResolveWLCDHost = new ExEventLog.EventTuple(3221231473U, 6, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400666E RID: 26222
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_WLCDRefusingConnection = new ExEventLog.EventTuple(3221231474U, 6, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400666F RID: 26223
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_UnknownErrorCommunicatingWithWLCD = new ExEventLog.EventTuple(3221231475U, 6, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04006670 RID: 26224
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_DomainInvalidStateWithWLCD = new ExEventLog.EventTuple(6004U, 6, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04006671 RID: 26225
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_FailedToAddToUSG = new ExEventLog.EventTuple(7001U, 7, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04006672 RID: 26226
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_FailedToRemoveFromUSG = new ExEventLog.EventTuple(7002U, 7, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04006673 RID: 26227
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_FailedToFindUSG = new ExEventLog.EventTuple(7003U, 7, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04006674 RID: 26228
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SupportedToolsInformationFileMissing = new ExEventLog.EventTuple(3221233473U, 8, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04006675 RID: 26229
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SupportedToolsInformationDataFileCorupted = new ExEventLog.EventTuple(3221233474U, 8, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04006676 RID: 26230
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SupportedToolsInformationDataFileInconsistent = new ExEventLog.EventTuple(3221233475U, 8, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04006677 RID: 26231
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_MigrationServiceConnectionError = new ExEventLog.EventTuple(3221234473U, 9, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04006678 RID: 26232
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_NewMailboxAttempts = new ExEventLog.EventTuple(3221235472U, 10, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04006679 RID: 26233
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_NewMailboxIterationAttempts = new ExEventLog.EventTuple(3221235473U, 10, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400667A RID: 26234
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_NewOrganizationAttempts = new ExEventLog.EventTuple(3221235474U, 10, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400667B RID: 26235
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_NewOrganizationIterationAttempts = new ExEventLog.EventTuple(3221235475U, 10, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400667C RID: 26236
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_RemoveOrganizationAttempts = new ExEventLog.EventTuple(3221235476U, 10, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400667D RID: 26237
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_RemoveOrganizationIterationAttempts = new ExEventLog.EventTuple(3221235477U, 10, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400667E RID: 26238
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_AddSecondaryDomainAttempts = new ExEventLog.EventTuple(3221235478U, 10, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400667F RID: 26239
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_AddSecondaryDomainIterationAttempts = new ExEventLog.EventTuple(3221235479U, 10, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04006680 RID: 26240
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_RemoveSecondaryDomainAttempts = new ExEventLog.EventTuple(3221235480U, 10, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04006681 RID: 26241
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_RemoveSecondaryDomainIterationAttempts = new ExEventLog.EventTuple(3221235481U, 10, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04006682 RID: 26242
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_GetManagementEndpointAttempts = new ExEventLog.EventTuple(3221235482U, 10, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04006683 RID: 26243
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_GetManagementEndpointIterationAttempts = new ExEventLog.EventTuple(3221235483U, 10, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04006684 RID: 26244
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_CmdletAttempts = new ExEventLog.EventTuple(3221235484U, 10, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04006685 RID: 26245
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_CmdletIterationAttempts = new ExEventLog.EventTuple(3221235485U, 10, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04006686 RID: 26246
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_TenantMonitoringSuccess = new ExEventLog.EventTuple(10014U, 10, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04006687 RID: 26247
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_InvalidStatusOrganizationFailure = new ExEventLog.EventTuple(2147493663U, 10, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04006688 RID: 26248
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_InvalidStatusOrganizationSuccess = new ExEventLog.EventTuple(10016U, 10, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04006689 RID: 26249
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_InstanceAboveThreshold = new ExEventLog.EventTuple(10017U, 10, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400668A RID: 26250
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_IgnoringInstanceData = new ExEventLog.EventTuple(2147493666U, 10, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400668B RID: 26251
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_InvalidMerveEntriesError = new ExEventLog.EventTuple(2147493668U, 10, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400668C RID: 26252
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_InvalidMerveEntriesSuccess = new ExEventLog.EventTuple(10021U, 10, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400668D RID: 26253
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_FailToRetrieveErrorDetails = new ExEventLog.EventTuple(2147493667U, 10, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400668E RID: 26254
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ProvisioningReconciliationFailure = new ExEventLog.EventTuple(3221240473U, 15, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400668F RID: 26255
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_FswChangedToPrimary = new ExEventLog.EventTuple(273145U, 11, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04006690 RID: 26256
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_FswChangedToAlternate = new ExEventLog.EventTuple(273146U, 11, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04006691 RID: 26257
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_BackSyncTooManyObjectReadRestarts = new ExEventLog.EventTuple(2147495649U, 12, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04006692 RID: 26258
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_BackSyncExcludeFromBackSync = new ExEventLog.EventTuple(3221237474U, 12, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04006693 RID: 26259
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_BackSyncFullSyncFailbackDetected = new ExEventLog.EventTuple(3221237475U, 12, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04006694 RID: 26260
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_BackSyncExceptionCaught = new ExEventLog.EventTuple(3221237476U, 12, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04006695 RID: 26261
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_RequiredServiceNotRunning = new ExEventLog.EventTuple(3221238473U, 13, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04006696 RID: 26262
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_AppPoolNotRunning = new ExEventLog.EventTuple(3221238474U, 13, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04006697 RID: 26263
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_TransientErrorCacheInsertEntry = new ExEventLog.EventTuple(2147496651U, 13, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04006698 RID: 26264
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_TransientErrorCacheRemoveEntry = new ExEventLog.EventTuple(2147496652U, 13, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04006699 RID: 26265
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_TransientErrorCacheFindEntry = new ExEventLog.EventTuple(2147496653U, 13, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400669A RID: 26266
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_RollAsaPwdStarting = new ExEventLog.EventTuple(1073755825U, 14, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400669B RID: 26267
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_RollAsaPwdFinishedSuccess = new ExEventLog.EventTuple(14002U, 14, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400669C RID: 26268
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_RollAsaPwdFinishedWithWarning = new ExEventLog.EventTuple(14003U, 14, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400669D RID: 26269
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_RollAsaPwdFinishedFailure = new ExEventLog.EventTuple(3221239476U, 14, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400669E RID: 26270
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_UpgradeOrchestratorSucceeded = new ExEventLog.EventTuple(16001U, 16, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400669F RID: 26271
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_UpgradeOrchestratorFailed = new ExEventLog.EventTuple(3221241474U, 16, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040066A0 RID: 26272
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_DataMartConnectionFailed = new ExEventLog.EventTuple(3221242473U, 17, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040066A1 RID: 26273
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_DataMartConfigurationError = new ExEventLog.EventTuple(3221242474U, 17, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040066A2 RID: 26274
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_DkmProvisioningException = new ExEventLog.EventTuple(3221242475U, 17, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040066A3 RID: 26275
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ImportTpdFailure = new ExEventLog.EventTuple(3221242476U, 17, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040066A4 RID: 26276
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_DkmProvisioningSuccessful = new ExEventLog.EventTuple(17005U, 17, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040066A5 RID: 26277
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SkipHSMEncryptedTpd = new ExEventLog.EventTuple(2147500654U, 17, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040066A6 RID: 26278
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_TenantConfigurationIsNull = new ExEventLog.EventTuple(2147500655U, 17, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040066A7 RID: 26279
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_DataMartConnectionFailOverToBackupServer = new ExEventLog.EventTuple(2147500656U, 17, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040066A8 RID: 26280
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_CorruptClassificationRuleCollection = new ExEventLog.EventTuple(2147763793U, 18, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040066A9 RID: 26281
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_DuplicateDataClassificationIdAcrossRulePack = new ExEventLog.EventTuple(2147763794U, 18, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040066AA RID: 26282
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ClassificationEngineFailure = new ExEventLog.EventTuple(3221505619U, 18, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040066AB RID: 26283
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ClassificationEngineTimeout = new ExEventLog.EventTuple(3221505620U, 18, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040066AC RID: 26284
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ClassificationEngineConfigurationError = new ExEventLog.EventTuple(2147763797U, 18, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040066AD RID: 26285
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_FfoReportingTaskFailure = new ExEventLog.EventTuple(3221506617U, 19, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040066AE RID: 26286
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_FfoReportingRecipientTaskFailure = new ExEventLog.EventTuple(3221506618U, 19, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040066AF RID: 26287
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_FailedToUploadPhoto = new ExEventLog.EventTuple(3221507617U, 20, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040066B0 RID: 26288
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_FailedToRetrievePhoto = new ExEventLog.EventTuple(3221507618U, 20, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040066B1 RID: 26289
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_FailedToRemovePhoto = new ExEventLog.EventTuple(3221507619U, 20, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040066B2 RID: 26290
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ReducedDynamicRangeSuccess = new ExEventLog.EventTuple(1074014971U, 11, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040066B3 RID: 26291
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ReducedDynamicRangeFailure = new ExEventLog.EventTuple(3221498620U, 11, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040066B4 RID: 26292
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_FailedToGetCurrentDynamicRange = new ExEventLog.EventTuple(3221498621U, 11, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040066B5 RID: 26293
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_CurrentDynamicRange = new ExEventLog.EventTuple(1074014974U, 11, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x02001243 RID: 4675
		private enum Category : short
		{
			// Token: 0x040066B7 RID: 26295
			Shell = 1,
			// Token: 0x040066B8 RID: 26296
			Console,
			// Token: 0x040066B9 RID: 26297
			ProvisioningAgent,
			// Token: 0x040066BA RID: 26298
			ComponentInfoBasedTask,
			// Token: 0x040066BB RID: 26299
			AdminAuditLog,
			// Token: 0x040066BC RID: 26300
			DatacenterProvisioningAgent,
			// Token: 0x040066BD RID: 26301
			MailboxTaskHelper,
			// Token: 0x040066BE RID: 26302
			SupportedToolsInformation,
			// Token: 0x040066BF RID: 26303
			Migration,
			// Token: 0x040066C0 RID: 26304
			TenantMonitoring,
			// Token: 0x040066C1 RID: 26305
			DatabaseAvailabilityGroupManagement,
			// Token: 0x040066C2 RID: 26306
			BackSync,
			// Token: 0x040066C3 RID: 26307
			MonitoringTask,
			// Token: 0x040066C4 RID: 26308
			Kerberos,
			// Token: 0x040066C5 RID: 26309
			ProvisioningReconciliation,
			// Token: 0x040066C6 RID: 26310
			UpgradeOrchestrator,
			// Token: 0x040066C7 RID: 26311
			Reporting,
			// Token: 0x040066C8 RID: 26312
			Dlp,
			// Token: 0x040066C9 RID: 26313
			FfoReporting,
			// Token: 0x040066CA RID: 26314
			Photos
		}

		// Token: 0x02001244 RID: 4676
		internal enum Message : uint
		{
			// Token: 0x040066CC RID: 26316
			RecipientsUpdateForAddressBookCancelled = 263144U,
			// Token: 0x040066CD RID: 26317
			RecipientsUpdateForEmailAddressPolicyCancelled,
			// Token: 0x040066CE RID: 26318
			DoNotApplyAddressBook = 264144U,
			// Token: 0x040066CF RID: 26319
			ApplyAddressBookCancelled,
			// Token: 0x040066D0 RID: 26320
			DoNotApplyEmailAddressPolicy,
			// Token: 0x040066D1 RID: 26321
			ApplyEmailAddressPolicyCancelled,
			// Token: 0x040066D2 RID: 26322
			TransportServerCmdletsDeprecated,
			// Token: 0x040066D3 RID: 26323
			UpdateProxyGenerationDllFailed = 265144U,
			// Token: 0x040066D4 RID: 26324
			GenerateProxyAddressFailed,
			// Token: 0x040066D5 RID: 26325
			CopyProxyGeneratinDllFailed,
			// Token: 0x040066D6 RID: 26326
			LoadBalancingFailedToFindDatabase = 3221490619U,
			// Token: 0x040066D7 RID: 26327
			ScriptExecutionSuccessfully = 4000U,
			// Token: 0x040066D8 RID: 26328
			ScriptExecutionFailed = 3221229473U,
			// Token: 0x040066D9 RID: 26329
			ComponentTaskExecutedSuccessfully = 4002U,
			// Token: 0x040066DA RID: 26330
			ComponentTaskFailed = 3221229475U,
			// Token: 0x040066DB RID: 26331
			ExecuteTaskScriptOptic = 4005U,
			// Token: 0x040066DC RID: 26332
			FailedToLog = 3221230472U,
			// Token: 0x040066DD RID: 26333
			FailedToInitEwsMailer,
			// Token: 0x040066DE RID: 26334
			NoAdminAuditLogConfig = 2147488650U,
			// Token: 0x040066DF RID: 26335
			AdminLogFull = 3221230475U,
			// Token: 0x040066E0 RID: 26336
			MultipleAdminAuditLogConfig,
			// Token: 0x040066E1 RID: 26337
			FailedToConnectToWLCD = 3221231472U,
			// Token: 0x040066E2 RID: 26338
			FailedToResolveWLCDHost,
			// Token: 0x040066E3 RID: 26339
			WLCDRefusingConnection,
			// Token: 0x040066E4 RID: 26340
			UnknownErrorCommunicatingWithWLCD,
			// Token: 0x040066E5 RID: 26341
			DomainInvalidStateWithWLCD = 6004U,
			// Token: 0x040066E6 RID: 26342
			FailedToAddToUSG = 7001U,
			// Token: 0x040066E7 RID: 26343
			FailedToRemoveFromUSG,
			// Token: 0x040066E8 RID: 26344
			FailedToFindUSG,
			// Token: 0x040066E9 RID: 26345
			SupportedToolsInformationFileMissing = 3221233473U,
			// Token: 0x040066EA RID: 26346
			SupportedToolsInformationDataFileCorupted,
			// Token: 0x040066EB RID: 26347
			SupportedToolsInformationDataFileInconsistent,
			// Token: 0x040066EC RID: 26348
			MigrationServiceConnectionError = 3221234473U,
			// Token: 0x040066ED RID: 26349
			NewMailboxAttempts = 3221235472U,
			// Token: 0x040066EE RID: 26350
			NewMailboxIterationAttempts,
			// Token: 0x040066EF RID: 26351
			NewOrganizationAttempts,
			// Token: 0x040066F0 RID: 26352
			NewOrganizationIterationAttempts,
			// Token: 0x040066F1 RID: 26353
			RemoveOrganizationAttempts,
			// Token: 0x040066F2 RID: 26354
			RemoveOrganizationIterationAttempts,
			// Token: 0x040066F3 RID: 26355
			AddSecondaryDomainAttempts,
			// Token: 0x040066F4 RID: 26356
			AddSecondaryDomainIterationAttempts,
			// Token: 0x040066F5 RID: 26357
			RemoveSecondaryDomainAttempts,
			// Token: 0x040066F6 RID: 26358
			RemoveSecondaryDomainIterationAttempts,
			// Token: 0x040066F7 RID: 26359
			GetManagementEndpointAttempts,
			// Token: 0x040066F8 RID: 26360
			GetManagementEndpointIterationAttempts,
			// Token: 0x040066F9 RID: 26361
			CmdletAttempts,
			// Token: 0x040066FA RID: 26362
			CmdletIterationAttempts,
			// Token: 0x040066FB RID: 26363
			TenantMonitoringSuccess = 10014U,
			// Token: 0x040066FC RID: 26364
			InvalidStatusOrganizationFailure = 2147493663U,
			// Token: 0x040066FD RID: 26365
			InvalidStatusOrganizationSuccess = 10016U,
			// Token: 0x040066FE RID: 26366
			InstanceAboveThreshold,
			// Token: 0x040066FF RID: 26367
			IgnoringInstanceData = 2147493666U,
			// Token: 0x04006700 RID: 26368
			InvalidMerveEntriesError = 2147493668U,
			// Token: 0x04006701 RID: 26369
			InvalidMerveEntriesSuccess = 10021U,
			// Token: 0x04006702 RID: 26370
			FailToRetrieveErrorDetails = 2147493667U,
			// Token: 0x04006703 RID: 26371
			ProvisioningReconciliationFailure = 3221240473U,
			// Token: 0x04006704 RID: 26372
			FswChangedToPrimary = 273145U,
			// Token: 0x04006705 RID: 26373
			FswChangedToAlternate,
			// Token: 0x04006706 RID: 26374
			BackSyncTooManyObjectReadRestarts = 2147495649U,
			// Token: 0x04006707 RID: 26375
			BackSyncExcludeFromBackSync = 3221237474U,
			// Token: 0x04006708 RID: 26376
			BackSyncFullSyncFailbackDetected,
			// Token: 0x04006709 RID: 26377
			BackSyncExceptionCaught,
			// Token: 0x0400670A RID: 26378
			RequiredServiceNotRunning = 3221238473U,
			// Token: 0x0400670B RID: 26379
			AppPoolNotRunning,
			// Token: 0x0400670C RID: 26380
			TransientErrorCacheInsertEntry = 2147496651U,
			// Token: 0x0400670D RID: 26381
			TransientErrorCacheRemoveEntry,
			// Token: 0x0400670E RID: 26382
			TransientErrorCacheFindEntry,
			// Token: 0x0400670F RID: 26383
			RollAsaPwdStarting = 1073755825U,
			// Token: 0x04006710 RID: 26384
			RollAsaPwdFinishedSuccess = 14002U,
			// Token: 0x04006711 RID: 26385
			RollAsaPwdFinishedWithWarning,
			// Token: 0x04006712 RID: 26386
			RollAsaPwdFinishedFailure = 3221239476U,
			// Token: 0x04006713 RID: 26387
			UpgradeOrchestratorSucceeded = 16001U,
			// Token: 0x04006714 RID: 26388
			UpgradeOrchestratorFailed = 3221241474U,
			// Token: 0x04006715 RID: 26389
			DataMartConnectionFailed = 3221242473U,
			// Token: 0x04006716 RID: 26390
			DataMartConfigurationError,
			// Token: 0x04006717 RID: 26391
			DkmProvisioningException,
			// Token: 0x04006718 RID: 26392
			ImportTpdFailure,
			// Token: 0x04006719 RID: 26393
			DkmProvisioningSuccessful = 17005U,
			// Token: 0x0400671A RID: 26394
			SkipHSMEncryptedTpd = 2147500654U,
			// Token: 0x0400671B RID: 26395
			TenantConfigurationIsNull,
			// Token: 0x0400671C RID: 26396
			DataMartConnectionFailOverToBackupServer,
			// Token: 0x0400671D RID: 26397
			CorruptClassificationRuleCollection = 2147763793U,
			// Token: 0x0400671E RID: 26398
			DuplicateDataClassificationIdAcrossRulePack,
			// Token: 0x0400671F RID: 26399
			ClassificationEngineFailure = 3221505619U,
			// Token: 0x04006720 RID: 26400
			ClassificationEngineTimeout,
			// Token: 0x04006721 RID: 26401
			ClassificationEngineConfigurationError = 2147763797U,
			// Token: 0x04006722 RID: 26402
			FfoReportingTaskFailure = 3221506617U,
			// Token: 0x04006723 RID: 26403
			FfoReportingRecipientTaskFailure,
			// Token: 0x04006724 RID: 26404
			FailedToUploadPhoto = 3221507617U,
			// Token: 0x04006725 RID: 26405
			FailedToRetrievePhoto,
			// Token: 0x04006726 RID: 26406
			FailedToRemovePhoto,
			// Token: 0x04006727 RID: 26407
			ReducedDynamicRangeSuccess = 1074014971U,
			// Token: 0x04006728 RID: 26408
			ReducedDynamicRangeFailure = 3221498620U,
			// Token: 0x04006729 RID: 26409
			FailedToGetCurrentDynamicRange,
			// Token: 0x0400672A RID: 26410
			CurrentDynamicRange = 1074014974U
		}
	}
}
