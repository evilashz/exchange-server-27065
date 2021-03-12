using System;
using System.Globalization;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Transport.MessageDepot;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x02000188 RID: 392
	internal class MessageDepotMailItem : IMessageDepotItem
	{
		// Token: 0x0600110C RID: 4364 RVA: 0x00045BC8 File Offset: 0x00043DC8
		public MessageDepotMailItem(TransportMailItem mailItem)
		{
			ArgumentValidator.ThrowIfNull("mailItem", mailItem);
			this.mailItem = mailItem;
			this.messageId = new TransportMessageId(this.mailItem.RecordId.ToString(CultureInfo.CurrentCulture));
		}

		// Token: 0x1700047D RID: 1149
		// (get) Token: 0x0600110D RID: 4365 RVA: 0x00045C10 File Offset: 0x00043E10
		public object MessageObject
		{
			get
			{
				return this.mailItem;
			}
		}

		// Token: 0x1700047E RID: 1150
		// (get) Token: 0x0600110E RID: 4366 RVA: 0x00045C18 File Offset: 0x00043E18
		public DateTime ArrivalTime
		{
			get
			{
				return this.mailItem.LatencyStartTime;
			}
		}

		// Token: 0x1700047F RID: 1151
		// (get) Token: 0x0600110F RID: 4367 RVA: 0x00045C25 File Offset: 0x00043E25
		public DateTime ExpirationTime
		{
			get
			{
				return this.mailItem.Expiry;
			}
		}

		// Token: 0x17000480 RID: 1152
		// (get) Token: 0x06001110 RID: 4368 RVA: 0x00045C32 File Offset: 0x00043E32
		public TransportMessageId Id
		{
			get
			{
				return this.messageId;
			}
		}

		// Token: 0x17000481 RID: 1153
		// (get) Token: 0x06001111 RID: 4369 RVA: 0x00045C3A File Offset: 0x00043E3A
		// (set) Token: 0x06001112 RID: 4370 RVA: 0x00045C42 File Offset: 0x00043E42
		public bool IsDelayDsnGenerated { get; set; }

		// Token: 0x17000482 RID: 1154
		// (get) Token: 0x06001113 RID: 4371 RVA: 0x00045C4B File Offset: 0x00043E4B
		public bool IsSuspended
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000483 RID: 1155
		// (get) Token: 0x06001114 RID: 4372 RVA: 0x00045C4E File Offset: 0x00043E4E
		public MessageDepotItemStage Stage
		{
			get
			{
				return MessageDepotItemStage.Submission;
			}
		}

		// Token: 0x17000484 RID: 1156
		// (get) Token: 0x06001115 RID: 4373 RVA: 0x00045C51 File Offset: 0x00043E51
		public bool IsPoison
		{
			get
			{
				return this.mailItem.IsPoison;
			}
		}

		// Token: 0x17000485 RID: 1157
		// (get) Token: 0x06001116 RID: 4374 RVA: 0x00045C5E File Offset: 0x00043E5E
		// (set) Token: 0x06001117 RID: 4375 RVA: 0x00045C6B File Offset: 0x00043E6B
		public DateTime DeferUntil
		{
			get
			{
				return this.mailItem.DeferUntil;
			}
			set
			{
				this.mailItem.DeferUntil = value;
			}
		}

		// Token: 0x17000486 RID: 1158
		// (get) Token: 0x06001118 RID: 4376 RVA: 0x00045C79 File Offset: 0x00043E79
		public MessageEnvelope MessageEnvelope
		{
			get
			{
				return this.mailItem.GetMessageEnvelope();
			}
		}

		// Token: 0x06001119 RID: 4377 RVA: 0x00045C86 File Offset: 0x00043E86
		public void Dehydrate()
		{
		}

		// Token: 0x0600111A RID: 4378 RVA: 0x00045C88 File Offset: 0x00043E88
		public object GetProperty(string propertyName)
		{
			throw new NotImplementedException();
		}

		// Token: 0x04000921 RID: 2337
		private readonly TransportMailItem mailItem;

		// Token: 0x04000922 RID: 2338
		private readonly TransportMessageId messageId;
	}
}
