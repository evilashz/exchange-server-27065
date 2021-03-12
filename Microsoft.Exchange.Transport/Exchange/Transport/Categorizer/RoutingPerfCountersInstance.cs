using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport.Categorizer
{
	// Token: 0x02000558 RID: 1368
	internal sealed class RoutingPerfCountersInstance : PerformanceCounterInstance
	{
		// Token: 0x06003EF8 RID: 16120 RVA: 0x0010DD54 File Offset: 0x0010BF54
		internal RoutingPerfCountersInstance(string instanceName, RoutingPerfCountersInstance autoUpdateTotalInstance) : base(instanceName, RoutingPerfCounters.CategoryName)
		{
			bool flag = false;
			List<ExPerformanceCounter> list = new List<ExPerformanceCounter>();
			try
			{
				this.RoutingNdrsTotal = new ExPerformanceCounter(base.CategoryName, "Routing NDRs Total", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.RoutingNdrsTotal);
				this.RoutingTablesCalculatedTotal = new ExPerformanceCounter(base.CategoryName, "Routing Tables Calculated Total", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.RoutingTablesCalculatedTotal);
				this.RoutingTablesChangedTotal = new ExPerformanceCounter(base.CategoryName, "Routing Tables Changed Total", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.RoutingTablesChangedTotal);
				long num = this.RoutingNdrsTotal.RawValue;
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

		// Token: 0x06003EF9 RID: 16121 RVA: 0x0010DE68 File Offset: 0x0010C068
		internal RoutingPerfCountersInstance(string instanceName) : base(instanceName, RoutingPerfCounters.CategoryName)
		{
			bool flag = false;
			List<ExPerformanceCounter> list = new List<ExPerformanceCounter>();
			try
			{
				this.RoutingNdrsTotal = new ExPerformanceCounter(base.CategoryName, "Routing NDRs Total", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.RoutingNdrsTotal);
				this.RoutingTablesCalculatedTotal = new ExPerformanceCounter(base.CategoryName, "Routing Tables Calculated Total", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.RoutingTablesCalculatedTotal);
				this.RoutingTablesChangedTotal = new ExPerformanceCounter(base.CategoryName, "Routing Tables Changed Total", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.RoutingTablesChangedTotal);
				long num = this.RoutingNdrsTotal.RawValue;
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

		// Token: 0x06003EFA RID: 16122 RVA: 0x0010DF7C File Offset: 0x0010C17C
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

		// Token: 0x0400230E RID: 8974
		public readonly ExPerformanceCounter RoutingNdrsTotal;

		// Token: 0x0400230F RID: 8975
		public readonly ExPerformanceCounter RoutingTablesCalculatedTotal;

		// Token: 0x04002310 RID: 8976
		public readonly ExPerformanceCounter RoutingTablesChangedTotal;
	}
}
