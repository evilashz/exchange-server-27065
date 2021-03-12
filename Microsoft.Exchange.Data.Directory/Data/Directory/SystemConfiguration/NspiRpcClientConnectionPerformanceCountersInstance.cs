using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000A4A RID: 2634
	internal sealed class NspiRpcClientConnectionPerformanceCountersInstance : PerformanceCounterInstance
	{
		// Token: 0x06007876 RID: 30838 RVA: 0x0018EDE8 File Offset: 0x0018CFE8
		internal NspiRpcClientConnectionPerformanceCountersInstance(string instanceName, NspiRpcClientConnectionPerformanceCountersInstance autoUpdateTotalInstance) : base(instanceName, "MSExchange NSPI RPC Client Connections")
		{
			bool flag = false;
			List<ExPerformanceCounter> list = new List<ExPerformanceCounter>();
			try
			{
				this.NumberOfOpenConnections = new ExPerformanceCounter(base.CategoryName, "Number of Open NSPI RPC Client Connections", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.NumberOfOpenConnections);
				long num = this.NumberOfOpenConnections.RawValue;
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

		// Token: 0x06007877 RID: 30839 RVA: 0x0018EEA8 File Offset: 0x0018D0A8
		internal NspiRpcClientConnectionPerformanceCountersInstance(string instanceName) : base(instanceName, "MSExchange NSPI RPC Client Connections")
		{
			bool flag = false;
			List<ExPerformanceCounter> list = new List<ExPerformanceCounter>();
			try
			{
				this.NumberOfOpenConnections = new ExPerformanceCounter(base.CategoryName, "Number of Open NSPI RPC Client Connections", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.NumberOfOpenConnections);
				long num = this.NumberOfOpenConnections.RawValue;
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

		// Token: 0x06007878 RID: 30840 RVA: 0x0018EF68 File Offset: 0x0018D168
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

		// Token: 0x04004F58 RID: 20312
		public readonly ExPerformanceCounter NumberOfOpenConnections;
	}
}
