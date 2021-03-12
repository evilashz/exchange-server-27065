using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020002E5 RID: 741
	[Serializable]
	public class MSERVEntry : ConfigurableObject
	{
		// Token: 0x060022BF RID: 8895 RVA: 0x000977A3 File Offset: 0x000959A3
		internal MSERVEntry() : base(new SimpleProviderPropertyBag())
		{
			this.propertyBag.SetField(this.propertyBag.ObjectVersionPropertyDefinition, ExchangeObjectVersion.Exchange2012);
		}

		// Token: 0x170008C9 RID: 2249
		// (get) Token: 0x060022C0 RID: 8896 RVA: 0x000977CC File Offset: 0x000959CC
		// (set) Token: 0x060022C1 RID: 8897 RVA: 0x000977DE File Offset: 0x000959DE
		public Guid ExternalDirectoryOrganizationId
		{
			get
			{
				return (Guid)this[MSERVEntrySchema.ExternalDirectoryOrganizationId];
			}
			set
			{
				this[MSERVEntrySchema.ExternalDirectoryOrganizationId] = value;
			}
		}

		// Token: 0x170008CA RID: 2250
		// (get) Token: 0x060022C2 RID: 8898 RVA: 0x000977F1 File Offset: 0x000959F1
		// (set) Token: 0x060022C3 RID: 8899 RVA: 0x00097803 File Offset: 0x00095A03
		public string DomainName
		{
			get
			{
				return (string)this[MSERVEntrySchema.DomainName];
			}
			set
			{
				this[MSERVEntrySchema.DomainName] = value;
			}
		}

		// Token: 0x170008CB RID: 2251
		// (get) Token: 0x060022C4 RID: 8900 RVA: 0x00097811 File Offset: 0x00095A11
		// (set) Token: 0x060022C5 RID: 8901 RVA: 0x00097823 File Offset: 0x00095A23
		public string AddressForPartnerId
		{
			get
			{
				return (string)this[MSERVEntrySchema.AddressForPartnerId];
			}
			set
			{
				this[MSERVEntrySchema.AddressForPartnerId] = value;
			}
		}

		// Token: 0x170008CC RID: 2252
		// (get) Token: 0x060022C6 RID: 8902 RVA: 0x00097831 File Offset: 0x00095A31
		// (set) Token: 0x060022C7 RID: 8903 RVA: 0x00097843 File Offset: 0x00095A43
		public int PartnerId
		{
			get
			{
				return (int)this[MSERVEntrySchema.PartnerId];
			}
			set
			{
				this[MSERVEntrySchema.PartnerId] = value;
			}
		}

		// Token: 0x170008CD RID: 2253
		// (get) Token: 0x060022C8 RID: 8904 RVA: 0x00097856 File Offset: 0x00095A56
		// (set) Token: 0x060022C9 RID: 8905 RVA: 0x00097868 File Offset: 0x00095A68
		public string AddressForMinorPartnerId
		{
			get
			{
				return (string)this[MSERVEntrySchema.AddressForMinorPartnerId];
			}
			set
			{
				this[MSERVEntrySchema.AddressForMinorPartnerId] = value;
			}
		}

		// Token: 0x170008CE RID: 2254
		// (get) Token: 0x060022CA RID: 8906 RVA: 0x00097876 File Offset: 0x00095A76
		// (set) Token: 0x060022CB RID: 8907 RVA: 0x00097888 File Offset: 0x00095A88
		public int MinorPartnerId
		{
			get
			{
				return (int)this[MSERVEntrySchema.MinorPartnerId];
			}
			set
			{
				this[MSERVEntrySchema.MinorPartnerId] = value;
			}
		}

		// Token: 0x170008CF RID: 2255
		// (get) Token: 0x060022CC RID: 8908 RVA: 0x0009789B File Offset: 0x00095A9B
		// (set) Token: 0x060022CD RID: 8909 RVA: 0x000978AD File Offset: 0x00095AAD
		public string Forest
		{
			get
			{
				return (string)this[MSERVEntrySchema.Forest];
			}
			set
			{
				this[MSERVEntrySchema.Forest] = value;
			}
		}

		// Token: 0x170008D0 RID: 2256
		// (get) Token: 0x060022CE RID: 8910 RVA: 0x000978BB File Offset: 0x00095ABB
		internal override ObjectSchema ObjectSchema
		{
			get
			{
				return ObjectSchema.GetInstance<MSERVEntrySchema>();
			}
		}
	}
}
