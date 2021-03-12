using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using Microsoft.Exchange.Cluster.ClusApi;
using Microsoft.Exchange.Cluster.Shared;
using Microsoft.Exchange.Common.HA;
using Microsoft.Exchange.Common.IL;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ManagedStore.StoreCommonServices;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;
using Microsoft.Exchange.Server.Storage.StoreCommonServices.DatabaseUpgraders;
using Microsoft.Win32;

namespace Microsoft.Exchange.Server.Storage.StoreCommonServices
{
	// Token: 0x0200013B RID: 315
	public class StoreDatabase : ISchemaVersion, IStateObject
	{
		// Token: 0x06000C05 RID: 3077 RVA: 0x0003D638 File Offset: 0x0003B838
		public StoreDatabase(string mdbName, Guid mdbGuid, Guid dagOrServerGuid, string serverName, string legacyDN, string description, string logPath, string filePath, string fileName, bool hostServerIsDAGMember, bool circularLoggingEnabled, DatabaseOptions databaseOptions, bool isMultiRole, TimeSpan eventHistoryRetentionPeriod, bool isRecoveryDatabase, bool allowFileRestore, ServerEditionType edition, string forestName)
		{
			this.lockName = new LockName<Guid>(mdbGuid, LockManager.LockLevel.Database);
			this.dagOrServerGuid = dagOrServerGuid;
			this.serverName = serverName;
			this.legacyDN = legacyDN;
			this.description = description;
			this.eventHistoryRetentionPeriod = eventHistoryRetentionPeriod;
			this.resourceDigest = new ResourceMonitorDigest();
			this.serverEdition = edition;
			this.allowFileRestore = allowFileRestore;
			this.hostServerIsDAGMember = hostServerIsDAGMember;
			this.forestName = forestName;
			DatabaseFlags databaseFlags = DatabaseFlags.None;
			if (circularLoggingEnabled)
			{
				databaseFlags |= DatabaseFlags.CircularLoggingEnabled;
			}
			if (isMultiRole)
			{
				databaseFlags |= DatabaseFlags.IsMultiRole;
			}
			if (isRecoveryDatabase)
			{
				this.SetStatusFlag(DatabaseStatus.ForRecovery);
			}
			else
			{
				databaseFlags |= DatabaseFlags.BackgroundMaintenanceEnabled;
			}
			this.physicalDatabase = Factory.CreateDatabase(mdbGuid, mdbName, logPath, filePath, fileName, databaseFlags, databaseOptions);
			this.DismountError = ErrorCode.NoError;
		}

		// Token: 0x17000327 RID: 807
		// (get) Token: 0x06000C06 RID: 3078 RVA: 0x0003D6FC File Offset: 0x0003B8FC
		public TaskList TaskList
		{
			get
			{
				return this.taskList;
			}
		}

		// Token: 0x17000328 RID: 808
		// (get) Token: 0x06000C07 RID: 3079 RVA: 0x0003D704 File Offset: 0x0003B904
		public StoreDatabase.ComponentDataStorage ComponentData
		{
			get
			{
				if (this.componentDataStorage == null)
				{
					Interlocked.CompareExchange<StoreDatabase.ComponentDataStorage>(ref this.componentDataStorage, new StoreDatabase.ComponentDataStorage(this), null);
				}
				return this.componentDataStorage;
			}
		}

		// Token: 0x17000329 RID: 809
		// (get) Token: 0x06000C08 RID: 3080 RVA: 0x0003D727 File Offset: 0x0003B927
		public Database PhysicalDatabase
		{
			get
			{
				return this.physicalDatabase;
			}
		}

		// Token: 0x1700032A RID: 810
		// (get) Token: 0x06000C09 RID: 3081 RVA: 0x0003D72F File Offset: 0x0003B92F
		public ILockName LockName
		{
			get
			{
				return this.lockName;
			}
		}

		// Token: 0x1700032B RID: 811
		// (get) Token: 0x06000C0A RID: 3082 RVA: 0x0003D737 File Offset: 0x0003B937
		public Guid MdbGuid
		{
			get
			{
				return this.lockName.NameValue;
			}
		}

		// Token: 0x1700032C RID: 812
		// (get) Token: 0x06000C0B RID: 3083 RVA: 0x0003D744 File Offset: 0x0003B944
		public Guid DagOrServerGuid
		{
			get
			{
				return this.dagOrServerGuid;
			}
		}

		// Token: 0x1700032D RID: 813
		// (get) Token: 0x06000C0C RID: 3084 RVA: 0x0003D74C File Offset: 0x0003B94C
		public string MdbName
		{
			get
			{
				return this.physicalDatabase.DisplayName;
			}
		}

		// Token: 0x1700032E RID: 814
		// (get) Token: 0x06000C0D RID: 3085 RVA: 0x0003D759 File Offset: 0x0003B959
		public string VServerName
		{
			get
			{
				return this.serverName;
			}
		}

		// Token: 0x1700032F RID: 815
		// (get) Token: 0x06000C0E RID: 3086 RVA: 0x0003D761 File Offset: 0x0003B961
		public string LegacyDN
		{
			get
			{
				return this.legacyDN;
			}
		}

		// Token: 0x17000330 RID: 816
		// (get) Token: 0x06000C0F RID: 3087 RVA: 0x0003D769 File Offset: 0x0003B969
		public bool HostServerIsDAGMember
		{
			get
			{
				return this.hostServerIsDAGMember;
			}
		}

		// Token: 0x17000331 RID: 817
		// (get) Token: 0x06000C10 RID: 3088 RVA: 0x0003D771 File Offset: 0x0003B971
		public bool CircularLoggingEnabled
		{
			get
			{
				return (this.physicalDatabase.Flags & DatabaseFlags.CircularLoggingEnabled) != DatabaseFlags.None;
			}
		}

		// Token: 0x17000332 RID: 818
		// (get) Token: 0x06000C11 RID: 3089 RVA: 0x0003D786 File Offset: 0x0003B986
		public bool IsMultiRole
		{
			get
			{
				return (this.physicalDatabase.Flags & DatabaseFlags.IsMultiRole) != DatabaseFlags.None;
			}
		}

		// Token: 0x17000333 RID: 819
		// (get) Token: 0x06000C12 RID: 3090 RVA: 0x0003D79B File Offset: 0x0003B99B
		public bool IsOnlineActive
		{
			get
			{
				return StoreDatabase.DatabaseState.OnlineActive == this.state;
			}
		}

		// Token: 0x17000334 RID: 820
		// (get) Token: 0x06000C13 RID: 3091 RVA: 0x0003D7A8 File Offset: 0x0003B9A8
		public bool IsOnlinePassive
		{
			get
			{
				StoreDatabase.DatabaseState databaseState = this.state;
				switch (databaseState)
				{
				case StoreDatabase.DatabaseState.OnlinePassiveAttachedReadOnly:
				case StoreDatabase.DatabaseState.OnlinePassiveReplayingLogs:
					break;
				default:
					switch (databaseState)
					{
					case StoreDatabase.DatabaseState.OnlinePassiveReplayingLogsToOnlinePassiveAttachedReadOnly:
					case StoreDatabase.DatabaseState.OnlinePassiveAttachedReadOnlyToOnlinePassiveReplayingLogs:
						break;
					default:
						return false;
					}
					break;
				}
				return true;
			}
		}

		// Token: 0x17000335 RID: 821
		// (get) Token: 0x06000C14 RID: 3092 RVA: 0x0003D7E1 File Offset: 0x0003B9E1
		public bool IsOnlinePassiveAttachedReadOnly
		{
			get
			{
				return StoreDatabase.DatabaseState.OnlinePassiveAttachedReadOnly == this.state;
			}
		}

		// Token: 0x17000336 RID: 822
		// (get) Token: 0x06000C15 RID: 3093 RVA: 0x0003D7EC File Offset: 0x0003B9EC
		public bool IsOnlinePassiveReplayingLogs
		{
			get
			{
				return StoreDatabase.DatabaseState.OnlinePassiveReplayingLogs == this.state;
			}
		}

		// Token: 0x17000337 RID: 823
		// (get) Token: 0x06000C16 RID: 3094 RVA: 0x0003D7F7 File Offset: 0x0003B9F7
		public bool IsOffline
		{
			get
			{
				return StoreDatabase.DatabaseState.Offline == this.state;
			}
		}

		// Token: 0x17000338 RID: 824
		// (get) Token: 0x06000C17 RID: 3095 RVA: 0x0003D804 File Offset: 0x0003BA04
		public bool IsTransitioningToOnlineActive
		{
			get
			{
				switch (this.state)
				{
				case StoreDatabase.DatabaseState.OfflineToOnlineActive:
				case StoreDatabase.DatabaseState.OnlinePassiveReplayingLogsToOnlineActive:
				case StoreDatabase.DatabaseState.OnlinePassiveAttachedReadOnlyToOnlineActive:
					return true;
				}
				return false;
			}
		}

		// Token: 0x17000339 RID: 825
		// (get) Token: 0x06000C18 RID: 3096 RVA: 0x0003D838 File Offset: 0x0003BA38
		public bool IsTransitioningToOnline
		{
			get
			{
				switch (this.state)
				{
				case StoreDatabase.DatabaseState.OfflineToOnlineActive:
				case StoreDatabase.DatabaseState.OfflineToOnlinePassiveReplayingLogs:
				case StoreDatabase.DatabaseState.OnlinePassiveReplayingLogsToOnlineActive:
				case StoreDatabase.DatabaseState.OnlinePassiveAttachedReadOnlyToOnlineActive:
				case StoreDatabase.DatabaseState.OnlinePassiveReplayingLogsToOnlinePassiveAttachedReadOnly:
				case StoreDatabase.DatabaseState.OnlinePassiveAttachedReadOnlyToOnlinePassiveReplayingLogs:
					return true;
				default:
					return false;
				}
			}
		}

		// Token: 0x1700033A RID: 826
		// (get) Token: 0x06000C19 RID: 3097 RVA: 0x0003D874 File Offset: 0x0003BA74
		public bool IsTransitioningToOffline
		{
			get
			{
				switch (this.state)
				{
				case StoreDatabase.DatabaseState.OnlineActiveToOffline:
				case StoreDatabase.DatabaseState.OnlinePassiveReplayingLogsToOffline:
				case StoreDatabase.DatabaseState.OnlinePassiveAttachedReadOnlyToOffline:
					return true;
				default:
					return false;
				}
			}
		}

		// Token: 0x1700033B RID: 827
		// (get) Token: 0x06000C1A RID: 3098 RVA: 0x0003D8A4 File Offset: 0x0003BAA4
		public bool IsTransitioningFromOffline
		{
			get
			{
				switch (this.state)
				{
				case StoreDatabase.DatabaseState.OfflineToOnlineActive:
				case StoreDatabase.DatabaseState.OfflineToOnlinePassiveReplayingLogs:
					return true;
				default:
					return false;
				}
			}
		}

		// Token: 0x1700033C RID: 828
		// (get) Token: 0x06000C1B RID: 3099 RVA: 0x0003D8D0 File Offset: 0x0003BAD0
		public bool IsTransitioningBetweenOnlinePassiveStates
		{
			get
			{
				switch (this.state)
				{
				case StoreDatabase.DatabaseState.OnlinePassiveReplayingLogsToOnlinePassiveAttachedReadOnly:
				case StoreDatabase.DatabaseState.OnlinePassiveAttachedReadOnlyToOnlinePassiveReplayingLogs:
					return true;
				default:
					return false;
				}
			}
		}

		// Token: 0x1700033D RID: 829
		// (get) Token: 0x06000C1C RID: 3100 RVA: 0x0003D8F9 File Offset: 0x0003BAF9
		public bool IsReadOnly
		{
			get
			{
				return StoreDatabase.DatabaseState.OnlinePassiveAttachedReadOnly == this.state;
			}
		}

		// Token: 0x1700033E RID: 830
		// (get) Token: 0x06000C1D RID: 3101 RVA: 0x0003D904 File Offset: 0x0003BB04
		public bool IsBackupInProgress
		{
			get
			{
				return (this.MdbStatus & DatabaseStatus.BackupInProgress) == DatabaseStatus.BackupInProgress;
			}
		}

		// Token: 0x1700033F RID: 831
		// (get) Token: 0x06000C1E RID: 3102 RVA: 0x0003D911 File Offset: 0x0003BB11
		public bool IsInInteg
		{
			get
			{
				return (this.MdbStatus & DatabaseStatus.InInteg) == DatabaseStatus.InInteg;
			}
		}

