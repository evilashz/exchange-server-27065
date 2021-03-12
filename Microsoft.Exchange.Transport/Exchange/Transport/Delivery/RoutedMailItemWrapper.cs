using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.Email;
using Microsoft.Exchange.Protocols.Smtp;

namespace Microsoft.Exchange.Transport.Delivery
{
	// Token: 0x020003CE RID: 974
	internal class RoutedMailItemWrapper : DeliverableMailItem
	{
		// Token: 0x06002C8E RID: 11406 RVA: 0x000B16E4 File Offset: 0x000AF8E4
		public RoutedMailItemWrapper(RoutedMailItem routedMailItem, IList<MailRecipient> recipientsToDeliver)
		{
			this.mailItem = routedMailItem;
			this.recipients = new ReadOnlyMailRecipientCollectionWrapper(recipientsToDeliver, routedMailItem);
		}

		// Token: 0x17000D7B RID: 3451
		// (get) Token: 0x06002C8F RID: 11407 RVA: 0x000B1700 File Offset: 0x000AF900
		public override string OriginalAuthenticator
		{
			get
			{
				this.ThrowIfClosed();
				return this.mailItem.Auth;
			}
		}

		// Token: 0x17000D7C RID: 3452
		// (get) Token: 0x06002C90 RID: 11408 RVA: 0x000B1713 File Offset: 0x000AF913
		public override DateTime DateTimeReceived
		{
			get
			{
				this.ThrowIfClosed();
				return this.mailItem.DateReceived;
			}
		}

		// Token: 0x17000D7D RID: 3453
		// (get) Token: 0x06002C91 RID: 11409 RVA: 0x000B1726 File Offset: 0x000AF926
		public override string EnvelopeId
		{
			get
			{
				this.ThrowIfClosed();
				return this.mailItem.EnvId;
			}
		}

		// Token: 0x17000D7E RID: 3454
		// (get) Token: 0x06002C92 RID: 11410 RVA: 0x000B1739 File Offset: 0x000AF939
		public override RoutingAddress FromAddress
		{
			get
			{
				this.ThrowIfClosed();
				return this.mailItem.From;
			}
		}

		// Token: 0x17000D7F RID: 3455
		// (get) Token: 0x06002C93 RID: 11411 RVA: 0x000B174C File Offset: 0x000AF94C
		public override string OriginatingDomain
		{
			get
			{
				this.ThrowIfClosed();
				return this.mailItem.HeloDomain;
			}
		}

		// Token: 0x17000D80 RID: 3456
		// (get) Token: 0x06002C94 RID: 11412 RVA: 0x000B175F File Offset: 0x000AF95F
		public override EmailMessage Message
		{
			get
			{
				this.ThrowIfClosed();
				return this.mailItem.Message;
			}
		}

		// Token: 0x17000D81 RID: 3457
		// (get) Token: 0x06002C95 RID: 11413 RVA: 0x000B1772 File Offset: 0x000AF972
		public override long MimeStreamLength
		{
			get
			{
				this.ThrowIfClosed();
				return this.mailItem.MimeSize;
			}
		}

		// Token: 0x17000D82 RID: 3458
		// (get) Token: 0x06002C96 RID: 11414 RVA: 0x000B1785 File Offset: 0x000AF985
		public override ReadOnlyEnvelopeRecipientCollection Recipients
		{
			get
			{
				this.ThrowIfClosed();
				return this.recipients;
			}
		}

		// Token: 0x17000D83 RID: 3459
		// (get) Token: 0x06002C97 RID: 11415 RVA: 0x000B1793 File Offset: 0x000AF993
		public override DsnFormatRequested DsnFormatRequested
		{
			get
			{
				this.ThrowIfClosed();
				return EnumConverter.InternalToPublic(this.mailItem.DsnFormat);
			}
		}

		// Token: 0x17000D84 RID: 3460
		// (get) Token: 0x06002C98 RID: 11416 RVA: 0x000B17AB File Offset: 0x000AF9AB
		public override DeliveryPriority DeliveryPriority
		{
			get
			{
				this.ThrowIfClosed();
				return this.mailItem.Priority;
			}
		}

