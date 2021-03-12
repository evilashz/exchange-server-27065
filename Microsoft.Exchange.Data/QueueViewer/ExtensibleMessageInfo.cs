using System;
using System.Net;
using Microsoft.Exchange.Data.Transport;

namespace Microsoft.Exchange.Data.QueueViewer
{
	// Token: 0x0200027D RID: 637
	[Serializable]
	public abstract class ExtensibleMessageInfo : ConfigurableObject, PagedDataObject, IConfigurable
	{
		// Token: 0x060015BC RID: 5564 RVA: 0x00044D95 File Offset: 0x00042F95
		internal ExtensibleMessageInfo(long identity, QueueIdentity queueIdentity, MessageInfoPropertyBag propertyBag) : base(propertyBag)
		{
			this[this.propertyBag.ObjectIdentityPropertyDefinition] = new MessageIdentity(identity, queueIdentity);
		}

		// Token: 0x060015BD RID: 5565 RVA: 0x00044DB6 File Offset: 0x00042FB6
		internal ExtensibleMessageInfo(MessageInfoPropertyBag emptyBag) : base(emptyBag)
		{
		}

		// Token: 0x060015BE RID: 5566 RVA: 0x00044DC0 File Offset: 0x00042FC0
		public void Reset(long identity, QueueIdentity queueIdentity)
		{
			this.propertyBag = new MessageInfoPropertyBag();
			this[this.propertyBag.ObjectIdentityPropertyDefinition] = new MessageIdentity(identity, queueIdentity);
			this.Subject = null;
			this.InternetMessageId = null;
			this.FromAddress = null;
			this.Status = MessageStatus.None;
			this.Size = default(ByteQuantifiedSize);
			this.MessageSourceName = null;
			this.SourceIP = null;
			this.SCL = 0;
			this.DateReceived = default(DateTime);
			this.ExpirationTime = null;
			this.LastError = null;
			this.LastErrorCode = 0;
			this.RetryCount = 0;
			this.Recipients = null;
			this.ComponentLatency = null;
			this.MessageLatency = default(EnhancedTimeSpan);
			this.DeferReason = null;
			this.LockReason = null;
			this.IsProbeMessage = false;
			this.OutboundIPPool = 0;
			this.Directionality = MailDirectionality.Undefined;
			this.ExternalDirectoryOrganizationId = default(Guid);
			this.OriginalFromAddress = null;
			this.AccountForest = null;
		}

		// Token: 0x17000664 RID: 1636
		// (get) Token: 0x060015BF RID: 5567 RVA: 0x00044EC1 File Offset: 0x000430C1
		internal override ObjectSchema ObjectSchema
		{
			get
			{
				return ExtensibleMessageInfo.schema;
			}
		}

		// Token: 0x060015C0 RID: 5568 RVA: 0x00044EC8 File Offset: 0x000430C8
		public void ConvertDatesToLocalTime()
		{
			this.DateReceived = this.DateReceived.ToLocalTime();
			if (this.ExpirationTime != null)
			{
				this.ExpirationTime = new DateTime?(this.ExpirationTime.Value.ToLocalTime());
			}
		}

		// Token: 0x060015C1 RID: 5569 RVA: 0x00044F1C File Offset: 0x0004311C
		public void ConvertDatesToUniversalTime()
		{
			this.DateReceived = this.DateReceived.ToUniversalTime();
			if (this.ExpirationTime != null)
			{
				this.ExpirationTime = new DateTime?(this.ExpirationTime.Value.ToUniversalTime());
			}
		}

		// Token: 0x17000665 RID: 1637
		// (get) Token: 0x060015C2 RID: 5570 RVA: 0x00044F6E File Offset: 0x0004316E
		public MessageIdentity MessageIdentity
		{
			get
			{
				return (MessageIdentity)this.Identity;
			}
		}

		// Token: 0x17000666 RID: 1638
		// (get) Token: 0x060015C3 RID: 5571 RVA: 0x00044F7B File Offset: 0x0004317B
		public QueueIdentity Queue
		{
			get
			{
				return this.MessageIdentity.QueueIdentity;
			}
		}

		// Token: 0x17000667 RID: 1639
		// (get) Token: 0x060015C4 RID: 5572
		// (set) Token: 0x060015C5 RID: 5573
		public abstract string Subject { get; internal set; }

		// Token: 0x17000668 RID: 1640
		// (get) Token: 0x060015C6 RID: 5574
		// (set) Token: 0x060015C7 RID: 5575
		public abstract string InternetMessageId { get; internal set; }

		// Token: 0x17000669 RID: 1641
		// (get) Token: 0x060015C8 RID: 5576
		// (set) Token: 0x060015C9 RID: 5577
		public abstract string FromAddress { get; internal set; }

		// Token: 0x1700066A RID: 1642
		// (get) Token: 0x060015CA RID: 5578
		// (set) Token: 0x060015CB RID: 5579
		public abstract MessageStatus Status { get; internal set; }

