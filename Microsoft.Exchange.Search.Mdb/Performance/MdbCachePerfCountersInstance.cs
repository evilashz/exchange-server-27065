using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Search.Performance
{
	// Token: 0x0200003E RID: 62
	internal sealed class MdbCachePerfCountersInstance : PerformanceCounterInstance
	{
		// Token: 0x060001F1 RID: 497 RVA: 0x0000D588 File Offset: 0x0000B788
		internal MdbCachePerfCountersInstance(string instanceName, MdbCachePerfCountersInstance autoUpdateTotalInstance) : base(instanceName, "MSExchangeSearch MailboxSession Cache")
		{
			bool flag = false;
			List<ExPerformanceCounter> list = new List<ExPerformanceCounter>();
			try
			{
				this.NumberOfRequest = new ExPerformanceCounter(base.CategoryName, "Cache Requests", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.NumberOfRequest);
				this.NumberOfCacheHit = new ExPerformanceCounter(base.CategoryName, "Cache Hits", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.NumberOfCacheHit);
				this.NumberOfCacheMiss = new ExPerformanceCounter(base.CategoryName, "Cache Misses", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.NumberOfCacheMiss);
				this.CacheHitRatio = new ExPerformanceCounter(base.CategoryName, "Cache Hit Ratio", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.CacheHitRatio);
				this.CacheHitRatioBase = new ExPerformanceCounter(base.CategoryName, "Cache Hit Ratio Base", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.CacheHitRatioBase);
				this.CacheMissRatio = new ExPerformanceCounter(base.CategoryName, "Cache Miss Ratio", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.CacheMissRatio);
				this.CacheMissRatioBase = new ExPerformanceCounter(base.CategoryName, "Cache Miss Ratio Base", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.CacheMissRatioBase);
				long num = this.NumberOfRequest.RawValue;
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

		// Token: 0x060001F2 RID: 498 RVA: 0x0000D75C File Offset: 0x0000B95C
		internal MdbCachePerfCountersInstance(string instanceName) : base(instanceName, "MSExchangeSearch MailboxSession Cache")
		{
			bool flag = false;
			List<ExPerformanceCounter> list = new List<ExPerformanceCounter>();
			try
			{
				this.NumberOfRequest = new ExPerformanceCounter(base.CategoryName, "Cache Requests", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.NumberOfRequest);
				this.NumberOfCacheHit = new ExPerformanceCounter(base.CategoryName, "Cache Hits", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.NumberOfCacheHit);
				this.NumberOfCacheMiss = new ExPerformanceCounter(base.CategoryName, "Cache Misses", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.NumberOfCacheMiss);
				this.CacheHitRatio = new ExPerformanceCounter(base.CategoryName, "Cache Hit Ratio", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.CacheHitRatio);
				this.CacheHitRatioBase = new ExPerformanceCounter(base.CategoryName, "Cache Hit Ratio Base", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.CacheHitRatioBase);
				this.CacheMissRatio = new ExPerformanceCounter(base.CategoryName, "Cache Miss Ratio", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.CacheMissRatio);
				this.CacheMissRatioBase = new ExPerformanceCounter(base.CategoryName, "Cache Miss Ratio Base", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.CacheMissRatioBase);
				long num = this.NumberOfRequest.RawValue;
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

		// Token: 0x060001F3 RID: 499 RVA: 0x0000D930 File Offset: 0x0000BB30
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

		// Token: 0x0400013C RID: 316
		public readonly ExPerformanceCounter NumberOfRequest;

		// Token: 0x0400013D RID: 317
		public readonly ExPerformanceCounter NumberOfCacheHit;

		// Token: 0x0400013E RID: 318
		public readonly ExPerformanceCounter NumberOfCacheMiss;

		// Token: 0x0400013F RID: 319
		public readonly ExPerformanceCounter CacheHitRatio;

		// Token: 0x04000140 RID: 320
		public readonly ExPerformanceCounter CacheHitRatioBase;

		// Token: 0x04000141 RID: 321
		public readonly ExPerformanceCounter CacheMissRatio;

		// Token: 0x04000142 RID: 322
		public readonly ExPerformanceCounter CacheMissRatioBase;
	}
}
