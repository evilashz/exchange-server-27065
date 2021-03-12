using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000062 RID: 98
	[DataContract]
	internal sealed class OrRestrictionData : HierRestrictionData
	{
		// Token: 0x060004C5 RID: 1221 RVA: 0x0000902C File Offset: 0x0000722C
		public OrRestrictionData()
		{
		}

		// Token: 0x060004C6 RID: 1222 RVA: 0x00009034 File Offset: 0x00007234
		internal OrRestrictionData(Restriction.OrRestriction r)
		{
			base.ParseRestrictions(r.Restrictions);
		}

		// Token: 0x060004C7 RID: 1223 RVA: 0x00009048 File Offset: 0x00007248
		internal OrRestrictionData(StoreSession storeSession, OrFilter orFilter)
		{
			base.ParseQueryFilters(storeSession, orFilter.Filters);
		}

		// Token: 0x060004C8 RID: 1224 RVA: 0x0000905D File Offset: 0x0000725D
		internal override Restriction GetRestriction()
		{
			return Restriction.Or(base.GetRestrictions());
		}

		// Token: 0x060004C9 RID: 1225 RVA: 0x0000906A File Offset: 0x0000726A
		internal override QueryFilter GetQueryFilter(StoreSession storeSession)
		{
			return new OrFilter(base.GetQueryFilters(storeSession));
		}

		// Token: 0x060004CA RID: 1226 RVA: 0x00009078 File Offset: 0x00007278
		internal override string ToStringInternal()
		{
			return base.ConcatSubRestrictions("OR");
		}
	}
}
