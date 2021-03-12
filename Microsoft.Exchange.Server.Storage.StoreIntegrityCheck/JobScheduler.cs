using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ManagedStore.StoreIntegrityCheck;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.StoreCommonServices;

namespace Microsoft.Exchange.Server.Storage.StoreIntegrityCheck
{
	// Token: 0x02000025 RID: 37
	public class JobScheduler : IJobScheduler
	{
		// Token: 0x060000BF RID: 191 RVA: 0x000064EC File Offset: 0x000046EC
		public JobScheduler(StoreDatabase database)
		{
			this.database = database;
			this.readyQueue = new JobScheduler.PriorityQueue();
			this.runningQueue = new HashSet<Guid>();
			this.ageoutQueue = new Queue<IntegrityCheckJob>();
			this.perfCounters = PerformanceCounterFactory.GetDatabaseInstance(database);
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060000C0 RID: 192 RVA: 0x00006528 File Offset: 0x00004728
		public static TimeSpan ScheduleInterval
		{
			get
			{
				return JobScheduler.interval;
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060000C1 RID: 193 RVA: 0x0000652F File Offset: 0x0000472F
		public StoreDatabase Database
		{
			get
			{
				return this.database;
			}
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x00006537 File Offset: 0x00004737
		public static JobScheduler Instance(StoreDatabase database)
		{
			return database.ComponentData[JobScheduler.jobSchedulerSlot] as JobScheduler;
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x00006550 File Offset: 0x00004750
		public void ScheduleJob(IntegrityCheckJob job)
		{
			((IJobStateTracker)job).MoveToState(JobState.Pending);
			using (LockManager.Lock(this))
			{
				this.readyQueue.Enqueue(job);
			}
			if (this.perfCounters != null)
			{
				this.perfCounters.ISIntegStorePendingJobs.Increment();
			}
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x000065B0 File Offset: 0x000047B0
		public void RemoveJob(IntegrityCheckJob job)
		{
			bool flag;
			using (LockManager.Lock(this))
			{
				flag = this.readyQueue.Remove(job.JobGuid);
			}
			if (flag && this.perfCounters != null)
			{
				this.perfCounters.ISIntegStorePendingJobs.Decrement();
			}
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x00006614 File Offset: 0x00004814
		public void ExecuteJob(Context context, IntegrityCheckJob job)
		{
			using (LockManager.Lock(this))
			{
				if (job != null)
				{
					if (this.runningQueue.Contains(job.JobGuid) || this.ageoutQueue.Contains(job))
					{
						return;
					}
					this.readyQueue.Dequeue(job.JobGuid);
				}
				else
				{
					job = this.readyQueue.Dequeue(JobPriority.High, JobPriority.Low);
				}
				if (job == null)
				{
					return;
				}
			}
			if (this.perfCounters != null)
			{
				this.perfCounters.ISIntegStorePendingJobs.Decrement();
			}
			try
			{
				using (LockManager.Lock(this))
				{
					this.runningQueue.Add(job.JobGuid);
				}
				((IJobStateTracker)job).MoveToState(JobState.Running);
				JobRunner jobRunner = new JobRunner(job, job, job);
				jobRunner.Run(context);
			}
			finally
			{
				using (LockManager.Lock(this))
				{
					this.runningQueue.Remove(job.JobGuid);
					this.ageoutQueue.Enqueue(job);
				}
				if (job.State == JobState.Failed && this.perfCounters != null)
				{
					this.perfCounters.ISIntegStoreFailedJobs.Decrement();
				}
			}
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x00006774 File Offset: 0x00004974
		public IEnumerable<IntegrityCheckJob> GetReadyJobs(JobPriority priority)
		{
			IEnumerable<IntegrityCheckJob> readyJobs;
			using (LockManager.Lock(this))
			{
				readyJobs = this.readyQueue.GetReadyJobs(priority);
			}
			return readyJobs;
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x000067B8 File Offset: 0x000049B8
		internal static void Initialize()
		{
			if (JobScheduler.jobSchedulerSlot == -1)
			{
				JobScheduler.jobSchedulerSlot = StoreDatabase.AllocateComponentDataSlot();
			}
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x000067CC File Offset: 0x000049CC
		internal static void MountEventHandler(Context context, StoreDatabase database, bool readOnly)
		{
			JobScheduler jobScheduler = new JobScheduler(database);
			database.ComponentData[JobScheduler.jobSchedulerSlot] = jobScheduler;
			if (!readOnly)
			{
				Task<JobScheduler>.TaskCallback callback = TaskExecutionWrapper<JobScheduler>.WrapExecute(new TaskDiagnosticInformation(TaskTypeId.OnlineIntegrityCheck, ClientType.System, database.MdbGuid), new TaskExecutionWrapper<JobScheduler>.TaskCallback<Context>(jobScheduler.IntegrityCheckJobScheduleThread));
				RecurringTask<JobScheduler> task = new RecurringTask<JobScheduler>(callback, jobScheduler, JobScheduler.interval, false);
				database.TaskList.Add(task, true);
			}
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x00006830 File Offset: 0x00004A30
		internal static void DismountEventHandler(StoreDatabase database)
		{
			database.ComponentData[JobScheduler.jobSchedulerSlot] = null;
		}

		// Token: 0x060000CA RID: 202 RVA: 0x00006844 File Offset: 0x00004A44
		private void IntegrityCheckJobScheduleThread(Context context, JobScheduler jobScheduler, Func<bool> shouldCallbackContinue)
		{
			if (ExTraceGlobals.OnlineIsintegTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				ExTraceGlobals.OnlineIsintegTracer.TraceDebug(0L, "Integrity check job manager thread start running on database \"" + jobScheduler.Database.MdbName + "\"");
			}
			for (int i = 0; i < JobScheduler.jobBatchSize; i++)
			{
				IntegrityCheckJob integrityCheckJob = null;
				using (LockManager.Lock(this))
				{
					integrityCheckJob = this.readyQueue.Peek(JobPriority.High, JobPriority.High);
				}
				if (integrityCheckJob == null)
				{
					break;
				}
				using (context.AssociateWithDatabase(this.Database))
				{
					this.ExecuteJob(context, integrityCheckJob);
				}
			}
			this.AgeoutOldJobs();
		}

		// Token: 0x060000CB RID: 203 RVA: 0x00006908 File Offset: 0x00004B08
		private void AgeoutOldJobs()
		{
			IntegrityCheckJob integrityCheckJob = null;
			do
			{
				using (LockManager.Lock(this))
				{
					integrityCheckJob = null;
					if (this.ageoutQueue.Count != 0)
					{
						integrityCheckJob = this.ageoutQueue.Peek();
						if (integrityCheckJob.CompletedTime == null || DateTime.UtcNow - integrityCheckJob.CompletedTime <= ConfigurationSchema.IntegrityCheckJobAgeoutTimeSpan.Value)
						{
							integrityCheckJob = null;
						}
						else
						{
							integrityCheckJob = this.ageoutQueue.Dequeue();
						}
					}
				}
				if (integrityCheckJob != null)
				{
					InMemoryJobStorage.Instance(this.database).RemoveJob(integrityCheckJob.JobGuid);
					if (ExTraceGlobals.OnlineIsintegTracer.IsTraceEnabled(TraceType.DebugTrace))
					{
						ExTraceGlobals.OnlineIsintegTracer.TraceDebug<Guid>(0L, "Integrity check job {0} aged out", integrityCheckJob.JobGuid);
					}
				}
			}
			while (integrityCheckJob != null);
		}

		// Token: 0x0400008F RID: 143
		private const int InitialCapacity = 500;

		// Token: 0x04000090 RID: 144
		private static int jobSchedulerSlot = -1;

		// Token: 0x04000091 RID: 145
		private static int jobBatchSize = 50;

		// Token: 0x04000092 RID: 146
		private static TimeSpan interval = TimeSpan.FromSeconds(30.0);

		// Token: 0x04000093 RID: 147
		private StoreDatabase database;

		// Token: 0x04000094 RID: 148
		private StorePerDatabasePerformanceCountersInstance perfCounters;

		// Token: 0x04000095 RID: 149
		private JobScheduler.PriorityQueue readyQueue;

		// Token: 0x04000096 RID: 150
		private HashSet<Guid> runningQueue;

		// Token: 0x04000097 RID: 151
		private Queue<IntegrityCheckJob> ageoutQueue;

		// Token: 0x02000026 RID: 38
		private class PriorityQueue
		{
			// Token: 0x060000CD RID: 205 RVA: 0x00006A44 File Offset: 0x00004C44
			public PriorityQueue()
			{
				this.map = new Dictionary<Guid, LinkedListNode<IntegrityCheckJob>>();
				this.activeQueue = new List<LinkedList<IntegrityCheckJob>>();
				for (int i = 0; i <= 2; i++)
				{
					this.activeQueue.Add(new LinkedList<IntegrityCheckJob>());
				}
			}

			// Token: 0x17000046 RID: 70
			// (get) Token: 0x060000CE RID: 206 RVA: 0x00006A89 File Offset: 0x00004C89
			public bool IsEmpty
			{
				get
				{
					return this.map.Count == 0;
				}
			}

			// Token: 0x060000CF RID: 207 RVA: 0x00006A9C File Offset: 0x00004C9C
			public void Enqueue(IntegrityCheckJob job)
			{
				LinkedListNode<IntegrityCheckJob> linkedListNode = new LinkedListNode<IntegrityCheckJob>(job);
				this.map.Add(job.JobGuid, linkedListNode);
				this.activeQueue[(int)job.Priority].AddLast(linkedListNode);
			}

			// Token: 0x060000D0 RID: 208 RVA: 0x00006ADC File Offset: 0x00004CDC
			public IntegrityCheckJob Dequeue(Guid jobGuid)
			{
				IntegrityCheckJob integrityCheckJob = this.Peek(jobGuid);
				if (integrityCheckJob != null)
				{
					this.Remove(integrityCheckJob);
				}
				return integrityCheckJob;
			}

			// Token: 0x060000D1 RID: 209 RVA: 0x00006AFC File Offset: 0x00004CFC
			public IntegrityCheckJob Dequeue(JobPriority from, JobPriority to)
			{
				IntegrityCheckJob integrityCheckJob = this.Peek(from, to);
				if (integrityCheckJob != null)
				{
					this.Remove(integrityCheckJob);
				}
				return integrityCheckJob;
			}

			// Token: 0x060000D2 RID: 210 RVA: 0x00006B20 File Offset: 0x00004D20
			public bool Remove(Guid jobGuid)
			{
				bool result = false;
				LinkedListNode<IntegrityCheckJob> linkedListNode = null;
				if (!this.IsEmpty && this.map.TryGetValue(jobGuid, out linkedListNode) && linkedListNode != null)
				{
					this.map.Remove(jobGuid);
					this.activeQueue[(int)linkedListNode.Value.Priority].Remove(linkedListNode);
					result = true;
				}
				return result;
			}

			// Token: 0x060000D3 RID: 211 RVA: 0x00006B78 File Offset: 0x00004D78
			public void Remove(IntegrityCheckJob job)
			{
				this.Remove(job.JobGuid);
			}

			// Token: 0x060000D4 RID: 212 RVA: 0x00006B88 File Offset: 0x00004D88
			public IEnumerable<IntegrityCheckJob> GetReadyJobs(JobPriority priority)
			{
				if (!this.IsEmpty && this.activeQueue[(int)priority].Count != 0)
				{
					List<IntegrityCheckJob> list = new List<IntegrityCheckJob>(JobScheduler.jobBatchSize);
					LinkedListNode<IntegrityCheckJob> linkedListNode = this.activeQueue[(int)priority].First;
					int num = 0;
					do
					{
						list.Add(linkedListNode.Value);
						linkedListNode = linkedListNode.Next;
						num++;
					}
					while (num < JobScheduler.jobBatchSize && linkedListNode != null);
					return list;
				}
				return null;
			}

			// Token: 0x060000D5 RID: 213 RVA: 0x00006BF4 File Offset: 0x00004DF4
			public IntegrityCheckJob Peek(Guid jobGuid)
			{
				if (!this.IsEmpty)
				{
					LinkedListNode<IntegrityCheckJob> linkedListNode = null;
					if (this.map.TryGetValue(jobGuid, out linkedListNode))
					{
						return linkedListNode.Value;
					}
				}
				return null;
			}

			// Token: 0x060000D6 RID: 214 RVA: 0x00006C24 File Offset: 0x00004E24
			public IntegrityCheckJob Peek(JobPriority from, JobPriority to)
			{
				if (!this.IsEmpty)
				{
					LinkedList<IntegrityCheckJob> linkedList = null;
					for (int i = (int)from; i <= (int)to; i++)
					{
						if (this.activeQueue[i].Count != 0)
						{
							linkedList = this.activeQueue[i];
							break;
						}
					}
					if (linkedList != null)
					{
						return linkedList.First.Value;
					}
				}
				return null;
			}

			// Token: 0x04000098 RID: 152
			private Dictionary<Guid, LinkedListNode<IntegrityCheckJob>> map;

			// Token: 0x04000099 RID: 153
			private List<LinkedList<IntegrityCheckJob>> activeQueue;
		}
	}
}
