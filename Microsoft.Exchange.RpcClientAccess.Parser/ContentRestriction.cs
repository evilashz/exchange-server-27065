using System;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess
{
	// Token: 0x0200020D RID: 525
	internal sealed class ContentRestriction : Restriction
	{
		// Token: 0x06000B66 RID: 2918 RVA: 0x000245FB File Offset: 0x000227FB
		internal ContentRestriction(FuzzyLevel fuzzyLevel, PropertyTag propertyTag, PropertyValue? propertyValue)
		{
			this.fuzzyLevel = fuzzyLevel;
			this.propertyTag = propertyTag;
			this.propertyValue = propertyValue;
		}

		// Token: 0x17000220 RID: 544
		// (get) Token: 0x06000B67 RID: 2919 RVA: 0x00024618 File Offset: 0x00022818
		internal override RestrictionType RestrictionType
		{
			get
			{
				return RestrictionType.Content;
			}
		}

		// Token: 0x06000B68 RID: 2920 RVA: 0x0002461C File Offset: 0x0002281C
		internal new static ContentRestriction InternalParse(Reader reader, WireFormatStyle wireFormatStyle, uint depth)
		{
			FuzzyLevel fuzzyLevel = (FuzzyLevel)reader.ReadUInt32();
			PropertyTag propertyTag = reader.ReadPropertyTag();
			return new ContentRestriction(fuzzyLevel, propertyTag, Restriction.ReadNullablePropertyValue(reader, wireFormatStyle));
		}

		// Token: 0x06000B69 RID: 2921 RVA: 0x00024645 File Offset: 0x00022845
		public override void Serialize(Writer writer, Encoding string8Encoding, WireFormatStyle wireFormatStyle)
		{
			base.Serialize(writer, string8Encoding, wireFormatStyle);
			writer.WriteUInt32((uint)this.fuzzyLevel);
			writer.WritePropertyTag(this.propertyTag);
			Restriction.WriteNullablePropertyValue(writer, this.propertyValue, string8Encoding, wireFormatStyle);
		}

		// Token: 0x17000221 RID: 545
		// (get) Token: 0x06000B6A RID: 2922 RVA: 0x00024676 File Offset: 0x00022876
		public PropertyValue? PropertyValue
		{
			get
			{
				return this.propertyValue;
			}
		}

		// Token: 0x17000222 RID: 546
		// (get) Token: 0x06000B6B RID: 2923 RVA: 0x0002467E File Offset: 0x0002287E
		public PropertyTag PropertyTag
		{
			get
			{
				return this.propertyTag;
			}
		}

		// Token: 0x17000223 RID: 547
		// (get) Token: 0x06000B6C RID: 2924 RVA: 0x00024686 File Offset: 0x00022886
		public FuzzyLevel FuzzyLevel
		{
			get
			{
				return this.fuzzyLevel;
			}
		}

		// Token: 0x06000B6D RID: 2925 RVA: 0x00024690 File Offset: 0x00022890
		public override ErrorCode Validate()
		{
			if (this.propertyValue == null)
			{
				return (ErrorCode)2147942487U;
			}
			if (this.PropertyValue.Value.PropertyTag.IsMultiValuedProperty)
			{
				return (ErrorCode)2147746071U;
			}
			if (!PropertyTag.HasCompatiblePropertyType(this.propertyTag, this.PropertyValue.Value.PropertyTag))
			{
				return (ErrorCode)2147746071U;
			}
			if (!this.propertyValue.Value.PropertyTag.IsStringProperty && this.propertyValue.Value.PropertyTag.PropertyType != PropertyType.Binary)
			{
				return (ErrorCode)2147746071U;
			}
			return base.Validate();
		}

		// Token: 0x06000B6E RID: 2926 RVA: 0x0002475C File Offset: 0x0002295C
		internal override void ResolveString8Values(Encoding string8Encoding)
		{
			base.ResolveString8Values(string8Encoding);
			if (this.propertyValue != null)
			{
				this.propertyValue.Value.ResolveString8Values(string8Encoding);
			}
		}

		// Token: 0x06000B6F RID: 2927 RVA: 0x00024798 File Offset: 0x00022998
		internal override void AppendToString(StringBuilder stringBuilder)
		{
			base.AppendToString(stringBuilder);
			stringBuilder.Append(" [Tag=").Append(this.PropertyTag.ToString());
			stringBuilder.Append(" ").Append(this.PropertyValue);
			stringBuilder.Append(" Fuzzy=").Append(this.FuzzyLevel);
			stringBuilder.Append("]");
		}

		// Token: 0x0400067D RID: 1661
		private readonly FuzzyLevel fuzzyLevel;

		// Token: 0x0400067E RID: 1662
		private readonly PropertyValue? propertyValue;

		// Token: 0x0400067F RID: 1663
		private readonly PropertyTag propertyTag;
	}
}
