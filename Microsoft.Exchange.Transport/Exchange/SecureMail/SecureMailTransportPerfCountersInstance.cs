using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.SecureMail
{
	// Token: 0x02000556 RID: 1366
	internal sealed class SecureMailTransportPerfCountersInstance : PerformanceCounterInstance
	{
		// Token: 0x06003EEA RID: 16106 RVA: 0x0010D9D0 File Offset: 0x0010BBD0
		internal SecureMailTransportPerfCountersInstance(string instanceName, SecureMailTransportPerfCountersInstance autoUpdateTotalInstance) : base(instanceName, "MSExchange Secure Mail Transport")
		{
			bool flag = false;
			List<ExPerformanceCounter> list = new List<ExPerformanceCounter>();
			try
			{
				this.DomainSecureMessagesReceivedTotal = new ExPerformanceCounter(base.CategoryName, "Domain Secure Messages Received", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.DomainSecureMessagesReceivedTotal);
				this.DomainSecureMessagesSentTotal = new ExPerformanceCounter(base.CategoryName, "Domain Secure Messages Sent", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.DomainSecureMessagesSentTotal);
				this.DomainSecureOutboundSessionFailuresTotal = new ExPerformanceCounter(base.CategoryName, "Domain Secure Outbound Session Failures", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.DomainSecureOutboundSessionFailuresTotal);
				long num = this.DomainSecureMessagesReceivedTotal.RawValue;
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

		// Token: 0x06003EEB RID: 16107 RVA: 0x0010DAE4 File Offset: 0x0010BCE4
		internal SecureMailTransportPerfCountersInstance(string instanceName) : base(instanceName, "MSExchange Secure Mail Transport")
		{
			bool flag = false;
			List<ExPerformanceCounter> list = new List<ExPerformanceCounter>();
			try
			{
				this.DomainSecureMessagesReceivedTotal = new ExPerformanceCounter(base.CategoryName, "Domain Secure Messages Received", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.DomainSecureMessagesReceivedTotal);
				this.DomainSecureMessagesSentTotal = new ExPerformanceCounter(base.CategoryName, "Domain Secure Messages Sent", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.DomainSecureMessagesSentTotal);
				this.DomainSecureOutboundSessionFailuresTotal = new ExPerformanceCounter(base.CategoryName, "Domain Secure Outbound Session Failures", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.DomainSecureOutboundSessionFailuresTotal);
				long num = this.DomainSecureMessagesReceivedTotal.RawValue;
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

		// Token: 0x06003EEC RID: 16108 RVA: 0x0010DBF8 File Offset: 0x0010BDF8
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

		// Token: 0x04002309 RID: 8969
		public readonly ExPerformanceCounter DomainSecureMessagesReceivedTotal;

		// Token: 0x0400230A RID: 8970
		public readonly ExPerformanceCounter DomainSecureMessagesSentTotal;

		// Token: 0x0400230B RID: 8971
		public readonly ExPerformanceCounter DomainSecureOutboundSessionFailuresTotal;
	}
}
