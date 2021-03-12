using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Protocols.Smtp
{
	// Token: 0x02000574 RID: 1396
	internal sealed class SmtpConnectionCachePerfCountersInstance : PerformanceCounterInstance
	{
		// Token: 0x06003FC0 RID: 16320 RVA: 0x00115904 File Offset: 0x00113B04
		internal SmtpConnectionCachePerfCountersInstance(string instanceName, SmtpConnectionCachePerfCountersInstance autoUpdateTotalInstance) : base(instanceName, SmtpConnectionCachePerfCounters.CategoryName)
		{
			bool flag = false;
			List<ExPerformanceCounter> list = new List<ExPerformanceCounter>();
			try
			{
				ExPerformanceCounter exPerformanceCounter = new ExPerformanceCounter(base.CategoryName, "Smtp Connection Cache requests per sec", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter);
				this.Requests = new ExPerformanceCounter(base.CategoryName, "Smtp Connection Cache Total requests", instanceName, true, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter
				});
				list.Add(this.Requests);
				this.HitRatio = new ExPerformanceCounter(base.CategoryName, "Smtp Connection Cache hit ratio", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.HitRatio);
				this.HitRatio_Base = new ExPerformanceCounter(base.CategoryName, "Smtp Connection Cache hit ratio base counter", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.HitRatio_Base);
				this.CacheSize = new ExPerformanceCounter(base.CategoryName, "Smtp Connection Cache size", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.CacheSize);
				long num = this.Requests.RawValue;
				num += 1L;
				flag = true;
			}
			finally
			{
				if (!flag)
				{
					foreach (ExPerformanceCounter exPerformanceCounter2 in list)
					{
						exPerformanceCounter2.Close();
					}
				}
			}
			this.counters = list.ToArray();
		}

		// Token: 0x06003FC1 RID: 16321 RVA: 0x00115A74 File Offset: 0x00113C74
		internal SmtpConnectionCachePerfCountersInstance(string instanceName) : base(instanceName, SmtpConnectionCachePerfCounters.CategoryName)
		{
			bool flag = false;
			List<ExPerformanceCounter> list = new List<ExPerformanceCounter>();
			try
			{
				ExPerformanceCounter exPerformanceCounter = new ExPerformanceCounter(base.CategoryName, "Smtp Connection Cache requests per sec", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter);
				this.Requests = new ExPerformanceCounter(base.CategoryName, "Smtp Connection Cache Total requests", instanceName, true, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter
				});
				list.Add(this.Requests);
				this.HitRatio = new ExPerformanceCounter(base.CategoryName, "Smtp Connection Cache hit ratio", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.HitRatio);
				this.HitRatio_Base = new ExPerformanceCounter(base.CategoryName, "Smtp Connection Cache hit ratio base counter", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.HitRatio_Base);
				this.CacheSize = new ExPerformanceCounter(base.CategoryName, "Smtp Connection Cache size", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.CacheSize);
				long num = this.Requests.RawValue;
				num += 1L;
				flag = true;
			}
			finally
			{
				if (!flag)
				{
					foreach (ExPerformanceCounter exPerformanceCounter2 in list)
					{
						exPerformanceCounter2.Close();
					}
				}
			}
			this.counters = list.ToArray();
		}

		// Token: 0x06003FC2 RID: 16322 RVA: 0x00115BE4 File Offset: 0x00113DE4
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

		// Token: 0x040023DD RID: 9181
		public readonly ExPerformanceCounter Requests;

		// Token: 0x040023DE RID: 9182
		public readonly ExPerformanceCounter HitRatio;

		// Token: 0x040023DF RID: 9183
		public readonly ExPerformanceCounter HitRatio_Base;

		// Token: 0x040023E0 RID: 9184
		public readonly ExPerformanceCounter CacheSize;
	}
}
