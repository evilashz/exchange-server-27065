using System;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x0200035F RID: 863
	internal sealed class RopUploadStateStreamContinue : InputRop
	{
		// Token: 0x17000391 RID: 913
		// (get) Token: 0x060014F8 RID: 5368 RVA: 0x00036AB1 File Offset: 0x00034CB1
		internal override RopId RopId
		{
			get
			{
				return RopId.UploadStateStreamContinue;
			}
		}

		// Token: 0x060014F9 RID: 5369 RVA: 0x00036AB5 File Offset: 0x00034CB5
		internal static Rop CreateRop()
		{
			return new RopUploadStateStreamContinue();
		}

		// Token: 0x060014FA RID: 5370 RVA: 0x00036ABC File Offset: 0x00034CBC
		internal void SetInput(byte logonIndex, byte handleTableIndex, ArraySegment<byte> data)
		{
			base.SetCommonInput(logonIndex, handleTableIndex);
			this.data = data;
		}

		// Token: 0x060014FB RID: 5371 RVA: 0x00036ACD File Offset: 0x00034CCD
		protected override void InternalSerializeInput(Writer writer, Encoding string8Encoding)
		{
			base.InternalSerializeInput(writer, string8Encoding);
			writer.WriteSizedBytesSegment(this.data, FieldLength.DWordSize);
		}

		// Token: 0x060014FC RID: 5372 RVA: 0x00036AE4 File Offset: 0x00034CE4
		protected override void InternalParseOutput(Reader reader, Encoding string8Encoding)
		{
			base.InternalParseOutput(reader, string8Encoding);
			base.Result = RopResult.Parse(reader, new RopResult.ResultParserDelegate(StandardRopResult.ParseSuccessResult), new RopResult.ResultParserDelegate(StandardRopResult.ParseFailResult));
		}

		// Token: 0x060014FD RID: 5373 RVA: 0x00036B12 File Offset: 0x00034D12
		protected override IResultFactory GetDefaultResultFactory(IConnectionInformation connection, ArraySegment<byte> outputBuffer)
		{
			return RopUploadStateStreamContinue.resultFactory;
		}

		// Token: 0x060014FE RID: 5374 RVA: 0x00036B19 File Offset: 0x00034D19
		protected override void InternalParseInput(Reader reader, ServerObjectHandleTable serverObjectHandleTable)
		{
			base.InternalParseInput(reader, serverObjectHandleTable);
			this.data = reader.ReadSizeAndByteArraySegment(FieldLength.DWordSize);
		}

		// Token: 0x060014FF RID: 5375 RVA: 0x00036B30 File Offset: 0x00034D30
		protected override void InternalSerializeOutput(Writer writer)
		{
			base.InternalSerializeOutput(writer);
			this.result.Serialize(writer);
		}

		// Token: 0x06001500 RID: 5376 RVA: 0x00036B45 File Offset: 0x00034D45
		protected override void InternalExecute(IServerObject serverObject, IRopHandler ropHandler, ArraySegment<byte> outputBuffer)
		{
			this.result = ropHandler.UploadStateStreamContinue(serverObject, this.data, RopUploadStateStreamContinue.resultFactory);
		}

		// Token: 0x06001501 RID: 5377 RVA: 0x00036B5F File Offset: 0x00034D5F
		internal override void AppendToString(StringBuilder stringBuilder)
		{
			base.AppendToString(stringBuilder);
			stringBuilder.Append(" data=[");
			Util.AppendToString<byte>(stringBuilder, this.data);
			stringBuilder.Append("]");
		}

		// Token: 0x04000B13 RID: 2835
		private const RopId RopType = RopId.UploadStateStreamContinue;

		// Token: 0x04000B14 RID: 2836
		private static UploadStateStreamContinueResultFactory resultFactory = new UploadStateStreamContinueResultFactory();

		// Token: 0x04000B15 RID: 2837
		private ArraySegment<byte> data;
	}
}
