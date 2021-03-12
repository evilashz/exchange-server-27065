using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.Email;
using Microsoft.Exchange.Protocols.Smtp;
using Microsoft.Exchange.Transport;

namespace Microsoft.Exchange.MailboxTransport.StoreDriverDelivery
{
	// Token: 0x02000046 RID: 70
	internal class MbxMailItemWrapper : DeliverableMailItem
	{
		// Token: 0x06000317 RID: 791 RVA: 0x0000D568 File Offset: 0x0000B768
		public MbxMailItemWrapper(MbxTransportMailItem mbxMailItem)
		{
			this.mailItem = mbxMailItem;
			this.recipients = new ReadOnlyMailRecipientCollectionWrapper(this.mailItem.GetRecipients(), mbxMailItem);
		}

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x06000318 RID: 792 RVA: 0x0000D58E File Offset: 0x0000B78E
		public override string OriginalAuthenticator
		{
			get
			{
				this.ThrowIfClosed();
				return this.mailItem.Auth;
			}
		}

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x06000319 RID: 793 RVA: 0x0000D5A1 File Offset: 0x0000B7A1
		public override DateTime DateTimeReceived
		{
			get
			{
				this.ThrowIfClosed();
				return this.mailItem.DateReceived;
			}
		}

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x0600031A RID: 794 RVA: 0x0000D5B4 File Offset: 0x0000B7B4
		public override string EnvelopeId
		{
			get
			{
				this.ThrowIfClosed();
				return this.mailItem.EnvId;
			}
		}

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x0600031B RID: 795 RVA: 0x0000D5C7 File Offset: 0x0000B7C7
		public override RoutingAddress FromAddress
		{
			get
			{
				this.ThrowIfClosed();
				return this.mailItem.From;
			}
		}

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x0600031C RID: 796 RVA: 0x0000D5DA File Offset: 0x0000B7DA
		public override string OriginatingDomain
		{
			get
			{
				this.ThrowIfClosed();
				return this.mailItem.HeloDomain;
			}
		}

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x0600031D RID: 797 RVA: 0x0000D5ED File Offset: 0x0000B7ED
		public override EmailMessage Message
		{
			get
			{
				this.ThrowIfClosed();
				return this.mailItem.Message;
			}
		}

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x0600031E RID: 798 RVA: 0x0000D600 File Offset: 0x0000B800
		public override long MimeStreamLength
		{
			get
			{
				this.ThrowIfClosed();
				return this.mailItem.MimeSize;
			}
		}

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x0600031F RID: 799 RVA: 0x0000D613 File Offset: 0x0000B813
		public override ReadOnlyEnvelopeRecipientCollection Recipients
		{
			get
			{
				this.ThrowIfClosed();
				return this.recipients;
			}
		}

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x06000320 RID: 800 RVA: 0x0000D621 File Offset: 0x0000B821
		public override DsnFormatRequested DsnFormatRequested
		{
			get
			{
				this.ThrowIfClosed();
				return EnumConverter.InternalToPublic(this.mailItem.DsnFormat);
			}
		}

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x06000321 RID: 801 RVA: 0x0000D639 File Offset: 0x0000B839
		public override DeliveryPriority DeliveryPriority
		{
			get
			{
				return this.mailItem.Priority;
			}
		}

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x06000322 RID: 802 RVA: 0x0000D646 File Offset: 0x0000B846
		public string PrioritizationReason
		{
			get
			{
				return this.mailItem.PrioritizationReason;
			}
		}

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x06000323 RID: 803 RVA: 0x0000D653 File Offset: 0x0000B853
		public override DeliveryMethod InboundDeliveryMethod
		{
			get
			{
				this.ThrowIfClosed();
				return TransportMailItemWrapper.GetInboundDeliveryMethod(this.mailItem);
			}
		}

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x06000324 RID: 804 RVA: 0x0000D666 File Offset: 0x0000B866
		public override bool MustDeliver
		{
			get
			{
				this.ThrowIfClosed();
				return this.mailItem.RetryDeliveryIfRejected;
			}
		}

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x06000325 RID: 805 RVA: 0x0000D679 File Offset: 0x0000B879
		public override Guid TenantId
		{
			get
			{
				this.ThrowIfClosed();
				return TransportMailItemWrapper.GetTenantId(this.mailItem);
			}
		}

		// Token: 0x06000326 RID: 806 RVA: 0x0000D68C File Offset: 0x0000B88C
		public void AddAgentInfo(string agentName, string eventName, List<KeyValuePair<string, string>> data)
		{
			this.mailItem.AddAgentInfo(agentName, eventName, data);
		}

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x06000327 RID: 807 RVA: 0x0000D69C File Offset: 0x0000B89C
		internal override Guid SystemProbeId
		{
			get
			{
				this.ThrowIfClosed();
				return this.mailItem.SystemProbeId;
			}
		}

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x06000328 RID: 808 RVA: 0x0000D6AF File Offset: 0x0000B8AF
		internal override object RecipientCache
		{
			get
			{
				this.ThrowIfClosed();
				return this.mailItem.ADRecipientCache;
			}
		}

		// Token: 0x06000329 RID: 809 RVA: 0x0000D6C2 File Offset: 0x0000B8C2
		public override Stream GetMimeReadStream()
		{
			this.ThrowIfClosed();
			return TransportMailItemWrapper.GetMimeReadStream(this.mailItem, ref this.openedReadStreams);
		}

		// Token: 0x0600032A RID: 810 RVA: 0x0000D6DB File Offset: 0x0000B8DB
		public void Close()
		{
			TransportMailItemWrapper.CloseStreams(ref this.openedReadStreams);
			this.mailItem = null;
			this.recipients = null;
		}

		// Token: 0x0600032B RID: 811 RVA: 0x0000D6F6 File Offset: 0x0000B8F6
		public override bool TryGetListProperty<ItemT>(string name, out ReadOnlyCollection<ItemT> value)
		{
			this.ThrowIfClosed();
			return this.mailItem.ExtendedProperties.TryGetListValue<ItemT>(name, out value);
		}

		// Token: 0x0600032C RID: 812 RVA: 0x0000D710 File Offset: 0x0000B910
		public override bool TryGetProperty<T>(string name, out T value)
		{
			this.ThrowIfClosed();
			return this.mailItem.ExtendedProperties.TryGetValue<T>(name, out value);
		}

		// Token: 0x0600032D RID: 813 RVA: 0x0000D72A File Offset: 0x0000B92A
		private void ThrowIfClosed()
		{
			if (this.mailItem == null || this.recipients == null)
			{
				throw new InvalidOperationException("The mail item is no longer available.");
			}
		}

		// Token: 0x04000148 RID: 328
		private MbxTransportMailItem mailItem;

		// Token: 0x04000149 RID: 329
		private ReadOnlyMailRecipientCollectionWrapper recipients;

		// Token: 0x0400014A RID: 330
		private List<Stream> openedReadStreams;
	}
}
