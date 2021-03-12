using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200005B RID: 91
	[DataContract]
	internal sealed class ExistRestrictionData : RestrictionData
	{
		// Token: 0x0600048B RID: 1163 RVA: 0x00008B12 File Offset: 0x00006D12
		public ExistRestrictionData()
		{
		}

		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x0600048C RID: 1164 RVA: 0x00008B1A File Offset: 0x00006D1A
		// (set) Token: 0x0600048D RID: 1165 RVA: 0x00008B22 File Offset: 0x00006D22
		[DataMember]
		public int PropTag { get; set; }

		// Token: 0x0600048E RID: 1166 RVA: 0x00008B2B File Offset: 0x00006D2B
		internal ExistRestrictionData(Restriction.ExistRestriction r)
		{
			this.PropTag = (int)r.Tag;
		}

		// Token: 0x0600048F RID: 1167 RVA: 0x00008B3F File Offset: 0x00006D3F
		internal ExistRestrictionData(StoreSession storeSession, ExistsFilter filter)
		{
			this.PropTag = base.GetPropTagFromDefinition(storeSession, filter.Property);
		}

		// Token: 0x06000490 RID: 1168 RVA: 0x00008B5A File Offset: 0x00006D5A
		internal override Restriction GetRestriction()
		{
			return Restriction.Exist((PropTag)this.PropTag);
		}

		// Token: 0x06000491 RID: 1169 RVA: 0x00008B67 File Offset: 0x00006D67
		internal override QueryFilter GetQueryFilter(StoreSession storeSession)
		{
			return new ExistsFilter(base.GetPropertyDefinitionFromPropTag(storeSession, this.PropTag));
		}

		// Token: 0x06000492 RID: 1170 RVA: 0x00008B7C File Offset: 0x00006D7C
		internal override void InternalEnumPropTags(CommonUtils.EnumPropTagDelegate del)
		{
			int propTag = this.PropTag;
			del(ref propTag);
			this.PropTag = propTag;
		}

		// Token: 0x06000493 RID: 1171 RVA: 0x00008B9F File Offset: 0x00006D9F
		internal override string ToStringInternal()
		{
			return string.Format("EXIST[ptag:{0}]", TraceUtils.DumpPropTag((PropTag)this.PropTag));
		}

		// Token: 0x06000494 RID: 1172 RVA: 0x00008BB6 File Offset: 0x00006DB6
		internal override int GetApproximateSize()
		{
			return base.GetApproximateSize() + 4;
		}
	}
}
