using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MailboxTransport.StoreDriverDelivery
{
	// Token: 0x0200004D RID: 77
	internal sealed class MSExchangeStoreDriverDatabaseInstance : PerformanceCounterInstance
	{
		// Token: 0x0600034A RID: 842 RVA: 0x0000DC78 File Offset: 0x0000BE78
		internal MSExchangeStoreDriverDatabaseInstance(string instanceName, MSExchangeStoreDriverDatabaseInstance autoUpdateTotalInstance) : base(instanceName, "MSExchange Delivery Store Driver Database")
		{
			bool flag = false;
			List<ExPerformanceCounter> list = new List<ExPerformanceCounter>();
			try
			{
				this.DeliveryAttempts = new ExPerformanceCounter(base.CategoryName, "Delivery attempts per minute over the last 5 minutes", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.DeliveryAttempts, new ExPerformanceCounter[0]);
				list.Add(this.DeliveryAttempts);
				this.DeliveryFailures = new ExPerformanceCounter(base.CategoryName, "Delivery failures per minute over the last 5 minutes", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.DeliveryFailures, new ExPerformanceCounter[0]);
				list.Add(this.DeliveryFailures);
				this.CurrentDeliveryThreadsPerMdb = new ExPerformanceCounter(base.CategoryName, "Inbound: Number of delivery threads for a given MDB", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.CurrentDeliveryThreadsPerMdb, new ExPerformanceCounter[0]);
				list.Add(this.CurrentDeliveryThreadsPerMdb);
				long num = this.DeliveryAttempts.RawValue;
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

		// Token: 0x0600034B RID: 843 RVA: 0x0000DDAC File Offset: 0x0000BFAC
		internal MSExchangeStoreDriverDatabaseInstance(string instanceName) : base(instanceName, "MSExchange Delivery Store Driver Database")
		{
			bool flag = false;
			List<ExPerformanceCounter> list = new List<ExPerformanceCounter>();
			try
			{
				this.DeliveryAttempts = new ExPerformanceCounter(base.CategoryName, "Delivery attempts per minute over the last 5 minutes", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.DeliveryAttempts);
				this.DeliveryFailures = new ExPerformanceCounter(base.CategoryName, "Delivery failures per minute over the last 5 minutes", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.DeliveryFailures);
				this.CurrentDeliveryThreadsPerMdb = new ExPerformanceCounter(base.CategoryName, "Inbound: Number of delivery threads for a given MDB", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.CurrentDeliveryThreadsPerMdb);
				long num = this.DeliveryAttempts.RawValue;
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

		// Token: 0x0600034C RID: 844 RVA: 0x0000DEC0 File Offset: 0x0000C0C0
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

		// Token: 0x0400017E RID: 382
		public readonly ExPerformanceCounter DeliveryAttempts;

		// Token: 0x0400017F RID: 383
		public readonly ExPerformanceCounter DeliveryFailures;

		// Token: 0x04000180 RID: 384
		public readonly ExPerformanceCounter CurrentDeliveryThreadsPerMdb;
	}
}
