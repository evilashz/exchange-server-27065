using System;
using System.Threading;
using Microsoft.Exchange.Diagnostics.Components.WorkloadManagement;
using Microsoft.Exchange.Diagnostics.WorkloadManagement;

namespace Microsoft.Exchange.WorkloadManagement
{
	// Token: 0x02000036 RID: 54
	internal abstract class SystemTaskBase
	{
		// Token: 0x060001E0 RID: 480 RVA: 0x000085AD File Offset: 0x000067AD
		protected SystemTaskBase(SystemWorkloadBase workload) : this(workload, null)
		{
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x000085B8 File Offset: 0x000067B8
		protected SystemTaskBase(SystemWorkloadBase workload, ResourceReservation reservation)
		{
			if (workload == null)
			{
				throw new ArgumentNullException("workload");
			}
			this.Identity = Guid.NewGuid();
			this.Workload = workload;
			this.ResourceReservation = reservation;
			this.CreationTime = DateTime.UtcNow;
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x060001E2 RID: 482 RVA: 0x00008608 File Offset: 0x00006808
		// (set) Token: 0x060001E3 RID: 483 RVA: 0x00008610 File Offset: 0x00006810
		public Guid Identity { get; private set; }

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x060001E4 RID: 484 RVA: 0x00008619 File Offset: 0x00006819
		// (set) Token: 0x060001E5 RID: 485 RVA: 0x00008621 File Offset: 0x00006821
		public SystemWorkloadBase Workload { get; private set; }

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x060001E6 RID: 486 RVA: 0x0000862A File Offset: 0x0000682A
		// (set) Token: 0x060001E7 RID: 487 RVA: 0x00008632 File Offset: 0x00006832
		public DateTime CreationTime { get; private set; }

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x060001E8 RID: 488 RVA: 0x0000863B File Offset: 0x0000683B
		public TimeSpan ExecutionTime
		{
			get
			{
				return this.elapsedExecutionTime;
			}
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x060001E9 RID: 489 RVA: 0x00008643 File Offset: 0x00006843
		public TimeSpan SuspendTime
		{
			get
			{
				return DateTime.UtcNow - this.CreationTime - this.ExecutionTime;
			}
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x060001EA RID: 490 RVA: 0x00008660 File Offset: 0x00006860
		// (set) Token: 0x060001EB RID: 491 RVA: 0x00008668 File Offset: 0x00006868
		public ResourceReservation ResourceReservation { get; set; }

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x060001EC RID: 492 RVA: 0x00008671 File Offset: 0x00006871
		internal Thread Thread
		{
			get
			{
				return this.thread;
			}
		}

		// Token: 0x060001ED RID: 493 RVA: 0x00008679 File Offset: 0x00006879
		public virtual void Complete()
		{
		}

		// Token: 0x060001EE RID: 494 RVA: 0x0000867C File Offset: 0x0000687C
		public override string ToString()
		{
			return this.Identity.ToString();
		}

		// Token: 0x060001EF RID: 495 RVA: 0x0000869D File Offset: 0x0000689D
		internal void InternalComplete()
		{
			this.CompleteStep();
			this.Complete();
			this.Workload.InternalCompleteTask(this);
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x000086B7 File Offset: 0x000068B7
		internal void InternalYield(ActivityContextState state)
		{
			this.activityContextState = state;
			this.CompleteStep();
			this.Workload.InternalYieldTask(this);
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x000086D4 File Offset: 0x000068D4
		internal ActivityContextState InternalResume()
		{
			ActivityContextState result = this.activityContextState;
			this.activityContextState = null;
			return result;
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x000086F8 File Offset: 0x000068F8
		internal TaskStepResult InternalExecute()
		{
			this.thread = Thread.CurrentThread;
			return this.TimedExecution<TaskStepResult>(() => this.Execute());
		}

		// Token: 0x060001F3 RID: 499
		protected abstract TaskStepResult Execute();

		// Token: 0x060001F4 RID: 500 RVA: 0x00008717 File Offset: 0x00006917
		private void CompleteStep()
		{
			this.thread = null;
			this.ResourceReservation.Dispose();
			this.ResourceReservation = null;
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x00008734 File Offset: 0x00006934
		private T TimedExecution<T>(Func<T> method)
		{
			DateTime utcNow = DateTime.UtcNow;
			T result;
			try
			{
				result = method();
			}
			finally
			{
				TimeSpan timeSpan = DateTime.UtcNow - utcNow;
				if (timeSpan > TimeSpan.Zero)
				{
					this.elapsedExecutionTime += timeSpan;
				}
				TimeSpan timeSpan2 = TimeSpan.FromMilliseconds(timeSpan.TotalMilliseconds * this.ResourceReservation.DelayFactor);
				if (timeSpan2 > TimeSpan.Zero)
				{
					if (timeSpan2 > SystemTaskBase.MaxDelay)
					{
						ExTraceGlobals.SchedulerTracer.TraceDebug<TimeSpan, TimeSpan>((long)this.GetHashCode(), "[SystemTaskBase.TimedExecution] Calculated delay {0} exceeded maximum {1}. Using maximum instead.", timeSpan2, SystemTaskBase.MaxDelay);
						timeSpan2 = SystemTaskBase.MaxDelay;
					}
					WorkloadManagementLogger.SetThrottlingValues(timeSpan2, false, null, null);
					Thread.Sleep(timeSpan2);
				}
			}
			return result;
		}

		// Token: 0x040000FA RID: 250
		private static readonly TimeSpan MaxDelay = TimeSpan.FromSeconds(5.0);

		// Token: 0x040000FB RID: 251
		private TimeSpan elapsedExecutionTime = TimeSpan.Zero;

		// Token: 0x040000FC RID: 252
		private ActivityContextState activityContextState;

		// Token: 0x040000FD RID: 253
		private Thread thread;
	}
}
