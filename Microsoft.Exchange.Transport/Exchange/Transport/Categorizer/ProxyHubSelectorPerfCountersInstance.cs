using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport.Categorizer
{
	// Token: 0x0200055C RID: 1372
	internal sealed class ProxyHubSelectorPerfCountersInstance : PerformanceCounterInstance
	{
		// Token: 0x06003F14 RID: 16148 RVA: 0x0010E6E0 File Offset: 0x0010C8E0
		internal ProxyHubSelectorPerfCountersInstance(string instanceName, ProxyHubSelectorPerfCountersInstance autoUpdateTotalInstance) : base(instanceName, ProxyHubSelectorPerfCounters.CategoryName)
		{
			bool flag = false;
			List<ExPerformanceCounter> list = new List<ExPerformanceCounter>();
			try
			{
				this.HubSelectionRequestsTotal = new ExPerformanceCounter(base.CategoryName, "Hub Selection Requests Total", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.HubSelectionRequestsTotal);
				this.HubSelectionResolverFailures = new ExPerformanceCounter(base.CategoryName, "Hub Selection Resolver Failures", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.HubSelectionResolverFailures);
				this.HubSelectionOrganizationMailboxFailures = new ExPerformanceCounter(base.CategoryName, "Hub Selection Organization Mailbox Failures", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.HubSelectionOrganizationMailboxFailures);
				this.HubSelectionMessagesWithoutMailboxRecipients = new ExPerformanceCounter(base.CategoryName, "Hub Selection Messages Without Mailbox Recipients", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.HubSelectionMessagesWithoutMailboxRecipients);
				this.HubSelectionMessagesWithoutOrganizationMailboxes = new ExPerformanceCounter(base.CategoryName, "Hub Selection Messages Without Organization Mailboxes", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.HubSelectionMessagesWithoutOrganizationMailboxes);
				this.HubSelectionMessagesRoutedUsingDagSelector = new ExPerformanceCounter(base.CategoryName, "Hub Selection Messages Routed Using Dag Selector", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.HubSelectionMessagesRoutedUsingDagSelector);
				this.HubSelectionFallbackRoutingRequests = new ExPerformanceCounter(base.CategoryName, "Hub Selection Fallback Routing Requests", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.HubSelectionFallbackRoutingRequests);
				this.HubSelectionRoutingFailures = new ExPerformanceCounter(base.CategoryName, "Hub Selection Routing Failures", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.HubSelectionRoutingFailures);
				long num = this.HubSelectionRequestsTotal.RawValue;
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

		// Token: 0x06003F15 RID: 16149 RVA: 0x0010E8E4 File Offset: 0x0010CAE4
		internal ProxyHubSelectorPerfCountersInstance(string instanceName) : base(instanceName, ProxyHubSelectorPerfCounters.CategoryName)
		{
			bool flag = false;
			List<ExPerformanceCounter> list = new List<ExPerformanceCounter>();
			try
			{
				this.HubSelectionRequestsTotal = new ExPerformanceCounter(base.CategoryName, "Hub Selection Requests Total", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.HubSelectionRequestsTotal);
				this.HubSelectionResolverFailures = new ExPerformanceCounter(base.CategoryName, "Hub Selection Resolver Failures", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.HubSelectionResolverFailures);
				this.HubSelectionOrganizationMailboxFailures = new ExPerformanceCounter(base.CategoryName, "Hub Selection Organization Mailbox Failures", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.HubSelectionOrganizationMailboxFailures);
				this.HubSelectionMessagesWithoutMailboxRecipients = new ExPerformanceCounter(base.CategoryName, "Hub Selection Messages Without Mailbox Recipients", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.HubSelectionMessagesWithoutMailboxRecipients);
				this.HubSelectionMessagesWithoutOrganizationMailboxes = new ExPerformanceCounter(base.CategoryName, "Hub Selection Messages Without Organization Mailboxes", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.HubSelectionMessagesWithoutOrganizationMailboxes);
				this.HubSelectionMessagesRoutedUsingDagSelector = new ExPerformanceCounter(base.CategoryName, "Hub Selection Messages Routed Using Dag Selector", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.HubSelectionMessagesRoutedUsingDagSelector);
				this.HubSelectionFallbackRoutingRequests = new ExPerformanceCounter(base.CategoryName, "Hub Selection Fallback Routing Requests", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.HubSelectionFallbackRoutingRequests);
				this.HubSelectionRoutingFailures = new ExPerformanceCounter(base.CategoryName, "Hub Selection Routing Failures", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.HubSelectionRoutingFailures);
				long num = this.HubSelectionRequestsTotal.RawValue;
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

		// Token: 0x06003F16 RID: 16150 RVA: 0x0010EAE8 File Offset: 0x0010CCE8
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

		// Token: 0x0400231F RID: 8991
		public readonly ExPerformanceCounter HubSelectionRequestsTotal;

		// Token: 0x04002320 RID: 8992
		public readonly ExPerformanceCounter HubSelectionResolverFailures;

		// Token: 0x04002321 RID: 8993
		public readonly ExPerformanceCounter HubSelectionOrganizationMailboxFailures;

		// Token: 0x04002322 RID: 8994
		public readonly ExPerformanceCounter HubSelectionMessagesWithoutMailboxRecipients;

		// Token: 0x04002323 RID: 8995
		public readonly ExPerformanceCounter HubSelectionMessagesWithoutOrganizationMailboxes;

		// Token: 0x04002324 RID: 8996
		public readonly ExPerformanceCounter HubSelectionMessagesRoutedUsingDagSelector;

		// Token: 0x04002325 RID: 8997
		public readonly ExPerformanceCounter HubSelectionFallbackRoutingRequests;

		// Token: 0x04002326 RID: 8998
		public readonly ExPerformanceCounter HubSelectionRoutingFailures;
	}
}
