using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000059 RID: 89
	[DataContract]
	internal sealed class ContentRestrictionData : RestrictionData
	{
		// Token: 0x06000471 RID: 1137 RVA: 0x00008739 File Offset: 0x00006939
		public ContentRestrictionData()
		{
		}

		// Token: 0x1700019D RID: 413
		// (get) Token: 0x06000472 RID: 1138 RVA: 0x00008741 File Offset: 0x00006941
		// (set) Token: 0x06000473 RID: 1139 RVA: 0x00008749 File Offset: 0x00006949
		[DataMember]
		public int Flags { get; set; }

		// Token: 0x1700019E RID: 414
		// (get) Token: 0x06000474 RID: 1140 RVA: 0x00008752 File Offset: 0x00006952
		// (set) Token: 0x06000475 RID: 1141 RVA: 0x0000875A File Offset: 0x0000695A
		[DataMember]
		public int PropTag { get; set; }

		// Token: 0x1700019F RID: 415
		// (get) Token: 0x06000476 RID: 1142 RVA: 0x00008763 File Offset: 0x00006963
		// (set) Token: 0x06000477 RID: 1143 RVA: 0x0000876B File Offset: 0x0000696B
		[DataMember]
		public bool MultiValued { get; set; }

		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x06000478 RID: 1144 RVA: 0x00008774 File Offset: 0x00006974
		// (set) Token: 0x06000479 RID: 1145 RVA: 0x0000877C File Offset: 0x0000697C
		[DataMember]
		public PropValueData Value { get; set; }

		// Token: 0x0600047A RID: 1146 RVA: 0x00008785 File Offset: 0x00006985
		internal ContentRestrictionData(Restriction.ContentRestriction r)
		{
			this.Flags = (int)r.Flags;
			this.PropTag = (int)r.PropTag;
			this.MultiValued = r.MultiValued;
			this.Value = DataConverter<PropValueConverter, PropValue, PropValueData>.GetData(r.PropValue);
		}

		// Token: 0x0600047B RID: 1147 RVA: 0x000087C4 File Offset: 0x000069C4
		internal ContentRestrictionData(StoreSession storeSession, ContentFilter contentFilter)
		{
			this.Flags = (int)base.GetContentFlags(contentFilter.MatchFlags, contentFilter.MatchOptions);
			this.PropTag = base.GetPropTagFromDefinition(storeSession, contentFilter.Property);
			this.MultiValued = ((PropTag)this.PropTag).IsMultiValued();
			if (contentFilter is TextFilter)
			{
				TextFilter textFilter = (TextFilter)contentFilter;
				this.Value = new PropValueData(((PropTag)this.PropTag).ChangePropType(PropType.String), textFilter.Text);
				return;
			}
			if (contentFilter is BinaryFilter)
			{
				BinaryFilter binaryFilter = (BinaryFilter)contentFilter;
				this.Value = new PropValueData(((PropTag)this.PropTag).ChangePropType(PropType.Binary), binaryFilter.BinaryData);
				return;
			}
			MrsTracer.Common.Error("Unknown content filter type '{0}' in content restriction data constructor", new object[]
			{
				contentFilter.GetType()
			});
			throw new CorruptRestrictionDataException();
		}

		// Token: 0x0600047C RID: 1148 RVA: 0x00008898 File Offset: 0x00006A98
		internal override Restriction GetRestriction()
		{
			return new Restriction.ContentRestriction((PropTag)this.PropTag, this.MultiValued, DataConverter<PropValueConverter, PropValue, PropValueData>.GetNative(this.Value).Value, (ContentFlags)this.Flags);
		}

		// Token: 0x0600047D RID: 1149 RVA: 0x000088D0 File Offset: 0x00006AD0
		internal override QueryFilter GetQueryFilter(StoreSession storeSession)
		{
			PropertyDefinition propertyDefinitionFromPropTag = base.GetPropertyDefinitionFromPropTag(storeSession, this.PropTag);
			MatchFlags matchFlags;
			MatchOptions matchOptions;
			base.GetMatchFlagsAndOptions((ContentFlags)this.Flags, out matchFlags, out matchOptions);
			if (this.Value.Value is string)
			{
				return new TextFilter(propertyDefinitionFromPropTag, (string)this.Value.Value, matchOptions, matchFlags);
			}
			if (this.Value.Value is byte[])
			{
				return new BinaryFilter(propertyDefinitionFromPropTag, (byte[])this.Value.Value, matchOptions, matchFlags);
			}
			MrsTracer.Common.Error("Unknown value type '{0}' in content filter.", new object[]
			{
				this.Value.Value.GetType()
			});
			throw new CorruptRestrictionDataException();
		}

		// Token: 0x0600047E RID: 1150 RVA: 0x00008980 File Offset: 0x00006B80
		internal override void InternalEnumPropTags(CommonUtils.EnumPropTagDelegate del)
		{
			int propTag = this.PropTag;
			del(ref propTag);
			this.PropTag = propTag;
		}

		// Token: 0x0600047F RID: 1151 RVA: 0x000089A3 File Offset: 0x00006BA3
		internal override void InternalEnumPropValues(CommonUtils.EnumPropValueDelegate del)
		{
			del(this.Value);
		}

		// Token: 0x06000480 RID: 1152 RVA: 0x000089B4 File Offset: 0x00006BB4
		internal override string ToStringInternal()
		{
			return string.Format("CONTENT[ptag:{0}, {1}{2}, val:{3}]", new object[]
			{
				TraceUtils.DumpPropTag((PropTag)this.PropTag),
				(ContentFlags)this.Flags,
				this.MultiValued ? " (mv)" : string.Empty,
				this.Value
			});
		}

		// Token: 0x06000481 RID: 1153 RVA: 0x00008A0F File Offset: 0x00006C0F
		internal override int GetApproximateSize()
		{
			return base.GetApproximateSize() + 9 + this.Value.GetApproximateSize();
		}
	}
}