		// Token: 0x17000340 RID: 832
		// (get) Token: 0x06000C1F RID: 3103 RVA: 0x0003D91E File Offset: 0x0003BB1E
		public bool IsPublic
		{
			get
			{
				return (this.MdbStatus & DatabaseStatus.IsPublic) == DatabaseStatus.IsPublic;
			}
		}

		// Token: 0x17000341 RID: 833
		// (get) Token: 0x06000C20 RID: 3104 RVA: 0x0003D92B File Offset: 0x0003BB2B
		public bool IsRecovery
		{
			get
			{
				return (this.MdbStatus & DatabaseStatus.ForRecovery) == DatabaseStatus.ForRecovery;
			}
		}

		// Token: 0x17000342 RID: 834
		// (get) Token: 0x06000C21 RID: 3105 RVA: 0x0003D93A File Offset: 0x0003BB3A
		public bool IsMaintenance
		{
			get
			{
				return (this.MdbStatus & DatabaseStatus.Maintenance) == DatabaseStatus.Maintenance;
			}
		}

		// Token: 0x17000343 RID: 835
		// (get) Token: 0x06000C22 RID: 3106 RVA: 0x0003D94C File Offset: 0x0003BB4C
		public uint ExternalMdbStatus
		{
			get
			{
				DatabaseStatus databaseStatus = this.MdbStatus;
				if (this.IsOnlineActive)
				{
					databaseStatus |= DatabaseStatus.OnLine;
				}
				else if (this.IsOnlinePassiveAttachedReadOnly)
				{
					databaseStatus |= DatabaseStatus.AttachedReadOnly;
				}
				else
				{
					databaseStatus = databaseStatus;
				}
				return (uint)databaseStatus;
			}
		}

		// Token: 0x17000344 RID: 836
		// (get) Token: 0x06000C23 RID: 3107 RVA: 0x0003D983 File Offset: 0x0003BB83
		public bool AllowFileRestore
		{
			get
			{
				return this.allowFileRestore;
			}
		}

		// Token: 0x17000345 RID: 837
		// (get) Token: 0x06000C24 RID: 3108 RVA: 0x0003D98B File Offset: 0x0003BB8B
		public string FilePath
		{
			get
			{
				return this.physicalDatabase.FilePath;
			}
		}

		// Token: 0x17000346 RID: 838
		// (get) Token: 0x06000C25 RID: 3109 RVA: 0x0003D998 File Offset: 0x0003BB98
		public string FileName
		{
			get
			{
				return this.physicalDatabase.FileName;
			}
		}

		// Token: 0x17000347 RID: 839
		// (get) Token: 0x06000C26 RID: 3110 RVA: 0x0003D9A5 File Offset: 0x0003BBA5
		public string LogPath
		{
			get
			{
				return this.physicalDatabase.LogPath;
			}
		}

		// Token: 0x17000348 RID: 840
		// (get) Token: 0x06000C27 RID: 3111 RVA: 0x0003D9B2 File Offset: 0x0003BBB2
		public string Description
		{
			get
			{
				return this.description;
			}
		}

		// Token: 0x17000349 RID: 841
		// (get) Token: 0x06000C28 RID: 3112 RVA: 0x0003D9BA File Offset: 0x0003BBBA
		public TimeSpan EventHistoryRetentionPeriod
		{
			get
			{
				return this.eventHistoryRetentionPeriod;
			}
		}

		// Token: 0x1700034A RID: 842
		// (get) Token: 0x06000C29 RID: 3113 RVA: 0x0003D9C2 File Offset: 0x0003BBC2
		// (set) Token: 0x06000C2A RID: 3114 RVA: 0x0003D9CA File Offset: 0x0003BBCA
		public DateTime MountTime
		{
			get
			{
				return this.mountTime;
			}
			private set
			{
				this.mountTime = value;
			}
		}

		// Token: 0x1700034B RID: 843
		// (get) Token: 0x06000C2B RID: 3115 RVA: 0x0003D9D3 File Offset: 0x0003BBD3
		public long MountId
		{
			get
			{
				return this.mountTime.Ticks;
			}
		}

		// Token: 0x1700034C RID: 844
		// (get) Token: 0x06000C2C RID: 3116 RVA: 0x0003D9E0 File Offset: 0x0003BBE0
		public ResourceMonitorDigest ResourceDigest
		{
			get
			{
				return this.resourceDigest;
			}
		}

		// Token: 0x1700034D RID: 845
		// (get) Token: 0x06000C2D RID: 3117 RVA: 0x0003D9E8 File Offset: 0x0003BBE8
		public string ForestName
		{
			get
			{
				return this.forestName;
			}
		}

		// Token: 0x1700034E RID: 846
		// (get) Token: 0x06000C2E RID: 3118 RVA: 0x0003D9F0 File Offset: 0x0003BBF0
		public DatabaseHeaderInfo DatabaseHeaderInfo
		{
			get
			{
				return this.databaseHeaderInfo;
			}
		}

		// Token: 0x1700034F RID: 847
		// (get) Token: 0x06000C2F RID: 3119 RVA: 0x0003D9F8 File Offset: 0x0003BBF8
		// (set) Token: 0x06000C30 RID: 3120 RVA: 0x0003D9FF File Offset: 0x0003BBFF
		internal static StoreDatabase.InitInMemoryDatabaseSchemaHandlerDelegate InitInMemoryDatabaseSchemaHandler { get; set; }

		// Token: 0x17000350 RID: 848
		// (get) Token: 0x06000C31 RID: 3121 RVA: 0x0003DA07 File Offset: 0x0003BC07
		// (set) Token: 0x06000C32 RID: 3122 RVA: 0x0003DA0E File Offset: 0x0003BC0E
		internal static StoreDatabase.MountingHandlerDelegate MountingHandler { get; set; }

		// Token: 0x17000351 RID: 849
		// (get) Token: 0x06000C33 RID: 3123 RVA: 0x0003DA16 File Offset: 0x0003BC16
		// (set) Token: 0x06000C34 RID: 3124 RVA: 0x0003DA1D File Offset: 0x0003BC1D
		internal static StoreDatabase.MountedHandlerDelegate MountedHandler { get; set; }

		// Token: 0x17000352 RID: 850
		// (get) Token: 0x06000C35 RID: 3125 RVA: 0x0003DA25 File Offset: 0x0003BC25
		// (set) Token: 0x06000C36 RID: 3126 RVA: 0x0003DA2C File Offset: 0x0003BC2C
		internal static StoreDatabase.DismountingHandlerDelegate DismountingHandler { get; set; }

		// Token: 0x17000353 RID: 851
		// (get) Token: 0x06000C37 RID: 3127 RVA: 0x0003DA34 File Offset: 0x0003BC34
		// (set) Token: 0x06000C38 RID: 3128 RVA: 0x0003DA3C File Offset: 0x0003BC3C
		internal StorePerDatabasePerformanceCountersInstance CachedStorePerDatabasePerformanceCountersInstance { get; set; }

		// Token: 0x17000354 RID: 852
		// (get) Token: 0x06000C39 RID: 3129 RVA: 0x0003DA45 File Offset: 0x0003BC45
		// (set) Token: 0x06000C3A RID: 3130 RVA: 0x0003DA4D File Offset: 0x0003BC4D
		internal ErrorCode DismountError { get; set; }

		// Token: 0x17000355 RID: 853
		// (get) Token: 0x06000C3B RID: 3131 RVA: 0x0003DA56 File Offset: 0x0003BC56
		internal ServerEditionType ServerEdition
		{
			get
			{
				return this.serverEdition;
			}
		}

		// Token: 0x17000356 RID: 854
		// (get) Token: 0x06000C3C RID: 3132 RVA: 0x0003DA5E File Offset: 0x0003BC5E
		private DatabaseStatus MdbStatus
		{
			get
			{
				return this.mdbStatus;
			}
		}

		// Token: 0x06000C3D RID: 3133 RVA: 0x0003DA68 File Offset: 0x0003BC68
		internal static void Initialize()
		{
			SchemaUpgradeService.Register(SchemaUpgradeService.SchemaCategory.Database, new SchemaUpgrader[]
			{
				AddLastMaintenanceTimeToMailbox.Instance,
				AddUpgradeHistoryTable.Instance,
				AsyncMessageCleanup.Instance,
				AddMidsetDeletedDelta.Instance,
				AddGroupMailboxType.Instance,
				UnifiedMailbox.Instance,
				RemoveFolderIdsetIn.Instance,
				UserInfoUpgrader.Instance,
				AddDatabaseDsGuidToGlobalsTable.Instance,
				EnableAddingSpecialFolders.Instance
			});
		}

		// Token: 0x06000C3E RID: 3134 RVA: 0x0003DAD5 File Offset: 0x0003BCD5
		internal static void Terminate()
		{
			SchemaUpgradeService.ClearRegistrations();
		}

		// Token: 0x06000C3F RID: 3135 RVA: 0x0003DADC File Offset: 0x0003BCDC
		public static ComponentVersion GetMinimumSchemaVersion()
		{
			return StoreDatabase.minimumSchemaVersionTestHook.Value;
		}

		// Token: 0x06000C40 RID: 3136 RVA: 0x0003DAE8 File Offset: 0x0003BCE8
		public static ComponentVersion GetMaximumSchemaVersion()
		{
			return StoreDatabase.maximumSchemaVersionTestHook.Value;
		}

		// Token: 0x06000C41 RID: 3137 RVA: 0x0003DAF4 File Offset: 0x0003BCF4
		public static int AllocateComponentDataSlot()
		{
			return StoreDatabase.ComponentDataStorage.AllocateSlot();
		}

		// Token: 0x06000C42 RID: 3138 RVA: 0x0003DAFB File Offset: 0x0003BCFB
		public static bool IsSharedLockHeld(Guid databaseGuid)
		{
			return LockManager.TestLock(StoreDatabase.GetDatabaseLockName(databaseGuid), LockManager.LockType.DatabaseShared);
		}

		// Token: 0x06000C43 RID: 3139 RVA: 0x0003DB0A File Offset: 0x0003BD0A
		public static bool IsExclusiveLockHeld(Guid databaseGuid)
		{
			return LockManager.TestLock(StoreDatabase.GetDatabaseLockName(databaseGuid), LockManager.LockType.DatabaseExclusive);
		}

		// Token: 0x06000C44 RID: 3140 RVA: 0x0003DB19 File Offset: 0x0003BD19
		[Conditional("DEBUG")]
		public static void AssertSharedLockHeld(Guid databaseGuid)
		{
		}

		// Token: 0x06000C45 RID: 3141 RVA: 0x0003DB1B File Offset: 0x0003BD1B
		[Conditional("DEBUG")]
		public static void AssertExclusiveLockHeld(Guid databaseGuid)
		{
		}

		// Token: 0x06000C46 RID: 3142 RVA: 0x0003DB1D File Offset: 0x0003BD1D
		[Conditional("DEBUG")]
		public static void AssertLocked(Guid databaseGuid)
		{
		}

		// Token: 0x06000C47 RID: 3143 RVA: 0x0003DB1F File Offset: 0x0003BD1F
		[Conditional("DEBUG")]
		public static void AssertNotLocked(Guid databaseGuid)
		{
		}

		// Token: 0x06000C48 RID: 3144 RVA: 0x0003DB21 File Offset: 0x0003BD21
		[Conditional("DEBUG")]
		public static void AssertNoDatabaseLocked()
		{
		}

		// Token: 0x06000C49 RID: 3145 RVA: 0x0003DB23 File Offset: 0x0003BD23
		public void GetSharedLock()
		{
			this.GetSharedLock(null);
		}

		// Token: 0x06000C4A RID: 3146 RVA: 0x0003DB2C File Offset: 0x0003BD2C
		public void GetSharedLock(ILockStatistics lockStats)
		{
			LockManager.GetLock(this.LockName, LockManager.LockType.DatabaseShared, lockStats);
		}

		// Token: 0x06000C4B RID: 3147 RVA: 0x0003DB3C File Offset: 0x0003BD3C
		public void ReleaseSharedLock()
		{
			LockManager.ReleaseLock(this.LockName, LockManager.LockType.DatabaseShared);
		}

		// Token: 0x06000C4C RID: 3148 RVA: 0x0003DB4B File Offset: 0x0003BD4B
		public LockManager.NamedLockFrame SharedLock(ILockStatistics lockStats)
		{
			return LockManager.Lock(this.LockName, LockManager.LockType.DatabaseShared, lockStats);
		}

