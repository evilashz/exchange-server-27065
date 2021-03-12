using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport.TransportService
{
	// Token: 0x02000007 RID: 7
	internal sealed class TransportServerAlivePerfCountersInstance : PerformanceCounterInstance
	{
		// Token: 0x06000036 RID: 54 RVA: 0x00004BE8 File Offset: 0x00002DE8
		internal TransportServerAlivePerfCountersInstance(string instanceName, TransportServerAlivePerfCountersInstance autoUpdateTotalInstance) : base(instanceName, "MSExchangeTransport ServerAlive")
		{
			bool flag = false;
			List<ExPerformanceCounter> list = new List<ExPerformanceCounter>();
			try
			{
				this.ServerAlive = new ExPerformanceCounter(base.CategoryName, "Server Alive", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.ServerAlive);
				long num = this.ServerAlive.RawValue;
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

		// Token: 0x06000037 RID: 55 RVA: 0x00004CA8 File Offset: 0x00002EA8
		internal TransportServerAlivePerfCountersInstance(string instanceName) : base(instanceName, "MSExchangeTransport ServerAlive")
		{
			bool flag = false;
			List<ExPerformanceCounter> list = new List<ExPerformanceCounter>();
			try
			{
				this.ServerAlive = new ExPerformanceCounter(base.CategoryName, "Server Alive", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.ServerAlive);
				long num = this.ServerAlive.RawValue;
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

		// Token: 0x06000038 RID: 56 RVA: 0x00004D68 File Offset: 0x00002F68
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

		// Token: 0x040002B2 RID: 690
		public readonly ExPerformanceCounter ServerAlive;
	}
}
