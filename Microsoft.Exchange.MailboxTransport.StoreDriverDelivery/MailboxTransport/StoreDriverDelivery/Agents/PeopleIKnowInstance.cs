using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MailboxTransport.StoreDriverDelivery.Agents
{
	// Token: 0x020000AE RID: 174
	internal sealed class PeopleIKnowInstance : PerformanceCounterInstance
	{
		// Token: 0x060005B2 RID: 1458 RVA: 0x0001F048 File Offset: 0x0001D248
		internal PeopleIKnowInstance(string instanceName, PeopleIKnowInstance autoUpdateTotalInstance) : base(instanceName, "People-I-Know Delivery Agent")
		{
			bool flag = false;
			List<ExPerformanceCounter> list = new List<ExPerformanceCounter>();
			try
			{
				this.AverageStopWatchTime = new ExPerformanceCounter(base.CategoryName, "Average Time to Process Message", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.AverageStopWatchTime, new ExPerformanceCounter[0]);
				list.Add(this.AverageStopWatchTime);
				this.AverageStopWatchTimeBase = new ExPerformanceCounter(base.CategoryName, "Base for Average Time to Process Message", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.AverageStopWatchTimeBase, new ExPerformanceCounter[0]);
				list.Add(this.AverageStopWatchTimeBase);
				this.AverageCpuTime = new ExPerformanceCounter(base.CategoryName, "Average CPU Time to Process Message", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.AverageCpuTime, new ExPerformanceCounter[0]);
				list.Add(this.AverageCpuTime);
				this.AverageCpuTimeBase = new ExPerformanceCounter(base.CategoryName, "Base for Average CPU Time to Process Message", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.AverageCpuTimeBase, new ExPerformanceCounter[0]);
				list.Add(this.AverageCpuTimeBase);
				this.AverageStoreRPCs = new ExPerformanceCounter(base.CategoryName, "Average Store RPCs", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.AverageStoreRPCs, new ExPerformanceCounter[0]);
				list.Add(this.AverageStoreRPCs);
				this.AverageStoreRPCsBase = new ExPerformanceCounter(base.CategoryName, "Base for Average Store RPCs", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.AverageStoreRPCsBase, new ExPerformanceCounter[0]);
				list.Add(this.AverageStoreRPCsBase);
				long num = this.AverageStopWatchTime.RawValue;
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

		// Token: 0x060005B3 RID: 1459 RVA: 0x0001F238 File Offset: 0x0001D438
		internal PeopleIKnowInstance(string instanceName) : base(instanceName, "People-I-Know Delivery Agent")
		{
			bool flag = false;
			List<ExPerformanceCounter> list = new List<ExPerformanceCounter>();
			try
			{
				this.AverageStopWatchTime = new ExPerformanceCounter(base.CategoryName, "Average Time to Process Message", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.AverageStopWatchTime);
				this.AverageStopWatchTimeBase = new ExPerformanceCounter(base.CategoryName, "Base for Average Time to Process Message", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.AverageStopWatchTimeBase);
				this.AverageCpuTime = new ExPerformanceCounter(base.CategoryName, "Average CPU Time to Process Message", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.AverageCpuTime);
				this.AverageCpuTimeBase = new ExPerformanceCounter(base.CategoryName, "Base for Average CPU Time to Process Message", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.AverageCpuTimeBase);
				this.AverageStoreRPCs = new ExPerformanceCounter(base.CategoryName, "Average Store RPCs", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.AverageStoreRPCs);
				this.AverageStoreRPCsBase = new ExPerformanceCounter(base.CategoryName, "Base for Average Store RPCs", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.AverageStoreRPCsBase);
				long num = this.AverageStopWatchTime.RawValue;
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

		// Token: 0x060005B4 RID: 1460 RVA: 0x0001F3E8 File Offset: 0x0001D5E8
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

		// Token: 0x04000340 RID: 832
		public readonly ExPerformanceCounter AverageStopWatchTime;

		// Token: 0x04000341 RID: 833
		public readonly ExPerformanceCounter AverageStopWatchTimeBase;

		// Token: 0x04000342 RID: 834
		public readonly ExPerformanceCounter AverageCpuTime;

		// Token: 0x04000343 RID: 835
		public readonly ExPerformanceCounter AverageCpuTimeBase;

		// Token: 0x04000344 RID: 836
		public readonly ExPerformanceCounter AverageStoreRPCs;

		// Token: 0x04000345 RID: 837
		public readonly ExPerformanceCounter AverageStoreRPCsBase;
	}
}
