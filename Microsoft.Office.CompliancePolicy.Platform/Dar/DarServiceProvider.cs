using System;
using Microsoft.Office.CompliancePolicy.Monitor;

namespace Microsoft.Office.CompliancePolicy.Dar
{
	// Token: 0x02000068 RID: 104
	public abstract class DarServiceProvider
	{
		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x060002D6 RID: 726 RVA: 0x0000A540 File Offset: 0x00008740
		public DarTaskQueue DarTaskQueue
		{
			get
			{
				DarTaskQueue result;
				if ((result = this.darTaskQueue) == null)
				{
					result = (this.darTaskQueue = this.CreateDarTaskQueue());
				}
				return result;
			}
		}

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x060002D7 RID: 727 RVA: 0x0000A568 File Offset: 0x00008768
		public DarWorkloadHost DarWorkloadHost
		{
			get
			{
				DarWorkloadHost result;
				if ((result = this.darWorkloadHost) == null)
				{
					result = (this.darWorkloadHost = this.CreateDarWorkloadHost());
				}
				return result;
			}
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x060002D8 RID: 728 RVA: 0x0000A590 File Offset: 0x00008790
		public DarTaskAggregateProvider DarTaskAggregateProvider
		{
			get
			{
				DarTaskAggregateProvider result;
				if ((result = this.darTaskAggregateProvider) == null)
				{
					result = (this.darTaskAggregateProvider = this.CreateDarTaskAggregateProvider());
				}
				return result;
			}
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x060002D9 RID: 729 RVA: 0x0000A5B8 File Offset: 0x000087B8
		public DarTaskFactory DarTaskFactory
		{
			get
			{
				DarTaskFactory result;
				if ((result = this.darTaskFactory) == null)
				{
					result = (this.darTaskFactory = this.CreateDarTaskFactory());
				}
				return result;
			}
		}

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x060002DA RID: 730 RVA: 0x0000A5E0 File Offset: 0x000087E0
		public ExecutionLog ExecutionLog
		{
			get
			{
				ExecutionLog result;
				if ((result = this.executionLog) == null)
				{
					result = (this.executionLog = this.CreateExecutionLog());
				}
				return result;
			}
		}

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x060002DB RID: 731 RVA: 0x0000A608 File Offset: 0x00008808
		public PerfCounterProvider PerformanceCounter
		{
			get
			{
				PerfCounterProvider result;
				if ((result = this.performanceCounter) == null)
				{
					result = (this.performanceCounter = this.CreatePerformanceCounter());
				}
				return result;
			}
		}

		// Token: 0x060002DC RID: 732
		protected abstract DarTaskQueue CreateDarTaskQueue();

		// Token: 0x060002DD RID: 733
		protected abstract DarWorkloadHost CreateDarWorkloadHost();

		// Token: 0x060002DE RID: 734
		protected abstract DarTaskAggregateProvider CreateDarTaskAggregateProvider();

		// Token: 0x060002DF RID: 735
		protected abstract DarTaskFactory CreateDarTaskFactory();

		// Token: 0x060002E0 RID: 736
		protected abstract ExecutionLog CreateExecutionLog();

		// Token: 0x060002E1 RID: 737
		protected abstract PerfCounterProvider CreatePerformanceCounter();

		// Token: 0x0400015F RID: 351
		private DarTaskQueue darTaskQueue;

		// Token: 0x04000160 RID: 352
		private DarWorkloadHost darWorkloadHost;

		// Token: 0x04000161 RID: 353
		private DarTaskAggregateProvider darTaskAggregateProvider;

		// Token: 0x04000162 RID: 354
		private DarTaskFactory darTaskFactory;

		// Token: 0x04000163 RID: 355
		private ExecutionLog executionLog;

		// Token: 0x04000164 RID: 356
		private PerfCounterProvider performanceCounter;
	}
}
