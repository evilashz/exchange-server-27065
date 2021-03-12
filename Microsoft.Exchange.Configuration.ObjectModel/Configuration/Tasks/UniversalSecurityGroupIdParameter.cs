using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x0200016E RID: 366
	[Serializable]
	public class UniversalSecurityGroupIdParameter : GroupIdParameter
	{
		// Token: 0x06000D2A RID: 3370 RVA: 0x00028433 File Offset: 0x00026633
		public UniversalSecurityGroupIdParameter(string identity) : base(identity)
		{
		}

		// Token: 0x06000D2B RID: 3371 RVA: 0x0002843C File Offset: 0x0002663C
		public UniversalSecurityGroupIdParameter()
		{
		}

		// Token: 0x06000D2C RID: 3372 RVA: 0x00028444 File Offset: 0x00026644
		public UniversalSecurityGroupIdParameter(ADObjectId adObjectId) : base(adObjectId)
		{
		}

		// Token: 0x06000D2D RID: 3373 RVA: 0x0002844D File Offset: 0x0002664D
		public UniversalSecurityGroupIdParameter(INamedIdentity namedIdentity) : base(namedIdentity)
		{
		}

		// Token: 0x1700026F RID: 623
		// (get) Token: 0x06000D2E RID: 3374 RVA: 0x00028456 File Offset: 0x00026656
		internal override RecipientType[] RecipientTypes
		{
			get
			{
				return UniversalSecurityGroupIdParameter.AllowedRecipientTypes;
			}
		}

		// Token: 0x17000270 RID: 624
		// (get) Token: 0x06000D2F RID: 3375 RVA: 0x0002845D File Offset: 0x0002665D
		protected override QueryFilter AdditionalQueryFilter
		{
			get
			{
				return UniversalSecurityGroupIdParameter.GetUniversalSecurityGroupFilter(base.AdditionalQueryFilter);
			}
		}

		// Token: 0x06000D30 RID: 3376 RVA: 0x0002846A File Offset: 0x0002666A
		public new static UniversalSecurityGroupIdParameter Parse(string identity)
		{
			return new UniversalSecurityGroupIdParameter(identity);
		}

		// Token: 0x06000D31 RID: 3377 RVA: 0x00028474 File Offset: 0x00026674
		internal static QueryFilter GetUniversalSecurityGroupFilter(QueryFilter additionalFilter)
		{
			return QueryFilter.AndTogether(new QueryFilter[]
			{
				additionalFilter,
				UniversalSecurityGroupIdParameter.USGFilter
			});
		}

		// Token: 0x040002ED RID: 749
		private new static readonly RecipientType[] AllowedRecipientTypes = new RecipientType[]
		{
			RecipientType.Group,
			RecipientType.MailUniversalSecurityGroup
		};

		// Token: 0x040002EE RID: 750
		private static QueryFilter USGFilter = new AndFilter(new QueryFilter[]
		{
			new BitMaskAndFilter(ADGroupSchema.GroupType, 8UL),
			new BitMaskAndFilter(ADGroupSchema.GroupType, (ulong)int.MinValue)
		});
	}
}
