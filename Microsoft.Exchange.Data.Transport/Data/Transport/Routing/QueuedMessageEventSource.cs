using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Transport.Smtp;

namespace Microsoft.Exchange.Data.Transport.Routing
{
	// Token: 0x02000088 RID: 136
	public abstract class QueuedMessageEventSource
	{
		// Token: 0x06000324 RID: 804 RVA: 0x000080EA File Offset: 0x000062EA
		internal QueuedMessageEventSource()
		{
		}

		// Token: 0x06000325 RID: 805
		public abstract void Defer(TimeSpan waitTime);

		// Token: 0x06000326 RID: 806
		public abstract void Defer(TimeSpan waitTime, string trackingContext);

		// Token: 0x06000327 RID: 807
		public abstract void Defer(TimeSpan waitTime, SmtpResponse deferReason);

		// Token: 0x06000328 RID: 808
		public abstract void Delete();

		// Token: 0x06000329 RID: 809
		public abstract void Delete(string trackingContext);

		// Token: 0x0600032A RID: 810
		public abstract void Fork(IList<EnvelopeRecipient> recipients);

		// Token: 0x0600032B RID: 811
		public abstract void ExpandRecipients(IList<RecipientExpansionInfo> recipientExpansionInfo);

		// Token: 0x0600032C RID: 812
		internal abstract void TrackAgentInfo(string agentName, string groupName, List<KeyValuePair<string, string>> data);

		// Token: 0x0600032D RID: 813
		internal abstract void SetRiskLevel(RiskLevel level);

		// Token: 0x0600032E RID: 814
		internal abstract RiskLevel GetRiskLevel();

		// Token: 0x0600032F RID: 815
		internal abstract void SetOutboundIPPool(EnvelopeRecipient recipient, int offset);

		// Token: 0x06000330 RID: 816
		internal abstract int GetOutboundIPPool(EnvelopeRecipient recipient);

		// Token: 0x06000331 RID: 817
		internal abstract void SetComponentCost(string componentName, long cost);

		// Token: 0x06000332 RID: 818
		internal abstract long GetComponentCost(string componentName);
	}
}
