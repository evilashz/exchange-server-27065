using System;
using System.Collections.Generic;
using System.Net;
using System.Reflection;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000299 RID: 665
	internal static class MrsStrings
	{
		// Token: 0x06002041 RID: 8257 RVA: 0x00044E3C File Offset: 0x0004303C
		static MrsStrings()
		{
			MrsStrings.stringIDs.Add(1424238063U, "InvalidRequestJob");
			MrsStrings.stringIDs.Add(392888166U, "WorkloadTypeOnboarding");
			MrsStrings.stringIDs.Add(3088101088U, "ReportMessagesCopied");
			MrsStrings.stringIDs.Add(3176193428U, "DestMailboxAlreadyBeingMoved");
			MrsStrings.stringIDs.Add(3671912657U, "ReportDestinationSDCannotBeRead");
			MrsStrings.stringIDs.Add(3429098023U, "ServiceIsStopping");
			MrsStrings.stringIDs.Add(1833949493U, "PSTPathMustBeAFile");
			MrsStrings.stringIDs.Add(3825549875U, "ReportMovedMailboxAlreadyMorphedToMailUser");
			MrsStrings.stringIDs.Add(3369317625U, "UnableToReadAD");
			MrsStrings.stringIDs.Add(1476056932U, "MoveRestartDueToContainerMailboxesChanged");
			MrsStrings.stringIDs.Add(3728110147U, "ReportCopyPerUserReadUnreadDataStarted");
			MrsStrings.stringIDs.Add(1704413971U, "JobCannotBeRehomedWhenInProgress");
			MrsStrings.stringIDs.Add(2750851982U, "FolderHierarchyIsInconsistent");
			MrsStrings.stringIDs.Add(3945110826U, "ReportMoveRestartedDueToSignatureChange");
			MrsStrings.stringIDs.Add(3089757325U, "ErrorCannotPreventCompletionForCompletingMove");
			MrsStrings.stringIDs.Add(4185133072U, "WorkloadTypeOffboarding");
			MrsStrings.stringIDs.Add(979363606U, "MRSAlreadyConfigured");
			MrsStrings.stringIDs.Add(135363966U, "ReportTargetPublicFolderDeploymentUnlocked");
			MrsStrings.stringIDs.Add(815599557U, "ReportRequestCancelPostponed");
			MrsStrings.stringIDs.Add(1388232024U, "JobHasBeenRelinquishedDueToHAStall");
			MrsStrings.stringIDs.Add(738613247U, "RequestPriorityLow");
			MrsStrings.stringIDs.Add(1580421706U, "MoveRequestDirectionPull");
			MrsStrings.stringIDs.Add(119144040U, "UnableToApplyFolderHierarchyChanges");
			MrsStrings.stringIDs.Add(3837251784U, "PickupStatusDisabled");
			MrsStrings.stringIDs.Add(2288253100U, "RemoteResource");
			MrsStrings.stringIDs.Add(2018114904U, "MoveRestartedDueToSignatureChange");
			MrsStrings.stringIDs.Add(3406658852U, "FolderHierarchyContainsNoRoots");
			MrsStrings.stringIDs.Add(2360217149U, "JobHasBeenRelinquishedDueToCIStall");
			MrsStrings.stringIDs.Add(2713483251U, "ContentIndexing");
			MrsStrings.stringIDs.Add(3235245142U, "TooManyLargeItems");
			MrsStrings.stringIDs.Add(2888971502U, "CouldNotConnectToTargetMailbox");
			MrsStrings.stringIDs.Add(1886860910U, "PSTIOException");
			MrsStrings.stringIDs.Add(2240969000U, "RequestPriorityNormal");
			MrsStrings.stringIDs.Add(268267125U, "SmtpServerInfoMissing");
			MrsStrings.stringIDs.Add(4282140830U, "NoPublicFolderMailboxFoundInSource");
			MrsStrings.stringIDs.Add(3551669574U, "WorkloadTypeRemotePstExport");
			MrsStrings.stringIDs.Add(3345143506U, "FastTransferArgumentError");
			MrsStrings.stringIDs.Add(3435030616U, "PickupStatusCompletedJob");
			MrsStrings.stringIDs.Add(856796896U, "ReportJobProcessingDisabled");
			MrsStrings.stringIDs.Add(1247936583U, "ImproperTypeForThisIdParameter");
			MrsStrings.stringIDs.Add(2006276195U, "MoveRequestMissingInfoDelete");
			MrsStrings.stringIDs.Add(3798869933U, "ReportRelinquishingJobDueToServiceStop");
			MrsStrings.stringIDs.Add(1827127136U, "PickupStatusCorruptJob");
			MrsStrings.stringIDs.Add(4161853231U, "RequestHasBeenRelinquishedDueToBadHealthOfBackendServers");
			MrsStrings.stringIDs.Add(2260777903U, "MoveRequestMissingInfoSave");
			MrsStrings.stringIDs.Add(3708951374U, "RestartingMove");
			MrsStrings.stringIDs.Add(3631041786U, "ErrorWhileUpdatingMovedMailbox");
			MrsStrings.stringIDs.Add(1320326778U, "MoveRequestValidationFailed");
			MrsStrings.stringIDs.Add(199165212U, "MustProvideValidSessionForFindingRequests");
			MrsStrings.stringIDs.Add(4003924173U, "TooManyMissingItems");
			MrsStrings.stringIDs.Add(4230582602U, "UpdateFolderFailed");
			MrsStrings.stringIDs.Add(990473213U, "OfflinePublicFolderMigrationNotSupported");
			MrsStrings.stringIDs.Add(2906979272U, "TaskCanceled");
			MrsStrings.stringIDs.Add(2323682581U, "SourceMailboxAlreadyBeingMoved");
			MrsStrings.stringIDs.Add(938866594U, "MoveJobDeserializationFailed");
			MrsStrings.stringIDs.Add(3959740005U, "MoveRequestNotFoundInQueue");
			MrsStrings.stringIDs.Add(2697508630U, "JobHasBeenCanceled");
			MrsStrings.stringIDs.Add(1085885350U, "ReportRequestStarted");
			MrsStrings.stringIDs.Add(90093709U, "ErrorDownlevelClientsNotSupported");
			MrsStrings.stringIDs.Add(2295784267U, "DataExportTimeout");
			MrsStrings.stringIDs.Add(2935411904U, "TargetMailboxConnectionWasLost");
			MrsStrings.stringIDs.Add(2247143358U, "JobHasBeenRelinquishedDueToDatabaseFailover");
			MrsStrings.stringIDs.Add(3942653727U, "PublicFolderMailboxesNotProvisionedForMigration");
			MrsStrings.stringIDs.Add(3540791304U, "RequestPriorityHigher");
			MrsStrings.stringIDs.Add(1147283780U, "JobHasBeenRelinquishedDueToHAOrCIStalls");
			MrsStrings.stringIDs.Add(1573145718U, "ReportRequestCanceled");
			MrsStrings.stringIDs.Add(4082292636U, "InvalidProxyOperationOrder");
			MrsStrings.stringIDs.Add(2286820319U, "ReportRequestOfflineMovePostponed");
			MrsStrings.stringIDs.Add(3759213604U, "MailboxIsBeingMoved");
			MrsStrings.stringIDs.Add(3604568438U, "NoSuchRequestInSpecifiedIndex");
			MrsStrings.stringIDs.Add(859003787U, "InitializedWithInvalidObjectId");
			MrsStrings.stringIDs.Add(215436927U, "ReportCopyPerUserReadUnreadDataCompleted");
			MrsStrings.stringIDs.Add(699419898U, "ReportSessionStatisticsUpdated");
			MrsStrings.stringIDs.Add(2708825588U, "ReportRelinquishingJobDueToServerThrottling");
			MrsStrings.stringIDs.Add(3162969353U, "MRSNotConfigured");
			MrsStrings.stringIDs.Add(2244475911U, "MailboxRootFolderNotFound");
			MrsStrings.stringIDs.Add(540485114U, "WorkloadTypeLoadBalancing");
			MrsStrings.stringIDs.Add(1035843159U, "JobIsQuarantined");
			MrsStrings.stringIDs.Add(2538826952U, "ReportSourceSDCannotBeRead");
			MrsStrings.stringIDs.Add(2850410499U, "ReportMoveRequestIsNoLongerSticky");
			MrsStrings.stringIDs.Add(1606949857U, "ClusterNotFound");
			MrsStrings.stringIDs.Add(833630790U, "MoveRestartDueToIsIntegCheck");
			MrsStrings.stringIDs.Add(952727642U, "ReportJobIsStillStalled");
			MrsStrings.stringIDs.Add(4023361926U, "WorkloadTypeRemotePstIngestion");
			MrsStrings.stringIDs.Add(472899259U, "ReportPrimaryMservEntryPointsToExo");
			MrsStrings.stringIDs.Add(2750716986U, "ValidationADUserIsNotBeingMoved");
			MrsStrings.stringIDs.Add(702077469U, "PostMoveStateIsUncertain");
			MrsStrings.stringIDs.Add(901032809U, "RequestPriorityHigh");
			MrsStrings.stringIDs.Add(888446530U, "SourceContainer");
			MrsStrings.stringIDs.Add(3595573419U, "WorkloadTypeTenantUpgrade");
			MrsStrings.stringIDs.Add(3655041298U, "EasMissingMessageCategory");
			MrsStrings.stringIDs.Add(336070114U, "JobHasBeenRelinquished");
			MrsStrings.stringIDs.Add(2930966715U, "RecoverySyncNotImplemented");
			MrsStrings.stringIDs.Add(1123448769U, "ErrorTooManyCleanupRetries");
			MrsStrings.stringIDs.Add(3238364772U, "ReportFinalSyncStarted");
			MrsStrings.stringIDs.Add(1078342972U, "ReportJobExitedStalledByThrottlingState");
			MrsStrings.stringIDs.Add(1302133406U, "MustProvideNonEmptyStringForIdentity");
			MrsStrings.stringIDs.Add(2181380613U, "ReportRelinquishingJobDueToNeedForRehome");
			MrsStrings.stringIDs.Add(2761303157U, "NotEnoughInformationSupplied");
			MrsStrings.stringIDs.Add(4239850164U, "NoDataContext");
			MrsStrings.stringIDs.Add(2023474786U, "ReportMoveCompleted");
			MrsStrings.stringIDs.Add(4145210966U, "UnableToDeleteMoveRequestMessage");
			MrsStrings.stringIDs.Add(4239846426U, "DestinationFolderHierarchyInconsistent");
			MrsStrings.stringIDs.Add(4095394241U, "NotEnoughInformationToFindMoveRequest");
			MrsStrings.stringIDs.Add(949547255U, "TaskSchedulerStopped");
			MrsStrings.stringIDs.Add(1292011054U, "ReportRelinquishingJobDueToCIStall");
			MrsStrings.stringIDs.Add(45208631U, "WriteCpu");
			MrsStrings.stringIDs.Add(73509179U, "ReportHomeMdbPointsToTarget");
			MrsStrings.stringIDs.Add(672463457U, "CorruptRestrictionData");
			MrsStrings.stringIDs.Add(1194778775U, "ReportIncrementalMoveRestartDueToGlobalCounterRangeDepletion");
			MrsStrings.stringIDs.Add(3109918443U, "ReadRpc");
			MrsStrings.stringIDs.Add(1212084898U, "WorkloadTypeLocal");
			MrsStrings.stringIDs.Add(3906020551U, "MoveRequestDirectionPush");
			MrsStrings.stringIDs.Add(3213196515U, "SourceFolderHierarchyInconsistent");
			MrsStrings.stringIDs.Add(2170397003U, "RequestPriorityHighest");
			MrsStrings.stringIDs.Add(1787602764U, "ErrorFinalizationIsBlocked");
			MrsStrings.stringIDs.Add(1410610418U, "MoveRequestTypeCrossOrg");
			MrsStrings.stringIDs.Add(1554654695U, "MrsProxyServiceIsDisabled");
			MrsStrings.stringIDs.Add(2497488330U, "ReportMoveCanceled");
			MrsStrings.stringIDs.Add(1153630962U, "ErrorCannotPreventCompletionForOfflineMove");
			MrsStrings.stringIDs.Add(3994387752U, "RequestHasBeenPostponedDueToBadHealthOfBackendServers2");
			MrsStrings.stringIDs.Add(1608330969U, "ValidationNoCorrespondingIndexEntries");
			MrsStrings.stringIDs.Add(1728063749U, "InvalidSyncStateData");
			MrsStrings.stringIDs.Add(874970588U, "ReportMoveRestartedDueToSourceCorruption");
			MrsStrings.stringIDs.Add(3603681089U, "JobHasBeenAutoSuspended");
			MrsStrings.stringIDs.Add(3843865613U, "InputDataIsInvalid");
			MrsStrings.stringIDs.Add(3287770946U, "ReportJobExitedStalledState");
			MrsStrings.stringIDs.Add(1094055921U, "ActionNotSupported");
			MrsStrings.stringIDs.Add(1690707778U, "ReportTargetAuxFolderContentMailboxGuidUpdated");
			MrsStrings.stringIDs.Add(1049832713U, "ReportStoreMailboxHasFinalized");
			MrsStrings.stringIDs.Add(1157208427U, "ErrorReservationExpired");
			MrsStrings.stringIDs.Add(3856642209U, "ErrorImplicitSplit");
			MrsStrings.stringIDs.Add(1609645991U, "ReportRelinquishingJob");
			MrsStrings.stringIDs.Add(2756637510U, "CouldNotConnectToSourceMailbox");
			MrsStrings.stringIDs.Add(2671521794U, "NoFoldersIncluded");
			MrsStrings.stringIDs.Add(780406631U, "ReportSuspendingJob");
			MrsStrings.stringIDs.Add(1228469352U, "WriteRpc");
			MrsStrings.stringIDs.Add(2309811282U, "NotConnected");
			MrsStrings.stringIDs.Add(4081437335U, "MdbReplication");
			MrsStrings.stringIDs.Add(1499253095U, "ReportRulesWillNotBeCopied");
			MrsStrings.stringIDs.Add(772970447U, "ReportSkippingUpdateSourceMailbox");
			MrsStrings.stringIDs.Add(2051604558U, "ErrorEmptyMailboxGuid");
			MrsStrings.stringIDs.Add(4227707103U, "ReportArchiveAlreadyUpdated");
			MrsStrings.stringIDs.Add(3167108423U, "JobHasBeenSynced");
			MrsStrings.stringIDs.Add(300694364U, "JobHasBeenRelinquishedDueToLongRun");
			MrsStrings.stringIDs.Add(2739554404U, "ReportRelinquishingJobDueToHAOrCIStalling");
			MrsStrings.stringIDs.Add(3709264734U, "Mailbox");
			MrsStrings.stringIDs.Add(2181899002U, "FolderHierarchyIsInconsistentTemporarily");
			MrsStrings.stringIDs.Add(1729678064U, "RequestPriorityEmergency");
			MrsStrings.stringIDs.Add(3018124355U, "ReportRelinquishingJobDueToHAStall");
			MrsStrings.stringIDs.Add(2466192281U, "CorruptSyncState");
			MrsStrings.stringIDs.Add(1574793031U, "ReportTargetPublicFolderContentMailboxGuidUpdated");
			MrsStrings.stringIDs.Add(551973766U, "NoMRSAvailable");
			MrsStrings.stringIDs.Add(3329536416U, "RequestPriorityLower");
			MrsStrings.stringIDs.Add(2648214014U, "ReportAutoSuspendingJob");
			MrsStrings.stringIDs.Add(123753966U, "ReportCalendarFolderFaiSaveFailed");
			MrsStrings.stringIDs.Add(2000650826U, "MoveIsPreventedFromFinalization");
			MrsStrings.stringIDs.Add(615074259U, "ReportMoveAlreadyFinished");
			MrsStrings.stringIDs.Add(3151307821U, "RehomeRequestFailure");
			MrsStrings.stringIDs.Add(2161771148U, "RequestIsStalledByHigherPriorityJobs");
			MrsStrings.stringIDs.Add(951459652U, "WorkloadTypeEmergency");
			MrsStrings.stringIDs.Add(1237434822U, "ReportCalendarFolderSaveFailed");
			MrsStrings.stringIDs.Add(2993893239U, "MRSProxyConnectionNotThrottledError");
			MrsStrings.stringIDs.Add(3133742332U, "ReportWaitingForMailboxDataReplication");
			MrsStrings.stringIDs.Add(1628850222U, "ReportDatabaseFailedOver");
			MrsStrings.stringIDs.Add(1268966663U, "FolderIsMissingInMerge");
			MrsStrings.stringIDs.Add(2846264340U, "Unknown");
			MrsStrings.stringIDs.Add(681951690U, "WorkloadTypeSyncAggregation");
			MrsStrings.stringIDs.Add(1347847794U, "ReportTargetUserIsNotMailEnabledUser");
			MrsStrings.stringIDs.Add(676706684U, "ReportRequestIsNoLongerSticky");
			MrsStrings.stringIDs.Add(3237229570U, "MoveRequestTypeIntraOrg");
			MrsStrings.stringIDs.Add(3107299645U, "ValidationMoveRequestNotDeserialized");
			MrsStrings.stringIDs.Add(2605163142U, "ReportMoveStarted");
			MrsStrings.stringIDs.Add(1655277830U, "ReportPostMoveCleanupStarted");
			MrsStrings.stringIDs.Add(704248557U, "InternalAccessFolderCreationIsNotSupported");
			MrsStrings.stringIDs.Add(1464484042U, "ReportRequestCompleted");
			MrsStrings.stringIDs.Add(3109918916U, "ReadCpu");
			MrsStrings.stringIDs.Add(202150486U, "TargetContainer");
			MrsStrings.stringIDs.Add(4026185433U, "RequestPriorityLowest");
			MrsStrings.stringIDs.Add(630728405U, "WorkloadTypeNone");
			MrsStrings.stringIDs.Add(4156705784U, "UnableToObtainServersInLocalSite");
			MrsStrings.stringIDs.Add(3403189953U, "WrongUserObjectFound");
			MrsStrings.stringIDs.Add(3829274068U, "GetIdsFromNamesCalledOnDestination");
			MrsStrings.stringIDs.Add(1551771611U, "DataExportCanceled");
			MrsStrings.stringIDs.Add(3775277574U, "TooManyBadItems");
			MrsStrings.stringIDs.Add(1220106367U, "ReportVerifyingMailboxContents");
		}

		// Token: 0x06002042 RID: 8258 RVA: 0x00045D04 File Offset: 0x00043F04
		public static LocalizedString EasFolderSyncFailed(string errorMessage)
		{
			return new LocalizedString("EasFolderSyncFailed", "", false, false, MrsStrings.ResourceManager, new object[]
			{
				errorMessage
			});
		}

		// Token: 0x17000C3B RID: 3131
		// (get) Token: 0x06002043 RID: 8259 RVA: 0x00045D33 File Offset: 0x00043F33
		public static LocalizedString InvalidRequestJob
		{
			get
			{
				return new LocalizedString("InvalidRequestJob", "", false, false, MrsStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06002044 RID: 8260 RVA: 0x00045D54 File Offset: 0x00043F54
		public static LocalizedString UnableToOpenMailbox(string serverName)
		{
			return new LocalizedString("UnableToOpenMailbox", "Ex72E5B4", false, true, MrsStrings.ResourceManager, new object[]
			{
				serverName
			});
		}

		// Token: 0x17000C3C RID: 3132
		// (get) Token: 0x06002045 RID: 8261 RVA: 0x00045D83 File Offset: 0x00043F83
		public static LocalizedString WorkloadTypeOnboarding
		{
			get
			{
				return new LocalizedString("WorkloadTypeOnboarding", "", false, false, MrsStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000C3D RID: 3133
		// (get) Token: 0x06002046 RID: 8262 RVA: 0x00045DA1 File Offset: 0x00043FA1
		public static LocalizedString ReportMessagesCopied
		{
			get
			{
				return new LocalizedString("ReportMessagesCopied", "", false, false, MrsStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06002047 RID: 8263 RVA: 0x00045DC0 File Offset: 0x00043FC0
		public static LocalizedString ServerError(string serviceURI, LocalizedString exceptionMessage)
		{
			return new LocalizedString("ServerError", "Ex25D79D", false, true, MrsStrings.ResourceManager, new object[]
			{
				serviceURI,
				exceptionMessage
			});
		}

		// Token: 0x17000C3E RID: 3134
		// (get) Token: 0x06002048 RID: 8264 RVA: 0x00045DF8 File Offset: 0x00043FF8
		public static LocalizedString DestMailboxAlreadyBeingMoved
		{
			get
			{
				return new LocalizedString("DestMailboxAlreadyBeingMoved", "ExF7DBE2", false, true, MrsStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06002049 RID: 8265 RVA: 0x00045E18 File Offset: 0x00044018
		public static LocalizedString ReportSyncStateNull(Guid requestGuid)
		{
			return new LocalizedString("ReportSyncStateNull", "", false, false, MrsStrings.ResourceManager, new object[]
			{
				requestGuid
			});
		}

		// Token: 0x0600204A RID: 8266 RVA: 0x00045E4C File Offset: 0x0004404C
		public static LocalizedString RecipientArchiveGuidMismatch(string recipient, Guid recipientArchiveGuid, Guid targetArchiveGuid)
		{
			return new LocalizedString("RecipientArchiveGuidMismatch", "Ex71CCCD", false, true, MrsStrings.ResourceManager, new object[]
			{
				recipient,
				recipientArchiveGuid,
				targetArchiveGuid
			});
		}

		// Token: 0x0600204B RID: 8267 RVA: 0x00045E90 File Offset: 0x00044090
		public static LocalizedString ReportDestinationMailboxResetSucceeded(LocalizedString mailboxId)
		{
			return new LocalizedString("ReportDestinationMailboxResetSucceeded", "ExF5A6D0", false, true, MrsStrings.ResourceManager, new object[]
			{
				mailboxId
			});
		}

		// Token: 0x0600204C RID: 8268 RVA: 0x00045EC4 File Offset: 0x000440C4
		public static LocalizedString ReportMovedMailboxUpdated(string domainController)
		{
			return new LocalizedString("ReportMovedMailboxUpdated", "", false, false, MrsStrings.ResourceManager, new object[]
			{
				domainController
			});
		}

		// Token: 0x17000C3F RID: 3135
		// (get) Token: 0x0600204D RID: 8269 RVA: 0x00045EF3 File Offset: 0x000440F3
		public static LocalizedString ReportDestinationSDCannotBeRead
		{
			get
			{
				return new LocalizedString("ReportDestinationSDCannotBeRead", "", false, false, MrsStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600204E RID: 8270 RVA: 0x00045F14 File Offset: 0x00044114
		public static LocalizedString SystemMailboxNotFound(string systemMailboxName)
		{
			return new LocalizedString("SystemMailboxNotFound", "Ex501017", false, true, MrsStrings.ResourceManager, new object[]
			{
				systemMailboxName
			});
		}

		// Token: 0x17000C40 RID: 3136
		// (get) Token: 0x0600204F RID: 8271 RVA: 0x00045F43 File Offset: 0x00044143
		public static LocalizedString ServiceIsStopping
		{
			get
			{
				return new LocalizedString("ServiceIsStopping", "", false, false, MrsStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06002050 RID: 8272 RVA: 0x00045F64 File Offset: 0x00044164
		public static LocalizedString ReportRemovingTargetMailboxDueToOfflineMoveFailure(LocalizedString mbxId)
		{
			return new LocalizedString("ReportRemovingTargetMailboxDueToOfflineMoveFailure", "", false, false, MrsStrings.ResourceManager, new object[]
			{
				mbxId
			});
		}

		// Token: 0x06002051 RID: 8273 RVA: 0x00045F98 File Offset: 0x00044198
		public static LocalizedString TimeoutError(string serviceURI, LocalizedString exceptionMessage)
		{
			return new LocalizedString("TimeoutError", "ExAA9BE9", false, true, MrsStrings.ResourceManager, new object[]
			{
				serviceURI,
				exceptionMessage
			});
		}

		// Token: 0x06002052 RID: 8274 RVA: 0x00045FD0 File Offset: 0x000441D0
		public static LocalizedString ReportMailboxBeforeFinalization2(string userID, string domainControllerName)
		{
			return new LocalizedString("ReportMailboxBeforeFinalization2", "", false, false, MrsStrings.ResourceManager, new object[]
			{
				userID,
				domainControllerName
			});
		}

		// Token: 0x17000C41 RID: 3137
		// (get) Token: 0x06002053 RID: 8275 RVA: 0x00046003 File Offset: 0x00044203
		public static LocalizedString PSTPathMustBeAFile
		{
			get
			{
				return new LocalizedString("PSTPathMustBeAFile", "Ex22ECEF", false, true, MrsStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06002054 RID: 8276 RVA: 0x00046024 File Offset: 0x00044224
		public static LocalizedString MoveHasBeenSynced(Guid mbxGuid, DateTime nextIncremental)
		{
			return new LocalizedString("MoveHasBeenSynced", "", false, false, MrsStrings.ResourceManager, new object[]
			{
				mbxGuid,
				nextIncremental
			});
		}

		// Token: 0x06002055 RID: 8277 RVA: 0x00046064 File Offset: 0x00044264
		public static LocalizedString MoveCancelFailedForAlreadyCompletedMove(Guid mbxGuid)
		{
			return new LocalizedString("MoveCancelFailedForAlreadyCompletedMove", "ExBA867E", false, true, MrsStrings.ResourceManager, new object[]
			{
				mbxGuid
			});
		}

		// Token: 0x06002056 RID: 8278 RVA: 0x00046098 File Offset: 0x00044298
		public static LocalizedString RPCHTTPPublicFoldersId(string legDN)
		{
			return new LocalizedString("RPCHTTPPublicFoldersId", "ExC426EA", false, true, MrsStrings.ResourceManager, new object[]
			{
				legDN
			});
		}

		// Token: 0x06002057 RID: 8279 RVA: 0x000460C8 File Offset: 0x000442C8
		public static LocalizedString ReportThrottles(string mdbThrottle, string cpuThrottle, string mdbReplicationThrottle, string contentIndexingThrottle, string unknownThrottle)
		{
			return new LocalizedString("ReportThrottles", "", false, false, MrsStrings.ResourceManager, new object[]
			{
				mdbThrottle,
				cpuThrottle,
				mdbReplicationThrottle,
				contentIndexingThrottle,
				unknownThrottle
			});
		}

		// Token: 0x06002058 RID: 8280 RVA: 0x00046108 File Offset: 0x00044308
		public static LocalizedString ReportMailboxInfoAfterMove(string mailboxInfoString)
		{
			return new LocalizedString("ReportMailboxInfoAfterMove", "ExDC727C", false, true, MrsStrings.ResourceManager, new object[]
			{
				mailboxInfoString
			});
		}

		// Token: 0x17000C42 RID: 3138
		// (get) Token: 0x06002059 RID: 8281 RVA: 0x00046137 File Offset: 0x00044337
		public static LocalizedString ReportMovedMailboxAlreadyMorphedToMailUser
		{
			get
			{
				return new LocalizedString("ReportMovedMailboxAlreadyMorphedToMailUser", "", false, false, MrsStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000C43 RID: 3139
		// (get) Token: 0x0600205A RID: 8282 RVA: 0x00046155 File Offset: 0x00044355
		public static LocalizedString UnableToReadAD
		{
			get
			{
				return new LocalizedString("UnableToReadAD", "Ex994787", false, true, MrsStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000C44 RID: 3140
		// (get) Token: 0x0600205B RID: 8283 RVA: 0x00046173 File Offset: 0x00044373
		public static LocalizedString MoveRestartDueToContainerMailboxesChanged
		{
			get
			{
				return new LocalizedString("MoveRestartDueToContainerMailboxesChanged", "", false, false, MrsStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000C45 RID: 3141
		// (get) Token: 0x0600205C RID: 8284 RVA: 0x00046191 File Offset: 0x00044391
		public static LocalizedString ReportCopyPerUserReadUnreadDataStarted
		{
			get
			{
				return new LocalizedString("ReportCopyPerUserReadUnreadDataStarted", "", false, false, MrsStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600205D RID: 8285 RVA: 0x000461B0 File Offset: 0x000443B0
		public static LocalizedString ReportSourceMailboxBeforeFinalization2(string userID, string domainControllerName)
		{
			return new LocalizedString("ReportSourceMailboxBeforeFinalization2", "", false, false, MrsStrings.ResourceManager, new object[]
			{
				userID,
				domainControllerName
			});
		}

		// Token: 0x17000C46 RID: 3142
		// (get) Token: 0x0600205E RID: 8286 RVA: 0x000461E3 File Offset: 0x000443E3
		public static LocalizedString JobCannotBeRehomedWhenInProgress
		{
			get
			{
				return new LocalizedString("JobCannotBeRehomedWhenInProgress", "", false, false, MrsStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600205F RID: 8287 RVA: 0x00046204 File Offset: 0x00044404
		public static LocalizedString RulesDataContext(string rulesStr)
		{
			return new LocalizedString("RulesDataContext", "Ex62EFB6", false, true, MrsStrings.ResourceManager, new object[]
			{
				rulesStr
			});
		}

		// Token: 0x06002060 RID: 8288 RVA: 0x00046234 File Offset: 0x00044434
		public static LocalizedString UnableToGetPSTProps(string filePath)
		{
			return new LocalizedString("UnableToGetPSTProps", "ExF6CDBC", false, true, MrsStrings.ResourceManager, new object[]
			{
				filePath
			});
		}

		// Token: 0x06002061 RID: 8289 RVA: 0x00046264 File Offset: 0x00044464
		public static LocalizedString ReportFailedToDisconnectFromSource2(string errorType)
		{
			return new LocalizedString("ReportFailedToDisconnectFromSource2", "", false, false, MrsStrings.ResourceManager, new object[]
			{
				errorType
			});
		}

		// Token: 0x06002062 RID: 8290 RVA: 0x00046294 File Offset: 0x00044494
		public static LocalizedString ReportJobHasBeenRelinquishedDueToServerBusy(DateTime pickupTime)
		{
			return new LocalizedString("ReportJobHasBeenRelinquishedDueToServerBusy", "", false, false, MrsStrings.ResourceManager, new object[]
			{
				pickupTime
			});
		}

		// Token: 0x06002063 RID: 8291 RVA: 0x000462C8 File Offset: 0x000444C8
		public static LocalizedString ReportDestinationMailboxClearSyncStateFailed(string errorType, LocalizedString errorMsg, string trace, int currentAttempt, int totalAttempts)
		{
			return new LocalizedString("ReportDestinationMailboxClearSyncStateFailed", "Ex088797", false, true, MrsStrings.ResourceManager, new object[]
			{
				errorType,
				errorMsg,
				trace,
				currentAttempt,
				totalAttempts
			});
		}

		// Token: 0x06002064 RID: 8292 RVA: 0x00046318 File Offset: 0x00044518
		public static LocalizedString MoveCompleteFailedForAlreadyFailedMove(Guid mbxGuid)
		{
			return new LocalizedString("MoveCompleteFailedForAlreadyFailedMove", "Ex2C160B", false, true, MrsStrings.ResourceManager, new object[]
			{
				mbxGuid
			});
		}

		// Token: 0x06002065 RID: 8293 RVA: 0x0004634C File Offset: 0x0004454C
		public static LocalizedString DatabaseCouldNotBeMapped(string databaseName)
		{
			return new LocalizedString("DatabaseCouldNotBeMapped", "", false, false, MrsStrings.ResourceManager, new object[]
			{
				databaseName
			});
		}

		// Token: 0x06002066 RID: 8294 RVA: 0x0004637C File Offset: 0x0004457C
		public static LocalizedString ReportCopyFolderPropertyProgress(ulong folderCount)
		{
			return new LocalizedString("ReportCopyFolderPropertyProgress", "", false, false, MrsStrings.ResourceManager, new object[]
			{
				folderCount
			});
		}

		// Token: 0x06002067 RID: 8295 RVA: 0x000463B0 File Offset: 0x000445B0
		public static LocalizedString ReportUnableToLoadDestinationUser(string errorType)
		{
			return new LocalizedString("ReportUnableToLoadDestinationUser", "", false, false, MrsStrings.ResourceManager, new object[]
			{
				errorType
			});
		}

		// Token: 0x06002068 RID: 8296 RVA: 0x000463E0 File Offset: 0x000445E0
		public static LocalizedString NotImplemented(string methodName)
		{
			return new LocalizedString("NotImplemented", "", false, false, MrsStrings.ResourceManager, new object[]
			{
				methodName
			});
		}

		// Token: 0x06002069 RID: 8297 RVA: 0x00046410 File Offset: 0x00044610
		public static LocalizedString ReportFolderCreationProgress(int foldersCreated, LocalizedString physicalMailboxId)
		{
			return new LocalizedString("ReportFolderCreationProgress", "", false, false, MrsStrings.ResourceManager, new object[]
			{
				foldersCreated,
				physicalMailboxId
			});
		}

		// Token: 0x0600206A RID: 8298 RVA: 0x00046450 File Offset: 0x00044650
		public static LocalizedString PropValuesDataContext(string propValuesStr)
		{
			return new LocalizedString("PropValuesDataContext", "Ex467613", false, true, MrsStrings.ResourceManager, new object[]
			{
				propValuesStr
			});
		}

		// Token: 0x0600206B RID: 8299 RVA: 0x00046480 File Offset: 0x00044680
		public static LocalizedString ReportMailboxInfoBeforeMoveLoc(LocalizedString mailboxInfoString)
		{
			return new LocalizedString("ReportMailboxInfoBeforeMoveLoc", "ExE09E72", false, true, MrsStrings.ResourceManager, new object[]
			{
				mailboxInfoString
			});
		}

		// Token: 0x0600206C RID: 8300 RVA: 0x000464B4 File Offset: 0x000446B4
		public static LocalizedString ReportMoveRequestCreated(string userName)
		{
			return new LocalizedString("ReportMoveRequestCreated", "Ex886978", false, true, MrsStrings.ResourceManager, new object[]
			{
				userName
			});
		}

		// Token: 0x0600206D RID: 8301 RVA: 0x000464E4 File Offset: 0x000446E4
		public static LocalizedString PickupStatusRequestTypeNotSupported(string requestType)
		{
			return new LocalizedString("PickupStatusRequestTypeNotSupported", "", false, false, MrsStrings.ResourceManager, new object[]
			{
				requestType
			});
		}

		// Token: 0x0600206E RID: 8302 RVA: 0x00046514 File Offset: 0x00044714
		public static LocalizedString ReportRequestSaveFailed(string errorType, LocalizedString errorMsg, string trace)
		{
			return new LocalizedString("ReportRequestSaveFailed", "ExFF7A34", false, true, MrsStrings.ResourceManager, new object[]
			{
				errorType,
				errorMsg,
				trace
			});
		}

		// Token: 0x17000C47 RID: 3143
		// (get) Token: 0x0600206F RID: 8303 RVA: 0x00046550 File Offset: 0x00044750
		public static LocalizedString FolderHierarchyIsInconsistent
		{
			get
			{
				return new LocalizedString("FolderHierarchyIsInconsistent", "Ex9CA71E", false, true, MrsStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06002070 RID: 8304 RVA: 0x00046570 File Offset: 0x00044770
		public static LocalizedString MoveRequestDataMissing(Guid mailboxGuid, Guid mdbGuid)
		{
			return new LocalizedString("MoveRequestDataMissing", "ExCB964E", false, true, MrsStrings.ResourceManager, new object[]
			{
				mailboxGuid,
				mdbGuid
			});
		}

		// Token: 0x06002071 RID: 8305 RVA: 0x000465B0 File Offset: 0x000447B0
		public static LocalizedString FolderDataContextSearch(string folderName, string entryId, string parentId)
		{
			return new LocalizedString("FolderDataContextSearch", "ExF7F5AE", false, true, MrsStrings.ResourceManager, new object[]
			{
				folderName,
				entryId,
				parentId
			});
		}

		// Token: 0x17000C48 RID: 3144
		// (get) Token: 0x06002072 RID: 8306 RVA: 0x000465E7 File Offset: 0x000447E7
		public static LocalizedString ReportMoveRestartedDueToSignatureChange
		{
			get
			{
				return new LocalizedString("ReportMoveRestartedDueToSignatureChange", "Ex81DF08", false, true, MrsStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000C49 RID: 3145
		// (get) Token: 0x06002073 RID: 8307 RVA: 0x00046605 File Offset: 0x00044805
		public static LocalizedString ErrorCannotPreventCompletionForCompletingMove
		{
			get
			{
				return new LocalizedString("ErrorCannotPreventCompletionForCompletingMove", "", false, false, MrsStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06002074 RID: 8308 RVA: 0x00046624 File Offset: 0x00044824
		public static LocalizedString ReportIncrementalSyncContentChangesPaged2(LocalizedString physicalMailboxId, int batch, int newMessages, int changedMessages, int deletedMessages, int readMessages, int unreadMessages, int total)
		{
			return new LocalizedString("ReportIncrementalSyncContentChangesPaged2", "", false, false, MrsStrings.ResourceManager, new object[]
			{
				physicalMailboxId,
				batch,
				newMessages,
				changedMessages,
				deletedMessages,
				readMessages,
				unreadMessages,
				total
			});
		}

		// Token: 0x17000C4A RID: 3146
		// (get) Token: 0x06002075 RID: 8309 RVA: 0x0004669B File Offset: 0x0004489B
		public static LocalizedString WorkloadTypeOffboarding
		{
			get
			{
				return new LocalizedString("WorkloadTypeOffboarding", "", false, false, MrsStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06002076 RID: 8310 RVA: 0x000466BC File Offset: 0x000448BC
		public static LocalizedString ValidationValueIsMissing(string valueName)
		{
			return new LocalizedString("ValidationValueIsMissing", "ExD12111", false, true, MrsStrings.ResourceManager, new object[]
			{
				valueName
			});
		}

		// Token: 0x17000C4B RID: 3147
		// (get) Token: 0x06002077 RID: 8311 RVA: 0x000466EB File Offset: 0x000448EB
		public static LocalizedString MRSAlreadyConfigured
		{
			get
			{
				return new LocalizedString("MRSAlreadyConfigured", "Ex4C33F5", false, true, MrsStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06002078 RID: 8312 RVA: 0x0004670C File Offset: 0x0004490C
		public static LocalizedString ReportLargeAmountOfDataLossAccepted2(string badItemLimit, string largeItemLimit, string requestorName)
		{
			return new LocalizedString("ReportLargeAmountOfDataLossAccepted2", "", false, false, MrsStrings.ResourceManager, new object[]
			{
				badItemLimit,
				largeItemLimit,
				requestorName
			});
		}

		// Token: 0x06002079 RID: 8313 RVA: 0x00046744 File Offset: 0x00044944
		public static LocalizedString ReportLargeItemEncountered(LocalizedString largeItemStr)
		{
			return new LocalizedString("ReportLargeItemEncountered", "", false, false, MrsStrings.ResourceManager, new object[]
			{
				largeItemStr
			});
		}

		// Token: 0x0600207A RID: 8314 RVA: 0x00046778 File Offset: 0x00044978
		public static LocalizedString JobHasBeenRelinquishedDueToCancelPostponed(DateTime removeAfter)
		{
			return new LocalizedString("JobHasBeenRelinquishedDueToCancelPostponed", "", false, false, MrsStrings.ResourceManager, new object[]
			{
				removeAfter
			});
		}

		// Token: 0x0600207B RID: 8315 RVA: 0x000467AC File Offset: 0x000449AC
		public static LocalizedString ReportMoveRequestResumed(string userName)
		{
			return new LocalizedString("ReportMoveRequestResumed", "ExF4C194", false, true, MrsStrings.ResourceManager, new object[]
			{
				userName
			});
		}

		// Token: 0x0600207C RID: 8316 RVA: 0x000467DC File Offset: 0x000449DC
		public static LocalizedString ReportRequestIsInvalid(LocalizedString validationMessage)
		{
			return new LocalizedString("ReportRequestIsInvalid", "Ex503A42", false, true, MrsStrings.ResourceManager, new object[]
			{
				validationMessage
			});
		}

		// Token: 0x0600207D RID: 8317 RVA: 0x00046810 File Offset: 0x00044A10
		public static LocalizedString PublicFoldersId(string orgID)
		{
			return new LocalizedString("PublicFoldersId", "Ex2A0D2E", false, true, MrsStrings.ResourceManager, new object[]
			{
				orgID
			});
		}

		// Token: 0x0600207E RID: 8318 RVA: 0x00046840 File Offset: 0x00044A40
		public static LocalizedString ItemCountsAndSizes(ulong regularCount, string regularSize, ulong delCount, string delSize, ulong faiCount, string faiSize, ulong faiDelCount, string faiDelSize)
		{
			return new LocalizedString("ItemCountsAndSizes", "Ex1398F0", false, true, MrsStrings.ResourceManager, new object[]
			{
				regularCount,
				regularSize,
				delCount,
				delSize,
				faiCount,
				faiSize,
				faiDelCount,
				faiDelSize
			});
		}

		// Token: 0x17000C4C RID: 3148
		// (get) Token: 0x0600207F RID: 8319 RVA: 0x000468A3 File Offset: 0x00044AA3
		public static LocalizedString ReportTargetPublicFolderDeploymentUnlocked
		{
			get
			{
				return new LocalizedString("ReportTargetPublicFolderDeploymentUnlocked", "", false, false, MrsStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06002080 RID: 8320 RVA: 0x000468C4 File Offset: 0x00044AC4
		public static LocalizedString ValidationTargetArchiveMDBMismatch(string adDatabase, string mrDatabase)
		{
			return new LocalizedString("ValidationTargetArchiveMDBMismatch", "Ex8C23C1", false, true, MrsStrings.ResourceManager, new object[]
			{
				adDatabase,
				mrDatabase
			});
		}

		// Token: 0x06002081 RID: 8321 RVA: 0x000468F8 File Offset: 0x00044AF8
		public static LocalizedString DestinationMailboxSeedMBICacheFailed(LocalizedString errorMsg)
		{
			return new LocalizedString("DestinationMailboxSeedMBICacheFailed", "ExA75EC7", false, true, MrsStrings.ResourceManager, new object[]
			{
				errorMsg
			});
		}

		// Token: 0x17000C4D RID: 3149
		// (get) Token: 0x06002082 RID: 8322 RVA: 0x0004692C File Offset: 0x00044B2C
		public static LocalizedString ReportRequestCancelPostponed
		{
			get
			{
				return new LocalizedString("ReportRequestCancelPostponed", "", false, false, MrsStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06002083 RID: 8323 RVA: 0x0004694C File Offset: 0x00044B4C
		public static LocalizedString MoveHasBeenRelinquishedDueToTargetDatabaseFailover(Guid mbxGuid)
		{
			return new LocalizedString("MoveHasBeenRelinquishedDueToTargetDatabaseFailover", "Ex25A23E", false, true, MrsStrings.ResourceManager, new object[]
			{
				mbxGuid
			});
		}

		// Token: 0x06002084 RID: 8324 RVA: 0x00046980 File Offset: 0x00044B80
		public static LocalizedString ServerNotFound(string serverLegDN)
		{
			return new LocalizedString("ServerNotFound", "Ex9F24CB", false, true, MrsStrings.ResourceManager, new object[]
			{
				serverLegDN
			});
		}

		// Token: 0x06002085 RID: 8325 RVA: 0x000469B0 File Offset: 0x00044BB0
		public static LocalizedString ReportIncrementalSyncProgress(LocalizedString physicalMailboxId, int changesApplied, int totalChanges)
		{
			return new LocalizedString("ReportIncrementalSyncProgress", "", false, false, MrsStrings.ResourceManager, new object[]
			{
				physicalMailboxId,
				changesApplied,
				totalChanges
			});
		}

		// Token: 0x17000C4E RID: 3150
		// (get) Token: 0x06002086 RID: 8326 RVA: 0x000469F6 File Offset: 0x00044BF6
		public static LocalizedString JobHasBeenRelinquishedDueToHAStall
		{
			get
			{
				return new LocalizedString("JobHasBeenRelinquishedDueToHAStall", "", false, false, MrsStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06002087 RID: 8327 RVA: 0x00046A14 File Offset: 0x00044C14
		public static LocalizedString ReportFailedToLinkADPublicFolder(string publicFolderId, string objectId, string entryId)
		{
			return new LocalizedString("ReportFailedToLinkADPublicFolder", "", false, false, MrsStrings.ResourceManager, new object[]
			{
				publicFolderId,
				objectId,
				entryId
			});
		}

		// Token: 0x17000C4F RID: 3151
		// (get) Token: 0x06002088 RID: 8328 RVA: 0x00046A4B File Offset: 0x00044C4B
		public static LocalizedString RequestPriorityLow
		{
			get
			{
				return new LocalizedString("RequestPriorityLow", "", false, false, MrsStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06002089 RID: 8329 RVA: 0x00046A6C File Offset: 0x00044C6C
		public static LocalizedString UnableToGetPSTFolderProps(uint folderId)
		{
			return new LocalizedString("UnableToGetPSTFolderProps", "Ex35D2F6", false, true, MrsStrings.ResourceManager, new object[]
			{
				folderId
			});
		}

		// Token: 0x17000C50 RID: 3152
		// (get) Token: 0x0600208A RID: 8330 RVA: 0x00046AA0 File Offset: 0x00044CA0
		public static LocalizedString MoveRequestDirectionPull
		{
			get
			{
				return new LocalizedString("MoveRequestDirectionPull", "ExB7BE2F", false, true, MrsStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000C51 RID: 3153
		// (get) Token: 0x0600208B RID: 8331 RVA: 0x00046ABE File Offset: 0x00044CBE
		public static LocalizedString UnableToApplyFolderHierarchyChanges
		{
			get
			{
				return new LocalizedString("UnableToApplyFolderHierarchyChanges", "", false, false, MrsStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600208C RID: 8332 RVA: 0x00046ADC File Offset: 0x00044CDC
		public static LocalizedString ReportArchiveUpdated(string domainController)
		{
			return new LocalizedString("ReportArchiveUpdated", "", false, false, MrsStrings.ResourceManager, new object[]
			{
				domainController
			});
		}

		// Token: 0x0600208D RID: 8333 RVA: 0x00046B0C File Offset: 0x00044D0C
		public static LocalizedString ReportSourceMailboxResetFailed(string errorType, LocalizedString errorMsg, string trace)
		{
			return new LocalizedString("ReportSourceMailboxResetFailed", "Ex3E2B7E", false, true, MrsStrings.ResourceManager, new object[]
			{
				errorType,
				errorMsg,
				trace
			});
		}

		// Token: 0x17000C52 RID: 3154
		// (get) Token: 0x0600208E RID: 8334 RVA: 0x00046B48 File Offset: 0x00044D48
		public static LocalizedString PickupStatusDisabled
		{
			get
			{
				return new LocalizedString("PickupStatusDisabled", "", false, false, MrsStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000C53 RID: 3155
		// (get) Token: 0x0600208F RID: 8335 RVA: 0x00046B66 File Offset: 0x00044D66
		public static LocalizedString RemoteResource
		{
			get
			{
				return new LocalizedString("RemoteResource", "", false, false, MrsStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06002090 RID: 8336 RVA: 0x00046B84 File Offset: 0x00044D84
		public static LocalizedString ReportMoveRequestSaveFailed(string errorType, LocalizedString errorMsg, string trace)
		{
			return new LocalizedString("ReportMoveRequestSaveFailed", "ExED8535", false, true, MrsStrings.ResourceManager, new object[]
			{
				errorType,
				errorMsg,
				trace
			});
		}

		// Token: 0x17000C54 RID: 3156
		// (get) Token: 0x06002091 RID: 8337 RVA: 0x00046BC0 File Offset: 0x00044DC0
		public static LocalizedString MoveRestartedDueToSignatureChange
		{
			get
			{
				return new LocalizedString("MoveRestartedDueToSignatureChange", "Ex367DD9", false, true, MrsStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06002092 RID: 8338 RVA: 0x00046BE0 File Offset: 0x00044DE0
		public static LocalizedString IdentityWasNotInValidFormat(string rawIdentity)
		{
			return new LocalizedString("IdentityWasNotInValidFormat", "", false, false, MrsStrings.ResourceManager, new object[]
			{
				rawIdentity
			});
		}

		// Token: 0x06002093 RID: 8339 RVA: 0x00046C10 File Offset: 0x00044E10
		public static LocalizedString ReportInitialSeedingStarted(int messageCount, string totalSize)
		{
			return new LocalizedString("ReportInitialSeedingStarted", "Ex23334E", false, true, MrsStrings.ResourceManager, new object[]
			{
				messageCount,
				totalSize
			});
		}

		// Token: 0x06002094 RID: 8340 RVA: 0x00046C48 File Offset: 0x00044E48
		public static LocalizedString DestinationADNotUpToDate(Guid mbxGuid)
		{
			return new LocalizedString("DestinationADNotUpToDate", "Ex580E71", false, true, MrsStrings.ResourceManager, new object[]
			{
				mbxGuid
			});
		}

		// Token: 0x06002095 RID: 8341 RVA: 0x00046C7C File Offset: 0x00044E7C
		public static LocalizedString EasFolderSyncFailedTransiently(string folderSyncStatus, string httpStatus)
		{
			return new LocalizedString("EasFolderSyncFailedTransiently", "", false, false, MrsStrings.ResourceManager, new object[]
			{
				folderSyncStatus,
				httpStatus
			});
		}

		// Token: 0x17000C55 RID: 3157
		// (get) Token: 0x06002096 RID: 8342 RVA: 0x00046CAF File Offset: 0x00044EAF
		public static LocalizedString FolderHierarchyContainsNoRoots
		{
			get
			{
				return new LocalizedString("FolderHierarchyContainsNoRoots", "", false, false, MrsStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000C56 RID: 3158
		// (get) Token: 0x06002097 RID: 8343 RVA: 0x00046CCD File Offset: 0x00044ECD
		public static LocalizedString JobHasBeenRelinquishedDueToCIStall
		{
			get
			{
				return new LocalizedString("JobHasBeenRelinquishedDueToCIStall", "", false, false, MrsStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06002098 RID: 8344 RVA: 0x00046CEC File Offset: 0x00044EEC
		public static LocalizedString CompositeDataContext(LocalizedString firstString, LocalizedString tail)
		{
			return new LocalizedString("CompositeDataContext", "Ex2E1326", false, true, MrsStrings.ResourceManager, new object[]
			{
				firstString,
				tail
			});
		}

		// Token: 0x06002099 RID: 8345 RVA: 0x00046D2C File Offset: 0x00044F2C
		public static LocalizedString BadItemMisplacedFolder(string folderName)
		{
			return new LocalizedString("BadItemMisplacedFolder", "", false, false, MrsStrings.ResourceManager, new object[]
			{
				folderName
			});
		}

		// Token: 0x0600209A RID: 8346 RVA: 0x00046D5C File Offset: 0x00044F5C
		public static LocalizedString PublicFolderMigrationNotSupportedFromCurrentExchange2007Version(int major, int minor, int build, int revision)
		{
			return new LocalizedString("PublicFolderMigrationNotSupportedFromCurrentExchange2007Version", "", false, false, MrsStrings.ResourceManager, new object[]
			{
				major,
				minor,
				build,
				revision
			});
		}

		// Token: 0x17000C57 RID: 3159
		// (get) Token: 0x0600209B RID: 8347 RVA: 0x00046DAB File Offset: 0x00044FAB
		public static LocalizedString ContentIndexing
		{
			get
			{
				return new LocalizedString("ContentIndexing", "", false, false, MrsStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600209C RID: 8348 RVA: 0x00046DCC File Offset: 0x00044FCC
		public static LocalizedString MailboxAlreadySynced(Guid mbxGuid)
		{
			return new LocalizedString("MailboxAlreadySynced", "ExF4F35A", false, true, MrsStrings.ResourceManager, new object[]
			{
				mbxGuid
			});
		}

		// Token: 0x17000C58 RID: 3160
		// (get) Token: 0x0600209D RID: 8349 RVA: 0x00046E00 File Offset: 0x00045000
		public static LocalizedString TooManyLargeItems
		{
			get
			{
				return new LocalizedString("TooManyLargeItems", "", false, false, MrsStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600209E RID: 8350 RVA: 0x00046E20 File Offset: 0x00045020
		public static LocalizedString OrganizationRelationshipNotFound(string domain, string orgId)
		{
			return new LocalizedString("OrganizationRelationshipNotFound", "Ex16E4F7", false, true, MrsStrings.ResourceManager, new object[]
			{
				domain,
				orgId
			});
		}

		// Token: 0x0600209F RID: 8351 RVA: 0x00046E54 File Offset: 0x00045054
		public static LocalizedString ReportUnableToUpdateSourceMailbox2(string errorType)
		{
			return new LocalizedString("ReportUnableToUpdateSourceMailbox2", "", false, false, MrsStrings.ResourceManager, new object[]
			{
				errorType
			});
		}

		// Token: 0x060020A0 RID: 8352 RVA: 0x00046E84 File Offset: 0x00045084
		public static LocalizedString PickupStatusLightJob(bool suspend, bool rehomeRequest, string priority)
		{
			return new LocalizedString("PickupStatusLightJob", "", false, false, MrsStrings.ResourceManager, new object[]
			{
				suspend,
				rehomeRequest,
				priority
			});
		}

		// Token: 0x060020A1 RID: 8353 RVA: 0x00046EC8 File Offset: 0x000450C8
		public static LocalizedString ReportSourceMailUserAfterFinalization(string userDataXML)
		{
			return new LocalizedString("ReportSourceMailUserAfterFinalization", "Ex529A72", false, true, MrsStrings.ResourceManager, new object[]
			{
				userDataXML
			});
		}

		// Token: 0x060020A2 RID: 8354 RVA: 0x00046EF8 File Offset: 0x000450F8
		public static LocalizedString JobIsStuck(DateTime lastProgressTimestamp, DateTime jobPickupTimestamp)
		{
			return new LocalizedString("JobIsStuck", "", false, false, MrsStrings.ResourceManager, new object[]
			{
				lastProgressTimestamp,
				jobPickupTimestamp
			});
		}

		// Token: 0x060020A3 RID: 8355 RVA: 0x00046F38 File Offset: 0x00045138
		public static LocalizedString UnexpectedError(int hr)
		{
			return new LocalizedString("UnexpectedError", "ExD60017", false, true, MrsStrings.ResourceManager, new object[]
			{
				hr
			});
		}

		// Token: 0x060020A4 RID: 8356 RVA: 0x00046F6C File Offset: 0x0004516C
		public static LocalizedString OlcSettingNotImplemented(string settingType, string settingName)
		{
			return new LocalizedString("OlcSettingNotImplemented", "", false, false, MrsStrings.ResourceManager, new object[]
			{
				settingType,
				settingName
			});
		}

		// Token: 0x060020A5 RID: 8357 RVA: 0x00046FA0 File Offset: 0x000451A0
		public static LocalizedString ReportDestinationMailboxCleanupFailed(string errorType, LocalizedString errorMsg, string trace)
		{
			return new LocalizedString("ReportDestinationMailboxCleanupFailed", "Ex6BFF9C", false, true, MrsStrings.ResourceManager, new object[]
			{
				errorType,
				errorMsg,
				trace
			});
		}

		// Token: 0x060020A6 RID: 8358 RVA: 0x00046FDC File Offset: 0x000451DC
		public static LocalizedString ReportMoveRequestProcessedByAnotherMRS(string mrsName)
		{
			return new LocalizedString("ReportMoveRequestProcessedByAnotherMRS", "ExEE2D31", false, true, MrsStrings.ResourceManager, new object[]
			{
				mrsName
			});
		}

		// Token: 0x17000C59 RID: 3161
		// (get) Token: 0x060020A7 RID: 8359 RVA: 0x0004700B File Offset: 0x0004520B
		public static LocalizedString CouldNotConnectToTargetMailbox
		{
			get
			{
				return new LocalizedString("CouldNotConnectToTargetMailbox", "Ex91B8F8", false, true, MrsStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060020A8 RID: 8360 RVA: 0x0004702C File Offset: 0x0004522C
		public static LocalizedString UnableToReadPSTFolder(uint folderId)
		{
			return new LocalizedString("UnableToReadPSTFolder", "Ex10A2B1", false, true, MrsStrings.ResourceManager, new object[]
			{
				folderId
			});
		}

		// Token: 0x17000C5A RID: 3162
		// (get) Token: 0x060020A9 RID: 8361 RVA: 0x00047060 File Offset: 0x00045260
		public static LocalizedString PSTIOException
		{
			get
			{
				return new LocalizedString("PSTIOException", "Ex6DEDE4", false, true, MrsStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060020AA RID: 8362 RVA: 0x00047080 File Offset: 0x00045280
		public static LocalizedString MailboxDatabaseNotUnique(string mdbIdentity)
		{
			return new LocalizedString("MailboxDatabaseNotUnique", "Ex01053A", false, true, MrsStrings.ResourceManager, new object[]
			{
				mdbIdentity
			});
		}

		// Token: 0x17000C5B RID: 3163
		// (get) Token: 0x060020AB RID: 8363 RVA: 0x000470AF File Offset: 0x000452AF
		public static LocalizedString RequestPriorityNormal
		{
			get
			{
				return new LocalizedString("RequestPriorityNormal", "", false, false, MrsStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000C5C RID: 3164
		// (get) Token: 0x060020AC RID: 8364 RVA: 0x000470CD File Offset: 0x000452CD
		public static LocalizedString SmtpServerInfoMissing
		{
			get
			{
				return new LocalizedString("SmtpServerInfoMissing", "", false, false, MrsStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060020AD RID: 8365 RVA: 0x000470EC File Offset: 0x000452EC
		public static LocalizedString EasFetchFailed(string errorMessage)
		{
			return new LocalizedString("EasFetchFailed", "", false, false, MrsStrings.ResourceManager, new object[]
			{
				errorMessage
			});
		}

		// Token: 0x060020AE RID: 8366 RVA: 0x0004711C File Offset: 0x0004531C
		public static LocalizedString FilterOperatorMustBeEQorNE(string propertyName)
		{
			return new LocalizedString("FilterOperatorMustBeEQorNE", "Ex572737", false, true, MrsStrings.ResourceManager, new object[]
			{
				propertyName
			});
		}

		// Token: 0x17000C5D RID: 3165
		// (get) Token: 0x060020AF RID: 8367 RVA: 0x0004714B File Offset: 0x0004534B
		public static LocalizedString NoPublicFolderMailboxFoundInSource
		{
			get
			{
				return new LocalizedString("NoPublicFolderMailboxFoundInSource", "", false, false, MrsStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060020B0 RID: 8368 RVA: 0x0004716C File Offset: 0x0004536C
		public static LocalizedString ReportMissingItemEncountered(LocalizedString missingItemStr)
		{
			return new LocalizedString("ReportMissingItemEncountered", "", false, false, MrsStrings.ResourceManager, new object[]
			{
				missingItemStr
			});
		}

		// Token: 0x060020B1 RID: 8369 RVA: 0x000471A0 File Offset: 0x000453A0
		public static LocalizedString PickupStatusInvalidJob(string validationResult, LocalizedString validationMessage)
		{
			return new LocalizedString("PickupStatusInvalidJob", "", false, false, MrsStrings.ResourceManager, new object[]
			{
				validationResult,
				validationMessage
			});
		}

		// Token: 0x17000C5E RID: 3166
		// (get) Token: 0x060020B2 RID: 8370 RVA: 0x000471D8 File Offset: 0x000453D8
		public static LocalizedString WorkloadTypeRemotePstExport
		{
			get
			{
				return new LocalizedString("WorkloadTypeRemotePstExport", "", false, false, MrsStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060020B3 RID: 8371 RVA: 0x000471F8 File Offset: 0x000453F8
		public static LocalizedString ReportMoveContinued(string syncStage)
		{
			return new LocalizedString("ReportMoveContinued", "Ex8CD301", false, true, MrsStrings.ResourceManager, new object[]
			{
				syncStage
			});
		}

		// Token: 0x060020B4 RID: 8372 RVA: 0x00047228 File Offset: 0x00045428
		public static LocalizedString ReportMoveRequestSuspended(string userName)
		{
			return new LocalizedString("ReportMoveRequestSuspended", "Ex04F380", false, true, MrsStrings.ResourceManager, new object[]
			{
				userName
			});
		}

		// Token: 0x060020B5 RID: 8373 RVA: 0x00047258 File Offset: 0x00045458
		public static LocalizedString PstTracingId(string filepath)
		{
			return new LocalizedString("PstTracingId", "", false, false, MrsStrings.ResourceManager, new object[]
			{
				filepath
			});
		}

		// Token: 0x060020B6 RID: 8374 RVA: 0x00047288 File Offset: 0x00045488
		public static LocalizedString InvalidUid(string uid)
		{
			return new LocalizedString("InvalidUid", "", false, false, MrsStrings.ResourceManager, new object[]
			{
				uid
			});
		}

		// Token: 0x17000C5F RID: 3167
		// (get) Token: 0x060020B7 RID: 8375 RVA: 0x000472B7 File Offset: 0x000454B7
		public static LocalizedString FastTransferArgumentError
		{
			get
			{
				return new LocalizedString("FastTransferArgumentError", "", false, false, MrsStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060020B8 RID: 8376 RVA: 0x000472D8 File Offset: 0x000454D8
		public static LocalizedString UnableToGetPSTHierarchy(string filePath)
		{
			return new LocalizedString("UnableToGetPSTHierarchy", "Ex444B92", false, true, MrsStrings.ResourceManager, new object[]
			{
				filePath
			});
		}

		// Token: 0x17000C60 RID: 3168
		// (get) Token: 0x060020B9 RID: 8377 RVA: 0x00047307 File Offset: 0x00045507
		public static LocalizedString PickupStatusCompletedJob
		{
			get
			{
				return new LocalizedString("PickupStatusCompletedJob", "", false, false, MrsStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060020BA RID: 8378 RVA: 0x00047328 File Offset: 0x00045528
		public static LocalizedString ReportCopyProgress2(int itemsWritten, int itemsTotal, string dataSizeCopied, string totalSize, int foldersCompleted, int totalFolders)
		{
			return new LocalizedString("ReportCopyProgress2", "", false, false, MrsStrings.ResourceManager, new object[]
			{
				itemsWritten,
				itemsTotal,
				dataSizeCopied,
				totalSize,
				foldersCompleted,
				totalFolders
			});
		}

		// Token: 0x060020BB RID: 8379 RVA: 0x00047384 File Offset: 0x00045584
		public static LocalizedString ReportSoftDeletedItemCountsAndSizesInArchiveLoc(LocalizedString softDeletedItems)
		{
			return new LocalizedString("ReportSoftDeletedItemCountsAndSizesInArchiveLoc", "ExE0E8AE", false, true, MrsStrings.ResourceManager, new object[]
			{
				softDeletedItems
			});
		}

		// Token: 0x060020BC RID: 8380 RVA: 0x000473B8 File Offset: 0x000455B8
		public static LocalizedString ReportMailboxAfterFinalization2(string userID, string domainControllerName)
		{
			return new LocalizedString("ReportMailboxAfterFinalization2", "", false, false, MrsStrings.ResourceManager, new object[]
			{
				userID,
				domainControllerName
			});
		}

		// Token: 0x060020BD RID: 8381 RVA: 0x000473EC File Offset: 0x000455EC
		public static LocalizedString ExceptionDetails(string exceptionType, int errorCode, LocalizedString exceptionMessage)
		{
			return new LocalizedString("ExceptionDetails", "Ex665B46", false, true, MrsStrings.ResourceManager, new object[]
			{
				exceptionType,
				errorCode,
				exceptionMessage
			});
		}

		// Token: 0x060020BE RID: 8382 RVA: 0x00047430 File Offset: 0x00045630
		public static LocalizedString ValidationNameMismatch(string jobName, string indexName)
		{
			return new LocalizedString("ValidationNameMismatch", "ExB58CB4", false, true, MrsStrings.ResourceManager, new object[]
			{
				jobName,
				indexName
			});
		}

		// Token: 0x060020BF RID: 8383 RVA: 0x00047464 File Offset: 0x00045664
		public static LocalizedString ReportInitialSyncCheckpointCompleted(int foldersProcessed)
		{
			return new LocalizedString("ReportInitialSyncCheckpointCompleted", "", false, false, MrsStrings.ResourceManager, new object[]
			{
				foldersProcessed
			});
		}

		// Token: 0x060020C0 RID: 8384 RVA: 0x00047498 File Offset: 0x00045698
		public static LocalizedString ReportLargeItemsSkipped(int count, string totalSize)
		{
			return new LocalizedString("ReportLargeItemsSkipped", "", false, false, MrsStrings.ResourceManager, new object[]
			{
				count,
				totalSize
			});
		}

		// Token: 0x17000C61 RID: 3169
		// (get) Token: 0x060020C1 RID: 8385 RVA: 0x000474D0 File Offset: 0x000456D0
		public static LocalizedString ReportJobProcessingDisabled
		{
			get
			{
				return new LocalizedString("ReportJobProcessingDisabled", "", false, false, MrsStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060020C2 RID: 8386 RVA: 0x000474F0 File Offset: 0x000456F0
		public static LocalizedString ReportMailboxArchiveInfoBeforeMove(string mailboxInfoString)
		{
			return new LocalizedString("ReportMailboxArchiveInfoBeforeMove", "Ex5B06CC", false, true, MrsStrings.ResourceManager, new object[]
			{
				mailboxInfoString
			});
		}

		// Token: 0x060020C3 RID: 8387 RVA: 0x00047520 File Offset: 0x00045720
		public static LocalizedString ReportSourceMailboxUpdateFailed(LocalizedString mailboxId, string errorType, int currentAttempt, int totalAttempts)
		{
			return new LocalizedString("ReportSourceMailboxUpdateFailed", "", false, false, MrsStrings.ResourceManager, new object[]
			{
				mailboxId,
				errorType,
				currentAttempt,
				totalAttempts
			});
		}

		// Token: 0x060020C4 RID: 8388 RVA: 0x0004756C File Offset: 0x0004576C
		public static LocalizedString EasFetchFailedTransiently(string ioStatus, string httpStatus, string folderId, string messageId)
		{
			return new LocalizedString("EasFetchFailedTransiently", "", false, false, MrsStrings.ResourceManager, new object[]
			{
				ioStatus,
				httpStatus,
				folderId,
				messageId
			});
		}

		// Token: 0x17000C62 RID: 3170
		// (get) Token: 0x060020C5 RID: 8389 RVA: 0x000475A7 File Offset: 0x000457A7
		public static LocalizedString ImproperTypeForThisIdParameter
		{
			get
			{
				return new LocalizedString("ImproperTypeForThisIdParameter", "", false, false, MrsStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060020C6 RID: 8390 RVA: 0x000475C8 File Offset: 0x000457C8
		public static LocalizedString ReportMoveRequestIsInvalid(LocalizedString validationMessage)
		{
			return new LocalizedString("ReportMoveRequestIsInvalid", "ExDD445A", false, true, MrsStrings.ResourceManager, new object[]
			{
				validationMessage
			});
		}

		// Token: 0x060020C7 RID: 8391 RVA: 0x000475FC File Offset: 0x000457FC
		public static LocalizedString EasSendFailedError(string sendStatus, string httpStatus, string messageId)
		{
			return new LocalizedString("EasSendFailedError", "", false, false, MrsStrings.ResourceManager, new object[]
			{
				sendStatus,
				httpStatus,
				messageId
			});
		}

		// Token: 0x060020C8 RID: 8392 RVA: 0x00047634 File Offset: 0x00045834
		public static LocalizedString ReportMovedMailUserMorphedToMailbox(string domainController)
		{
			return new LocalizedString("ReportMovedMailUserMorphedToMailbox", "", false, false, MrsStrings.ResourceManager, new object[]
			{
				domainController
			});
		}

		// Token: 0x060020C9 RID: 8393 RVA: 0x00047664 File Offset: 0x00045864
		public static LocalizedString CorruptSortOrderData(int flags)
		{
			return new LocalizedString("CorruptSortOrderData", "", false, false, MrsStrings.ResourceManager, new object[]
			{
				flags
			});
		}

		// Token: 0x060020CA RID: 8394 RVA: 0x00047698 File Offset: 0x00045898
		public static LocalizedString ReportRetryingPostMoveCleanup(int delaySecs, int iAttempts, int iMaxRetries)
		{
			return new LocalizedString("ReportRetryingPostMoveCleanup", "Ex1C805B", false, true, MrsStrings.ResourceManager, new object[]
			{
				delaySecs,
				iAttempts,
				iMaxRetries
			});
		}

		// Token: 0x17000C63 RID: 3171
		// (get) Token: 0x060020CB RID: 8395 RVA: 0x000476DE File Offset: 0x000458DE
		public static LocalizedString MoveRequestMissingInfoDelete
		{
			get
			{
				return new LocalizedString("MoveRequestMissingInfoDelete", "Ex417EC3", false, true, MrsStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060020CC RID: 8396 RVA: 0x000476FC File Offset: 0x000458FC
		public static LocalizedString EasFolderDeleteFailed(string errorMessage)
		{
			return new LocalizedString("EasFolderDeleteFailed", "", false, false, MrsStrings.ResourceManager, new object[]
			{
				errorMessage
			});
		}

		// Token: 0x060020CD RID: 8397 RVA: 0x0004772C File Offset: 0x0004592C
		public static LocalizedString ErrorResourceReservation(string reservationStatus, Guid reservationId, Guid resourceId, string reservationType, string serverName)
		{
			return new LocalizedString("ErrorResourceReservation", "", false, false, MrsStrings.ResourceManager, new object[]
			{
				reservationStatus,
				reservationId,
				resourceId,
				reservationType,
				serverName
			});
		}

		// Token: 0x060020CE RID: 8398 RVA: 0x00047778 File Offset: 0x00045978
		public static LocalizedString MrsProxyServiceIsDisabled2(string serverName)
		{
			return new LocalizedString("MrsProxyServiceIsDisabled2", "", false, false, MrsStrings.ResourceManager, new object[]
			{
				serverName
			});
		}

		// Token: 0x17000C64 RID: 3172
		// (get) Token: 0x060020CF RID: 8399 RVA: 0x000477A7 File Offset: 0x000459A7
		public static LocalizedString ReportRelinquishingJobDueToServiceStop
		{
			get
			{
				return new LocalizedString("ReportRelinquishingJobDueToServiceStop", "ExBEAE86", false, true, MrsStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060020D0 RID: 8400 RVA: 0x000477C8 File Offset: 0x000459C8
		public static LocalizedString EasConnectFailed(string connectStatus, string httpStatus, string smtpAddress)
		{
			return new LocalizedString("EasConnectFailed", "", false, false, MrsStrings.ResourceManager, new object[]
			{
				connectStatus,
				httpStatus,
				smtpAddress
			});
		}

		// Token: 0x060020D1 RID: 8401 RVA: 0x00047800 File Offset: 0x00045A00
		public static LocalizedString CorruptNamedPropData(string type)
		{
			return new LocalizedString("CorruptNamedPropData", "", false, false, MrsStrings.ResourceManager, new object[]
			{
				type
			});
		}

		// Token: 0x17000C65 RID: 3173
		// (get) Token: 0x060020D2 RID: 8402 RVA: 0x0004782F File Offset: 0x00045A2F
		public static LocalizedString PickupStatusCorruptJob
		{
			get
			{
				return new LocalizedString("PickupStatusCorruptJob", "", false, false, MrsStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000C66 RID: 3174
		// (get) Token: 0x060020D3 RID: 8403 RVA: 0x0004784D File Offset: 0x00045A4D
		public static LocalizedString RequestHasBeenRelinquishedDueToBadHealthOfBackendServers
		{
			get
			{
				return new LocalizedString("RequestHasBeenRelinquishedDueToBadHealthOfBackendServers", "", false, false, MrsStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000C67 RID: 3175
		// (get) Token: 0x060020D4 RID: 8404 RVA: 0x0004786B File Offset: 0x00045A6B
		public static LocalizedString MoveRequestMissingInfoSave
		{
			get
			{
				return new LocalizedString("MoveRequestMissingInfoSave", "ExB3585F", false, true, MrsStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000C68 RID: 3176
		// (get) Token: 0x060020D5 RID: 8405 RVA: 0x00047889 File Offset: 0x00045A89
		public static LocalizedString RestartingMove
		{
			get
			{
				return new LocalizedString("RestartingMove", "ExEA1AA5", false, true, MrsStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060020D6 RID: 8406 RVA: 0x000478A8 File Offset: 0x00045AA8
		public static LocalizedString SourceMailboxCleanupFailed(LocalizedString errorMsg)
		{
			return new LocalizedString("SourceMailboxCleanupFailed", "ExEF4B26", false, true, MrsStrings.ResourceManager, new object[]
			{
				errorMsg
			});
		}

		// Token: 0x060020D7 RID: 8407 RVA: 0x000478DC File Offset: 0x00045ADC
		public static LocalizedString ReportMailboxBeforeFinalization(string userDataXML)
		{
			return new LocalizedString("ReportMailboxBeforeFinalization", "Ex78D949", false, true, MrsStrings.ResourceManager, new object[]
			{
				userDataXML
			});
		}

		// Token: 0x060020D8 RID: 8408 RVA: 0x0004790C File Offset: 0x00045B0C
		public static LocalizedString RequestHasBeenPostponedDueToBadHealthOfBackendServers(DateTime pickupTime)
		{
			return new LocalizedString("RequestHasBeenPostponedDueToBadHealthOfBackendServers", "", false, false, MrsStrings.ResourceManager, new object[]
			{
				pickupTime
			});
		}

		// Token: 0x17000C69 RID: 3177
		// (get) Token: 0x060020D9 RID: 8409 RVA: 0x00047940 File Offset: 0x00045B40
		public static LocalizedString ErrorWhileUpdatingMovedMailbox
		{
			get
			{
				return new LocalizedString("ErrorWhileUpdatingMovedMailbox", "ExFA20DD", false, true, MrsStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060020DA RID: 8410 RVA: 0x00047960 File Offset: 0x00045B60
		public static LocalizedString JobHasBeenRelinquishedDueToProxyThrottling(DateTime pickupTime)
		{
			return new LocalizedString("JobHasBeenRelinquishedDueToProxyThrottling", "ExDAF750", false, true, MrsStrings.ResourceManager, new object[]
			{
				pickupTime
			});
		}

		// Token: 0x060020DB RID: 8411 RVA: 0x00047994 File Offset: 0x00045B94
		public static LocalizedString PopTracingId(string emailAddress)
		{
			return new LocalizedString("PopTracingId", "", false, false, MrsStrings.ResourceManager, new object[]
			{
				emailAddress
			});
		}

		// Token: 0x17000C6A RID: 3178
		// (get) Token: 0x060020DC RID: 8412 RVA: 0x000479C3 File Offset: 0x00045BC3
		public static LocalizedString MoveRequestValidationFailed
		{
			get
			{
				return new LocalizedString("MoveRequestValidationFailed", "ExDA15BA", false, true, MrsStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000C6B RID: 3179
		// (get) Token: 0x060020DD RID: 8413 RVA: 0x000479E1 File Offset: 0x00045BE1
		public static LocalizedString MustProvideValidSessionForFindingRequests
		{
			get
			{
				return new LocalizedString("MustProvideValidSessionForFindingRequests", "", false, false, MrsStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000C6C RID: 3180
		// (get) Token: 0x060020DE RID: 8414 RVA: 0x000479FF File Offset: 0x00045BFF
		public static LocalizedString TooManyMissingItems
		{
			get
			{
				return new LocalizedString("TooManyMissingItems", "", false, false, MrsStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060020DF RID: 8415 RVA: 0x00047A20 File Offset: 0x00045C20
		public static LocalizedString MoveHasBeenAutoSuspendedUntilCompleteAfter(Guid mbxGuid, DateTime completeAfter)
		{
			return new LocalizedString("MoveHasBeenAutoSuspendedUntilCompleteAfter", "", false, false, MrsStrings.ResourceManager, new object[]
			{
				mbxGuid,
				completeAfter
			});
		}

		// Token: 0x17000C6D RID: 3181
		// (get) Token: 0x060020E0 RID: 8416 RVA: 0x00047A5D File Offset: 0x00045C5D
		public static LocalizedString UpdateFolderFailed
		{
			get
			{
				return new LocalizedString("UpdateFolderFailed", "", false, false, MrsStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000C6E RID: 3182
		// (get) Token: 0x060020E1 RID: 8417 RVA: 0x00047A7B File Offset: 0x00045C7B
		public static LocalizedString OfflinePublicFolderMigrationNotSupported
		{
			get
			{
				return new LocalizedString("OfflinePublicFolderMigrationNotSupported", "", false, false, MrsStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060020E2 RID: 8418 RVA: 0x00047A9C File Offset: 0x00045C9C
		public static LocalizedString MustRehomeRequestToSupportedVersion(string mdbID, string serverVersion)
		{
			return new LocalizedString("MustRehomeRequestToSupportedVersion", "", false, false, MrsStrings.ResourceManager, new object[]
			{
				mdbID,
				serverVersion
			});
		}

		// Token: 0x060020E3 RID: 8419 RVA: 0x00047AD0 File Offset: 0x00045CD0
		public static LocalizedString UnsupportedSyncProtocol(string protocol)
		{
			return new LocalizedString("UnsupportedSyncProtocol", "", false, false, MrsStrings.ResourceManager, new object[]
			{
				protocol
			});
		}

		// Token: 0x060020E4 RID: 8420 RVA: 0x00047B00 File Offset: 0x00045D00
		public static LocalizedString ReportDestinationMailboxResetFailed3(string errorType, int currentAttempt, int totalAttempts)
		{
			return new LocalizedString("ReportDestinationMailboxResetFailed3", "", false, false, MrsStrings.ResourceManager, new object[]
			{
				errorType,
				currentAttempt,
				totalAttempts
			});
		}

		// Token: 0x060020E5 RID: 8421 RVA: 0x00047B44 File Offset: 0x00045D44
		public static LocalizedString MoveHasBeenCanceled(Guid mbxGuid)
		{
			return new LocalizedString("MoveHasBeenCanceled", "Ex67DC2A", false, true, MrsStrings.ResourceManager, new object[]
			{
				mbxGuid
			});
		}

		// Token: 0x060020E6 RID: 8422 RVA: 0x00047B78 File Offset: 0x00045D78
		public static LocalizedString InvalidMoveRequest(string mailboxId)
		{
			return new LocalizedString("InvalidMoveRequest", "Ex11D5F8", false, true, MrsStrings.ResourceManager, new object[]
			{
				mailboxId
			});
		}

		// Token: 0x060020E7 RID: 8423 RVA: 0x00047BA8 File Offset: 0x00045DA8
		public static LocalizedString ErrorWlmResourceUnhealthy1(string resourceName, string resourceType, string wlmResourceKey, int wlmResourceMetricType, double reportedLoadRatio, string reportedLoadState, string metric)
		{
			return new LocalizedString("ErrorWlmResourceUnhealthy1", "", false, false, MrsStrings.ResourceManager, new object[]
			{
				resourceName,
				resourceType,
				wlmResourceKey,
				wlmResourceMetricType,
				reportedLoadRatio,
				reportedLoadState,
				metric
			});
		}

		// Token: 0x17000C6F RID: 3183
		// (get) Token: 0x060020E8 RID: 8424 RVA: 0x00047BFC File Offset: 0x00045DFC
		public static LocalizedString TaskCanceled
		{
			get
			{
				return new LocalizedString("TaskCanceled", "Ex6CBC51", false, true, MrsStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060020E9 RID: 8425 RVA: 0x00047C1C File Offset: 0x00045E1C
		public static LocalizedString ReportRelinquishBecauseMailboxIsLocked(DateTime pickupTime)
		{
			return new LocalizedString("ReportRelinquishBecauseMailboxIsLocked", "", false, false, MrsStrings.ResourceManager, new object[]
			{
				pickupTime
			});
		}

		// Token: 0x17000C70 RID: 3184
		// (get) Token: 0x060020EA RID: 8426 RVA: 0x00047C50 File Offset: 0x00045E50
		public static LocalizedString SourceMailboxAlreadyBeingMoved
		{
			get
			{
				return new LocalizedString("SourceMailboxAlreadyBeingMoved", "Ex7E66C2", false, true, MrsStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060020EB RID: 8427 RVA: 0x00047C70 File Offset: 0x00045E70
		public static LocalizedString ErrorWlmResourceUnhealthy(string resourceName, string resourceType, string wlmResourceKey, double reportedLoadRatio, string reportedLoadState, string metric)
		{
			return new LocalizedString("ErrorWlmResourceUnhealthy", "", false, false, MrsStrings.ResourceManager, new object[]
			{
				resourceName,
				resourceType,
				wlmResourceKey,
				reportedLoadRatio,
				reportedLoadState,
				metric
			});
		}

		// Token: 0x060020EC RID: 8428 RVA: 0x00047CBC File Offset: 0x00045EBC
		public static LocalizedString PickupStatusCreateJob(string syncStage, bool cancelRequest, string priority)
		{
			return new LocalizedString("PickupStatusCreateJob", "", false, false, MrsStrings.ResourceManager, new object[]
			{
				syncStage,
				cancelRequest,
				priority
			});
		}

		// Token: 0x060020ED RID: 8429 RVA: 0x00047CF8 File Offset: 0x00045EF8
		public static LocalizedString MailboxDatabaseNotFoundById(string mdbIdentity, LocalizedString notFoundReason)
		{
			return new LocalizedString("MailboxDatabaseNotFoundById", "ExD09830", false, true, MrsStrings.ResourceManager, new object[]
			{
				mdbIdentity,
				notFoundReason
			});
		}

		// Token: 0x17000C71 RID: 3185
		// (get) Token: 0x060020EE RID: 8430 RVA: 0x00047D30 File Offset: 0x00045F30
		public static LocalizedString MoveJobDeserializationFailed
		{
			get
			{
				return new LocalizedString("MoveJobDeserializationFailed", "Ex6EA931", false, true, MrsStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000C72 RID: 3186
		// (get) Token: 0x060020EF RID: 8431 RVA: 0x00047D4E File Offset: 0x00045F4E
		public static LocalizedString MoveRequestNotFoundInQueue
		{
			get
			{
				return new LocalizedString("MoveRequestNotFoundInQueue", "Ex7BDDE5", false, true, MrsStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000C73 RID: 3187
		// (get) Token: 0x060020F0 RID: 8432 RVA: 0x00047D6C File Offset: 0x00045F6C
		public static LocalizedString JobHasBeenCanceled
		{
			get
			{
				return new LocalizedString("JobHasBeenCanceled", "Ex5D2B5E", false, true, MrsStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060020F1 RID: 8433 RVA: 0x00047D8C File Offset: 0x00045F8C
		public static LocalizedString UnsupportedRecipientType(string recipient, string recipientType)
		{
			return new LocalizedString("UnsupportedRecipientType", "Ex936D59", false, true, MrsStrings.ResourceManager, new object[]
			{
				recipient,
				recipientType
			});
		}

		// Token: 0x060020F2 RID: 8434 RVA: 0x00047DC0 File Offset: 0x00045FC0
		public static LocalizedString SimpleValueDataContext(string name, string value)
		{
			return new LocalizedString("SimpleValueDataContext", "Ex7A0F13", false, true, MrsStrings.ResourceManager, new object[]
			{
				name,
				value
			});
		}

		// Token: 0x060020F3 RID: 8435 RVA: 0x00047DF4 File Offset: 0x00045FF4
		public static LocalizedString PropTagsDataContext(string propTagsStr)
		{
			return new LocalizedString("PropTagsDataContext", "Ex785429", false, true, MrsStrings.ResourceManager, new object[]
			{
				propTagsStr
			});
		}

		// Token: 0x17000C74 RID: 3188
		// (get) Token: 0x060020F4 RID: 8436 RVA: 0x00047E23 File Offset: 0x00046023
		public static LocalizedString ReportRequestStarted
		{
			get
			{
				return new LocalizedString("ReportRequestStarted", "Ex8B0872", false, true, MrsStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060020F5 RID: 8437 RVA: 0x00047E44 File Offset: 0x00046044
		public static LocalizedString ReportMergeInitialized(LocalizedString physicalMailboxId, int totalFolders, int messageCount, string totalSizeStr)
		{
			return new LocalizedString("ReportMergeInitialized", "Ex024C01", false, true, MrsStrings.ResourceManager, new object[]
			{
				physicalMailboxId,
				totalFolders,
				messageCount,
				totalSizeStr
			});
		}

		// Token: 0x060020F6 RID: 8438 RVA: 0x00047E90 File Offset: 0x00046090
		public static LocalizedString InvalidOperationError(string serviceURI, LocalizedString exceptionMessage)
		{
			return new LocalizedString("InvalidOperationError", "", false, false, MrsStrings.ResourceManager, new object[]
			{
				serviceURI,
				exceptionMessage
			});
		}

		// Token: 0x17000C75 RID: 3189
		// (get) Token: 0x060020F7 RID: 8439 RVA: 0x00047EC8 File Offset: 0x000460C8
		public static LocalizedString ErrorDownlevelClientsNotSupported
		{
			get
			{
				return new LocalizedString("ErrorDownlevelClientsNotSupported", "Ex9D981C", false, true, MrsStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000C76 RID: 3190
		// (get) Token: 0x060020F8 RID: 8440 RVA: 0x00047EE6 File Offset: 0x000460E6
		public static LocalizedString DataExportTimeout
		{
			get
			{
				return new LocalizedString("DataExportTimeout", "Ex66C917", false, true, MrsStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000C77 RID: 3191
		// (get) Token: 0x060020F9 RID: 8441 RVA: 0x00047F04 File Offset: 0x00046104
		public static LocalizedString TargetMailboxConnectionWasLost
		{
			get
			{
				return new LocalizedString("TargetMailboxConnectionWasLost", "", false, false, MrsStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060020FA RID: 8442 RVA: 0x00047F24 File Offset: 0x00046124
		public static LocalizedString ReportFailedToUpdateUserSD2(string errorType)
		{
			return new LocalizedString("ReportFailedToUpdateUserSD2", "", false, false, MrsStrings.ResourceManager, new object[]
			{
				errorType
			});
		}

		// Token: 0x060020FB RID: 8443 RVA: 0x00047F54 File Offset: 0x00046154
		public static LocalizedString ReportMoveRequestSet(string userName)
		{
			return new LocalizedString("ReportMoveRequestSet", "Ex6F6EDC", false, true, MrsStrings.ResourceManager, new object[]
			{
				userName
			});
		}

		// Token: 0x060020FC RID: 8444 RVA: 0x00047F84 File Offset: 0x00046184
		public static LocalizedString PickupStatusJobPoisoned(int poisionCount)
		{
			return new LocalizedString("PickupStatusJobPoisoned", "", false, false, MrsStrings.ResourceManager, new object[]
			{
				poisionCount
			});
		}

		// Token: 0x060020FD RID: 8445 RVA: 0x00047FB8 File Offset: 0x000461B8
		public static LocalizedString UnexpectedFilterType(string filterTypeName)
		{
			return new LocalizedString("UnexpectedFilterType", "ExBB52FB", false, true, MrsStrings.ResourceManager, new object[]
			{
				filterTypeName
			});
		}

		// Token: 0x17000C78 RID: 3192
		// (get) Token: 0x060020FE RID: 8446 RVA: 0x00047FE7 File Offset: 0x000461E7
		public static LocalizedString JobHasBeenRelinquishedDueToDatabaseFailover
		{
			get
			{
				return new LocalizedString("JobHasBeenRelinquishedDueToDatabaseFailover", "ExBC0F22", false, true, MrsStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060020FF RID: 8447 RVA: 0x00048008 File Offset: 0x00046208
		public static LocalizedString StoreIntegError(int error)
		{
			return new LocalizedString("StoreIntegError", "", false, false, MrsStrings.ResourceManager, new object[]
			{
				error
			});
		}

		// Token: 0x17000C79 RID: 3193
		// (get) Token: 0x06002100 RID: 8448 RVA: 0x0004803C File Offset: 0x0004623C
		public static LocalizedString PublicFolderMailboxesNotProvisionedForMigration
		{
			get
			{
				return new LocalizedString("PublicFolderMailboxesNotProvisionedForMigration", "", false, false, MrsStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06002101 RID: 8449 RVA: 0x0004805C File Offset: 0x0004625C
		public static LocalizedString ErrorWlmCapacityExceeded3(string resourceName, string resourceType, string wlmResourceKey, int wlmResourceMetricType, int capacity)
		{
			return new LocalizedString("ErrorWlmCapacityExceeded3", "", false, false, MrsStrings.ResourceManager, new object[]
			{
				resourceName,
				resourceType,
				wlmResourceKey,
				wlmResourceMetricType,
				capacity
			});
		}

		// Token: 0x06002102 RID: 8450 RVA: 0x000480A8 File Offset: 0x000462A8
		public static LocalizedString ClusterIPNotFound(IPAddress clusterIp)
		{
			return new LocalizedString("ClusterIPNotFound", "", false, false, MrsStrings.ResourceManager, new object[]
			{
				clusterIp
			});
		}

		// Token: 0x17000C7A RID: 3194
		// (get) Token: 0x06002103 RID: 8451 RVA: 0x000480D7 File Offset: 0x000462D7
		public static LocalizedString RequestPriorityHigher
		{
			get
			{
				return new LocalizedString("RequestPriorityHigher", "", false, false, MrsStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000C7B RID: 3195
		// (get) Token: 0x06002104 RID: 8452 RVA: 0x000480F5 File Offset: 0x000462F5
		public static LocalizedString JobHasBeenRelinquishedDueToHAOrCIStalls
		{
			get
			{
				return new LocalizedString("JobHasBeenRelinquishedDueToHAOrCIStalls", "", false, false, MrsStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000C7C RID: 3196
		// (get) Token: 0x06002105 RID: 8453 RVA: 0x00048113 File Offset: 0x00046313
		public static LocalizedString ReportRequestCanceled
		{
			get
			{
				return new LocalizedString("ReportRequestCanceled", "ExAEFE90", false, true, MrsStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000C7D RID: 3197
		// (get) Token: 0x06002106 RID: 8454 RVA: 0x00048131 File Offset: 0x00046331
		public static LocalizedString InvalidProxyOperationOrder
		{
			get
			{
				return new LocalizedString("InvalidProxyOperationOrder", "ExEA3D43", false, true, MrsStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06002107 RID: 8455 RVA: 0x00048150 File Offset: 0x00046350
		public static LocalizedString InvalidEscapedChar(string folderPath, int charPosition)
		{
			return new LocalizedString("InvalidEscapedChar", "Ex6B03CE", false, true, MrsStrings.ResourceManager, new object[]
			{
				folderPath,
				charPosition
			});
		}

		// Token: 0x06002108 RID: 8456 RVA: 0x00048188 File Offset: 0x00046388
		public static LocalizedString ReportFailedToApplySearchCondition(string folderName, string errorType, LocalizedString error, string trace, LocalizedString dataContext)
		{
			return new LocalizedString("ReportFailedToApplySearchCondition", "Ex04A902", false, true, MrsStrings.ResourceManager, new object[]
			{
				folderName,
				errorType,
				error,
				trace,
				dataContext
			});
		}

		// Token: 0x06002109 RID: 8457 RVA: 0x000481D4 File Offset: 0x000463D4
		public static LocalizedString BadItemMissingFolder(string folderName)
		{
			return new LocalizedString("BadItemMissingFolder", "", false, false, MrsStrings.ResourceManager, new object[]
			{
				folderName
			});
		}

		// Token: 0x17000C7E RID: 3198
		// (get) Token: 0x0600210A RID: 8458 RVA: 0x00048203 File Offset: 0x00046403
		public static LocalizedString ReportRequestOfflineMovePostponed
		{
			get
			{
				return new LocalizedString("ReportRequestOfflineMovePostponed", "", false, false, MrsStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600210B RID: 8459 RVA: 0x00048224 File Offset: 0x00046424
		public static LocalizedString FolderAliasIsInvalid(string folderAlias)
		{
			return new LocalizedString("FolderAliasIsInvalid", "ExDAD81E", false, true, MrsStrings.ResourceManager, new object[]
			{
				folderAlias
			});
		}

		// Token: 0x17000C7F RID: 3199
		// (get) Token: 0x0600210C RID: 8460 RVA: 0x00048253 File Offset: 0x00046453
		public static LocalizedString MailboxIsBeingMoved
		{
			get
			{
				return new LocalizedString("MailboxIsBeingMoved", "Ex0B3BF6", false, true, MrsStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600210D RID: 8461 RVA: 0x00048274 File Offset: 0x00046474
		public static LocalizedString FolderDataContextGeneric(string folderName, string entryId, string parentId)
		{
			return new LocalizedString("FolderDataContextGeneric", "Ex4EFBAA", false, true, MrsStrings.ResourceManager, new object[]
			{
				folderName,
				entryId,
				parentId
			});
		}

		// Token: 0x0600210E RID: 8462 RVA: 0x000482AC File Offset: 0x000464AC
		public static LocalizedString ReportFailingMoveBecauseSyncStateIssue(string mbxId)
		{
			return new LocalizedString("ReportFailingMoveBecauseSyncStateIssue", "", false, false, MrsStrings.ResourceManager, new object[]
			{
				mbxId
			});
		}

		// Token: 0x17000C80 RID: 3200
		// (get) Token: 0x0600210F RID: 8463 RVA: 0x000482DB File Offset: 0x000464DB
		public static LocalizedString NoSuchRequestInSpecifiedIndex
		{
			get
			{
				return new LocalizedString("NoSuchRequestInSpecifiedIndex", "", false, false, MrsStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06002110 RID: 8464 RVA: 0x000482FC File Offset: 0x000464FC
		public static LocalizedString ReportMailboxRemovedRetrying(int delaySecs)
		{
			return new LocalizedString("ReportMailboxRemovedRetrying", "ExF9A9F7", false, true, MrsStrings.ResourceManager, new object[]
			{
				delaySecs
			});
		}

		// Token: 0x17000C81 RID: 3201
		// (get) Token: 0x06002111 RID: 8465 RVA: 0x00048330 File Offset: 0x00046530
		public static LocalizedString InitializedWithInvalidObjectId
		{
			get
			{
				return new LocalizedString("InitializedWithInvalidObjectId", "", false, false, MrsStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000C82 RID: 3202
		// (get) Token: 0x06002112 RID: 8466 RVA: 0x0004834E File Offset: 0x0004654E
		public static LocalizedString ReportCopyPerUserReadUnreadDataCompleted
		{
			get
			{
				return new LocalizedString("ReportCopyPerUserReadUnreadDataCompleted", "", false, false, MrsStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06002113 RID: 8467 RVA: 0x0004836C File Offset: 0x0004656C
		public static LocalizedString ReportRequestSet(string userName)
		{
			return new LocalizedString("ReportRequestSet", "Ex82B05E", false, true, MrsStrings.ResourceManager, new object[]
			{
				userName
			});
		}

		// Token: 0x06002114 RID: 8468 RVA: 0x0004839C File Offset: 0x0004659C
		public static LocalizedString PublicFolderMoveTracingId(string orgID, Guid mbxGuid)
		{
			return new LocalizedString("PublicFolderMoveTracingId", "", false, false, MrsStrings.ResourceManager, new object[]
			{
				orgID,
				mbxGuid
			});
		}

		// Token: 0x17000C83 RID: 3203
		// (get) Token: 0x06002115 RID: 8469 RVA: 0x000483D4 File Offset: 0x000465D4
		public static LocalizedString ReportSessionStatisticsUpdated
		{
			get
			{
				return new LocalizedString("ReportSessionStatisticsUpdated", "", false, false, MrsStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000C84 RID: 3204
		// (get) Token: 0x06002116 RID: 8470 RVA: 0x000483F2 File Offset: 0x000465F2
		public static LocalizedString ReportRelinquishingJobDueToServerThrottling
		{
			get
			{
				return new LocalizedString("ReportRelinquishingJobDueToServerThrottling", "", false, false, MrsStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06002117 RID: 8471 RVA: 0x00048410 File Offset: 0x00046610
		public static LocalizedString UnableToReadPSTMessage(string filePath, uint messageId)
		{
			return new LocalizedString("UnableToReadPSTMessage", "Ex7AC263", false, true, MrsStrings.ResourceManager, new object[]
			{
				filePath,
				messageId
			});
		}

		// Token: 0x06002118 RID: 8472 RVA: 0x00048448 File Offset: 0x00046648
		public static LocalizedString ReportTransientExceptionOccurred(string errorType, LocalizedString errorMsg, string trace, int retryCount, int maxRetries, LocalizedString context)
		{
			return new LocalizedString("ReportTransientExceptionOccurred", "ExA03465", false, true, MrsStrings.ResourceManager, new object[]
			{
				errorType,
				errorMsg,
				trace,
				retryCount,
				maxRetries,
				context
			});
		}

		// Token: 0x06002119 RID: 8473 RVA: 0x000484A4 File Offset: 0x000466A4
		public static LocalizedString IsIntegAttemptsExceededError(short attempts)
		{
			return new LocalizedString("IsIntegAttemptsExceededError", "", false, false, MrsStrings.ResourceManager, new object[]
			{
				attempts
			});
		}

		// Token: 0x17000C85 RID: 3205
		// (get) Token: 0x0600211A RID: 8474 RVA: 0x000484D8 File Offset: 0x000466D8
		public static LocalizedString MRSNotConfigured
		{
			get
			{
				return new LocalizedString("MRSNotConfigured", "ExF550B8", false, true, MrsStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000C86 RID: 3206
		// (get) Token: 0x0600211B RID: 8475 RVA: 0x000484F6 File Offset: 0x000466F6
		public static LocalizedString MailboxRootFolderNotFound
		{
			get
			{
				return new LocalizedString("MailboxRootFolderNotFound", "", false, false, MrsStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600211C RID: 8476 RVA: 0x00048514 File Offset: 0x00046714
		public static LocalizedString EasSyncFailedPermanently(string syncStatus, string httpStatus, string folderId)
		{
			return new LocalizedString("EasSyncFailedPermanently", "", false, false, MrsStrings.ResourceManager, new object[]
			{
				syncStatus,
				httpStatus,
				folderId
			});
		}

		// Token: 0x0600211D RID: 8477 RVA: 0x0004854C File Offset: 0x0004674C
		public static LocalizedString FolderDataContextRoot(string entryId)
		{
			return new LocalizedString("FolderDataContextRoot", "ExF4072D", false, true, MrsStrings.ResourceManager, new object[]
			{
				entryId
			});
		}

		// Token: 0x0600211E RID: 8478 RVA: 0x0004857C File Offset: 0x0004677C
		public static LocalizedString SourceMailboxIsNotInSourceMDB(Guid mdbGuid)
		{
			return new LocalizedString("SourceMailboxIsNotInSourceMDB", "Ex8BAFEE", false, true, MrsStrings.ResourceManager, new object[]
			{
				mdbGuid
			});
		}

		// Token: 0x0600211F RID: 8479 RVA: 0x000485B0 File Offset: 0x000467B0
		public static LocalizedString DestinationAddMoveHistoryEntryFailed(LocalizedString errorMsg)
		{
			return new LocalizedString("DestinationAddMoveHistoryEntryFailed", "Ex5E766E", false, true, MrsStrings.ResourceManager, new object[]
			{
				errorMsg
			});
		}

		// Token: 0x06002120 RID: 8480 RVA: 0x000485E4 File Offset: 0x000467E4
		public static LocalizedString ReportReplaySyncStateNull(Guid requestGuid)
		{
			return new LocalizedString("ReportReplaySyncStateNull", "", false, false, MrsStrings.ResourceManager, new object[]
			{
				requestGuid
			});
		}

		// Token: 0x06002121 RID: 8481 RVA: 0x00048618 File Offset: 0x00046818
		public static LocalizedString JobIsPoisoned(int poisonCount)
		{
			return new LocalizedString("JobIsPoisoned", "", false, false, MrsStrings.ResourceManager, new object[]
			{
				poisonCount
			});
		}

		// Token: 0x17000C87 RID: 3207
		// (get) Token: 0x06002122 RID: 8482 RVA: 0x0004864C File Offset: 0x0004684C
		public static LocalizedString WorkloadTypeLoadBalancing
		{
			get
			{
				return new LocalizedString("WorkloadTypeLoadBalancing", "", false, false, MrsStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000C88 RID: 3208
		// (get) Token: 0x06002123 RID: 8483 RVA: 0x0004866A File Offset: 0x0004686A
		public static LocalizedString JobIsQuarantined
		{
			get
			{
				return new LocalizedString("JobIsQuarantined", "", false, false, MrsStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06002124 RID: 8484 RVA: 0x00048688 File Offset: 0x00046888
		public static LocalizedString ValidationFlagsMismatch2(string jobFlags, string indexFlags)
		{
			return new LocalizedString("ValidationFlagsMismatch2", "ExCA5911", false, true, MrsStrings.ResourceManager, new object[]
			{
				jobFlags,
				indexFlags
			});
		}

		// Token: 0x17000C89 RID: 3209
		// (get) Token: 0x06002125 RID: 8485 RVA: 0x000486BB File Offset: 0x000468BB
		public static LocalizedString ReportSourceSDCannotBeRead
		{
			get
			{
				return new LocalizedString("ReportSourceSDCannotBeRead", "", false, false, MrsStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000C8A RID: 3210
		// (get) Token: 0x06002126 RID: 8486 RVA: 0x000486D9 File Offset: 0x000468D9
		public static LocalizedString ReportMoveRequestIsNoLongerSticky
		{
			get
			{
				return new LocalizedString("ReportMoveRequestIsNoLongerSticky", "", false, false, MrsStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06002127 RID: 8487 RVA: 0x000486F8 File Offset: 0x000468F8
		public static LocalizedString InvalidDataError(string serviceURI, LocalizedString exceptionMessage)
		{
			return new LocalizedString("InvalidDataError", "", false, false, MrsStrings.ResourceManager, new object[]
			{
				serviceURI,
				exceptionMessage
			});
		}

		// Token: 0x17000C8B RID: 3211
		// (get) Token: 0x06002128 RID: 8488 RVA: 0x00048730 File Offset: 0x00046930
		public static LocalizedString ClusterNotFound
		{
			get
			{
				return new LocalizedString("ClusterNotFound", "", false, false, MrsStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06002129 RID: 8489 RVA: 0x00048750 File Offset: 0x00046950
		public static LocalizedString MoveRequestMessageError(LocalizedString message)
		{
			return new LocalizedString("MoveRequestMessageError", "Ex4657E1", false, true, MrsStrings.ResourceManager, new object[]
			{
				message
			});
		}

		// Token: 0x0600212A RID: 8490 RVA: 0x00048784 File Offset: 0x00046984
		public static LocalizedString UnsupportedClientVersion(string clientVersion)
		{
			return new LocalizedString("UnsupportedClientVersion", "ExA7EA19", false, true, MrsStrings.ResourceManager, new object[]
			{
				clientVersion
			});
		}

		// Token: 0x0600212B RID: 8491 RVA: 0x000487B4 File Offset: 0x000469B4
		public static LocalizedString ICSViewDataContext(string icsViewStr)
		{
			return new LocalizedString("ICSViewDataContext", "", false, false, MrsStrings.ResourceManager, new object[]
			{
				icsViewStr
			});
		}

		// Token: 0x17000C8C RID: 3212
		// (get) Token: 0x0600212C RID: 8492 RVA: 0x000487E3 File Offset: 0x000469E3
		public static LocalizedString MoveRestartDueToIsIntegCheck
		{
			get
			{
				return new LocalizedString("MoveRestartDueToIsIntegCheck", "", false, false, MrsStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000C8D RID: 3213
		// (get) Token: 0x0600212D RID: 8493 RVA: 0x00048801 File Offset: 0x00046A01
		public static LocalizedString ReportJobIsStillStalled
		{
			get
			{
				return new LocalizedString("ReportJobIsStillStalled", "ExF99E7D", false, true, MrsStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600212E RID: 8494 RVA: 0x00048820 File Offset: 0x00046A20
		public static LocalizedString ValidationNoIndexEntryForRequest(string requestId)
		{
			return new LocalizedString("ValidationNoIndexEntryForRequest", "Ex219CEC", false, true, MrsStrings.ResourceManager, new object[]
			{
				requestId
			});
		}

		// Token: 0x0600212F RID: 8495 RVA: 0x00048850 File Offset: 0x00046A50
		public static LocalizedString MailboxSettingsJunkMailError(string collectionName, string itemList, string validationError)
		{
			return new LocalizedString("MailboxSettingsJunkMailError", "", false, false, MrsStrings.ResourceManager, new object[]
			{
				collectionName,
				itemList,
				validationError
			});
		}

		// Token: 0x17000C8E RID: 3214
		// (get) Token: 0x06002130 RID: 8496 RVA: 0x00048887 File Offset: 0x00046A87
		public static LocalizedString WorkloadTypeRemotePstIngestion
		{
			get
			{
				return new LocalizedString("WorkloadTypeRemotePstIngestion", "", false, false, MrsStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06002131 RID: 8497 RVA: 0x000488A8 File Offset: 0x00046AA8
		public static LocalizedString ReportInitialSyncCheckpointCreationProgress(int foldersProcessed, int totalFolders, LocalizedString physicalMailboxId)
		{
			return new LocalizedString("ReportInitialSyncCheckpointCreationProgress", "", false, false, MrsStrings.ResourceManager, new object[]
			{
				foldersProcessed,
				totalFolders,
				physicalMailboxId
			});
		}

		// Token: 0x06002132 RID: 8498 RVA: 0x000488F0 File Offset: 0x00046AF0
		public static LocalizedString NestedExceptionMsg(LocalizedString message, LocalizedString innerMessage)
		{
			return new LocalizedString("NestedExceptionMsg", "Ex9D5913", false, true, MrsStrings.ResourceManager, new object[]
			{
				message,
				innerMessage
			});
		}

		// Token: 0x17000C8F RID: 3215
		// (get) Token: 0x06002133 RID: 8499 RVA: 0x0004892D File Offset: 0x00046B2D
		public static LocalizedString ReportPrimaryMservEntryPointsToExo
		{
			get
			{
				return new LocalizedString("ReportPrimaryMservEntryPointsToExo", "", false, false, MrsStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06002134 RID: 8500 RVA: 0x0004894C File Offset: 0x00046B4C
		public static LocalizedString EasFetchFailedPermanently(string ioStatus, string httpStatus, string folderId, string messageId)
		{
			return new LocalizedString("EasFetchFailedPermanently", "", false, false, MrsStrings.ResourceManager, new object[]
			{
				ioStatus,
				httpStatus,
				folderId,
				messageId
			});
		}

		// Token: 0x06002135 RID: 8501 RVA: 0x00048988 File Offset: 0x00046B88
		public static LocalizedString MdbNotOnServer(string mdbName, Guid mdbId, string mdbServerFqdn, string currentServerFqdn)
		{
			return new LocalizedString("MdbNotOnServer", "", false, false, MrsStrings.ResourceManager, new object[]
			{
				mdbName,
				mdbId,
				mdbServerFqdn,
				currentServerFqdn
			});
		}

		// Token: 0x06002136 RID: 8502 RVA: 0x000489C8 File Offset: 0x00046BC8
		public static LocalizedString RPCHTTPMailboxId(string legDN)
		{
			return new LocalizedString("RPCHTTPMailboxId", "Ex9A73BC", false, true, MrsStrings.ResourceManager, new object[]
			{
				legDN
			});
		}

		// Token: 0x06002137 RID: 8503 RVA: 0x000489F8 File Offset: 0x00046BF8
		public static LocalizedString ValidationObjectInvolvedInMultipleRelocations(LocalizedString objectInvolved, string requestGuids)
		{
			return new LocalizedString("ValidationObjectInvolvedInMultipleRelocations", "", false, false, MrsStrings.ResourceManager, new object[]
			{
				objectInvolved,
				requestGuids
			});
		}

		// Token: 0x06002138 RID: 8504 RVA: 0x00048A30 File Offset: 0x00046C30
		public static LocalizedString ReportDestinationMailboxSeedMBICacheFailed2(string errorType, int currentAttempt, int totalAttempts)
		{
			return new LocalizedString("ReportDestinationMailboxSeedMBICacheFailed2", "", false, false, MrsStrings.ResourceManager, new object[]
			{
				errorType,
				currentAttempt,
				totalAttempts
			});
		}

		// Token: 0x06002139 RID: 8505 RVA: 0x00048A74 File Offset: 0x00046C74
		public static LocalizedString UnknownSecurityProp(int securityProp)
		{
			return new LocalizedString("UnknownSecurityProp", "", false, false, MrsStrings.ResourceManager, new object[]
			{
				securityProp
			});
		}

		// Token: 0x0600213A RID: 8506 RVA: 0x00048AA8 File Offset: 0x00046CA8
		public static LocalizedString ReportMovedMailboxMorphedToMailUser(string domainController)
		{
			return new LocalizedString("ReportMovedMailboxMorphedToMailUser", "", false, false, MrsStrings.ResourceManager, new object[]
			{
				domainController
			});
		}

		// Token: 0x17000C90 RID: 3216
		// (get) Token: 0x0600213B RID: 8507 RVA: 0x00048AD7 File Offset: 0x00046CD7
		public static LocalizedString ValidationADUserIsNotBeingMoved
		{
			get
			{
				return new LocalizedString("ValidationADUserIsNotBeingMoved", "ExB8C59E", false, true, MrsStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600213C RID: 8508 RVA: 0x00048AF8 File Offset: 0x00046CF8
		public static LocalizedString RemoteServerName(string serverName)
		{
			return new LocalizedString("RemoteServerName", "ExC45085", false, true, MrsStrings.ResourceManager, new object[]
			{
				serverName
			});
		}

		// Token: 0x0600213D RID: 8509 RVA: 0x00048B28 File Offset: 0x00046D28
		public static LocalizedString ReportMailboxInfoBeforeMove(string mailboxInfoString)
		{
			return new LocalizedString("ReportMailboxInfoBeforeMove", "Ex69C3B4", false, true, MrsStrings.ResourceManager, new object[]
			{
				mailboxInfoString
			});
		}

		// Token: 0x0600213E RID: 8510 RVA: 0x00048B58 File Offset: 0x00046D58
		public static LocalizedString ErrorCouldNotFindMoveRequest(string identity)
		{
			return new LocalizedString("ErrorCouldNotFindMoveRequest", "", false, false, MrsStrings.ResourceManager, new object[]
			{
				identity
			});
		}

		// Token: 0x17000C91 RID: 3217
		// (get) Token: 0x0600213F RID: 8511 RVA: 0x00048B87 File Offset: 0x00046D87
		public static LocalizedString PostMoveStateIsUncertain
		{
			get
			{
				return new LocalizedString("PostMoveStateIsUncertain", "", false, false, MrsStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06002140 RID: 8512 RVA: 0x00048BA8 File Offset: 0x00046DA8
		public static LocalizedString UnexpectedValue(string value, string parameterName)
		{
			return new LocalizedString("UnexpectedValue", "", false, false, MrsStrings.ResourceManager, new object[]
			{
				value,
				parameterName
			});
		}

		// Token: 0x06002141 RID: 8513 RVA: 0x00048BDC File Offset: 0x00046DDC
		public static LocalizedString RestoreMailboxId(Guid mailboxGuid)
		{
			return new LocalizedString("RestoreMailboxId", "Ex5477E9", false, true, MrsStrings.ResourceManager, new object[]
			{
				mailboxGuid
			});
		}

		// Token: 0x06002142 RID: 8514 RVA: 0x00048C10 File Offset: 0x00046E10
		public static LocalizedString UnableToSavePSTSyncState(string filePath)
		{
			return new LocalizedString("UnableToSavePSTSyncState", "Ex934321", false, true, MrsStrings.ResourceManager, new object[]
			{
				filePath
			});
		}

		// Token: 0x06002143 RID: 8515 RVA: 0x00048C40 File Offset: 0x00046E40
		public static LocalizedString MissingDatabaseName2(Guid dbGuid, string forestFqdn)
		{
			return new LocalizedString("MissingDatabaseName2", "", false, false, MrsStrings.ResourceManager, new object[]
			{
				dbGuid,
				forestFqdn
			});
		}

		// Token: 0x06002144 RID: 8516 RVA: 0x00048C78 File Offset: 0x00046E78
		public static LocalizedString ReportIncrementalSyncContentChangesSynced(LocalizedString physicalMailboxId, int messageChanges)
		{
			return new LocalizedString("ReportIncrementalSyncContentChangesSynced", "", false, false, MrsStrings.ResourceManager, new object[]
			{
				physicalMailboxId,
				messageChanges
			});
		}

		// Token: 0x17000C92 RID: 3218
		// (get) Token: 0x06002145 RID: 8517 RVA: 0x00048CB5 File Offset: 0x00046EB5
		public static LocalizedString RequestPriorityHigh
		{
			get
			{
				return new LocalizedString("RequestPriorityHigh", "", false, false, MrsStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06002146 RID: 8518 RVA: 0x00048CD4 File Offset: 0x00046ED4
		public static LocalizedString MRSProxyConnectionLimitReachedError(int activeConnections, int connectionLimit)
		{
			return new LocalizedString("MRSProxyConnectionLimitReachedError", "Ex56F050", false, true, MrsStrings.ResourceManager, new object[]
			{
				activeConnections,
				connectionLimit
			});
		}

		// Token: 0x06002147 RID: 8519 RVA: 0x00048D14 File Offset: 0x00046F14
		public static LocalizedString ValidationUserIsNotInAD(string mrUserId)
		{
			return new LocalizedString("ValidationUserIsNotInAD", "ExDC37E6", false, true, MrsStrings.ResourceManager, new object[]
			{
				mrUserId
			});
		}

		// Token: 0x17000C93 RID: 3219
		// (get) Token: 0x06002148 RID: 8520 RVA: 0x00048D43 File Offset: 0x00046F43
		public static LocalizedString SourceContainer
		{
			get
			{
				return new LocalizedString("SourceContainer", "", false, false, MrsStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06002149 RID: 8521 RVA: 0x00048D64 File Offset: 0x00046F64
		public static LocalizedString ReportRequestRemoved(string userName)
		{
			return new LocalizedString("ReportRequestRemoved", "ExA3AF49", false, true, MrsStrings.ResourceManager, new object[]
			{
				userName
			});
		}

		// Token: 0x0600214A RID: 8522 RVA: 0x00048D94 File Offset: 0x00046F94
		public static LocalizedString ReportRequestCreated(string userName)
		{
			return new LocalizedString("ReportRequestCreated", "Ex484BDB", false, true, MrsStrings.ResourceManager, new object[]
			{
				userName
			});
		}

		// Token: 0x17000C94 RID: 3220
		// (get) Token: 0x0600214B RID: 8523 RVA: 0x00048DC3 File Offset: 0x00046FC3
		public static LocalizedString WorkloadTypeTenantUpgrade
		{
			get
			{
				return new LocalizedString("WorkloadTypeTenantUpgrade", "", false, false, MrsStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600214C RID: 8524 RVA: 0x00048DE4 File Offset: 0x00046FE4
		public static LocalizedString DestinationMailboxResetFailed(LocalizedString errorMsg)
		{
			return new LocalizedString("DestinationMailboxResetFailed", "ExCFFF85", false, true, MrsStrings.ResourceManager, new object[]
			{
				errorMsg
			});
		}

		// Token: 0x0600214D RID: 8525 RVA: 0x00048E18 File Offset: 0x00047018
		public static LocalizedString ReportMoveAlreadyFinished2(LocalizedString reasonForMoveFinished)
		{
			return new LocalizedString("ReportMoveAlreadyFinished2", "Ex7B031B", false, true, MrsStrings.ResourceManager, new object[]
			{
				reasonForMoveFinished
			});
		}

		// Token: 0x17000C95 RID: 3221
		// (get) Token: 0x0600214E RID: 8526 RVA: 0x00048E4C File Offset: 0x0004704C
		public static LocalizedString EasMissingMessageCategory
		{
			get
			{
				return new LocalizedString("EasMissingMessageCategory", "", false, false, MrsStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600214F RID: 8527 RVA: 0x00048E6C File Offset: 0x0004706C
		public static LocalizedString PositionInteger(int position)
		{
			return new LocalizedString("PositionInteger", "Ex0C9383", false, true, MrsStrings.ResourceManager, new object[]
			{
				position
			});
		}

		// Token: 0x06002150 RID: 8528 RVA: 0x00048EA0 File Offset: 0x000470A0
		public static LocalizedString ValidationMoveRequestInWrongMDB(Guid originatingMdbGuid, Guid mrQueueGuid)
		{
			return new LocalizedString("ValidationMoveRequestInWrongMDB", "ExC890DD", false, true, MrsStrings.ResourceManager, new object[]
			{
				originatingMdbGuid,
				mrQueueGuid
			});
		}

		// Token: 0x06002151 RID: 8529 RVA: 0x00048EE0 File Offset: 0x000470E0
		public static LocalizedString BadItemMissingItem(string msgClass, string subject, string folderName)
		{
			return new LocalizedString("BadItemMissingItem", "", false, false, MrsStrings.ResourceManager, new object[]
			{
				msgClass,
				subject,
				folderName
			});
		}

		// Token: 0x06002152 RID: 8530 RVA: 0x00048F18 File Offset: 0x00047118
		public static LocalizedString CrossSiteError(Guid mdbGuid, Guid serverGuid, string serverSite, string localSite)
		{
			return new LocalizedString("CrossSiteError", "", false, false, MrsStrings.ResourceManager, new object[]
			{
				mdbGuid,
				serverGuid,
				serverSite,
				localSite
			});
		}

		// Token: 0x06002153 RID: 8531 RVA: 0x00048F60 File Offset: 0x00047160
		public static LocalizedString ReportSourceMailboxCleanupFailed3(LocalizedString mailboxId, string errorType, int currentAttempt, int totalAttempts)
		{
			return new LocalizedString("ReportSourceMailboxCleanupFailed3", "", false, false, MrsStrings.ResourceManager, new object[]
			{
				mailboxId,
				errorType,
				currentAttempt,
				totalAttempts
			});
		}

		// Token: 0x06002154 RID: 8532 RVA: 0x00048FAC File Offset: 0x000471AC
		public static LocalizedString ReportRequestIsSticky(string stickyServer)
		{
			return new LocalizedString("ReportRequestIsSticky", "ExB16C26", false, true, MrsStrings.ResourceManager, new object[]
			{
				stickyServer
			});
		}

		// Token: 0x06002155 RID: 8533 RVA: 0x00048FDC File Offset: 0x000471DC
		public static LocalizedString SettingRehomeOnRelatedRequestsFailed(LocalizedString errorMsg)
		{
			return new LocalizedString("SettingRehomeOnRelatedRequestsFailed", "", false, false, MrsStrings.ResourceManager, new object[]
			{
				errorMsg
			});
		}

		// Token: 0x06002156 RID: 8534 RVA: 0x00049010 File Offset: 0x00047210
		public static LocalizedString FolderHierarchyContainsParentChainLoop(string folderIdStr)
		{
			return new LocalizedString("FolderHierarchyContainsParentChainLoop", "", false, false, MrsStrings.ResourceManager, new object[]
			{
				folderIdStr
			});
		}

		// Token: 0x06002157 RID: 8535 RVA: 0x00049040 File Offset: 0x00047240
		public static LocalizedString ReportRestartingMoveBecauseSyncStateDoesNotExist(string mbxId)
		{
			return new LocalizedString("ReportRestartingMoveBecauseSyncStateDoesNotExist", "", false, false, MrsStrings.ResourceManager, new object[]
			{
				mbxId
			});
		}

		// Token: 0x17000C96 RID: 3222
		// (get) Token: 0x06002158 RID: 8536 RVA: 0x0004906F File Offset: 0x0004726F
		public static LocalizedString JobHasBeenRelinquished
		{
			get
			{
				return new LocalizedString("JobHasBeenRelinquished", "ExBE7BFD", false, true, MrsStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06002159 RID: 8537 RVA: 0x00049090 File Offset: 0x00047290
		public static LocalizedString CertificateLoadError(string certificateName, string errorMessage)
		{
			return new LocalizedString("CertificateLoadError", "", false, false, MrsStrings.ResourceManager, new object[]
			{
				certificateName,
				errorMessage
			});
		}

		// Token: 0x0600215A RID: 8538 RVA: 0x000490C4 File Offset: 0x000472C4
		public static LocalizedString KBytesPerSec(double kbytesPerSec)
		{
			return new LocalizedString("KBytesPerSec", "Ex56AEBC", false, true, MrsStrings.ResourceManager, new object[]
			{
				kbytesPerSec
			});
		}

		// Token: 0x0600215B RID: 8539 RVA: 0x000490F8 File Offset: 0x000472F8
		public static LocalizedString ValidationTargetUserMismatch(string jobTgtUser, string indexTgtUser)
		{
			return new LocalizedString("ValidationTargetUserMismatch", "Ex3C4DD6", false, true, MrsStrings.ResourceManager, new object[]
			{
				jobTgtUser,
				indexTgtUser
			});
		}

		// Token: 0x0600215C RID: 8540 RVA: 0x0004912C File Offset: 0x0004732C
		public static LocalizedString OperationDataContext(string operation)
		{
			return new LocalizedString("OperationDataContext", "Ex675D9C", false, true, MrsStrings.ResourceManager, new object[]
			{
				operation
			});
		}

		// Token: 0x0600215D RID: 8541 RVA: 0x0004915C File Offset: 0x0004735C
		public static LocalizedString ReportRelinquishBecauseResourceReservationFailed(LocalizedString error)
		{
			return new LocalizedString("ReportRelinquishBecauseResourceReservationFailed", "", false, false, MrsStrings.ResourceManager, new object[]
			{
				error
			});
		}

		// Token: 0x17000C97 RID: 3223
		// (get) Token: 0x0600215E RID: 8542 RVA: 0x00049190 File Offset: 0x00047390
		public static LocalizedString RecoverySyncNotImplemented
		{
			get
			{
				return new LocalizedString("RecoverySyncNotImplemented", "", false, false, MrsStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600215F RID: 8543 RVA: 0x000491B0 File Offset: 0x000473B0
		public static LocalizedString ReportIncrementalSyncCompleted2(LocalizedString physicalMailboxId, int numberOfHierarchyUpdates, int numberOfContentUpdates)
		{
			return new LocalizedString("ReportIncrementalSyncCompleted2", "", false, false, MrsStrings.ResourceManager, new object[]
			{
				physicalMailboxId,
				numberOfHierarchyUpdates,
				numberOfContentUpdates
			});
		}

		// Token: 0x06002160 RID: 8544 RVA: 0x000491F8 File Offset: 0x000473F8
		public static LocalizedString CommunicationError(string serviceURI, LocalizedString exceptionMessage)
		{
			return new LocalizedString("CommunicationError", "ExE8AAEA", false, true, MrsStrings.ResourceManager, new object[]
			{
				serviceURI,
				exceptionMessage
			});
		}

		// Token: 0x17000C98 RID: 3224
		// (get) Token: 0x06002161 RID: 8545 RVA: 0x00049230 File Offset: 0x00047430
		public static LocalizedString ErrorTooManyCleanupRetries
		{
			get
			{
				return new LocalizedString("ErrorTooManyCleanupRetries", "", false, false, MrsStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06002162 RID: 8546 RVA: 0x00049250 File Offset: 0x00047450
		public static LocalizedString ReportBadItemEncountered(string badItemData)
		{
			return new LocalizedString("ReportBadItemEncountered", "ExE0F6EA", false, true, MrsStrings.ResourceManager, new object[]
			{
				badItemData
			});
		}

		// Token: 0x06002163 RID: 8547 RVA: 0x00049280 File Offset: 0x00047480
		public static LocalizedString InvalidServerName(string serverName)
		{
			return new LocalizedString("InvalidServerName", "Ex5984CC", false, true, MrsStrings.ResourceManager, new object[]
			{
				serverName
			});
		}

		// Token: 0x17000C99 RID: 3225
		// (get) Token: 0x06002164 RID: 8548 RVA: 0x000492AF File Offset: 0x000474AF
		public static LocalizedString ReportFinalSyncStarted
		{
			get
			{
				return new LocalizedString("ReportFinalSyncStarted", "Ex1B5796", false, true, MrsStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06002165 RID: 8549 RVA: 0x000492D0 File Offset: 0x000474D0
		public static LocalizedString OnlineMoveNotSupported(string mbxGuid)
		{
			return new LocalizedString("OnlineMoveNotSupported", "", false, false, MrsStrings.ResourceManager, new object[]
			{
				mbxGuid
			});
		}

		// Token: 0x06002166 RID: 8550 RVA: 0x00049300 File Offset: 0x00047500
		public static LocalizedString UnsupportedRemoteServerVersion(string remoteServerAddress, string serverVersion)
		{
			return new LocalizedString("UnsupportedRemoteServerVersion", "Ex052E7B", false, true, MrsStrings.ResourceManager, new object[]
			{
				remoteServerAddress,
				serverVersion
			});
		}

		// Token: 0x17000C9A RID: 3226
		// (get) Token: 0x06002167 RID: 8551 RVA: 0x00049333 File Offset: 0x00047533
		public static LocalizedString ReportJobExitedStalledByThrottlingState
		{
			get
			{
				return new LocalizedString("ReportJobExitedStalledByThrottlingState", "", false, false, MrsStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06002168 RID: 8552 RVA: 0x00049354 File Offset: 0x00047554
		public static LocalizedString ReportBadItemEncountered2(LocalizedString badItemStr)
		{
			return new LocalizedString("ReportBadItemEncountered2", "", false, false, MrsStrings.ResourceManager, new object[]
			{
				badItemStr
			});
		}

		// Token: 0x17000C9B RID: 3227
		// (get) Token: 0x06002169 RID: 8553 RVA: 0x00049388 File Offset: 0x00047588
		public static LocalizedString MustProvideNonEmptyStringForIdentity
		{
			get
			{
				return new LocalizedString("MustProvideNonEmptyStringForIdentity", "", false, false, MrsStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000C9C RID: 3228
		// (get) Token: 0x0600216A RID: 8554 RVA: 0x000493A6 File Offset: 0x000475A6
		public static LocalizedString ReportRelinquishingJobDueToNeedForRehome
		{
			get
			{
				return new LocalizedString("ReportRelinquishingJobDueToNeedForRehome", "", false, false, MrsStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600216B RID: 8555 RVA: 0x000493C4 File Offset: 0x000475C4
		public static LocalizedString ReportSyncedJob(DateTime pikUpTime)
		{
			return new LocalizedString("ReportSyncedJob", "", false, false, MrsStrings.ResourceManager, new object[]
			{
				pikUpTime
			});
		}

		// Token: 0x17000C9D RID: 3229
		// (get) Token: 0x0600216C RID: 8556 RVA: 0x000493F8 File Offset: 0x000475F8
		public static LocalizedString NotEnoughInformationSupplied
		{
			get
			{
				return new LocalizedString("NotEnoughInformationSupplied", "", false, false, MrsStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600216D RID: 8557 RVA: 0x00049418 File Offset: 0x00047618
		public static LocalizedString ValidationRequestTypeMismatch(string jobType, string indexType)
		{
			return new LocalizedString("ValidationRequestTypeMismatch", "ExFD2B3F", false, true, MrsStrings.ResourceManager, new object[]
			{
				jobType,
				indexType
			});
		}

		// Token: 0x17000C9E RID: 3230
		// (get) Token: 0x0600216E RID: 8558 RVA: 0x0004944B File Offset: 0x0004764B
		public static LocalizedString NoDataContext
		{
			get
			{
				return new LocalizedString("NoDataContext", "Ex488979", false, true, MrsStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600216F RID: 8559 RVA: 0x0004946C File Offset: 0x0004766C
		public static LocalizedString RecipientIsNotAMailbox(string recipient)
		{
			return new LocalizedString("RecipientIsNotAMailbox", "Ex3D8115", false, true, MrsStrings.ResourceManager, new object[]
			{
				recipient
			});
		}

		// Token: 0x06002170 RID: 8560 RVA: 0x0004949C File Offset: 0x0004769C
		public static LocalizedString ReportFailedToDisconnectFromSource(string errorType, LocalizedString errorMsg, string trace)
		{
			return new LocalizedString("ReportFailedToDisconnectFromSource", "Ex247F65", false, true, MrsStrings.ResourceManager, new object[]
			{
				errorType,
				errorMsg,
				trace
			});
		}

		// Token: 0x06002171 RID: 8561 RVA: 0x000494D8 File Offset: 0x000476D8
		public static LocalizedString UnableToCreateToken(string user)
		{
			return new LocalizedString("UnableToCreateToken", "ExCFA47C", false, true, MrsStrings.ResourceManager, new object[]
			{
				user
			});
		}

		// Token: 0x17000C9F RID: 3231
		// (get) Token: 0x06002172 RID: 8562 RVA: 0x00049507 File Offset: 0x00047707
		public static LocalizedString ReportMoveCompleted
		{
			get
			{
				return new LocalizedString("ReportMoveCompleted", "Ex219D9F", false, true, MrsStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06002173 RID: 8563 RVA: 0x00049528 File Offset: 0x00047728
		public static LocalizedString EasFetchFailedError(string fetchStatus, string httpStatus, string folderId)
		{
			return new LocalizedString("EasFetchFailedError", "", false, false, MrsStrings.ResourceManager, new object[]
			{
				fetchStatus,
				httpStatus,
				folderId
			});
		}

		// Token: 0x06002174 RID: 8564 RVA: 0x00049560 File Offset: 0x00047760
		public static LocalizedString ReportMoveRequestIsSticky(string stickyServer)
		{
			return new LocalizedString("ReportMoveRequestIsSticky", "Ex159027", false, true, MrsStrings.ResourceManager, new object[]
			{
				stickyServer
			});
		}

		// Token: 0x06002175 RID: 8565 RVA: 0x00049590 File Offset: 0x00047790
		public static LocalizedString ReportMailboxArchiveInfoBeforeMoveLoc(LocalizedString mailboxInfoString)
		{
			return new LocalizedString("ReportMailboxArchiveInfoBeforeMoveLoc", "Ex6D940D", false, true, MrsStrings.ResourceManager, new object[]
			{
				mailboxInfoString
			});
		}

		// Token: 0x06002176 RID: 8566 RVA: 0x000495C4 File Offset: 0x000477C4
		public static LocalizedString ReportDestinationMailboxCleanupFailed2(string errorType)
		{
			return new LocalizedString("ReportDestinationMailboxCleanupFailed2", "", false, false, MrsStrings.ResourceManager, new object[]
			{
				errorType
			});
		}

		// Token: 0x17000CA0 RID: 3232
		// (get) Token: 0x06002177 RID: 8567 RVA: 0x000495F3 File Offset: 0x000477F3
		public static LocalizedString UnableToDeleteMoveRequestMessage
		{
			get
			{
				return new LocalizedString("UnableToDeleteMoveRequestMessage", "Ex24DBFE", false, true, MrsStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06002178 RID: 8568 RVA: 0x00049614 File Offset: 0x00047814
		public static LocalizedString JobIsStalled(string jobId, string mdbId, LocalizedString failureReason, string agentName, int agentId)
		{
			return new LocalizedString("JobIsStalled", "Ex9DAB41", false, true, MrsStrings.ResourceManager, new object[]
			{
				jobId,
				mdbId,
				failureReason,
				agentName,
				agentId
			});
		}

		// Token: 0x06002179 RID: 8569 RVA: 0x00049660 File Offset: 0x00047860
		public static LocalizedString ReportRequestResumedWithSuspendWhenReadyToComplete(string userName)
		{
			return new LocalizedString("ReportRequestResumedWithSuspendWhenReadyToComplete", "", false, false, MrsStrings.ResourceManager, new object[]
			{
				userName
			});
		}

		// Token: 0x17000CA1 RID: 3233
		// (get) Token: 0x0600217A RID: 8570 RVA: 0x0004968F File Offset: 0x0004788F
		public static LocalizedString DestinationFolderHierarchyInconsistent
		{
			get
			{
				return new LocalizedString("DestinationFolderHierarchyInconsistent", "", false, false, MrsStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600217B RID: 8571 RVA: 0x000496B0 File Offset: 0x000478B0
		public static LocalizedString BlockedType(string type)
		{
			return new LocalizedString("BlockedType", "", false, false, MrsStrings.ResourceManager, new object[]
			{
				type
			});
		}

		// Token: 0x0600217C RID: 8572 RVA: 0x000496E0 File Offset: 0x000478E0
		public static LocalizedString PositionIntegerPlus(int position)
		{
			return new LocalizedString("PositionIntegerPlus", "Ex14FD3C", false, true, MrsStrings.ResourceManager, new object[]
			{
				position
			});
		}

		// Token: 0x0600217D RID: 8573 RVA: 0x00049714 File Offset: 0x00047914
		public static LocalizedString MoveHasBeenRelinquished(Guid mbxGuid)
		{
			return new LocalizedString("MoveHasBeenRelinquished", "ExC86B5F", false, true, MrsStrings.ResourceManager, new object[]
			{
				mbxGuid
			});
		}

		// Token: 0x0600217E RID: 8574 RVA: 0x00049748 File Offset: 0x00047948
		public static LocalizedString FolderIsLive(string folderName)
		{
			return new LocalizedString("FolderIsLive", "", false, false, MrsStrings.ResourceManager, new object[]
			{
				folderName
			});
		}

		// Token: 0x0600217F RID: 8575 RVA: 0x00049778 File Offset: 0x00047978
		public static LocalizedString ValidationSourceMDBMismatch(string adDatabase, string mrDatabase)
		{
			return new LocalizedString("ValidationSourceMDBMismatch", "Ex73B8CA", false, true, MrsStrings.ResourceManager, new object[]
			{
				adDatabase,
				mrDatabase
			});
		}

		// Token: 0x06002180 RID: 8576 RVA: 0x000497AC File Offset: 0x000479AC
		public static LocalizedString ReportMergingFolder(string sourceFolderName, string targetFolderName)
		{
			return new LocalizedString("ReportMergingFolder", "ExB5FDAB", false, true, MrsStrings.ResourceManager, new object[]
			{
				sourceFolderName,
				targetFolderName
			});
		}

		// Token: 0x06002181 RID: 8577 RVA: 0x000497E0 File Offset: 0x000479E0
		public static LocalizedString ReportSourceMailboxBeforeFinalization(string userDataXML)
		{
			return new LocalizedString("ReportSourceMailboxBeforeFinalization", "ExDD9B47", false, true, MrsStrings.ResourceManager, new object[]
			{
				userDataXML
			});
		}

		// Token: 0x06002182 RID: 8578 RVA: 0x00049810 File Offset: 0x00047A10
		public static LocalizedString ValidationSourceArchiveMDBMismatch(string adDatabase, string mrDatabase)
		{
			return new LocalizedString("ValidationSourceArchiveMDBMismatch", "Ex3935BC", false, true, MrsStrings.ResourceManager, new object[]
			{
				adDatabase,
				mrDatabase
			});
		}

		// Token: 0x06002183 RID: 8579 RVA: 0x00049844 File Offset: 0x00047A44
		public static LocalizedString ReportDestinationMailboxClearSyncStateFailed2(string errorType, int currentAttempt, int totalAttempts)
		{
			return new LocalizedString("ReportDestinationMailboxClearSyncStateFailed2", "", false, false, MrsStrings.ResourceManager, new object[]
			{
				errorType,
				currentAttempt,
				totalAttempts
			});
		}

		// Token: 0x06002184 RID: 8580 RVA: 0x00049888 File Offset: 0x00047A88
		public static LocalizedString UnableToDetermineMDBSite(Guid mdbGuid)
		{
			return new LocalizedString("UnableToDetermineMDBSite", "Ex44A805", false, true, MrsStrings.ResourceManager, new object[]
			{
				mdbGuid
			});
		}

		// Token: 0x06002185 RID: 8581 RVA: 0x000498BC File Offset: 0x00047ABC
		public static LocalizedString KBytes(double kbytes)
		{
			return new LocalizedString("KBytes", "ExDBCEA5", false, true, MrsStrings.ResourceManager, new object[]
			{
				kbytes
			});
		}

		// Token: 0x17000CA2 RID: 3234
		// (get) Token: 0x06002186 RID: 8582 RVA: 0x000498F0 File Offset: 0x00047AF0
		public static LocalizedString NotEnoughInformationToFindMoveRequest
		{
			get
			{
				return new LocalizedString("NotEnoughInformationToFindMoveRequest", "Ex315C49", false, true, MrsStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06002187 RID: 8583 RVA: 0x00049910 File Offset: 0x00047B10
		public static LocalizedString StorageConnectionType(string type)
		{
			return new LocalizedString("StorageConnectionType", "", false, false, MrsStrings.ResourceManager, new object[]
			{
				type
			});
		}

		// Token: 0x06002188 RID: 8584 RVA: 0x00049940 File Offset: 0x00047B40
		public static LocalizedString PropTagToPropertyDefinitionConversion(int propTag)
		{
			return new LocalizedString("PropTagToPropertyDefinitionConversion", "", false, false, MrsStrings.ResourceManager, new object[]
			{
				propTag
			});
		}

		// Token: 0x17000CA3 RID: 3235
		// (get) Token: 0x06002189 RID: 8585 RVA: 0x00049974 File Offset: 0x00047B74
		public static LocalizedString TaskSchedulerStopped
		{
			get
			{
				return new LocalizedString("TaskSchedulerStopped", "Ex9F286D", false, true, MrsStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000CA4 RID: 3236
		// (get) Token: 0x0600218A RID: 8586 RVA: 0x00049992 File Offset: 0x00047B92
		public static LocalizedString ReportRelinquishingJobDueToCIStall
		{
			get
			{
				return new LocalizedString("ReportRelinquishingJobDueToCIStall", "", false, false, MrsStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600218B RID: 8587 RVA: 0x000499B0 File Offset: 0x00047BB0
		public static LocalizedString MailboxDataReplicationFailed(LocalizedString failureReason)
		{
			return new LocalizedString("MailboxDataReplicationFailed", "Ex5186AA", false, true, MrsStrings.ResourceManager, new object[]
			{
				failureReason
			});
		}

		// Token: 0x0600218C RID: 8588 RVA: 0x000499E4 File Offset: 0x00047BE4
		public static LocalizedString HandleNotFound(long handle)
		{
			return new LocalizedString("HandleNotFound", "ExAEA2A1", false, true, MrsStrings.ResourceManager, new object[]
			{
				handle
			});
		}

		// Token: 0x17000CA5 RID: 3237
		// (get) Token: 0x0600218D RID: 8589 RVA: 0x00049A18 File Offset: 0x00047C18
		public static LocalizedString WriteCpu
		{
			get
			{
				return new LocalizedString("WriteCpu", "", false, false, MrsStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600218E RID: 8590 RVA: 0x00049A38 File Offset: 0x00047C38
		public static LocalizedString MdbIsOffline(Guid mdbGuid)
		{
			return new LocalizedString("MdbIsOffline", "ExC2B9A8", false, true, MrsStrings.ResourceManager, new object[]
			{
				mdbGuid
			});
		}

		// Token: 0x0600218F RID: 8591 RVA: 0x00049A6C File Offset: 0x00047C6C
		public static LocalizedString UnableToLoadPSTSyncState(string filePath)
		{
			return new LocalizedString("UnableToLoadPSTSyncState", "Ex1A9EB5", false, true, MrsStrings.ResourceManager, new object[]
			{
				filePath
			});
		}

		// Token: 0x06002190 RID: 8592 RVA: 0x00049A9C File Offset: 0x00047C9C
		public static LocalizedString ReportSyncStateLoaded2(Guid requestGuid, int syncStateLength, int icsSyncStateLength, int replaySyncStateLength)
		{
			return new LocalizedString("ReportSyncStateLoaded2", "", false, false, MrsStrings.ResourceManager, new object[]
			{
				requestGuid,
				syncStateLength,
				icsSyncStateLength,
				replaySyncStateLength
			});
		}

		// Token: 0x06002191 RID: 8593 RVA: 0x00049AEC File Offset: 0x00047CEC
		public static LocalizedString ReportSoftDeletedItemCountsAndSizesInArchive(string softDeletedItems)
		{
			return new LocalizedString("ReportSoftDeletedItemCountsAndSizesInArchive", "Ex1F925D", false, true, MrsStrings.ResourceManager, new object[]
			{
				softDeletedItems
			});
		}

		// Token: 0x17000CA6 RID: 3238
		// (get) Token: 0x06002192 RID: 8594 RVA: 0x00049B1B File Offset: 0x00047D1B
		public static LocalizedString ReportHomeMdbPointsToTarget
		{
			get
			{
				return new LocalizedString("ReportHomeMdbPointsToTarget", "Ex47A346", false, true, MrsStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06002193 RID: 8595 RVA: 0x00049B3C File Offset: 0x00047D3C
		public static LocalizedString ImapTracingId(string emailAddress)
		{
			return new LocalizedString("ImapTracingId", "", false, false, MrsStrings.ResourceManager, new object[]
			{
				emailAddress
			});
		}

		// Token: 0x06002194 RID: 8596 RVA: 0x00049B6C File Offset: 0x00047D6C
		public static LocalizedString ReportIncrementalSyncContentChanges(LocalizedString physicalMailboxId, int messageChanges)
		{
			return new LocalizedString("ReportIncrementalSyncContentChanges", "", false, false, MrsStrings.ResourceManager, new object[]
			{
				physicalMailboxId,
				messageChanges
			});
		}

		// Token: 0x06002195 RID: 8597 RVA: 0x00049BAC File Offset: 0x00047DAC
		public static LocalizedString UnableToFindMbxServer(string server)
		{
			return new LocalizedString("UnableToFindMbxServer", "", false, false, MrsStrings.ResourceManager, new object[]
			{
				server
			});
		}

		// Token: 0x06002196 RID: 8598 RVA: 0x00049BDC File Offset: 0x00047DDC
		public static LocalizedString MailboxDatabaseNotFoundByGuid(Guid dbGuid)
		{
			return new LocalizedString("MailboxDatabaseNotFoundByGuid", "ExED7079", false, true, MrsStrings.ResourceManager, new object[]
			{
				dbGuid
			});
		}

		// Token: 0x06002197 RID: 8599 RVA: 0x00049C10 File Offset: 0x00047E10
		public static LocalizedString RequestTypeNotUnderstoodOnThisServer(string serverName, string serverVersion, int requestType)
		{
			return new LocalizedString("RequestTypeNotUnderstoodOnThisServer", "Ex53ADB0", false, true, MrsStrings.ResourceManager, new object[]
			{
				serverName,
				serverVersion,
				requestType
			});
		}

		// Token: 0x06002198 RID: 8600 RVA: 0x00049C4C File Offset: 0x00047E4C
		public static LocalizedString ReportCopyProgress(int itemsWritten, int itemsTotal, string dataSizeCopied, string totalSize)
		{
			return new LocalizedString("ReportCopyProgress", "Ex2CE5A9", false, true, MrsStrings.ResourceManager, new object[]
			{
				itemsWritten,
				itemsTotal,
				dataSizeCopied,
				totalSize
			});
		}

		// Token: 0x06002199 RID: 8601 RVA: 0x00049C94 File Offset: 0x00047E94
		public static LocalizedString ValidationSourceUserMismatch(string jobSrcUser, string indexSrcUser)
		{
			return new LocalizedString("ValidationSourceUserMismatch", "Ex6FE853", false, true, MrsStrings.ResourceManager, new object[]
			{
				jobSrcUser,
				indexSrcUser
			});
		}

		// Token: 0x0600219A RID: 8602 RVA: 0x00049CC8 File Offset: 0x00047EC8
		public static LocalizedString TargetRecipientIsNotAnMEU(string recipient)
		{
			return new LocalizedString("TargetRecipientIsNotAnMEU", "Ex5E4C38", false, true, MrsStrings.ResourceManager, new object[]
			{
				recipient
			});
		}

		// Token: 0x17000CA7 RID: 3239
		// (get) Token: 0x0600219B RID: 8603 RVA: 0x00049CF7 File Offset: 0x00047EF7
		public static LocalizedString CorruptRestrictionData
		{
			get
			{
				return new LocalizedString("CorruptRestrictionData", "", false, false, MrsStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000CA8 RID: 3240
		// (get) Token: 0x0600219C RID: 8604 RVA: 0x00049D15 File Offset: 0x00047F15
		public static LocalizedString ReportIncrementalMoveRestartDueToGlobalCounterRangeDepletion
		{
			get
			{
				return new LocalizedString("ReportIncrementalMoveRestartDueToGlobalCounterRangeDepletion", "", false, false, MrsStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600219D RID: 8605 RVA: 0x00049D34 File Offset: 0x00047F34
		public static LocalizedString MoveRequestMessageWarningSeparator(LocalizedString startMessage, LocalizedString additionalMessage)
		{
			return new LocalizedString("MoveRequestMessageWarningSeparator", "Ex296E0F", false, true, MrsStrings.ResourceManager, new object[]
			{
				startMessage,
				additionalMessage
			});
		}

		// Token: 0x0600219E RID: 8606 RVA: 0x00049D74 File Offset: 0x00047F74
		public static LocalizedString ReportRequestResumed(string userName)
		{
			return new LocalizedString("ReportRequestResumed", "Ex88D0AA", false, true, MrsStrings.ResourceManager, new object[]
			{
				userName
			});
		}

		// Token: 0x0600219F RID: 8607 RVA: 0x00049DA4 File Offset: 0x00047FA4
		public static LocalizedString ReportSyncStateCorrupt(Guid requestGuid, int length, string start, string end)
		{
			return new LocalizedString("ReportSyncStateCorrupt", "", false, false, MrsStrings.ResourceManager, new object[]
			{
				requestGuid,
				length,
				start,
				end
			});
		}

		// Token: 0x060021A0 RID: 8608 RVA: 0x00049DEC File Offset: 0x00047FEC
		public static LocalizedString ReportMoveIsStalled(LocalizedString failureMsg, int retryCount, int maxRetries)
		{
			return new LocalizedString("ReportMoveIsStalled", "Ex0409FE", false, true, MrsStrings.ResourceManager, new object[]
			{
				failureMsg,
				retryCount,
				maxRetries
			});
		}

		// Token: 0x060021A1 RID: 8609 RVA: 0x00049E34 File Offset: 0x00048034
		public static LocalizedString JobHasBeenRelinquishedDueToMailboxLockout(DateTime pickupTime)
		{
			return new LocalizedString("JobHasBeenRelinquishedDueToMailboxLockout", "", false, false, MrsStrings.ResourceManager, new object[]
			{
				pickupTime
			});
		}

		// Token: 0x060021A2 RID: 8610 RVA: 0x00049E68 File Offset: 0x00048068
		public static LocalizedString DestinationMailboxNotCleanedUp(Guid mbxGuid)
		{
			return new LocalizedString("DestinationMailboxNotCleanedUp", "Ex333D54", false, true, MrsStrings.ResourceManager, new object[]
			{
				mbxGuid
			});
		}

		// Token: 0x060021A3 RID: 8611 RVA: 0x00049E9C File Offset: 0x0004809C
		public static LocalizedString PublicFolderMove(string mbxGuid)
		{
			return new LocalizedString("PublicFolderMove", "", false, false, MrsStrings.ResourceManager, new object[]
			{
				mbxGuid
			});
		}

		// Token: 0x060021A4 RID: 8612 RVA: 0x00049ECC File Offset: 0x000480CC
		public static LocalizedString ReportSourceMailboxCleanupSucceeded(LocalizedString mailboxId)
		{
			return new LocalizedString("ReportSourceMailboxCleanupSucceeded", "ExC21FE4", false, true, MrsStrings.ResourceManager, new object[]
			{
				mailboxId
			});
		}

		// Token: 0x060021A5 RID: 8613 RVA: 0x00049F00 File Offset: 0x00048100
		public static LocalizedString ReportDestinationMailboxResetNotGuaranteed(string errorType)
		{
			return new LocalizedString("ReportDestinationMailboxResetNotGuaranteed", "", false, false, MrsStrings.ResourceManager, new object[]
			{
				errorType
			});
		}

		// Token: 0x17000CA9 RID: 3241
		// (get) Token: 0x060021A6 RID: 8614 RVA: 0x00049F2F File Offset: 0x0004812F
		public static LocalizedString ReadRpc
		{
			get
			{
				return new LocalizedString("ReadRpc", "", false, false, MrsStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000CAA RID: 3242
		// (get) Token: 0x060021A7 RID: 8615 RVA: 0x00049F4D File Offset: 0x0004814D
		public static LocalizedString WorkloadTypeLocal
		{
			get
			{
				return new LocalizedString("WorkloadTypeLocal", "", false, false, MrsStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060021A8 RID: 8616 RVA: 0x00049F6C File Offset: 0x0004816C
		public static LocalizedString CommunicationWithRemoteServiceFailed(string endpoint)
		{
			return new LocalizedString("CommunicationWithRemoteServiceFailed", "ExB22688", false, true, MrsStrings.ResourceManager, new object[]
			{
				endpoint
			});
		}

		// Token: 0x060021A9 RID: 8617 RVA: 0x00049F9C File Offset: 0x0004819C
		public static LocalizedString MailboxIsNotBeingMoved(string mailboxId)
		{
			return new LocalizedString("MailboxIsNotBeingMoved", "Ex0BED2C", false, true, MrsStrings.ResourceManager, new object[]
			{
				mailboxId
			});
		}

		// Token: 0x17000CAB RID: 3243
		// (get) Token: 0x060021AA RID: 8618 RVA: 0x00049FCB File Offset: 0x000481CB
		public static LocalizedString MoveRequestDirectionPush
		{
			get
			{
				return new LocalizedString("MoveRequestDirectionPush", "ExB24789", false, true, MrsStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060021AB RID: 8619 RVA: 0x00049FEC File Offset: 0x000481EC
		public static LocalizedString ErrorWlmCapacityExceeded2(string resourceName, string resourceType, string wlmResourceKey, int capacity)
		{
			return new LocalizedString("ErrorWlmCapacityExceeded2", "", false, false, MrsStrings.ResourceManager, new object[]
			{
				resourceName,
				resourceType,
				wlmResourceKey,
				capacity
			});
		}

		// Token: 0x060021AC RID: 8620 RVA: 0x0004A02C File Offset: 0x0004822C
		public static LocalizedString PublicFolderMigrationNotSupportedFromExchange2003OrEarlier(int major, int minor, int build, int revision)
		{
			return new LocalizedString("PublicFolderMigrationNotSupportedFromExchange2003OrEarlier", "", false, false, MrsStrings.ResourceManager, new object[]
			{
				major,
				minor,
				build,
				revision
			});
		}

		// Token: 0x17000CAC RID: 3244
		// (get) Token: 0x060021AD RID: 8621 RVA: 0x0004A07B File Offset: 0x0004827B
		public static LocalizedString SourceFolderHierarchyInconsistent
		{
			get
			{
				return new LocalizedString("SourceFolderHierarchyInconsistent", "", false, false, MrsStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060021AE RID: 8622 RVA: 0x0004A09C File Offset: 0x0004829C
		public static LocalizedString CouldNotFindDcHavingUmmUpdateError(Guid expectedDb, string recipient)
		{
			return new LocalizedString("CouldNotFindDcHavingUmmUpdateError", "", false, false, MrsStrings.ResourceManager, new object[]
			{
				expectedDb,
				recipient
			});
		}

		// Token: 0x060021AF RID: 8623 RVA: 0x0004A0D4 File Offset: 0x000482D4
		public static LocalizedString UnknownRestrictionType(string type)
		{
			return new LocalizedString("UnknownRestrictionType", "", false, false, MrsStrings.ResourceManager, new object[]
			{
				type
			});
		}

		// Token: 0x060021B0 RID: 8624 RVA: 0x0004A104 File Offset: 0x00048304
		public static LocalizedString ReportDestinationMailboxClearSyncStateSucceeded(LocalizedString mailboxId)
		{
			return new LocalizedString("ReportDestinationMailboxClearSyncStateSucceeded", "Ex71ED82", false, true, MrsStrings.ResourceManager, new object[]
			{
				mailboxId
			});
		}

		// Token: 0x060021B1 RID: 8625 RVA: 0x0004A138 File Offset: 0x00048338
		public static LocalizedString ReportInitializingMove(string serverName, string serverVersion)
		{
			return new LocalizedString("ReportInitializingMove", "Ex37BEC6", false, true, MrsStrings.ResourceManager, new object[]
			{
				serverName,
				serverVersion
			});
		}

		// Token: 0x060021B2 RID: 8626 RVA: 0x0004A16C File Offset: 0x0004836C
		public static LocalizedString BadItemFolderRule(string folderName)
		{
			return new LocalizedString("BadItemFolderRule", "", false, false, MrsStrings.ResourceManager, new object[]
			{
				folderName
			});
		}

		// Token: 0x060021B3 RID: 8627 RVA: 0x0004A19C File Offset: 0x0004839C
		public static LocalizedString FolderIsMissing(string folderPath)
		{
			return new LocalizedString("FolderIsMissing", "Ex996590", false, true, MrsStrings.ResourceManager, new object[]
			{
				folderPath
			});
		}

		// Token: 0x17000CAD RID: 3245
		// (get) Token: 0x060021B4 RID: 8628 RVA: 0x0004A1CB File Offset: 0x000483CB
		public static LocalizedString RequestPriorityHighest
		{
			get
			{
				return new LocalizedString("RequestPriorityHighest", "", false, false, MrsStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060021B5 RID: 8629 RVA: 0x0004A1EC File Offset: 0x000483EC
		public static LocalizedString ReportInitializingFolderHierarchy(LocalizedString physicalMailboxId, int totalFolders)
		{
			return new LocalizedString("ReportInitializingFolderHierarchy", "Ex56B873", false, true, MrsStrings.ResourceManager, new object[]
			{
				physicalMailboxId,
				totalFolders
			});
		}

		// Token: 0x060021B6 RID: 8630 RVA: 0x0004A22C File Offset: 0x0004842C
		public static LocalizedString ReportSyncStateSaveFailed2(string errorType)
		{
			return new LocalizedString("ReportSyncStateSaveFailed2", "", false, false, MrsStrings.ResourceManager, new object[]
			{
				errorType
			});
		}

		// Token: 0x17000CAE RID: 3246
		// (get) Token: 0x060021B7 RID: 8631 RVA: 0x0004A25B File Offset: 0x0004845B
		public static LocalizedString ErrorFinalizationIsBlocked
		{
			get
			{
				return new LocalizedString("ErrorFinalizationIsBlocked", "", false, false, MrsStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060021B8 RID: 8632 RVA: 0x0004A27C File Offset: 0x0004847C
		public static LocalizedString ReportSyncStateLoaded(Guid requestGuid, int syncStateLength, int icsSyncStateLength)
		{
			return new LocalizedString("ReportSyncStateLoaded", "", false, false, MrsStrings.ResourceManager, new object[]
			{
				requestGuid,
				syncStateLength,
				icsSyncStateLength
			});
		}

		// Token: 0x060021B9 RID: 8633 RVA: 0x0004A2C4 File Offset: 0x000484C4
		public static LocalizedString ReportUpdateMovedMailboxFailureAfterADSwitchover(LocalizedString error)
		{
			return new LocalizedString("ReportUpdateMovedMailboxFailureAfterADSwitchover", "", false, false, MrsStrings.ResourceManager, new object[]
			{
				error
			});
		}

		// Token: 0x060021BA RID: 8634 RVA: 0x0004A2F8 File Offset: 0x000484F8
		public static LocalizedString MailboxDoesNotExist(LocalizedString mbxId)
		{
			return new LocalizedString("MailboxDoesNotExist", "ExF60D53", false, true, MrsStrings.ResourceManager, new object[]
			{
				mbxId
			});
		}

		// Token: 0x060021BB RID: 8635 RVA: 0x0004A32C File Offset: 0x0004852C
		public static LocalizedString PositionOfMoveRequestInSystemMailboxQueue(string positionInQueue, string totalQueueLength)
		{
			return new LocalizedString("PositionOfMoveRequestInSystemMailboxQueue", "ExD035A9", false, true, MrsStrings.ResourceManager, new object[]
			{
				positionInQueue,
				totalQueueLength
			});
		}

		// Token: 0x060021BC RID: 8636 RVA: 0x0004A360 File Offset: 0x00048560
		public static LocalizedString ArchiveMailboxTracingId(string orgID, Guid mbxGuid)
		{
			return new LocalizedString("ArchiveMailboxTracingId", "", false, false, MrsStrings.ResourceManager, new object[]
			{
				orgID,
				mbxGuid
			});
		}

		// Token: 0x060021BD RID: 8637 RVA: 0x0004A398 File Offset: 0x00048598
		public static LocalizedString ReportRetryingMailboxCreation(LocalizedString physicalMailboxId, int delaySecs, int iAttempts, int iMaxRetries)
		{
			return new LocalizedString("ReportRetryingMailboxCreation", "ExC8C4B1", false, true, MrsStrings.ResourceManager, new object[]
			{
				physicalMailboxId,
				delaySecs,
				iAttempts,
				iMaxRetries
			});
		}

		// Token: 0x060021BE RID: 8638 RVA: 0x0004A3E8 File Offset: 0x000485E8
		public static LocalizedString ReportInitialSeedingCompleted(int messageCount, string totalSize)
		{
			return new LocalizedString("ReportInitialSeedingCompleted", "ExF0A482", false, true, MrsStrings.ResourceManager, new object[]
			{
				messageCount,
				totalSize
			});
		}

		// Token: 0x17000CAF RID: 3247
		// (get) Token: 0x060021BF RID: 8639 RVA: 0x0004A420 File Offset: 0x00048620
		public static LocalizedString MoveRequestTypeCrossOrg
		{
			get
			{
				return new LocalizedString("MoveRequestTypeCrossOrg", "Ex1C329E", false, true, MrsStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000CB0 RID: 3248
		// (get) Token: 0x060021C0 RID: 8640 RVA: 0x0004A43E File Offset: 0x0004863E
		public static LocalizedString MrsProxyServiceIsDisabled
		{
			get
			{
				return new LocalizedString("MrsProxyServiceIsDisabled", "Ex85E367", false, true, MrsStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060021C1 RID: 8641 RVA: 0x0004A45C File Offset: 0x0004865C
		public static LocalizedString FastTransferBuffer(string property, int value)
		{
			return new LocalizedString("FastTransferBuffer", "", false, false, MrsStrings.ResourceManager, new object[]
			{
				property,
				value
			});
		}

		// Token: 0x17000CB1 RID: 3249
		// (get) Token: 0x060021C2 RID: 8642 RVA: 0x0004A494 File Offset: 0x00048694
		public static LocalizedString ReportMoveCanceled
		{
			get
			{
				return new LocalizedString("ReportMoveCanceled", "Ex5AA91A", false, true, MrsStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060021C3 RID: 8643 RVA: 0x0004A4B4 File Offset: 0x000486B4
		public static LocalizedString ErrorWlmCapacityExceeded(string resourceName, string resourceKey, double reportedLoadRatio, string reportedLoadState, string metric)
		{
			return new LocalizedString("ErrorWlmCapacityExceeded", "", false, false, MrsStrings.ResourceManager, new object[]
			{
				resourceName,
				resourceKey,
				reportedLoadRatio,
				reportedLoadState,
				metric
			});
		}

		// Token: 0x060021C4 RID: 8644 RVA: 0x0004A4FC File Offset: 0x000486FC
		public static LocalizedString RestoringConnectedMailboxError(Guid mailboxGuid)
		{
			return new LocalizedString("RestoringConnectedMailboxError", "", false, false, MrsStrings.ResourceManager, new object[]
			{
				mailboxGuid
			});
		}

		// Token: 0x17000CB2 RID: 3250
		// (get) Token: 0x060021C5 RID: 8645 RVA: 0x0004A530 File Offset: 0x00048730
		public static LocalizedString ErrorCannotPreventCompletionForOfflineMove
		{
			get
			{
				return new LocalizedString("ErrorCannotPreventCompletionForOfflineMove", "", false, false, MrsStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060021C6 RID: 8646 RVA: 0x0004A550 File Offset: 0x00048750
		public static LocalizedString ReportProgress(string syncStage, int percentComplete)
		{
			return new LocalizedString("ReportProgress", "Ex587B63", false, true, MrsStrings.ResourceManager, new object[]
			{
				syncStage,
				percentComplete
			});
		}

		// Token: 0x060021C7 RID: 8647 RVA: 0x0004A588 File Offset: 0x00048788
		public static LocalizedString EntryIDsDataContext(string entryIdsStr)
		{
			return new LocalizedString("EntryIDsDataContext", "ExADF226", false, true, MrsStrings.ResourceManager, new object[]
			{
				entryIdsStr
			});
		}

		// Token: 0x060021C8 RID: 8648 RVA: 0x0004A5B8 File Offset: 0x000487B8
		public static LocalizedString EasTracingId(string emailAddress)
		{
			return new LocalizedString("EasTracingId", "", false, false, MrsStrings.ResourceManager, new object[]
			{
				emailAddress
			});
		}

		// Token: 0x060021C9 RID: 8649 RVA: 0x0004A5E8 File Offset: 0x000487E8
		public static LocalizedString ReportTransientException(string errorType, int retryCount, int maxRetries)
		{
			return new LocalizedString("ReportTransientException", "", false, false, MrsStrings.ResourceManager, new object[]
			{
				errorType,
				retryCount,
				maxRetries
			});
		}

		// Token: 0x060021CA RID: 8650 RVA: 0x0004A62C File Offset: 0x0004882C
		public static LocalizedString RequestGuidNotUnique(string guid, string type)
		{
			return new LocalizedString("RequestGuidNotUnique", "ExE77A61", false, true, MrsStrings.ResourceManager, new object[]
			{
				guid,
				type
			});
		}

		// Token: 0x060021CB RID: 8651 RVA: 0x0004A660 File Offset: 0x00048860
		public static LocalizedString MoveIsStalled(string mailboxId, string mdbId, LocalizedString failureReason, string agentName)
		{
			return new LocalizedString("MoveIsStalled", "ExA334F9", false, true, MrsStrings.ResourceManager, new object[]
			{
				mailboxId,
				mdbId,
				failureReason,
				agentName
			});
		}

		// Token: 0x060021CC RID: 8652 RVA: 0x0004A6A0 File Offset: 0x000488A0
		public static LocalizedString ContainerMailboxTracingId(Guid containerGuid, Guid mbxGuid)
		{
			return new LocalizedString("ContainerMailboxTracingId", "", false, false, MrsStrings.ResourceManager, new object[]
			{
				containerGuid,
				mbxGuid
			});
		}

		// Token: 0x060021CD RID: 8653 RVA: 0x0004A6E0 File Offset: 0x000488E0
		public static LocalizedString RequestIsStalled(LocalizedString agent, string throttledResource)
		{
			return new LocalizedString("RequestIsStalled", "", false, false, MrsStrings.ResourceManager, new object[]
			{
				agent,
				throttledResource
			});
		}

		// Token: 0x060021CE RID: 8654 RVA: 0x0004A718 File Offset: 0x00048918
		public static LocalizedString JobHasBeenRelinquishedDueToTransientErrorDuringOfflineMove(DateTime pickupTime)
		{
			return new LocalizedString("JobHasBeenRelinquishedDueToTransientErrorDuringOfflineMove", "", false, false, MrsStrings.ResourceManager, new object[]
			{
				pickupTime
			});
		}

		// Token: 0x060021CF RID: 8655 RVA: 0x0004A74C File Offset: 0x0004894C
		public static LocalizedString ReportFailedToUpdateUserSD(string errorType, LocalizedString errorMsg)
		{
			return new LocalizedString("ReportFailedToUpdateUserSD", "Ex29FD5A", false, true, MrsStrings.ResourceManager, new object[]
			{
				errorType,
				errorMsg
			});
		}

		// Token: 0x060021D0 RID: 8656 RVA: 0x0004A784 File Offset: 0x00048984
		public static LocalizedString PrimaryMailboxId(Guid mbxGuid)
		{
			return new LocalizedString("PrimaryMailboxId", "Ex539CB5", false, true, MrsStrings.ResourceManager, new object[]
			{
				mbxGuid
			});
		}

		// Token: 0x060021D1 RID: 8657 RVA: 0x0004A7B8 File Offset: 0x000489B8
		public static LocalizedString ReportIncrementalSyncContentChangesPaged(LocalizedString physicalMailboxId, int messageChanges, int batch)
		{
			return new LocalizedString("ReportIncrementalSyncContentChangesPaged", "", false, false, MrsStrings.ResourceManager, new object[]
			{
				physicalMailboxId,
				messageChanges,
				batch
			});
		}

		// Token: 0x060021D2 RID: 8658 RVA: 0x0004A800 File Offset: 0x00048A00
		public static LocalizedString InvalidEndpointAddressError(string serviceURI)
		{
			return new LocalizedString("InvalidEndpointAddressError", "ExFEB6F3", false, true, MrsStrings.ResourceManager, new object[]
			{
				serviceURI
			});
		}

		// Token: 0x060021D3 RID: 8659 RVA: 0x0004A830 File Offset: 0x00048A30
		public static LocalizedString DatacenterMissingHosts(string datacenterName)
		{
			return new LocalizedString("DatacenterMissingHosts", "", false, false, MrsStrings.ResourceManager, new object[]
			{
				datacenterName
			});
		}

		// Token: 0x060021D4 RID: 8660 RVA: 0x0004A860 File Offset: 0x00048A60
		public static LocalizedString ReportSyncStateCleared(Guid requestGuid, string reason)
		{
			return new LocalizedString("ReportSyncStateCleared", "", false, false, MrsStrings.ResourceManager, new object[]
			{
				requestGuid,
				reason
			});
		}

		// Token: 0x060021D5 RID: 8661 RVA: 0x0004A898 File Offset: 0x00048A98
		public static LocalizedString ReportJobIsStalled(LocalizedString failureMsg, int retryCount, int maxRetries)
		{
			return new LocalizedString("ReportJobIsStalled", "ExFEF504", false, true, MrsStrings.ResourceManager, new object[]
			{
				failureMsg,
				retryCount,
				maxRetries
			});
		}

		// Token: 0x060021D6 RID: 8662 RVA: 0x0004A8E0 File Offset: 0x00048AE0
		public static LocalizedString ReportSourceMailboxConnection(LocalizedString mailboxId, LocalizedString serverInformation, string databaseID)
		{
			return new LocalizedString("ReportSourceMailboxConnection", "", false, false, MrsStrings.ResourceManager, new object[]
			{
				mailboxId,
				serverInformation,
				databaseID
			});
		}

		// Token: 0x060021D7 RID: 8663 RVA: 0x0004A924 File Offset: 0x00048B24
		public static LocalizedString ReportDestinationMailboxSeedMBICacheSucceeded2(LocalizedString mailboxId)
		{
			return new LocalizedString("ReportDestinationMailboxSeedMBICacheSucceeded2", "Ex8BD61A", false, true, MrsStrings.ResourceManager, new object[]
			{
				mailboxId
			});
		}

		// Token: 0x060021D8 RID: 8664 RVA: 0x0004A958 File Offset: 0x00048B58
		public static LocalizedString RecipientNotFound(Guid mailboxGuid)
		{
			return new LocalizedString("RecipientNotFound", "Ex0578D0", false, true, MrsStrings.ResourceManager, new object[]
			{
				mailboxGuid
			});
		}

		// Token: 0x060021D9 RID: 8665 RVA: 0x0004A98C File Offset: 0x00048B8C
		public static LocalizedString ReportCleanUpFoldersDestination(string stage)
		{
			return new LocalizedString("ReportCleanUpFoldersDestination", "", false, false, MrsStrings.ResourceManager, new object[]
			{
				stage
			});
		}

		// Token: 0x060021DA RID: 8666 RVA: 0x0004A9BC File Offset: 0x00048BBC
		public static LocalizedString UnableToOpenPST2(string filePath, string exceptionMessage)
		{
			return new LocalizedString("UnableToOpenPST2", "Ex951A94", false, true, MrsStrings.ResourceManager, new object[]
			{
				filePath,
				exceptionMessage
			});
		}

		// Token: 0x060021DB RID: 8667 RVA: 0x0004A9F0 File Offset: 0x00048BF0
		public static LocalizedString ValidationFlagsMismatch(string adFlags, string mrFlags)
		{
			return new LocalizedString("ValidationFlagsMismatch", "ExDBA7D4", false, true, MrsStrings.ResourceManager, new object[]
			{
				adFlags,
				mrFlags
			});
		}

		// Token: 0x060021DC RID: 8668 RVA: 0x0004AA24 File Offset: 0x00048C24
		public static LocalizedString ReportRequestAllowedMismatch(string userName)
		{
			return new LocalizedString("ReportRequestAllowedMismatch", "Ex5EEEA7", false, true, MrsStrings.ResourceManager, new object[]
			{
				userName
			});
		}

		// Token: 0x17000CB3 RID: 3251
		// (get) Token: 0x060021DD RID: 8669 RVA: 0x0004AA53 File Offset: 0x00048C53
		public static LocalizedString RequestHasBeenPostponedDueToBadHealthOfBackendServers2
		{
			get
			{
				return new LocalizedString("RequestHasBeenPostponedDueToBadHealthOfBackendServers2", "", false, false, MrsStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000CB4 RID: 3252
		// (get) Token: 0x060021DE RID: 8670 RVA: 0x0004AA71 File Offset: 0x00048C71
		public static LocalizedString ValidationNoCorrespondingIndexEntries
		{
			get
			{
				return new LocalizedString("ValidationNoCorrespondingIndexEntries", "ExF6BA89", false, true, MrsStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000CB5 RID: 3253
		// (get) Token: 0x060021DF RID: 8671 RVA: 0x0004AA8F File Offset: 0x00048C8F
		public static LocalizedString InvalidSyncStateData
		{
			get
			{
				return new LocalizedString("InvalidSyncStateData", "ExD8FD56", false, true, MrsStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060021E0 RID: 8672 RVA: 0x0004AAB0 File Offset: 0x00048CB0
		public static LocalizedString ReportRequestSuspended(string userName)
		{
			return new LocalizedString("ReportRequestSuspended", "Ex4DFB2E", false, true, MrsStrings.ResourceManager, new object[]
			{
				userName
			});
		}

		// Token: 0x060021E1 RID: 8673 RVA: 0x0004AAE0 File Offset: 0x00048CE0
		public static LocalizedString PublicFolderMigrationNotSupportedFromCurrentExchange2010Version(int major, int minor, int build, int revision)
		{
			return new LocalizedString("PublicFolderMigrationNotSupportedFromCurrentExchange2010Version", "", false, false, MrsStrings.ResourceManager, new object[]
			{
				major,
				minor,
				build,
				revision
			});
		}

		// Token: 0x17000CB6 RID: 3254
		// (get) Token: 0x060021E2 RID: 8674 RVA: 0x0004AB2F File Offset: 0x00048D2F
		public static LocalizedString ReportMoveRestartedDueToSourceCorruption
		{
			get
			{
				return new LocalizedString("ReportMoveRestartedDueToSourceCorruption", "", false, false, MrsStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060021E3 RID: 8675 RVA: 0x0004AB50 File Offset: 0x00048D50
		public static LocalizedString MoveRequestDataIsCorrupt(LocalizedString validationMessage)
		{
			return new LocalizedString("MoveRequestDataIsCorrupt", "ExA3AFCA", false, true, MrsStrings.ResourceManager, new object[]
			{
				validationMessage
			});
		}

		// Token: 0x060021E4 RID: 8676 RVA: 0x0004AB84 File Offset: 0x00048D84
		public static LocalizedString SourceMailboxUpdateFailed(LocalizedString errorMsg)
		{
			return new LocalizedString("SourceMailboxUpdateFailed", "", false, false, MrsStrings.ResourceManager, new object[]
			{
				errorMsg
			});
		}

		// Token: 0x060021E5 RID: 8677 RVA: 0x0004ABB8 File Offset: 0x00048DB8
		public static LocalizedString ReportUnableToPreserveMailboxSignature(LocalizedString mailboxId)
		{
			return new LocalizedString("ReportUnableToPreserveMailboxSignature", "ExB2DCA8", false, true, MrsStrings.ResourceManager, new object[]
			{
				mailboxId
			});
		}

		// Token: 0x060021E6 RID: 8678 RVA: 0x0004ABEC File Offset: 0x00048DEC
		public static LocalizedString UsageText(string processName)
		{
			return new LocalizedString("UsageText", "ExF6869B", false, true, MrsStrings.ResourceManager, new object[]
			{
				processName
			});
		}

		// Token: 0x060021E7 RID: 8679 RVA: 0x0004AC1C File Offset: 0x00048E1C
		public static LocalizedString ReportLargeAmountOfDataLossAccepted(string badItemLimit, string requestorName)
		{
			return new LocalizedString("ReportLargeAmountOfDataLossAccepted", "ExC02752", false, true, MrsStrings.ResourceManager, new object[]
			{
				badItemLimit,
				requestorName
			});
		}

		// Token: 0x17000CB7 RID: 3255
		// (get) Token: 0x060021E8 RID: 8680 RVA: 0x0004AC4F File Offset: 0x00048E4F
		public static LocalizedString JobHasBeenAutoSuspended
		{
			get
			{
				return new LocalizedString("JobHasBeenAutoSuspended", "ExB8F79F", false, true, MrsStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060021E9 RID: 8681 RVA: 0x0004AC70 File Offset: 0x00048E70
		public static LocalizedString EndpointNotFoundError(string serviceURI, LocalizedString exceptionMessage)
		{
			return new LocalizedString("EndpointNotFoundError", "ExB791EB", false, true, MrsStrings.ResourceManager, new object[]
			{
				serviceURI,
				exceptionMessage
			});
		}

		// Token: 0x060021EA RID: 8682 RVA: 0x0004ACA8 File Offset: 0x00048EA8
		public static LocalizedString MailboxNotSynced(Guid mbxGuid)
		{
			return new LocalizedString("MailboxNotSynced", "Ex45D1FE", false, true, MrsStrings.ResourceManager, new object[]
			{
				mbxGuid
			});
		}

		// Token: 0x060021EB RID: 8683 RVA: 0x0004ACDC File Offset: 0x00048EDC
		public static LocalizedString ReportSyncStateWrongRequestGuid(Guid requestGuid, Guid returnedGuid)
		{
			return new LocalizedString("ReportSyncStateWrongRequestGuid", "", false, false, MrsStrings.ResourceManager, new object[]
			{
				requestGuid,
				returnedGuid
			});
		}

		// Token: 0x060021EC RID: 8684 RVA: 0x0004AD1C File Offset: 0x00048F1C
		public static LocalizedString ReportDestinationMailboxConnection(LocalizedString mailboxId, LocalizedString serverInformation, string databaseID)
		{
			return new LocalizedString("ReportDestinationMailboxConnection", "", false, false, MrsStrings.ResourceManager, new object[]
			{
				mailboxId,
				serverInformation,
				databaseID
			});
		}

		// Token: 0x060021ED RID: 8685 RVA: 0x0004AD60 File Offset: 0x00048F60
		public static LocalizedString ReportIncrementalSyncContentChangesSynced2(LocalizedString physicalMailboxId, int newMessages, int changedMessages, int deletedMessages, int readMessages, int unreadMessages, int skipped, int applied)
		{
			return new LocalizedString("ReportIncrementalSyncContentChangesSynced2", "", false, false, MrsStrings.ResourceManager, new object[]
			{
				physicalMailboxId,
				newMessages,
				changedMessages,
				deletedMessages,
				readMessages,
				unreadMessages,
				skipped,
				applied
			});
		}

		// Token: 0x060021EE RID: 8686 RVA: 0x0004ADD8 File Offset: 0x00048FD8
		public static LocalizedString EasFolderSyncFailedPermanently(string folderSyncStatus, string httpStatus)
		{
			return new LocalizedString("EasFolderSyncFailedPermanently", "", false, false, MrsStrings.ResourceManager, new object[]
			{
				folderSyncStatus,
				httpStatus
			});
		}

		// Token: 0x060021EF RID: 8687 RVA: 0x0004AE0C File Offset: 0x0004900C
		public static LocalizedString ReportFailedToDisconnectFromDestination2(string errorType)
		{
			return new LocalizedString("ReportFailedToDisconnectFromDestination2", "", false, false, MrsStrings.ResourceManager, new object[]
			{
				errorType
			});
		}

		// Token: 0x060021F0 RID: 8688 RVA: 0x0004AE3C File Offset: 0x0004903C
		public static LocalizedString NamedPropsDataContext(string namedPropsStr)
		{
			return new LocalizedString("NamedPropsDataContext", "ExECA2F9", false, true, MrsStrings.ResourceManager, new object[]
			{
				namedPropsStr
			});
		}

		// Token: 0x17000CB8 RID: 3256
		// (get) Token: 0x060021F1 RID: 8689 RVA: 0x0004AE6B File Offset: 0x0004906B
		public static LocalizedString InputDataIsInvalid
		{
			get
			{
				return new LocalizedString("InputDataIsInvalid", "ExA48100", false, true, MrsStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060021F2 RID: 8690 RVA: 0x0004AE8C File Offset: 0x0004908C
		public static LocalizedString ReportFailingInvalidMoveRequest(LocalizedString validationMessage)
		{
			return new LocalizedString("ReportFailingInvalidMoveRequest", "Ex505BD9", false, true, MrsStrings.ResourceManager, new object[]
			{
				validationMessage
			});
		}

		// Token: 0x060021F3 RID: 8691 RVA: 0x0004AEC0 File Offset: 0x000490C0
		public static LocalizedString BadItemCorruptMailboxSetting(string typeStr)
		{
			return new LocalizedString("BadItemCorruptMailboxSetting", "", false, false, MrsStrings.ResourceManager, new object[]
			{
				typeStr
			});
		}

		// Token: 0x17000CB9 RID: 3257
		// (get) Token: 0x060021F4 RID: 8692 RVA: 0x0004AEEF File Offset: 0x000490EF
		public static LocalizedString ReportJobExitedStalledState
		{
			get
			{
				return new LocalizedString("ReportJobExitedStalledState", "ExA919EC", false, true, MrsStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000CBA RID: 3258
		// (get) Token: 0x060021F5 RID: 8693 RVA: 0x0004AF0D File Offset: 0x0004910D
		public static LocalizedString ActionNotSupported
		{
			get
			{
				return new LocalizedString("ActionNotSupported", "", false, false, MrsStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000CBB RID: 3259
		// (get) Token: 0x060021F6 RID: 8694 RVA: 0x0004AF2B File Offset: 0x0004912B
		public static LocalizedString ReportTargetAuxFolderContentMailboxGuidUpdated
		{
			get
			{
				return new LocalizedString("ReportTargetAuxFolderContentMailboxGuidUpdated", "", false, false, MrsStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000CBC RID: 3260
		// (get) Token: 0x060021F7 RID: 8695 RVA: 0x0004AF49 File Offset: 0x00049149
		public static LocalizedString ReportStoreMailboxHasFinalized
		{
			get
			{
				return new LocalizedString("ReportStoreMailboxHasFinalized", "", false, false, MrsStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060021F8 RID: 8696 RVA: 0x0004AF68 File Offset: 0x00049168
		public static LocalizedString ReportMailboxAfterFinalization(string userDataXML)
		{
			return new LocalizedString("ReportMailboxAfterFinalization", "Ex38D684", false, true, MrsStrings.ResourceManager, new object[]
			{
				userDataXML
			});
		}

		// Token: 0x060021F9 RID: 8697 RVA: 0x0004AF98 File Offset: 0x00049198
		public static LocalizedString MessageEnumerationFailed(int exists, int messagesEnumeratedCount)
		{
			return new LocalizedString("MessageEnumerationFailed", "", false, false, MrsStrings.ResourceManager, new object[]
			{
				exists,
				messagesEnumeratedCount
			});
		}

		// Token: 0x060021FA RID: 8698 RVA: 0x0004AFD8 File Offset: 0x000491D8
		public static LocalizedString ErrorTargetDeliveryDomainMismatch(string targetDeliveryDomain)
		{
			return new LocalizedString("ErrorTargetDeliveryDomainMismatch", "", false, false, MrsStrings.ResourceManager, new object[]
			{
				targetDeliveryDomain
			});
		}

		// Token: 0x060021FB RID: 8699 RVA: 0x0004B008 File Offset: 0x00049208
		public static LocalizedString SortOrderDataContext(string sortOrderStr)
		{
			return new LocalizedString("SortOrderDataContext", "ExCA4609", false, true, MrsStrings.ResourceManager, new object[]
			{
				sortOrderStr
			});
		}

		// Token: 0x060021FC RID: 8700 RVA: 0x0004B038 File Offset: 0x00049238
		public static LocalizedString CannotCreateEntryId(string input)
		{
			return new LocalizedString("CannotCreateEntryId", "", false, false, MrsStrings.ResourceManager, new object[]
			{
				input
			});
		}

		// Token: 0x17000CBD RID: 3261
		// (get) Token: 0x060021FD RID: 8701 RVA: 0x0004B067 File Offset: 0x00049267
		public static LocalizedString ErrorReservationExpired
		{
			get
			{
				return new LocalizedString("ErrorReservationExpired", "", false, false, MrsStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060021FE RID: 8702 RVA: 0x0004B088 File Offset: 0x00049288
		public static LocalizedString PropertyMismatch(uint propTag, string value1, string value2)
		{
			return new LocalizedString("PropertyMismatch", "", false, false, MrsStrings.ResourceManager, new object[]
			{
				propTag,
				value1,
				value2
			});
		}

		// Token: 0x060021FF RID: 8703 RVA: 0x0004B0C4 File Offset: 0x000492C4
		public static LocalizedString ReportWaitingIsInteg(Guid mailboxGuid, Guid isIntegRequestGuid, string percentages)
		{
			return new LocalizedString("ReportWaitingIsInteg", "", false, false, MrsStrings.ResourceManager, new object[]
			{
				mailboxGuid,
				isIntegRequestGuid,
				percentages
			});
		}

		// Token: 0x17000CBE RID: 3262
		// (get) Token: 0x06002200 RID: 8704 RVA: 0x0004B105 File Offset: 0x00049305
		public static LocalizedString ErrorImplicitSplit
		{
			get
			{
				return new LocalizedString("ErrorImplicitSplit", "", false, false, MrsStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06002201 RID: 8705 RVA: 0x0004B124 File Offset: 0x00049324
		public static LocalizedString SoftDeletedItemsCountAndSize(int items, string size)
		{
			return new LocalizedString("SoftDeletedItemsCountAndSize", "Ex4CAE5B", false, true, MrsStrings.ResourceManager, new object[]
			{
				items,
				size
			});
		}

		// Token: 0x06002202 RID: 8706 RVA: 0x0004B15C File Offset: 0x0004935C
		public static LocalizedString EasSyncCouldNotFindFolder(string folderId)
		{
			return new LocalizedString("EasSyncCouldNotFindFolder", "", false, false, MrsStrings.ResourceManager, new object[]
			{
				folderId
			});
		}

		// Token: 0x17000CBF RID: 3263
		// (get) Token: 0x06002203 RID: 8707 RVA: 0x0004B18B File Offset: 0x0004938B
		public static LocalizedString ReportRelinquishingJob
		{
			get
			{
				return new LocalizedString("ReportRelinquishingJob", "Ex84F9BD", false, true, MrsStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06002204 RID: 8708 RVA: 0x0004B1AC File Offset: 0x000493AC
		public static LocalizedString EasFolderCreateFailed(string errorMessage)
		{
			return new LocalizedString("EasFolderCreateFailed", "", false, false, MrsStrings.ResourceManager, new object[]
			{
				errorMessage
			});
		}

		// Token: 0x06002205 RID: 8709 RVA: 0x0004B1DC File Offset: 0x000493DC
		public static LocalizedString MailPublicFolderWithLegacyExchangeDnNotFound(string legacyExchangeDN)
		{
			return new LocalizedString("MailPublicFolderWithLegacyExchangeDnNotFound", "", false, false, MrsStrings.ResourceManager, new object[]
			{
				legacyExchangeDN
			});
		}

		// Token: 0x17000CC0 RID: 3264
		// (get) Token: 0x06002206 RID: 8710 RVA: 0x0004B20B File Offset: 0x0004940B
		public static LocalizedString CouldNotConnectToSourceMailbox
		{
			get
			{
				return new LocalizedString("CouldNotConnectToSourceMailbox", "Ex05C4C9", false, true, MrsStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06002207 RID: 8711 RVA: 0x0004B22C File Offset: 0x0004942C
		public static LocalizedString ReportFolderHierarchyInitialized(LocalizedString physicalMailboxId, int foldersCreated)
		{
			return new LocalizedString("ReportFolderHierarchyInitialized", "Ex05DD9F", false, true, MrsStrings.ResourceManager, new object[]
			{
				physicalMailboxId,
				foldersCreated
			});
		}

		// Token: 0x06002208 RID: 8712 RVA: 0x0004B26C File Offset: 0x0004946C
		public static LocalizedString ReportRequestContinued(string syncStage)
		{
			return new LocalizedString("ReportRequestContinued", "Ex0E7078", false, true, MrsStrings.ResourceManager, new object[]
			{
				syncStage
			});
		}

		// Token: 0x17000CC1 RID: 3265
		// (get) Token: 0x06002209 RID: 8713 RVA: 0x0004B29B File Offset: 0x0004949B
		public static LocalizedString NoFoldersIncluded
		{
			get
			{
				return new LocalizedString("NoFoldersIncluded", "Ex16C9A0", false, true, MrsStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600220A RID: 8714 RVA: 0x0004B2BC File Offset: 0x000494BC
		public static LocalizedString ReportFolderMergeStats(int itemsToCopyCount, string itemsToCopySizeStr, int skippedItemCount, string skippedItemSizeStr)
		{
			return new LocalizedString("ReportFolderMergeStats", "ExD3C1F2", false, true, MrsStrings.ResourceManager, new object[]
			{
				itemsToCopyCount,
				itemsToCopySizeStr,
				skippedItemCount,
				skippedItemSizeStr
			});
		}

		// Token: 0x17000CC2 RID: 3266
		// (get) Token: 0x0600220B RID: 8715 RVA: 0x0004B301 File Offset: 0x00049501
		public static LocalizedString ReportSuspendingJob
		{
			get
			{
				return new LocalizedString("ReportSuspendingJob", "Ex1BE920", false, true, MrsStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000CC3 RID: 3267
		// (get) Token: 0x0600220C RID: 8716 RVA: 0x0004B31F File Offset: 0x0004951F
		public static LocalizedString WriteRpc
		{
			get
			{
				return new LocalizedString("WriteRpc", "", false, false, MrsStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600220D RID: 8717 RVA: 0x0004B340 File Offset: 0x00049540
		public static LocalizedString ReportCorruptItemsSkipped(int count, string totalSize)
		{
			return new LocalizedString("ReportCorruptItemsSkipped", "", false, false, MrsStrings.ResourceManager, new object[]
			{
				count,
				totalSize
			});
		}

		// Token: 0x0600220E RID: 8718 RVA: 0x0004B378 File Offset: 0x00049578
		public static LocalizedString ValidationUserLacksMailbox(string jobUser)
		{
			return new LocalizedString("ValidationUserLacksMailbox", "", false, false, MrsStrings.ResourceManager, new object[]
			{
				jobUser
			});
		}

		// Token: 0x17000CC4 RID: 3268
		// (get) Token: 0x0600220F RID: 8719 RVA: 0x0004B3A7 File Offset: 0x000495A7
		public static LocalizedString NotConnected
		{
			get
			{
				return new LocalizedString("NotConnected", "ExA6F98B", false, true, MrsStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06002210 RID: 8720 RVA: 0x0004B3C8 File Offset: 0x000495C8
		public static LocalizedString EasSyncFailed(string errorMessage)
		{
			return new LocalizedString("EasSyncFailed", "", false, false, MrsStrings.ResourceManager, new object[]
			{
				errorMessage
			});
		}

		// Token: 0x06002211 RID: 8721 RVA: 0x0004B3F8 File Offset: 0x000495F8
		public static LocalizedString ReportSoftDeletedItemsWillNotBeMigrated(LocalizedString mailboxId, int itemCount, string itemsSize)
		{
			return new LocalizedString("ReportSoftDeletedItemsWillNotBeMigrated", "ExAAC01F", false, true, MrsStrings.ResourceManager, new object[]
			{
				mailboxId,
				itemCount,
				itemsSize
			});
		}

		// Token: 0x06002212 RID: 8722 RVA: 0x0004B43C File Offset: 0x0004963C
		public static LocalizedString FolderDataContextId(string entryId)
		{
			return new LocalizedString("FolderDataContextId", "Ex280BF7", false, true, MrsStrings.ResourceManager, new object[]
			{
				entryId
			});
		}

		// Token: 0x06002213 RID: 8723 RVA: 0x0004B46C File Offset: 0x0004966C
		public static LocalizedString ReportIcsSyncStateNull(Guid requestGuid)
		{
			return new LocalizedString("ReportIcsSyncStateNull", "", false, false, MrsStrings.ResourceManager, new object[]
			{
				requestGuid
			});
		}

		// Token: 0x06002214 RID: 8724 RVA: 0x0004B4A0 File Offset: 0x000496A0
		public static LocalizedString ReportFatalExceptionOccurred(string errorType, LocalizedString errorMsg, string trace, LocalizedString context)
		{
			return new LocalizedString("ReportFatalExceptionOccurred", "Ex0B0B72", false, true, MrsStrings.ResourceManager, new object[]
			{
				errorType,
				errorMsg,
				trace,
				context
			});
		}

		// Token: 0x17000CC5 RID: 3269
		// (get) Token: 0x06002215 RID: 8725 RVA: 0x0004B4E5 File Offset: 0x000496E5
		public static LocalizedString MdbReplication
		{
			get
			{
				return new LocalizedString("MdbReplication", "", false, false, MrsStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06002216 RID: 8726 RVA: 0x0004B504 File Offset: 0x00049704
		public static LocalizedString ValidationMailboxIdentitiesDontMatch(string adUserId, string mrUserId)
		{
			return new LocalizedString("ValidationMailboxIdentitiesDontMatch", "Ex03029B", false, true, MrsStrings.ResourceManager, new object[]
			{
				adUserId,
				mrUserId
			});
		}

		// Token: 0x06002217 RID: 8727 RVA: 0x0004B538 File Offset: 0x00049738
		public static LocalizedString ReportMailboxArchiveInfoAfterMove(string mailboxInfoString)
		{
			return new LocalizedString("ReportMailboxArchiveInfoAfterMove", "ExFCC1B7", false, true, MrsStrings.ResourceManager, new object[]
			{
				mailboxInfoString
			});
		}

		// Token: 0x06002218 RID: 8728 RVA: 0x0004B568 File Offset: 0x00049768
		public static LocalizedString FolderHierarchyContainsMultipleRoots(string root1str, string root2str)
		{
			return new LocalizedString("FolderHierarchyContainsMultipleRoots", "", false, false, MrsStrings.ResourceManager, new object[]
			{
				root1str,
				root2str
			});
		}

		// Token: 0x06002219 RID: 8729 RVA: 0x0004B59C File Offset: 0x0004979C
		public static LocalizedString ValidationStorageMDBMismatch(string indexDatabase, string jobDatabase)
		{
			return new LocalizedString("ValidationStorageMDBMismatch", "Ex8FD9D3", false, true, MrsStrings.ResourceManager, new object[]
			{
				indexDatabase,
				jobDatabase
			});
		}

		// Token: 0x0600221A RID: 8730 RVA: 0x0004B5D0 File Offset: 0x000497D0
		public static LocalizedString ReportFatalException(string errorType)
		{
			return new LocalizedString("ReportFatalException", "", false, false, MrsStrings.ResourceManager, new object[]
			{
				errorType
			});
		}

		// Token: 0x17000CC6 RID: 3270
		// (get) Token: 0x0600221B RID: 8731 RVA: 0x0004B5FF File Offset: 0x000497FF
		public static LocalizedString ReportRulesWillNotBeCopied
		{
			get
			{
				return new LocalizedString("ReportRulesWillNotBeCopied", "ExFB7131", false, true, MrsStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600221C RID: 8732 RVA: 0x0004B620 File Offset: 0x00049820
		public static LocalizedString ReportMailboxInfoAfterMoveLoc(LocalizedString mailboxInfoString)
		{
			return new LocalizedString("ReportMailboxInfoAfterMoveLoc", "ExCEAE7D", false, true, MrsStrings.ResourceManager, new object[]
			{
				mailboxInfoString
			});
		}

		// Token: 0x17000CC7 RID: 3271
		// (get) Token: 0x0600221D RID: 8733 RVA: 0x0004B654 File Offset: 0x00049854
		public static LocalizedString ReportSkippingUpdateSourceMailbox
		{
			get
			{
				return new LocalizedString("ReportSkippingUpdateSourceMailbox", "", false, false, MrsStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600221E RID: 8734 RVA: 0x0004B674 File Offset: 0x00049874
		public static LocalizedString UnableToFetchMimeStream(string identity)
		{
			return new LocalizedString("UnableToFetchMimeStream", "", false, false, MrsStrings.ResourceManager, new object[]
			{
				identity
			});
		}

		// Token: 0x0600221F RID: 8735 RVA: 0x0004B6A4 File Offset: 0x000498A4
		public static LocalizedString ReportUnableToUpdateSourceMailbox(string errorType, LocalizedString errorMsg, string trace)
		{
			return new LocalizedString("ReportUnableToUpdateSourceMailbox", "Ex1D7F37", false, true, MrsStrings.ResourceManager, new object[]
			{
				errorType,
				errorMsg,
				trace
			});
		}

		// Token: 0x06002220 RID: 8736 RVA: 0x0004B6E0 File Offset: 0x000498E0
		public static LocalizedString ErrorNoCASServersInSite(string site, string minVersion)
		{
			return new LocalizedString("ErrorNoCASServersInSite", "Ex4D34FC", false, true, MrsStrings.ResourceManager, new object[]
			{
				site,
				minVersion
			});
		}

		// Token: 0x06002221 RID: 8737 RVA: 0x0004B714 File Offset: 0x00049914
		public static LocalizedString ReportSoftDeletedItemsNotMigrated(int itemCount, string itemsSize)
		{
			return new LocalizedString("ReportSoftDeletedItemsNotMigrated", "", false, false, MrsStrings.ResourceManager, new object[]
			{
				itemCount,
				itemsSize
			});
		}

		// Token: 0x17000CC8 RID: 3272
		// (get) Token: 0x06002222 RID: 8738 RVA: 0x0004B74C File Offset: 0x0004994C
		public static LocalizedString ErrorEmptyMailboxGuid
		{
			get
			{
				return new LocalizedString("ErrorEmptyMailboxGuid", "Ex3953C5", false, true, MrsStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06002223 RID: 8739 RVA: 0x0004B76C File Offset: 0x0004996C
		public static LocalizedString EasMoveFailedError(string moveStatus, string httpStatus, string sourcrFolderId, string destFolderId)
		{
			return new LocalizedString("EasMoveFailedError", "", false, false, MrsStrings.ResourceManager, new object[]
			{
				moveStatus,
				httpStatus,
				sourcrFolderId,
				destFolderId
			});
		}

		// Token: 0x17000CC9 RID: 3273
		// (get) Token: 0x06002224 RID: 8740 RVA: 0x0004B7A7 File Offset: 0x000499A7
		public static LocalizedString ReportArchiveAlreadyUpdated
		{
			get
			{
				return new LocalizedString("ReportArchiveAlreadyUpdated", "", false, false, MrsStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06002225 RID: 8741 RVA: 0x0004B7C8 File Offset: 0x000499C8
		public static LocalizedString ReportReplayActionsEnumerated(LocalizedString physicalMailboxId, int numberOfActions, int batch)
		{
			return new LocalizedString("ReportReplayActionsEnumerated", "", false, false, MrsStrings.ResourceManager, new object[]
			{
				physicalMailboxId,
				numberOfActions,
				batch
			});
		}

		// Token: 0x17000CCA RID: 3274
		// (get) Token: 0x06002226 RID: 8742 RVA: 0x0004B80E File Offset: 0x00049A0E
		public static LocalizedString JobHasBeenSynced
		{
			get
			{
				return new LocalizedString("JobHasBeenSynced", "", false, false, MrsStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000CCB RID: 3275
		// (get) Token: 0x06002227 RID: 8743 RVA: 0x0004B82C File Offset: 0x00049A2C
		public static LocalizedString JobHasBeenRelinquishedDueToLongRun
		{
			get
			{
				return new LocalizedString("JobHasBeenRelinquishedDueToLongRun", "", false, false, MrsStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06002228 RID: 8744 RVA: 0x0004B84C File Offset: 0x00049A4C
		public static LocalizedString ValidationTargetMDBMismatch(string adDatabase, string mrDatabase)
		{
			return new LocalizedString("ValidationTargetMDBMismatch", "ExAE91DE", false, true, MrsStrings.ResourceManager, new object[]
			{
				adDatabase,
				mrDatabase
			});
		}

		// Token: 0x17000CCC RID: 3276
		// (get) Token: 0x06002229 RID: 8745 RVA: 0x0004B87F File Offset: 0x00049A7F
		public static LocalizedString ReportRelinquishingJobDueToHAOrCIStalling
		{
			get
			{
				return new LocalizedString("ReportRelinquishingJobDueToHAOrCIStalling", "", false, false, MrsStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600222A RID: 8746 RVA: 0x0004B8A0 File Offset: 0x00049AA0
		public static LocalizedString EasMoveFailed(string errorMessage)
		{
			return new LocalizedString("EasMoveFailed", "", false, false, MrsStrings.ResourceManager, new object[]
			{
				errorMessage
			});
		}

		// Token: 0x0600222B RID: 8747 RVA: 0x0004B8D0 File Offset: 0x00049AD0
		public static LocalizedString ReportRequestSaveFailed2(string errorType)
		{
			return new LocalizedString("ReportRequestSaveFailed2", "", false, false, MrsStrings.ResourceManager, new object[]
			{
				errorType
			});
		}

		// Token: 0x0600222C RID: 8748 RVA: 0x0004B900 File Offset: 0x00049B00
		public static LocalizedString IsExcludedFromProvisioningError(Guid mdbName)
		{
			return new LocalizedString("IsExcludedFromProvisioningError", "", false, false, MrsStrings.ResourceManager, new object[]
			{
				mdbName
			});
		}

		// Token: 0x0600222D RID: 8749 RVA: 0x0004B934 File Offset: 0x00049B34
		public static LocalizedString ClusterIPMissingHosts(IPAddress clusterIp)
		{
			return new LocalizedString("ClusterIPMissingHosts", "", false, false, MrsStrings.ResourceManager, new object[]
			{
				clusterIp
			});
		}

		// Token: 0x0600222E RID: 8750 RVA: 0x0004B964 File Offset: 0x00049B64
		public static LocalizedString MoveCompleteFailedForAlreadyCanceledMove(Guid mbxGuid)
		{
			return new LocalizedString("MoveCompleteFailedForAlreadyCanceledMove", "Ex45C692", false, true, MrsStrings.ResourceManager, new object[]
			{
				mbxGuid
			});
		}

		// Token: 0x0600222F RID: 8751 RVA: 0x0004B998 File Offset: 0x00049B98
		public static LocalizedString WindowsLiveIDAddressIsMissing(string user)
		{
			return new LocalizedString("WindowsLiveIDAddressIsMissing", "Ex3FC517", false, true, MrsStrings.ResourceManager, new object[]
			{
				user
			});
		}

		// Token: 0x06002230 RID: 8752 RVA: 0x0004B9C8 File Offset: 0x00049BC8
		public static LocalizedString ReportStartedIsInteg(Guid mailboxGuid, Guid isIntegRequestGuid)
		{
			return new LocalizedString("ReportStartedIsInteg", "", false, false, MrsStrings.ResourceManager, new object[]
			{
				mailboxGuid,
				isIntegRequestGuid
			});
		}

		// Token: 0x06002231 RID: 8753 RVA: 0x0004BA08 File Offset: 0x00049C08
		public static LocalizedString ContentFilterIsInvalid(string msg)
		{
			return new LocalizedString("ContentFilterIsInvalid", "Ex5E2C36", false, true, MrsStrings.ResourceManager, new object[]
			{
				msg
			});
		}

		// Token: 0x17000CCD RID: 3277
		// (get) Token: 0x06002232 RID: 8754 RVA: 0x0004BA37 File Offset: 0x00049C37
		public static LocalizedString Mailbox
		{
			get
			{
				return new LocalizedString("Mailbox", "", false, false, MrsStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06002233 RID: 8755 RVA: 0x0004BA58 File Offset: 0x00049C58
		public static LocalizedString ReportCompletedIsInteg(Guid mailboxGuid, Guid isIntegRequestGuid)
		{
			return new LocalizedString("ReportCompletedIsInteg", "", false, false, MrsStrings.ResourceManager, new object[]
			{
				mailboxGuid,
				isIntegRequestGuid
			});
		}

		// Token: 0x06002234 RID: 8756 RVA: 0x0004BA98 File Offset: 0x00049C98
		public static LocalizedString BadItemSearchFolder(string folderName)
		{
			return new LocalizedString("BadItemSearchFolder", "", false, false, MrsStrings.ResourceManager, new object[]
			{
				folderName
			});
		}

		// Token: 0x06002235 RID: 8757 RVA: 0x0004BAC8 File Offset: 0x00049CC8
		public static LocalizedString UnableToGetPSTReceiveFolder(string filePath, string messageClass)
		{
			return new LocalizedString("UnableToGetPSTReceiveFolder", "Ex58C864", false, true, MrsStrings.ResourceManager, new object[]
			{
				filePath,
				messageClass
			});
		}

		// Token: 0x17000CCE RID: 3278
		// (get) Token: 0x06002236 RID: 8758 RVA: 0x0004BAFB File Offset: 0x00049CFB
		public static LocalizedString FolderHierarchyIsInconsistentTemporarily
		{
			get
			{
				return new LocalizedString("FolderHierarchyIsInconsistentTemporarily", "", false, false, MrsStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06002237 RID: 8759 RVA: 0x0004BB1C File Offset: 0x00049D1C
		public static LocalizedString BadItemLarge(string msgClass, string subject, string itemSize, string folderName)
		{
			return new LocalizedString("BadItemLarge", "", false, false, MrsStrings.ResourceManager, new object[]
			{
				msgClass,
				subject,
				itemSize,
				folderName
			});
		}

		// Token: 0x06002238 RID: 8760 RVA: 0x0004BB58 File Offset: 0x00049D58
		public static LocalizedString BadItemFolderPropertyMismatch(string folderName, string error)
		{
			return new LocalizedString("BadItemFolderPropertyMismatch", "", false, false, MrsStrings.ResourceManager, new object[]
			{
				folderName,
				error
			});
		}

		// Token: 0x06002239 RID: 8761 RVA: 0x0004BB8C File Offset: 0x00049D8C
		public static LocalizedString AuxFolderMoveTracingId(string orgID, Guid mbxGuid)
		{
			return new LocalizedString("AuxFolderMoveTracingId", "", false, false, MrsStrings.ResourceManager, new object[]
			{
				orgID,
				mbxGuid
			});
		}

		// Token: 0x0600223A RID: 8762 RVA: 0x0004BBC4 File Offset: 0x00049DC4
		public static LocalizedString JobHasBeenRelinquishedDueToDataGuaranteeTimeout(DateTime pickupTime)
		{
			return new LocalizedString("JobHasBeenRelinquishedDueToDataGuaranteeTimeout", "", false, false, MrsStrings.ResourceManager, new object[]
			{
				pickupTime
			});
		}

		// Token: 0x17000CCF RID: 3279
		// (get) Token: 0x0600223B RID: 8763 RVA: 0x0004BBF8 File Offset: 0x00049DF8
		public static LocalizedString RequestPriorityEmergency
		{
			get
			{
				return new LocalizedString("RequestPriorityEmergency", "", false, false, MrsStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600223C RID: 8764 RVA: 0x0004BC18 File Offset: 0x00049E18
		public static LocalizedString EasSendFailed(string errorMessage)
		{
			return new LocalizedString("EasSendFailed", "", false, false, MrsStrings.ResourceManager, new object[]
			{
				errorMessage
			});
		}

		// Token: 0x17000CD0 RID: 3280
		// (get) Token: 0x0600223D RID: 8765 RVA: 0x0004BC47 File Offset: 0x00049E47
		public static LocalizedString ReportRelinquishingJobDueToHAStall
		{
			get
			{
				return new LocalizedString("ReportRelinquishingJobDueToHAStall", "", false, false, MrsStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600223E RID: 8766 RVA: 0x0004BC68 File Offset: 0x00049E68
		public static LocalizedString ErrorRegKeyNotExist(string keyPath)
		{
			return new LocalizedString("ErrorRegKeyNotExist", "", false, false, MrsStrings.ResourceManager, new object[]
			{
				keyPath
			});
		}

		// Token: 0x17000CD1 RID: 3281
		// (get) Token: 0x0600223F RID: 8767 RVA: 0x0004BC97 File Offset: 0x00049E97
		public static LocalizedString CorruptSyncState
		{
			get
			{
				return new LocalizedString("CorruptSyncState", "", false, false, MrsStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06002240 RID: 8768 RVA: 0x0004BCB8 File Offset: 0x00049EB8
		public static LocalizedString ReportTooManyTransientFailures(int totalRetryCount)
		{
			return new LocalizedString("ReportTooManyTransientFailures", "ExB4C8AF", false, true, MrsStrings.ResourceManager, new object[]
			{
				totalRetryCount
			});
		}

		// Token: 0x06002241 RID: 8769 RVA: 0x0004BCEC File Offset: 0x00049EEC
		public static LocalizedString RestrictionDataContext(string restrictionStr)
		{
			return new LocalizedString("RestrictionDataContext", "Ex75BA5A", false, true, MrsStrings.ResourceManager, new object[]
			{
				restrictionStr
			});
		}

		// Token: 0x06002242 RID: 8770 RVA: 0x0004BD1C File Offset: 0x00049F1C
		public static LocalizedString ServerNotFoundByGuid(Guid serverGuid)
		{
			return new LocalizedString("ServerNotFoundByGuid", "", false, false, MrsStrings.ResourceManager, new object[]
			{
				serverGuid
			});
		}

		// Token: 0x06002243 RID: 8771 RVA: 0x0004BD50 File Offset: 0x00049F50
		public static LocalizedString MoveRequestMessageWarning(LocalizedString message)
		{
			return new LocalizedString("MoveRequestMessageWarning", "Ex92F5D6", false, true, MrsStrings.ResourceManager, new object[]
			{
				message
			});
		}

		// Token: 0x06002244 RID: 8772 RVA: 0x0004BD84 File Offset: 0x00049F84
		public static LocalizedString MRSInstanceFailed(string mrsInstance, LocalizedString exceptionMessage)
		{
			return new LocalizedString("MRSInstanceFailed", "Ex23B90F", false, true, MrsStrings.ResourceManager, new object[]
			{
				mrsInstance,
				exceptionMessage
			});
		}

		// Token: 0x06002245 RID: 8773 RVA: 0x0004BDBC File Offset: 0x00049FBC
		public static LocalizedString ReportTargetMailboxAfterFinalization2(string userID, string domainControllerName)
		{
			return new LocalizedString("ReportTargetMailboxAfterFinalization2", "", false, false, MrsStrings.ResourceManager, new object[]
			{
				userID,
				domainControllerName
			});
		}

		// Token: 0x06002246 RID: 8774 RVA: 0x0004BDF0 File Offset: 0x00049FF0
		public static LocalizedString EasCountFailed(string errorMessage)
		{
			return new LocalizedString("EasCountFailed", "", false, false, MrsStrings.ResourceManager, new object[]
			{
				errorMessage
			});
		}

		// Token: 0x06002247 RID: 8775 RVA: 0x0004BE20 File Offset: 0x0004A020
		public static LocalizedString FolderReferencedMoreThanOnce(string folderPath)
		{
			return new LocalizedString("FolderReferencedMoreThanOnce", "Ex37DDC0", false, true, MrsStrings.ResourceManager, new object[]
			{
				folderPath
			});
		}

		// Token: 0x06002248 RID: 8776 RVA: 0x0004BE50 File Offset: 0x0004A050
		public static LocalizedString RecipientAggregatedMailboxNotFound(string recipient, string recipientAggregatedMailboxGuidsAsString, Guid targetAggregatedMailboxGuid)
		{
			return new LocalizedString("RecipientAggregatedMailboxNotFound", "", false, false, MrsStrings.ResourceManager, new object[]
			{
				recipient,
				recipientAggregatedMailboxGuidsAsString,
				targetAggregatedMailboxGuid
			});
		}

		// Token: 0x06002249 RID: 8777 RVA: 0x0004BE8C File Offset: 0x0004A08C
		public static LocalizedString PostSaveActionFailed(LocalizedString error)
		{
			return new LocalizedString("PostSaveActionFailed", "ExEEA65D", false, true, MrsStrings.ResourceManager, new object[]
			{
				error
			});
		}

		// Token: 0x0600224A RID: 8778 RVA: 0x0004BEC0 File Offset: 0x0004A0C0
		public static LocalizedString ReportReplayActionsSynced(LocalizedString physicalMailboxId, int numberOfActionsReplayed, int numberOfActionsIgnored)
		{
			return new LocalizedString("ReportReplayActionsSynced", "", false, false, MrsStrings.ResourceManager, new object[]
			{
				physicalMailboxId,
				numberOfActionsReplayed,
				numberOfActionsIgnored
			});
		}

		// Token: 0x17000CD2 RID: 3282
		// (get) Token: 0x0600224B RID: 8779 RVA: 0x0004BF06 File Offset: 0x0004A106
		public static LocalizedString ReportTargetPublicFolderContentMailboxGuidUpdated
		{
			get
			{
				return new LocalizedString("ReportTargetPublicFolderContentMailboxGuidUpdated", "", false, false, MrsStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600224C RID: 8780 RVA: 0x0004BF24 File Offset: 0x0004A124
		public static LocalizedString FolderPathIsInvalid(string folderPath)
		{
			return new LocalizedString("FolderPathIsInvalid", "Ex7903F7", false, true, MrsStrings.ResourceManager, new object[]
			{
				folderPath
			});
		}

		// Token: 0x0600224D RID: 8781 RVA: 0x0004BF54 File Offset: 0x0004A154
		public static LocalizedString RestoreMailboxTracingId(string dbName, Guid mailboxGuid)
		{
			return new LocalizedString("RestoreMailboxTracingId", "", false, false, MrsStrings.ResourceManager, new object[]
			{
				dbName,
				mailboxGuid
			});
		}

		// Token: 0x0600224E RID: 8782 RVA: 0x0004BF8C File Offset: 0x0004A18C
		public static LocalizedString ReportMoveRequestRemoved(string userName)
		{
			return new LocalizedString("ReportMoveRequestRemoved", "Ex36440E", false, true, MrsStrings.ResourceManager, new object[]
			{
				userName
			});
		}

		// Token: 0x0600224F RID: 8783 RVA: 0x0004BFBC File Offset: 0x0004A1BC
		public static LocalizedString JobHasBeenRelinquishedDueToServerBusy(LocalizedString error, TimeSpan backoffTimeSpan)
		{
			return new LocalizedString("JobHasBeenRelinquishedDueToServerBusy", "", false, false, MrsStrings.ResourceManager, new object[]
			{
				error,
				backoffTimeSpan
			});
		}

		// Token: 0x06002250 RID: 8784 RVA: 0x0004BFFC File Offset: 0x0004A1FC
		public static LocalizedString PickupStatusSubTypeNotSupported(string requestType)
		{
			return new LocalizedString("PickupStatusSubTypeNotSupported", "", false, false, MrsStrings.ResourceManager, new object[]
			{
				requestType
			});
		}

		// Token: 0x17000CD3 RID: 3283
		// (get) Token: 0x06002251 RID: 8785 RVA: 0x0004C02B File Offset: 0x0004A22B
		public static LocalizedString NoMRSAvailable
		{
			get
			{
				return new LocalizedString("NoMRSAvailable", "ExA4CEA5", false, true, MrsStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06002252 RID: 8786 RVA: 0x0004C04C File Offset: 0x0004A24C
		public static LocalizedString PickupStatusReservationFailure(LocalizedString exceptionMessage)
		{
			return new LocalizedString("PickupStatusReservationFailure", "", false, false, MrsStrings.ResourceManager, new object[]
			{
				exceptionMessage
			});
		}

		// Token: 0x06002253 RID: 8787 RVA: 0x0004C080 File Offset: 0x0004A280
		public static LocalizedString ReportIncrementalSyncHierarchyChanges(LocalizedString physicalMailboxId, int changedFolders, int deletedFolders)
		{
			return new LocalizedString("ReportIncrementalSyncHierarchyChanges", "", false, false, MrsStrings.ResourceManager, new object[]
			{
				physicalMailboxId,
				changedFolders,
				deletedFolders
			});
		}

		// Token: 0x17000CD4 RID: 3284
		// (get) Token: 0x06002254 RID: 8788 RVA: 0x0004C0C6 File Offset: 0x0004A2C6
		public static LocalizedString RequestPriorityLower
		{
			get
			{
				return new LocalizedString("RequestPriorityLower", "", false, false, MrsStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06002255 RID: 8789 RVA: 0x0004C0E4 File Offset: 0x0004A2E4
		public static LocalizedString InvalidHandleType(long handle, string handleType, string expectedType)
		{
			return new LocalizedString("InvalidHandleType", "Ex3B9BC5", false, true, MrsStrings.ResourceManager, new object[]
			{
				handle,
				handleType,
				expectedType
			});
		}

		// Token: 0x06002256 RID: 8790 RVA: 0x0004C120 File Offset: 0x0004A320
		public static LocalizedString ProviderAlreadySpecificToDatabase(Guid oldMdbGuid, Guid newMdbGuid)
		{
			return new LocalizedString("ProviderAlreadySpecificToDatabase", "Ex5EF850", false, true, MrsStrings.ResourceManager, new object[]
			{
				oldMdbGuid,
				newMdbGuid
			});
		}

		// Token: 0x06002257 RID: 8791 RVA: 0x0004C160 File Offset: 0x0004A360
		public static LocalizedString ReportSourceMailboxCleanupFailed2(LocalizedString mailboxId, string errorType, LocalizedString errorMsg, string trace, int currentAttempt, int totalAttempts)
		{
			return new LocalizedString("ReportSourceMailboxCleanupFailed2", "ExB72C5D", false, true, MrsStrings.ResourceManager, new object[]
			{
				mailboxId,
				errorType,
				errorMsg,
				trace,
				currentAttempt,
				totalAttempts
			});
		}

		// Token: 0x06002258 RID: 8792 RVA: 0x0004C1BC File Offset: 0x0004A3BC
		public static LocalizedString ReportReplayActionsCompleted(LocalizedString physicalMailboxId, int numberOfActionsReplayed, int numberOfActionsIgnored)
		{
			return new LocalizedString("ReportReplayActionsCompleted", "", false, false, MrsStrings.ResourceManager, new object[]
			{
				physicalMailboxId,
				numberOfActionsReplayed,
				numberOfActionsIgnored
			});
		}

		// Token: 0x06002259 RID: 8793 RVA: 0x0004C204 File Offset: 0x0004A404
		public static LocalizedString ReportTargetMailUserBeforeFinalization(string userDataXML)
		{
			return new LocalizedString("ReportTargetMailUserBeforeFinalization", "Ex0F3B97", false, true, MrsStrings.ResourceManager, new object[]
			{
				userDataXML
			});
		}

		// Token: 0x0600225A RID: 8794 RVA: 0x0004C234 File Offset: 0x0004A434
		public static LocalizedString MailPublicFolderWithObjectIdNotFound(Guid objectId)
		{
			return new LocalizedString("MailPublicFolderWithObjectIdNotFound", "", false, false, MrsStrings.ResourceManager, new object[]
			{
				objectId
			});
		}

		// Token: 0x0600225B RID: 8795 RVA: 0x0004C268 File Offset: 0x0004A468
		public static LocalizedString ReportIncrementalSyncCompleted(LocalizedString physicalMailboxId, int numberOfUpdates)
		{
			return new LocalizedString("ReportIncrementalSyncCompleted", "ExD7C287", false, true, MrsStrings.ResourceManager, new object[]
			{
				physicalMailboxId,
				numberOfUpdates
			});
		}

		// Token: 0x0600225C RID: 8796 RVA: 0x0004C2A8 File Offset: 0x0004A4A8
		public static LocalizedString PickupStatusProxyBackoff(string remoteHostName)
		{
			return new LocalizedString("PickupStatusProxyBackoff", "", false, false, MrsStrings.ResourceManager, new object[]
			{
				remoteHostName
			});
		}

		// Token: 0x0600225D RID: 8797 RVA: 0x0004C2D8 File Offset: 0x0004A4D8
		public static LocalizedString MailPublicFolderWithMultipleEntriesFound(string legacyExchangeDN)
		{
			return new LocalizedString("MailPublicFolderWithMultipleEntriesFound", "", false, false, MrsStrings.ResourceManager, new object[]
			{
				legacyExchangeDN
			});
		}

		// Token: 0x17000CD5 RID: 3285
		// (get) Token: 0x0600225E RID: 8798 RVA: 0x0004C307 File Offset: 0x0004A507
		public static LocalizedString ReportAutoSuspendingJob
		{
			get
			{
				return new LocalizedString("ReportAutoSuspendingJob", "Ex15DBB7", false, true, MrsStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600225F RID: 8799 RVA: 0x0004C328 File Offset: 0x0004A528
		public static LocalizedString MoveRequestMessageInformational(LocalizedString message)
		{
			return new LocalizedString("MoveRequestMessageInformational", "Ex6C0B53", false, true, MrsStrings.ResourceManager, new object[]
			{
				message
			});
		}

		// Token: 0x06002260 RID: 8800 RVA: 0x0004C35C File Offset: 0x0004A55C
		public static LocalizedString ReportJobIsStalledWithFailure(LocalizedString failureMsg, DateTime willWaitUntilTimestamp)
		{
			return new LocalizedString("ReportJobIsStalledWithFailure", "Ex821B3B", false, true, MrsStrings.ResourceManager, new object[]
			{
				failureMsg,
				willWaitUntilTimestamp
			});
		}

		// Token: 0x06002261 RID: 8801 RVA: 0x0004C39C File Offset: 0x0004A59C
		public static LocalizedString ReportIncrementalSyncContentChanges2(LocalizedString physicalMailboxId, int newMessages, int changedMessages, int deletedMessages, int readMessages, int unreadMessages, int total)
		{
			return new LocalizedString("ReportIncrementalSyncContentChanges2", "", false, false, MrsStrings.ResourceManager, new object[]
			{
				physicalMailboxId,
				newMessages,
				changedMessages,
				deletedMessages,
				readMessages,
				unreadMessages,
				total
			});
		}

		// Token: 0x06002262 RID: 8802 RVA: 0x0004C40C File Offset: 0x0004A60C
		public static LocalizedString NotSupportedCodePageError(int codePage, string server)
		{
			return new LocalizedString("NotSupportedCodePageError", "", false, false, MrsStrings.ResourceManager, new object[]
			{
				codePage,
				server
			});
		}

		// Token: 0x06002263 RID: 8803 RVA: 0x0004C444 File Offset: 0x0004A644
		public static LocalizedString RecipientInvalidLegDN(string recipient, string legacyExchangeDN)
		{
			return new LocalizedString("RecipientInvalidLegDN", "", false, false, MrsStrings.ResourceManager, new object[]
			{
				recipient,
				legacyExchangeDN
			});
		}

		// Token: 0x06002264 RID: 8804 RVA: 0x0004C478 File Offset: 0x0004A678
		public static LocalizedString ReportMailboxContentsVerificationComplete(int folderCount, int itemCount, string itemSizeStr)
		{
			return new LocalizedString("ReportMailboxContentsVerificationComplete", "", false, false, MrsStrings.ResourceManager, new object[]
			{
				folderCount,
				itemCount,
				itemSizeStr
			});
		}

		// Token: 0x06002265 RID: 8805 RVA: 0x0004C4BC File Offset: 0x0004A6BC
		public static LocalizedString ValidationOrganizationMismatch(string jobOrg, string indexOrg)
		{
			return new LocalizedString("ValidationOrganizationMismatch", "ExC6F293", false, true, MrsStrings.ResourceManager, new object[]
			{
				jobOrg,
				indexOrg
			});
		}

		// Token: 0x06002266 RID: 8806 RVA: 0x0004C4F0 File Offset: 0x0004A6F0
		public static LocalizedString UnableToStreamPSTProp(uint propTag, int offset, int bytesToRead, long length)
		{
			return new LocalizedString("UnableToStreamPSTProp", "", false, false, MrsStrings.ResourceManager, new object[]
			{
				propTag,
				offset,
				bytesToRead,
				length
			});
		}

		// Token: 0x06002267 RID: 8807 RVA: 0x0004C540 File Offset: 0x0004A740
		public static LocalizedString UnsupportedClientVersionWithOperation(string clientName, string clientVersion, string operationName)
		{
			return new LocalizedString("UnsupportedClientVersionWithOperation", "Ex3032A7", false, true, MrsStrings.ResourceManager, new object[]
			{
				clientName,
				clientVersion,
				operationName
			});
		}

		// Token: 0x17000CD6 RID: 3286
		// (get) Token: 0x06002268 RID: 8808 RVA: 0x0004C577 File Offset: 0x0004A777
		public static LocalizedString ReportCalendarFolderFaiSaveFailed
		{
			get
			{
				return new LocalizedString("ReportCalendarFolderFaiSaveFailed", "", false, false, MrsStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000CD7 RID: 3287
		// (get) Token: 0x06002269 RID: 8809 RVA: 0x0004C595 File Offset: 0x0004A795
		public static LocalizedString MoveIsPreventedFromFinalization
		{
			get
			{
				return new LocalizedString("MoveIsPreventedFromFinalization", "", false, false, MrsStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000CD8 RID: 3288
		// (get) Token: 0x0600226A RID: 8810 RVA: 0x0004C5B3 File Offset: 0x0004A7B3
		public static LocalizedString ReportMoveAlreadyFinished
		{
			get
			{
				return new LocalizedString("ReportMoveAlreadyFinished", "ExF76633", false, true, MrsStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000CD9 RID: 3289
		// (get) Token: 0x0600226B RID: 8811 RVA: 0x0004C5D1 File Offset: 0x0004A7D1
		public static LocalizedString RehomeRequestFailure
		{
			get
			{
				return new LocalizedString("RehomeRequestFailure", "", false, false, MrsStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600226C RID: 8812 RVA: 0x0004C5F0 File Offset: 0x0004A7F0
		public static LocalizedString ReportJobRehomed(string sourceMDB, string targetMDB)
		{
			return new LocalizedString("ReportJobRehomed", "", false, false, MrsStrings.ResourceManager, new object[]
			{
				sourceMDB,
				targetMDB
			});
		}

		// Token: 0x0600226D RID: 8813 RVA: 0x0004C624 File Offset: 0x0004A824
		public static LocalizedString ReportSourceMailboxCleanupSkipped(LocalizedString mailboxId)
		{
			return new LocalizedString("ReportSourceMailboxCleanupSkipped", "", false, false, MrsStrings.ResourceManager, new object[]
			{
				mailboxId
			});
		}

		// Token: 0x17000CDA RID: 3290
		// (get) Token: 0x0600226E RID: 8814 RVA: 0x0004C658 File Offset: 0x0004A858
		public static LocalizedString RequestIsStalledByHigherPriorityJobs
		{
			get
			{
				return new LocalizedString("RequestIsStalledByHigherPriorityJobs", "", false, false, MrsStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000CDB RID: 3291
		// (get) Token: 0x0600226F RID: 8815 RVA: 0x0004C676 File Offset: 0x0004A876
		public static LocalizedString WorkloadTypeEmergency
		{
			get
			{
				return new LocalizedString("WorkloadTypeEmergency", "", false, false, MrsStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06002270 RID: 8816 RVA: 0x0004C694 File Offset: 0x0004A894
		public static LocalizedString ReportFailedToDisconnectFromDestination(string errorType, LocalizedString errorMsg, string trace)
		{
			return new LocalizedString("ReportFailedToDisconnectFromDestination", "Ex37060D", false, true, MrsStrings.ResourceManager, new object[]
			{
				errorType,
				errorMsg,
				trace
			});
		}

		// Token: 0x17000CDC RID: 3292
		// (get) Token: 0x06002271 RID: 8817 RVA: 0x0004C6D0 File Offset: 0x0004A8D0
		public static LocalizedString ReportCalendarFolderSaveFailed
		{
			get
			{
				return new LocalizedString("ReportCalendarFolderSaveFailed", "", false, false, MrsStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06002272 RID: 8818 RVA: 0x0004C6F0 File Offset: 0x0004A8F0
		public static LocalizedString IsIntegTooLongError(DateTime firstRepairAttemptedAt)
		{
			return new LocalizedString("IsIntegTooLongError", "", false, false, MrsStrings.ResourceManager, new object[]
			{
				firstRepairAttemptedAt
			});
		}

		// Token: 0x06002273 RID: 8819 RVA: 0x0004C724 File Offset: 0x0004A924
		public static LocalizedString ReportIncrementalSyncChanges(LocalizedString physicalMailboxId, int changedFolders, int deletedFolders, int messageChanges)
		{
			return new LocalizedString("ReportIncrementalSyncChanges", "Ex997352", false, true, MrsStrings.ResourceManager, new object[]
			{
				physicalMailboxId,
				changedFolders,
				deletedFolders,
				messageChanges
			});
		}

		// Token: 0x06002274 RID: 8820 RVA: 0x0004C774 File Offset: 0x0004A974
		public static LocalizedString UnableToCreatePSTMessage(string filePath)
		{
			return new LocalizedString("UnableToCreatePSTMessage", "Ex9E4BEA", false, true, MrsStrings.ResourceManager, new object[]
			{
				filePath
			});
		}

		// Token: 0x06002275 RID: 8821 RVA: 0x0004C7A4 File Offset: 0x0004A9A4
		public static LocalizedString ErrorStaticCapacityExceeded(string resourceName, int capacity)
		{
			return new LocalizedString("ErrorStaticCapacityExceeded", "", false, false, MrsStrings.ResourceManager, new object[]
			{
				resourceName,
				capacity
			});
		}

		// Token: 0x06002276 RID: 8822 RVA: 0x0004C7DC File Offset: 0x0004A9DC
		public static LocalizedString FolderAlreadyExists(string name)
		{
			return new LocalizedString("FolderAlreadyExists", "", false, false, MrsStrings.ResourceManager, new object[]
			{
				name
			});
		}

		// Token: 0x06002277 RID: 8823 RVA: 0x0004C80C File Offset: 0x0004AA0C
		public static LocalizedString ReportMoveResumed(string status)
		{
			return new LocalizedString("ReportMoveResumed", "Ex64D6A5", false, true, MrsStrings.ResourceManager, new object[]
			{
				status
			});
		}

		// Token: 0x06002278 RID: 8824 RVA: 0x0004C83C File Offset: 0x0004AA3C
		public static LocalizedString ReportSoftDeletedItemCountsAndSizes(string softDeletedItems)
		{
			return new LocalizedString("ReportSoftDeletedItemCountsAndSizes", "Ex9D9096", false, true, MrsStrings.ResourceManager, new object[]
			{
				softDeletedItems
			});
		}

		// Token: 0x06002279 RID: 8825 RVA: 0x0004C86C File Offset: 0x0004AA6C
		public static LocalizedString MissingDatabaseName(Guid dbGuid)
		{
			return new LocalizedString("MissingDatabaseName", "", false, false, MrsStrings.ResourceManager, new object[]
			{
				dbGuid
			});
		}

		// Token: 0x0600227A RID: 8826 RVA: 0x0004C8A0 File Offset: 0x0004AAA0
		public static LocalizedString ReportUpdateMovedMailboxError(LocalizedString error)
		{
			return new LocalizedString("ReportUpdateMovedMailboxError", "", false, false, MrsStrings.ResourceManager, new object[]
			{
				error
			});
		}

		// Token: 0x0600227B RID: 8827 RVA: 0x0004C8D4 File Offset: 0x0004AAD4
		public static LocalizedString UnableToVerifyMailboxConnectivity(LocalizedString mailboxId)
		{
			return new LocalizedString("UnableToVerifyMailboxConnectivity", "", false, false, MrsStrings.ResourceManager, new object[]
			{
				mailboxId
			});
		}

		// Token: 0x0600227C RID: 8828 RVA: 0x0004C908 File Offset: 0x0004AB08
		public static LocalizedString BadItemCorrupt(string msgClass, string subject, string folderName)
		{
			return new LocalizedString("BadItemCorrupt", "", false, false, MrsStrings.ResourceManager, new object[]
			{
				msgClass,
				subject,
				folderName
			});
		}

		// Token: 0x17000CDD RID: 3293
		// (get) Token: 0x0600227D RID: 8829 RVA: 0x0004C93F File Offset: 0x0004AB3F
		public static LocalizedString MRSProxyConnectionNotThrottledError
		{
			get
			{
				return new LocalizedString("MRSProxyConnectionNotThrottledError", "ExD69EFE", false, true, MrsStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600227E RID: 8830 RVA: 0x0004C960 File Offset: 0x0004AB60
		public static LocalizedString ReportMailboxSignatureIsNotPreserved(LocalizedString mailboxId)
		{
			return new LocalizedString("ReportMailboxSignatureIsNotPreserved", "Ex28305C", false, true, MrsStrings.ResourceManager, new object[]
			{
				mailboxId
			});
		}

		// Token: 0x17000CDE RID: 3294
		// (get) Token: 0x0600227F RID: 8831 RVA: 0x0004C994 File Offset: 0x0004AB94
		public static LocalizedString ReportWaitingForMailboxDataReplication
		{
			get
			{
				return new LocalizedString("ReportWaitingForMailboxDataReplication", "Ex806433", false, true, MrsStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06002280 RID: 8832 RVA: 0x0004C9B4 File Offset: 0x0004ABB4
		public static LocalizedString ReportProxyConnectionLimitMet(DateTime pickUpTime)
		{
			return new LocalizedString("ReportProxyConnectionLimitMet", "Ex1D0478", false, true, MrsStrings.ResourceManager, new object[]
			{
				pickUpTime
			});
		}

		// Token: 0x06002281 RID: 8833 RVA: 0x0004C9E8 File Offset: 0x0004ABE8
		public static LocalizedString FolderAlreadyInTarget(string folderId)
		{
			return new LocalizedString("FolderAlreadyInTarget", "", false, false, MrsStrings.ResourceManager, new object[]
			{
				folderId
			});
		}

		// Token: 0x06002282 RID: 8834 RVA: 0x0004CA18 File Offset: 0x0004AC18
		public static LocalizedString ReportTargetFolderDeleted(string targetFolderName, string targetFolderEntryIdStr, string sourceFolderName)
		{
			return new LocalizedString("ReportTargetFolderDeleted", "Ex01A8A8", false, true, MrsStrings.ResourceManager, new object[]
			{
				targetFolderName,
				targetFolderEntryIdStr,
				sourceFolderName
			});
		}

		// Token: 0x06002283 RID: 8835 RVA: 0x0004CA50 File Offset: 0x0004AC50
		public static LocalizedString FolderCopyFailed(string folderName)
		{
			return new LocalizedString("FolderCopyFailed", "Ex73C57C", false, true, MrsStrings.ResourceManager, new object[]
			{
				folderName
			});
		}

		// Token: 0x17000CDF RID: 3295
		// (get) Token: 0x06002284 RID: 8836 RVA: 0x0004CA7F File Offset: 0x0004AC7F
		public static LocalizedString ReportDatabaseFailedOver
		{
			get
			{
				return new LocalizedString("ReportDatabaseFailedOver", "Ex5E3943", false, true, MrsStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06002285 RID: 8837 RVA: 0x0004CAA0 File Offset: 0x0004ACA0
		public static LocalizedString ErrorStaticCapacityExceeded1(string resourceName, string resourceType, int capacity)
		{
			return new LocalizedString("ErrorStaticCapacityExceeded1", "", false, false, MrsStrings.ResourceManager, new object[]
			{
				resourceName,
				resourceType,
				capacity
			});
		}

		// Token: 0x06002286 RID: 8838 RVA: 0x0004CADC File Offset: 0x0004ACDC
		public static LocalizedString ReportWaitingForAdReplication(int delaySecs, int iAttempts, int iMaxRetries)
		{
			return new LocalizedString("ReportWaitingForAdReplication", "ExAD7B08", false, true, MrsStrings.ResourceManager, new object[]
			{
				delaySecs,
				iAttempts,
				iMaxRetries
			});
		}

		// Token: 0x17000CE0 RID: 3296
		// (get) Token: 0x06002287 RID: 8839 RVA: 0x0004CB22 File Offset: 0x0004AD22
		public static LocalizedString FolderIsMissingInMerge
		{
			get
			{
				return new LocalizedString("FolderIsMissingInMerge", "", false, false, MrsStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06002288 RID: 8840 RVA: 0x0004CB40 File Offset: 0x0004AD40
		public static LocalizedString MailboxServerInformation(string serverName, string serverVersion)
		{
			return new LocalizedString("MailboxServerInformation", "Ex0E4F93", false, true, MrsStrings.ResourceManager, new object[]
			{
				serverName,
				serverVersion
			});
		}

		// Token: 0x06002289 RID: 8841 RVA: 0x0004CB74 File Offset: 0x0004AD74
		public static LocalizedString ReportTargetMailUserBeforeFinalization2(string userID, string domainControllerName)
		{
			return new LocalizedString("ReportTargetMailUserBeforeFinalization2", "", false, false, MrsStrings.ResourceManager, new object[]
			{
				userID,
				domainControllerName
			});
		}

		// Token: 0x0600228A RID: 8842 RVA: 0x0004CBA8 File Offset: 0x0004ADA8
		public static LocalizedString MoveHasBeenAutoSuspended(Guid mbxGuid)
		{
			return new LocalizedString("MoveHasBeenAutoSuspended", "ExBFE36D", false, true, MrsStrings.ResourceManager, new object[]
			{
				mbxGuid
			});
		}

		// Token: 0x17000CE1 RID: 3297
		// (get) Token: 0x0600228B RID: 8843 RVA: 0x0004CBDC File Offset: 0x0004ADDC
		public static LocalizedString Unknown
		{
			get
			{
				return new LocalizedString("Unknown", "", false, false, MrsStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600228C RID: 8844 RVA: 0x0004CBFC File Offset: 0x0004ADFC
		public static LocalizedString ReportRelinquishingJobDueToFailover(string serverName)
		{
			return new LocalizedString("ReportRelinquishingJobDueToFailover", "", false, false, MrsStrings.ResourceManager, new object[]
			{
				serverName
			});
		}

		// Token: 0x0600228D RID: 8845 RVA: 0x0004CC2C File Offset: 0x0004AE2C
		public static LocalizedString OrphanedDatabaseName(LocalizedString dbName)
		{
			return new LocalizedString("OrphanedDatabaseName", "", false, false, MrsStrings.ResourceManager, new object[]
			{
				dbName
			});
		}

		// Token: 0x0600228E RID: 8846 RVA: 0x0004CC60 File Offset: 0x0004AE60
		public static LocalizedString ReportTargetMailboxAfterFinalization(string userDataXML)
		{
			return new LocalizedString("ReportTargetMailboxAfterFinalization", "ExB9CEEC", false, true, MrsStrings.ResourceManager, new object[]
			{
				userDataXML
			});
		}

		// Token: 0x0600228F RID: 8847 RVA: 0x0004CC90 File Offset: 0x0004AE90
		public static LocalizedString RecipientPropertyIsNotWriteable(string recipient, string propertyName)
		{
			return new LocalizedString("RecipientPropertyIsNotWriteable", "", false, false, MrsStrings.ResourceManager, new object[]
			{
				recipient,
				propertyName
			});
		}

		// Token: 0x17000CE2 RID: 3298
		// (get) Token: 0x06002290 RID: 8848 RVA: 0x0004CCC3 File Offset: 0x0004AEC3
		public static LocalizedString WorkloadTypeSyncAggregation
		{
			get
			{
				return new LocalizedString("WorkloadTypeSyncAggregation", "", false, false, MrsStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06002291 RID: 8849 RVA: 0x0004CCE4 File Offset: 0x0004AEE4
		public static LocalizedString PropertyTagsDoNotMatch(uint propTagFromSource, uint propTagFromDestination)
		{
			return new LocalizedString("PropertyTagsDoNotMatch", "", false, false, MrsStrings.ResourceManager, new object[]
			{
				propTagFromSource,
				propTagFromDestination
			});
		}

		// Token: 0x06002292 RID: 8850 RVA: 0x0004CD24 File Offset: 0x0004AF24
		public static LocalizedString PrimaryMailboxTracingId(string orgID, Guid mbxGuid)
		{
			return new LocalizedString("PrimaryMailboxTracingId", "", false, false, MrsStrings.ResourceManager, new object[]
			{
				orgID,
				mbxGuid
			});
		}

		// Token: 0x06002293 RID: 8851 RVA: 0x0004CD5C File Offset: 0x0004AF5C
		public static LocalizedString ArchiveMailboxId(Guid mbxGuid)
		{
			return new LocalizedString("ArchiveMailboxId", "Ex9FA974", false, true, MrsStrings.ResourceManager, new object[]
			{
				mbxGuid
			});
		}

		// Token: 0x17000CE3 RID: 3299
		// (get) Token: 0x06002294 RID: 8852 RVA: 0x0004CD90 File Offset: 0x0004AF90
		public static LocalizedString ReportTargetUserIsNotMailEnabledUser
		{
			get
			{
				return new LocalizedString("ReportTargetUserIsNotMailEnabledUser", "Ex7AA32B", false, true, MrsStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06002295 RID: 8853 RVA: 0x0004CDB0 File Offset: 0x0004AFB0
		public static LocalizedString ImapObjectNotFound(string entryId)
		{
			return new LocalizedString("ImapObjectNotFound", "", false, false, MrsStrings.ResourceManager, new object[]
			{
				entryId
			});
		}

		// Token: 0x06002296 RID: 8854 RVA: 0x0004CDE0 File Offset: 0x0004AFE0
		public static LocalizedString FolderReferencedAsBothIncludedAndExcluded(string folderPath)
		{
			return new LocalizedString("FolderReferencedAsBothIncludedAndExcluded", "ExEB6169", false, true, MrsStrings.ResourceManager, new object[]
			{
				folderPath
			});
		}

		// Token: 0x17000CE4 RID: 3300
		// (get) Token: 0x06002297 RID: 8855 RVA: 0x0004CE0F File Offset: 0x0004B00F
		public static LocalizedString ReportRequestIsNoLongerSticky
		{
			get
			{
				return new LocalizedString("ReportRequestIsNoLongerSticky", "", false, false, MrsStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06002298 RID: 8856 RVA: 0x0004CE30 File Offset: 0x0004B030
		public static LocalizedString DestinationMailboxSyncStateDeletionFailed(LocalizedString errorMsg)
		{
			return new LocalizedString("DestinationMailboxSyncStateDeletionFailed", "ExC3111B", false, true, MrsStrings.ResourceManager, new object[]
			{
				errorMsg
			});
		}

		// Token: 0x17000CE5 RID: 3301
		// (get) Token: 0x06002299 RID: 8857 RVA: 0x0004CE64 File Offset: 0x0004B064
		public static LocalizedString MoveRequestTypeIntraOrg
		{
			get
			{
				return new LocalizedString("MoveRequestTypeIntraOrg", "ExA16067", false, true, MrsStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600229A RID: 8858 RVA: 0x0004CE84 File Offset: 0x0004B084
		public static LocalizedString FolderHierarchyContainsDuplicates(string folder1str, string folder2str)
		{
			return new LocalizedString("FolderHierarchyContainsDuplicates", "", false, false, MrsStrings.ResourceManager, new object[]
			{
				folder1str,
				folder2str
			});
		}

		// Token: 0x0600229B RID: 8859 RVA: 0x0004CEB8 File Offset: 0x0004B0B8
		public static LocalizedString ReportSourceFolderDeleted(string sourceFolderName, string sourceFolderEntryIdStr)
		{
			return new LocalizedString("ReportSourceFolderDeleted", "Ex8A2D15", false, true, MrsStrings.ResourceManager, new object[]
			{
				sourceFolderName,
				sourceFolderEntryIdStr
			});
		}

		// Token: 0x17000CE6 RID: 3302
		// (get) Token: 0x0600229C RID: 8860 RVA: 0x0004CEEB File Offset: 0x0004B0EB
		public static LocalizedString ValidationMoveRequestNotDeserialized
		{
			get
			{
				return new LocalizedString("ValidationMoveRequestNotDeserialized", "Ex6468E8", false, true, MrsStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600229D RID: 8861 RVA: 0x0004CF0C File Offset: 0x0004B10C
		public static LocalizedString ReportSoftDeletedItemCountsAndSizesLoc(LocalizedString softDeletedItems)
		{
			return new LocalizedString("ReportSoftDeletedItemCountsAndSizesLoc", "ExF1A8E0", false, true, MrsStrings.ResourceManager, new object[]
			{
				softDeletedItems
			});
		}

		// Token: 0x0600229E RID: 8862 RVA: 0x0004CF40 File Offset: 0x0004B140
		public static LocalizedString EasSyncFailedTransiently(string syncStatus, string httpStatus, string folderId)
		{
			return new LocalizedString("EasSyncFailedTransiently", "", false, false, MrsStrings.ResourceManager, new object[]
			{
				syncStatus,
				httpStatus,
				folderId
			});
		}

		// Token: 0x17000CE7 RID: 3303
		// (get) Token: 0x0600229F RID: 8863 RVA: 0x0004CF77 File Offset: 0x0004B177
		public static LocalizedString ReportMoveStarted
		{
			get
			{
				return new LocalizedString("ReportMoveStarted", "ExD6C9E1", false, true, MrsStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060022A0 RID: 8864 RVA: 0x0004CF98 File Offset: 0x0004B198
		public static LocalizedString ReportJobResumed(string status)
		{
			return new LocalizedString("ReportJobResumed", "Ex33921C", false, true, MrsStrings.ResourceManager, new object[]
			{
				status
			});
		}

		// Token: 0x060022A1 RID: 8865 RVA: 0x0004CFC8 File Offset: 0x0004B1C8
		public static LocalizedString PickupStatusJobTypeNotSupported(string jobType)
		{
			return new LocalizedString("PickupStatusJobTypeNotSupported", "", false, false, MrsStrings.ResourceManager, new object[]
			{
				jobType
			});
		}

		// Token: 0x060022A2 RID: 8866 RVA: 0x0004CFF8 File Offset: 0x0004B1F8
		public static LocalizedString ValidationMailboxGuidsDontMatch(Guid userMailboxGuid, Guid mrExchangeGuid)
		{
			return new LocalizedString("ValidationMailboxGuidsDontMatch", "ExBA892D", false, true, MrsStrings.ResourceManager, new object[]
			{
				userMailboxGuid,
				mrExchangeGuid
			});
		}

		// Token: 0x060022A3 RID: 8867 RVA: 0x0004D038 File Offset: 0x0004B238
		public static LocalizedString ReportReplaySyncStateCorrupt(Guid requestGuid, int length, string start, string end)
		{
			return new LocalizedString("ReportReplaySyncStateCorrupt", "", false, false, MrsStrings.ResourceManager, new object[]
			{
				requestGuid,
				length,
				start,
				end
			});
		}

		// Token: 0x17000CE8 RID: 3304
		// (get) Token: 0x060022A4 RID: 8868 RVA: 0x0004D07D File Offset: 0x0004B27D
		public static LocalizedString ReportPostMoveCleanupStarted
		{
			get
			{
				return new LocalizedString("ReportPostMoveCleanupStarted", "Ex047582", false, true, MrsStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000CE9 RID: 3305
		// (get) Token: 0x060022A5 RID: 8869 RVA: 0x0004D09B File Offset: 0x0004B29B
		public static LocalizedString InternalAccessFolderCreationIsNotSupported
		{
			get
			{
				return new LocalizedString("InternalAccessFolderCreationIsNotSupported", "", false, false, MrsStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000CEA RID: 3306
		// (get) Token: 0x060022A6 RID: 8870 RVA: 0x0004D0B9 File Offset: 0x0004B2B9
		public static LocalizedString ReportRequestCompleted
		{
			get
			{
				return new LocalizedString("ReportRequestCompleted", "ExDABAF1", false, true, MrsStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060022A7 RID: 8871 RVA: 0x0004D0D8 File Offset: 0x0004B2D8
		public static LocalizedString UnableToReadADUser(string userId)
		{
			return new LocalizedString("UnableToReadADUser", "", false, false, MrsStrings.ResourceManager, new object[]
			{
				userId
			});
		}

		// Token: 0x060022A8 RID: 8872 RVA: 0x0004D108 File Offset: 0x0004B308
		public static LocalizedString UnsupportedRemoteServerVersionWithOperation(string remoteServerAddress, string serverVersion, string operationName)
		{
			return new LocalizedString("UnsupportedRemoteServerVersionWithOperation", "Ex26CC4A", false, true, MrsStrings.ResourceManager, new object[]
			{
				remoteServerAddress,
				serverVersion,
				operationName
			});
		}

		// Token: 0x060022A9 RID: 8873 RVA: 0x0004D140 File Offset: 0x0004B340
		public static LocalizedString JobHasBeenRelinquishedDueToResourceReservation(LocalizedString error)
		{
			return new LocalizedString("JobHasBeenRelinquishedDueToResourceReservation", "", false, false, MrsStrings.ResourceManager, new object[]
			{
				error
			});
		}

		// Token: 0x060022AA RID: 8874 RVA: 0x0004D174 File Offset: 0x0004B374
		public static LocalizedString ReportUnableToComputeTargetAddress(string targetDeliveryDomain, string primarySmtpAddress)
		{
			return new LocalizedString("ReportUnableToComputeTargetAddress", "Ex50BD17", false, true, MrsStrings.ResourceManager, new object[]
			{
				targetDeliveryDomain,
				primarySmtpAddress
			});
		}

		// Token: 0x060022AB RID: 8875 RVA: 0x0004D1A8 File Offset: 0x0004B3A8
		public static LocalizedString ReportSourceMailUserAfterFinalization2(string userID, string domainControllerName)
		{
			return new LocalizedString("ReportSourceMailUserAfterFinalization2", "", false, false, MrsStrings.ResourceManager, new object[]
			{
				userID,
				domainControllerName
			});
		}

		// Token: 0x060022AC RID: 8876 RVA: 0x0004D1DC File Offset: 0x0004B3DC
		public static LocalizedString RecipientMissingLegDN(string recipient)
		{
			return new LocalizedString("RecipientMissingLegDN", "Ex259BEB", false, true, MrsStrings.ResourceManager, new object[]
			{
				recipient
			});
		}

		// Token: 0x060022AD RID: 8877 RVA: 0x0004D20C File Offset: 0x0004B40C
		public static LocalizedString ReportIcsSyncStateCorrupt(Guid requestGuid, int length, string start, string end)
		{
			return new LocalizedString("ReportIcsSyncStateCorrupt", "", false, false, MrsStrings.ResourceManager, new object[]
			{
				requestGuid,
				length,
				start,
				end
			});
		}

		// Token: 0x060022AE RID: 8878 RVA: 0x0004D254 File Offset: 0x0004B454
		public static LocalizedString EasFolderUpdateFailed(string errorMessage)
		{
			return new LocalizedString("EasFolderUpdateFailed", "", false, false, MrsStrings.ResourceManager, new object[]
			{
				errorMessage
			});
		}

		// Token: 0x060022AF RID: 8879 RVA: 0x0004D284 File Offset: 0x0004B484
		public static LocalizedString UnableToUpdateSourceMailbox(LocalizedString errorMsg)
		{
			return new LocalizedString("UnableToUpdateSourceMailbox", "ExE23BCC", false, true, MrsStrings.ResourceManager, new object[]
			{
				errorMsg
			});
		}

		// Token: 0x060022B0 RID: 8880 RVA: 0x0004D2B8 File Offset: 0x0004B4B8
		public static LocalizedString DestinationMailboxResetNotGuaranteed(LocalizedString errorMsg)
		{
			return new LocalizedString("DestinationMailboxResetNotGuaranteed", "", false, false, MrsStrings.ResourceManager, new object[]
			{
				errorMsg
			});
		}

		// Token: 0x17000CEB RID: 3307
		// (get) Token: 0x060022B1 RID: 8881 RVA: 0x0004D2EC File Offset: 0x0004B4EC
		public static LocalizedString ReadCpu
		{
			get
			{
				return new LocalizedString("ReadCpu", "", false, false, MrsStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060022B2 RID: 8882 RVA: 0x0004D30C File Offset: 0x0004B50C
		public static LocalizedString ReportMailboxArchiveInfoAfterMoveLoc(LocalizedString mailboxInfoString)
		{
			return new LocalizedString("ReportMailboxArchiveInfoAfterMoveLoc", "Ex7DFF8C", false, true, MrsStrings.ResourceManager, new object[]
			{
				mailboxInfoString
			});
		}

		// Token: 0x060022B3 RID: 8883 RVA: 0x0004D340 File Offset: 0x0004B540
		public static LocalizedString UnableToClosePST(string filePath)
		{
			return new LocalizedString("UnableToClosePST", "Ex423CAC", false, true, MrsStrings.ResourceManager, new object[]
			{
				filePath
			});
		}

		// Token: 0x060022B4 RID: 8884 RVA: 0x0004D370 File Offset: 0x0004B570
		public static LocalizedString ReportRequestProcessedByAnotherMRS(string mrsName)
		{
			return new LocalizedString("ReportRequestProcessedByAnotherMRS", "Ex2DE801", false, true, MrsStrings.ResourceManager, new object[]
			{
				mrsName
			});
		}

		// Token: 0x17000CEC RID: 3308
		// (get) Token: 0x060022B5 RID: 8885 RVA: 0x0004D39F File Offset: 0x0004B59F
		public static LocalizedString TargetContainer
		{
			get
			{
				return new LocalizedString("TargetContainer", "", false, false, MrsStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060022B6 RID: 8886 RVA: 0x0004D3C0 File Offset: 0x0004B5C0
		public static LocalizedString JobIsStalledAndFailed(LocalizedString failureReason, int agentId)
		{
			return new LocalizedString("JobIsStalledAndFailed", "Ex5DA074", false, true, MrsStrings.ResourceManager, new object[]
			{
				failureReason,
				agentId
			});
		}

		// Token: 0x060022B7 RID: 8887 RVA: 0x0004D400 File Offset: 0x0004B600
		public static LocalizedString ReportSyncStateSaveFailed(string errorType, LocalizedString errorMsg, string trace)
		{
			return new LocalizedString("ReportSyncStateSaveFailed", "Ex2D4B71", false, true, MrsStrings.ResourceManager, new object[]
			{
				errorType,
				errorMsg,
				trace
			});
		}

		// Token: 0x17000CED RID: 3309
		// (get) Token: 0x060022B8 RID: 8888 RVA: 0x0004D43C File Offset: 0x0004B63C
		public static LocalizedString RequestPriorityLowest
		{
			get
			{
				return new LocalizedString("RequestPriorityLowest", "", false, false, MrsStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060022B9 RID: 8889 RVA: 0x0004D45C File Offset: 0x0004B65C
		public static LocalizedString ErrorMoveInProgress(string resourceName, string clientName)
		{
			return new LocalizedString("ErrorMoveInProgress", "", false, false, MrsStrings.ResourceManager, new object[]
			{
				resourceName,
				clientName
			});
		}

		// Token: 0x060022BA RID: 8890 RVA: 0x0004D490 File Offset: 0x0004B690
		public static LocalizedString ParsingMessageEntryIdFailed(string messageEntryId)
		{
			return new LocalizedString("ParsingMessageEntryIdFailed", "", false, false, MrsStrings.ResourceManager, new object[]
			{
				messageEntryId
			});
		}

		// Token: 0x17000CEE RID: 3310
		// (get) Token: 0x060022BB RID: 8891 RVA: 0x0004D4BF File Offset: 0x0004B6BF
		public static LocalizedString WorkloadTypeNone
		{
			get
			{
				return new LocalizedString("WorkloadTypeNone", "", false, false, MrsStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060022BC RID: 8892 RVA: 0x0004D4E0 File Offset: 0x0004B6E0
		public static LocalizedString ReportInitializingJob(string serverName, string serverVersion)
		{
			return new LocalizedString("ReportInitializingJob", "Ex7E8DA9", false, true, MrsStrings.ResourceManager, new object[]
			{
				serverName,
				serverVersion
			});
		}

		// Token: 0x17000CEF RID: 3311
		// (get) Token: 0x060022BD RID: 8893 RVA: 0x0004D513 File Offset: 0x0004B713
		public static LocalizedString UnableToObtainServersInLocalSite
		{
			get
			{
				return new LocalizedString("UnableToObtainServersInLocalSite", "", false, false, MrsStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000CF0 RID: 3312
		// (get) Token: 0x060022BE RID: 8894 RVA: 0x0004D531 File Offset: 0x0004B731
		public static LocalizedString WrongUserObjectFound
		{
			get
			{
				return new LocalizedString("WrongUserObjectFound", "Ex5D823D", false, true, MrsStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060022BF RID: 8895 RVA: 0x0004D550 File Offset: 0x0004B750
		public static LocalizedString MoveHasBeenRelinquishedDueToProxyThrottling(Guid mbxGuid, DateTime pickupTime)
		{
			return new LocalizedString("MoveHasBeenRelinquishedDueToProxyThrottling", "Ex35A6DA", false, true, MrsStrings.ResourceManager, new object[]
			{
				mbxGuid,
				pickupTime
			});
		}

		// Token: 0x060022C0 RID: 8896 RVA: 0x0004D590 File Offset: 0x0004B790
		public static LocalizedString RpcClientAccessServerNotConfiguredForMdb(string mdbID)
		{
			return new LocalizedString("RpcClientAccessServerNotConfiguredForMdb", "Ex5B0815", false, true, MrsStrings.ResourceManager, new object[]
			{
				mdbID
			});
		}

		// Token: 0x17000CF1 RID: 3313
		// (get) Token: 0x060022C1 RID: 8897 RVA: 0x0004D5BF File Offset: 0x0004B7BF
		public static LocalizedString GetIdsFromNamesCalledOnDestination
		{
			get
			{
				return new LocalizedString("GetIdsFromNamesCalledOnDestination", "", false, false, MrsStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060022C2 RID: 8898 RVA: 0x0004D5E0 File Offset: 0x0004B7E0
		public static LocalizedString RemoteMailboxServerInformation(string serverName, string serverVersion, string proxyServerName, string proxyServerVersion)
		{
			return new LocalizedString("RemoteMailboxServerInformation", "Ex18ED52", false, true, MrsStrings.ResourceManager, new object[]
			{
				serverName,
				serverVersion,
				proxyServerName,
				proxyServerVersion
			});
		}

		// Token: 0x060022C3 RID: 8899 RVA: 0x0004D61C File Offset: 0x0004B81C
		public static LocalizedString MoveReportEntryMessage(string timestamp, string serverName, LocalizedString message)
		{
			return new LocalizedString("MoveReportEntryMessage", "Ex95F02C", false, true, MrsStrings.ResourceManager, new object[]
			{
				timestamp,
				serverName,
				message
			});
		}

		// Token: 0x060022C4 RID: 8900 RVA: 0x0004D658 File Offset: 0x0004B858
		public static LocalizedString ReportDestinationMailboxResetFailed2(string errorType, LocalizedString errorMsg, string trace, int currentAttempt, int totalAttempts)
		{
			return new LocalizedString("ReportDestinationMailboxResetFailed2", "Ex09A66C", false, true, MrsStrings.ResourceManager, new object[]
			{
				errorType,
				errorMsg,
				trace,
				currentAttempt,
				totalAttempts
			});
		}

		// Token: 0x060022C5 RID: 8901 RVA: 0x0004D6A8 File Offset: 0x0004B8A8
		public static LocalizedString BadItemFolderProperty(string folderName)
		{
			return new LocalizedString("BadItemFolderProperty", "", false, false, MrsStrings.ResourceManager, new object[]
			{
				folderName
			});
		}

		// Token: 0x060022C6 RID: 8902 RVA: 0x0004D6D8 File Offset: 0x0004B8D8
		public static LocalizedString EasObjectNotFound(string entryId)
		{
			return new LocalizedString("EasObjectNotFound", "", false, false, MrsStrings.ResourceManager, new object[]
			{
				entryId
			});
		}

		// Token: 0x060022C7 RID: 8903 RVA: 0x0004D708 File Offset: 0x0004B908
		public static LocalizedString MaxSubmissionExceeded(string messageSizeStr, LocalizedString errorMsg)
		{
			return new LocalizedString("MaxSubmissionExceeded", "ExCAE458", false, true, MrsStrings.ResourceManager, new object[]
			{
				messageSizeStr,
				errorMsg
			});
		}

		// Token: 0x17000CF2 RID: 3314
		// (get) Token: 0x060022C8 RID: 8904 RVA: 0x0004D740 File Offset: 0x0004B940
		public static LocalizedString DataExportCanceled
		{
			get
			{
				return new LocalizedString("DataExportCanceled", "Ex22658E", false, true, MrsStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060022C9 RID: 8905 RVA: 0x0004D760 File Offset: 0x0004B960
		public static LocalizedString UnsupportedImapMessageEntryIdVersion(string version)
		{
			return new LocalizedString("UnsupportedImapMessageEntryIdVersion", "", false, false, MrsStrings.ResourceManager, new object[]
			{
				version
			});
		}

		// Token: 0x060022CA RID: 8906 RVA: 0x0004D790 File Offset: 0x0004B990
		public static LocalizedString ExceptionWithStack(LocalizedString exceptionMessage, string stackTrace)
		{
			return new LocalizedString("ExceptionWithStack", "Ex8E18E1", false, true, MrsStrings.ResourceManager, new object[]
			{
				exceptionMessage,
				stackTrace
			});
		}

		// Token: 0x060022CB RID: 8907 RVA: 0x0004D7C8 File Offset: 0x0004B9C8
		public static LocalizedString ReportDestinationMailboxSeedMBICacheFailed(string errorType, LocalizedString errorMsg, string trace, int currentAttempt, int totalAttempts)
		{
			return new LocalizedString("ReportDestinationMailboxSeedMBICacheFailed", "ExF078BD", false, true, MrsStrings.ResourceManager, new object[]
			{
				errorType,
				errorMsg,
				trace,
				currentAttempt,
				totalAttempts
			});
		}

		// Token: 0x17000CF3 RID: 3315
		// (get) Token: 0x060022CC RID: 8908 RVA: 0x0004D817 File Offset: 0x0004BA17
		public static LocalizedString TooManyBadItems
		{
			get
			{
				return new LocalizedString("TooManyBadItems", "ExCBFC85", false, true, MrsStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000CF4 RID: 3316
		// (get) Token: 0x060022CD RID: 8909 RVA: 0x0004D835 File Offset: 0x0004BA35
		public static LocalizedString ReportVerifyingMailboxContents
		{
			get
			{
				return new LocalizedString("ReportVerifyingMailboxContents", "", false, false, MrsStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060022CE RID: 8910 RVA: 0x0004D854 File Offset: 0x0004BA54
		public static LocalizedString BadItemFolderACL(string folderName)
		{
			return new LocalizedString("BadItemFolderACL", "", false, false, MrsStrings.ResourceManager, new object[]
			{
				folderName
			});
		}

		// Token: 0x060022CF RID: 8911 RVA: 0x0004D884 File Offset: 0x0004BA84
		public static LocalizedString CannotCreateMessageId(long uid, string folderName)
		{
			return new LocalizedString("CannotCreateMessageId", "", false, false, MrsStrings.ResourceManager, new object[]
			{
				uid,
				folderName
			});
		}

		// Token: 0x060022D0 RID: 8912 RVA: 0x0004D8BC File Offset: 0x0004BABC
		public static LocalizedString ReportRestartingMoveBecauseMailboxSignatureVersionIsDifferent(LocalizedString mbxId, uint originalVersion, uint currentVersion)
		{
			return new LocalizedString("ReportRestartingMoveBecauseMailboxSignatureVersionIsDifferent", "", false, false, MrsStrings.ResourceManager, new object[]
			{
				mbxId,
				originalVersion,
				currentVersion
			});
		}

		// Token: 0x060022D1 RID: 8913 RVA: 0x0004D902 File Offset: 0x0004BB02
		public static LocalizedString GetLocalizedString(MrsStrings.IDs key)
		{
			return new LocalizedString(MrsStrings.stringIDs[(uint)key], MrsStrings.ResourceManager, new object[0]);
		}

		// Token: 0x04000D15 RID: 3349
		private static Dictionary<uint, string> stringIDs = new Dictionary<uint, string>(186);

		// Token: 0x04000D16 RID: 3350
		private static ExchangeResourceManager ResourceManager = ExchangeResourceManager.GetResourceManager("Microsoft.Exchange.MailboxReplicationService.Strings", typeof(MrsStrings).GetTypeInfo().Assembly);

		// Token: 0x0200029A RID: 666
		public enum IDs : uint
		{
			// Token: 0x04000D18 RID: 3352
			InvalidRequestJob = 1424238063U,
			// Token: 0x04000D19 RID: 3353
			WorkloadTypeOnboarding = 392888166U,
			// Token: 0x04000D1A RID: 3354
			ReportMessagesCopied = 3088101088U,
			// Token: 0x04000D1B RID: 3355
			DestMailboxAlreadyBeingMoved = 3176193428U,
			// Token: 0x04000D1C RID: 3356
			ReportDestinationSDCannotBeRead = 3671912657U,
			// Token: 0x04000D1D RID: 3357
			ServiceIsStopping = 3429098023U,
			// Token: 0x04000D1E RID: 3358
			PSTPathMustBeAFile = 1833949493U,
			// Token: 0x04000D1F RID: 3359
			ReportMovedMailboxAlreadyMorphedToMailUser = 3825549875U,
			// Token: 0x04000D20 RID: 3360
			UnableToReadAD = 3369317625U,
			// Token: 0x04000D21 RID: 3361
			MoveRestartDueToContainerMailboxesChanged = 1476056932U,
			// Token: 0x04000D22 RID: 3362
			ReportCopyPerUserReadUnreadDataStarted = 3728110147U,
			// Token: 0x04000D23 RID: 3363
			JobCannotBeRehomedWhenInProgress = 1704413971U,
			// Token: 0x04000D24 RID: 3364
			FolderHierarchyIsInconsistent = 2750851982U,
			// Token: 0x04000D25 RID: 3365
			ReportMoveRestartedDueToSignatureChange = 3945110826U,
			// Token: 0x04000D26 RID: 3366
			ErrorCannotPreventCompletionForCompletingMove = 3089757325U,
			// Token: 0x04000D27 RID: 3367
			WorkloadTypeOffboarding = 4185133072U,
			// Token: 0x04000D28 RID: 3368
			MRSAlreadyConfigured = 979363606U,
			// Token: 0x04000D29 RID: 3369
			ReportTargetPublicFolderDeploymentUnlocked = 135363966U,
			// Token: 0x04000D2A RID: 3370
			ReportRequestCancelPostponed = 815599557U,
			// Token: 0x04000D2B RID: 3371
			JobHasBeenRelinquishedDueToHAStall = 1388232024U,
			// Token: 0x04000D2C RID: 3372
			RequestPriorityLow = 738613247U,
			// Token: 0x04000D2D RID: 3373
			MoveRequestDirectionPull = 1580421706U,
			// Token: 0x04000D2E RID: 3374
			UnableToApplyFolderHierarchyChanges = 119144040U,
			// Token: 0x04000D2F RID: 3375
			PickupStatusDisabled = 3837251784U,
			// Token: 0x04000D30 RID: 3376
			RemoteResource = 2288253100U,
			// Token: 0x04000D31 RID: 3377
			MoveRestartedDueToSignatureChange = 2018114904U,
			// Token: 0x04000D32 RID: 3378
			FolderHierarchyContainsNoRoots = 3406658852U,
			// Token: 0x04000D33 RID: 3379
			JobHasBeenRelinquishedDueToCIStall = 2360217149U,
			// Token: 0x04000D34 RID: 3380
			ContentIndexing = 2713483251U,
			// Token: 0x04000D35 RID: 3381
			TooManyLargeItems = 3235245142U,
			// Token: 0x04000D36 RID: 3382
			CouldNotConnectToTargetMailbox = 2888971502U,
			// Token: 0x04000D37 RID: 3383
			PSTIOException = 1886860910U,
			// Token: 0x04000D38 RID: 3384
			RequestPriorityNormal = 2240969000U,
			// Token: 0x04000D39 RID: 3385
			SmtpServerInfoMissing = 268267125U,
			// Token: 0x04000D3A RID: 3386
			NoPublicFolderMailboxFoundInSource = 4282140830U,
			// Token: 0x04000D3B RID: 3387
			WorkloadTypeRemotePstExport = 3551669574U,
			// Token: 0x04000D3C RID: 3388
			FastTransferArgumentError = 3345143506U,
			// Token: 0x04000D3D RID: 3389
			PickupStatusCompletedJob = 3435030616U,
			// Token: 0x04000D3E RID: 3390
			ReportJobProcessingDisabled = 856796896U,
			// Token: 0x04000D3F RID: 3391
			ImproperTypeForThisIdParameter = 1247936583U,
			// Token: 0x04000D40 RID: 3392
			MoveRequestMissingInfoDelete = 2006276195U,
			// Token: 0x04000D41 RID: 3393
			ReportRelinquishingJobDueToServiceStop = 3798869933U,
			// Token: 0x04000D42 RID: 3394
			PickupStatusCorruptJob = 1827127136U,
			// Token: 0x04000D43 RID: 3395
			RequestHasBeenRelinquishedDueToBadHealthOfBackendServers = 4161853231U,
			// Token: 0x04000D44 RID: 3396
			MoveRequestMissingInfoSave = 2260777903U,
			// Token: 0x04000D45 RID: 3397
			RestartingMove = 3708951374U,
			// Token: 0x04000D46 RID: 3398
			ErrorWhileUpdatingMovedMailbox = 3631041786U,
			// Token: 0x04000D47 RID: 3399
			MoveRequestValidationFailed = 1320326778U,
			// Token: 0x04000D48 RID: 3400
			MustProvideValidSessionForFindingRequests = 199165212U,
			// Token: 0x04000D49 RID: 3401
			TooManyMissingItems = 4003924173U,
			// Token: 0x04000D4A RID: 3402
			UpdateFolderFailed = 4230582602U,
			// Token: 0x04000D4B RID: 3403
			OfflinePublicFolderMigrationNotSupported = 990473213U,
			// Token: 0x04000D4C RID: 3404
			TaskCanceled = 2906979272U,
			// Token: 0x04000D4D RID: 3405
			SourceMailboxAlreadyBeingMoved = 2323682581U,
			// Token: 0x04000D4E RID: 3406
			MoveJobDeserializationFailed = 938866594U,
			// Token: 0x04000D4F RID: 3407
			MoveRequestNotFoundInQueue = 3959740005U,
			// Token: 0x04000D50 RID: 3408
			JobHasBeenCanceled = 2697508630U,
			// Token: 0x04000D51 RID: 3409
			ReportRequestStarted = 1085885350U,
			// Token: 0x04000D52 RID: 3410
			ErrorDownlevelClientsNotSupported = 90093709U,
			// Token: 0x04000D53 RID: 3411
			DataExportTimeout = 2295784267U,
			// Token: 0x04000D54 RID: 3412
			TargetMailboxConnectionWasLost = 2935411904U,
			// Token: 0x04000D55 RID: 3413
			JobHasBeenRelinquishedDueToDatabaseFailover = 2247143358U,
			// Token: 0x04000D56 RID: 3414
			PublicFolderMailboxesNotProvisionedForMigration = 3942653727U,
			// Token: 0x04000D57 RID: 3415
			RequestPriorityHigher = 3540791304U,
			// Token: 0x04000D58 RID: 3416
			JobHasBeenRelinquishedDueToHAOrCIStalls = 1147283780U,
			// Token: 0x04000D59 RID: 3417
			ReportRequestCanceled = 1573145718U,
			// Token: 0x04000D5A RID: 3418
			InvalidProxyOperationOrder = 4082292636U,
			// Token: 0x04000D5B RID: 3419
			ReportRequestOfflineMovePostponed = 2286820319U,
			// Token: 0x04000D5C RID: 3420
			MailboxIsBeingMoved = 3759213604U,
			// Token: 0x04000D5D RID: 3421
			NoSuchRequestInSpecifiedIndex = 3604568438U,
			// Token: 0x04000D5E RID: 3422
			InitializedWithInvalidObjectId = 859003787U,
			// Token: 0x04000D5F RID: 3423
			ReportCopyPerUserReadUnreadDataCompleted = 215436927U,
			// Token: 0x04000D60 RID: 3424
			ReportSessionStatisticsUpdated = 699419898U,
			// Token: 0x04000D61 RID: 3425
			ReportRelinquishingJobDueToServerThrottling = 2708825588U,
			// Token: 0x04000D62 RID: 3426
			MRSNotConfigured = 3162969353U,
			// Token: 0x04000D63 RID: 3427
			MailboxRootFolderNotFound = 2244475911U,
			// Token: 0x04000D64 RID: 3428
			WorkloadTypeLoadBalancing = 540485114U,
			// Token: 0x04000D65 RID: 3429
			JobIsQuarantined = 1035843159U,
			// Token: 0x04000D66 RID: 3430
			ReportSourceSDCannotBeRead = 2538826952U,
			// Token: 0x04000D67 RID: 3431
			ReportMoveRequestIsNoLongerSticky = 2850410499U,
			// Token: 0x04000D68 RID: 3432
			ClusterNotFound = 1606949857U,
			// Token: 0x04000D69 RID: 3433
			MoveRestartDueToIsIntegCheck = 833630790U,
			// Token: 0x04000D6A RID: 3434
			ReportJobIsStillStalled = 952727642U,
			// Token: 0x04000D6B RID: 3435
			WorkloadTypeRemotePstIngestion = 4023361926U,
			// Token: 0x04000D6C RID: 3436
			ReportPrimaryMservEntryPointsToExo = 472899259U,
			// Token: 0x04000D6D RID: 3437
			ValidationADUserIsNotBeingMoved = 2750716986U,
			// Token: 0x04000D6E RID: 3438
			PostMoveStateIsUncertain = 702077469U,
			// Token: 0x04000D6F RID: 3439
			RequestPriorityHigh = 901032809U,
			// Token: 0x04000D70 RID: 3440
			SourceContainer = 888446530U,
			// Token: 0x04000D71 RID: 3441
			WorkloadTypeTenantUpgrade = 3595573419U,
			// Token: 0x04000D72 RID: 3442
			EasMissingMessageCategory = 3655041298U,
			// Token: 0x04000D73 RID: 3443
			JobHasBeenRelinquished = 336070114U,
			// Token: 0x04000D74 RID: 3444
			RecoverySyncNotImplemented = 2930966715U,
			// Token: 0x04000D75 RID: 3445
			ErrorTooManyCleanupRetries = 1123448769U,
			// Token: 0x04000D76 RID: 3446
			ReportFinalSyncStarted = 3238364772U,
			// Token: 0x04000D77 RID: 3447
			ReportJobExitedStalledByThrottlingState = 1078342972U,
			// Token: 0x04000D78 RID: 3448
			MustProvideNonEmptyStringForIdentity = 1302133406U,
			// Token: 0x04000D79 RID: 3449
			ReportRelinquishingJobDueToNeedForRehome = 2181380613U,
			// Token: 0x04000D7A RID: 3450
			NotEnoughInformationSupplied = 2761303157U,
			// Token: 0x04000D7B RID: 3451
			NoDataContext = 4239850164U,
			// Token: 0x04000D7C RID: 3452
			ReportMoveCompleted = 2023474786U,
			// Token: 0x04000D7D RID: 3453
			UnableToDeleteMoveRequestMessage = 4145210966U,
			// Token: 0x04000D7E RID: 3454
			DestinationFolderHierarchyInconsistent = 4239846426U,
			// Token: 0x04000D7F RID: 3455
			NotEnoughInformationToFindMoveRequest = 4095394241U,
			// Token: 0x04000D80 RID: 3456
			TaskSchedulerStopped = 949547255U,
			// Token: 0x04000D81 RID: 3457
			ReportRelinquishingJobDueToCIStall = 1292011054U,
			// Token: 0x04000D82 RID: 3458
			WriteCpu = 45208631U,
			// Token: 0x04000D83 RID: 3459
			ReportHomeMdbPointsToTarget = 73509179U,
			// Token: 0x04000D84 RID: 3460
			CorruptRestrictionData = 672463457U,
			// Token: 0x04000D85 RID: 3461
			ReportIncrementalMoveRestartDueToGlobalCounterRangeDepletion = 1194778775U,
			// Token: 0x04000D86 RID: 3462
			ReadRpc = 3109918443U,
			// Token: 0x04000D87 RID: 3463
			WorkloadTypeLocal = 1212084898U,
			// Token: 0x04000D88 RID: 3464
			MoveRequestDirectionPush = 3906020551U,
			// Token: 0x04000D89 RID: 3465
			SourceFolderHierarchyInconsistent = 3213196515U,
			// Token: 0x04000D8A RID: 3466
			RequestPriorityHighest = 2170397003U,
			// Token: 0x04000D8B RID: 3467
			ErrorFinalizationIsBlocked = 1787602764U,
			// Token: 0x04000D8C RID: 3468
			MoveRequestTypeCrossOrg = 1410610418U,
			// Token: 0x04000D8D RID: 3469
			MrsProxyServiceIsDisabled = 1554654695U,
			// Token: 0x04000D8E RID: 3470
			ReportMoveCanceled = 2497488330U,
			// Token: 0x04000D8F RID: 3471
			ErrorCannotPreventCompletionForOfflineMove = 1153630962U,
			// Token: 0x04000D90 RID: 3472
			RequestHasBeenPostponedDueToBadHealthOfBackendServers2 = 3994387752U,
			// Token: 0x04000D91 RID: 3473
			ValidationNoCorrespondingIndexEntries = 1608330969U,
			// Token: 0x04000D92 RID: 3474
			InvalidSyncStateData = 1728063749U,
			// Token: 0x04000D93 RID: 3475
			ReportMoveRestartedDueToSourceCorruption = 874970588U,
			// Token: 0x04000D94 RID: 3476
			JobHasBeenAutoSuspended = 3603681089U,
			// Token: 0x04000D95 RID: 3477
			InputDataIsInvalid = 3843865613U,
			// Token: 0x04000D96 RID: 3478
			ReportJobExitedStalledState = 3287770946U,
			// Token: 0x04000D97 RID: 3479
			ActionNotSupported = 1094055921U,
			// Token: 0x04000D98 RID: 3480
			ReportTargetAuxFolderContentMailboxGuidUpdated = 1690707778U,
			// Token: 0x04000D99 RID: 3481
			ReportStoreMailboxHasFinalized = 1049832713U,
			// Token: 0x04000D9A RID: 3482
			ErrorReservationExpired = 1157208427U,
			// Token: 0x04000D9B RID: 3483
			ErrorImplicitSplit = 3856642209U,
			// Token: 0x04000D9C RID: 3484
			ReportRelinquishingJob = 1609645991U,
			// Token: 0x04000D9D RID: 3485
			CouldNotConnectToSourceMailbox = 2756637510U,
			// Token: 0x04000D9E RID: 3486
			NoFoldersIncluded = 2671521794U,
			// Token: 0x04000D9F RID: 3487
			ReportSuspendingJob = 780406631U,
			// Token: 0x04000DA0 RID: 3488
			WriteRpc = 1228469352U,
			// Token: 0x04000DA1 RID: 3489
			NotConnected = 2309811282U,
			// Token: 0x04000DA2 RID: 3490
			MdbReplication = 4081437335U,
			// Token: 0x04000DA3 RID: 3491
			ReportRulesWillNotBeCopied = 1499253095U,
			// Token: 0x04000DA4 RID: 3492
			ReportSkippingUpdateSourceMailbox = 772970447U,
			// Token: 0x04000DA5 RID: 3493
			ErrorEmptyMailboxGuid = 2051604558U,
			// Token: 0x04000DA6 RID: 3494
			ReportArchiveAlreadyUpdated = 4227707103U,
			// Token: 0x04000DA7 RID: 3495
			JobHasBeenSynced = 3167108423U,
			// Token: 0x04000DA8 RID: 3496
			JobHasBeenRelinquishedDueToLongRun = 300694364U,
			// Token: 0x04000DA9 RID: 3497
			ReportRelinquishingJobDueToHAOrCIStalling = 2739554404U,
			// Token: 0x04000DAA RID: 3498
			Mailbox = 3709264734U,
			// Token: 0x04000DAB RID: 3499
			FolderHierarchyIsInconsistentTemporarily = 2181899002U,
			// Token: 0x04000DAC RID: 3500
			RequestPriorityEmergency = 1729678064U,
			// Token: 0x04000DAD RID: 3501
			ReportRelinquishingJobDueToHAStall = 3018124355U,
			// Token: 0x04000DAE RID: 3502
			CorruptSyncState = 2466192281U,
			// Token: 0x04000DAF RID: 3503
			ReportTargetPublicFolderContentMailboxGuidUpdated = 1574793031U,
			// Token: 0x04000DB0 RID: 3504
			NoMRSAvailable = 551973766U,
			// Token: 0x04000DB1 RID: 3505
			RequestPriorityLower = 3329536416U,
			// Token: 0x04000DB2 RID: 3506
			ReportAutoSuspendingJob = 2648214014U,
			// Token: 0x04000DB3 RID: 3507
			ReportCalendarFolderFaiSaveFailed = 123753966U,
			// Token: 0x04000DB4 RID: 3508
			MoveIsPreventedFromFinalization = 2000650826U,
			// Token: 0x04000DB5 RID: 3509
			ReportMoveAlreadyFinished = 615074259U,
			// Token: 0x04000DB6 RID: 3510
			RehomeRequestFailure = 3151307821U,
			// Token: 0x04000DB7 RID: 3511
			RequestIsStalledByHigherPriorityJobs = 2161771148U,
			// Token: 0x04000DB8 RID: 3512
			WorkloadTypeEmergency = 951459652U,
			// Token: 0x04000DB9 RID: 3513
			ReportCalendarFolderSaveFailed = 1237434822U,
			// Token: 0x04000DBA RID: 3514
			MRSProxyConnectionNotThrottledError = 2993893239U,
			// Token: 0x04000DBB RID: 3515
			ReportWaitingForMailboxDataReplication = 3133742332U,
			// Token: 0x04000DBC RID: 3516
			ReportDatabaseFailedOver = 1628850222U,
			// Token: 0x04000DBD RID: 3517
			FolderIsMissingInMerge = 1268966663U,
			// Token: 0x04000DBE RID: 3518
			Unknown = 2846264340U,
			// Token: 0x04000DBF RID: 3519
			WorkloadTypeSyncAggregation = 681951690U,
			// Token: 0x04000DC0 RID: 3520
			ReportTargetUserIsNotMailEnabledUser = 1347847794U,
			// Token: 0x04000DC1 RID: 3521
			ReportRequestIsNoLongerSticky = 676706684U,
			// Token: 0x04000DC2 RID: 3522
			MoveRequestTypeIntraOrg = 3237229570U,
			// Token: 0x04000DC3 RID: 3523
			ValidationMoveRequestNotDeserialized = 3107299645U,
			// Token: 0x04000DC4 RID: 3524
			ReportMoveStarted = 2605163142U,
			// Token: 0x04000DC5 RID: 3525
			ReportPostMoveCleanupStarted = 1655277830U,
			// Token: 0x04000DC6 RID: 3526
			InternalAccessFolderCreationIsNotSupported = 704248557U,
			// Token: 0x04000DC7 RID: 3527
			ReportRequestCompleted = 1464484042U,
			// Token: 0x04000DC8 RID: 3528
			ReadCpu = 3109918916U,
			// Token: 0x04000DC9 RID: 3529
			TargetContainer = 202150486U,
			// Token: 0x04000DCA RID: 3530
			RequestPriorityLowest = 4026185433U,
			// Token: 0x04000DCB RID: 3531
			WorkloadTypeNone = 630728405U,
			// Token: 0x04000DCC RID: 3532
			UnableToObtainServersInLocalSite = 4156705784U,
			// Token: 0x04000DCD RID: 3533
			WrongUserObjectFound = 3403189953U,
			// Token: 0x04000DCE RID: 3534
			GetIdsFromNamesCalledOnDestination = 3829274068U,
			// Token: 0x04000DCF RID: 3535
			DataExportCanceled = 1551771611U,
			// Token: 0x04000DD0 RID: 3536
			TooManyBadItems = 3775277574U,
			// Token: 0x04000DD1 RID: 3537
			ReportVerifyingMailboxContents = 1220106367U
		}

		// Token: 0x0200029B RID: 667
		private enum ParamIDs
		{
			// Token: 0x04000DD3 RID: 3539
			EasFolderSyncFailed,
			// Token: 0x04000DD4 RID: 3540
			UnableToOpenMailbox,
			// Token: 0x04000DD5 RID: 3541
			ServerError,
			// Token: 0x04000DD6 RID: 3542
			ReportSyncStateNull,
			// Token: 0x04000DD7 RID: 3543
			RecipientArchiveGuidMismatch,
			// Token: 0x04000DD8 RID: 3544
			ReportDestinationMailboxResetSucceeded,
			// Token: 0x04000DD9 RID: 3545
			ReportMovedMailboxUpdated,
			// Token: 0x04000DDA RID: 3546
			SystemMailboxNotFound,
			// Token: 0x04000DDB RID: 3547
			ReportRemovingTargetMailboxDueToOfflineMoveFailure,
			// Token: 0x04000DDC RID: 3548
			TimeoutError,
			// Token: 0x04000DDD RID: 3549
			ReportMailboxBeforeFinalization2,
			// Token: 0x04000DDE RID: 3550
			MoveHasBeenSynced,
			// Token: 0x04000DDF RID: 3551
			MoveCancelFailedForAlreadyCompletedMove,
			// Token: 0x04000DE0 RID: 3552
			RPCHTTPPublicFoldersId,
			// Token: 0x04000DE1 RID: 3553
			ReportThrottles,
			// Token: 0x04000DE2 RID: 3554
			ReportMailboxInfoAfterMove,
			// Token: 0x04000DE3 RID: 3555
			ReportSourceMailboxBeforeFinalization2,
			// Token: 0x04000DE4 RID: 3556
			RulesDataContext,
			// Token: 0x04000DE5 RID: 3557
			UnableToGetPSTProps,
			// Token: 0x04000DE6 RID: 3558
			ReportFailedToDisconnectFromSource2,
			// Token: 0x04000DE7 RID: 3559
			ReportJobHasBeenRelinquishedDueToServerBusy,
			// Token: 0x04000DE8 RID: 3560
			ReportDestinationMailboxClearSyncStateFailed,
			// Token: 0x04000DE9 RID: 3561
			MoveCompleteFailedForAlreadyFailedMove,
			// Token: 0x04000DEA RID: 3562
			DatabaseCouldNotBeMapped,
			// Token: 0x04000DEB RID: 3563
			ReportCopyFolderPropertyProgress,
			// Token: 0x04000DEC RID: 3564
			ReportUnableToLoadDestinationUser,
			// Token: 0x04000DED RID: 3565
			NotImplemented,
			// Token: 0x04000DEE RID: 3566
			ReportFolderCreationProgress,
			// Token: 0x04000DEF RID: 3567
			PropValuesDataContext,
			// Token: 0x04000DF0 RID: 3568
			ReportMailboxInfoBeforeMoveLoc,
			// Token: 0x04000DF1 RID: 3569
			ReportMoveRequestCreated,
			// Token: 0x04000DF2 RID: 3570
			PickupStatusRequestTypeNotSupported,
			// Token: 0x04000DF3 RID: 3571
			ReportRequestSaveFailed,
			// Token: 0x04000DF4 RID: 3572
			MoveRequestDataMissing,
			// Token: 0x04000DF5 RID: 3573
			FolderDataContextSearch,
			// Token: 0x04000DF6 RID: 3574
			ReportIncrementalSyncContentChangesPaged2,
			// Token: 0x04000DF7 RID: 3575
			ValidationValueIsMissing,
			// Token: 0x04000DF8 RID: 3576
			ReportLargeAmountOfDataLossAccepted2,
			// Token: 0x04000DF9 RID: 3577
			ReportLargeItemEncountered,
			// Token: 0x04000DFA RID: 3578
			JobHasBeenRelinquishedDueToCancelPostponed,
			// Token: 0x04000DFB RID: 3579
			ReportMoveRequestResumed,
			// Token: 0x04000DFC RID: 3580
			ReportRequestIsInvalid,
			// Token: 0x04000DFD RID: 3581
			PublicFoldersId,
			// Token: 0x04000DFE RID: 3582
			ItemCountsAndSizes,
			// Token: 0x04000DFF RID: 3583
			ValidationTargetArchiveMDBMismatch,
			// Token: 0x04000E00 RID: 3584
			DestinationMailboxSeedMBICacheFailed,
			// Token: 0x04000E01 RID: 3585
			MoveHasBeenRelinquishedDueToTargetDatabaseFailover,
			// Token: 0x04000E02 RID: 3586
			ServerNotFound,
			// Token: 0x04000E03 RID: 3587
			ReportIncrementalSyncProgress,
			// Token: 0x04000E04 RID: 3588
			ReportFailedToLinkADPublicFolder,
			// Token: 0x04000E05 RID: 3589
			UnableToGetPSTFolderProps,
			// Token: 0x04000E06 RID: 3590
			ReportArchiveUpdated,
			// Token: 0x04000E07 RID: 3591
			ReportSourceMailboxResetFailed,
			// Token: 0x04000E08 RID: 3592
			ReportMoveRequestSaveFailed,
			// Token: 0x04000E09 RID: 3593
			IdentityWasNotInValidFormat,
			// Token: 0x04000E0A RID: 3594
			ReportInitialSeedingStarted,
			// Token: 0x04000E0B RID: 3595
			DestinationADNotUpToDate,
			// Token: 0x04000E0C RID: 3596
			EasFolderSyncFailedTransiently,
			// Token: 0x04000E0D RID: 3597
			CompositeDataContext,
			// Token: 0x04000E0E RID: 3598
			BadItemMisplacedFolder,
			// Token: 0x04000E0F RID: 3599
			PublicFolderMigrationNotSupportedFromCurrentExchange2007Version,
			// Token: 0x04000E10 RID: 3600
			MailboxAlreadySynced,
			// Token: 0x04000E11 RID: 3601
			OrganizationRelationshipNotFound,
			// Token: 0x04000E12 RID: 3602
			ReportUnableToUpdateSourceMailbox2,
			// Token: 0x04000E13 RID: 3603
			PickupStatusLightJob,
			// Token: 0x04000E14 RID: 3604
			ReportSourceMailUserAfterFinalization,
			// Token: 0x04000E15 RID: 3605
			JobIsStuck,
			// Token: 0x04000E16 RID: 3606
			UnexpectedError,
			// Token: 0x04000E17 RID: 3607
			OlcSettingNotImplemented,
			// Token: 0x04000E18 RID: 3608
			ReportDestinationMailboxCleanupFailed,
			// Token: 0x04000E19 RID: 3609
			ReportMoveRequestProcessedByAnotherMRS,
			// Token: 0x04000E1A RID: 3610
			UnableToReadPSTFolder,
			// Token: 0x04000E1B RID: 3611
			MailboxDatabaseNotUnique,
			// Token: 0x04000E1C RID: 3612
			EasFetchFailed,
			// Token: 0x04000E1D RID: 3613
			FilterOperatorMustBeEQorNE,
			// Token: 0x04000E1E RID: 3614
			ReportMissingItemEncountered,
			// Token: 0x04000E1F RID: 3615
			PickupStatusInvalidJob,
			// Token: 0x04000E20 RID: 3616
			ReportMoveContinued,
			// Token: 0x04000E21 RID: 3617
			ReportMoveRequestSuspended,
			// Token: 0x04000E22 RID: 3618
			PstTracingId,
			// Token: 0x04000E23 RID: 3619
			InvalidUid,
			// Token: 0x04000E24 RID: 3620
			UnableToGetPSTHierarchy,
			// Token: 0x04000E25 RID: 3621
			ReportCopyProgress2,
			// Token: 0x04000E26 RID: 3622
			ReportSoftDeletedItemCountsAndSizesInArchiveLoc,
			// Token: 0x04000E27 RID: 3623
			ReportMailboxAfterFinalization2,
			// Token: 0x04000E28 RID: 3624
			ExceptionDetails,
			// Token: 0x04000E29 RID: 3625
			ValidationNameMismatch,
			// Token: 0x04000E2A RID: 3626
			ReportInitialSyncCheckpointCompleted,
			// Token: 0x04000E2B RID: 3627
			ReportLargeItemsSkipped,
			// Token: 0x04000E2C RID: 3628
			ReportMailboxArchiveInfoBeforeMove,
			// Token: 0x04000E2D RID: 3629
			ReportSourceMailboxUpdateFailed,
			// Token: 0x04000E2E RID: 3630
			EasFetchFailedTransiently,
			// Token: 0x04000E2F RID: 3631
			ReportMoveRequestIsInvalid,
			// Token: 0x04000E30 RID: 3632
			EasSendFailedError,
			// Token: 0x04000E31 RID: 3633
			ReportMovedMailUserMorphedToMailbox,
			// Token: 0x04000E32 RID: 3634
			CorruptSortOrderData,
			// Token: 0x04000E33 RID: 3635
			ReportRetryingPostMoveCleanup,
			// Token: 0x04000E34 RID: 3636
			EasFolderDeleteFailed,
			// Token: 0x04000E35 RID: 3637
			ErrorResourceReservation,
			// Token: 0x04000E36 RID: 3638
			MrsProxyServiceIsDisabled2,
			// Token: 0x04000E37 RID: 3639
			EasConnectFailed,
			// Token: 0x04000E38 RID: 3640
			CorruptNamedPropData,
			// Token: 0x04000E39 RID: 3641
			SourceMailboxCleanupFailed,
			// Token: 0x04000E3A RID: 3642
			ReportMailboxBeforeFinalization,
			// Token: 0x04000E3B RID: 3643
			RequestHasBeenPostponedDueToBadHealthOfBackendServers,
			// Token: 0x04000E3C RID: 3644
			JobHasBeenRelinquishedDueToProxyThrottling,
			// Token: 0x04000E3D RID: 3645
			PopTracingId,
			// Token: 0x04000E3E RID: 3646
			MoveHasBeenAutoSuspendedUntilCompleteAfter,
			// Token: 0x04000E3F RID: 3647
			MustRehomeRequestToSupportedVersion,
			// Token: 0x04000E40 RID: 3648
			UnsupportedSyncProtocol,
			// Token: 0x04000E41 RID: 3649
			ReportDestinationMailboxResetFailed3,
			// Token: 0x04000E42 RID: 3650
			MoveHasBeenCanceled,
			// Token: 0x04000E43 RID: 3651
			InvalidMoveRequest,
			// Token: 0x04000E44 RID: 3652
			ErrorWlmResourceUnhealthy1,
			// Token: 0x04000E45 RID: 3653
			ReportRelinquishBecauseMailboxIsLocked,
			// Token: 0x04000E46 RID: 3654
			ErrorWlmResourceUnhealthy,
			// Token: 0x04000E47 RID: 3655
			PickupStatusCreateJob,
			// Token: 0x04000E48 RID: 3656
			MailboxDatabaseNotFoundById,
			// Token: 0x04000E49 RID: 3657
			UnsupportedRecipientType,
			// Token: 0x04000E4A RID: 3658
			SimpleValueDataContext,
			// Token: 0x04000E4B RID: 3659
			PropTagsDataContext,
			// Token: 0x04000E4C RID: 3660
			ReportMergeInitialized,
			// Token: 0x04000E4D RID: 3661
			InvalidOperationError,
			// Token: 0x04000E4E RID: 3662
			ReportFailedToUpdateUserSD2,
			// Token: 0x04000E4F RID: 3663
			ReportMoveRequestSet,
			// Token: 0x04000E50 RID: 3664
			PickupStatusJobPoisoned,
			// Token: 0x04000E51 RID: 3665
			UnexpectedFilterType,
			// Token: 0x04000E52 RID: 3666
			StoreIntegError,
			// Token: 0x04000E53 RID: 3667
			ErrorWlmCapacityExceeded3,
			// Token: 0x04000E54 RID: 3668
			ClusterIPNotFound,
			// Token: 0x04000E55 RID: 3669
			InvalidEscapedChar,
			// Token: 0x04000E56 RID: 3670
			ReportFailedToApplySearchCondition,
			// Token: 0x04000E57 RID: 3671
			BadItemMissingFolder,
			// Token: 0x04000E58 RID: 3672
			FolderAliasIsInvalid,
			// Token: 0x04000E59 RID: 3673
			FolderDataContextGeneric,
			// Token: 0x04000E5A RID: 3674
			ReportFailingMoveBecauseSyncStateIssue,
			// Token: 0x04000E5B RID: 3675
			ReportMailboxRemovedRetrying,
			// Token: 0x04000E5C RID: 3676
			ReportRequestSet,
			// Token: 0x04000E5D RID: 3677
			PublicFolderMoveTracingId,
			// Token: 0x04000E5E RID: 3678
			UnableToReadPSTMessage,
			// Token: 0x04000E5F RID: 3679
			ReportTransientExceptionOccurred,
			// Token: 0x04000E60 RID: 3680
			IsIntegAttemptsExceededError,
			// Token: 0x04000E61 RID: 3681
			EasSyncFailedPermanently,
			// Token: 0x04000E62 RID: 3682
			FolderDataContextRoot,
			// Token: 0x04000E63 RID: 3683
			SourceMailboxIsNotInSourceMDB,
			// Token: 0x04000E64 RID: 3684
			DestinationAddMoveHistoryEntryFailed,
			// Token: 0x04000E65 RID: 3685
			ReportReplaySyncStateNull,
			// Token: 0x04000E66 RID: 3686
			JobIsPoisoned,
			// Token: 0x04000E67 RID: 3687
			ValidationFlagsMismatch2,
			// Token: 0x04000E68 RID: 3688
			InvalidDataError,
			// Token: 0x04000E69 RID: 3689
			MoveRequestMessageError,
			// Token: 0x04000E6A RID: 3690
			UnsupportedClientVersion,
			// Token: 0x04000E6B RID: 3691
			ICSViewDataContext,
			// Token: 0x04000E6C RID: 3692
			ValidationNoIndexEntryForRequest,
			// Token: 0x04000E6D RID: 3693
			MailboxSettingsJunkMailError,
			// Token: 0x04000E6E RID: 3694
			ReportInitialSyncCheckpointCreationProgress,
			// Token: 0x04000E6F RID: 3695
			NestedExceptionMsg,
			// Token: 0x04000E70 RID: 3696
			EasFetchFailedPermanently,
			// Token: 0x04000E71 RID: 3697
			MdbNotOnServer,
			// Token: 0x04000E72 RID: 3698
			RPCHTTPMailboxId,
			// Token: 0x04000E73 RID: 3699
			ValidationObjectInvolvedInMultipleRelocations,
			// Token: 0x04000E74 RID: 3700
			ReportDestinationMailboxSeedMBICacheFailed2,
			// Token: 0x04000E75 RID: 3701
			UnknownSecurityProp,
			// Token: 0x04000E76 RID: 3702
			ReportMovedMailboxMorphedToMailUser,
			// Token: 0x04000E77 RID: 3703
			RemoteServerName,
			// Token: 0x04000E78 RID: 3704
			ReportMailboxInfoBeforeMove,
			// Token: 0x04000E79 RID: 3705
			ErrorCouldNotFindMoveRequest,
			// Token: 0x04000E7A RID: 3706
			UnexpectedValue,
			// Token: 0x04000E7B RID: 3707
			RestoreMailboxId,
			// Token: 0x04000E7C RID: 3708
			UnableToSavePSTSyncState,
			// Token: 0x04000E7D RID: 3709
			MissingDatabaseName2,
			// Token: 0x04000E7E RID: 3710
			ReportIncrementalSyncContentChangesSynced,
			// Token: 0x04000E7F RID: 3711
			MRSProxyConnectionLimitReachedError,
			// Token: 0x04000E80 RID: 3712
			ValidationUserIsNotInAD,
			// Token: 0x04000E81 RID: 3713
			ReportRequestRemoved,
			// Token: 0x04000E82 RID: 3714
			ReportRequestCreated,
			// Token: 0x04000E83 RID: 3715
			DestinationMailboxResetFailed,
			// Token: 0x04000E84 RID: 3716
			ReportMoveAlreadyFinished2,
			// Token: 0x04000E85 RID: 3717
			PositionInteger,
			// Token: 0x04000E86 RID: 3718
			ValidationMoveRequestInWrongMDB,
			// Token: 0x04000E87 RID: 3719
			BadItemMissingItem,
			// Token: 0x04000E88 RID: 3720
			CrossSiteError,
			// Token: 0x04000E89 RID: 3721
			ReportSourceMailboxCleanupFailed3,
			// Token: 0x04000E8A RID: 3722
			ReportRequestIsSticky,
			// Token: 0x04000E8B RID: 3723
			SettingRehomeOnRelatedRequestsFailed,
			// Token: 0x04000E8C RID: 3724
			FolderHierarchyContainsParentChainLoop,
			// Token: 0x04000E8D RID: 3725
			ReportRestartingMoveBecauseSyncStateDoesNotExist,
			// Token: 0x04000E8E RID: 3726
			CertificateLoadError,
			// Token: 0x04000E8F RID: 3727
			KBytesPerSec,
			// Token: 0x04000E90 RID: 3728
			ValidationTargetUserMismatch,
			// Token: 0x04000E91 RID: 3729
			OperationDataContext,
			// Token: 0x04000E92 RID: 3730
			ReportRelinquishBecauseResourceReservationFailed,
			// Token: 0x04000E93 RID: 3731
			ReportIncrementalSyncCompleted2,
			// Token: 0x04000E94 RID: 3732
			CommunicationError,
			// Token: 0x04000E95 RID: 3733
			ReportBadItemEncountered,
			// Token: 0x04000E96 RID: 3734
			InvalidServerName,
			// Token: 0x04000E97 RID: 3735
			OnlineMoveNotSupported,
			// Token: 0x04000E98 RID: 3736
			UnsupportedRemoteServerVersion,
			// Token: 0x04000E99 RID: 3737
			ReportBadItemEncountered2,
			// Token: 0x04000E9A RID: 3738
			ReportSyncedJob,
			// Token: 0x04000E9B RID: 3739
			ValidationRequestTypeMismatch,
			// Token: 0x04000E9C RID: 3740
			RecipientIsNotAMailbox,
			// Token: 0x04000E9D RID: 3741
			ReportFailedToDisconnectFromSource,
			// Token: 0x04000E9E RID: 3742
			UnableToCreateToken,
			// Token: 0x04000E9F RID: 3743
			EasFetchFailedError,
			// Token: 0x04000EA0 RID: 3744
			ReportMoveRequestIsSticky,
			// Token: 0x04000EA1 RID: 3745
			ReportMailboxArchiveInfoBeforeMoveLoc,
			// Token: 0x04000EA2 RID: 3746
			ReportDestinationMailboxCleanupFailed2,
			// Token: 0x04000EA3 RID: 3747
			JobIsStalled,
			// Token: 0x04000EA4 RID: 3748
			ReportRequestResumedWithSuspendWhenReadyToComplete,
			// Token: 0x04000EA5 RID: 3749
			BlockedType,
			// Token: 0x04000EA6 RID: 3750
			PositionIntegerPlus,
			// Token: 0x04000EA7 RID: 3751
			MoveHasBeenRelinquished,
			// Token: 0x04000EA8 RID: 3752
			FolderIsLive,
			// Token: 0x04000EA9 RID: 3753
			ValidationSourceMDBMismatch,
			// Token: 0x04000EAA RID: 3754
			ReportMergingFolder,
			// Token: 0x04000EAB RID: 3755
			ReportSourceMailboxBeforeFinalization,
			// Token: 0x04000EAC RID: 3756
			ValidationSourceArchiveMDBMismatch,
			// Token: 0x04000EAD RID: 3757
			ReportDestinationMailboxClearSyncStateFailed2,
			// Token: 0x04000EAE RID: 3758
			UnableToDetermineMDBSite,
			// Token: 0x04000EAF RID: 3759
			KBytes,
			// Token: 0x04000EB0 RID: 3760
			StorageConnectionType,
			// Token: 0x04000EB1 RID: 3761
			PropTagToPropertyDefinitionConversion,
			// Token: 0x04000EB2 RID: 3762
			MailboxDataReplicationFailed,
			// Token: 0x04000EB3 RID: 3763
			HandleNotFound,
			// Token: 0x04000EB4 RID: 3764
			MdbIsOffline,
			// Token: 0x04000EB5 RID: 3765
			UnableToLoadPSTSyncState,
			// Token: 0x04000EB6 RID: 3766
			ReportSyncStateLoaded2,
			// Token: 0x04000EB7 RID: 3767
			ReportSoftDeletedItemCountsAndSizesInArchive,
			// Token: 0x04000EB8 RID: 3768
			ImapTracingId,
			// Token: 0x04000EB9 RID: 3769
			ReportIncrementalSyncContentChanges,
			// Token: 0x04000EBA RID: 3770
			UnableToFindMbxServer,
			// Token: 0x04000EBB RID: 3771
			MailboxDatabaseNotFoundByGuid,
			// Token: 0x04000EBC RID: 3772
			RequestTypeNotUnderstoodOnThisServer,
			// Token: 0x04000EBD RID: 3773
			ReportCopyProgress,
			// Token: 0x04000EBE RID: 3774
			ValidationSourceUserMismatch,
			// Token: 0x04000EBF RID: 3775
			TargetRecipientIsNotAnMEU,
			// Token: 0x04000EC0 RID: 3776
			MoveRequestMessageWarningSeparator,
			// Token: 0x04000EC1 RID: 3777
			ReportRequestResumed,
			// Token: 0x04000EC2 RID: 3778
			ReportSyncStateCorrupt,
			// Token: 0x04000EC3 RID: 3779
			ReportMoveIsStalled,
			// Token: 0x04000EC4 RID: 3780
			JobHasBeenRelinquishedDueToMailboxLockout,
			// Token: 0x04000EC5 RID: 3781
			DestinationMailboxNotCleanedUp,
			// Token: 0x04000EC6 RID: 3782
			PublicFolderMove,
			// Token: 0x04000EC7 RID: 3783
			ReportSourceMailboxCleanupSucceeded,
			// Token: 0x04000EC8 RID: 3784
			ReportDestinationMailboxResetNotGuaranteed,
			// Token: 0x04000EC9 RID: 3785
			CommunicationWithRemoteServiceFailed,
			// Token: 0x04000ECA RID: 3786
			MailboxIsNotBeingMoved,
			// Token: 0x04000ECB RID: 3787
			ErrorWlmCapacityExceeded2,
			// Token: 0x04000ECC RID: 3788
			PublicFolderMigrationNotSupportedFromExchange2003OrEarlier,
			// Token: 0x04000ECD RID: 3789
			CouldNotFindDcHavingUmmUpdateError,
			// Token: 0x04000ECE RID: 3790
			UnknownRestrictionType,
			// Token: 0x04000ECF RID: 3791
			ReportDestinationMailboxClearSyncStateSucceeded,
			// Token: 0x04000ED0 RID: 3792
			ReportInitializingMove,
			// Token: 0x04000ED1 RID: 3793
			BadItemFolderRule,
			// Token: 0x04000ED2 RID: 3794
			FolderIsMissing,
			// Token: 0x04000ED3 RID: 3795
			ReportInitializingFolderHierarchy,
			// Token: 0x04000ED4 RID: 3796
			ReportSyncStateSaveFailed2,
			// Token: 0x04000ED5 RID: 3797
			ReportSyncStateLoaded,
			// Token: 0x04000ED6 RID: 3798
			ReportUpdateMovedMailboxFailureAfterADSwitchover,
			// Token: 0x04000ED7 RID: 3799
			MailboxDoesNotExist,
			// Token: 0x04000ED8 RID: 3800
			PositionOfMoveRequestInSystemMailboxQueue,
			// Token: 0x04000ED9 RID: 3801
			ArchiveMailboxTracingId,
			// Token: 0x04000EDA RID: 3802
			ReportRetryingMailboxCreation,
			// Token: 0x04000EDB RID: 3803
			ReportInitialSeedingCompleted,
			// Token: 0x04000EDC RID: 3804
			FastTransferBuffer,
			// Token: 0x04000EDD RID: 3805
			ErrorWlmCapacityExceeded,
			// Token: 0x04000EDE RID: 3806
			RestoringConnectedMailboxError,
			// Token: 0x04000EDF RID: 3807
			ReportProgress,
			// Token: 0x04000EE0 RID: 3808
			EntryIDsDataContext,
			// Token: 0x04000EE1 RID: 3809
			EasTracingId,
			// Token: 0x04000EE2 RID: 3810
			ReportTransientException,
			// Token: 0x04000EE3 RID: 3811
			RequestGuidNotUnique,
			// Token: 0x04000EE4 RID: 3812
			MoveIsStalled,
			// Token: 0x04000EE5 RID: 3813
			ContainerMailboxTracingId,
			// Token: 0x04000EE6 RID: 3814
			RequestIsStalled,
			// Token: 0x04000EE7 RID: 3815
			JobHasBeenRelinquishedDueToTransientErrorDuringOfflineMove,
			// Token: 0x04000EE8 RID: 3816
			ReportFailedToUpdateUserSD,
			// Token: 0x04000EE9 RID: 3817
			PrimaryMailboxId,
			// Token: 0x04000EEA RID: 3818
			ReportIncrementalSyncContentChangesPaged,
			// Token: 0x04000EEB RID: 3819
			InvalidEndpointAddressError,
			// Token: 0x04000EEC RID: 3820
			DatacenterMissingHosts,
			// Token: 0x04000EED RID: 3821
			ReportSyncStateCleared,
			// Token: 0x04000EEE RID: 3822
			ReportJobIsStalled,
			// Token: 0x04000EEF RID: 3823
			ReportSourceMailboxConnection,
			// Token: 0x04000EF0 RID: 3824
			ReportDestinationMailboxSeedMBICacheSucceeded2,
			// Token: 0x04000EF1 RID: 3825
			RecipientNotFound,
			// Token: 0x04000EF2 RID: 3826
			ReportCleanUpFoldersDestination,
			// Token: 0x04000EF3 RID: 3827
			UnableToOpenPST2,
			// Token: 0x04000EF4 RID: 3828
			ValidationFlagsMismatch,
			// Token: 0x04000EF5 RID: 3829
			ReportRequestAllowedMismatch,
			// Token: 0x04000EF6 RID: 3830
			ReportRequestSuspended,
			// Token: 0x04000EF7 RID: 3831
			PublicFolderMigrationNotSupportedFromCurrentExchange2010Version,
			// Token: 0x04000EF8 RID: 3832
			MoveRequestDataIsCorrupt,
			// Token: 0x04000EF9 RID: 3833
			SourceMailboxUpdateFailed,
			// Token: 0x04000EFA RID: 3834
			ReportUnableToPreserveMailboxSignature,
			// Token: 0x04000EFB RID: 3835
			UsageText,
			// Token: 0x04000EFC RID: 3836
			ReportLargeAmountOfDataLossAccepted,
			// Token: 0x04000EFD RID: 3837
			EndpointNotFoundError,
			// Token: 0x04000EFE RID: 3838
			MailboxNotSynced,
			// Token: 0x04000EFF RID: 3839
			ReportSyncStateWrongRequestGuid,
			// Token: 0x04000F00 RID: 3840
			ReportDestinationMailboxConnection,
			// Token: 0x04000F01 RID: 3841
			ReportIncrementalSyncContentChangesSynced2,
			// Token: 0x04000F02 RID: 3842
			EasFolderSyncFailedPermanently,
			// Token: 0x04000F03 RID: 3843
			ReportFailedToDisconnectFromDestination2,
			// Token: 0x04000F04 RID: 3844
			NamedPropsDataContext,
			// Token: 0x04000F05 RID: 3845
			ReportFailingInvalidMoveRequest,
			// Token: 0x04000F06 RID: 3846
			BadItemCorruptMailboxSetting,
			// Token: 0x04000F07 RID: 3847
			ReportMailboxAfterFinalization,
			// Token: 0x04000F08 RID: 3848
			MessageEnumerationFailed,
			// Token: 0x04000F09 RID: 3849
			ErrorTargetDeliveryDomainMismatch,
			// Token: 0x04000F0A RID: 3850
			SortOrderDataContext,
			// Token: 0x04000F0B RID: 3851
			CannotCreateEntryId,
			// Token: 0x04000F0C RID: 3852
			PropertyMismatch,
			// Token: 0x04000F0D RID: 3853
			ReportWaitingIsInteg,
			// Token: 0x04000F0E RID: 3854
			SoftDeletedItemsCountAndSize,
			// Token: 0x04000F0F RID: 3855
			EasSyncCouldNotFindFolder,
			// Token: 0x04000F10 RID: 3856
			EasFolderCreateFailed,
			// Token: 0x04000F11 RID: 3857
			MailPublicFolderWithLegacyExchangeDnNotFound,
			// Token: 0x04000F12 RID: 3858
			ReportFolderHierarchyInitialized,
			// Token: 0x04000F13 RID: 3859
			ReportRequestContinued,
			// Token: 0x04000F14 RID: 3860
			ReportFolderMergeStats,
			// Token: 0x04000F15 RID: 3861
			ReportCorruptItemsSkipped,
			// Token: 0x04000F16 RID: 3862
			ValidationUserLacksMailbox,
			// Token: 0x04000F17 RID: 3863
			EasSyncFailed,
			// Token: 0x04000F18 RID: 3864
			ReportSoftDeletedItemsWillNotBeMigrated,
			// Token: 0x04000F19 RID: 3865
			FolderDataContextId,
			// Token: 0x04000F1A RID: 3866
			ReportIcsSyncStateNull,
			// Token: 0x04000F1B RID: 3867
			ReportFatalExceptionOccurred,
			// Token: 0x04000F1C RID: 3868
			ValidationMailboxIdentitiesDontMatch,
			// Token: 0x04000F1D RID: 3869
			ReportMailboxArchiveInfoAfterMove,
			// Token: 0x04000F1E RID: 3870
			FolderHierarchyContainsMultipleRoots,
			// Token: 0x04000F1F RID: 3871
			ValidationStorageMDBMismatch,
			// Token: 0x04000F20 RID: 3872
			ReportFatalException,
			// Token: 0x04000F21 RID: 3873
			ReportMailboxInfoAfterMoveLoc,
			// Token: 0x04000F22 RID: 3874
			UnableToFetchMimeStream,
			// Token: 0x04000F23 RID: 3875
			ReportUnableToUpdateSourceMailbox,
			// Token: 0x04000F24 RID: 3876
			ErrorNoCASServersInSite,
			// Token: 0x04000F25 RID: 3877
			ReportSoftDeletedItemsNotMigrated,
			// Token: 0x04000F26 RID: 3878
			EasMoveFailedError,
			// Token: 0x04000F27 RID: 3879
			ReportReplayActionsEnumerated,
			// Token: 0x04000F28 RID: 3880
			ValidationTargetMDBMismatch,
			// Token: 0x04000F29 RID: 3881
			EasMoveFailed,
			// Token: 0x04000F2A RID: 3882
			ReportRequestSaveFailed2,
			// Token: 0x04000F2B RID: 3883
			IsExcludedFromProvisioningError,
			// Token: 0x04000F2C RID: 3884
			ClusterIPMissingHosts,
			// Token: 0x04000F2D RID: 3885
			MoveCompleteFailedForAlreadyCanceledMove,
			// Token: 0x04000F2E RID: 3886
			WindowsLiveIDAddressIsMissing,
			// Token: 0x04000F2F RID: 3887
			ReportStartedIsInteg,
			// Token: 0x04000F30 RID: 3888
			ContentFilterIsInvalid,
			// Token: 0x04000F31 RID: 3889
			ReportCompletedIsInteg,
			// Token: 0x04000F32 RID: 3890
			BadItemSearchFolder,
			// Token: 0x04000F33 RID: 3891
			UnableToGetPSTReceiveFolder,
			// Token: 0x04000F34 RID: 3892
			BadItemLarge,
			// Token: 0x04000F35 RID: 3893
			BadItemFolderPropertyMismatch,
			// Token: 0x04000F36 RID: 3894
			AuxFolderMoveTracingId,
			// Token: 0x04000F37 RID: 3895
			JobHasBeenRelinquishedDueToDataGuaranteeTimeout,
			// Token: 0x04000F38 RID: 3896
			EasSendFailed,
			// Token: 0x04000F39 RID: 3897
			ErrorRegKeyNotExist,
			// Token: 0x04000F3A RID: 3898
			ReportTooManyTransientFailures,
			// Token: 0x04000F3B RID: 3899
			RestrictionDataContext,
			// Token: 0x04000F3C RID: 3900
			ServerNotFoundByGuid,
			// Token: 0x04000F3D RID: 3901
			MoveRequestMessageWarning,
			// Token: 0x04000F3E RID: 3902
			MRSInstanceFailed,
			// Token: 0x04000F3F RID: 3903
			ReportTargetMailboxAfterFinalization2,
			// Token: 0x04000F40 RID: 3904
			EasCountFailed,
			// Token: 0x04000F41 RID: 3905
			FolderReferencedMoreThanOnce,
			// Token: 0x04000F42 RID: 3906
			RecipientAggregatedMailboxNotFound,
			// Token: 0x04000F43 RID: 3907
			PostSaveActionFailed,
			// Token: 0x04000F44 RID: 3908
			ReportReplayActionsSynced,
			// Token: 0x04000F45 RID: 3909
			FolderPathIsInvalid,
			// Token: 0x04000F46 RID: 3910
			RestoreMailboxTracingId,
			// Token: 0x04000F47 RID: 3911
			ReportMoveRequestRemoved,
			// Token: 0x04000F48 RID: 3912
			JobHasBeenRelinquishedDueToServerBusy,
			// Token: 0x04000F49 RID: 3913
			PickupStatusSubTypeNotSupported,
			// Token: 0x04000F4A RID: 3914
			PickupStatusReservationFailure,
			// Token: 0x04000F4B RID: 3915
			ReportIncrementalSyncHierarchyChanges,
			// Token: 0x04000F4C RID: 3916
			InvalidHandleType,
			// Token: 0x04000F4D RID: 3917
			ProviderAlreadySpecificToDatabase,
			// Token: 0x04000F4E RID: 3918
			ReportSourceMailboxCleanupFailed2,
			// Token: 0x04000F4F RID: 3919
			ReportReplayActionsCompleted,
			// Token: 0x04000F50 RID: 3920
			ReportTargetMailUserBeforeFinalization,
			// Token: 0x04000F51 RID: 3921
			MailPublicFolderWithObjectIdNotFound,
			// Token: 0x04000F52 RID: 3922
			ReportIncrementalSyncCompleted,
			// Token: 0x04000F53 RID: 3923
			PickupStatusProxyBackoff,
			// Token: 0x04000F54 RID: 3924
			MailPublicFolderWithMultipleEntriesFound,
			// Token: 0x04000F55 RID: 3925
			MoveRequestMessageInformational,
			// Token: 0x04000F56 RID: 3926
			ReportJobIsStalledWithFailure,
			// Token: 0x04000F57 RID: 3927
			ReportIncrementalSyncContentChanges2,
			// Token: 0x04000F58 RID: 3928
			NotSupportedCodePageError,
			// Token: 0x04000F59 RID: 3929
			RecipientInvalidLegDN,
			// Token: 0x04000F5A RID: 3930
			ReportMailboxContentsVerificationComplete,
			// Token: 0x04000F5B RID: 3931
			ValidationOrganizationMismatch,
			// Token: 0x04000F5C RID: 3932
			UnableToStreamPSTProp,
			// Token: 0x04000F5D RID: 3933
			UnsupportedClientVersionWithOperation,
			// Token: 0x04000F5E RID: 3934
			ReportJobRehomed,
			// Token: 0x04000F5F RID: 3935
			ReportSourceMailboxCleanupSkipped,
			// Token: 0x04000F60 RID: 3936
			ReportFailedToDisconnectFromDestination,
			// Token: 0x04000F61 RID: 3937
			IsIntegTooLongError,
			// Token: 0x04000F62 RID: 3938
			ReportIncrementalSyncChanges,
			// Token: 0x04000F63 RID: 3939
			UnableToCreatePSTMessage,
			// Token: 0x04000F64 RID: 3940
			ErrorStaticCapacityExceeded,
			// Token: 0x04000F65 RID: 3941
			FolderAlreadyExists,
			// Token: 0x04000F66 RID: 3942
			ReportMoveResumed,
			// Token: 0x04000F67 RID: 3943
			ReportSoftDeletedItemCountsAndSizes,
			// Token: 0x04000F68 RID: 3944
			MissingDatabaseName,
			// Token: 0x04000F69 RID: 3945
			ReportUpdateMovedMailboxError,
			// Token: 0x04000F6A RID: 3946
			UnableToVerifyMailboxConnectivity,
			// Token: 0x04000F6B RID: 3947
			BadItemCorrupt,
			// Token: 0x04000F6C RID: 3948
			ReportMailboxSignatureIsNotPreserved,
			// Token: 0x04000F6D RID: 3949
			ReportProxyConnectionLimitMet,
			// Token: 0x04000F6E RID: 3950
			FolderAlreadyInTarget,
			// Token: 0x04000F6F RID: 3951
			ReportTargetFolderDeleted,
			// Token: 0x04000F70 RID: 3952
			FolderCopyFailed,
			// Token: 0x04000F71 RID: 3953
			ErrorStaticCapacityExceeded1,
			// Token: 0x04000F72 RID: 3954
			ReportWaitingForAdReplication,
			// Token: 0x04000F73 RID: 3955
			MailboxServerInformation,
			// Token: 0x04000F74 RID: 3956
			ReportTargetMailUserBeforeFinalization2,
			// Token: 0x04000F75 RID: 3957
			MoveHasBeenAutoSuspended,
			// Token: 0x04000F76 RID: 3958
			ReportRelinquishingJobDueToFailover,
			// Token: 0x04000F77 RID: 3959
			OrphanedDatabaseName,
			// Token: 0x04000F78 RID: 3960
			ReportTargetMailboxAfterFinalization,
			// Token: 0x04000F79 RID: 3961
			RecipientPropertyIsNotWriteable,
			// Token: 0x04000F7A RID: 3962
			PropertyTagsDoNotMatch,
			// Token: 0x04000F7B RID: 3963
			PrimaryMailboxTracingId,
			// Token: 0x04000F7C RID: 3964
			ArchiveMailboxId,
			// Token: 0x04000F7D RID: 3965
			ImapObjectNotFound,
			// Token: 0x04000F7E RID: 3966
			FolderReferencedAsBothIncludedAndExcluded,
			// Token: 0x04000F7F RID: 3967
			DestinationMailboxSyncStateDeletionFailed,
			// Token: 0x04000F80 RID: 3968
			FolderHierarchyContainsDuplicates,
			// Token: 0x04000F81 RID: 3969
			ReportSourceFolderDeleted,
			// Token: 0x04000F82 RID: 3970
			ReportSoftDeletedItemCountsAndSizesLoc,
			// Token: 0x04000F83 RID: 3971
			EasSyncFailedTransiently,
			// Token: 0x04000F84 RID: 3972
			ReportJobResumed,
			// Token: 0x04000F85 RID: 3973
			PickupStatusJobTypeNotSupported,
			// Token: 0x04000F86 RID: 3974
			ValidationMailboxGuidsDontMatch,
			// Token: 0x04000F87 RID: 3975
			ReportReplaySyncStateCorrupt,
			// Token: 0x04000F88 RID: 3976
			UnableToReadADUser,
			// Token: 0x04000F89 RID: 3977
			UnsupportedRemoteServerVersionWithOperation,
			// Token: 0x04000F8A RID: 3978
			JobHasBeenRelinquishedDueToResourceReservation,
			// Token: 0x04000F8B RID: 3979
			ReportUnableToComputeTargetAddress,
			// Token: 0x04000F8C RID: 3980
			ReportSourceMailUserAfterFinalization2,
			// Token: 0x04000F8D RID: 3981
			RecipientMissingLegDN,
			// Token: 0x04000F8E RID: 3982
			ReportIcsSyncStateCorrupt,
			// Token: 0x04000F8F RID: 3983
			EasFolderUpdateFailed,
			// Token: 0x04000F90 RID: 3984
			UnableToUpdateSourceMailbox,
			// Token: 0x04000F91 RID: 3985
			DestinationMailboxResetNotGuaranteed,
			// Token: 0x04000F92 RID: 3986
			ReportMailboxArchiveInfoAfterMoveLoc,
			// Token: 0x04000F93 RID: 3987
			UnableToClosePST,
			// Token: 0x04000F94 RID: 3988
			ReportRequestProcessedByAnotherMRS,
			// Token: 0x04000F95 RID: 3989
			JobIsStalledAndFailed,
			// Token: 0x04000F96 RID: 3990
			ReportSyncStateSaveFailed,
			// Token: 0x04000F97 RID: 3991
			ErrorMoveInProgress,
			// Token: 0x04000F98 RID: 3992
			ParsingMessageEntryIdFailed,
			// Token: 0x04000F99 RID: 3993
			ReportInitializingJob,
			// Token: 0x04000F9A RID: 3994
			MoveHasBeenRelinquishedDueToProxyThrottling,
			// Token: 0x04000F9B RID: 3995
			RpcClientAccessServerNotConfiguredForMdb,
			// Token: 0x04000F9C RID: 3996
			RemoteMailboxServerInformation,
			// Token: 0x04000F9D RID: 3997
			MoveReportEntryMessage,
			// Token: 0x04000F9E RID: 3998
			ReportDestinationMailboxResetFailed2,
			// Token: 0x04000F9F RID: 3999
			BadItemFolderProperty,
			// Token: 0x04000FA0 RID: 4000
			EasObjectNotFound,
			// Token: 0x04000FA1 RID: 4001
			MaxSubmissionExceeded,
			// Token: 0x04000FA2 RID: 4002
			UnsupportedImapMessageEntryIdVersion,
			// Token: 0x04000FA3 RID: 4003
			ExceptionWithStack,
			// Token: 0x04000FA4 RID: 4004
			ReportDestinationMailboxSeedMBICacheFailed,
			// Token: 0x04000FA5 RID: 4005
			BadItemFolderACL,
			// Token: 0x04000FA6 RID: 4006
			CannotCreateMessageId,
			// Token: 0x04000FA7 RID: 4007
			ReportRestartingMoveBecauseMailboxSignatureVersionIsDifferent
		}
	}
}