		// Token: 0x06000C4D RID: 3149 RVA: 0x0003DB5B File Offset: 0x0003BD5B
		public bool IsSharedLockHeld()
		{
			return LockManager.TestLock(this.LockName, LockManager.LockType.DatabaseShared);
		}

		// Token: 0x06000C4E RID: 3150 RVA: 0x0003DB6C File Offset: 0x0003BD6C
		public void GetExclusiveLock()
		{
			try
			{
				Interlocked.Increment(ref this.exclusiveLockContentionCounter);
				LockManager.GetLock(this.LockName, LockManager.LockType.DatabaseExclusive);
			}
			finally
			{
				Interlocked.Decrement(ref this.exclusiveLockContentionCounter);
			}
		}

		// Token: 0x06000C4F RID: 3151 RVA: 0x0003DBB4 File Offset: 0x0003BDB4
		public void ReleaseExclusiveLock()
		{
			LockManager.ReleaseLock(this.LockName, LockManager.LockType.DatabaseExclusive);
		}

		// Token: 0x06000C50 RID: 3152 RVA: 0x0003DBC3 File Offset: 0x0003BDC3
		public bool IsExclusiveLockHeld()
		{
			return LockManager.TestLock(this.LockName, LockManager.LockType.DatabaseExclusive);
		}

		// Token: 0x06000C51 RID: 3153 RVA: 0x0003DBD2 File Offset: 0x0003BDD2
		public bool HasExclusiveLockContention()
		{
			return this.exclusiveLockContentionCounter > 0;
		}

		// Token: 0x06000C52 RID: 3154 RVA: 0x0003DBDD File Offset: 0x0003BDDD
		[Conditional("DEBUG")]
		public void AssertSharedLockHeld()
		{
		}

		// Token: 0x06000C53 RID: 3155 RVA: 0x0003DBDF File Offset: 0x0003BDDF
		[Conditional("DEBUG")]
		public void AssertExclusiveLockHeld()
		{
		}

		// Token: 0x06000C54 RID: 3156 RVA: 0x0003DBE1 File Offset: 0x0003BDE1
		[Conditional("DEBUG")]
		public void AssertLocked()
		{
		}

		// Token: 0x06000C55 RID: 3157 RVA: 0x0003DBE3 File Offset: 0x0003BDE3
		[Conditional("DEBUG")]
		public void AssertNotLocked()
		{
		}

		// Token: 0x06000C56 RID: 3158 RVA: 0x0003DBE5 File Offset: 0x0003BDE5
		[Conditional("DEBUG")]
		public void AssertDatabaseIsSafe()
		{
		}

		// Token: 0x06000C57 RID: 3159 RVA: 0x0003DBE7 File Offset: 0x0003BDE7
		[Conditional("DEBUG")]
		public void GetLockAndAssert(Func<bool> assertCondition, string assertMessage, bool sharedLock)
		{
			if (sharedLock)
			{
				this.GetSharedLock();
			}
			else
			{
				this.GetExclusiveLock();
			}
			if (sharedLock)
			{
				this.ReleaseSharedLock();
				return;
			}
			this.ReleaseExclusiveLock();
		}

		// Token: 0x06000C58 RID: 3160 RVA: 0x0003DC0A File Offset: 0x0003BE0A
		public void SetBackupInProgress()
		{
			this.SetStatusFlag(DatabaseStatus.BackupInProgress);
		}

		// Token: 0x06000C59 RID: 3161 RVA: 0x0003DC13 File Offset: 0x0003BE13
		public void ResetBackupInProgress()
		{
			this.ResetStatusFlag(DatabaseStatus.BackupInProgress);
		}

		// Token: 0x06000C5A RID: 3162 RVA: 0x0003DC1C File Offset: 0x0003BE1C
		public void ResetDatabaseEngine()
		{
			if (this.IsOnlineActive)
			{
				this.PhysicalDatabase.ResetDatabaseEngine();
			}
		}

		// Token: 0x06000C5B RID: 3163 RVA: 0x0003DC34 File Offset: 0x0003BE34
		public bool IsDatabaseEngineTooBusyForDatabaseMaintenanceTask(Guid maintenanceId)
		{
			string text;
			long num;
			long num2;
			bool flag = this.PhysicalDatabase.IsDatabaseEngineBusy(out text, out num, out num2);
			if (StoreDatabase.isDatabaseEngineBusyTestHook.Value != null)
			{
				flag = StoreDatabase.isDatabaseEngineBusyTestHook.Value(flag);
			}
			if (flag)
			{
				Microsoft.Exchange.Server.Storage.Common.Globals.LogEvent(MSExchangeISEventLogConstants.Tuple_DatabaseMaintenancePreemptedByDbEngineBusy, new object[]
				{
					this.MdbGuid,
					this.MdbName,
					maintenanceId,
					MaintenanceHandler.GetDatabaseMaintenanceTaskName(maintenanceId),
					text,
					num,
					num2
				});
			}
			return flag;
		}

		// Token: 0x06000C5C RID: 3164 RVA: 0x0003DCD0 File Offset: 0x0003BED0
		public bool IsDatabaseEngineTooBusyForMailboxMaintenanceTask(MailboxState mailboxState, Guid maintenanceId)
		{
			string text;
			long num;
			long num2;
			bool flag = this.PhysicalDatabase.IsDatabaseEngineBusy(out text, out num, out num2);
			if (StoreDatabase.isDatabaseEngineBusyTestHook.Value != null)
			{
				flag = StoreDatabase.isDatabaseEngineBusyTestHook.Value(flag);
			}
			if (flag)
			{
				Microsoft.Exchange.Server.Storage.Common.Globals.LogEvent(MSExchangeISEventLogConstants.Tuple_MailboxMaintenancePreemptedByDbEngineBusy, new object[]
				{
					this.MdbGuid,
					this.MdbName,
					mailboxState.MailboxGuid,
					mailboxState.MailboxNumber,
					mailboxState.TenantHint,
					maintenanceId,
					MaintenanceHandler.GetMailboxMaintenanceTaskName(maintenanceId),
					text,
					num,
					num2
				});
			}
			return flag;
		}

		// Token: 0x06000C5D RID: 3165 RVA: 0x0003DD9C File Offset: 0x0003BF9C
		public void TraceState(LID lid)
		{
			DiagnosticContext.TraceDword(lid, (uint)this.state);
		}

		// Token: 0x06000C5E RID: 3166 RVA: 0x0003DDAA File Offset: 0x0003BFAA
		void IStateObject.OnBeforeCommit(Context context)
		{
		}

		// Token: 0x06000C5F RID: 3167 RVA: 0x0003DDAC File Offset: 0x0003BFAC
		void IStateObject.OnCommit(Context context)
		{
		}

		// Token: 0x06000C60 RID: 3168 RVA: 0x0003DDAE File Offset: 0x0003BFAE
		void IStateObject.OnAbort(Context context)
		{
			this.currentSchemaVersion = null;
		}

		// Token: 0x06000C61 RID: 3169 RVA: 0x0003DDBC File Offset: 0x0003BFBC
		public void ExtendDatabase(Context context)
		{
			this.PhysicalDatabase.ExtendDatabase(context);
		}

		// Token: 0x06000C62 RID: 3170 RVA: 0x0003DDCA File Offset: 0x0003BFCA
		public void ShrinkDatabase(Context context)
		{
			this.PhysicalDatabase.ShrinkDatabase(context);
		}

		// Token: 0x17000357 RID: 855
		// (get) Token: 0x06000C63 RID: 3171 RVA: 0x0003DDD8 File Offset: 0x0003BFD8
		public ComponentVersion CurrentSchemaVersionForDiagnostics
		{
			get
			{
				if (this.currentSchemaVersion == null)
				{
					return StoreDatabase.maximumSchemaVersionTestHook.Value;
				}
				return this.currentSchemaVersion.Value;
			}
		}

		// Token: 0x06000C64 RID: 3172 RVA: 0x0003DE00 File Offset: 0x0003C000
		public ComponentVersion GetCurrentSchemaVersion(Context context)
		{
			if (this.currentSchemaVersion == null)
			{
				GlobalsTable globalsTable = DatabaseSchema.GlobalsTable(this);
				Column[] columnsToFetch = new Column[]
				{
					globalsTable.DatabaseVersion
				};
				int @int;
				try
				{
					using (TableOperator tableOperator = Factory.CreateTableOperator(CultureHelper.DefaultCultureInfo, context, globalsTable.Table, globalsTable.Table.PrimaryKeyIndex, columnsToFetch, null, null, 0, 1, KeyRange.AllRows, false, true))
					{
						using (Reader reader = tableOperator.ExecuteReader(false))
						{
							if (!reader.Read())
							{
								throw new DatabaseSchemaBroken(this.MdbGuid.ToString(), "No globals table row");
							}
							@int = reader.GetInt32(globalsTable.DatabaseVersion);
						}
					}
				}
				catch (DatabaseSchemaBroken databaseSchemaBroken)
				{
					context.OnExceptionCatch(databaseSchemaBroken);
					Microsoft.Exchange.Server.Storage.Common.Globals.LogEvent(MSExchangeISEventLogConstants.Tuple_DatabaseLogicalCorruption, new object[]
					{
						this.MdbGuid
					});
					this.PublishHaFailure(FailureTag.Configuration);
					throw new DatabaseLogicalCorruption((LID)63776U, this.MdbGuid, databaseSchemaBroken);
				}
				this.currentSchemaVersion = new ComponentVersion?(new ComponentVersion(@int));
			}
			return this.currentSchemaVersion.Value;
		}

		// Token: 0x06000C65 RID: 3173 RVA: 0x0003DF50 File Offset: 0x0003C150
		public void SetCurrentSchemaVersion(Context context, ComponentVersion version)
		{
			GlobalsTable globalsTable = DatabaseSchema.GlobalsTable(this);
			Column[] columnsToUpdate = new Column[]
			{
				globalsTable.DatabaseVersion
			};
			List<object> list = new List<object>(1);
			list.Add(version.Value);
			using (UpdateOperator updateOperator = Factory.CreateUpdateOperator(CultureHelper.DefaultCultureInfo, context, Factory.CreateTableOperator(CultureHelper.DefaultCultureInfo, context, globalsTable.Table, globalsTable.Table.PrimaryKeyIndex, null, null, null, 0, 1, KeyRange.AllRows, false, true), columnsToUpdate, list, true))
			{
				int num = (int)updateOperator.ExecuteScalar();
			}
			this.currentSchemaVersion = new ComponentVersion?(version);
		}

		// Token: 0x06000C66 RID: 3174 RVA: 0x0003E000 File Offset: 0x0003C200
		public ComponentVersion GetRequestedSchemaVersion(Context context, ComponentVersion currentVersion, ComponentVersion maximumSupportedVersion)
		{
			int num;
			if ((this.MdbStatus & DatabaseStatus.ForRecovery) == DatabaseStatus.ForRecovery)
			{
				num = currentVersion.Value;
			}
			else
			{
				if (this.hostServerIsDAGMember)
				{
					num = currentVersion.Value;
					try
					{
						using (IClusterDB clusterDB = ClusterDB.Open())
						{
							if (clusterDB != null && clusterDB.IsInstalled && clusterDB.IsInitialized)
							{
								ClusterDBHelpers.ReadRequestedDatabaseSchemaVersion(clusterDB, this.MdbGuid, currentVersion.Value, out num);
							}
						}
						goto IL_89;
					}
					catch (ClusterException exception)
					{
						context.OnExceptionCatch(exception);
						Microsoft.Exchange.Server.Storage.Common.Globals.AssertRetail(num == currentVersion.Value, "value changed?");
						goto IL_89;
					}
				}
				num = maximumSupportedVersion.Value;
			}
			IL_89:
			num = RegistryReader.Instance.GetValue<int>(Registry.LocalMachine, "SYSTEM\\CurrentControlSet\\Services\\MSExchangeIS\\ParametersSystem", "Requested Version", num);
			num = Math.Min(num, maximumSupportedVersion.Value);
			return new ComponentVersion(num);
		}

		// Token: 0x17000358 RID: 856
		// (get) Token: 0x06000C67 RID: 3175 RVA: 0x0003E0E4 File Offset: 0x0003C2E4
		public string Identifier
		{
			get
			{
				return this.MdbGuid.ToString();
			}
		}

		// Token: 0x17000359 RID: 857
		// (get) Token: 0x06000C68 RID: 3176 RVA: 0x0003E105 File Offset: 0x0003C305
		int ISchemaVersion.MailboxNumber
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x06000C69 RID: 3177 RVA: 0x0003E108 File Offset: 0x0003C308
		internal static IDisposable SetMinimumSchemaVersionTestHook(ComponentVersion newMinimum)
		{
			return StoreDatabase.minimumSchemaVersionTestHook.SetTestHook(newMinimum);
		}

