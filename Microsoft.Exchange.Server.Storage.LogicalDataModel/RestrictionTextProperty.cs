using System;
using System.Text;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.Common.ExtensionMethods;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;
using Microsoft.Exchange.Server.Storage.PropTags;
using Microsoft.Exchange.Server.Storage.StoreCommonServices;

namespace Microsoft.Exchange.Server.Storage.LogicalDataModel
{
	// Token: 0x020000C8 RID: 200
	public sealed class RestrictionTextProperty : Restriction
	{
		// Token: 0x06000ABB RID: 2747 RVA: 0x00053FB2 File Offset: 0x000521B2
		public RestrictionTextProperty(StorePropTag propertyTag, object value, RestrictionTextFuzzyLevel fuzzyLevel, RestrictionTextFullness fullness)
		{
			this.propertyTag = propertyTag;
			this.value = value;
			this.fullness = fullness;
			this.fuzzyLevel = fuzzyLevel;
		}

		// Token: 0x06000ABC RID: 2748 RVA: 0x00053FD8 File Offset: 0x000521D8
		public RestrictionTextProperty(Context context, byte[] byteForm, ref int position, int posMax, Mailbox mailbox, ObjectType objectType)
		{
			ParseSerialize.GetByte(byteForm, ref position, posMax);
			this.fullness = (RestrictionTextFullness)ParseSerialize.GetWord(byteForm, ref position, posMax);
			this.fuzzyLevel = (RestrictionTextFuzzyLevel)ParseSerialize.GetWord(byteForm, ref position, posMax);
			this.propertyTag = Mailbox.GetStorePropTag(context, mailbox, ParseSerialize.GetDword(byteForm, ref position, posMax), objectType);
			StorePropTag storePropTag = Mailbox.GetStorePropTag(context, mailbox, ParseSerialize.GetDword(byteForm, ref position, posMax), objectType);
			this.value = Restriction.DeserializeValue(byteForm, ref position, storePropTag);
			if (!Restriction.FSamePropType(storePropTag.PropType, this.propertyTag.PropType))
			{
				throw new StoreException((LID)38904U, ErrorCodeValue.TooComplex);
			}
			if (storePropTag.PropType != PropertyType.Unicode)
			{
				DiagnosticContext.TraceDword((LID)37736U, storePropTag.PropTag);
				throw new StoreException((LID)55288U, ErrorCodeValue.TooComplex);
			}
		}

		// Token: 0x1700024D RID: 589
		// (get) Token: 0x06000ABD RID: 2749 RVA: 0x000540B2 File Offset: 0x000522B2
		public StorePropTag PropertyTag
		{
			get
			{
				return this.propertyTag;
			}
		}

		// Token: 0x1700024E RID: 590
		// (get) Token: 0x06000ABE RID: 2750 RVA: 0x000540BA File Offset: 0x000522BA
		public object Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x1700024F RID: 591
		// (get) Token: 0x06000ABF RID: 2751 RVA: 0x000540C2 File Offset: 0x000522C2
		public RestrictionTextFuzzyLevel FuzzyLevel
		{
			get
			{
				return this.fuzzyLevel;
			}
		}

		// Token: 0x17000250 RID: 592
		// (get) Token: 0x06000AC0 RID: 2752 RVA: 0x000540CA File Offset: 0x000522CA
		public RestrictionTextFullness Fullness
		{
			get
			{
				return this.fullness;
			}
		}

		// Token: 0x06000AC1 RID: 2753 RVA: 0x000540D4 File Offset: 0x000522D4
		internal override void AppendToString(StringBuilder sb)
		{
			sb.Append("TEXT([");
			this.propertyTag.AppendToString(sb);
			sb.Append("], ");
			sb.AppendAsString(this.value);
			sb.Append(", ");
			sb.Append(this.fullness.ToString());
			sb.Append(", ");
			sb.Append(this.fuzzyLevel.ToString());
			sb.Append(')');
		}

		// Token: 0x06000AC2 RID: 2754 RVA: 0x00054164 File Offset: 0x00052364
		public override void Serialize(byte[] byteForm, ref int position)
		{
			ParseSerialize.SetByte(byteForm, ref position, 3);
			ParseSerialize.SetWord(byteForm, ref position, (ushort)this.fullness);
			ParseSerialize.SetWord(byteForm, ref position, (ushort)this.fuzzyLevel);
			ParseSerialize.SetDword(byteForm, ref position, this.propertyTag.ExternalTag);
			StorePropTag propTag = this.propertyTag;
			if (propTag.IsMultiValued && !(this.value is string[]))
			{
				propTag = StorePropTag.CreateWithoutInfo(this.propertyTag.PropId, this.propertyTag.PropType & (PropertyType)61439, this.propertyTag.BaseObjectType);
			}
			ParseSerialize.SetDword(byteForm, ref position, propTag.ExternalTag);
			Restriction.SerializeValue(byteForm, ref position, propTag, this.value);
		}

		// Token: 0x06000AC3 RID: 2755 RVA: 0x0005421C File Offset: 0x0005241C
		public override SearchCriteria ToSearchCriteria(StoreDatabase database, ObjectType objectType)
		{
			Column lhs = PropertySchema.MapToColumn(database, objectType, this.propertyTag);
			Column rhs = Factory.CreateConstantColumn(this.value);
			SearchCriteriaText.SearchTextFullness searchTextFullness = RestrictionTextProperty.GetSearchTextFullness(this.fullness);
			SearchCriteriaText.SearchTextFuzzyLevel searchTextFuzzyLevel = RestrictionTextProperty.GetSearchTextFuzzyLevel(this.fuzzyLevel);
			return Factory.CreateSearchCriteriaText(lhs, searchTextFullness, searchTextFuzzyLevel, rhs);
		}

		// Token: 0x06000AC4 RID: 2756 RVA: 0x00054264 File Offset: 0x00052464
		public override bool RefersToProperty(StorePropTag propTag)
		{
			return this.propertyTag == propTag;
		}

		// Token: 0x06000AC5 RID: 2757 RVA: 0x00054272 File Offset: 0x00052472
		private static SearchCriteriaText.SearchTextFullness GetSearchTextFullness(RestrictionTextFullness fullness)
		{
			return (SearchCriteriaText.SearchTextFullness)fullness;
		}

		// Token: 0x06000AC6 RID: 2758 RVA: 0x00054276 File Offset: 0x00052476
		private static SearchCriteriaText.SearchTextFuzzyLevel GetSearchTextFuzzyLevel(RestrictionTextFuzzyLevel fuzzyLevel)
		{
			return (SearchCriteriaText.SearchTextFuzzyLevel)fuzzyLevel;
		}

		// Token: 0x0400050A RID: 1290
		private readonly StorePropTag propertyTag;

		// Token: 0x0400050B RID: 1291
		private readonly object value;

		// Token: 0x0400050C RID: 1292
		private readonly RestrictionTextFuzzyLevel fuzzyLevel;

		// Token: 0x0400050D RID: 1293
		private readonly RestrictionTextFullness fullness;
	}
}
