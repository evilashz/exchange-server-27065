using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport.MessageDepot
{
	// Token: 0x02000008 RID: 8
	internal sealed class MessageDepotPerfCountersInstance : PerformanceCounterInstance
	{
		// Token: 0x0600005B RID: 91 RVA: 0x000037A4 File Offset: 0x000019A4
		internal MessageDepotPerfCountersInstance(string instanceName, MessageDepotPerfCountersInstance autoUpdateTotalInstance) : base(instanceName, "MSExchangeTransport MessageDepot")
		{
			bool flag = false;
			List<ExPerformanceCounter> list = new List<ExPerformanceCounter>();
			try
			{
				this.ReadyMessages = new ExPerformanceCounter(base.CategoryName, "Ready Messages", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.ReadyMessages);
				this.DeferredMessages = new ExPerformanceCounter(base.CategoryName, "Deferred Messages", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.DeferredMessages);
				this.SuspendedMessages = new ExPerformanceCounter(base.CategoryName, "Suspended Messages", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.SuspendedMessages);
				this.PoisonMessages = new ExPerformanceCounter(base.CategoryName, "Poison Messages", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.PoisonMessages);
				this.RetryMessages = new ExPerformanceCounter(base.CategoryName, "Retry Messages", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.RetryMessages);
				this.ProcessingMessages = new ExPerformanceCounter(base.CategoryName, "Processing Messages", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.ProcessingMessages);
				this.ExpiringMessages = new ExPerformanceCounter(base.CategoryName, "Expiring Messages", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.ExpiringMessages);
				long num = this.ReadyMessages.RawValue;
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

		// Token: 0x0600005C RID: 92 RVA: 0x0000397C File Offset: 0x00001B7C
		internal MessageDepotPerfCountersInstance(string instanceName) : base(instanceName, "MSExchangeTransport MessageDepot")
		{
			bool flag = false;
			List<ExPerformanceCounter> list = new List<ExPerformanceCounter>();
			try
			{
				this.ReadyMessages = new ExPerformanceCounter(base.CategoryName, "Ready Messages", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.ReadyMessages);
				this.DeferredMessages = new ExPerformanceCounter(base.CategoryName, "Deferred Messages", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.DeferredMessages);
				this.SuspendedMessages = new ExPerformanceCounter(base.CategoryName, "Suspended Messages", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.SuspendedMessages);
				this.PoisonMessages = new ExPerformanceCounter(base.CategoryName, "Poison Messages", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.PoisonMessages);
				this.RetryMessages = new ExPerformanceCounter(base.CategoryName, "Retry Messages", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.RetryMessages);
				this.ProcessingMessages = new ExPerformanceCounter(base.CategoryName, "Processing Messages", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.ProcessingMessages);
				this.ExpiringMessages = new ExPerformanceCounter(base.CategoryName, "Expiring Messages", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.ExpiringMessages);
				long num = this.ReadyMessages.RawValue;
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

		// Token: 0x0600005D RID: 93 RVA: 0x00003B54 File Offset: 0x00001D54
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

		// Token: 0x04000031 RID: 49
		public readonly ExPerformanceCounter ReadyMessages;

		// Token: 0x04000032 RID: 50
		public readonly ExPerformanceCounter DeferredMessages;

		// Token: 0x04000033 RID: 51
		public readonly ExPerformanceCounter SuspendedMessages;

		// Token: 0x04000034 RID: 52
		public readonly ExPerformanceCounter PoisonMessages;

		// Token: 0x04000035 RID: 53
		public readonly ExPerformanceCounter RetryMessages;

		// Token: 0x04000036 RID: 54
		public readonly ExPerformanceCounter ProcessingMessages;

		// Token: 0x04000037 RID: 55
		public readonly ExPerformanceCounter ExpiringMessages;
	}
}
