using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.WorkloadManagement
{
	// Token: 0x02000046 RID: 70
	internal sealed class MSExchangeWorkloadManagementClassificationInstance : PerformanceCounterInstance
	{
		// Token: 0x060002C9 RID: 713 RVA: 0x0000CAA4 File Offset: 0x0000ACA4
		internal MSExchangeWorkloadManagementClassificationInstance(string instanceName, MSExchangeWorkloadManagementClassificationInstance autoUpdateTotalInstance) : base(instanceName, "MSExchange WorkloadManagement Classification")
		{
			bool flag = false;
			List<ExPerformanceCounter> list = new List<ExPerformanceCounter>();
			try
			{
				this.WorkloadCount = new ExPerformanceCounter(base.CategoryName, "Workload Count", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.WorkloadCount);
				this.ActiveThreadCount = new ExPerformanceCounter(base.CategoryName, "Active Thread Count", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.ActiveThreadCount);
				this.FairnessFactor = new ExPerformanceCounter(base.CategoryName, "Fairness Factor", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.FairnessFactor);
				long num = this.WorkloadCount.RawValue;
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

		// Token: 0x060002CA RID: 714 RVA: 0x0000CBB8 File Offset: 0x0000ADB8
		internal MSExchangeWorkloadManagementClassificationInstance(string instanceName) : base(instanceName, "MSExchange WorkloadManagement Classification")
		{
			bool flag = false;
			List<ExPerformanceCounter> list = new List<ExPerformanceCounter>();
			try
			{
				this.WorkloadCount = new ExPerformanceCounter(base.CategoryName, "Workload Count", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.WorkloadCount);
				this.ActiveThreadCount = new ExPerformanceCounter(base.CategoryName, "Active Thread Count", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.ActiveThreadCount);
				this.FairnessFactor = new ExPerformanceCounter(base.CategoryName, "Fairness Factor", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.FairnessFactor);
				long num = this.WorkloadCount.RawValue;
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

		// Token: 0x060002CB RID: 715 RVA: 0x0000CCCC File Offset: 0x0000AECC
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

		// Token: 0x04000168 RID: 360
		public readonly ExPerformanceCounter WorkloadCount;

		// Token: 0x04000169 RID: 361
		public readonly ExPerformanceCounter ActiveThreadCount;

		// Token: 0x0400016A RID: 362
		public readonly ExPerformanceCounter FairnessFactor;
	}
}
