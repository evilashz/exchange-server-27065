using System;
using System.Collections.ObjectModel;
using System.IO;
using Microsoft.Exchange.Data.Transport.Email;

namespace Microsoft.Exchange.Data.Transport
{
	// Token: 0x0200004F RID: 79
	public abstract class DeliverableMailItem
	{
		// Token: 0x060001CB RID: 459 RVA: 0x0000657A File Offset: 0x0000477A
		internal DeliverableMailItem()
		{
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x060001CC RID: 460
		public abstract string OriginalAuthenticator { get; }

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x060001CD RID: 461
		public abstract DateTime DateTimeReceived { get; }

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x060001CE RID: 462
		public abstract string EnvelopeId { get; }

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x060001CF RID: 463
		public abstract RoutingAddress FromAddress { get; }

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x060001D0 RID: 464
		public abstract string OriginatingDomain { get; }

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x060001D1 RID: 465
		public abstract EmailMessage Message { get; }

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x060001D2 RID: 466
		public abstract long MimeStreamLength { get; }

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x060001D3 RID: 467
		internal abstract object RecipientCache { get; }

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x060001D4 RID: 468
		public abstract ReadOnlyEnvelopeRecipientCollection Recipients { get; }

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x060001D5 RID: 469
		public abstract DsnFormatRequested DsnFormatRequested { get; }

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x060001D6 RID: 470
		public abstract DeliveryPriority DeliveryPriority { get; }

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x060001D7 RID: 471
		public abstract DeliveryMethod InboundDeliveryMethod { get; }

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x060001D8 RID: 472
		public abstract bool MustDeliver { get; }

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x060001D9 RID: 473
		public abstract Guid TenantId { get; }

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x060001DA RID: 474
		internal abstract Guid SystemProbeId { get; }

		// Token: 0x060001DB RID: 475
		public abstract Stream GetMimeReadStream();

		// Token: 0x060001DC RID: 476
		public abstract bool TryGetListProperty<ItemT>(string name, out ReadOnlyCollection<ItemT> value);

		// Token: 0x060001DD RID: 477
		public abstract bool TryGetProperty<T>(string name, out T value);
	}
}
