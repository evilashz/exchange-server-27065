using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Protocols.Smtp
{
	// Token: 0x02000545 RID: 1349
	internal sealed class SmtpProxyPerfCountersInstance : PerformanceCounterInstance
	{
		// Token: 0x06003E7F RID: 15999 RVA: 0x00109E70 File Offset: 0x00108070
		internal SmtpProxyPerfCountersInstance(string instanceName, SmtpProxyPerfCountersInstance autoUpdateTotalInstance) : base(instanceName, "MSExchangeFrontEndTransport Smtp Blind Proxy")
		{
			bool flag = false;
			List<ExPerformanceCounter> list = new List<ExPerformanceCounter>();
			try
			{
				this.TotalProxyAttempts = new ExPerformanceCounter(base.CategoryName, "Total Proxy Attempts", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.TotalProxyAttempts);
				this.TotalSuccessfulProxySessions = new ExPerformanceCounter(base.CategoryName, "Total Successful Proxy Sessions", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.TotalSuccessfulProxySessions);
				this.PercentageProxySetupFailures = new ExPerformanceCounter(base.CategoryName, "Percentage Proxy Setup Failures", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.PercentageProxySetupFailures);
				this.TotalProxyUserLookupFailures = new ExPerformanceCounter(base.CategoryName, "Total Proxy User Lookup Failures", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.TotalProxyUserLookupFailures);
				this.TotalProxyBackEndLocatorFailures = new ExPerformanceCounter(base.CategoryName, "Total Proxy BackEndLocator Failures", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.TotalProxyBackEndLocatorFailures);
				this.TotalProxyDnsLookupFailures = new ExPerformanceCounter(base.CategoryName, "Total Proxy Dns Lookup Failures", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.TotalProxyDnsLookupFailures);
				this.TotalProxyConnectionFailures = new ExPerformanceCounter(base.CategoryName, "Total Proxy Connection Failures", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.TotalProxyConnectionFailures);
				this.TotalProxyProtocolErrors = new ExPerformanceCounter(base.CategoryName, "Total Proxy Protocol Errors", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.TotalProxyProtocolErrors);
				this.TotalProxySocketErrors = new ExPerformanceCounter(base.CategoryName, "Total Proxy Socket Errors", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.TotalProxySocketErrors);
				this.TotalBytesProxied = new ExPerformanceCounter(base.CategoryName, "Total Bytes Proxied", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.TotalBytesProxied);
				ExPerformanceCounter exPerformanceCounter = new ExPerformanceCounter(base.CategoryName, "Messages Proxied/sec", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter);
				this.MessagesProxiedTotal = new ExPerformanceCounter(base.CategoryName, "Messages Proxied Total", instanceName, true, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter
				});
				list.Add(this.MessagesProxiedTotal);
				this.OutboundConnectionsCurrent = new ExPerformanceCounter(base.CategoryName, "Outbound Connections Current", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.OutboundConnectionsCurrent);
				ExPerformanceCounter exPerformanceCounter2 = new ExPerformanceCounter(base.CategoryName, "Outbound Connections Created/sec", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter2);
				this.OutboundConnectionsTotal = new ExPerformanceCounter(base.CategoryName, "Outbound Connections Total", instanceName, true, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter2
				});
				list.Add(this.OutboundConnectionsTotal);
				this.InboundConnectionsCurrent = new ExPerformanceCounter(base.CategoryName, "Inbound Connections Current", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.InboundConnectionsCurrent);
				long num = this.TotalProxyAttempts.RawValue;
				num += 1L;
				flag = true;
			}
			finally
			{
				if (!flag)
				{
					foreach (ExPerformanceCounter exPerformanceCounter3 in list)
					{
						exPerformanceCounter3.Close();
					}
				}
			}
			this.counters = list.ToArray();
		}

		// Token: 0x06003E80 RID: 16000 RVA: 0x0010A1D4 File Offset: 0x001083D4
		internal SmtpProxyPerfCountersInstance(string instanceName) : base(instanceName, "MSExchangeFrontEndTransport Smtp Blind Proxy")
		{
			bool flag = false;
			List<ExPerformanceCounter> list = new List<ExPerformanceCounter>();
			try
			{
				this.TotalProxyAttempts = new ExPerformanceCounter(base.CategoryName, "Total Proxy Attempts", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.TotalProxyAttempts);
				this.TotalSuccessfulProxySessions = new ExPerformanceCounter(base.CategoryName, "Total Successful Proxy Sessions", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.TotalSuccessfulProxySessions);
				this.PercentageProxySetupFailures = new ExPerformanceCounter(base.CategoryName, "Percentage Proxy Setup Failures", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.PercentageProxySetupFailures);
				this.TotalProxyUserLookupFailures = new ExPerformanceCounter(base.CategoryName, "Total Proxy User Lookup Failures", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.TotalProxyUserLookupFailures);
				this.TotalProxyBackEndLocatorFailures = new ExPerformanceCounter(base.CategoryName, "Total Proxy BackEndLocator Failures", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.TotalProxyBackEndLocatorFailures);
				this.TotalProxyDnsLookupFailures = new ExPerformanceCounter(base.CategoryName, "Total Proxy Dns Lookup Failures", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.TotalProxyDnsLookupFailures);
				this.TotalProxyConnectionFailures = new ExPerformanceCounter(base.CategoryName, "Total Proxy Connection Failures", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.TotalProxyConnectionFailures);
				this.TotalProxyProtocolErrors = new ExPerformanceCounter(base.CategoryName, "Total Proxy Protocol Errors", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.TotalProxyProtocolErrors);
				this.TotalProxySocketErrors = new ExPerformanceCounter(base.CategoryName, "Total Proxy Socket Errors", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.TotalProxySocketErrors);
				this.TotalBytesProxied = new ExPerformanceCounter(base.CategoryName, "Total Bytes Proxied", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.TotalBytesProxied);
				ExPerformanceCounter exPerformanceCounter = new ExPerformanceCounter(base.CategoryName, "Messages Proxied/sec", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter);
				this.MessagesProxiedTotal = new ExPerformanceCounter(base.CategoryName, "Messages Proxied Total", instanceName, true, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter
				});
				list.Add(this.MessagesProxiedTotal);
				this.OutboundConnectionsCurrent = new ExPerformanceCounter(base.CategoryName, "Outbound Connections Current", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.OutboundConnectionsCurrent);
				ExPerformanceCounter exPerformanceCounter2 = new ExPerformanceCounter(base.CategoryName, "Outbound Connections Created/sec", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter2);
				this.OutboundConnectionsTotal = new ExPerformanceCounter(base.CategoryName, "Outbound Connections Total", instanceName, true, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter2
				});
				list.Add(this.OutboundConnectionsTotal);
				this.InboundConnectionsCurrent = new ExPerformanceCounter(base.CategoryName, "Inbound Connections Current", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.InboundConnectionsCurrent);
				long num = this.TotalProxyAttempts.RawValue;
				num += 1L;
				flag = true;
			}
			finally
			{
				if (!flag)
				{
					foreach (ExPerformanceCounter exPerformanceCounter3 in list)
					{
						exPerformanceCounter3.Close();
					}
				}
			}
			this.counters = list.ToArray();
		}

		// Token: 0x06003E81 RID: 16001 RVA: 0x0010A538 File Offset: 0x00108738
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

		// Token: 0x0400228C RID: 8844
		public readonly ExPerformanceCounter TotalProxyAttempts;

		// Token: 0x0400228D RID: 8845
		public readonly ExPerformanceCounter TotalSuccessfulProxySessions;

		// Token: 0x0400228E RID: 8846
		public readonly ExPerformanceCounter PercentageProxySetupFailures;

		// Token: 0x0400228F RID: 8847
		public readonly ExPerformanceCounter TotalProxyUserLookupFailures;

		// Token: 0x04002290 RID: 8848
		public readonly ExPerformanceCounter TotalProxyBackEndLocatorFailures;

		// Token: 0x04002291 RID: 8849
		public readonly ExPerformanceCounter TotalProxyDnsLookupFailures;

		// Token: 0x04002292 RID: 8850
		public readonly ExPerformanceCounter TotalProxyConnectionFailures;

		// Token: 0x04002293 RID: 8851
		public readonly ExPerformanceCounter TotalProxyProtocolErrors;

		// Token: 0x04002294 RID: 8852
		public readonly ExPerformanceCounter TotalProxySocketErrors;

		// Token: 0x04002295 RID: 8853
		public readonly ExPerformanceCounter TotalBytesProxied;

		// Token: 0x04002296 RID: 8854
		public readonly ExPerformanceCounter MessagesProxiedTotal;

		// Token: 0x04002297 RID: 8855
		public readonly ExPerformanceCounter OutboundConnectionsCurrent;

		// Token: 0x04002298 RID: 8856
		public readonly ExPerformanceCounter OutboundConnectionsTotal;

		// Token: 0x04002299 RID: 8857
		public readonly ExPerformanceCounter InboundConnectionsCurrent;
	}
}
