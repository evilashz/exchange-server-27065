using System;
using System.Collections.Generic;
using System.Management.Automation;
using System.Net;
using System.Text;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Directory.SystemConfiguration.ConfigurationSettings;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.MailboxReplicationService;
using Microsoft.Exchange.Management.Common;
using Microsoft.Exchange.Management.EventMessages;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.Provisioning.LoadBalancing;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x02000C80 RID: 3200
	internal static class RequestTaskHelper
	{
		// Token: 0x06007AFE RID: 31486 RVA: 0x001F8554 File Offset: 0x001F6754
		public static void WriteReportEntries(string request, List<ReportEntry> entries, object target, Task.TaskVerboseLoggingDelegate writeVerbose, Task.TaskWarningLoggingDelegate writeWarning, Task.TaskErrorLoggingDelegate writeError)
		{
			if (entries != null)
			{
				foreach (ReportEntry reportEntry in entries)
				{
					if (reportEntry.Type == ReportEntryType.Error)
					{
						writeError(new InvalidRequestPermanentException(request, reportEntry.Message), ErrorCategory.InvalidOperation, target);
					}
					else if (reportEntry.Type == ReportEntryType.Warning || reportEntry.Type == ReportEntryType.WarningCondition)
					{
						writeWarning(reportEntry.Message);
					}
					else
					{
						writeVerbose(reportEntry.Message);
					}
				}
			}
		}

		// Token: 0x06007AFF RID: 31487 RVA: 0x001F85F0 File Offset: 0x001F67F0
		public static void TickleMRS(TransactionalRequestJob requestJob, MoveRequestNotification notification, Guid mdbGuid, ITopologyConfigurationSession configSession, List<string> unreachableServers)
		{
			using (MailboxReplicationServiceClient mailboxReplicationServiceClient = requestJob.CreateMRSClient(configSession, mdbGuid, unreachableServers))
			{
				if (notification == MoveRequestNotification.Canceled && mailboxReplicationServiceClient.ServerVersion[3])
				{
					mailboxReplicationServiceClient.RefreshMoveRequest2(requestJob.RequestGuid, mdbGuid, (int)requestJob.Flags, notification);
				}
				else
				{
					mailboxReplicationServiceClient.RefreshMoveRequest(requestJob.RequestGuid, mdbGuid, notification);
				}
			}
		}

		// Token: 0x06007B00 RID: 31488 RVA: 0x001F865C File Offset: 0x001F685C
		public static bool IsKnownExceptionHandler(Exception exception, Task.TaskVerboseLoggingDelegate writeVerbose)
		{
			if (exception is MapiRetryableException || exception is MapiPermanentException)
			{
				return true;
			}
			if (exception is MailboxReplicationPermanentException || exception is MailboxReplicationTransientException || exception is ConfigurationSettingsException)
			{
				writeVerbose(CommonUtils.FullExceptionMessage(exception));
				return true;
			}
			return false;
		}

		// Token: 0x06007B01 RID: 31489 RVA: 0x001F8697 File Offset: 0x001F6897
		public static bool CheckUserOrgIdIsTenant(OrganizationId userOrgId)
		{
			return !userOrgId.Equals(OrganizationId.ForestWideOrgId);
		}

		// Token: 0x06007B02 RID: 31490 RVA: 0x001F86C4 File Offset: 0x001F68C4
		public static MailboxDatabase ChooseTargetMDB(IEnumerable<ADObjectId> excludedDatabaseIds, bool checkInitialProvisioningSetting, ADUser adUser, Fqdn domainController, ScopeSet scopeSet, Action<LocalizedString> writeVerbose, Action<LocalizedException, ExchangeErrorCategory, object> writeExchangeError, Action<Exception, ErrorCategory, object> writeError, object identity)
		{
			MailboxProvisioningConstraint mailboxProvisioningConstraint = (adUser == null) ? new MailboxProvisioningConstraint() : adUser.MailboxProvisioningConstraint;
			LoadBalancingReport loadBalancingReport = new LoadBalancingReport();
			MailboxDatabaseWithLocationInfo mailboxDatabaseWithLocationInfo = PhysicalResourceLoadBalancing.FindDatabaseAndLocation(domainController, delegate(string msg)
			{
				writeVerbose(new LocalizedString(msg));
			}, scopeSet, checkInitialProvisioningSetting, false, new int?(Server.E15MinVersion), mailboxProvisioningConstraint, excludedDatabaseIds, ref loadBalancingReport);
			if (mailboxDatabaseWithLocationInfo == null)
			{
				ExManagementApplicationLogger.LogEvent(ManagementEventLogConstants.Tuple_LoadBalancingFailedToFindDatabase, new string[]
				{
					domainController,
					loadBalancingReport.ToString()
				});
				writeExchangeError(new RecipientTaskException(Strings.ErrorAutomaticProvisioningFailedToFindDatabase("TargetDatabase")), ExchangeErrorCategory.ServerOperation, null);
			}
			return mailboxDatabaseWithLocationInfo.MailboxDatabase;
		}

		// Token: 0x06007B03 RID: 31491 RVA: 0x001F8770 File Offset: 0x001F6970
		public static ITopologyConfigurationSession GetConfigSessionForDatabase(ITopologyConfigurationSession originalConfigSession, ADObjectId database)
		{
			ITopologyConfigurationSession result = originalConfigSession;
			if (database != null && ConfigBase<MRSConfigSchema>.GetConfig<bool>("CrossResourceForestEnabled"))
			{
				PartitionId partitionId = database.GetPartitionId();
				if (!partitionId.IsLocalForestPartition())
				{
					ADSessionSettings sessionSettings = ADSessionSettings.FromAccountPartitionRootOrgScopeSet(partitionId);
					result = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(null, true, ConsistencyMode.PartiallyConsistent, null, sessionSettings, 612, "GetConfigSessionForDatabase", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\MailboxReplication\\RequestBase\\RequestTaskHelper.cs");
				}
			}
			return result;
		}

		// Token: 0x06007B04 RID: 31492 RVA: 0x001F87C4 File Offset: 0x001F69C4
		public static IConfigurationSession CreateOrganizationFindingSession(OrganizationId currentOrgId, OrganizationId executingUserOrgId)
		{
			ADObjectId rootOrgContainerId = ADSystemConfigurationSession.GetRootOrgContainerId(null, null);
			ADSessionSettings sessionSettings = ADSessionSettings.FromOrganizationIdWithoutRbacScopes(rootOrgContainerId, currentOrgId, executingUserOrgId, true);
			IConfigurationSession tenantOrTopologyConfigurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(null, true, ConsistencyMode.PartiallyConsistent, null, sessionSettings, ConfigScopes.TenantSubTree, 643, "CreateOrganizationFindingSession", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\MailboxReplication\\RequestBase\\RequestTaskHelper.cs");
			tenantOrTopologyConfigurationSession.UseConfigNC = false;
			return tenantOrTopologyConfigurationSession;
		}

		// Token: 0x06007B05 RID: 31493 RVA: 0x001F880C File Offset: 0x001F6A0C
		public static ADUser ResolveADUser(IRecipientSession dataSession, IRecipientSession globalCatalogSession, ADServerSettings serverSettings, IIdentityParameter identity, OptionalIdentityData optionalData, string domainController, DataAccessHelper.CategorizedGetDataObjectDelegate getDataObjectHandler, Task.TaskVerboseLoggingDelegate logHandler, Task.ErrorLoggerDelegate errorHandler, bool checkScopes)
		{
			ADUser aduser = (ADUser)RecipientTaskHelper.ResolveDataObject<ADUser>(dataSession, globalCatalogSession, serverSettings, identity, null, optionalData, domainController, getDataObjectHandler, logHandler, errorHandler);
			ADScopeException exception;
			if (checkScopes && !dataSession.TryVerifyIsWithinScopes(aduser, true, out exception))
			{
				errorHandler(exception, (ExchangeErrorCategory)5, identity);
			}
			return aduser;
		}

		// Token: 0x06007B06 RID: 31494 RVA: 0x001F884E File Offset: 0x001F6A4E
		public static void ValidateStartAfterTime(DateTime startAfterUtc, Task.TaskErrorLoggingDelegate writeError, DateTime utcNow)
		{
			if (utcNow.AddDays(30.0) < startAfterUtc)
			{
				writeError(new MoveStartAfterDateRangeException(30), ErrorCategory.InvalidArgument, startAfterUtc);
			}
		}

		// Token: 0x06007B07 RID: 31495 RVA: 0x001F887C File Offset: 0x001F6A7C
		public static void ValidateCompleteAfterTime(DateTime completeAfterUtc, Task.TaskErrorLoggingDelegate writeError, DateTime utcNow)
		{
			if (utcNow.AddDays(120.0) < completeAfterUtc)
			{
				writeError(new MoveCompleteAfterDateRangeException(120), ErrorCategory.InvalidArgument, completeAfterUtc);
			}
		}

		// Token: 0x06007B08 RID: 31496 RVA: 0x001F88AA File Offset: 0x001F6AAA
		public static void ValidateStartAfterComesBeforeCompleteAfter(DateTime? startAfterUtc, DateTime? completeAfterUtc, Task.TaskErrorLoggingDelegate writeError)
		{
			if (startAfterUtc != null && completeAfterUtc != null && startAfterUtc.Value > completeAfterUtc.Value)
			{
				writeError(new MoveStartAfterEarlierThanCompleteAfterException(), ErrorCategory.InvalidArgument, startAfterUtc);
			}
		}

		// Token: 0x06007B09 RID: 31497 RVA: 0x001F88E5 File Offset: 0x001F6AE5
		public static void ValidateStartAfterCompleteAfterWithSuspendWhenReadyToComplete(DateTime? startAfter, DateTime? completeAfter, bool suspendWhenReadyToComplete, Task.TaskErrorLoggingDelegate writeError)
		{
			if (suspendWhenReadyToComplete && (startAfter != null || completeAfter != null))
			{
				writeError(new SuspendWhenReadyToCompleteCannotBeSetWithStartAfterOrCompleteAfterException(), ErrorCategory.InvalidArgument, suspendWhenReadyToComplete);
			}
		}

		// Token: 0x06007B0A RID: 31498 RVA: 0x001F890E File Offset: 0x001F6B0E
		public static void ValidateIncrementalSyncInterval(TimeSpan incrementalSyncInterval, Task.TaskErrorLoggingDelegate writeError)
		{
			if (incrementalSyncInterval < TimeSpan.Zero || incrementalSyncInterval > TimeSpan.FromDays(120.0))
			{
				writeError(new IncrementalSyncIntervalRangeException(0, 120), ErrorCategory.InvalidArgument, incrementalSyncInterval);
			}
		}

		// Token: 0x06007B0B RID: 31499 RVA: 0x001F8948 File Offset: 0x001F6B48
		public static bool CompareUtcTimeWithLocalTime(DateTime? utcTime, DateTime? localTime)
		{
			return (utcTime == null && localTime == null) || (utcTime != null && localTime != null && utcTime.Value == localTime.Value.ToUniversalTime());
		}

		// Token: 0x06007B0C RID: 31500 RVA: 0x001F8998 File Offset: 0x001F6B98
		public static void ValidateItemLimits(Unlimited<int> badItemLimit, Unlimited<int> largeItemLimit, SwitchParameter acceptLargeDataLoss, Task.TaskErrorLoggingDelegate writeError, Task.TaskWarningLoggingDelegate writeWarning, string executingUserIdentity)
		{
			Unlimited<int> value = new Unlimited<int>(TestIntegration.Instance.LargeDataLossThreshold);
			PropertyValidationError propertyValidationError = RequestJobSchema.BadItemLimit.ValidateValue(badItemLimit, false);
			if (propertyValidationError != null)
			{
				writeError(new DataValidationException(propertyValidationError), ErrorCategory.InvalidArgument, badItemLimit);
			}
			propertyValidationError = RequestJobSchema.LargeItemLimit.ValidateValue(largeItemLimit, false);
			if (propertyValidationError != null)
			{
				writeError(new DataValidationException(propertyValidationError), ErrorCategory.InvalidArgument, largeItemLimit);
			}
			if (largeItemLimit > value && !acceptLargeDataLoss)
			{
				writeError(new LargeDataLossNotAcceptedPermanentException("LargeItemLimit", largeItemLimit.ToString(), "AcceptLargeDataLoss", executingUserIdentity), ErrorCategory.InvalidArgument, acceptLargeDataLoss);
			}
			if (badItemLimit == RequestTaskHelper.UnlimitedZero && largeItemLimit == RequestTaskHelper.UnlimitedZero && acceptLargeDataLoss)
			{
				writeError(new RecipientTaskException(Strings.ErrorParameterValueNotAllowed("AcceptLargeDataLoss")), ErrorCategory.InvalidArgument, acceptLargeDataLoss);
			}
			if (badItemLimit > RequestTaskHelper.UnlimitedZero)
			{
				writeWarning(Strings.WarningNonZeroItemLimitMove("BadItemLimit"));
			}
			if (largeItemLimit > RequestTaskHelper.UnlimitedZero)
			{
				writeWarning(Strings.WarningNonZeroItemLimitMove("LargeItemLimit"));
			}
		}

		// Token: 0x06007B0D RID: 31501 RVA: 0x001F8ABC File Offset: 0x001F6CBC
		public static void ValidatePrimaryOnlyMoveArchiveDatabase(ADUser user, Action<Exception, ErrorCategory> writeError)
		{
			if (user.HasLocalArchive)
			{
				if (user.ArchiveDatabaseRaw == null)
				{
					writeError(new ArchiveDatabaseNotStampedPermanentException(), ErrorCategory.InvalidArgument);
				}
				if (!ADObjectId.Equals(user.ArchiveDatabase, user.ArchiveDatabaseRaw))
				{
					string archiveDb = (user.ArchiveDatabase != null) ? user.ArchiveDatabase.ToString() : "null";
					string archiveDbRaw = (user.ArchiveDatabaseRaw != null) ? user.ArchiveDatabaseRaw.ToString() : "null";
					writeError(new ArchiveDatabaseDifferentFromRawValuePermanentException(archiveDb, archiveDbRaw), ErrorCategory.InvalidArgument);
				}
			}
		}

		// Token: 0x06007B0E RID: 31502 RVA: 0x001F8B3C File Offset: 0x001F6D3C
		public static void ValidateNotImplicitSplit(RequestFlags moveFlags, ADUser sourceUser, Task.TaskErrorLoggingDelegate writeError, object errorTarget)
		{
			if (CommonUtils.IsImplicitSplit(moveFlags, sourceUser))
			{
				writeError(new ImplicitSplitPermanentException(), ErrorCategory.InvalidArgument, errorTarget);
			}
		}

		// Token: 0x06007B0F RID: 31503 RVA: 0x001F8B54 File Offset: 0x001F6D54
		public static NetworkCredential GetNetworkCredential(PSCredential psCred, AuthenticationMethod? authMethod)
		{
			return CommonUtils.GetNetworkCredential(psCred, authMethod);
		}

		// Token: 0x06007B10 RID: 31504 RVA: 0x001F8B60 File Offset: 0x001F6D60
		public static void SetSkipMoving(SkippableMoveComponent[] skipMoving, RequestJobBase moveRequest, Task.TaskErrorLoggingDelegate writeError, bool computeCalculatedDefaultsFromVersions = true)
		{
			moveRequest.SkipFolderACLs = false;
			moveRequest.SkipFolderRules = false;
			moveRequest.SkipFolderPromotedProperties = false;
			moveRequest.SkipFolderViews = false;
			moveRequest.SkipFolderRestrictions = false;
			moveRequest.SkipContentVerification = false;
			moveRequest.BlockFinalization = false;
			moveRequest.FailOnFirstBadItem = false;
			moveRequest.SkipKnownCorruptions = false;
			moveRequest.FailOnCorruptSyncState = false;
			if (computeCalculatedDefaultsFromVersions)
			{
				RequestTaskHelper.SetCalculatedSkipMovingDefaults(moveRequest);
			}
			if (skipMoving == null)
			{
				return;
			}
			int i = 0;
			while (i < skipMoving.Length)
			{
				SkippableMoveComponent skippableMoveComponent = skipMoving[i];
				switch (skippableMoveComponent)
				{
				case SkippableMoveComponent.FolderRules:
					moveRequest.SkipFolderRules = true;
					break;
				case SkippableMoveComponent.FolderACLs:
					moveRequest.SkipFolderACLs = true;
					break;
				case SkippableMoveComponent.FolderPromotedProperties:
					moveRequest.SkipFolderPromotedProperties = true;
					break;
				case SkippableMoveComponent.FolderViews:
					moveRequest.SkipFolderViews = true;
					break;
				case SkippableMoveComponent.FolderRestrictions:
					moveRequest.SkipFolderRestrictions = true;
					break;
				case SkippableMoveComponent.ContentVerification:
					moveRequest.SkipContentVerification = true;
					break;
				case SkippableMoveComponent.BlockFinalization:
					moveRequest.BlockFinalization = true;
					break;
				case SkippableMoveComponent.FailOnFirstBadItem:
					moveRequest.FailOnFirstBadItem = true;
					break;
				case (SkippableMoveComponent)8:
				case (SkippableMoveComponent)9:
				case (SkippableMoveComponent)10:
				case (SkippableMoveComponent)11:
				case (SkippableMoveComponent)13:
					goto IL_100;
				case SkippableMoveComponent.KnownCorruptions:
					moveRequest.SkipKnownCorruptions = true;
					break;
				case SkippableMoveComponent.FailOnCorruptSyncState:
					moveRequest.FailOnCorruptSyncState = true;
					break;
				default:
					goto IL_100;
				}
				IL_11D:
				i++;
				continue;
				IL_100:
				writeError(new ArgumentException(string.Format("Unknown value in SkipMoving parameter: {0}", skippableMoveComponent)), ErrorCategory.InvalidArgument, skipMoving);
				goto IL_11D;
			}
		}

		// Token: 0x06007B11 RID: 31505 RVA: 0x001F8C98 File Offset: 0x001F6E98
		public static void SetSkipMerging(SkippableMergeComponent[] skipMerging, RequestJobBase dataObject, Task.TaskErrorLoggingDelegate writeError)
		{
			dataObject.SkipFolderACLs = false;
			dataObject.SkipFolderRules = false;
			dataObject.SkipInitialConnectionValidation = false;
			dataObject.FailOnFirstBadItem = false;
			dataObject.SkipContentVerification = false;
			dataObject.SkipKnownCorruptions = false;
			dataObject.FailOnCorruptSyncState = false;
			if (skipMerging == null)
			{
				return;
			}
			int i = 0;
			while (i < skipMerging.Length)
			{
				SkippableMergeComponent skippableMergeComponent = skipMerging[i];
				switch (skippableMergeComponent)
				{
				case SkippableMergeComponent.FolderRules:
					dataObject.SkipFolderRules = true;
					break;
				case SkippableMergeComponent.FolderACLs:
					dataObject.SkipFolderACLs = true;
					break;
				case SkippableMergeComponent.InitialConnectionValidation:
					dataObject.SkipInitialConnectionValidation = true;
					break;
				case (SkippableMergeComponent)3:
					goto IL_AB;
				case SkippableMergeComponent.FailOnFirstBadItem:
					dataObject.FailOnFirstBadItem = true;
					break;
				case SkippableMergeComponent.ContentVerification:
					dataObject.SkipContentVerification = true;
					break;
				case SkippableMergeComponent.KnownCorruptions:
					dataObject.SkipKnownCorruptions = true;
					break;
				case SkippableMergeComponent.FailOnCorruptSyncState:
					dataObject.FailOnCorruptSyncState = true;
					break;
				default:
					goto IL_AB;
				}
				IL_C8:
				i++;
				continue;
				IL_AB:
				writeError(new ArgumentException(string.Format("Unknown value in SkipMerging parameter: {0}", skippableMergeComponent)), ErrorCategory.InvalidArgument, skipMerging);
				goto IL_C8;
			}
		}

		// Token: 0x06007B12 RID: 31506 RVA: 0x001F8D7C File Offset: 0x001F6F7C
		public static void SetInternalFlags(InternalMrsFlag[] flags, RequestJobBase dataObject, Task.TaskErrorLoggingDelegate writeError)
		{
			dataObject.SkipConvertingSourceToMeu = false;
			dataObject.ResolveServer = false;
			dataObject.UseTcp = false;
			dataObject.CrossResourceForest = false;
			dataObject.SkipPreFinalSyncDataProcessing = false;
			dataObject.SkipWordBreaking = false;
			dataObject.SkipStorageProviderForSource = false;
			dataObject.SkipMailboxReleaseCheck = false;
			dataObject.SkipProvisioningCheck = false;
			dataObject.UseCertificateAuthentication = false;
			dataObject.InvalidateContentIndexAnnotations = false;
			if (flags == null)
			{
				return;
			}
			foreach (InternalMrsFlag internalMrsFlag in flags)
			{
				switch (internalMrsFlag)
				{
				case InternalMrsFlag.SkipPreFinalSyncDataProcessing:
					dataObject.SkipPreFinalSyncDataProcessing = true;
					break;
				case InternalMrsFlag.SkipWordBreaking:
					dataObject.SkipWordBreaking = true;
					break;
				case InternalMrsFlag.SkipStorageProviderForSource:
					dataObject.SkipStorageProviderForSource = true;
					break;
				case InternalMrsFlag.SkipMailboxReleaseCheck:
					dataObject.SkipMailboxReleaseCheck = true;
					break;
				case InternalMrsFlag.SkipProvisioningCheck:
					dataObject.SkipProvisioningCheck = true;
					break;
				case InternalMrsFlag.CrossResourceForest:
					dataObject.CrossResourceForest = true;
					break;
				case InternalMrsFlag.DoNotConvertSourceToMeu:
					dataObject.SkipConvertingSourceToMeu = true;
					break;
				case InternalMrsFlag.ResolveServer:
					dataObject.ResolveServer = true;
					break;
				case InternalMrsFlag.UseTcp:
					dataObject.UseTcp = true;
					break;
				case InternalMrsFlag.UseCertificateAuthentication:
					dataObject.UseCertificateAuthentication = true;
					break;
				case InternalMrsFlag.InvalidateContentIndexAnnotations:
					dataObject.InvalidateContentIndexAnnotations = true;
					break;
				default:
					writeError(new ArgumentException(string.Format("Unknown value in InternalFlags parameter: {0}", internalMrsFlag)), ErrorCategory.InvalidArgument, flags);
					break;
				}
			}
		}

		// Token: 0x06007B13 RID: 31507 RVA: 0x001F8F84 File Offset: 0x001F7184
		public static void GetUpdatedMRSRequestInfo(RequestStatisticsBase requestJob, bool diagnostic, string diagnosticArgument)
		{
			MoveRequestInfo requestInfo = null;
			CommonUtils.CatchKnownExceptions(delegate
			{
				string mrsServer = MailboxReplicationServiceClient.GetMrsServer(requestJob.WorkItemQueueMdb.ObjectGuid);
				using (MailboxReplicationServiceClient mailboxReplicationServiceClient = MailboxReplicationServiceClient.Create(mrsServer))
				{
					requestInfo = mailboxReplicationServiceClient.GetMoveRequestInfo(requestJob.IdentifyingGuid);
					requestJob.UpdateThroughputFromMoveRequestInfo(requestInfo);
					if (RequestTaskHelper.NeedToUpdateJobPickupMessage())
					{
						requestJob.UpdateMessageFromMoveRequestInfo(requestInfo);
					}
					if (diagnostic)
					{
						string jobPickupFailureMessage = (requestInfo == null) ? string.Empty : requestInfo.Message.ToString();
						requestJob.PopulateDiagnosticInfo(new RequestStatisticsDiagnosticArgument(diagnosticArgument), jobPickupFailureMessage);
					}
				}
			}, null);
		}

		// Token: 0x06007B14 RID: 31508 RVA: 0x001F8FC5 File Offset: 0x001F71C5
		private static bool NeedToUpdateJobPickupMessage()
		{
			return ConfigBase<MRSConfigSchema>.GetConfig<bool>("ShowJobPickupStatusInRequestStatisticsMessage");
		}

		// Token: 0x06007B15 RID: 31509 RVA: 0x001F8FD4 File Offset: 0x001F71D4
		public static void SetStartAfter(DateTime? startAfter, RequestJobBase dataObject, StringBuilder changedValuesTracker = null)
		{
			DateTime? value = null;
			if (startAfter != null)
			{
				value = new DateTime?(startAfter.Value.ToUniversalTime());
			}
			string arg = (value == null) ? "(null)" : value.ToString();
			RequestTaskHelper.TrackerAppendLine(changedValuesTracker, string.Format("TimeTracker.StartAfter: {0} -> {1}", dataObject.TimeTracker.GetTimestamp(RequestJobTimestamp.StartAfter), arg));
			dataObject.TimeTracker.SetTimestamp(RequestJobTimestamp.StartAfter, value);
			DateTime? timestamp = dataObject.TimeTracker.GetTimestamp(RequestJobTimestamp.DoNotPickUntil);
			RequestTaskHelper.TrackerAppendLine(changedValuesTracker, string.Format("TimeTracker.DoNotPickUntilTimeStamp: {0} -> {1}", timestamp, arg));
			dataObject.TimeTracker.SetTimestamp(RequestJobTimestamp.DoNotPickUntil, value);
		}

		// Token: 0x06007B16 RID: 31510 RVA: 0x001F9088 File Offset: 0x001F7288
		public static void SetCompleteAfter(DateTime? completeAfter, RequestJobBase dataObject, StringBuilder changedValuesTracker = null)
		{
			DateTime? value = null;
			if (completeAfter != null)
			{
				value = new DateTime?(completeAfter.Value.ToUniversalTime());
			}
			string arg = (value == null) ? "(null)" : value.ToString();
			RequestTaskHelper.TrackerAppendLine(changedValuesTracker, string.Format("TimeTracker.CompleteAfter: {0} -> {1}", dataObject.TimeTracker.GetTimestamp(RequestJobTimestamp.CompleteAfter), arg));
			dataObject.TimeTracker.SetTimestamp(RequestJobTimestamp.CompleteAfter, value);
			if (dataObject.Status == RequestStatus.Synced)
			{
				DateTime? timestamp = dataObject.TimeTracker.GetTimestamp(RequestJobTimestamp.DoNotPickUntil);
				TimeSpan incrementalSyncInterval = dataObject.IncrementalSyncInterval;
				if (incrementalSyncInterval == TimeSpan.Zero || (value != null && value.Value < timestamp))
				{
					RequestTaskHelper.TrackerAppendLine(changedValuesTracker, string.Format("TimeTracker.DoNotPickUntilTimeStamp: {0} -> {1}", timestamp, arg));
					dataObject.TimeTracker.SetTimestamp(RequestJobTimestamp.DoNotPickUntil, value);
				}
			}
		}

		// Token: 0x06007B17 RID: 31511 RVA: 0x001F918C File Offset: 0x001F738C
		internal static LocalizedException TranslateExceptionHandler(Exception e)
		{
			if (e is LocalizedException)
			{
				if (!(e is MapiRetryableException))
				{
					if (!(e is MapiPermanentException))
					{
						goto IL_42;
					}
				}
				try
				{
					LocalizedException ex = StorageGlobals.TranslateMapiException(Strings.UnableToCommunicate, (LocalizedException)e, null, null, string.Empty, new object[0]);
					if (ex != null)
					{
						return ex;
					}
				}
				catch (ArgumentException)
				{
				}
			}
			IL_42:
			return null;
		}

		// Token: 0x06007B18 RID: 31512 RVA: 0x001F91F0 File Offset: 0x001F73F0
		internal static void ApplyOrganization(TransactionalRequestJob dataObject, OrganizationId organizationId)
		{
			dataObject.OrganizationId = organizationId;
			if (organizationId != OrganizationId.ForestWideOrgId)
			{
				dataObject.PartitionHint = TenantPartitionHint.FromOrganizationId(organizationId);
				dataObject.ExternalDirectoryOrganizationId = dataObject.PartitionHint.GetExternalDirectoryOrganizationId();
			}
		}

		// Token: 0x06007B19 RID: 31513 RVA: 0x001F9224 File Offset: 0x001F7424
		private static void SetCalculatedSkipMovingDefaults(RequestJobBase moveRequest)
		{
			bool flag = false;
			bool flag2 = false;
			if (moveRequest.PrimaryIsMoving)
			{
				ServerVersion serverVersion = new ServerVersion(moveRequest.SourceVersion);
				ServerVersion serverVersion2 = new ServerVersion(moveRequest.TargetVersion);
				if (serverVersion.Major != serverVersion2.Major)
				{
					flag = true;
				}
				if (serverVersion2.Major < Server.Exchange2011MajorVersion)
				{
					flag2 = true;
				}
			}
			if (moveRequest.ArchiveIsMoving)
			{
				ServerVersion serverVersion = new ServerVersion(moveRequest.SourceArchiveVersion);
				ServerVersion serverVersion2 = new ServerVersion(moveRequest.TargetArchiveVersion);
				if (serverVersion.Major != serverVersion2.Major)
				{
					flag = true;
				}
				if (serverVersion2.Major < Server.Exchange2011MajorVersion)
				{
					flag2 = true;
				}
			}
			if (flag)
			{
				moveRequest.SkipFolderPromotedProperties = true;
			}
			if (flag2)
			{
				moveRequest.SkipFolderViews = true;
			}
		}

		// Token: 0x06007B1A RID: 31514 RVA: 0x001F92C7 File Offset: 0x001F74C7
		private static void TrackerAppendLine(StringBuilder changedValuesTracker, string line)
		{
			if (changedValuesTracker != null)
			{
				changedValuesTracker.AppendLine(line);
			}
		}

		// Token: 0x04003CCB RID: 15563
		public const int MaxSuspendCommentLength = 4096;

		// Token: 0x04003CCC RID: 15564
		public const int MaxBatchNameLength = 255;

		// Token: 0x04003CCD RID: 15565
		public const int MaxNameLength = 255;

		// Token: 0x04003CCE RID: 15566
		public const int StartAfterMaxDaysFromNow = 30;

		// Token: 0x04003CCF RID: 15567
		public const int CompleteAfterMaxDaysFromNow = 120;

		// Token: 0x04003CD0 RID: 15568
		public const string ParameterSourceStoreMailbox = "SourceStoreMailbox";

		// Token: 0x04003CD1 RID: 15569
		public const string ParameterSourceDatabase = "SourceDatabase";

		// Token: 0x04003CD2 RID: 15570
		public const string ParameterTargetMailbox = "TargetMailbox";

		// Token: 0x04003CD3 RID: 15571
		public const string ParameterSourceRootFolder = "SourceRootFolder";

		// Token: 0x04003CD4 RID: 15572
		public const string ParameterTargetRootFolder = "TargetRootFolder";

		// Token: 0x04003CD5 RID: 15573
		public const string ParameterTargetIsArchive = "TargetIsArchive";

		// Token: 0x04003CD6 RID: 15574
		public const string ParameterTargetDatabase = "TargetDatabase";

		// Token: 0x04003CD7 RID: 15575
		public const string ParameterArchiveTargetDatabase = "ArchiveTargetDatabase";

		// Token: 0x04003CD8 RID: 15576
		public const string ParameterPrimaryOnly = "PrimaryOnly";

		// Token: 0x04003CD9 RID: 15577
		public const string ParameterArchiveOnly = "ArchiveOnly";

		// Token: 0x04003CDA RID: 15578
		public const string ParameterForcePull = "ForcePull";

		// Token: 0x04003CDB RID: 15579
		public const string ParameterForcePush = "ForcePush";

		// Token: 0x04003CDC RID: 15580
		public const string ParameterRemoteTargetDatabase = "RemoteTargetDatabase";

		// Token: 0x04003CDD RID: 15581
		public const string ParameterRemoteArchiveTargetDatabase = "RemoteArchiveTargetDatabase";

		// Token: 0x04003CDE RID: 15582
		public const string ParameterRemoteDatabaseGuid = "RemoteDatabaseGuid";

		// Token: 0x04003CDF RID: 15583
		public const string ParameterRemoteRestoreType = "RemoteRestoreType";

		// Token: 0x04003CE0 RID: 15584
		public const string ParameterRemote = "Remote";

		// Token: 0x04003CE1 RID: 15585
		public const string ParameterOutbound = "Outbound";

		// Token: 0x04003CE2 RID: 15586
		public const string ParameterRemoteLegacy = "RemoteLegacy";

		// Token: 0x04003CE3 RID: 15587
		public const string ParameterRemoteGlobalCatalog = "RemoteGlobalCatalog";

		// Token: 0x04003CE4 RID: 15588
		public const string ParameterBadItemLimit = "BadItemLimit";

		// Token: 0x04003CE5 RID: 15589
		public const string ParameterLargeItemLimit = "LargeItemLimit";

		// Token: 0x04003CE6 RID: 15590
		public const string ParameterAllowLargeItems = "AllowLargeItems";

		// Token: 0x04003CE7 RID: 15591
		public const string ParameterIgnoreTenantMigrationPolicies = "IgnoreTenantMigrationPolicies";

		// Token: 0x04003CE8 RID: 15592
		public const string ParameterAcceptLargeDataLoss = "AcceptLargeDataLoss";

		// Token: 0x04003CE9 RID: 15593
		public const string ParameterCheckInitialProvisioningSetting = "CheckInitialProvisioningSetting";

		// Token: 0x04003CEA RID: 15594
		public const string ParameterRemoteHostName = "RemoteHostName";

		// Token: 0x04003CEB RID: 15595
		public const string ParameterBatchName = "BatchName";

		// Token: 0x04003CEC RID: 15596
		public const string ParameterRemoteOrganizationName = "RemoteOrganizationName";

		// Token: 0x04003CED RID: 15597
		public const string ParameterArchiveDomain = "ArchiveDomain";

		// Token: 0x04003CEE RID: 15598
		public const string ParameterRemoteCredential = "RemoteCredential";

		// Token: 0x04003CEF RID: 15599
		public const string ParameterProtect = "Protect";

		// Token: 0x04003CF0 RID: 15600
		public const string ParameterIdentity = "Identity";

		// Token: 0x04003CF1 RID: 15601
		public const string ParameterSuspendWhenReadyToComplete = "SuspendWhenReadyToComplete";

		// Token: 0x04003CF2 RID: 15602
		public const string ParameterSuspend = "Suspend";

		// Token: 0x04003CF3 RID: 15603
		public const string ParameterSuspendComment = "SuspendComment";

		// Token: 0x04003CF4 RID: 15604
		public const string ParameterIgnoreRuleLimitErrors = "IgnoreRuleLimitErrors";

		// Token: 0x04003CF5 RID: 15605
		public const string ParameterDoNotPreserveMailboxSignature = "DoNotPreserveMailboxSignature";

		// Token: 0x04003CF6 RID: 15606
		public const string ParameterTargetDeliveryDomain = "TargetDeliveryDomain";

		// Token: 0x04003CF7 RID: 15607
		public const string ParameterPriority = "Priority";

		// Token: 0x04003CF8 RID: 15608
		public const string ParameterWorkloadType = "WorkloadType";

		// Token: 0x04003CF9 RID: 15609
		public const string ParameterCompletedRequestAgeLimit = "CompletedRequestAgeLimit";

		// Token: 0x04003CFA RID: 15610
		public const string ParameterForceOffline = "ForceOffline";

		// Token: 0x04003CFB RID: 15611
		public const string ParameterPreventCompletion = "PreventCompletion";

		// Token: 0x04003CFC RID: 15612
		public const string ParameterSkipMoving = "SkipMoving";

		// Token: 0x04003CFD RID: 15613
		public const string ParameterInternalFlags = "InternalFlags";

		// Token: 0x04003CFE RID: 15614
		public const string ParameterStartAfter = "StartAfter";

		// Token: 0x04003CFF RID: 15615
		public const string ParameterCompleteAfter = "CompleteAfter";

		// Token: 0x04003D00 RID: 15616
		public const string ParameterIncrementalSyncInterval = "IncrementalSyncInterval";

		// Token: 0x04003D01 RID: 15617
		public const string ParameterAllowLegacyDNMismatch = "AllowLegacyDNMismatch";

		// Token: 0x04003D02 RID: 15618
		public const string ParameterName = "Name";

		// Token: 0x04003D03 RID: 15619
		public const string ParameterContentFilter = "ContentFilter";

		// Token: 0x04003D04 RID: 15620
		public const string ParameterContentFilterLanguage = "ContentFilterLanguage";

		// Token: 0x04003D05 RID: 15621
		public const string ParameterIncludeFolders = "IncludeFolders";

		// Token: 0x04003D06 RID: 15622
		public const string ParameterExcludeFolders = "ExcludeFolders";

		// Token: 0x04003D07 RID: 15623
		public const string ParameterExcludeDumpster = "ExcludeDumpster";

		// Token: 0x04003D08 RID: 15624
		public const string ParameterConflictResolutionOption = "ConflictResolutionOption";

		// Token: 0x04003D09 RID: 15625
		public const string ParameterAssociatedMessagesCopyOption = "AssociatedMessagesCopyOption";

		// Token: 0x04003D0A RID: 15626
		public const string ParameterSkipMerging = "SkipMerging";

		// Token: 0x04003D0B RID: 15627
		public const string TaskNoun = "MailboxImportRequest";

		// Token: 0x04003D0C RID: 15628
		public const string ParameterContentCodePage = "ContentCodePage";

		// Token: 0x04003D0D RID: 15629
		public const string ParameterMailbox = "Mailbox";

		// Token: 0x04003D0E RID: 15630
		public const string ParameterFilePath = "FilePath";

		// Token: 0x04003D0F RID: 15631
		public const string ParameterIsArchive = "IsArchive";

		// Token: 0x04003D10 RID: 15632
		public const string ParameterRehomeRequest = "RehomeRequest";

		// Token: 0x04003D11 RID: 15633
		public static readonly Unlimited<int> UnlimitedZero = new Unlimited<int>(0);

		// Token: 0x04003D12 RID: 15634
		public static readonly Unlimited<EnhancedTimeSpan> DefaultCompletedRequestAgeLimit = TestIntegration.Instance.GetCompletedRequestAgeLimit(TimeSpan.FromDays(30.0));
	}
}
