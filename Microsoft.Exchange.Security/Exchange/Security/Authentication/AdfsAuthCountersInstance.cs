using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Security.Authentication
{
	// Token: 0x02000006 RID: 6
	internal sealed class AdfsAuthCountersInstance : PerformanceCounterInstance
	{
		// Token: 0x0600000D RID: 13 RVA: 0x0000267C File Offset: 0x0000087C
		internal AdfsAuthCountersInstance(string instanceName, AdfsAuthCountersInstance autoUpdateTotalInstance) : base(instanceName, "MSExchange AdfsAuth")
		{
			bool flag = false;
			List<ExPerformanceCounter> list = new List<ExPerformanceCounter>();
			try
			{
				this.AdfsFedAuthModuleKeyCacheSize = new ExPerformanceCounter(base.CategoryName, "AdfsFedAuth Module Key Cache Size", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.AdfsFedAuthModuleKeyCacheSize);
				this.AdfsFedAuthModuleKeyCacheHitsRate = new ExPerformanceCounter(base.CategoryName, "AdfsFedAuth Module Key Cache Hits Rate", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.AdfsFedAuthModuleKeyCacheHitsRate);
				this.AdfsFedAuthModuleCacheHitsRateBase = new ExPerformanceCounter(base.CategoryName, "AdfsFedAuth Module Key Cache Hits Rate Base", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.AdfsFedAuthModuleCacheHitsRateBase);
				long num = this.AdfsFedAuthModuleKeyCacheSize.RawValue;
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

		// Token: 0x0600000E RID: 14 RVA: 0x00002790 File Offset: 0x00000990
		internal AdfsAuthCountersInstance(string instanceName) : base(instanceName, "MSExchange AdfsAuth")
		{
			bool flag = false;
			List<ExPerformanceCounter> list = new List<ExPerformanceCounter>();
			try
			{
				this.AdfsFedAuthModuleKeyCacheSize = new ExPerformanceCounter(base.CategoryName, "AdfsFedAuth Module Key Cache Size", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.AdfsFedAuthModuleKeyCacheSize);
				this.AdfsFedAuthModuleKeyCacheHitsRate = new ExPerformanceCounter(base.CategoryName, "AdfsFedAuth Module Key Cache Hits Rate", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.AdfsFedAuthModuleKeyCacheHitsRate);
				this.AdfsFedAuthModuleCacheHitsRateBase = new ExPerformanceCounter(base.CategoryName, "AdfsFedAuth Module Key Cache Hits Rate Base", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.AdfsFedAuthModuleCacheHitsRateBase);
				long num = this.AdfsFedAuthModuleKeyCacheSize.RawValue;
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

		// Token: 0x0600000F RID: 15 RVA: 0x000028A4 File Offset: 0x00000AA4
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

		// Token: 0x0400008E RID: 142
		public readonly ExPerformanceCounter AdfsFedAuthModuleKeyCacheSize;

		// Token: 0x0400008F RID: 143
		public readonly ExPerformanceCounter AdfsFedAuthModuleKeyCacheHitsRate;

		// Token: 0x04000090 RID: 144
		public readonly ExPerformanceCounter AdfsFedAuthModuleCacheHitsRateBase;
	}
}
