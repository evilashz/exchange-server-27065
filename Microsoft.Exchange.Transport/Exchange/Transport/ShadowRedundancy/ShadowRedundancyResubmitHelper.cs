using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Transport;
using Microsoft.Exchange.Transport.RemoteDelivery;

namespace Microsoft.Exchange.Transport.ShadowRedundancy
{
	// Token: 0x0200038D RID: 909
	internal sealed class ShadowRedundancyResubmitHelper : ResubmitHelper
	{
		// Token: 0x060027E6 RID: 10214 RVA: 0x0009C7AD File Offset: 0x0009A9AD
		public ShadowRedundancyResubmitHelper(ResubmitReason resubmitReason, NextHopSolutionKey solutionKey) : base(resubmitReason, MessageTrackingSource.REDUNDANCY, ShadowRedundancyResubmitHelper.GetShadowResubmitSourceContext(solutionKey, resubmitReason), solutionKey, ExTraceGlobals.ShadowRedundancyTracer)
		{
			if (string.IsNullOrEmpty(solutionKey.NextHopDomain))
			{
				throw new ArgumentException("solutionKey.NextHopDomain cannot be empty", "solutionKey");
			}
			this.solutionKey = solutionKey;
		}

		// Token: 0x17000BF0 RID: 3056
		// (get) Token: 0x060027E7 RID: 10215 RVA: 0x0009C7EA File Offset: 0x0009A9EA
		private string PrimaryServerFqdn
		{
			get
			{
				return this.solutionKey.NextHopDomain;
			}
		}

		// Token: 0x060027E8 RID: 10216 RVA: 0x0009C7F7 File Offset: 0x0009A9F7
		protected override string GetComponentNameForReceivedHeader()
		{
			return "ShadowRedundancy";
		}

		// Token: 0x060027E9 RID: 10217 RVA: 0x0009C800 File Offset: 0x0009AA00
		protected override TransportMailItem CloneWithoutRecipients(TransportMailItem mailItem)
		{
			TransportMailItem transportMailItem = base.CloneWithoutRecipients(mailItem);
			transportMailItem.BumpExpirationTime();
			return transportMailItem;
		}

		// Token: 0x060027EA RID: 10218 RVA: 0x0009C81C File Offset: 0x0009AA1C
		protected override ResubmitHelper.RecipientAction DetermineRecipientAction(MailRecipient recipient)
		{
			if (string.IsNullOrEmpty(recipient.PrimaryServerFqdnGuid))
			{
				return ResubmitHelper.RecipientAction.None;
			}
			ShadowRedundancyManager.QualifiedPrimaryServerId qualifiedPrimaryServerId;
			if (!ShadowRedundancyManager.QualifiedPrimaryServerId.TryParse(recipient.PrimaryServerFqdnGuid, out qualifiedPrimaryServerId))
			{
				return ResubmitHelper.RecipientAction.None;
			}
			if (!string.Equals(qualifiedPrimaryServerId.Fqdn, this.PrimaryServerFqdn, StringComparison.OrdinalIgnoreCase))
			{
				return ResubmitHelper.RecipientAction.None;
			}
			return ResubmitHelper.RecipientAction.Copy;
		}

		// Token: 0x060027EB RID: 10219 RVA: 0x0009C861 File Offset: 0x0009AA61
		protected override void TrackLatency(TransportMailItem mailItemToResubmit)
		{
			mailItemToResubmit.LatencyTracker = LatencyTracker.CreateInstance(LatencyComponent.ShadowQueue);
			LatencyTracker.TrackPreProcessLatency(LatencyComponent.ShadowQueue, mailItemToResubmit.LatencyTracker, mailItemToResubmit.DateReceived);
		}

		// Token: 0x060027EC RID: 10220 RVA: 0x0009C883 File Offset: 0x0009AA83
		private static string GetShadowResubmitSourceContext(NextHopSolutionKey solutionKey, ResubmitReason reason)
		{
			return string.Format("{0} ({1})", solutionKey.NextHopDomain, reason);
		}

		// Token: 0x04001449 RID: 5193
		internal const string ComponentNameForReceivedHeader = "ShadowRedundancy";

		// Token: 0x0400144A RID: 5194
		private NextHopSolutionKey solutionKey;
	}
}
