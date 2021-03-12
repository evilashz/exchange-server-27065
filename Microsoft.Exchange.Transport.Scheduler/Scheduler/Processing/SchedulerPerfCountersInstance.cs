using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport.Scheduler.Processing
{
	// Token: 0x02000033 RID: 51
	internal sealed class SchedulerPerfCountersInstance : PerformanceCounterInstance
	{
		// Token: 0x06000139 RID: 313 RVA: 0x00005798 File Offset: 0x00003998
		internal SchedulerPerfCountersInstance(string instanceName, SchedulerPerfCountersInstance autoUpdateTotalInstance) : base(instanceName, "MSExchangeTransport Processing Scheduler")
		{
			bool flag = false;
			List<ExPerformanceCounter> list = new List<ExPerformanceCounter>();
			try
			{
				this.TotalReceived = new ExPerformanceCounter(base.CategoryName, "Total Received Messages", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.TotalReceived);
				this.TotalScheduled = new ExPerformanceCounter(base.CategoryName, "Total Scheduled Messages", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.TotalScheduled);
				this.ProcessingVelocity = new ExPerformanceCounter(base.CategoryName, "Processing Velocity", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.ProcessingVelocity);
				this.TotalScopedQueues = new ExPerformanceCounter(base.CategoryName, "Total Scoped Queues", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.TotalScopedQueues);
				this.ScopedQueuesCreationRate = new ExPerformanceCounter(base.CategoryName, "Scoped Queues created/min", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.ScopedQueuesCreationRate);
				this.ScopedQueuesRemovalRate = new ExPerformanceCounter(base.CategoryName, "Scoped Queues removed/min", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.ScopedQueuesRemovalRate);
				this.ThrottlingRate = new ExPerformanceCounter(base.CategoryName, "Items Throttled/min", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.ThrottlingRate);
				this.OldestScopedQueueAge = new ExPerformanceCounter(base.CategoryName, "Oldest Scoped Queue Age", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.OldestScopedQueueAge);
				this.OldestLockAge = new ExPerformanceCounter(base.CategoryName, "Oldest Lock Age", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.OldestLockAge);
				long num = this.TotalReceived.RawValue;
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

		// Token: 0x0600013A RID: 314 RVA: 0x000059C8 File Offset: 0x00003BC8
		internal SchedulerPerfCountersInstance(string instanceName) : base(instanceName, "MSExchangeTransport Processing Scheduler")
		{
			bool flag = false;
			List<ExPerformanceCounter> list = new List<ExPerformanceCounter>();
			try
			{
				this.TotalReceived = new ExPerformanceCounter(base.CategoryName, "Total Received Messages", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.TotalReceived);
				this.TotalScheduled = new ExPerformanceCounter(base.CategoryName, "Total Scheduled Messages", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.TotalScheduled);
				this.ProcessingVelocity = new ExPerformanceCounter(base.CategoryName, "Processing Velocity", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.ProcessingVelocity);
				this.TotalScopedQueues = new ExPerformanceCounter(base.CategoryName, "Total Scoped Queues", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.TotalScopedQueues);
				this.ScopedQueuesCreationRate = new ExPerformanceCounter(base.CategoryName, "Scoped Queues created/min", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.ScopedQueuesCreationRate);
				this.ScopedQueuesRemovalRate = new ExPerformanceCounter(base.CategoryName, "Scoped Queues removed/min", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.ScopedQueuesRemovalRate);
				this.ThrottlingRate = new ExPerformanceCounter(base.CategoryName, "Items Throttled/min", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.ThrottlingRate);
				this.OldestScopedQueueAge = new ExPerformanceCounter(base.CategoryName, "Oldest Scoped Queue Age", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.OldestScopedQueueAge);
				this.OldestLockAge = new ExPerformanceCounter(base.CategoryName, "Oldest Lock Age", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.OldestLockAge);
				long num = this.TotalReceived.RawValue;
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

		// Token: 0x0600013B RID: 315 RVA: 0x00005BF8 File Offset: 0x00003DF8
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

		// Token: 0x040000AE RID: 174
		public readonly ExPerformanceCounter TotalReceived;

		// Token: 0x040000AF RID: 175
		public readonly ExPerformanceCounter TotalScheduled;

		// Token: 0x040000B0 RID: 176
		public readonly ExPerformanceCounter ProcessingVelocity;

		// Token: 0x040000B1 RID: 177
		public readonly ExPerformanceCounter TotalScopedQueues;

		// Token: 0x040000B2 RID: 178
		public readonly ExPerformanceCounter ScopedQueuesCreationRate;

		// Token: 0x040000B3 RID: 179
		public readonly ExPerformanceCounter ScopedQueuesRemovalRate;

		// Token: 0x040000B4 RID: 180
		public readonly ExPerformanceCounter ThrottlingRate;

		// Token: 0x040000B5 RID: 181
		public readonly ExPerformanceCounter OldestScopedQueueAge;

		// Token: 0x040000B6 RID: 182
		public readonly ExPerformanceCounter OldestLockAge;
	}
}
