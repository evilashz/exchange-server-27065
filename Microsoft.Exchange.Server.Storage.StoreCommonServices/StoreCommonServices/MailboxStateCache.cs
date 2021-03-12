using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;
using Microsoft.Exchange.Server.Storage.PropTags;
using Microsoft.Exchange.Server.Storage.StoreCommonServices.DatabaseUpgraders;

namespace Microsoft.Exchange.Server.Storage.StoreCommonServices
{
	// Token: 0x020000BC RID: 188
	public class MailboxStateCache : IStoreDatabaseQueryTarget<MailboxStateCache.QueryableMailboxState>, IStoreQueryTargetBase<MailboxStateCache.QueryableMailboxState>
	{
		// Token: 0x06000790 RID: 1936 RVA: 0x00024FE4 File Offset: 0x000231E4
		private MailboxStateCache(StoreDatabase database)
		{
			this.database = database;
			this.mailboxNumberDictionary = new Dictionary<int, MailboxState>(500);
			this.mailboxGuidDictionary = new Dictionary<Guid, MailboxState>(500);
			this.unifiedMailboxGuidDictionary = new Dictionary<Guid, MailboxState.UnifiedMailboxState>(100);
			this.deletedMailboxes = new HashSet<int>();
			this.postMailboxDisposeList = new List<MailboxState>(500);
			this.perfInstance = StorePerDatabasePerformanceCounters.GetInstance(database.MdbName);
			this.activeMailboxesEvictionPolicy = new LRU2WithTimeToLiveExpirationPolicy<int>(MailboxStateCache.expectedNumberOfActiveMailboxes.Value, MailboxStateCache.activeMailboxCacheTimeToLive, false);
		}

		// Token: 0x170001E3 RID: 483
		// (get) Token: 0x06000791 RID: 1937 RVA: 0x00025085 File Offset: 0x00023285
		public StoreDatabase Database
		{
			get
			{
				return this.database;
			}
		}

		// Token: 0x170001E4 RID: 484
		// (get) Token: 0x06000792 RID: 1938 RVA: 0x0002508D File Offset: 0x0002328D
		private Dictionary<int, MailboxState> MailboxNumberDictionary
		{
			get
			{
				return this.mailboxNumberDictionary;
			}
		}

		// Token: 0x170001E5 RID: 485
		// (get) Token: 0x06000793 RID: 1939 RVA: 0x00025095 File Offset: 0x00023295
		private Dictionary<Guid, MailboxState> MailboxGuidDictionary
		{
			get
			{
				return this.mailboxGuidDictionary;
			}
		}

		// Token: 0x170001E6 RID: 486
		// (get) Token: 0x06000794 RID: 1940 RVA: 0x0002509D File Offset: 0x0002329D
		private Dictionary<Guid, MailboxState.UnifiedMailboxState> UnifiedMailboxGuidDictionary
		{
			get
			{
				return this.unifiedMailboxGuidDictionary;
			}
		}

		// Token: 0x170001E7 RID: 487
		// (get) Token: 0x06000795 RID: 1941 RVA: 0x000250A5 File Offset: 0x000232A5
		private object LockObject
		{
			get
			{
				return this.lockObject;
			}
		}

		// Token: 0x06000796 RID: 1942 RVA: 0x000250B0 File Offset: 0x000232B0
		public static List<int> GetActiveMailboxNumbers(Context context)
		{
			MailboxStateCache mailboxStateCache = (MailboxStateCache)context.Database.ComponentData[MailboxStateCache.cacheSlot];
			if (mailboxStateCache != null)
			{
				List<int> list = new List<int>(1000);
				using (LockManager.Lock(mailboxStateCache.LockObject, LockManager.LockType.MailboxStateCache, context.Diagnostics))
				{
					foreach (KeyValuePair<int, MailboxState> keyValuePair in mailboxStateCache.mailboxNumberDictionary)
					{
						if (keyValuePair.Value.CurrentlyActive)
						{
							list.Add(keyValuePair.Key);
						}
					}
				}
				return list;
			}
			return null;
		}

		// Token: 0x06000797 RID: 1943 RVA: 0x00025174 File Offset: 0x00023374
		public static MailboxState Get(Context context, int mailboxNumber)
		{
			MailboxStateCache mailboxStateCache = (MailboxStateCache)context.Database.ComponentData[MailboxStateCache.cacheSlot];
			MailboxState result;
			using (LockManager.Lock(mailboxStateCache.LockObject, LockManager.LockType.MailboxStateCache, context.Diagnostics))
			{
				if (mailboxStateCache.MailboxNumberDictionary.TryGetValue(mailboxNumber, out result))
				{
					return result;
				}
			}
			if (mailboxStateCache.TryLoadMailboxState(context, null, new int?(mailboxNumber), out result))
			{
				return result;
			}
			return null;
		}

		// Token: 0x06000798 RID: 1944 RVA: 0x00025204 File Offset: 0x00023404
		public static bool TryGetLocked(Context context, int mailboxNumber, bool sharedLock, Func<MailboxState, TimeSpan> timeoutFunc, ILockStatistics lockStats, out bool timeoutReached, out MailboxState lockedMailboxState)
		{
			lockedMailboxState = null;
			timeoutReached = false;
			MailboxState mailboxState = MailboxStateCache.Get(context, mailboxNumber);
			if (mailboxState == null)
			{
				return false;
			}
			if (!mailboxState.TryGetMailboxLock(sharedLock, timeoutFunc(mailboxState), lockStats))
			{
				timeoutReached = true;
				return false;
			}
			if (!mailboxState.IsValid)
			{
				MailboxState mailboxState2 = MailboxStateCache.Get(context, mailboxNumber);
				if (mailboxState2 == null)
				{
					mailboxState.ReleaseMailboxLock(sharedLock);
					return false;
				}
				mailboxState = mailboxState2;
			}
			lockedMailboxState = mailboxState;
			return true;
		}

		// Token: 0x06000799 RID: 1945 RVA: 0x00025268 File Offset: 0x00023468
		public static MailboxState DangerousGetByGuidForTest(Context context, Guid mailboxGuid, bool createIfNotFound)
		{
			return MailboxStateCache.DangerousGetByGuidForTest(context, mailboxGuid, createIfNotFound ? MailboxCreation.Allow(null) : MailboxCreation.DontAllow);
		}

		// Token: 0x0600079A RID: 1946 RVA: 0x00025294 File Offset: 0x00023494
		public static MailboxState DangerousGetByGuidForTest(Context context, Guid mailboxGuid, MailboxCreation mailboxCreation)
		{
			bool flag = !context.Database.IsSharedLockHeld() && !context.Database.IsExclusiveLockHeld();
			MailboxState byGuid;
			try
			{
				if (flag)
				{
					context.Database.GetSharedLock();
				}
				byGuid = MailboxStateCache.GetByGuid(context, mailboxGuid, mailboxCreation);
			}
			finally
			{
				if (flag)
				{
					context.Database.ReleaseSharedLock();
				}
			}
			return byGuid;
		}

