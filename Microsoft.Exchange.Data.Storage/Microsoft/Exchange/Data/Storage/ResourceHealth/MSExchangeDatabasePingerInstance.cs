using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.ResourceHealth
{
	// Token: 0x020001B1 RID: 433
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class MSExchangeDatabasePingerInstance : PerformanceCounterInstance
	{
		// Token: 0x060017C2 RID: 6082 RVA: 0x0007367C File Offset: 0x0007187C
		internal MSExchangeDatabasePingerInstance(string instanceName, MSExchangeDatabasePingerInstance autoUpdateTotalInstance) : base(instanceName, "MSExchange Database Pinger")
		{
			bool flag = false;
			List<ExPerformanceCounter> list = new List<ExPerformanceCounter>();
			try
			{
				this.PingsPerMinute = new ExPerformanceCounter(base.CategoryName, "Pings Per Minute", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.PingsPerMinute);
				this.FailedPings = new ExPerformanceCounter(base.CategoryName, "Failed Pings", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.FailedPings);
				this.PingTimeouts = new ExPerformanceCounter(base.CategoryName, "Number of Ping Timeouts", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.PingTimeouts);
				this.CacheSize = new ExPerformanceCounter(base.CategoryName, "Cache Size", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.CacheSize);
				long num = this.PingsPerMinute.RawValue;
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

		// Token: 0x060017C3 RID: 6083 RVA: 0x000737BC File Offset: 0x000719BC
		internal MSExchangeDatabasePingerInstance(string instanceName) : base(instanceName, "MSExchange Database Pinger")
		{
			bool flag = false;
			List<ExPerformanceCounter> list = new List<ExPerformanceCounter>();
			try
			{
				this.PingsPerMinute = new ExPerformanceCounter(base.CategoryName, "Pings Per Minute", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.PingsPerMinute);
				this.FailedPings = new ExPerformanceCounter(base.CategoryName, "Failed Pings", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.FailedPings);
				this.PingTimeouts = new ExPerformanceCounter(base.CategoryName, "Number of Ping Timeouts", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.PingTimeouts);
				this.CacheSize = new ExPerformanceCounter(base.CategoryName, "Cache Size", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.CacheSize);
				long num = this.PingsPerMinute.RawValue;
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

		// Token: 0x060017C4 RID: 6084 RVA: 0x000738FC File Offset: 0x00071AFC
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

		// Token: 0x04000C37 RID: 3127
		public readonly ExPerformanceCounter PingsPerMinute;

		// Token: 0x04000C38 RID: 3128
		public readonly ExPerformanceCounter FailedPings;

		// Token: 0x04000C39 RID: 3129
		public readonly ExPerformanceCounter PingTimeouts;

		// Token: 0x04000C3A RID: 3130
		public readonly ExPerformanceCounter CacheSize;
	}
}
