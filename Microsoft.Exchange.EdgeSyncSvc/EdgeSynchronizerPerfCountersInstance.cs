using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.EdgeSync
{
	// Token: 0x02000017 RID: 23
	internal sealed class EdgeSynchronizerPerfCountersInstance : PerformanceCounterInstance
	{
		// Token: 0x060000D0 RID: 208 RVA: 0x00008E28 File Offset: 0x00007028
		internal EdgeSynchronizerPerfCountersInstance(string instanceName, EdgeSynchronizerPerfCountersInstance autoUpdateTotalInstance) : base(instanceName, "MSExchangeEdgeSync Synchronizer")
		{
			bool flag = false;
			List<ExPerformanceCounter> list = new List<ExPerformanceCounter>();
			try
			{
				this.Added = new ExPerformanceCounter(base.CategoryName, "Total Objects Added", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.Added, new ExPerformanceCounter[0]);
				list.Add(this.Added);
				this.Updated = new ExPerformanceCounter(base.CategoryName, "Total objects updated", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.Updated, new ExPerformanceCounter[0]);
				list.Add(this.Updated);
				this.Deleted = new ExPerformanceCounter(base.CategoryName, "Total objects deleted", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.Deleted, new ExPerformanceCounter[0]);
				list.Add(this.Deleted);
				this.AddedPerCycle = new ExPerformanceCounter(base.CategoryName, "Objects Added per Synchronization Cycle", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.AddedPerCycle, new ExPerformanceCounter[0]);
				list.Add(this.AddedPerCycle);
				this.UpdatedPerCycle = new ExPerformanceCounter(base.CategoryName, "Objects updated per synchronization cycle", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.UpdatedPerCycle, new ExPerformanceCounter[0]);
				list.Add(this.UpdatedPerCycle);
				this.DeletedPerCycle = new ExPerformanceCounter(base.CategoryName, "Objects deleted per synchronization cycle", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.DeletedPerCycle, new ExPerformanceCounter[0]);
				list.Add(this.DeletedPerCycle);
				this.FullSyncs = new ExPerformanceCounter(base.CategoryName, "Number of full synchronizations", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.FullSyncs, new ExPerformanceCounter[0]);
				list.Add(this.FullSyncs);
				long num = this.Added.RawValue;
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

		// Token: 0x060000D1 RID: 209 RVA: 0x00009048 File Offset: 0x00007248
		internal EdgeSynchronizerPerfCountersInstance(string instanceName) : base(instanceName, "MSExchangeEdgeSync Synchronizer")
		{
			bool flag = false;
			List<ExPerformanceCounter> list = new List<ExPerformanceCounter>();
			try
			{
				this.Added = new ExPerformanceCounter(base.CategoryName, "Total Objects Added", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.Added);
				this.Updated = new ExPerformanceCounter(base.CategoryName, "Total objects updated", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.Updated);
				this.Deleted = new ExPerformanceCounter(base.CategoryName, "Total objects deleted", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.Deleted);
				this.AddedPerCycle = new ExPerformanceCounter(base.CategoryName, "Objects Added per Synchronization Cycle", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.AddedPerCycle);
				this.UpdatedPerCycle = new ExPerformanceCounter(base.CategoryName, "Objects updated per synchronization cycle", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.UpdatedPerCycle);
				this.DeletedPerCycle = new ExPerformanceCounter(base.CategoryName, "Objects deleted per synchronization cycle", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.DeletedPerCycle);
				this.FullSyncs = new ExPerformanceCounter(base.CategoryName, "Number of full synchronizations", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.FullSyncs);
				long num = this.Added.RawValue;
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

		// Token: 0x060000D2 RID: 210 RVA: 0x0000921C File Offset: 0x0000741C
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

		// Token: 0x0400007A RID: 122
		public readonly ExPerformanceCounter Added;

		// Token: 0x0400007B RID: 123
		public readonly ExPerformanceCounter Updated;

		// Token: 0x0400007C RID: 124
		public readonly ExPerformanceCounter Deleted;

		// Token: 0x0400007D RID: 125
		public readonly ExPerformanceCounter AddedPerCycle;

		// Token: 0x0400007E RID: 126
		public readonly ExPerformanceCounter UpdatedPerCycle;

		// Token: 0x0400007F RID: 127
		public readonly ExPerformanceCounter DeletedPerCycle;

		// Token: 0x04000080 RID: 128
		public readonly ExPerformanceCounter FullSyncs;
	}
}
