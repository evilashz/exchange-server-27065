using System;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x020002D3 RID: 723
	internal sealed class RopGetPropertiesSpecific : InputRop
	{
		// Token: 0x17000308 RID: 776
		// (get) Token: 0x060010AD RID: 4269 RVA: 0x0002EDC9 File Offset: 0x0002CFC9
		internal PropertyTag[] PropertyTags
		{
			get
			{
				return this.propertyTags;
			}
		}

		// Token: 0x17000309 RID: 777
		// (get) Token: 0x060010AE RID: 4270 RVA: 0x0002EDD1 File Offset: 0x0002CFD1
		internal override RopId RopId
		{
			get
			{
				return RopId.GetPropertiesSpecific;
			}
		}

		// Token: 0x060010AF RID: 4271 RVA: 0x0002EDD4 File Offset: 0x0002CFD4
		internal static Rop CreateRop()
		{
			return new RopGetPropertiesSpecific();
		}

		// Token: 0x060010B0 RID: 4272 RVA: 0x0002EDDB File Offset: 0x0002CFDB
		protected override IResultFactory GetDefaultResultFactory(IConnectionInformation connection, ArraySegment<byte> outputBuffer)
		{
			return new GetPropertiesSpecificResultFactory(outputBuffer.Count, connection.String8Encoding);
		}

		// Token: 0x060010B1 RID: 4273 RVA: 0x0002EDEF File Offset: 0x0002CFEF
		protected override void InternalParseInput(Reader reader, ServerObjectHandleTable serverObjectHandleTable)
		{
			base.InternalParseInput(reader, serverObjectHandleTable);
			this.streamLimit = reader.ReadUInt16();
			this.flags = ((reader.ReadUInt16() == 0) ? GetPropertiesFlags.None : ((GetPropertiesFlags)int.MinValue));
			this.propertyTags = reader.ReadCountAndPropertyTagArray(FieldLength.WordSize);
		}

		// Token: 0x060010B2 RID: 4274 RVA: 0x0002EE2A File Offset: 0x0002D02A
		protected override void InternalSerializeOutput(Writer writer)
		{
			base.InternalSerializeOutput(writer);
			this.result.Serialize(writer);
		}

		// Token: 0x060010B3 RID: 4275 RVA: 0x0002EE40 File Offset: 0x0002D040
		protected override void InternalExecute(IServerObject serverObject, IRopHandler ropHandler, ArraySegment<byte> outputBuffer)
		{
			GetPropertiesSpecificResultFactory resultFactory = new GetPropertiesSpecificResultFactory(outputBuffer.Count, serverObject.String8Encoding);
			this.result = ropHandler.GetPropertiesSpecific(serverObject, this.streamLimit, this.flags, this.propertyTags, resultFactory);
		}

		// Token: 0x060010B4 RID: 4276 RVA: 0x0002EE80 File Offset: 0x0002D080
		internal void SetInput(byte logonIndex, byte handleTableIndex, ushort streamLimit, GetPropertiesFlags flags, PropertyTag[] propertyTags)
		{
			base.SetCommonInput(logonIndex, handleTableIndex);
			this.streamLimit = streamLimit;
			this.flags = flags;
			this.propertyTags = propertyTags;
		}

		// Token: 0x060010B5 RID: 4277 RVA: 0x0002EEA1 File Offset: 0x0002D0A1
		protected override void InternalSerializeInput(Writer writer, Encoding string8Encoding)
		{
			base.InternalSerializeInput(writer, string8Encoding);
			writer.WriteUInt16(this.streamLimit);
			writer.WriteUInt16((this.flags == GetPropertiesFlags.None) ? 0 : 1);
			writer.WriteCountAndPropertyTagArray(this.PropertyTags, FieldLength.WordSize);
		}

		// Token: 0x060010B6 RID: 4278 RVA: 0x0002EED9 File Offset: 0x0002D0D9
		internal void SetParseOutputData(PropertyTag[] parserPropertyTags)
		{
			Util.ThrowOnNullArgument(parserPropertyTags, "parserPropertyTags");
			this.parserPropertyTags = parserPropertyTags;
		}

		// Token: 0x060010B7 RID: 4279 RVA: 0x0002EF10 File Offset: 0x0002D110
		protected override void InternalParseOutput(Reader reader, Encoding string8Encoding)
		{
			if (this.parserPropertyTags == null)
			{
				throw new InvalidOperationException("SetParseOutputData must be called before ParseOutput.");
			}
			base.InternalParseOutput(reader, string8Encoding);
			this.result = RopResult.Parse(reader, (Reader readerParameter) => new SuccessfulGetPropertiesSpecificResult(readerParameter, this.parserPropertyTags, string8Encoding), new RopResult.ResultParserDelegate(StandardRopResult.ParseFailResult));
		}

		// Token: 0x060010B8 RID: 4280 RVA: 0x0002EF78 File Offset: 0x0002D178
		internal override void AppendToString(StringBuilder stringBuilder)
		{
			base.AppendToString(stringBuilder);
			stringBuilder.Append(" Flags=").Append(this.flags);
			stringBuilder.Append(" StreamLimit=").Append(this.streamLimit);
			stringBuilder.Append(" Tags=[");
			Util.AppendToString<PropertyTag>(stringBuilder, this.propertyTags);
			stringBuilder.Append("]");
		}

		// Token: 0x0400083A RID: 2106
		private const RopId RopType = RopId.GetPropertiesSpecific;

		// Token: 0x0400083B RID: 2107
		private PropertyTag[] propertyTags;

		// Token: 0x0400083C RID: 2108
		private ushort streamLimit;

		// Token: 0x0400083D RID: 2109
		private GetPropertiesFlags flags;

		// Token: 0x0400083E RID: 2110
		private PropertyTag[] parserPropertyTags;
	}
}
