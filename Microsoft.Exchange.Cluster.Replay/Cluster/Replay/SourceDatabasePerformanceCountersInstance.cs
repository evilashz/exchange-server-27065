using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000393 RID: 915
	internal sealed class SourceDatabasePerformanceCountersInstance : PerformanceCounterInstance
	{
		// Token: 0x06002491 RID: 9361 RVA: 0x000ACAB0 File Offset: 0x000AACB0
		internal SourceDatabasePerformanceCountersInstance(string instanceName, SourceDatabasePerformanceCountersInstance autoUpdateTotalInstance) : base(instanceName, "MSExchangeRepl Source Database")
		{
			bool flag = false;
			List<ExPerformanceCounter> list = new List<ExPerformanceCounter>();
			try
			{
				this.TotalBytesSent = new ExPerformanceCounter(base.CategoryName, "Total Bytes Sent", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.TotalBytesSent, new ExPerformanceCounter[0]);
				list.Add(this.TotalBytesSent);
				this.AverageWriteTime = new ExPerformanceCounter(base.CategoryName, "Avg. Network sec/Write", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.AverageWriteTime, new ExPerformanceCounter[0]);
				list.Add(this.AverageWriteTime);
				this.AverageWriteTimeBase = new ExPerformanceCounter(base.CategoryName, "AverageWriteTimeBase", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.AverageWriteTimeBase, new ExPerformanceCounter[0]);
				list.Add(this.AverageWriteTimeBase);
				this.AverageReadTime = new ExPerformanceCounter(base.CategoryName, "Avg. Disk sec/Read", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.AverageReadTime, new ExPerformanceCounter[0]);
				list.Add(this.AverageReadTime);
				this.AverageReadTimeBase = new ExPerformanceCounter(base.CategoryName, "AverageReadTimeBase", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.AverageReadTimeBase, new ExPerformanceCounter[0]);
				list.Add(this.AverageReadTimeBase);
				this.WriteThruput = new ExPerformanceCounter(base.CategoryName, "Network Write Bytes/sec", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.WriteThruput, new ExPerformanceCounter[0]);
				list.Add(this.WriteThruput);
				long num = this.TotalBytesSent.RawValue;
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

		// Token: 0x06002492 RID: 9362 RVA: 0x000ACC9C File Offset: 0x000AAE9C
		internal SourceDatabasePerformanceCountersInstance(string instanceName) : base(instanceName, "MSExchangeRepl Source Database")
		{
			bool flag = false;
			List<ExPerformanceCounter> list = new List<ExPerformanceCounter>();
			try
			{
				this.TotalBytesSent = new ExPerformanceCounter(base.CategoryName, "Total Bytes Sent", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.TotalBytesSent);
				this.AverageWriteTime = new ExPerformanceCounter(base.CategoryName, "Avg. Network sec/Write", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.AverageWriteTime);
				this.AverageWriteTimeBase = new ExPerformanceCounter(base.CategoryName, "AverageWriteTimeBase", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.AverageWriteTimeBase);
				this.AverageReadTime = new ExPerformanceCounter(base.CategoryName, "Avg. Disk sec/Read", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.AverageReadTime);
				this.AverageReadTimeBase = new ExPerformanceCounter(base.CategoryName, "AverageReadTimeBase", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.AverageReadTimeBase);
				this.WriteThruput = new ExPerformanceCounter(base.CategoryName, "Network Write Bytes/sec", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.WriteThruput);
				long num = this.TotalBytesSent.RawValue;
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

		// Token: 0x06002493 RID: 9363 RVA: 0x000ACE44 File Offset: 0x000AB044
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

		// Token: 0x040010D4 RID: 4308
		public readonly ExPerformanceCounter TotalBytesSent;

		// Token: 0x040010D5 RID: 4309
		public readonly ExPerformanceCounter AverageWriteTime;

		// Token: 0x040010D6 RID: 4310
		public readonly ExPerformanceCounter AverageWriteTimeBase;

		// Token: 0x040010D7 RID: 4311
		public readonly ExPerformanceCounter AverageReadTime;

		// Token: 0x040010D8 RID: 4312
		public readonly ExPerformanceCounter AverageReadTimeBase;

		// Token: 0x040010D9 RID: 4313
		public readonly ExPerformanceCounter WriteThruput;
	}
}
