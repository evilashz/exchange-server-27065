using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.WorkloadManagement;

namespace Microsoft.Exchange.WorkloadManagement
{
	// Token: 0x0200002A RID: 42
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class ClassificationBlock
	{
		// Token: 0x06000168 RID: 360 RVA: 0x00006A0C File Offset: 0x00004C0C
		internal ClassificationBlock(WorkloadClassification classification)
		{
			this.WorkloadClassification = classification;
			this.perfCounter = new ClassificationPerfCounterWrapper(classification);
			ExTraceGlobals.SchedulerTracer.TraceDebug<WorkloadClassification>((long)this.GetHashCode(), "[ClassificationBlock.ctor] Created ClassificationBlock for {0}", classification);
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x06000169 RID: 361 RVA: 0x00006A5F File Offset: 0x00004C5F
		// (set) Token: 0x0600016A RID: 362 RVA: 0x00006A67 File Offset: 0x00004C67
		internal WorkloadClassification WorkloadClassification { get; private set; }

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x0600016B RID: 363 RVA: 0x00006A70 File Offset: 0x00004C70
		// (set) Token: 0x0600016C RID: 364 RVA: 0x00006A78 File Offset: 0x00004C78
		internal int FairnessFactor
		{
			get
			{
				return this.fairnessFactor;
			}
			set
			{
				lock (this.instanceLock)
				{
					this.fairnessFactor = value;
				}
				this.perfCounter.UpdateFairnessFactor((long)value);
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x0600016D RID: 365 RVA: 0x00006AC8 File Offset: 0x00004CC8
		internal int ActiveThreads
		{
			get
			{
				return this.activeThreadCount;
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x0600016E RID: 366 RVA: 0x00006AD0 File Offset: 0x00004CD0
		internal int WorkloadCount
		{
			get
			{
				return this.workloads.Count;
			}
		}

		// Token: 0x0600016F RID: 367 RVA: 0x00006AE0 File Offset: 0x00004CE0
		internal void Activate()
		{
			int num = Interlocked.Increment(ref this.activeThreadCount);
			this.perfCounter.UpdateActiveThreads((long)num);
		}

		// Token: 0x06000170 RID: 368 RVA: 0x00006B08 File Offset: 0x00004D08
		internal void Deactivate()
		{
			int num = Interlocked.Decrement(ref this.activeThreadCount);
			this.perfCounter.UpdateActiveThreads((long)num);
		}

		// Token: 0x06000171 RID: 369 RVA: 0x00006B30 File Offset: 0x00004D30
		internal void AddWorkload(SystemWorkloadBase workload)
		{
			if (workload == null)
			{
				throw new ArgumentNullException("workload", "[ClassificationBlock.AddWorkload] workload cannot be null.");
			}
			int num = 0;
			lock (this.instanceLock)
			{
				if (workload.ClassificationBlock != null)
				{
					throw new ArgumentException(string.Format("Workload {0} is already registered to classification block.", workload.Id));
				}
				this.workloads.Add(workload);
				num = this.workloads.Count;
				workload.ClassificationBlock = this;
			}
			this.perfCounter.UpdateWorkloadCount((long)num);
			ExTraceGlobals.SchedulerTracer.TraceDebug<string, WorkloadType>((long)this.GetHashCode(), "[ClassificationBlock.AddWorkload] Added Workload '{0}' for WorkloadType: '{1}'", workload.Id, workload.WorkloadType);
		}

		// Token: 0x06000172 RID: 370 RVA: 0x00006BEC File Offset: 0x00004DEC
		internal bool RemoveWorkload(SystemWorkloadBase workload)
		{
			bool flag = false;
			int num = 0;
			lock (this.instanceLock)
			{
				flag = this.workloads.Remove(workload);
				if (flag)
				{
					num = this.workloads.Count;
					workload.ClassificationBlock = null;
					if (this.cursorIndex >= this.workloads.Count)
					{
						this.cursorIndex = 0;
					}
				}
			}
			if (flag)
			{
				this.perfCounter.UpdateWorkloadCount((long)num);
			}
			ExTraceGlobals.SchedulerTracer.TraceDebug<string, WorkloadType, bool>((long)this.GetHashCode(), "[Classification.RemoveWorkload] Removed Workload '{0}' for WorkloadType: '{1}', Removed? {2}", workload.Id, workload.WorkloadType, flag);
			return flag;
		}

		// Token: 0x06000173 RID: 371 RVA: 0x00006CA0 File Offset: 0x00004EA0
		internal SystemWorkloadBase[] GetWorkloads()
		{
			SystemWorkloadBase[] result;
			lock (this.instanceLock)
			{
				if (this.workloads == null || this.workloads.Count < 1)
				{
					result = null;
				}
				else
				{
					SystemWorkloadBase[] array = new SystemWorkloadBase[this.workloads.Count];
					this.workloads.CopyTo(array);
					result = array;
				}
			}
			return result;
		}

		// Token: 0x06000174 RID: 372 RVA: 0x00006D14 File Offset: 0x00004F14
		internal SystemWorkloadBase GetNextWorkload()
		{
			SystemWorkloadBase systemWorkloadBase = null;
			bool flag = false;
			lock (this.instanceLock)
			{
				if (this.workloads.Count == 0)
				{
					return null;
				}
				int num = this.cursorIndex;
				for (;;)
				{
					systemWorkloadBase = this.workloads[this.cursorIndex];
					if (this.workloads.Count > 1)
					{
						this.cursorIndex = (this.cursorIndex + 1) % this.workloads.Count;
					}
					if (!systemWorkloadBase.Paused && systemWorkloadBase.TaskCount > 0)
					{
						break;
					}
					if (num == this.cursorIndex)
					{
						goto IL_91;
					}
				}
				flag = true;
				IL_91:;
			}
			if (!flag)
			{
				return null;
			}
			return systemWorkloadBase;
		}

		// Token: 0x040000C6 RID: 198
		private volatile int cursorIndex;

		// Token: 0x040000C7 RID: 199
		private ClassificationPerfCounterWrapper perfCounter;

		// Token: 0x040000C8 RID: 200
		private object instanceLock = new object();

		// Token: 0x040000C9 RID: 201
		private List<SystemWorkloadBase> workloads = new List<SystemWorkloadBase>();

		// Token: 0x040000CA RID: 202
		private int activeThreadCount;

		// Token: 0x040000CB RID: 203
		private int fairnessFactor;
	}
}
