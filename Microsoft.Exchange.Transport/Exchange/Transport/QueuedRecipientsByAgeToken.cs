using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Transport;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x02000199 RID: 409
	internal class QueuedRecipientsByAgeToken
	{
		// Token: 0x060011D7 RID: 4567 RVA: 0x00048EBE File Offset: 0x000470BE
		public static QueuedRecipientsByAgeToken Generate(TransportMailItem item)
		{
			return new QueuedRecipientsByAgeToken(item.LatencyStartTime, DeliveryType.Undefined, item.Priority, item.Recipients.Count);
		}

		// Token: 0x060011D8 RID: 4568 RVA: 0x00048EDD File Offset: 0x000470DD
		public static QueuedRecipientsByAgeToken Generate(RoutedMailItem item)
		{
			return new QueuedRecipientsByAgeToken(item.LatencyStartTime, item.DeliveryType, item.Priority, item.Recipients.Count);
		}

		// Token: 0x170004AF RID: 1199
		// (get) Token: 0x060011D9 RID: 4569 RVA: 0x00048F01 File Offset: 0x00047101
		// (set) Token: 0x060011DA RID: 4570 RVA: 0x00048F09 File Offset: 0x00047109
		public DateTime OrgArrivalTimeUsed
		{
			get
			{
				return this.orgArrivalTimeUsed;
			}
			set
			{
				this.orgArrivalTimeUsed = value;
			}
		}

		// Token: 0x170004B0 RID: 1200
		// (get) Token: 0x060011DB RID: 4571 RVA: 0x00048F12 File Offset: 0x00047112
		public DateTime OrgArrivalTimeUtc
		{
			get
			{
				return this.orgArrivalTimeUtc;
			}
		}

		// Token: 0x170004B1 RID: 1201
		// (get) Token: 0x060011DC RID: 4572 RVA: 0x00048F1A File Offset: 0x0004711A
		public DeliveryType DeliveryType
		{
			get
			{
				return this.deliveryType;
			}
		}

		// Token: 0x170004B2 RID: 1202
		// (get) Token: 0x060011DD RID: 4573 RVA: 0x00048F22 File Offset: 0x00047122
		public DeliveryPriority DeliveryPriority
		{
			get
			{
				return this.deliveryPriority;
			}
		}

		// Token: 0x170004B3 RID: 1203
		// (get) Token: 0x060011DE RID: 4574 RVA: 0x00048F2A File Offset: 0x0004712A
		public int RecipientCount
		{
			get
			{
				return this.recipientCount;
			}
		}

		// Token: 0x060011DF RID: 4575 RVA: 0x00048F32 File Offset: 0x00047132
		private QueuedRecipientsByAgeToken(DateTime orgArrivalTimeUtc, DeliveryType deliveryType, DeliveryPriority deliveryPriority, int recipientCount)
		{
			this.orgArrivalTimeUtc = orgArrivalTimeUtc;
			this.deliveryType = deliveryType;
			this.deliveryPriority = deliveryPriority;
			this.recipientCount = recipientCount;
		}

		// Token: 0x04000970 RID: 2416
		private DateTime orgArrivalTimeUsed;

		// Token: 0x04000971 RID: 2417
		private readonly DateTime orgArrivalTimeUtc;

		// Token: 0x04000972 RID: 2418
		private readonly DeliveryType deliveryType;

		// Token: 0x04000973 RID: 2419
		private readonly DeliveryPriority deliveryPriority;

		// Token: 0x04000974 RID: 2420
		private readonly int recipientCount;
	}
}