		// Token: 0x0600079B RID: 1947 RVA: 0x000252FC File Offset: 0x000234FC
		public static bool TryGetByGuidLocked(Context context, Guid mailboxGuid, MailboxCreation mailboxCreation, bool findRemovedMailbox, bool sharedLock, Func<MailboxState, TimeSpan> timeoutFunc, ILockStatistics lockStats, out bool timeoutReached, out MailboxState lockedMailboxState)
		{
			lockedMailboxState = null;
			MailboxState mailboxState;
			for (;;)
			{
				timeoutReached = false;
				mailboxState = MailboxStateCache.GetByGuid(context, mailboxGuid, mailboxCreation);
				if (mailboxState == null)
				{
					break;
				}
				if (!mailboxState.TryGetMailboxLock(sharedLock, timeoutFunc(mailboxState), lockStats))
				{
					goto Block_2;
				}
				if (mailboxState.IsCurrent || mailboxState.IsNew)
				{
					goto IL_44;
				}
				Microsoft.Exchange.Server.Storage.Common.Globals.AssertRetail(!object.ReferenceEquals(mailboxState, MailboxStateCache.GetByGuid(context, mailboxGuid, MailboxCreation.DontAllow)), "Mailbox state object with such status should be accessible by GUID!");
				mailboxState.ReleaseMailboxLock(sharedLock);
			}
			return false;
			Block_2:
			timeoutReached = true;
			return false;
			IL_44:
			if ((mailboxState.IsNew && !mailboxCreation.IsAllowed) || (mailboxState.IsRemoved && !findRemovedMailbox))
			{
				mailboxState.ReleaseMailboxLock(sharedLock);
				mailboxState = null;
			}
			lockedMailboxState = mailboxState;
			return lockedMailboxState != null;
		}

		// Token: 0x0600079C RID: 1948 RVA: 0x000253B0 File Offset: 0x000235B0
		public static void DeleteMailboxState(Context context, MailboxState mailboxState)
		{
			MailboxStateCache mailboxStateCache = mailboxState.MailboxStateCache;
			using (LockManager.Lock(mailboxStateCache.LockObject, LockManager.LockType.MailboxStateCache))
			{
				mailboxStateCache.MailboxNumberDictionary.Remove(mailboxState.MailboxNumber);
				mailboxStateCache.deletedMailboxes.Add(mailboxState.MailboxNumber);
				MailboxState mailboxState2;
				if (mailboxState.MailboxGuid != Guid.Empty && mailboxStateCache.MailboxGuidDictionary.TryGetValue(mailboxState.MailboxGuid, out mailboxState2) && mailboxState2.MailboxNumber == mailboxState.MailboxNumber)
				{
					mailboxStateCache.MailboxGuidDictionary.Remove(mailboxState.MailboxGuid);
				}
				if (mailboxState.IsNewMailboxPartition)
				{
					mailboxStateCache.unifiedMailboxGuidDictionary.Remove(mailboxState.UnifiedState.UnifiedMailboxGuid);
				}
				mailboxState.Invalidate(context);
				if (mailboxState.CurrentlyActive)
				{
					mailboxStateCache.activeMailboxesEvictionPolicy.Remove(mailboxState.MailboxNumber);
					mailboxState.CurrentlyActive = false;
					if (mailboxStateCache.perfInstance != null)
					{
						mailboxStateCache.perfInstance.ActiveMailboxes.Decrement();
					}
				}
			}
		}

		// Token: 0x0600079D RID: 1949 RVA: 0x000254BC File Offset: 0x000236BC
		public static void MakeRoomForNewMailbox(MailboxState mailboxState)
		{
			MailboxStateCache mailboxStateCache = mailboxState.MailboxStateCache;
			using (LockManager.Lock(mailboxStateCache.LockObject, LockManager.LockType.MailboxStateCache))
			{
				MailboxState mailboxState2;
				if (mailboxState.MailboxGuid != Guid.Empty && mailboxStateCache.MailboxGuidDictionary.TryGetValue(mailboxState.MailboxGuid, out mailboxState2) && mailboxState2.MailboxNumber == mailboxState.MailboxNumber)
				{
					mailboxStateCache.MailboxGuidDictionary.Remove(mailboxState.MailboxGuid);
					mailboxStateCache.AddToPostDisposeCleanupList(mailboxState);
				}
			}
		}

		// Token: 0x0600079E RID: 1950 RVA: 0x0002554C File Offset: 0x0002374C
		internal static void MakeRoomForNewPartition(Context context, MailboxState mailboxState, Guid newUnifiedMailboxGuid)
		{
			MailboxStateCache mailboxStateCache = mailboxState.MailboxStateCache;
			using (LockManager.Lock(mailboxStateCache.LockObject, LockManager.LockType.MailboxStateCache))
			{
				mailboxStateCache.UnifiedMailboxGuidDictionary.Remove(mailboxState.UnifiedState.UnifiedMailboxGuid);
				mailboxState.UnifiedState.SetNewUnfiedMailboxGuid(newUnifiedMailboxGuid);
				mailboxStateCache.UnifiedMailboxGuidDictionary.Add(mailboxState.UnifiedState.UnifiedMailboxGuid, mailboxState.UnifiedState);
			}
			if (mailboxState.IsNew)
			{
				MailboxStateCache.DeleteMailboxState(context, mailboxState);
				return;
			}
			MailboxStateCache.MakeRoomForNewMailbox(mailboxState);
		}

		// Token: 0x0600079F RID: 1951 RVA: 0x000255E4 File Offset: 0x000237E4
		public static MailboxState ResetMailboxState(Context context, MailboxState mailboxState, MailboxStatus newStatus)
		{
			MailboxStateCache mailboxStateCache = mailboxState.MailboxStateCache;
			MailboxState mailboxState2 = new MailboxState(mailboxStateCache, mailboxState.MailboxNumber, mailboxState.MailboxPartitionNumber, mailboxState.MailboxGuid, mailboxState.MailboxInstanceGuid, newStatus, mailboxState.GlobalIdLowWatermark, mailboxState.GlobalCnLowWatermark, mailboxState.CountersAlreadyPatched, mailboxState.Quarantined, mailboxState.LastMailboxMaintenanceTime, mailboxState.LastQuotaCheckTime, mailboxState.TenantHint, mailboxState.MailboxType, mailboxState.MailboxTypeDetail);
			mailboxState2.SetUnifiedMailboxState(mailboxState.UnifiedState);
			using (LockManager.Lock(mailboxStateCache.LockObject, LockManager.LockType.MailboxStateCache))
			{
				mailboxState.Invalidate(context);
				mailboxStateCache.MailboxNumberDictionary[mailboxState.MailboxNumber] = mailboxState2;
				MailboxState mailboxState3;
				if (mailboxState.MailboxGuid != Guid.Empty && mailboxStateCache.MailboxGuidDictionary.TryGetValue(mailboxState.MailboxGuid, out mailboxState3) && mailboxState3.MailboxNumber == mailboxState.MailboxNumber)
				{
					mailboxStateCache.MailboxGuidDictionary[mailboxState.MailboxGuid] = mailboxState2;
				}
				if (mailboxState.CurrentlyActive)
				{
					mailboxStateCache.activeMailboxesEvictionPolicy.Remove(mailboxState.MailboxNumber);
					mailboxState.CurrentlyActive = false;
					if (mailboxStateCache.perfInstance != null)
					{
						mailboxStateCache.perfInstance.ActiveMailboxes.Decrement();
					}
				}
			}
			return mailboxState2;
		}

		// Token: 0x060007A0 RID: 1952 RVA: 0x00025724 File Offset: 0x00023924
		public static bool TryGetMailboxNumber(Context context, Guid mailboxGuid, bool active, out int mailboxNumber)
		{
			MailboxState byGuid = MailboxStateCache.GetByGuid(context, mailboxGuid, MailboxCreation.DontAllow);
			if (byGuid != null && (!active || byGuid.IsUserAccessible))
			{
				mailboxNumber = byGuid.MailboxNumber;
				return true;
			}
			mailboxNumber = 0;
			return false;
		}