		// Token: 0x1700066B RID: 1643
		// (get) Token: 0x060015CC RID: 5580
		// (set) Token: 0x060015CD RID: 5581
		public abstract ByteQuantifiedSize Size { get; internal set; }

		// Token: 0x1700066C RID: 1644
		// (get) Token: 0x060015CE RID: 5582
		// (set) Token: 0x060015CF RID: 5583
		public abstract string MessageSourceName { get; internal set; }

		// Token: 0x1700066D RID: 1645
		// (get) Token: 0x060015D0 RID: 5584
		// (set) Token: 0x060015D1 RID: 5585
		public abstract IPAddress SourceIP { get; internal set; }

		// Token: 0x1700066E RID: 1646
		// (get) Token: 0x060015D2 RID: 5586
		// (set) Token: 0x060015D3 RID: 5587
		public abstract int SCL { get; internal set; }

		// Token: 0x1700066F RID: 1647
		// (get) Token: 0x060015D4 RID: 5588
		// (set) Token: 0x060015D5 RID: 5589
		public abstract DateTime DateReceived { get; internal set; }

		// Token: 0x17000670 RID: 1648
		// (get) Token: 0x060015D6 RID: 5590
		// (set) Token: 0x060015D7 RID: 5591
		public abstract DateTime? ExpirationTime { get; internal set; }

		// Token: 0x17000671 RID: 1649
		// (get) Token: 0x060015D8 RID: 5592
		// (set) Token: 0x060015D9 RID: 5593
		internal abstract int LastErrorCode { get; set; }

		// Token: 0x17000672 RID: 1650
		// (get) Token: 0x060015DA RID: 5594
		// (set) Token: 0x060015DB RID: 5595
		public abstract string LastError { get; internal set; }

		// Token: 0x17000673 RID: 1651
		// (get) Token: 0x060015DC RID: 5596
		// (set) Token: 0x060015DD RID: 5597
		public abstract int RetryCount { get; internal set; }

		// Token: 0x17000674 RID: 1652
		// (get) Token: 0x060015DE RID: 5598
		// (set) Token: 0x060015DF RID: 5599
		public abstract RecipientInfo[] Recipients { get; internal set; }

		// Token: 0x17000675 RID: 1653
		// (get) Token: 0x060015E0 RID: 5600
		// (set) Token: 0x060015E1 RID: 5601
		public abstract ComponentLatencyInfo[] ComponentLatency { get; internal set; }

		// Token: 0x17000676 RID: 1654
		// (get) Token: 0x060015E2 RID: 5602
		// (set) Token: 0x060015E3 RID: 5603
		public abstract EnhancedTimeSpan MessageLatency { get; internal set; }

		// Token: 0x17000677 RID: 1655
		// (get) Token: 0x060015E4 RID: 5604
		// (set) Token: 0x060015E5 RID: 5605
		public abstract string DeferReason { get; internal set; }

		// Token: 0x17000678 RID: 1656
		// (get) Token: 0x060015E6 RID: 5606
		// (set) Token: 0x060015E7 RID: 5607
		public abstract string LockReason { get; internal set; }

		// Token: 0x17000679 RID: 1657
		// (get) Token: 0x060015E8 RID: 5608
		// (set) Token: 0x060015E9 RID: 5609
		public abstract string Priority { get; internal set; }

		// Token: 0x1700067A RID: 1658
		// (get) Token: 0x060015EA RID: 5610
		// (set) Token: 0x060015EB RID: 5611
		internal abstract bool IsProbeMessage { get; set; }

		// Token: 0x1700067B RID: 1659
		// (get) Token: 0x060015EC RID: 5612
		// (set) Token: 0x060015ED RID: 5613
		public abstract Guid ExternalDirectoryOrganizationId { get; internal set; }

		// Token: 0x1700067C RID: 1660
		// (get) Token: 0x060015EE RID: 5614
		// (set) Token: 0x060015EF RID: 5615
		public abstract MailDirectionality Directionality { get; internal set; }

		// Token: 0x1700067D RID: 1661
		// (get) Token: 0x060015F0 RID: 5616
		// (set) Token: 0x060015F1 RID: 5617
		public abstract string OriginalFromAddress { get; internal set; }

		// Token: 0x1700067E RID: 1662
		// (get) Token: 0x060015F2 RID: 5618
		// (set) Token: 0x060015F3 RID: 5619
		public abstract string AccountForest { get; internal set; }

		// Token: 0x1700067F RID: 1663
		// (get) Token: 0x060015F4 RID: 5620
		// (set) Token: 0x060015F5 RID: 5621
		internal abstract int OutboundIPPool { get; set; }

		// Token: 0x04000CC1 RID: 3265
		private static ExtensibleMessageInfoSchema schema = ObjectSchema.GetInstance<ExtensibleMessageInfoSchema>();
	}
}
