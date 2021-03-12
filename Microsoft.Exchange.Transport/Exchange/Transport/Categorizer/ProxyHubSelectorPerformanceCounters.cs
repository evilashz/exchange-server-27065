using System;
using Microsoft.Exchange.Extensibility.Internal;

namespace Microsoft.Exchange.Transport.Categorizer
{
	// Token: 0x020001BF RID: 447
	internal class ProxyHubSelectorPerformanceCounters
	{
		// Token: 0x0600148C RID: 5260 RVA: 0x00052E77 File Offset: 0x00051077
		public ProxyHubSelectorPerformanceCounters(ProcessTransportRole role)
		{
			this.Initialize(role);
		}

		// Token: 0x0600148D RID: 5261 RVA: 0x00052E86 File Offset: 0x00051086
		public virtual void IncrementHubSelectionRequestsTotal()
		{
			if (this.instance != null)
			{
				this.instance.HubSelectionRequestsTotal.Increment();
			}
		}

		// Token: 0x0600148E RID: 5262 RVA: 0x00052EA1 File Offset: 0x000510A1
		public virtual void IncrementResolverFailures()
		{
			if (this.instance != null)
			{
				this.instance.HubSelectionResolverFailures.Increment();
			}
		}

		// Token: 0x0600148F RID: 5263 RVA: 0x00052EBC File Offset: 0x000510BC
		public virtual void IncrementOrganizationMailboxFailures()
		{
			if (this.instance != null)
			{
				this.instance.HubSelectionOrganizationMailboxFailures.Increment();
			}
		}

		// Token: 0x06001490 RID: 5264 RVA: 0x00052ED7 File Offset: 0x000510D7
		public virtual void IncrementMessagesWithoutMailboxRecipients()
		{
			if (this.instance != null)
			{
				this.instance.HubSelectionMessagesWithoutMailboxRecipients.Increment();
			}
		}

		// Token: 0x06001491 RID: 5265 RVA: 0x00052EF2 File Offset: 0x000510F2
		public virtual void IncrementMessagesWithoutOrganizationMailboxes()
		{
			if (this.instance != null)
			{
				this.instance.HubSelectionMessagesWithoutOrganizationMailboxes.Increment();
			}
		}

		// Token: 0x06001492 RID: 5266 RVA: 0x00052F0D File Offset: 0x0005110D
		public virtual void IncrementMessagesRoutedUsingDagSelector()
		{
			if (this.instance != null)
			{
				this.instance.HubSelectionMessagesRoutedUsingDagSelector.Increment();
			}
		}

		// Token: 0x06001493 RID: 5267 RVA: 0x00052F28 File Offset: 0x00051128
		public virtual void IncrementFallbackRoutingRequests()
		{
			if (this.instance != null)
			{
				this.instance.HubSelectionFallbackRoutingRequests.Increment();
			}
		}

		// Token: 0x06001494 RID: 5268 RVA: 0x00052F43 File Offset: 0x00051143
		public virtual void IncrementRoutingFailures()
		{
			if (this.instance != null)
			{
				this.instance.HubSelectionRoutingFailures.Increment();
			}
		}

		// Token: 0x06001495 RID: 5269 RVA: 0x00052F60 File Offset: 0x00051160
		protected virtual void Initialize(ProcessTransportRole role)
		{
			try
			{
				ProxyHubSelectorPerfCounters.SetCategoryName(ProxyHubSelectorPerformanceCounters.GetCategoryName(role));
				this.instance = ProxyHubSelectorPerfCounters.GetInstance("_total");
			}
			catch (InvalidOperationException arg)
			{
				ProxyHubSelectorComponent.Tracer.TraceError<InvalidOperationException>((long)this.GetHashCode(), "Failed to initialize performance counters: {0}", arg);
			}
		}

		// Token: 0x06001496 RID: 5270 RVA: 0x00052FB4 File Offset: 0x000511B4
		private static string GetCategoryName(ProcessTransportRole role)
		{
			switch (role)
			{
			case ProcessTransportRole.FrontEnd:
				return "MSExchangeFrontEndTransport ProxyHubSelector";
			case ProcessTransportRole.MailboxSubmission:
				return "MSExchange Submission ProxyHubSelector";
			case ProcessTransportRole.MailboxDelivery:
				return "MSExchange Delivery ProxyHubSelector";
			default:
				throw new NotSupportedException("Hub Selector perf counters are not supported on role " + role);
			}
		}

		// Token: 0x04000A68 RID: 2664
		private const string InstanceName = "_total";

		// Token: 0x04000A69 RID: 2665
		private ProxyHubSelectorPerfCountersInstance instance;
	}
}
