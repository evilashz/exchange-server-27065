using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Directory.TopologyService
{
	// Token: 0x02000036 RID: 54
	internal sealed class ForestDiscoveryPerfCountersInstance : PerformanceCounterInstance
	{
		// Token: 0x06000226 RID: 550 RVA: 0x0000E7A0 File Offset: 0x0000C9A0
		internal ForestDiscoveryPerfCountersInstance(string instanceName, ForestDiscoveryPerfCountersInstance autoUpdateTotalInstance) : base(instanceName, "MSExchange ADAccess Forest Discovery")
		{
			bool flag = false;
			List<ExPerformanceCounter> list = new List<ExPerformanceCounter>();
			try
			{
				this.DCInSite = new ExPerformanceCounter(base.CategoryName, "In-site Domain Controllers", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.DCInSite);
				this.GCInSite = new ExPerformanceCounter(base.CategoryName, "In-site Global Catalogs", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.GCInSite);
				this.DCOutOfSite = new ExPerformanceCounter(base.CategoryName, "Out-of-site Domain Controllers", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.DCOutOfSite);
				this.GCOutOfSite = new ExPerformanceCounter(base.CategoryName, "Out-of-site Global Catalogs", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.GCOutOfSite);
				this.DiscoveryFailures = new ExPerformanceCounter(base.CategoryName, "Discovery Failures", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.DiscoveryFailures);
				this.TopologyVersion = new ExPerformanceCounter(base.CategoryName, "Topology Version", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.TopologyVersion);
				this.AverageDiscoveryTime = new ExPerformanceCounter(base.CategoryName, "Average Discovery Time", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.AverageDiscoveryTime);
				this.AverageDiscoveryTimeBase = new ExPerformanceCounter(base.CategoryName, "Average Discovery Time Base", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.AverageDiscoveryTimeBase);
				this.MaxDiscoveryTime = new ExPerformanceCounter(base.CategoryName, "Max Discovery Time", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.MaxDiscoveryTime);
				this.DiscoveryTime = new ExPerformanceCounter(base.CategoryName, "Last Topology Discovery Duration Time", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.DiscoveryTime);
				this.WebServiceGetServersAverageCallDuration = new ExPerformanceCounter(base.CategoryName, "Web Service - Get Servers For Role Average Call Duration", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.WebServiceGetServersAverageCallDuration);
				this.WebServiceGetServersAverageCallDurationBase = new ExPerformanceCounter(base.CategoryName, "Web Service - Get Servers For Role Average Call Duration Base", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.WebServiceGetServersAverageCallDurationBase);
				this.WebServiceGetServersMaxCallDuration = new ExPerformanceCounter(base.CategoryName, "Web Service - Get Servers For Role Max Call Duration", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.WebServiceGetServersMaxCallDuration);
				long num = this.DCInSite.RawValue;
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

		// Token: 0x06000227 RID: 551 RVA: 0x0000EA70 File Offset: 0x0000CC70
		internal ForestDiscoveryPerfCountersInstance(string instanceName) : base(instanceName, "MSExchange ADAccess Forest Discovery")
		{
			bool flag = false;
			List<ExPerformanceCounter> list = new List<ExPerformanceCounter>();
			try
			{
				this.DCInSite = new ExPerformanceCounter(base.CategoryName, "In-site Domain Controllers", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.DCInSite);
				this.GCInSite = new ExPerformanceCounter(base.CategoryName, "In-site Global Catalogs", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.GCInSite);
				this.DCOutOfSite = new ExPerformanceCounter(base.CategoryName, "Out-of-site Domain Controllers", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.DCOutOfSite);
				this.GCOutOfSite = new ExPerformanceCounter(base.CategoryName, "Out-of-site Global Catalogs", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.GCOutOfSite);
				this.DiscoveryFailures = new ExPerformanceCounter(base.CategoryName, "Discovery Failures", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.DiscoveryFailures);
				this.TopologyVersion = new ExPerformanceCounter(base.CategoryName, "Topology Version", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.TopologyVersion);
				this.AverageDiscoveryTime = new ExPerformanceCounter(base.CategoryName, "Average Discovery Time", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.AverageDiscoveryTime);
				this.AverageDiscoveryTimeBase = new ExPerformanceCounter(base.CategoryName, "Average Discovery Time Base", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.AverageDiscoveryTimeBase);
				this.MaxDiscoveryTime = new ExPerformanceCounter(base.CategoryName, "Max Discovery Time", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.MaxDiscoveryTime);
				this.DiscoveryTime = new ExPerformanceCounter(base.CategoryName, "Last Topology Discovery Duration Time", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.DiscoveryTime);
				this.WebServiceGetServersAverageCallDuration = new ExPerformanceCounter(base.CategoryName, "Web Service - Get Servers For Role Average Call Duration", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.WebServiceGetServersAverageCallDuration);
				this.WebServiceGetServersAverageCallDurationBase = new ExPerformanceCounter(base.CategoryName, "Web Service - Get Servers For Role Average Call Duration Base", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.WebServiceGetServersAverageCallDurationBase);
				this.WebServiceGetServersMaxCallDuration = new ExPerformanceCounter(base.CategoryName, "Web Service - Get Servers For Role Max Call Duration", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.WebServiceGetServersMaxCallDuration);
				long num = this.DCInSite.RawValue;
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

		// Token: 0x06000228 RID: 552 RVA: 0x0000ED40 File Offset: 0x0000CF40
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

		// Token: 0x0400016C RID: 364
		public readonly ExPerformanceCounter DCInSite;

		// Token: 0x0400016D RID: 365
		public readonly ExPerformanceCounter GCInSite;

		// Token: 0x0400016E RID: 366
		public readonly ExPerformanceCounter DCOutOfSite;

		// Token: 0x0400016F RID: 367
		public readonly ExPerformanceCounter GCOutOfSite;

		// Token: 0x04000170 RID: 368
		public readonly ExPerformanceCounter DiscoveryFailures;

		// Token: 0x04000171 RID: 369
		public readonly ExPerformanceCounter TopologyVersion;

		// Token: 0x04000172 RID: 370
		public readonly ExPerformanceCounter AverageDiscoveryTime;

		// Token: 0x04000173 RID: 371
		public readonly ExPerformanceCounter AverageDiscoveryTimeBase;

		// Token: 0x04000174 RID: 372
		public readonly ExPerformanceCounter MaxDiscoveryTime;

		// Token: 0x04000175 RID: 373
		public readonly ExPerformanceCounter DiscoveryTime;

		// Token: 0x04000176 RID: 374
		public readonly ExPerformanceCounter WebServiceGetServersAverageCallDuration;

		// Token: 0x04000177 RID: 375
		public readonly ExPerformanceCounter WebServiceGetServersAverageCallDurationBase;

		// Token: 0x04000178 RID: 376
		public readonly ExPerformanceCounter WebServiceGetServersMaxCallDuration;
	}
}
