using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200005A RID: 90
	[DataContract]
	internal sealed class CountRestrictionData : HierRestrictionData
	{
		// Token: 0x06000482 RID: 1154 RVA: 0x00008A26 File Offset: 0x00006C26
		public CountRestrictionData()
		{
		}

		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x06000483 RID: 1155 RVA: 0x00008A2E File Offset: 0x00006C2E
		// (set) Token: 0x06000484 RID: 1156 RVA: 0x00008A36 File Offset: 0x00006C36
		[DataMember]
		public int Count { get; set; }

		// Token: 0x06000485 RID: 1157 RVA: 0x00008A40 File Offset: 0x00006C40
		internal CountRestrictionData(Restriction.CountRestriction r)
		{
			this.Count = r.Count;
			base.ParseRestrictions(new Restriction[]
			{
				r.Restriction
			});
		}

		// Token: 0x06000486 RID: 1158 RVA: 0x00008A78 File Offset: 0x00006C78
		internal CountRestrictionData(StoreSession storeSession, CountFilter countFilter)
		{
			int count = (int)countFilter.Count;
			if (countFilter.Count > 2147483647U)
			{
				count = int.MaxValue;
			}
			this.Count = count;
			base.ParseQueryFilter(storeSession, countFilter.Filter);
		}

		// Token: 0x06000487 RID: 1159 RVA: 0x00008AB9 File Offset: 0x00006CB9
		internal override Restriction GetRestriction()
		{
			return Restriction.Count(this.Count, base.GetRestrictions()[0]);
		}

		// Token: 0x06000488 RID: 1160 RVA: 0x00008ACE File Offset: 0x00006CCE
		internal override QueryFilter GetQueryFilter(StoreSession storeSession)
		{
			return new CountFilter((uint)this.Count, base.GetQueryFilters(storeSession)[0]);
		}

		// Token: 0x06000489 RID: 1161 RVA: 0x00008AE4 File Offset: 0x00006CE4
		internal override string ToStringInternal()
		{
			return string.Format("COUNT[{0}, {1}]", this.Count, base.Restrictions[0].ToStringInternal());
		}

		// Token: 0x0600048A RID: 1162 RVA: 0x00008B08 File Offset: 0x00006D08
		internal override int GetApproximateSize()
		{
			return base.GetApproximateSize() + 4;
		}
	}
}