		// Token: 0x06000C6A RID: 3178 RVA: 0x0003E118 File Offset: 0x0003C318
		internal static IDisposable SetMaximumSchemaVersionTestHook(ComponentVersion newMaximum)
		{
			DisposeGuard disposeGuard = default(DisposeGuard);
			disposeGuard.Add<IDisposable>(StoreDatabase.maximumSchemaVersionTestHook.SetTestHook(newMaximum));
			disposeGuard.Add<IDisposable>(ConfigurationSchema.MaximumRequestableSchemaVersion.SetDefaultValueHook(newMaximum));
			return disposeGuard;
		}

		// Token: 0x06000C6B RID: 3179 RVA: 0x0003E159 File Offset: 0x0003C359
		internal static IDisposable SetDismountRequestedStopTasksTestHook(Action action)
		{
			return StoreDatabase.dismountRequestedStopTasksTestHook.SetTestHook(action);
		}

		// Token: 0x06000C6C RID: 3180 RVA: 0x0003E166 File Offset: 0x0003C366
		internal static IDisposable SetIsDatabaseEngineBusyTestHook(Func<bool, bool> testDelegate)
		{
			return StoreDatabase.isDatabaseEngineBusyTestHook.SetTestHook(testDelegate);
		}

		// Token: 0x06000C6D RID: 3181 RVA: 0x0003E173 File Offset: 0x0003C373
		internal static IDisposable SetPreMoveToNewStateTestHook(Action<StoreDatabase.DatabaseState, StoreDatabase.DatabaseState> testDelegate)
		{
			return StoreDatabase.preMoveToNewStateTestHook.SetTestHook(testDelegate);
		}

		// Token: 0x06000C6E RID: 3182 RVA: 0x0003E180 File Offset: 0x0003C380
		internal static IDisposable SetPostMoveToNewStateTestHook(Action<StoreDatabase.DatabaseState, object> testDelegate)
		{
			return StoreDatabase.postMoveToNewStateTestHook.SetTestHook(testDelegate);
		}

		// Token: 0x06000C6F RID: 3183 RVA: 0x0003E18D File Offset: 0x0003C38D
		internal static IDisposable SetWaitForTransitionBetweenOnlinePassiveStatesTestHook(Action testDelegate)
		{
			return StoreDatabase.waitForTransitionBetweenOnlinePassiveStatesTestHook.SetTestHook(testDelegate);
		}

		// Token: 0x06000C70 RID: 3184 RVA: 0x0003E19A File Offset: 0x0003C39A
		internal static IDisposable SetSimulateTimeoutWaitingForTransitionBetweenOnlinePassiveStatesTestHook(Func<int, bool> testDelegate)
		{
			return StoreDatabase.simulateTimeoutWaitingForTransitionBetweenOnlinePassiveStatesTestHook.SetTestHook(testDelegate);
		}

		// Token: 0x06000C71 RID: 3185 RVA: 0x0003E1A7 File Offset: 0x0003C3A7
		internal static IDisposable SetDatabaseCreationTestHook(Action action)
		{
			return StoreDatabase.databaseCreationTestHook.SetTestHook(action);
		}

		// Token: 0x06000C72 RID: 3186 RVA: 0x0003E1B4 File Offset: 0x0003C3B4
		internal static IDisposable SetPassiveAttachedDetachedTestHook(Action<bool, uint> testDelegate)
		{
			return StoreDatabase.passiveAttachedDetachedTestHook.SetTestHook(testDelegate);
		}

		// Token: 0x06000C73 RID: 3187 RVA: 0x0003E1C1 File Offset: 0x0003C3C1
		internal static IDisposable SetPermitReplayThreadAttachToPassiveDatabaseTestHook(Action<StoreDatabase> testDelegate)
		{
			return StoreDatabase.permitReplayThreadAttachPassiveDatabaseTestHook.SetTestHook(testDelegate);
		}

		// Token: 0x06000C74 RID: 3188 RVA: 0x0003E1D0 File Offset: 0x0003C3D0
		internal ErrorCode MountDatabase(Context context, MountFlags flags, ref bool errorOnTheThreadExecutingTheMount)
		{
			bool flag = (flags & MountFlags.LogReplay) == MountFlags.LogReplay;
			bool allowLoss = (flags & MountFlags.AllowDatabasePatch) == MountFlags.AllowDatabasePatch || (flags & MountFlags.AcceptDataLoss) == MountFlags.AcceptDataLoss;
			StoreDatabase.MountOperation mountOperation = StoreDatabase.MountOperation.None;
			bool flag2 = true;
			bool flag3 = false;
			bool flag4 = false;
			StoreDatabase.DatabaseState databaseState = StoreDatabase.DatabaseState.Offline;
			try
			{
				if (ExTraceGlobals.StoreDatabaseTracer.IsTraceEnabled(TraceType.DebugTrace))
				{
					ExTraceGlobals.StoreDatabaseTracer.TraceDebug<MountFlags, StoreDatabase.DatabaseState>(0L, "StoreDatabase:MountDatabase Flags:{0} Current State:{1}.", flags, this.state);
				}
				this.GetExclusiveLock();
				if (this.IsTransitioningBetweenOnlinePassiveStates)
				{
					if (flag)
					{
						flag2 = false;
						return ErrorCode.NoError;
					}
					if (!this.WaitForTransitionBetweenOnlinePassiveStates())
					{
						this.TraceState((LID)36604U);
						return ErrorCode.CreateMountInProgress((LID)61180U);
					}
				}
				if (this.IsOffline)
				{
					if (flag)
					{
						mountOperation = StoreDatabase.MountOperation.MountPassive;
						databaseState = StoreDatabase.DatabaseState.OfflineToOnlinePassiveReplayingLogs;
					}
					else
					{
						mountOperation = StoreDatabase.MountOperation.MountActive;
						databaseState = StoreDatabase.DatabaseState.OfflineToOnlineActive;
					}
				}
				else if (this.IsOnlineActive)
				{
					if (ExTraceGlobals.FaultInjectionTracer.IsTraceEnabled(TraceType.FaultInjection))
					{
						ExTraceGlobals.FaultInjectionTracer.TraceTest(2730896701U);
					}
					if (flag)
					{
						flag2 = false;
						return ErrorCode.CreateDatabaseStateConflict((LID)33340U);
					}
					flag2 = false;
					return ErrorCode.NoError;
				}
				else if (this.IsOnlinePassiveAttachedReadOnly)
				{
					if (flag)
					{
						flag2 = false;
						return ErrorCode.NoError;
					}
					this.StartForceDetachPassiveDatabase();
					flag4 = true;
					databaseState = StoreDatabase.DatabaseState.OnlinePassiveAttachedReadOnlyToOnlineActive;
					mountOperation = StoreDatabase.MountOperation.ActivatePassive;
				}
				else if (this.IsOnlinePassiveReplayingLogs)
				{
					if (flag)
					{
						flag2 = false;
						return ErrorCode.NoError;
					}
					databaseState = StoreDatabase.DatabaseState.OnlinePassiveReplayingLogsToOnlineActive;
					mountOperation = StoreDatabase.MountOperation.ActivatePassive;
				}
				else
				{
					if (this.IsTransitioningToOnline)
					{
						this.TraceState((LID)50172U);
						return ErrorCode.CreateMountInProgress((LID)58296U);
					}
					if (this.IsTransitioningToOffline)
					{
						this.TraceState((LID)48380U);
						return ErrorCode.CreateDismountInProgress((LID)33720U);
					}
					Microsoft.Exchange.Server.Storage.Common.Globals.AssertRetail(false, "Unexpected database state.");
				}
				Microsoft.Exchange.Server.Storage.Common.Globals.AssertRetail(StoreDatabase.MountOperation.None != mountOperation, "The mount operation should be defined.");
				this.SetNewState(databaseState);
				this.ReleaseExclusiveLock();
				if (StoreDatabase.postMoveToNewStateTestHook.Value != null)
				{
					StoreDatabase.postMoveToNewStateTestHook.Value(databaseState, null);
				}
				flag3 = true;
				if (ExTraceGlobals.FaultInjectionTracer.IsTraceEnabled(TraceType.FaultInjection))
				{
					ExTraceGlobals.FaultInjectionTracer.TraceTest(4207291709U);
				}
				databaseState = StoreDatabase.DatabaseState.Offline;
				DiagnosticContext.TraceDword((LID)45120U, (uint)Environment.TickCount);
				switch (mountOperation)
				{
				case StoreDatabase.MountOperation.MountActive:
					this.MountActive(context, allowLoss);
					databaseState = StoreDatabase.DatabaseState.OnlineActive;
					break;
				case StoreDatabase.MountOperation.MountPassive:
					this.MountPassive(context);
					databaseState = StoreDatabase.DatabaseState.OnlinePassiveReplayingLogs;
					break;
				case StoreDatabase.MountOperation.ActivatePassive:
					if (flag4)
					{
						this.FinishForceDetachPassiveDatabase(context);
					}
					this.ActivatePassive(context);
					databaseState = StoreDatabase.DatabaseState.OnlineActive;
					break;
				}
				DiagnosticContext.TraceDword((LID)59776U, (uint)Environment.TickCount);
				FaultInjection.InjectFault(this.databaseTestHook);
				this.GetExclusiveLock();
				this.SetNewState(databaseState);
				if (mountOperation == StoreDatabase.MountOperation.MountPassive)
				{
					this.PermitReplayThreadAttachPassiveDatabase();
				}
				this.ReleaseExclusiveLock();
				if (StoreDatabase.postMoveToNewStateTestHook.Value != null)
				{
					StoreDatabase.postMoveToNewStateTestHook.Value(databaseState, null);
				}
				this.PostMountInitialization(context);
				if (ExTraceGlobals.FaultInjectionTracer.IsTraceEnabled(TraceType.FaultInjection))
				{
					ExTraceGlobals.FaultInjectionTracer.TraceTest(2596678973U);
				}
				flag3 = false;
				flag2 = false;
			}
			finally
			{
				errorOnTheThreadExecutingTheMount = flag3;
				if (this.IsExclusiveLockHeld())
				{
					this.ReleaseExclusiveLock();
				}
				if (flag2)
				{
					this.PublishHaFailure(FailureTag.GenericMountFailure);
				}
				else if (!flag)
				{
					Microsoft.Exchange.Server.Storage.Common.Globals.LogEvent(MSExchangeISEventLogConstants.Tuple_WorkerMountCompleted, new object[]
					{
						this.MdbGuid
					});
				}
			}
			return ErrorCode.NoError;
		}

		// Token: 0x06000C75 RID: 3189 RVA: 0x0003E540 File Offset: 0x0003C740
		internal ErrorCode MountDatabase(Context context, MountFlags flags)
		{
			bool flag = false;
			this.GetExclusiveLock();
			StoreDatabase.DatabaseState databaseState = this.state;
			this.ReleaseExclusiveLock();
			ErrorCode result;
			try
			{
				result = this.MountDatabase(context, flags, ref flag);
			}
			finally
			{
				if (flag)
				{
					this.GetExclusiveLock();
					this.SetNewState(databaseState);
					this.ReleaseExclusiveLock();
					if (StoreDatabase.postMoveToNewStateTestHook.Value != null)
					{
						StoreDatabase.postMoveToNewStateTestHook.Value(databaseState, null);
					}
				}
			}
			return result;
		}