		// Token: 0x060007A1 RID: 1953 RVA: 0x00025AA0 File Offset: 0x00023CA0
		public static IEnumerable<MailboxState> GetStateListSnapshot(Context context, SearchCriteria restriction)
		{
			MailboxTable mailboxTable = DatabaseSchema.MailboxTable(context.Database);
			List<int> mailboxNumbers = new List<int>();
			Column[] columnsToFetch = new Column[]
			{
				mailboxTable.MailboxNumber
			};
			bool transactionStarted = false;
			try
			{
				if (!context.DatabaseTransactionStarted && context.Database.PhysicalDatabase.DatabaseType != DatabaseType.Sql)
				{
					context.BeginTransactionIfNeeded();
					transactionStarted = true;
				}
				using (TableOperator tableOperator = Factory.CreateTableOperator(context.Culture, context, mailboxTable.Table, mailboxTable.MailboxTablePK, columnsToFetch, restriction, null, 0, 0, KeyRange.AllRows, false, false))
				{
					using (Reader reader = tableOperator.ExecuteReader(false))
					{
						while (reader.Read())
						{
							int @int = reader.GetInt32(mailboxTable.MailboxNumber);
							mailboxNumbers.Add(@int);
						}
					}
				}
			}
			finally
			{
				if (transactionStarted)
				{
					try
					{
						context.Commit();
					}
					finally
					{
						context.Abort();
					}
				}
			}
			foreach (int mailboxNumber in mailboxNumbers)
			{
				MailboxState mailboxState = MailboxStateCache.Get(context, mailboxNumber);
				if (mailboxState != null)
				{
					yield return mailboxState;
				}
			}
			yield break;
		}

		// Token: 0x060007A2 RID: 1954 RVA: 0x00025AC4 File Offset: 0x00023CC4
		public static void OnMailboxActivity(MailboxState mailboxState)
		{
			MailboxStateCache mailboxStateCache = mailboxState.MailboxStateCache;
			if (!mailboxState.IsValid)
			{
				return;
			}
			DateTime utcNow = DateTime.UtcNow;
			if (utcNow - mailboxState.LastUpdatedActiveTime < MailboxStateCache.activeMailboxesEvictionPolicyUpdateThreshold.Value && mailboxState.CurrentlyActive)
			{
				return;
			}
			int[] array = null;
			mailboxState.AddReference();
			try
			{
				using (LockManager.Lock(mailboxStateCache.LockObject, LockManager.LockType.MailboxStateCache))
				{
					if (mailboxState.CurrentlyActive)
					{
						mailboxStateCache.activeMailboxesEvictionPolicy.KeyAccess(mailboxState.MailboxNumber);
					}
					else
					{
						mailboxStateCache.activeMailboxesEvictionPolicy.Insert(mailboxState.MailboxNumber);
						mailboxState.CurrentlyActive = true;
						if (mailboxStateCache.perfInstance != null)
						{
							mailboxStateCache.perfInstance.ActiveMailboxes.Increment();
						}
					}
					mailboxState.LastUpdatedActiveTime = utcNow;
					mailboxStateCache.activeMailboxesEvictionPolicy.EvictionCheckpoint();
					if (mailboxStateCache.activeMailboxesEvictionPolicy.CountOfKeysToCleanup >= MailboxStateCache.numberOfMailboxesToStartCleanupTask.Value)
					{
						array = mailboxStateCache.activeMailboxesEvictionPolicy.GetKeysToCleanup(true);
						foreach (int key in array)
						{
							MailboxState mailboxState2;
							if (mailboxStateCache.MailboxNumberDictionary.TryGetValue(key, out mailboxState2))
							{
								mailboxState2.CurrentlyActive = false;
								if (mailboxStateCache.perfInstance != null)
								{
									mailboxStateCache.perfInstance.ActiveMailboxes.Decrement();
								}
							}
						}
					}
				}
			}
			finally
			{
				mailboxState.ReleaseReference();
			}
			if (array != null)
			{
				SingleExecutionTask<MailboxStateCache.DatabaseAndMailboxNumbers>.CreateSingleExecutionTask(mailboxStateCache.Database.TaskList, TaskExecutionWrapper<MailboxStateCache.DatabaseAndMailboxNumbers>.WrapExecute(new TaskDiagnosticInformation(TaskTypeId.CleanupNonActiveMailboxStates, ClientType.System, mailboxStateCache.Database.MdbGuid), new TaskExecutionWrapper<MailboxStateCache.DatabaseAndMailboxNumbers>.TaskCallback<Context>(MailboxStateCache.CleanupNonActiveMailboxStates)), new MailboxStateCache.DatabaseAndMailboxNumbers(mailboxStateCache.Database, array), true);
			}
		}

		// Token: 0x170001E8 RID: 488
		// (get) Token: 0x060007A3 RID: 1955 RVA: 0x00025C70 File Offset: 0x00023E70
		string IStoreQueryTargetBase<MailboxStateCache.QueryableMailboxState>.Name
		{
			get
			{
				return "MailboxState";
			}
		}

		// Token: 0x170001E9 RID: 489
		// (get) Token: 0x060007A4 RID: 1956 RVA: 0x00025C77 File Offset: 0x00023E77
		Type[] IStoreQueryTargetBase<MailboxStateCache.QueryableMailboxState>.ParameterTypes
		{
			get
			{
				return Array<Type>.Empty;
			}
		}

		// Token: 0x060007A5 RID: 1957 RVA: 0x00025C8C File Offset: 0x00023E8C
		IEnumerable<MailboxStateCache.QueryableMailboxState> IStoreDatabaseQueryTarget<MailboxStateCache.QueryableMailboxState>.GetRows(IConnectionProvider connectionProvider, object[] parameters)
		{
			IContextProvider contextProvider = connectionProvider as IContextProvider;
			if (contextProvider == null)
			{
				throw new DiagnosticQueryException("The connection provider given is not a context provider.");
			}
			IList<MailboxStateCache.QueryableMailboxState> list = new List<MailboxStateCache.QueryableMailboxState>(30);
			MailboxStateCache mailboxStateCache = (MailboxStateCache)this.database.ComponentData[MailboxStateCache.cacheSlot];
			IList<int> list2;
			using (LockManager.Lock(mailboxStateCache.LockObject, LockManager.LockType.MailboxStateCache))
			{
				list2 = new List<int>(mailboxStateCache.MailboxNumberDictionary.Keys);
			}
			foreach (int mailboxNumber in list2)
			{
				bool flag;
				MailboxState mailboxState;
				if (MailboxStateCache.TryGetLocked(contextProvider.CurrentContext, mailboxNumber, true, (MailboxState state) => DefaultSettings.Get.DiagnosticQueryLockTimeout, contextProvider.CurrentContext.Diagnostics, out flag, out mailboxState))
				{
					try
					{
						mailboxState.AddReference();
						try
						{
							list.Add(new MailboxStateCache.QueryableMailboxState(mailboxState));
						}
						finally
						{
							mailboxState.ReleaseReference();
						}
						continue;
					}
					finally
					{
						mailboxState.ReleaseMailboxLock(true);
					}
				}
				if (flag)
				{
					throw new DiagnosticQueryException(DiagnosticQueryStrings.UnableToLockMailbox(mailboxNumber));
				}
				throw new DiagnosticQueryException(DiagnosticQueryStrings.MailboxStateNotFound(mailboxNumber));
			}
			return list;
		}

