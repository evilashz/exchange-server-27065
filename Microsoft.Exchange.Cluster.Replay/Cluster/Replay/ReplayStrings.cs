using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000394 RID: 916
	internal static class ReplayStrings
	{
		// Token: 0x06002494 RID: 9364 RVA: 0x000ACEEC File Offset: 0x000AB0EC
		static ReplayStrings()
		{
			ReplayStrings.stringIDs.Add(800943078U, "RepairStateClusterIsNotRunning");
			ReplayStrings.stringIDs.Add(3145329300U, "RemoveLogReasonE00OutOfDate");
			ReplayStrings.stringIDs.Add(2169511412U, "FailedToOpenShipLogContextAccessDenied");
			ReplayStrings.stringIDs.Add(3896662565U, "ReplayServiceRpcUnknownInstanceException");
			ReplayStrings.stringIDs.Add(1265493030U, "LockOwnerIdle");
			ReplayStrings.stringIDs.Add(1348435424U, "IncSeedNotSupportedWithShrinkDatabaseError");
			ReplayStrings.stringIDs.Add(1095788331U, "CantStartFromCommandLineTitle");
			ReplayStrings.stringIDs.Add(1390940095U, "AmBcsDatabaseHasNoCopies");
			ReplayStrings.stringIDs.Add(1065254054U, "ErrorCouldNotFindClusterGroupOwnerNodeForAmConfig");
			ReplayStrings.stringIDs.Add(615086071U, "SeederEcDBNotFound");
			ReplayStrings.stringIDs.Add(276770850U, "SeederEcSeedingCancelled");
			ReplayStrings.stringIDs.Add(3718483653U, "DagTaskInstalledFailoverClustering");
			ReplayStrings.stringIDs.Add(2876590454U, "SeederEcDeviceNotReady");
			ReplayStrings.stringIDs.Add(3794143290U, "ErrorReadingDagServerForAmConfig");
			ReplayStrings.stringIDs.Add(3273471454U, "NetworkReadEOF");
			ReplayStrings.stringIDs.Add(3630272863U, "RemoveLogReasonFailedInspection");
			ReplayStrings.stringIDs.Add(3925066867U, "InvalidLogCopyResponse");
			ReplayStrings.stringIDs.Add(2936934256U, "MonitoringADInitNotCompleteException");
			ReplayStrings.stringIDs.Add(861304653U, "AutoReseedFailedToFindVolumeName");
			ReplayStrings.stringIDs.Add(1749232750U, "DagTaskComponentManagerAnotherInstanceRunning");
			ReplayStrings.stringIDs.Add(4014700033U, "AutoReseedManualReseedLaunched");
			ReplayStrings.stringIDs.Add(3988781731U, "SeederEcNoOnlineEdb");
			ReplayStrings.stringIDs.Add(3096281422U, "FullServerSeedInProgressException");
			ReplayStrings.stringIDs.Add(418608118U, "ReplayServiceSuspendCommentException");
			ReplayStrings.stringIDs.Add(856033660U, "InvalidDbForSeedSpecifiedException");
			ReplayStrings.stringIDs.Add(798341136U, "VolumeMountPathDoesNotExistException");
			ReplayStrings.stringIDs.Add(1105864708U, "AmDbOperationWaitFailedException");
			ReplayStrings.stringIDs.Add(2630456743U, "ErrorAmConfigNotInitialized");
			ReplayStrings.stringIDs.Add(2364081162U, "NetworkCorruptDataGeneric");
			ReplayStrings.stringIDs.Add(2266299742U, "ErrorAutomountConsensusNotReached");
			ReplayStrings.stringIDs.Add(1607989356U, "AmBcsNoneSpecified");
			ReplayStrings.stringIDs.Add(3766459347U, "SeederEcNotEnoughDiskException");
			ReplayStrings.stringIDs.Add(1148446483U, "NetworkFailedToAuthServer");
			ReplayStrings.stringIDs.Add(4045836413U, "ErrorFailedToFindLocalServer");
			ReplayStrings.stringIDs.Add(4106538885U, "FailedToOpenShipLogContextDatabaseNotMounted");
			ReplayStrings.stringIDs.Add(910204805U, "ReplayServiceSuspendBlockedAcllException");
			ReplayStrings.stringIDs.Add(1295385018U, "DatabaseCopyLayoutTableNullException");
			ReplayStrings.stringIDs.Add(820683245U, "AmServiceShuttingDown");
			ReplayStrings.stringIDs.Add(512707820U, "DagTaskDagIpAddressesMustBeIpv4Exception");
			ReplayStrings.stringIDs.Add(3351215994U, "UnknownError");
			ReplayStrings.stringIDs.Add(3572810924U, "EseBackFileSystemCorruption");
			ReplayStrings.stringIDs.Add(214622620U, "SuspendOperationName");
			ReplayStrings.stringIDs.Add(3342720985U, "PagePatchLegacyFileExistsException");
			ReplayStrings.stringIDs.Add(606338949U, "SeederEcSuccess");
			ReplayStrings.stringIDs.Add(1055054291U, "NoServices");
			ReplayStrings.stringIDs.Add(1298429885U, "StoreServiceMonitorCriticalError");
			ReplayStrings.stringIDs.Add(3887748188U, "CannotChangeProperties");
			ReplayStrings.stringIDs.Add(2034469932U, "SeederEchrInvalidCallSequence");
			ReplayStrings.stringIDs.Add(3043738041U, "FailedToOpenShipLogContextEseCircularLoggingEnabled");
			ReplayStrings.stringIDs.Add(2591444232U, "ReplayServiceResumeRpcFailedException");
			ReplayStrings.stringIDs.Add(1344658319U, "NetworkSecurityFailed");
			ReplayStrings.stringIDs.Add(3184656137U, "ReplayServiceSuspendRpcFailedException");
			ReplayStrings.stringIDs.Add(1163467430U, "SuspendWantedWriteFailedException");
			ReplayStrings.stringIDs.Add(3512548730U, "ReplayServiceSuspendResumeBlockedException");
			ReplayStrings.stringIDs.Add(1378783691U, "AmDbActionDismountFailedException");
			ReplayStrings.stringIDs.Add(387713678U, "AutoReseedNeverMountedWorkflowReason");
			ReplayStrings.stringIDs.Add(1967903804U, "AutoReseedLogAndDbNotOnSameVolume");
			ReplayStrings.stringIDs.Add(85017254U, "FullServerSeedSkippedShutdownException");
			ReplayStrings.stringIDs.Add(2554265426U, "SeederEcOverlappedWriteErr");
			ReplayStrings.stringIDs.Add(3580573960U, "FailToCleanUpFiles");
			ReplayStrings.stringIDs.Add(2045268632U, "SeederEcNotEnoughDisk");
			ReplayStrings.stringIDs.Add(1045878879U, "MonitoringADServiceShuttingDownException");
			ReplayStrings.stringIDs.Add(1612966754U, "DbHTServiceShuttingDownException");
			ReplayStrings.stringIDs.Add(4049732327U, "NullDbCopyException");
			ReplayStrings.stringIDs.Add(2602551968U, "ErrorClusterServiceNotRunningForAmConfig");
			ReplayStrings.stringIDs.Add(3128283390U, "SeederEcError");
			ReplayStrings.stringIDs.Add(129692820U, "AutoReseedFailedAdminSuspended");
			ReplayStrings.stringIDs.Add(726910307U, "NetworkNoUsableEndpoints");
			ReplayStrings.stringIDs.Add(3772399040U, "ClusterServiceMonitorCriticalError");
			ReplayStrings.stringIDs.Add(1871702564U, "Resynchronizing");
			ReplayStrings.stringIDs.Add(3929636263U, "ReplayServiceSuspendBlockedResynchronizingException");
			ReplayStrings.stringIDs.Add(828128533U, "LockOwnerComponent");
			ReplayStrings.stringIDs.Add(4232351636U, "NetworkIsDisabled");
			ReplayStrings.stringIDs.Add(707284339U, "ResumeOperationName");
			ReplayStrings.stringIDs.Add(2777244007U, "ReplayServiceSuspendReseedBlockedException");
			ReplayStrings.stringIDs.Add(544686542U, "SuspendMessageWriteFailedException");
			ReplayStrings.stringIDs.Add(627050854U, "SyncSuspendResumeOperationName");
			ReplayStrings.stringIDs.Add(1279119335U, "FailedAndSuspended");
			ReplayStrings.stringIDs.Add(1839653327U, "TPRProviderNotListening");
			ReplayStrings.stringIDs.Add(3099361335U, "Suspended");
			ReplayStrings.stringIDs.Add(4137367259U, "ReplayServiceSuspendInPlaceReseedBlockedException");
			ReplayStrings.stringIDs.Add(3167054525U, "AutoReseedMoveActiveBeforeRebuildCatalog");
			ReplayStrings.stringIDs.Add(448095525U, "ErrorCouldNotConnectClusterForAmConfig");
			ReplayStrings.stringIDs.Add(3631647333U, "ReplayServiceShuttingDownException");
			ReplayStrings.stringIDs.Add(2056943490U, "ErrorFailedToOpenClusterObjects");
			ReplayStrings.stringIDs.Add(4042526291U, "FailedToOpenShipLogContextStoreStopped");
			ReplayStrings.stringIDs.Add(1622537741U, "NullDatabaseException");
			ReplayStrings.stringIDs.Add(865952845U, "SeederEcCommunicationsError");
			ReplayStrings.stringIDs.Add(1445478759U, "DagTaskPamNotMovedSubsequentOperationsMayBeSlowOrUnreliable");
			ReplayStrings.stringIDs.Add(482041519U, "SeederEcFailAcqRight");
			ReplayStrings.stringIDs.Add(204895011U, "ProgressStatusInProgress");
			ReplayStrings.stringIDs.Add(3439296139U, "CouldNotGetMountStatus");
			ReplayStrings.stringIDs.Add(3630311467U, "AmClusterNotRunningException");
			ReplayStrings.stringIDs.Add(2719597889U, "LockOwnerConfigChecker");
			ReplayStrings.stringIDs.Add(200627337U, "PrepareToStopCalled");
			ReplayStrings.stringIDs.Add(394420742U, "ReplayServiceSuspendWantedClearedException");
			ReplayStrings.stringIDs.Add(913564495U, "DBCHasNoValidTargetEdbPath");
			ReplayStrings.stringIDs.Add(682288005U, "DeleteChkptReasonCorrupted");
			ReplayStrings.stringIDs.Add(3559163736U, "TPRChangeFailedBecauseNotDismounted");
			ReplayStrings.stringIDs.Add(3778987676U, "CouldNotFindVolumeForFormatException");
			ReplayStrings.stringIDs.Add(2242986398U, "CannotChangeName");
			ReplayStrings.stringIDs.Add(3293194015U, "NetworkManagerInitError");
			ReplayStrings.stringIDs.Add(1105077015U, "PassiveCopyDisconnected");
			ReplayStrings.stringIDs.Add(3691652467U, "SeederEchrErrorFromESECall");
			ReplayStrings.stringIDs.Add(2139980255U, "ReplayServiceSuspendBlockedBackupInProgressException");
			ReplayStrings.stringIDs.Add(3673369909U, "ErrorCouldNotFindServerForAmConfig");
			ReplayStrings.stringIDs.Add(2812191260U, "FailedAtReplacingLogFiles");
			ReplayStrings.stringIDs.Add(887012549U, "ReplayServiceResumeRpcFailedSeedingException");
			ReplayStrings.stringIDs.Add(3025714949U, "StoreNotOnline");
			ReplayStrings.stringIDs.Add(2151299999U, "LockOwnerAttemptCopyLastLogs");
			ReplayStrings.stringIDs.Add(833784388U, "MsexchangereplLong");
			ReplayStrings.stringIDs.Add(2787647009U, "AmOperationInvalidForStandaloneRoleException");
			ReplayStrings.stringIDs.Add(1556772377U, "JetErrorDatabaseNotFound");
			ReplayStrings.stringIDs.Add(2325224058U, "SeederEcBackupInProgress");
			ReplayStrings.stringIDs.Add(511004165U, "LogCopierFailedToGetSuspendLock");
			ReplayStrings.stringIDs.Add(3508567603U, "NetworkDataOverflowGeneric");
			ReplayStrings.stringIDs.Add(4084429598U, "TPRNotEnabled");
			ReplayStrings.stringIDs.Add(458303596U, "LockOwnerSuspend");
			ReplayStrings.stringIDs.Add(1791510079U, "DbValidationFullCopyStatusResultsLabel");
			ReplayStrings.stringIDs.Add(3318672153U, "LogRepairNotPossibleInsuffientToCheckDivergence");
			ReplayStrings.stringIDs.Add(1539727809U, "DagTaskComponentManagerWantsToRebootException");
			ReplayStrings.stringIDs.Add(3985107625U, "NetworkCancelled");
			ReplayStrings.stringIDs.Add(413167004U, "ErrorFailedToGetClusterCoreGroup");
			ReplayStrings.stringIDs.Add(1348455624U, "NotInPendingState");
			ReplayStrings.stringIDs.Add(662858283U, "ErrorInvalidPamServerName");
			ReplayStrings.stringIDs.Add(2070740709U, "SeederEchrErrorFromCallbackCall");
			ReplayStrings.stringIDs.Add(4031202440U, "EnableReplayLagOperationName");
			ReplayStrings.stringIDs.Add(2490829887U, "SeederInstanceAlreadyFailedException");
			ReplayStrings.stringIDs.Add(854026562U, "ReplayServiceSuspendWantedSetException");
			ReplayStrings.stringIDs.Add(1857824471U, "SeederEcOOMem");
			ReplayStrings.stringIDs.Add(399717229U, "DbValidationCopyStatusNameLabel");
			ReplayStrings.stringIDs.Add(3239149605U, "SeederEchrRestoreAtFileLevel");
			ReplayStrings.stringIDs.Add(2097320554U, "TPRNotYetStarted");
			ReplayStrings.stringIDs.Add(560332821U, "SeederEcInvalidInput");
			ReplayStrings.stringIDs.Add(3655345505U, "ErrorRemoteSiteNotConnected");
			ReplayStrings.stringIDs.Add(4251585129U, "ErrorLocalNodeNotUpYet");
			ReplayStrings.stringIDs.Add(1556943668U, "DagTaskClusteringRequiresEnterpriseSkuException");
			ReplayStrings.stringIDs.Add(844399741U, "MsexchangesearchLong");
			ReplayStrings.stringIDs.Add(3809273542U, "LockOwnerBackup");
			ReplayStrings.stringIDs.Add(1718068369U, "AmDbActionMoveFailedException");
			ReplayStrings.stringIDs.Add(1054423051U, "Failed");
			ReplayStrings.stringIDs.Add(3438214222U, "GranularReplicationOverflow");
			ReplayStrings.stringIDs.Add(3142177591U, "CantStartFromCommandLine");
			ReplayStrings.stringIDs.Add(1944472637U, "AmDbActionMountFailedException");
			ReplayStrings.stringIDs.Add(2389546758U, "InvalidSavedStateException");
			ReplayStrings.stringIDs.Add(1836563684U, "SeederOperationAborted");
			ReplayStrings.stringIDs.Add(3897089617U, "FailedToOpenShipLogContextInvalidParameter");
			ReplayStrings.stringIDs.Add(2203487748U, "ErrorDagDoesNotHaveAnyMemberServers");
			ReplayStrings.stringIDs.Add(1450196582U, "ErrorAmInjectedUnknownConfig");
			ReplayStrings.stringIDs.Add(663986635U, "DisableReplayLagOperationName");
		}

		// Token: 0x17000956 RID: 2390
		// (get) Token: 0x06002495 RID: 9365 RVA: 0x000ADAE3 File Offset: 0x000ABCE3
		public static LocalizedString RepairStateClusterIsNotRunning
		{
			get
			{
				return new LocalizedString("RepairStateClusterIsNotRunning", "", false, false, ReplayStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06002496 RID: 9366 RVA: 0x000ADB04 File Offset: 0x000ABD04
		public static LocalizedString SeederFailedToCreateDirectory(string directory, string error)
		{
			return new LocalizedString("SeederFailedToCreateDirectory", "Ex37435E", false, true, ReplayStrings.ResourceManager, new object[]
			{
				directory,
				error
			});
		}

		// Token: 0x06002497 RID: 9367 RVA: 0x000ADB38 File Offset: 0x000ABD38
		public static LocalizedString RepairStateLocalServerIsNotInDag(string serverName)
		{
			return new LocalizedString("RepairStateLocalServerIsNotInDag", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				serverName
			});
		}

		// Token: 0x06002498 RID: 9368 RVA: 0x000ADB68 File Offset: 0x000ABD68
		public static LocalizedString PagePatchFileDeletionException(string file, string error)
		{
			return new LocalizedString("PagePatchFileDeletionException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				file,
				error
			});
		}

		// Token: 0x17000957 RID: 2391
		// (get) Token: 0x06002499 RID: 9369 RVA: 0x000ADB9B File Offset: 0x000ABD9B
		public static LocalizedString RemoveLogReasonE00OutOfDate
		{
			get
			{
				return new LocalizedString("RemoveLogReasonE00OutOfDate", "", false, false, ReplayStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600249A RID: 9370 RVA: 0x000ADBBC File Offset: 0x000ABDBC
		public static LocalizedString AcllLastLogNotFoundException(string dbCopy, long generation)
		{
			return new LocalizedString("AcllLastLogNotFoundException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				dbCopy,
				generation
			});
		}

		// Token: 0x0600249B RID: 9371 RVA: 0x000ADBF4 File Offset: 0x000ABDF4
		public static LocalizedString ReplayServiceResumeInvalidDuringMoveException(string dbCopy)
		{
			return new LocalizedString("ReplayServiceResumeInvalidDuringMoveException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				dbCopy
			});
		}

		// Token: 0x0600249C RID: 9372 RVA: 0x000ADC24 File Offset: 0x000ABE24
		public static LocalizedString DumpsterRedeliveryException(string errorMsg)
		{
			return new LocalizedString("DumpsterRedeliveryException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				errorMsg
			});
		}

		// Token: 0x0600249D RID: 9373 RVA: 0x000ADC54 File Offset: 0x000ABE54
		public static LocalizedString LastLogReplacementFileNotSubsetException(string dbCopy, string subsetFile, string superSetFile)
		{
			return new LocalizedString("LastLogReplacementFileNotSubsetException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				dbCopy,
				subsetFile,
				superSetFile
			});
		}

		// Token: 0x0600249E RID: 9374 RVA: 0x000ADC8C File Offset: 0x000ABE8C
		public static LocalizedString ReplayServiceRestartInvalidSeedingException(string dbCopy)
		{
			return new LocalizedString("ReplayServiceRestartInvalidSeedingException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				dbCopy
			});
		}

		// Token: 0x0600249F RID: 9375 RVA: 0x000ADCBC File Offset: 0x000ABEBC
		public static LocalizedString DagTaskComponentManagerGenericFailure(int error)
		{
			return new LocalizedString("DagTaskComponentManagerGenericFailure", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				error
			});
		}

		// Token: 0x060024A0 RID: 9376 RVA: 0x000ADCF0 File Offset: 0x000ABEF0
		public static LocalizedString DumpsterCouldNotFindHubServerException(string hubServerName)
		{
			return new LocalizedString("DumpsterCouldNotFindHubServerException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				hubServerName
			});
		}

		// Token: 0x060024A1 RID: 9377 RVA: 0x000ADD20 File Offset: 0x000ABF20
		public static LocalizedString LastLogReplacementRollbackFailedException(string dbCopy, string error)
		{
			return new LocalizedString("LastLogReplacementRollbackFailedException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				dbCopy,
				error
			});
		}

		// Token: 0x060024A2 RID: 9378 RVA: 0x000ADD54 File Offset: 0x000ABF54
		public static LocalizedString AmPreMountCallbackFailedMountInhibitException(string dbName, string server, string errMsg)
		{
			return new LocalizedString("AmPreMountCallbackFailedMountInhibitException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				dbName,
				server,
				errMsg
			});
		}

		// Token: 0x17000958 RID: 2392
		// (get) Token: 0x060024A3 RID: 9379 RVA: 0x000ADD8B File Offset: 0x000ABF8B
		public static LocalizedString FailedToOpenShipLogContextAccessDenied
		{
			get
			{
				return new LocalizedString("FailedToOpenShipLogContextAccessDenied", "", false, false, ReplayStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060024A4 RID: 9380 RVA: 0x000ADDAC File Offset: 0x000ABFAC
		public static LocalizedString InvalidRcrConfigOnNonMailboxException(string nodeName)
		{
			return new LocalizedString("InvalidRcrConfigOnNonMailboxException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				nodeName
			});
		}

		// Token: 0x060024A5 RID: 9381 RVA: 0x000ADDDC File Offset: 0x000ABFDC
		public static LocalizedString SeederEcLogAlreadyExist(string directory)
		{
			return new LocalizedString("SeederEcLogAlreadyExist", "ExF56313", false, true, ReplayStrings.ResourceManager, new object[]
			{
				directory
			});
		}

		// Token: 0x060024A6 RID: 9382 RVA: 0x000ADE0C File Offset: 0x000AC00C
		public static LocalizedString AutoReseedTooManyConcurrentSeeds(int maxSeedsLimit)
		{
			return new LocalizedString("AutoReseedTooManyConcurrentSeeds", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				maxSeedsLimit
			});
		}

		// Token: 0x060024A7 RID: 9383 RVA: 0x000ADE40 File Offset: 0x000AC040
		public static LocalizedString FileCheckAccessDeniedDismountFailedException(string file, string dismountError)
		{
			return new LocalizedString("FileCheckAccessDeniedDismountFailedException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				file,
				dismountError
			});
		}

		// Token: 0x17000959 RID: 2393
		// (get) Token: 0x060024A8 RID: 9384 RVA: 0x000ADE73 File Offset: 0x000AC073
		public static LocalizedString ReplayServiceRpcUnknownInstanceException
		{
			get
			{
				return new LocalizedString("ReplayServiceRpcUnknownInstanceException", "", false, false, ReplayStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060024A9 RID: 9385 RVA: 0x000ADE94 File Offset: 0x000AC094
		public static LocalizedString GranularReplicationInitFailed(string reason)
		{
			return new LocalizedString("GranularReplicationInitFailed", "Ex4A3FF0", false, true, ReplayStrings.ResourceManager, new object[]
			{
				reason
			});
		}

		// Token: 0x060024AA RID: 9386 RVA: 0x000ADEC4 File Offset: 0x000AC0C4
		public static LocalizedString AmDbNotMountedMultipleServersException(string dbName, string detailedMsg)
		{
			return new LocalizedString("AmDbNotMountedMultipleServersException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				dbName,
				detailedMsg
			});
		}

		// Token: 0x1700095A RID: 2394
		// (get) Token: 0x060024AB RID: 9387 RVA: 0x000ADEF7 File Offset: 0x000AC0F7
		public static LocalizedString LockOwnerIdle
		{
			get
			{
				return new LocalizedString("LockOwnerIdle", "Ex9D8503", false, true, ReplayStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060024AC RID: 9388 RVA: 0x000ADF18 File Offset: 0x000AC118
		public static LocalizedString FailedToGetCopyStatus(string server, string db)
		{
			return new LocalizedString("FailedToGetCopyStatus", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				server,
				db
			});
		}

		// Token: 0x060024AD RID: 9389 RVA: 0x000ADF4C File Offset: 0x000AC14C
		public static LocalizedString DisableReplayLagWriteFailedException(string dbCopy)
		{
			return new LocalizedString("DisableReplayLagWriteFailedException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				dbCopy
			});
		}

		// Token: 0x060024AE RID: 9390 RVA: 0x000ADF7C File Offset: 0x000AC17C
		public static LocalizedString TargetDbNotThere(string path)
		{
			return new LocalizedString("TargetDbNotThere", "ExE14333", false, true, ReplayStrings.ResourceManager, new object[]
			{
				path
			});
		}

		// Token: 0x060024AF RID: 9391 RVA: 0x000ADFAC File Offset: 0x000AC1AC
		public static LocalizedString CiSeederExchangeSearchTransientException(string message)
		{
			return new LocalizedString("CiSeederExchangeSearchTransientException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				message
			});
		}

		// Token: 0x060024B0 RID: 9392 RVA: 0x000ADFDC File Offset: 0x000AC1DC
		public static LocalizedString PotentialRedundancyValidationDBReplicationStalled(string dbName, int totalPassiveCopiesCount, string detailedMsg)
		{
			return new LocalizedString("PotentialRedundancyValidationDBReplicationStalled", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				dbName,
				totalPassiveCopiesCount,
				detailedMsg
			});
		}

		// Token: 0x060024B1 RID: 9393 RVA: 0x000AE018 File Offset: 0x000AC218
		public static LocalizedString LastLogReplacementTempOldFileNotDeletedException(string dbCopy, string tempOldFile, string error)
		{
			return new LocalizedString("LastLogReplacementTempOldFileNotDeletedException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				dbCopy,
				tempOldFile,
				error
			});
		}

		// Token: 0x060024B2 RID: 9394 RVA: 0x000AE050 File Offset: 0x000AC250
		public static LocalizedString PreserveInspectorLogsError(string errorText)
		{
			return new LocalizedString("PreserveInspectorLogsError", "Ex304365", false, true, ReplayStrings.ResourceManager, new object[]
			{
				errorText
			});
		}

		// Token: 0x060024B3 RID: 9395 RVA: 0x000AE080 File Offset: 0x000AC280
		public static LocalizedString ServerStopped(string server)
		{
			return new LocalizedString("ServerStopped", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				server
			});
		}

		// Token: 0x060024B4 RID: 9396 RVA: 0x000AE0B0 File Offset: 0x000AC2B0
		public static LocalizedString SeederInstanceAlreadyAddedException(string sourceMachine)
		{
			return new LocalizedString("SeederInstanceAlreadyAddedException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				sourceMachine
			});
		}

		// Token: 0x060024B5 RID: 9397 RVA: 0x000AE0E0 File Offset: 0x000AC2E0
		public static LocalizedString AutoReseedAllCatalogFailed(string databaseName)
		{
			return new LocalizedString("AutoReseedAllCatalogFailed", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				databaseName
			});
		}

		// Token: 0x060024B6 RID: 9398 RVA: 0x000AE110 File Offset: 0x000AC310
		public static LocalizedString LogCopierE00InconsistentError(long e00Gen, long expectedGen)
		{
			return new LocalizedString("LogCopierE00InconsistentError", "Ex17629C", false, true, ReplayStrings.ResourceManager, new object[]
			{
				e00Gen,
				expectedGen
			});
		}

		// Token: 0x060024B7 RID: 9399 RVA: 0x000AE150 File Offset: 0x000AC350
		public static LocalizedString FailedToDisableMountPointConfigurationException(string regkeyroot)
		{
			return new LocalizedString("FailedToDisableMountPointConfigurationException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				regkeyroot
			});
		}

		// Token: 0x060024B8 RID: 9400 RVA: 0x000AE180 File Offset: 0x000AC380
		public static LocalizedString AcllFailedLogDivergenceDetected(string dbCopy, string sourceServer)
		{
			return new LocalizedString("AcllFailedLogDivergenceDetected", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				dbCopy,
				sourceServer
			});
		}

		// Token: 0x1700095B RID: 2395
		// (get) Token: 0x060024B9 RID: 9401 RVA: 0x000AE1B3 File Offset: 0x000AC3B3
		public static LocalizedString IncSeedNotSupportedWithShrinkDatabaseError
		{
			get
			{
				return new LocalizedString("IncSeedNotSupportedWithShrinkDatabaseError", "", false, false, ReplayStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060024BA RID: 9402 RVA: 0x000AE1D4 File Offset: 0x000AC3D4
		public static LocalizedString AutoReseedFailedCopyWorkflowSuspendedCopy(string failedSuppressionDuration)
		{
			return new LocalizedString("AutoReseedFailedCopyWorkflowSuspendedCopy", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				failedSuppressionDuration
			});
		}

		// Token: 0x060024BB RID: 9403 RVA: 0x000AE204 File Offset: 0x000AC404
		public static LocalizedString CiSeederSearchCatalogRpcPermanentException(string message)
		{
			return new LocalizedString("CiSeederSearchCatalogRpcPermanentException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				message
			});
		}

		// Token: 0x060024BC RID: 9404 RVA: 0x000AE234 File Offset: 0x000AC434
		public static LocalizedString InvalidRCROperationOnNonRcrDB(string dbName)
		{
			return new LocalizedString("InvalidRCROperationOnNonRcrDB", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				dbName
			});
		}

		// Token: 0x060024BD RID: 9405 RVA: 0x000AE264 File Offset: 0x000AC464
		public static LocalizedString CouldNotDeleteDbMountPointException(string database, string dbMountPoint, string errMsg)
		{
			return new LocalizedString("CouldNotDeleteDbMountPointException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				database,
				dbMountPoint,
				errMsg
			});
		}

		// Token: 0x1700095C RID: 2396
		// (get) Token: 0x060024BE RID: 9406 RVA: 0x000AE29B File Offset: 0x000AC49B
		public static LocalizedString CantStartFromCommandLineTitle
		{
			get
			{
				return new LocalizedString("CantStartFromCommandLineTitle", "ExC1715A", false, true, ReplayStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060024BF RID: 9407 RVA: 0x000AE2BC File Offset: 0x000AC4BC
		public static LocalizedString AutoReseedCatalogActiveException(string databaseName, string serverName)
		{
			return new LocalizedString("AutoReseedCatalogActiveException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				databaseName,
				serverName
			});
		}

		// Token: 0x060024C0 RID: 9408 RVA: 0x000AE2F0 File Offset: 0x000AC4F0
		public static LocalizedString SafeDeleteExistingFilesDataRedundancyNoResultException(string db)
		{
			return new LocalizedString("SafeDeleteExistingFilesDataRedundancyNoResultException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				db
			});
		}

		// Token: 0x1700095D RID: 2397
		// (get) Token: 0x060024C1 RID: 9409 RVA: 0x000AE31F File Offset: 0x000AC51F
		public static LocalizedString AmBcsDatabaseHasNoCopies
		{
			get
			{
				return new LocalizedString("AmBcsDatabaseHasNoCopies", "Ex479B43", false, true, ReplayStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060024C2 RID: 9410 RVA: 0x000AE340 File Offset: 0x000AC540
		public static LocalizedString NetworkAddressResolutionFailed(string nodeName, string errMsg)
		{
			return new LocalizedString("NetworkAddressResolutionFailed", "Ex01C78B", false, true, ReplayStrings.ResourceManager, new object[]
			{
				nodeName,
				errMsg
			});
		}

		// Token: 0x1700095E RID: 2398
		// (get) Token: 0x060024C3 RID: 9411 RVA: 0x000AE373 File Offset: 0x000AC573
		public static LocalizedString ErrorCouldNotFindClusterGroupOwnerNodeForAmConfig
		{
			get
			{
				return new LocalizedString("ErrorCouldNotFindClusterGroupOwnerNodeForAmConfig", "Ex6C1CEC", false, true, ReplayStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060024C4 RID: 9412 RVA: 0x000AE394 File Offset: 0x000AC594
		public static LocalizedString FileCheckDatabaseLogfileSignature(string database, string logfileSignature, string expectedSignature)
		{
			return new LocalizedString("FileCheckDatabaseLogfileSignature", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				database,
				logfileSignature,
				expectedSignature
			});
		}

		// Token: 0x060024C5 RID: 9413 RVA: 0x000AE3CC File Offset: 0x000AC5CC
		public static LocalizedString AcllCopyStatusResumeBlockedException(string dbCopy, string errorMsg)
		{
			return new LocalizedString("AcllCopyStatusResumeBlockedException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				dbCopy,
				errorMsg
			});
		}

		// Token: 0x060024C6 RID: 9414 RVA: 0x000AE400 File Offset: 0x000AC600
		public static LocalizedString CiServiceDownException(string serverName, string rpcErrorMessage)
		{
			return new LocalizedString("CiServiceDownException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				serverName,
				rpcErrorMessage
			});
		}

		// Token: 0x1700095F RID: 2399
		// (get) Token: 0x060024C7 RID: 9415 RVA: 0x000AE433 File Offset: 0x000AC633
		public static LocalizedString SeederEcDBNotFound
		{
			get
			{
				return new LocalizedString("SeederEcDBNotFound", "Ex6D88D5", false, true, ReplayStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060024C8 RID: 9416 RVA: 0x000AE454 File Offset: 0x000AC654
		public static LocalizedString AmBcsDatabaseCopyIsSeedingSource(string db, string server)
		{
			return new LocalizedString("AmBcsDatabaseCopyIsSeedingSource", "Ex2A550B", false, true, ReplayStrings.ResourceManager, new object[]
			{
				db,
				server
			});
		}

		// Token: 0x060024C9 RID: 9417 RVA: 0x000AE488 File Offset: 0x000AC688
		public static LocalizedString FailedToDeleteTempDatabase(string db, string error)
		{
			return new LocalizedString("FailedToDeleteTempDatabase", "Ex6FBC6C", false, true, ReplayStrings.ResourceManager, new object[]
			{
				db,
				error
			});
		}

		// Token: 0x060024CA RID: 9418 RVA: 0x000AE4BC File Offset: 0x000AC6BC
		public static LocalizedString SeederFailedToInspectLogException(string logfileName, string inspectionError)
		{
			return new LocalizedString("SeederFailedToInspectLogException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				logfileName,
				inspectionError
			});
		}

		// Token: 0x060024CB RID: 9419 RVA: 0x000AE4F0 File Offset: 0x000AC6F0
		public static LocalizedString ClusterBatchWriter_CommitFailed(int ec)
		{
			return new LocalizedString("ClusterBatchWriter_CommitFailed", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				ec
			});
		}

		// Token: 0x060024CC RID: 9420 RVA: 0x000AE524 File Offset: 0x000AC724
		public static LocalizedString DbAvailabilityActiveCopyUnknownState(string dbName, string serverName, string copyStatus)
		{
			return new LocalizedString("DbAvailabilityActiveCopyUnknownState", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				dbName,
				serverName,
				copyStatus
			});
		}

		// Token: 0x060024CD RID: 9421 RVA: 0x000AE55C File Offset: 0x000AC75C
		public static LocalizedString AmBcsSelectionException(string bcsMessage)
		{
			return new LocalizedString("AmBcsSelectionException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				bcsMessage
			});
		}

		// Token: 0x060024CE RID: 9422 RVA: 0x000AE58C File Offset: 0x000AC78C
		public static LocalizedString TargetChkptAlreadyExists(string chkFile)
		{
			return new LocalizedString("TargetChkptAlreadyExists", "Ex9D81E3", false, true, ReplayStrings.ResourceManager, new object[]
			{
				chkFile
			});
		}

		// Token: 0x060024CF RID: 9423 RVA: 0x000AE5BC File Offset: 0x000AC7BC
		public static LocalizedString DatabaseDismountOrKillStoreException(string databaseName, string serverName, string errorText)
		{
			return new LocalizedString("DatabaseDismountOrKillStoreException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				databaseName,
				serverName,
				errorText
			});
		}

		// Token: 0x060024D0 RID: 9424 RVA: 0x000AE5F4 File Offset: 0x000AC7F4
		public static LocalizedString LogTruncationException(string error)
		{
			return new LocalizedString("LogTruncationException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				error
			});
		}

		// Token: 0x060024D1 RID: 9425 RVA: 0x000AE624 File Offset: 0x000AC824
		public static LocalizedString SeederFailedToFindDirectoriesUnderMountPoint(string name)
		{
			return new LocalizedString("SeederFailedToFindDirectoriesUnderMountPoint", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				name
			});
		}

		// Token: 0x060024D2 RID: 9426 RVA: 0x000AE654 File Offset: 0x000AC854
		public static LocalizedString AmDbActionRejectedMountAtStartupNotEnabledException(string actionCode)
		{
			return new LocalizedString("AmDbActionRejectedMountAtStartupNotEnabledException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				actionCode
			});
		}

		// Token: 0x060024D3 RID: 9427 RVA: 0x000AE684 File Offset: 0x000AC884
		public static LocalizedString DagNetworkRpcServerError(string rpcName, string errMsg)
		{
			return new LocalizedString("DagNetworkRpcServerError", "ExCE1A30", false, true, ReplayStrings.ResourceManager, new object[]
			{
				rpcName,
				errMsg
			});
		}

		// Token: 0x060024D4 RID: 9428 RVA: 0x000AE6B8 File Offset: 0x000AC8B8
		public static LocalizedString AmDbMoveOperationNotSupportedException(string dbName)
		{
			return new LocalizedString("AmDbMoveOperationNotSupportedException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				dbName
			});
		}

		// Token: 0x060024D5 RID: 9429 RVA: 0x000AE6E8 File Offset: 0x000AC8E8
		public static LocalizedString FailedToDeserializeStr(string stringToDeserialize, string typeName)
		{
			return new LocalizedString("FailedToDeserializeStr", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				stringToDeserialize,
				typeName
			});
		}

		// Token: 0x17000960 RID: 2400
		// (get) Token: 0x060024D6 RID: 9430 RVA: 0x000AE71B File Offset: 0x000AC91B
		public static LocalizedString SeederEcSeedingCancelled
		{
			get
			{
				return new LocalizedString("SeederEcSeedingCancelled", "Ex5D30C7", false, true, ReplayStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000961 RID: 2401
		// (get) Token: 0x060024D7 RID: 9431 RVA: 0x000AE739 File Offset: 0x000AC939
		public static LocalizedString DagTaskInstalledFailoverClustering
		{
			get
			{
				return new LocalizedString("DagTaskInstalledFailoverClustering", "", false, false, ReplayStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000962 RID: 2402
		// (get) Token: 0x060024D8 RID: 9432 RVA: 0x000AE757 File Offset: 0x000AC957
		public static LocalizedString SeederEcDeviceNotReady
		{
			get
			{
				return new LocalizedString("SeederEcDeviceNotReady", "Ex476112", false, true, ReplayStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060024D9 RID: 9433 RVA: 0x000AE778 File Offset: 0x000AC978
		public static LocalizedString AmBcsDatabaseCopyCatalogUnhealthy(string db, string server, string catalogState)
		{
			return new LocalizedString("AmBcsDatabaseCopyCatalogUnhealthy", "Ex2173EF", false, true, ReplayStrings.ResourceManager, new object[]
			{
				db,
				server,
				catalogState
			});
		}

		// Token: 0x060024DA RID: 9434 RVA: 0x000AE7B0 File Offset: 0x000AC9B0
		public static LocalizedString LogRepairFailedToVerifyFromActive(string tempLogName, string activeServerName, string exceptionText)
		{
			return new LocalizedString("LogRepairFailedToVerifyFromActive", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				tempLogName,
				activeServerName,
				exceptionText
			});
		}

		// Token: 0x060024DB RID: 9435 RVA: 0x000AE7E8 File Offset: 0x000AC9E8
		public static LocalizedString SafetyNetVersionCheckException(string error)
		{
			return new LocalizedString("SafetyNetVersionCheckException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				error
			});
		}

		// Token: 0x060024DC RID: 9436 RVA: 0x000AE818 File Offset: 0x000ACA18
		public static LocalizedString SuspendCommentTooLong(int length, int limit)
		{
			return new LocalizedString("SuspendCommentTooLong", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				length,
				limit
			});
		}

		// Token: 0x060024DD RID: 9437 RVA: 0x000AE858 File Offset: 0x000ACA58
		public static LocalizedString AmBcsDatabaseCopyTotalQueueLengthTooHigh(string db, string server, long length, long maxLength)
		{
			return new LocalizedString("AmBcsDatabaseCopyTotalQueueLengthTooHigh", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				db,
				server,
				length,
				maxLength
			});
		}

		// Token: 0x060024DE RID: 9438 RVA: 0x000AE8A0 File Offset: 0x000ACAA0
		public static LocalizedString SeederOperationFailedException(string errMessage)
		{
			return new LocalizedString("SeederOperationFailedException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				errMessage
			});
		}

		// Token: 0x17000963 RID: 2403
		// (get) Token: 0x060024DF RID: 9439 RVA: 0x000AE8CF File Offset: 0x000ACACF
		public static LocalizedString ErrorReadingDagServerForAmConfig
		{
			get
			{
				return new LocalizedString("ErrorReadingDagServerForAmConfig", "Ex691995", false, true, ReplayStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060024E0 RID: 9440 RVA: 0x000AE8F0 File Offset: 0x000ACAF0
		public static LocalizedString AutoReseedInvalidLogFolderPath(string actualPath, string expectedPath)
		{
			return new LocalizedString("AutoReseedInvalidLogFolderPath", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				actualPath,
				expectedPath
			});
		}

		// Token: 0x060024E1 RID: 9441 RVA: 0x000AE924 File Offset: 0x000ACB24
		public static LocalizedString AmBcsDatabaseCopyAlreadyTried(string db, string server)
		{
			return new LocalizedString("AmBcsDatabaseCopyAlreadyTried", "Ex9CF852", false, true, ReplayStrings.ResourceManager, new object[]
			{
				db,
				server
			});
		}

		// Token: 0x060024E2 RID: 9442 RVA: 0x000AE958 File Offset: 0x000ACB58
		public static LocalizedString InsufficientSparesForExtraCopiesException(int spares, int copies)
		{
			return new LocalizedString("InsufficientSparesForExtraCopiesException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				spares,
				copies
			});
		}

		// Token: 0x060024E3 RID: 9443 RVA: 0x000AE998 File Offset: 0x000ACB98
		public static LocalizedString AcllCouldNotControlReplicaInstanceException(string dbCopy)
		{
			return new LocalizedString("AcllCouldNotControlReplicaInstanceException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				dbCopy
			});
		}

		// Token: 0x17000964 RID: 2404
		// (get) Token: 0x060024E4 RID: 9444 RVA: 0x000AE9C7 File Offset: 0x000ACBC7
		public static LocalizedString NetworkReadEOF
		{
			get
			{
				return new LocalizedString("NetworkReadEOF", "ExC387A2", false, true, ReplayStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060024E5 RID: 9445 RVA: 0x000AE9E8 File Offset: 0x000ACBE8
		public static LocalizedString DagTaskClusteringMustBeInstalledException(string serverName)
		{
			return new LocalizedString("DagTaskClusteringMustBeInstalledException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				serverName
			});
		}

		// Token: 0x060024E6 RID: 9446 RVA: 0x000AEA18 File Offset: 0x000ACC18
		public static LocalizedString DagTaskRemoveNodeNotUpException(string machineName, string clusterName, string machineState)
		{
			return new LocalizedString("DagTaskRemoveNodeNotUpException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				machineName,
				clusterName,
				machineState
			});
		}

		// Token: 0x060024E7 RID: 9447 RVA: 0x000AEA50 File Offset: 0x000ACC50
		public static LocalizedString DagTaskComponentManagerServerManagerPSFailure(string error)
		{
			return new LocalizedString("DagTaskComponentManagerServerManagerPSFailure", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				error
			});
		}

		// Token: 0x060024E8 RID: 9448 RVA: 0x000AEA80 File Offset: 0x000ACC80
		public static LocalizedString CouldNotFindDatabase(string dbGuid, string error)
		{
			return new LocalizedString("CouldNotFindDatabase", "Ex2324E2", false, true, ReplayStrings.ResourceManager, new object[]
			{
				dbGuid,
				error
			});
		}

		// Token: 0x060024E9 RID: 9449 RVA: 0x000AEAB4 File Offset: 0x000ACCB4
		public static LocalizedString DbMoveSkippedBecauseNotFoundInClusDb(string dbName)
		{
			return new LocalizedString("DbMoveSkippedBecauseNotFoundInClusDb", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				dbName
			});
		}

		// Token: 0x060024EA RID: 9450 RVA: 0x000AEAE4 File Offset: 0x000ACCE4
		public static LocalizedString DagTaskSetDagNeedsAllNodesUpToChangeQuorumException(string machineNames)
		{
			return new LocalizedString("DagTaskSetDagNeedsAllNodesUpToChangeQuorumException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				machineNames
			});
		}

		// Token: 0x17000965 RID: 2405
		// (get) Token: 0x060024EB RID: 9451 RVA: 0x000AEB13 File Offset: 0x000ACD13
		public static LocalizedString RemoveLogReasonFailedInspection
		{
			get
			{
				return new LocalizedString("RemoveLogReasonFailedInspection", "", false, false, ReplayStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060024EC RID: 9452 RVA: 0x000AEB34 File Offset: 0x000ACD34
		public static LocalizedString AmInvalidActionCodeException(int actionCode, string member, string value)
		{
			return new LocalizedString("AmInvalidActionCodeException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				actionCode,
				member,
				value
			});
		}

		// Token: 0x060024ED RID: 9453 RVA: 0x000AEB70 File Offset: 0x000ACD70
		public static LocalizedString ReplayConfigNotFoundException(string dbName, string serverName)
		{
			return new LocalizedString("ReplayConfigNotFoundException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				dbName,
				serverName
			});
		}

		// Token: 0x17000966 RID: 2406
		// (get) Token: 0x060024EE RID: 9454 RVA: 0x000AEBA3 File Offset: 0x000ACDA3
		public static LocalizedString InvalidLogCopyResponse
		{
			get
			{
				return new LocalizedString("InvalidLogCopyResponse", "ExD7CFAC", false, true, ReplayStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060024EF RID: 9455 RVA: 0x000AEBC4 File Offset: 0x000ACDC4
		public static LocalizedString DatabaseLogFoldersNotUnderMountpoint(string name)
		{
			return new LocalizedString("DatabaseLogFoldersNotUnderMountpoint", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				name
			});
		}

		// Token: 0x060024F0 RID: 9456 RVA: 0x000AEBF4 File Offset: 0x000ACDF4
		public static LocalizedString ReplayServiceTooMuchMemoryNoDumpException(double memoryUsageInMib, long maximumMemoryUsageInMib, string enableWatsonRegKey)
		{
			return new LocalizedString("ReplayServiceTooMuchMemoryNoDumpException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				memoryUsageInMib,
				maximumMemoryUsageInMib,
				enableWatsonRegKey
			});
		}

		// Token: 0x060024F1 RID: 9457 RVA: 0x000AEC38 File Offset: 0x000ACE38
		public static LocalizedString NetworkNameNotFound(string netName)
		{
			return new LocalizedString("NetworkNameNotFound", "Ex6D44A7", false, true, ReplayStrings.ResourceManager, new object[]
			{
				netName
			});
		}

		// Token: 0x060024F2 RID: 9458 RVA: 0x000AEC68 File Offset: 0x000ACE68
		public static LocalizedString AmMountCallbackFailedWithDBFolderNotUnderMountPointException(string dbName, string error)
		{
			return new LocalizedString("AmMountCallbackFailedWithDBFolderNotUnderMountPointException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				dbName,
				error
			});
		}

		// Token: 0x17000967 RID: 2407
		// (get) Token: 0x060024F3 RID: 9459 RVA: 0x000AEC9B File Offset: 0x000ACE9B
		public static LocalizedString MonitoringADInitNotCompleteException
		{
			get
			{
				return new LocalizedString("MonitoringADInitNotCompleteException", "", false, false, ReplayStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000968 RID: 2408
		// (get) Token: 0x060024F4 RID: 9460 RVA: 0x000AECB9 File Offset: 0x000ACEB9
		public static LocalizedString AutoReseedFailedToFindVolumeName
		{
			get
			{
				return new LocalizedString("AutoReseedFailedToFindVolumeName", "", false, false, ReplayStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060024F5 RID: 9461 RVA: 0x000AECD8 File Offset: 0x000ACED8
		public static LocalizedString RegistryParameterReadException(string valueName, string errMsg)
		{
			return new LocalizedString("RegistryParameterReadException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				valueName,
				errMsg
			});
		}

		// Token: 0x060024F6 RID: 9462 RVA: 0x000AED0C File Offset: 0x000ACF0C
		public static LocalizedString IncrementalReseedFailedException(string msg, uint error)
		{
			return new LocalizedString("IncrementalReseedFailedException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				msg,
				error
			});
		}

		// Token: 0x17000969 RID: 2409
		// (get) Token: 0x060024F7 RID: 9463 RVA: 0x000AED44 File Offset: 0x000ACF44
		public static LocalizedString DagTaskComponentManagerAnotherInstanceRunning
		{
			get
			{
				return new LocalizedString("DagTaskComponentManagerAnotherInstanceRunning", "", false, false, ReplayStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700096A RID: 2410
		// (get) Token: 0x060024F8 RID: 9464 RVA: 0x000AED62 File Offset: 0x000ACF62
		public static LocalizedString AutoReseedManualReseedLaunched
		{
			get
			{
				return new LocalizedString("AutoReseedManualReseedLaunched", "", false, false, ReplayStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060024F9 RID: 9465 RVA: 0x000AED80 File Offset: 0x000ACF80
		public static LocalizedString FailedToNotifySourceLogTrunc(string dbSrc, uint hresult, string optionalFriendlyError)
		{
			return new LocalizedString("FailedToNotifySourceLogTrunc", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				dbSrc,
				hresult,
				optionalFriendlyError
			});
		}

		// Token: 0x060024FA RID: 9466 RVA: 0x000AEDBC File Offset: 0x000ACFBC
		public static LocalizedString FileCheckInvalidDatabaseState(string database, string state)
		{
			return new LocalizedString("FileCheckInvalidDatabaseState", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				database,
				state
			});
		}

		// Token: 0x060024FB RID: 9467 RVA: 0x000AEDF0 File Offset: 0x000ACFF0
		public static LocalizedString SeederFailedToDeleteCheckpoint(string file, string error)
		{
			return new LocalizedString("SeederFailedToDeleteCheckpoint", "Ex71D111", false, true, ReplayStrings.ResourceManager, new object[]
			{
				file,
				error
			});
		}

		// Token: 0x1700096B RID: 2411
		// (get) Token: 0x060024FC RID: 9468 RVA: 0x000AEE23 File Offset: 0x000AD023
		public static LocalizedString SeederEcNoOnlineEdb
		{
			get
			{
				return new LocalizedString("SeederEcNoOnlineEdb", "ExC73175", false, true, ReplayStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060024FD RID: 9469 RVA: 0x000AEE44 File Offset: 0x000AD044
		public static LocalizedString LogRepairNotPossibleActiveIsDivergent(string activeServerName)
		{
			return new LocalizedString("LogRepairNotPossibleActiveIsDivergent", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				activeServerName
			});
		}

		// Token: 0x1700096C RID: 2412
		// (get) Token: 0x060024FE RID: 9470 RVA: 0x000AEE73 File Offset: 0x000AD073
		public static LocalizedString FullServerSeedInProgressException
		{
			get
			{
				return new LocalizedString("FullServerSeedInProgressException", "", false, false, ReplayStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060024FF RID: 9471 RVA: 0x000AEE94 File Offset: 0x000AD094
		public static LocalizedString FailedToOpenBackupFileHandle(string databaseSource, string serverSrc, int ec, string errorMessage)
		{
			return new LocalizedString("FailedToOpenBackupFileHandle", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				databaseSource,
				serverSrc,
				ec,
				errorMessage
			});
		}

		// Token: 0x1700096D RID: 2413
		// (get) Token: 0x06002500 RID: 9472 RVA: 0x000AEED4 File Offset: 0x000AD0D4
		public static LocalizedString ReplayServiceSuspendCommentException
		{
			get
			{
				return new LocalizedString("ReplayServiceSuspendCommentException", "", false, false, ReplayStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06002501 RID: 9473 RVA: 0x000AEEF4 File Offset: 0x000AD0F4
		public static LocalizedString DeleteChkptReasonTooFarBehindAndLogMissing(long checkpointGeneration)
		{
			return new LocalizedString("DeleteChkptReasonTooFarBehindAndLogMissing", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				checkpointGeneration
			});
		}

		// Token: 0x06002502 RID: 9474 RVA: 0x000AEF28 File Offset: 0x000AD128
		public static LocalizedString NetworkNotUsable(string netName, string nodeName, string reason)
		{
			return new LocalizedString("NetworkNotUsable", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				netName,
				nodeName,
				reason
			});
		}

		// Token: 0x06002503 RID: 9475 RVA: 0x000AEF60 File Offset: 0x000AD160
		public static LocalizedString AutoReseedWorkflowNotSupportedOnTPR(string dagName)
		{
			return new LocalizedString("AutoReseedWorkflowNotSupportedOnTPR", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				dagName
			});
		}

		// Token: 0x06002504 RID: 9476 RVA: 0x000AEF90 File Offset: 0x000AD190
		public static LocalizedString ReplayLagRpcUnsupportedException(string serverName, string serverVersion, string supportedVersion)
		{
			return new LocalizedString("ReplayLagRpcUnsupportedException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				serverName,
				serverVersion,
				supportedVersion
			});
		}

		// Token: 0x06002505 RID: 9477 RVA: 0x000AEFC8 File Offset: 0x000AD1C8
		public static LocalizedString RepairStateDatabaseShouldBeDismounted(string dbName, string mountedServer)
		{
			return new LocalizedString("RepairStateDatabaseShouldBeDismounted", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				dbName,
				mountedServer
			});
		}

		// Token: 0x06002506 RID: 9478 RVA: 0x000AEFFC File Offset: 0x000AD1FC
		public static LocalizedString RegistryParameterKeyNotOpenedException(string keyName)
		{
			return new LocalizedString("RegistryParameterKeyNotOpenedException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				keyName
			});
		}

		// Token: 0x06002507 RID: 9479 RVA: 0x000AF02C File Offset: 0x000AD22C
		public static LocalizedString DagTaskServerException(string errorMessage)
		{
			return new LocalizedString("DagTaskServerException", "ExC9C315", false, true, ReplayStrings.ResourceManager, new object[]
			{
				errorMessage
			});
		}

		// Token: 0x06002508 RID: 9480 RVA: 0x000AF05C File Offset: 0x000AD25C
		public static LocalizedString ReplayServiceRpcCopyStatusTimeoutException(string dbCopyName, int timeoutSecs)
		{
			return new LocalizedString("ReplayServiceRpcCopyStatusTimeoutException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				dbCopyName,
				timeoutSecs
			});
		}

		// Token: 0x06002509 RID: 9481 RVA: 0x000AF094 File Offset: 0x000AD294
		public static LocalizedString AutoReseedTooManyFailedCopies(int numFailedCopies)
		{
			return new LocalizedString("AutoReseedTooManyFailedCopies", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				numFailedCopies
			});
		}

		// Token: 0x0600250A RID: 9482 RVA: 0x000AF0C8 File Offset: 0x000AD2C8
		public static LocalizedString CiSeederCatalogCouldNotDismount(string ex)
		{
			return new LocalizedString("CiSeederCatalogCouldNotDismount", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				ex
			});
		}

		// Token: 0x0600250B RID: 9483 RVA: 0x000AF0F8 File Offset: 0x000AD2F8
		public static LocalizedString CancelSeedingDueToFailed(string id, string machine)
		{
			return new LocalizedString("CancelSeedingDueToFailed", "Ex6D1C26", false, true, ReplayStrings.ResourceManager, new object[]
			{
				id,
				machine
			});
		}

		// Token: 0x0600250C RID: 9484 RVA: 0x000AF12C File Offset: 0x000AD32C
		public static LocalizedString IOBufferPoolLimitError(int limit, int bufSize)
		{
			return new LocalizedString("IOBufferPoolLimitError", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				limit,
				bufSize
			});
		}

		// Token: 0x0600250D RID: 9485 RVA: 0x000AF16C File Offset: 0x000AD36C
		public static LocalizedString AutoReseedInPlaceReseedTooSoon(string failedElapsedTimeStr, string inPlaceDelayTimeString)
		{
			return new LocalizedString("AutoReseedInPlaceReseedTooSoon", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				failedElapsedTimeStr,
				inPlaceDelayTimeString
			});
		}

		// Token: 0x0600250E RID: 9486 RVA: 0x000AF1A0 File Offset: 0x000AD3A0
		public static LocalizedString AmBcsDagNotFoundInAd(string server, string adError)
		{
			return new LocalizedString("AmBcsDagNotFoundInAd", "Ex7E61AD", false, true, ReplayStrings.ResourceManager, new object[]
			{
				server,
				adError
			});
		}

		// Token: 0x0600250F RID: 9487 RVA: 0x000AF1D4 File Offset: 0x000AD3D4
		public static LocalizedString AmBcsDatabaseCopyResynchronizing(string db, string server)
		{
			return new LocalizedString("AmBcsDatabaseCopyResynchronizing", "Ex1B8D35", false, true, ReplayStrings.ResourceManager, new object[]
			{
				db,
				server
			});
		}

		// Token: 0x06002510 RID: 9488 RVA: 0x000AF208 File Offset: 0x000AD408
		public static LocalizedString FoundTooManyVolumesWithSameVolumeLabelException(string volumeNames, string volumeLabel)
		{
			return new LocalizedString("FoundTooManyVolumesWithSameVolumeLabelException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				volumeNames,
				volumeLabel
			});
		}

		// Token: 0x06002511 RID: 9489 RVA: 0x000AF23C File Offset: 0x000AD43C
		public static LocalizedString AmDbMoveOperationOnTimeoutFailureCancelled(string dbName, string fromServer)
		{
			return new LocalizedString("AmDbMoveOperationOnTimeoutFailureCancelled", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				dbName,
				fromServer
			});
		}

		// Token: 0x06002512 RID: 9490 RVA: 0x000AF270 File Offset: 0x000AD470
		public static LocalizedString DbFixupFailedVolumeHasMaxDbMountPointsException(string dbName, string volumeName)
		{
			return new LocalizedString("DbFixupFailedVolumeHasMaxDbMountPointsException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				dbName,
				volumeName
			});
		}

		// Token: 0x06002513 RID: 9491 RVA: 0x000AF2A4 File Offset: 0x000AD4A4
		public static LocalizedString MissingActiveLogRequiredForDivergenceDetection(string file, string sourceServer)
		{
			return new LocalizedString("MissingActiveLogRequiredForDivergenceDetection", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				file,
				sourceServer
			});
		}

		// Token: 0x1700096E RID: 2414
		// (get) Token: 0x06002514 RID: 9492 RVA: 0x000AF2D7 File Offset: 0x000AD4D7
		public static LocalizedString InvalidDbForSeedSpecifiedException
		{
			get
			{
				return new LocalizedString("InvalidDbForSeedSpecifiedException", "", false, false, ReplayStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06002515 RID: 9493 RVA: 0x000AF2F8 File Offset: 0x000AD4F8
		public static LocalizedString ClusterBatchWriter_FailedToReadClusterRegistry(string msg)
		{
			return new LocalizedString("ClusterBatchWriter_FailedToReadClusterRegistry", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				msg
			});
		}

		// Token: 0x06002516 RID: 9494 RVA: 0x000AF328 File Offset: 0x000AD528
		public static LocalizedString AutoReseedCatalogSkipRebuild(string databaseName, string serverName)
		{
			return new LocalizedString("AutoReseedCatalogSkipRebuild", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				databaseName,
				serverName
			});
		}

		// Token: 0x06002517 RID: 9495 RVA: 0x000AF35C File Offset: 0x000AD55C
		public static LocalizedString SeederInstanceAlreadyInProgressException(string sourceMachine)
		{
			return new LocalizedString("SeederInstanceAlreadyInProgressException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				sourceMachine
			});
		}

		// Token: 0x06002518 RID: 9496 RVA: 0x000AF38C File Offset: 0x000AD58C
		public static LocalizedString FileCheckUnableToDeleteCheckpointError(string file, string errorMessage)
		{
			return new LocalizedString("FileCheckUnableToDeleteCheckpointError", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				file,
				errorMessage
			});
		}

		// Token: 0x06002519 RID: 9497 RVA: 0x000AF3C0 File Offset: 0x000AD5C0
		public static LocalizedString AcllInvalidForSingleCopyException(string dbCopy)
		{
			return new LocalizedString("AcllInvalidForSingleCopyException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				dbCopy
			});
		}

		// Token: 0x1700096F RID: 2415
		// (get) Token: 0x0600251A RID: 9498 RVA: 0x000AF3EF File Offset: 0x000AD5EF
		public static LocalizedString VolumeMountPathDoesNotExistException
		{
			get
			{
				return new LocalizedString("VolumeMountPathDoesNotExistException", "", false, false, ReplayStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600251B RID: 9499 RVA: 0x000AF410 File Offset: 0x000AD610
		public static LocalizedString CiSeederRpcOperationFailedException(string errMessage)
		{
			return new LocalizedString("CiSeederRpcOperationFailedException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				errMessage
			});
		}

		// Token: 0x0600251C RID: 9500 RVA: 0x000AF440 File Offset: 0x000AD640
		public static LocalizedString MonitoringADConfigStaleException(string age, string maxTTL, string lastError)
		{
			return new LocalizedString("MonitoringADConfigStaleException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				age,
				maxTTL,
				lastError
			});
		}

		// Token: 0x17000970 RID: 2416
		// (get) Token: 0x0600251D RID: 9501 RVA: 0x000AF477 File Offset: 0x000AD677
		public static LocalizedString AmDbOperationWaitFailedException
		{
			get
			{
				return new LocalizedString("AmDbOperationWaitFailedException", "", false, false, ReplayStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600251E RID: 9502 RVA: 0x000AF498 File Offset: 0x000AD698
		public static LocalizedString EseutilParseError(string line, string regex)
		{
			return new LocalizedString("EseutilParseError", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				line,
				regex
			});
		}

		// Token: 0x17000971 RID: 2417
		// (get) Token: 0x0600251F RID: 9503 RVA: 0x000AF4CB File Offset: 0x000AD6CB
		public static LocalizedString ErrorAmConfigNotInitialized
		{
			get
			{
				return new LocalizedString("ErrorAmConfigNotInitialized", "Ex6447E3", false, true, ReplayStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06002520 RID: 9504 RVA: 0x000AF4EC File Offset: 0x000AD6EC
		public static LocalizedString AmClusterEvictWithoutCleanupException(string nodeName)
		{
			return new LocalizedString("AmClusterEvictWithoutCleanupException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				nodeName
			});
		}

		// Token: 0x06002521 RID: 9505 RVA: 0x000AF51C File Offset: 0x000AD71C
		public static LocalizedString FileSystemCorruptionDetected(string filePath)
		{
			return new LocalizedString("FileSystemCorruptionDetected", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				filePath
			});
		}

		// Token: 0x06002522 RID: 9506 RVA: 0x000AF54C File Offset: 0x000AD74C
		public static LocalizedString DbValidationPassiveCopyUnhealthyState(string dbCopyName, string copyStatus, string errorMessage, string suspendComment)
		{
			return new LocalizedString("DbValidationPassiveCopyUnhealthyState", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				dbCopyName,
				copyStatus,
				errorMessage,
				suspendComment
			});
		}

		// Token: 0x06002523 RID: 9507 RVA: 0x000AF588 File Offset: 0x000AD788
		public static LocalizedString LogCopierE00MissingPrevious(long e00Gen, string filename)
		{
			return new LocalizedString("LogCopierE00MissingPrevious", "Ex87ED89", false, true, ReplayStrings.ResourceManager, new object[]
			{
				e00Gen,
				filename
			});
		}

		// Token: 0x06002524 RID: 9508 RVA: 0x000AF5C0 File Offset: 0x000AD7C0
		public static LocalizedString LogDriveNotBigEnough(string path, long expectedSize, ulong actualSize)
		{
			return new LocalizedString("LogDriveNotBigEnough", "Ex40DD54", false, true, ReplayStrings.ResourceManager, new object[]
			{
				path,
				expectedSize,
				actualSize
			});
		}

		// Token: 0x06002525 RID: 9509 RVA: 0x000AF604 File Offset: 0x000AD804
		public static LocalizedString DagTaskAddingServerToDag(string serverName, string dagName)
		{
			return new LocalizedString("DagTaskAddingServerToDag", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				serverName,
				dagName
			});
		}

		// Token: 0x17000972 RID: 2418
		// (get) Token: 0x06002526 RID: 9510 RVA: 0x000AF637 File Offset: 0x000AD837
		public static LocalizedString NetworkCorruptDataGeneric
		{
			get
			{
				return new LocalizedString("NetworkCorruptDataGeneric", "", false, false, ReplayStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06002527 RID: 9511 RVA: 0x000AF658 File Offset: 0x000AD858
		public static LocalizedString DatabaseNotFound(Guid dbGuid)
		{
			return new LocalizedString("DatabaseNotFound", "ExEBD8A9", false, true, ReplayStrings.ResourceManager, new object[]
			{
				dbGuid
			});
		}

		// Token: 0x17000973 RID: 2419
		// (get) Token: 0x06002528 RID: 9512 RVA: 0x000AF68C File Offset: 0x000AD88C
		public static LocalizedString ErrorAutomountConsensusNotReached
		{
			get
			{
				return new LocalizedString("ErrorAutomountConsensusNotReached", "Ex3569DC", false, true, ReplayStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06002529 RID: 9513 RVA: 0x000AF6AC File Offset: 0x000AD8AC
		public static LocalizedString AmBcsDatabaseCopyFailed(string db, string server, string failedMessage)
		{
			return new LocalizedString("AmBcsDatabaseCopyFailed", "ExB5E79C", false, true, ReplayStrings.ResourceManager, new object[]
			{
				db,
				server,
				failedMessage
			});
		}

		// Token: 0x0600252A RID: 9514 RVA: 0x000AF6E4 File Offset: 0x000AD8E4
		public static LocalizedString AmDbRemountSkippedSinceMasterChanged(string dbName, string currentActive, string prevActive)
		{
			return new LocalizedString("AmDbRemountSkippedSinceMasterChanged", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				dbName,
				currentActive,
				prevActive
			});
		}

		// Token: 0x0600252B RID: 9515 RVA: 0x000AF71C File Offset: 0x000AD91C
		public static LocalizedString AutoReseedNoExchangeVolumesConfigured(string exchangeVolumeRootPath)
		{
			return new LocalizedString("AutoReseedNoExchangeVolumesConfigured", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				exchangeVolumeRootPath
			});
		}

		// Token: 0x0600252C RID: 9516 RVA: 0x000AF74C File Offset: 0x000AD94C
		public static LocalizedString AmDatabaseNameNotFoundException(string dbName)
		{
			return new LocalizedString("AmDatabaseNameNotFoundException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				dbName
			});
		}

		// Token: 0x17000974 RID: 2420
		// (get) Token: 0x0600252D RID: 9517 RVA: 0x000AF77B File Offset: 0x000AD97B
		public static LocalizedString AmBcsNoneSpecified
		{
			get
			{
				return new LocalizedString("AmBcsNoneSpecified", "Ex621F21", false, true, ReplayStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600252E RID: 9518 RVA: 0x000AF79C File Offset: 0x000AD99C
		public static LocalizedString NoInstancesFoundForManagementPath(string path)
		{
			return new LocalizedString("NoInstancesFoundForManagementPath", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				path
			});
		}

		// Token: 0x0600252F RID: 9519 RVA: 0x000AF7CC File Offset: 0x000AD9CC
		public static LocalizedString LastLogReplacementFailedUnexpectedFileFoundException(string dbCopy, string unexpectedFile, string e00log)
		{
			return new LocalizedString("LastLogReplacementFailedUnexpectedFileFoundException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				dbCopy,
				unexpectedFile,
				e00log
			});
		}

		// Token: 0x06002530 RID: 9520 RVA: 0x000AF804 File Offset: 0x000ADA04
		public static LocalizedString ExchangeVolumeInfoMultipleDbMountPointsException(string volumeName, string dbVolRootPath, string dbMountPoints, int maxDbs)
		{
			return new LocalizedString("ExchangeVolumeInfoMultipleDbMountPointsException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				volumeName,
				dbVolRootPath,
				dbMountPoints,
				maxDbs
			});
		}

		// Token: 0x17000975 RID: 2421
		// (get) Token: 0x06002531 RID: 9521 RVA: 0x000AF844 File Offset: 0x000ADA44
		public static LocalizedString SeederEcNotEnoughDiskException
		{
			get
			{
				return new LocalizedString("SeederEcNotEnoughDiskException", "", false, false, ReplayStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000976 RID: 2422
		// (get) Token: 0x06002532 RID: 9522 RVA: 0x000AF862 File Offset: 0x000ADA62
		public static LocalizedString NetworkFailedToAuthServer
		{
			get
			{
				return new LocalizedString("NetworkFailedToAuthServer", "ExE3255A", false, true, ReplayStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000977 RID: 2423
		// (get) Token: 0x06002533 RID: 9523 RVA: 0x000AF880 File Offset: 0x000ADA80
		public static LocalizedString ErrorFailedToFindLocalServer
		{
			get
			{
				return new LocalizedString("ErrorFailedToFindLocalServer", "ExDA9608", false, true, ReplayStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06002534 RID: 9524 RVA: 0x000AF8A0 File Offset: 0x000ADAA0
		public static LocalizedString MonitoringServerSiteIsNullException(string serverName)
		{
			return new LocalizedString("MonitoringServerSiteIsNullException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				serverName
			});
		}

		// Token: 0x06002535 RID: 9525 RVA: 0x000AF8D0 File Offset: 0x000ADAD0
		public static LocalizedString AmServiceMonitorSystemShutdownException(string serviceName)
		{
			return new LocalizedString("AmServiceMonitorSystemShutdownException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				serviceName
			});
		}

		// Token: 0x06002536 RID: 9526 RVA: 0x000AF900 File Offset: 0x000ADB00
		public static LocalizedString DatabaseFailedToGetVolumeInfo(string name)
		{
			return new LocalizedString("DatabaseFailedToGetVolumeInfo", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				name
			});
		}

		// Token: 0x06002537 RID: 9527 RVA: 0x000AF930 File Offset: 0x000ADB30
		public static LocalizedString LogRepairFailedTransient(string reason)
		{
			return new LocalizedString("LogRepairFailedTransient", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				reason
			});
		}

		// Token: 0x06002538 RID: 9528 RVA: 0x000AF960 File Offset: 0x000ADB60
		public static LocalizedString AcllCopyStatusInvalidException(string dbCopy, string status)
		{
			return new LocalizedString("AcllCopyStatusInvalidException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				dbCopy,
				status
			});
		}

		// Token: 0x06002539 RID: 9529 RVA: 0x000AF994 File Offset: 0x000ADB94
		public static LocalizedString DatabaseGroupNotSetException(string databaseName)
		{
			return new LocalizedString("DatabaseGroupNotSetException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				databaseName
			});
		}

		// Token: 0x17000978 RID: 2424
		// (get) Token: 0x0600253A RID: 9530 RVA: 0x000AF9C3 File Offset: 0x000ADBC3
		public static LocalizedString FailedToOpenShipLogContextDatabaseNotMounted
		{
			get
			{
				return new LocalizedString("FailedToOpenShipLogContextDatabaseNotMounted", "", false, false, ReplayStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000979 RID: 2425
		// (get) Token: 0x0600253B RID: 9531 RVA: 0x000AF9E1 File Offset: 0x000ADBE1
		public static LocalizedString ReplayServiceSuspendBlockedAcllException
		{
			get
			{
				return new LocalizedString("ReplayServiceSuspendBlockedAcllException", "", false, false, ReplayStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600253C RID: 9532 RVA: 0x000AFA00 File Offset: 0x000ADC00
		public static LocalizedString AcllBackupInProgressException(string dbCopy)
		{
			return new LocalizedString("AcllBackupInProgressException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				dbCopy
			});
		}

		// Token: 0x0600253D RID: 9533 RVA: 0x000AFA30 File Offset: 0x000ADC30
		public static LocalizedString AmDbMoveOperationNoLongerApplicableException(string dbName, string fromServer, string activeServer)
		{
			return new LocalizedString("AmDbMoveOperationNoLongerApplicableException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				dbName,
				fromServer,
				activeServer
			});
		}

		// Token: 0x0600253E RID: 9534 RVA: 0x000AFA68 File Offset: 0x000ADC68
		public static LocalizedString SeederSuspendFailedException(string specificError)
		{
			return new LocalizedString("SeederSuspendFailedException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				specificError
			});
		}

		// Token: 0x0600253F RID: 9535 RVA: 0x000AFA98 File Offset: 0x000ADC98
		public static LocalizedString CouldNotCreateDbMountPointFolderException(string database, string dbMountPoint, string errMsg)
		{
			return new LocalizedString("CouldNotCreateDbMountPointFolderException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				database,
				dbMountPoint,
				errMsg
			});
		}

		// Token: 0x1700097A RID: 2426
		// (get) Token: 0x06002540 RID: 9536 RVA: 0x000AFACF File Offset: 0x000ADCCF
		public static LocalizedString DatabaseCopyLayoutTableNullException
		{
			get
			{
				return new LocalizedString("DatabaseCopyLayoutTableNullException", "", false, false, ReplayStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06002541 RID: 9537 RVA: 0x000AFAF0 File Offset: 0x000ADCF0
		public static LocalizedString RemoteRegistryTimedOutException(string machineName, int secondsTimeout)
		{
			return new LocalizedString("RemoteRegistryTimedOutException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				machineName,
				secondsTimeout
			});
		}

		// Token: 0x1700097B RID: 2427
		// (get) Token: 0x06002542 RID: 9538 RVA: 0x000AFB28 File Offset: 0x000ADD28
		public static LocalizedString AmServiceShuttingDown
		{
			get
			{
				return new LocalizedString("AmServiceShuttingDown", "", false, false, ReplayStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06002543 RID: 9539 RVA: 0x000AFB48 File Offset: 0x000ADD48
		public static LocalizedString MonitoringCouldNotFindDatabasesException(string serverName, string adError)
		{
			return new LocalizedString("MonitoringCouldNotFindDatabasesException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				serverName,
				adError
			});
		}

		// Token: 0x06002544 RID: 9540 RVA: 0x000AFB7C File Offset: 0x000ADD7C
		public static LocalizedString TargetDBAlreadyExists(string edbFile)
		{
			return new LocalizedString("TargetDBAlreadyExists", "Ex8F2543", false, true, ReplayStrings.ResourceManager, new object[]
			{
				edbFile
			});
		}

		// Token: 0x06002545 RID: 9541 RVA: 0x000AFBAC File Offset: 0x000ADDAC
		public static LocalizedString SeederCatalogNotHealthyErr(string reason)
		{
			return new LocalizedString("SeederCatalogNotHealthyErr", "ExA4EA78", false, true, ReplayStrings.ResourceManager, new object[]
			{
				reason
			});
		}

		// Token: 0x06002546 RID: 9542 RVA: 0x000AFBDC File Offset: 0x000ADDDC
		public static LocalizedString ReplayServiceUnknownReplicaInstanceException(string operationName, string db)
		{
			return new LocalizedString("ReplayServiceUnknownReplicaInstanceException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				operationName,
				db
			});
		}

		// Token: 0x06002547 RID: 9543 RVA: 0x000AFC10 File Offset: 0x000ADE10
		public static LocalizedString PagePatchFileReadException(string fileName, long actualBytesRead, long expectedBytesRead)
		{
			return new LocalizedString("PagePatchFileReadException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				fileName,
				actualBytesRead,
				expectedBytesRead
			});
		}

		// Token: 0x06002548 RID: 9544 RVA: 0x000AFC54 File Offset: 0x000ADE54
		public static LocalizedString SeedingChannelIsClosedException(Guid g)
		{
			return new LocalizedString("SeedingChannelIsClosedException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				g
			});
		}

		// Token: 0x06002549 RID: 9545 RVA: 0x000AFC88 File Offset: 0x000ADE88
		public static LocalizedString ReplayDatabaseOperationTimedoutException(string operationName, string db, TimeSpan timeout)
		{
			return new LocalizedString("ReplayDatabaseOperationTimedoutException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				operationName,
				db,
				timeout
			});
		}

		// Token: 0x0600254A RID: 9546 RVA: 0x000AFCC4 File Offset: 0x000ADEC4
		public static LocalizedString AutoReseedFailedSeedRetryExceeded(int maxRetryCount)
		{
			return new LocalizedString("AutoReseedFailedSeedRetryExceeded", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				maxRetryCount
			});
		}

		// Token: 0x0600254B RID: 9547 RVA: 0x000AFCF8 File Offset: 0x000ADEF8
		public static LocalizedString AcllLastLogTimeErrorException(string dbCopy, string logfilePath, string err)
		{
			return new LocalizedString("AcllLastLogTimeErrorException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				dbCopy,
				logfilePath,
				err
			});
		}

		// Token: 0x0600254C RID: 9548 RVA: 0x000AFD30 File Offset: 0x000ADF30
		public static LocalizedString SeederOperationFailedWithEcException(int ec, string errMessage)
		{
			return new LocalizedString("SeederOperationFailedWithEcException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				ec,
				errMessage
			});
		}

		// Token: 0x0600254D RID: 9549 RVA: 0x000AFD68 File Offset: 0x000ADF68
		public static LocalizedString VolumeCouldNotBeReclaimedException(string volumeName, string mountPoint)
		{
			return new LocalizedString("VolumeCouldNotBeReclaimedException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				volumeName,
				mountPoint
			});
		}

		// Token: 0x0600254E RID: 9550 RVA: 0x000AFD9C File Offset: 0x000ADF9C
		public static LocalizedString DbValidationNotEnoughCopies(string dbName)
		{
			return new LocalizedString("DbValidationNotEnoughCopies", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				dbName
			});
		}

		// Token: 0x1700097C RID: 2428
		// (get) Token: 0x0600254F RID: 9551 RVA: 0x000AFDCB File Offset: 0x000ADFCB
		public static LocalizedString DagTaskDagIpAddressesMustBeIpv4Exception
		{
			get
			{
				return new LocalizedString("DagTaskDagIpAddressesMustBeIpv4Exception", "", false, false, ReplayStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06002550 RID: 9552 RVA: 0x000AFDEC File Offset: 0x000ADFEC
		public static LocalizedString RepairStateFailedToCreateTempLogFile(string dbName, string errorMsg)
		{
			return new LocalizedString("RepairStateFailedToCreateTempLogFile", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				dbName,
				errorMsg
			});
		}

		// Token: 0x06002551 RID: 9553 RVA: 0x000AFE20 File Offset: 0x000AE020
		public static LocalizedString DagTaskRemoveDagServerMustHaveQuorumException(string dagName)
		{
			return new LocalizedString("DagTaskRemoveDagServerMustHaveQuorumException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				dagName
			});
		}

		// Token: 0x06002552 RID: 9554 RVA: 0x000AFE50 File Offset: 0x000AE050
		public static LocalizedString AcllFailedCurrentLogPresent(string dbCopy, string e00logfile, string sourceServer)
		{
			return new LocalizedString("AcllFailedCurrentLogPresent", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				dbCopy,
				e00logfile,
				sourceServer
			});
		}

		// Token: 0x06002553 RID: 9555 RVA: 0x000AFE88 File Offset: 0x000AE088
		public static LocalizedString AutoReseedFailedToFindTargetVolumeName(string volumeInfoErr)
		{
			return new LocalizedString("AutoReseedFailedToFindTargetVolumeName", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				volumeInfoErr
			});
		}

		// Token: 0x06002554 RID: 9556 RVA: 0x000AFEB8 File Offset: 0x000AE0B8
		public static LocalizedString AmBcsTargetServerIsHAComponentOffline(string server)
		{
			return new LocalizedString("AmBcsTargetServerIsHAComponentOffline", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				server
			});
		}

		// Token: 0x1700097D RID: 2429
		// (get) Token: 0x06002555 RID: 9557 RVA: 0x000AFEE7 File Offset: 0x000AE0E7
		public static LocalizedString UnknownError
		{
			get
			{
				return new LocalizedString("UnknownError", "", false, false, ReplayStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06002556 RID: 9558 RVA: 0x000AFF08 File Offset: 0x000AE108
		public static LocalizedString NoDivergedPointFound(string dbCopy, string sourceServer)
		{
			return new LocalizedString("NoDivergedPointFound", "Ex4229B4", false, true, ReplayStrings.ResourceManager, new object[]
			{
				dbCopy,
				sourceServer
			});
		}

		// Token: 0x06002557 RID: 9559 RVA: 0x000AFF3C File Offset: 0x000AE13C
		public static LocalizedString SeederRpcServerLevelUnsupportedException(string serverName, string serverVersion, string supportedVersion)
		{
			return new LocalizedString("SeederRpcServerLevelUnsupportedException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				serverName,
				serverVersion,
				supportedVersion
			});
		}

		// Token: 0x06002558 RID: 9560 RVA: 0x000AFF74 File Offset: 0x000AE174
		public static LocalizedString CouldNotCreateDbMountPointException(string database, string dbMountPoint, string volumeName, string errMsg)
		{
			return new LocalizedString("CouldNotCreateDbMountPointException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				database,
				dbMountPoint,
				volumeName,
				errMsg
			});
		}

		// Token: 0x06002559 RID: 9561 RVA: 0x000AFFB0 File Offset: 0x000AE1B0
		public static LocalizedString FailedToConfigureMountPointException(string volumeName, string reason)
		{
			return new LocalizedString("FailedToConfigureMountPointException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				volumeName,
				reason
			});
		}

		// Token: 0x0600255A RID: 9562 RVA: 0x000AFFE4 File Offset: 0x000AE1E4
		public static LocalizedString AmDismountSucceededButStillMounted(string serverName, string dbName)
		{
			return new LocalizedString("AmDismountSucceededButStillMounted", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				serverName,
				dbName
			});
		}

		// Token: 0x1700097E RID: 2430
		// (get) Token: 0x0600255B RID: 9563 RVA: 0x000B0017 File Offset: 0x000AE217
		public static LocalizedString EseBackFileSystemCorruption
		{
			get
			{
				return new LocalizedString("EseBackFileSystemCorruption", "", false, false, ReplayStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700097F RID: 2431
		// (get) Token: 0x0600255C RID: 9564 RVA: 0x000B0035 File Offset: 0x000AE235
		public static LocalizedString SuspendOperationName
		{
			get
			{
				return new LocalizedString("SuspendOperationName", "", false, false, ReplayStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000980 RID: 2432
		// (get) Token: 0x0600255D RID: 9565 RVA: 0x000B0053 File Offset: 0x000AE253
		public static LocalizedString PagePatchLegacyFileExistsException
		{
			get
			{
				return new LocalizedString("PagePatchLegacyFileExistsException", "", false, false, ReplayStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600255E RID: 9566 RVA: 0x000B0074 File Offset: 0x000AE274
		public static LocalizedString LogCopierInitFailedBecauseNoLogsOnSource(string srcServer)
		{
			return new LocalizedString("LogCopierInitFailedBecauseNoLogsOnSource", "Ex3D0F10", false, true, ReplayStrings.ResourceManager, new object[]
			{
				srcServer
			});
		}

		// Token: 0x0600255F RID: 9567 RVA: 0x000B00A4 File Offset: 0x000AE2A4
		public static LocalizedString FailureItemRecoveryFailed(string dbName, string msg)
		{
			return new LocalizedString("FailureItemRecoveryFailed", "Ex1DA2B7", false, true, ReplayStrings.ResourceManager, new object[]
			{
				dbName,
				msg
			});
		}

		// Token: 0x06002560 RID: 9568 RVA: 0x000B00D8 File Offset: 0x000AE2D8
		public static LocalizedString AcllCopyStatusFailedException(string dbCopy, string status, string errorMsg)
		{
			return new LocalizedString("AcllCopyStatusFailedException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				dbCopy,
				status,
				errorMsg
			});
		}

		// Token: 0x06002561 RID: 9569 RVA: 0x000B0110 File Offset: 0x000AE310
		public static LocalizedString LastLogReplacementTooManyTempFilesException(string dbCopy, string filter, int count, string logPath)
		{
			return new LocalizedString("LastLogReplacementTooManyTempFilesException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				dbCopy,
				filter,
				count,
				logPath
			});
		}

		// Token: 0x17000981 RID: 2433
		// (get) Token: 0x06002562 RID: 9570 RVA: 0x000B0150 File Offset: 0x000AE350
		public static LocalizedString SeederEcSuccess
		{
			get
			{
				return new LocalizedString("SeederEcSuccess", "Ex9395B4", false, true, ReplayStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06002563 RID: 9571 RVA: 0x000B0170 File Offset: 0x000AE370
		public static LocalizedString AmBcsTargetServerActivationDisabled(string server)
		{
			return new LocalizedString("AmBcsTargetServerActivationDisabled", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				server
			});
		}

		// Token: 0x06002564 RID: 9572 RVA: 0x000B01A0 File Offset: 0x000AE3A0
		public static LocalizedString ManagementApiError(string api)
		{
			return new LocalizedString("ManagementApiError", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				api
			});
		}

		// Token: 0x17000982 RID: 2434
		// (get) Token: 0x06002565 RID: 9573 RVA: 0x000B01CF File Offset: 0x000AE3CF
		public static LocalizedString NoServices
		{
			get
			{
				return new LocalizedString("NoServices", "Ex5154B4", false, true, ReplayStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06002566 RID: 9574 RVA: 0x000B01F0 File Offset: 0x000AE3F0
		public static LocalizedString KernelWatchdogTimerError(string msg)
		{
			return new LocalizedString("KernelWatchdogTimerError", "Ex4842FB", false, true, ReplayStrings.ResourceManager, new object[]
			{
				msg
			});
		}

		// Token: 0x06002567 RID: 9575 RVA: 0x000B0220 File Offset: 0x000AE420
		public static LocalizedString AmDbActionTransientException(string actionError)
		{
			return new LocalizedString("AmDbActionTransientException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				actionError
			});
		}

		// Token: 0x06002568 RID: 9576 RVA: 0x000B0250 File Offset: 0x000AE450
		public static LocalizedString AmBcsActiveCopyIsSeedingSource(string db, string server)
		{
			return new LocalizedString("AmBcsActiveCopyIsSeedingSource", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				db,
				server
			});
		}

		// Token: 0x06002569 RID: 9577 RVA: 0x000B0284 File Offset: 0x000AE484
		public static LocalizedString NetworkAddressResolutionFailedNoDnsEntry(string nodeName)
		{
			return new LocalizedString("NetworkAddressResolutionFailedNoDnsEntry", "Ex496595", false, true, ReplayStrings.ResourceManager, new object[]
			{
				nodeName
			});
		}

		// Token: 0x0600256A RID: 9578 RVA: 0x000B02B4 File Offset: 0x000AE4B4
		public static LocalizedString AmBcsDatabaseCopyInitializing(string db, string server)
		{
			return new LocalizedString("AmBcsDatabaseCopyInitializing", "Ex1C33B4", false, true, ReplayStrings.ResourceManager, new object[]
			{
				db,
				server
			});
		}

		// Token: 0x0600256B RID: 9579 RVA: 0x000B02E8 File Offset: 0x000AE4E8
		public static LocalizedString RepairStateFailedPendingPagePatchException(string dbName, string errorMsg)
		{
			return new LocalizedString("RepairStateFailedPendingPagePatchException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				dbName,
				errorMsg
			});
		}

		// Token: 0x0600256C RID: 9580 RVA: 0x000B031C File Offset: 0x000AE51C
		public static LocalizedString NetworkTimeoutError(string remoteNodeName, string errorText)
		{
			return new LocalizedString("NetworkTimeoutError", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				remoteNodeName,
				errorText
			});
		}

		// Token: 0x17000983 RID: 2435
		// (get) Token: 0x0600256D RID: 9581 RVA: 0x000B034F File Offset: 0x000AE54F
		public static LocalizedString StoreServiceMonitorCriticalError
		{
			get
			{
				return new LocalizedString("StoreServiceMonitorCriticalError", "", false, false, ReplayStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600256E RID: 9582 RVA: 0x000B0370 File Offset: 0x000AE570
		public static LocalizedString CouldNotFindVolumeException(string volumeName)
		{
			return new LocalizedString("CouldNotFindVolumeException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				volumeName
			});
		}

		// Token: 0x0600256F RID: 9583 RVA: 0x000B03A0 File Offset: 0x000AE5A0
		public static LocalizedString AmCommonException(string error)
		{
			return new LocalizedString("AmCommonException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				error
			});
		}

		// Token: 0x06002570 RID: 9584 RVA: 0x000B03D0 File Offset: 0x000AE5D0
		public static LocalizedString DagTaskRemoteOperationLogEnd(string serverName)
		{
			return new LocalizedString("DagTaskRemoteOperationLogEnd", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				serverName
			});
		}

		// Token: 0x06002571 RID: 9585 RVA: 0x000B0400 File Offset: 0x000AE600
		public static LocalizedString RepairStateDatabaseIsActive(string dbName)
		{
			return new LocalizedString("RepairStateDatabaseIsActive", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				dbName
			});
		}

		// Token: 0x06002572 RID: 9586 RVA: 0x000B0430 File Offset: 0x000AE630
		public static LocalizedString DirectoryEnumeratorIOError(string apiName, string ioErrorMessage, int win32ErrCode, string directoryName)
		{
			return new LocalizedString("DirectoryEnumeratorIOError", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				apiName,
				ioErrorMessage,
				win32ErrCode,
				directoryName
			});
		}

		// Token: 0x06002573 RID: 9587 RVA: 0x000B0470 File Offset: 0x000AE670
		public static LocalizedString NetworkTransportError(string err)
		{
			return new LocalizedString("NetworkTransportError", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				err
			});
		}

		// Token: 0x06002574 RID: 9588 RVA: 0x000B04A0 File Offset: 0x000AE6A0
		public static LocalizedString DagTaskMovedPam(string newPam)
		{
			return new LocalizedString("DagTaskMovedPam", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				newPam
			});
		}

		// Token: 0x06002575 RID: 9589 RVA: 0x000B04D0 File Offset: 0x000AE6D0
		public static LocalizedString LogInspectorE00OutOfSequence(string logfileInspected, long genFromHeader, long highestGenPresent)
		{
			return new LocalizedString("LogInspectorE00OutOfSequence", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				logfileInspected,
				genFromHeader,
				highestGenPresent
			});
		}

		// Token: 0x06002576 RID: 9590 RVA: 0x000B0514 File Offset: 0x000AE714
		public static LocalizedString LogCopierInitFailedActiveTruncatingException(string srcServer, long startingLogGen, long srcLowestGen)
		{
			return new LocalizedString("LogCopierInitFailedActiveTruncatingException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				srcServer,
				startingLogGen,
				srcLowestGen
			});
		}

		// Token: 0x17000984 RID: 2436
		// (get) Token: 0x06002577 RID: 9591 RVA: 0x000B0555 File Offset: 0x000AE755
		public static LocalizedString CannotChangeProperties
		{
			get
			{
				return new LocalizedString("CannotChangeProperties", "Ex939705", false, true, ReplayStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06002578 RID: 9592 RVA: 0x000B0574 File Offset: 0x000AE774
		public static LocalizedString AmFailedToDetermineDatabaseMountStatus(string serverName, string dbName)
		{
			return new LocalizedString("AmFailedToDetermineDatabaseMountStatus", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				serverName,
				dbName
			});
		}

		// Token: 0x06002579 RID: 9593 RVA: 0x000B05A8 File Offset: 0x000AE7A8
		public static LocalizedString ReplayFailedToFindServerRpcVersionException(string serverName)
		{
			return new LocalizedString("ReplayFailedToFindServerRpcVersionException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				serverName
			});
		}

		// Token: 0x0600257A RID: 9594 RVA: 0x000B05D8 File Offset: 0x000AE7D8
		public static LocalizedString AmBcsDatabaseCopyActivationSuspended(string db, string server, string reason)
		{
			return new LocalizedString("AmBcsDatabaseCopyActivationSuspended", "Ex80D1AB", false, true, ReplayStrings.ResourceManager, new object[]
			{
				db,
				server,
				reason
			});
		}

		// Token: 0x0600257B RID: 9595 RVA: 0x000B0610 File Offset: 0x000AE810
		public static LocalizedString IncrementalReseedRetryableException(string error)
		{
			return new LocalizedString("IncrementalReseedRetryableException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				error
			});
		}

		// Token: 0x0600257C RID: 9596 RVA: 0x000B0640 File Offset: 0x000AE840
		public static LocalizedString AmMountBlockedOnStandaloneDbWithMissingEdbException(string dbName, long highestLogGen, string edbFilePath)
		{
			return new LocalizedString("AmMountBlockedOnStandaloneDbWithMissingEdbException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				dbName,
				highestLogGen,
				edbFilePath
			});
		}

		// Token: 0x0600257D RID: 9597 RVA: 0x000B067C File Offset: 0x000AE87C
		public static LocalizedString SeederInstanceAlreadyCompletedException(string sourceMachine)
		{
			return new LocalizedString("SeederInstanceAlreadyCompletedException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				sourceMachine
			});
		}

		// Token: 0x0600257E RID: 9598 RVA: 0x000B06AC File Offset: 0x000AE8AC
		public static LocalizedString CouldNotFindDagObjectLookupErrorForServer(string serverName, string error)
		{
			return new LocalizedString("CouldNotFindDagObjectLookupErrorForServer", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				serverName,
				error
			});
		}

		// Token: 0x0600257F RID: 9599 RVA: 0x000B06E0 File Offset: 0x000AE8E0
		public static LocalizedString ServiceName(string name, string length)
		{
			return new LocalizedString("ServiceName", "ExA57B32", false, true, ReplayStrings.ResourceManager, new object[]
			{
				name,
				length
			});
		}

		// Token: 0x06002580 RID: 9600 RVA: 0x000B0714 File Offset: 0x000AE914
		public static LocalizedString AmClusterNodeNotFoundException(string nodeName)
		{
			return new LocalizedString("AmClusterNodeNotFoundException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				nodeName
			});
		}

		// Token: 0x06002581 RID: 9601 RVA: 0x000B0744 File Offset: 0x000AE944
		public static LocalizedString AutoReseedFailedResumeBlocked(string error)
		{
			return new LocalizedString("AutoReseedFailedResumeBlocked", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				error
			});
		}

		// Token: 0x17000985 RID: 2437
		// (get) Token: 0x06002582 RID: 9602 RVA: 0x000B0773 File Offset: 0x000AE973
		public static LocalizedString SeederEchrInvalidCallSequence
		{
			get
			{
				return new LocalizedString("SeederEchrInvalidCallSequence", "Ex87FBE8", false, true, ReplayStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06002583 RID: 9603 RVA: 0x000B0794 File Offset: 0x000AE994
		public static LocalizedString SeederFailedToFindValidVolumeInfo(string name, string error)
		{
			return new LocalizedString("SeederFailedToFindValidVolumeInfo", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				name,
				error
			});
		}

		// Token: 0x17000986 RID: 2438
		// (get) Token: 0x06002584 RID: 9604 RVA: 0x000B07C7 File Offset: 0x000AE9C7
		public static LocalizedString FailedToOpenShipLogContextEseCircularLoggingEnabled
		{
			get
			{
				return new LocalizedString("FailedToOpenShipLogContextEseCircularLoggingEnabled", "", false, false, ReplayStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06002585 RID: 9605 RVA: 0x000B07E8 File Offset: 0x000AE9E8
		public static LocalizedString SeederEcJtxAlreadyExist(string directory)
		{
			return new LocalizedString("SeederEcJtxAlreadyExist", "Ex1CD4A4", false, true, ReplayStrings.ResourceManager, new object[]
			{
				directory
			});
		}

		// Token: 0x06002586 RID: 9606 RVA: 0x000B0818 File Offset: 0x000AEA18
		public static LocalizedString SeederFailedToDeleteDatabase(string file, string error)
		{
			return new LocalizedString("SeederFailedToDeleteDatabase", "Ex9D8082", false, true, ReplayStrings.ResourceManager, new object[]
			{
				file,
				error
			});
		}

		// Token: 0x17000987 RID: 2439
		// (get) Token: 0x06002587 RID: 9607 RVA: 0x000B084B File Offset: 0x000AEA4B
		public static LocalizedString ReplayServiceResumeRpcFailedException
		{
			get
			{
				return new LocalizedString("ReplayServiceResumeRpcFailedException", "", false, false, ReplayStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06002588 RID: 9608 RVA: 0x000B086C File Offset: 0x000AEA6C
		public static LocalizedString DagTaskComponentManagerServerManagerCmdFailure(string error)
		{
			return new LocalizedString("DagTaskComponentManagerServerManagerCmdFailure", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				error
			});
		}

		// Token: 0x06002589 RID: 9609 RVA: 0x000B089C File Offset: 0x000AEA9C
		public static LocalizedString FailedToGetDiskSpace(string path, string systemErrMessage)
		{
			return new LocalizedString("FailedToGetDiskSpace", "ExAE22D9", false, true, ReplayStrings.ResourceManager, new object[]
			{
				path,
				systemErrMessage
			});
		}

		// Token: 0x17000988 RID: 2440
		// (get) Token: 0x0600258A RID: 9610 RVA: 0x000B08CF File Offset: 0x000AEACF
		public static LocalizedString NetworkSecurityFailed
		{
			get
			{
				return new LocalizedString("NetworkSecurityFailed", "Ex80869A", false, true, ReplayStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600258B RID: 9611 RVA: 0x000B08F0 File Offset: 0x000AEAF0
		public static LocalizedString DbRedundancyValidationErrorsOccurred(string dbName, int healthyCopiesCount, int expectedHealthyCount, string detailedMsg)
		{
			return new LocalizedString("DbRedundancyValidationErrorsOccurred", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				dbName,
				healthyCopiesCount,
				expectedHealthyCount,
				detailedMsg
			});
		}

		// Token: 0x0600258C RID: 9612 RVA: 0x000B0938 File Offset: 0x000AEB38
		public static LocalizedString AcllUnboundedDatalossDetectedException(string dbName, string lastUpdatedTimeStr, string allowedDurationStr, string actualDurationStr)
		{
			return new LocalizedString("AcllUnboundedDatalossDetectedException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				dbName,
				lastUpdatedTimeStr,
				allowedDurationStr,
				actualDurationStr
			});
		}

		// Token: 0x0600258D RID: 9613 RVA: 0x000B0974 File Offset: 0x000AEB74
		public static LocalizedString ClusterBatchWriter_BatchAddCommandFailed(int ec)
		{
			return new LocalizedString("ClusterBatchWriter_BatchAddCommandFailed", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				ec
			});
		}

		// Token: 0x17000989 RID: 2441
		// (get) Token: 0x0600258E RID: 9614 RVA: 0x000B09A8 File Offset: 0x000AEBA8
		public static LocalizedString ReplayServiceSuspendRpcFailedException
		{
			get
			{
				return new LocalizedString("ReplayServiceSuspendRpcFailedException", "", false, false, ReplayStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700098A RID: 2442
		// (get) Token: 0x0600258F RID: 9615 RVA: 0x000B09C6 File Offset: 0x000AEBC6
		public static LocalizedString SuspendWantedWriteFailedException
		{
			get
			{
				return new LocalizedString("SuspendWantedWriteFailedException", "", false, false, ReplayStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06002590 RID: 9616 RVA: 0x000B09E4 File Offset: 0x000AEBE4
		public static LocalizedString SeedingSourceReplicaInstanceNotFoundException(Guid guid, string sourceServer)
		{
			return new LocalizedString("SeedingSourceReplicaInstanceNotFoundException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				guid,
				sourceServer
			});
		}

		// Token: 0x06002591 RID: 9617 RVA: 0x000B0A1C File Offset: 0x000AEC1C
		public static LocalizedString AmBcsDatabaseCopySuspended(string db, string server, string reason)
		{
			return new LocalizedString("AmBcsDatabaseCopySuspended", "ExF2C537", false, true, ReplayStrings.ResourceManager, new object[]
			{
				db,
				server,
				reason
			});
		}

		// Token: 0x06002592 RID: 9618 RVA: 0x000B0A54 File Offset: 0x000AEC54
		public static LocalizedString FileCheckLogfileCreationTime(string logfile, DateTime previousGenerationCreationTime, DateTime previousGenerationCreationTimeActual)
		{
			return new LocalizedString("FileCheckLogfileCreationTime", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				logfile,
				previousGenerationCreationTime,
				previousGenerationCreationTimeActual
			});
		}

		// Token: 0x1700098B RID: 2443
		// (get) Token: 0x06002593 RID: 9619 RVA: 0x000B0A95 File Offset: 0x000AEC95
		public static LocalizedString ReplayServiceSuspendResumeBlockedException
		{
			get
			{
				return new LocalizedString("ReplayServiceSuspendResumeBlockedException", "", false, false, ReplayStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700098C RID: 2444
		// (get) Token: 0x06002594 RID: 9620 RVA: 0x000B0AB3 File Offset: 0x000AECB3
		public static LocalizedString AmDbActionDismountFailedException
		{
			get
			{
				return new LocalizedString("AmDbActionDismountFailedException", "", false, false, ReplayStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700098D RID: 2445
		// (get) Token: 0x06002595 RID: 9621 RVA: 0x000B0AD1 File Offset: 0x000AECD1
		public static LocalizedString AutoReseedNeverMountedWorkflowReason
		{
			get
			{
				return new LocalizedString("AutoReseedNeverMountedWorkflowReason", "", false, false, ReplayStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700098E RID: 2446
		// (get) Token: 0x06002596 RID: 9622 RVA: 0x000B0AEF File Offset: 0x000AECEF
		public static LocalizedString AutoReseedLogAndDbNotOnSameVolume
		{
			get
			{
				return new LocalizedString("AutoReseedLogAndDbNotOnSameVolume", "", false, false, ReplayStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700098F RID: 2447
		// (get) Token: 0x06002597 RID: 9623 RVA: 0x000B0B0D File Offset: 0x000AED0D
		public static LocalizedString FullServerSeedSkippedShutdownException
		{
			get
			{
				return new LocalizedString("FullServerSeedSkippedShutdownException", "", false, false, ReplayStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06002598 RID: 9624 RVA: 0x000B0B2C File Offset: 0x000AED2C
		public static LocalizedString ReplayLagManagerException(string errorMsg)
		{
			return new LocalizedString("ReplayLagManagerException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				errorMsg
			});
		}

		// Token: 0x06002599 RID: 9625 RVA: 0x000B0B5C File Offset: 0x000AED5C
		public static LocalizedString DatabaseVolumeInfoInitException(string databaseCopy, string errMsg)
		{
			return new LocalizedString("DatabaseVolumeInfoInitException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				databaseCopy,
				errMsg
			});
		}

		// Token: 0x0600259A RID: 9626 RVA: 0x000B0B90 File Offset: 0x000AED90
		public static LocalizedString ReplayDbOperationTransientException(string opError)
		{
			return new LocalizedString("ReplayDbOperationTransientException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				opError
			});
		}

		// Token: 0x0600259B RID: 9627 RVA: 0x000B0BC0 File Offset: 0x000AEDC0
		public static LocalizedString VolumeFormatFailedException(string volumeName, string mountPoint, string err)
		{
			return new LocalizedString("VolumeFormatFailedException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				volumeName,
				mountPoint,
				err
			});
		}

		// Token: 0x17000990 RID: 2448
		// (get) Token: 0x0600259C RID: 9628 RVA: 0x000B0BF7 File Offset: 0x000AEDF7
		public static LocalizedString SeederEcOverlappedWriteErr
		{
			get
			{
				return new LocalizedString("SeederEcOverlappedWriteErr", "ExCEBAF4", false, true, ReplayStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600259D RID: 9629 RVA: 0x000B0C18 File Offset: 0x000AEE18
		public static LocalizedString AutoReseedNoCopiesException(string databaseName)
		{
			return new LocalizedString("AutoReseedNoCopiesException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				databaseName
			});
		}

		// Token: 0x0600259E RID: 9630 RVA: 0x000B0C48 File Offset: 0x000AEE48
		public static LocalizedString ReplayDatabaseOperationCancelledException(string operationName, string db)
		{
			return new LocalizedString("ReplayDatabaseOperationCancelledException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				operationName,
				db
			});
		}

		// Token: 0x0600259F RID: 9631 RVA: 0x000B0C7C File Offset: 0x000AEE7C
		public static LocalizedString CiStatusIsFailed(string server, string db)
		{
			return new LocalizedString("CiStatusIsFailed", "Ex650952", false, true, ReplayStrings.ResourceManager, new object[]
			{
				server,
				db
			});
		}

		// Token: 0x060025A0 RID: 9632 RVA: 0x000B0CB0 File Offset: 0x000AEEB0
		public static LocalizedString NetworkEndOfData(string nodeName, string messageText)
		{
			return new LocalizedString("NetworkEndOfData", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				nodeName,
				messageText
			});
		}

		// Token: 0x060025A1 RID: 9633 RVA: 0x000B0CE4 File Offset: 0x000AEEE4
		public static LocalizedString DbAvailabilityActiveCopyDismountedError(string dbName, string serverName, string error)
		{
			return new LocalizedString("DbAvailabilityActiveCopyDismountedError", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				dbName,
				serverName,
				error
			});
		}

		// Token: 0x17000991 RID: 2449
		// (get) Token: 0x060025A2 RID: 9634 RVA: 0x000B0D1B File Offset: 0x000AEF1B
		public static LocalizedString FailToCleanUpFiles
		{
			get
			{
				return new LocalizedString("FailToCleanUpFiles", "Ex1AF8B4", false, true, ReplayStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060025A3 RID: 9635 RVA: 0x000B0D3C File Offset: 0x000AEF3C
		public static LocalizedString NetworkReadTimeout(int waitInsecs)
		{
			return new LocalizedString("NetworkReadTimeout", "ExC36360", false, true, ReplayStrings.ResourceManager, new object[]
			{
				waitInsecs
			});
		}

		// Token: 0x060025A4 RID: 9636 RVA: 0x000B0D70 File Offset: 0x000AEF70
		public static LocalizedString CiSeederSearchCatalogException(string sourceServer, Guid database, string specificError)
		{
			return new LocalizedString("CiSeederSearchCatalogException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				sourceServer,
				database,
				specificError
			});
		}

		// Token: 0x060025A5 RID: 9637 RVA: 0x000B0DAC File Offset: 0x000AEFAC
		public static LocalizedString TPRExchangeNotListening(string reason)
		{
			return new LocalizedString("TPRExchangeNotListening", "ExDACB39", false, true, ReplayStrings.ResourceManager, new object[]
			{
				reason
			});
		}

		// Token: 0x060025A6 RID: 9638 RVA: 0x000B0DDC File Offset: 0x000AEFDC
		public static LocalizedString AmClusterEventNotifierTransientException(int errCode)
		{
			return new LocalizedString("AmClusterEventNotifierTransientException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				errCode
			});
		}

		// Token: 0x060025A7 RID: 9639 RVA: 0x000B0E10 File Offset: 0x000AF010
		public static LocalizedString MissingPassiveLogRequiredForDivergenceDetection(string file)
		{
			return new LocalizedString("MissingPassiveLogRequiredForDivergenceDetection", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				file
			});
		}

		// Token: 0x060025A8 RID: 9640 RVA: 0x000B0E40 File Offset: 0x000AF040
		public static LocalizedString AmBcsTargetServerPreferredMaxActivesExceeded(string server, string maxActiveDatabases)
		{
			return new LocalizedString("AmBcsTargetServerPreferredMaxActivesExceeded", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				server,
				maxActiveDatabases
			});
		}

		// Token: 0x060025A9 RID: 9641 RVA: 0x000B0E74 File Offset: 0x000AF074
		public static LocalizedString AmClusterNodeJoinedException(string nodeName)
		{
			return new LocalizedString("AmClusterNodeJoinedException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				nodeName
			});
		}

		// Token: 0x060025AA RID: 9642 RVA: 0x000B0EA4 File Offset: 0x000AF0A4
		public static LocalizedString AmBcsTargetServerPreferredMaxActivesReached(string server, string maxActiveDatabases)
		{
			return new LocalizedString("AmBcsTargetServerPreferredMaxActivesReached", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				server,
				maxActiveDatabases
			});
		}

		// Token: 0x060025AB RID: 9643 RVA: 0x000B0ED8 File Offset: 0x000AF0D8
		public static LocalizedString AmDbLockConflict(Guid dbGuid, string reqReason, string ownerReason)
		{
			return new LocalizedString("AmDbLockConflict", "Ex0F5D8B", false, true, ReplayStrings.ResourceManager, new object[]
			{
				dbGuid,
				reqReason,
				ownerReason
			});
		}

		// Token: 0x060025AC RID: 9644 RVA: 0x000B0F14 File Offset: 0x000AF114
		public static LocalizedString FileCheck(string error)
		{
			return new LocalizedString("FileCheck", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				error
			});
		}

		// Token: 0x060025AD RID: 9645 RVA: 0x000B0F44 File Offset: 0x000AF144
		public static LocalizedString LogInspectorGenerationMismatch(string logfileInspected, long genFromHeader, long genFromFileName)
		{
			return new LocalizedString("LogInspectorGenerationMismatch", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				logfileInspected,
				genFromHeader,
				genFromFileName
			});
		}

		// Token: 0x060025AE RID: 9646 RVA: 0x000B0F88 File Offset: 0x000AF188
		public static LocalizedString SeederInstanceNotFoundException(string dbGuid)
		{
			return new LocalizedString("SeederInstanceNotFoundException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				dbGuid
			});
		}

		// Token: 0x060025AF RID: 9647 RVA: 0x000B0FB8 File Offset: 0x000AF1B8
		public static LocalizedString FailedToOpenLogTruncContext(string dbSrc, uint hresult, string optionalFriendlyError)
		{
			return new LocalizedString("FailedToOpenLogTruncContext", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				dbSrc,
				hresult,
				optionalFriendlyError
			});
		}

		// Token: 0x17000992 RID: 2450
		// (get) Token: 0x060025B0 RID: 9648 RVA: 0x000B0FF4 File Offset: 0x000AF1F4
		public static LocalizedString SeederEcNotEnoughDisk
		{
			get
			{
				return new LocalizedString("SeederEcNotEnoughDisk", "ExF8A360", false, true, ReplayStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060025B1 RID: 9649 RVA: 0x000B1014 File Offset: 0x000AF214
		public static LocalizedString DagTaskOperationFailedWithEcException(int ec)
		{
			return new LocalizedString("DagTaskOperationFailedWithEcException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				ec
			});
		}

		// Token: 0x060025B2 RID: 9650 RVA: 0x000B1048 File Offset: 0x000AF248
		public static LocalizedString DagTaskServerTransientException(string errorMessage)
		{
			return new LocalizedString("DagTaskServerTransientException", "Ex5C50F1", false, true, ReplayStrings.ResourceManager, new object[]
			{
				errorMessage
			});
		}

		// Token: 0x060025B3 RID: 9651 RVA: 0x000B1078 File Offset: 0x000AF278
		public static LocalizedString ReplayServiceSuspendRpcPartialSuccessCatalogFailedException(string errMsg)
		{
			return new LocalizedString("ReplayServiceSuspendRpcPartialSuccessCatalogFailedException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				errMsg
			});
		}

		// Token: 0x060025B4 RID: 9652 RVA: 0x000B10A8 File Offset: 0x000AF2A8
		public static LocalizedString PathIsAlreadyAValidMountPoint(string path)
		{
			return new LocalizedString("PathIsAlreadyAValidMountPoint", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				path
			});
		}

		// Token: 0x17000993 RID: 2451
		// (get) Token: 0x060025B5 RID: 9653 RVA: 0x000B10D7 File Offset: 0x000AF2D7
		public static LocalizedString MonitoringADServiceShuttingDownException
		{
			get
			{
				return new LocalizedString("MonitoringADServiceShuttingDownException", "", false, false, ReplayStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000994 RID: 2452
		// (get) Token: 0x060025B6 RID: 9654 RVA: 0x000B10F5 File Offset: 0x000AF2F5
		public static LocalizedString DbHTServiceShuttingDownException
		{
			get
			{
				return new LocalizedString("DbHTServiceShuttingDownException", "", false, false, ReplayStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060025B7 RID: 9655 RVA: 0x000B1114 File Offset: 0x000AF314
		public static LocalizedString AmLastLogPropertyCorruptedException(string property, string corruptedValue)
		{
			return new LocalizedString("AmLastLogPropertyCorruptedException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				property,
				corruptedValue
			});
		}

		// Token: 0x060025B8 RID: 9656 RVA: 0x000B1148 File Offset: 0x000AF348
		public static LocalizedString CouldNotFindDagObjectForServer(string serverName)
		{
			return new LocalizedString("CouldNotFindDagObjectForServer", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				serverName
			});
		}

		// Token: 0x060025B9 RID: 9657 RVA: 0x000B1178 File Offset: 0x000AF378
		public static LocalizedString TagHandlerFormatMsgFailed(uint msgId)
		{
			return new LocalizedString("TagHandlerFormatMsgFailed", "ExCFCA21", false, true, ReplayStrings.ResourceManager, new object[]
			{
				msgId
			});
		}

		// Token: 0x060025BA RID: 9658 RVA: 0x000B11AC File Offset: 0x000AF3AC
		public static LocalizedString IncrementalReseedPrereqException(string error)
		{
			return new LocalizedString("IncrementalReseedPrereqException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				error
			});
		}

		// Token: 0x17000995 RID: 2453
		// (get) Token: 0x060025BB RID: 9659 RVA: 0x000B11DB File Offset: 0x000AF3DB
		public static LocalizedString NullDbCopyException
		{
			get
			{
				return new LocalizedString("NullDbCopyException", "", false, false, ReplayStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000996 RID: 2454
		// (get) Token: 0x060025BC RID: 9660 RVA: 0x000B11F9 File Offset: 0x000AF3F9
		public static LocalizedString ErrorClusterServiceNotRunningForAmConfig
		{
			get
			{
				return new LocalizedString("ErrorClusterServiceNotRunningForAmConfig", "Ex576E35", false, true, ReplayStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060025BD RID: 9661 RVA: 0x000B1218 File Offset: 0x000AF418
		public static LocalizedString MonitoredDatabaseInitFailure(string dbName, string error)
		{
			return new LocalizedString("MonitoredDatabaseInitFailure", "Ex3B2197", false, true, ReplayStrings.ResourceManager, new object[]
			{
				dbName,
				error
			});
		}

		// Token: 0x060025BE RID: 9662 RVA: 0x000B124C File Offset: 0x000AF44C
		public static LocalizedString AmBcsTargetServerActivationBlocked(string server)
		{
			return new LocalizedString("AmBcsTargetServerActivationBlocked", "ExC2D6D2", false, true, ReplayStrings.ResourceManager, new object[]
			{
				server
			});
		}

		// Token: 0x060025BF RID: 9663 RVA: 0x000B127C File Offset: 0x000AF47C
		public static LocalizedString RepairStateDatabaseCopyShouldBeSuspended(string dbName)
		{
			return new LocalizedString("RepairStateDatabaseCopyShouldBeSuspended", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				dbName
			});
		}

		// Token: 0x060025C0 RID: 9664 RVA: 0x000B12AC File Offset: 0x000AF4AC
		public static LocalizedString InvalidRcrConfigAlreadyHostsDb(string nodeName, string dbName)
		{
			return new LocalizedString("InvalidRcrConfigAlreadyHostsDb", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				nodeName,
				dbName
			});
		}

		// Token: 0x060025C1 RID: 9665 RVA: 0x000B12E0 File Offset: 0x000AF4E0
		public static LocalizedString RegistryParameterException(string errorMsg)
		{
			return new LocalizedString("RegistryParameterException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				errorMsg
			});
		}

		// Token: 0x060025C2 RID: 9666 RVA: 0x000B1310 File Offset: 0x000AF510
		public static LocalizedString FailedToDeserializeDumpsterRequestStrException(string dbName, string stringToDeserialize, string typeName, string serializationError)
		{
			return new LocalizedString("FailedToDeserializeDumpsterRequestStrException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				dbName,
				stringToDeserialize,
				typeName,
				serializationError
			});
		}

		// Token: 0x060025C3 RID: 9667 RVA: 0x000B134C File Offset: 0x000AF54C
		public static LocalizedString SeederEcUndefined(int ec)
		{
			return new LocalizedString("SeederEcUndefined", "Ex82B4D6", false, true, ReplayStrings.ResourceManager, new object[]
			{
				ec
			});
		}

		// Token: 0x060025C4 RID: 9668 RVA: 0x000B1380 File Offset: 0x000AF580
		public static LocalizedString CiSeederGenericException(string sourceServer, string destServer, string specificError)
		{
			return new LocalizedString("CiSeederGenericException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				sourceServer,
				destServer,
				specificError
			});
		}

		// Token: 0x060025C5 RID: 9669 RVA: 0x000B13B8 File Offset: 0x000AF5B8
		public static LocalizedString FileCheckEDBMissing(string edbFileName)
		{
			return new LocalizedString("FileCheckEDBMissing", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				edbFileName
			});
		}

		// Token: 0x060025C6 RID: 9670 RVA: 0x000B13E8 File Offset: 0x000AF5E8
		public static LocalizedString AcllCopyIsNotViableErrorException(string dbCopy, string err)
		{
			return new LocalizedString("AcllCopyIsNotViableErrorException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				dbCopy,
				err
			});
		}

		// Token: 0x060025C7 RID: 9671 RVA: 0x000B141C File Offset: 0x000AF61C
		public static LocalizedString DatabaseVolumeInfoException(string errorMsg)
		{
			return new LocalizedString("DatabaseVolumeInfoException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				errorMsg
			});
		}

		// Token: 0x060025C8 RID: 9672 RVA: 0x000B144C File Offset: 0x000AF64C
		public static LocalizedString ReplayServiceResumeBlockedException(string previousError)
		{
			return new LocalizedString("ReplayServiceResumeBlockedException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				previousError
			});
		}

		// Token: 0x060025C9 RID: 9673 RVA: 0x000B147C File Offset: 0x000AF67C
		public static LocalizedString ReplaySystemOperationTimedoutException(string operationName, TimeSpan timeout)
		{
			return new LocalizedString("ReplaySystemOperationTimedoutException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				operationName,
				timeout
			});
		}

		// Token: 0x060025CA RID: 9674 RVA: 0x000B14B4 File Offset: 0x000AF6B4
		public static LocalizedString AmBcsSourceServerADError(string server, string adError)
		{
			return new LocalizedString("AmBcsSourceServerADError", "Ex7274B9", false, true, ReplayStrings.ResourceManager, new object[]
			{
				server,
				adError
			});
		}

		// Token: 0x060025CB RID: 9675 RVA: 0x000B14E8 File Offset: 0x000AF6E8
		public static LocalizedString AmDbActionWrapperTransientException(string dbActionError)
		{
			return new LocalizedString("AmDbActionWrapperTransientException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				dbActionError
			});
		}

		// Token: 0x060025CC RID: 9676 RVA: 0x000B1518 File Offset: 0x000AF718
		public static LocalizedString FileCheckAccessDenied(string file)
		{
			return new LocalizedString("FileCheckAccessDenied", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				file
			});
		}

		// Token: 0x060025CD RID: 9677 RVA: 0x000B1548 File Offset: 0x000AF748
		public static LocalizedString ActiveRecoveryNotApplicableException(string dbName)
		{
			return new LocalizedString("ActiveRecoveryNotApplicableException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				dbName
			});
		}

		// Token: 0x060025CE RID: 9678 RVA: 0x000B1578 File Offset: 0x000AF778
		public static LocalizedString SeederServerException(string errorMessage)
		{
			return new LocalizedString("SeederServerException", "Ex4543D9", false, true, ReplayStrings.ResourceManager, new object[]
			{
				errorMessage
			});
		}

		// Token: 0x060025CF RID: 9679 RVA: 0x000B15A8 File Offset: 0x000AF7A8
		public static LocalizedString AutoReseedCatalogIsBehindRetry(int retryCount)
		{
			return new LocalizedString("AutoReseedCatalogIsBehindRetry", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				retryCount
			});
		}

		// Token: 0x060025D0 RID: 9680 RVA: 0x000B15DC File Offset: 0x000AF7DC
		public static LocalizedString AutoReseedWrongNumberOfCopiesOnVolume(string volumeName, int numDbs, int expectedDbs)
		{
			return new LocalizedString("AutoReseedWrongNumberOfCopiesOnVolume", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				volumeName,
				numDbs,
				expectedDbs
			});
		}

		// Token: 0x060025D1 RID: 9681 RVA: 0x000B1620 File Offset: 0x000AF820
		public static LocalizedString SourceLogBreakStallsPassiveError(string sourceServerName, string error)
		{
			return new LocalizedString("SourceLogBreakStallsPassiveError", "ExB93F7E", false, true, ReplayStrings.ResourceManager, new object[]
			{
				sourceServerName,
				error
			});
		}

		// Token: 0x060025D2 RID: 9682 RVA: 0x000B1654 File Offset: 0x000AF854
		public static LocalizedString TagHandlerSuspendCopy(string suspendReason)
		{
			return new LocalizedString("TagHandlerSuspendCopy", "Ex8F58E2", false, true, ReplayStrings.ResourceManager, new object[]
			{
				suspendReason
			});
		}

		// Token: 0x060025D3 RID: 9683 RVA: 0x000B1684 File Offset: 0x000AF884
		public static LocalizedString TPRExchangeListenerNotResponding(string reason)
		{
			return new LocalizedString("TPRExchangeListenerNotResponding", "Ex423627", false, true, ReplayStrings.ResourceManager, new object[]
			{
				reason
			});
		}

		// Token: 0x060025D4 RID: 9684 RVA: 0x000B16B4 File Offset: 0x000AF8B4
		public static LocalizedString EnableReplayLagAlreadyDisabledFailedException(string dbCopy)
		{
			return new LocalizedString("EnableReplayLagAlreadyDisabledFailedException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				dbCopy
			});
		}

		// Token: 0x17000997 RID: 2455
		// (get) Token: 0x060025D5 RID: 9685 RVA: 0x000B16E3 File Offset: 0x000AF8E3
		public static LocalizedString SeederEcError
		{
			get
			{
				return new LocalizedString("SeederEcError", "Ex84746E", false, true, ReplayStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060025D6 RID: 9686 RVA: 0x000B1704 File Offset: 0x000AF904
		public static LocalizedString IncSeedDivergenceCheckFailedException(string dbName, string sourceServer, string error)
		{
			return new LocalizedString("IncSeedDivergenceCheckFailedException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				dbName,
				sourceServer,
				error
			});
		}

		// Token: 0x060025D7 RID: 9687 RVA: 0x000B173C File Offset: 0x000AF93C
		public static LocalizedString TPRInitFailure(string errMsg)
		{
			return new LocalizedString("TPRInitFailure", "Ex6D1D36", false, true, ReplayStrings.ResourceManager, new object[]
			{
				errMsg
			});
		}

		// Token: 0x060025D8 RID: 9688 RVA: 0x000B176C File Offset: 0x000AF96C
		public static LocalizedString FileCheckRequiredGenerationCorrupt(string file, long min, long max)
		{
			return new LocalizedString("FileCheckRequiredGenerationCorrupt", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				file,
				min,
				max
			});
		}

		// Token: 0x060025D9 RID: 9689 RVA: 0x000B17B0 File Offset: 0x000AF9B0
		public static LocalizedString AmCommonTransientException(string error)
		{
			return new LocalizedString("AmCommonTransientException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				error
			});
		}

		// Token: 0x060025DA RID: 9690 RVA: 0x000B17E0 File Offset: 0x000AF9E0
		public static LocalizedString ClusterBatchWriter_OpenClusterFailed(int ec)
		{
			return new LocalizedString("ClusterBatchWriter_OpenClusterFailed", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				ec
			});
		}

		// Token: 0x060025DB RID: 9691 RVA: 0x000B1814 File Offset: 0x000AFA14
		public static LocalizedString CIStatusFailedException(string server, string db)
		{
			return new LocalizedString("CIStatusFailedException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				server,
				db
			});
		}

		// Token: 0x17000998 RID: 2456
		// (get) Token: 0x060025DC RID: 9692 RVA: 0x000B1847 File Offset: 0x000AFA47
		public static LocalizedString AutoReseedFailedAdminSuspended
		{
			get
			{
				return new LocalizedString("AutoReseedFailedAdminSuspended", "", false, false, ReplayStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060025DD RID: 9693 RVA: 0x000B1868 File Offset: 0x000AFA68
		public static LocalizedString DumpsterSafetyNetRpcFailedException(string hubServerName, string rpcStatus)
		{
			return new LocalizedString("DumpsterSafetyNetRpcFailedException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				hubServerName,
				rpcStatus
			});
		}

		// Token: 0x17000999 RID: 2457
		// (get) Token: 0x060025DE RID: 9694 RVA: 0x000B189B File Offset: 0x000AFA9B
		public static LocalizedString NetworkNoUsableEndpoints
		{
			get
			{
				return new LocalizedString("NetworkNoUsableEndpoints", "Ex84B8D7", false, true, ReplayStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060025DF RID: 9695 RVA: 0x000B18BC File Offset: 0x000AFABC
		public static LocalizedString AmDbActionException(string actionError)
		{
			return new LocalizedString("AmDbActionException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				actionError
			});
		}

		// Token: 0x060025E0 RID: 9696 RVA: 0x000B18EC File Offset: 0x000AFAEC
		public static LocalizedString InstanceSuspendedAutoInitialSeed(string databaseName)
		{
			return new LocalizedString("InstanceSuspendedAutoInitialSeed", "Ex0E8355", false, true, ReplayStrings.ResourceManager, new object[]
			{
				databaseName
			});
		}

		// Token: 0x060025E1 RID: 9697 RVA: 0x000B191C File Offset: 0x000AFB1C
		public static LocalizedString ReseedCheckMissingLogfile(string logfile)
		{
			return new LocalizedString("ReseedCheckMissingLogfile", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				logfile
			});
		}

		// Token: 0x060025E2 RID: 9698 RVA: 0x000B194C File Offset: 0x000AFB4C
		public static LocalizedString LogCopierFailsBecauseLogGap(string srcServer, string missingFileName)
		{
			return new LocalizedString("LogCopierFailsBecauseLogGap", "Ex3EA540", false, true, ReplayStrings.ResourceManager, new object[]
			{
				srcServer,
				missingFileName
			});
		}

		// Token: 0x060025E3 RID: 9699 RVA: 0x000B1980 File Offset: 0x000AFB80
		public static LocalizedString LogDirectoryCreationDisabled(string directoryPath)
		{
			return new LocalizedString("LogDirectoryCreationDisabled", "ExE59C1D", false, true, ReplayStrings.ResourceManager, new object[]
			{
				directoryPath
			});
		}

		// Token: 0x060025E4 RID: 9700 RVA: 0x000B19B0 File Offset: 0x000AFBB0
		public static LocalizedString DbAvailabilityActiveCopyMountState(string dbName, string serverName, string copyStatus)
		{
			return new LocalizedString("DbAvailabilityActiveCopyMountState", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				dbName,
				serverName,
				copyStatus
			});
		}

		// Token: 0x060025E5 RID: 9701 RVA: 0x000B19E8 File Offset: 0x000AFBE8
		public static LocalizedString LogGapDetected(string file)
		{
			return new LocalizedString("LogGapDetected", "ExF43290", false, true, ReplayStrings.ResourceManager, new object[]
			{
				file
			});
		}

		// Token: 0x1700099A RID: 2458
		// (get) Token: 0x060025E6 RID: 9702 RVA: 0x000B1A17 File Offset: 0x000AFC17
		public static LocalizedString ClusterServiceMonitorCriticalError
		{
			get
			{
				return new LocalizedString("ClusterServiceMonitorCriticalError", "", false, false, ReplayStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060025E7 RID: 9703 RVA: 0x000B1A38 File Offset: 0x000AFC38
		public static LocalizedString FileCheckEseutilError(string errorMessage)
		{
			return new LocalizedString("FileCheckEseutilError", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				errorMessage
			});
		}

		// Token: 0x1700099B RID: 2459
		// (get) Token: 0x060025E8 RID: 9704 RVA: 0x000B1A67 File Offset: 0x000AFC67
		public static LocalizedString Resynchronizing
		{
			get
			{
				return new LocalizedString("Resynchronizing", "", false, false, ReplayStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060025E9 RID: 9705 RVA: 0x000B1A88 File Offset: 0x000AFC88
		public static LocalizedString ClusterBatchWriter_CreateBatchFailed(int ec)
		{
			return new LocalizedString("ClusterBatchWriter_CreateBatchFailed", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				ec
			});
		}

		// Token: 0x060025EA RID: 9706 RVA: 0x000B1ABC File Offset: 0x000AFCBC
		public static LocalizedString AmRefreshConfigTimeoutError(int timeoutSecs)
		{
			return new LocalizedString("AmRefreshConfigTimeoutError", "Ex80A46E", false, true, ReplayStrings.ResourceManager, new object[]
			{
				timeoutSecs
			});
		}

		// Token: 0x060025EB RID: 9707 RVA: 0x000B1AF0 File Offset: 0x000AFCF0
		public static LocalizedString AmDbOperationException(string opError)
		{
			return new LocalizedString("AmDbOperationException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				opError
			});
		}

		// Token: 0x060025EC RID: 9708 RVA: 0x000B1B20 File Offset: 0x000AFD20
		public static LocalizedString DbCopyNotTargetException(string dbName, string serverName)
		{
			return new LocalizedString("DbCopyNotTargetException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				dbName,
				serverName
			});
		}

		// Token: 0x060025ED RID: 9709 RVA: 0x000B1B54 File Offset: 0x000AFD54
		public static LocalizedString LogRepairDivergenceCheckFailedDueToCorruptEndOfLog(string logName, string exceptionText)
		{
			return new LocalizedString("LogRepairDivergenceCheckFailedDueToCorruptEndOfLog", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				logName,
				exceptionText
			});
		}

		// Token: 0x1700099C RID: 2460
		// (get) Token: 0x060025EE RID: 9710 RVA: 0x000B1B87 File Offset: 0x000AFD87
		public static LocalizedString ReplayServiceSuspendBlockedResynchronizingException
		{
			get
			{
				return new LocalizedString("ReplayServiceSuspendBlockedResynchronizingException", "", false, false, ReplayStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060025EF RID: 9711 RVA: 0x000B1BA8 File Offset: 0x000AFDA8
		public static LocalizedString AmBcsDatabaseCopyHostedOnTarget(string db, string server)
		{
			return new LocalizedString("AmBcsDatabaseCopyHostedOnTarget", "Ex632DC8", false, true, ReplayStrings.ResourceManager, new object[]
			{
				db,
				server
			});
		}

		// Token: 0x060025F0 RID: 9712 RVA: 0x000B1BDC File Offset: 0x000AFDDC
		public static LocalizedString SearchProxyRpcException(string msg)
		{
			return new LocalizedString("SearchProxyRpcException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				msg
			});
		}

		// Token: 0x060025F1 RID: 9713 RVA: 0x000B1C0C File Offset: 0x000AFE0C
		public static LocalizedString AutoReseedCatalogIsBehindBacklog(int backlog)
		{
			return new LocalizedString("AutoReseedCatalogIsBehindBacklog", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				backlog
			});
		}

		// Token: 0x060025F2 RID: 9714 RVA: 0x000B1C40 File Offset: 0x000AFE40
		public static LocalizedString CiSeederCatalogCouldNotPause(string ex)
		{
			return new LocalizedString("CiSeederCatalogCouldNotPause", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				ex
			});
		}

		// Token: 0x060025F3 RID: 9715 RVA: 0x000B1C70 File Offset: 0x000AFE70
		public static LocalizedString TPRChangeFailedBecauseAlreadyActive(string curServerName)
		{
			return new LocalizedString("TPRChangeFailedBecauseAlreadyActive", "ExF4AFD9", false, true, ReplayStrings.ResourceManager, new object[]
			{
				curServerName
			});
		}

		// Token: 0x060025F4 RID: 9716 RVA: 0x000B1CA0 File Offset: 0x000AFEA0
		public static LocalizedString DatabaseValidationException(string errorMsg)
		{
			return new LocalizedString("DatabaseValidationException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				errorMsg
			});
		}

		// Token: 0x060025F5 RID: 9717 RVA: 0x000B1CD0 File Offset: 0x000AFED0
		public static LocalizedString FileCheckLogfileMissing(string logfile)
		{
			return new LocalizedString("FileCheckLogfileMissing", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				logfile
			});
		}

		// Token: 0x060025F6 RID: 9718 RVA: 0x000B1D00 File Offset: 0x000AFF00
		public static LocalizedString FileCheckLogfileSignature(string logfile, string logfileSignature, string expectedSignature)
		{
			return new LocalizedString("FileCheckLogfileSignature", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				logfile,
				logfileSignature,
				expectedSignature
			});
		}

		// Token: 0x1700099D RID: 2461
		// (get) Token: 0x060025F7 RID: 9719 RVA: 0x000B1D37 File Offset: 0x000AFF37
		public static LocalizedString LockOwnerComponent
		{
			get
			{
				return new LocalizedString("LockOwnerComponent", "Ex2BE9EF", false, true, ReplayStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700099E RID: 2462
		// (get) Token: 0x060025F8 RID: 9720 RVA: 0x000B1D55 File Offset: 0x000AFF55
		public static LocalizedString NetworkIsDisabled
		{
			get
			{
				return new LocalizedString("NetworkIsDisabled", "ExB66A66", false, true, ReplayStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060025F9 RID: 9721 RVA: 0x000B1D74 File Offset: 0x000AFF74
		public static LocalizedString FileCheckJustCreatedEDB(string file)
		{
			return new LocalizedString("FileCheckJustCreatedEDB", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				file
			});
		}

		// Token: 0x060025FA RID: 9722 RVA: 0x000B1DA4 File Offset: 0x000AFFA4
		public static LocalizedString AmDbOperationTimedoutException(string dbName, string opr, TimeSpan timeout)
		{
			return new LocalizedString("AmDbOperationTimedoutException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				dbName,
				opr,
				timeout
			});
		}

		// Token: 0x1700099F RID: 2463
		// (get) Token: 0x060025FB RID: 9723 RVA: 0x000B1DE0 File Offset: 0x000AFFE0
		public static LocalizedString ResumeOperationName
		{
			get
			{
				return new LocalizedString("ResumeOperationName", "", false, false, ReplayStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170009A0 RID: 2464
		// (get) Token: 0x060025FC RID: 9724 RVA: 0x000B1DFE File Offset: 0x000AFFFE
		public static LocalizedString ReplayServiceSuspendReseedBlockedException
		{
			get
			{
				return new LocalizedString("ReplayServiceSuspendReseedBlockedException", "", false, false, ReplayStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170009A1 RID: 2465
		// (get) Token: 0x060025FD RID: 9725 RVA: 0x000B1E1C File Offset: 0x000B001C
		public static LocalizedString SuspendMessageWriteFailedException
		{
			get
			{
				return new LocalizedString("SuspendMessageWriteFailedException", "", false, false, ReplayStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060025FE RID: 9726 RVA: 0x000B1E3C File Offset: 0x000B003C
		public static LocalizedString AmDbActionWrapperException(string dbActionError)
		{
			return new LocalizedString("AmDbActionWrapperException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				dbActionError
			});
		}

		// Token: 0x060025FF RID: 9727 RVA: 0x000B1E6C File Offset: 0x000B006C
		public static LocalizedString UnexpectedEOF(string filename)
		{
			return new LocalizedString("UnexpectedEOF", "Ex012C8C", false, true, ReplayStrings.ResourceManager, new object[]
			{
				filename
			});
		}

		// Token: 0x170009A2 RID: 2466
		// (get) Token: 0x06002600 RID: 9728 RVA: 0x000B1E9B File Offset: 0x000B009B
		public static LocalizedString SyncSuspendResumeOperationName
		{
			get
			{
				return new LocalizedString("SyncSuspendResumeOperationName", "", false, false, ReplayStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06002601 RID: 9729 RVA: 0x000B1EBC File Offset: 0x000B00BC
		public static LocalizedString DbDriveNotBigEnough(string path, long expectedSize, ulong actualSize)
		{
			return new LocalizedString("DbDriveNotBigEnough", "Ex1081AE", false, true, ReplayStrings.ResourceManager, new object[]
			{
				path,
				expectedSize,
				actualSize
			});
		}

		// Token: 0x06002602 RID: 9730 RVA: 0x000B1F00 File Offset: 0x000B0100
		public static LocalizedString PagePatchApiFailedException(string msg)
		{
			return new LocalizedString("PagePatchApiFailedException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				msg
			});
		}

		// Token: 0x06002603 RID: 9731 RVA: 0x000B1F30 File Offset: 0x000B0130
		public static LocalizedString AutoReseedCatalogToUpgrade(int current, int latest)
		{
			return new LocalizedString("AutoReseedCatalogToUpgrade", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				current,
				latest
			});
		}

		// Token: 0x06002604 RID: 9732 RVA: 0x000B1F70 File Offset: 0x000B0170
		public static LocalizedString RlmDatabaseCopyInvalidException(string databaseName, string serverName)
		{
			return new LocalizedString("RlmDatabaseCopyInvalidException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				databaseName,
				serverName
			});
		}

		// Token: 0x06002605 RID: 9733 RVA: 0x000B1FA4 File Offset: 0x000B01A4
		public static LocalizedString LogRepairDivergenceCheckFailedError(string localEndOfLogFilename, string remoteDataInTempFilename, string exceptionText)
		{
			return new LocalizedString("LogRepairDivergenceCheckFailedError", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				localEndOfLogFilename,
				remoteDataInTempFilename,
				exceptionText
			});
		}

		// Token: 0x170009A3 RID: 2467
		// (get) Token: 0x06002606 RID: 9734 RVA: 0x000B1FDB File Offset: 0x000B01DB
		public static LocalizedString FailedAndSuspended
		{
			get
			{
				return new LocalizedString("FailedAndSuspended", "", false, false, ReplayStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06002607 RID: 9735 RVA: 0x000B1FFC File Offset: 0x000B01FC
		public static LocalizedString DbValidationActiveCopyStatusUnknown(string dbName)
		{
			return new LocalizedString("DbValidationActiveCopyStatusUnknown", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				dbName
			});
		}

		// Token: 0x06002608 RID: 9736 RVA: 0x000B202C File Offset: 0x000B022C
		public static LocalizedString SeederReplayServiceDownException(string serverName, string rpcErrorMessage)
		{
			return new LocalizedString("SeederReplayServiceDownException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				serverName,
				rpcErrorMessage
			});
		}

		// Token: 0x170009A4 RID: 2468
		// (get) Token: 0x06002609 RID: 9737 RVA: 0x000B205F File Offset: 0x000B025F
		public static LocalizedString TPRProviderNotListening
		{
			get
			{
				return new LocalizedString("TPRProviderNotListening", "Ex5CF21B", false, true, ReplayStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170009A5 RID: 2469
		// (get) Token: 0x0600260A RID: 9738 RVA: 0x000B207D File Offset: 0x000B027D
		public static LocalizedString Suspended
		{
			get
			{
				return new LocalizedString("Suspended", "", false, false, ReplayStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600260B RID: 9739 RVA: 0x000B209C File Offset: 0x000B029C
		public static LocalizedString DbMoveSkippedBecauseNotActive(string dbName, string serverName, string activeServerName)
		{
			return new LocalizedString("DbMoveSkippedBecauseNotActive", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				dbName,
				serverName,
				activeServerName
			});
		}

		// Token: 0x170009A6 RID: 2470
		// (get) Token: 0x0600260C RID: 9740 RVA: 0x000B20D3 File Offset: 0x000B02D3
		public static LocalizedString ReplayServiceSuspendInPlaceReseedBlockedException
		{
			get
			{
				return new LocalizedString("ReplayServiceSuspendInPlaceReseedBlockedException", "", false, false, ReplayStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600260D RID: 9741 RVA: 0x000B20F4 File Offset: 0x000B02F4
		public static LocalizedString LogInspectorSignatureMismatch(string logfileInspected, long genFromHeader)
		{
			return new LocalizedString("LogInspectorSignatureMismatch", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				logfileInspected,
				genFromHeader
			});
		}

		// Token: 0x0600260E RID: 9742 RVA: 0x000B212C File Offset: 0x000B032C
		public static LocalizedString GranularReplicationMsgSequenceError(string msgContext)
		{
			return new LocalizedString("GranularReplicationMsgSequenceError", "Ex5A0B0D", false, true, ReplayStrings.ResourceManager, new object[]
			{
				msgContext
			});
		}

		// Token: 0x0600260F RID: 9743 RVA: 0x000B215C File Offset: 0x000B035C
		public static LocalizedString PagePatchTooManyPagesToPatchException(int numPages, int maxSupported)
		{
			return new LocalizedString("PagePatchTooManyPagesToPatchException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				numPages,
				maxSupported
			});
		}

		// Token: 0x06002610 RID: 9744 RVA: 0x000B219C File Offset: 0x000B039C
		public static LocalizedString LogRepairNotPossible(string reason)
		{
			return new LocalizedString("LogRepairNotPossible", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				reason
			});
		}

		// Token: 0x06002611 RID: 9745 RVA: 0x000B21CC File Offset: 0x000B03CC
		public static LocalizedString AmRoleChangedWhileOperationIsInProgress(string roleStart, string roleCurrent)
		{
			return new LocalizedString("AmRoleChangedWhileOperationIsInProgress", "ExCDF904", false, true, ReplayStrings.ResourceManager, new object[]
			{
				roleStart,
				roleCurrent
			});
		}

		// Token: 0x06002612 RID: 9746 RVA: 0x000B2200 File Offset: 0x000B0400
		public static LocalizedString AmPreMountCallbackFailedNoReplicaInstanceException(string dbName, string server)
		{
			return new LocalizedString("AmPreMountCallbackFailedNoReplicaInstanceException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				dbName,
				server
			});
		}

		// Token: 0x170009A7 RID: 2471
		// (get) Token: 0x06002613 RID: 9747 RVA: 0x000B2233 File Offset: 0x000B0433
		public static LocalizedString AutoReseedMoveActiveBeforeRebuildCatalog
		{
			get
			{
				return new LocalizedString("AutoReseedMoveActiveBeforeRebuildCatalog", "", false, false, ReplayStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06002614 RID: 9748 RVA: 0x000B2254 File Offset: 0x000B0454
		public static LocalizedString DagTaskMovingPam(string serverName)
		{
			return new LocalizedString("DagTaskMovingPam", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				serverName
			});
		}

		// Token: 0x170009A8 RID: 2472
		// (get) Token: 0x06002615 RID: 9749 RVA: 0x000B2283 File Offset: 0x000B0483
		public static LocalizedString ErrorCouldNotConnectClusterForAmConfig
		{
			get
			{
				return new LocalizedString("ErrorCouldNotConnectClusterForAmConfig", "ExFDD9FF", false, true, ReplayStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06002616 RID: 9750 RVA: 0x000B22A4 File Offset: 0x000B04A4
		public static LocalizedString ReplayServiceSuspendRpcInvalidForActiveCopyException(string dbCopy)
		{
			return new LocalizedString("ReplayServiceSuspendRpcInvalidForActiveCopyException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				dbCopy
			});
		}

		// Token: 0x06002617 RID: 9751 RVA: 0x000B22D4 File Offset: 0x000B04D4
		public static LocalizedString ReplayServiceTooMuchMemoryException(double memoryUsageInMib, long maximumMemoryUsageInMib)
		{
			return new LocalizedString("ReplayServiceTooMuchMemoryException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				memoryUsageInMib,
				maximumMemoryUsageInMib
			});
		}

		// Token: 0x06002618 RID: 9752 RVA: 0x000B2314 File Offset: 0x000B0514
		public static LocalizedString LogInspectorCouldNotMoveLogFileException(string oldpath, string newpath, string error)
		{
			return new LocalizedString("LogInspectorCouldNotMoveLogFileException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				oldpath,
				newpath,
				error
			});
		}

		// Token: 0x06002619 RID: 9753 RVA: 0x000B234C File Offset: 0x000B054C
		public static LocalizedString ServerMoveAllDatabasesFailed(int numberFailedMoves)
		{
			return new LocalizedString("ServerMoveAllDatabasesFailed", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				numberFailedMoves
			});
		}

		// Token: 0x0600261A RID: 9754 RVA: 0x000B2380 File Offset: 0x000B0580
		public static LocalizedString CouldNotMoveLogFile(string oldpath, string newpath)
		{
			return new LocalizedString("CouldNotMoveLogFile", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				oldpath,
				newpath
			});
		}

		// Token: 0x0600261B RID: 9755 RVA: 0x000B23B4 File Offset: 0x000B05B4
		public static LocalizedString DagTaskInstallingFailoverClustering(string serverName)
		{
			return new LocalizedString("DagTaskInstallingFailoverClustering", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				serverName
			});
		}

		// Token: 0x0600261C RID: 9756 RVA: 0x000B23E4 File Offset: 0x000B05E4
		public static LocalizedString AmBcsDatabaseCopyIsHAComponentOffline(string db, string server)
		{
			return new LocalizedString("AmBcsDatabaseCopyIsHAComponentOffline", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				db,
				server
			});
		}

		// Token: 0x0600261D RID: 9757 RVA: 0x000B2418 File Offset: 0x000B0618
		public static LocalizedString CopyStatusIsNotHealthy(string server, string db, string status)
		{
			return new LocalizedString("CopyStatusIsNotHealthy", "ExF39BB2", false, true, ReplayStrings.ResourceManager, new object[]
			{
				server,
				db,
				status
			});
		}

		// Token: 0x170009A9 RID: 2473
		// (get) Token: 0x0600261E RID: 9758 RVA: 0x000B244F File Offset: 0x000B064F
		public static LocalizedString ReplayServiceShuttingDownException
		{
			get
			{
				return new LocalizedString("ReplayServiceShuttingDownException", "", false, false, ReplayStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600261F RID: 9759 RVA: 0x000B2470 File Offset: 0x000B0670
		public static LocalizedString AmLastServerTimeStampCorruptedException(string property, string corruptedValue)
		{
			return new LocalizedString("AmLastServerTimeStampCorruptedException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				property,
				corruptedValue
			});
		}

		// Token: 0x06002620 RID: 9760 RVA: 0x000B24A4 File Offset: 0x000B06A4
		public static LocalizedString PagePatchInvalidPageSizeException(long dataSize, long expectedPageSize)
		{
			return new LocalizedString("PagePatchInvalidPageSizeException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				dataSize,
				expectedPageSize
			});
		}

		// Token: 0x06002621 RID: 9761 RVA: 0x000B24E4 File Offset: 0x000B06E4
		public static LocalizedString FileStateInternalError(string condition)
		{
			return new LocalizedString("FileStateInternalError", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				condition
			});
		}

		// Token: 0x06002622 RID: 9762 RVA: 0x000B2514 File Offset: 0x000B0714
		public static LocalizedString SeederRpcUnsupportedException(string serverName, string serverVersion, string supportedVersion)
		{
			return new LocalizedString("SeederRpcUnsupportedException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				serverName,
				serverVersion,
				supportedVersion
			});
		}

		// Token: 0x170009AA RID: 2474
		// (get) Token: 0x06002623 RID: 9763 RVA: 0x000B254B File Offset: 0x000B074B
		public static LocalizedString ErrorFailedToOpenClusterObjects
		{
			get
			{
				return new LocalizedString("ErrorFailedToOpenClusterObjects", "Ex2A5125", false, true, ReplayStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170009AB RID: 2475
		// (get) Token: 0x06002624 RID: 9764 RVA: 0x000B2569 File Offset: 0x000B0769
		public static LocalizedString FailedToOpenShipLogContextStoreStopped
		{
			get
			{
				return new LocalizedString("FailedToOpenShipLogContextStoreStopped", "", false, false, ReplayStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06002625 RID: 9765 RVA: 0x000B2588 File Offset: 0x000B0788
		public static LocalizedString WarningPerformingFastOperationException(string db, string error)
		{
			return new LocalizedString("WarningPerformingFastOperationException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				db,
				error
			});
		}

		// Token: 0x06002626 RID: 9766 RVA: 0x000B25BC File Offset: 0x000B07BC
		public static LocalizedString SeedInProgressException(string errMessage)
		{
			return new LocalizedString("SeedInProgressException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				errMessage
			});
		}

		// Token: 0x06002627 RID: 9767 RVA: 0x000B25EC File Offset: 0x000B07EC
		public static LocalizedString AmMountTimeoutError(string dbName, string serverName, int timeoutInSecs)
		{
			return new LocalizedString("AmMountTimeoutError", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				dbName,
				serverName,
				timeoutInSecs
			});
		}

		// Token: 0x06002628 RID: 9768 RVA: 0x000B2628 File Offset: 0x000B0828
		public static LocalizedString LastLogReplacementTempNewFileNotDeletedException(string dbCopy, string tempNewFile, string error)
		{
			return new LocalizedString("LastLogReplacementTempNewFileNotDeletedException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				dbCopy,
				tempNewFile,
				error
			});
		}

		// Token: 0x06002629 RID: 9769 RVA: 0x000B2660 File Offset: 0x000B0860
		public static LocalizedString CiSeederExchangeSearchPermanentException(string message)
		{
			return new LocalizedString("CiSeederExchangeSearchPermanentException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				message
			});
		}

		// Token: 0x0600262A RID: 9770 RVA: 0x000B2690 File Offset: 0x000B0890
		public static LocalizedString ReplayServiceTooManyHandlesException(long numberOfHandles, long maxNumberOfHandles)
		{
			return new LocalizedString("ReplayServiceTooManyHandlesException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				numberOfHandles,
				maxNumberOfHandles
			});
		}

		// Token: 0x170009AC RID: 2476
		// (get) Token: 0x0600262B RID: 9771 RVA: 0x000B26CD File Offset: 0x000B08CD
		public static LocalizedString NullDatabaseException
		{
			get
			{
				return new LocalizedString("NullDatabaseException", "", false, false, ReplayStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600262C RID: 9772 RVA: 0x000B26EC File Offset: 0x000B08EC
		public static LocalizedString AmBcsTargetServerIsStoppedOnDAC(string server)
		{
			return new LocalizedString("AmBcsTargetServerIsStoppedOnDAC", "ExBEB4AA", false, true, ReplayStrings.ResourceManager, new object[]
			{
				server
			});
		}

		// Token: 0x0600262D RID: 9773 RVA: 0x000B271C File Offset: 0x000B091C
		public static LocalizedString DagTaskNetFtProblem(int specificErrorCode)
		{
			return new LocalizedString("DagTaskNetFtProblem", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				specificErrorCode
			});
		}

		// Token: 0x170009AD RID: 2477
		// (get) Token: 0x0600262E RID: 9774 RVA: 0x000B2750 File Offset: 0x000B0950
		public static LocalizedString SeederEcCommunicationsError
		{
			get
			{
				return new LocalizedString("SeederEcCommunicationsError", "ExE04686", false, true, ReplayStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600262F RID: 9775 RVA: 0x000B2770 File Offset: 0x000B0970
		public static LocalizedString AmDbRemountSkippedSinceDatabaseWasAdminDismounted(string dbName)
		{
			return new LocalizedString("AmDbRemountSkippedSinceDatabaseWasAdminDismounted", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				dbName
			});
		}

		// Token: 0x170009AE RID: 2478
		// (get) Token: 0x06002630 RID: 9776 RVA: 0x000B279F File Offset: 0x000B099F
		public static LocalizedString DagTaskPamNotMovedSubsequentOperationsMayBeSlowOrUnreliable
		{
			get
			{
				return new LocalizedString("DagTaskPamNotMovedSubsequentOperationsMayBeSlowOrUnreliable", "", false, false, ReplayStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170009AF RID: 2479
		// (get) Token: 0x06002631 RID: 9777 RVA: 0x000B27BD File Offset: 0x000B09BD
		public static LocalizedString SeederEcFailAcqRight
		{
			get
			{
				return new LocalizedString("SeederEcFailAcqRight", "Ex7B8F91", false, true, ReplayStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06002632 RID: 9778 RVA: 0x000B27DC File Offset: 0x000B09DC
		public static LocalizedString DagTaskNotEnoughStaticIPAddresses(string network, string staticIps)
		{
			return new LocalizedString("DagTaskNotEnoughStaticIPAddresses", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				network,
				staticIps
			});
		}

		// Token: 0x06002633 RID: 9779 RVA: 0x000B2810 File Offset: 0x000B0A10
		public static LocalizedString AmClusterException(string clusterError)
		{
			return new LocalizedString("AmClusterException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				clusterError
			});
		}

		// Token: 0x06002634 RID: 9780 RVA: 0x000B2840 File Offset: 0x000B0A40
		public static LocalizedString SeedDivergenceFailedException(string targetCopyName, string divergenceFileName, string errorMsg)
		{
			return new LocalizedString("SeedDivergenceFailedException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				targetCopyName,
				divergenceFileName,
				errorMsg
			});
		}

		// Token: 0x170009B0 RID: 2480
		// (get) Token: 0x06002635 RID: 9781 RVA: 0x000B2877 File Offset: 0x000B0A77
		public static LocalizedString ProgressStatusInProgress
		{
			get
			{
				return new LocalizedString("ProgressStatusInProgress", "Ex3EC6C5", false, true, ReplayStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06002636 RID: 9782 RVA: 0x000B2898 File Offset: 0x000B0A98
		public static LocalizedString SeederInstanceReseedBlockedException(string dbCopyName, string errorMsg)
		{
			return new LocalizedString("SeederInstanceReseedBlockedException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				dbCopyName,
				errorMsg
			});
		}

		// Token: 0x170009B1 RID: 2481
		// (get) Token: 0x06002637 RID: 9783 RVA: 0x000B28CB File Offset: 0x000B0ACB
		public static LocalizedString CouldNotGetMountStatus
		{
			get
			{
				return new LocalizedString("CouldNotGetMountStatus", "", false, false, ReplayStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06002638 RID: 9784 RVA: 0x000B28EC File Offset: 0x000B0AEC
		public static LocalizedString DagTaskComputerAccountCouldNotBeValidatedAccessDeniedException(string computerAccount, string userAccount)
		{
			return new LocalizedString("DagTaskComputerAccountCouldNotBeValidatedAccessDeniedException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				computerAccount,
				userAccount
			});
		}

		// Token: 0x06002639 RID: 9785 RVA: 0x000B2920 File Offset: 0x000B0B20
		public static LocalizedString ReplayServiceSuspendRpcInvalidSeedingSourceException(string dbCopy)
		{
			return new LocalizedString("ReplayServiceSuspendRpcInvalidSeedingSourceException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				dbCopy
			});
		}

		// Token: 0x0600263A RID: 9786 RVA: 0x000B2950 File Offset: 0x000B0B50
		public static LocalizedString FailedToTruncateLocallyException(uint hresult, string optionalFriendlyError)
		{
			return new LocalizedString("FailedToTruncateLocallyException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				hresult,
				optionalFriendlyError
			});
		}

		// Token: 0x170009B2 RID: 2482
		// (get) Token: 0x0600263B RID: 9787 RVA: 0x000B2988 File Offset: 0x000B0B88
		public static LocalizedString AmClusterNotRunningException
		{
			get
			{
				return new LocalizedString("AmClusterNotRunningException", "", false, false, ReplayStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170009B3 RID: 2483
		// (get) Token: 0x0600263C RID: 9788 RVA: 0x000B29A6 File Offset: 0x000B0BA6
		public static LocalizedString LockOwnerConfigChecker
		{
			get
			{
				return new LocalizedString("LockOwnerConfigChecker", "Ex662E4A", false, true, ReplayStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600263D RID: 9789 RVA: 0x000B29C4 File Offset: 0x000B0BC4
		public static LocalizedString ReplayDbOperationException(string opError)
		{
			return new LocalizedString("ReplayDbOperationException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				opError
			});
		}

		// Token: 0x170009B4 RID: 2484
		// (get) Token: 0x0600263E RID: 9790 RVA: 0x000B29F3 File Offset: 0x000B0BF3
		public static LocalizedString PrepareToStopCalled
		{
			get
			{
				return new LocalizedString("PrepareToStopCalled", "", false, false, ReplayStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170009B5 RID: 2485
		// (get) Token: 0x0600263F RID: 9791 RVA: 0x000B2A11 File Offset: 0x000B0C11
		public static LocalizedString ReplayServiceSuspendWantedClearedException
		{
			get
			{
				return new LocalizedString("ReplayServiceSuspendWantedClearedException", "", false, false, ReplayStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06002640 RID: 9792 RVA: 0x000B2A30 File Offset: 0x000B0C30
		public static LocalizedString AmDbActionRejectedAdminDismountedException(string actionCode)
		{
			return new LocalizedString("AmDbActionRejectedAdminDismountedException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				actionCode
			});
		}

		// Token: 0x06002641 RID: 9793 RVA: 0x000B2A60 File Offset: 0x000B0C60
		public static LocalizedString ExchangeVolumeInfoMultipleExMountPointsException(string volumeName, string exVolRootPath, string exMountPoints)
		{
			return new LocalizedString("ExchangeVolumeInfoMultipleExMountPointsException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				volumeName,
				exVolRootPath,
				exMountPoints
			});
		}

		// Token: 0x06002642 RID: 9794 RVA: 0x000B2A98 File Offset: 0x000B0C98
		public static LocalizedString SeederInstanceInvalidStateForEndException(string dbGuid)
		{
			return new LocalizedString("SeederInstanceInvalidStateForEndException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				dbGuid
			});
		}

		// Token: 0x06002643 RID: 9795 RVA: 0x000B2AC8 File Offset: 0x000B0CC8
		public static LocalizedString ServerNotFoundException(string serverName)
		{
			return new LocalizedString("ServerNotFoundException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				serverName
			});
		}

		// Token: 0x170009B6 RID: 2486
		// (get) Token: 0x06002644 RID: 9796 RVA: 0x000B2AF7 File Offset: 0x000B0CF7
		public static LocalizedString DBCHasNoValidTargetEdbPath
		{
			get
			{
				return new LocalizedString("DBCHasNoValidTargetEdbPath", "ExFC74E4", false, true, ReplayStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06002645 RID: 9797 RVA: 0x000B2B18 File Offset: 0x000B0D18
		public static LocalizedString SeederServerTransientException(string errorMessage)
		{
			return new LocalizedString("SeederServerTransientException", "Ex21B881", false, true, ReplayStrings.ResourceManager, new object[]
			{
				errorMessage
			});
		}

		// Token: 0x06002646 RID: 9798 RVA: 0x000B2B48 File Offset: 0x000B0D48
		public static LocalizedString ReplayConfigPropException(string id, string propertyName)
		{
			return new LocalizedString("ReplayConfigPropException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				id,
				propertyName
			});
		}

		// Token: 0x170009B7 RID: 2487
		// (get) Token: 0x06002647 RID: 9799 RVA: 0x000B2B7B File Offset: 0x000B0D7B
		public static LocalizedString DeleteChkptReasonCorrupted
		{
			get
			{
				return new LocalizedString("DeleteChkptReasonCorrupted", "", false, false, ReplayStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170009B8 RID: 2488
		// (get) Token: 0x06002648 RID: 9800 RVA: 0x000B2B99 File Offset: 0x000B0D99
		public static LocalizedString TPRChangeFailedBecauseNotDismounted
		{
			get
			{
				return new LocalizedString("TPRChangeFailedBecauseNotDismounted", "Ex7237ED", false, true, ReplayStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06002649 RID: 9801 RVA: 0x000B2BB8 File Offset: 0x000B0DB8
		public static LocalizedString PagePatchInvalidFileException(string patchFile)
		{
			return new LocalizedString("PagePatchInvalidFileException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				patchFile
			});
		}

		// Token: 0x170009B9 RID: 2489
		// (get) Token: 0x0600264A RID: 9802 RVA: 0x000B2BE7 File Offset: 0x000B0DE7
		public static LocalizedString CouldNotFindVolumeForFormatException
		{
			get
			{
				return new LocalizedString("CouldNotFindVolumeForFormatException", "", false, false, ReplayStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600264B RID: 9803 RVA: 0x000B2C08 File Offset: 0x000B0E08
		public static LocalizedString SeedPrepareException(string errMessage)
		{
			return new LocalizedString("SeedPrepareException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				errMessage
			});
		}

		// Token: 0x0600264C RID: 9804 RVA: 0x000B2C38 File Offset: 0x000B0E38
		public static LocalizedString FileCheckRequiredLogfileGapException(string logfile)
		{
			return new LocalizedString("FileCheckRequiredLogfileGapException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				logfile
			});
		}

		// Token: 0x0600264D RID: 9805 RVA: 0x000B2C68 File Offset: 0x000B0E68
		public static LocalizedString DeleteChkptReasonForce(long checkpointGeneration)
		{
			return new LocalizedString("DeleteChkptReasonForce", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				checkpointGeneration
			});
		}

		// Token: 0x0600264E RID: 9806 RVA: 0x000B2C9C File Offset: 0x000B0E9C
		public static LocalizedString DbValidationInspectorQueueLengthTooHigh(string dbCopyName, long length, long maxLength)
		{
			return new LocalizedString("DbValidationInspectorQueueLengthTooHigh", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				dbCopyName,
				length,
				maxLength
			});
		}

		// Token: 0x170009BA RID: 2490
		// (get) Token: 0x0600264F RID: 9807 RVA: 0x000B2CDD File Offset: 0x000B0EDD
		public static LocalizedString CannotChangeName
		{
			get
			{
				return new LocalizedString("CannotChangeName", "Ex07B680", false, true, ReplayStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06002650 RID: 9808 RVA: 0x000B2CFC File Offset: 0x000B0EFC
		public static LocalizedString AutoReseedPrereqFailedException(string databaseName, string serverName, string error)
		{
			return new LocalizedString("AutoReseedPrereqFailedException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				databaseName,
				serverName,
				error
			});
		}

		// Token: 0x170009BB RID: 2491
		// (get) Token: 0x06002651 RID: 9809 RVA: 0x000B2D33 File Offset: 0x000B0F33
		public static LocalizedString NetworkManagerInitError
		{
			get
			{
				return new LocalizedString("NetworkManagerInitError", "Ex5905F3", false, true, ReplayStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170009BC RID: 2492
		// (get) Token: 0x06002652 RID: 9810 RVA: 0x000B2D51 File Offset: 0x000B0F51
		public static LocalizedString PassiveCopyDisconnected
		{
			get
			{
				return new LocalizedString("PassiveCopyDisconnected", "", false, false, ReplayStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06002653 RID: 9811 RVA: 0x000B2D70 File Offset: 0x000B0F70
		public static LocalizedString CopyUnknownToActiveLogTruncationException(string db, string activeNode, string targetNode, uint hresult)
		{
			return new LocalizedString("CopyUnknownToActiveLogTruncationException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				db,
				activeNode,
				targetNode,
				hresult
			});
		}

		// Token: 0x06002654 RID: 9812 RVA: 0x000B2DB0 File Offset: 0x000B0FB0
		public static LocalizedString AutoReseedFailedInPlaceReseedBlocked(string error)
		{
			return new LocalizedString("AutoReseedFailedInPlaceReseedBlocked", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				error
			});
		}

		// Token: 0x06002655 RID: 9813 RVA: 0x000B2DE0 File Offset: 0x000B0FE0
		public static LocalizedString TPRChangeFailedServerValidation(string databaseName, string curServerName, string validationError)
		{
			return new LocalizedString("TPRChangeFailedServerValidation", "Ex2A382B", false, true, ReplayStrings.ResourceManager, new object[]
			{
				databaseName,
				curServerName,
				validationError
			});
		}

		// Token: 0x06002656 RID: 9814 RVA: 0x000B2E18 File Offset: 0x000B1018
		public static LocalizedString AmDbMoveSkippedSinceMasterChanged(string dbName)
		{
			return new LocalizedString("AmDbMoveSkippedSinceMasterChanged", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				dbName
			});
		}

		// Token: 0x06002657 RID: 9815 RVA: 0x000B2E48 File Offset: 0x000B1048
		public static LocalizedString HungDetectionGumIdChanged(int localGumId, int remoteGumId, string lockOwnerName, long hungNodesMask)
		{
			return new LocalizedString("HungDetectionGumIdChanged", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				localGumId,
				remoteGumId,
				lockOwnerName,
				hungNodesMask
			});
		}

		// Token: 0x06002658 RID: 9816 RVA: 0x000B2E94 File Offset: 0x000B1094
		public static LocalizedString SeederInstanceAlreadyCancelledException(string sourceMachine)
		{
			return new LocalizedString("SeederInstanceAlreadyCancelledException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				sourceMachine
			});
		}

		// Token: 0x06002659 RID: 9817 RVA: 0x000B2EC4 File Offset: 0x000B10C4
		public static LocalizedString CancelSeedingDueToConfigChangeOrServiceShutdown(string id, string machine, string reasonCode)
		{
			return new LocalizedString("CancelSeedingDueToConfigChangeOrServiceShutdown", "ExBFF2AD", false, true, ReplayStrings.ResourceManager, new object[]
			{
				id,
				machine,
				reasonCode
			});
		}

		// Token: 0x170009BD RID: 2493
		// (get) Token: 0x0600265A RID: 9818 RVA: 0x000B2EFB File Offset: 0x000B10FB
		public static LocalizedString SeederEchrErrorFromESECall
		{
			get
			{
				return new LocalizedString("SeederEchrErrorFromESECall", "Ex7E026C", false, true, ReplayStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600265B RID: 9819 RVA: 0x000B2F1C File Offset: 0x000B111C
		public static LocalizedString ClusterNetworkNullSubnetError(string clusterNetworkName)
		{
			return new LocalizedString("ClusterNetworkNullSubnetError", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				clusterNetworkName
			});
		}

		// Token: 0x170009BE RID: 2494
		// (get) Token: 0x0600265C RID: 9820 RVA: 0x000B2F4B File Offset: 0x000B114B
		public static LocalizedString ReplayServiceSuspendBlockedBackupInProgressException
		{
			get
			{
				return new LocalizedString("ReplayServiceSuspendBlockedBackupInProgressException", "", false, false, ReplayStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170009BF RID: 2495
		// (get) Token: 0x0600265D RID: 9821 RVA: 0x000B2F69 File Offset: 0x000B1169
		public static LocalizedString ErrorCouldNotFindServerForAmConfig
		{
			get
			{
				return new LocalizedString("ErrorCouldNotFindServerForAmConfig", "Ex3242AD", false, true, ReplayStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600265E RID: 9822 RVA: 0x000B2F88 File Offset: 0x000B1188
		public static LocalizedString TPRProviderNotResponding(string reason)
		{
			return new LocalizedString("TPRProviderNotResponding", "ExB10FA4", false, true, ReplayStrings.ResourceManager, new object[]
			{
				reason
			});
		}

		// Token: 0x0600265F RID: 9823 RVA: 0x000B2FB8 File Offset: 0x000B11B8
		public static LocalizedString SpareConflictInLayoutException(int spares)
		{
			return new LocalizedString("SpareConflictInLayoutException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				spares
			});
		}

		// Token: 0x06002660 RID: 9824 RVA: 0x000B2FEC File Offset: 0x000B11EC
		public static LocalizedString ErrorDagMisconfiguredForAmConfig(string serverName, string dagName)
		{
			return new LocalizedString("ErrorDagMisconfiguredForAmConfig", "Ex0EB713", false, true, ReplayStrings.ResourceManager, new object[]
			{
				serverName,
				dagName
			});
		}

		// Token: 0x06002661 RID: 9825 RVA: 0x000B3020 File Offset: 0x000B1220
		public static LocalizedString AmBcsFailedToQueryCopiesException(string dbName, string queryError)
		{
			return new LocalizedString("AmBcsFailedToQueryCopiesException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				dbName,
				queryError
			});
		}

		// Token: 0x06002662 RID: 9826 RVA: 0x000B3054 File Offset: 0x000B1254
		public static LocalizedString ReplayServiceTooManyThreadsException(long numberOfThreads, long maxNumberOfThreads)
		{
			return new LocalizedString("ReplayServiceTooManyThreadsException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				numberOfThreads,
				maxNumberOfThreads
			});
		}

		// Token: 0x06002663 RID: 9827 RVA: 0x000B3094 File Offset: 0x000B1294
		public static LocalizedString AmDbActionCancelledException(string dbName, string opr)
		{
			return new LocalizedString("AmDbActionCancelledException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				dbName,
				opr
			});
		}

		// Token: 0x06002664 RID: 9828 RVA: 0x000B30C8 File Offset: 0x000B12C8
		public static LocalizedString FileIOonSourceException(string serverName, string fileFullPath, string ioErrorMessage)
		{
			return new LocalizedString("FileIOonSourceException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				serverName,
				fileFullPath,
				ioErrorMessage
			});
		}

		// Token: 0x06002665 RID: 9829 RVA: 0x000B3100 File Offset: 0x000B1300
		public static LocalizedString FileOpenError(string fileName, string errMsg)
		{
			return new LocalizedString("FileOpenError", "ExBB666A", false, true, ReplayStrings.ResourceManager, new object[]
			{
				fileName,
				errMsg
			});
		}

		// Token: 0x06002666 RID: 9830 RVA: 0x000B3134 File Offset: 0x000B1334
		public static LocalizedString SeederEcTargetDbFileInUse(string dbFilePath)
		{
			return new LocalizedString("SeederEcTargetDbFileInUse", "Ex1A9177", false, true, ReplayStrings.ResourceManager, new object[]
			{
				dbFilePath
			});
		}

		// Token: 0x06002667 RID: 9831 RVA: 0x000B3164 File Offset: 0x000B1364
		public static LocalizedString AutoReseedUnhandledException(string databaseName, string serverName)
		{
			return new LocalizedString("AutoReseedUnhandledException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				databaseName,
				serverName
			});
		}

		// Token: 0x170009C0 RID: 2496
		// (get) Token: 0x06002668 RID: 9832 RVA: 0x000B3197 File Offset: 0x000B1397
		public static LocalizedString FailedAtReplacingLogFiles
		{
			get
			{
				return new LocalizedString("FailedAtReplacingLogFiles", "", false, false, ReplayStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06002669 RID: 9833 RVA: 0x000B31B8 File Offset: 0x000B13B8
		public static LocalizedString InvalidDbStateForIncReseed(string dbState)
		{
			return new LocalizedString("InvalidDbStateForIncReseed", "Ex659260", false, true, ReplayStrings.ResourceManager, new object[]
			{
				dbState
			});
		}

		// Token: 0x170009C1 RID: 2497
		// (get) Token: 0x0600266A RID: 9834 RVA: 0x000B31E7 File Offset: 0x000B13E7
		public static LocalizedString ReplayServiceResumeRpcFailedSeedingException
		{
			get
			{
				return new LocalizedString("ReplayServiceResumeRpcFailedSeedingException", "", false, false, ReplayStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600266B RID: 9835 RVA: 0x000B3208 File Offset: 0x000B1408
		public static LocalizedString LastLogReplacementUnexpectedTempFilesException(string dbCopy, string logPath)
		{
			return new LocalizedString("LastLogReplacementUnexpectedTempFilesException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				dbCopy,
				logPath
			});
		}

		// Token: 0x0600266C RID: 9836 RVA: 0x000B323C File Offset: 0x000B143C
		public static LocalizedString AmBcsDatabaseCopySeeding(string db, string server)
		{
			return new LocalizedString("AmBcsDatabaseCopySeeding", "ExA513C3", false, true, ReplayStrings.ResourceManager, new object[]
			{
				db,
				server
			});
		}

		// Token: 0x0600266D RID: 9837 RVA: 0x000B3270 File Offset: 0x000B1470
		public static LocalizedString AmInvalidDbState(Guid databaseGuid, string stateStr)
		{
			return new LocalizedString("AmInvalidDbState", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				databaseGuid,
				stateStr
			});
		}

		// Token: 0x170009C2 RID: 2498
		// (get) Token: 0x0600266E RID: 9838 RVA: 0x000B32A8 File Offset: 0x000B14A8
		public static LocalizedString StoreNotOnline
		{
			get
			{
				return new LocalizedString("StoreNotOnline", "", false, false, ReplayStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600266F RID: 9839 RVA: 0x000B32C8 File Offset: 0x000B14C8
		public static LocalizedString AmBcsGetCopyStatusRpcException(string server, string database, string rpcError)
		{
			return new LocalizedString("AmBcsGetCopyStatusRpcException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				server,
				database,
				rpcError
			});
		}

		// Token: 0x06002670 RID: 9840 RVA: 0x000B3300 File Offset: 0x000B1500
		public static LocalizedString AmDbMoveOperationSkippedException(string dbName, string reason)
		{
			return new LocalizedString("AmDbMoveOperationSkippedException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				dbName,
				reason
			});
		}

		// Token: 0x06002671 RID: 9841 RVA: 0x000B3334 File Offset: 0x000B1534
		public static LocalizedString SeederFailedToFindDirectory(string directory)
		{
			return new LocalizedString("SeederFailedToFindDirectory", "Ex656E3A", false, true, ReplayStrings.ResourceManager, new object[]
			{
				directory
			});
		}

		// Token: 0x06002672 RID: 9842 RVA: 0x000B3364 File Offset: 0x000B1564
		public static LocalizedString NetworkUnexpectedMessage(string nodeName, string messageText)
		{
			return new LocalizedString("NetworkUnexpectedMessage", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				nodeName,
				messageText
			});
		}

		// Token: 0x170009C3 RID: 2499
		// (get) Token: 0x06002673 RID: 9843 RVA: 0x000B3397 File Offset: 0x000B1597
		public static LocalizedString LockOwnerAttemptCopyLastLogs
		{
			get
			{
				return new LocalizedString("LockOwnerAttemptCopyLastLogs", "Ex35A464", false, true, ReplayStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06002674 RID: 9844 RVA: 0x000B33B8 File Offset: 0x000B15B8
		public static LocalizedString ClusterBatchWriter_OpenActiveManagerKeyFailed(int ec)
		{
			return new LocalizedString("ClusterBatchWriter_OpenActiveManagerKeyFailed", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				ec
			});
		}

		// Token: 0x06002675 RID: 9845 RVA: 0x000B33EC File Offset: 0x000B15EC
		public static LocalizedString SeederRpcSafeDeleteUnsupportedException(string serverName, string serverVersion, string supportedVersion)
		{
			return new LocalizedString("SeederRpcSafeDeleteUnsupportedException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				serverName,
				serverVersion,
				supportedVersion
			});
		}

		// Token: 0x06002676 RID: 9846 RVA: 0x000B3424 File Offset: 0x000B1624
		public static LocalizedString AutoReseedNotAllCopiesOnVolumeFailedSuspended(string dbNames)
		{
			return new LocalizedString("AutoReseedNotAllCopiesOnVolumeFailedSuspended", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				dbNames
			});
		}

		// Token: 0x170009C4 RID: 2500
		// (get) Token: 0x06002677 RID: 9847 RVA: 0x000B3453 File Offset: 0x000B1653
		public static LocalizedString MsexchangereplLong
		{
			get
			{
				return new LocalizedString("MsexchangereplLong", "", false, false, ReplayStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06002678 RID: 9848 RVA: 0x000B3474 File Offset: 0x000B1674
		public static LocalizedString LastLogReplacementFailedFileNotFoundException(string dbCopy, string missingFile, string e00log)
		{
			return new LocalizedString("LastLogReplacementFailedFileNotFoundException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				dbCopy,
				missingFile,
				e00log
			});
		}

		// Token: 0x06002679 RID: 9849 RVA: 0x000B34AC File Offset: 0x000B16AC
		public static LocalizedString DatabaseNotHealthyOnVolume(string databaseName, string volumeName)
		{
			return new LocalizedString("DatabaseNotHealthyOnVolume", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				databaseName,
				volumeName
			});
		}

		// Token: 0x0600267A RID: 9850 RVA: 0x000B34E0 File Offset: 0x000B16E0
		public static LocalizedString DagTaskComputerAccountCouldNotBeValidatedException(string computerAccount, string userAccount, string error)
		{
			return new LocalizedString("DagTaskComputerAccountCouldNotBeValidatedException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				computerAccount,
				userAccount,
				error
			});
		}

		// Token: 0x0600267B RID: 9851 RVA: 0x000B3518 File Offset: 0x000B1718
		public static LocalizedString AmDbActionRejectedLastAdminActionDidNotSucceedException(string actionCode)
		{
			return new LocalizedString("AmDbActionRejectedLastAdminActionDidNotSucceedException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				actionCode
			});
		}

		// Token: 0x0600267C RID: 9852 RVA: 0x000B3548 File Offset: 0x000B1748
		public static LocalizedString AcllInvalidForActiveCopyException(string dbCopy)
		{
			return new LocalizedString("AcllInvalidForActiveCopyException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				dbCopy
			});
		}

		// Token: 0x0600267D RID: 9853 RVA: 0x000B3578 File Offset: 0x000B1778
		public static LocalizedString DumpsterCouldNotReadMaxDumpsterTimeException(string dbName)
		{
			return new LocalizedString("DumpsterCouldNotReadMaxDumpsterTimeException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				dbName
			});
		}

		// Token: 0x0600267E RID: 9854 RVA: 0x000B35A8 File Offset: 0x000B17A8
		public static LocalizedString AmPreMountCallbackFailedNoReplicaInstanceErrorException(string dbName, string server, string errMsg)
		{
			return new LocalizedString("AmPreMountCallbackFailedNoReplicaInstanceErrorException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				dbName,
				server,
				errMsg
			});
		}

		// Token: 0x0600267F RID: 9855 RVA: 0x000B35E0 File Offset: 0x000B17E0
		public static LocalizedString MonitoringCouldNotFindDagException(string dagName, string adError)
		{
			return new LocalizedString("MonitoringCouldNotFindDagException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				dagName,
				adError
			});
		}

		// Token: 0x06002680 RID: 9856 RVA: 0x000B3614 File Offset: 0x000B1814
		public static LocalizedString AcllAlreadyRunningException(string dbCopy)
		{
			return new LocalizedString("AcllAlreadyRunningException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				dbCopy
			});
		}

		// Token: 0x170009C5 RID: 2501
		// (get) Token: 0x06002681 RID: 9857 RVA: 0x000B3643 File Offset: 0x000B1843
		public static LocalizedString AmOperationInvalidForStandaloneRoleException
		{
			get
			{
				return new LocalizedString("AmOperationInvalidForStandaloneRoleException", "", false, false, ReplayStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06002682 RID: 9858 RVA: 0x000B3664 File Offset: 0x000B1864
		public static LocalizedString AcllCopyIsNotViableException(string dbCopy)
		{
			return new LocalizedString("AcllCopyIsNotViableException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				dbCopy
			});
		}

		// Token: 0x06002683 RID: 9859 RVA: 0x000B3694 File Offset: 0x000B1894
		public static LocalizedString DatabaseFailoverFailedException(string dbName, string msg)
		{
			return new LocalizedString("DatabaseFailoverFailedException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				dbName,
				msg
			});
		}

		// Token: 0x06002684 RID: 9860 RVA: 0x000B36C8 File Offset: 0x000B18C8
		public static LocalizedString VolumeRecentlyModifiedException(string volumeName, TimeSpan threshold, string dateTimeUtc, string mountPoint, string lastUpdatePath)
		{
			return new LocalizedString("VolumeRecentlyModifiedException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				volumeName,
				threshold,
				dateTimeUtc,
				mountPoint,
				lastUpdatePath
			});
		}

		// Token: 0x170009C6 RID: 2502
		// (get) Token: 0x06002685 RID: 9861 RVA: 0x000B370D File Offset: 0x000B190D
		public static LocalizedString JetErrorDatabaseNotFound
		{
			get
			{
				return new LocalizedString("JetErrorDatabaseNotFound", "", false, false, ReplayStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06002686 RID: 9862 RVA: 0x000B372C File Offset: 0x000B192C
		public static LocalizedString DagTaskQuorumNotAchievedException(string dagName)
		{
			return new LocalizedString("DagTaskQuorumNotAchievedException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				dagName
			});
		}

		// Token: 0x06002687 RID: 9863 RVA: 0x000B375C File Offset: 0x000B195C
		public static LocalizedString OperationTimeoutErr(int secs)
		{
			return new LocalizedString("OperationTimeoutErr", "ExA61C08", false, true, ReplayStrings.ResourceManager, new object[]
			{
				secs
			});
		}

		// Token: 0x06002688 RID: 9864 RVA: 0x000B3790 File Offset: 0x000B1990
		public static LocalizedString DagTaskFormingClusterToLog(string clusterName, string firstServer, string ipAddresses, string ipAddressMasks)
		{
			return new LocalizedString("DagTaskFormingClusterToLog", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				clusterName,
				firstServer,
				ipAddresses,
				ipAddressMasks
			});
		}

		// Token: 0x06002689 RID: 9865 RVA: 0x000B37CC File Offset: 0x000B19CC
		public static LocalizedString ErrorNullServerFromDb(string dbName)
		{
			return new LocalizedString("ErrorNullServerFromDb", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				dbName
			});
		}

		// Token: 0x170009C7 RID: 2503
		// (get) Token: 0x0600268A RID: 9866 RVA: 0x000B37FB File Offset: 0x000B19FB
		public static LocalizedString SeederEcBackupInProgress
		{
			get
			{
				return new LocalizedString("SeederEcBackupInProgress", "ExDB80C0", false, true, ReplayStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600268B RID: 9867 RVA: 0x000B381C File Offset: 0x000B1A1C
		public static LocalizedString TPREnabledInvalidOperation(string operationName)
		{
			return new LocalizedString("TPREnabledInvalidOperation", "Ex70DB25", false, true, ReplayStrings.ResourceManager, new object[]
			{
				operationName
			});
		}

		// Token: 0x0600268C RID: 9868 RVA: 0x000B384C File Offset: 0x000B1A4C
		public static LocalizedString AcllSetCurrentLogGenerationException(string dbCopy, string e00logPath, string err)
		{
			return new LocalizedString("AcllSetCurrentLogGenerationException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				dbCopy,
				e00logPath,
				err
			});
		}

		// Token: 0x0600268D RID: 9869 RVA: 0x000B3884 File Offset: 0x000B1A84
		public static LocalizedString FailedToReadDatabasePage(int error)
		{
			return new LocalizedString("FailedToReadDatabasePage", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				error
			});
		}

		// Token: 0x0600268E RID: 9870 RVA: 0x000B38B8 File Offset: 0x000B1AB8
		public static LocalizedString DagTaskRemoteOperationLogData(string verboseData)
		{
			return new LocalizedString("DagTaskRemoteOperationLogData", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				verboseData
			});
		}

		// Token: 0x0600268F RID: 9871 RVA: 0x000B38E8 File Offset: 0x000B1AE8
		public static LocalizedString AmBcsTargetServerMaxActivesReached(string server, string maxActiveDatabases)
		{
			return new LocalizedString("AmBcsTargetServerMaxActivesReached", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				server,
				maxActiveDatabases
			});
		}

		// Token: 0x06002690 RID: 9872 RVA: 0x000B391C File Offset: 0x000B1B1C
		public static LocalizedString NetworkCorruptData(string srcNode)
		{
			return new LocalizedString("NetworkCorruptData", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				srcNode
			});
		}

		// Token: 0x06002691 RID: 9873 RVA: 0x000B394C File Offset: 0x000B1B4C
		public static LocalizedString LogInspectorFailedGeneral(string fileName, string specificError)
		{
			return new LocalizedString("LogInspectorFailedGeneral", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				fileName,
				specificError
			});
		}

		// Token: 0x170009C8 RID: 2504
		// (get) Token: 0x06002692 RID: 9874 RVA: 0x000B397F File Offset: 0x000B1B7F
		public static LocalizedString LogCopierFailedToGetSuspendLock
		{
			get
			{
				return new LocalizedString("LogCopierFailedToGetSuspendLock", "Ex2707FB", false, true, ReplayStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06002693 RID: 9875 RVA: 0x000B39A0 File Offset: 0x000B1BA0
		public static LocalizedString ReplayServiceRpcArgumentException(string argument)
		{
			return new LocalizedString("ReplayServiceRpcArgumentException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				argument
			});
		}

		// Token: 0x06002694 RID: 9876 RVA: 0x000B39D0 File Offset: 0x000B1BD0
		public static LocalizedString AcllFailedException(string error)
		{
			return new LocalizedString("AcllFailedException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				error
			});
		}

		// Token: 0x06002695 RID: 9877 RVA: 0x000B3A00 File Offset: 0x000B1C00
		public static LocalizedString NetworkRemoteErrorUnknown(string nodeName, string messageText)
		{
			return new LocalizedString("NetworkRemoteErrorUnknown", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				nodeName,
				messageText
			});
		}

		// Token: 0x06002696 RID: 9878 RVA: 0x000B3A34 File Offset: 0x000B1C34
		public static LocalizedString FileCheckCorruptFile(string file, string errorMessage)
		{
			return new LocalizedString("FileCheckCorruptFile", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				file,
				errorMessage
			});
		}

		// Token: 0x06002697 RID: 9879 RVA: 0x000B3A68 File Offset: 0x000B1C68
		public static LocalizedString DagTaskFileShareWitnessResourceIsStillNotOnlineException(string fswResource, string currentState)
		{
			return new LocalizedString("DagTaskFileShareWitnessResourceIsStillNotOnlineException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				fswResource,
				currentState
			});
		}

		// Token: 0x06002698 RID: 9880 RVA: 0x000B3A9C File Offset: 0x000B1C9C
		public static LocalizedString DeleteChkptReasonTooAdvanced(long checkpointGeneration)
		{
			return new LocalizedString("DeleteChkptReasonTooAdvanced", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				checkpointGeneration
			});
		}

		// Token: 0x170009C9 RID: 2505
		// (get) Token: 0x06002699 RID: 9881 RVA: 0x000B3AD0 File Offset: 0x000B1CD0
		public static LocalizedString NetworkDataOverflowGeneric
		{
			get
			{
				return new LocalizedString("NetworkDataOverflowGeneric", "Ex473BF9", false, true, ReplayStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600269A RID: 9882 RVA: 0x000B3AF0 File Offset: 0x000B1CF0
		public static LocalizedString AmDbOperationAttempedTooSoonException(string dbName)
		{
			return new LocalizedString("AmDbOperationAttempedTooSoonException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				dbName
			});
		}

		// Token: 0x0600269B RID: 9883 RVA: 0x000B3B20 File Offset: 0x000B1D20
		public static LocalizedString AmTransientException(string errMessage)
		{
			return new LocalizedString("AmTransientException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				errMessage
			});
		}

		// Token: 0x0600269C RID: 9884 RVA: 0x000B3B50 File Offset: 0x000B1D50
		public static LocalizedString AmGetServiceProcessFailed(string serviceName, int state, int pid)
		{
			return new LocalizedString("AmGetServiceProcessFailed", "Ex466359", false, true, ReplayStrings.ResourceManager, new object[]
			{
				serviceName,
				state,
				pid
			});
		}

		// Token: 0x0600269D RID: 9885 RVA: 0x000B3B94 File Offset: 0x000B1D94
		public static LocalizedString SourceDatabaseNotFound(Guid g, string sourceServer)
		{
			return new LocalizedString("SourceDatabaseNotFound", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				g,
				sourceServer
			});
		}

		// Token: 0x0600269E RID: 9886 RVA: 0x000B3BCC File Offset: 0x000B1DCC
		public static LocalizedString DatabaseRemountFailedException(string dbName, string msg)
		{
			return new LocalizedString("DatabaseRemountFailedException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				dbName,
				msg
			});
		}

		// Token: 0x0600269F RID: 9887 RVA: 0x000B3C00 File Offset: 0x000B1E00
		public static LocalizedString LogRepairUnexpectedVerifyError(string logName, string exceptionText)
		{
			return new LocalizedString("LogRepairUnexpectedVerifyError", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				logName,
				exceptionText
			});
		}

		// Token: 0x170009CA RID: 2506
		// (get) Token: 0x060026A0 RID: 9888 RVA: 0x000B3C33 File Offset: 0x000B1E33
		public static LocalizedString TPRNotEnabled
		{
			get
			{
				return new LocalizedString("TPRNotEnabled", "Ex668CBF", false, true, ReplayStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170009CB RID: 2507
		// (get) Token: 0x060026A1 RID: 9889 RVA: 0x000B3C51 File Offset: 0x000B1E51
		public static LocalizedString LockOwnerSuspend
		{
			get
			{
				return new LocalizedString("LockOwnerSuspend", "Ex4BDE99", false, true, ReplayStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170009CC RID: 2508
		// (get) Token: 0x060026A2 RID: 9890 RVA: 0x000B3C6F File Offset: 0x000B1E6F
		public static LocalizedString DbValidationFullCopyStatusResultsLabel
		{
			get
			{
				return new LocalizedString("DbValidationFullCopyStatusResultsLabel", "", false, false, ReplayStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170009CD RID: 2509
		// (get) Token: 0x060026A3 RID: 9891 RVA: 0x000B3C8D File Offset: 0x000B1E8D
		public static LocalizedString LogRepairNotPossibleInsuffientToCheckDivergence
		{
			get
			{
				return new LocalizedString("LogRepairNotPossibleInsuffientToCheckDivergence", "", false, false, ReplayStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060026A4 RID: 9892 RVA: 0x000B3CAC File Offset: 0x000B1EAC
		public static LocalizedString EseLogEnumeratorIOError(string apiName, string ioErrorMessage, int win32ErrCode, string directoryName)
		{
			return new LocalizedString("EseLogEnumeratorIOError", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				apiName,
				ioErrorMessage,
				win32ErrCode,
				directoryName
			});
		}

		// Token: 0x060026A5 RID: 9893 RVA: 0x000B3CEC File Offset: 0x000B1EEC
		public static LocalizedString UnExpectedPageSize(string db, long pageSize)
		{
			return new LocalizedString("UnExpectedPageSize", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				db,
				pageSize
			});
		}

		// Token: 0x060026A6 RID: 9894 RVA: 0x000B3D24 File Offset: 0x000B1F24
		public static LocalizedString ErrorCouldNotConnectNativeClusterForAmConfig(int ec)
		{
			return new LocalizedString("ErrorCouldNotConnectNativeClusterForAmConfig", "Ex05C2B1", false, true, ReplayStrings.ResourceManager, new object[]
			{
				ec
			});
		}

		// Token: 0x060026A7 RID: 9895 RVA: 0x000B3D58 File Offset: 0x000B1F58
		public static LocalizedString DbHTFirstLookupTimeoutException(int timeoutMs)
		{
			return new LocalizedString("DbHTFirstLookupTimeoutException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				timeoutMs
			});
		}

		// Token: 0x060026A8 RID: 9896 RVA: 0x000B3D8C File Offset: 0x000B1F8C
		public static LocalizedString AmBcsTargetNodeDownError(string server)
		{
			return new LocalizedString("AmBcsTargetNodeDownError", "ExF8DBF3", false, true, ReplayStrings.ResourceManager, new object[]
			{
				server
			});
		}

		// Token: 0x170009CE RID: 2510
		// (get) Token: 0x060026A9 RID: 9897 RVA: 0x000B3DBB File Offset: 0x000B1FBB
		public static LocalizedString DagTaskComponentManagerWantsToRebootException
		{
			get
			{
				return new LocalizedString("DagTaskComponentManagerWantsToRebootException", "", false, false, ReplayStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060026AA RID: 9898 RVA: 0x000B3DDC File Offset: 0x000B1FDC
		public static LocalizedString RegistryParameterWriteException(string valueName, string errMsg)
		{
			return new LocalizedString("RegistryParameterWriteException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				valueName,
				errMsg
			});
		}

		// Token: 0x060026AB RID: 9899 RVA: 0x000B3E10 File Offset: 0x000B2010
		public static LocalizedString DbAvailabilityValidationErrorsOccurred(string dbName, int healthyCopiesCount, int expectedHealthyCount, string detailedMsg)
		{
			return new LocalizedString("DbAvailabilityValidationErrorsOccurred", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				dbName,
				healthyCopiesCount,
				expectedHealthyCount,
				detailedMsg
			});
		}

		// Token: 0x060026AC RID: 9900 RVA: 0x000B3E58 File Offset: 0x000B2058
		public static LocalizedString MonitoringCouldNotFindMiniServerException(string serverName)
		{
			return new LocalizedString("MonitoringCouldNotFindMiniServerException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				serverName
			});
		}

		// Token: 0x170009CF RID: 2511
		// (get) Token: 0x060026AD RID: 9901 RVA: 0x000B3E87 File Offset: 0x000B2087
		public static LocalizedString NetworkCancelled
		{
			get
			{
				return new LocalizedString("NetworkCancelled", "ExC24353", false, true, ReplayStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060026AE RID: 9902 RVA: 0x000B3EA8 File Offset: 0x000B20A8
		public static LocalizedString ReplayDbOperationWrapperException(string operationError)
		{
			return new LocalizedString("ReplayDbOperationWrapperException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				operationError
			});
		}

		// Token: 0x170009D0 RID: 2512
		// (get) Token: 0x060026AF RID: 9903 RVA: 0x000B3ED7 File Offset: 0x000B20D7
		public static LocalizedString ErrorFailedToGetClusterCoreGroup
		{
			get
			{
				return new LocalizedString("ErrorFailedToGetClusterCoreGroup", "ExE99407", false, true, ReplayStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170009D1 RID: 2513
		// (get) Token: 0x060026B0 RID: 9904 RVA: 0x000B3EF5 File Offset: 0x000B20F5
		public static LocalizedString NotInPendingState
		{
			get
			{
				return new LocalizedString("NotInPendingState", "Ex69E4EF", false, true, ReplayStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060026B1 RID: 9905 RVA: 0x000B3F14 File Offset: 0x000B2114
		public static LocalizedString FailedToFindLocalServerException(string serverName)
		{
			return new LocalizedString("FailedToFindLocalServerException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				serverName
			});
		}

		// Token: 0x060026B2 RID: 9906 RVA: 0x000B3F44 File Offset: 0x000B2144
		public static LocalizedString CallWithoutnumberOfExtraCopiesOnSparesException(string errMsg)
		{
			return new LocalizedString("CallWithoutnumberOfExtraCopiesOnSparesException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				errMsg
			});
		}

		// Token: 0x060026B3 RID: 9907 RVA: 0x000B3F74 File Offset: 0x000B2174
		public static LocalizedString AmBcsTargetServerADError(string server, string adError)
		{
			return new LocalizedString("AmBcsTargetServerADError", "Ex5DD649", false, true, ReplayStrings.ResourceManager, new object[]
			{
				server,
				adError
			});
		}

		// Token: 0x060026B4 RID: 9908 RVA: 0x000B3FA8 File Offset: 0x000B21A8
		public static LocalizedString ReplaySystemOperationCancelledException(string operationName)
		{
			return new LocalizedString("ReplaySystemOperationCancelledException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				operationName
			});
		}

		// Token: 0x060026B5 RID: 9909 RVA: 0x000B3FD8 File Offset: 0x000B21D8
		public static LocalizedString ServiceNotRunningOnNodeException(string serviceName, string nodeName)
		{
			return new LocalizedString("ServiceNotRunningOnNodeException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				serviceName,
				nodeName
			});
		}

		// Token: 0x170009D2 RID: 2514
		// (get) Token: 0x060026B6 RID: 9910 RVA: 0x000B400B File Offset: 0x000B220B
		public static LocalizedString ErrorInvalidPamServerName
		{
			get
			{
				return new LocalizedString("ErrorInvalidPamServerName", "Ex5C007A", false, true, ReplayStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170009D3 RID: 2515
		// (get) Token: 0x060026B7 RID: 9911 RVA: 0x000B4029 File Offset: 0x000B2229
		public static LocalizedString SeederEchrErrorFromCallbackCall
		{
			get
			{
				return new LocalizedString("SeederEchrErrorFromCallbackCall", "Ex685E98", false, true, ReplayStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060026B8 RID: 9912 RVA: 0x000B4048 File Offset: 0x000B2248
		public static LocalizedString LogFileCheckError(string logFileName, string errMsg)
		{
			return new LocalizedString("LogFileCheckError", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				logFileName,
				errMsg
			});
		}

		// Token: 0x170009D4 RID: 2516
		// (get) Token: 0x060026B9 RID: 9913 RVA: 0x000B407B File Offset: 0x000B227B
		public static LocalizedString EnableReplayLagOperationName
		{
			get
			{
				return new LocalizedString("EnableReplayLagOperationName", "", false, false, ReplayStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170009D5 RID: 2517
		// (get) Token: 0x060026BA RID: 9914 RVA: 0x000B4099 File Offset: 0x000B2299
		public static LocalizedString SeederInstanceAlreadyFailedException
		{
			get
			{
				return new LocalizedString("SeederInstanceAlreadyFailedException", "", false, false, ReplayStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060026BB RID: 9915 RVA: 0x000B40B8 File Offset: 0x000B22B8
		public static LocalizedString ServerIsNotJoinedYet(string server)
		{
			return new LocalizedString("ServerIsNotJoinedYet", "Ex18095F", false, true, ReplayStrings.ResourceManager, new object[]
			{
				server
			});
		}

		// Token: 0x060026BC RID: 9916 RVA: 0x000B40E8 File Offset: 0x000B22E8
		public static LocalizedString LastLogGenerationTimeStampStale(string timeStamp)
		{
			return new LocalizedString("LastLogGenerationTimeStampStale", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				timeStamp
			});
		}

		// Token: 0x060026BD RID: 9917 RVA: 0x000B4118 File Offset: 0x000B2318
		public static LocalizedString DbValidationDbNotReplicated(string dbName)
		{
			return new LocalizedString("DbValidationDbNotReplicated", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				dbName
			});
		}

		// Token: 0x060026BE RID: 9918 RVA: 0x000B4148 File Offset: 0x000B2348
		public static LocalizedString ReplayDbOperationWrapperTransientException(string operationError)
		{
			return new LocalizedString("ReplayDbOperationWrapperTransientException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				operationError
			});
		}

		// Token: 0x060026BF RID: 9919 RVA: 0x000B4178 File Offset: 0x000B2378
		public static LocalizedString ReplayStoreOperationAbortedException(string operationName)
		{
			return new LocalizedString("ReplayStoreOperationAbortedException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				operationName
			});
		}

		// Token: 0x170009D6 RID: 2518
		// (get) Token: 0x060026C0 RID: 9920 RVA: 0x000B41A7 File Offset: 0x000B23A7
		public static LocalizedString ReplayServiceSuspendWantedSetException
		{
			get
			{
				return new LocalizedString("ReplayServiceSuspendWantedSetException", "", false, false, ReplayStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060026C1 RID: 9921 RVA: 0x000B41C8 File Offset: 0x000B23C8
		public static LocalizedString DBCNotSuspendedYet(string db)
		{
			return new LocalizedString("DBCNotSuspendedYet", "ExE73AC3", false, true, ReplayStrings.ResourceManager, new object[]
			{
				db
			});
		}

		// Token: 0x060026C2 RID: 9922 RVA: 0x000B41F8 File Offset: 0x000B23F8
		public static LocalizedString AutoReseedThrottledException(string databaseName, string serverName, string throttlingInterval)
		{
			return new LocalizedString("AutoReseedThrottledException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				databaseName,
				serverName,
				throttlingInterval
			});
		}

		// Token: 0x060026C3 RID: 9923 RVA: 0x000B4230 File Offset: 0x000B2430
		public static LocalizedString DbValidationActiveCopyStatusRpcFailed(string dbName, string serverName, string error)
		{
			return new LocalizedString("DbValidationActiveCopyStatusRpcFailed", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				dbName,
				serverName,
				error
			});
		}

		// Token: 0x170009D7 RID: 2519
		// (get) Token: 0x060026C4 RID: 9924 RVA: 0x000B4267 File Offset: 0x000B2467
		public static LocalizedString SeederEcOOMem
		{
			get
			{
				return new LocalizedString("SeederEcOOMem", "ExAF161F", false, true, ReplayStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060026C5 RID: 9925 RVA: 0x000B4288 File Offset: 0x000B2488
		public static LocalizedString InvalidVolumeMissingException(string volumeName)
		{
			return new LocalizedString("InvalidVolumeMissingException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				volumeName
			});
		}

		// Token: 0x060026C6 RID: 9926 RVA: 0x000B42B8 File Offset: 0x000B24B8
		public static LocalizedString DagTaskJoinedNodeToCluster(string serverName)
		{
			return new LocalizedString("DagTaskJoinedNodeToCluster", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				serverName
			});
		}

		// Token: 0x060026C7 RID: 9927 RVA: 0x000B42E8 File Offset: 0x000B24E8
		public static LocalizedString CouldNotGetMountStatusError(string errorMessage)
		{
			return new LocalizedString("CouldNotGetMountStatusError", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				errorMessage
			});
		}

		// Token: 0x060026C8 RID: 9928 RVA: 0x000B4318 File Offset: 0x000B2518
		public static LocalizedString LastLogReplacementException(string msg)
		{
			return new LocalizedString("LastLogReplacementException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				msg
			});
		}

		// Token: 0x060026C9 RID: 9929 RVA: 0x000B4348 File Offset: 0x000B2548
		public static LocalizedString DatabasesMissingInCopyStatusLookUpTable(string databaseNames)
		{
			return new LocalizedString("DatabasesMissingInCopyStatusLookUpTable", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				databaseNames
			});
		}

		// Token: 0x170009D8 RID: 2520
		// (get) Token: 0x060026CA RID: 9930 RVA: 0x000B4377 File Offset: 0x000B2577
		public static LocalizedString DbValidationCopyStatusNameLabel
		{
			get
			{
				return new LocalizedString("DbValidationCopyStatusNameLabel", "", false, false, ReplayStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060026CB RID: 9931 RVA: 0x000B4398 File Offset: 0x000B2598
		public static LocalizedString IncSeedConfigNotSupportedError(string field)
		{
			return new LocalizedString("IncSeedConfigNotSupportedError", "Ex719D9C", false, true, ReplayStrings.ResourceManager, new object[]
			{
				field
			});
		}

		// Token: 0x060026CC RID: 9932 RVA: 0x000B43C8 File Offset: 0x000B25C8
		public static LocalizedString SeederFailedToDeleteLogs(string directory, string error)
		{
			return new LocalizedString("SeederFailedToDeleteLogs", "Ex2C38CB", false, true, ReplayStrings.ResourceManager, new object[]
			{
				directory,
				error
			});
		}

		// Token: 0x060026CD RID: 9933 RVA: 0x000B43FC File Offset: 0x000B25FC
		public static LocalizedString ReplayServiceResumeRpcInvalidForActiveCopyException(string dbCopy)
		{
			return new LocalizedString("ReplayServiceResumeRpcInvalidForActiveCopyException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				dbCopy
			});
		}

		// Token: 0x060026CE RID: 9934 RVA: 0x000B442C File Offset: 0x000B262C
		public static LocalizedString TPRmmediateDismountFailed(Guid dbId, string reason)
		{
			return new LocalizedString("TPRmmediateDismountFailed", "Ex1BA4FE", false, true, ReplayStrings.ResourceManager, new object[]
			{
				dbId,
				reason
			});
		}

		// Token: 0x060026CF RID: 9935 RVA: 0x000B4464 File Offset: 0x000B2664
		public static LocalizedString RepairStateError(string error)
		{
			return new LocalizedString("RepairStateError", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				error
			});
		}

		// Token: 0x060026D0 RID: 9936 RVA: 0x000B4494 File Offset: 0x000B2694
		public static LocalizedString CannotBeginSeedingInstanceNotInStateException(string dbName, string state)
		{
			return new LocalizedString("CannotBeginSeedingInstanceNotInStateException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				dbName,
				state
			});
		}

		// Token: 0x060026D1 RID: 9937 RVA: 0x000B44C8 File Offset: 0x000B26C8
		public static LocalizedString RepairStateDatabaseNotReplicated(string dbName)
		{
			return new LocalizedString("RepairStateDatabaseNotReplicated", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				dbName
			});
		}

		// Token: 0x060026D2 RID: 9938 RVA: 0x000B44F8 File Offset: 0x000B26F8
		public static LocalizedString MonitoringADFirstLookupTimeoutException(int timeoutMs)
		{
			return new LocalizedString("MonitoringADFirstLookupTimeoutException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				timeoutMs
			});
		}

		// Token: 0x060026D3 RID: 9939 RVA: 0x000B452C File Offset: 0x000B272C
		public static LocalizedString AmBcsManagedAvailabilityCheckFailed(string srcServer, string tgtServer, string componentName, string failures)
		{
			return new LocalizedString("AmBcsManagedAvailabilityCheckFailed", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				srcServer,
				tgtServer,
				componentName,
				failures
			});
		}

		// Token: 0x170009D9 RID: 2521
		// (get) Token: 0x060026D4 RID: 9940 RVA: 0x000B4567 File Offset: 0x000B2767
		public static LocalizedString SeederEchrRestoreAtFileLevel
		{
			get
			{
				return new LocalizedString("SeederEchrRestoreAtFileLevel", "Ex99EC0F", false, true, ReplayStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170009DA RID: 2522
		// (get) Token: 0x060026D5 RID: 9941 RVA: 0x000B4585 File Offset: 0x000B2785
		public static LocalizedString TPRNotYetStarted
		{
			get
			{
				return new LocalizedString("TPRNotYetStarted", "Ex0F2837", false, true, ReplayStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170009DB RID: 2523
		// (get) Token: 0x060026D6 RID: 9942 RVA: 0x000B45A3 File Offset: 0x000B27A3
		public static LocalizedString SeederEcInvalidInput
		{
			get
			{
				return new LocalizedString("SeederEcInvalidInput", "Ex69E7D7", false, true, ReplayStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060026D7 RID: 9943 RVA: 0x000B45C4 File Offset: 0x000B27C4
		public static LocalizedString ReplayServiceSyncStateInvalidDuringMoveException(string dbCopy)
		{
			return new LocalizedString("ReplayServiceSyncStateInvalidDuringMoveException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				dbCopy
			});
		}

		// Token: 0x060026D8 RID: 9944 RVA: 0x000B45F4 File Offset: 0x000B27F4
		public static LocalizedString DatabasePageSizeUnexpected(long size, long expected)
		{
			return new LocalizedString("DatabasePageSizeUnexpected", "Ex6E8BA7", false, true, ReplayStrings.ResourceManager, new object[]
			{
				size,
				expected
			});
		}

		// Token: 0x060026D9 RID: 9945 RVA: 0x000B4634 File Offset: 0x000B2834
		public static LocalizedString AmBcsDatabaseCopyQueueLengthTooHigh(string db, string server, long length, long maxLength)
		{
			return new LocalizedString("AmBcsDatabaseCopyQueueLengthTooHigh", "Ex18998C", false, true, ReplayStrings.ResourceManager, new object[]
			{
				db,
				server,
				length,
				maxLength
			});
		}

		// Token: 0x060026DA RID: 9946 RVA: 0x000B467C File Offset: 0x000B287C
		public static LocalizedString DagTaskFswNeedsCnoPermissionException(string fswPath, string accountName)
		{
			return new LocalizedString("DagTaskFswNeedsCnoPermissionException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				fswPath,
				accountName
			});
		}

		// Token: 0x060026DB RID: 9947 RVA: 0x000B46B0 File Offset: 0x000B28B0
		public static LocalizedString ReplayServiceResumeRpcPartialSuccessCatalogFailedException(string errMsg)
		{
			return new LocalizedString("ReplayServiceResumeRpcPartialSuccessCatalogFailedException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				errMsg
			});
		}

		// Token: 0x060026DC RID: 9948 RVA: 0x000B46E0 File Offset: 0x000B28E0
		public static LocalizedString ReplayServiceCouldNotFindReplayConfigException(string database, string server)
		{
			return new LocalizedString("ReplayServiceCouldNotFindReplayConfigException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				database,
				server
			});
		}

		// Token: 0x060026DD RID: 9949 RVA: 0x000B4714 File Offset: 0x000B2914
		public static LocalizedString SeederCopyNotSuspended(string db)
		{
			return new LocalizedString("SeederCopyNotSuspended", "Ex01168E", false, true, ReplayStrings.ResourceManager, new object[]
			{
				db
			});
		}

		// Token: 0x060026DE RID: 9950 RVA: 0x000B4744 File Offset: 0x000B2944
		public static LocalizedString DagTaskFormingClusterProgress(string clusterName, string firstServer)
		{
			return new LocalizedString("DagTaskFormingClusterProgress", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				clusterName,
				firstServer
			});
		}

		// Token: 0x060026DF RID: 9951 RVA: 0x000B4778 File Offset: 0x000B2978
		public static LocalizedString NetworkConnectionTimeout(int waitInsecs)
		{
			return new LocalizedString("NetworkConnectionTimeout", "Ex84B58B", false, true, ReplayStrings.ResourceManager, new object[]
			{
				waitInsecs
			});
		}

		// Token: 0x060026E0 RID: 9952 RVA: 0x000B47AC File Offset: 0x000B29AC
		public static LocalizedString DbFixupFailedException(string dbName, string volumeName, string reason)
		{
			return new LocalizedString("DbFixupFailedException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				dbName,
				volumeName,
				reason
			});
		}

		// Token: 0x060026E1 RID: 9953 RVA: 0x000B47E4 File Offset: 0x000B29E4
		public static LocalizedString AmPreMountCallbackFailedException(string dbName, string error)
		{
			return new LocalizedString("AmPreMountCallbackFailedException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				dbName,
				error
			});
		}

		// Token: 0x060026E2 RID: 9954 RVA: 0x000B4818 File Offset: 0x000B2A18
		public static LocalizedString AmBcsSingleCopyValidationException(string bcsMessage)
		{
			return new LocalizedString("AmBcsSingleCopyValidationException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				bcsMessage
			});
		}

		// Token: 0x060026E3 RID: 9955 RVA: 0x000B4848 File Offset: 0x000B2A48
		public static LocalizedString AutoReseedCatalogSourceException(string databaseName, string serverName)
		{
			return new LocalizedString("AutoReseedCatalogSourceException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				databaseName,
				serverName
			});
		}

		// Token: 0x170009DC RID: 2524
		// (get) Token: 0x060026E4 RID: 9956 RVA: 0x000B487B File Offset: 0x000B2A7B
		public static LocalizedString ErrorRemoteSiteNotConnected
		{
			get
			{
				return new LocalizedString("ErrorRemoteSiteNotConnected", "", false, false, ReplayStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060026E5 RID: 9957 RVA: 0x000B489C File Offset: 0x000B2A9C
		public static LocalizedString AmMountBlockedDbMountedBeforeWithMissingEdbException(string dbName, string edbFilePath)
		{
			return new LocalizedString("AmMountBlockedDbMountedBeforeWithMissingEdbException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				dbName,
				edbFilePath
			});
		}

		// Token: 0x060026E6 RID: 9958 RVA: 0x000B48D0 File Offset: 0x000B2AD0
		public static LocalizedString AutoReseedInvalidEdbFolderPath(string actualPath, string expectedPath)
		{
			return new LocalizedString("AutoReseedInvalidEdbFolderPath", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				actualPath,
				expectedPath
			});
		}

		// Token: 0x060026E7 RID: 9959 RVA: 0x000B4904 File Offset: 0x000B2B04
		public static LocalizedString MissingLogRequired(string file)
		{
			return new LocalizedString("MissingLogRequired", "Ex5DBF20", false, true, ReplayStrings.ResourceManager, new object[]
			{
				file
			});
		}

		// Token: 0x060026E8 RID: 9960 RVA: 0x000B4934 File Offset: 0x000B2B34
		public static LocalizedString SeederFailedToDeployDatabase(string src, string dest, string error)
		{
			return new LocalizedString("SeederFailedToDeployDatabase", "Ex07D113", false, true, ReplayStrings.ResourceManager, new object[]
			{
				src,
				dest,
				error
			});
		}

		// Token: 0x060026E9 RID: 9961 RVA: 0x000B496C File Offset: 0x000B2B6C
		public static LocalizedString PreferFullReseed(int wayPoint)
		{
			return new LocalizedString("PreferFullReseed", "Ex12ED74", false, true, ReplayStrings.ResourceManager, new object[]
			{
				wayPoint
			});
		}

		// Token: 0x060026EA RID: 9962 RVA: 0x000B49A0 File Offset: 0x000B2BA0
		public static LocalizedString DagReplayServiceDownException(string serverName, string rpcErrorMessage)
		{
			return new LocalizedString("DagReplayServiceDownException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				serverName,
				rpcErrorMessage
			});
		}

		// Token: 0x060026EB RID: 9963 RVA: 0x000B49D4 File Offset: 0x000B2BD4
		public static LocalizedString AutoReseedFailedResumeRetryExceeded(int maxRetryCount)
		{
			return new LocalizedString("AutoReseedFailedResumeRetryExceeded", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				maxRetryCount
			});
		}

		// Token: 0x060026EC RID: 9964 RVA: 0x000B4A08 File Offset: 0x000B2C08
		public static LocalizedString AcllTempLogCreationFailedException(string dbCopy, string err)
		{
			return new LocalizedString("AcllTempLogCreationFailedException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				dbCopy,
				err
			});
		}

		// Token: 0x060026ED RID: 9965 RVA: 0x000B4A3C File Offset: 0x000B2C3C
		public static LocalizedString NetworkCommunicationError(string remoteNodeName, string errorText)
		{
			return new LocalizedString("NetworkCommunicationError", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				remoteNodeName,
				errorText
			});
		}

		// Token: 0x060026EE RID: 9966 RVA: 0x000B4A70 File Offset: 0x000B2C70
		public static LocalizedString SetBrokenWatsonException(string errMsg)
		{
			return new LocalizedString("SetBrokenWatsonException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				errMsg
			});
		}

		// Token: 0x060026EF RID: 9967 RVA: 0x000B4AA0 File Offset: 0x000B2CA0
		public static LocalizedString LogRepairRetryCountExceeded(long retryCount)
		{
			return new LocalizedString("LogRepairRetryCountExceeded", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				retryCount
			});
		}

		// Token: 0x060026F0 RID: 9968 RVA: 0x000B4AD4 File Offset: 0x000B2CD4
		public static LocalizedString FileCheckLogfileGeneration(string logfile, long logfileGeneration, long expectedGeneration)
		{
			return new LocalizedString("FileCheckLogfileGeneration", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				logfile,
				logfileGeneration,
				expectedGeneration
			});
		}

		// Token: 0x170009DD RID: 2525
		// (get) Token: 0x060026F1 RID: 9969 RVA: 0x000B4B15 File Offset: 0x000B2D15
		public static LocalizedString ErrorLocalNodeNotUpYet
		{
			get
			{
				return new LocalizedString("ErrorLocalNodeNotUpYet", "Ex4FCD4B", false, true, ReplayStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060026F2 RID: 9970 RVA: 0x000B4B34 File Offset: 0x000B2D34
		public static LocalizedString FailedToGetProcessForServiceException(string serviceName, string msg)
		{
			return new LocalizedString("FailedToGetProcessForServiceException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				serviceName,
				msg
			});
		}

		// Token: 0x060026F3 RID: 9971 RVA: 0x000B4B68 File Offset: 0x000B2D68
		public static LocalizedString DatabasesMissingInADException(string databaseName, string volumeName)
		{
			return new LocalizedString("DatabasesMissingInADException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				databaseName,
				volumeName
			});
		}

		// Token: 0x060026F4 RID: 9972 RVA: 0x000B4B9C File Offset: 0x000B2D9C
		public static LocalizedString VolumeNotSafeForFormatException(string volumeName, string mountPoint)
		{
			return new LocalizedString("VolumeNotSafeForFormatException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				volumeName,
				mountPoint
			});
		}

		// Token: 0x060026F5 RID: 9973 RVA: 0x000B4BD0 File Offset: 0x000B2DD0
		public static LocalizedString AmFailedToReadClusdbException(string error)
		{
			return new LocalizedString("AmFailedToReadClusdbException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				error
			});
		}

		// Token: 0x060026F6 RID: 9974 RVA: 0x000B4C00 File Offset: 0x000B2E00
		public static LocalizedString AcllLossDeterminationFailedException(string error)
		{
			return new LocalizedString("AcllLossDeterminationFailedException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				error
			});
		}

		// Token: 0x170009DE RID: 2526
		// (get) Token: 0x060026F7 RID: 9975 RVA: 0x000B4C2F File Offset: 0x000B2E2F
		public static LocalizedString DagTaskClusteringRequiresEnterpriseSkuException
		{
			get
			{
				return new LocalizedString("DagTaskClusteringRequiresEnterpriseSkuException", "", false, false, ReplayStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060026F8 RID: 9976 RVA: 0x000B4C50 File Offset: 0x000B2E50
		public static LocalizedString DatabaseHealthTrackerException(string errorMsg)
		{
			return new LocalizedString("DatabaseHealthTrackerException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				errorMsg
			});
		}

		// Token: 0x060026F9 RID: 9977 RVA: 0x000B4C80 File Offset: 0x000B2E80
		public static LocalizedString DagTaskJoiningNodeToCluster(string serverName)
		{
			return new LocalizedString("DagTaskJoiningNodeToCluster", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				serverName
			});
		}

		// Token: 0x060026FA RID: 9978 RVA: 0x000B4CB0 File Offset: 0x000B2EB0
		public static LocalizedString LastLogReplacementFailedErrorException(string dbCopy, string e00log, string error)
		{
			return new LocalizedString("LastLogReplacementFailedErrorException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				dbCopy,
				e00log,
				error
			});
		}

		// Token: 0x170009DF RID: 2527
		// (get) Token: 0x060026FB RID: 9979 RVA: 0x000B4CE7 File Offset: 0x000B2EE7
		public static LocalizedString MsexchangesearchLong
		{
			get
			{
				return new LocalizedString("MsexchangesearchLong", "", false, false, ReplayStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170009E0 RID: 2528
		// (get) Token: 0x060026FC RID: 9980 RVA: 0x000B4D05 File Offset: 0x000B2F05
		public static LocalizedString LockOwnerBackup
		{
			get
			{
				return new LocalizedString("LockOwnerBackup", "ExC9C62B", false, true, ReplayStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060026FD RID: 9981 RVA: 0x000B4D24 File Offset: 0x000B2F24
		public static LocalizedString DagTaskRemoteOperationLogBegin(string serverName)
		{
			return new LocalizedString("DagTaskRemoteOperationLogBegin", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				serverName
			});
		}

		// Token: 0x060026FE RID: 9982 RVA: 0x000B4D54 File Offset: 0x000B2F54
		public static LocalizedString AmBcsDatabaseCopyReplayQueueLengthTooHigh(string db, string server, long length, long maxLength)
		{
			return new LocalizedString("AmBcsDatabaseCopyReplayQueueLengthTooHigh", "ExDAC1F8", false, true, ReplayStrings.ResourceManager, new object[]
			{
				db,
				server,
				length,
				maxLength
			});
		}

		// Token: 0x060026FF RID: 9983 RVA: 0x000B4D9C File Offset: 0x000B2F9C
		public static LocalizedString CorruptLogDetectedError(string filename, string errorText)
		{
			return new LocalizedString("CorruptLogDetectedError", "ExCE54EB", false, true, ReplayStrings.ResourceManager, new object[]
			{
				filename,
				errorText
			});
		}

		// Token: 0x06002700 RID: 9984 RVA: 0x000B4DD0 File Offset: 0x000B2FD0
		public static LocalizedString SafeDeleteExistingFilesDataRedundancyException(string db, string errMsg2)
		{
			return new LocalizedString("SafeDeleteExistingFilesDataRedundancyException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				db,
				errMsg2
			});
		}

		// Token: 0x170009E1 RID: 2529
		// (get) Token: 0x06002701 RID: 9985 RVA: 0x000B4E03 File Offset: 0x000B3003
		public static LocalizedString AmDbActionMoveFailedException
		{
			get
			{
				return new LocalizedString("AmDbActionMoveFailedException", "", false, false, ReplayStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170009E2 RID: 2530
		// (get) Token: 0x06002702 RID: 9986 RVA: 0x000B4E21 File Offset: 0x000B3021
		public static LocalizedString Failed
		{
			get
			{
				return new LocalizedString("Failed", "", false, false, ReplayStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170009E3 RID: 2531
		// (get) Token: 0x06002703 RID: 9987 RVA: 0x000B4E3F File Offset: 0x000B303F
		public static LocalizedString GranularReplicationOverflow
		{
			get
			{
				return new LocalizedString("GranularReplicationOverflow", "Ex267116", false, true, ReplayStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06002704 RID: 9988 RVA: 0x000B4E60 File Offset: 0x000B3060
		public static LocalizedString DumpsterInvalidResubmitRequestException(string dbName)
		{
			return new LocalizedString("DumpsterInvalidResubmitRequestException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				dbName
			});
		}

		// Token: 0x06002705 RID: 9989 RVA: 0x000B4E90 File Offset: 0x000B3090
		public static LocalizedString FileCheckIsamError(string errorMessage)
		{
			return new LocalizedString("FileCheckIsamError", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				errorMessage
			});
		}

		// Token: 0x170009E4 RID: 2532
		// (get) Token: 0x06002706 RID: 9990 RVA: 0x000B4EBF File Offset: 0x000B30BF
		public static LocalizedString CantStartFromCommandLine
		{
			get
			{
				return new LocalizedString("CantStartFromCommandLine", "ExD57B51", false, true, ReplayStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170009E5 RID: 2533
		// (get) Token: 0x06002707 RID: 9991 RVA: 0x000B4EDD File Offset: 0x000B30DD
		public static LocalizedString AmDbActionMountFailedException
		{
			get
			{
				return new LocalizedString("AmDbActionMountFailedException", "", false, false, ReplayStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06002708 RID: 9992 RVA: 0x000B4EFC File Offset: 0x000B30FC
		public static LocalizedString AmClusterNoServerToConnect(string dagName)
		{
			return new LocalizedString("AmClusterNoServerToConnect", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				dagName
			});
		}

		// Token: 0x06002709 RID: 9993 RVA: 0x000B4F2C File Offset: 0x000B312C
		public static LocalizedString DatabaseValidationNoCopiesException(string databaseName)
		{
			return new LocalizedString("DatabaseValidationNoCopiesException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				databaseName
			});
		}

		// Token: 0x0600270A RID: 9994 RVA: 0x000B4F5C File Offset: 0x000B315C
		public static LocalizedString ClusterBatchWriter_OpenCopyStateKeyFailed(int ec)
		{
			return new LocalizedString("ClusterBatchWriter_OpenCopyStateKeyFailed", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				ec
			});
		}

		// Token: 0x0600270B RID: 9995 RVA: 0x000B4F90 File Offset: 0x000B3190
		public static LocalizedString FileCheckIOError(string errorMessage)
		{
			return new LocalizedString("FileCheckIOError", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				errorMessage
			});
		}

		// Token: 0x0600270C RID: 9996 RVA: 0x000B4FC0 File Offset: 0x000B31C0
		public static LocalizedString CouldNotFindSpareVolumeException(string databases)
		{
			return new LocalizedString("CouldNotFindSpareVolumeException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				databases
			});
		}

		// Token: 0x0600270D RID: 9997 RVA: 0x000B4FF0 File Offset: 0x000B31F0
		public static LocalizedString FileReadException(string fileName, int expectedBytes, int actualBytes)
		{
			return new LocalizedString("FileReadException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				fileName,
				expectedBytes,
				actualBytes
			});
		}

		// Token: 0x0600270E RID: 9998 RVA: 0x000B5034 File Offset: 0x000B3234
		public static LocalizedString ReplayServiceResumeRpcInvalidForSingleCopyException(string dbCopy)
		{
			return new LocalizedString("ReplayServiceResumeRpcInvalidForSingleCopyException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				dbCopy
			});
		}

		// Token: 0x170009E6 RID: 2534
		// (get) Token: 0x0600270F RID: 9999 RVA: 0x000B5063 File Offset: 0x000B3263
		public static LocalizedString InvalidSavedStateException
		{
			get
			{
				return new LocalizedString("InvalidSavedStateException", "", false, false, ReplayStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06002710 RID: 10000 RVA: 0x000B5084 File Offset: 0x000B3284
		public static LocalizedString DbValidationCopyStatusRpcFailed(string dbName, string serverName, string error)
		{
			return new LocalizedString("DbValidationCopyStatusRpcFailed", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				dbName,
				serverName,
				error
			});
		}

		// Token: 0x06002711 RID: 10001 RVA: 0x000B50BC File Offset: 0x000B32BC
		public static LocalizedString AmClusterFileNotFoundException(string nodeName)
		{
			return new LocalizedString("AmClusterFileNotFoundException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				nodeName
			});
		}

		// Token: 0x06002712 RID: 10002 RVA: 0x000B50EC File Offset: 0x000B32EC
		public static LocalizedString SeedingAnotherServerException(string seedingServerName, string requestServerName)
		{
			return new LocalizedString("SeedingAnotherServerException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				seedingServerName,
				requestServerName
			});
		}

		// Token: 0x06002713 RID: 10003 RVA: 0x000B5120 File Offset: 0x000B3320
		public static LocalizedString DagTaskOperationFailedException(string errMessage)
		{
			return new LocalizedString("DagTaskOperationFailedException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				errMessage
			});
		}

		// Token: 0x06002714 RID: 10004 RVA: 0x000B5150 File Offset: 0x000B3350
		public static LocalizedString AmDbMoveOperationNotSupportedStandaloneException(string dbName)
		{
			return new LocalizedString("AmDbMoveOperationNotSupportedStandaloneException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				dbName
			});
		}

		// Token: 0x06002715 RID: 10005 RVA: 0x000B5180 File Offset: 0x000B3380
		public static LocalizedString AutoReseedNotAllCopiesPassive(string dbNames)
		{
			return new LocalizedString("AutoReseedNotAllCopiesPassive", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				dbNames
			});
		}

		// Token: 0x06002716 RID: 10006 RVA: 0x000B51B0 File Offset: 0x000B33B0
		public static LocalizedString MonitoringCouldNotFindHubServersException(string siteName, string adError)
		{
			return new LocalizedString("MonitoringCouldNotFindHubServersException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				siteName,
				adError
			});
		}

		// Token: 0x06002717 RID: 10007 RVA: 0x000B51E4 File Offset: 0x000B33E4
		public static LocalizedString FailedToFindDatabaseException(string databaseName)
		{
			return new LocalizedString("FailedToFindDatabaseException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				databaseName
			});
		}

		// Token: 0x06002718 RID: 10008 RVA: 0x000B5214 File Offset: 0x000B3414
		public static LocalizedString DbValidationCopyStatusTooOld(string dbCopyName, TimeSpan actualAgeOfStatus, TimeSpan maxAge)
		{
			return new LocalizedString("DbValidationCopyStatusTooOld", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				dbCopyName,
				actualAgeOfStatus,
				maxAge
			});
		}

		// Token: 0x06002719 RID: 10009 RVA: 0x000B5258 File Offset: 0x000B3458
		public static LocalizedString CouldNotCreateDbDirectoriesException(string database, string volumeName, string errMsg)
		{
			return new LocalizedString("CouldNotCreateDbDirectoriesException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				database,
				volumeName,
				errMsg
			});
		}

		// Token: 0x0600271A RID: 10010 RVA: 0x000B5290 File Offset: 0x000B3490
		public static LocalizedString DatabaseCopyLayoutException(string errorMsg)
		{
			return new LocalizedString("DatabaseCopyLayoutException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				errorMsg
			});
		}

		// Token: 0x0600271B RID: 10011 RVA: 0x000B52C0 File Offset: 0x000B34C0
		public static LocalizedString DatabaseLogCorruptRecoveryFailed(string dbName, string msg)
		{
			return new LocalizedString("DatabaseLogCorruptRecoveryFailed", "ExC836D7", false, true, ReplayStrings.ResourceManager, new object[]
			{
				dbName,
				msg
			});
		}

		// Token: 0x0600271C RID: 10012 RVA: 0x000B52F4 File Offset: 0x000B34F4
		public static LocalizedString MonitoringADLookupTimeoutException(int timeoutMs)
		{
			return new LocalizedString("MonitoringADLookupTimeoutException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				timeoutMs
			});
		}

		// Token: 0x0600271D RID: 10013 RVA: 0x000B5328 File Offset: 0x000B3528
		public static LocalizedString SeederEcDirDoesNotExist(string tempSeedDirectory)
		{
			return new LocalizedString("SeederEcDirDoesNotExist", "Ex5EB5A5", false, true, ReplayStrings.ResourceManager, new object[]
			{
				tempSeedDirectory
			});
		}

		// Token: 0x0600271E RID: 10014 RVA: 0x000B5358 File Offset: 0x000B3558
		public static LocalizedString FailedToKillProcessForServiceException(string serviceName, string msg)
		{
			return new LocalizedString("FailedToKillProcessForServiceException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				serviceName,
				msg
			});
		}

		// Token: 0x170009E7 RID: 2535
		// (get) Token: 0x0600271F RID: 10015 RVA: 0x000B538B File Offset: 0x000B358B
		public static LocalizedString SeederOperationAborted
		{
			get
			{
				return new LocalizedString("SeederOperationAborted", "", false, false, ReplayStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06002720 RID: 10016 RVA: 0x000B53AC File Offset: 0x000B35AC
		public static LocalizedString JetErrorFileIOBeyondEOFException(string pageno)
		{
			return new LocalizedString("JetErrorFileIOBeyondEOFException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				pageno
			});
		}

		// Token: 0x06002721 RID: 10017 RVA: 0x000B53DC File Offset: 0x000B35DC
		public static LocalizedString DatabaseCopySuspendException(string dbName, string server, string msg)
		{
			return new LocalizedString("DatabaseCopySuspendException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				dbName,
				server,
				msg
			});
		}

		// Token: 0x06002722 RID: 10018 RVA: 0x000B5414 File Offset: 0x000B3614
		public static LocalizedString DatabaseNotMounted(string dbName)
		{
			return new LocalizedString("DatabaseNotMounted", "Ex9F64C8", false, true, ReplayStrings.ResourceManager, new object[]
			{
				dbName
			});
		}

		// Token: 0x06002723 RID: 10019 RVA: 0x000B5444 File Offset: 0x000B3644
		public static LocalizedString ExchangeVolumeInfoInitException(string volumeName, string errMsg)
		{
			return new LocalizedString("ExchangeVolumeInfoInitException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				volumeName,
				errMsg
			});
		}

		// Token: 0x06002724 RID: 10020 RVA: 0x000B5478 File Offset: 0x000B3678
		public static LocalizedString CopyPageFailed(int pageno, string source)
		{
			return new LocalizedString("CopyPageFailed", "ExDA5292", false, true, ReplayStrings.ResourceManager, new object[]
			{
				pageno,
				source
			});
		}

		// Token: 0x06002725 RID: 10021 RVA: 0x000B54B0 File Offset: 0x000B36B0
		public static LocalizedString ReplayTestStoreConnectivityTimedoutException(string operationName, string errorMsg)
		{
			return new LocalizedString("ReplayTestStoreConnectivityTimedoutException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				operationName,
				errorMsg
			});
		}

		// Token: 0x06002726 RID: 10022 RVA: 0x000B54E4 File Offset: 0x000B36E4
		public static LocalizedString ReplayServiceSuspendRpcInvalidForSingleCopyException(string dbCopy)
		{
			return new LocalizedString("ReplayServiceSuspendRpcInvalidForSingleCopyException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				dbCopy
			});
		}

		// Token: 0x06002727 RID: 10023 RVA: 0x000B5514 File Offset: 0x000B3714
		public static LocalizedString MonitoringADConfigException(string errorMsg)
		{
			return new LocalizedString("MonitoringADConfigException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				errorMsg
			});
		}

		// Token: 0x170009E8 RID: 2536
		// (get) Token: 0x06002728 RID: 10024 RVA: 0x000B5543 File Offset: 0x000B3743
		public static LocalizedString FailedToOpenShipLogContextInvalidParameter
		{
			get
			{
				return new LocalizedString("FailedToOpenShipLogContextInvalidParameter", "", false, false, ReplayStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06002729 RID: 10025 RVA: 0x000B5564 File Offset: 0x000B3764
		public static LocalizedString LogRepairFailedToCopyFromActive(string tempLogName, string exceptionText)
		{
			return new LocalizedString("LogRepairFailedToCopyFromActive", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				tempLogName,
				exceptionText
			});
		}

		// Token: 0x170009E9 RID: 2537
		// (get) Token: 0x0600272A RID: 10026 RVA: 0x000B5597 File Offset: 0x000B3797
		public static LocalizedString ErrorDagDoesNotHaveAnyMemberServers
		{
			get
			{
				return new LocalizedString("ErrorDagDoesNotHaveAnyMemberServers", "Ex25A318", false, true, ReplayStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600272B RID: 10027 RVA: 0x000B55B8 File Offset: 0x000B37B8
		public static LocalizedString DagTaskValidateNodeTimedOutException(string serverName)
		{
			return new LocalizedString("DagTaskValidateNodeTimedOutException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				serverName
			});
		}

		// Token: 0x0600272C RID: 10028 RVA: 0x000B55E8 File Offset: 0x000B37E8
		public static LocalizedString SeederEcStoreNotOnline(string sourceServerName)
		{
			return new LocalizedString("SeederEcStoreNotOnline", "ExCD6093", false, true, ReplayStrings.ResourceManager, new object[]
			{
				sourceServerName
			});
		}

		// Token: 0x0600272D RID: 10029 RVA: 0x000B5618 File Offset: 0x000B3818
		public static LocalizedString AmRegistryException(string apiName)
		{
			return new LocalizedString("AmRegistryException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				apiName
			});
		}

		// Token: 0x0600272E RID: 10030 RVA: 0x000B5648 File Offset: 0x000B3848
		public static LocalizedString ReplayServiceSuspendInvalidDuringMoveException(string dbCopy)
		{
			return new LocalizedString("ReplayServiceSuspendInvalidDuringMoveException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				dbCopy
			});
		}

		// Token: 0x0600272F RID: 10031 RVA: 0x000B5678 File Offset: 0x000B3878
		public static LocalizedString ReplayServiceRestartInvalidDuringMoveException(string dbCopy)
		{
			return new LocalizedString("ReplayServiceRestartInvalidDuringMoveException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				dbCopy
			});
		}

		// Token: 0x06002730 RID: 10032 RVA: 0x000B56A8 File Offset: 0x000B38A8
		public static LocalizedString ClusterBatchWriter_OpenClusterRootKeyFailed(int ec)
		{
			return new LocalizedString("ClusterBatchWriter_OpenClusterRootKeyFailed", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				ec
			});
		}

		// Token: 0x06002731 RID: 10033 RVA: 0x000B56DC File Offset: 0x000B38DC
		public static LocalizedString AmBcsException(string bcsError)
		{
			return new LocalizedString("AmBcsException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				bcsError
			});
		}

		// Token: 0x06002732 RID: 10034 RVA: 0x000B570C File Offset: 0x000B390C
		public static LocalizedString FileSharingViolationOnSourceException(string serverName, string fileFullPath)
		{
			return new LocalizedString("FileSharingViolationOnSourceException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				serverName,
				fileFullPath
			});
		}

		// Token: 0x06002733 RID: 10035 RVA: 0x000B5740 File Offset: 0x000B3940
		public static LocalizedString NetworkRemoteError(string nodeName, string messageText)
		{
			return new LocalizedString("NetworkRemoteError", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				nodeName,
				messageText
			});
		}

		// Token: 0x06002734 RID: 10036 RVA: 0x000B5774 File Offset: 0x000B3974
		public static LocalizedString DbVolumeInvalidDirectoriesException(string volumeName, string mountedFolder, int numExpected, int numActual)
		{
			return new LocalizedString("DbVolumeInvalidDirectoriesException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				volumeName,
				mountedFolder,
				numExpected,
				numActual
			});
		}

		// Token: 0x06002735 RID: 10037 RVA: 0x000B57BC File Offset: 0x000B39BC
		public static LocalizedString LogInspectorFailed(string errorMsg)
		{
			return new LocalizedString("LogInspectorFailed", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				errorMsg
			});
		}

		// Token: 0x06002736 RID: 10038 RVA: 0x000B57EC File Offset: 0x000B39EC
		public static LocalizedString AmBcsTargetServerActivationIntraSite(string targetServer, string sourceServer, string targetSite, string sourceSite)
		{
			return new LocalizedString("AmBcsTargetServerActivationIntraSite", "ExA93FD4", false, true, ReplayStrings.ResourceManager, new object[]
			{
				targetServer,
				sourceServer,
				targetSite,
				sourceSite
			});
		}

		// Token: 0x06002737 RID: 10039 RVA: 0x000B5828 File Offset: 0x000B3A28
		public static LocalizedString AmBcsTargetNodeDebugOptionEnabled(string server, string debugOptions)
		{
			return new LocalizedString("AmBcsTargetNodeDebugOptionEnabled", "Ex05A3FC", false, true, ReplayStrings.ResourceManager, new object[]
			{
				server,
				debugOptions
			});
		}

		// Token: 0x170009EA RID: 2538
		// (get) Token: 0x06002738 RID: 10040 RVA: 0x000B585B File Offset: 0x000B3A5B
		public static LocalizedString ErrorAmInjectedUnknownConfig
		{
			get
			{
				return new LocalizedString("ErrorAmInjectedUnknownConfig", "Ex46DB23", false, true, ReplayStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06002739 RID: 10041 RVA: 0x000B587C File Offset: 0x000B3A7C
		public static LocalizedString GranularReplicationTerminated(string reason)
		{
			return new LocalizedString("GranularReplicationTerminated", "ExBDC931", false, true, ReplayStrings.ResourceManager, new object[]
			{
				reason
			});
		}

		// Token: 0x0600273A RID: 10042 RVA: 0x000B58AC File Offset: 0x000B3AAC
		public static LocalizedString AutoReseedException(string errorMsg)
		{
			return new LocalizedString("AutoReseedException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				errorMsg
			});
		}

		// Token: 0x0600273B RID: 10043 RVA: 0x000B58DC File Offset: 0x000B3ADC
		public static LocalizedString FailedToGetDatabaseInfo(int error)
		{
			return new LocalizedString("FailedToGetDatabaseInfo", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				error
			});
		}

		// Token: 0x0600273C RID: 10044 RVA: 0x000B5910 File Offset: 0x000B3B10
		public static LocalizedString CiSeederSearchCatalogRpcTransientException(string message)
		{
			return new LocalizedString("CiSeederSearchCatalogRpcTransientException", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				message
			});
		}

		// Token: 0x0600273D RID: 10045 RVA: 0x000B5940 File Offset: 0x000B3B40
		public static LocalizedString AutoReseedFailedReseedBlocked(string error)
		{
			return new LocalizedString("AutoReseedFailedReseedBlocked", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				error
			});
		}

		// Token: 0x170009EB RID: 2539
		// (get) Token: 0x0600273E RID: 10046 RVA: 0x000B596F File Offset: 0x000B3B6F
		public static LocalizedString DisableReplayLagOperationName
		{
			get
			{
				return new LocalizedString("DisableReplayLagOperationName", "", false, false, ReplayStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600273F RID: 10047 RVA: 0x000B5990 File Offset: 0x000B3B90
		public static LocalizedString FileCheckInternalError(string condition)
		{
			return new LocalizedString("FileCheckInternalError", "", false, false, ReplayStrings.ResourceManager, new object[]
			{
				condition
			});
		}

		// Token: 0x06002740 RID: 10048 RVA: 0x000B59BF File Offset: 0x000B3BBF
		public static LocalizedString GetLocalizedString(ReplayStrings.IDs key)
		{
			return new LocalizedString(ReplayStrings.stringIDs[(uint)key], ReplayStrings.ResourceManager, new object[0]);
		}

		// Token: 0x040010DA RID: 4314
		private static Dictionary<uint, string> stringIDs = new Dictionary<uint, string>(150);

		// Token: 0x040010DB RID: 4315
		private static ExchangeResourceManager ResourceManager = ExchangeResourceManager.GetResourceManager("Microsoft.Exchange.Cluster.Replay.Strings", typeof(ReplayStrings).GetTypeInfo().Assembly);

		// Token: 0x02000395 RID: 917
		public enum IDs : uint
		{
			// Token: 0x040010DD RID: 4317
			RepairStateClusterIsNotRunning = 800943078U,
			// Token: 0x040010DE RID: 4318
			RemoveLogReasonE00OutOfDate = 3145329300U,
			// Token: 0x040010DF RID: 4319
			FailedToOpenShipLogContextAccessDenied = 2169511412U,
			// Token: 0x040010E0 RID: 4320
			ReplayServiceRpcUnknownInstanceException = 3896662565U,
			// Token: 0x040010E1 RID: 4321
			LockOwnerIdle = 1265493030U,
			// Token: 0x040010E2 RID: 4322
			IncSeedNotSupportedWithShrinkDatabaseError = 1348435424U,
			// Token: 0x040010E3 RID: 4323
			CantStartFromCommandLineTitle = 1095788331U,
			// Token: 0x040010E4 RID: 4324
			AmBcsDatabaseHasNoCopies = 1390940095U,
			// Token: 0x040010E5 RID: 4325
			ErrorCouldNotFindClusterGroupOwnerNodeForAmConfig = 1065254054U,
			// Token: 0x040010E6 RID: 4326
			SeederEcDBNotFound = 615086071U,
			// Token: 0x040010E7 RID: 4327
			SeederEcSeedingCancelled = 276770850U,
			// Token: 0x040010E8 RID: 4328
			DagTaskInstalledFailoverClustering = 3718483653U,
			// Token: 0x040010E9 RID: 4329
			SeederEcDeviceNotReady = 2876590454U,
			// Token: 0x040010EA RID: 4330
			ErrorReadingDagServerForAmConfig = 3794143290U,
			// Token: 0x040010EB RID: 4331
			NetworkReadEOF = 3273471454U,
			// Token: 0x040010EC RID: 4332
			RemoveLogReasonFailedInspection = 3630272863U,
			// Token: 0x040010ED RID: 4333
			InvalidLogCopyResponse = 3925066867U,
			// Token: 0x040010EE RID: 4334
			MonitoringADInitNotCompleteException = 2936934256U,
			// Token: 0x040010EF RID: 4335
			AutoReseedFailedToFindVolumeName = 861304653U,
			// Token: 0x040010F0 RID: 4336
			DagTaskComponentManagerAnotherInstanceRunning = 1749232750U,
			// Token: 0x040010F1 RID: 4337
			AutoReseedManualReseedLaunched = 4014700033U,
			// Token: 0x040010F2 RID: 4338
			SeederEcNoOnlineEdb = 3988781731U,
			// Token: 0x040010F3 RID: 4339
			FullServerSeedInProgressException = 3096281422U,
			// Token: 0x040010F4 RID: 4340
			ReplayServiceSuspendCommentException = 418608118U,
			// Token: 0x040010F5 RID: 4341
			InvalidDbForSeedSpecifiedException = 856033660U,
			// Token: 0x040010F6 RID: 4342
			VolumeMountPathDoesNotExistException = 798341136U,
			// Token: 0x040010F7 RID: 4343
			AmDbOperationWaitFailedException = 1105864708U,
			// Token: 0x040010F8 RID: 4344
			ErrorAmConfigNotInitialized = 2630456743U,
			// Token: 0x040010F9 RID: 4345
			NetworkCorruptDataGeneric = 2364081162U,
			// Token: 0x040010FA RID: 4346
			ErrorAutomountConsensusNotReached = 2266299742U,
			// Token: 0x040010FB RID: 4347
			AmBcsNoneSpecified = 1607989356U,
			// Token: 0x040010FC RID: 4348
			SeederEcNotEnoughDiskException = 3766459347U,
			// Token: 0x040010FD RID: 4349
			NetworkFailedToAuthServer = 1148446483U,
			// Token: 0x040010FE RID: 4350
			ErrorFailedToFindLocalServer = 4045836413U,
			// Token: 0x040010FF RID: 4351
			FailedToOpenShipLogContextDatabaseNotMounted = 4106538885U,
			// Token: 0x04001100 RID: 4352
			ReplayServiceSuspendBlockedAcllException = 910204805U,
			// Token: 0x04001101 RID: 4353
			DatabaseCopyLayoutTableNullException = 1295385018U,
			// Token: 0x04001102 RID: 4354
			AmServiceShuttingDown = 820683245U,
			// Token: 0x04001103 RID: 4355
			DagTaskDagIpAddressesMustBeIpv4Exception = 512707820U,
			// Token: 0x04001104 RID: 4356
			UnknownError = 3351215994U,
			// Token: 0x04001105 RID: 4357
			EseBackFileSystemCorruption = 3572810924U,
			// Token: 0x04001106 RID: 4358
			SuspendOperationName = 214622620U,
			// Token: 0x04001107 RID: 4359
			PagePatchLegacyFileExistsException = 3342720985U,
			// Token: 0x04001108 RID: 4360
			SeederEcSuccess = 606338949U,
			// Token: 0x04001109 RID: 4361
			NoServices = 1055054291U,
			// Token: 0x0400110A RID: 4362
			StoreServiceMonitorCriticalError = 1298429885U,
			// Token: 0x0400110B RID: 4363
			CannotChangeProperties = 3887748188U,
			// Token: 0x0400110C RID: 4364
			SeederEchrInvalidCallSequence = 2034469932U,
			// Token: 0x0400110D RID: 4365
			FailedToOpenShipLogContextEseCircularLoggingEnabled = 3043738041U,
			// Token: 0x0400110E RID: 4366
			ReplayServiceResumeRpcFailedException = 2591444232U,
			// Token: 0x0400110F RID: 4367
			NetworkSecurityFailed = 1344658319U,
			// Token: 0x04001110 RID: 4368
			ReplayServiceSuspendRpcFailedException = 3184656137U,
			// Token: 0x04001111 RID: 4369
			SuspendWantedWriteFailedException = 1163467430U,
			// Token: 0x04001112 RID: 4370
			ReplayServiceSuspendResumeBlockedException = 3512548730U,
			// Token: 0x04001113 RID: 4371
			AmDbActionDismountFailedException = 1378783691U,
			// Token: 0x04001114 RID: 4372
			AutoReseedNeverMountedWorkflowReason = 387713678U,
			// Token: 0x04001115 RID: 4373
			AutoReseedLogAndDbNotOnSameVolume = 1967903804U,
			// Token: 0x04001116 RID: 4374
			FullServerSeedSkippedShutdownException = 85017254U,
			// Token: 0x04001117 RID: 4375
			SeederEcOverlappedWriteErr = 2554265426U,
			// Token: 0x04001118 RID: 4376
			FailToCleanUpFiles = 3580573960U,
			// Token: 0x04001119 RID: 4377
			SeederEcNotEnoughDisk = 2045268632U,
			// Token: 0x0400111A RID: 4378
			MonitoringADServiceShuttingDownException = 1045878879U,
			// Token: 0x0400111B RID: 4379
			DbHTServiceShuttingDownException = 1612966754U,
			// Token: 0x0400111C RID: 4380
			NullDbCopyException = 4049732327U,
			// Token: 0x0400111D RID: 4381
			ErrorClusterServiceNotRunningForAmConfig = 2602551968U,
			// Token: 0x0400111E RID: 4382
			SeederEcError = 3128283390U,
			// Token: 0x0400111F RID: 4383
			AutoReseedFailedAdminSuspended = 129692820U,
			// Token: 0x04001120 RID: 4384
			NetworkNoUsableEndpoints = 726910307U,
			// Token: 0x04001121 RID: 4385
			ClusterServiceMonitorCriticalError = 3772399040U,
			// Token: 0x04001122 RID: 4386
			Resynchronizing = 1871702564U,
			// Token: 0x04001123 RID: 4387
			ReplayServiceSuspendBlockedResynchronizingException = 3929636263U,
			// Token: 0x04001124 RID: 4388
			LockOwnerComponent = 828128533U,
			// Token: 0x04001125 RID: 4389
			NetworkIsDisabled = 4232351636U,
			// Token: 0x04001126 RID: 4390
			ResumeOperationName = 707284339U,
			// Token: 0x04001127 RID: 4391
			ReplayServiceSuspendReseedBlockedException = 2777244007U,
			// Token: 0x04001128 RID: 4392
			SuspendMessageWriteFailedException = 544686542U,
			// Token: 0x04001129 RID: 4393
			SyncSuspendResumeOperationName = 627050854U,
			// Token: 0x0400112A RID: 4394
			FailedAndSuspended = 1279119335U,
			// Token: 0x0400112B RID: 4395
			TPRProviderNotListening = 1839653327U,
			// Token: 0x0400112C RID: 4396
			Suspended = 3099361335U,
			// Token: 0x0400112D RID: 4397
			ReplayServiceSuspendInPlaceReseedBlockedException = 4137367259U,
			// Token: 0x0400112E RID: 4398
			AutoReseedMoveActiveBeforeRebuildCatalog = 3167054525U,
			// Token: 0x0400112F RID: 4399
			ErrorCouldNotConnectClusterForAmConfig = 448095525U,
			// Token: 0x04001130 RID: 4400
			ReplayServiceShuttingDownException = 3631647333U,
			// Token: 0x04001131 RID: 4401
			ErrorFailedToOpenClusterObjects = 2056943490U,
			// Token: 0x04001132 RID: 4402
			FailedToOpenShipLogContextStoreStopped = 4042526291U,
			// Token: 0x04001133 RID: 4403
			NullDatabaseException = 1622537741U,
			// Token: 0x04001134 RID: 4404
			SeederEcCommunicationsError = 865952845U,
			// Token: 0x04001135 RID: 4405
			DagTaskPamNotMovedSubsequentOperationsMayBeSlowOrUnreliable = 1445478759U,
			// Token: 0x04001136 RID: 4406
			SeederEcFailAcqRight = 482041519U,
			// Token: 0x04001137 RID: 4407
			ProgressStatusInProgress = 204895011U,
			// Token: 0x04001138 RID: 4408
			CouldNotGetMountStatus = 3439296139U,
			// Token: 0x04001139 RID: 4409
			AmClusterNotRunningException = 3630311467U,
			// Token: 0x0400113A RID: 4410
			LockOwnerConfigChecker = 2719597889U,
			// Token: 0x0400113B RID: 4411
			PrepareToStopCalled = 200627337U,
			// Token: 0x0400113C RID: 4412
			ReplayServiceSuspendWantedClearedException = 394420742U,
			// Token: 0x0400113D RID: 4413
			DBCHasNoValidTargetEdbPath = 913564495U,
			// Token: 0x0400113E RID: 4414
			DeleteChkptReasonCorrupted = 682288005U,
			// Token: 0x0400113F RID: 4415
			TPRChangeFailedBecauseNotDismounted = 3559163736U,
			// Token: 0x04001140 RID: 4416
			CouldNotFindVolumeForFormatException = 3778987676U,
			// Token: 0x04001141 RID: 4417
			CannotChangeName = 2242986398U,
			// Token: 0x04001142 RID: 4418
			NetworkManagerInitError = 3293194015U,
			// Token: 0x04001143 RID: 4419
			PassiveCopyDisconnected = 1105077015U,
			// Token: 0x04001144 RID: 4420
			SeederEchrErrorFromESECall = 3691652467U,
			// Token: 0x04001145 RID: 4421
			ReplayServiceSuspendBlockedBackupInProgressException = 2139980255U,
			// Token: 0x04001146 RID: 4422
			ErrorCouldNotFindServerForAmConfig = 3673369909U,
			// Token: 0x04001147 RID: 4423
			FailedAtReplacingLogFiles = 2812191260U,
			// Token: 0x04001148 RID: 4424
			ReplayServiceResumeRpcFailedSeedingException = 887012549U,
			// Token: 0x04001149 RID: 4425
			StoreNotOnline = 3025714949U,
			// Token: 0x0400114A RID: 4426
			LockOwnerAttemptCopyLastLogs = 2151299999U,
			// Token: 0x0400114B RID: 4427
			MsexchangereplLong = 833784388U,
			// Token: 0x0400114C RID: 4428
			AmOperationInvalidForStandaloneRoleException = 2787647009U,
			// Token: 0x0400114D RID: 4429
			JetErrorDatabaseNotFound = 1556772377U,
			// Token: 0x0400114E RID: 4430
			SeederEcBackupInProgress = 2325224058U,
			// Token: 0x0400114F RID: 4431
			LogCopierFailedToGetSuspendLock = 511004165U,
			// Token: 0x04001150 RID: 4432
			NetworkDataOverflowGeneric = 3508567603U,
			// Token: 0x04001151 RID: 4433
			TPRNotEnabled = 4084429598U,
			// Token: 0x04001152 RID: 4434
			LockOwnerSuspend = 458303596U,
			// Token: 0x04001153 RID: 4435
			DbValidationFullCopyStatusResultsLabel = 1791510079U,
			// Token: 0x04001154 RID: 4436
			LogRepairNotPossibleInsuffientToCheckDivergence = 3318672153U,
			// Token: 0x04001155 RID: 4437
			DagTaskComponentManagerWantsToRebootException = 1539727809U,
			// Token: 0x04001156 RID: 4438
			NetworkCancelled = 3985107625U,
			// Token: 0x04001157 RID: 4439
			ErrorFailedToGetClusterCoreGroup = 413167004U,
			// Token: 0x04001158 RID: 4440
			NotInPendingState = 1348455624U,
			// Token: 0x04001159 RID: 4441
			ErrorInvalidPamServerName = 662858283U,
			// Token: 0x0400115A RID: 4442
			SeederEchrErrorFromCallbackCall = 2070740709U,
			// Token: 0x0400115B RID: 4443
			EnableReplayLagOperationName = 4031202440U,
			// Token: 0x0400115C RID: 4444
			SeederInstanceAlreadyFailedException = 2490829887U,
			// Token: 0x0400115D RID: 4445
			ReplayServiceSuspendWantedSetException = 854026562U,
			// Token: 0x0400115E RID: 4446
			SeederEcOOMem = 1857824471U,
			// Token: 0x0400115F RID: 4447
			DbValidationCopyStatusNameLabel = 399717229U,
			// Token: 0x04001160 RID: 4448
			SeederEchrRestoreAtFileLevel = 3239149605U,
			// Token: 0x04001161 RID: 4449
			TPRNotYetStarted = 2097320554U,
			// Token: 0x04001162 RID: 4450
			SeederEcInvalidInput = 560332821U,
			// Token: 0x04001163 RID: 4451
			ErrorRemoteSiteNotConnected = 3655345505U,
			// Token: 0x04001164 RID: 4452
			ErrorLocalNodeNotUpYet = 4251585129U,
			// Token: 0x04001165 RID: 4453
			DagTaskClusteringRequiresEnterpriseSkuException = 1556943668U,
			// Token: 0x04001166 RID: 4454
			MsexchangesearchLong = 844399741U,
			// Token: 0x04001167 RID: 4455
			LockOwnerBackup = 3809273542U,
			// Token: 0x04001168 RID: 4456
			AmDbActionMoveFailedException = 1718068369U,
			// Token: 0x04001169 RID: 4457
			Failed = 1054423051U,
			// Token: 0x0400116A RID: 4458
			GranularReplicationOverflow = 3438214222U,
			// Token: 0x0400116B RID: 4459
			CantStartFromCommandLine = 3142177591U,
			// Token: 0x0400116C RID: 4460
			AmDbActionMountFailedException = 1944472637U,
			// Token: 0x0400116D RID: 4461
			InvalidSavedStateException = 2389546758U,
			// Token: 0x0400116E RID: 4462
			SeederOperationAborted = 1836563684U,
			// Token: 0x0400116F RID: 4463
			FailedToOpenShipLogContextInvalidParameter = 3897089617U,
			// Token: 0x04001170 RID: 4464
			ErrorDagDoesNotHaveAnyMemberServers = 2203487748U,
			// Token: 0x04001171 RID: 4465
			ErrorAmInjectedUnknownConfig = 1450196582U,
			// Token: 0x04001172 RID: 4466
			DisableReplayLagOperationName = 663986635U
		}

		// Token: 0x02000396 RID: 918
		private enum ParamIDs
		{
			// Token: 0x04001174 RID: 4468
			SeederFailedToCreateDirectory,
			// Token: 0x04001175 RID: 4469
			RepairStateLocalServerIsNotInDag,
			// Token: 0x04001176 RID: 4470
			PagePatchFileDeletionException,
			// Token: 0x04001177 RID: 4471
			AcllLastLogNotFoundException,
			// Token: 0x04001178 RID: 4472
			ReplayServiceResumeInvalidDuringMoveException,
			// Token: 0x04001179 RID: 4473
			DumpsterRedeliveryException,
			// Token: 0x0400117A RID: 4474
			LastLogReplacementFileNotSubsetException,
			// Token: 0x0400117B RID: 4475
			ReplayServiceRestartInvalidSeedingException,
			// Token: 0x0400117C RID: 4476
			DagTaskComponentManagerGenericFailure,
			// Token: 0x0400117D RID: 4477
			DumpsterCouldNotFindHubServerException,
			// Token: 0x0400117E RID: 4478
			LastLogReplacementRollbackFailedException,
			// Token: 0x0400117F RID: 4479
			AmPreMountCallbackFailedMountInhibitException,
			// Token: 0x04001180 RID: 4480
			InvalidRcrConfigOnNonMailboxException,
			// Token: 0x04001181 RID: 4481
			SeederEcLogAlreadyExist,
			// Token: 0x04001182 RID: 4482
			AutoReseedTooManyConcurrentSeeds,
			// Token: 0x04001183 RID: 4483
			FileCheckAccessDeniedDismountFailedException,
			// Token: 0x04001184 RID: 4484
			GranularReplicationInitFailed,
			// Token: 0x04001185 RID: 4485
			AmDbNotMountedMultipleServersException,
			// Token: 0x04001186 RID: 4486
			FailedToGetCopyStatus,
			// Token: 0x04001187 RID: 4487
			DisableReplayLagWriteFailedException,
			// Token: 0x04001188 RID: 4488
			TargetDbNotThere,
			// Token: 0x04001189 RID: 4489
			CiSeederExchangeSearchTransientException,
			// Token: 0x0400118A RID: 4490
			PotentialRedundancyValidationDBReplicationStalled,
			// Token: 0x0400118B RID: 4491
			LastLogReplacementTempOldFileNotDeletedException,
			// Token: 0x0400118C RID: 4492
			PreserveInspectorLogsError,
			// Token: 0x0400118D RID: 4493
			ServerStopped,
			// Token: 0x0400118E RID: 4494
			SeederInstanceAlreadyAddedException,
			// Token: 0x0400118F RID: 4495
			AutoReseedAllCatalogFailed,
			// Token: 0x04001190 RID: 4496
			LogCopierE00InconsistentError,
			// Token: 0x04001191 RID: 4497
			FailedToDisableMountPointConfigurationException,
			// Token: 0x04001192 RID: 4498
			AcllFailedLogDivergenceDetected,
			// Token: 0x04001193 RID: 4499
			AutoReseedFailedCopyWorkflowSuspendedCopy,
			// Token: 0x04001194 RID: 4500
			CiSeederSearchCatalogRpcPermanentException,
			// Token: 0x04001195 RID: 4501
			InvalidRCROperationOnNonRcrDB,
			// Token: 0x04001196 RID: 4502
			CouldNotDeleteDbMountPointException,
			// Token: 0x04001197 RID: 4503
			AutoReseedCatalogActiveException,
			// Token: 0x04001198 RID: 4504
			SafeDeleteExistingFilesDataRedundancyNoResultException,
			// Token: 0x04001199 RID: 4505
			NetworkAddressResolutionFailed,
			// Token: 0x0400119A RID: 4506
			FileCheckDatabaseLogfileSignature,
			// Token: 0x0400119B RID: 4507
			AcllCopyStatusResumeBlockedException,
			// Token: 0x0400119C RID: 4508
			CiServiceDownException,
			// Token: 0x0400119D RID: 4509
			AmBcsDatabaseCopyIsSeedingSource,
			// Token: 0x0400119E RID: 4510
			FailedToDeleteTempDatabase,
			// Token: 0x0400119F RID: 4511
			SeederFailedToInspectLogException,
			// Token: 0x040011A0 RID: 4512
			ClusterBatchWriter_CommitFailed,
			// Token: 0x040011A1 RID: 4513
			DbAvailabilityActiveCopyUnknownState,
			// Token: 0x040011A2 RID: 4514
			AmBcsSelectionException,
			// Token: 0x040011A3 RID: 4515
			TargetChkptAlreadyExists,
			// Token: 0x040011A4 RID: 4516
			DatabaseDismountOrKillStoreException,
			// Token: 0x040011A5 RID: 4517
			LogTruncationException,
			// Token: 0x040011A6 RID: 4518
			SeederFailedToFindDirectoriesUnderMountPoint,
			// Token: 0x040011A7 RID: 4519
			AmDbActionRejectedMountAtStartupNotEnabledException,
			// Token: 0x040011A8 RID: 4520
			DagNetworkRpcServerError,
			// Token: 0x040011A9 RID: 4521
			AmDbMoveOperationNotSupportedException,
			// Token: 0x040011AA RID: 4522
			FailedToDeserializeStr,
			// Token: 0x040011AB RID: 4523
			AmBcsDatabaseCopyCatalogUnhealthy,
			// Token: 0x040011AC RID: 4524
			LogRepairFailedToVerifyFromActive,
			// Token: 0x040011AD RID: 4525
			SafetyNetVersionCheckException,
			// Token: 0x040011AE RID: 4526
			SuspendCommentTooLong,
			// Token: 0x040011AF RID: 4527
			AmBcsDatabaseCopyTotalQueueLengthTooHigh,
			// Token: 0x040011B0 RID: 4528
			SeederOperationFailedException,
			// Token: 0x040011B1 RID: 4529
			AutoReseedInvalidLogFolderPath,
			// Token: 0x040011B2 RID: 4530
			AmBcsDatabaseCopyAlreadyTried,
			// Token: 0x040011B3 RID: 4531
			InsufficientSparesForExtraCopiesException,
			// Token: 0x040011B4 RID: 4532
			AcllCouldNotControlReplicaInstanceException,
			// Token: 0x040011B5 RID: 4533
			DagTaskClusteringMustBeInstalledException,
			// Token: 0x040011B6 RID: 4534
			DagTaskRemoveNodeNotUpException,
			// Token: 0x040011B7 RID: 4535
			DagTaskComponentManagerServerManagerPSFailure,
			// Token: 0x040011B8 RID: 4536
			CouldNotFindDatabase,
			// Token: 0x040011B9 RID: 4537
			DbMoveSkippedBecauseNotFoundInClusDb,
			// Token: 0x040011BA RID: 4538
			DagTaskSetDagNeedsAllNodesUpToChangeQuorumException,
			// Token: 0x040011BB RID: 4539
			AmInvalidActionCodeException,
			// Token: 0x040011BC RID: 4540
			ReplayConfigNotFoundException,
			// Token: 0x040011BD RID: 4541
			DatabaseLogFoldersNotUnderMountpoint,
			// Token: 0x040011BE RID: 4542
			ReplayServiceTooMuchMemoryNoDumpException,
			// Token: 0x040011BF RID: 4543
			NetworkNameNotFound,
			// Token: 0x040011C0 RID: 4544
			AmMountCallbackFailedWithDBFolderNotUnderMountPointException,
			// Token: 0x040011C1 RID: 4545
			RegistryParameterReadException,
			// Token: 0x040011C2 RID: 4546
			IncrementalReseedFailedException,
			// Token: 0x040011C3 RID: 4547
			FailedToNotifySourceLogTrunc,
			// Token: 0x040011C4 RID: 4548
			FileCheckInvalidDatabaseState,
			// Token: 0x040011C5 RID: 4549
			SeederFailedToDeleteCheckpoint,
			// Token: 0x040011C6 RID: 4550
			LogRepairNotPossibleActiveIsDivergent,
			// Token: 0x040011C7 RID: 4551
			FailedToOpenBackupFileHandle,
			// Token: 0x040011C8 RID: 4552
			DeleteChkptReasonTooFarBehindAndLogMissing,
			// Token: 0x040011C9 RID: 4553
			NetworkNotUsable,
			// Token: 0x040011CA RID: 4554
			AutoReseedWorkflowNotSupportedOnTPR,
			// Token: 0x040011CB RID: 4555
			ReplayLagRpcUnsupportedException,
			// Token: 0x040011CC RID: 4556
			RepairStateDatabaseShouldBeDismounted,
			// Token: 0x040011CD RID: 4557
			RegistryParameterKeyNotOpenedException,
			// Token: 0x040011CE RID: 4558
			DagTaskServerException,
			// Token: 0x040011CF RID: 4559
			ReplayServiceRpcCopyStatusTimeoutException,
			// Token: 0x040011D0 RID: 4560
			AutoReseedTooManyFailedCopies,
			// Token: 0x040011D1 RID: 4561
			CiSeederCatalogCouldNotDismount,
			// Token: 0x040011D2 RID: 4562
			CancelSeedingDueToFailed,
			// Token: 0x040011D3 RID: 4563
			IOBufferPoolLimitError,
			// Token: 0x040011D4 RID: 4564
			AutoReseedInPlaceReseedTooSoon,
			// Token: 0x040011D5 RID: 4565
			AmBcsDagNotFoundInAd,
			// Token: 0x040011D6 RID: 4566
			AmBcsDatabaseCopyResynchronizing,
			// Token: 0x040011D7 RID: 4567
			FoundTooManyVolumesWithSameVolumeLabelException,
			// Token: 0x040011D8 RID: 4568
			AmDbMoveOperationOnTimeoutFailureCancelled,
			// Token: 0x040011D9 RID: 4569
			DbFixupFailedVolumeHasMaxDbMountPointsException,
			// Token: 0x040011DA RID: 4570
			MissingActiveLogRequiredForDivergenceDetection,
			// Token: 0x040011DB RID: 4571
			ClusterBatchWriter_FailedToReadClusterRegistry,
			// Token: 0x040011DC RID: 4572
			AutoReseedCatalogSkipRebuild,
			// Token: 0x040011DD RID: 4573
			SeederInstanceAlreadyInProgressException,
			// Token: 0x040011DE RID: 4574
			FileCheckUnableToDeleteCheckpointError,
			// Token: 0x040011DF RID: 4575
			AcllInvalidForSingleCopyException,
			// Token: 0x040011E0 RID: 4576
			CiSeederRpcOperationFailedException,
			// Token: 0x040011E1 RID: 4577
			MonitoringADConfigStaleException,
			// Token: 0x040011E2 RID: 4578
			EseutilParseError,
			// Token: 0x040011E3 RID: 4579
			AmClusterEvictWithoutCleanupException,
			// Token: 0x040011E4 RID: 4580
			FileSystemCorruptionDetected,
			// Token: 0x040011E5 RID: 4581
			DbValidationPassiveCopyUnhealthyState,
			// Token: 0x040011E6 RID: 4582
			LogCopierE00MissingPrevious,
			// Token: 0x040011E7 RID: 4583
			LogDriveNotBigEnough,
			// Token: 0x040011E8 RID: 4584
			DagTaskAddingServerToDag,
			// Token: 0x040011E9 RID: 4585
			DatabaseNotFound,
			// Token: 0x040011EA RID: 4586
			AmBcsDatabaseCopyFailed,
			// Token: 0x040011EB RID: 4587
			AmDbRemountSkippedSinceMasterChanged,
			// Token: 0x040011EC RID: 4588
			AutoReseedNoExchangeVolumesConfigured,
			// Token: 0x040011ED RID: 4589
			AmDatabaseNameNotFoundException,
			// Token: 0x040011EE RID: 4590
			NoInstancesFoundForManagementPath,
			// Token: 0x040011EF RID: 4591
			LastLogReplacementFailedUnexpectedFileFoundException,
			// Token: 0x040011F0 RID: 4592
			ExchangeVolumeInfoMultipleDbMountPointsException,
			// Token: 0x040011F1 RID: 4593
			MonitoringServerSiteIsNullException,
			// Token: 0x040011F2 RID: 4594
			AmServiceMonitorSystemShutdownException,
			// Token: 0x040011F3 RID: 4595
			DatabaseFailedToGetVolumeInfo,
			// Token: 0x040011F4 RID: 4596
			LogRepairFailedTransient,
			// Token: 0x040011F5 RID: 4597
			AcllCopyStatusInvalidException,
			// Token: 0x040011F6 RID: 4598
			DatabaseGroupNotSetException,
			// Token: 0x040011F7 RID: 4599
			AcllBackupInProgressException,
			// Token: 0x040011F8 RID: 4600
			AmDbMoveOperationNoLongerApplicableException,
			// Token: 0x040011F9 RID: 4601
			SeederSuspendFailedException,
			// Token: 0x040011FA RID: 4602
			CouldNotCreateDbMountPointFolderException,
			// Token: 0x040011FB RID: 4603
			RemoteRegistryTimedOutException,
			// Token: 0x040011FC RID: 4604
			MonitoringCouldNotFindDatabasesException,
			// Token: 0x040011FD RID: 4605
			TargetDBAlreadyExists,
			// Token: 0x040011FE RID: 4606
			SeederCatalogNotHealthyErr,
			// Token: 0x040011FF RID: 4607
			ReplayServiceUnknownReplicaInstanceException,
			// Token: 0x04001200 RID: 4608
			PagePatchFileReadException,
			// Token: 0x04001201 RID: 4609
			SeedingChannelIsClosedException,
			// Token: 0x04001202 RID: 4610
			ReplayDatabaseOperationTimedoutException,
			// Token: 0x04001203 RID: 4611
			AutoReseedFailedSeedRetryExceeded,
			// Token: 0x04001204 RID: 4612
			AcllLastLogTimeErrorException,
			// Token: 0x04001205 RID: 4613
			SeederOperationFailedWithEcException,
			// Token: 0x04001206 RID: 4614
			VolumeCouldNotBeReclaimedException,
			// Token: 0x04001207 RID: 4615
			DbValidationNotEnoughCopies,
			// Token: 0x04001208 RID: 4616
			RepairStateFailedToCreateTempLogFile,
			// Token: 0x04001209 RID: 4617
			DagTaskRemoveDagServerMustHaveQuorumException,
			// Token: 0x0400120A RID: 4618
			AcllFailedCurrentLogPresent,
			// Token: 0x0400120B RID: 4619
			AutoReseedFailedToFindTargetVolumeName,
			// Token: 0x0400120C RID: 4620
			AmBcsTargetServerIsHAComponentOffline,
			// Token: 0x0400120D RID: 4621
			NoDivergedPointFound,
			// Token: 0x0400120E RID: 4622
			SeederRpcServerLevelUnsupportedException,
			// Token: 0x0400120F RID: 4623
			CouldNotCreateDbMountPointException,
			// Token: 0x04001210 RID: 4624
			FailedToConfigureMountPointException,
			// Token: 0x04001211 RID: 4625
			AmDismountSucceededButStillMounted,
			// Token: 0x04001212 RID: 4626
			LogCopierInitFailedBecauseNoLogsOnSource,
			// Token: 0x04001213 RID: 4627
			FailureItemRecoveryFailed,
			// Token: 0x04001214 RID: 4628
			AcllCopyStatusFailedException,
			// Token: 0x04001215 RID: 4629
			LastLogReplacementTooManyTempFilesException,
			// Token: 0x04001216 RID: 4630
			AmBcsTargetServerActivationDisabled,
			// Token: 0x04001217 RID: 4631
			ManagementApiError,
			// Token: 0x04001218 RID: 4632
			KernelWatchdogTimerError,
			// Token: 0x04001219 RID: 4633
			AmDbActionTransientException,
			// Token: 0x0400121A RID: 4634
			AmBcsActiveCopyIsSeedingSource,
			// Token: 0x0400121B RID: 4635
			NetworkAddressResolutionFailedNoDnsEntry,
			// Token: 0x0400121C RID: 4636
			AmBcsDatabaseCopyInitializing,
			// Token: 0x0400121D RID: 4637
			RepairStateFailedPendingPagePatchException,
			// Token: 0x0400121E RID: 4638
			NetworkTimeoutError,
			// Token: 0x0400121F RID: 4639
			CouldNotFindVolumeException,
			// Token: 0x04001220 RID: 4640
			AmCommonException,
			// Token: 0x04001221 RID: 4641
			DagTaskRemoteOperationLogEnd,
			// Token: 0x04001222 RID: 4642
			RepairStateDatabaseIsActive,
			// Token: 0x04001223 RID: 4643
			DirectoryEnumeratorIOError,
			// Token: 0x04001224 RID: 4644
			NetworkTransportError,
			// Token: 0x04001225 RID: 4645
			DagTaskMovedPam,
			// Token: 0x04001226 RID: 4646
			LogInspectorE00OutOfSequence,
			// Token: 0x04001227 RID: 4647
			LogCopierInitFailedActiveTruncatingException,
			// Token: 0x04001228 RID: 4648
			AmFailedToDetermineDatabaseMountStatus,
			// Token: 0x04001229 RID: 4649
			ReplayFailedToFindServerRpcVersionException,
			// Token: 0x0400122A RID: 4650
			AmBcsDatabaseCopyActivationSuspended,
			// Token: 0x0400122B RID: 4651
			IncrementalReseedRetryableException,
			// Token: 0x0400122C RID: 4652
			AmMountBlockedOnStandaloneDbWithMissingEdbException,
			// Token: 0x0400122D RID: 4653
			SeederInstanceAlreadyCompletedException,
			// Token: 0x0400122E RID: 4654
			CouldNotFindDagObjectLookupErrorForServer,
			// Token: 0x0400122F RID: 4655
			ServiceName,
			// Token: 0x04001230 RID: 4656
			AmClusterNodeNotFoundException,
			// Token: 0x04001231 RID: 4657
			AutoReseedFailedResumeBlocked,
			// Token: 0x04001232 RID: 4658
			SeederFailedToFindValidVolumeInfo,
			// Token: 0x04001233 RID: 4659
			SeederEcJtxAlreadyExist,
			// Token: 0x04001234 RID: 4660
			SeederFailedToDeleteDatabase,
			// Token: 0x04001235 RID: 4661
			DagTaskComponentManagerServerManagerCmdFailure,
			// Token: 0x04001236 RID: 4662
			FailedToGetDiskSpace,
			// Token: 0x04001237 RID: 4663
			DbRedundancyValidationErrorsOccurred,
			// Token: 0x04001238 RID: 4664
			AcllUnboundedDatalossDetectedException,
			// Token: 0x04001239 RID: 4665
			ClusterBatchWriter_BatchAddCommandFailed,
			// Token: 0x0400123A RID: 4666
			SeedingSourceReplicaInstanceNotFoundException,
			// Token: 0x0400123B RID: 4667
			AmBcsDatabaseCopySuspended,
			// Token: 0x0400123C RID: 4668
			FileCheckLogfileCreationTime,
			// Token: 0x0400123D RID: 4669
			ReplayLagManagerException,
			// Token: 0x0400123E RID: 4670
			DatabaseVolumeInfoInitException,
			// Token: 0x0400123F RID: 4671
			ReplayDbOperationTransientException,
			// Token: 0x04001240 RID: 4672
			VolumeFormatFailedException,
			// Token: 0x04001241 RID: 4673
			AutoReseedNoCopiesException,
			// Token: 0x04001242 RID: 4674
			ReplayDatabaseOperationCancelledException,
			// Token: 0x04001243 RID: 4675
			CiStatusIsFailed,
			// Token: 0x04001244 RID: 4676
			NetworkEndOfData,
			// Token: 0x04001245 RID: 4677
			DbAvailabilityActiveCopyDismountedError,
			// Token: 0x04001246 RID: 4678
			NetworkReadTimeout,
			// Token: 0x04001247 RID: 4679
			CiSeederSearchCatalogException,
			// Token: 0x04001248 RID: 4680
			TPRExchangeNotListening,
			// Token: 0x04001249 RID: 4681
			AmClusterEventNotifierTransientException,
			// Token: 0x0400124A RID: 4682
			MissingPassiveLogRequiredForDivergenceDetection,
			// Token: 0x0400124B RID: 4683
			AmBcsTargetServerPreferredMaxActivesExceeded,
			// Token: 0x0400124C RID: 4684
			AmClusterNodeJoinedException,
			// Token: 0x0400124D RID: 4685
			AmBcsTargetServerPreferredMaxActivesReached,
			// Token: 0x0400124E RID: 4686
			AmDbLockConflict,
			// Token: 0x0400124F RID: 4687
			FileCheck,
			// Token: 0x04001250 RID: 4688
			LogInspectorGenerationMismatch,
			// Token: 0x04001251 RID: 4689
			SeederInstanceNotFoundException,
			// Token: 0x04001252 RID: 4690
			FailedToOpenLogTruncContext,
			// Token: 0x04001253 RID: 4691
			DagTaskOperationFailedWithEcException,
			// Token: 0x04001254 RID: 4692
			DagTaskServerTransientException,
			// Token: 0x04001255 RID: 4693
			ReplayServiceSuspendRpcPartialSuccessCatalogFailedException,
			// Token: 0x04001256 RID: 4694
			PathIsAlreadyAValidMountPoint,
			// Token: 0x04001257 RID: 4695
			AmLastLogPropertyCorruptedException,
			// Token: 0x04001258 RID: 4696
			CouldNotFindDagObjectForServer,
			// Token: 0x04001259 RID: 4697
			TagHandlerFormatMsgFailed,
			// Token: 0x0400125A RID: 4698
			IncrementalReseedPrereqException,
			// Token: 0x0400125B RID: 4699
			MonitoredDatabaseInitFailure,
			// Token: 0x0400125C RID: 4700
			AmBcsTargetServerActivationBlocked,
			// Token: 0x0400125D RID: 4701
			RepairStateDatabaseCopyShouldBeSuspended,
			// Token: 0x0400125E RID: 4702
			InvalidRcrConfigAlreadyHostsDb,
			// Token: 0x0400125F RID: 4703
			RegistryParameterException,
			// Token: 0x04001260 RID: 4704
			FailedToDeserializeDumpsterRequestStrException,
			// Token: 0x04001261 RID: 4705
			SeederEcUndefined,
			// Token: 0x04001262 RID: 4706
			CiSeederGenericException,
			// Token: 0x04001263 RID: 4707
			FileCheckEDBMissing,
			// Token: 0x04001264 RID: 4708
			AcllCopyIsNotViableErrorException,
			// Token: 0x04001265 RID: 4709
			DatabaseVolumeInfoException,
			// Token: 0x04001266 RID: 4710
			ReplayServiceResumeBlockedException,
			// Token: 0x04001267 RID: 4711
			ReplaySystemOperationTimedoutException,
			// Token: 0x04001268 RID: 4712
			AmBcsSourceServerADError,
			// Token: 0x04001269 RID: 4713
			AmDbActionWrapperTransientException,
			// Token: 0x0400126A RID: 4714
			FileCheckAccessDenied,
			// Token: 0x0400126B RID: 4715
			ActiveRecoveryNotApplicableException,
			// Token: 0x0400126C RID: 4716
			SeederServerException,
			// Token: 0x0400126D RID: 4717
			AutoReseedCatalogIsBehindRetry,
			// Token: 0x0400126E RID: 4718
			AutoReseedWrongNumberOfCopiesOnVolume,
			// Token: 0x0400126F RID: 4719
			SourceLogBreakStallsPassiveError,
			// Token: 0x04001270 RID: 4720
			TagHandlerSuspendCopy,
			// Token: 0x04001271 RID: 4721
			TPRExchangeListenerNotResponding,
			// Token: 0x04001272 RID: 4722
			EnableReplayLagAlreadyDisabledFailedException,
			// Token: 0x04001273 RID: 4723
			IncSeedDivergenceCheckFailedException,
			// Token: 0x04001274 RID: 4724
			TPRInitFailure,
			// Token: 0x04001275 RID: 4725
			FileCheckRequiredGenerationCorrupt,
			// Token: 0x04001276 RID: 4726
			AmCommonTransientException,
			// Token: 0x04001277 RID: 4727
			ClusterBatchWriter_OpenClusterFailed,
			// Token: 0x04001278 RID: 4728
			CIStatusFailedException,
			// Token: 0x04001279 RID: 4729
			DumpsterSafetyNetRpcFailedException,
			// Token: 0x0400127A RID: 4730
			AmDbActionException,
			// Token: 0x0400127B RID: 4731
			InstanceSuspendedAutoInitialSeed,
			// Token: 0x0400127C RID: 4732
			ReseedCheckMissingLogfile,
			// Token: 0x0400127D RID: 4733
			LogCopierFailsBecauseLogGap,
			// Token: 0x0400127E RID: 4734
			LogDirectoryCreationDisabled,
			// Token: 0x0400127F RID: 4735
			DbAvailabilityActiveCopyMountState,
			// Token: 0x04001280 RID: 4736
			LogGapDetected,
			// Token: 0x04001281 RID: 4737
			FileCheckEseutilError,
			// Token: 0x04001282 RID: 4738
			ClusterBatchWriter_CreateBatchFailed,
			// Token: 0x04001283 RID: 4739
			AmRefreshConfigTimeoutError,
			// Token: 0x04001284 RID: 4740
			AmDbOperationException,
			// Token: 0x04001285 RID: 4741
			DbCopyNotTargetException,
			// Token: 0x04001286 RID: 4742
			LogRepairDivergenceCheckFailedDueToCorruptEndOfLog,
			// Token: 0x04001287 RID: 4743
			AmBcsDatabaseCopyHostedOnTarget,
			// Token: 0x04001288 RID: 4744
			SearchProxyRpcException,
			// Token: 0x04001289 RID: 4745
			AutoReseedCatalogIsBehindBacklog,
			// Token: 0x0400128A RID: 4746
			CiSeederCatalogCouldNotPause,
			// Token: 0x0400128B RID: 4747
			TPRChangeFailedBecauseAlreadyActive,
			// Token: 0x0400128C RID: 4748
			DatabaseValidationException,
			// Token: 0x0400128D RID: 4749
			FileCheckLogfileMissing,
			// Token: 0x0400128E RID: 4750
			FileCheckLogfileSignature,
			// Token: 0x0400128F RID: 4751
			FileCheckJustCreatedEDB,
			// Token: 0x04001290 RID: 4752
			AmDbOperationTimedoutException,
			// Token: 0x04001291 RID: 4753
			AmDbActionWrapperException,
			// Token: 0x04001292 RID: 4754
			UnexpectedEOF,
			// Token: 0x04001293 RID: 4755
			DbDriveNotBigEnough,
			// Token: 0x04001294 RID: 4756
			PagePatchApiFailedException,
			// Token: 0x04001295 RID: 4757
			AutoReseedCatalogToUpgrade,
			// Token: 0x04001296 RID: 4758
			RlmDatabaseCopyInvalidException,
			// Token: 0x04001297 RID: 4759
			LogRepairDivergenceCheckFailedError,
			// Token: 0x04001298 RID: 4760
			DbValidationActiveCopyStatusUnknown,
			// Token: 0x04001299 RID: 4761
			SeederReplayServiceDownException,
			// Token: 0x0400129A RID: 4762
			DbMoveSkippedBecauseNotActive,
			// Token: 0x0400129B RID: 4763
			LogInspectorSignatureMismatch,
			// Token: 0x0400129C RID: 4764
			GranularReplicationMsgSequenceError,
			// Token: 0x0400129D RID: 4765
			PagePatchTooManyPagesToPatchException,
			// Token: 0x0400129E RID: 4766
			LogRepairNotPossible,
			// Token: 0x0400129F RID: 4767
			AmRoleChangedWhileOperationIsInProgress,
			// Token: 0x040012A0 RID: 4768
			AmPreMountCallbackFailedNoReplicaInstanceException,
			// Token: 0x040012A1 RID: 4769
			DagTaskMovingPam,
			// Token: 0x040012A2 RID: 4770
			ReplayServiceSuspendRpcInvalidForActiveCopyException,
			// Token: 0x040012A3 RID: 4771
			ReplayServiceTooMuchMemoryException,
			// Token: 0x040012A4 RID: 4772
			LogInspectorCouldNotMoveLogFileException,
			// Token: 0x040012A5 RID: 4773
			ServerMoveAllDatabasesFailed,
			// Token: 0x040012A6 RID: 4774
			CouldNotMoveLogFile,
			// Token: 0x040012A7 RID: 4775
			DagTaskInstallingFailoverClustering,
			// Token: 0x040012A8 RID: 4776
			AmBcsDatabaseCopyIsHAComponentOffline,
			// Token: 0x040012A9 RID: 4777
			CopyStatusIsNotHealthy,
			// Token: 0x040012AA RID: 4778
			AmLastServerTimeStampCorruptedException,
			// Token: 0x040012AB RID: 4779
			PagePatchInvalidPageSizeException,
			// Token: 0x040012AC RID: 4780
			FileStateInternalError,
			// Token: 0x040012AD RID: 4781
			SeederRpcUnsupportedException,
			// Token: 0x040012AE RID: 4782
			WarningPerformingFastOperationException,
			// Token: 0x040012AF RID: 4783
			SeedInProgressException,
			// Token: 0x040012B0 RID: 4784
			AmMountTimeoutError,
			// Token: 0x040012B1 RID: 4785
			LastLogReplacementTempNewFileNotDeletedException,
			// Token: 0x040012B2 RID: 4786
			CiSeederExchangeSearchPermanentException,
			// Token: 0x040012B3 RID: 4787
			ReplayServiceTooManyHandlesException,
			// Token: 0x040012B4 RID: 4788
			AmBcsTargetServerIsStoppedOnDAC,
			// Token: 0x040012B5 RID: 4789
			DagTaskNetFtProblem,
			// Token: 0x040012B6 RID: 4790
			AmDbRemountSkippedSinceDatabaseWasAdminDismounted,
			// Token: 0x040012B7 RID: 4791
			DagTaskNotEnoughStaticIPAddresses,
			// Token: 0x040012B8 RID: 4792
			AmClusterException,
			// Token: 0x040012B9 RID: 4793
			SeedDivergenceFailedException,
			// Token: 0x040012BA RID: 4794
			SeederInstanceReseedBlockedException,
			// Token: 0x040012BB RID: 4795
			DagTaskComputerAccountCouldNotBeValidatedAccessDeniedException,
			// Token: 0x040012BC RID: 4796
			ReplayServiceSuspendRpcInvalidSeedingSourceException,
			// Token: 0x040012BD RID: 4797
			FailedToTruncateLocallyException,
			// Token: 0x040012BE RID: 4798
			ReplayDbOperationException,
			// Token: 0x040012BF RID: 4799
			AmDbActionRejectedAdminDismountedException,
			// Token: 0x040012C0 RID: 4800
			ExchangeVolumeInfoMultipleExMountPointsException,
			// Token: 0x040012C1 RID: 4801
			SeederInstanceInvalidStateForEndException,
			// Token: 0x040012C2 RID: 4802
			ServerNotFoundException,
			// Token: 0x040012C3 RID: 4803
			SeederServerTransientException,
			// Token: 0x040012C4 RID: 4804
			ReplayConfigPropException,
			// Token: 0x040012C5 RID: 4805
			PagePatchInvalidFileException,
			// Token: 0x040012C6 RID: 4806
			SeedPrepareException,
			// Token: 0x040012C7 RID: 4807
			FileCheckRequiredLogfileGapException,
			// Token: 0x040012C8 RID: 4808
			DeleteChkptReasonForce,
			// Token: 0x040012C9 RID: 4809
			DbValidationInspectorQueueLengthTooHigh,
			// Token: 0x040012CA RID: 4810
			AutoReseedPrereqFailedException,
			// Token: 0x040012CB RID: 4811
			CopyUnknownToActiveLogTruncationException,
			// Token: 0x040012CC RID: 4812
			AutoReseedFailedInPlaceReseedBlocked,
			// Token: 0x040012CD RID: 4813
			TPRChangeFailedServerValidation,
			// Token: 0x040012CE RID: 4814
			AmDbMoveSkippedSinceMasterChanged,
			// Token: 0x040012CF RID: 4815
			HungDetectionGumIdChanged,
			// Token: 0x040012D0 RID: 4816
			SeederInstanceAlreadyCancelledException,
			// Token: 0x040012D1 RID: 4817
			CancelSeedingDueToConfigChangeOrServiceShutdown,
			// Token: 0x040012D2 RID: 4818
			ClusterNetworkNullSubnetError,
			// Token: 0x040012D3 RID: 4819
			TPRProviderNotResponding,
			// Token: 0x040012D4 RID: 4820
			SpareConflictInLayoutException,
			// Token: 0x040012D5 RID: 4821
			ErrorDagMisconfiguredForAmConfig,
			// Token: 0x040012D6 RID: 4822
			AmBcsFailedToQueryCopiesException,
			// Token: 0x040012D7 RID: 4823
			ReplayServiceTooManyThreadsException,
			// Token: 0x040012D8 RID: 4824
			AmDbActionCancelledException,
			// Token: 0x040012D9 RID: 4825
			FileIOonSourceException,
			// Token: 0x040012DA RID: 4826
			FileOpenError,
			// Token: 0x040012DB RID: 4827
			SeederEcTargetDbFileInUse,
			// Token: 0x040012DC RID: 4828
			AutoReseedUnhandledException,
			// Token: 0x040012DD RID: 4829
			InvalidDbStateForIncReseed,
			// Token: 0x040012DE RID: 4830
			LastLogReplacementUnexpectedTempFilesException,
			// Token: 0x040012DF RID: 4831
			AmBcsDatabaseCopySeeding,
			// Token: 0x040012E0 RID: 4832
			AmInvalidDbState,
			// Token: 0x040012E1 RID: 4833
			AmBcsGetCopyStatusRpcException,
			// Token: 0x040012E2 RID: 4834
			AmDbMoveOperationSkippedException,
			// Token: 0x040012E3 RID: 4835
			SeederFailedToFindDirectory,
			// Token: 0x040012E4 RID: 4836
			NetworkUnexpectedMessage,
			// Token: 0x040012E5 RID: 4837
			ClusterBatchWriter_OpenActiveManagerKeyFailed,
			// Token: 0x040012E6 RID: 4838
			SeederRpcSafeDeleteUnsupportedException,
			// Token: 0x040012E7 RID: 4839
			AutoReseedNotAllCopiesOnVolumeFailedSuspended,
			// Token: 0x040012E8 RID: 4840
			LastLogReplacementFailedFileNotFoundException,
			// Token: 0x040012E9 RID: 4841
			DatabaseNotHealthyOnVolume,
			// Token: 0x040012EA RID: 4842
			DagTaskComputerAccountCouldNotBeValidatedException,
			// Token: 0x040012EB RID: 4843
			AmDbActionRejectedLastAdminActionDidNotSucceedException,
			// Token: 0x040012EC RID: 4844
			AcllInvalidForActiveCopyException,
			// Token: 0x040012ED RID: 4845
			DumpsterCouldNotReadMaxDumpsterTimeException,
			// Token: 0x040012EE RID: 4846
			AmPreMountCallbackFailedNoReplicaInstanceErrorException,
			// Token: 0x040012EF RID: 4847
			MonitoringCouldNotFindDagException,
			// Token: 0x040012F0 RID: 4848
			AcllAlreadyRunningException,
			// Token: 0x040012F1 RID: 4849
			AcllCopyIsNotViableException,
			// Token: 0x040012F2 RID: 4850
			DatabaseFailoverFailedException,
			// Token: 0x040012F3 RID: 4851
			VolumeRecentlyModifiedException,
			// Token: 0x040012F4 RID: 4852
			DagTaskQuorumNotAchievedException,
			// Token: 0x040012F5 RID: 4853
			OperationTimeoutErr,
			// Token: 0x040012F6 RID: 4854
			DagTaskFormingClusterToLog,
			// Token: 0x040012F7 RID: 4855
			ErrorNullServerFromDb,
			// Token: 0x040012F8 RID: 4856
			TPREnabledInvalidOperation,
			// Token: 0x040012F9 RID: 4857
			AcllSetCurrentLogGenerationException,
			// Token: 0x040012FA RID: 4858
			FailedToReadDatabasePage,
			// Token: 0x040012FB RID: 4859
			DagTaskRemoteOperationLogData,
			// Token: 0x040012FC RID: 4860
			AmBcsTargetServerMaxActivesReached,
			// Token: 0x040012FD RID: 4861
			NetworkCorruptData,
			// Token: 0x040012FE RID: 4862
			LogInspectorFailedGeneral,
			// Token: 0x040012FF RID: 4863
			ReplayServiceRpcArgumentException,
			// Token: 0x04001300 RID: 4864
			AcllFailedException,
			// Token: 0x04001301 RID: 4865
			NetworkRemoteErrorUnknown,
			// Token: 0x04001302 RID: 4866
			FileCheckCorruptFile,
			// Token: 0x04001303 RID: 4867
			DagTaskFileShareWitnessResourceIsStillNotOnlineException,
			// Token: 0x04001304 RID: 4868
			DeleteChkptReasonTooAdvanced,
			// Token: 0x04001305 RID: 4869
			AmDbOperationAttempedTooSoonException,
			// Token: 0x04001306 RID: 4870
			AmTransientException,
			// Token: 0x04001307 RID: 4871
			AmGetServiceProcessFailed,
			// Token: 0x04001308 RID: 4872
			SourceDatabaseNotFound,
			// Token: 0x04001309 RID: 4873
			DatabaseRemountFailedException,
			// Token: 0x0400130A RID: 4874
			LogRepairUnexpectedVerifyError,
			// Token: 0x0400130B RID: 4875
			EseLogEnumeratorIOError,
			// Token: 0x0400130C RID: 4876
			UnExpectedPageSize,
			// Token: 0x0400130D RID: 4877
			ErrorCouldNotConnectNativeClusterForAmConfig,
			// Token: 0x0400130E RID: 4878
			DbHTFirstLookupTimeoutException,
			// Token: 0x0400130F RID: 4879
			AmBcsTargetNodeDownError,
			// Token: 0x04001310 RID: 4880
			RegistryParameterWriteException,
			// Token: 0x04001311 RID: 4881
			DbAvailabilityValidationErrorsOccurred,
			// Token: 0x04001312 RID: 4882
			MonitoringCouldNotFindMiniServerException,
			// Token: 0x04001313 RID: 4883
			ReplayDbOperationWrapperException,
			// Token: 0x04001314 RID: 4884
			FailedToFindLocalServerException,
			// Token: 0x04001315 RID: 4885
			CallWithoutnumberOfExtraCopiesOnSparesException,
			// Token: 0x04001316 RID: 4886
			AmBcsTargetServerADError,
			// Token: 0x04001317 RID: 4887
			ReplaySystemOperationCancelledException,
			// Token: 0x04001318 RID: 4888
			ServiceNotRunningOnNodeException,
			// Token: 0x04001319 RID: 4889
			LogFileCheckError,
			// Token: 0x0400131A RID: 4890
			ServerIsNotJoinedYet,
			// Token: 0x0400131B RID: 4891
			LastLogGenerationTimeStampStale,
			// Token: 0x0400131C RID: 4892
			DbValidationDbNotReplicated,
			// Token: 0x0400131D RID: 4893
			ReplayDbOperationWrapperTransientException,
			// Token: 0x0400131E RID: 4894
			ReplayStoreOperationAbortedException,
			// Token: 0x0400131F RID: 4895
			DBCNotSuspendedYet,
			// Token: 0x04001320 RID: 4896
			AutoReseedThrottledException,
			// Token: 0x04001321 RID: 4897
			DbValidationActiveCopyStatusRpcFailed,
			// Token: 0x04001322 RID: 4898
			InvalidVolumeMissingException,
			// Token: 0x04001323 RID: 4899
			DagTaskJoinedNodeToCluster,
			// Token: 0x04001324 RID: 4900
			CouldNotGetMountStatusError,
			// Token: 0x04001325 RID: 4901
			LastLogReplacementException,
			// Token: 0x04001326 RID: 4902
			DatabasesMissingInCopyStatusLookUpTable,
			// Token: 0x04001327 RID: 4903
			IncSeedConfigNotSupportedError,
			// Token: 0x04001328 RID: 4904
			SeederFailedToDeleteLogs,
			// Token: 0x04001329 RID: 4905
			ReplayServiceResumeRpcInvalidForActiveCopyException,
			// Token: 0x0400132A RID: 4906
			TPRmmediateDismountFailed,
			// Token: 0x0400132B RID: 4907
			RepairStateError,
			// Token: 0x0400132C RID: 4908
			CannotBeginSeedingInstanceNotInStateException,
			// Token: 0x0400132D RID: 4909
			RepairStateDatabaseNotReplicated,
			// Token: 0x0400132E RID: 4910
			MonitoringADFirstLookupTimeoutException,
			// Token: 0x0400132F RID: 4911
			AmBcsManagedAvailabilityCheckFailed,
			// Token: 0x04001330 RID: 4912
			ReplayServiceSyncStateInvalidDuringMoveException,
			// Token: 0x04001331 RID: 4913
			DatabasePageSizeUnexpected,
			// Token: 0x04001332 RID: 4914
			AmBcsDatabaseCopyQueueLengthTooHigh,
			// Token: 0x04001333 RID: 4915
			DagTaskFswNeedsCnoPermissionException,
			// Token: 0x04001334 RID: 4916
			ReplayServiceResumeRpcPartialSuccessCatalogFailedException,
			// Token: 0x04001335 RID: 4917
			ReplayServiceCouldNotFindReplayConfigException,
			// Token: 0x04001336 RID: 4918
			SeederCopyNotSuspended,
			// Token: 0x04001337 RID: 4919
			DagTaskFormingClusterProgress,
			// Token: 0x04001338 RID: 4920
			NetworkConnectionTimeout,
			// Token: 0x04001339 RID: 4921
			DbFixupFailedException,
			// Token: 0x0400133A RID: 4922
			AmPreMountCallbackFailedException,
			// Token: 0x0400133B RID: 4923
			AmBcsSingleCopyValidationException,
			// Token: 0x0400133C RID: 4924
			AutoReseedCatalogSourceException,
			// Token: 0x0400133D RID: 4925
			AmMountBlockedDbMountedBeforeWithMissingEdbException,
			// Token: 0x0400133E RID: 4926
			AutoReseedInvalidEdbFolderPath,
			// Token: 0x0400133F RID: 4927
			MissingLogRequired,
			// Token: 0x04001340 RID: 4928
			SeederFailedToDeployDatabase,
			// Token: 0x04001341 RID: 4929
			PreferFullReseed,
			// Token: 0x04001342 RID: 4930
			DagReplayServiceDownException,
			// Token: 0x04001343 RID: 4931
			AutoReseedFailedResumeRetryExceeded,
			// Token: 0x04001344 RID: 4932
			AcllTempLogCreationFailedException,
			// Token: 0x04001345 RID: 4933
			NetworkCommunicationError,
			// Token: 0x04001346 RID: 4934
			SetBrokenWatsonException,
			// Token: 0x04001347 RID: 4935
			LogRepairRetryCountExceeded,
			// Token: 0x04001348 RID: 4936
			FileCheckLogfileGeneration,
			// Token: 0x04001349 RID: 4937
			FailedToGetProcessForServiceException,
			// Token: 0x0400134A RID: 4938
			DatabasesMissingInADException,
			// Token: 0x0400134B RID: 4939
			VolumeNotSafeForFormatException,
			// Token: 0x0400134C RID: 4940
			AmFailedToReadClusdbException,
			// Token: 0x0400134D RID: 4941
			AcllLossDeterminationFailedException,
			// Token: 0x0400134E RID: 4942
			DatabaseHealthTrackerException,
			// Token: 0x0400134F RID: 4943
			DagTaskJoiningNodeToCluster,
			// Token: 0x04001350 RID: 4944
			LastLogReplacementFailedErrorException,
			// Token: 0x04001351 RID: 4945
			DagTaskRemoteOperationLogBegin,
			// Token: 0x04001352 RID: 4946
			AmBcsDatabaseCopyReplayQueueLengthTooHigh,
			// Token: 0x04001353 RID: 4947
			CorruptLogDetectedError,
			// Token: 0x04001354 RID: 4948
			SafeDeleteExistingFilesDataRedundancyException,
			// Token: 0x04001355 RID: 4949
			DumpsterInvalidResubmitRequestException,
			// Token: 0x04001356 RID: 4950
			FileCheckIsamError,
			// Token: 0x04001357 RID: 4951
			AmClusterNoServerToConnect,
			// Token: 0x04001358 RID: 4952
			DatabaseValidationNoCopiesException,
			// Token: 0x04001359 RID: 4953
			ClusterBatchWriter_OpenCopyStateKeyFailed,
			// Token: 0x0400135A RID: 4954
			FileCheckIOError,
			// Token: 0x0400135B RID: 4955
			CouldNotFindSpareVolumeException,
			// Token: 0x0400135C RID: 4956
			FileReadException,
			// Token: 0x0400135D RID: 4957
			ReplayServiceResumeRpcInvalidForSingleCopyException,
			// Token: 0x0400135E RID: 4958
			DbValidationCopyStatusRpcFailed,
			// Token: 0x0400135F RID: 4959
			AmClusterFileNotFoundException,
			// Token: 0x04001360 RID: 4960
			SeedingAnotherServerException,
			// Token: 0x04001361 RID: 4961
			DagTaskOperationFailedException,
			// Token: 0x04001362 RID: 4962
			AmDbMoveOperationNotSupportedStandaloneException,
			// Token: 0x04001363 RID: 4963
			AutoReseedNotAllCopiesPassive,
			// Token: 0x04001364 RID: 4964
			MonitoringCouldNotFindHubServersException,
			// Token: 0x04001365 RID: 4965
			FailedToFindDatabaseException,
			// Token: 0x04001366 RID: 4966
			DbValidationCopyStatusTooOld,
			// Token: 0x04001367 RID: 4967
			CouldNotCreateDbDirectoriesException,
			// Token: 0x04001368 RID: 4968
			DatabaseCopyLayoutException,
			// Token: 0x04001369 RID: 4969
			DatabaseLogCorruptRecoveryFailed,
			// Token: 0x0400136A RID: 4970
			MonitoringADLookupTimeoutException,
			// Token: 0x0400136B RID: 4971
			SeederEcDirDoesNotExist,
			// Token: 0x0400136C RID: 4972
			FailedToKillProcessForServiceException,
			// Token: 0x0400136D RID: 4973
			JetErrorFileIOBeyondEOFException,
			// Token: 0x0400136E RID: 4974
			DatabaseCopySuspendException,
			// Token: 0x0400136F RID: 4975
			DatabaseNotMounted,
			// Token: 0x04001370 RID: 4976
			ExchangeVolumeInfoInitException,
			// Token: 0x04001371 RID: 4977
			CopyPageFailed,
			// Token: 0x04001372 RID: 4978
			ReplayTestStoreConnectivityTimedoutException,
			// Token: 0x04001373 RID: 4979
			ReplayServiceSuspendRpcInvalidForSingleCopyException,
			// Token: 0x04001374 RID: 4980
			MonitoringADConfigException,
			// Token: 0x04001375 RID: 4981
			LogRepairFailedToCopyFromActive,
			// Token: 0x04001376 RID: 4982
			DagTaskValidateNodeTimedOutException,
			// Token: 0x04001377 RID: 4983
			SeederEcStoreNotOnline,
			// Token: 0x04001378 RID: 4984
			AmRegistryException,
			// Token: 0x04001379 RID: 4985
			ReplayServiceSuspendInvalidDuringMoveException,
			// Token: 0x0400137A RID: 4986
			ReplayServiceRestartInvalidDuringMoveException,
			// Token: 0x0400137B RID: 4987
			ClusterBatchWriter_OpenClusterRootKeyFailed,
			// Token: 0x0400137C RID: 4988
			AmBcsException,
			// Token: 0x0400137D RID: 4989
			FileSharingViolationOnSourceException,
			// Token: 0x0400137E RID: 4990
			NetworkRemoteError,
			// Token: 0x0400137F RID: 4991
			DbVolumeInvalidDirectoriesException,
			// Token: 0x04001380 RID: 4992
			LogInspectorFailed,
			// Token: 0x04001381 RID: 4993
			AmBcsTargetServerActivationIntraSite,
			// Token: 0x04001382 RID: 4994
			AmBcsTargetNodeDebugOptionEnabled,
			// Token: 0x04001383 RID: 4995
			GranularReplicationTerminated,
			// Token: 0x04001384 RID: 4996
			AutoReseedException,
			// Token: 0x04001385 RID: 4997
			FailedToGetDatabaseInfo,
			// Token: 0x04001386 RID: 4998
			CiSeederSearchCatalogRpcTransientException,
			// Token: 0x04001387 RID: 4999
			AutoReseedFailedReseedBlocked,
			// Token: 0x04001388 RID: 5000
			FileCheckInternalError
		}
	}
}