		// Token: 0x06000C76 RID: 3190 RVA: 0x0003E5B4 File Offset: 0x0003C7B4
		internal ErrorCode DismountDatabase(Context context)
		{
			if (ExTraceGlobals.StoreDatabaseTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				ExTraceGlobals.StoreDatabaseTracer.TraceDebug<StoreDatabase.DatabaseState>(0L, "Execute DismountDatabase. Current State:{0}", this.state);
			}
			bool flag = false;
			StoreDatabase.DismountOperation dismountOperation = StoreDatabase.DismountOperation.None;
			StoreDatabase.DatabaseState databaseState = StoreDatabase.DatabaseState.OnlineActive;
			try
			{
				this.GetSharedLock();
				if (this.IsOnlineActive)
				{
					this.taskList.StopAllAndPreventFurtherRegistration();
					FaultInjection.InjectFault(StoreDatabase.dismountRequestedStopTasksTestHook);
				}
			}
			finally
			{
				this.ReleaseSharedLock();
			}
			try
			{
				this.GetExclusiveLock();
				if (this.IsTransitioningBetweenOnlinePassiveStates && !this.WaitForTransitionBetweenOnlinePassiveStates())
				{
					this.TraceState((LID)52988U);
					return ErrorCode.CreateMountInProgress((LID)46844U);
				}
				if (this.IsOnlineActive)
				{
					dismountOperation = StoreDatabase.DismountOperation.DismountActive;
					databaseState = StoreDatabase.DatabaseState.OnlineActiveToOffline;
				}
				else if (this.IsOnlinePassiveAttachedReadOnly)
				{
					this.StartForceDetachPassiveDatabase();
					flag = true;
					dismountOperation = StoreDatabase.DismountOperation.DismountPassive;
					databaseState = StoreDatabase.DatabaseState.OnlinePassiveAttachedReadOnlyToOffline;
				}
				else if (this.IsOnlinePassiveReplayingLogs)
				{
					dismountOperation = StoreDatabase.DismountOperation.DismountPassive;
					databaseState = StoreDatabase.DatabaseState.OnlinePassiveReplayingLogsToOffline;
				}
				else
				{
					if (this.IsTransitioningToOnline)
					{
						this.TraceState((LID)64764U);
						return ErrorCode.CreateMountInProgress((LID)50104U);
					}
					if (this.IsTransitioningToOffline)
					{
						this.TraceState((LID)40188U);
						return ErrorCode.CreateDismountInProgress((LID)48312U);
					}
					if (this.IsOffline)
					{
						return ErrorCode.NoError;
					}
					Microsoft.Exchange.Server.Storage.Common.Globals.AssertRetail(false, "Unexpected database state.");
				}
				Microsoft.Exchange.Server.Storage.Common.Globals.AssertRetail(StoreDatabase.DismountOperation.None != dismountOperation, "The dismount operation should be defined.");
				this.SetNewState(databaseState);
				this.ReleaseExclusiveLock();
				if (StoreDatabase.postMoveToNewStateTestHook.Value != null)
				{
					StoreDatabase.postMoveToNewStateTestHook.Value(databaseState, null);
				}
				if (ExTraceGlobals.FaultInjectionTracer.IsTraceEnabled(TraceType.FaultInjection))
				{
					ExTraceGlobals.FaultInjectionTracer.TraceTest(3032886589U);
				}
				DiagnosticContext.TraceDword((LID)43072U, (uint)Environment.TickCount);
				using (context.CriticalBlock((LID)30664U, CriticalBlockScope.Database))
				{
					switch (dismountOperation)
					{
					case StoreDatabase.DismountOperation.DismountActive:
						this.DismountActive(context);
						break;
					case StoreDatabase.DismountOperation.DismountPassive:
						if (flag)
						{
							this.FinishForceDetachPassiveDatabase(context);
						}
						this.DismountPassive(context);
						break;
					}
					DiagnosticContext.TraceDword((LID)59456U, (uint)Environment.TickCount);
					FaultInjection.InjectFault(this.databaseTestHook);
					context.EndCriticalBlock();
				}
				this.GetExclusiveLock();
				this.SetNewState(StoreDatabase.DatabaseState.Offline);
			}
			finally
			{
				if (this.IsExclusiveLockHeld())
				{
					this.ReleaseExclusiveLock();
				}
			}
			if (StoreDatabase.postMoveToNewStateTestHook.Value != null)
			{
				StoreDatabase.postMoveToNewStateTestHook.Value(StoreDatabase.DatabaseState.Offline, null);
			}
			return ErrorCode.NoError;
		}

		// Token: 0x06000C77 RID: 3191 RVA: 0x0003E878 File Offset: 0x0003CA78
		internal void FinalizeDatabaseDismount(Context context)
		{
			DiagnosticContext.TraceDword((LID)55360U, (uint)Environment.TickCount);
			if (this.taskList != null)
			{
				this.taskList.Dispose();
				this.taskList = null;
			}
			try
			{
				this.InvokeDismountingHandler(context);
			}
			finally
			{
				DiagnosticContext.TraceDword((LID)38976U, (uint)Environment.TickCount);
				this.physicalDatabase.Close();
				DiagnosticContext.TraceDword((LID)42048U, (uint)Environment.TickCount);
			}
		}

		// Token: 0x06000C78 RID: 3192 RVA: 0x0003E900 File Offset: 0x0003CB00
		internal void InvokeDismountingHandler(Context context)
		{
			using (context.AssociateWithDatabaseNoLock(this))
			{
				if (this.taskList != null)
				{
					this.taskList.Dispose();
					this.taskList = null;
				}
				StoreDatabase.DismountingHandler(context, this);
				context.Commit();
			}
		}

		// Token: 0x06000C79 RID: 3193 RVA: 0x0003E964 File Offset: 0x0003CB64
		internal void StartBackgroundChecksumming(Context context)
		{
			this.PhysicalDatabase.StartBackgroundChecksumming(context);
		}

		// Token: 0x06000C7A RID: 3194 RVA: 0x0003E972 File Offset: 0x0003CB72
		internal void VersionStoreCleanup(Context context)
		{
			this.PhysicalDatabase.VersionStoreCleanup(context);
		}

		// Token: 0x06000C7B RID: 3195 RVA: 0x0003E980 File Offset: 0x0003CB80
		internal Guid GetDatabaseDsGuid(Context context)
		{
			Microsoft.Exchange.Server.Storage.Common.Globals.AssertRetail(AddDatabaseDsGuidToGlobalsTable.IsReady(context, this), "Schema not ready.");
			GlobalsTable globalsTable = DatabaseSchema.GlobalsTable(this);
			Column[] columnsToFetch = new Column[]
			{
				globalsTable.DatabaseDsGuid
			};
			Guid guid;
			using (TableOperator tableOperator = Factory.CreateTableOperator(CultureHelper.DefaultCultureInfo, context, globalsTable.Table, globalsTable.Table.PrimaryKeyIndex, columnsToFetch, null, null, 0, 1, KeyRange.AllRows, false, true))
			{
				using (Reader reader = tableOperator.ExecuteReader(false))
				{
					reader.Read();
					guid = reader.GetGuid(globalsTable.DatabaseDsGuid);
				}
			}
			return guid;
		}

		// Token: 0x06000C7C RID: 3196 RVA: 0x0003EA3C File Offset: 0x0003CC3C
		internal void SetDatabaseDsGuid(Context context, Guid databaseDsGuid)
		{
			Microsoft.Exchange.Server.Storage.Common.Globals.AssertRetail(AddDatabaseDsGuidToGlobalsTable.IsReady(context, this), "Schema not ready.");
			GlobalsTable globalsTable = DatabaseSchema.GlobalsTable(this);
			Column[] columnsToUpdate = new Column[]
			{
				globalsTable.DatabaseDsGuid
			};
			List<object> list = new List<object>(1);
			list.Add(databaseDsGuid);
			using (UpdateOperator updateOperator = Factory.CreateUpdateOperator(CultureHelper.DefaultCultureInfo, context, Factory.CreateTableOperator(CultureHelper.DefaultCultureInfo, context, globalsTable.Table, globalsTable.Table.PrimaryKeyIndex, null, null, null, 0, 1, KeyRange.AllRows, false, true), columnsToUpdate, list, true))
			{
				int num = (int)updateOperator.ExecuteScalar();
			}
		}

		// Token: 0x06000C7D RID: 3197 RVA: 0x0003EAE8 File Offset: 0x0003CCE8
		internal IDisposable SetDatabaseTestHook(Action action)
		{
			return this.databaseTestHook.SetTestHook(action);
		}

		// Token: 0x06000C7E RID: 3198 RVA: 0x0003EAF6 File Offset: 0x0003CCF6
		internal void PublishHaFailure(FailureTag failureTag)
		{
			this.PhysicalDatabase.PublishHaFailure(failureTag);
		}

		// Token: 0x06000C7F RID: 3199 RVA: 0x0003EB04 File Offset: 0x0003CD04
		private static LockName<Guid> GetDatabaseLockName(Guid databaseGuid)
		{
			return new LockName<Guid>(databaseGuid, LockManager.LockLevel.Database);
		}

		// Token: 0x06000C80 RID: 3200 RVA: 0x0003EB10 File Offset: 0x0003CD10
		private void SetNewState(StoreDatabase.DatabaseState newState)
		{
			if (ExTraceGlobals.StoreDatabaseTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				ExTraceGlobals.StoreDatabaseTracer.TraceDebug<StoreDatabase.DatabaseState, StoreDatabase.DatabaseState>(0L, "SetNewState:{0}, Current State:{1}.", newState, this.state);
			}
			if (StoreDatabase.preMoveToNewStateTestHook.Value != null)
			{
				StoreDatabase.preMoveToNewStateTestHook.Value(this.state, newState);
			}
			switch (newState)
			{
			case StoreDatabase.DatabaseState.Offline:
				Microsoft.Exchange.Server.Storage.Common.Globals.AssertRetail(this.IsTransitioningToOffline || this.IsTransitioningFromOffline, "Unexpected current state transitioning to Offline.");
				break;
			case StoreDatabase.DatabaseState.OnlineActive:
				Microsoft.Exchange.Server.Storage.Common.Globals.AssertRetail(this.IsTransitioningToOnlineActive, "Unexpected current state transitioning to OnlineActive.");
				break;
			case StoreDatabase.DatabaseState.OnlinePassiveAttachedReadOnly:
				Microsoft.Exchange.Server.Storage.Common.Globals.AssertRetail(this.state == StoreDatabase.DatabaseState.OnlinePassiveReplayingLogsToOnlinePassiveAttachedReadOnly, "Unexpected current state transitioning to OnlinePassiveAttachedReadOnly.");
				break;
			case StoreDatabase.DatabaseState.OnlinePassiveReplayingLogs:
				Microsoft.Exchange.Server.Storage.Common.Globals.AssertRetail(this.state == StoreDatabase.DatabaseState.OfflineToOnlinePassiveReplayingLogs || this.state == StoreDatabase.DatabaseState.OnlinePassiveAttachedReadOnlyToOnlinePassiveReplayingLogs, "Unexpected current state transitioning to OnlinePassiveAttachedReadOnly.");
				break;
			case StoreDatabase.DatabaseState.OfflineToOnlineActive:
				Microsoft.Exchange.Server.Storage.Common.Globals.AssertRetail(this.IsOffline, "Unexpected current state before setting to transition state OfflineToOnlineActive.");
				break;
			case StoreDatabase.DatabaseState.OfflineToOnlinePassiveReplayingLogs:
				Microsoft.Exchange.Server.Storage.Common.Globals.AssertRetail(this.IsOffline, "Unexpected current state before setting to transition state OfflineToOnlinePassiveReplayingLogs.");
				break;
			case StoreDatabase.DatabaseState.OnlinePassiveReplayingLogsToOnlineActive:
				Microsoft.Exchange.Server.Storage.Common.Globals.AssertRetail(this.state == StoreDatabase.DatabaseState.OnlinePassiveReplayingLogs, "Unexpected current state before setting to transition state OnlinePassiveReplayingLogsToOnlineActive.");
				break;
			case StoreDatabase.DatabaseState.OnlinePassiveAttachedReadOnlyToOnlineActive:
				Microsoft.Exchange.Server.Storage.Common.Globals.AssertRetail(this.state == StoreDatabase.DatabaseState.OnlinePassiveAttachedReadOnly, "Unexpected current state before setting to transition state OnlinePassiveAttachedReadOnlyToOnlineActive.");
				break;
			case StoreDatabase.DatabaseState.OnlinePassiveReplayingLogsToOnlinePassiveAttachedReadOnly:
				Microsoft.Exchange.Server.Storage.Common.Globals.AssertRetail(this.state == StoreDatabase.DatabaseState.OnlinePassiveReplayingLogs, "Unexpected current state before setting to transition state OnlinePassiveReplayingLogsToOnlinePassiveAttachedReadOnly.");
				break;
			case StoreDatabase.DatabaseState.OnlinePassiveAttachedReadOnlyToOnlinePassiveReplayingLogs:
				Microsoft.Exchange.Server.Storage.Common.Globals.AssertRetail(this.state == StoreDatabase.DatabaseState.OnlinePassiveAttachedReadOnly, "Unexpected current state before setting to transition state OnlinePassiveAttachedReadOnlyToOnlinePassiveReplayingLogs.");
				break;
			case StoreDatabase.DatabaseState.OnlineActiveToOffline:
				Microsoft.Exchange.Server.Storage.Common.Globals.AssertRetail(this.state == StoreDatabase.DatabaseState.OnlineActive, "Unexpected current state before setting to transition state OnlineActiveToOffline.");
				break;
			case StoreDatabase.DatabaseState.OnlinePassiveReplayingLogsToOffline:
				Microsoft.Exchange.Server.Storage.Common.Globals.AssertRetail(this.state == StoreDatabase.DatabaseState.OnlinePassiveReplayingLogs, "Unexpected current state before setting to transition state OnlinePassiveReplayingLogsToOffline.");
				break;
			case StoreDatabase.DatabaseState.OnlinePassiveAttachedReadOnlyToOffline:
				Microsoft.Exchange.Server.Storage.Common.Globals.AssertRetail(this.state == StoreDatabase.DatabaseState.OnlinePassiveAttachedReadOnly, "Unexpected current state before setting to transition state OnlinePassiveAttachedReadOnlyToOffline.");
				break;
			default:
				Microsoft.Exchange.Server.Storage.Common.Globals.AssertRetail(false, "Unexpected database state.");
				break;
			}
			this.state = newState;
		}

