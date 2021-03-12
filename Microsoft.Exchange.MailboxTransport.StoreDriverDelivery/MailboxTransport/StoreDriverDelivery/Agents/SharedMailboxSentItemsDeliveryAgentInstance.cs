using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MailboxTransport.StoreDriverDelivery.Agents
{
	// Token: 0x020000BC RID: 188
	internal sealed class SharedMailboxSentItemsDeliveryAgentInstance : PerformanceCounterInstance
	{
		// Token: 0x060005E6 RID: 1510 RVA: 0x0001FF20 File Offset: 0x0001E120
		internal SharedMailboxSentItemsDeliveryAgentInstance(string instanceName, SharedMailboxSentItemsDeliveryAgentInstance autoUpdateTotalInstance) : base(instanceName, "MSExchange Shared Mailbox Sent Items Agent")
		{
			bool flag = false;
			List<ExPerformanceCounter> list = new List<ExPerformanceCounter>();
			try
			{
				this.AverageMessageCopyTime = new ExPerformanceCounter(base.CategoryName, "Average Delivery Time", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.AverageMessageCopyTime, new ExPerformanceCounter[0]);
				list.Add(this.AverageMessageCopyTime);
				this.AverageMessageCopyTimeBase = new ExPerformanceCounter(base.CategoryName, "Base for Average Delivery Time", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.AverageMessageCopyTimeBase, new ExPerformanceCounter[0]);
				list.Add(this.AverageMessageCopyTimeBase);
				this.SentItemsMessages = new ExPerformanceCounter(base.CategoryName, "Number of Sent Item Messages", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.SentItemsMessages, new ExPerformanceCounter[0]);
				list.Add(this.SentItemsMessages);
				this.Errors = new ExPerformanceCounter(base.CategoryName, "Number of Errors", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.Errors, new ExPerformanceCounter[0]);
				list.Add(this.Errors);
				long num = this.AverageMessageCopyTime.RawValue;
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

		// Token: 0x060005E7 RID: 1511 RVA: 0x0002008C File Offset: 0x0001E28C
		internal SharedMailboxSentItemsDeliveryAgentInstance(string instanceName) : base(instanceName, "MSExchange Shared Mailbox Sent Items Agent")
		{
			bool flag = false;
			List<ExPerformanceCounter> list = new List<ExPerformanceCounter>();
			try
			{
				this.AverageMessageCopyTime = new ExPerformanceCounter(base.CategoryName, "Average Delivery Time", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.AverageMessageCopyTime);
				this.AverageMessageCopyTimeBase = new ExPerformanceCounter(base.CategoryName, "Base for Average Delivery Time", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.AverageMessageCopyTimeBase);
				this.SentItemsMessages = new ExPerformanceCounter(base.CategoryName, "Number of Sent Item Messages", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.SentItemsMessages);
				this.Errors = new ExPerformanceCounter(base.CategoryName, "Number of Errors", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.Errors);
				long num = this.AverageMessageCopyTime.RawValue;
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

		// Token: 0x060005E8 RID: 1512 RVA: 0x000201CC File Offset: 0x0001E3CC
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

		// Token: 0x04000357 RID: 855
		public readonly ExPerformanceCounter AverageMessageCopyTime;

		// Token: 0x04000358 RID: 856
		public readonly ExPerformanceCounter AverageMessageCopyTimeBase;

		// Token: 0x04000359 RID: 857
		public readonly ExPerformanceCounter SentItemsMessages;

		// Token: 0x0400035A RID: 858
		public readonly ExPerformanceCounter Errors;
	}
}
