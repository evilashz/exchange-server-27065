using System;
using System.Text;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.RpcClientAccess
{
	// Token: 0x02000204 RID: 516
	internal sealed class PropertyRestriction : Restriction
	{
		// Token: 0x06000B38 RID: 2872 RVA: 0x00023F81 File Offset: 0x00022181
		internal PropertyRestriction(RelationOperator relop, PropertyTag propertyTag, PropertyValue? propertyValue)
		{
			this.relop = relop;
			this.propertyTag = propertyTag;
			this.propertyValue = propertyValue;
		}

		// Token: 0x17000213 RID: 531
		// (get) Token: 0x06000B39 RID: 2873 RVA: 0x00023F9E File Offset: 0x0002219E
		internal override RestrictionType RestrictionType
		{
			get
			{
				return RestrictionType.Property;
			}
		}

		// Token: 0x06000B3A RID: 2874 RVA: 0x00023FA4 File Offset: 0x000221A4
		internal new static PropertyRestriction InternalParse(Reader reader, WireFormatStyle wireFormatStyle, uint depth)
		{
			RelationOperator relationOperator = (RelationOperator)reader.ReadByte();
			PropertyTag propertyTag = reader.ReadPropertyTag();
			return new PropertyRestriction(relationOperator, propertyTag, Restriction.ReadNullablePropertyValue(reader, wireFormatStyle));
		}

		// Token: 0x06000B3B RID: 2875 RVA: 0x00023FCD File Offset: 0x000221CD
		public override void Serialize(Writer writer, Encoding string8Encoding, WireFormatStyle wireFormatStyle)
		{
			base.Serialize(writer, string8Encoding, wireFormatStyle);
			writer.WriteByte((byte)this.relop);
			writer.WriteUInt32(this.propertyTag);
			Restriction.WriteNullablePropertyValue(writer, this.propertyValue, string8Encoding, wireFormatStyle);
		}

		// Token: 0x17000214 RID: 532
		// (get) Token: 0x06000B3C RID: 2876 RVA: 0x00024004 File Offset: 0x00022204
		public RelationOperator RelationOperator
		{
			get
			{
				return this.relop;
			}
		}

		// Token: 0x17000215 RID: 533
		// (get) Token: 0x06000B3D RID: 2877 RVA: 0x0002400C File Offset: 0x0002220C
		public PropertyTag PropertyTag
		{
			get
			{
				return this.propertyTag;
			}
		}

		// Token: 0x17000216 RID: 534
		// (get) Token: 0x06000B3E RID: 2878 RVA: 0x00024014 File Offset: 0x00022214
		public PropertyValue? PropertyValue
		{
			get
			{
				return this.propertyValue;
			}
		}

		// Token: 0x06000B3F RID: 2879 RVA: 0x0002401C File Offset: 0x0002221C
		public override ErrorCode Validate()
		{
			if (this.propertyValue == null)
			{
				return (ErrorCode)2147942487U;
			}
			if (!EnumValidator.IsValidValue<RelationOperator>(this.relop))
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
			return base.Validate();
		}

		// Token: 0x06000B40 RID: 2880 RVA: 0x000240AC File Offset: 0x000222AC
		internal override void ResolveString8Values(Encoding string8Encoding)
		{
			base.ResolveString8Values(string8Encoding);
			if (this.propertyValue != null)
			{
				this.propertyValue.Value.ResolveString8Values(string8Encoding);
			}
		}

		// Token: 0x06000B41 RID: 2881 RVA: 0x000240E8 File Offset: 0x000222E8
		internal override void AppendToString(StringBuilder stringBuilder)
		{
			base.AppendToString(stringBuilder);
			stringBuilder.Append(" [RelOp=").Append(this.RelationOperator);
			stringBuilder.Append(" Tag=").Append(this.PropertyTag.ToString());
			if (this.propertyValue != null)
			{
				stringBuilder.Append(" ").Append(this.propertyValue.Value);
			}
			else
			{
				stringBuilder.Append(" (null)");
			}
			stringBuilder.Append("]");
		}

		// Token: 0x0400066A RID: 1642
		private readonly RelationOperator relop;

		// Token: 0x0400066B RID: 1643
		private readonly PropertyValue? propertyValue;

		// Token: 0x0400066C RID: 1644
		private readonly PropertyTag propertyTag;
	}
}
