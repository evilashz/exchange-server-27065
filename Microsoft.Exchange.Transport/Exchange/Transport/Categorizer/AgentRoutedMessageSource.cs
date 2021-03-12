using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.Routing;
using Microsoft.Exchange.Data.Transport.Smtp;

namespace Microsoft.Exchange.Transport.Categorizer
{
	// Token: 0x020001FE RID: 510
	internal sealed class AgentRoutedMessageSource : RoutedMessageEventSource
	{
		// Token: 0x060016AF RID: 5807 RVA: 0x0005C529 File Offset: 0x0005A729
		public override void Defer(TimeSpan waitTime)
		{
			this.baseProxy.Defer(waitTime, null);
		}

		// Token: 0x060016B0 RID: 5808 RVA: 0x0005C538 File Offset: 0x0005A738
		public override void Defer(TimeSpan waitTime, string trackingContext)
		{
			this.baseProxy.Defer(waitTime, trackingContext);
		}

		// Token: 0x060016B1 RID: 5809 RVA: 0x0005C547 File Offset: 0x0005A747
		public override void Defer(TimeSpan waitTime, SmtpResponse deferReason)
		{
			this.baseProxy.Defer(waitTime, deferReason, null);
		}

		// Token: 0x060016B2 RID: 5810 RVA: 0x0005C557 File Offset: 0x0005A757
		public override void Delete()
		{
			this.baseProxy.Delete(null);
		}

		// Token: 0x060016B3 RID: 5811 RVA: 0x0005C565 File Offset: 0x0005A765
		public override void Delete(string trackingContext)
		{
			this.baseProxy.Delete(trackingContext);
		}

		// Token: 0x060016B4 RID: 5812 RVA: 0x0005C573 File Offset: 0x0005A773
		public override void Fork(IList<EnvelopeRecipient> recipients)
		{
			this.baseProxy.Fork(recipients);
		}

		// Token: 0x060016B5 RID: 5813 RVA: 0x0005C581 File Offset: 0x0005A781
		public override void ExpandRecipients(IList<RecipientExpansionInfo> recipientExpansionInfo)
		{
			this.baseProxy.ExpandRecipients(recipientExpansionInfo);
		}

		// Token: 0x060016B6 RID: 5814 RVA: 0x0005C58F File Offset: 0x0005A78F
		internal override void TrackAgentInfo(string agentName, string groupName, List<KeyValuePair<string, string>> data)
		{
			this.baseProxy.TrackAgentInfo(agentName, groupName, data);
		}

		// Token: 0x060016B7 RID: 5815 RVA: 0x0005C59F File Offset: 0x0005A79F
		internal override void SetRiskLevel(RiskLevel level)
		{
			this.baseProxy.SetRiskLevel(level);
		}

		// Token: 0x060016B8 RID: 5816 RVA: 0x0005C5AD File Offset: 0x0005A7AD
		internal override RiskLevel GetRiskLevel()
		{
			return this.baseProxy.GetRiskLevel();
		}

		// Token: 0x060016B9 RID: 5817 RVA: 0x0005C5BA File Offset: 0x0005A7BA
		internal override void SetOutboundIPPool(EnvelopeRecipient recipient, int pool)
		{
			this.baseProxy.SetOutboundIPPool(recipient, pool);
		}

		// Token: 0x060016BA RID: 5818 RVA: 0x0005C5C9 File Offset: 0x0005A7C9
		internal override int GetOutboundIPPool(EnvelopeRecipient recipient)
		{
			return this.baseProxy.GetOutboundIPPool(recipient);
		}

		// Token: 0x060016BB RID: 5819 RVA: 0x0005C5D7 File Offset: 0x0005A7D7
		internal override void SetComponentCost(string componentName, long cost)
		{
			this.baseProxy.SetComponentCost(componentName, cost);
		}

		// Token: 0x060016BC RID: 5820 RVA: 0x0005C5E6 File Offset: 0x0005A7E6
		internal override long GetComponentCost(string componentName)
		{
			return this.baseProxy.GetComponentCost(componentName);
		}

		// Token: 0x060016BD RID: 5821 RVA: 0x0005C5F4 File Offset: 0x0005A7F4
		internal void Initialize(InternalRoutedMessageSource baseProxy)
		{
			this.baseProxy = baseProxy;
		}

		// Token: 0x04000B5B RID: 2907
		private InternalRoutedMessageSource baseProxy;
	}
}
