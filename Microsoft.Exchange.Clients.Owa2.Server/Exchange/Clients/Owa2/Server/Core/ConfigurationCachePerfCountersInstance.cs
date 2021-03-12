using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020004A6 RID: 1190
	internal sealed class ConfigurationCachePerfCountersInstance : PerformanceCounterInstance
	{
		// Token: 0x06002898 RID: 10392 RVA: 0x0009647C File Offset: 0x0009467C
		internal ConfigurationCachePerfCountersInstance(string instanceName, ConfigurationCachePerfCountersInstance autoUpdateTotalInstance) : base(instanceName, "MSExchange Owa Configuration Cache")
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
				this.CacheSizeKB = new ExPerformanceCounter(base.CategoryName, "Configuration Cache size in KB", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.CacheSizeKB);
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

		// Token: 0x06002899 RID: 10393 RVA: 0x0009662C File Offset: 0x0009482C
		internal ConfigurationCachePerfCountersInstance(string instanceName) : base(instanceName, "MSExchange Owa Configuration Cache")
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
				this.CacheSizeKB = new ExPerformanceCounter(base.CategoryName, "Configuration Cache size in KB", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.CacheSizeKB);
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

		// Token: 0x0600289A RID: 10394 RVA: 0x000967DC File Offset: 0x000949DC
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

		// Token: 0x04001782 RID: 6018
		public readonly ExPerformanceCounter Requests;

		// Token: 0x04001783 RID: 6019
		public readonly ExPerformanceCounter HitRatio;

		// Token: 0x04001784 RID: 6020
		public readonly ExPerformanceCounter HitRatio_Base;

		// Token: 0x04001785 RID: 6021
		public readonly ExPerformanceCounter CacheSize;

		// Token: 0x04001786 RID: 6022
		public readonly ExPerformanceCounter CacheSizeKB;
	}
}