		// Token: 0x060007A6 RID: 1958 RVA: 0x00025DEC File Offset: 0x00023FEC
		internal static void Initialize()
		{
			MailboxStateCache.expectedNumberOfActiveMailboxes = Hookable<int>.Create(false, ConfigurationSchema.ActiveMailboxCacheSize.Value);
			MailboxStateCache.activeMailboxCacheTimeToLive = ConfigurationSchema.ActiveMailboxCacheTimeToLive.Value;
			MailboxStateCache.numberOfMailboxesToStartCleanupTask = Hookable<int>.Create(false, ConfigurationSchema.ActiveMailboxCacheCleanupThreshold.Value);
			if (MailboxStateCache.cacheSlot == -1)
			{
				MailboxStateCache.cacheSlot = StoreDatabase.AllocateComponentDataSlot();
				Mailbox.RegisterOnPostDisposeAction(new Action<Context, StoreDatabase>(MailboxStateCache.PostMailboxDisposeCleanup));
			}
		}

		// Token: 0x060007A7 RID: 1959 RVA: 0x00025E58 File Offset: 0x00024058
		internal static void MountHandler(StoreDatabase database, IConnectionProvider connectionProvider)
		{
			MailboxStateCache mailboxStateCache = new MailboxStateCache(database);
			mailboxStateCache.Configure(connectionProvider);
			database.ComponentData[MailboxStateCache.cacheSlot] = mailboxStateCache;
			StoreQueryTargets.Register<MailboxStateCache.QueryableMailboxState>(mailboxStateCache, database.PhysicalDatabase, Visibility.Public);
		}

		// Token: 0x060007A8 RID: 1960 RVA: 0x00025E91 File Offset: 0x00024091
		internal static void DismountHandler(Context context, StoreDatabase database)
		{
			if (MailboxStateCache.onDismountTestHook.Value != null)
			{
				MailboxStateCache.onDismountTestHook.Value(context, database);
			}
			MailboxStateCache.PostMailboxDisposeCleanup(context, database);
			database.ComponentData[MailboxStateCache.cacheSlot] = null;
		}

		// Token: 0x060007A9 RID: 1961 RVA: 0x00025EC8 File Offset: 0x000240C8
		internal static IDisposable SetOnDismountingTestHook()
		{
			return MailboxStateCache.onDismountTestHook.SetTestHook(new Action<Context, StoreDatabase>(MailboxStateCache.DismountHandlerForTest));
		}

		// Token: 0x060007AA RID: 1962 RVA: 0x00025EE0 File Offset: 0x000240E0
		internal static IDisposable SetOnBeforeLoadTestHook(Action<Context> action)
		{
			return MailboxStateCache.onBeforeLoadTestHook.SetTestHook(action);
		}

		// Token: 0x060007AB RID: 1963 RVA: 0x00025EED File Offset: 0x000240ED
		internal static IDisposable SetOnAfterLoadTestHook(Action<Context> action)
		{
			return MailboxStateCache.onAfterLoadTestHook.SetTestHook(action);
		}

		// Token: 0x060007AC RID: 1964 RVA: 0x00025EFA File Offset: 0x000240FA
		internal static IDisposable SetActiveMailboxesEvictionPolicyUpdateThreshold(TimeSpan threshold)
		{
			return MailboxStateCache.activeMailboxesEvictionPolicyUpdateThreshold.SetTestHook(threshold);
		}

		// Token: 0x060007AD RID: 1965 RVA: 0x00025F07 File Offset: 0x00024107
		internal static IDisposable SetExpectedNumberOfActiveMailboxes(int numberOfActiveMailboxes)
		{
			return MailboxStateCache.expectedNumberOfActiveMailboxes.SetTestHook(numberOfActiveMailboxes);
		}

		// Token: 0x060007AE RID: 1966 RVA: 0x00025F14 File Offset: 0x00024114
		internal static void CleanupNonActiveMailboxStates(Context context, MailboxStateCache.DatabaseAndMailboxNumbers databaseAndMailboxNumbers, Func<bool> shouldTaskContinue)
		{
			using (context.AssociateWithDatabase(databaseAndMailboxNumbers.Database))
			{
				if (context.Database.IsOnlineActive)
				{
					MailboxStateCache.CleanupNonActiveMailboxStates(context, databaseAndMailboxNumbers.MailboxNumbers);
				}
			}
		}

		// Token: 0x060007AF RID: 1967 RVA: 0x00025F74 File Offset: 0x00024174
		internal static void CleanupNonActiveMailboxStates(Context context, int[] mailboxNumbers)
		{
			MailboxStateCache mailboxStateCache = (MailboxStateCache)context.Database.ComponentData[MailboxStateCache.cacheSlot];
			int i = 0;
			while (i < mailboxNumbers.Length)
			{
				int num = mailboxNumbers[i];
				bool flag;
				MailboxState mailboxState;
				if (!MailboxStateCache.TryGetLocked(context, num, false, (MailboxState state) => TimeSpan.Zero, context.Diagnostics, out flag, out mailboxState))
				{
					using (LockManager.Lock(mailboxStateCache.LockObject, LockManager.LockType.MailboxStateCache, context.Diagnostics))
					{
						if (!mailboxStateCache.activeMailboxesEvictionPolicy.Contains(num) && mailboxStateCache.MailboxNumberDictionary.TryGetValue(num, out mailboxState))
						{
							mailboxStateCache.activeMailboxesEvictionPolicy.AddKeyToCleanup(num);
							mailboxState.CurrentlyActive = true;
							if (mailboxStateCache.perfInstance != null)
							{
								mailboxStateCache.perfInstance.ActiveMailboxes.Increment();
							}
						}
						goto IL_F6;
					}
					goto Block_4;
				}
				goto IL_CC;
				IL_F6:
				i++;
				continue;
				Block_4:
				try
				{
					IL_CC:
					if (!mailboxState.CurrentlyActive)
					{
						mailboxState.AddReference();
						try
						{
							mailboxState.CleanupAsNonActive(context);
						}
						finally
						{
							mailboxState.ReleaseReference();
						}
					}
				}
				finally
				{
					mailboxState.ReleaseMailboxLock(false);
				}
				goto IL_F6;
			}
		}

		// Token: 0x060007B0 RID: 1968 RVA: 0x000260B0 File Offset: 0x000242B0
		internal static int[] GetMailboxesForCleanupForTest(Context context, bool clearKeys)
		{
			MailboxStateCache mailboxStateCache = (MailboxStateCache)context.Database.ComponentData[MailboxStateCache.cacheSlot];
			int[] result;
			using (LockManager.Lock(mailboxStateCache.LockObject, LockManager.LockType.MailboxStateCache, context.Diagnostics))
			{
				mailboxStateCache.activeMailboxesEvictionPolicy.EvictionCheckpoint();
				int[] keysToCleanup = mailboxStateCache.activeMailboxesEvictionPolicy.GetKeysToCleanup(clearKeys);
				if (clearKeys)
				{
					foreach (int key in keysToCleanup)
					{
						MailboxState mailboxState;
						if (mailboxStateCache.MailboxNumberDictionary.TryGetValue(key, out mailboxState))
						{
							mailboxState.CurrentlyActive = false;
							if (mailboxStateCache.perfInstance != null)
							{
								mailboxStateCache.perfInstance.ActiveMailboxes.Decrement();
							}
						}
					}
				}
				result = keysToCleanup;
			}
			return result;
		}

