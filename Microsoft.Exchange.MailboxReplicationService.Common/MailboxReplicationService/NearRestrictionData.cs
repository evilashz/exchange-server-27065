using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200005F RID: 95
	[DataContract]
	internal sealed class NearRestrictionData : RestrictionData
	{
		// Token: 0x060004AE RID: 1198 RVA: 0x00008DF8 File Offset: 0x00006FF8
		public NearRestrictionData()
		{
		}

		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x060004AF RID: 1199 RVA: 0x00008E00 File Offset: 0x00007000
		// (set) Token: 0x060004B0 RID: 1200 RVA: 0x00008E08 File Offset: 0x00007008
		[DataMember]
		public int Distance { get; set; }

		// Token: 0x170001AA RID: 426
		// (get) Token: 0x060004B1 RID: 1201 RVA: 0x00008E11 File Offset: 0x00007011
		// (set) Token: 0x060004B2 RID: 1202 RVA: 0x00008E19 File Offset: 0x00007019
		[DataMember]
		public bool Ordered { get; set; }

		// Token: 0x170001AB RID: 427
		// (get) Token: 0x060004B3 RID: 1203 RVA: 0x00008E22 File Offset: 0x00007022
		// (set) Token: 0x060004B4 RID: 1204 RVA: 0x00008E2A File Offset: 0x0000702A
		[DataMember]
		public AndRestrictionData RestrictionData { get; set; }

		// Token: 0x060004B5 RID: 1205 RVA: 0x00008E34 File Offset: 0x00007034
		internal NearRestrictionData(Restriction.NearRestriction r)
		{
			if (r == null || r.Restriction == null)
			{
				MrsTracer.Common.Error("Null near restriction in near restriction data constructor", new object[0]);
				throw new CorruptRestrictionDataException();
			}
			this.Distance = r.Distance;
			this.Ordered = r.Ordered;
			this.RestrictionData = new AndRestrictionData(r.Restriction);
		}

		// Token: 0x060004B6 RID: 1206 RVA: 0x00008E98 File Offset: 0x00007098
		internal NearRestrictionData(StoreSession storeSession, NearFilter nearFilter)
		{
			if (nearFilter == null || nearFilter.Filter == null)
			{
				MrsTracer.Common.Error("Null near filter in near restriction data constructor", new object[0]);
				throw new CorruptRestrictionDataException();
			}
			this.Distance = (int)nearFilter.Distance;
			this.Ordered = nearFilter.Ordered;
			this.RestrictionData = new AndRestrictionData(storeSession, nearFilter.Filter);
		}

		// Token: 0x060004B7 RID: 1207 RVA: 0x00008EFB File Offset: 0x000070FB
		internal override Restriction GetRestriction()
		{
			return Restriction.Near(this.Distance, this.Ordered, (Restriction.AndRestriction)this.RestrictionData.GetRestriction());
		}

		// Token: 0x060004B8 RID: 1208 RVA: 0x00008F1E File Offset: 0x0000711E
		internal override QueryFilter GetQueryFilter(StoreSession storeSession)
		{
			return new NearFilter((uint)this.Distance, this.Ordered, (AndFilter)this.RestrictionData.GetQueryFilter(storeSession));
		}

		// Token: 0x060004B9 RID: 1209 RVA: 0x00008F42 File Offset: 0x00007142
		internal override string ToStringInternal()
		{
			return string.Format("Near[distance:{0}, ordered:{1}, {2}]", this.Distance, this.Ordered, (this.RestrictionData == null) ? string.Empty : this.RestrictionData.ToStringInternal());
		}

		// Token: 0x060004BA RID: 1210 RVA: 0x00008F7E File Offset: 0x0000717E
		internal override int GetApproximateSize()
		{
			return base.GetApproximateSize() + 5 + this.RestrictionData.GetApproximateSize();
		}
	}
}
