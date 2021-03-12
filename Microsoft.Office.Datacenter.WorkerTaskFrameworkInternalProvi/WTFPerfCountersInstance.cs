using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Office.Datacenter.WorkerTaskFramework
{
	// Token: 0x0200003A RID: 58
	internal sealed class WTFPerfCountersInstance : PerformanceCounterInstance
	{
		// Token: 0x0600038F RID: 911 RVA: 0x0000C27C File Offset: 0x0000A47C
		internal WTFPerfCountersInstance(string instanceName, WTFPerfCountersInstance autoUpdateTotalInstance) : base(instanceName, "MSExchangeWorkerTaskFramework")
		{
			bool flag = false;
			List<ExPerformanceCounter> list = new List<ExPerformanceCounter>();
			try
			{
				this.WorkItemExecutionRate = new ExPerformanceCounter(base.CategoryName, "Workitem Execution Rate", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.WorkItemExecutionRate);
				this.WorkItemCount = new ExPerformanceCounter(base.CategoryName, "Active Workitem Count", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.WorkItemCount);
				this.TimedOutWorkItemCount = new ExPerformanceCounter(base.CategoryName, "Timed Out Workitem Count", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.TimedOutWorkItemCount);
				this.WorkItemRetrievalRate = new ExPerformanceCounter(base.CategoryName, "Workitem Retrieval Rate", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.WorkItemRetrievalRate);
				this.ThrottleRate = new ExPerformanceCounter(base.CategoryName, "Throttle Rate", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.ThrottleRate);
				this.QueryRate = new ExPerformanceCounter(base.CategoryName, "Query Rate", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.QueryRate);
				this.SchedulingLatency = new ExPerformanceCounter(base.CategoryName, "Scheduling Latency", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.SchedulingLatency);
				this.PoisonedWorkItemCount = new ExPerformanceCounter(base.CategoryName, "Poisoned Workitem Count", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.PoisonedWorkItemCount);
				long num = this.WorkItemExecutionRate.RawValue;
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

		// Token: 0x06000390 RID: 912 RVA: 0x0000C478 File Offset: 0x0000A678
		internal WTFPerfCountersInstance(string instanceName) : base(instanceName, "MSExchangeWorkerTaskFramework")
		{
			bool flag = false;
			List<ExPerformanceCounter> list = new List<ExPerformanceCounter>();
			try
			{
				this.WorkItemExecutionRate = new ExPerformanceCounter(base.CategoryName, "Workitem Execution Rate", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.WorkItemExecutionRate);
				this.WorkItemCount = new ExPerformanceCounter(base.CategoryName, "Active Workitem Count", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.WorkItemCount);
				this.TimedOutWorkItemCount = new ExPerformanceCounter(base.CategoryName, "Timed Out Workitem Count", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.TimedOutWorkItemCount);
				this.WorkItemRetrievalRate = new ExPerformanceCounter(base.CategoryName, "Workitem Retrieval Rate", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.WorkItemRetrievalRate);
				this.ThrottleRate = new ExPerformanceCounter(base.CategoryName, "Throttle Rate", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.ThrottleRate);
				this.QueryRate = new ExPerformanceCounter(base.CategoryName, "Query Rate", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.QueryRate);
				this.SchedulingLatency = new ExPerformanceCounter(base.CategoryName, "Scheduling Latency", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.SchedulingLatency);
				this.PoisonedWorkItemCount = new ExPerformanceCounter(base.CategoryName, "Poisoned Workitem Count", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.PoisonedWorkItemCount);
				long num = this.WorkItemExecutionRate.RawValue;
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

		// Token: 0x06000391 RID: 913 RVA: 0x0000C674 File Offset: 0x0000A874
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

		// Token: 0x0400016F RID: 367
		public readonly ExPerformanceCounter WorkItemExecutionRate;

		// Token: 0x04000170 RID: 368
		public readonly ExPerformanceCounter WorkItemCount;

		// Token: 0x04000171 RID: 369
		public readonly ExPerformanceCounter TimedOutWorkItemCount;

		// Token: 0x04000172 RID: 370
		public readonly ExPerformanceCounter WorkItemRetrievalRate;

		// Token: 0x04000173 RID: 371
		public readonly ExPerformanceCounter ThrottleRate;

		// Token: 0x04000174 RID: 372
		public readonly ExPerformanceCounter QueryRate;

		// Token: 0x04000175 RID: 373
		public readonly ExPerformanceCounter SchedulingLatency;

		// Token: 0x04000176 RID: 374
		public readonly ExPerformanceCounter PoisonedWorkItemCount;
	}
}
