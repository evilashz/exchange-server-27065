using System;
using System.Net;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Hygiene.Data.Reporting
{
	// Token: 0x020001BE RID: 446
	internal class AggOutboundIpHistory : ConfigurablePropertyBag
	{
		// Token: 0x170005BB RID: 1467
		// (get) Token: 0x060012B4 RID: 4788 RVA: 0x00038F05 File Offset: 0x00037105
		// (set) Token: 0x060012B5 RID: 4789 RVA: 0x00038F17 File Offset: 0x00037117
		public Guid TenantId
		{
			get
			{
				return (Guid)this[AggOutboundIPSchema.TenantIdProperty];
			}
			set
			{
				this[AggOutboundIPSchema.TenantIdProperty] = value;
			}
		}

		// Token: 0x170005BC RID: 1468
		// (get) Token: 0x060012B6 RID: 4790 RVA: 0x00038F2A File Offset: 0x0003712A
		// (set) Token: 0x060012B7 RID: 4791 RVA: 0x00038F3C File Offset: 0x0003713C
		public IPAddress IPAddress
		{
			get
			{
				return (IPAddress)this[AggOutboundIPSchema.IPAddressProperty];
			}
			set
			{
				this[AggOutboundIPSchema.IPAddressProperty] = value;
			}
		}

		// Token: 0x170005BD RID: 1469
		// (get) Token: 0x060012B8 RID: 4792 RVA: 0x00038F4A File Offset: 0x0003714A
		// (set) Token: 0x060012B9 RID: 4793 RVA: 0x00038F5C File Offset: 0x0003715C
		public long SpamMessageCount
		{
			get
			{
				return (long)this[AggOutboundIPSchema.SpamMessageCountProperty];
			}
			set
			{
				this[AggOutboundIPSchema.SpamMessageCountProperty] = value;
			}
		}

		// Token: 0x170005BE RID: 1470
		// (get) Token: 0x060012BA RID: 4794 RVA: 0x00038F6F File Offset: 0x0003716F
		// (set) Token: 0x060012BB RID: 4795 RVA: 0x00038F81 File Offset: 0x00037181
		public long TotalMessageCount
		{
			get
			{
				return (long)this[AggOutboundIPSchema.TotalMessageCountProperty];
			}
			set
			{
				this[AggOutboundIPSchema.TotalMessageCountProperty] = value;
			}
		}

		// Token: 0x170005BF RID: 1471
		// (get) Token: 0x060012BC RID: 4796 RVA: 0x00038F94 File Offset: 0x00037194
		// (set) Token: 0x060012BD RID: 4797 RVA: 0x00038FA6 File Offset: 0x000371A6
		public long SpamRecipientCount
		{
			get
			{
				return (long)this[AggOutboundIPSchema.SpamRecipientCountProperty];
			}
			set
			{
				this[AggOutboundIPSchema.SpamRecipientCountProperty] = value;
			}
		}

		// Token: 0x170005C0 RID: 1472
		// (get) Token: 0x060012BE RID: 4798 RVA: 0x00038FB9 File Offset: 0x000371B9
		// (set) Token: 0x060012BF RID: 4799 RVA: 0x00038FCB File Offset: 0x000371CB
		public long TotalRecipientCount
		{
			get
			{
				return (long)this[AggOutboundIPSchema.TotalRecipientCountProperty];
			}
			set
			{
				this[AggOutboundIPSchema.TotalRecipientCountProperty] = value;
			}
		}

		// Token: 0x170005C1 RID: 1473
		// (get) Token: 0x060012C0 RID: 4800 RVA: 0x00038FDE File Offset: 0x000371DE
		// (set) Token: 0x060012C1 RID: 4801 RVA: 0x00038FF0 File Offset: 0x000371F0
		public long NDRSpamMessageCount
		{
			get
			{
				return (long)this[AggOutboundIPSchema.NDRSpamMessageCountProperty];
			}
			set
			{
				this[AggOutboundIPSchema.NDRSpamMessageCountProperty] = value;
			}
		}

		// Token: 0x170005C2 RID: 1474
		// (get) Token: 0x060012C2 RID: 4802 RVA: 0x00039003 File Offset: 0x00037203
		// (set) Token: 0x060012C3 RID: 4803 RVA: 0x00039015 File Offset: 0x00037215
		public long NDRTotalMessageCount
		{
			get
			{
				return (long)this[AggOutboundIPSchema.NDRTotalMessageCountProperty];
			}
			set
			{
				this[AggOutboundIPSchema.NDRTotalMessageCountProperty] = value;
			}
		}

		// Token: 0x170005C3 RID: 1475
		// (get) Token: 0x060012C4 RID: 4804 RVA: 0x00039028 File Offset: 0x00037228
		// (set) Token: 0x060012C5 RID: 4805 RVA: 0x0003903A File Offset: 0x0003723A
		public long NDRSpamRecipientCount
		{
			get
			{
				return (long)this[AggOutboundIPSchema.NDRSpamRecipientCountProperty];
			}
			set
			{
				this[AggOutboundIPSchema.NDRSpamRecipientCountProperty] = value;
			}
		}

		// Token: 0x170005C4 RID: 1476
		// (get) Token: 0x060012C6 RID: 4806 RVA: 0x0003904D File Offset: 0x0003724D
		// (set) Token: 0x060012C7 RID: 4807 RVA: 0x0003905F File Offset: 0x0003725F
		public long NDRTotalRecipientCount
		{
			get
			{
				return (long)this[AggOutboundIPSchema.NDRTotalRecipientCountProperty];
			}
			set
			{
				this[AggOutboundIPSchema.NDRTotalRecipientCountProperty] = value;
			}
		}

		// Token: 0x170005C5 RID: 1477
		// (get) Token: 0x060012C8 RID: 4808 RVA: 0x00039072 File Offset: 0x00037272
		// (set) Token: 0x060012C9 RID: 4809 RVA: 0x00039084 File Offset: 0x00037284
		public long UniqueDomainsCount
		{
			get
			{
				return (long)this[AggOutboundIPSchema.UniqueDomainsCountProperty];
			}
			set
			{
				this[AggOutboundIPSchema.UniqueDomainsCountProperty] = value;
			}
		}

		// Token: 0x170005C6 RID: 1478
		// (get) Token: 0x060012CA RID: 4810 RVA: 0x00039097 File Offset: 0x00037297
		// (set) Token: 0x060012CB RID: 4811 RVA: 0x000390A9 File Offset: 0x000372A9
		public long NonProvisionedDomainCount
		{
			get
			{
				return (long)this[AggOutboundIPSchema.NonProvisionedDomainCountProperty];
			}
			set
			{
				this[AggOutboundIPSchema.NonProvisionedDomainCountProperty] = value;
			}
		}

		// Token: 0x170005C7 RID: 1479
		// (get) Token: 0x060012CC RID: 4812 RVA: 0x000390BC File Offset: 0x000372BC
		// (set) Token: 0x060012CD RID: 4813 RVA: 0x000390CE File Offset: 0x000372CE
		public long UniqueSendersCount
		{
			get
			{
				return (long)this[AggOutboundIPSchema.UniqueSendersCountProperty];
			}
			set
			{
				this[AggOutboundIPSchema.UniqueSendersCountProperty] = value;
			}
		}

		// Token: 0x170005C8 RID: 1480
		// (get) Token: 0x060012CE RID: 4814 RVA: 0x000390E4 File Offset: 0x000372E4
		public override ObjectId Identity
		{
			get
			{
				return new ConfigObjectId(this.identity.ToString());
			}
		}

		// Token: 0x060012CF RID: 4815 RVA: 0x0003910A File Offset: 0x0003730A
		public override Type GetSchemaType()
		{
			return typeof(AggOutboundIPSchema);
		}

		// Token: 0x040008F1 RID: 2289
		private readonly Guid identity = ReportingSession.GenerateNewId();
	}
}
