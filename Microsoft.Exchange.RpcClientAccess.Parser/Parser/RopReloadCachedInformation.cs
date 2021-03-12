using System;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000325 RID: 805
	internal sealed class RopReloadCachedInformation : InputRop
	{
		// Token: 0x1700035D RID: 861
		// (get) Token: 0x06001318 RID: 4888 RVA: 0x00033CCE File Offset: 0x00031ECE
		internal override RopId RopId
		{
			get
			{
				return RopId.ReloadCachedInformation;
			}
		}

		// Token: 0x06001319 RID: 4889 RVA: 0x00033CD2 File Offset: 0x00031ED2
		internal static Rop CreateRop()
		{
			return new RopReloadCachedInformation();
		}

		// Token: 0x0600131A RID: 4890 RVA: 0x00033CD9 File Offset: 0x00031ED9
		internal void SetInput(byte logonIndex, byte handleTableIndex, PropertyTag[] extraUnicodePropertyTags)
		{
			base.SetCommonInput(logonIndex, handleTableIndex);
			this.extraUnicodePropertyTags = extraUnicodePropertyTags;
		}

		// Token: 0x0600131B RID: 4891 RVA: 0x00033CEA File Offset: 0x00031EEA
		protected override void InternalSerializeInput(Writer writer, Encoding string8Encoding)
		{
			base.InternalSerializeInput(writer, string8Encoding);
			writer.WriteCountAndPropertyTagArray(this.extraUnicodePropertyTags, FieldLength.WordSize);
		}

		// Token: 0x0600131C RID: 4892 RVA: 0x00033D18 File Offset: 0x00031F18
		protected override void InternalParseOutput(Reader reader, Encoding string8Encoding)
		{
			base.InternalParseOutput(reader, string8Encoding);
			this.result = RopResult.Parse(reader, (Reader readerParameter) => SuccessfulReloadCachedInformationResult.Parse(readerParameter, string8Encoding), new RopResult.ResultParserDelegate(StandardRopResult.ParseFailResult));
		}

		// Token: 0x0600131D RID: 4893 RVA: 0x00033D63 File Offset: 0x00031F63
		protected override IResultFactory GetDefaultResultFactory(IConnectionInformation connection, ArraySegment<byte> outputBuffer)
		{
			return new ReloadCachedInformationResultFactory(outputBuffer.Count);
		}

		// Token: 0x0600131E RID: 4894 RVA: 0x00033D71 File Offset: 0x00031F71
		protected override void InternalParseInput(Reader reader, ServerObjectHandleTable serverObjectHandleTable)
		{
			base.InternalParseInput(reader, serverObjectHandleTable);
			this.extraUnicodePropertyTags = reader.ReadCountAndPropertyTagArray(FieldLength.WordSize);
		}

		// Token: 0x0600131F RID: 4895 RVA: 0x00033D88 File Offset: 0x00031F88
		protected override void InternalSerializeOutput(Writer writer)
		{
			base.InternalSerializeOutput(writer);
			this.result.Serialize(writer);
		}

		// Token: 0x06001320 RID: 4896 RVA: 0x00033DA0 File Offset: 0x00031FA0
		protected override void InternalExecute(IServerObject serverObject, IRopHandler ropHandler, ArraySegment<byte> outputBuffer)
		{
			ReloadCachedInformationResultFactory resultFactory = new ReloadCachedInformationResultFactory(outputBuffer.Count);
			this.result = ropHandler.ReloadCachedInformation(serverObject, this.extraUnicodePropertyTags, resultFactory);
		}

		// Token: 0x06001321 RID: 4897 RVA: 0x00033DCE File Offset: 0x00031FCE
		internal override void AppendToString(StringBuilder stringBuilder)
		{
			base.AppendToString(stringBuilder);
			stringBuilder.Append(" ExtraTags=[");
			Util.AppendToString<PropertyTag>(stringBuilder, this.extraUnicodePropertyTags);
			stringBuilder.Append("]");
		}

		// Token: 0x04000A3F RID: 2623
		private const RopId RopType = RopId.ReloadCachedInformation;

		// Token: 0x04000A40 RID: 2624
		private PropertyTag[] extraUnicodePropertyTags;
	}
}
