using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x02000576 RID: 1398
	internal sealed class CertificateValidationResultCachePerfCountersInstance : PerformanceCounterInstance
	{
		// Token: 0x06003FCE RID: 16334 RVA: 0x00115D40 File Offset: 0x00113F40
		internal CertificateValidationResultCachePerfCountersInstance(string instanceName, CertificateValidationResultCachePerfCountersInstance autoUpdateTotalInstance) : base(instanceName, CertificateValidationResultCachePerfCounters.CategoryName)
		{
			bool flag = false;
			List<ExPerformanceCounter> list = new List<ExPerformanceCounter>();
			try
			{
				ExPerformanceCounter exPerformanceCounter = new ExPerformanceCounter(base.CategoryName, "Requests per Second", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter);
				this.Requests = new ExPerformanceCounter(base.CategoryName, "Total Requests", instanceName, true, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter
				});
				list.Add(this.Requests);
				this.HitRatio = new ExPerformanceCounter(base.CategoryName, "Hit Ratio", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.HitRatio);
				this.HitRatio_Base = new ExPerformanceCounter(base.CategoryName, "Certificate Validation Result Cache hit ratio base counter", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.HitRatio_Base);
				this.CacheSize = new ExPerformanceCounter(base.CategoryName, "Cache Size", instanceName, true, null, new ExPerformanceCounter[0]);
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

		// Token: 0x06003FCF RID: 16335 RVA: 0x00115EB0 File Offset: 0x001140B0
		internal CertificateValidationResultCachePerfCountersInstance(string instanceName) : base(instanceName, CertificateValidationResultCachePerfCounters.CategoryName)
		{
			bool flag = false;
			List<ExPerformanceCounter> list = new List<ExPerformanceCounter>();
			try
			{
				ExPerformanceCounter exPerformanceCounter = new ExPerformanceCounter(base.CategoryName, "Requests per Second", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter);
				this.Requests = new ExPerformanceCounter(base.CategoryName, "Total Requests", instanceName, true, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter
				});
				list.Add(this.Requests);
				this.HitRatio = new ExPerformanceCounter(base.CategoryName, "Hit Ratio", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.HitRatio);
				this.HitRatio_Base = new ExPerformanceCounter(base.CategoryName, "Certificate Validation Result Cache hit ratio base counter", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.HitRatio_Base);
				this.CacheSize = new ExPerformanceCounter(base.CategoryName, "Cache Size", instanceName, true, null, new ExPerformanceCounter[0]);
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

		// Token: 0x06003FD0 RID: 16336 RVA: 0x00116020 File Offset: 0x00114220
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

		// Token: 0x040023E3 RID: 9187
		public readonly ExPerformanceCounter Requests;

		// Token: 0x040023E4 RID: 9188
		public readonly ExPerformanceCounter HitRatio;

		// Token: 0x040023E5 RID: 9189
		public readonly ExPerformanceCounter HitRatio_Base;

		// Token: 0x040023E6 RID: 9190
		public readonly ExPerformanceCounter CacheSize;
	}
}