		// Token: 0x060007B1 RID: 1969 RVA: 0x0002617C File Offset: 0x0002437C
		private static void PostMailboxDisposeCleanup(Context context, StoreDatabase database)
		{
			if (database != null)
			{
				MailboxStateCache mailboxStateCache = (MailboxStateCache)database.ComponentData[MailboxStateCache.cacheSlot];
				if (mailboxStateCache == null)
				{
					return;
				}
				List<MailboxState> list = null;
				using (LockManager.Lock(mailboxStateCache.LockObject, LockManager.LockType.MailboxStateCache, context.Diagnostics))
				{
					for (int i = mailboxStateCache.postMailboxDisposeList.Count - 1; i >= 0; i--)
					{
						MailboxState mailboxState = mailboxStateCache.postMailboxDisposeList[i];
						if (mailboxState.IsMailboxLockedExclusively())
						{
							if (list == null)
							{
								list = new List<MailboxState>(1);
							}
							list.Add(mailboxState);
							mailboxStateCache.postMailboxDisposeList.RemoveAt(i);
						}
					}
				}
				if (list != null)
				{
					foreach (MailboxState mailboxState2 in list)
					{
						mailboxState2.CleanupDataSlots(context);
					}
				}
			}
		}

		// Token: 0x060007B2 RID: 1970 RVA: 0x00026270 File Offset: 0x00024470
		private static void DismountHandlerForTest(Context context, StoreDatabase database)
		{
			MailboxStateCache mailboxStateCache = (MailboxStateCache)context.Database.ComponentData[MailboxStateCache.cacheSlot];
			if (mailboxStateCache != null)
			{
				List<MailboxState> list;
				using (LockManager.Lock(mailboxStateCache.LockObject, LockManager.LockType.MailboxStateCache, context.Diagnostics))
				{
					list = new List<MailboxState>(mailboxStateCache.mailboxNumberDictionary.Values);
				}
				foreach (MailboxState mailboxState in list)
				{
					mailboxState.GetMailboxLock(false, context.Diagnostics);
					MailboxTaskQueue mailboxTaskQueueNoCreate;
					try
					{
						mailboxState.AddReference();
						try
						{
							mailboxState.CleanupAsNonActive(context);
						}
						finally
						{
							mailboxState.ReleaseReference();
						}
						mailboxTaskQueueNoCreate = MailboxTaskQueue.GetMailboxTaskQueueNoCreate(context, mailboxState);
					}
					finally
					{
						mailboxState.ReleaseMailboxLock(false);
					}
					if (mailboxTaskQueueNoCreate != null)
					{
						mailboxTaskQueueNoCreate.DrainQueue();
					}
				}
			}
		}

		// Token: 0x060007B3 RID: 1971 RVA: 0x00026374 File Offset: 0x00024574
		private static MailboxState GetByGuid(Context context, Guid mailboxGuid, MailboxCreation mailboxCreation)
		{
			MailboxStateCache mailboxStateCache = (MailboxStateCache)context.Database.ComponentData[MailboxStateCache.cacheSlot];
			MailboxState mailboxState;
			using (LockManager.Lock(mailboxStateCache.LockObject, LockManager.LockType.MailboxStateCache, context.Diagnostics))
			{
				if (mailboxStateCache.MailboxGuidDictionary.TryGetValue(mailboxGuid, out mailboxState))
				{
					return mailboxState;
				}
			}
			if (mailboxStateCache.TryLoadMailboxState(context, new Guid?(mailboxGuid), null, out mailboxState))
			{
				return mailboxState;
			}
			if (!mailboxCreation.IsAllowed)
			{
				return null;
			}
			if (context.Database.IsReadOnly)
			{
				throw new StoreException((LID)56268U, ErrorCodeValue.NotSupported);
			}
			if (mailboxCreation.UnifiedMailboxGuid != null)
			{
				if (!DefaultSettings.Get.EnableUnifiedMailbox)
				{
					throw new StoreException((LID)54732U, ErrorCodeValue.NotSupported);
				}
				if (!UnifiedMailbox.IsReady(context, context.Database))
				{
					throw new StoreException((LID)49692U, ErrorCodeValue.InvalidParameter);
				}
			}
			MailboxState result;
			using (LockManager.Lock(mailboxStateCache.LockObject, LockManager.LockType.MailboxStateCache, context.Diagnostics))
			{
				if (mailboxStateCache.MailboxGuidDictionary.TryGetValue(mailboxGuid, out mailboxState))
				{
					result = mailboxState;
				}
				else
				{
					int num = mailboxStateCache.nextMailboxNumber++;
					MailboxState.UnifiedMailboxState unifiedMailboxState = null;
					int mailboxPartitionNumber;
					if (mailboxCreation.UnifiedMailboxGuid != null)
					{
						if (mailboxStateCache.unifiedMailboxGuidDictionary.TryGetValue(mailboxCreation.UnifiedMailboxGuid.Value, out unifiedMailboxState))
						{
							mailboxPartitionNumber = unifiedMailboxState.MailboxPartitionNumber;
						}
						else
						{
							int num2;
							bool countersAlreadyPatched;
							if (mailboxStateCache.TryLoadMailboxPartitionNumber(context, mailboxCreation.UnifiedMailboxGuid.Value, out num2))
							{
								mailboxPartitionNumber = num2;
								countersAlreadyPatched = false;
							}
							else
							{
								if (!mailboxCreation.AllowPartitionCreation)
								{
									throw new StoreException((LID)62876U, ErrorCodeValue.NoAccess, "Partition creation is not allowed");
								}
								mailboxPartitionNumber = num;
								countersAlreadyPatched = true;
							}
							unifiedMailboxState = new MailboxState.UnifiedMailboxState(mailboxStateCache, mailboxCreation.UnifiedMailboxGuid.Value, mailboxPartitionNumber, 0UL, 0UL, countersAlreadyPatched);
							mailboxStateCache.unifiedMailboxGuidDictionary.Add(mailboxCreation.UnifiedMailboxGuid.Value, unifiedMailboxState);
						}
					}
					else
					{
						mailboxPartitionNumber = num;
					}
					mailboxState = new MailboxState(mailboxStateCache, num, mailboxPartitionNumber, mailboxGuid, Guid.NewGuid(), MailboxStatus.New, 0UL, 0UL, true, false, DateTime.MinValue, DateTime.MinValue, TenantHint.Empty, MailboxInfo.MailboxType.Private, MailboxInfo.MailboxTypeDetail.UserMailbox);
					if (unifiedMailboxState != null)
					{
						mailboxState.SetUnifiedMailboxState(unifiedMailboxState);
					}
					mailboxStateCache.MailboxNumberDictionary.Add(mailboxState.MailboxNumber, mailboxState);
					mailboxStateCache.MailboxGuidDictionary.Add(mailboxState.MailboxGuid, mailboxState);
					result = mailboxState;
				}
			}
			return result;
		}

		// Token: 0x060007B4 RID: 1972 RVA: 0x0002662C File Offset: 0x0002482C
		internal void AddToPostDisposeCleanupList(MailboxState mailboxState)
		{
			this.postMailboxDisposeList.Add(mailboxState);
		}

		// Token: 0x060007B5 RID: 1973 RVA: 0x0002663C File Offset: 0x0002483C
		private void Configure(IConnectionProvider connectionProvider)
		{
			MailboxTable mailboxTable = DatabaseSchema.MailboxTable(this.database);
			Column[] columnsToFetch = new Column[]
			{
				mailboxTable.MailboxNumber
			};
			using (TableOperator tableOperator = Factory.CreateTableOperator(CultureHelper.DefaultCultureInfo, connectionProvider, mailboxTable.Table, mailboxTable.Table.PrimaryKeyIndex, columnsToFetch, null, null, 0, 1, KeyRange.AllRows, true, false))
			{
				using (Reader reader = tableOperator.ExecuteReader(false))
				{
					if (reader.Read())
					{
						int @int = reader.GetInt32(mailboxTable.MailboxNumber);
						this.nextMailboxNumber = @int + 1;
					}
				}
			}
		}

