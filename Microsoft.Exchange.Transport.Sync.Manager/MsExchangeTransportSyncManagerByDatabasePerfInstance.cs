using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport.Sync.Manager
{
	// Token: 0x02000061 RID: 97
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class MsExchangeTransportSyncManagerByDatabasePerfInstance : PerformanceCounterInstance
	{
		// Token: 0x06000464 RID: 1124 RVA: 0x0001B93C File Offset: 0x00019B3C
		internal MsExchangeTransportSyncManagerByDatabasePerfInstance(string instanceName, MsExchangeTransportSyncManagerByDatabasePerfInstance autoUpdateTotalInstance) : base(instanceName, "MSExchange Transport Sync Manager By Database")
		{
			bool flag = false;
			List<ExPerformanceCounter> list = new List<ExPerformanceCounter>();
			try
			{
				this.TotalSubscriptionsQueuedInDatabaseQueueManager = new ExPerformanceCounter(base.CategoryName, "Database Queue Manager - Total subscriptions queued", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.TotalSubscriptionsQueuedInDatabaseQueueManager);
				this.TotalSubscriptionInstancesQueuedInDatabaseQueueManager = new ExPerformanceCounter(base.CategoryName, "Database Queue Manager - Total subscription instances queued", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.TotalSubscriptionInstancesQueuedInDatabaseQueueManager);
				long num = this.TotalSubscriptionsQueuedInDatabaseQueueManager.RawValue;
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

		// Token: 0x06000465 RID: 1125 RVA: 0x0001BA24 File Offset: 0x00019C24
		internal MsExchangeTransportSyncManagerByDatabasePerfInstance(string instanceName) : base(instanceName, "MSExchange Transport Sync Manager By Database")
		{
			bool flag = false;
			List<ExPerformanceCounter> list = new List<ExPerformanceCounter>();
			try
			{
				this.TotalSubscriptionsQueuedInDatabaseQueueManager = new ExPerformanceCounter(base.CategoryName, "Database Queue Manager - Total subscriptions queued", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.TotalSubscriptionsQueuedInDatabaseQueueManager);
				this.TotalSubscriptionInstancesQueuedInDatabaseQueueManager = new ExPerformanceCounter(base.CategoryName, "Database Queue Manager - Total subscription instances queued", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.TotalSubscriptionInstancesQueuedInDatabaseQueueManager);
				long num = this.TotalSubscriptionsQueuedInDatabaseQueueManager.RawValue;
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

		// Token: 0x06000466 RID: 1126 RVA: 0x0001BB0C File Offset: 0x00019D0C
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

		// Token: 0x04000297 RID: 663
		public readonly ExPerformanceCounter TotalSubscriptionsQueuedInDatabaseQueueManager;

		// Token: 0x04000298 RID: 664
		public readonly ExPerformanceCounter TotalSubscriptionInstancesQueuedInDatabaseQueueManager;
	}
}
