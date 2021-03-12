using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x0200056E RID: 1390
	internal sealed class E2ELatencyBucketsPerfCountersInstance : PerformanceCounterInstance
	{
		// Token: 0x06003F95 RID: 16277 RVA: 0x001143D8 File Offset: 0x001125D8
		internal E2ELatencyBucketsPerfCountersInstance(string instanceName, E2ELatencyBucketsPerfCountersInstance autoUpdateTotalInstance) : base(instanceName, "MSExchangeTransport E2E Latency Buckets")
		{
			bool flag = false;
			List<ExPerformanceCounter> list = new List<ExPerformanceCounter>();
			try
			{
				this.DeliverHighPriority = new ExPerformanceCounter(base.CategoryName, "Deliver High Priority", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.DeliverHighPriority, new ExPerformanceCounter[0]);
				list.Add(this.DeliverHighPriority);
				this.DeliverNormalPriority = new ExPerformanceCounter(base.CategoryName, "Deliver Normal Priority", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.DeliverNormalPriority, new ExPerformanceCounter[0]);
				list.Add(this.DeliverNormalPriority);
				this.DeliverLowPriority = new ExPerformanceCounter(base.CategoryName, "Deliver Low Priority", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.DeliverLowPriority, new ExPerformanceCounter[0]);
				list.Add(this.DeliverLowPriority);
				this.DeliverNonePriority = new ExPerformanceCounter(base.CategoryName, "Deliver None Priority", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.DeliverNonePriority, new ExPerformanceCounter[0]);
				list.Add(this.DeliverNonePriority);
				this.SendToExternalHighPriority = new ExPerformanceCounter(base.CategoryName, "Send To External High Priority", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.SendToExternalHighPriority, new ExPerformanceCounter[0]);
				list.Add(this.SendToExternalHighPriority);
				this.SendToExternalNormalPriority = new ExPerformanceCounter(base.CategoryName, "Send To External Normal Priority", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.SendToExternalNormalPriority, new ExPerformanceCounter[0]);
				list.Add(this.SendToExternalNormalPriority);
				this.SendToExternalLowPriority = new ExPerformanceCounter(base.CategoryName, "Send To External Low Priority", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.SendToExternalLowPriority, new ExPerformanceCounter[0]);
				list.Add(this.SendToExternalLowPriority);
				this.SendToExternalNonePriority = new ExPerformanceCounter(base.CategoryName, "Send To External None Priority", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.SendToExternalNonePriority, new ExPerformanceCounter[0]);
				list.Add(this.SendToExternalNonePriority);
				long num = this.DeliverHighPriority.RawValue;
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

		// Token: 0x06003F96 RID: 16278 RVA: 0x0011462C File Offset: 0x0011282C
		internal E2ELatencyBucketsPerfCountersInstance(string instanceName) : base(instanceName, "MSExchangeTransport E2E Latency Buckets")
		{
			bool flag = false;
			List<ExPerformanceCounter> list = new List<ExPerformanceCounter>();
			try
			{
				this.DeliverHighPriority = new ExPerformanceCounter(base.CategoryName, "Deliver High Priority", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.DeliverHighPriority);
				this.DeliverNormalPriority = new ExPerformanceCounter(base.CategoryName, "Deliver Normal Priority", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.DeliverNormalPriority);
				this.DeliverLowPriority = new ExPerformanceCounter(base.CategoryName, "Deliver Low Priority", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.DeliverLowPriority);
				this.DeliverNonePriority = new ExPerformanceCounter(base.CategoryName, "Deliver None Priority", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.DeliverNonePriority);
				this.SendToExternalHighPriority = new ExPerformanceCounter(base.CategoryName, "Send To External High Priority", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.SendToExternalHighPriority);
				this.SendToExternalNormalPriority = new ExPerformanceCounter(base.CategoryName, "Send To External Normal Priority", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.SendToExternalNormalPriority);
				this.SendToExternalLowPriority = new ExPerformanceCounter(base.CategoryName, "Send To External Low Priority", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.SendToExternalLowPriority);
				this.SendToExternalNonePriority = new ExPerformanceCounter(base.CategoryName, "Send To External None Priority", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.SendToExternalNonePriority);
				long num = this.DeliverHighPriority.RawValue;
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

		// Token: 0x06003F97 RID: 16279 RVA: 0x00114828 File Offset: 0x00112A28
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

		// Token: 0x040023C1 RID: 9153
		public readonly ExPerformanceCounter DeliverHighPriority;

		// Token: 0x040023C2 RID: 9154
		public readonly ExPerformanceCounter DeliverNormalPriority;

		// Token: 0x040023C3 RID: 9155
		public readonly ExPerformanceCounter DeliverLowPriority;

		// Token: 0x040023C4 RID: 9156
		public readonly ExPerformanceCounter DeliverNonePriority;

		// Token: 0x040023C5 RID: 9157
		public readonly ExPerformanceCounter SendToExternalHighPriority;

		// Token: 0x040023C6 RID: 9158
		public readonly ExPerformanceCounter SendToExternalNormalPriority;

		// Token: 0x040023C7 RID: 9159
		public readonly ExPerformanceCounter SendToExternalLowPriority;

		// Token: 0x040023C8 RID: 9160
		public readonly ExPerformanceCounter SendToExternalNonePriority;
	}
}