		// Token: 0x060007B6 RID: 1974 RVA: 0x000266F0 File Offset: 0x000248F0
		private bool TryLoadMailboxPartitionNumber(Context context, Guid unifiedMailboxGuid, out int mailboxPartitionNumber)
		{
			Microsoft.Exchange.Server.Storage.Common.Globals.AssertRetail(UnifiedMailbox.IsReady(context, context.Database), "This function should be called only after UnifiedMailbox upgrader is executed");
			mailboxPartitionNumber = 0;
			MailboxTable mailboxTable = DatabaseSchema.MailboxTable(context.Database);
			StartStopKey startStopKey = new StartStopKey(true, new object[]
			{
				unifiedMailboxGuid
			});
			bool result;
			using (TableOperator tableOperator = Factory.CreateTableOperator(context.Culture, context, mailboxTable.Table, mailboxTable.UnifiedMailboxGuidIndex, new Column[]
			{
				mailboxTable.MailboxPartitionNumber
			}, null, null, 0, 1, new KeyRange(startStopKey, startStopKey), false, true))
			{
				using (Reader reader = tableOperator.ExecuteReader(false))
				{
					if (!reader.Read())
					{
						result = false;
					}
					else
					{
						mailboxPartitionNumber = reader.GetInt32(mailboxTable.MailboxPartitionNumber);
						result = true;
					}
				}
			}
			return result;
		}

		// Token: 0x060007B7 RID: 1975 RVA: 0x000267D4 File Offset: 0x000249D4
		private bool TryLoadMailboxState(Context context, Guid? mailboxGuid, int? mailboxNumber, out MailboxState mailboxState)
		{
			mailboxState = null;
			if (MailboxStateCache.onBeforeLoadTestHook.Value != null)
			{
				MailboxStateCache.onBeforeLoadTestHook.Value(context);
			}
			MailboxTable mailboxTable = DatabaseSchema.MailboxTable(context.Database);
			Column column = PropertySchema.MapToColumn(context.Database, ObjectType.Mailbox, PropTag.Mailbox.MailboxType);
			Column column2 = PropertySchema.MapToColumn(context.Database, ObjectType.Mailbox, PropTag.Mailbox.MailboxTypeDetail);
			Column column3 = PropertySchema.MapToColumn(context.Database, ObjectType.Mailbox, PropTag.Mailbox.TenantHint);
			Column column4 = PropertySchema.MapToColumn(context.Database, ObjectType.Mailbox, PropTag.Mailbox.MailboxPartitionNumber);
			List<Column> list = new List<Column>(new Column[]
			{
				mailboxTable.LastQuotaNotificationTime,
				mailboxTable.DeletedOn,
				mailboxTable.MailboxGuid,
				mailboxTable.MailboxInstanceGuid,
				mailboxTable.MailboxNumber,
				mailboxTable.Status,
				column,
				column2,
				column3,
				column4
			});
			if (AddLastMaintenanceTimeToMailbox.IsReady(context, context.Database))
			{
				list.Add(mailboxTable.LastMailboxMaintenanceTime);
			}
			if (UnifiedMailbox.IsReady(context, context.Database))
			{
				list.Add(mailboxTable.UnifiedMailboxGuid);
			}
			StartStopKey startStopKey;
			Index index;
			if (mailboxGuid != null)
			{
				startStopKey = new StartStopKey(true, new object[]
				{
					mailboxGuid.Value
				});
				index = mailboxTable.MailboxGuidIndex;
			}
			else
			{
				startStopKey = new StartStopKey(true, new object[]
				{
					mailboxNumber.Value
				});
				index = mailboxTable.MailboxTablePK;
			}
			Guid? guid = null;
			bool flag = false;
			MailboxState mailboxState2;
			try
			{
				if (!context.DatabaseTransactionStarted && context.Database.PhysicalDatabase.DatabaseType != DatabaseType.Sql)
				{
					context.BeginTransactionIfNeeded();
					flag = true;
				}
				using (TableOperator tableOperator = Factory.CreateTableOperator(context.Culture, context, mailboxTable.Table, index, list, null, null, 0, 1, new KeyRange(startStopKey, startStopKey), false, true))
				{
					using (Reader reader = tableOperator.ExecuteReader(false))
					{
						if (!reader.Read())
						{
							return false;
						}
						int @int = reader.GetInt32(mailboxTable.MailboxNumber);
						Guid? nullableGuid = reader.GetNullableGuid(mailboxTable.MailboxGuid);
						Guid? nullableGuid2 = reader.GetNullableGuid(mailboxTable.MailboxInstanceGuid);
						reader.GetNullableDateTime(mailboxTable.DeletedOn);
						MailboxStatus int2 = (MailboxStatus)reader.GetInt16(mailboxTable.Status);
						TenantHint tenantHint = new TenantHint(reader.GetBinary(column3));
						int? nullableInt = reader.GetNullableInt32(column);
						int? nullableInt2 = reader.GetNullableInt32(column2);
						MailboxInfo.MailboxType valueOrDefault = (MailboxInfo.MailboxType)nullableInt.GetValueOrDefault(0);
						MailboxInfo.MailboxTypeDetail valueOrDefault2 = (MailboxInfo.MailboxTypeDetail)nullableInt2.GetValueOrDefault(1);
						DateTime dateTime = reader.GetDateTime(mailboxTable.LastQuotaNotificationTime);
						int? nullableInt3 = reader.GetNullableInt32(column4);
						DateTime lastMailboxMaintenanceTime;
						if (AddLastMaintenanceTimeToMailbox.IsReady(context, context.Database))
						{
							lastMailboxMaintenanceTime = reader.GetNullableDateTime(mailboxTable.LastMailboxMaintenanceTime).GetValueOrDefault(DateTime.MinValue);
						}
						else
						{
							lastMailboxMaintenanceTime = dateTime;
						}
						if (UnifiedMailbox.IsReady(context, context.Database))
						{
							guid = reader.GetNullableGuid(mailboxTable.UnifiedMailboxGuid);
						}
						mailboxState2 = new MailboxState(this, @int, (nullableInt3 != null) ? nullableInt3.Value : @int, nullableGuid.GetValueOrDefault(Guid.Empty), nullableGuid2.GetValueOrDefault(Guid.Empty), int2, 0UL, 0UL, false, false, lastMailboxMaintenanceTime, dateTime, tenantHint, valueOrDefault, valueOrDefault2);
					}
				}
			}
			finally
			{
				if (flag)
				{
					try
					{
						context.Commit();
					}
					finally
					{
						context.Abort();
					}
				}
			}
			if (MailboxStateCache.onAfterLoadTestHook.Value != null)
			{
				MailboxStateCache.onAfterLoadTestHook.Value(context);
			}
			bool result;
			using (LockManager.Lock(this.LockObject, LockManager.LockType.MailboxStateCache, context.Diagnostics))
			{
				if (this.deletedMailboxes.Contains(mailboxState2.MailboxNumber))
				{
					result = false;
				}
				else
				{
					MailboxState mailboxState3;
					if (mailboxGuid != null && this.MailboxGuidDictionary.TryGetValue(mailboxState2.MailboxGuid, out mailboxState3))
					{
						mailboxState = mailboxState3;
					}
					else if (this.MailboxNumberDictionary.TryGetValue(mailboxState2.MailboxNumber, out mailboxState3))
					{
						if (mailboxGuid != null)
						{
							return false;
						}
						mailboxState = mailboxState3;
					}
					else
					{
						if (guid != null)
						{
							Microsoft.Exchange.Server.Storage.Common.Globals.AssertRetail(UnifiedMailbox.IsReady(context, context.Database), "We should reach this code only after UnifiedMailbox upgrader is executed");
							MailboxState.UnifiedMailboxState unifiedMailboxState = null;
							if (!this.unifiedMailboxGuidDictionary.TryGetValue(guid.Value, out unifiedMailboxState))
							{
								unifiedMailboxState = new MailboxState.UnifiedMailboxState(this, guid.Value, mailboxState2.MailboxPartitionNumber, mailboxState2.GlobalIdLowWatermark, mailboxState2.GlobalCnLowWatermark, mailboxState2.CountersAlreadyPatched);
								this.unifiedMailboxGuidDictionary.Add(guid.Value, unifiedMailboxState);
							}
							mailboxState2.SetUnifiedMailboxState(unifiedMailboxState);
						}
						this.mailboxNumberDictionary.Add(mailboxState2.MailboxNumber, mailboxState2);
						if (mailboxState2.MailboxGuid != Guid.Empty)
						{
							this.mailboxGuidDictionary.Add(mailboxState2.MailboxGuid, mailboxState2);
						}
						mailboxState = mailboxState2;
					}
					result = true;
				}
			}
			return result;
		}

