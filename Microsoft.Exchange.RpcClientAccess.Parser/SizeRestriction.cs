using System;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess
{
	// Token: 0x02000216 RID: 534
	internal sealed class SizeRestriction : Restriction
	{
		// Token: 0x06000B9B RID: 2971 RVA: 0x00024ECA File Offset: 0x000230CA
		internal SizeRestriction(RelationOperator relop, PropertyTag propertyTag, uint size)
		{
			this.relop = relop;
			this.propertyTag = propertyTag;
			this.size = size;
		}

		// Token: 0x17000233 RID: 563
		// (get) Token: 0x06000B9C RID: 2972 RVA: 0x00024EE7 File Offset: 0x000230E7
		internal override RestrictionType RestrictionType
		{
			get
			{
				return RestrictionType.Size;
			}
		}

		// Token: 0x17000234 RID: 564
		// (get) Token: 0x06000B9D RID: 2973 RVA: 0x00024EEA File Offset: 0x000230EA
		public RelationOperator RelationOperator
		{
			get
			{
				return this.relop;
			}
		}

		// Token: 0x17000235 RID: 565
		// (get) Token: 0x06000B9E RID: 2974 RVA: 0x00024EF2 File Offset: 0x000230F2
		public PropertyTag PropertyTag
		{
			get
			{
				return this.propertyTag;
			}
		}

		// Token: 0x17000236 RID: 566
		// (get) Token: 0x06000B9F RID: 2975 RVA: 0x00024EFA File Offset: 0x000230FA
		public uint Size
		{
			get
			{
				return this.size;
			}
		}

		// Token: 0x06000BA0 RID: 2976 RVA: 0x00024F04 File Offset: 0x00023104
		internal new static SizeRestriction InternalParse(Reader reader, WireFormatStyle wireFormatStyle, uint depth)
		{
			RelationOperator relationOperator = (RelationOperator)reader.ReadByte();
			PropertyTag propertyTag = reader.ReadPropertyTag();
			uint num = reader.ReadUInt32();
			return new SizeRestriction(relationOperator, propertyTag, num);
		}

		// Token: 0x06000BA1 RID: 2977 RVA: 0x00024F2E File Offset: 0x0002312E
		public override void Serialize(Writer writer, Encoding string8Encoding, WireFormatStyle wireFormatStyle)
		{
			base.Serialize(writer, string8Encoding, wireFormatStyle);
			writer.WriteByte((byte)this.relop);
			writer.WritePropertyTag(this.propertyTag);
			writer.WriteUInt32(this.size);
		}

		// Token: 0x06000BA2 RID: 2978 RVA: 0x00024F60 File Offset: 0x00023160
		public override ErrorCode Validate()
		{
			if (this.propertyTag.ElementPropertyType == PropertyType.String8 && this.size > 2147483647U)
			{
				return (ErrorCode)2147746071U;
			}
			if (this.relop > (RelationOperator)255U)
			{
				return (ErrorCode)2147746050U;
			}
			return base.Validate();
		}

		// Token: 0x06000BA3 RID: 2979 RVA: 0x00024FAC File Offset: 0x000231AC
		internal override void AppendToString(StringBuilder stringBuilder)
		{
			base.AppendToString(stringBuilder);
			stringBuilder.Append(" [Tag=").Append(this.PropertyTag.ToString());
			stringBuilder.Append(" Size=0x").Append(this.Size.ToString("X8"));
			stringBuilder.Append(" RelOp=").Append(this.RelationOperator);
			stringBuilder.Append("]");
		}

		// Token: 0x0400068F RID: 1679
		private readonly RelationOperator relop;

		// Token: 0x04000690 RID: 1680
		private readonly PropertyTag propertyTag;

		// Token: 0x04000691 RID: 1681
		private readonly uint size;
	}
}