		// Token: 0x06000C81 RID: 3201 RVA: 0x0003ECE9 File Offset: 0x0003CEE9
		private void SetStatusFlag(DatabaseStatus flag)
		{
			this.mdbStatus |= flag;
		}

		// Token: 0x06000C82 RID: 3202 RVA: 0x0003ECF9 File Offset: 0x0003CEF9
		private void ResetStatusFlag(DatabaseStatus flag)
		{
			this.mdbStatus &= ~flag;
		}

		// Token: 0x06000C83 RID: 3203 RVA: 0x0003EDD4 File Offset: 0x0003CFD4
		private void MountActive(Context context, bool allowLoss)
		{
			StoreDatabase.<>c__DisplayClass6 CS$<>8__locals1 = new StoreDatabase.<>c__DisplayClass6();
			CS$<>8__locals1.context = context;
			CS$<>8__locals1.<>4__this = this;
			if (ExTraceGlobals.StoreDatabaseTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				ExTraceGlobals.StoreDatabaseTracer.TraceDebug(0L, "Mount the database as active.");
			}
			bool flag = false;
			try
			{
				this.physicalDatabase.Configure();
				using (CS$<>8__locals1.context.AssociateWithDatabaseNoLock(this))
				{
					if (!this.physicalDatabase.TryOpen(allowLoss))
					{
						ILUtil.DoTryFilterCatch<IExecutionDiagnostics>(new TryDelegate(CS$<>8__locals1, (UIntPtr)ldftn(<MountActive>b__0)), new GenericFilterDelegate<IExecutionDiagnostics>(CS$<>8__locals1, (UIntPtr)ldftn(<MountActive>b__1)), new GenericCatchDelegate<IExecutionDiagnostics>(null, (UIntPtr)ldftn(<MountActive>b__2)), CS$<>8__locals1.context.Diagnostics);
					}
					this.FinishMount(CS$<>8__locals1.context, false);
					CS$<>8__locals1.context.Commit();
					CS$<>8__locals1.context.GetConnection().FlushDatabaseLogs(true);
				}
				flag = true;
			}
			finally
			{
				if (!flag)
				{
					this.physicalDatabase.Close();
				}
			}
		}

		// Token: 0x06000C84 RID: 3204 RVA: 0x0003EEF8 File Offset: 0x0003D0F8
		private void FinishMount(Context context, bool readOnly)
		{
			StoreDatabase.InitInMemoryDatabaseSchemaHandler(context, this);
			this.CheckDatabaseVersionAndUpgrade(context, readOnly);
			if (AddDatabaseDsGuidToGlobalsTable.IsReady(context, this))
			{
				Guid databaseDsGuid = this.GetDatabaseDsGuid(context);
				if (!databaseDsGuid.Equals(this.MdbGuid))
				{
					Microsoft.Exchange.Server.Storage.Common.Globals.LogEvent(MSExchangeISEventLogConstants.Tuple_DatabaseGuidPatchRequired, new object[]
					{
						this.MdbGuid,
						databaseDsGuid,
						this.MdbName
					});
					if (!this.allowFileRestore || readOnly)
					{
						Microsoft.Exchange.Server.Storage.Common.Globals.LogEvent(MSExchangeISEventLogConstants.Tuple_DatabaseFileRestoreNotAllowed, new object[]
						{
							this.MdbName
						});
						throw new StoreException((LID)39200U, ErrorCodeValue.DatabaseRolledBack);
					}
					this.SetDatabaseDsGuid(context, this.MdbGuid);
				}
				this.allowFileRestore = false;
			}
			if (!readOnly)
			{
				this.StartBackgroundChecksumming(context);
			}
			this.MountTime = DateTime.UtcNow;
			this.taskList = new TaskList();
			StoreDatabase.MountingHandler(context, this, readOnly);
			this.databaseHeaderInfo = this.PhysicalDatabase.GetDatabaseHeaderInfo(context.GetConnection());
			this.CheckForRepairedDatabase(context);
		}

		// Token: 0x06000C85 RID: 3205 RVA: 0x0003F094 File Offset: 0x0003D294
		private void CheckForRepairedDatabase(Context context)
		{
			bool databaseRepaired = this.databaseHeaderInfo.DatabaseRepaired;
			context.Diagnostics.DatabaseRepaired = new bool?(databaseRepaired);
			if (databaseRepaired)
			{
				if (ExTraceGlobals.StoreDatabaseTracer.IsTraceEnabled(TraceType.DebugTrace))
				{
					ExTraceGlobals.StoreDatabaseTracer.TraceDebug(0L, "WARNING: Database {0} was previously offline-repaired: lastRepairTime={1}, repairCountSinceLastDefrag=[2}, repairCountBeforeLastDefrag={3}", new object[]
					{
						this.MdbName,
						this.databaseHeaderInfo.LastRepairedTime,
						this.databaseHeaderInfo.RepairCountSinceLastDefrag,
						this.databaseHeaderInfo.RepairCountBeforeLastDefrag
					});
				}
				bool isRecoveryDatabase = (this.MdbStatus & DatabaseStatus.ForRecovery) != DatabaseStatus.OffLine;
				RecurringTask<StoreDatabase> task = new RecurringTask<StoreDatabase>(delegate(TaskExecutionDiagnosticsProxy diagnosticsContext, StoreDatabase storeDatabase, Func<bool> shouldCallbackContinue)
				{
					Microsoft.Exchange.Server.Storage.Common.Globals.LogEvent(MSExchangeISEventLogConstants.Tuple_RunningWithRepairedDatabase, new object[]
					{
						storeDatabase.MdbName,
						storeDatabase.MdbGuid,
						storeDatabase.databaseHeaderInfo.LastRepairedTime,
						storeDatabase.databaseHeaderInfo.RepairCountSinceLastDefrag,
						storeDatabase.databaseHeaderInfo.RepairCountBeforeLastDefrag,
						isRecoveryDatabase
					});
				}, this, StoreDatabase.FrequencyForReportingRepairedDatabase);
				this.TaskList.Add(task, true);
			}
		}

		// Token: 0x06000C86 RID: 3206 RVA: 0x0003F170 File Offset: 0x0003D370
		private void PostMountInitialization(Context context)
		{
			if (ExTraceGlobals.StoreDatabaseTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				ExTraceGlobals.StoreDatabaseTracer.TraceDebug(0L, "Execute PostMountInitialization");
			}
			this.GetSharedLock(context.Diagnostics);
			try
			{
				if (this.IsOnlineActive || this.IsOnlinePassiveAttachedReadOnly)
				{
					using (context.AssociateWithDatabaseNoLock(this))
					{
						StoreDatabase.MountedHandler(context, this);
						context.Commit();
					}
				}
			}
			finally
			{
				this.ReleaseSharedLock();
			}
		}

		// Token: 0x06000C87 RID: 3207 RVA: 0x0003F208 File Offset: 0x0003D408
		private void DismountActive(Context context)
		{
			if (ExTraceGlobals.StoreDatabaseTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				ExTraceGlobals.StoreDatabaseTracer.TraceDebug(0L, "Dismount the active database.");
			}
			this.FinalizeDatabaseDismount(context);
		}

		// Token: 0x06000C88 RID: 3208 RVA: 0x0003F230 File Offset: 0x0003D430
		private void CreateGlobalsTableRow(IConnectionProvider connectionProvider)
		{
			GlobalsTable globalsTable = new GlobalsTable();
			List<ColumnValue> list = new List<ColumnValue>(10);
			int value;
			if (this.PhysicalDatabase.DatabaseType == DatabaseType.Jet)
			{
				value = StoreDatabase.GetMinimumSchemaVersion().Value;
			}
			else
			{
				value = StoreDatabase.GetMaximumSchemaVersion().Value;
				list.Add(new ColumnValue(globalsTable.DatabaseDsGuid, this.MdbGuid));
				list.Add(new ColumnValue(globalsTable.EventCounterLowerBound, 0L));
				list.Add(new ColumnValue(globalsTable.EventCounterUpperBound, 1L));
			}
			list.Add(new ColumnValue(globalsTable.DatabaseVersion, value));
			list.Add(new ColumnValue(globalsTable.VersionName, "Exchange"));
			list.Add(new ColumnValue(globalsTable.Inid, 0L));
			list.Add(new ColumnValue(globalsTable.LastMaintenanceTask, 0));
			using (DataRow dataRow = Factory.CreateDataRow(CultureHelper.DefaultCultureInfo, connectionProvider, globalsTable.Table, true, list.ToArray()))
			{
				dataRow.Flush(connectionProvider);
			}
		}

		// Token: 0x06000C89 RID: 3209 RVA: 0x0003F35C File Offset: 0x0003D55C
		private void MountPassive(Context context)
		{
			if (ExTraceGlobals.StoreDatabaseTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				ExTraceGlobals.StoreDatabaseTracer.TraceDebug(0L, "Mount the database as passive");
			}
			this.ResetPassiveDatabaseAttachDetach();
			this.replayThreadMayAttachPassiveDatabaseEventHandle = new ManualResetEvent(false);
			this.physicalDatabase.Configure();
			using (context.AssociateWithDatabaseNoLock(this))
			{
				this.physicalDatabase.StartLogReplay(new Func<bool, bool>(this.PassiveDatabaseAttachDetachHandler));
				context.Commit();
			}
			if (Microsoft.Exchange.Server.Storage.Common.Globals.IsMultiProcess)
			{
				PerformanceCounterFactory.CreateDatabaseInstance(this);
			}
		}

		// Token: 0x06000C8A RID: 3210 RVA: 0x0003F3F8 File Offset: 0x0003D5F8
		private void ActivatePassive(Context context)
		{
			if (ExTraceGlobals.StoreDatabaseTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				ExTraceGlobals.StoreDatabaseTracer.TraceDebug(0L, "Mount a passive database as active.");
			}
			bool flag = false;
			try
			{
				this.physicalDatabase.FinishLogReplay();
				this.ResetPassiveDatabaseAttachDetach();
				using (context.AssociateWithDatabaseNoLock(this))
				{
					this.FinishMount(context, false);
					context.Commit();
				}
				flag = true;
			}
			finally
			{
				if (!flag)
				{
					this.physicalDatabase.Close();
				}
			}
		}

		// Token: 0x06000C8B RID: 3211 RVA: 0x0003F48C File Offset: 0x0003D68C
		private void DismountPassive(Context context)
		{
			if (ExTraceGlobals.StoreDatabaseTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				ExTraceGlobals.StoreDatabaseTracer.TraceDebug(0L, "Dismount the passive database.");
			}
			this.physicalDatabase.CancelLogReplay();
			this.ResetPassiveDatabaseAttachDetach();
			this.FinalizeDatabaseDismount(context);
		}

		// Token: 0x06000C8C RID: 3212 RVA: 0x0003F4C4 File Offset: 0x0003D6C4
		private void ResetPassiveDatabaseAttachDetach()
		{
			this.replayThreadMayAttachPassiveDatabase = false;
			if (this.replayThreadMayAttachPassiveDatabaseEventHandle != null)
			{
				this.replayThreadMayAttachPassiveDatabaseEventHandle.Dispose();
				this.replayThreadMayAttachPassiveDatabaseEventHandle = null;
			}
			if (this.foregroundThreadForceDetachingPassiveDatabaseEventHandle != null)
			{
				this.foregroundThreadForceDetachingPassiveDatabaseEventHandle.Dispose();
				this.foregroundThreadForceDetachingPassiveDatabaseEventHandle = null;
			}
		}

