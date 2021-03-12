using System;
using Microsoft.Exchange.Servicelets.CommonCode;
using Microsoft.Office.CompliancePolicy.Dar;
using Microsoft.Office.CompliancePolicy.Monitor;

namespace Microsoft.Office.CompliancePolicy.Exchange.Dar
{
	// Token: 0x02000002 RID: 2
	internal class ExDarServiceProvider : DarServiceProvider
	{
		// Token: 0x06000001 RID: 1 RVA: 0x000020D0 File Offset: 0x000002D0
		protected override DarTaskAggregateProvider CreateDarTaskAggregateProvider()
		{
			return new ExDarTaskAggregateProvider(this);
		}

		// Token: 0x06000002 RID: 2 RVA: 0x000020D8 File Offset: 0x000002D8
		protected override DarTaskFactory CreateDarTaskFactory()
		{
			return new ExDarTaskFactory(this);
		}

		// Token: 0x06000003 RID: 3 RVA: 0x000020E0 File Offset: 0x000002E0
		protected override DarTaskQueue CreateDarTaskQueue()
		{
			return new ExDarTaskQueue(this);
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000020E8 File Offset: 0x000002E8
		protected override DarWorkloadHost CreateDarWorkloadHost()
		{
			return new ExDarWorkloadHost(this);
		}

		// Token: 0x06000005 RID: 5 RVA: 0x000020F0 File Offset: 0x000002F0
		protected override ExecutionLog CreateExecutionLog()
		{
			return new ExExecutionLog(this);
		}

		// Token: 0x06000006 RID: 6 RVA: 0x000020F8 File Offset: 0x000002F8
		protected override PerfCounterProvider CreatePerformanceCounter()
		{
			if (this.performanceCounter == null)
			{
				lock (this.performanceCounterSingletonLock)
				{
					if (this.performanceCounter == null)
					{
						this.performanceCounter = new ExPerfCounterProvider("MSUnified Compliance Sync", UnifiedPolicySyncPerfCounters.AllCounters);
					}
				}
			}
			return this.performanceCounter;
		}

		// Token: 0x04000001 RID: 1
		private ExPerfCounterProvider performanceCounter;

		// Token: 0x04000002 RID: 2
		private object performanceCounterSingletonLock = new object();
	}
}
