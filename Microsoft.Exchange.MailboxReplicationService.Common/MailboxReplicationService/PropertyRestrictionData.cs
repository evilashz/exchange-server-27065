using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000063 RID: 99
	[DataContract]
	internal sealed class PropertyRestrictionData : RestrictionData
	{
		// Token: 0x060004CB RID: 1227 RVA: 0x00009085 File Offset: 0x00007285
		public PropertyRestrictionData()
		{
		}

		// Token: 0x170001AC RID: 428
		// (get) Token: 0x060004CC RID: 1228 RVA: 0x0000908D File Offset: 0x0000728D
		// (set) Token: 0x060004CD RID: 1229 RVA: 0x00009095 File Offset: 0x00007295
		[DataMember]
		public int RelOp { get; set; }

		// Token: 0x170001AD RID: 429
		// (get) Token: 0x060004CE RID: 1230 RVA: 0x0000909E File Offset: 0x0000729E
		// (set) Token: 0x060004CF RID: 1231 RVA: 0x000090A6 File Offset: 0x000072A6
		[DataMember]
		public int PropTag { get; set; }

		// Token: 0x170001AE RID: 430
		// (get) Token: 0x060004D0 RID: 1232 RVA: 0x000090AF File Offset: 0x000072AF
		// (set) Token: 0x060004D1 RID: 1233 RVA: 0x000090B7 File Offset: 0x000072B7
		[DataMember]
		public bool MultiValued { get; set; }

		// Token: 0x170001AF RID: 431
		// (get) Token: 0x060004D2 RID: 1234 RVA: 0x000090C0 File Offset: 0x000072C0
		// (set) Token: 0x060004D3 RID: 1235 RVA: 0x000090C8 File Offset: 0x000072C8
		[DataMember]
		public PropValueData Value { get; set; }

		// Token: 0x060004D4 RID: 1236 RVA: 0x000090D1 File Offset: 0x000072D1
		internal PropertyRestrictionData(Restriction.PropertyRestriction r)
		{
			this.RelOp = (int)r.Op;
			this.PropTag = (int)r.PropTag;
			this.MultiValued = r.MultiValued;
			this.Value = DataConverter<PropValueConverter, PropValue, PropValueData>.GetData(r.PropValue);
		}

		// Token: 0x060004D5 RID: 1237 RVA: 0x00009110 File Offset: 0x00007310
		internal PropertyRestrictionData(StoreSession storeSession, ComparisonFilter comparisonFilter)
		{
			this.RelOp = base.GetRelOpFromComparisonOperator(comparisonFilter.ComparisonOperator);
			this.PropTag = base.GetPropTagFromDefinition(storeSession, comparisonFilter.Property);
			this.MultiValued = (comparisonFilter is MultivaluedInstanceComparisonFilter);
			this.Value = new PropValueData((PropTag)this.PropTag, comparisonFilter.PropertyValue);
		}

		// Token: 0x060004D6 RID: 1238 RVA: 0x00009170 File Offset: 0x00007370
		internal override Restriction GetRestriction()
		{
			return new Restriction.PropertyRestriction((Restriction.RelOp)this.RelOp, (PropTag)this.PropTag, this.MultiValued, DataConverter<PropValueConverter, PropValue, PropValueData>.GetNative(this.Value).Value);
		}

		// Token: 0x060004D7 RID: 1239 RVA: 0x000091A7 File Offset: 0x000073A7
		internal override QueryFilter GetQueryFilter(StoreSession storeSession)
		{
			return new ComparisonFilter(base.GetComparisonOperatorFromRelOp(this.RelOp), base.GetPropertyDefinitionFromPropTag(storeSession, this.PropTag), this.Value.Value);
		}

		// Token: 0x060004D8 RID: 1240 RVA: 0x000091D4 File Offset: 0x000073D4
		internal override void InternalEnumPropTags(CommonUtils.EnumPropTagDelegate del)
		{
			int propTag = this.PropTag;
			del(ref propTag);
			this.PropTag = propTag;
		}

		// Token: 0x060004D9 RID: 1241 RVA: 0x000091F7 File Offset: 0x000073F7
		internal override void InternalEnumPropValues(CommonUtils.EnumPropValueDelegate del)
		{
			del(this.Value);
		}

		// Token: 0x060004DA RID: 1242 RVA: 0x00009208 File Offset: 0x00007408
		internal override string ToStringInternal()
		{
			return string.Format("PROPERTY[ptag:{0}, {1}{2}, val:{3}]", new object[]
			{
				TraceUtils.DumpPropTag((PropTag)this.PropTag),
				(Restriction.RelOp)this.RelOp,
				this.MultiValued ? " (mv)" : string.Empty,
				this.Value
			});
		}

		// Token: 0x060004DB RID: 1243 RVA: 0x00009263 File Offset: 0x00007463
		internal override int GetApproximateSize()
		{
			return base.GetApproximateSize() + 9 + this.Value.GetApproximateSize();
		}
	}
}
