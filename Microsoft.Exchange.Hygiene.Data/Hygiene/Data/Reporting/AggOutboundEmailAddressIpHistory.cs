using System;
using System.Net;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Hygiene.Data.Reporting
{
	// Token: 0x020001BD RID: 445
	internal class AggOutboundEmailAddressIpHistory : ConfigurablePropertyBag
	{
		// Token: 0x170005B0 RID: 1456
		// (get) Token: 0x0600129D RID: 4765 RVA: 0x00038D55 File Offset: 0x00036F55
		// (set) Token: 0x0600129E RID: 4766 RVA: 0x00038D67 File Offset: 0x00036F67
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

		// Token: 0x170005B1 RID: 1457
		// (get) Token: 0x0600129F RID: 4767 RVA: 0x00038D7A File Offset: 0x00036F7A
		// (set) Token: 0x060012A0 RID: 4768 RVA: 0x00038D8C File Offset: 0x00036F8C
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

		// Token: 0x170005B2 RID: 1458
		// (get) Token: 0x060012A1 RID: 4769 RVA: 0x00038D9A File Offset: 0x00036F9A
		// (set) Token: 0x060012A2 RID: 4770 RVA: 0x00038DAC File Offset: 0x00036FAC
		public string FromEmailAddress
		{
			get
			{
				return (string)this[AggOutboundIPSchema.FromEmailAddressProperty];
			}
			set
			{
				this[AggOutboundIPSchema.FromEmailAddressProperty] = value;
			}
		}

		// Token: 0x170005B3 RID: 1459
		// (get) Token: 0x060012A3 RID: 4771 RVA: 0x00038DBA File Offset: 0x00036FBA
		// (set) Token: 0x060012A4 RID: 4772 RVA: 0x00038DCC File Offset: 0x00036FCC
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

		// Token: 0x170005B4 RID: 1460
		// (get) Token: 0x060012A5 RID: 4773 RVA: 0x00038DDF File Offset: 0x00036FDF
		// (set) Token: 0x060012A6 RID: 4774 RVA: 0x00038DF1 File Offset: 0x00036FF1
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

		// Token: 0x170005B5 RID: 1461
		// (get) Token: 0x060012A7 RID: 4775 RVA: 0x00038E04 File Offset: 0x00037004
		// (set) Token: 0x060012A8 RID: 4776 RVA: 0x00038E16 File Offset: 0x00037016
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

		// Token: 0x170005B6 RID: 1462
		// (get) Token: 0x060012A9 RID: 4777 RVA: 0x00038E29 File Offset: 0x00037029
		// (set) Token: 0x060012AA RID: 4778 RVA: 0x00038E3B File Offset: 0x0003703B
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

		// Token: 0x170005B7 RID: 1463
		// (get) Token: 0x060012AB RID: 4779 RVA: 0x00038E4E File Offset: 0x0003704E
		// (set) Token: 0x060012AC RID: 4780 RVA: 0x00038E60 File Offset: 0x00037060
		public long ToSameDomainCount
		{
			get
			{
				return (long)this[AggOutboundIPSchema.ToSameDomainCountProperty];
			}
			set
			{
				this[AggOutboundIPSchema.ToSameDomainCountProperty] = value;
			}
		}

		// Token: 0x170005B8 RID: 1464
		// (get) Token: 0x060012AD RID: 4781 RVA: 0x00038E73 File Offset: 0x00037073
		// (set) Token: 0x060012AE RID: 4782 RVA: 0x00038E85 File Offset: 0x00037085
		public long MaxRecipientCount
		{
			get
			{
				return (long)this[AggOutboundIPSchema.MaxRecipientCountProperty];
			}
			set
			{
				this[AggOutboundIPSchema.MaxRecipientCountProperty] = value;
			}
		}

		// Token: 0x170005B9 RID: 1465
		// (get) Token: 0x060012AF RID: 4783 RVA: 0x00038E98 File Offset: 0x00037098
		// (set) Token: 0x060012B0 RID: 4784 RVA: 0x00038EAA File Offset: 0x000370AA
		public long ProvisionedDomainCount
		{
			get
			{
				return (long)this[AggOutboundIPSchema.ProvisionedDomainCountProperty];
			}
			set
			{
				this[AggOutboundIPSchema.ProvisionedDomainCountProperty] = value;
			}
		}

		// Token: 0x170005BA RID: 1466
		// (get) Token: 0x060012B1 RID: 4785 RVA: 0x00038EC0 File Offset: 0x000370C0
		public override ObjectId Identity
		{
			get
			{
				return new ConfigObjectId(this.identity.ToString());
			}
		}

		// Token: 0x060012B2 RID: 4786 RVA: 0x00038EE6 File Offset: 0x000370E6
		public override Type GetSchemaType()
		{
			return typeof(AggOutboundIPSchema);
		}

		// Token: 0x040008F0 RID: 2288
		private readonly Guid identity = ReportingSession.GenerateNewId();
	}
}
