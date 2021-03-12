using System;
using System.Runtime.InteropServices;

namespace Microsoft.Exchange.Rpc.Cluster
{
	// Token: 0x0200012E RID: 302
	[Serializable]
	internal sealed class RpcDatabaseCopyStatus2 : RpcDatabaseCopyStatusBasic
	{
		// Token: 0x0600071D RID: 1821 RVA: 0x000187EC File Offset: 0x00017BEC
		public RpcDatabaseCopyStatus2(RpcDatabaseCopyStatus s)
		{
			Guid dbguid = s.DBGuid;
			this.m_dbGuid = dbguid;
			this.m_mailboxServer = s.MailboxServer;
			this.m_activeDatabaseCopy = s.ActiveDatabaseCopy;
			DateTime lastInspectedLogTime = s.LastInspectedLogTime;
			this.m_dataProtectionTime = lastInspectedLogTime;
			DateTime lastReplayedLogTime = s.LastReplayedLogTime;
			this.m_dataAvailabilityTime = lastReplayedLogTime;
			this.m_lastLogGenerated = s.LastLogGenerated;
			this.m_lastLogCopied = s.LastLogCopied;
			this.m_lastLogInspected = s.LastLogInspected;
			this.m_lastLogReplayed = s.LastLogReplayed;
			this.m_serverVersion = s.ServerVersion;
			this.m_copyStatus = s.CopyStatus;
			this.m_ciCurrentness = s.CICurrentness;
			DateTime statusRetrievedTime = s.StatusRetrievedTime;
			this.m_statusRetrievedTime = statusRetrievedTime;
			if (!<Module>.?A0x3667cccd.IsValidDateTime(this.m_statusRetrievedTime))
			{
				DateTime utcNow = DateTime.UtcNow;
				this.m_statusRetrievedTime = utcNow;
			}
			this.m_lastLogGeneratedTime = this.m_statusRetrievedTime;
			this.m_suspendComment = s.SuspendComment;
			this.m_dumpsterServers = s.DumpsterServers;
			this.m_actionInitiator = s.ActionInitiator;
			this.m_seedingSource = s.SeedingSource;
			this.m_dumpsterRequired = s.DumpsterRequired;
			DateTime dumpsterStartTime = s.DumpsterStartTime;
			this.m_dumpsterStartTime = dumpsterStartTime;
			DateTime dumpsterEndTime = s.DumpsterEndTime;
			this.m_dumpsterEndTime = dumpsterEndTime;
			DateTime latestAvailableLogTime = s.LatestAvailableLogTime;
			this.m_latestAvailableLogTime = latestAvailableLogTime;
			DateTime lastCopyNotifiedLogTime = s.LastCopyNotifiedLogTime;
			this.m_lastCopyNotifiedLogTime = lastCopyNotifiedLogTime;
			DateTime lastCopiedLogTime = s.LastCopiedLogTime;
			this.m_lastCopiedLogTime = lastCopiedLogTime;
			DateTime lastInspectedLogTime2 = s.LastInspectedLogTime;
			this.m_lastInspectedLogTime = lastInspectedLogTime2;
			DateTime lastReplayedLogTime2 = s.LastReplayedLogTime;
			this.m_lastReplayedLogTime = lastReplayedLogTime2;
			DateTime currentReplayLogTime = s.CurrentReplayLogTime;
			this.m_currentReplayLogTime = currentReplayLogTime;
			this.m_lastLogCopyNotified = s.LastLogCopyNotified;
			DateTime latestFullBackupTime = s.LatestFullBackupTime;
			this.m_latestFullBackupTime = latestFullBackupTime;
			DateTime latestIncrementalBackupTime = s.LatestIncrementalBackupTime;
			this.m_latestIncrementalBackupTime = latestIncrementalBackupTime;
			DateTime latestDifferentialBackupTime = s.LatestDifferentialBackupTime;
			this.m_latestDifferentialBackupTime = latestDifferentialBackupTime;
			DateTime latestCopyBackupTime = s.LatestCopyBackupTime;
			this.m_latestCopyBackupTime = latestCopyBackupTime;
			this.m_snapshotLatestFullBackup = s.SnapshotLatestFullBackup;
			this.m_snapshotLatestIncrementalBackup = s.SnapshotLatestIncrementalBackup;
			this.m_snapshotLatestDifferentialBackup = s.SnapshotLatestDifferentialBackup;
			this.m_snapshotLatestCopyBackup = s.SnapshotLatestCopyBackup;
			this.m_copyQueueNotKeepingUp = s.CopyQueueNotKeepingUp;
			this.m_replayQueueNotKeepingUp = s.ReplayQueueNotKeepingUp;
			this.m_contentIndexStatus = s.ContentIndexStatus;
			this.m_contentIndexErrorMessage = s.ContentIndexErrorMessage;
			this.m_activationSuspended = s.ActivationSuspended;
			this.m_singlePageRestore = s.SinglePageRestore;
			this.m_singlePageRestoreNumber = s.SinglePageRestoreNumber;
			this.m_viable = s.Viable;
			this.m_lostWrite = s.LostWrite;
			this.m_outgoingConnections = s.OutgoingConnections;
			this.m_incomingLogCopyingNetwork = s.IncomingLogCopyingNetwork;
			this.m_seedingNetwork = s.SeedingNetwork;
			this.m_errorMessage = s.ErrorMessage;
			this.m_errorEventId = s.ErrorEventId;
			this.m_extendedErrorInfo = s.ExtendedErrorInfo;
			this.m_logsReplayedSinceInstanceStart = s.LogsReplayedSinceInstanceStart;
			this.m_logsCopiedSinceInstanceStart = s.LogsCopiedSinceInstanceStart;
		}

		// Token: 0x0600071E RID: 1822 RVA: 0x00005C40 File Offset: 0x00005040
		public RpcDatabaseCopyStatus2()
		{
		}

