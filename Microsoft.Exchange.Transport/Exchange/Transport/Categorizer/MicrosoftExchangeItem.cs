using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Transport;
using Microsoft.Exchange.Transport.Logging.MessageTracking;

namespace Microsoft.Exchange.Transport.Categorizer
{
	// Token: 0x020001E2 RID: 482
	internal class MicrosoftExchangeItem : ForwardableItem
	{
		// Token: 0x060015BE RID: 5566 RVA: 0x000580F0 File Offset: 0x000562F0
		public MicrosoftExchangeItem(MailRecipient recipient) : base(recipient)
		{
		}

		// Token: 0x060015BF RID: 5567 RVA: 0x000580FC File Offset: 0x000562FC
		public override void Allow(Expansion expansion)
		{
			if (base.ForwardingAddress == null || base.DeliverToMailboxAndForward)
			{
				ExTraceGlobals.ResolverTracer.TraceDebug<RoutingAddress>((long)this.GetHashCode(), "Suppressing 'Microsoft Exchange' recipient '{0}'.", base.Recipient.Email);
				base.Recipient.Ack(AckStatus.SuccessNoDsn, AckReason.MicrosoftExchangeRecipientSuppressed);
				MsgTrackExpandInfo msgTrackInfo = new MsgTrackExpandInfo(base.Recipient.Email, null, base.Recipient.SmtpResponse.ToString());
				MessageTrackingLog.TrackExpand<MailRecipient>(MessageTrackingSource.ROUTING, expansion.MailItem, msgTrackInfo, new List<MailRecipient>());
				return;
			}
			ExTraceGlobals.ResolverTracer.TraceDebug<RoutingAddress>((long)this.GetHashCode(), "Forwarding to alt recipient for 'Microsoft Exchange' recipient '{0}'.", base.Recipient.Email);
			base.Allow(expansion);
		}
	}
}
