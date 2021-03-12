using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000058 RID: 88
	[DataContract]
	internal sealed class ComparePropertyRestrictionData : RestrictionData
	{
		// Token: 0x06000463 RID: 1123 RVA: 0x000085DB File Offset: 0x000067DB
		public ComparePropertyRestrictionData()
		{
		}

		// Token: 0x1700019A RID: 410
		// (get) Token: 0x06000464 RID: 1124 RVA: 0x000085E3 File Offset: 0x000067E3
		// (set) Token: 0x06000465 RID: 1125 RVA: 0x000085EB File Offset: 0x000067EB
		[DataMember]
		public int RelOp { get; set; }

		// Token: 0x1700019B RID: 411
		// (get) Token: 0x06000466 RID: 1126 RVA: 0x000085F4 File Offset: 0x000067F4
		// (set) Token: 0x06000467 RID: 1127 RVA: 0x000085FC File Offset: 0x000067FC
		[DataMember]
		public int TagLeft { get; set; }

		// Token: 0x1700019C RID: 412
		// (get) Token: 0x06000468 RID: 1128 RVA: 0x00008605 File Offset: 0x00006805
		// (set) Token: 0x06000469 RID: 1129 RVA: 0x0000860D File Offset: 0x0000680D
		[DataMember]
		public int TagRight { get; set; }

		// Token: 0x0600046A RID: 1130 RVA: 0x00008616 File Offset: 0x00006816
		internal ComparePropertyRestrictionData(Restriction.ComparePropertyRestriction r)
		{
			this.RelOp = (int)r.Op;
			this.TagLeft = (int)r.TagLeft;
			this.TagRight = (int)r.TagRight;
		}

		// Token: 0x0600046B RID: 1131 RVA: 0x00008642 File Offset: 0x00006842
		internal ComparePropertyRestrictionData(StoreSession storeSession, PropertyComparisonFilter filter)
		{
			this.RelOp = base.GetRelOpFromComparisonOperator(filter.ComparisonOperator);
			this.TagLeft = base.GetPropTagFromDefinition(storeSession, filter.Property1);
			this.TagRight = base.GetPropTagFromDefinition(storeSession, filter.Property2);
		}

		// Token: 0x0600046C RID: 1132 RVA: 0x00008682 File Offset: 0x00006882
		internal override Restriction GetRestriction()
		{
			return new Restriction.ComparePropertyRestriction((Restriction.RelOp)this.RelOp, (PropTag)this.TagLeft, (PropTag)this.TagRight);
		}

		// Token: 0x0600046D RID: 1133 RVA: 0x0000869B File Offset: 0x0000689B
		internal override QueryFilter GetQueryFilter(StoreSession storeSession)
		{
			return new PropertyComparisonFilter(base.GetComparisonOperatorFromRelOp(this.RelOp), base.GetPropertyDefinitionFromPropTag(storeSession, this.TagLeft), base.GetPropertyDefinitionFromPropTag(storeSession, this.TagRight));
		}

		// Token: 0x0600046E RID: 1134 RVA: 0x000086C8 File Offset: 0x000068C8
		internal override void InternalEnumPropTags(CommonUtils.EnumPropTagDelegate del)
		{
			int tagLeft = this.TagLeft;
			int tagRight = this.TagRight;
			del(ref tagLeft);
			this.TagLeft = tagLeft;
			del(ref tagRight);
			this.TagRight = tagRight;
		}

		// Token: 0x0600046F RID: 1135 RVA: 0x00008701 File Offset: 0x00006901
		internal override string ToStringInternal()
		{
			return string.Format("COMPARE[ptag:{0}, {1}, ptag:{2}]", TraceUtils.DumpPropTag((PropTag)this.TagLeft), (Restriction.RelOp)this.RelOp, TraceUtils.DumpPropTag((PropTag)this.TagLeft));
		}

		// Token: 0x06000470 RID: 1136 RVA: 0x0000872E File Offset: 0x0000692E
		internal override int GetApproximateSize()
		{
			return base.GetApproximateSize() + 12;
		}
	}
}
