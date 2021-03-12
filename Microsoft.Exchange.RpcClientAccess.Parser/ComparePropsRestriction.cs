using System;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess
{
	// Token: 0x02000215 RID: 533
	internal sealed class ComparePropsRestriction : Restriction
	{
		// Token: 0x06000B92 RID: 2962 RVA: 0x00024D91 File Offset: 0x00022F91
		internal ComparePropsRestriction(RelationOperator relop, PropertyTag property1, PropertyTag property2)
		{
			this.relop = relop;
			this.property1 = property1;
			this.property2 = property2;
		}

		// Token: 0x1700022F RID: 559
		// (get) Token: 0x06000B93 RID: 2963 RVA: 0x00024DAE File Offset: 0x00022FAE
		internal override RestrictionType RestrictionType
		{
			get
			{
				return RestrictionType.CompareProps;
			}
		}

		// Token: 0x17000230 RID: 560
		// (get) Token: 0x06000B94 RID: 2964 RVA: 0x00024DB1 File Offset: 0x00022FB1
		public RelationOperator RelationOperator
		{
			get
			{
				return this.relop;
			}
		}

		// Token: 0x17000231 RID: 561
		// (get) Token: 0x06000B95 RID: 2965 RVA: 0x00024DB9 File Offset: 0x00022FB9
		public PropertyTag Property1
		{
			get
			{
				return this.property1;
			}
		}

		// Token: 0x17000232 RID: 562
		// (get) Token: 0x06000B96 RID: 2966 RVA: 0x00024DC1 File Offset: 0x00022FC1
		public PropertyTag Property2
		{
			get
			{
				return this.property2;
			}
		}

		// Token: 0x06000B97 RID: 2967 RVA: 0x00024DCC File Offset: 0x00022FCC
		internal new static ComparePropsRestriction InternalParse(Reader reader, WireFormatStyle wireFormatStyle, uint depth)
		{
			RelationOperator relationOperator = (RelationOperator)reader.ReadByte();
			PropertyTag propertyTag = reader.ReadPropertyTag();
			PropertyTag propertyTag2 = reader.ReadPropertyTag();
			return new ComparePropsRestriction(relationOperator, propertyTag, propertyTag2);
		}

		// Token: 0x06000B98 RID: 2968 RVA: 0x00024DF6 File Offset: 0x00022FF6
		public override void Serialize(Writer writer, Encoding string8Encoding, WireFormatStyle wireFormatStyle)
		{
			base.Serialize(writer, string8Encoding, wireFormatStyle);
			writer.WriteByte((byte)this.relop);
			writer.WritePropertyTag(this.property1);
			writer.WritePropertyTag(this.property2);
		}

		// Token: 0x06000B99 RID: 2969 RVA: 0x00024E26 File Offset: 0x00023026
		public override ErrorCode Validate()
		{
			if ((byte)this.relop > 255)
			{
				return (ErrorCode)2147746050U;
			}
			return base.Validate();
		}

		// Token: 0x06000B9A RID: 2970 RVA: 0x00024E44 File Offset: 0x00023044
		internal override void AppendToString(StringBuilder stringBuilder)
		{
			base.AppendToString(stringBuilder);
			stringBuilder.Append(" [Tag1=").Append(this.Property1.ToString());
			stringBuilder.Append(" Tag2=").Append(this.Property2.ToString());
			stringBuilder.Append(" RelOp=").Append(this.RelationOperator);
			stringBuilder.Append("]");
		}

		// Token: 0x0400068C RID: 1676
		private readonly PropertyTag property1;

		// Token: 0x0400068D RID: 1677
		private readonly PropertyTag property2;

		// Token: 0x0400068E RID: 1678
		private readonly RelationOperator relop;
	}
}