		// Token: 0x0600071F RID: 1823 RVA: 0x00018ADC File Offset: 0x00017EDC
		[return: MarshalAs(UnmanagedType.U1)]
		public static bool operator ==(RpcDatabaseCopyStatus2 left, RpcDatabaseCopyStatus2 right)
		{
			return object.ReferenceEquals(left, right) || (left != null && right != null && (left == right && <Module>.StringEqual(left.m_suspendComment, right.m_suspendComment) && <Module>.StringEqual(left.m_dumpsterServers, right.m_dumpsterServers) && left.m_instanceStartTime == right.m_instanceStartTime && left.m_lastStatusTransitionTime == right.m_lastStatusTransitionTime && left.m_dumpsterRequired == right.m_dumpsterRequired && left.m_dumpsterStartTime == right.m_dumpsterStartTime && left.m_dumpsterEndTime == right.m_dumpsterEndTime && left.m_latestAvailableLogTime == right.m_latestAvailableLogTime && left.m_lastCopyNotifiedLogTime == right.m_lastCopyNotifiedLogTime && left.m_lastCopiedLogTime == right.m_lastCopiedLogTime && left.m_lastReplayedLogTime == right.m_lastReplayedLogTime && left.m_lastInspectedLogTime == right.m_lastInspectedLogTime && left.m_currentReplayLogTime == right.m_currentReplayLogTime && left.m_lastLogGenerated == right.m_lastLogGenerated && left.m_lastLogCopyNotified == right.m_lastLogCopyNotified && left.m_latestFullBackupTime == right.m_latestFullBackupTime && left.m_latestIncrementalBackupTime == right.m_latestIncrementalBackupTime && left.m_latestDifferentialBackupTime == right.m_latestDifferentialBackupTime && left.m_latestCopyBackupTime == right.m_latestCopyBackupTime && left.m_snapshotLatestFullBackup == right.m_snapshotLatestFullBackup && left.m_snapshotLatestIncrementalBackup == right.m_snapshotLatestIncrementalBackup && left.m_snapshotLatestDifferentialBackup == right.m_snapshotLatestDifferentialBackup && left.m_snapshotLatestCopyBackup == right.m_snapshotLatestCopyBackup && left.m_copyQueueNotKeepingUp == right.m_copyQueueNotKeepingUp && left.m_replayQueueNotKeepingUp == right.m_replayQueueNotKeepingUp && left.m_viable == right.m_viable && left.m_isReplaySuspended == right.m_isReplaySuspended && left.m_resumeBlocked == right.m_resumeBlocked && left.m_reseedBlocked == right.m_reseedBlocked && left.m_workerProcessId == right.m_workerProcessId && left.m_nodeStatus == right.m_nodeStatus && left.m_configuredReplayLagTime == right.m_configuredReplayLagTime && left.m_actualReplayLagTime == right.m_actualReplayLagTime && left.m_replayLagEnabled == right.m_replayLagEnabled && left.m_replayLagPlayDownReason == right.m_replayLagPlayDownReason && left.m_replayLagPercentage == right.m_replayLagPercentage && left.m_singlePageRestore == right.m_singlePageRestore && left.m_singlePageRestoreNumber == right.m_singlePageRestoreNumber && left.m_activationSuspended == right.m_activationSuspended && left.m_lostWrite == right.m_lostWrite && left.m_contentIndexStatus == right.m_contentIndexStatus && <Module>.StringEqual(left.m_contentIndexErrorMessage, right.m_contentIndexErrorMessage) && Nullable.Equals<int>(left.m_contentIndexErrorCode, right.m_contentIndexErrorCode) && Nullable.Equals<int>(left.m_contentIndexVersion, right.m_contentIndexVersion) && Nullable.Equals<int>(left.m_contentIndexBacklog, right.m_contentIndexBacklog) && Nullable.Equals<int>(left.m_contentIndexRetryQueueSize, right.m_contentIndexRetryQueueSize) && Nullable.Equals<int>(left.m_contentIndexMailboxesToCrawl, right.m_contentIndexMailboxesToCrawl) && Nullable.Equals<int>(left.m_contentIndexSeedingPercent, right.m_contentIndexSeedingPercent) && <Module>.StringEqual(left.m_contentIndexSeedingSource, right.m_contentIndexSeedingSource) && left.m_dbSeedingPercent == right.m_dbSeedingPercent && left.m_dbSeedingKBytesRead == right.m_dbSeedingKBytesRead && left.m_dbSeedingKBytesWritten == right.m_dbSeedingKBytesWritten && (double)left.m_dbSeedingKBytesReadPerSec == (double)right.m_dbSeedingKBytesReadPerSec && (double)left.m_dbSeedingKBytesWrittenPerSec == (double)right.m_dbSeedingKBytesWrittenPerSec && left.m_actionInitiator == right.m_actionInitiator && left.m_seedingSource == right.m_seedingSource && <Module>.StringEqual(left.m_errorMessage, right.m_errorMessage) && left.m_errorEventId == right.m_errorEventId && left.m_extendedErrorInfo == right.m_extendedErrorInfo && left.m_logsReplayedSinceInstanceStart == right.m_logsReplayedSinceInstanceStart && left.m_logsCopiedSinceInstanceStart == right.m_logsCopiedSinceInstanceStart && left.m_activationPreference == right.m_activationPreference && left.m_diskFreeSpaceBytes == right.m_diskFreeSpaceBytes && left.m_diskTotalSpaceBytes == right.m_diskTotalSpaceBytes && left.m_diskFreeSpacePercent == right.m_diskFreeSpacePercent && left.m_lastDatabaseVolumeNameTransitionTime == right.m_lastDatabaseVolumeNameTransitionTime && <Module>.StringEqual(left.m_lastDatabaseVolumeName, right.m_lastDatabaseVolumeName) && <Module>.StringEqual(left.m_exchangeVolumeMountPoint, right.m_exchangeVolumeMountPoint) && <Module>.StringEqual(left.m_databaseVolumeMountPoint, right.m_databaseVolumeMountPoint) && <Module>.StringEqual(left.m_databaseVolumeName, right.m_databaseVolumeName) && left.m_isDbOnMountedFolder == right.m_isDbOnMountedFolder && <Module>.StringEqual(left.m_logVolumeMountPoint, right.m_logVolumeMountPoint) && <Module>.StringEqual(left.m_logVolumeName, right.m_logVolumeName) && left.m_isLogOnMountedFolder == right.m_isLogOnMountedFolder && <Module>.StringEqual(left.m_volumeInfoLastError, right.m_volumeInfoLastError)));
		}

		// Token: 0x06000720 RID: 1824 RVA: 0x00005C54 File Offset: 0x00005054
		[return: MarshalAs(UnmanagedType.U1)]
		public static bool operator !=(RpcDatabaseCopyStatus2 left, RpcDatabaseCopyStatus2 right)
		{
			return ((!(left == right)) ? 1 : 0) != 0;
		}

		// Token: 0x06000721 RID: 1825 RVA: 0x00005C6C File Offset: 0x0000506C
		[return: MarshalAs(UnmanagedType.U1)]
		public static bool IsOn(uint statusWord, uint bitsToTest)
		{
			return (((statusWord & bitsToTest) == bitsToTest) ? 1 : 0) != 0;
		}

		// Token: 0x06000722 RID: 1826 RVA: 0x00005C80 File Offset: 0x00005080
		public static void TurnOn(ref uint statusWord, uint bitsToSet)
		{
			statusWord |= bitsToSet;
		}

		// Token: 0x06000723 RID: 1827 RVA: 0x00005C94 File Offset: 0x00005094
		public static void TurnOff(ref uint statusWord, uint bitsToClear)
		{
			statusWord &= ~bitsToClear;
		}

