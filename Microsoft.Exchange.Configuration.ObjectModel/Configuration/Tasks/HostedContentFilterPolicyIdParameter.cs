using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x02000113 RID: 275
	[Serializable]
	public class HostedContentFilterPolicyIdParameter : ADIdParameter
	{
		// Token: 0x170001F9 RID: 505
		// (get) Token: 0x060009DF RID: 2527 RVA: 0x00021549 File Offset: 0x0001F749
		protected override SharedTenantConfigurationMode SharedTenantConfigurationMode
		{
			get
			{
				return SharedTenantConfigurationMode.Dehydrateable;
			}
		}

		// Token: 0x060009E0 RID: 2528 RVA: 0x0002154C File Offset: 0x0001F74C
		public HostedContentFilterPolicyIdParameter()
		{
		}

		// Token: 0x060009E1 RID: 2529 RVA: 0x00021554 File Offset: 0x0001F754
		public HostedContentFilterPolicyIdParameter(ADObjectId adobjectid) : base(adobjectid)
		{
		}

		// Token: 0x060009E2 RID: 2530 RVA: 0x0002155D File Offset: 0x0001F75D
		public HostedContentFilterPolicyIdParameter(string identity) : base(identity)
		{
		}

		// Token: 0x060009E3 RID: 2531 RVA: 0x00021566 File Offset: 0x0001F766
		public HostedContentFilterPolicyIdParameter(INamedIdentity namedIdentity) : base(namedIdentity)
		{
		}

		// Token: 0x170001FA RID: 506
		// (get) Token: 0x060009E4 RID: 2532 RVA: 0x00021570 File Offset: 0x0001F770
		protected override QueryFilter AdditionalQueryFilter
		{
			get
			{
				return QueryFilter.AndTogether(new QueryFilter[]
				{
					base.AdditionalQueryFilter,
					new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.ExchangeVersion, ExchangeObjectVersion.Exchange2012)
				});
			}
		}

		// Token: 0x060009E5 RID: 2533 RVA: 0x000215A6 File Offset: 0x0001F7A6
		public static HostedContentFilterPolicyIdParameter Parse(string identity)
		{
			return new HostedContentFilterPolicyIdParameter(identity);
		}
	}
}
