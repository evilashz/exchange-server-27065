using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x0200038C RID: 908
	internal sealed class NetworkManagerPerfmonInstance : PerformanceCounterInstance
	{
		// Token: 0x06002463 RID: 9315 RVA: 0x000AAA24 File Offset: 0x000A8C24
		internal NetworkManagerPerfmonInstance(string instanceName, NetworkManagerPerfmonInstance autoUpdateTotalInstance) : base(instanceName, "MSExchange Network Manager")
		{
			bool flag = false;
			List<ExPerformanceCounter> list = new List<ExPerformanceCounter>();
			try
			{
				this.LogCopyThruputReceived = new ExPerformanceCounter(base.CategoryName, "Log Copy KB Received/Sec", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.LogCopyThruputReceived, new ExPerformanceCounter[0]);
				list.Add(this.LogCopyThruputReceived);
				this.SeederThruputReceived = new ExPerformanceCounter(base.CategoryName, "Seeder KB Received/Sec", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.SeederThruputReceived, new ExPerformanceCounter[0]);
				list.Add(this.SeederThruputReceived);
				this.TotalCompressedLogBytesReceived = new ExPerformanceCounter(base.CategoryName, "Total Compressed Log Bytes Received", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.TotalCompressedLogBytesReceived, new ExPerformanceCounter[0]);
				list.Add(this.TotalCompressedLogBytesReceived);
				this.TotalLogBytesDecompressed = new ExPerformanceCounter(base.CategoryName, "Total Log Bytes Decompressed", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.TotalLogBytesDecompressed, new ExPerformanceCounter[0]);
				list.Add(this.TotalLogBytesDecompressed);
				this.TotalCompressedSeedingBytesReceived = new ExPerformanceCounter(base.CategoryName, "Total Compressed Seeding Bytes Received", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.TotalCompressedSeedingBytesReceived, new ExPerformanceCounter[0]);
				list.Add(this.TotalCompressedSeedingBytesReceived);
				this.TotalSeedingBytesDecompressed = new ExPerformanceCounter(base.CategoryName, "Total Seeding Bytes Decompressed", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.TotalSeedingBytesDecompressed, new ExPerformanceCounter[0]);
				list.Add(this.TotalSeedingBytesDecompressed);
				long num = this.LogCopyThruputReceived.RawValue;
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

		// Token: 0x06002464 RID: 9316 RVA: 0x000AAC10 File Offset: 0x000A8E10
		internal NetworkManagerPerfmonInstance(string instanceName) : base(instanceName, "MSExchange Network Manager")
		{
			bool flag = false;
			List<ExPerformanceCounter> list = new List<ExPerformanceCounter>();
			try
			{
				this.LogCopyThruputReceived = new ExPerformanceCounter(base.CategoryName, "Log Copy KB Received/Sec", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.LogCopyThruputReceived);
				this.SeederThruputReceived = new ExPerformanceCounter(base.CategoryName, "Seeder KB Received/Sec", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.SeederThruputReceived);
				this.TotalCompressedLogBytesReceived = new ExPerformanceCounter(base.CategoryName, "Total Compressed Log Bytes Received", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.TotalCompressedLogBytesReceived);
				this.TotalLogBytesDecompressed = new ExPerformanceCounter(base.CategoryName, "Total Log Bytes Decompressed", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.TotalLogBytesDecompressed);
				this.TotalCompressedSeedingBytesReceived = new ExPerformanceCounter(base.CategoryName, "Total Compressed Seeding Bytes Received", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.TotalCompressedSeedingBytesReceived);
				this.TotalSeedingBytesDecompressed = new ExPerformanceCounter(base.CategoryName, "Total Seeding Bytes Decompressed", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.TotalSeedingBytesDecompressed);
				long num = this.LogCopyThruputReceived.RawValue;
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

		// Token: 0x06002465 RID: 9317 RVA: 0x000AADB8 File Offset: 0x000A8FB8
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

		// Token: 0x04001082 RID: 4226
		public readonly ExPerformanceCounter LogCopyThruputReceived;

		// Token: 0x04001083 RID: 4227
		public readonly ExPerformanceCounter SeederThruputReceived;

		// Token: 0x04001084 RID: 4228
		public readonly ExPerformanceCounter TotalCompressedLogBytesReceived;

		// Token: 0x04001085 RID: 4229
		public readonly ExPerformanceCounter TotalLogBytesDecompressed;

		// Token: 0x04001086 RID: 4230
		public readonly ExPerformanceCounter TotalCompressedSeedingBytesReceived;

		// Token: 0x04001087 RID: 4231
		public readonly ExPerformanceCounter TotalSeedingBytesDecompressed;
	}
}
