using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MailboxTransport.StoreDriverDelivery
{
	// Token: 0x0200004B RID: 75
	internal sealed class MSExchangeStoreDriverDeliveryAgentInstance : PerformanceCounterInstance
	{
		// Token: 0x0600033B RID: 827 RVA: 0x0000D984 File Offset: 0x0000BB84
		internal MSExchangeStoreDriverDeliveryAgentInstance(string instanceName, MSExchangeStoreDriverDeliveryAgentInstance autoUpdateTotalInstance) : base(instanceName, "MSExchange Delivery Store Driver Agents")
		{
			bool flag = false;
			List<ExPerformanceCounter> list = new List<ExPerformanceCounter>();
			try
			{
				this.DeliveryAgentFailures = new ExPerformanceCounter(base.CategoryName, "StoreDriverDelivery Agent Failure", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.DeliveryAgentFailures, new ExPerformanceCounter[0]);
				list.Add(this.DeliveryAgentFailures);
				long num = this.DeliveryAgentFailures.RawValue;
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

		// Token: 0x0600033C RID: 828 RVA: 0x0000DA4C File Offset: 0x0000BC4C
		internal MSExchangeStoreDriverDeliveryAgentInstance(string instanceName) : base(instanceName, "MSExchange Delivery Store Driver Agents")
		{
			bool flag = false;
			List<ExPerformanceCounter> list = new List<ExPerformanceCounter>();
			try
			{
				this.DeliveryAgentFailures = new ExPerformanceCounter(base.CategoryName, "StoreDriverDelivery Agent Failure", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.DeliveryAgentFailures);
				long num = this.DeliveryAgentFailures.RawValue;
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

		// Token: 0x0600033D RID: 829 RVA: 0x0000DB0C File Offset: 0x0000BD0C
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

		// Token: 0x0400017B RID: 379
		public readonly ExPerformanceCounter DeliveryAgentFailures;
	}
}
