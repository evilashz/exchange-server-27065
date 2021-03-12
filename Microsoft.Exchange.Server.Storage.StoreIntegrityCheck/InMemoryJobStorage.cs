using System;
using System.Collections.Generic;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.StoreCommonServices;

namespace Microsoft.Exchange.Server.Storage.StoreIntegrityCheck
{
	// Token: 0x02000015 RID: 21
	public class InMemoryJobStorage : IJobStorage
	{
		// Token: 0x06000065 RID: 101 RVA: 0x00004DC0 File Offset: 0x00002FC0
		internal InMemoryJobStorage(int initialCapacity, StorePerDatabasePerformanceCountersInstance perfCounters)
		{
			this.capacity = initialCapacity;
			this.jobIdToJobMap = new Dictionary<Guid, IntegrityCheckJob>(initialCapacity);
			this.mailboxGuidToJobIndex = new Dictionary<Guid, HashSet<Guid>>(initialCapacity);
			this.requestGuidToJobIndex = new Dictionary<Guid, HashSet<Guid>>(initialCapacity);
			this.perfCounters = perfCounters;
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000066 RID: 102 RVA: 0x00004DFA File Offset: 0x00002FFA
		public bool IsEmpty
		{
			get
			{
				return this.jobIdToJobMap.Count == 0;
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000067 RID: 103 RVA: 0x00004E0A File Offset: 0x0000300A
		public bool IsFull
		{
			get
			{
				return this.jobIdToJobMap.Count > this.capacity;
			}
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00004E1F File Offset: 0x0000301F
		public static InMemoryJobStorage Instance(StoreDatabase database)
		{
			return database.ComponentData[InMemoryJobStorage.jobStorageSlot] as InMemoryJobStorage;
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00004E36 File Offset: 0x00003036
		public static IEnumerable<IntegrityCheckJob> GetRequestQueueSnapshot(Context context)
		{
			return InMemoryJobStorage.Instance(context.Database).GetAllJobs();
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00004E48 File Offset: 0x00003048
		public void AddJob(IntegrityCheckJob job)
		{
			using (LockManager.Lock(this))
			{
				this.jobIdToJobMap.Add(job.JobGuid, job);
				HashSet<Guid> hashSet;
				if (this.mailboxGuidToJobIndex.TryGetValue(job.MailboxGuid, out hashSet) && hashSet != null)
				{
					hashSet.Add(job.JobGuid);
				}
				else
				{
					hashSet = new HashSet<Guid>();
					hashSet.Add(job.JobGuid);
					this.mailboxGuidToJobIndex[job.MailboxGuid] = hashSet;
				}
				HashSet<Guid> hashSet2;
				if (this.requestGuidToJobIndex.TryGetValue(job.RequestGuid, out hashSet2) && hashSet2 != null)
				{
					hashSet2.Add(job.JobGuid);
				}
				else
				{
					hashSet2 = new HashSet<Guid>();
					hashSet2.Add(job.JobGuid);
					this.requestGuidToJobIndex[job.RequestGuid] = hashSet2;
				}
				this.perfCounters.ISIntegStoreTotalJobs.Increment();
			}
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00004F38 File Offset: 0x00003138
		public void RemoveJob(Guid jobGuid)
		{
			using (LockManager.Lock(this))
			{
				IntegrityCheckJob integrityCheckJob;
				if (this.jobIdToJobMap.TryGetValue(jobGuid, out integrityCheckJob))
				{
					this.jobIdToJobMap.Remove(jobGuid);
					HashSet<Guid> hashSet;
					if (this.mailboxGuidToJobIndex.TryGetValue(integrityCheckJob.MailboxGuid, out hashSet) && hashSet != null)
					{
						hashSet.Remove(jobGuid);
					}
					HashSet<Guid> hashSet2;
					if (this.requestGuidToJobIndex.TryGetValue(integrityCheckJob.RequestGuid, out hashSet2) && hashSet2 != null)
					{
						hashSet2.Remove(jobGuid);
					}
					this.perfCounters.ISIntegStoreTotalJobs.Decrement();
				}
			}
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00004FDC File Offset: 0x000031DC
		public IntegrityCheckJob GetJob(Guid jobGuid)
		{
			IntegrityCheckJob result;
			using (LockManager.Lock(this))
			{
				IntegrityCheckJob integrityCheckJob = null;
				if (this.jobIdToJobMap.TryGetValue(jobGuid, out integrityCheckJob))
				{
					result = integrityCheckJob;
				}
				else
				{
					result = null;
				}
			}
			return result;
		}

		// Token: 0x0600006D RID: 109 RVA: 0x0000502C File Offset: 0x0000322C
		public IEnumerable<IntegrityCheckJob> GetJobsByRequestGuid(Guid requestGuid)
		{
			List<IntegrityCheckJob> list = null;
			using (LockManager.Lock(this))
			{
				HashSet<Guid> hashSet;
				if (this.requestGuidToJobIndex.TryGetValue(requestGuid, out hashSet) && hashSet != null)
				{
					list = new List<IntegrityCheckJob>(hashSet.Count);
					foreach (Guid key in hashSet)
					{
						list.Add(this.jobIdToJobMap[key]);
					}
				}
			}
			return list;
		}

		// Token: 0x0600006E RID: 110 RVA: 0x000050CC File Offset: 0x000032CC
		public IEnumerable<IntegrityCheckJob> GetJobsByMailboxGuid(Guid mailboxGuid)
		{
			List<IntegrityCheckJob> list = null;
			using (LockManager.Lock(this))
			{
				HashSet<Guid> hashSet;
				if (this.mailboxGuidToJobIndex.TryGetValue(mailboxGuid, out hashSet) && hashSet != null)
				{
					list = new List<IntegrityCheckJob>(hashSet.Count);
					foreach (Guid key in hashSet)
					{
						list.Add(this.jobIdToJobMap[key]);
					}
				}
			}
			return list;
		}

		// Token: 0x0600006F RID: 111 RVA: 0x0000516C File Offset: 0x0000336C
		public IEnumerable<IntegrityCheckJob> GetAllJobs()
		{
			List<IntegrityCheckJob> result = null;
			using (LockManager.Lock(this))
			{
				result = new List<IntegrityCheckJob>(this.jobIdToJobMap.Values);
			}
			return result;
		}

		// Token: 0x06000070 RID: 112 RVA: 0x000051B4 File Offset: 0x000033B4
		internal static void Initialize()
		{
			if (InMemoryJobStorage.jobStorageSlot == -1)
			{
				InMemoryJobStorage.jobStorageSlot = StoreDatabase.AllocateComponentDataSlot();
			}
		}

		// Token: 0x06000071 RID: 113 RVA: 0x000051C8 File Offset: 0x000033C8
		internal static void MountEventHandler(Context context, StoreDatabase database)
		{
			database.ComponentData[InMemoryJobStorage.jobStorageSlot] = new InMemoryJobStorage(ConfigurationSchema.IntegrityCheckJobStorageCapacity.Value, PerformanceCounterFactory.GetDatabaseInstance(database));
		}

		// Token: 0x06000072 RID: 114 RVA: 0x000051EF File Offset: 0x000033EF
		internal static void DismountEventHandler(StoreDatabase database)
		{
			database.ComponentData[InMemoryJobStorage.jobStorageSlot] = null;
		}

		// Token: 0x04000019 RID: 25
		private static int jobStorageSlot = -1;

		// Token: 0x0400001A RID: 26
		private readonly int capacity;

		// Token: 0x0400001B RID: 27
		private readonly StorePerDatabasePerformanceCountersInstance perfCounters;

		// Token: 0x0400001C RID: 28
		private Dictionary<Guid, IntegrityCheckJob> jobIdToJobMap;

		// Token: 0x0400001D RID: 29
		private Dictionary<Guid, HashSet<Guid>> mailboxGuidToJobIndex;

		// Token: 0x0400001E RID: 30
		private Dictionary<Guid, HashSet<Guid>> requestGuidToJobIndex;
	}
}