		// Token: 0x04000465 RID: 1125
		private static int cacheSlot = -1;

		// Token: 0x04000466 RID: 1126
		private static Hookable<int> expectedNumberOfActiveMailboxes = Hookable<int>.Create(false, 1000);

		// Token: 0x04000467 RID: 1127
		private static TimeSpan activeMailboxCacheTimeToLive = TimeSpan.FromMinutes(30.0);

		// Token: 0x04000468 RID: 1128
		private static Hookable<int> numberOfMailboxesToStartCleanupTask = Hookable<int>.Create(false, 5);

		// Token: 0x04000469 RID: 1129
		private static Hookable<TimeSpan> activeMailboxesEvictionPolicyUpdateThreshold = Hookable<TimeSpan>.Create(false, TimeSpan.FromMinutes(0.0));

		// Token: 0x0400046A RID: 1130
		private static Hookable<Action<Context, StoreDatabase>> onDismountTestHook = Hookable<Action<Context, StoreDatabase>>.Create(false, null);

		// Token: 0x0400046B RID: 1131
		private static Hookable<Action<Context>> onBeforeLoadTestHook = Hookable<Action<Context>>.Create(false, null);

		// Token: 0x0400046C RID: 1132
		private static Hookable<Action<Context>> onAfterLoadTestHook = Hookable<Action<Context>>.Create(false, null);

		// Token: 0x0400046D RID: 1133
		private readonly StoreDatabase database;

		// Token: 0x0400046E RID: 1134
		private readonly EvictionPolicy<int> activeMailboxesEvictionPolicy;

		// Token: 0x0400046F RID: 1135
		private Dictionary<int, MailboxState> mailboxNumberDictionary;

		// Token: 0x04000470 RID: 1136
		private Dictionary<Guid, MailboxState> mailboxGuidDictionary;

		// Token: 0x04000471 RID: 1137
		private Dictionary<Guid, MailboxState.UnifiedMailboxState> unifiedMailboxGuidDictionary;

		// Token: 0x04000472 RID: 1138
		private List<MailboxState> postMailboxDisposeList;

		// Token: 0x04000473 RID: 1139
		private object lockObject = new object();

		// Token: 0x04000474 RID: 1140
		private readonly StorePerDatabasePerformanceCountersInstance perfInstance;

		// Token: 0x04000475 RID: 1141
		private int nextMailboxNumber = 100;

		// Token: 0x04000476 RID: 1142
		private HashSet<int> deletedMailboxes;

		// Token: 0x020000BD RID: 189
		internal struct DatabaseAndMailboxNumbers
		{
			// Token: 0x060007BB RID: 1979 RVA: 0x00026D8F File Offset: 0x00024F8F
			internal DatabaseAndMailboxNumbers(StoreDatabase database, int[] mailboxNumbers)
			{
				this.database = database;
				this.mailboxNumbers = mailboxNumbers;
			}

			// Token: 0x170001EA RID: 490
			// (get) Token: 0x060007BC RID: 1980 RVA: 0x00026D9F File Offset: 0x00024F9F
			internal StoreDatabase Database
			{
				get
				{
					return this.database;
				}
			}

			// Token: 0x170001EB RID: 491
			// (get) Token: 0x060007BD RID: 1981 RVA: 0x00026DA7 File Offset: 0x00024FA7
			internal int[] MailboxNumbers
			{
				get
				{
					return this.mailboxNumbers;
				}
			}

			// Token: 0x04000479 RID: 1145
			private readonly StoreDatabase database;

			// Token: 0x0400047A RID: 1146
			private readonly int[] mailboxNumbers;
		}

		// Token: 0x020000BE RID: 190
		internal class QueryableMailboxState
		{
			// Token: 0x060007BE RID: 1982 RVA: 0x00026DB0 File Offset: 0x00024FB0
			internal QueryableMailboxState(MailboxState state)
			{
				this.CountersAlreadyPatched = state.CountersAlreadyPatched;
				this.CurrentlyActive = state.CurrentlyActive;
				this.DatabaseGuid = state.DatabaseGuid;
				this.GlobalCnLowWatermark = state.GlobalCnLowWatermark;
				this.GlobalIdLowWatermark = state.GlobalIdLowWatermark;
				this.IsAccessible = state.IsAccessible;
				this.IsCurrent = state.IsCurrent;
				this.IsDeleted = state.IsDeleted;
				this.IsDisabled = state.IsDisabled;
				this.IsDisconnected = state.IsDisconnected;
				this.IsHardDeleted = state.IsHardDeleted;
				this.IsNew = state.IsNew;
				this.IsRemoved = state.IsRemoved;
				this.IsSoftDeleted = state.IsSoftDeleted;
				this.IsTombstone = state.IsTombstone;
				this.IsUserAccessible = state.IsUserAccessible;
				this.IsValid = state.IsValid;
				this.LastUpdatedActiveTime = state.LastUpdatedActiveTime;
				this.MailboxGuid = state.MailboxGuid;
				this.MailboxInstanceGuid = state.MailboxInstanceGuid;
				this.MailboxNumber = state.MailboxNumber;
				this.MailboxType = state.MailboxType;
				this.Quarantined = state.Quarantined;
				this.Status = state.Status;
				this.TenantHint = state.TenantHint;
			}

			// Token: 0x170001EC RID: 492
			// (get) Token: 0x060007BF RID: 1983 RVA: 0x00026EEF File Offset: 0x000250EF
			// (set) Token: 0x060007C0 RID: 1984 RVA: 0x00026EF7 File Offset: 0x000250F7
			public bool CountersAlreadyPatched { get; private set; }

			// Token: 0x170001ED RID: 493
			// (get) Token: 0x060007C1 RID: 1985 RVA: 0x00026F00 File Offset: 0x00025100
			// (set) Token: 0x060007C2 RID: 1986 RVA: 0x00026F08 File Offset: 0x00025108
			public bool CurrentlyActive { get; private set; }