		// Token: 0x06000C8D RID: 3213 RVA: 0x0003F504 File Offset: 0x0003D704
		private bool PassiveDatabaseAttachDetachHandler(bool attachDatabase)
		{
			bool result;
			try
			{
				result = (attachDatabase ? this.PassiveDatabaseAttachHandler() : this.PassiveDatabaseDetachHandler());
			}
			catch (Exception ex)
			{
				NullExecutionDiagnostics.Instance.OnExceptionCatch(ex);
				this.HandlePassiveDatabaseAttachDetachException(ex);
				throw;
			}
			return result;
		}

		// Token: 0x06000C8E RID: 3214 RVA: 0x0003F54C File Offset: 0x0003D74C
		private void HandlePassiveDatabaseAttachDetachException(Exception e)
		{
			if (!this.IsExclusiveLockHeld())
			{
				this.GetExclusiveLock();
			}
			StoreDatabase.DatabaseState databaseState = this.state;
			this.state = StoreDatabase.DatabaseState.OnlinePassiveReplayingLogs;
			this.ReleaseExclusiveLock();
			if (ExTraceGlobals.StoreDatabaseTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				ExTraceGlobals.StoreDatabaseTracer.TraceDebug<StoreDatabase.DatabaseState, Exception>(0L, "Exception raised while attaching or detaching the passive database: Current state: {0}, Exception: {1}", databaseState, e);
			}
			Microsoft.Exchange.Server.Storage.Common.Globals.LogEvent(MSExchangeISEventLogConstants.Tuple_PassiveDatabaseAttachDetachException, new object[]
			{
				this.MdbName,
				this.MdbGuid,
				databaseState,
				e
			});
		}

		// Token: 0x06000C8F RID: 3215 RVA: 0x0003F5D4 File Offset: 0x0003D7D4
		private bool PassiveDatabaseAttachHandler()
		{
			bool flag = false;
			bool result = false;
			this.GetExclusiveLock();
			if (ExTraceGlobals.StoreDatabaseTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				ExTraceGlobals.StoreDatabaseTracer.TraceDebug<StoreDatabase.DatabaseState>(0L, "Recovery callback invoked to mount the passive database for read-only access (current state: {0}).", this.state);
			}
			if (!this.replayThreadMayAttachPassiveDatabase)
			{
				this.ReleaseExclusiveLock();
				bool assertCondition = this.replayThreadMayAttachPassiveDatabaseEventHandle.WaitOne(TimeSpan.FromMinutes(1.0));
				Microsoft.Exchange.Server.Storage.Common.Globals.AssertRetail(assertCondition, "Waited an abnormally long time for the foreground mount thread to set the database into the OnlinePassiveReplayingLogs state.");
				this.GetExclusiveLock();
			}
			if (this.IsOnlinePassiveReplayingLogs)
			{
				if (this.replayThreadMayAttachPassiveDatabaseEventHandle != null)
				{
					this.replayThreadMayAttachPassiveDatabaseEventHandle.Dispose();
					this.replayThreadMayAttachPassiveDatabaseEventHandle = null;
				}
				if (this.physicalDatabase.CheckTableExists("Globals"))
				{
					this.SetNewState(StoreDatabase.DatabaseState.OnlinePassiveReplayingLogsToOnlinePassiveAttachedReadOnly);
					flag = true;
				}
				else if (ExTraceGlobals.StoreDatabaseTracer.IsTraceEnabled(TraceType.DebugTrace))
				{
					ExTraceGlobals.StoreDatabaseTracer.TraceDebug(0L, "Cannot attach to passive database because the database does not contain all initial schema objects yet.");
				}
			}
			this.ReleaseExclusiveLock();
			if (flag)
			{
				uint currentLogReplayGeneration = this.GetCurrentLogReplayGeneration();
				if (StoreDatabase.postMoveToNewStateTestHook.Value != null)
				{
					StoreDatabase.postMoveToNewStateTestHook.Value(StoreDatabase.DatabaseState.OnlinePassiveReplayingLogsToOnlinePassiveAttachedReadOnly, currentLogReplayGeneration);
				}
				using (Context context = Context.CreateForSystem())
				{
					using (context.AssociateWithDatabaseNoLock(this))
					{
						this.FinishMount(context, true);
						context.Commit();
					}
					this.GetExclusiveLock();
					this.SetNewState(StoreDatabase.DatabaseState.OnlinePassiveAttachedReadOnly);
					this.ReleaseExclusiveLock();
					if (StoreDatabase.postMoveToNewStateTestHook.Value != null)
					{
						StoreDatabase.postMoveToNewStateTestHook.Value(StoreDatabase.DatabaseState.OnlinePassiveAttachedReadOnly, this.GetCurrentLogReplayGeneration());
					}
					this.PostMountInitialization(context);
				}
				result = true;
				Microsoft.Exchange.Server.Storage.Common.Globals.LogEvent(MSExchangeISEventLogConstants.Tuple_PassiveDatabaseAttachedReadOnly, new object[]
				{
					this.MdbName,
					this.MdbGuid,
					string.Format("0x{0:X}", currentLogReplayGeneration)
				});
				if (StoreDatabase.passiveAttachedDetachedTestHook.Value != null)
				{
					StoreDatabase.passiveAttachedDetachedTestHook.Value(true, this.GetCurrentLogReplayGeneration());
				}
			}
			return result;
		}

		// Token: 0x06000C90 RID: 3216 RVA: 0x0003F7E0 File Offset: 0x0003D9E0
		private bool PassiveDatabaseDetachHandler()
		{
			bool flag = false;
			bool result = false;
			this.GetExclusiveLock();
			if (ExTraceGlobals.StoreDatabaseTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				ExTraceGlobals.StoreDatabaseTracer.TraceDebug<StoreDatabase.DatabaseState>(0L, "Recovery callback invoked to dismount the read-only passive database (current state: {0}).", this.state);
			}
			if (this.foregroundThreadForceDetachingPassiveDatabaseEventHandle != null)
			{
				this.ReleaseExclusiveLock();
				bool assertCondition = this.foregroundThreadForceDetachingPassiveDatabaseEventHandle.WaitOne(TimeSpan.FromMinutes(1.0));
				Microsoft.Exchange.Server.Storage.Common.Globals.AssertRetail(assertCondition, "Waited an abnormally long time for the foreground thread to force-detach the passive database.");
				if (ExTraceGlobals.StoreDatabaseTracer.IsTraceEnabled(TraceType.DebugTrace))
				{
					ExTraceGlobals.StoreDatabaseTracer.TraceDebug(0L, "Recovery callback cannot dismount the read-only passive database because it has already been force-detached.");
				}
				return true;
			}
			if (this.IsOnlinePassiveAttachedReadOnly)
			{
				this.SetNewState(StoreDatabase.DatabaseState.OnlinePassiveAttachedReadOnlyToOnlinePassiveReplayingLogs);
				flag = true;
			}
			this.ReleaseExclusiveLock();
			if (flag)
			{
				uint currentLogReplayGeneration = this.GetCurrentLogReplayGeneration();
				if (StoreDatabase.postMoveToNewStateTestHook.Value != null)
				{
					StoreDatabase.postMoveToNewStateTestHook.Value(StoreDatabase.DatabaseState.OnlinePassiveAttachedReadOnlyToOnlinePassiveReplayingLogs, currentLogReplayGeneration);
				}
				using (Context context = Context.CreateForSystem())
				{
					this.InvokeDismountingHandler(context);
				}
				this.GetExclusiveLock();
				this.SetNewState(StoreDatabase.DatabaseState.OnlinePassiveReplayingLogs);
				this.ReleaseExclusiveLock();
				if (StoreDatabase.postMoveToNewStateTestHook.Value != null)
				{
					StoreDatabase.postMoveToNewStateTestHook.Value(StoreDatabase.DatabaseState.OnlinePassiveReplayingLogs, this.GetCurrentLogReplayGeneration());
				}
				Microsoft.Exchange.Server.Storage.Common.Globals.LogEvent(MSExchangeISEventLogConstants.Tuple_PassiveDatabaseDetached, new object[]
				{
					this.MdbName,
					this.MdbGuid,
					string.Format("0x{0:X}", currentLogReplayGeneration)
				});
				if (StoreDatabase.passiveAttachedDetachedTestHook.Value != null)
				{
					StoreDatabase.passiveAttachedDetachedTestHook.Value(false, this.GetCurrentLogReplayGeneration());
				}
			}
			return result;
		}

		// Token: 0x06000C91 RID: 3217 RVA: 0x0003F984 File Offset: 0x0003DB84
		private void PermitReplayThreadAttachPassiveDatabase()
		{
			if (ExTraceGlobals.StoreDatabaseTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				ExTraceGlobals.StoreDatabaseTracer.TraceDebug(0L, "Signalling log replay thread that the passive database was transitioned to the PassiveReplayingLogs state.");
			}
			this.replayThreadMayAttachPassiveDatabase = true;
			bool assertCondition = this.replayThreadMayAttachPassiveDatabaseEventHandle.Set();
			Microsoft.Exchange.Server.Storage.Common.Globals.AssertRetail(assertCondition, "Setting the event must succeed.");
			if (StoreDatabase.permitReplayThreadAttachPassiveDatabaseTestHook.Value != null)
			{
				StoreDatabase.permitReplayThreadAttachPassiveDatabaseTestHook.Value(this);
			}
		}

		// Token: 0x06000C92 RID: 3218 RVA: 0x0003F9E9 File Offset: 0x0003DBE9
		private void StartForceDetachPassiveDatabase()
		{
			this.foregroundThreadForceDetachingPassiveDatabaseEventHandle = new ManualResetEvent(false);
		}

		// Token: 0x06000C93 RID: 3219 RVA: 0x0003F9F8 File Offset: 0x0003DBF8
		private void FinishForceDetachPassiveDatabase(Context context)
		{
			if (ExTraceGlobals.StoreDatabaseTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				ExTraceGlobals.StoreDatabaseTracer.TraceDebug(0L, "Force-detaching the passive database.");
			}
			this.InvokeDismountingHandler(context);
			if (StoreDatabase.passiveAttachedDetachedTestHook.Value != null)
			{
				StoreDatabase.passiveAttachedDetachedTestHook.Value(false, this.GetCurrentLogReplayGeneration());
			}
			bool assertCondition = this.foregroundThreadForceDetachingPassiveDatabaseEventHandle.Set();
			Microsoft.Exchange.Server.Storage.Common.Globals.AssertRetail(assertCondition, "Setting the signal must succeed.");
		}

		// Token: 0x06000C94 RID: 3220 RVA: 0x0003FA64 File Offset: 0x0003DC64
		private bool WaitForTransitionBetweenOnlinePassiveStates()
		{
			if (ExTraceGlobals.StoreDatabaseTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				ExTraceGlobals.StoreDatabaseTracer.TraceDebug(0L, "Starting to wait for the database to complete a transition between online passive states.");
			}
			if (StoreDatabase.waitForTransitionBetweenOnlinePassiveStatesTestHook.Value != null)
			{
				StoreDatabase.waitForTransitionBetweenOnlinePassiveStatesTestHook.Value();
			}
			int num = 0;
			while (this.IsTransitioningBetweenOnlinePassiveStates)
			{
				num++;
				bool flag = StoreDatabase.simulateTimeoutWaitingForTransitionBetweenOnlinePassiveStatesTestHook.Value(num);
				if (num > 2000 || flag)
				{
					if (ExTraceGlobals.StoreDatabaseTracer.IsTraceEnabled(TraceType.DebugTrace))
					{
						ExTraceGlobals.StoreDatabaseTracer.TraceDebug<StoreDatabase.DatabaseState>(0L, "Failed waiting because the online passive state transition appeared to be taking an abnormally long time (current state: {0}).", this.state);
					}
					return false;
				}
				this.ReleaseExclusiveLock();
				Thread.Sleep(TimeSpan.FromMilliseconds(10.0));
				this.GetExclusiveLock();
			}
			if (ExTraceGlobals.StoreDatabaseTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				ExTraceGlobals.StoreDatabaseTracer.TraceDebug<int>(0L, "Successfully finished waiting for the database to complete a transition between online passive states (retryCount=={0}).", num);
			}
			return true;
		}

		// Token: 0x06000C95 RID: 3221 RVA: 0x0003FB3C File Offset: 0x0003DD3C
		private uint GetCurrentLogReplayGeneration()
		{
			uint result;
			byte[] array;
			uint num;
			byte[] array2;
			byte[] array3;
			uint[] array4;
			this.physicalDatabase.LogReplayStatus.GetReplayStatus(out result, out array, out num, out array2, out array3, out array4);
			return result;
		}

