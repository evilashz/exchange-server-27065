using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x02000115 RID: 277
	[Serializable]
	public class HostedSpamFilterConfigIdParameter : ADIdParameter
	{
		// Token: 0x170001FC RID: 508
		// (get) Token: 0x060009ED RID: 2541 RVA: 0x00021635 File Offset: 0x0001F835
		protected override SharedTenantConfigurationMode SharedTenantConfigurationMode
		{
			get
			{
				return SharedTenantConfigurationMode.Dehydrateable;
			}
		}

		// Token: 0x060009EE RID: 2542 RVA: 0x00021638 File Offset: 0x0001F838
		public HostedSpamFilterConfigIdParameter()
		{
		}

		// Token: 0x060009EF RID: 2543 RVA: 0x00021640 File Offset: 0x0001F840
		public HostedSpamFilterConfigIdParameter(ADObjectId adobjectid) : base(adobjectid)
		{
		}

		// Token: 0x060009F0 RID: 2544 RVA: 0x00021649 File Offset: 0x0001F849
		protected HostedSpamFilterConfigIdParameter(string identity) : base(identity)
		{
		}

		// Token: 0x060009F1 RID: 2545 RVA: 0x00021652 File Offset: 0x0001F852
		public HostedSpamFilterConfigIdParameter(INamedIdentity namedIdentity) : base(namedIdentity)
		{
		}

		// Token: 0x170001FD RID: 509
		// (get) Token: 0x060009F2 RID: 2546 RVA: 0x0002165C File Offset: 0x0001F85C
		protected override QueryFilter AdditionalQueryFilter
		{
			get
			{
				return QueryFilter.AndTogether(new QueryFilter[]
				{
					base.AdditionalQueryFilter,
					new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.ExchangeVersion, ExchangeObjectVersion.Exchange2007)
				});
			}
		}

		// Token: 0x060009F3 RID: 2547 RVA: 0x00021692 File Offset: 0x0001F892
		public static HostedSpamFilterConfigIdParameter Parse(string identity)
		{
			return new HostedSpamFilterConfigIdParameter(identity);
		}
	}
}
