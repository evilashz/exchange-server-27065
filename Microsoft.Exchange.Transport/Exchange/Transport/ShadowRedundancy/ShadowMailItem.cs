using System;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Transport.Logging.MessageTracking;

namespace Microsoft.Exchange.Transport.ShadowRedundancy
{
	// Token: 0x02000373 RID: 883
	internal sealed class ShadowMailItem : IQueueItem
	{
		// Token: 0x06002630 RID: 9776 RVA: 0x00094654 File Offset: 0x00092854
		public ShadowMailItem(TransportMailItem mailItem, NextHopSolution nextHopSolution, DateTime creationTime, IShadowRedundancyConfigurationSource configuration)
		{
			if (mailItem == null)
			{
				throw new ArgumentNullException("mailItem");
			}
			if (configuration == null)
			{
				throw new ArgumentNullException("configuration");
			}
			this.creationTime = creationTime;
			this.mailItem = mailItem;
			this.nextHopSolution = nextHopSolution;
			this.configuration = configuration;
		}

		// Token: 0x17000B6F RID: 2927
		// (get) Token: 0x06002631 RID: 9777 RVA: 0x000946A1 File Offset: 0x000928A1
		public TransportMailItem TransportMailItem
		{
			get
			{
				return this.mailItem;
			}
		}

		// Token: 0x17000B70 RID: 2928
		// (get) Token: 0x06002632 RID: 9778 RVA: 0x000946A9 File Offset: 0x000928A9
		public NextHopSolution NextHopSolution
		{
			get
			{
				return this.nextHopSolution;
			}
		}

		// Token: 0x17000B71 RID: 2929
		// (get) Token: 0x06002633 RID: 9779 RVA: 0x000946B1 File Offset: 0x000928B1
		public DiscardReason? DiscardReason
		{
			get
			{
				return this.discardReason;
			}
		}

		// Token: 0x17000B72 RID: 2930
		// (get) Token: 0x06002634 RID: 9780 RVA: 0x000946B9 File Offset: 0x000928B9
		// (set) Token: 0x06002635 RID: 9781 RVA: 0x000946C6 File Offset: 0x000928C6
		DateTime IQueueItem.DeferUntil
		{
			get
			{
				return this.NextHopSolution.DeferUntil;
			}
			set
			{
				throw new NotSupportedException("DeferUntil not supported on ShadowItems.");
			}
		}

		// Token: 0x17000B73 RID: 2931
		// (get) Token: 0x06002636 RID: 9782 RVA: 0x000946D4 File Offset: 0x000928D4
		DateTime IQueueItem.Expiry
		{
			get
			{
				AdminActionStatus adminActionStatus = this.nextHopSolution.AdminActionStatus;
				if (adminActionStatus == AdminActionStatus.PendingDeleteWithNDR || adminActionStatus == AdminActionStatus.PendingDeleteWithOutNDR)
				{
					return DateTime.MinValue;
				}
				return this.creationTime + this.configuration.ShadowMessageAutoDiscardInterval;
			}
		}

		// Token: 0x17000B74 RID: 2932
		// (get) Token: 0x06002637 RID: 9783 RVA: 0x00094711 File Offset: 0x00092911
		// (set) Token: 0x06002638 RID: 9784 RVA: 0x00094714 File Offset: 0x00092914
		DeliveryPriority IQueueItem.Priority
		{
			get
			{
				return DeliveryPriority.Normal;
			}
			set
			{
				if (value != DeliveryPriority.Normal)
				{
					throw new NotSupportedException("Priorities not supported for Shadow Messages.");
				}
			}
		}

		// Token: 0x06002639 RID: 9785 RVA: 0x00094725 File Offset: 0x00092925
		void IQueueItem.Update()
		{
		}

		// Token: 0x0600263A RID: 9786 RVA: 0x00094727 File Offset: 0x00092927
		MessageContext IQueueItem.GetMessageContext(MessageProcessingSource source)
		{
			return new MessageContext(this.TransportMailItem.RecordId, this.TransportMailItem.InternetMessageId, source);
		}

		// Token: 0x0600263B RID: 9787 RVA: 0x00094748 File Offset: 0x00092948
		public bool Discard(DiscardReason discardReason)
		{
			if (this.discardReason != null)
			{
				return false;
			}
			this.discardReason = new DiscardReason?(discardReason);
			this.creationTime = DateTime.MinValue;
			MessageTrackingLog.TrackHighAvailabilityDiscard(MessageTrackingSource.SMTP, this.TransportMailItem, this.discardReason.ToString());
			return true;
		}

		// Token: 0x040013A3 RID: 5027
		private TransportMailItem mailItem;

		// Token: 0x040013A4 RID: 5028
		private NextHopSolution nextHopSolution;

		// Token: 0x040013A5 RID: 5029
		private DiscardReason? discardReason;

		// Token: 0x040013A6 RID: 5030
		private DateTime creationTime;

		// Token: 0x040013A7 RID: 5031
		private IShadowRedundancyConfigurationSource configuration;
	}
}
