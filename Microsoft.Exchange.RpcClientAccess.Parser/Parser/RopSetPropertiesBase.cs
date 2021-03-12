using System;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x0200033C RID: 828
	internal abstract class RopSetPropertiesBase : InputRop
	{
		// Token: 0x17000371 RID: 881
		// (get) Token: 0x060013D7 RID: 5079 RVA: 0x00034EE6 File Offset: 0x000330E6
		internal PropertyValue[] Properties
		{
			get
			{
				return this.propertyValues;
			}
		}

		// Token: 0x060013D8 RID: 5080 RVA: 0x00034EEE File Offset: 0x000330EE
		internal override void AppendToString(StringBuilder stringBuilder)
		{
			base.AppendToString(stringBuilder);
			stringBuilder.Append(" Properties=[");
			Util.AppendToString<PropertyValue>(stringBuilder, this.propertyValues);
			stringBuilder.Append("]");
		}

		// Token: 0x060013D9 RID: 5081 RVA: 0x00034F1B File Offset: 0x0003311B
		internal void SetInput(byte logonIndex, byte handleTableIndex, PropertyValue[] propertyValues)
		{
			base.SetCommonInput(logonIndex, handleTableIndex);
			this.propertyValues = propertyValues;
		}

		// Token: 0x060013DA RID: 5082 RVA: 0x00034F2C File Offset: 0x0003312C
		protected override void InternalSerializeInput(Writer writer, Encoding string8Encoding)
		{
			base.InternalSerializeInput(writer, string8Encoding);
			long position = writer.Position;
			writer.WriteUInt16(0);
			long position2 = writer.Position;
			writer.WriteCountAndPropertyValueList(this.propertyValues, string8Encoding, WireFormatStyle.Rop);
			long position3 = writer.Position;
			writer.Position = position;
			writer.WriteUInt16((ushort)(position3 - position2));
			writer.Position = position3;
		}

		// Token: 0x060013DB RID: 5083 RVA: 0x00034F83 File Offset: 0x00033183
		protected override void InternalParseOutput(Reader reader, Encoding string8Encoding)
		{
			base.InternalParseOutput(reader, string8Encoding);
			this.result = RopResult.Parse(reader, new RopResult.ResultParserDelegate(SuccessfulSetPropertiesResult.Parse), new RopResult.ResultParserDelegate(StandardRopResult.ParseFailResult));
		}

		// Token: 0x060013DC RID: 5084 RVA: 0x00034FB4 File Offset: 0x000331B4
		protected override void InternalParseInput(Reader reader, ServerObjectHandleTable serverObjectHandleTable)
		{
			base.InternalParseInput(reader, serverObjectHandleTable);
			uint num = (uint)reader.ReadUInt16();
			reader.CheckBoundary(num, 1U);
			long position = reader.Position;
			int num2 = (int)reader.ReadUInt16();
			reader.CheckBoundary((uint)num2, 4U);
			this.propertyValues = new PropertyValue[num2];
			for (int i = 0; i < num2; i++)
			{
				this.propertyValues[i] = reader.ReadPropertyValue(WireFormatStyle.Rop);
			}
			long position2 = reader.Position;
			ulong num3 = (ulong)(position2 - position);
			if (num3 != (ulong)num)
			{
				throw new BufferParseException(string.Format("Size of PropertyValue[] reported incorrectly by client.  Size = {0}, Actual = {1}.", num, num3));
			}
		}

		// Token: 0x060013DD RID: 5085 RVA: 0x0003504F File Offset: 0x0003324F
		protected override void InternalSerializeOutput(Writer writer)
		{
			base.InternalSerializeOutput(writer);
			this.result.Serialize(writer);
		}

		// Token: 0x060013DE RID: 5086 RVA: 0x00035064 File Offset: 0x00033264
		internal override void ResolveString8Values(Encoding string8Encoding)
		{
			base.ResolveString8Values(string8Encoding);
			foreach (PropertyValue propertyValue in this.propertyValues)
			{
				propertyValue.ResolveString8Values(string8Encoding);
			}
		}

		// Token: 0x04000A88 RID: 2696
		private PropertyValue[] propertyValues;
	}
}
