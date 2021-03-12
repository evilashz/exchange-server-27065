using System;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess
{
	// Token: 0x0200020F RID: 527
	internal sealed class BitMaskRestriction : Restriction
	{
		// Token: 0x06000B70 RID: 2928 RVA: 0x00024815 File Offset: 0x00022A15
		internal BitMaskRestriction(BitMaskOperator bitMaskOperator, PropertyTag propertyTag, uint bitMask)
		{
			this.bitMaskOperator = bitMaskOperator;
			this.propertyTag = propertyTag;
			this.bitMask = bitMask;
		}

		// Token: 0x17000224 RID: 548
		// (get) Token: 0x06000B71 RID: 2929 RVA: 0x00024832 File Offset: 0x00022A32
		internal override RestrictionType RestrictionType
		{
			get
			{
				return RestrictionType.BitMask;
			}
		}

		// Token: 0x06000B72 RID: 2930 RVA: 0x00024838 File Offset: 0x00022A38
		internal new static BitMaskRestriction InternalParse(Reader reader, WireFormatStyle wireFormatStyle, uint depth)
		{
			BitMaskOperator bitMaskOperator = (BitMaskOperator)reader.ReadByte();
			PropertyTag propertyTag = reader.ReadPropertyTag();
			uint num = reader.ReadUInt32();
			return new BitMaskRestriction(bitMaskOperator, propertyTag, num);
		}

		// Token: 0x06000B73 RID: 2931 RVA: 0x00024862 File Offset: 0x00022A62
		public override void Serialize(Writer writer, Encoding string8Encoding, WireFormatStyle wireFormatStyle)
		{
			base.Serialize(writer, string8Encoding, wireFormatStyle);
			writer.WriteByte((byte)this.bitMaskOperator);
			writer.WriteUInt32(this.propertyTag);
			writer.WriteUInt32(this.bitMask);
		}

		// Token: 0x17000225 RID: 549
		// (get) Token: 0x06000B74 RID: 2932 RVA: 0x00024896 File Offset: 0x00022A96
		public PropertyTag PropertyTag
		{
			get
			{
				return this.propertyTag;
			}
		}

		// Token: 0x17000226 RID: 550
		// (get) Token: 0x06000B75 RID: 2933 RVA: 0x0002489E File Offset: 0x00022A9E
		public uint BitMask
		{
			get
			{
				return this.bitMask;
			}
		}

		// Token: 0x17000227 RID: 551
		// (get) Token: 0x06000B76 RID: 2934 RVA: 0x000248A6 File Offset: 0x00022AA6
		public BitMaskOperator BitMaskOperator
		{
			get
			{
				return this.bitMaskOperator;
			}
		}

		// Token: 0x06000B77 RID: 2935 RVA: 0x000248B0 File Offset: 0x00022AB0
		internal override void AppendToString(StringBuilder stringBuilder)
		{
			base.AppendToString(stringBuilder);
			stringBuilder.Append(" [Operator=").Append(this.BitMaskOperator);
			stringBuilder.Append(" Tag=").Append(this.PropertyTag.ToString());
			stringBuilder.Append(" Mask=0x").Append(this.BitMask.ToString("X08"));
			stringBuilder.Append("]");
		}

		// Token: 0x04000683 RID: 1667
		private readonly BitMaskOperator bitMaskOperator;

		// Token: 0x04000684 RID: 1668
		private readonly uint bitMask;

		// Token: 0x04000685 RID: 1669
		private readonly PropertyTag propertyTag;
	}
}