		// Token: 0x06000724 RID: 1828 RVA: 0x00005CA8 File Offset: 0x000050A8
		public void SetOrClearStatusBits(RpcDatabaseCopyStatus2.StatusBitsMask bitsToChange, [MarshalAs(UnmanagedType.U1)] bool turnBitsOn)
		{
			if (turnBitsOn)
			{
				this.m_statusBits |= (uint)bitsToChange;
			}
			else
			{
				this.m_statusBits &= (uint)(~(uint)bitsToChange);
			}
		}

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x06000725 RID: 1829 RVA: 0x00005CD8 File Offset: 0x000050D8
		// (set) Token: 0x06000726 RID: 1830 RVA: 0x00005CF0 File Offset: 0x000050F0
		public bool ReplicationIsInBlockMode
		{
			[return: MarshalAs(UnmanagedType.U1)]
			get
			{
				return (byte)(this.m_statusBits & 1U) != 0;
			}
			[param: MarshalAs(UnmanagedType.U1)]
			set
			{
				if (value)
				{
					this.m_statusBits |= 1U;
				}
				else
				{
					this.m_statusBits &= 4294967294U;
				}
			}
		}

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x06000727 RID: 1831 RVA: 0x00005D20 File Offset: 0x00005120
		// (set) Token: 0x06000728 RID: 1832 RVA: 0x00005D38 File Offset: 0x00005138
		public bool ActivationDisabledAndMoveNow
		{
			[return: MarshalAs(UnmanagedType.U1)]
			get
			{
				return (byte)(this.m_statusBits >> 1 & 1U) != 0;
			}
			[param: MarshalAs(UnmanagedType.U1)]
			set
			{
				if (value)
				{
					this.m_statusBits |= 2U;
				}
				else
				{
					this.m_statusBits &= 4294967293U;
				}
			}
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x06000729 RID: 1833 RVA: 0x00005D68 File Offset: 0x00005168
		// (set) Token: 0x0600072A RID: 1834 RVA: 0x00005D80 File Offset: 0x00005180
		public bool HAComponentOffline
		{
			[return: MarshalAs(UnmanagedType.U1)]
			get
			{
				return (byte)(this.m_statusBits >> 2 & 1U) != 0;
			}
			[param: MarshalAs(UnmanagedType.U1)]
			set
			{
				if (value)
				{
					this.m_statusBits |= 4U;
				}
				else
				{
					this.m_statusBits &= 4294967291U;
				}
			}
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x0600072B RID: 1835 RVA: 0x00005DB0 File Offset: 0x000051B0
		// (set) Token: 0x0600072C RID: 1836 RVA: 0x00005DC8 File Offset: 0x000051C8
		public bool IsPrimaryActiveManager
		{
			[return: MarshalAs(UnmanagedType.U1)]
			get
			{
				return (byte)(this.m_statusBits >> 3 & 1U) != 0;
			}
			[param: MarshalAs(UnmanagedType.U1)]
			set
			{
				if (value)
				{
					this.m_statusBits |= 8U;
				}
				else
				{
					this.m_statusBits &= 4294967287U;
				}
			}
		}

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x0600072D RID: 1837 RVA: 0x00005DF8 File Offset: 0x000051F8
		// (set) Token: 0x0600072E RID: 1838 RVA: 0x00005E0C File Offset: 0x0000520C
		public int AutoActivationPolicy
		{
			get
			{
				return this.m_serverAutoActivationPolicy;
			}
			set
			{
				this.m_serverAutoActivationPolicy = value;
			}
		}

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x0600072F RID: 1839 RVA: 0x00005E20 File Offset: 0x00005220
		// (set) Token: 0x06000730 RID: 1840 RVA: 0x00005E38 File Offset: 0x00005238
		public DateTime InstanceStartTime
		{
			get
			{
				return this.m_instanceStartTime;
			}
			set
			{
				this.m_instanceStartTime = value;
			}
		}

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x06000731 RID: 1841 RVA: 0x00005E4C File Offset: 0x0000524C
		// (set) Token: 0x06000732 RID: 1842 RVA: 0x00005E64 File Offset: 0x00005264
		public DateTime LastStatusTransitionTime
		{
			get
			{
				return this.m_lastStatusTransitionTime;
			}
			set
			{
				this.m_lastStatusTransitionTime = value;
			}
		}

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x06000733 RID: 1843 RVA: 0x00005E78 File Offset: 0x00005278
		// (set) Token: 0x06000734 RID: 1844 RVA: 0x00005E90 File Offset: 0x00005290
		public DateTime LastCopyAvailabilityChecksPassedTime
		{
			get
			{
				return this.m_lastCopyAvailabilityChecksPassedTime;
			}
			set
			{
				this.m_lastCopyAvailabilityChecksPassedTime = value;
			}
		}

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x06000735 RID: 1845 RVA: 0x00005EA4 File Offset: 0x000052A4
		// (set) Token: 0x06000736 RID: 1846 RVA: 0x00005EBC File Offset: 0x000052BC
		public DateTime LastCopyRedundancyChecksPassedTime
		{
			get
			{
				return this.m_lastCopyRedundancyChecksPassedTime;
			}
			set
			{
				this.m_lastCopyRedundancyChecksPassedTime = value;
			}
		}

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x06000737 RID: 1847 RVA: 0x00005ED0 File Offset: 0x000052D0
		// (set) Token: 0x06000738 RID: 1848 RVA: 0x00005EE4 File Offset: 0x000052E4
		public bool IsLastCopyAvailabilityChecksPassed
		{
			[return: MarshalAs(UnmanagedType.U1)]
			get
			{
				return this.m_isLastCopyAvailabilityChecksPassed;
			}
			[param: MarshalAs(UnmanagedType.U1)]
			set
			{
				this.m_isLastCopyAvailabilityChecksPassed = value;
			}
		}

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x06000739 RID: 1849 RVA: 0x00005EF8 File Offset: 0x000052F8
		// (set) Token: 0x0600073A RID: 1850 RVA: 0x00005F0C File Offset: 0x0000530C
		public bool IsLastCopyRedundancyChecksPassed
		{
			[return: MarshalAs(UnmanagedType.U1)]
			get
			{
				return this.m_isLastCopyRedundancyChecksPassed;
			}
			[param: MarshalAs(UnmanagedType.U1)]
			set
			{
				this.m_isLastCopyRedundancyChecksPassed = value;
			}
		}

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x0600073B RID: 1851 RVA: 0x00005F20 File Offset: 0x00005320
		// (set) Token: 0x0600073C RID: 1852 RVA: 0x00005F34 File Offset: 0x00005334
		public ContentIndexStatusType ContentIndexStatus
		{
			get
			{
				return this.m_contentIndexStatus;
			}
			set
			{
				this.m_contentIndexStatus = value;
			}
		}

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x0600073D RID: 1853 RVA: 0x00005F48 File Offset: 0x00005348
		// (set) Token: 0x0600073E RID: 1854 RVA: 0x00005F5C File Offset: 0x0000535C
		public string ContentIndexErrorMessage
		{
			get
			{
				return this.m_contentIndexErrorMessage;
			}
			set
			{
				this.m_contentIndexErrorMessage = value;
			}
		}

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x0600073F RID: 1855 RVA: 0x00005F70 File Offset: 0x00005370
		// (set) Token: 0x06000740 RID: 1856 RVA: 0x00005F88 File Offset: 0x00005388
		public int? ContentIndexBacklog
		{
			get
			{
				return this.m_contentIndexBacklog;
			}
			set
			{
				this.m_contentIndexBacklog = value;
			}
		}

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x06000741 RID: 1857 RVA: 0x00005F9C File Offset: 0x0000539C
		// (set) Token: 0x06000742 RID: 1858 RVA: 0x00005FB4 File Offset: 0x000053B4
		public int? ContentIndexRetryQueueSize
		{
			get
			{
				return this.m_contentIndexRetryQueueSize;
			}
			set
			{
				this.m_contentIndexRetryQueueSize = value;
			}
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x06000743 RID: 1859 RVA: 0x00005FC8 File Offset: 0x000053C8
		// (set) Token: 0x06000744 RID: 1860 RVA: 0x00005FE0 File Offset: 0x000053E0
		public int? ContentIndexMailboxesToCrawl
		{
			get
			{
				return this.m_contentIndexMailboxesToCrawl;
			}
			set
			{
				this.m_contentIndexMailboxesToCrawl = value;
			}
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x06000745 RID: 1861 RVA: 0x00005FF4 File Offset: 0x000053F4
		// (set) Token: 0x06000746 RID: 1862 RVA: 0x0000600C File Offset: 0x0000540C
		public int? ContentIndexSeedingPercent
		{
			get
			{
				return this.m_contentIndexSeedingPercent;
			}
			set
			{
				this.m_contentIndexSeedingPercent = value;
			}
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x06000747 RID: 1863 RVA: 0x00006020 File Offset: 0x00005420
		// (set) Token: 0x06000748 RID: 1864 RVA: 0x00006034 File Offset: 0x00005434
		public string ContentIndexSeedingSource
		{
			get
			{
				return this.m_contentIndexSeedingSource;
			}
			set
			{
				this.m_contentIndexSeedingSource = value;
			}
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x06000749 RID: 1865 RVA: 0x00006048 File Offset: 0x00005448
		// (set) Token: 0x0600074A RID: 1866 RVA: 0x00006060 File Offset: 0x00005460
		public int? ContentIndexVersion
		{
			get
			{
				return this.m_contentIndexVersion;
			}
			set
			{
				this.m_contentIndexVersion = value;
			}
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x0600074B RID: 1867 RVA: 0x00006074 File Offset: 0x00005474
		// (set) Token: 0x0600074C RID: 1868 RVA: 0x0000608C File Offset: 0x0000548C
		public int? ContentIndexErrorCode
		{
			get
			{
				return this.m_contentIndexErrorCode;
			}
			set
			{
				this.m_contentIndexErrorCode = value;
			}
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x0600074D RID: 1869 RVA: 0x000060A0 File Offset: 0x000054A0
		// (set) Token: 0x0600074E RID: 1870 RVA: 0x000060B4 File Offset: 0x000054B4
		public int DbSeedingPercent
		{
			get
			{
				return this.m_dbSeedingPercent;
			}
			set
			{
				this.m_dbSeedingPercent = value;
			}
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x0600074F RID: 1871 RVA: 0x000060C8 File Offset: 0x000054C8
		// (set) Token: 0x06000750 RID: 1872 RVA: 0x000060DC File Offset: 0x000054DC
		public long DbSeedingKBytesRead
		{
			get
			{
				return this.m_dbSeedingKBytesRead;
			}
			set
			{
				this.m_dbSeedingKBytesRead = value;
			}
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x06000751 RID: 1873 RVA: 0x000060F0 File Offset: 0x000054F0
		// (set) Token: 0x06000752 RID: 1874 RVA: 0x00006104 File Offset: 0x00005504
		public long DbSeedingKBytesWritten
		{
			get
			{
				return this.m_dbSeedingKBytesWritten;
			}
			set
			{
				this.m_dbSeedingKBytesWritten = value;
			}
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x06000753 RID: 1875 RVA: 0x00006118 File Offset: 0x00005518
		// (set) Token: 0x06000754 RID: 1876 RVA: 0x0000612C File Offset: 0x0000552C
		public float DbSeedingKBytesReadPerSec
		{
			get
			{
				return this.m_dbSeedingKBytesReadPerSec;
			}
			set
			{
				this.m_dbSeedingKBytesReadPerSec = value;
			}
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x06000755 RID: 1877 RVA: 0x00006140 File Offset: 0x00005540
		// (set) Token: 0x06000756 RID: 1878 RVA: 0x00006154 File Offset: 0x00005554
		public float DbSeedingKBytesWrittenPerSec
		{
			get
			{
				return this.m_dbSeedingKBytesWrittenPerSec;
			}
			set
			{
				this.m_dbSeedingKBytesWrittenPerSec = value;
			}
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x06000757 RID: 1879 RVA: 0x00006168 File Offset: 0x00005568
		// (set) Token: 0x06000758 RID: 1880 RVA: 0x0000617C File Offset: 0x0000557C
		public bool ActivationSuspended
		{
			[return: MarshalAs(UnmanagedType.U1)]
			get
			{
				return this.m_activationSuspended;
			}
			[param: MarshalAs(UnmanagedType.U1)]
			set
			{
				this.m_activationSuspended = value;
			}
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x06000759 RID: 1881 RVA: 0x00006190 File Offset: 0x00005590
		// (set) Token: 0x0600075A RID: 1882 RVA: 0x000061A4 File Offset: 0x000055A4
		public bool Viable
		{
			[return: MarshalAs(UnmanagedType.U1)]
			get
			{
				return this.m_viable;
			}
			[param: MarshalAs(UnmanagedType.U1)]
			set
			{
				this.m_viable = value;
			}
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x0600075B RID: 1883 RVA: 0x000061B8 File Offset: 0x000055B8
		// (set) Token: 0x0600075C RID: 1884 RVA: 0x000061CC File Offset: 0x000055CC
		public bool LostWrite
		{
			[return: MarshalAs(UnmanagedType.U1)]
			get
			{
				return this.m_lostWrite;
			}
			[param: MarshalAs(UnmanagedType.U1)]
			set
			{
				this.m_lostWrite = value;
			}
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x0600075D RID: 1885 RVA: 0x000061E0 File Offset: 0x000055E0
		// (set) Token: 0x0600075E RID: 1886 RVA: 0x000061F4 File Offset: 0x000055F4
		public bool ReplaySuspended
		{
			[return: MarshalAs(UnmanagedType.U1)]
			get
			{
				return this.m_isReplaySuspended;
			}
			[param: MarshalAs(UnmanagedType.U1)]
			set
			{
				this.m_isReplaySuspended = value;
			}
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x0600075F RID: 1887 RVA: 0x00006208 File Offset: 0x00005608
		// (set) Token: 0x06000760 RID: 1888 RVA: 0x0000621C File Offset: 0x0000561C
		public bool ResumeBlocked
		{
			[return: MarshalAs(UnmanagedType.U1)]
			get
			{
				return this.m_resumeBlocked;
			}
			[param: MarshalAs(UnmanagedType.U1)]
			set
			{
				this.m_resumeBlocked = value;
			}
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x06000761 RID: 1889 RVA: 0x00006230 File Offset: 0x00005630
		// (set) Token: 0x06000762 RID: 1890 RVA: 0x00006244 File Offset: 0x00005644
		public bool ReseedBlocked
		{
			[return: MarshalAs(UnmanagedType.U1)]
			get
			{
				return this.m_reseedBlocked;
			}
			[param: MarshalAs(UnmanagedType.U1)]
			set
			{
				this.m_reseedBlocked = value;
			}
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x06000763 RID: 1891 RVA: 0x00006258 File Offset: 0x00005658
		// (set) Token: 0x06000764 RID: 1892 RVA: 0x0000626C File Offset: 0x0000566C
		public bool InPlaceReseedBlocked
		{
			[return: MarshalAs(UnmanagedType.U1)]
			get
			{
				return this.m_inPlaceReseedBlocked;
			}
			[param: MarshalAs(UnmanagedType.U1)]
			set
			{
				this.m_inPlaceReseedBlocked = value;
			}
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x06000765 RID: 1893 RVA: 0x00006280 File Offset: 0x00005680
		// (set) Token: 0x06000766 RID: 1894 RVA: 0x00006294 File Offset: 0x00005694
		public int WorkerProcessId
		{
			get
			{
				return this.m_workerProcessId;
			}
			set
			{
				this.m_workerProcessId = value;
			}
		}

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x06000767 RID: 1895 RVA: 0x000062A8 File Offset: 0x000056A8
		// (set) Token: 0x06000768 RID: 1896 RVA: 0x000062BC File Offset: 0x000056BC
		public NodeUpStatusEnum NodeStatus
		{
			get
			{
				return this.m_nodeStatus;
			}
			set
			{
				this.m_nodeStatus = value;
			}
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x06000769 RID: 1897 RVA: 0x000062D0 File Offset: 0x000056D0
		// (set) Token: 0x0600076A RID: 1898 RVA: 0x000062E8 File Offset: 0x000056E8
		public TimeSpan ConfiguredReplayLagTime
		{
			get
			{
				return this.m_configuredReplayLagTime;
			}
			set
			{
				this.m_configuredReplayLagTime = value;
			}
		}

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x0600076B RID: 1899 RVA: 0x000062FC File Offset: 0x000056FC
		// (set) Token: 0x0600076C RID: 1900 RVA: 0x00006314 File Offset: 0x00005714
		public TimeSpan ActualReplayLagTime
		{
			get
			{
				return this.m_actualReplayLagTime;
			}
			set
			{
				this.m_actualReplayLagTime = value;
			}
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x0600076D RID: 1901 RVA: 0x00006328 File Offset: 0x00005728
		// (set) Token: 0x0600076E RID: 1902 RVA: 0x0000633C File Offset: 0x0000573C
		public string ReplayLagDisabledReason
		{
			get
			{
				return this.m_replayLagDisabledReason;
			}
			set
			{
				this.m_replayLagDisabledReason = value;
			}
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x0600076F RID: 1903 RVA: 0x00006350 File Offset: 0x00005750
		// (set) Token: 0x06000770 RID: 1904 RVA: 0x00006364 File Offset: 0x00005764
		public ReplayLagEnabledEnum ReplayLagEnabled
		{
			get
			{
				return this.m_replayLagEnabled;
			}
			set
			{
				this.m_replayLagEnabled = value;
			}
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x06000771 RID: 1905 RVA: 0x00006378 File Offset: 0x00005778
		// (set) Token: 0x06000772 RID: 1906 RVA: 0x0000638C File Offset: 0x0000578C
		public ReplayLagPlayDownReasonEnum ReplayLagPlayDownReason
		{
			get
			{
				return this.m_replayLagPlayDownReason;
			}
			set
			{
				this.m_replayLagPlayDownReason = value;
			}
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x06000773 RID: 1907 RVA: 0x000063A0 File Offset: 0x000057A0
		// (set) Token: 0x06000774 RID: 1908 RVA: 0x000063B4 File Offset: 0x000057B4
		public int ReplayLagPercentage
		{
			get
			{
				return this.m_replayLagPercentage;
			}
			set
			{
				this.m_replayLagPercentage = value;
			}
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x06000775 RID: 1909 RVA: 0x000063C8 File Offset: 0x000057C8
		// (set) Token: 0x06000776 RID: 1910 RVA: 0x000063DC File Offset: 0x000057DC
		public bool SinglePageRestore
		{
			[return: MarshalAs(UnmanagedType.U1)]
			get
			{
				return this.m_singlePageRestore;
			}
			[param: MarshalAs(UnmanagedType.U1)]
			set
			{
				this.m_singlePageRestore = value;
			}
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x06000777 RID: 1911 RVA: 0x000063F0 File Offset: 0x000057F0
		// (set) Token: 0x06000778 RID: 1912 RVA: 0x00006404 File Offset: 0x00005804
		public long SinglePageRestoreNumber
		{
			get
			{
				return this.m_singlePageRestoreNumber;
			}
			set
			{
				this.m_singlePageRestoreNumber = value;
			}
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x06000779 RID: 1913 RVA: 0x00006418 File Offset: 0x00005818
		// (set) Token: 0x0600077A RID: 1914 RVA: 0x0000642C File Offset: 0x0000582C
		public string SuspendComment
		{
			get
			{
				return this.m_suspendComment;
			}
			set
			{
				this.m_suspendComment = value;
			}
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x0600077B RID: 1915 RVA: 0x00006440 File Offset: 0x00005840
		// (set) Token: 0x0600077C RID: 1916 RVA: 0x00006454 File Offset: 0x00005854
		public ActionInitiatorType ActionInitiator
		{
			get
			{
				return this.m_actionInitiator;
			}
			set
			{
				this.m_actionInitiator = value;
			}
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x0600077D RID: 1917 RVA: 0x00006468 File Offset: 0x00005868
		// (set) Token: 0x0600077E RID: 1918 RVA: 0x0000647C File Offset: 0x0000587C
		public string DumpsterServers
		{
			get
			{
				return this.m_dumpsterServers;
			}
			set
			{
				this.m_dumpsterServers = value;
			}
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x0600077F RID: 1919 RVA: 0x00006490 File Offset: 0x00005890
		// (set) Token: 0x06000780 RID: 1920 RVA: 0x000064A4 File Offset: 0x000058A4
		public bool DumpsterRequired
		{
			[return: MarshalAs(UnmanagedType.U1)]
			get
			{
				return this.m_dumpsterRequired;
			}
			[param: MarshalAs(UnmanagedType.U1)]
			set
			{
				this.m_dumpsterRequired = value;
			}
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x06000781 RID: 1921 RVA: 0x000064B8 File Offset: 0x000058B8
		// (set) Token: 0x06000782 RID: 1922 RVA: 0x000064D0 File Offset: 0x000058D0
		public DateTime DumpsterStartTime
		{
			get
			{
				return this.m_dumpsterStartTime;
			}
			set
			{
				this.m_dumpsterStartTime = value;
			}
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x06000783 RID: 1923 RVA: 0x000064E4 File Offset: 0x000058E4
		// (set) Token: 0x06000784 RID: 1924 RVA: 0x000064FC File Offset: 0x000058FC
		public DateTime DumpsterEndTime
		{
			get
			{
				return this.m_dumpsterEndTime;
			}
			set
			{
				this.m_dumpsterEndTime = value;
			}
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x06000785 RID: 1925 RVA: 0x00006510 File Offset: 0x00005910
		// (set) Token: 0x06000786 RID: 1926 RVA: 0x00006528 File Offset: 0x00005928
		public DateTime LatestAvailableLogTime
		{
			get
			{
				return this.m_latestAvailableLogTime;
			}
			set
			{
				this.m_latestAvailableLogTime = value;
			}
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x06000787 RID: 1927 RVA: 0x0000653C File Offset: 0x0000593C
		// (set) Token: 0x06000788 RID: 1928 RVA: 0x00006554 File Offset: 0x00005954
		public DateTime LastCopyNotifiedLogTime
		{
			get
			{
				return this.m_lastCopyNotifiedLogTime;
			}
			set
			{
				this.m_lastCopyNotifiedLogTime = value;
			}
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x06000789 RID: 1929 RVA: 0x00006568 File Offset: 0x00005968
		// (set) Token: 0x0600078A RID: 1930 RVA: 0x0000657C File Offset: 0x0000597C
		public bool LastLogInfoIsStale
		{
			[return: MarshalAs(UnmanagedType.U1)]
			get
			{
				return this.m_lastLogInfoIsStale;
			}
			[param: MarshalAs(UnmanagedType.U1)]
			set
			{
				this.m_lastLogInfoIsStale = value;
			}
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x0600078B RID: 1931 RVA: 0x00006590 File Offset: 0x00005990
		// (set) Token: 0x0600078C RID: 1932 RVA: 0x000065A8 File Offset: 0x000059A8
		public DateTime LastLogInfoFromCopierTime
		{
			get
			{
				return this.m_lastLogInfoFromCopierTime;
			}
			set
			{
				this.m_lastLogInfoFromCopierTime = value;
			}
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x0600078D RID: 1933 RVA: 0x000065BC File Offset: 0x000059BC
		// (set) Token: 0x0600078E RID: 1934 RVA: 0x000065D4 File Offset: 0x000059D4
		public DateTime LastLogInfoFromClusterTime
		{
			get
			{
				return this.m_lastLogInfoFromClusterTime;
			}
			set
			{
				this.m_lastLogInfoFromClusterTime = value;
			}
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x0600078F RID: 1935 RVA: 0x000065E8 File Offset: 0x000059E8
		// (set) Token: 0x06000790 RID: 1936 RVA: 0x000065FC File Offset: 0x000059FC
		public long LastLogInfoFromClusterGen
		{
			get
			{
				return this.m_lastLogInfoFromClusterGen;
			}
			set
			{
				this.m_lastLogInfoFromClusterGen = value;
			}
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x06000791 RID: 1937 RVA: 0x00006610 File Offset: 0x00005A10
		// (set) Token: 0x06000792 RID: 1938 RVA: 0x00006628 File Offset: 0x00005A28
		public DateTime LastCopiedLogTime
		{
			get
			{
				return this.m_lastCopiedLogTime;
			}
			set
			{
				this.m_lastCopiedLogTime = value;
			}
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x06000793 RID: 1939 RVA: 0x0000663C File Offset: 0x00005A3C
		// (set) Token: 0x06000794 RID: 1940 RVA: 0x00006654 File Offset: 0x00005A54
		public DateTime LastInspectedLogTime
		{
			get
			{
				return this.m_lastInspectedLogTime;
			}
			set
			{
				this.m_lastInspectedLogTime = value;
			}
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x06000795 RID: 1941 RVA: 0x00006668 File Offset: 0x00005A68
		// (set) Token: 0x06000796 RID: 1942 RVA: 0x00006680 File Offset: 0x00005A80
		public DateTime LastReplayedLogTime
		{
			get
			{
				return this.m_lastReplayedLogTime;
			}
			set
			{
				this.m_lastReplayedLogTime = value;
			}
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x06000797 RID: 1943 RVA: 0x00006694 File Offset: 0x00005A94
		// (set) Token: 0x06000798 RID: 1944 RVA: 0x000066AC File Offset: 0x00005AAC
		public DateTime CurrentReplayLogTime
		{
			get
			{
				return this.m_currentReplayLogTime;
			}
			set
			{
				this.m_currentReplayLogTime = value;
			}
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x06000799 RID: 1945 RVA: 0x000066C0 File Offset: 0x00005AC0
		// (set) Token: 0x0600079A RID: 1946 RVA: 0x000066D4 File Offset: 0x00005AD4
		public long LastLogCopyNotified
		{
			get
			{
				return this.m_lastLogCopyNotified;
			}
			set
			{
				this.m_lastLogCopyNotified = value;
			}
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x0600079B RID: 1947 RVA: 0x000066E8 File Offset: 0x00005AE8
		// (set) Token: 0x0600079C RID: 1948 RVA: 0x00006700 File Offset: 0x00005B00
		public DateTime LatestFullBackupTime
		{
			get
			{
				return this.m_latestFullBackupTime;
			}
			set
			{
				this.m_latestFullBackupTime = value;
			}
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x0600079D RID: 1949 RVA: 0x00006714 File Offset: 0x00005B14
		// (set) Token: 0x0600079E RID: 1950 RVA: 0x0000672C File Offset: 0x00005B2C
		public DateTime LatestIncrementalBackupTime
		{
			get
			{
				return this.m_latestIncrementalBackupTime;
			}
			set
			{
				this.m_latestIncrementalBackupTime = value;
			}
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x0600079F RID: 1951 RVA: 0x00006740 File Offset: 0x00005B40
		// (set) Token: 0x060007A0 RID: 1952 RVA: 0x00006758 File Offset: 0x00005B58
		public DateTime LatestDifferentialBackupTime
		{
			get
			{
				return this.m_latestDifferentialBackupTime;
			}
			set
			{
				this.m_latestDifferentialBackupTime = value;
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x060007A1 RID: 1953 RVA: 0x0000676C File Offset: 0x00005B6C
		// (set) Token: 0x060007A2 RID: 1954 RVA: 0x00006784 File Offset: 0x00005B84
		public DateTime LatestCopyBackupTime
		{
			get
			{
				return this.m_latestCopyBackupTime;
			}
			set
			{
				this.m_latestCopyBackupTime = value;
			}
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x060007A3 RID: 1955 RVA: 0x00006798 File Offset: 0x00005B98
		// (set) Token: 0x060007A4 RID: 1956 RVA: 0x000067AC File Offset: 0x00005BAC
		public bool SnapshotLatestFullBackup
		{
			[return: MarshalAs(UnmanagedType.U1)]
			get
			{
				return this.m_snapshotLatestFullBackup;
			}
			[param: MarshalAs(UnmanagedType.U1)]
			set
			{
				this.m_snapshotLatestFullBackup = value;
			}
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x060007A5 RID: 1957 RVA: 0x000067C0 File Offset: 0x00005BC0
		// (set) Token: 0x060007A6 RID: 1958 RVA: 0x000067D4 File Offset: 0x00005BD4
		public bool SnapshotLatestIncrementalBackup
		{
			[return: MarshalAs(UnmanagedType.U1)]
			get
			{
				return this.m_snapshotLatestIncrementalBackup;
			}
			[param: MarshalAs(UnmanagedType.U1)]
			set
			{
				this.m_snapshotLatestIncrementalBackup = value;
			}
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x060007A7 RID: 1959 RVA: 0x000067E8 File Offset: 0x00005BE8
		// (set) Token: 0x060007A8 RID: 1960 RVA: 0x000067FC File Offset: 0x00005BFC
		public bool SnapshotLatestDifferentialBackup
		{
			[return: MarshalAs(UnmanagedType.U1)]
			get
			{
				return this.m_snapshotLatestDifferentialBackup;
			}
			[param: MarshalAs(UnmanagedType.U1)]
			set
			{
				this.m_snapshotLatestDifferentialBackup = value;
			}
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x060007A9 RID: 1961 RVA: 0x00006810 File Offset: 0x00005C10
		// (set) Token: 0x060007AA RID: 1962 RVA: 0x00006824 File Offset: 0x00005C24
		public bool SnapshotLatestCopyBackup
		{
			[return: MarshalAs(UnmanagedType.U1)]
			get
			{
				return this.m_snapshotLatestCopyBackup;
			}
			[param: MarshalAs(UnmanagedType.U1)]
			set
			{
				this.m_snapshotLatestCopyBackup = value;
			}
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x060007AB RID: 1963 RVA: 0x00006838 File Offset: 0x00005C38
		// (set) Token: 0x060007AC RID: 1964 RVA: 0x0000684C File Offset: 0x00005C4C
		public bool CopyQueueNotKeepingUp
		{
			[return: MarshalAs(UnmanagedType.U1)]
			get
			{
				return this.m_copyQueueNotKeepingUp;
			}
			[param: MarshalAs(UnmanagedType.U1)]
			set
			{
				this.m_copyQueueNotKeepingUp = value;
			}
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x060007AD RID: 1965 RVA: 0x00006860 File Offset: 0x00005C60
		// (set) Token: 0x060007AE RID: 1966 RVA: 0x00006874 File Offset: 0x00005C74
		public bool ReplayQueueNotKeepingUp
		{
			[return: MarshalAs(UnmanagedType.U1)]
			get
			{
				return this.m_replayQueueNotKeepingUp;
			}
			[param: MarshalAs(UnmanagedType.U1)]
			set
			{
				this.m_replayQueueNotKeepingUp = value;
			}
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x060007AF RID: 1967 RVA: 0x00006888 File Offset: 0x00005C88
		// (set) Token: 0x060007B0 RID: 1968 RVA: 0x0000689C File Offset: 0x00005C9C
		public string ErrorMessage
		{
			get
			{
				return this.m_errorMessage;
			}
			set
			{
				this.m_errorMessage = value;
			}
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x060007B1 RID: 1969 RVA: 0x000068B0 File Offset: 0x00005CB0
		// (set) Token: 0x060007B2 RID: 1970 RVA: 0x000068C4 File Offset: 0x00005CC4
		public uint ErrorEventId
		{
			get
			{
				return this.m_errorEventId;
			}
			set
			{
				this.m_errorEventId = value;
			}
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x060007B3 RID: 1971 RVA: 0x000068D8 File Offset: 0x00005CD8
		// (set) Token: 0x060007B4 RID: 1972 RVA: 0x000068EC File Offset: 0x00005CEC
		public ExtendedErrorInfo ExtendedErrorInfo
		{
			get
			{
				return this.m_extendedErrorInfo;
			}
			set
			{
				this.m_extendedErrorInfo = value;
				this.m_extendedErrorInfoBytes = SerializationServices.Serialize(value);
			}
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x060007B5 RID: 1973 RVA: 0x0000690C File Offset: 0x00005D0C
		// (set) Token: 0x060007B6 RID: 1974 RVA: 0x00006920 File Offset: 0x00005D20
		public long LogsReplayedSinceInstanceStart
		{
			get
			{
				return this.m_logsReplayedSinceInstanceStart;
			}
			set
			{
				this.m_logsReplayedSinceInstanceStart = value;
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x060007B7 RID: 1975 RVA: 0x00006934 File Offset: 0x00005D34
		// (set) Token: 0x060007B8 RID: 1976 RVA: 0x00006948 File Offset: 0x00005D48
		public long LogsCopiedSinceInstanceStart
		{
			get
			{
				return this.m_logsCopiedSinceInstanceStart;
			}
			set
			{
				this.m_logsCopiedSinceInstanceStart = value;
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x060007B9 RID: 1977 RVA: 0x0000695C File Offset: 0x00005D5C
		// (set) Token: 0x060007BA RID: 1978 RVA: 0x00006970 File Offset: 0x00005D70
		public int ActivationPreference
		{
			get
			{
				return this.m_activationPreference;
			}
			set
			{
				this.m_activationPreference = value;
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x060007BB RID: 1979 RVA: 0x00006984 File Offset: 0x00005D84
		// (set) Token: 0x060007BC RID: 1980 RVA: 0x00006998 File Offset: 0x00005D98
		public bool SeedingSource
		{
			[return: MarshalAs(UnmanagedType.U1)]
			get
			{
				return this.m_seedingSource;
			}
			[param: MarshalAs(UnmanagedType.U1)]
			set
			{
				this.m_seedingSource = value;
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x060007BD RID: 1981 RVA: 0x000069AC File Offset: 0x00005DAC
		// (set) Token: 0x060007BE RID: 1982 RVA: 0x000069C0 File Offset: 0x00005DC0
		public bool SeedingSourceForDB
		{
			[return: MarshalAs(UnmanagedType.U1)]
			get
			{
				return this.m_seedingSourceForDB;
			}
			[param: MarshalAs(UnmanagedType.U1)]
			set
			{
				this.m_seedingSourceForDB = value;
			}
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x060007BF RID: 1983 RVA: 0x000069D4 File Offset: 0x00005DD4
		// (set) Token: 0x060007C0 RID: 1984 RVA: 0x000069E8 File Offset: 0x00005DE8
		public bool SeedingSourceForCI
		{
			[return: MarshalAs(UnmanagedType.U1)]
			get
			{
				return this.m_seedingSourceForCI;
			}
			[param: MarshalAs(UnmanagedType.U1)]
			set
			{
				this.m_seedingSourceForCI = value;
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x060007C1 RID: 1985 RVA: 0x000069FC File Offset: 0x00005DFC
		// (set) Token: 0x060007C2 RID: 1986 RVA: 0x00006A10 File Offset: 0x00005E10
		public ulong DiskFreeSpaceBytes
		{
			get
			{
				return this.m_diskFreeSpaceBytes;
			}
			set
			{
				this.m_diskFreeSpaceBytes = value;
			}
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x060007C3 RID: 1987 RVA: 0x00006A24 File Offset: 0x00005E24
		// (set) Token: 0x060007C4 RID: 1988 RVA: 0x00006A38 File Offset: 0x00005E38
		public ulong DiskTotalSpaceBytes
		{
			get
			{
				return this.m_diskTotalSpaceBytes;
			}
			set
			{
				this.m_diskTotalSpaceBytes = value;
			}
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x060007C5 RID: 1989 RVA: 0x00006A4C File Offset: 0x00005E4C
		// (set) Token: 0x060007C6 RID: 1990 RVA: 0x00006A60 File Offset: 0x00005E60
		public int DiskFreeSpacePercent
		{
			get
			{
				return this.m_diskFreeSpacePercent;
			}
			set
			{
				this.m_diskFreeSpacePercent = value;
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x060007C7 RID: 1991 RVA: 0x00006A74 File Offset: 0x00005E74
		// (set) Token: 0x060007C8 RID: 1992 RVA: 0x00006A88 File Offset: 0x00005E88
		public string LastDatabaseVolumeName
		{
			get
			{
				return this.m_lastDatabaseVolumeName;
			}
			set
			{
				this.m_lastDatabaseVolumeName = value;
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x060007C9 RID: 1993 RVA: 0x00006A9C File Offset: 0x00005E9C
		// (set) Token: 0x060007CA RID: 1994 RVA: 0x00006AB4 File Offset: 0x00005EB4
		public DateTime LastDatabaseVolumeNameTransitionTime
		{
			get
			{
				return this.m_lastDatabaseVolumeNameTransitionTime;
			}
			set
			{
				this.m_lastDatabaseVolumeNameTransitionTime = value;
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x060007CB RID: 1995 RVA: 0x00006AC8 File Offset: 0x00005EC8
		// (set) Token: 0x060007CC RID: 1996 RVA: 0x00006ADC File Offset: 0x00005EDC
		public string ExchangeVolumeMountPoint
		{
			get
			{
				return this.m_exchangeVolumeMountPoint;
			}
			set
			{
				this.m_exchangeVolumeMountPoint = value;
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x060007CD RID: 1997 RVA: 0x00006AF0 File Offset: 0x00005EF0
		// (set) Token: 0x060007CE RID: 1998 RVA: 0x00006B04 File Offset: 0x00005F04
		public string DatabaseVolumeMountPoint
		{
			get
			{
				return this.m_databaseVolumeMountPoint;
			}
			set
			{
				this.m_databaseVolumeMountPoint = value;
			}
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x060007CF RID: 1999 RVA: 0x00006B18 File Offset: 0x00005F18
		// (set) Token: 0x060007D0 RID: 2000 RVA: 0x00006B2C File Offset: 0x00005F2C
		public string DatabaseVolumeName
		{
			get
			{
				return this.m_databaseVolumeName;
			}
			set
			{
				this.m_databaseVolumeName = value;
			}
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x060007D1 RID: 2001 RVA: 0x00006B40 File Offset: 0x00005F40
		// (set) Token: 0x060007D2 RID: 2002 RVA: 0x00006B54 File Offset: 0x00005F54
		public bool DatabasePathIsOnMountedFolder
		{
			[return: MarshalAs(UnmanagedType.U1)]
			get
			{
				return this.m_isDbOnMountedFolder;
			}
			[param: MarshalAs(UnmanagedType.U1)]
			set
			{
				this.m_isDbOnMountedFolder = value;
			}
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x060007D3 RID: 2003 RVA: 0x00006B68 File Offset: 0x00005F68
		// (set) Token: 0x060007D4 RID: 2004 RVA: 0x00006B7C File Offset: 0x00005F7C
		public string LogVolumeMountPoint
		{
			get
			{
				return this.m_logVolumeMountPoint;
			}
			set
			{
				this.m_logVolumeMountPoint = value;
			}
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x060007D5 RID: 2005 RVA: 0x00006B90 File Offset: 0x00005F90
		// (set) Token: 0x060007D6 RID: 2006 RVA: 0x00006BA4 File Offset: 0x00005FA4
		public string LogVolumeName
		{
			get
			{
				return this.m_logVolumeName;
			}
			set
			{
				this.m_logVolumeName = value;
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x060007D7 RID: 2007 RVA: 0x00006BB8 File Offset: 0x00005FB8
		// (set) Token: 0x060007D8 RID: 2008 RVA: 0x00006BCC File Offset: 0x00005FCC
		public bool LogPathIsOnMountedFolder
		{
			[return: MarshalAs(UnmanagedType.U1)]
			get
			{
				return this.m_isLogOnMountedFolder;
			}
			[param: MarshalAs(UnmanagedType.U1)]
			set
			{
				this.m_isLogOnMountedFolder = value;
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x060007D9 RID: 2009 RVA: 0x00006BE0 File Offset: 0x00005FE0
		// (set) Token: 0x060007DA RID: 2010 RVA: 0x00006BF4 File Offset: 0x00005FF4
		public string VolumeInfoLastError
		{
			get
			{
				return this.m_volumeInfoLastError;
			}
			set
			{
				this.m_volumeInfoLastError = value;
			}
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x060007DB RID: 2011 RVA: 0x00006C08 File Offset: 0x00006008
		// (set) Token: 0x060007DC RID: 2012 RVA: 0x00006C1C File Offset: 0x0000601C
		public int MinimumSupportedDatabaseSchemaVersion
		{
			get
			{
				return this.m_minSupportedSchemaVersion;
			}
			set
			{
				this.m_minSupportedSchemaVersion = value;
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x060007DD RID: 2013 RVA: 0x00006C30 File Offset: 0x00006030
		// (set) Token: 0x060007DE RID: 2014 RVA: 0x00006C44 File Offset: 0x00006044
		public int MaximumSupportedDatabaseSchemaVersion
		{
			get
			{
				return this.m_maxSupportedSchemaVersion;
			}
			set
			{
				this.m_maxSupportedSchemaVersion = value;
			}
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x060007DF RID: 2015 RVA: 0x00006C58 File Offset: 0x00006058
		// (set) Token: 0x060007E0 RID: 2016 RVA: 0x00006C6C File Offset: 0x0000606C
		public int RequestedDatabaseSchemaVersion
		{
			get
			{
				return this.m_requestedSchemaVersion;
			}
			set
			{
				this.m_requestedSchemaVersion = value;
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x060007E1 RID: 2017 RVA: 0x00006C80 File Offset: 0x00006080
		// (set) Token: 0x060007E2 RID: 2018 RVA: 0x00006C94 File Offset: 0x00006094
		public long LowestLogPresent
		{
			get
			{
				return this.m_lowestLogPresent;
			}
			set
			{
				this.m_lowestLogPresent = value;
			}
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x060007E3 RID: 2019 RVA: 0x00006CA8 File Offset: 0x000060A8
		// (set) Token: 0x060007E4 RID: 2020 RVA: 0x00006CBC File Offset: 0x000060BC
		public long MaxLogToReplay
		{
			get
			{
				return this.m_maxLogToReplay;
			}
			set
			{
				this.m_maxLogToReplay = value;
			}
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x060007E5 RID: 2021 RVA: 0x00006CD0 File Offset: 0x000060D0
		// (set) Token: 0x060007E6 RID: 2022 RVA: 0x00006CE4 File Offset: 0x000060E4
		internal byte[] OutgoingConnections
		{
			get
			{
				return this.m_outgoingConnections;
			}
			set
			{
				this.m_outgoingConnections = value;
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x060007E7 RID: 2023 RVA: 0x00006CF8 File Offset: 0x000060F8
		// (set) Token: 0x060007E8 RID: 2024 RVA: 0x00006D0C File Offset: 0x0000610C
		internal byte[] IncomingLogCopyingNetwork
		{
			get
			{
				return this.m_incomingLogCopyingNetwork;
			}
			set
			{
				this.m_incomingLogCopyingNetwork = value;
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x060007E9 RID: 2025 RVA: 0x00006D20 File Offset: 0x00006120
		// (set) Token: 0x060007EA RID: 2026 RVA: 0x00006D34 File Offset: 0x00006134
		internal byte[] SeedingNetwork
		{
			get
			{
				return this.m_seedingNetwork;
			}
			set
			{
				this.m_seedingNetwork = value;
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x060007EB RID: 2027 RVA: 0x00006D48 File Offset: 0x00006148
		internal byte[] ExtendedErrorInfoBytes
		{
			get
			{
				return this.m_extendedErrorInfoBytes;
			}
		}

		// Token: 0x040009B5 RID: 2485
		private string m_suspendComment;

		// Token: 0x040009B6 RID: 2486
		private string m_dumpsterServers;

		// Token: 0x040009B7 RID: 2487
		private ActionInitiatorType m_actionInitiator;

		// Token: 0x040009B8 RID: 2488
		private bool m_seedingSource;

		// Token: 0x040009B9 RID: 2489
		private bool m_dumpsterRequired;

		// Token: 0x040009BA RID: 2490
		private DateTime m_dumpsterStartTime;

		// Token: 0x040009BB RID: 2491
		private DateTime m_dumpsterEndTime;

		// Token: 0x040009BC RID: 2492
		private DateTime m_latestAvailableLogTime;

		// Token: 0x040009BD RID: 2493
		private DateTime m_lastCopyNotifiedLogTime;

		// Token: 0x040009BE RID: 2494
		private DateTime m_lastCopiedLogTime;

		// Token: 0x040009BF RID: 2495
		private DateTime m_lastInspectedLogTime;

		// Token: 0x040009C0 RID: 2496
		private DateTime m_lastReplayedLogTime;

		// Token: 0x040009C1 RID: 2497
		private DateTime m_currentReplayLogTime;

		// Token: 0x040009C2 RID: 2498
		private long m_lastLogCopyNotified;

		// Token: 0x040009C3 RID: 2499
		private DateTime m_latestFullBackupTime;

		// Token: 0x040009C4 RID: 2500
		private DateTime m_latestIncrementalBackupTime;

		// Token: 0x040009C5 RID: 2501
		private DateTime m_latestDifferentialBackupTime;

		// Token: 0x040009C6 RID: 2502
		private DateTime m_latestCopyBackupTime;

		// Token: 0x040009C7 RID: 2503
		private bool m_snapshotLatestFullBackup;

		// Token: 0x040009C8 RID: 2504
		private bool m_snapshotLatestIncrementalBackup;

		// Token: 0x040009C9 RID: 2505
		private bool m_snapshotLatestDifferentialBackup;

		// Token: 0x040009CA RID: 2506
		private bool m_snapshotLatestCopyBackup;

		// Token: 0x040009CB RID: 2507
		private bool m_copyQueueNotKeepingUp;

		// Token: 0x040009CC RID: 2508
		private bool m_replayQueueNotKeepingUp;

		// Token: 0x040009CD RID: 2509
		private ContentIndexStatusType m_contentIndexStatus;

		// Token: 0x040009CE RID: 2510
		private string m_contentIndexErrorMessage;

		// Token: 0x040009CF RID: 2511
		private int? m_contentIndexErrorCode;

		// Token: 0x040009D0 RID: 2512
		private int? m_contentIndexVersion;

		// Token: 0x040009D1 RID: 2513
		private int? m_contentIndexBacklog;

		// Token: 0x040009D2 RID: 2514
		private int? m_contentIndexRetryQueueSize;

		// Token: 0x040009D3 RID: 2515
		private int? m_contentIndexMailboxesToCrawl;

		// Token: 0x040009D4 RID: 2516
		private int? m_contentIndexSeedingPercent;

		// Token: 0x040009D5 RID: 2517
		private string m_contentIndexSeedingSource;

		// Token: 0x040009D6 RID: 2518
		private int m_dbSeedingPercent;

		// Token: 0x040009D7 RID: 2519
		private long m_dbSeedingKBytesRead;

		// Token: 0x040009D8 RID: 2520
		private long m_dbSeedingKBytesWritten;

		// Token: 0x040009D9 RID: 2521
		private float m_dbSeedingKBytesReadPerSec;

		// Token: 0x040009DA RID: 2522
		private float m_dbSeedingKBytesWrittenPerSec;

		// Token: 0x040009DB RID: 2523
		private bool m_activationSuspended;

		// Token: 0x040009DC RID: 2524
		private bool m_singlePageRestore;

		// Token: 0x040009DD RID: 2525
		private long m_singlePageRestoreNumber;

		// Token: 0x040009DE RID: 2526
		private bool m_viable;

		// Token: 0x040009DF RID: 2527
		private bool m_lostWrite;

		// Token: 0x040009E0 RID: 2528
		private bool m_isReplaySuspended;

		// Token: 0x040009E1 RID: 2529
		private int m_workerProcessId;

		// Token: 0x040009E2 RID: 2530
		private NodeUpStatusEnum m_nodeStatus;

		// Token: 0x040009E3 RID: 2531
		private TimeSpan m_configuredReplayLagTime;

		// Token: 0x040009E4 RID: 2532
		private TimeSpan m_actualReplayLagTime;

		// Token: 0x040009E5 RID: 2533
		private string m_replayLagDisabledReason;

		// Token: 0x040009E6 RID: 2534
		private ReplayLagEnabledEnum m_replayLagEnabled;

		// Token: 0x040009E7 RID: 2535
		private ReplayLagPlayDownReasonEnum m_replayLagPlayDownReason;

		// Token: 0x040009E8 RID: 2536
		private int m_replayLagPercentage;

		// Token: 0x040009E9 RID: 2537
		private byte[] m_outgoingConnections;

		// Token: 0x040009EA RID: 2538
		private byte[] m_incomingLogCopyingNetwork;

		// Token: 0x040009EB RID: 2539
		private byte[] m_seedingNetwork;

		// Token: 0x040009EC RID: 2540
		private string m_errorMessage;

		// Token: 0x040009ED RID: 2541
		private uint m_errorEventId;

		// Token: 0x040009EE RID: 2542
		[NonSerialized]
		private ExtendedErrorInfo m_extendedErrorInfo;

		// Token: 0x040009EF RID: 2543
		private byte[] m_extendedErrorInfoBytes;

		// Token: 0x040009F0 RID: 2544
		private long m_logsReplayedSinceInstanceStart;

		// Token: 0x040009F1 RID: 2545
		private long m_logsCopiedSinceInstanceStart;

		// Token: 0x040009F2 RID: 2546
		private bool m_lastLogInfoIsStale;

		// Token: 0x040009F3 RID: 2547
		private DateTime m_lastLogInfoFromCopierTime;

		// Token: 0x040009F4 RID: 2548
		private DateTime m_lastLogInfoFromClusterTime;

		// Token: 0x040009F5 RID: 2549
		private long m_lastLogInfoFromClusterGen;

		// Token: 0x040009F6 RID: 2550
		private int m_activationPreference;

		// Token: 0x040009F7 RID: 2551
		private ulong m_diskFreeSpaceBytes;

		// Token: 0x040009F8 RID: 2552
		private ulong m_diskTotalSpaceBytes;

		// Token: 0x040009F9 RID: 2553
		private int m_diskFreeSpacePercent;

		// Token: 0x040009FA RID: 2554
		private DateTime m_lastDatabaseVolumeNameTransitionTime;

		// Token: 0x040009FB RID: 2555
		private string m_lastDatabaseVolumeName;

		// Token: 0x040009FC RID: 2556
		private string m_exchangeVolumeMountPoint;

		// Token: 0x040009FD RID: 2557
		private string m_databaseVolumeMountPoint;

		// Token: 0x040009FE RID: 2558
		private string m_databaseVolumeName;

		// Token: 0x040009FF RID: 2559
		private bool m_isDbOnMountedFolder;

		// Token: 0x04000A00 RID: 2560
		private string m_logVolumeMountPoint;

		// Token: 0x04000A01 RID: 2561
		private string m_logVolumeName;

		// Token: 0x04000A02 RID: 2562
		private bool m_isLogOnMountedFolder;

		// Token: 0x04000A03 RID: 2563
		private string m_volumeInfoLastError;

		// Token: 0x04000A04 RID: 2564
		private DateTime m_lastStatusTransitionTime;

		// Token: 0x04000A05 RID: 2565
		private DateTime m_instanceStartTime;

		// Token: 0x04000A06 RID: 2566
		private DateTime m_lastCopyAvailabilityChecksPassedTime;

		// Token: 0x04000A07 RID: 2567
		private DateTime m_lastCopyRedundancyChecksPassedTime;

		// Token: 0x04000A08 RID: 2568
		private bool m_isLastCopyAvailabilityChecksPassed;

		// Token: 0x04000A09 RID: 2569
		private bool m_isLastCopyRedundancyChecksPassed;

		// Token: 0x04000A0A RID: 2570
		private bool m_resumeBlocked;

		// Token: 0x04000A0B RID: 2571
		private bool m_reseedBlocked;

		// Token: 0x04000A0C RID: 2572
		private bool m_inPlaceReseedBlocked;

		// Token: 0x04000A0D RID: 2573
		private int m_minSupportedSchemaVersion;

		// Token: 0x04000A0E RID: 2574
		private int m_maxSupportedSchemaVersion;

		// Token: 0x04000A0F RID: 2575
		private int m_requestedSchemaVersion;

		// Token: 0x04000A10 RID: 2576
		private long m_lowestLogPresent;

		// Token: 0x04000A11 RID: 2577
		private long m_maxLogToReplay;

		// Token: 0x04000A12 RID: 2578
		private int m_serverAutoActivationPolicy;

		// Token: 0x04000A13 RID: 2579
		private uint m_statusBits;

		// Token: 0x04000A14 RID: 2580
		private bool m_seedingSourceForDB;

		// Token: 0x04000A15 RID: 2581
		private bool m_seedingSourceForCI;

		// Token: 0x0200012F RID: 303
		[Flags]
		private enum StatusBitsMask : uint
		{
			// Token: 0x04000A17 RID: 2583
			None = 0U,
			// Token: 0x04000A18 RID: 2584
			BlockMode = 1U,
			// Token: 0x04000A19 RID: 2585
			ActivationDisabledAndMoveNow = 2U,
			// Token: 0x04000A1A RID: 2586
			HAComponentOffline = 4U,
			// Token: 0x04000A1B RID: 2587
			IsPrimaryActiveManager = 8U
		}
	}
}
