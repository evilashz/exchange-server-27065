using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport.Configuration
{
	// Token: 0x02000566 RID: 1382
	internal sealed class ConfigurationCachePerfCountersInstance : PerformanceCounterInstance
	{
		// Token: 0x06003F5C RID: 16220 RVA: 0x00112FF4 File Offset: 0x001111F4
		internal ConfigurationCachePerfCountersInstance(string instanceName, ConfigurationCachePerfCountersInstance autoUpdateTotalInstance) : base(instanceName, ConfigurationCachePerfCounters.CategoryName)
		{
			bool flag = false;
			List<ExPerformanceCounter> list = new List<ExPerformanceCounter>();
			try
			{
				ExPerformanceCounter exPerformanceCounter = new ExPerformanceCounter(base.CategoryName, "Configuration Cache requests per sec", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter);
				this.Requests = new ExPerformanceCounter(base.CategoryName, "Configuration Cache Total requests", instanceName, true, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter
				});
				list.Add(this.Requests);
				this.HitRatio = new ExPerformanceCounter(base.CategoryName, "Configuration Cache hit ratio", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.HitRatio);
				this.HitRatio_Base = new ExPerformanceCounter(base.CategoryName, "Configuration Cache hit ratio base counter", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.HitRatio_Base);
				this.CacheSize = new ExPerformanceCounter(base.CategoryName, "Configuration Cache size", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.CacheSize);
				this.CacheSizeMB = new ExPerformanceCounter(base.CategoryName, "Configuration Cache size in MB", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.CacheSizeMB);
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

		// Token: 0x06003F5D RID: 16221 RVA: 0x001131A4 File Offset: 0x001113A4
		internal ConfigurationCachePerfCountersInstance(string instanceName) : base(instanceName, ConfigurationCachePerfCounters.CategoryName)
		{
			bool flag = false;
			List<ExPerformanceCounter> list = new List<ExPerformanceCounter>();
			try
			{
				ExPerformanceCounter exPerformanceCounter = new ExPerformanceCounter(base.CategoryName, "Configuration Cache requests per sec", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter);
				this.Requests = new ExPerformanceCounter(base.CategoryName, "Configuration Cache Total requests", instanceName, true, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter
				});
				list.Add(this.Requests);
				this.HitRatio = new ExPerformanceCounter(base.CategoryName, "Configuration Cache hit ratio", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.HitRatio);
				this.HitRatio_Base = new ExPerformanceCounter(base.CategoryName, "Configuration Cache hit ratio base counter", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.HitRatio_Base);
				this.CacheSize = new ExPerformanceCounter(base.CategoryName, "Configuration Cache size", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.CacheSize);
				this.CacheSizeMB = new ExPerformanceCounter(base.CategoryName, "Configuration Cache size in MB", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.CacheSizeMB);
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

		// Token: 0x06003F5E RID: 16222 RVA: 0x00113354 File Offset: 0x00111554
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

		// Token: 0x0400239E RID: 9118
		public readonly ExPerformanceCounter Requests;

		// Token: 0x0400239F RID: 9119
		public readonly ExPerformanceCounter HitRatio;

		// Token: 0x040023A0 RID: 9120
		public readonly ExPerformanceCounter HitRatio_Base;

		// Token: 0x040023A1 RID: 9121
		public readonly ExPerformanceCounter CacheSize;

		// Token: 0x040023A2 RID: 9122
		public readonly ExPerformanceCounter CacheSizeMB;
	}
}
