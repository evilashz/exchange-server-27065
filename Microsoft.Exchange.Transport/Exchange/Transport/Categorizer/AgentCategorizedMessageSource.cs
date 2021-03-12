using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.Routing;
using Microsoft.Exchange.Data.Transport.Smtp;

namespace Microsoft.Exchange.Transport.Categorizer
{
	// Token: 0x020001FC RID: 508
	internal sealed class AgentCategorizedMessageSource : CategorizedMessageEventSource
	{
		// Token: 0x0600169D RID: 5789 RVA: 0x0005C436 File Offset: 0x0005A636
		public override void Defer(TimeSpan waitTime)
		{
			this.baseProxy.Defer(waitTime, null);
		}

		// Token: 0x0600169E RID: 5790 RVA: 0x0005C445 File Offset: 0x0005A645
		public override void Defer(TimeSpan waitTime, string trackingContext)
		{
			this.baseProxy.Defer(waitTime, trackingContext);
		}

		// Token: 0x0600169F RID: 5791 RVA: 0x0005C454 File Offset: 0x0005A654
		public override void Defer(TimeSpan waitTime, SmtpResponse deferReason)
		{
			this.baseProxy.Defer(waitTime, deferReason, null);
		}

		// Token: 0x060016A0 RID: 5792 RVA: 0x0005C464 File Offset: 0x0005A664
		public override void Delete()
		{
			this.baseProxy.Delete(null);
		}

		// Token: 0x060016A1 RID: 5793 RVA: 0x0005C472 File Offset: 0x0005A672
		public override void Delete(string trackingContext)
		{
			this.baseProxy.Delete(trackingContext);
		}

		// Token: 0x060016A2 RID: 5794 RVA: 0x0005C480 File Offset: 0x0005A680
		public override void Fork(IList<EnvelopeRecipient> recipients)
		{
			this.baseProxy.Fork(recipients);
		}

		// Token: 0x060016A3 RID: 5795 RVA: 0x0005C48E File Offset: 0x0005A68E
		public override void ExpandRecipients(IList<RecipientExpansionInfo> recipientExpansionInfo)
		{
			this.baseProxy.ExpandRecipients(recipientExpansionInfo);
		}

		// Token: 0x060016A4 RID: 5796 RVA: 0x0005C49C File Offset: 0x0005A69C
		internal override void TrackAgentInfo(string agentName, string groupName, List<KeyValuePair<string, string>> data)
		{
			this.baseProxy.TrackAgentInfo(agentName, groupName, data);
		}

		// Token: 0x060016A5 RID: 5797 RVA: 0x0005C4AC File Offset: 0x0005A6AC
		internal override void SetRiskLevel(RiskLevel level)
		{
			this.baseProxy.SetRiskLevel(level);
		}

		// Token: 0x060016A6 RID: 5798 RVA: 0x0005C4BA File Offset: 0x0005A6BA
		internal override RiskLevel GetRiskLevel()
		{
			return this.baseProxy.GetRiskLevel();
		}

		// Token: 0x060016A7 RID: 5799 RVA: 0x0005C4C7 File Offset: 0x0005A6C7
		internal override void SetOutboundIPPool(EnvelopeRecipient recipient, int pool)
		{
			this.baseProxy.SetOutboundIPPool(recipient, pool);
		}

		// Token: 0x060016A8 RID: 5800 RVA: 0x0005C4D6 File Offset: 0x0005A6D6
		internal override int GetOutboundIPPool(EnvelopeRecipient recipient)
		{
			return this.baseProxy.GetOutboundIPPool(recipient);
		}

		// Token: 0x060016A9 RID: 5801 RVA: 0x0005C4E4 File Offset: 0x0005A6E4
		internal override void SetComponentCost(string componentName, long cost)
		{
			this.baseProxy.SetComponentCost(componentName, cost);
		}

		// Token: 0x060016AA RID: 5802 RVA: 0x0005C4F3 File Offset: 0x0005A6F3
		internal override long GetComponentCost(string componentName)
		{
			return this.baseProxy.GetComponentCost(componentName);
		}

		// Token: 0x060016AB RID: 5803 RVA: 0x0005C501 File Offset: 0x0005A701
		internal void Initialize(InternalCategorizedMessageSource baseProxy)
		{
			this.baseProxy = baseProxy;
		}

		// Token: 0x04000B59 RID: 2905
		private InternalCategorizedMessageSource baseProxy;
	}
}
