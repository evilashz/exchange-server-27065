using System;
using System.Diagnostics;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.InfoWorker.EventLog
{
	// Token: 0x02000327 RID: 807
	public static class InfoWorkerEventLogConstants
	{
		// Token: 0x04001143 RID: 4419
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ServiceStarted = new ExEventLog.EventTuple(1074004969U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04001144 RID: 4420
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ServiceStopped = new ExEventLog.EventTuple(1074004970U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04001145 RID: 4421
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ServiceFailedToStart = new ExEventLog.EventTuple(3221488619U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04001146 RID: 4422
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ServiceStarting = new ExEventLog.EventTuple(1074004972U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04001147 RID: 4423
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ServiceStopping = new ExEventLog.EventTuple(1074004973U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04001148 RID: 4424
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ServiceOutOfMemory = new ExEventLog.EventTuple(3221488622U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04001149 RID: 4425
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_CreatingAssistant = new ExEventLog.EventTuple(1074004975U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400114A RID: 4426
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_FailCreateAssistant = new ExEventLog.EventTuple(3221488624U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400114B RID: 4427
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_DisabledAssistant = new ExEventLog.EventTuple(1074004977U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400114C RID: 4428
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_OOFConfigNotAccessible = new ExEventLog.EventTuple(3221490617U, 3, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0400114D RID: 4429
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_OOFInvalidScheduleLine = new ExEventLog.EventTuple(3221490618U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Expert, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0400114E RID: 4430
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_OOFTooManyContacts = new ExEventLog.EventTuple(3221490619U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0400114F RID: 4431
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_OOFRulesQuotaExceeded = new ExEventLog.EventTuple(3221490620U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04001150 RID: 4432
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_OOFHistoryMapiPermanentException = new ExEventLog.EventTuple(3221490621U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04001151 RID: 4433
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_OOFUnableToReadScheduleCache = new ExEventLog.EventTuple(3221490622U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04001152 RID: 4434
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_AutoDiscoverFailed = new ExEventLog.EventTuple(3221491617U, 4, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04001153 RID: 4435
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_ProxyWebRequestFailed = new ExEventLog.EventTuple(3221491618U, 4, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04001154 RID: 4436
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_PublicFolderRequestFailed = new ExEventLog.EventTuple(3221491619U, 4, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04001155 RID: 4437
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_PublicFolderServerNotFoundForOU = new ExEventLog.EventTuple(3221491620U, 4, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04001156 RID: 4438
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_LocalForestConfigurationNotFound = new ExEventLog.EventTuple(3221491621U, 4, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04001157 RID: 4439
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_MailboxLogonFailed = new ExEventLog.EventTuple(3221491625U, 4, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04001158 RID: 4440
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_WebRequestFailedSecurityChecks = new ExEventLog.EventTuple(3221491626U, 6, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04001159 RID: 4441
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_InvalidCredentialsForCrossForestProxying = new ExEventLog.EventTuple(3221491628U, 5, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400115A RID: 4442
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_CalendarQueryFailed = new ExEventLog.EventTuple(3221491629U, 4, EventLogEntryType.Error, ExEventLog.EventLevel.Medium, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400115B RID: 4443
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_LogonAsNetworkServiceFailed = new ExEventLog.EventTuple(3221491632U, 4, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400115C RID: 4444
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_NoCASFoundForRequest = new ExEventLog.EventTuple(3221491633U, 4, EventLogEntryType.Error, ExEventLog.EventLevel.Low, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400115D RID: 4445
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_CASDiscoveryExceptionHandled = new ExEventLog.EventTuple(3221491634U, 4, EventLogEntryType.Error, ExEventLog.EventLevel.Low, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400115E RID: 4446
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_WorkingHoursFailed = new ExEventLog.EventTuple(3221491635U, 4, EventLogEntryType.Error, ExEventLog.EventLevel.Medium, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400115F RID: 4447
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_InvalidMaximumDatabasesInQuery = new ExEventLog.EventTuple(3221491637U, 4, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04001160 RID: 4448
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_InvalidMinimumDatabasesInQuery = new ExEventLog.EventTuple(3221491638U, 4, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04001161 RID: 4449
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_DuplicateAvailabilityAddressSpace = new ExEventLog.EventTuple(3221491639U, 5, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04001162 RID: 4450
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_CannotGetLocalSiteName = new ExEventLog.EventTuple(3221491640U, 5, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04001163 RID: 4451
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SCPCannotConnectToRemoteDirectory = new ExEventLog.EventTuple(3221491641U, 5, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04001164 RID: 4452
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SCPErrorSearchingLocalADForSCP = new ExEventLog.EventTuple(3221491642U, 5, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04001165 RID: 4453
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SCPErrorSearchingForRemoteSCP = new ExEventLog.EventTuple(3221491643U, 5, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04001166 RID: 4454
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SCPMisconfiguredLocalServiceBindings = new ExEventLog.EventTuple(3221491644U, 5, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04001167 RID: 4455
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SCPMisconfiguredRemoteServiceBindings = new ExEventLog.EventTuple(3221491645U, 5, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04001168 RID: 4456
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_MSExchangeSystemAccountRetrieval = new ExEventLog.EventTuple(3221491646U, 5, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04001169 RID: 4457
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_AAResourceBooked = new ExEventLog.EventTuple(1074011969U, 8, EventLogEntryType.Information, ExEventLog.EventLevel.Low, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400116A RID: 4458
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_AAResourceCanceled = new ExEventLog.EventTuple(1074011970U, 8, EventLogEntryType.Information, ExEventLog.EventLevel.Low, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400116B RID: 4459
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_CorruptCalendarConfiguration = new ExEventLog.EventTuple(3221495619U, 8, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400116C RID: 4460
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_RBAProcessedMeetingMessage = new ExEventLog.EventTuple(1074011972U, 8, EventLogEntryType.Information, ExEventLog.EventLevel.Low, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400116D RID: 4461
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_RBAProcessedMeetingCancelation = new ExEventLog.EventTuple(1074011973U, 8, EventLogEntryType.Information, ExEventLog.EventLevel.Low, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400116E RID: 4462
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_RBAValidationException = new ExEventLog.EventTuple(3221495622U, 8, EventLogEntryType.Error, ExEventLog.EventLevel.Low, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400116F RID: 4463
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_RBANeutralCultureEncountered = new ExEventLog.EventTuple(1074011975U, 8, EventLogEntryType.Information, ExEventLog.EventLevel.Low, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04001170 RID: 4464
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_RBANonUniqueLegacyDN = new ExEventLog.EventTuple(1074011976U, 8, EventLogEntryType.Information, ExEventLog.EventLevel.Low, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04001171 RID: 4465
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ProcessingMeetingMessageFailure = new ExEventLog.EventTuple(2147754793U, 9, EventLogEntryType.Warning, ExEventLog.EventLevel.High, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04001172 RID: 4466
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_InvalidElcDataInAD = new ExEventLog.EventTuple(3221497617U, 10, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04001173 RID: 4467
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_InvalidELCFolderChange = new ExEventLog.EventTuple(3221497618U, 10, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04001174 RID: 4468
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_InvalidServerADObject = new ExEventLog.EventTuple(3221497619U, 10, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04001175 RID: 4469
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_NullDestinationFolder = new ExEventLog.EventTuple(3221497620U, 10, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04001176 RID: 4470
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_DestinationFolderSameAsSource = new ExEventLog.EventTuple(3221497621U, 10, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04001177 RID: 4471
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_UnableToUpdateElcFolder = new ExEventLog.EventTuple(3221497622U, 10, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04001178 RID: 4472
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_CorruptionInADElcFolders = new ExEventLog.EventTuple(3221497623U, 10, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04001179 RID: 4473
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_FailedToCreateFolderHierarchy = new ExEventLog.EventTuple(3221497624U, 10, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400117A RID: 4474
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_FailedToReadAuditLogArgsFromAD = new ExEventLog.EventTuple(3221497625U, 10, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400117B RID: 4475
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_MissingAuditLogPath = new ExEventLog.EventTuple(3221497626U, 10, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400117C RID: 4476
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ConfigureAuditLogFailed = new ExEventLog.EventTuple(3221497627U, 10, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400117D RID: 4477
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_AppendAuditLogFailed = new ExEventLog.EventTuple(3221497628U, 10, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400117E RID: 4478
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ELCRootNameClash = new ExEventLog.EventTuple(3221497630U, 10, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400117F RID: 4479
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_CycleInPolicies = new ExEventLog.EventTuple(3221497631U, 10, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04001180 RID: 4480
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_InvalidRetentionAgeLimit = new ExEventLog.EventTuple(3221497632U, 10, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04001181 RID: 4481
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_CurrentFolderOnUnhandledException = new ExEventLog.EventTuple(3221497633U, 10, EventLogEntryType.Error, ExEventLog.EventLevel.Expert, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04001182 RID: 4482
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_InvalidTagDataInAD = new ExEventLog.EventTuple(3221497634U, 10, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04001183 RID: 4483
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_IncompleteRPTUpgrade = new ExEventLog.EventTuple(3221497635U, 10, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04001184 RID: 4484
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_CorruptionInADElcTags = new ExEventLog.EventTuple(3221497636U, 10, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04001185 RID: 4485
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_MRMSkippingMailbox = new ExEventLog.EventTuple(3221497637U, 10, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04001186 RID: 4486
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ArchiveOverWarningQuota = new ExEventLog.EventTuple(2147755814U, 10, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04001187 RID: 4487
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_DumpsterOverQuotaDeletedMails = new ExEventLog.EventTuple(2147755815U, 10, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04001188 RID: 4488
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_MRMSkippingFolder = new ExEventLog.EventTuple(3221497640U, 10, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04001189 RID: 4489
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ExpirationOfCurrentBatchFailed = new ExEventLog.EventTuple(2147755817U, 10, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400118A RID: 4490
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_FoldersWithOversizedItems = new ExEventLog.EventTuple(2147755818U, 10, EventLogEntryType.Warning, ExEventLog.EventLevel.Expert, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400118B RID: 4491
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_MRMExpirationStatistics = new ExEventLog.EventTuple(1074013995U, 10, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400118C RID: 4492
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_AdminAuditsQuotaWarning = new ExEventLog.EventTuple(2147755820U, 10, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400118D RID: 4493
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_DiscoveryHoldTransientErrorSkipMailbox = new ExEventLog.EventTuple(3221497645U, 10, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400118E RID: 4494
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_DiscoveryHoldPermanentErrorSkipMailbox = new ExEventLog.EventTuple(3221497646U, 10, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400118F RID: 4495
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_DiscoveryHoldSearchFailed = new ExEventLog.EventTuple(3221497647U, 10, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04001190 RID: 4496
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ExpirationOfMsgsInDiscoveryHoldsFolderFailed = new ExEventLog.EventTuple(2147755824U, 10, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04001191 RID: 4497
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_DiscoveryHoldsSkippedForTooManyQueries = new ExEventLog.EventTuple(1074014001U, 10, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04001192 RID: 4498
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_CorruptDiscoverySearchObject = new ExEventLog.EventTuple(3221497650U, 16, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04001193 RID: 4499
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_CorruptDiscoverySearchObjectId = new ExEventLog.EventTuple(3221497651U, 16, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04001194 RID: 4500
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_DiscoverySearchObjectNotFound = new ExEventLog.EventTuple(3221497652U, 16, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04001195 RID: 4501
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_DiscoverySearchObjectLoadError = new ExEventLog.EventTuple(3221497653U, 16, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04001196 RID: 4502
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_DiscoverySearchObjectNotFoundForOrg = new ExEventLog.EventTuple(3221497654U, 16, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04001197 RID: 4503
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_FailedToCopyDiscoverySearchToArchive = new ExEventLog.EventTuple(3221497655U, 16, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04001198 RID: 4504
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_CorruptDiscoverySearchObjectProperty = new ExEventLog.EventTuple(3221497656U, 16, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04001199 RID: 4505
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_DiscoverySearchObjectLoadErrorForMailbox = new ExEventLog.EventTuple(3221497657U, 16, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400119A RID: 4506
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_DumpsterOverQuotaDeletedAuditLogs = new ExEventLog.EventTuple(2147755834U, 10, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400119B RID: 4507
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_EhaMailboxQuotaWarning = new ExEventLog.EventTuple(2147755835U, 10, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400119C RID: 4508
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_FailedToCopyLitigationHoldDurationToArchive = new ExEventLog.EventTuple(3221497660U, 10, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400119D RID: 4509
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_FailedToReadLitigationHoldDurationFromPrimaryMailbox = new ExEventLog.EventTuple(3221497661U, 10, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400119E RID: 4510
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ELCFailedToLoadProcessEhaMigrationMessageSetting = new ExEventLog.EventTuple(1074014014U, 10, EventLogEntryType.Information, ExEventLog.EventLevel.Low, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400119F RID: 4511
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_FailedToCopyEhaMigrationFlagToArchive = new ExEventLog.EventTuple(3221497663U, 10, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040011A0 RID: 4512
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_HoldCleanupStatistics = new ExEventLog.EventTuple(1074014016U, 10, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040011A1 RID: 4513
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_DiscoveryFailedToFetchSizeInformation = new ExEventLog.EventTuple(3221497665U, 26, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040011A2 RID: 4514
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_DiscoveryServerLocatorTimeout = new ExEventLog.EventTuple(3221497666U, 26, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040011A3 RID: 4515
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_DiscoveryFanoutError = new ExEventLog.EventTuple(3221497667U, 26, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040011A4 RID: 4516
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_DiscoveryAutodiscoverError = new ExEventLog.EventTuple(3221497668U, 26, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040011A5 RID: 4517
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_DiscoveryFailedToGetOWAUrl = new ExEventLog.EventTuple(2147755858U, 26, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040011A6 RID: 4518
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_DiscoveryFailedToGetOWAService = new ExEventLog.EventTuple(2147755852U, 16, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040011A7 RID: 4519
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_DiscoveryFailedToGetOWAServiceWithException = new ExEventLog.EventTuple(2147755853U, 16, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040011A8 RID: 4520
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_DiscoverySearchCIFailure = new ExEventLog.EventTuple(3221497678U, 26, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040011A9 RID: 4521
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_DiscoverySearchFailure = new ExEventLog.EventTuple(3221497679U, 26, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040011AA RID: 4522
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_DiscoveryMailboxServerLocatorTime = new ExEventLog.EventTuple(1074014032U, 26, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040011AB RID: 4523
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ELCSkipProcessingMailboxTransientException = new ExEventLog.EventTuple(3221497681U, 10, EventLogEntryType.Error, ExEventLog.EventLevel.Low, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040011AC RID: 4524
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ELCSkipProcessingMailbox = new ExEventLog.EventTuple(1074014068U, 10, EventLogEntryType.Information, ExEventLog.EventLevel.Low, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040011AD RID: 4525
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_FailedToUpdateSafeLists = new ExEventLog.EventTuple(2147756792U, 11, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040011AE RID: 4526
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_FailedToEnsureJunkEmailRule = new ExEventLog.EventTuple(2147756798U, 11, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040011AF RID: 4527
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_FailedToUpdateMailbox = new ExEventLog.EventTuple(3221498624U, 11, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040011B0 RID: 4528
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_FailedToUpdateConversationOnDelete = new ExEventLog.EventTuple(2147757793U, 12, EventLogEntryType.Warning, ExEventLog.EventLevel.Low, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040011B1 RID: 4529
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_FailedToUpdateConversationOnQuotaExceeded = new ExEventLog.EventTuple(2147757794U, 12, EventLogEntryType.Warning, ExEventLog.EventLevel.Low, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040011B2 RID: 4530
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_FailedToUpdateConversationOnFolderDelete = new ExEventLog.EventTuple(2147757795U, 12, EventLogEntryType.Warning, ExEventLog.EventLevel.Low, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040011B3 RID: 4531
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_CorruptConversationActionItem = new ExEventLog.EventTuple(3221499620U, 12, EventLogEntryType.Error, ExEventLog.EventLevel.Expert, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040011B4 RID: 4532
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_TotalNumberOfItemsForBodyTagProcessing = new ExEventLog.EventTuple(1074015973U, 12, EventLogEntryType.Information, ExEventLog.EventLevel.Expert, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040011B5 RID: 4533
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_BodyTagProcessingFailed = new ExEventLog.EventTuple(3221499622U, 12, EventLogEntryType.Error, ExEventLog.EventLevel.Expert, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040011B6 RID: 4534
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_BodyTagProcessingSucceeded = new ExEventLog.EventTuple(1074015975U, 12, EventLogEntryType.Information, ExEventLog.EventLevel.Expert, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040011B7 RID: 4535
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_BodyTagProcessingSkipped = new ExEventLog.EventTuple(1074015976U, 12, EventLogEntryType.Information, ExEventLog.EventLevel.Expert, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040011B8 RID: 4536
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_BodyTagProcessingRequestQueued = new ExEventLog.EventTuple(1074015977U, 12, EventLogEntryType.Information, ExEventLog.EventLevel.Expert, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040011B9 RID: 4537
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_DeliveryToArbitrationMailboxExceededRateLimits = new ExEventLog.EventTuple(2147758793U, 13, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040011BA RID: 4538
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_UnableToReadConfigurationFromAD = new ExEventLog.EventTuple(3221501617U, 14, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040011BB RID: 4539
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_UnableToCreateGroupMetricsDirectory = new ExEventLog.EventTuple(3221501618U, 14, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040011BC RID: 4540
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_UnableToGetDomainList = new ExEventLog.EventTuple(3221501624U, 14, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040011BD RID: 4541
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_UnableToGetListOfChangedGroupsForDomain = new ExEventLog.EventTuple(3221501626U, 14, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040011BE RID: 4542
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_UnableToReadGroupMetricsCookie = new ExEventLog.EventTuple(2147759804U, 14, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040011BF RID: 4543
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_UnableToDeserializeGroupMetricsCookie = new ExEventLog.EventTuple(2147759805U, 14, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040011C0 RID: 4544
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_UnableToRemoveCorruptGroupMetricsCookie = new ExEventLog.EventTuple(3221501630U, 14, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040011C1 RID: 4545
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_UnableToSaveGroupMetricsCookie = new ExEventLog.EventTuple(3221501631U, 14, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040011C2 RID: 4546
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_UnableToOpenFile = new ExEventLog.EventTuple(3221501632U, 14, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040011C3 RID: 4547
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_GroupMetricsCookieExpired = new ExEventLog.EventTuple(3221501633U, 14, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040011C4 RID: 4548
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_UnableToReadChangedGroupList = new ExEventLog.EventTuple(3221501635U, 14, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040011C5 RID: 4549
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_GroupExpansionHaltedWarning = new ExEventLog.EventTuple(2147759812U, 14, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040011C6 RID: 4550
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_GroupExpansionHaltedError = new ExEventLog.EventTuple(3221501637U, 14, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040011C7 RID: 4551
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_UnableToRegisterChangeNotification = new ExEventLog.EventTuple(2147759817U, 14, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040011C8 RID: 4552
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_UnableToFindPublicFolderServer = new ExEventLog.EventTuple(3221501645U, 15, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040011C9 RID: 4553
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_UnableToConnectToAnyPublicFolderServer = new ExEventLog.EventTuple(3221501646U, 15, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040011CA RID: 4554
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_UnableToFindFreeBusyPublicFolder = new ExEventLog.EventTuple(3221501647U, 15, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040011CB RID: 4555
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_UnableToSaveGroupMetricsToAD = new ExEventLog.EventTuple(1074018000U, 14, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040011CC RID: 4556
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_GroupMetricsGenerationFailed = new ExEventLog.EventTuple(3221501649U, 14, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040011CD RID: 4557
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_GroupMetricsGenerationSuccessful = new ExEventLog.EventTuple(1074018002U, 14, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040011CE RID: 4558
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_MailTipsMailboxQueryFailed = new ExEventLog.EventTuple(3221501651U, 14, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040011CF RID: 4559
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_GroupMetricsGenerationStarted = new ExEventLog.EventTuple(1074018004U, 14, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040011D0 RID: 4560
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_UnableToWriteFile = new ExEventLog.EventTuple(3221501653U, 14, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040011D1 RID: 4561
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_UnableToRemoveDirectory = new ExEventLog.EventTuple(3221501654U, 14, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040011D2 RID: 4562
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_GroupMetricsGenerationSkippedNoADFile = new ExEventLog.EventTuple(3221501655U, 14, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040011D3 RID: 4563
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_GroupMetricsGenerationCouldntFindSystemMailbox = new ExEventLog.EventTuple(3221501656U, 14, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040011D4 RID: 4564
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_UnableToAccessOrganizationMailbox = new ExEventLog.EventTuple(3221501657U, 14, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040011D5 RID: 4565
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_UploadGroupMetricsCookieFailed = new ExEventLog.EventTuple(2147759834U, 14, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040011D6 RID: 4566
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_DownloadGroupMetricsCookieFailed = new ExEventLog.EventTuple(2147759835U, 14, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040011D7 RID: 4567
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_DeleteGroupMetricsCookieFailed = new ExEventLog.EventTuple(2147759836U, 14, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040011D8 RID: 4568
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_CorruptTagConfigItem = new ExEventLog.EventTuple(2147761793U, 16, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040011D9 RID: 4569
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_OABGenerationStartGeneration = new ExEventLog.EventTuple(279145U, 17, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040011DA RID: 4570
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_OABGenerationCompletedGeneration = new ExEventLog.EventTuple(279146U, 17, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040011DB RID: 4571
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_OABGenerationCouldntFindSystemMailbox = new ExEventLog.EventTuple(3221504619U, 17, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040011DC RID: 4572
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_OABGenerationFailureException = new ExEventLog.EventTuple(3221504620U, 17, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040011DD RID: 4573
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_CannotFindOAB = new ExEventLog.EventTuple(3221504621U, 17, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040011DE RID: 4574
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_FatalOABFindException = new ExEventLog.EventTuple(3221504622U, 17, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040011DF RID: 4575
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_OrganizationalMailboxGuidIsNotUnique = new ExEventLog.EventTuple(3221504623U, 17, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040011E0 RID: 4576
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_OABGenerationFailureExceptionNoSpecificOAB = new ExEventLog.EventTuple(3221504624U, 17, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040011E1 RID: 4577
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_OABNotProcessedBecauseAddressListIsInvalid = new ExEventLog.EventTuple(3221504625U, 17, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040011E2 RID: 4578
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_CorruptOutlookProtectionRule = new ExEventLog.EventTuple(3221506617U, 19, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040011E3 RID: 4579
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_CorruptPolicyNudgeRule = new ExEventLog.EventTuple(3221506618U, 21, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040011E4 RID: 4580
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ProvisioningAssitantProvisionedMailbox = new ExEventLog.EventTuple(1074023970U, 20, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040011E5 RID: 4581
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ProvisioningAssistantFailedToProvisionMailbox = new ExEventLog.EventTuple(3221507619U, 20, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040011E6 RID: 4582
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_CorruptClassificationDefinition = new ExEventLog.EventTuple(3221508617U, 22, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040011E7 RID: 4583
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_FailedToUpdateSubscriptionOnMailboxTable = new ExEventLog.EventTuple(3221509617U, 23, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040011E8 RID: 4584
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_ErrorProcessingHandleEvent = new ExEventLog.EventTuple(3221509618U, 23, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040011E9 RID: 4585
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_FailedToResolveInboxFolderId = new ExEventLog.EventTuple(2147767795U, 23, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040011EA RID: 4586
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_ErrorAccessingRemoteMailbox = new ExEventLog.EventTuple(3221511617U, 24, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040011EB RID: 4587
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ProcessingStatisticsForPeopleRelevanceFeeder = new ExEventLog.EventTuple(287145U, 25, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x02000328 RID: 808
		private enum Category : short
		{
			// Token: 0x040011ED RID: 4589
			Service = 1,
			// Token: 0x040011EE RID: 4590
			OOF_Assistant,
			// Token: 0x040011EF RID: 4591
			OOF_Library,
			// Token: 0x040011F0 RID: 4592
			Availability_Service,
			// Token: 0x040011F1 RID: 4593
			Availability_Service_Configuration,
			// Token: 0x040011F2 RID: 4594
			Availability_Service_Authentication,
			// Token: 0x040011F3 RID: 4595
			Availability_Service_Authorization,
			// Token: 0x040011F4 RID: 4596
			Resource_Booking_Attendant,
			// Token: 0x040011F5 RID: 4597
			Calendar_Attendant,
			// Token: 0x040011F6 RID: 4598
			Managed_Folder_Assistant,
			// Token: 0x040011F7 RID: 4599
			Junk_Email_Options_Assistant,
			// Token: 0x040011F8 RID: 4600
			Conversations_Assistant,
			// Token: 0x040011F9 RID: 4601
			Approval_Assistant,
			// Token: 0x040011FA RID: 4602
			MailTips,
			// Token: 0x040011FB RID: 4603
			FreeBusy_Assistant,
			// Token: 0x040011FC RID: 4604
			ELC_library,
			// Token: 0x040011FD RID: 4605
			OAB_Generator_Assistant,
			// Token: 0x040011FE RID: 4606
			TopN_Words_Assistant,
			// Token: 0x040011FF RID: 4607
			Outlook_Protection_Rules,
			// Token: 0x04001200 RID: 4608
			Provisioning_Assistant_General,
			// Token: 0x04001201 RID: 4609
			Policy_Nudge_Rules,
			// Token: 0x04001202 RID: 4610
			Classification_Definitions,
			// Token: 0x04001203 RID: 4611
			Push_Notification_Assistant,
			// Token: 0x04001204 RID: 4612
			Calendar_Repair_Assistant,
			// Token: 0x04001205 RID: 4613
			People_Relevance_Assistant,
			// Token: 0x04001206 RID: 4614
			Discovery_Search,
			// Token: 0x04001207 RID: 4615
			Reminders_Assistant,
			// Token: 0x04001208 RID: 4616
			Calendar_Interop_Assistant
		}

		// Token: 0x02000329 RID: 809
		internal enum Message : uint
		{
			// Token: 0x0400120A RID: 4618
			ServiceStarted = 1074004969U,
			// Token: 0x0400120B RID: 4619
			ServiceStopped,
			// Token: 0x0400120C RID: 4620
			ServiceFailedToStart = 3221488619U,
			// Token: 0x0400120D RID: 4621
			ServiceStarting = 1074004972U,
			// Token: 0x0400120E RID: 4622
			ServiceStopping,
			// Token: 0x0400120F RID: 4623
			ServiceOutOfMemory = 3221488622U,
			// Token: 0x04001210 RID: 4624
			CreatingAssistant = 1074004975U,
			// Token: 0x04001211 RID: 4625
			FailCreateAssistant = 3221488624U,
			// Token: 0x04001212 RID: 4626
			DisabledAssistant = 1074004977U,
			// Token: 0x04001213 RID: 4627
			OOFConfigNotAccessible = 3221490617U,
			// Token: 0x04001214 RID: 4628
			OOFInvalidScheduleLine,
			// Token: 0x04001215 RID: 4629
			OOFTooManyContacts,
			// Token: 0x04001216 RID: 4630
			OOFRulesQuotaExceeded,
			// Token: 0x04001217 RID: 4631
			OOFHistoryMapiPermanentException,
			// Token: 0x04001218 RID: 4632
			OOFUnableToReadScheduleCache,
			// Token: 0x04001219 RID: 4633
			AutoDiscoverFailed = 3221491617U,
			// Token: 0x0400121A RID: 4634
			ProxyWebRequestFailed,
			// Token: 0x0400121B RID: 4635
			PublicFolderRequestFailed,
			// Token: 0x0400121C RID: 4636
			PublicFolderServerNotFoundForOU,
			// Token: 0x0400121D RID: 4637
			LocalForestConfigurationNotFound,
			// Token: 0x0400121E RID: 4638
			MailboxLogonFailed = 3221491625U,
			// Token: 0x0400121F RID: 4639
			WebRequestFailedSecurityChecks,
			// Token: 0x04001220 RID: 4640
			InvalidCredentialsForCrossForestProxying = 3221491628U,
			// Token: 0x04001221 RID: 4641
			CalendarQueryFailed,
			// Token: 0x04001222 RID: 4642
			LogonAsNetworkServiceFailed = 3221491632U,
			// Token: 0x04001223 RID: 4643
			NoCASFoundForRequest,
			// Token: 0x04001224 RID: 4644
			CASDiscoveryExceptionHandled,
			// Token: 0x04001225 RID: 4645
			WorkingHoursFailed,
			// Token: 0x04001226 RID: 4646
			InvalidMaximumDatabasesInQuery = 3221491637U,
			// Token: 0x04001227 RID: 4647
			InvalidMinimumDatabasesInQuery,
			// Token: 0x04001228 RID: 4648
			DuplicateAvailabilityAddressSpace,
			// Token: 0x04001229 RID: 4649
			CannotGetLocalSiteName,
			// Token: 0x0400122A RID: 4650
			SCPCannotConnectToRemoteDirectory,
			// Token: 0x0400122B RID: 4651
			SCPErrorSearchingLocalADForSCP,
			// Token: 0x0400122C RID: 4652
			SCPErrorSearchingForRemoteSCP,
			// Token: 0x0400122D RID: 4653
			SCPMisconfiguredLocalServiceBindings,
			// Token: 0x0400122E RID: 4654
			SCPMisconfiguredRemoteServiceBindings,
			// Token: 0x0400122F RID: 4655
			MSExchangeSystemAccountRetrieval,
			// Token: 0x04001230 RID: 4656
			AAResourceBooked = 1074011969U,
			// Token: 0x04001231 RID: 4657
			AAResourceCanceled,
			// Token: 0x04001232 RID: 4658
			CorruptCalendarConfiguration = 3221495619U,
			// Token: 0x04001233 RID: 4659
			RBAProcessedMeetingMessage = 1074011972U,
			// Token: 0x04001234 RID: 4660
			RBAProcessedMeetingCancelation,
			// Token: 0x04001235 RID: 4661
			RBAValidationException = 3221495622U,
			// Token: 0x04001236 RID: 4662
			RBANeutralCultureEncountered = 1074011975U,
			// Token: 0x04001237 RID: 4663
			RBANonUniqueLegacyDN,
			// Token: 0x04001238 RID: 4664
			ProcessingMeetingMessageFailure = 2147754793U,
			// Token: 0x04001239 RID: 4665
			InvalidElcDataInAD = 3221497617U,
			// Token: 0x0400123A RID: 4666
			InvalidELCFolderChange,
			// Token: 0x0400123B RID: 4667
			InvalidServerADObject,
			// Token: 0x0400123C RID: 4668
			NullDestinationFolder,
			// Token: 0x0400123D RID: 4669
			DestinationFolderSameAsSource,
			// Token: 0x0400123E RID: 4670
			UnableToUpdateElcFolder,
			// Token: 0x0400123F RID: 4671
			CorruptionInADElcFolders,
			// Token: 0x04001240 RID: 4672
			FailedToCreateFolderHierarchy,
			// Token: 0x04001241 RID: 4673
			FailedToReadAuditLogArgsFromAD,
			// Token: 0x04001242 RID: 4674
			MissingAuditLogPath,
			// Token: 0x04001243 RID: 4675
			ConfigureAuditLogFailed,
			// Token: 0x04001244 RID: 4676
			AppendAuditLogFailed,
			// Token: 0x04001245 RID: 4677
			ELCRootNameClash = 3221497630U,
			// Token: 0x04001246 RID: 4678
			CycleInPolicies,
			// Token: 0x04001247 RID: 4679
			InvalidRetentionAgeLimit,
			// Token: 0x04001248 RID: 4680
			CurrentFolderOnUnhandledException,
			// Token: 0x04001249 RID: 4681
			InvalidTagDataInAD,
			// Token: 0x0400124A RID: 4682
			IncompleteRPTUpgrade,
			// Token: 0x0400124B RID: 4683
			CorruptionInADElcTags,
			// Token: 0x0400124C RID: 4684
			MRMSkippingMailbox,
			// Token: 0x0400124D RID: 4685
			ArchiveOverWarningQuota = 2147755814U,
			// Token: 0x0400124E RID: 4686
			DumpsterOverQuotaDeletedMails,
			// Token: 0x0400124F RID: 4687
			MRMSkippingFolder = 3221497640U,
			// Token: 0x04001250 RID: 4688
			ExpirationOfCurrentBatchFailed = 2147755817U,
			// Token: 0x04001251 RID: 4689
			FoldersWithOversizedItems,
			// Token: 0x04001252 RID: 4690
			MRMExpirationStatistics = 1074013995U,
			// Token: 0x04001253 RID: 4691
			AdminAuditsQuotaWarning = 2147755820U,
			// Token: 0x04001254 RID: 4692
			DiscoveryHoldTransientErrorSkipMailbox = 3221497645U,
			// Token: 0x04001255 RID: 4693
			DiscoveryHoldPermanentErrorSkipMailbox,
			// Token: 0x04001256 RID: 4694
			DiscoveryHoldSearchFailed,
			// Token: 0x04001257 RID: 4695
			ExpirationOfMsgsInDiscoveryHoldsFolderFailed = 2147755824U,
			// Token: 0x04001258 RID: 4696
			DiscoveryHoldsSkippedForTooManyQueries = 1074014001U,
			// Token: 0x04001259 RID: 4697
			CorruptDiscoverySearchObject = 3221497650U,
			// Token: 0x0400125A RID: 4698
			CorruptDiscoverySearchObjectId,
			// Token: 0x0400125B RID: 4699
			DiscoverySearchObjectNotFound,
			// Token: 0x0400125C RID: 4700
			DiscoverySearchObjectLoadError,
			// Token: 0x0400125D RID: 4701
			DiscoverySearchObjectNotFoundForOrg,
			// Token: 0x0400125E RID: 4702
			FailedToCopyDiscoverySearchToArchive,
			// Token: 0x0400125F RID: 4703
			CorruptDiscoverySearchObjectProperty,
			// Token: 0x04001260 RID: 4704
			DiscoverySearchObjectLoadErrorForMailbox,
			// Token: 0x04001261 RID: 4705
			DumpsterOverQuotaDeletedAuditLogs = 2147755834U,
			// Token: 0x04001262 RID: 4706
			EhaMailboxQuotaWarning,
			// Token: 0x04001263 RID: 4707
			FailedToCopyLitigationHoldDurationToArchive = 3221497660U,
			// Token: 0x04001264 RID: 4708
			FailedToReadLitigationHoldDurationFromPrimaryMailbox,
			// Token: 0x04001265 RID: 4709
			ELCFailedToLoadProcessEhaMigrationMessageSetting = 1074014014U,
			// Token: 0x04001266 RID: 4710
			FailedToCopyEhaMigrationFlagToArchive = 3221497663U,
			// Token: 0x04001267 RID: 4711
			HoldCleanupStatistics = 1074014016U,
			// Token: 0x04001268 RID: 4712
			DiscoveryFailedToFetchSizeInformation = 3221497665U,
			// Token: 0x04001269 RID: 4713
			DiscoveryServerLocatorTimeout,
			// Token: 0x0400126A RID: 4714
			DiscoveryFanoutError,
			// Token: 0x0400126B RID: 4715
			DiscoveryAutodiscoverError,
			// Token: 0x0400126C RID: 4716
			DiscoveryFailedToGetOWAUrl = 2147755858U,
			// Token: 0x0400126D RID: 4717
			DiscoveryFailedToGetOWAService = 2147755852U,
			// Token: 0x0400126E RID: 4718
			DiscoveryFailedToGetOWAServiceWithException,
			// Token: 0x0400126F RID: 4719
			DiscoverySearchCIFailure = 3221497678U,
			// Token: 0x04001270 RID: 4720
			DiscoverySearchFailure,
			// Token: 0x04001271 RID: 4721
			DiscoveryMailboxServerLocatorTime = 1074014032U,
			// Token: 0x04001272 RID: 4722
			ELCSkipProcessingMailboxTransientException = 3221497681U,
			// Token: 0x04001273 RID: 4723
			ELCSkipProcessingMailbox = 1074014068U,
			// Token: 0x04001274 RID: 4724
			FailedToUpdateSafeLists = 2147756792U,
			// Token: 0x04001275 RID: 4725
			FailedToEnsureJunkEmailRule = 2147756798U,
			// Token: 0x04001276 RID: 4726
			FailedToUpdateMailbox = 3221498624U,
			// Token: 0x04001277 RID: 4727
			FailedToUpdateConversationOnDelete = 2147757793U,
			// Token: 0x04001278 RID: 4728
			FailedToUpdateConversationOnQuotaExceeded,
			// Token: 0x04001279 RID: 4729
			FailedToUpdateConversationOnFolderDelete,
			// Token: 0x0400127A RID: 4730
			CorruptConversationActionItem = 3221499620U,
			// Token: 0x0400127B RID: 4731
			TotalNumberOfItemsForBodyTagProcessing = 1074015973U,
			// Token: 0x0400127C RID: 4732
			BodyTagProcessingFailed = 3221499622U,
			// Token: 0x0400127D RID: 4733
			BodyTagProcessingSucceeded = 1074015975U,
			// Token: 0x0400127E RID: 4734
			BodyTagProcessingSkipped,
			// Token: 0x0400127F RID: 4735
			BodyTagProcessingRequestQueued,
			// Token: 0x04001280 RID: 4736
			DeliveryToArbitrationMailboxExceededRateLimits = 2147758793U,
			// Token: 0x04001281 RID: 4737
			UnableToReadConfigurationFromAD = 3221501617U,
			// Token: 0x04001282 RID: 4738
			UnableToCreateGroupMetricsDirectory,
			// Token: 0x04001283 RID: 4739
			UnableToGetDomainList = 3221501624U,
			// Token: 0x04001284 RID: 4740
			UnableToGetListOfChangedGroupsForDomain = 3221501626U,
			// Token: 0x04001285 RID: 4741
			UnableToReadGroupMetricsCookie = 2147759804U,
			// Token: 0x04001286 RID: 4742
			UnableToDeserializeGroupMetricsCookie,
			// Token: 0x04001287 RID: 4743
			UnableToRemoveCorruptGroupMetricsCookie = 3221501630U,
			// Token: 0x04001288 RID: 4744
			UnableToSaveGroupMetricsCookie,
			// Token: 0x04001289 RID: 4745
			UnableToOpenFile,
			// Token: 0x0400128A RID: 4746
			GroupMetricsCookieExpired,
			// Token: 0x0400128B RID: 4747
			UnableToReadChangedGroupList = 3221501635U,
			// Token: 0x0400128C RID: 4748
			GroupExpansionHaltedWarning = 2147759812U,
			// Token: 0x0400128D RID: 4749
			GroupExpansionHaltedError = 3221501637U,
			// Token: 0x0400128E RID: 4750
			UnableToRegisterChangeNotification = 2147759817U,
			// Token: 0x0400128F RID: 4751
			UnableToFindPublicFolderServer = 3221501645U,
			// Token: 0x04001290 RID: 4752
			UnableToConnectToAnyPublicFolderServer,
			// Token: 0x04001291 RID: 4753
			UnableToFindFreeBusyPublicFolder,
			// Token: 0x04001292 RID: 4754
			UnableToSaveGroupMetricsToAD = 1074018000U,
			// Token: 0x04001293 RID: 4755
			GroupMetricsGenerationFailed = 3221501649U,
			// Token: 0x04001294 RID: 4756
			GroupMetricsGenerationSuccessful = 1074018002U,
			// Token: 0x04001295 RID: 4757
			MailTipsMailboxQueryFailed = 3221501651U,
			// Token: 0x04001296 RID: 4758
			GroupMetricsGenerationStarted = 1074018004U,
			// Token: 0x04001297 RID: 4759
			UnableToWriteFile = 3221501653U,
			// Token: 0x04001298 RID: 4760
			UnableToRemoveDirectory,
			// Token: 0x04001299 RID: 4761
			GroupMetricsGenerationSkippedNoADFile,
			// Token: 0x0400129A RID: 4762
			GroupMetricsGenerationCouldntFindSystemMailbox,
			// Token: 0x0400129B RID: 4763
			UnableToAccessOrganizationMailbox,
			// Token: 0x0400129C RID: 4764
			UploadGroupMetricsCookieFailed = 2147759834U,
			// Token: 0x0400129D RID: 4765
			DownloadGroupMetricsCookieFailed,
			// Token: 0x0400129E RID: 4766
			DeleteGroupMetricsCookieFailed,
			// Token: 0x0400129F RID: 4767
			CorruptTagConfigItem = 2147761793U,
			// Token: 0x040012A0 RID: 4768
			OABGenerationStartGeneration = 279145U,
			// Token: 0x040012A1 RID: 4769
			OABGenerationCompletedGeneration,
			// Token: 0x040012A2 RID: 4770
			OABGenerationCouldntFindSystemMailbox = 3221504619U,
			// Token: 0x040012A3 RID: 4771
			OABGenerationFailureException,
			// Token: 0x040012A4 RID: 4772
			CannotFindOAB,
			// Token: 0x040012A5 RID: 4773
			FatalOABFindException,
			// Token: 0x040012A6 RID: 4774
			OrganizationalMailboxGuidIsNotUnique,
			// Token: 0x040012A7 RID: 4775
			OABGenerationFailureExceptionNoSpecificOAB,
			// Token: 0x040012A8 RID: 4776
			OABNotProcessedBecauseAddressListIsInvalid,
			// Token: 0x040012A9 RID: 4777
			CorruptOutlookProtectionRule = 3221506617U,
			// Token: 0x040012AA RID: 4778
			CorruptPolicyNudgeRule,
			// Token: 0x040012AB RID: 4779
			ProvisioningAssitantProvisionedMailbox = 1074023970U,
			// Token: 0x040012AC RID: 4780
			ProvisioningAssistantFailedToProvisionMailbox = 3221507619U,
			// Token: 0x040012AD RID: 4781
			CorruptClassificationDefinition = 3221508617U,
			// Token: 0x040012AE RID: 4782
			FailedToUpdateSubscriptionOnMailboxTable = 3221509617U,
			// Token: 0x040012AF RID: 4783
			ErrorProcessingHandleEvent,
			// Token: 0x040012B0 RID: 4784
			FailedToResolveInboxFolderId = 2147767795U,
			// Token: 0x040012B1 RID: 4785
			ErrorAccessingRemoteMailbox = 3221511617U,
			// Token: 0x040012B2 RID: 4786
			ProcessingStatisticsForPeopleRelevanceFeeder = 287145U
		}
	}
}