		// Token: 0x06000C96 RID: 3222 RVA: 0x0003FB68 File Offset: 0x0003DD68
		internal void CheckDatabaseVersionAndUpgrade(Context context, bool readOnly)
		{
			ComponentVersion minimumSchemaVersion = StoreDatabase.GetMinimumSchemaVersion();
			ComponentVersion maximumSchemaVersion = StoreDatabase.GetMaximumSchemaVersion();
			ComponentVersion componentVersion = this.GetCurrentSchemaVersion(context);
			if (componentVersion.Value < minimumSchemaVersion.Value)
			{
				this.PublishHaFailure(FailureTag.Configuration);
				Microsoft.Exchange.Server.Storage.Common.Globals.LogEvent(MSExchangeISEventLogConstants.Tuple_DatabaseVersionTooOld, new object[]
				{
					this.MdbGuid,
					componentVersion,
					minimumSchemaVersion
				});
				throw new DatabaseBadVersion((LID)55584U, this.MdbGuid, minimumSchemaVersion, componentVersion);
			}
			if (componentVersion.Value > maximumSchemaVersion.Value)
			{
				this.PublishHaFailure(FailureTag.Configuration);
				Microsoft.Exchange.Server.Storage.Common.Globals.LogEvent(MSExchangeISEventLogConstants.Tuple_DatabaseVersionTooNew, new object[]
				{
					this.MdbGuid,
					componentVersion,
					maximumSchemaVersion
				});
				throw new DatabaseBadVersion((LID)50464U, this.MdbGuid, maximumSchemaVersion, componentVersion);
			}
			ComponentVersion requestedSchemaVersion = this.GetRequestedSchemaVersion(context, componentVersion, maximumSchemaVersion);
			if (requestedSchemaVersion.Value < componentVersion.Value || requestedSchemaVersion.Value > maximumSchemaVersion.Value)
			{
				Microsoft.Exchange.Server.Storage.Common.Globals.LogEvent(MSExchangeISEventLogConstants.Tuple_DatabaseBadRequestedUpdgradeVersion, new object[]
				{
					this.MdbGuid,
					requestedSchemaVersion,
					componentVersion,
					maximumSchemaVersion
				});
				return;
			}
			if (readOnly)
			{
				return;
			}
			SchemaUpgradeService.Upgrade(context, this, SchemaUpgradeService.SchemaCategory.Database, requestedSchemaVersion);
		}

		// Token: 0x040006C3 RID: 1731
		private const string RequestedVersion = "Requested Version";

		// Token: 0x040006C4 RID: 1732
		private static readonly TimeSpan FrequencyForReportingRepairedDatabase = TimeSpan.FromHours(1.0);

		// Token: 0x040006C5 RID: 1733
		private static Hookable<ComponentVersion> maximumSchemaVersionTestHook = Hookable<ComponentVersion>.Create(true, (Factory.GetDatabaseType() == DatabaseType.Jet) ? DefaultSettings.Get.MaximumSupportableDatabaseSchemaVersion : new ComponentVersion(int.MaxValue));

		// Token: 0x040006C6 RID: 1734
		private static readonly Hookable<Action<bool, uint>> passiveAttachedDetachedTestHook = Hookable<Action<bool, uint>>.Create(true, null);

		// Token: 0x040006C7 RID: 1735
		private static readonly Hookable<Action<StoreDatabase>> permitReplayThreadAttachPassiveDatabaseTestHook = Hookable<Action<StoreDatabase>>.Create(true, null);

		// Token: 0x040006C8 RID: 1736
		private static Hookable<ComponentVersion> minimumSchemaVersionTestHook = Hookable<ComponentVersion>.Create(true, new ComponentVersion(0, 121));

		// Token: 0x040006C9 RID: 1737
		private static Hookable<Action> dismountRequestedStopTasksTestHook = Hookable<Action>.Create(true, null);

		// Token: 0x040006CA RID: 1738
		private static readonly Hookable<Func<bool, bool>> isDatabaseEngineBusyTestHook = Hookable<Func<bool, bool>>.Create(true, null);

		// Token: 0x040006CB RID: 1739
		private static Hookable<Action<StoreDatabase.DatabaseState, StoreDatabase.DatabaseState>> preMoveToNewStateTestHook = Hookable<Action<StoreDatabase.DatabaseState, StoreDatabase.DatabaseState>>.Create(true, null);

		// Token: 0x040006CC RID: 1740
		private static Hookable<Action<StoreDatabase.DatabaseState, object>> postMoveToNewStateTestHook = Hookable<Action<StoreDatabase.DatabaseState, object>>.Create(true, null);

		// Token: 0x040006CD RID: 1741
		private static Hookable<Action> waitForTransitionBetweenOnlinePassiveStatesTestHook = Hookable<Action>.Create(true, null);

		// Token: 0x040006CE RID: 1742
		private static Hookable<Func<int, bool>> simulateTimeoutWaitingForTransitionBetweenOnlinePassiveStatesTestHook = Hookable<Func<int, bool>>.Create(true, (int retryCount) => false);

		// Token: 0x040006CF RID: 1743
		private static Hookable<Action> databaseCreationTestHook = Hookable<Action>.Create(true, null);

		// Token: 0x040006D0 RID: 1744
		private readonly Database physicalDatabase;

		// Token: 0x040006D1 RID: 1745
		private readonly LockName<Guid> lockName;

		// Token: 0x040006D2 RID: 1746
		private readonly Guid dagOrServerGuid;

		// Token: 0x040006D3 RID: 1747
		private readonly string serverName;

		// Token: 0x040006D4 RID: 1748
		private readonly string legacyDN;

		// Token: 0x040006D5 RID: 1749
		private readonly string description;

		// Token: 0x040006D6 RID: 1750
		private readonly TimeSpan eventHistoryRetentionPeriod;

		// Token: 0x040006D7 RID: 1751
		private readonly bool hostServerIsDAGMember;

		// Token: 0x040006D8 RID: 1752
		private ComponentVersion? currentSchemaVersion;

		// Token: 0x040006D9 RID: 1753
		private TaskList taskList;

		// Token: 0x040006DA RID: 1754
		private StoreDatabase.DatabaseState state;

		// Token: 0x040006DB RID: 1755
		private DatabaseStatus mdbStatus;

		// Token: 0x040006DC RID: 1756
		private StoreDatabase.ComponentDataStorage componentDataStorage;

		// Token: 0x040006DD RID: 1757
		private DateTime mountTime;

		// Token: 0x040006DE RID: 1758
		private Hookable<Action> databaseTestHook = Hookable<Action>.Create(true, null);

		// Token: 0x040006DF RID: 1759
		private int exclusiveLockContentionCounter;

		// Token: 0x040006E0 RID: 1760
		private ResourceMonitorDigest resourceDigest;

		// Token: 0x040006E1 RID: 1761
		private ServerEditionType serverEdition;

		// Token: 0x040006E2 RID: 1762
		private bool allowFileRestore;

		// Token: 0x040006E3 RID: 1763
		private readonly string forestName;

		// Token: 0x040006E4 RID: 1764
		private DatabaseHeaderInfo databaseHeaderInfo;

		// Token: 0x040006E5 RID: 1765
		private EventWaitHandle replayThreadMayAttachPassiveDatabaseEventHandle;

		// Token: 0x040006E6 RID: 1766
		private bool replayThreadMayAttachPassiveDatabase;

		// Token: 0x040006E7 RID: 1767
		private EventWaitHandle foregroundThreadForceDetachingPassiveDatabaseEventHandle;

		// Token: 0x0200013C RID: 316
		// (Invoke) Token: 0x06000C9B RID: 3227
		public delegate void InitInMemoryDatabaseSchemaHandlerDelegate(Context context, StoreDatabase database);

		// Token: 0x0200013D RID: 317
		// (Invoke) Token: 0x06000C9F RID: 3231
		public delegate void MountingHandlerDelegate(Context context, StoreDatabase database, bool readOnly);

		// Token: 0x0200013E RID: 318
		// (Invoke) Token: 0x06000CA3 RID: 3235
		public delegate void MountedHandlerDelegate(Context context, StoreDatabase database);

		// Token: 0x0200013F RID: 319
		// (Invoke) Token: 0x06000CA7 RID: 3239
		public delegate void DismountingHandlerDelegate(Context context, StoreDatabase database);

		// Token: 0x02000140 RID: 320
		[Flags]
		internal enum MountOperation
		{
			// Token: 0x040006F1 RID: 1777
			None = 0,
			// Token: 0x040006F2 RID: 1778
			MountActive = 1,
			// Token: 0x040006F3 RID: 1779
			MountPassive = 2,
			// Token: 0x040006F4 RID: 1780
			ActivatePassive = 3
		}

		// Token: 0x02000141 RID: 321
		[Flags]
		internal enum DismountOperation
		{
			// Token: 0x040006F6 RID: 1782
			None = 0,
			// Token: 0x040006F7 RID: 1783
			DismountActive = 1,
			// Token: 0x040006F8 RID: 1784
			DismountPassive = 2
		}

		// Token: 0x02000142 RID: 322
		public enum DatabaseState
		{
			// Token: 0x040006FA RID: 1786
			Offline,
			// Token: 0x040006FB RID: 1787
			OnlineActive,
			// Token: 0x040006FC RID: 1788
			OnlinePassiveAttachedReadOnly,
			// Token: 0x040006FD RID: 1789
			OnlinePassiveReplayingLogs,
			// Token: 0x040006FE RID: 1790
			OfflineToOnlineActive,
			// Token: 0x040006FF RID: 1791
			OfflineToOnlinePassiveReplayingLogs,
			// Token: 0x04000700 RID: 1792
			OnlinePassiveReplayingLogsToOnlineActive,
			// Token: 0x04000701 RID: 1793
			OnlinePassiveAttachedReadOnlyToOnlineActive,
			// Token: 0x04000702 RID: 1794
			OnlinePassiveReplayingLogsToOnlinePassiveAttachedReadOnly,
			// Token: 0x04000703 RID: 1795
			OnlinePassiveAttachedReadOnlyToOnlinePassiveReplayingLogs,
			// Token: 0x04000704 RID: 1796
			OnlineActiveToOffline,
			// Token: 0x04000705 RID: 1797
			OnlinePassiveReplayingLogsToOffline,
			// Token: 0x04000706 RID: 1798
			OnlinePassiveAttachedReadOnlyToOffline
		}

		// Token: 0x02000143 RID: 323
		public class ComponentDataStorage : ComponentDataStorageBase
		{
			// Token: 0x06000CAA RID: 3242 RVA: 0x0003FDB7 File Offset: 0x0003DFB7
			public ComponentDataStorage(StoreDatabase database)
			{
				this.database = database;
			}

			// Token: 0x1700035A RID: 858
			// (get) Token: 0x06000CAB RID: 3243 RVA: 0x0003FDC6 File Offset: 0x0003DFC6
			// (set) Token: 0x06000CAC RID: 3244 RVA: 0x0003FDCD File Offset: 0x0003DFCD
			public static bool SkipDatabaseStateValidation
			{
				get
				{
					return StoreDatabase.ComponentDataStorage.skipDatabaseStateValidation;
				}
				set
				{
					StoreDatabase.ComponentDataStorage.skipDatabaseStateValidation = value;
				}
			}

			// Token: 0x1700035B RID: 859
			public new object this[int slotNumber]
			{
				get
				{
					return base[slotNumber];
				}
				set
				{
					base[slotNumber] = value;
				}
			}

			// Token: 0x06000CAF RID: 3247 RVA: 0x0003FDE8 File Offset: 0x0003DFE8
			internal static int AllocateSlot()
			{
				return Interlocked.Increment(ref StoreDatabase.ComponentDataStorage.nextAvailableSlot) - 1;
			}

			// Token: 0x1700035C RID: 860
			// (get) Token: 0x06000CB0 RID: 3248 RVA: 0x0003FDF6 File Offset: 0x0003DFF6
			internal override int SlotCount
			{
				get
				{
					return StoreDatabase.ComponentDataStorage.nextAvailableSlot;
				}
			}

			// Token: 0x04000707 RID: 1799
			private static int nextAvailableSlot;

			// Token: 0x04000708 RID: 1800
			private static bool skipDatabaseStateValidation;

			// Token: 0x04000709 RID: 1801
			private StoreDatabase database;
		}
	}
}
