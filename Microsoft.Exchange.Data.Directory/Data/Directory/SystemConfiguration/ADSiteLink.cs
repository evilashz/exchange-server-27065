using System;
using System.Management.Automation;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000390 RID: 912
	[ObjectScope(ConfigScopes.Global)]
	[Serializable]
	public class ADSiteLink : ADConfigurationObject
	{
		// Token: 0x17000B8D RID: 2957
		// (get) Token: 0x060029A9 RID: 10665 RVA: 0x000AEE11 File Offset: 0x000AD011
		internal override ADObjectSchema Schema
		{
			get
			{
				return ADSiteLink.schema;
			}
		}

		// Token: 0x17000B8E RID: 2958
		// (get) Token: 0x060029AA RID: 10666 RVA: 0x000AEE18 File Offset: 0x000AD018
		internal override string MostDerivedObjectClass
		{
			get
			{
				return ADSiteLink.mostDerivedClass;
			}
		}

		// Token: 0x17000B8F RID: 2959
		// (get) Token: 0x060029AB RID: 10667 RVA: 0x000AEE1F File Offset: 0x000AD01F
		internal override bool ExchangeVersionUpgradeSupported
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000B90 RID: 2960
		// (get) Token: 0x060029AD RID: 10669 RVA: 0x000AEE2A File Offset: 0x000AD02A
		public int Cost
		{
			get
			{
				return (int)this[ADSiteLinkSchema.Cost];
			}
		}

		// Token: 0x17000B91 RID: 2961
		// (get) Token: 0x060029AE RID: 10670 RVA: 0x000AEE3C File Offset: 0x000AD03C
		public int ADCost
		{
			get
			{
				return (int)this[ADSiteLinkSchema.ADCost];
			}
		}

		// Token: 0x17000B92 RID: 2962
		// (get) Token: 0x060029AF RID: 10671 RVA: 0x000AEE4E File Offset: 0x000AD04E
		// (set) Token: 0x060029B0 RID: 10672 RVA: 0x000AEE60 File Offset: 0x000AD060
		[Parameter]
		public int? ExchangeCost
		{
			get
			{
				return (int?)this[ADSiteLinkSchema.ExchangeCost];
			}
			set
			{
				this[ADSiteLinkSchema.ExchangeCost] = value;
			}
		}

		// Token: 0x17000B93 RID: 2963
		// (get) Token: 0x060029B1 RID: 10673 RVA: 0x000AEE73 File Offset: 0x000AD073
		// (set) Token: 0x060029B2 RID: 10674 RVA: 0x000AEE85 File Offset: 0x000AD085
		[Parameter]
		public Unlimited<ByteQuantifiedSize> MaxMessageSize
		{
			get
			{
				return (Unlimited<ByteQuantifiedSize>)this[ADSiteLinkSchema.MaxMessageSize];
			}
			set
			{
				this[ADSiteLinkSchema.MaxMessageSize] = value;
			}
		}

		// Token: 0x17000B94 RID: 2964
		// (get) Token: 0x060029B3 RID: 10675 RVA: 0x000AEE98 File Offset: 0x000AD098
		public MultiValuedProperty<ADObjectId> Sites
		{
			get
			{
				return (MultiValuedProperty<ADObjectId>)this[ADSiteLinkSchema.Sites];
			}
		}

		// Token: 0x060029B4 RID: 10676 RVA: 0x000AEEAA File Offset: 0x000AD0AA
		internal static object CostGetter(IPropertyBag propertyBag)
		{
			return propertyBag[ADSiteLinkSchema.ExchangeCost] ?? propertyBag[ADSiteLinkSchema.ADCost];
		}

		// Token: 0x0400197A RID: 6522
		private static ADSiteLinkSchema schema = ObjectSchema.GetInstance<ADSiteLinkSchema>();

		// Token: 0x0400197B RID: 6523
		private static string mostDerivedClass = "siteLink";

		// Token: 0x0400197C RID: 6524
		internal static ulong UnlimitedMaxMessageSize = ByteQuantifiedSize.MaxValue.ToBytes();
	}
}
