using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Protocols.Smtp
{
	// Token: 0x02000543 RID: 1347
	internal sealed class SmtpReceivePerfCountersInstance : PerformanceCounterInstance
	{
		// Token: 0x06003E71 RID: 15985 RVA: 0x00108F34 File Offset: 0x00107134
		internal SmtpReceivePerfCountersInstance(string instanceName, SmtpReceivePerfCountersInstance autoUpdateTotalInstance) : base(instanceName, SmtpReceivePerfCounters.CategoryName)
		{
			bool flag = false;
			List<ExPerformanceCounter> list = new List<ExPerformanceCounter>();
			try
			{
				this.ConnectionsCurrent = new ExPerformanceCounter(base.CategoryName, "Connections Current", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.ConnectionsCurrent, new ExPerformanceCounter[0]);
				list.Add(this.ConnectionsCurrent);
				ExPerformanceCounter exPerformanceCounter = new ExPerformanceCounter(base.CategoryName, "Connections Created/sec", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter);
				ExPerformanceCounter exPerformanceCounter2 = new ExPerformanceCounter(base.CategoryName, "TLS Connections Created/sec", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter2);
				this.TlsConnectionsCurrent = new ExPerformanceCounter(base.CategoryName, "TLS Connections Current", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.TlsConnectionsCurrent, new ExPerformanceCounter[]
				{
					exPerformanceCounter2
				});
				list.Add(this.TlsConnectionsCurrent);
				this.TlsNegotiationsFailed = new ExPerformanceCounter(base.CategoryName, "TLS Negotiations Failed", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.TlsNegotiationsFailed, new ExPerformanceCounter[0]);
				list.Add(this.TlsNegotiationsFailed);
				this.TlsConnectionsRejectedDueToRateExceeded = new ExPerformanceCounter(base.CategoryName, "TLS Connections Rejected Due To Rate Exceeded", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.TlsConnectionsRejectedDueToRateExceeded, new ExPerformanceCounter[0]);
				list.Add(this.TlsConnectionsRejectedDueToRateExceeded);
				ExPerformanceCounter exPerformanceCounter3 = new ExPerformanceCounter(base.CategoryName, "Disconnections by Agents/second", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter3);
				this.ConnectionsDroppedByAgentsTotal = new ExPerformanceCounter(base.CategoryName, "Disconnections By Agents", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.ConnectionsDroppedByAgentsTotal, new ExPerformanceCounter[]
				{
					exPerformanceCounter3
				});
				list.Add(this.ConnectionsDroppedByAgentsTotal);
				ExPerformanceCounter exPerformanceCounter4 = new ExPerformanceCounter(base.CategoryName, "Messages Received/sec", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter4);
				this.MessagesReceivedForNonProvisionedUsers = new ExPerformanceCounter(base.CategoryName, "Messages Received For Non-Provisioned users", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.MessagesReceivedForNonProvisionedUsers, new ExPerformanceCounter[0]);
				list.Add(this.MessagesReceivedForNonProvisionedUsers);
				ExPerformanceCounter exPerformanceCounter5 = new ExPerformanceCounter(base.CategoryName, "Message Bytes Received/sec", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter5);
				this.MessagesRefusedForSize = new ExPerformanceCounter(base.CategoryName, "Messages Refused for Size", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.MessagesRefusedForSize, new ExPerformanceCounter[0]);
				list.Add(this.MessagesRefusedForSize);
				this.MessagesReceivedWithBareLinefeeds = new ExPerformanceCounter(base.CategoryName, "Messages Received Containing Bare Linefeeds in the SMTP DATA Stream", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.MessagesReceivedWithBareLinefeeds, new ExPerformanceCounter[0]);
				list.Add(this.MessagesReceivedWithBareLinefeeds);
				this.MessagesRefusedForBareLinefeeds = new ExPerformanceCounter(base.CategoryName, "Messages Rejected During SMTP DATA Due to Bare Linefeeds", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.MessagesRefusedForBareLinefeeds, new ExPerformanceCounter[0]);
				list.Add(this.MessagesRefusedForBareLinefeeds);
				ExPerformanceCounter exPerformanceCounter6 = new ExPerformanceCounter(base.CategoryName, "Average bytes/message", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter6);
				this.MessageBytesReceivedTotal = new ExPerformanceCounter(base.CategoryName, "Message Bytes Received Total", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.MessageBytesReceivedTotal, new ExPerformanceCounter[]
				{
					exPerformanceCounter5,
					exPerformanceCounter6
				});
				list.Add(this.MessageBytesReceivedTotal);
				ExPerformanceCounter exPerformanceCounter7 = new ExPerformanceCounter(base.CategoryName, "Average bytes/message Base", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter7);
				ExPerformanceCounter exPerformanceCounter8 = new ExPerformanceCounter(base.CategoryName, "Average recipients/message", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter8);
				this.RecipientsAccepted = new ExPerformanceCounter(base.CategoryName, "Recipients accepted Total", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.RecipientsAccepted, new ExPerformanceCounter[]
				{
					exPerformanceCounter8
				});
				list.Add(this.RecipientsAccepted);
				ExPerformanceCounter exPerformanceCounter9 = new ExPerformanceCounter(base.CategoryName, "Average recipients/message base", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter9);
				ExPerformanceCounter exPerformanceCounter10 = new ExPerformanceCounter(base.CategoryName, "Average bytes/connection", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter10);
				ExPerformanceCounter exPerformanceCounter11 = new ExPerformanceCounter(base.CategoryName, "Average bytes/connection base", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter11);
				ExPerformanceCounter exPerformanceCounter12 = new ExPerformanceCounter(base.CategoryName, "Average messages/connection", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter12);
				this.MessagesReceivedTotal = new ExPerformanceCounter(base.CategoryName, "Messages Received Total", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.MessagesReceivedTotal, new ExPerformanceCounter[]
				{
					exPerformanceCounter4,
					exPerformanceCounter7,
					exPerformanceCounter9,
					exPerformanceCounter12
				});
				list.Add(this.MessagesReceivedTotal);
				ExPerformanceCounter exPerformanceCounter13 = new ExPerformanceCounter(base.CategoryName, "Average messages/connection base", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter13);
				this.ConnectionsTotal = new ExPerformanceCounter(base.CategoryName, "Connections Total", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.ConnectionsTotal, new ExPerformanceCounter[]
				{
					exPerformanceCounter,
					exPerformanceCounter11,
					exPerformanceCounter13
				});
				list.Add(this.ConnectionsTotal);
				ExPerformanceCounter exPerformanceCounter14 = new ExPerformanceCounter(base.CategoryName, "Bytes Received/sec", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter14);
				this.TotalBytesReceived = new ExPerformanceCounter(base.CategoryName, "Bytes Received Total", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.TotalBytesReceived, new ExPerformanceCounter[]
				{
					exPerformanceCounter10,
					exPerformanceCounter14
				});
				list.Add(this.TotalBytesReceived);
				ExPerformanceCounter exPerformanceCounter15 = new ExPerformanceCounter(base.CategoryName, "Tarpitting Delays Authenticated/sec", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter15);
				this.TarpittingDelaysAuthenticated = new ExPerformanceCounter(base.CategoryName, "Tarpitting Delays Authenticated", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.TarpittingDelaysAuthenticated, new ExPerformanceCounter[]
				{
					exPerformanceCounter15
				});
				list.Add(this.TarpittingDelaysAuthenticated);
				ExPerformanceCounter exPerformanceCounter16 = new ExPerformanceCounter(base.CategoryName, "Tarpitting Delays Anonymous/sec", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter16);
				this.TarpittingDelaysAnonymous = new ExPerformanceCounter(base.CategoryName, "Tarpitting Delays Anonymous", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.TarpittingDelaysAnonymous, new ExPerformanceCounter[]
				{
					exPerformanceCounter16
				});
				list.Add(this.TarpittingDelaysAnonymous);
				ExPerformanceCounter exPerformanceCounter17 = new ExPerformanceCounter(base.CategoryName, "Tarpitting Delays Backpressure/sec", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter17);
				this.TarpittingDelaysBackpressure = new ExPerformanceCounter(base.CategoryName, "Tarpitting Delays Backpressure", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.TarpittingDelaysBackpressure, new ExPerformanceCounter[]
				{
					exPerformanceCounter17
				});
				list.Add(this.TarpittingDelaysBackpressure);
				long num = this.ConnectionsCurrent.RawValue;
				num += 1L;
				flag = true;
			}
			finally
			{
				if (!flag)
				{
					foreach (ExPerformanceCounter exPerformanceCounter18 in list)
					{
						exPerformanceCounter18.Close();
					}
				}
			}
			this.counters = list.ToArray();
		}

		// Token: 0x06003E72 RID: 15986 RVA: 0x00109688 File Offset: 0x00107888
		internal SmtpReceivePerfCountersInstance(string instanceName) : base(instanceName, SmtpReceivePerfCounters.CategoryName)
		{
			bool flag = false;
			List<ExPerformanceCounter> list = new List<ExPerformanceCounter>();
			try
			{
				this.ConnectionsCurrent = new ExPerformanceCounter(base.CategoryName, "Connections Current", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.ConnectionsCurrent);
				ExPerformanceCounter exPerformanceCounter = new ExPerformanceCounter(base.CategoryName, "Connections Created/sec", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter);
				ExPerformanceCounter exPerformanceCounter2 = new ExPerformanceCounter(base.CategoryName, "TLS Connections Created/sec", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter2);
				this.TlsConnectionsCurrent = new ExPerformanceCounter(base.CategoryName, "TLS Connections Current", instanceName, true, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter2
				});
				list.Add(this.TlsConnectionsCurrent);
				this.TlsNegotiationsFailed = new ExPerformanceCounter(base.CategoryName, "TLS Negotiations Failed", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.TlsNegotiationsFailed);
				this.TlsConnectionsRejectedDueToRateExceeded = new ExPerformanceCounter(base.CategoryName, "TLS Connections Rejected Due To Rate Exceeded", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.TlsConnectionsRejectedDueToRateExceeded);
				ExPerformanceCounter exPerformanceCounter3 = new ExPerformanceCounter(base.CategoryName, "Disconnections by Agents/second", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter3);
				this.ConnectionsDroppedByAgentsTotal = new ExPerformanceCounter(base.CategoryName, "Disconnections By Agents", instanceName, true, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter3
				});
				list.Add(this.ConnectionsDroppedByAgentsTotal);
				ExPerformanceCounter exPerformanceCounter4 = new ExPerformanceCounter(base.CategoryName, "Messages Received/sec", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter4);
				this.MessagesReceivedForNonProvisionedUsers = new ExPerformanceCounter(base.CategoryName, "Messages Received For Non-Provisioned users", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.MessagesReceivedForNonProvisionedUsers);
				ExPerformanceCounter exPerformanceCounter5 = new ExPerformanceCounter(base.CategoryName, "Message Bytes Received/sec", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter5);
				this.MessagesRefusedForSize = new ExPerformanceCounter(base.CategoryName, "Messages Refused for Size", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.MessagesRefusedForSize);
				this.MessagesReceivedWithBareLinefeeds = new ExPerformanceCounter(base.CategoryName, "Messages Received Containing Bare Linefeeds in the SMTP DATA Stream", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.MessagesReceivedWithBareLinefeeds);
				this.MessagesRefusedForBareLinefeeds = new ExPerformanceCounter(base.CategoryName, "Messages Rejected During SMTP DATA Due to Bare Linefeeds", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.MessagesRefusedForBareLinefeeds);
				ExPerformanceCounter exPerformanceCounter6 = new ExPerformanceCounter(base.CategoryName, "Average bytes/message", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter6);
				this.MessageBytesReceivedTotal = new ExPerformanceCounter(base.CategoryName, "Message Bytes Received Total", instanceName, true, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter5,
					exPerformanceCounter6
				});
				list.Add(this.MessageBytesReceivedTotal);
				ExPerformanceCounter exPerformanceCounter7 = new ExPerformanceCounter(base.CategoryName, "Average bytes/message Base", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter7);
				ExPerformanceCounter exPerformanceCounter8 = new ExPerformanceCounter(base.CategoryName, "Average recipients/message", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter8);
				this.RecipientsAccepted = new ExPerformanceCounter(base.CategoryName, "Recipients accepted Total", instanceName, true, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter8
				});
				list.Add(this.RecipientsAccepted);
				ExPerformanceCounter exPerformanceCounter9 = new ExPerformanceCounter(base.CategoryName, "Average recipients/message base", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter9);
				ExPerformanceCounter exPerformanceCounter10 = new ExPerformanceCounter(base.CategoryName, "Average bytes/connection", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter10);
				ExPerformanceCounter exPerformanceCounter11 = new ExPerformanceCounter(base.CategoryName, "Average bytes/connection base", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter11);
				ExPerformanceCounter exPerformanceCounter12 = new ExPerformanceCounter(base.CategoryName, "Average messages/connection", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter12);
				this.MessagesReceivedTotal = new ExPerformanceCounter(base.CategoryName, "Messages Received Total", instanceName, true, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter4,
					exPerformanceCounter7,
					exPerformanceCounter9,
					exPerformanceCounter12
				});
				list.Add(this.MessagesReceivedTotal);
				ExPerformanceCounter exPerformanceCounter13 = new ExPerformanceCounter(base.CategoryName, "Average messages/connection base", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter13);
				this.ConnectionsTotal = new ExPerformanceCounter(base.CategoryName, "Connections Total", instanceName, true, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter,
					exPerformanceCounter11,
					exPerformanceCounter13
				});
				list.Add(this.ConnectionsTotal);
				ExPerformanceCounter exPerformanceCounter14 = new ExPerformanceCounter(base.CategoryName, "Bytes Received/sec", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter14);
				this.TotalBytesReceived = new ExPerformanceCounter(base.CategoryName, "Bytes Received Total", instanceName, true, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter10,
					exPerformanceCounter14
				});
				list.Add(this.TotalBytesReceived);
				ExPerformanceCounter exPerformanceCounter15 = new ExPerformanceCounter(base.CategoryName, "Tarpitting Delays Authenticated/sec", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter15);
				this.TarpittingDelaysAuthenticated = new ExPerformanceCounter(base.CategoryName, "Tarpitting Delays Authenticated", instanceName, true, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter15
				});
				list.Add(this.TarpittingDelaysAuthenticated);
				ExPerformanceCounter exPerformanceCounter16 = new ExPerformanceCounter(base.CategoryName, "Tarpitting Delays Anonymous/sec", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter16);
				this.TarpittingDelaysAnonymous = new ExPerformanceCounter(base.CategoryName, "Tarpitting Delays Anonymous", instanceName, true, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter16
				});
				list.Add(this.TarpittingDelaysAnonymous);
				ExPerformanceCounter exPerformanceCounter17 = new ExPerformanceCounter(base.CategoryName, "Tarpitting Delays Backpressure/sec", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter17);
				this.TarpittingDelaysBackpressure = new ExPerformanceCounter(base.CategoryName, "Tarpitting Delays Backpressure", instanceName, true, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter17
				});
				list.Add(this.TarpittingDelaysBackpressure);
				long num = this.ConnectionsCurrent.RawValue;
				num += 1L;
				flag = true;
			}
			finally
			{
				if (!flag)
				{
					foreach (ExPerformanceCounter exPerformanceCounter18 in list)
					{
						exPerformanceCounter18.Close();
					}
				}
			}
			this.counters = list.ToArray();
		}

		// Token: 0x06003E73 RID: 15987 RVA: 0x00109D20 File Offset: 0x00107F20
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

		// Token: 0x04002279 RID: 8825
		public readonly ExPerformanceCounter ConnectionsCurrent;

		// Token: 0x0400227A RID: 8826
		public readonly ExPerformanceCounter ConnectionsTotal;

		// Token: 0x0400227B RID: 8827
		public readonly ExPerformanceCounter TlsConnectionsCurrent;

		// Token: 0x0400227C RID: 8828
		public readonly ExPerformanceCounter TlsNegotiationsFailed;

		// Token: 0x0400227D RID: 8829
		public readonly ExPerformanceCounter TlsConnectionsRejectedDueToRateExceeded;

		// Token: 0x0400227E RID: 8830
		public readonly ExPerformanceCounter ConnectionsDroppedByAgentsTotal;

		// Token: 0x0400227F RID: 8831
		public readonly ExPerformanceCounter MessagesReceivedTotal;

		// Token: 0x04002280 RID: 8832
		public readonly ExPerformanceCounter MessagesReceivedForNonProvisionedUsers;

		// Token: 0x04002281 RID: 8833
		public readonly ExPerformanceCounter MessageBytesReceivedTotal;

		// Token: 0x04002282 RID: 8834
		public readonly ExPerformanceCounter MessagesRefusedForSize;

		// Token: 0x04002283 RID: 8835
		public readonly ExPerformanceCounter MessagesReceivedWithBareLinefeeds;

		// Token: 0x04002284 RID: 8836
		public readonly ExPerformanceCounter MessagesRefusedForBareLinefeeds;

		// Token: 0x04002285 RID: 8837
		public readonly ExPerformanceCounter RecipientsAccepted;

		// Token: 0x04002286 RID: 8838
		public readonly ExPerformanceCounter TotalBytesReceived;

		// Token: 0x04002287 RID: 8839
		public readonly ExPerformanceCounter TarpittingDelaysAuthenticated;

		// Token: 0x04002288 RID: 8840
		public readonly ExPerformanceCounter TarpittingDelaysAnonymous;

		// Token: 0x04002289 RID: 8841
		public readonly ExPerformanceCounter TarpittingDelaysBackpressure;
	}
}
