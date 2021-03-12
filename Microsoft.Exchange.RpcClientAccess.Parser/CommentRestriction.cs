using System;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess
{
	// Token: 0x02000214 RID: 532
	internal sealed class CommentRestriction : SingleRestriction
	{
		// Token: 0x06000B8A RID: 2954 RVA: 0x00024B80 File Offset: 0x00022D80
		internal CommentRestriction(PropertyValue[] propertyValues, Restriction childRestriction) : base(childRestriction)
		{
			this.propertyValues = propertyValues;
		}

		// Token: 0x1700022D RID: 557
		// (get) Token: 0x06000B8B RID: 2955 RVA: 0x00024B90 File Offset: 0x00022D90
		internal override RestrictionType RestrictionType
		{
			get
			{
				return RestrictionType.Comment;
			}
		}

		// Token: 0x1700022E RID: 558
		// (get) Token: 0x06000B8C RID: 2956 RVA: 0x00024B94 File Offset: 0x00022D94
		public PropertyValue[] PropertyValues
		{
			get
			{
				return this.propertyValues;
			}
		}

		// Token: 0x06000B8D RID: 2957 RVA: 0x00024B9C File Offset: 0x00022D9C
		internal new static CommentRestriction InternalParse(Reader reader, WireFormatStyle wireFormatStyle, uint depth)
		{
			byte b = reader.ReadByte();
			reader.CheckBoundary((uint)b, 4U);
			PropertyValue[] array = new PropertyValue[(int)b];
			for (byte b2 = 0; b2 < b; b2 += 1)
			{
				array[(int)b2] = reader.ReadPropertyValue(wireFormatStyle);
			}
			Restriction childRestriction = null;
			if (reader.ReadByte() != 0)
			{
				childRestriction = Restriction.InternalParse(reader, wireFormatStyle, depth);
			}
			return new CommentRestriction(array, childRestriction);
		}

		// Token: 0x06000B8E RID: 2958 RVA: 0x00024BFC File Offset: 0x00022DFC
		public override void Serialize(Writer writer, Encoding string8Encoding, WireFormatStyle wireFormatStyle)
		{
			base.Serialize(writer, string8Encoding, wireFormatStyle);
			writer.WriteByte((byte)this.propertyValues.Length);
			foreach (PropertyValue value in this.propertyValues)
			{
				writer.WritePropertyValue(value, string8Encoding, wireFormatStyle);
			}
			writer.WriteBool(base.ChildRestriction != null, 1);
			if (base.ChildRestriction != null)
			{
				base.ChildRestriction.Serialize(writer, string8Encoding, wireFormatStyle);
			}
		}

		// Token: 0x06000B8F RID: 2959 RVA: 0x00024C78 File Offset: 0x00022E78
		public override ErrorCode Validate()
		{
			if (this.propertyValues.Length > 255)
			{
				return (ErrorCode)2147746050U;
			}
			foreach (PropertyValue propertyValue in this.propertyValues)
			{
				if ((propertyValue.PropertyTag.PropertyType & (PropertyType)12288) != PropertyType.Unspecified)
				{
					return (ErrorCode)2147746071U;
				}
			}
			return base.Validate();
		}

		// Token: 0x06000B90 RID: 2960 RVA: 0x00024CE4 File Offset: 0x00022EE4
		internal override void ResolveString8Values(Encoding string8Encoding)
		{
			base.ResolveString8Values(string8Encoding);
			foreach (PropertyValue propertyValue in this.propertyValues)
			{
				propertyValue.ResolveString8Values(string8Encoding);
			}
		}

		// Token: 0x06000B91 RID: 2961 RVA: 0x00024D24 File Offset: 0x00022F24
		internal override void AppendToString(StringBuilder stringBuilder)
		{
			base.AppendToString(stringBuilder);
			stringBuilder.Append(" [Values=[");
			Util.AppendToString<PropertyValue>(stringBuilder, this.PropertyValues);
			stringBuilder.Append("]");
			if (base.ChildRestriction != null)
			{
				stringBuilder.Append(" Child=[").Append(base.ChildRestriction).Append("]");
			}
			stringBuilder.Append("]");
		}

		// Token: 0x0400068B RID: 1675
		private readonly PropertyValue[] propertyValues;
	}
}
