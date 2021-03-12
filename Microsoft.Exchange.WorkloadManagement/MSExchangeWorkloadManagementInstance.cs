using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.WorkloadManagement
{
	// Token: 0x02000044 RID: 68
	internal sealed class MSExchangeWorkloadManagementInstance : PerformanceCounterInstance
	{
		// Token: 0x060002BB RID: 699 RVA: 0x0000C77C File Offset: 0x0000A97C
		internal MSExchangeWorkloadManagementInstance(string instanceName, MSExchangeWorkloadManagementInstance autoUpdateTotalInstance) : base(instanceName, "MSExchange WorkloadManagement")
		{
			bool flag = false;
			List<ExPerformanceCounter> list = new List<ExPerformanceCounter>();
			try
			{
				this.WorkloadCount = new ExPerformanceCounter(base.CategoryName, "Workload Count", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.WorkloadCount);
				this.ActiveClassifications = new ExPerformanceCounter(base.CategoryName, "Active Classifications", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.ActiveClassifications);
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

		// Token: 0x060002BC RID: 700 RVA: 0x0000C868 File Offset: 0x0000AA68
		internal MSExchangeWorkloadManagementInstance(string instanceName) : base(instanceName, "MSExchange WorkloadManagement")
		{
			bool flag = false;
			List<ExPerformanceCounter> list = new List<ExPerformanceCounter>();
			try
			{
				this.WorkloadCount = new ExPerformanceCounter(base.CategoryName, "Workload Count", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.WorkloadCount);
				this.ActiveClassifications = new ExPerformanceCounter(base.CategoryName, "Active Classifications", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.ActiveClassifications);
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

		// Token: 0x060002BD RID: 701 RVA: 0x0000C954 File Offset: 0x0000AB54
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

		// Token: 0x04000164 RID: 356
		public readonly ExPerformanceCounter WorkloadCount;

		// Token: 0x04000165 RID: 357
		public readonly ExPerformanceCounter ActiveClassifications;
	}
}
