using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200007D RID: 125
	[DataContract]
	internal sealed class SizeRestrictionData : RestrictionData
	{
		// Token: 0x06000584 RID: 1412 RVA: 0x0000A463 File Offset: 0x00008663
		public SizeRestrictionData()
		{
		}

		// Token: 0x170001CC RID: 460
		// (get) Token: 0x06000585 RID: 1413 RVA: 0x0000A46B File Offset: 0x0000866B
		// (set) Token: 0x06000586 RID: 1414 RVA: 0x0000A473 File Offset: 0x00008673
		[DataMember]
		public int RelOp { get; set; }

		// Token: 0x170001CD RID: 461
		// (get) Token: 0x06000587 RID: 1415 RVA: 0x0000A47C File Offset: 0x0000867C
		// (set) Token: 0x06000588 RID: 1416 RVA: 0x0000A484 File Offset: 0x00008684
		[DataMember]
		public int PropTag { get; set; }

		// Token: 0x170001CE RID: 462
		// (get) Token: 0x06000589 RID: 1417 RVA: 0x0000A48D File Offset: 0x0000868D
		// (set) Token: 0x0600058A RID: 1418 RVA: 0x0000A495 File Offset: 0x00008695
		[DataMember]
		public int Size { get; set; }

		// Token: 0x0600058B RID: 1419 RVA: 0x0000A49E File Offset: 0x0000869E
		internal SizeRestrictionData(Restriction.SizeRestriction r)
		{
			this.RelOp = (int)r.Op;
			this.PropTag = (int)r.Tag;
			this.Size = r.Size;
		}

		// Token: 0x0600058C RID: 1420 RVA: 0x0000A4CA File Offset: 0x000086CA
		internal SizeRestrictionData(StoreSession storeSession, SizeFilter filter)
		{
			this.RelOp = base.GetRelOpFromComparisonOperator(filter.ComparisonOperator);
			this.PropTag = base.GetPropTagFromDefinition(storeSession, filter.Property);
			this.Size = (int)filter.PropertySize;
		}

		// Token: 0x0600058D RID: 1421 RVA: 0x0000A503 File Offset: 0x00008703
		internal override Restriction GetRestriction()
		{
			return new Restriction.SizeRestriction((Restriction.RelOp)this.RelOp, (PropTag)this.PropTag, this.Size);
		}

		// Token: 0x0600058E RID: 1422 RVA: 0x0000A51C File Offset: 0x0000871C
		internal override QueryFilter GetQueryFilter(StoreSession storeSession)
		{
			return new SizeFilter(base.GetComparisonOperatorFromRelOp(this.RelOp), base.GetPropertyDefinitionFromPropTag(storeSession, this.PropTag), (uint)this.Size);
		}

		// Token: 0x0600058F RID: 1423 RVA: 0x0000A544 File Offset: 0x00008744
		internal override void InternalEnumPropTags(CommonUtils.EnumPropTagDelegate del)
		{
			int propTag = this.PropTag;
			del(ref propTag);
			this.PropTag = propTag;
		}

		// Token: 0x06000590 RID: 1424 RVA: 0x0000A567 File Offset: 0x00008767
		internal override string ToStringInternal()
		{
			return string.Format("SIZE[ptag:{0}, {1}, size:{2}]", TraceUtils.DumpPropTag((PropTag)this.PropTag), (Restriction.RelOp)this.RelOp, this.Size);
		}

		// Token: 0x06000591 RID: 1425 RVA: 0x0000A594 File Offset: 0x00008794
		internal override int GetApproximateSize()
		{
			return base.GetApproximateSize() + 12;
		}
	}
}
