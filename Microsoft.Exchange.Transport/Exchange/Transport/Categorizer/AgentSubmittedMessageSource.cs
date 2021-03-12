using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.Routing;
using Microsoft.Exchange.Data.Transport.Smtp;

namespace Microsoft.Exchange.Transport.Categorizer
{
	// Token: 0x02000200 RID: 512
	internal sealed class AgentSubmittedMessageSource : SubmittedMessageEventSource
	{
		// Token: 0x060016D6 RID: 5846 RVA: 0x0005C7CD File Offset: 0x0005A9CD
		public override void Defer(TimeSpan waitTime)
		{
			this.baseProxy.Defer(waitTime, null);
		}

		// Token: 0x060016D7 RID: 5847 RVA: 0x0005C7DC File Offset: 0x0005A9DC
		public override void Defer(TimeSpan waitTime, string trackingContext)
		{
			this.baseProxy.Defer(waitTime, trackingContext);
		}

		// Token: 0x060016D8 RID: 5848 RVA: 0x0005C7EB File Offset: 0x0005A9EB
		public override void Defer(TimeSpan waitTime, SmtpResponse deferReason)
		{
			this.baseProxy.Defer(waitTime, deferReason, null);
		}

		// Token: 0x060016D9 RID: 5849 RVA: 0x0005C7FB File Offset: 0x0005A9FB
		public override void Delete()
		{
			this.baseProxy.Delete(null);
		}

		// Token: 0x060016DA RID: 5850 RVA: 0x0005C809 File Offset: 0x0005AA09
		public override void Delete(string trackingContext)
		{
			this.baseProxy.Delete(trackingContext);
		}

		// Token: 0x060016DB RID: 5851 RVA: 0x0005C817 File Offset: 0x0005AA17
		public override void Fork(IList<EnvelopeRecipient> recipients)
		{
			this.baseProxy.Fork(recipients);
		}

		// Token: 0x060016DC RID: 5852 RVA: 0x0005C825 File Offset: 0x0005AA25
		public override void ExpandRecipients(IList<RecipientExpansionInfo> recipientExpansionInfo)
		{
			this.baseProxy.ExpandRecipients(recipientExpansionInfo);
		}

		// Token: 0x060016DD RID: 5853 RVA: 0x0005C833 File Offset: 0x0005AA33
		internal override void TrackAgentInfo(string agentName, string groupName, List<KeyValuePair<string, string>> data)
		{
			this.baseProxy.TrackAgentInfo(agentName, groupName, data);
		}

		// Token: 0x060016DE RID: 5854 RVA: 0x0005C843 File Offset: 0x0005AA43
		internal override void SetRiskLevel(RiskLevel level)
		{
			this.baseProxy.SetRiskLevel(level);
		}

		// Token: 0x060016DF RID: 5855 RVA: 0x0005C851 File Offset: 0x0005AA51
		internal override RiskLevel GetRiskLevel()
		{
			return this.baseProxy.GetRiskLevel();
		}

		// Token: 0x060016E0 RID: 5856 RVA: 0x0005C85E File Offset: 0x0005AA5E
		internal override void SetOutboundIPPool(EnvelopeRecipient recipient, int pool)
		{
			this.baseProxy.SetOutboundIPPool(recipient, pool);
		}

		// Token: 0x060016E1 RID: 5857 RVA: 0x0005C86D File Offset: 0x0005AA6D
		internal override int GetOutboundIPPool(EnvelopeRecipient recipient)
		{
			return this.baseProxy.GetOutboundIPPool(recipient);
		}

		// Token: 0x060016E2 RID: 5858 RVA: 0x0005C87B File Offset: 0x0005AA7B
		internal override void SetComponentCost(string componentName, long cost)
		{
			this.baseProxy.SetComponentCost(componentName, cost);
		}

		// Token: 0x060016E3 RID: 5859 RVA: 0x0005C88A File Offset: 0x0005AA8A
		internal override long GetComponentCost(string componentName)
		{
			return this.baseProxy.GetComponentCost(componentName);
		}

		// Token: 0x060016E4 RID: 5860 RVA: 0x0005C898 File Offset: 0x0005AA98
		internal void Initialize(InternalSubmittedMessageSource baseProxy)
		{
			this.baseProxy = baseProxy;
		}

		// Token: 0x04000B5D RID: 2909
		private InternalSubmittedMessageSource baseProxy;
	}
}
