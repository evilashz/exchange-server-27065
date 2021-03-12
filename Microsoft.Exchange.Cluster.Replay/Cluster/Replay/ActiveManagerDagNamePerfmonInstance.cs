using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000386 RID: 902
	internal sealed class ActiveManagerDagNamePerfmonInstance : PerformanceCounterInstance
	{
		// Token: 0x06002451 RID: 9297 RVA: 0x000A9B58 File Offset: 0x000A7D58
		internal ActiveManagerDagNamePerfmonInstance(string instanceName, ActiveManagerDagNamePerfmonInstance autoUpdateTotalInstance) : base(instanceName, "MSExchange Active Manager Dag Name Instance")
		{
			bool flag = false;
			List<ExPerformanceCounter> list = new List<ExPerformanceCounter>();
			try
			{
				this.ActiveManagerRoleInDag = new ExPerformanceCounter(base.CategoryName, "Active Manager Role In Dag", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.ActiveManagerRoleInDag);
				long num = this.ActiveManagerRoleInDag.RawValue;
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

		// Token: 0x06002452 RID: 9298 RVA: 0x000A9C18 File Offset: 0x000A7E18
		internal ActiveManagerDagNamePerfmonInstance(string instanceName) : base(instanceName, "MSExchange Active Manager Dag Name Instance")
		{
			bool flag = false;
			List<ExPerformanceCounter> list = new List<ExPerformanceCounter>();
			try
			{
				this.ActiveManagerRoleInDag = new ExPerformanceCounter(base.CategoryName, "Active Manager Role In Dag", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.ActiveManagerRoleInDag);
				long num = this.ActiveManagerRoleInDag.RawValue;
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

		// Token: 0x06002453 RID: 9299 RVA: 0x000A9CD8 File Offset: 0x000A7ED8
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

		// Token: 0x04000F6D RID: 3949
		public readonly ExPerformanceCounter ActiveManagerRoleInDag;
	}
}
