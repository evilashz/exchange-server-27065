using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000054 RID: 84
	[DataContract]
	internal sealed class AndRestrictionData : HierRestrictionData
	{
		// Token: 0x0600043E RID: 1086 RVA: 0x0000813B File Offset: 0x0000633B
		public AndRestrictionData()
		{
		}

		// Token: 0x0600043F RID: 1087 RVA: 0x00008143 File Offset: 0x00006343
		internal AndRestrictionData(Restriction.AndRestriction r)
		{
			base.ParseRestrictions(r.Restrictions);
		}

		// Token: 0x06000440 RID: 1088 RVA: 0x00008157 File Offset: 0x00006357
		internal AndRestrictionData(StoreSession storeSession, AndFilter queryFilter)
		{
			base.ParseQueryFilters(storeSession, queryFilter.Filters);
		}

		// Token: 0x06000441 RID: 1089 RVA: 0x0000816C File Offset: 0x0000636C
		internal override Restriction GetRestriction()
		{
			return Restriction.And(base.GetRestrictions());
		}

		// Token: 0x06000442 RID: 1090 RVA: 0x00008179 File Offset: 0x00006379
		internal override QueryFilter GetQueryFilter(StoreSession storeSession)
		{
			return new AndFilter(base.GetQueryFilters(storeSession));
		}

		// Token: 0x06000443 RID: 1091 RVA: 0x00008187 File Offset: 0x00006387
		internal override string ToStringInternal()
		{
			return base.ConcatSubRestrictions("AND");
		}
	}
}
