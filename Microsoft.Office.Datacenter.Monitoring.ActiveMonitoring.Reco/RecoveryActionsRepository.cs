using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Office.Datacenter.Monitoring.ActiveMonitoring.Recovery
{
	// Token: 0x02000046 RID: 70
	public class RecoveryActionsRepository
	{
		// Token: 0x060002E6 RID: 742 RVA: 0x00009B37 File Offset: 0x00007D37
		private RecoveryActionsRepository()
		{
		}

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x060002E7 RID: 743 RVA: 0x00009B60 File Offset: 0x00007D60
		public static RecoveryActionsRepository Instance
		{
			get
			{
				if (RecoveryActionsRepository.instance == null)
				{
					lock (RecoveryActionsRepository.initLock)
					{
						if (RecoveryActionsRepository.instance == null)
						{
							RecoveryActionsRepository.instance = new RecoveryActionsRepository();
						}
					}
				}
				return RecoveryActionsRepository.instance;
			}
		}

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x060002E8 RID: 744 RVA: 0x00009BB8 File Offset: 0x00007DB8
		// (set) Token: 0x060002E9 RID: 745 RVA: 0x00009BC0 File Offset: 0x00007DC0
		internal string LocalServerName { get; set; }

		// Token: 0x060002EA RID: 746 RVA: 0x00009BE4 File Offset: 0x00007DE4
		public void Initialize(Tuple<RecoveryActionEntry, RecoveryActionEntry> tuple, bool isCrimsonStoreEnabled = true, string localServerName = null)
		{
			if (this.isInitAttempted)
			{
				return;
			}
			lock (this)
			{
				if (!this.isInitAttempted)
				{
					this.isInitAttempted = true;
					this.isCrimsonStoreEnabled = isCrimsonStoreEnabled;
					this.LocalServerName = (localServerName ?? Dependencies.ThrottleHelper.Tunables.LocalMachineName);
					ThreadPool.QueueUserWorkItem(delegate(object o)
					{
						this.InitializeInternal(tuple);
					});
				}
			}
		}

		// Token: 0x060002EB RID: 747 RVA: 0x00009C80 File Offset: 0x00007E80
		internal static RecoveryActionsRepository CreateTestInstance(bool isCrimsonStoreEnabled, string localServerName)
		{
			RecoveryActionsRepository recoveryActionsRepository = new RecoveryActionsRepository();
			recoveryActionsRepository.Initialize(null, isCrimsonStoreEnabled, localServerName);
			return recoveryActionsRepository;
		}

		// Token: 0x060002EC RID: 748 RVA: 0x00009CA0 File Offset: 0x00007EA0
		internal static bool IsRecoveryInProgress(RpcGetThrottlingStatisticsImpl.ThrottlingStatistics ts)
		{
			if (ts.MostRecentEntry == null)
			{
				return false;
			}
			if (ts.MostRecentEntry.State != RecoveryActionState.Started)
			{
				return false;
			}
			if (ts.MostRecentEntry.LamProcessStartTime < ts.WorkerProcessStartTime)
			{
				return false;
			}
			if (ts.MostRecentEntry.LamProcessStartTime < ts.HostProcessStartTime)
			{
				return false;
			}
			DateTime localTime = ExDateTime.Now.LocalTime;
			return !(localTime > ts.MostRecentEntry.EndTime);
		}

		// Token: 0x060002ED RID: 749 RVA: 0x00009D48 File Offset: 0x00007F48
		internal void PopulateEntriesFromCrimson(Tuple<RecoveryActionEntry, RecoveryActionEntry> tuple)
		{
			DateTime localTime = ExDateTime.Now.LocalTime;
			DateTime dateTime = localTime.AddHours(-25.0);
			DateTime dateTime2 = localTime.AddSeconds(2.0);
			int entriesCount = 0;
			Stopwatch stopwatch = new Stopwatch();
			stopwatch.Start();
			TimeSpan timeSpan = TimeSpan.FromSeconds(300.0);
			try
			{
				bool flag;
				RecoveryActionHelper.ForeachMatching(delegate(RecoveryActionEntry entry, bool isNewestToOldest)
				{
					entriesCount++;
					this.AddEntryInternal(entry, false, isNewestToOldest);
					return false;
				}, RecoveryActionId.None, null, null, RecoveryActionState.None, RecoveryActionResult.None, dateTime, dateTime2, out flag, null, TimeSpan.FromSeconds(300.0), int.MaxValue);
				if (tuple != null)
				{
					RecoveryActionEntry item = tuple.Item1;
					if (item != null && !this.IsEntryExist(item))
					{
						this.AddEntryInternal(item, false, false);
						entriesCount++;
					}
					RecoveryActionEntry item2 = tuple.Item2;
					if (item2 != null && !this.IsEntryExist(item2))
					{
						this.AddEntryInternal(item2, false, false);
						entriesCount++;
					}
				}
				ManagedAvailabilityCrimsonEvents.RecoveryActionRepositoryInitSuccess.Log<string, int, DateTime, DateTime, TimeSpan, string>(stopwatch.Elapsed.ToString(), entriesCount, dateTime, dateTime2, timeSpan, "<none>");
			}
			catch (Exception ex)
			{
				ManagedAvailabilityCrimsonEvents.RecoveryActionRepositoryInitFailed.Log<TimeSpan, int, DateTime, DateTime, TimeSpan, string>(stopwatch.Elapsed, entriesCount, dateTime, dateTime2, timeSpan, ex.ToString());
			}
		}

		// Token: 0x060002EE RID: 750 RVA: 0x00009EBC File Offset: 0x000080BC
		internal void WaitUntilInitializationComplete()
		{
			if (!this.isInitCompleted)
			{
				this.initCompleteEvent.WaitOne(TimeSpan.FromSeconds(300.0));
			}
		}

		// Token: 0x060002EF RID: 751 RVA: 0x00009EE0 File Offset: 0x000080E0
		internal void AddEntry(RecoveryActionEntry entry, bool isWritePersistentStore = true, bool isNewestToOldest = false)
		{
			this.WaitUntilInitializationComplete();
			this.AddEntryInternal(entry, this.isCrimsonStoreEnabled && isWritePersistentStore, isNewestToOldest);
		}

		// Token: 0x060002F0 RID: 752 RVA: 0x00009EFC File Offset: 0x000080FC
		internal void AddEntry(RecoveryActionHelper.RecoveryActionEntrySerializable serializedEntry, bool isWritePersistentStore = true, bool isNewestToOldest = false)
		{
			if (serializedEntry != null)
			{
				this.AddEntry(new RecoveryActionEntry(serializedEntry), this.isCrimsonStoreEnabled && isWritePersistentStore, isNewestToOldest);
			}
		}

		// Token: 0x060002F1 RID: 753 RVA: 0x0000A038 File Offset: 0x00008238
		internal RpcGetThrottlingStatisticsImpl.ThrottlingStatistics GetThrottlingStatistics(RecoveryActionId actionId, string resourceName, int maxAllowedInOneHour, int maxAllowedInOneDay, bool isStopSearchWhenLimitExceeds = false, bool isCountFailedActions = false)
		{
			this.WaitUntilInitializationComplete();
			RpcGetThrottlingStatisticsImpl.ThrottlingStatistics throttlingStatistics = new RpcGetThrottlingStatisticsImpl.ThrottlingStatistics();
			throttlingStatistics.ServerName = this.LocalServerName;
			DateTime localTime = ExDateTime.Now.LocalTime;
			DateTime dayCutoffTime = localTime.AddDays(-1.0);
			DateTime hourCutoffTime = localTime.AddHours(-1.0);
			int totalActionsInDay = 0;
			int totalActionsInHour = 0;
			RecoveryActionEntry mostRecentEntry = null;
			RecoveryActionEntry entryExceedingOneHourLimit = null;
			RecoveryActionEntry entryExceedingOneDayLimit = null;
			int totalEntriesSearched = 0;
			this.SearchEntries(actionId, resourceName, dayCutoffTime, localTime, delegate(RecoveryActionEntry entry)
			{
				totalEntriesSearched++;
				if (entry.State == RecoveryActionState.Started && mostRecentEntry == null)
				{
					mostRecentEntry = entry;
				}
				if (entry.State == RecoveryActionState.Finished && (entry.Result == RecoveryActionResult.Succeeded || (isCountFailedActions && entry.Result == RecoveryActionResult.Failed)))
				{
					if (mostRecentEntry == null)
					{
						mostRecentEntry = entry;
					}
					if (entry.EndTime > hourCutoffTime)
					{
						totalActionsInHour++;
						if (maxAllowedInOneHour != -1 && totalActionsInHour == maxAllowedInOneHour)
						{
							if (entryExceedingOneHourLimit == null)
							{
								entryExceedingOneHourLimit = entry;
							}
							if (isStopSearchWhenLimitExceeds)
							{
								return false;
							}
						}
					}
					if (entry.EndTime > dayCutoffTime)
					{
						totalActionsInDay++;
						if (maxAllowedInOneDay != -1 && totalActionsInDay == maxAllowedInOneDay)
						{
							if (entryExceedingOneDayLimit == null)
							{
								entryExceedingOneDayLimit = entry;
							}
							if (isStopSearchWhenLimitExceeds)
							{
								return false;
							}
						}
					}
				}
				return true;
			});
			throttlingStatistics.TotalEntriesSearched = totalEntriesSearched;
			throttlingStatistics.NumberOfActionsInOneHour = totalActionsInHour;
			throttlingStatistics.NumberOfActionsInOneDay = totalActionsInDay;
			throttlingStatistics.MostRecentEntry = RecoveryActionHelper.CreateSerializableRecoveryActionEntry(mostRecentEntry);
			throttlingStatistics.EntryExceedingOneHourLimit = RecoveryActionHelper.CreateSerializableRecoveryActionEntry(entryExceedingOneHourLimit);
			throttlingStatistics.EntryExceedingOneDayLimit = RecoveryActionHelper.CreateSerializableRecoveryActionEntry(entryExceedingOneDayLimit);
			throttlingStatistics.WorkerProcessStartTime = WorkerProcessRepository.Instance.GetWorkerProcessStartTime();
			throttlingStatistics.HostProcessStartTime = this.localProcessTime;
			throttlingStatistics.SystemBootTime = RecoveryActionHelper.GetSystemBootTime();
			RpcSetThrottlingInProgressImpl.ThrottlingProgressInfo throttlingProgressInfo = this.throttlingProgressRepository.Get(actionId, resourceName);
			throttlingStatistics.ThrottleProgressInfo = throttlingProgressInfo;
			throttlingStatistics.IsThrottlingInProgress = (throttlingProgressInfo != null && throttlingProgressInfo.IsInProgress(throttlingStatistics.WorkerProcessStartTime));
			throttlingStatistics.IsRecoveryInProgress = RecoveryActionsRepository.IsRecoveryInProgress(throttlingStatistics);
			return throttlingStatistics;
		}

		// Token: 0x060002F2 RID: 754 RVA: 0x0000A1BA File Offset: 0x000083BA
		internal RpcSetThrottlingInProgressImpl.Reply UpdateThrottlingProgress(RpcSetThrottlingInProgressImpl.Request request)
		{
			return this.throttlingProgressRepository.Update(request);
		}

		// Token: 0x060002F3 RID: 755 RVA: 0x0000A208 File Offset: 0x00008408
		internal bool IsEntryExist(RecoveryActionEntry entry)
		{
			bool isFound = false;
			this.SearchEntries(entry.Id, null, DateTime.MinValue, DateTime.MaxValue, delegate(RecoveryActionEntry tmpEntry)
			{
				if (string.Equals(entry.InstanceId, tmpEntry.InstanceId, StringComparison.OrdinalIgnoreCase) && entry.State == tmpEntry.State)
				{
					isFound = true;
					return false;
				}
				return true;
			});
			return isFound;
		}

		// Token: 0x060002F4 RID: 756 RVA: 0x0000A2CC File Offset: 0x000084CC
		internal void SearchEntries(RecoveryActionId actionId, string resourceName, DateTime startTime, DateTime endTime, Func<RecoveryActionEntry, bool> func)
		{
			RecoveryActionsRepository.RecoveryActionList recoveryActionList = null;
			if (this.map.TryGetValue(actionId, out recoveryActionList))
			{
				recoveryActionList.ForEachUntil(delegate(RecoveryActionEntry entry)
				{
					if (resourceName != null && !string.Equals(resourceName, entry.ResourceName))
					{
						return true;
					}
					DateTime t = (entry.State == RecoveryActionState.Finished) ? entry.EndTime : entry.StartTime;
					return t < startTime || t > endTime || func(entry);
				});
			}
		}

		// Token: 0x060002F5 RID: 757 RVA: 0x0000A328 File Offset: 0x00008528
		private void InitializeInternal(Tuple<RecoveryActionEntry, RecoveryActionEntry> tuple)
		{
			using (Process currentProcess = Process.GetCurrentProcess())
			{
				this.localProcessTime = currentProcess.StartTime;
			}
			this.throttlingProgressRepository = new ThrottlingProgressRepository();
			if (this.isCrimsonStoreEnabled)
			{
				this.PopulateEntriesFromCrimson(tuple);
			}
			this.isInitCompleted = true;
			this.initCompleteEvent.Set();
		}

		// Token: 0x060002F6 RID: 758 RVA: 0x0000A398 File Offset: 0x00008598
		private void AddEntryInternal(RecoveryActionEntry entry, bool isWritePersistentStore, bool isNewestToOldest)
		{
			RecoveryActionsRepository.RecoveryActionList orAdd = this.map.GetOrAdd(entry.Id, (RecoveryActionId id) => new RecoveryActionsRepository.RecoveryActionList());
			orAdd.Add(entry, this.isCrimsonStoreEnabled && isWritePersistentStore, isNewestToOldest);
		}

		// Token: 0x040001CA RID: 458
		internal const int HoursToKeepInMemory = 25;

		// Token: 0x040001CB RID: 459
		internal const int SecondsToCompleteInit = 300;

		// Token: 0x040001CC RID: 460
		private static readonly object initLock = new object();

		// Token: 0x040001CD RID: 461
		private static RecoveryActionsRepository instance = null;

		// Token: 0x040001CE RID: 462
		private readonly ManualResetEvent initCompleteEvent = new ManualResetEvent(false);

		// Token: 0x040001CF RID: 463
		private ConcurrentDictionary<RecoveryActionId, RecoveryActionsRepository.RecoveryActionList> map = new ConcurrentDictionary<RecoveryActionId, RecoveryActionsRepository.RecoveryActionList>();

		// Token: 0x040001D0 RID: 464
		private DateTime localProcessTime;

		// Token: 0x040001D1 RID: 465
		private bool isInitAttempted;

		// Token: 0x040001D2 RID: 466
		private bool isInitCompleted;

		// Token: 0x040001D3 RID: 467
		private bool isCrimsonStoreEnabled = true;

		// Token: 0x040001D4 RID: 468
		private ThrottlingProgressRepository throttlingProgressRepository;

		// Token: 0x02000047 RID: 71
		internal class RecoveryActionList
		{
			// Token: 0x060002F9 RID: 761 RVA: 0x0000A3FA File Offset: 0x000085FA
			internal RecoveryActionList()
			{
				this.rwLock = new ReaderWriterLockSlim(LockRecursionPolicy.NoRecursion);
				this.Entries = new LinkedList<RecoveryActionEntry>();
			}

			// Token: 0x17000105 RID: 261
			// (get) Token: 0x060002FA RID: 762 RVA: 0x0000A419 File Offset: 0x00008619
			// (set) Token: 0x060002FB RID: 763 RVA: 0x0000A421 File Offset: 0x00008621
			internal LinkedList<RecoveryActionEntry> Entries { get; set; }

			// Token: 0x060002FC RID: 764 RVA: 0x0000A42C File Offset: 0x0000862C
			internal DateTime GetEntryTime(RecoveryActionEntry entry)
			{
				return (entry.State == RecoveryActionState.Finished) ? entry.EndTime : entry.StartTime;
			}

			// Token: 0x060002FD RID: 765 RVA: 0x0000A454 File Offset: 0x00008654
			internal void Add(RecoveryActionEntry entryToInsert, bool isWritePersistentStore, bool isNewestToOldest = true)
			{
				try
				{
					this.rwLock.EnterWriteLock();
					if (isWritePersistentStore)
					{
						entryToInsert.Write(null);
					}
					DateTime entryTime = this.GetEntryTime(entryToInsert);
					if (isNewestToOldest)
					{
						LinkedListNode<RecoveryActionEntry> linkedListNode = this.Entries.Last;
						while (linkedListNode != null)
						{
							DateTime entryTime2 = this.GetEntryTime(linkedListNode.Value);
							if (entryTime <= entryTime2)
							{
								if (entryTime == entryTime2 && entryToInsert.LocalDataAccessMetaData != null && linkedListNode.Value.LocalDataAccessMetaData != null && linkedListNode.Value.LocalDataAccessMetaData.RecordId < entryToInsert.LocalDataAccessMetaData.RecordId)
								{
									this.Entries.AddBefore(linkedListNode, entryToInsert);
									break;
								}
								this.Entries.AddAfter(linkedListNode, entryToInsert);
								break;
							}
							else
							{
								linkedListNode = linkedListNode.Previous;
							}
						}
						if (linkedListNode == null)
						{
							this.Entries.AddFirst(entryToInsert);
						}
					}
					else
					{
						LinkedListNode<RecoveryActionEntry> linkedListNode = this.Entries.First;
						while (linkedListNode != null)
						{
							DateTime entryTime3 = this.GetEntryTime(linkedListNode.Value);
							if (entryTime >= entryTime3)
							{
								if (entryTime == entryTime3 && entryToInsert.LocalDataAccessMetaData != null && linkedListNode.Value.LocalDataAccessMetaData != null && linkedListNode.Value.LocalDataAccessMetaData.RecordId > entryToInsert.LocalDataAccessMetaData.RecordId)
								{
									this.Entries.AddAfter(linkedListNode, entryToInsert);
									break;
								}
								this.Entries.AddBefore(linkedListNode, entryToInsert);
								break;
							}
							else
							{
								linkedListNode = linkedListNode.Next;
							}
						}
						if (linkedListNode == null)
						{
							this.Entries.AddLast(entryToInsert);
						}
					}
					this.PurgeOldEntriesIfRequired();
				}
				finally
				{
					this.rwLock.ExitWriteLock();
				}
			}

			// Token: 0x060002FE RID: 766 RVA: 0x0000A5F0 File Offset: 0x000087F0
			internal void ForEachUntil(Func<RecoveryActionEntry, bool> func)
			{
				try
				{
					this.rwLock.EnterReadLock();
					foreach (RecoveryActionEntry arg in this.Entries)
					{
						if (!func(arg))
						{
							break;
						}
					}
				}
				finally
				{
					this.rwLock.ExitReadLock();
				}
			}

			// Token: 0x060002FF RID: 767 RVA: 0x0000A66C File Offset: 0x0000886C
			private void PurgeOldEntriesIfRequired()
			{
				LinkedListNode<RecoveryActionEntry> linkedListNode = this.Entries.Last;
				DateTime t = ExDateTime.Now.LocalTime.AddHours(-25.0);
				while (linkedListNode != null)
				{
					DateTime entryTime = this.GetEntryTime(linkedListNode.Value);
					if (entryTime >= t)
					{
						return;
					}
					LinkedListNode<RecoveryActionEntry> previous = linkedListNode.Previous;
					this.Entries.Remove(linkedListNode);
					linkedListNode = previous;
				}
			}

			// Token: 0x040001D7 RID: 471
			private readonly ReaderWriterLockSlim rwLock;
		}
	}
}
