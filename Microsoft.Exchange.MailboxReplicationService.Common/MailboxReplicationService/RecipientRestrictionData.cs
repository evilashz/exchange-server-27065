using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000066 RID: 102
	[DataContract]
	internal sealed class RecipientRestrictionData : HierRestrictionData
	{
		// Token: 0x060004EB RID: 1259 RVA: 0x0000950F File Offset: 0x0000770F
		public RecipientRestrictionData()
		{
		}

		// Token: 0x060004EC RID: 1260 RVA: 0x00009518 File Offset: 0x00007718
		internal RecipientRestrictionData(Restriction.RecipientRestriction r)
		{
			base.ParseRestrictions(new Restriction[]
			{
				r.Restriction
			});
		}

		// Token: 0x060004ED RID: 1261 RVA: 0x00009542 File Offset: 0x00007742
		internal RecipientRestrictionData(StoreSession storeSession, SubFilter filter)
		{
			base.ParseQueryFilter(storeSession, filter.Filter);
		}

		// Token: 0x060004EE RID: 1262 RVA: 0x00009557 File Offset: 0x00007757
		internal override Restriction GetRestriction()
		{
			return Restriction.Sub(PropTag.MessageRecipients, base.GetRestrictions()[0]);
		}

		// Token: 0x060004EF RID: 1263 RVA: 0x0000956B File Offset: 0x0000776B
		internal override QueryFilter GetQueryFilter(StoreSession storeSession)
		{
			return new SubFilter(SubFilterProperties.Recipients, base.GetQueryFilters(storeSession)[0]);
		}

		// Token: 0x060004F0 RID: 1264 RVA: 0x0000957C File Offset: 0x0000777C
		internal override string ToStringInternal()
		{
			return string.Format("RECIPIENT[{0}]", base.Restrictions[0].ToStringInternal());
		}
	}
}
