using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000056 RID: 86
	[DataContract]
	internal sealed class BitMaskRestrictionData : RestrictionData
	{
		// Token: 0x0600044A RID: 1098 RVA: 0x00008219 File Offset: 0x00006419
		public BitMaskRestrictionData()
		{
		}

		// Token: 0x17000196 RID: 406
		// (get) Token: 0x0600044B RID: 1099 RVA: 0x00008221 File Offset: 0x00006421
		// (set) Token: 0x0600044C RID: 1100 RVA: 0x00008229 File Offset: 0x00006429
		[DataMember]
		public int RelBmr { get; set; }

		// Token: 0x17000197 RID: 407
		// (get) Token: 0x0600044D RID: 1101 RVA: 0x00008232 File Offset: 0x00006432
		// (set) Token: 0x0600044E RID: 1102 RVA: 0x0000823A File Offset: 0x0000643A
		[DataMember]
		public int PropTag { get; set; }

		// Token: 0x17000198 RID: 408
		// (get) Token: 0x0600044F RID: 1103 RVA: 0x00008243 File Offset: 0x00006443
		// (set) Token: 0x06000450 RID: 1104 RVA: 0x0000824B File Offset: 0x0000644B
		[DataMember]
		public int Mask { get; set; }

		// Token: 0x06000451 RID: 1105 RVA: 0x00008254 File Offset: 0x00006454
		internal BitMaskRestrictionData(Restriction.BitMaskRestriction r)
		{
			this.RelBmr = (int)r.Bmr;
			this.PropTag = (int)r.Tag;
			this.Mask = r.Mask;
		}

		// Token: 0x06000452 RID: 1106 RVA: 0x00008280 File Offset: 0x00006480
		internal BitMaskRestrictionData(StoreSession storeSession, BitMaskFilter bitMaskFilter)
		{
			this.RelBmr = (bitMaskFilter.IsNonZero ? 1 : 0);
			this.PropTag = base.GetPropTagFromDefinition(storeSession, bitMaskFilter.Property);
			this.Mask = (int)bitMaskFilter.Mask;
		}

		// Token: 0x06000453 RID: 1107 RVA: 0x000082BA File Offset: 0x000064BA
		internal override Restriction GetRestriction()
		{
			return new Restriction.BitMaskRestriction((Restriction.RelBmr)this.RelBmr, (PropTag)this.PropTag, this.Mask);
		}

		// Token: 0x06000454 RID: 1108 RVA: 0x000082D3 File Offset: 0x000064D3
		internal override QueryFilter GetQueryFilter(StoreSession storeSession)
		{
			return new BitMaskFilter(base.GetPropertyDefinitionFromPropTag(storeSession, this.PropTag), (ulong)((long)this.Mask), this.RelBmr == 1);
		}

		// Token: 0x06000455 RID: 1109 RVA: 0x000082F8 File Offset: 0x000064F8
		internal override void InternalEnumPropTags(CommonUtils.EnumPropTagDelegate del)
		{
			int propTag = this.PropTag;
			del(ref propTag);
			this.PropTag = propTag;
		}

		// Token: 0x06000456 RID: 1110 RVA: 0x0000831B File Offset: 0x0000651B
		internal override string ToStringInternal()
		{
			return string.Format("BITMASK[ptag:{0}, {1}, mask:0x{2:X}]", TraceUtils.DumpPropTag((PropTag)this.PropTag), (Restriction.RelBmr)this.RelBmr, this.Mask);
		}

		// Token: 0x06000457 RID: 1111 RVA: 0x00008348 File Offset: 0x00006548
		internal override int GetApproximateSize()
		{
			return base.GetApproximateSize() + 12;
		}
	}
}
