using System;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x0200031E RID: 798
	internal sealed class RopReadRecipients : InputRop
	{
		// Token: 0x17000358 RID: 856
		// (get) Token: 0x060012DD RID: 4829 RVA: 0x00033272 File Offset: 0x00031472
		internal override RopId RopId
		{
			get
			{
				return RopId.ReadRecipients;
			}
		}

		// Token: 0x060012DE RID: 4830 RVA: 0x00033276 File Offset: 0x00031476
		internal static Rop CreateRop()
		{
			return new RopReadRecipients();
		}

		// Token: 0x060012DF RID: 4831 RVA: 0x0003327D File Offset: 0x0003147D
		internal void SetInput(byte logonIndex, byte handleTableIndex, uint recipientRowId, PropertyTag[] extraUnicodePropertyTags)
		{
			base.SetCommonInput(logonIndex, handleTableIndex);
			this.recipientRowId = recipientRowId;
			this.extraUnicodePropertyTags = extraUnicodePropertyTags;
		}

		// Token: 0x060012E0 RID: 4832 RVA: 0x00033296 File Offset: 0x00031496
		protected override void InternalSerializeInput(Writer writer, Encoding string8Encoding)
		{
			base.InternalSerializeInput(writer, string8Encoding);
			writer.WriteUInt32(this.recipientRowId);
			writer.WriteCountAndPropertyTagArray(this.extraUnicodePropertyTags, FieldLength.WordSize);
		}

		// Token: 0x060012E1 RID: 4833 RVA: 0x000332B9 File Offset: 0x000314B9
		internal void SetParseOutputData(PropertyTag[] extraPropertyTags)
		{
			Util.ThrowOnNullArgument(extraPropertyTags, "extraPropertyTags");
			this.extraPropertyTags = extraPropertyTags;
		}

		// Token: 0x060012E2 RID: 4834 RVA: 0x000332DC File Offset: 0x000314DC
		protected override void InternalParseOutput(Reader reader, Encoding string8Encoding)
		{
			if (this.extraPropertyTags == null)
			{
				throw new InvalidOperationException("SetParseOutputData must be called before ParseOutput.");
			}
			base.InternalParseOutput(reader, string8Encoding);
			this.result = RopResult.Parse(reader, (Reader readerParameter) => new SuccessfulReadRecipientsResult(readerParameter, this.extraPropertyTags), new RopResult.ResultParserDelegate(StandardRopResult.ParseFailResult));
		}

		// Token: 0x060012E3 RID: 4835 RVA: 0x00033328 File Offset: 0x00031528
		protected override IResultFactory GetDefaultResultFactory(IConnectionInformation connection, ArraySegment<byte> outputBuffer)
		{
			return new ReadRecipientsResultFactory(outputBuffer.Count);
		}

		// Token: 0x060012E4 RID: 4836 RVA: 0x00033336 File Offset: 0x00031536
		protected override void InternalParseInput(Reader reader, ServerObjectHandleTable serverObjectHandleTable)
		{
			base.InternalParseInput(reader, serverObjectHandleTable);
			this.recipientRowId = reader.ReadUInt32();
			this.extraUnicodePropertyTags = reader.ReadCountAndPropertyTagArray(FieldLength.WordSize);
		}

		// Token: 0x060012E5 RID: 4837 RVA: 0x00033359 File Offset: 0x00031559
		protected override void InternalSerializeOutput(Writer writer)
		{
			base.InternalSerializeOutput(writer);
			this.result.Serialize(writer);
		}

		// Token: 0x060012E6 RID: 4838 RVA: 0x00033370 File Offset: 0x00031570
		protected override void InternalExecute(IServerObject serverObject, IRopHandler ropHandler, ArraySegment<byte> outputBuffer)
		{
			ReadRecipientsResultFactory resultFactory = new ReadRecipientsResultFactory(outputBuffer.Count);
			this.result = ropHandler.ReadRecipients(serverObject, this.recipientRowId, this.extraUnicodePropertyTags, resultFactory);
		}

		// Token: 0x060012E7 RID: 4839 RVA: 0x000333A4 File Offset: 0x000315A4
		internal override void AppendToString(StringBuilder stringBuilder)
		{
			base.AppendToString(stringBuilder);
			stringBuilder.Append(" RowId=").Append(this.recipientRowId);
			stringBuilder.Append(" UnicodeTags=[");
			Util.AppendToString<PropertyTag>(stringBuilder, this.extraUnicodePropertyTags);
			stringBuilder.Append("]");
		}

		// Token: 0x04000A19 RID: 2585
		private const RopId RopType = RopId.ReadRecipients;

		// Token: 0x04000A1A RID: 2586
		private PropertyTag[] extraUnicodePropertyTags;

		// Token: 0x04000A1B RID: 2587
		private uint recipientRowId;

		// Token: 0x04000A1C RID: 2588
		private PropertyTag[] extraPropertyTags;
	}
}
