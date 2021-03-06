using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Server.Storage.StoreCommonServices;

namespace Microsoft.Exchange.Server.Storage.HA
{
	// Token: 0x02000005 RID: 5
	internal sealed class ActiveDatabaseSenderPerformanceCountersInstance : PerformanceCounterInstance
	{
		// Token: 0x0600001C RID: 28 RVA: 0x0000292C File Offset: 0x00000B2C
		internal ActiveDatabaseSenderPerformanceCountersInstance(string instanceName, ActiveDatabaseSenderPerformanceCountersInstance autoUpdateTotalInstance) : base(instanceName, "MSExchangeIS HA Active Database Sender")
		{
			bool flag = false;
			List<ExPerformanceCounter> list = new List<ExPerformanceCounter>();
			try
			{
				this.TotalBytesSent = new BufferedPerformanceCounter(base.CategoryName, "Total Bytes Sent", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.TotalBytesSent, new ExPerformanceCounter[0]);
				list.Add(this.TotalBytesSent);
				this.TotalNetworkWrites = new BufferedPerformanceCounter(base.CategoryName, "Total Network Writes", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.TotalNetworkWrites, new ExPerformanceCounter[0]);
				list.Add(this.TotalNetworkWrites);
				this.WritesPerSec = new BufferedPerformanceCounter(base.CategoryName, "Network Writes/sec", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.WritesPerSec, new ExPerformanceCounter[0]);
				list.Add(this.WritesPerSec);
				this.AverageWriteTime = new BufferedPerformanceCounter(base.CategoryName, "Avg. Network sec/Write", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.AverageWriteTime, new ExPerformanceCounter[0]);
				list.Add(this.AverageWriteTime);
				this.AverageWriteTimeBase = new BufferedPerformanceCounter(base.CategoryName, "AverageWriteTimeBase", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.AverageWriteTimeBase, new ExPerformanceCounter[0]);
				list.Add(this.AverageWriteTimeBase);
				this.AverageWriteSize = new BufferedPerformanceCounter(base.CategoryName, "Avg. Network Bytes/Write", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.AverageWriteSize, new ExPerformanceCounter[0]);
				list.Add(this.AverageWriteSize);
				this.AverageWriteSizeBase = new BufferedPerformanceCounter(base.CategoryName, "AverageWriteSizeBase", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.AverageWriteSizeBase, new ExPerformanceCounter[0]);
				list.Add(this.AverageWriteSizeBase);
				this.WriteThruput = new BufferedPerformanceCounter(base.CategoryName, "Network Write Bytes/sec", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.WriteThruput, new ExPerformanceCounter[0]);
				list.Add(this.WriteThruput);
				this.AverageWriteAckLatency = new BufferedPerformanceCounter(base.CategoryName, "Avg. mSec/Write Ack", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.AverageWriteAckLatency, new ExPerformanceCounter[0]);
				list.Add(this.AverageWriteAckLatency);
				this.AverageWriteAckLatencyBase = new BufferedPerformanceCounter(base.CategoryName, "AverageWriteAckLatencyBase", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.AverageWriteAckLatencyBase, new ExPerformanceCounter[0]);
				list.Add(this.AverageWriteAckLatencyBase);
				this.AverageSocketWriteLatency = new BufferedPerformanceCounter(base.CategoryName, "Avg. Microsecond/SocketWrite", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.AverageSocketWriteLatency, new ExPerformanceCounter[0]);
				list.Add(this.AverageSocketWriteLatency);
				this.AverageSocketWriteLatencyBase = new BufferedPerformanceCounter(base.CategoryName, "AverageSocketWriteLatencyBase", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.AverageSocketWriteLatencyBase, new ExPerformanceCounter[0]);
				list.Add(this.AverageSocketWriteLatencyBase);
				this.AcknowledgedGenerationNumber = new BufferedPerformanceCounter(base.CategoryName, "Acknowledged Generation Number", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.AcknowledgedGenerationNumber, new ExPerformanceCounter[0]);
				list.Add(this.AcknowledgedGenerationNumber);
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

		// Token: 0x0600001D RID: 29 RVA: 0x00002C88 File Offset: 0x00000E88
		internal ActiveDatabaseSenderPerformanceCountersInstance(string instanceName) : base(instanceName, "MSExchangeIS HA Active Database Sender")
		{
			bool flag = false;
			List<ExPerformanceCounter> list = new List<ExPerformanceCounter>();
			try
			{
				this.TotalBytesSent = new ExPerformanceCounter(base.CategoryName, "Total Bytes Sent", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.TotalBytesSent);
				this.TotalNetworkWrites = new ExPerformanceCounter(base.CategoryName, "Total Network Writes", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.TotalNetworkWrites);
				this.WritesPerSec = new ExPerformanceCounter(base.CategoryName, "Network Writes/sec", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.WritesPerSec);
				this.AverageWriteTime = new ExPerformanceCounter(base.CategoryName, "Avg. Network sec/Write", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.AverageWriteTime);
				this.AverageWriteTimeBase = new ExPerformanceCounter(base.CategoryName, "AverageWriteTimeBase", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.AverageWriteTimeBase);
				this.AverageWriteSize = new ExPerformanceCounter(base.CategoryName, "Avg. Network Bytes/Write", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.AverageWriteSize);
				this.AverageWriteSizeBase = new ExPerformanceCounter(base.CategoryName, "AverageWriteSizeBase", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.AverageWriteSizeBase);
				this.WriteThruput = new ExPerformanceCounter(base.CategoryName, "Network Write Bytes/sec", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.WriteThruput);
				this.AverageWriteAckLatency = new ExPerformanceCounter(base.CategoryName, "Avg. mSec/Write Ack", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.AverageWriteAckLatency);
				this.AverageWriteAckLatencyBase = new ExPerformanceCounter(base.CategoryName, "AverageWriteAckLatencyBase", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.AverageWriteAckLatencyBase);
				this.AverageSocketWriteLatency = new ExPerformanceCounter(base.CategoryName, "Avg. Microsecond/SocketWrite", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.AverageSocketWriteLatency);
				this.AverageSocketWriteLatencyBase = new ExPerformanceCounter(base.CategoryName, "AverageSocketWriteLatencyBase", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.AverageSocketWriteLatencyBase);
				this.AcknowledgedGenerationNumber = new ExPerformanceCounter(base.CategoryName, "Acknowledged Generation Number", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.AcknowledgedGenerationNumber);
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

		// Token: 0x0600001E RID: 30 RVA: 0x00002F58 File Offset: 0x00001158
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

		// Token: 0x04000012 RID: 18
		public readonly ExPerformanceCounter TotalBytesSent;

		// Token: 0x04000013 RID: 19
		public readonly ExPerformanceCounter TotalNetworkWrites;

		// Token: 0x04000014 RID: 20
		public readonly ExPerformanceCounter WritesPerSec;

		// Token: 0x04000015 RID: 21
		public readonly ExPerformanceCounter AverageWriteTime;

		// Token: 0x04000016 RID: 22
		public readonly ExPerformanceCounter AverageWriteTimeBase;

		// Token: 0x04000017 RID: 23
		public readonly ExPerformanceCounter AverageWriteSize;

		// Token: 0x04000018 RID: 24
		public readonly ExPerformanceCounter AverageWriteSizeBase;

		// Token: 0x04000019 RID: 25
		public readonly ExPerformanceCounter WriteThruput;

		// Token: 0x0400001A RID: 26
		public readonly ExPerformanceCounter AverageWriteAckLatency;

		// Token: 0x0400001B RID: 27
		public readonly ExPerformanceCounter AverageWriteAckLatencyBase;

		// Token: 0x0400001C RID: 28
		public readonly ExPerformanceCounter AverageSocketWriteLatency;

		// Token: 0x0400001D RID: 29
		public readonly ExPerformanceCounter AverageSocketWriteLatencyBase;

		// Token: 0x0400001E RID: 30
		public readonly ExPerformanceCounter AcknowledgedGenerationNumber;
	}
}