			// Token: 0x170001EE RID: 494
			// (get) Token: 0x060007C3 RID: 1987 RVA: 0x00026F11 File Offset: 0x00025111
			// (set) Token: 0x060007C4 RID: 1988 RVA: 0x00026F19 File Offset: 0x00025119
			public Guid DatabaseGuid { get; private set; }

			// Token: 0x170001EF RID: 495
			// (get) Token: 0x060007C5 RID: 1989 RVA: 0x00026F22 File Offset: 0x00025122
			// (set) Token: 0x060007C6 RID: 1990 RVA: 0x00026F2A File Offset: 0x0002512A
			public ulong GlobalCnLowWatermark { get; private set; }

			// Token: 0x170001F0 RID: 496
			// (get) Token: 0x060007C7 RID: 1991 RVA: 0x00026F33 File Offset: 0x00025133
			// (set) Token: 0x060007C8 RID: 1992 RVA: 0x00026F3B File Offset: 0x0002513B
			public ulong GlobalIdLowWatermark { get; private set; }

			// Token: 0x170001F1 RID: 497
			// (get) Token: 0x060007C9 RID: 1993 RVA: 0x00026F44 File Offset: 0x00025144
			// (set) Token: 0x060007CA RID: 1994 RVA: 0x00026F4C File Offset: 0x0002514C
			public bool IsAccessible { get; private set; }

			// Token: 0x170001F2 RID: 498
			// (get) Token: 0x060007CB RID: 1995 RVA: 0x00026F55 File Offset: 0x00025155
			// (set) Token: 0x060007CC RID: 1996 RVA: 0x00026F5D File Offset: 0x0002515D
			public bool IsCurrent { get; private set; }

			// Token: 0x170001F3 RID: 499
			// (get) Token: 0x060007CD RID: 1997 RVA: 0x00026F66 File Offset: 0x00025166
			// (set) Token: 0x060007CE RID: 1998 RVA: 0x00026F6E File Offset: 0x0002516E
			public bool IsDeleted { get; private set; }

			// Token: 0x170001F4 RID: 500
			// (get) Token: 0x060007CF RID: 1999 RVA: 0x00026F77 File Offset: 0x00025177
			// (set) Token: 0x060007D0 RID: 2000 RVA: 0x00026F7F File Offset: 0x0002517F
			public bool IsDisabled { get; private set; }

			// Token: 0x170001F5 RID: 501
			// (get) Token: 0x060007D1 RID: 2001 RVA: 0x00026F88 File Offset: 0x00025188
			// (set) Token: 0x060007D2 RID: 2002 RVA: 0x00026F90 File Offset: 0x00025190
			public bool IsDisconnected { get; private set; }

			// Token: 0x170001F6 RID: 502
			// (get) Token: 0x060007D3 RID: 2003 RVA: 0x00026F99 File Offset: 0x00025199
			// (set) Token: 0x060007D4 RID: 2004 RVA: 0x00026FA1 File Offset: 0x000251A1
			public bool IsHardDeleted { get; private set; }

			// Token: 0x170001F7 RID: 503
			// (get) Token: 0x060007D5 RID: 2005 RVA: 0x00026FAA File Offset: 0x000251AA
			// (set) Token: 0x060007D6 RID: 2006 RVA: 0x00026FB2 File Offset: 0x000251B2
			public bool IsNew { get; private set; }

			// Token: 0x170001F8 RID: 504
			// (get) Token: 0x060007D7 RID: 2007 RVA: 0x00026FBB File Offset: 0x000251BB
			// (set) Token: 0x060007D8 RID: 2008 RVA: 0x00026FC3 File Offset: 0x000251C3
			public bool IsRemoved { get; private set; }

			// Token: 0x170001F9 RID: 505
			// (get) Token: 0x060007D9 RID: 2009 RVA: 0x00026FCC File Offset: 0x000251CC
			// (set) Token: 0x060007DA RID: 2010 RVA: 0x00026FD4 File Offset: 0x000251D4
			public bool IsSoftDeleted { get; private set; }

			// Token: 0x170001FA RID: 506
			// (get) Token: 0x060007DB RID: 2011 RVA: 0x00026FDD File Offset: 0x000251DD
			// (set) Token: 0x060007DC RID: 2012 RVA: 0x00026FE5 File Offset: 0x000251E5
			public bool IsTombstone { get; private set; }

			// Token: 0x170001FB RID: 507
			// (get) Token: 0x060007DD RID: 2013 RVA: 0x00026FEE File Offset: 0x000251EE
			// (set) Token: 0x060007DE RID: 2014 RVA: 0x00026FF6 File Offset: 0x000251F6
			public bool IsUserAccessible { get; private set; }

			// Token: 0x170001FC RID: 508
			// (get) Token: 0x060007DF RID: 2015 RVA: 0x00026FFF File Offset: 0x000251FF
			// (set) Token: 0x060007E0 RID: 2016 RVA: 0x00027007 File Offset: 0x00025207
			public bool IsValid { get; private set; }

			// Token: 0x170001FD RID: 509
			// (get) Token: 0x060007E1 RID: 2017 RVA: 0x00027010 File Offset: 0x00025210
			// (set) Token: 0x060007E2 RID: 2018 RVA: 0x00027018 File Offset: 0x00025218
			public DateTime LastUpdatedActiveTime { get; private set; }

			// Token: 0x170001FE RID: 510
			// (get) Token: 0x060007E3 RID: 2019 RVA: 0x00027021 File Offset: 0x00025221
			// (set) Token: 0x060007E4 RID: 2020 RVA: 0x00027029 File Offset: 0x00025229
			public Guid MailboxGuid { get; private set; }

			// Token: 0x170001FF RID: 511
			// (get) Token: 0x060007E5 RID: 2021 RVA: 0x00027032 File Offset: 0x00025232
			// (set) Token: 0x060007E6 RID: 2022 RVA: 0x0002703A File Offset: 0x0002523A
			public Guid MailboxInstanceGuid { get; private set; }

			// Token: 0x17000200 RID: 512
			// (get) Token: 0x060007E7 RID: 2023 RVA: 0x00027043 File Offset: 0x00025243
			// (set) Token: 0x060007E8 RID: 2024 RVA: 0x0002704B File Offset: 0x0002524B
			public int MailboxNumber { get; private set; }

			// Token: 0x17000201 RID: 513
			// (get) Token: 0x060007E9 RID: 2025 RVA: 0x00027054 File Offset: 0x00025254
			// (set) Token: 0x060007EA RID: 2026 RVA: 0x0002705C File Offset: 0x0002525C
			public MailboxInfo.MailboxType MailboxType { get; private set; }

			// Token: 0x17000202 RID: 514
			// (get) Token: 0x060007EB RID: 2027 RVA: 0x00027065 File Offset: 0x00025265
			// (set) Token: 0x060007EC RID: 2028 RVA: 0x0002706D File Offset: 0x0002526D
			public bool Quarantined { get; private set; }

			// Token: 0x17000203 RID: 515
			// (get) Token: 0x060007ED RID: 2029 RVA: 0x00027076 File Offset: 0x00025276
			// (set) Token: 0x060007EE RID: 2030 RVA: 0x0002707E File Offset: 0x0002527E
			public MailboxStatus Status { get; private set; }

			// Token: 0x17000204 RID: 516
			// (get) Token: 0x060007EF RID: 2031 RVA: 0x00027087 File Offset: 0x00025287
			// (set) Token: 0x060007F0 RID: 2032 RVA: 0x0002708F File Offset: 0x0002528F
			public TenantHint TenantHint { get; private set; }
		}
	}
}
