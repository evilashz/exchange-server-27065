using System;
using System.Runtime.InteropServices;

namespace Microsoft.Exchange.Rpc.Cluster
{
	// Token: 0x02000130 RID: 304
	[Serializable]
	internal sealed class RpcDatabaseCopyStatus
	{
		// Token: 0x060007EC RID: 2028 RVA: 0x00019198 File Offset: 0x00018598
		public RpcDatabaseCopyStatus(RpcDatabaseCopyStatus2 s)
		{
			Guid dbguid = s.DBGuid;
			this.m_dbGuid = dbguid;
			this.m_mailboxServer = s.MailboxServer;
			this.m_activeDatabaseCopy = s.ActiveDatabaseCopy;
			DateTime statusRetrievedTime = s.StatusRetrievedTime;
			this.m_statusRetrievedTime = statusRetrievedTime;
			DateTime lastInspectedLogTime = s.LastInspectedLogTime;
			this.m_lastInspectedLogTime = lastInspectedLogTime;
			DateTime lastReplayedLogTime = s.LastReplayedLogTime;
			this.m_lastReplayedLogTime = lastReplayedLogTime;
			this.m_lastLogGenerated = s.LastLogGenerated;
			this.m_lastLogCopied = s.LastLogCopied;
			this.m_lastLogInspected = s.LastLogInspected;
			this.m_lastLogReplayed = s.LastLogReplayed;
			this.m_serverVersion = s.ServerVersion;
			this.m_copyStatus = s.CopyStatus;
			this.m_ciCurrentness = s.CICurrentness;
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

		// Token: 0x060007ED RID: 2029 RVA: 0x00006D5C File Offset: 0x0000615C
		public RpcDatabaseCopyStatus()
		{
		}

		// Token: 0x060007EE RID: 2030 RVA: 0x00019434 File Offset: 0x00018834
		[return: MarshalAs(UnmanagedType.U1)]
		public static bool operator ==(RpcDatabaseCopyStatus left, RpcDatabaseCopyStatus right)
		{
			return object.ReferenceEquals(left, right) || (left != null && right != null && (left.m_dbGuid == right.m_dbGuid && <Module>.StringEqual(left.m_suspendComment, right.m_suspendComment) && <Module>.StringEqual(left.m_mailboxServer, right.m_mailboxServer) && <Module>.StringEqual(left.m_dumpsterServers, right.m_dumpsterServers) && left.m_dumpsterRequired == right.m_dumpsterRequired && left.m_dumpsterStartTime == right.m_dumpsterStartTime && left.m_dumpsterEndTime == right.m_dumpsterEndTime && left.m_statusRetrievedTime == right.m_statusRetrievedTime && left.m_latestAvailableLogTime == right.m_latestAvailableLogTime && left.m_lastCopyNotifiedLogTime == right.m_lastCopyNotifiedLogTime && left.m_lastCopiedLogTime == right.m_lastCopiedLogTime && left.m_lastInspectedLogTime == right.m_lastInspectedLogTime && left.m_lastReplayedLogTime == right.m_lastReplayedLogTime && left.m_currentReplayLogTime == right.m_currentReplayLogTime && left.m_lastLogGenerated == right.m_lastLogGenerated && left.m_lastLogCopyNotified == right.m_lastLogCopyNotified && left.m_lastLogCopied == right.m_lastLogCopied && left.m_lastLogInspected == right.m_lastLogInspected && left.m_lastLogReplayed == right.m_lastLogReplayed && left.m_latestFullBackupTime == right.m_latestFullBackupTime && left.m_latestIncrementalBackupTime == right.m_latestIncrementalBackupTime && left.m_latestDifferentialBackupTime == right.m_latestDifferentialBackupTime && left.m_latestCopyBackupTime == right.m_latestCopyBackupTime && left.m_snapshotLatestFullBackup == right.m_snapshotLatestFullBackup && left.m_snapshotLatestIncrementalBackup == right.m_snapshotLatestIncrementalBackup && left.m_snapshotLatestDifferentialBackup == right.m_snapshotLatestDifferentialBackup && left.m_snapshotLatestCopyBackup == right.m_snapshotLatestCopyBackup && left.m_copyQueueNotKeepingUp == right.m_copyQueueNotKeepingUp && left.m_replayQueueNotKeepingUp == right.m_replayQueueNotKeepingUp && left.m_viable == right.m_viable && left.m_singlePageRestore == right.m_singlePageRestore && left.m_singlePageRestoreNumber == right.m_singlePageRestoreNumber && left.m_activationSuspended == right.m_activationSuspended && left.m_lostWrite == right.m_lostWrite && left.m_contentIndexStatus == right.m_contentIndexStatus && <Module>.StringEqual(left.m_contentIndexErrorMessage, right.m_contentIndexErrorMessage) && left.m_serverVersion == right.m_serverVersion && left.m_copyStatus == right.m_copyStatus && left.m_ciCurrentness == right.m_ciCurrentness && left.m_actionInitiator == right.m_actionInitiator && left.m_seedingSource == right.m_seedingSource && <Module>.StringEqual(left.m_errorMessage, right.m_errorMessage) && left.m_errorEventId == right.m_errorEventId && left.m_extendedErrorInfo == right.m_extendedErrorInfo && left.m_logsReplayedSinceInstanceStart == right.m_logsReplayedSinceInstanceStart && left.m_logsCopiedSinceInstanceStart == right.m_logsCopiedSinceInstanceStart));
		}

		// Token: 0x060007EF RID: 2031 RVA: 0x00006D70 File Offset: 0x00006170
		[return: MarshalAs(UnmanagedType.U1)]
		public static bool operator !=(RpcDatabaseCopyStatus left, RpcDatabaseCopyStatus right)
		{
			return ((!(left == right)) ? 1 : 0) != 0;
		}

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x060007F0 RID: 2032 RVA: 0x00006D88 File Offset: 0x00006188
		// (set) Token: 0x060007F1 RID: 2033 RVA: 0x00006DA0 File Offset: 0x000061A0
		public Guid DBGuid
		{
			get
			{
				return this.m_dbGuid;
			}
			set
			{
				this.m_dbGuid = value;
			}
		}

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x060007F2 RID: 2034 RVA: 0x00006DB4 File Offset: 0x000061B4
		// (set) Token: 0x060007F3 RID: 2035 RVA: 0x00006DC8 File Offset: 0x000061C8
		public string MailboxServer
		{
			get
			{
				return this.m_mailboxServer;
			}
			set
			{
				this.m_mailboxServer = value;
			}
		}

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x060007F4 RID: 2036 RVA: 0x00006DDC File Offset: 0x000061DC
		// (set) Token: 0x060007F5 RID: 2037 RVA: 0x00006DF0 File Offset: 0x000061F0
		public string ActiveDatabaseCopy
		{
			get
			{
				return this.m_activeDatabaseCopy;
			}
			set
			{
				this.m_activeDatabaseCopy = value;
			}
		}

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x060007F6 RID: 2038 RVA: 0x00006E04 File Offset: 0x00006204
		// (set) Token: 0x060007F7 RID: 2039 RVA: 0x00006E18 File Offset: 0x00006218
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

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x060007F8 RID: 2040 RVA: 0x00006E2C File Offset: 0x0000622C
		// (set) Token: 0x060007F9 RID: 2041 RVA: 0x00006E40 File Offset: 0x00006240
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

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x060007FA RID: 2042 RVA: 0x00006E54 File Offset: 0x00006254
		// (set) Token: 0x060007FB RID: 2043 RVA: 0x00006E68 File Offset: 0x00006268
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

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x060007FC RID: 2044 RVA: 0x00006E7C File Offset: 0x0000627C
		// (set) Token: 0x060007FD RID: 2045 RVA: 0x00006E90 File Offset: 0x00006290
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

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x060007FE RID: 2046 RVA: 0x00006EA4 File Offset: 0x000062A4
		// (set) Token: 0x060007FF RID: 2047 RVA: 0x00006EB8 File Offset: 0x000062B8
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

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x06000800 RID: 2048 RVA: 0x00006ECC File Offset: 0x000062CC
		// (set) Token: 0x06000801 RID: 2049 RVA: 0x00006EE0 File Offset: 0x000062E0
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

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x06000802 RID: 2050 RVA: 0x00006EF4 File Offset: 0x000062F4
		// (set) Token: 0x06000803 RID: 2051 RVA: 0x00006F08 File Offset: 0x00006308
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

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x06000804 RID: 2052 RVA: 0x00006F1C File Offset: 0x0000631C
		// (set) Token: 0x06000805 RID: 2053 RVA: 0x00006F30 File Offset: 0x00006330
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

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x06000806 RID: 2054 RVA: 0x00006F44 File Offset: 0x00006344
		// (set) Token: 0x06000807 RID: 2055 RVA: 0x00006F58 File Offset: 0x00006358
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

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x06000808 RID: 2056 RVA: 0x00006F6C File Offset: 0x0000636C
		// (set) Token: 0x06000809 RID: 2057 RVA: 0x00006F80 File Offset: 0x00006380
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

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x0600080A RID: 2058 RVA: 0x00006F94 File Offset: 0x00006394
		// (set) Token: 0x0600080B RID: 2059 RVA: 0x00006FA8 File Offset: 0x000063A8
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

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x0600080C RID: 2060 RVA: 0x00006FBC File Offset: 0x000063BC
		// (set) Token: 0x0600080D RID: 2061 RVA: 0x00006FD4 File Offset: 0x000063D4
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

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x0600080E RID: 2062 RVA: 0x00006FE8 File Offset: 0x000063E8
		// (set) Token: 0x0600080F RID: 2063 RVA: 0x00007000 File Offset: 0x00006400
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

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x06000810 RID: 2064 RVA: 0x00007014 File Offset: 0x00006414
		// (set) Token: 0x06000811 RID: 2065 RVA: 0x0000702C File Offset: 0x0000642C
		public DateTime StatusRetrievedTime
		{
			get
			{
				return this.m_statusRetrievedTime;
			}
			set
			{
				this.m_statusRetrievedTime = value;
			}
		}

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x06000812 RID: 2066 RVA: 0x00007040 File Offset: 0x00006440
		// (set) Token: 0x06000813 RID: 2067 RVA: 0x00007058 File Offset: 0x00006458
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

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x06000814 RID: 2068 RVA: 0x0000706C File Offset: 0x0000646C
		// (set) Token: 0x06000815 RID: 2069 RVA: 0x00007084 File Offset: 0x00006484
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

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x06000816 RID: 2070 RVA: 0x00007098 File Offset: 0x00006498
		// (set) Token: 0x06000817 RID: 2071 RVA: 0x000070B0 File Offset: 0x000064B0
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

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x06000818 RID: 2072 RVA: 0x000070C4 File Offset: 0x000064C4
		// (set) Token: 0x06000819 RID: 2073 RVA: 0x000070DC File Offset: 0x000064DC
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

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x0600081A RID: 2074 RVA: 0x000070F0 File Offset: 0x000064F0
		// (set) Token: 0x0600081B RID: 2075 RVA: 0x00007108 File Offset: 0x00006508
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

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x0600081C RID: 2076 RVA: 0x0000711C File Offset: 0x0000651C
		// (set) Token: 0x0600081D RID: 2077 RVA: 0x00007134 File Offset: 0x00006534
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

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x0600081E RID: 2078 RVA: 0x00007148 File Offset: 0x00006548
		// (set) Token: 0x0600081F RID: 2079 RVA: 0x0000715C File Offset: 0x0000655C
		public long LastLogGenerated
		{
			get
			{
				return this.m_lastLogGenerated;
			}
			set
			{
				this.m_lastLogGenerated = value;
			}
		}

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x06000820 RID: 2080 RVA: 0x00007170 File Offset: 0x00006570
		// (set) Token: 0x06000821 RID: 2081 RVA: 0x00007184 File Offset: 0x00006584
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

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x06000822 RID: 2082 RVA: 0x00007198 File Offset: 0x00006598
		// (set) Token: 0x06000823 RID: 2083 RVA: 0x000071AC File Offset: 0x000065AC
		public long LastLogCopied
		{
			get
			{
				return this.m_lastLogCopied;
			}
			set
			{
				this.m_lastLogCopied = value;
			}
		}

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x06000824 RID: 2084 RVA: 0x000071C0 File Offset: 0x000065C0
		// (set) Token: 0x06000825 RID: 2085 RVA: 0x000071D4 File Offset: 0x000065D4
		public long LastLogInspected
		{
			get
			{
				return this.m_lastLogInspected;
			}
			set
			{
				this.m_lastLogInspected = value;
			}
		}

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x06000826 RID: 2086 RVA: 0x000071E8 File Offset: 0x000065E8
		// (set) Token: 0x06000827 RID: 2087 RVA: 0x000071FC File Offset: 0x000065FC
		public long LastLogReplayed
		{
			get
			{
				return this.m_lastLogReplayed;
			}
			set
			{
				this.m_lastLogReplayed = value;
			}
		}

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x06000828 RID: 2088 RVA: 0x00007210 File Offset: 0x00006610
		// (set) Token: 0x06000829 RID: 2089 RVA: 0x00007228 File Offset: 0x00006628
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

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x0600082A RID: 2090 RVA: 0x0000723C File Offset: 0x0000663C
		// (set) Token: 0x0600082B RID: 2091 RVA: 0x00007254 File Offset: 0x00006654
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

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x0600082C RID: 2092 RVA: 0x00007268 File Offset: 0x00006668
		// (set) Token: 0x0600082D RID: 2093 RVA: 0x00007280 File Offset: 0x00006680
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

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x0600082E RID: 2094 RVA: 0x00007294 File Offset: 0x00006694
		// (set) Token: 0x0600082F RID: 2095 RVA: 0x000072AC File Offset: 0x000066AC
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

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x06000830 RID: 2096 RVA: 0x000072C0 File Offset: 0x000066C0
		// (set) Token: 0x06000831 RID: 2097 RVA: 0x000072D4 File Offset: 0x000066D4
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

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x06000832 RID: 2098 RVA: 0x000072E8 File Offset: 0x000066E8
		// (set) Token: 0x06000833 RID: 2099 RVA: 0x000072FC File Offset: 0x000066FC
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

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x06000834 RID: 2100 RVA: 0x00007310 File Offset: 0x00006710
		// (set) Token: 0x06000835 RID: 2101 RVA: 0x00007324 File Offset: 0x00006724
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

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x06000836 RID: 2102 RVA: 0x00007338 File Offset: 0x00006738
		// (set) Token: 0x06000837 RID: 2103 RVA: 0x0000734C File Offset: 0x0000674C
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

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x06000838 RID: 2104 RVA: 0x00007360 File Offset: 0x00006760
		// (set) Token: 0x06000839 RID: 2105 RVA: 0x00007374 File Offset: 0x00006774
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

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x0600083A RID: 2106 RVA: 0x00007388 File Offset: 0x00006788
		// (set) Token: 0x0600083B RID: 2107 RVA: 0x0000739C File Offset: 0x0000679C
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

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x0600083C RID: 2108 RVA: 0x000073B0 File Offset: 0x000067B0
		// (set) Token: 0x0600083D RID: 2109 RVA: 0x000073C4 File Offset: 0x000067C4
		public int ServerVersion
		{
			get
			{
				return this.m_serverVersion;
			}
			set
			{
				this.m_serverVersion = value;
			}
		}

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x0600083E RID: 2110 RVA: 0x000073D8 File Offset: 0x000067D8
		// (set) Token: 0x0600083F RID: 2111 RVA: 0x000073EC File Offset: 0x000067EC
		public CopyStatusEnum CopyStatus
		{
			get
			{
				return this.m_copyStatus;
			}
			set
			{
				this.m_copyStatus = value;
			}
		}

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x06000840 RID: 2112 RVA: 0x00007400 File Offset: 0x00006800
		// (set) Token: 0x06000841 RID: 2113 RVA: 0x00007414 File Offset: 0x00006814
		public ContentIndexCurrentness CICurrentness
		{
			get
			{
				return this.m_ciCurrentness;
			}
			set
			{
				this.m_ciCurrentness = value;
			}
		}

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x06000842 RID: 2114 RVA: 0x00007428 File Offset: 0x00006828
		// (set) Token: 0x06000843 RID: 2115 RVA: 0x0000743C File Offset: 0x0000683C
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

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x06000844 RID: 2116 RVA: 0x00007450 File Offset: 0x00006850
		// (set) Token: 0x06000845 RID: 2117 RVA: 0x00007464 File Offset: 0x00006864
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

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x06000846 RID: 2118 RVA: 0x00007478 File Offset: 0x00006878
		// (set) Token: 0x06000847 RID: 2119 RVA: 0x0000748C File Offset: 0x0000688C
		public ExtendedErrorInfo ExtendedErrorInfo
		{
			get
			{
				return this.m_extendedErrorInfo;
			}
			set
			{
				this.m_extendedErrorInfo = value;
			}
		}

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x06000848 RID: 2120 RVA: 0x000074A0 File Offset: 0x000068A0
		// (set) Token: 0x06000849 RID: 2121 RVA: 0x000074B4 File Offset: 0x000068B4
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

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x0600084A RID: 2122 RVA: 0x000074C8 File Offset: 0x000068C8
		// (set) Token: 0x0600084B RID: 2123 RVA: 0x000074DC File Offset: 0x000068DC
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

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x0600084C RID: 2124 RVA: 0x000074F0 File Offset: 0x000068F0
		// (set) Token: 0x0600084D RID: 2125 RVA: 0x00007504 File Offset: 0x00006904
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

		// Token: 0x0600084E RID: 2126 RVA: 0x00007518 File Offset: 0x00006918
		internal long GetCopyQueueLength()
		{
			long lastLogGenerated = this.m_lastLogGenerated;
			long lastLogInspected = this.m_lastLogInspected;
			return Math.Max(0L, lastLogGenerated - lastLogInspected);
		}

		// Token: 0x0600084F RID: 2127 RVA: 0x00007540 File Offset: 0x00006940
		internal long GetReplayQueueLength()
		{
			long lastLogInspected = this.m_lastLogInspected;
			long lastLogReplayed = this.m_lastLogReplayed;
			return Math.Max(0L, lastLogInspected - lastLogReplayed);
		}

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x06000850 RID: 2128 RVA: 0x00007568 File Offset: 0x00006968
		// (set) Token: 0x06000851 RID: 2129 RVA: 0x0000757C File Offset: 0x0000697C
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

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x06000852 RID: 2130 RVA: 0x00007590 File Offset: 0x00006990
		// (set) Token: 0x06000853 RID: 2131 RVA: 0x000075A4 File Offset: 0x000069A4
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

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x06000854 RID: 2132 RVA: 0x000075B8 File Offset: 0x000069B8
		// (set) Token: 0x06000855 RID: 2133 RVA: 0x000075CC File Offset: 0x000069CC
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

		// Token: 0x04000A1C RID: 2588
		private Guid m_dbGuid;

		// Token: 0x04000A1D RID: 2589
		private string m_suspendComment;

		// Token: 0x04000A1E RID: 2590
		private string m_mailboxServer;

		// Token: 0x04000A1F RID: 2591
		private string m_dumpsterServers;

		// Token: 0x04000A20 RID: 2592
		private string m_activeDatabaseCopy;

		// Token: 0x04000A21 RID: 2593
		private ActionInitiatorType m_actionInitiator;

		// Token: 0x04000A22 RID: 2594
		private bool m_seedingSource;

		// Token: 0x04000A23 RID: 2595
		private bool m_dumpsterRequired;

		// Token: 0x04000A24 RID: 2596
		private DateTime m_dumpsterStartTime;

		// Token: 0x04000A25 RID: 2597
		private DateTime m_dumpsterEndTime;

		// Token: 0x04000A26 RID: 2598
		private DateTime m_latestAvailableLogTime;

		// Token: 0x04000A27 RID: 2599
		private DateTime m_lastCopyNotifiedLogTime;

		// Token: 0x04000A28 RID: 2600
		private DateTime m_lastCopiedLogTime;

		// Token: 0x04000A29 RID: 2601
		private DateTime m_lastInspectedLogTime;

		// Token: 0x04000A2A RID: 2602
		private DateTime m_lastReplayedLogTime;

		// Token: 0x04000A2B RID: 2603
		private DateTime m_currentReplayLogTime;

		// Token: 0x04000A2C RID: 2604
		private long m_lastLogGenerated;

		// Token: 0x04000A2D RID: 2605
		private long m_lastLogCopyNotified;

		// Token: 0x04000A2E RID: 2606
		private long m_lastLogCopied;

		// Token: 0x04000A2F RID: 2607
		private long m_lastLogInspected;

		// Token: 0x04000A30 RID: 2608
		private long m_lastLogReplayed;

		// Token: 0x04000A31 RID: 2609
		private DateTime m_latestFullBackupTime;

		// Token: 0x04000A32 RID: 2610
		private DateTime m_latestIncrementalBackupTime;

		// Token: 0x04000A33 RID: 2611
		private DateTime m_latestDifferentialBackupTime;

		// Token: 0x04000A34 RID: 2612
		private DateTime m_latestCopyBackupTime;

		// Token: 0x04000A35 RID: 2613
		private bool m_snapshotLatestFullBackup;

		// Token: 0x04000A36 RID: 2614
		private bool m_snapshotLatestIncrementalBackup;

		// Token: 0x04000A37 RID: 2615
		private bool m_snapshotLatestDifferentialBackup;

		// Token: 0x04000A38 RID: 2616
		private bool m_snapshotLatestCopyBackup;

		// Token: 0x04000A39 RID: 2617
		private bool m_copyQueueNotKeepingUp;

		// Token: 0x04000A3A RID: 2618
		private bool m_replayQueueNotKeepingUp;

		// Token: 0x04000A3B RID: 2619
		private ContentIndexStatusType m_contentIndexStatus;

		// Token: 0x04000A3C RID: 2620
		private string m_contentIndexErrorMessage;

		// Token: 0x04000A3D RID: 2621
		private bool m_activationSuspended;

		// Token: 0x04000A3E RID: 2622
		private bool m_singlePageRestore;

		// Token: 0x04000A3F RID: 2623
		private long m_singlePageRestoreNumber;

		// Token: 0x04000A40 RID: 2624
		private bool m_viable;

		// Token: 0x04000A41 RID: 2625
		private bool m_lostWrite;

		// Token: 0x04000A42 RID: 2626
		private byte[] m_outgoingConnections;

		// Token: 0x04000A43 RID: 2627
		private byte[] m_incomingLogCopyingNetwork;

		// Token: 0x04000A44 RID: 2628
		private byte[] m_seedingNetwork;

		// Token: 0x04000A45 RID: 2629
		private int m_serverVersion;

		// Token: 0x04000A46 RID: 2630
		private CopyStatusEnum m_copyStatus;

		// Token: 0x04000A47 RID: 2631
		private ContentIndexCurrentness m_ciCurrentness;

		// Token: 0x04000A48 RID: 2632
		private string m_errorMessage;

		// Token: 0x04000A49 RID: 2633
		private uint m_errorEventId;

		// Token: 0x04000A4A RID: 2634
		private ExtendedErrorInfo m_extendedErrorInfo;

		// Token: 0x04000A4B RID: 2635
		private long m_logsReplayedSinceInstanceStart;

		// Token: 0x04000A4C RID: 2636
		private long m_logsCopiedSinceInstanceStart;

		// Token: 0x04000A4D RID: 2637
		private DateTime m_statusRetrievedTime;
	}
}
