using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000384 RID: 900
	internal sealed class ActiveManagerPerfmonInstance : PerformanceCounterInstance
	{
		// Token: 0x06002443 RID: 9283 RVA: 0x000A9820 File Offset: 0x000A7A20
		internal ActiveManagerPerfmonInstance(string instanceName, ActiveManagerPerfmonInstance autoUpdateTotalInstance) : base(instanceName, "MSExchange Active Manager")
		{
			bool flag = false;
			List<ExPerformanceCounter> list = new List<ExPerformanceCounter>();
			try
			{
				this.IsMounted = new ExPerformanceCounter(base.CategoryName, "Database Mounted", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.IsMounted, new ExPerformanceCounter[0]);
				list.Add(this.IsMounted);
				this.CopyRoleIsActive = new ExPerformanceCounter(base.CategoryName, "Database Copy Role Active", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.CopyRoleIsActive, new ExPerformanceCounter[0]);
				list.Add(this.CopyRoleIsActive);
				long num = this.IsMounted.RawValue;
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

		// Token: 0x06002444 RID: 9284 RVA: 0x000A9920 File Offset: 0x000A7B20
		internal ActiveManagerPerfmonInstance(string instanceName) : base(instanceName, "MSExchange Active Manager")
		{
			bool flag = false;
			List<ExPerformanceCounter> list = new List<ExPerformanceCounter>();
			try
			{
				this.IsMounted = new ExPerformanceCounter(base.CategoryName, "Database Mounted", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.IsMounted);
				this.CopyRoleIsActive = new ExPerformanceCounter(base.CategoryName, "Database Copy Role Active", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.CopyRoleIsActive);
				long num = this.IsMounted.RawValue;
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

		// Token: 0x06002445 RID: 9285 RVA: 0x000A9A08 File Offset: 0x000A7C08
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

		// Token: 0x04000F69 RID: 3945
		public readonly ExPerformanceCounter IsMounted;

		// Token: 0x04000F6A RID: 3946
		public readonly ExPerformanceCounter CopyRoleIsActive;
	}
}
