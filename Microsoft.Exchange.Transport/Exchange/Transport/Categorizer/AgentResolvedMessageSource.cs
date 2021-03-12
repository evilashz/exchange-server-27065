using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.Routing;
using Microsoft.Exchange.Data.Transport.Smtp;

namespace Microsoft.Exchange.Transport.Categorizer
{
	// Token: 0x020001FF RID: 511
	internal sealed class AgentResolvedMessageSource : ResolvedMessageEventSource
	{
		// Token: 0x060016BF RID: 5823 RVA: 0x0005C605 File Offset: 0x0005A805
		public override void Defer(TimeSpan waitTime)
		{
			this.baseProxy.Defer(waitTime, null);
		}

		// Token: 0x060016C0 RID: 5824 RVA: 0x0005C614 File Offset: 0x0005A814
		public override void Defer(TimeSpan waitTime, string trackingContext)
		{
			this.baseProxy.Defer(waitTime, trackingContext);
		}

		// Token: 0x060016C1 RID: 5825 RVA: 0x0005C623 File Offset: 0x0005A823
		public override void Defer(TimeSpan waitTime, SmtpResponse deferReason)
		{
			this.baseProxy.Defer(waitTime, deferReason, null);
		}

		// Token: 0x060016C2 RID: 5826 RVA: 0x0005C633 File Offset: 0x0005A833
		public override void Delete()
		{
			this.baseProxy.Delete(null);
		}

		// Token: 0x060016C3 RID: 5827 RVA: 0x0005C641 File Offset: 0x0005A841
		public override void Delete(string trackingContext)
		{
			this.baseProxy.Delete(trackingContext);
		}

		// Token: 0x060016C4 RID: 5828 RVA: 0x0005C64F File Offset: 0x0005A84F
		public override void Fork(IList<EnvelopeRecipient> recipients)
		{
			this.baseProxy.Fork(recipients);
		}

		// Token: 0x060016C5 RID: 5829 RVA: 0x0005C65D File Offset: 0x0005A85D
		public override void ExpandRecipients(IList<RecipientExpansionInfo> recipientExpansionInfo)
		{
			this.baseProxy.ExpandRecipients(recipientExpansionInfo);
		}

		// Token: 0x060016C6 RID: 5830 RVA: 0x0005C66C File Offset: 0x0005A86C
		public override RoutingOverride GetRoutingOverride(EnvelopeRecipient recipient)
		{
			MailRecipient mailRecipient = InternalQueuedMessageSource.RetrieveMailRecipient(recipient);
			return mailRecipient.RoutingOverride;
		}

		// Token: 0x060016C7 RID: 5831 RVA: 0x0005C686 File Offset: 0x0005A886
		public override void SetRoutingOverride(EnvelopeRecipient recipient, RoutingOverride routingOverride)
		{
			this.SetRoutingOverride(recipient, routingOverride, null);
		}

		// Token: 0x060016C8 RID: 5832 RVA: 0x0005C694 File Offset: 0x0005A894
		internal override void SetRoutingOverride(EnvelopeRecipient recipient, RoutingOverride routingOverride, string overrideSource)
		{
			if (overrideSource != null && !overrideSource.StartsWith("Connector"))
			{
				throw new ArgumentException(string.Format("Invalid override source. Should begin with {0}", "Connector"));
			}
			MailRecipient mailRecipient = InternalQueuedMessageSource.RetrieveMailRecipient(recipient);
			mailRecipient.SetRoutingOverride(routingOverride, this.baseProxy.MexSession.ExecutingAgentName, overrideSource);
		}

		// Token: 0x060016C9 RID: 5833 RVA: 0x0005C6E8 File Offset: 0x0005A8E8
		public override string GetTlsDomain(EnvelopeRecipient recipient)
		{
			MailRecipient mailRecipient = InternalQueuedMessageSource.RetrieveMailRecipient(recipient);
			return mailRecipient.GetTlsDomain();
		}

		// Token: 0x060016CA RID: 5834 RVA: 0x0005C704 File Offset: 0x0005A904
		public override void SetTlsDomain(EnvelopeRecipient recipient, string domain)
		{
			MailRecipient mailRecipient = InternalQueuedMessageSource.RetrieveMailRecipient(recipient);
			mailRecipient.SetTlsDomain(domain);
		}

		// Token: 0x060016CB RID: 5835 RVA: 0x0005C720 File Offset: 0x0005A920
		internal override RequiredTlsAuthLevel? GetTlsAuthLevel(EnvelopeRecipient recipient)
		{
			MailRecipient mailRecipient = InternalQueuedMessageSource.RetrieveMailRecipient(recipient);
			return mailRecipient.TlsAuthLevel;
		}

		// Token: 0x060016CC RID: 5836 RVA: 0x0005C73C File Offset: 0x0005A93C
		internal override void SetTlsAuthLevel(EnvelopeRecipient recipient, RequiredTlsAuthLevel? tlsAuthLevel)
		{
			MailRecipient mailRecipient = InternalQueuedMessageSource.RetrieveMailRecipient(recipient);
			mailRecipient.TlsAuthLevel = tlsAuthLevel;
		}

		// Token: 0x060016CD RID: 5837 RVA: 0x0005C757 File Offset: 0x0005A957
		internal override void TrackAgentInfo(string agentName, string groupName, List<KeyValuePair<string, string>> data)
		{
			this.baseProxy.TrackAgentInfo(agentName, groupName, data);
		}

		// Token: 0x060016CE RID: 5838 RVA: 0x0005C767 File Offset: 0x0005A967
		internal override void SetRiskLevel(RiskLevel level)
		{
			this.baseProxy.SetRiskLevel(level);
		}

		// Token: 0x060016CF RID: 5839 RVA: 0x0005C775 File Offset: 0x0005A975
		internal override RiskLevel GetRiskLevel()
		{
			return this.baseProxy.GetRiskLevel();
		}

		// Token: 0x060016D0 RID: 5840 RVA: 0x0005C782 File Offset: 0x0005A982
		internal override void SetOutboundIPPool(EnvelopeRecipient recipient, int pool)
		{
			this.baseProxy.SetOutboundIPPool(recipient, pool);
		}

		// Token: 0x060016D1 RID: 5841 RVA: 0x0005C791 File Offset: 0x0005A991
		internal override int GetOutboundIPPool(EnvelopeRecipient recipient)
		{
			return this.baseProxy.GetOutboundIPPool(recipient);
		}

		// Token: 0x060016D2 RID: 5842 RVA: 0x0005C79F File Offset: 0x0005A99F
		internal override void SetComponentCost(string componentName, long cost)
		{
			this.baseProxy.SetComponentCost(componentName, cost);
		}

		// Token: 0x060016D3 RID: 5843 RVA: 0x0005C7AE File Offset: 0x0005A9AE
		internal override long GetComponentCost(string componentName)
		{
			return this.baseProxy.GetComponentCost(componentName);
		}

		// Token: 0x060016D4 RID: 5844 RVA: 0x0005C7BC File Offset: 0x0005A9BC
		internal void Initialize(InternalResolvedMessageSource baseProxy)
		{
			this.baseProxy = baseProxy;
		}

		// Token: 0x04000B5C RID: 2908
		private InternalResolvedMessageSource baseProxy;
	}
}
