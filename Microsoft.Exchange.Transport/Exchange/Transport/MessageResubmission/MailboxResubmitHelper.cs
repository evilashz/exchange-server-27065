using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Mime;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Transport;
using Microsoft.Exchange.Extensibility.Internal;
using Microsoft.Exchange.Transport.RemoteDelivery;

namespace Microsoft.Exchange.Transport.MessageResubmission
{
	// Token: 0x02000136 RID: 310
	internal class MailboxResubmitHelper : ResubmitHelper
	{
		// Token: 0x06000D9C RID: 3484 RVA: 0x00031570 File Offset: 0x0002F770
		public MailboxResubmitHelper(string sourceContext) : base(ResubmitReason.Recovery, MessageTrackingSource.SAFETYNET, sourceContext, NextHopSolutionKey.Empty, ExTraceGlobals.MessageResubmissionTracer)
		{
		}

		// Token: 0x06000D9D RID: 3485 RVA: 0x00031586 File Offset: 0x0002F786
		protected override bool IsDeleted(MailRecipient recipient)
		{
			return false;
		}

		// Token: 0x06000D9E RID: 3486 RVA: 0x00031589 File Offset: 0x0002F789
		protected override string GetComponentNameForReceivedHeader()
		{
			return "MailboxResubmission";
		}

		// Token: 0x06000D9F RID: 3487 RVA: 0x00031590 File Offset: 0x0002F790
		protected override TransportMailItem CloneWithoutRecipients(TransportMailItem mailItem)
		{
			TransportMailItem transportMailItem = base.CloneWithoutRecipients(mailItem);
			if (Components.TransportAppConfig.Dumpster.AllowDuplicateDelivery)
			{
				DateHeader dateHeader = transportMailItem.RootPart.Headers.FindFirst(HeaderId.Date) as DateHeader;
				if (dateHeader != null)
				{
					DateTime dateTime = dateHeader.DateTime.AddMinutes(1.0);
					dateHeader.DateTime = dateTime;
				}
			}
			return transportMailItem;
		}

		// Token: 0x06000DA0 RID: 3488 RVA: 0x000315F0 File Offset: 0x0002F7F0
		protected override ResubmitHelper.RecipientAction DetermineRecipientAction(MailRecipient recipient)
		{
			return ResubmitHelper.RecipientAction.Copy;
		}

		// Token: 0x06000DA1 RID: 3489 RVA: 0x000315F3 File Offset: 0x0002F7F3
		protected override void ProcessRecipient(MailRecipient recipient)
		{
			base.ProcessRecipient(recipient);
			recipient.DsnNeeded &= ~DsnFlags.Delay;
			if (recipient.DsnRequested != DsnRequestedFlags.Default)
			{
				recipient.DsnRequested &= ~DsnRequestedFlags.Delay;
				return;
			}
			recipient.DsnRequested = DsnRequestedFlags.Failure;
		}

		// Token: 0x040005D8 RID: 1496
		internal const string ComponentNameForReceivedHeader = "MailboxResubmission";
	}
}
