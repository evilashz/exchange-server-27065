using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.WorkloadManagement
{
	// Token: 0x0200004A RID: 74
	internal sealed class MSExchangeWorkloadManagementWorkloadInstance : PerformanceCounterInstance
	{
		// Token: 0x060002E5 RID: 741 RVA: 0x0000D63C File Offset: 0x0000B83C
		internal MSExchangeWorkloadManagementWorkloadInstance(string instanceName, MSExchangeWorkloadManagementWorkloadInstance autoUpdateTotalInstance) : base(instanceName, "MSExchange WorkloadManagement Workloads")
		{
			bool flag = false;
			List<ExPerformanceCounter> list = new List<ExPerformanceCounter>();
			try
			{
				this.ActiveTasks = new ExPerformanceCounter(base.CategoryName, "ActiveTasks", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.ActiveTasks);
				this.QueuedTasks = new ExPerformanceCounter(base.CategoryName, "QueuedTasks", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.QueuedTasks);
				this.BlockedTasks = new ExPerformanceCounter(base.CategoryName, "BlockedTasks", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.BlockedTasks);
				this.CompletedTasks = new ExPerformanceCounter(base.CategoryName, "CompletedTasks", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.CompletedTasks);
				this.YieldedTasks = new ExPerformanceCounter(base.CategoryName, "YieldedTasks", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.YieldedTasks);
				this.TasksPerMinute = new ExPerformanceCounter(base.CategoryName, "Tasks Per Minute", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.TasksPerMinute);
				this.AverageTaskStepLength = new ExPerformanceCounter(base.CategoryName, "Average Step Execution Length", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.AverageTaskStepLength);
				this.Active = new ExPerformanceCounter(base.CategoryName, "Active", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.Active);
				long num = this.ActiveTasks.RawValue;
				num += 1L;
				flag = true;
			}
			finally
			{
				if (!flag)
				{
					foreach (ExPerformanceCounter exPerformanceCounter in list)
					{
						exPerformanceCounter.Close();
					}
				}
			}
			this.counters = list.ToArray();
		}

		// Token: 0x060002E6 RID: 742 RVA: 0x0000D840 File Offset: 0x0000BA40
		internal MSExchangeWorkloadManagementWorkloadInstance(string instanceName) : base(instanceName, "MSExchange WorkloadManagement Workloads")
		{
			bool flag = false;
			List<ExPerformanceCounter> list = new List<ExPerformanceCounter>();
			try
			{
				this.ActiveTasks = new ExPerformanceCounter(base.CategoryName, "ActiveTasks", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.ActiveTasks);
				this.QueuedTasks = new ExPerformanceCounter(base.CategoryName, "QueuedTasks", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.QueuedTasks);
				this.BlockedTasks = new ExPerformanceCounter(base.CategoryName, "BlockedTasks", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.BlockedTasks);
				this.CompletedTasks = new ExPerformanceCounter(base.CategoryName, "CompletedTasks", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.CompletedTasks);
				this.YieldedTasks = new ExPerformanceCounter(base.CategoryName, "YieldedTasks", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.YieldedTasks);
				this.TasksPerMinute = new ExPerformanceCounter(base.CategoryName, "Tasks Per Minute", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.TasksPerMinute);
				this.AverageTaskStepLength = new ExPerformanceCounter(base.CategoryName, "Average Step Execution Length", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.AverageTaskStepLength);
				this.Active = new ExPerformanceCounter(base.CategoryName, "Active", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.Active);
				long num = this.ActiveTasks.RawValue;
				num += 1L;
				flag = true;
			}
			finally
			{
				if (!flag)
				{
					foreach (ExPerformanceCounter exPerformanceCounter in list)
					{
						exPerformanceCounter.Close();
					}
				}
			}
			this.counters = list.ToArray();
		}

		// Token: 0x060002E7 RID: 743 RVA: 0x0000DA44 File Offset: 0x0000BC44
		public override void GetPerfCounterDiagnosticsInfo(XElement topElement)
		{
			XElement xelement = null;
			foreach (ExPerformanceCounter exPerformanceCounter in this.counters)
			{
				try
				{
					if (xelement == null)
					{
						xelement = new XElement(ExPerformanceCounter.GetEncodedName(exPerformanceCounter.InstanceName));
						topElement.Add(xelement);
					}
					xelement.Add(new XElement(ExPerformanceCounter.GetEncodedName(exPerformanceCounter.CounterName), exPerformanceCounter.NextValue()));
				}
				catch (XmlException ex)
				{
					XElement content = new XElement("Error", ex.Message);
					topElement.Add(content);
				}
			}
		}

		// Token: 0x0400017C RID: 380
		public readonly ExPerformanceCounter ActiveTasks;

		// Token: 0x0400017D RID: 381
		public readonly ExPerformanceCounter QueuedTasks;

		// Token: 0x0400017E RID: 382
		public readonly ExPerformanceCounter BlockedTasks;

		// Token: 0x0400017F RID: 383
		public readonly ExPerformanceCounter CompletedTasks;

		// Token: 0x04000180 RID: 384
		public readonly ExPerformanceCounter YieldedTasks;

		// Token: 0x04000181 RID: 385
		public readonly ExPerformanceCounter TasksPerMinute;

		// Token: 0x04000182 RID: 386
		public readonly ExPerformanceCounter AverageTaskStepLength;

		// Token: 0x04000183 RID: 387
		public readonly ExPerformanceCounter Active;
	}
}
