using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Net.Mserve.Perf
{
	// Token: 0x0200017D RID: 381
	internal sealed class MserveWebServiceCountersInstance : PerformanceCounterInstance
	{
		// Token: 0x06000931 RID: 2353 RVA: 0x000199E0 File Offset: 0x00017BE0
		internal MserveWebServiceCountersInstance(string instanceName, MserveWebServiceCountersInstance autoUpdateTotalInstance) : base(instanceName, "MSExchange MserveWebService")
		{
			bool flag = false;
			List<ExPerformanceCounter> list = new List<ExPerformanceCounter>();
			try
			{
				this.ReadRequestsInMserveCacheService = new ExPerformanceCounter(base.CategoryName, "Read requests by Mserve Cache", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.ReadRequestsInMserveCacheService);
				this.TotalRequestsInMserveCacheService = new ExPerformanceCounter(base.CategoryName, "Total requests by Mserve Cache Service", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.TotalRequestsInMserveCacheService);
				this.TotalFailuresInMserveCacheService = new ExPerformanceCounter(base.CategoryName, "Total failures by Mserve Cache Service", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.TotalFailuresInMserveCacheService);
				this.PercentageFailuresInMserveCacheService = new ExPerformanceCounter(base.CategoryName, "Percentage of failures by Mserve Cache Service", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.PercentageFailuresInMserveCacheService);
				this.PercentageRequestsInMserveCacheService = new ExPerformanceCounter(base.CategoryName, "Percentage of requests by Mserve Cache Service", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.PercentageRequestsInMserveCacheService);
				this.ReadRequestsInMserveWebService = new ExPerformanceCounter(base.CategoryName, "Read requests by Real Mserve Web Service", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.ReadRequestsInMserveWebService);
				this.AddRequestsInMserveWebService = new ExPerformanceCounter(base.CategoryName, "Add requests by Real Mserve Web Service", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.AddRequestsInMserveWebService);
				this.DeleteRequestsInMserveWebService = new ExPerformanceCounter(base.CategoryName, "Delete requests by Real Mserve Web Service", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.DeleteRequestsInMserveWebService);
				this.TotalRequestsInMserveWebService = new ExPerformanceCounter(base.CategoryName, "Total requests by Real Mserve Web Service", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.TotalRequestsInMserveWebService);
				this.TotalFailuresInMserveWebService = new ExPerformanceCounter(base.CategoryName, "Total failures by Real Mserve Web Service", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.TotalFailuresInMserveWebService);
				this.PercentageFailuresInMserveWebService = new ExPerformanceCounter(base.CategoryName, "Percentage of failures by Real Mserve Web Service", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.PercentageFailuresInMserveWebService);
				this.PercentageRequestsInMserveWebService = new ExPerformanceCounter(base.CategoryName, "Percentage of requests by Real Mserve Web Service", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.PercentageRequestsInMserveWebService);
				this.TotalRequestsInMserveService = new ExPerformanceCounter(base.CategoryName, "Total requests by Mserve", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.TotalRequestsInMserveService);
				this.TotalFailuresInMserveService = new ExPerformanceCounter(base.CategoryName, "Total failures by Mserve", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.TotalFailuresInMserveService);
				this.PercentageTotalFailuresInMserveService = new ExPerformanceCounter(base.CategoryName, "Percentage of failures in Mserve", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.PercentageTotalFailuresInMserveService);
				long num = this.ReadRequestsInMserveCacheService.RawValue;
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

		// Token: 0x06000932 RID: 2354 RVA: 0x00019D04 File Offset: 0x00017F04
		internal MserveWebServiceCountersInstance(string instanceName) : base(instanceName, "MSExchange MserveWebService")
		{
			bool flag = false;
			List<ExPerformanceCounter> list = new List<ExPerformanceCounter>();
			try
			{
				this.ReadRequestsInMserveCacheService = new ExPerformanceCounter(base.CategoryName, "Read requests by Mserve Cache", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.ReadRequestsInMserveCacheService);
				this.TotalRequestsInMserveCacheService = new ExPerformanceCounter(base.CategoryName, "Total requests by Mserve Cache Service", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.TotalRequestsInMserveCacheService);
				this.TotalFailuresInMserveCacheService = new ExPerformanceCounter(base.CategoryName, "Total failures by Mserve Cache Service", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.TotalFailuresInMserveCacheService);
				this.PercentageFailuresInMserveCacheService = new ExPerformanceCounter(base.CategoryName, "Percentage of failures by Mserve Cache Service", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.PercentageFailuresInMserveCacheService);
				this.PercentageRequestsInMserveCacheService = new ExPerformanceCounter(base.CategoryName, "Percentage of requests by Mserve Cache Service", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.PercentageRequestsInMserveCacheService);
				this.ReadRequestsInMserveWebService = new ExPerformanceCounter(base.CategoryName, "Read requests by Real Mserve Web Service", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.ReadRequestsInMserveWebService);
				this.AddRequestsInMserveWebService = new ExPerformanceCounter(base.CategoryName, "Add requests by Real Mserve Web Service", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.AddRequestsInMserveWebService);
				this.DeleteRequestsInMserveWebService = new ExPerformanceCounter(base.CategoryName, "Delete requests by Real Mserve Web Service", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.DeleteRequestsInMserveWebService);
				this.TotalRequestsInMserveWebService = new ExPerformanceCounter(base.CategoryName, "Total requests by Real Mserve Web Service", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.TotalRequestsInMserveWebService);
				this.TotalFailuresInMserveWebService = new ExPerformanceCounter(base.CategoryName, "Total failures by Real Mserve Web Service", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.TotalFailuresInMserveWebService);
				this.PercentageFailuresInMserveWebService = new ExPerformanceCounter(base.CategoryName, "Percentage of failures by Real Mserve Web Service", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.PercentageFailuresInMserveWebService);
				this.PercentageRequestsInMserveWebService = new ExPerformanceCounter(base.CategoryName, "Percentage of requests by Real Mserve Web Service", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.PercentageRequestsInMserveWebService);
				this.TotalRequestsInMserveService = new ExPerformanceCounter(base.CategoryName, "Total requests by Mserve", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.TotalRequestsInMserveService);
				this.TotalFailuresInMserveService = new ExPerformanceCounter(base.CategoryName, "Total failures by Mserve", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.TotalFailuresInMserveService);
				this.PercentageTotalFailuresInMserveService = new ExPerformanceCounter(base.CategoryName, "Percentage of failures in Mserve", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.PercentageTotalFailuresInMserveService);
				long num = this.ReadRequestsInMserveCacheService.RawValue;
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

		// Token: 0x06000933 RID: 2355 RVA: 0x0001A028 File Offset: 0x00018228
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

		// Token: 0x0400064B RID: 1611
		public readonly ExPerformanceCounter ReadRequestsInMserveCacheService;

		// Token: 0x0400064C RID: 1612
		public readonly ExPerformanceCounter TotalRequestsInMserveCacheService;

		// Token: 0x0400064D RID: 1613
		public readonly ExPerformanceCounter TotalFailuresInMserveCacheService;

		// Token: 0x0400064E RID: 1614
		public readonly ExPerformanceCounter PercentageFailuresInMserveCacheService;

		// Token: 0x0400064F RID: 1615
		public readonly ExPerformanceCounter PercentageRequestsInMserveCacheService;

		// Token: 0x04000650 RID: 1616
		public readonly ExPerformanceCounter ReadRequestsInMserveWebService;

		// Token: 0x04000651 RID: 1617
		public readonly ExPerformanceCounter AddRequestsInMserveWebService;

		// Token: 0x04000652 RID: 1618
		public readonly ExPerformanceCounter DeleteRequestsInMserveWebService;

		// Token: 0x04000653 RID: 1619
		public readonly ExPerformanceCounter TotalRequestsInMserveWebService;

		// Token: 0x04000654 RID: 1620
		public readonly ExPerformanceCounter TotalFailuresInMserveWebService;

		// Token: 0x04000655 RID: 1621
		public readonly ExPerformanceCounter PercentageFailuresInMserveWebService;

		// Token: 0x04000656 RID: 1622
		public readonly ExPerformanceCounter PercentageRequestsInMserveWebService;

		// Token: 0x04000657 RID: 1623
		public readonly ExPerformanceCounter TotalRequestsInMserveService;

		// Token: 0x04000658 RID: 1624
		public readonly ExPerformanceCounter TotalFailuresInMserveService;

		// Token: 0x04000659 RID: 1625
		public readonly ExPerformanceCounter PercentageTotalFailuresInMserveService;
	}
}
