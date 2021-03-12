using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000057 RID: 87
	[DataContract]
	internal sealed class CommentRestrictionData : HierRestrictionData
	{
		// Token: 0x06000458 RID: 1112 RVA: 0x00008353 File Offset: 0x00006553
		public CommentRestrictionData()
		{
		}

		// Token: 0x17000199 RID: 409
		// (get) Token: 0x06000459 RID: 1113 RVA: 0x0000835B File Offset: 0x0000655B
		// (set) Token: 0x0600045A RID: 1114 RVA: 0x00008363 File Offset: 0x00006563
		[DataMember]
		public PropValueData[] PropValues { get; set; }

		// Token: 0x0600045B RID: 1115 RVA: 0x0000836C File Offset: 0x0000656C
		internal CommentRestrictionData(Restriction.CommentRestriction r)
		{
			base.ParseRestrictions(new Restriction[]
			{
				r.Restriction
			});
			this.PropValues = new PropValueData[r.Values.Length];
			for (int i = 0; i < r.Values.Length; i++)
			{
				this.PropValues[i] = DataConverter<PropValueConverter, PropValue, PropValueData>.GetData(r.Values[i]);
			}
		}

		// Token: 0x0600045C RID: 1116 RVA: 0x000083DC File Offset: 0x000065DC
		internal CommentRestrictionData(StoreSession storeSession, CommentFilter filter)
		{
			base.ParseQueryFilter(storeSession, filter.Filter);
			this.PropValues = new PropValueData[filter.Values.Length];
			for (int i = 0; i < filter.Values.Length; i++)
			{
				this.PropValues[i] = new PropValueData((PropTag)base.GetPropTagFromDefinition(storeSession, filter.Properties[i]), filter.Values[i]);
			}
		}

		// Token: 0x0600045D RID: 1117 RVA: 0x00008448 File Offset: 0x00006648
		internal override Restriction GetRestriction()
		{
			PropValue[] array = new PropValue[this.PropValues.Length];
			for (int i = 0; i < this.PropValues.Length; i++)
			{
				array[i] = DataConverter<PropValueConverter, PropValue, PropValueData>.GetNative(this.PropValues[i]);
			}
			return Restriction.Comment(base.GetRestrictions()[0], array);
		}

		// Token: 0x0600045E RID: 1118 RVA: 0x000084A0 File Offset: 0x000066A0
		internal override QueryFilter GetQueryFilter(StoreSession storeSession)
		{
			NativeStorePropertyDefinition[] array = new NativeStorePropertyDefinition[this.PropValues.Length];
			object[] array2 = new object[this.PropValues.Length];
			for (int i = 0; i < this.PropValues.Length; i++)
			{
				array[i] = base.GetPropertyDefinitionFromPropTag(storeSession, this.PropValues[i].PropTag);
				array2[i] = this.PropValues[i].Value;
			}
			return new CommentFilter(array, array2, base.GetQueryFilters(storeSession)[0]);
		}

		// Token: 0x0600045F RID: 1119 RVA: 0x00008514 File Offset: 0x00006714
		internal override void InternalEnumPropTags(CommonUtils.EnumPropTagDelegate del)
		{
			foreach (PropValueData propValueData in this.PropValues)
			{
				int propTag = propValueData.PropTag;
				del(ref propTag);
				propValueData.PropTag = propTag;
			}
		}

		// Token: 0x06000460 RID: 1120 RVA: 0x00008550 File Offset: 0x00006750
		internal override void InternalEnumPropValues(CommonUtils.EnumPropValueDelegate del)
		{
			foreach (PropValueData pval in this.PropValues)
			{
				del(pval);
			}
		}

		// Token: 0x06000461 RID: 1121 RVA: 0x0000857D File Offset: 0x0000677D
		internal override string ToStringInternal()
		{
			return string.Format("COMMENT[{0}, {1}]", CommonUtils.ConcatEntries<PropValueData>(this.PropValues, null), base.Restrictions[0].ToStringInternal());
		}

		// Token: 0x06000462 RID: 1122 RVA: 0x000085A4 File Offset: 0x000067A4
		internal override int GetApproximateSize()
		{
			int num = base.GetApproximateSize();
			foreach (PropValueData propValueData in this.PropValues)
			{
				num += propValueData.GetApproximateSize();
			}
			return num;
		}
	}
}
