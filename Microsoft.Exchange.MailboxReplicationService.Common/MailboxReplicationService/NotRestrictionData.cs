using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000060 RID: 96
	[DataContract]
	internal sealed class NotRestrictionData : HierRestrictionData
	{
		// Token: 0x060004BB RID: 1211 RVA: 0x00008F94 File Offset: 0x00007194
		public NotRestrictionData()
		{
		}

		// Token: 0x060004BC RID: 1212 RVA: 0x00008F9C File Offset: 0x0000719C
		internal NotRestrictionData(Restriction.NotRestriction r)
		{
			base.ParseRestrictions(new Restriction[]
			{
				r.Restriction
			});
		}

		// Token: 0x060004BD RID: 1213 RVA: 0x00008FC6 File Offset: 0x000071C6
		internal NotRestrictionData(StoreSession storeSession, NotFilter notFilter)
		{
			base.ParseQueryFilter(storeSession, notFilter.Filter);
		}

		// Token: 0x060004BE RID: 1214 RVA: 0x00008FDB File Offset: 0x000071DB
		internal override Restriction GetRestriction()
		{
			return Restriction.Not(base.GetRestrictions()[0]);
		}

		// Token: 0x060004BF RID: 1215 RVA: 0x00008FEA File Offset: 0x000071EA
		internal override QueryFilter GetQueryFilter(StoreSession storeSession)
		{
			return new NotFilter(base.GetQueryFilters(storeSession)[0]);
		}

		// Token: 0x060004C0 RID: 1216 RVA: 0x00008FFA File Offset: 0x000071FA
		internal override string ToStringInternal()
		{
			return string.Format("NOT[{0}]", base.Restrictions[0].ToStringInternal());
		}
	}
}
