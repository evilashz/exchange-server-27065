using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Transport.Logging.MessageTracking;

namespace Microsoft.Exchange.Transport.Categorizer
{
	// Token: 0x020001EC RID: 492
	internal class RecipSettingBifurcator : IMailBifurcationHelper<TemplateWithHistory>
	{
		// Token: 0x060015E2 RID: 5602 RVA: 0x00058F99 File Offset: 0x00057199
		public RecipSettingBifurcator(TransportMailItem mailItem)
		{
			this.mailItem = mailItem;
			this.message = new ResolverMessage(mailItem.Message, mailItem.MimeSize);
		}

		// Token: 0x060015E3 RID: 5603 RVA: 0x00058FC0 File Offset: 0x000571C0
		public bool GetBifurcationInfo(MailRecipient recipient, out TemplateWithHistory template)
		{
			template = null;
			if (!recipient.IsActive || recipient.Status == Status.Complete)
			{
				return false;
			}
			TemplateWithHistory templateWithHistory = TemplateWithHistory.ReadFrom(recipient);
			if (recipient.IsProcessed)
			{
				templateWithHistory.Template = templateWithHistory.Template.Derive(MessageTemplate.StripHistory);
			}
			templateWithHistory.Normalize(this.message);
			if (templateWithHistory.Equals(TemplateWithHistory.Default))
			{
				return false;
			}
			template = templateWithHistory;
			return true;
		}

		// Token: 0x060015E4 RID: 5604 RVA: 0x00059028 File Offset: 0x00057228
		public TransportMailItem GenerateNewMailItem(IList<MailRecipient> newMailItemRecipients, TemplateWithHistory bifurcationInfo)
		{
			TransportMailItem transportMailItem = this.mailItem.NewCloneWithoutRecipients();
			foreach (MailRecipient mailRecipient in newMailItemRecipients)
			{
				mailRecipient.MoveTo(transportMailItem);
			}
			bifurcationInfo.ApplyTo(transportMailItem);
			transportMailItem.CommitLazy();
			MessageTrackingLog.TrackTransfer(MessageTrackingSource.ROUTING, transportMailItem, this.mailItem.RecordId, "Resolver");
			if (Resolver.PerfCounters != null)
			{
				Resolver.PerfCounters.MessagesCreatedTotal.Increment();
			}
			return transportMailItem;
		}

		// Token: 0x060015E5 RID: 5605 RVA: 0x000590B8 File Offset: 0x000572B8
		public bool NeedsBifurcation()
		{
			return true;
		}

		// Token: 0x04000ADF RID: 2783
		private TransportMailItem mailItem;

		// Token: 0x04000AE0 RID: 2784
		private ResolverMessage message;
	}
}