		// Token: 0x17000D85 RID: 3461
		// (get) Token: 0x06002C99 RID: 11417 RVA: 0x000B17BE File Offset: 0x000AF9BE
		public string PrioritizationReason
		{
			get
			{
				return this.mailItem.PrioritizationReason;
			}
		}

		// Token: 0x17000D86 RID: 3462
		// (get) Token: 0x06002C9A RID: 11418 RVA: 0x000B17CB File Offset: 0x000AF9CB
		public override DeliveryMethod InboundDeliveryMethod
		{
			get
			{
				this.ThrowIfClosed();
				return TransportMailItemWrapper.GetInboundDeliveryMethod(this.mailItem);
			}
		}

		// Token: 0x17000D87 RID: 3463
		// (get) Token: 0x06002C9B RID: 11419 RVA: 0x000B17DE File Offset: 0x000AF9DE
		public override bool MustDeliver
		{
			get
			{
				this.ThrowIfClosed();
				return this.mailItem.RetryDeliveryIfRejected;
			}
		}

		// Token: 0x17000D88 RID: 3464
		// (get) Token: 0x06002C9C RID: 11420 RVA: 0x000B17F1 File Offset: 0x000AF9F1
		internal override object RecipientCache
		{
			get
			{
				this.ThrowIfClosed();
				return this.mailItem.ADRecipientCache;
			}
		}

		// Token: 0x17000D89 RID: 3465
		// (get) Token: 0x06002C9D RID: 11421 RVA: 0x000B1804 File Offset: 0x000AFA04
		public override Guid TenantId
		{
			get
			{
				this.ThrowIfClosed();
				return TransportMailItemWrapper.GetTenantId(this.mailItem);
			}
		}

		// Token: 0x17000D8A RID: 3466
		// (get) Token: 0x06002C9E RID: 11422 RVA: 0x000B1817 File Offset: 0x000AFA17
		internal override Guid SystemProbeId
		{
			get
			{
				this.ThrowIfClosed();
				return this.mailItem.SystemProbeId;
			}
		}

		// Token: 0x17000D8B RID: 3467
		// (get) Token: 0x06002C9F RID: 11423 RVA: 0x000B182A File Offset: 0x000AFA2A
		internal RoutedMailItem RoutedMailItem
		{
			get
			{
				this.ThrowIfClosed();
				return this.mailItem;
			}
		}

		// Token: 0x06002CA0 RID: 11424 RVA: 0x000B1838 File Offset: 0x000AFA38
		public override Stream GetMimeReadStream()
		{
			this.ThrowIfClosed();
			return TransportMailItemWrapper.GetMimeReadStream(this.mailItem, ref this.openedReadStreams);
		}

		// Token: 0x06002CA1 RID: 11425 RVA: 0x000B1851 File Offset: 0x000AFA51
		public void Close()
		{
			TransportMailItemWrapper.CloseStreams(ref this.openedReadStreams);
			this.mailItem = null;
			this.recipients = null;
		}

		// Token: 0x06002CA2 RID: 11426 RVA: 0x000B186C File Offset: 0x000AFA6C
		public override bool TryGetListProperty<ItemT>(string name, out ReadOnlyCollection<ItemT> value)
		{
			this.ThrowIfClosed();
			return this.mailItem.ExtendedProperties.TryGetListValue<ItemT>(name, out value);
		}

		// Token: 0x06002CA3 RID: 11427 RVA: 0x000B1886 File Offset: 0x000AFA86
		public override bool TryGetProperty<T>(string name, out T value)
		{
			this.ThrowIfClosed();
			return this.mailItem.ExtendedProperties.TryGetValue<T>(name, out value);
		}

		// Token: 0x06002CA4 RID: 11428 RVA: 0x000B18A0 File Offset: 0x000AFAA0
		private void ThrowIfClosed()
		{
			if (this.mailItem == null || this.recipients == null)
			{
				throw new InvalidOperationException("The mail item is no longer available.");
			}
		}

		// Token: 0x04001646 RID: 5702
		private RoutedMailItem mailItem;

		// Token: 0x04001647 RID: 5703
		private ReadOnlyMailRecipientCollectionWrapper recipients;

		// Token: 0x04001648 RID: 5704
		private List<Stream> openedReadStreams;
	}
}
