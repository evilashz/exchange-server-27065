using System;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000363 RID: 867
	internal sealed class RopWriteStream : InputRop
	{
		// Token: 0x17000395 RID: 917
		// (get) Token: 0x06001526 RID: 5414 RVA: 0x00036F89 File Offset: 0x00035189
		internal override RopId RopId
		{
			get
			{
				return RopId.WriteStream;
			}
		}

		// Token: 0x06001527 RID: 5415 RVA: 0x00036F8D File Offset: 0x0003518D
		internal static Rop CreateRop()
		{
			return new RopWriteStream();
		}

		// Token: 0x06001528 RID: 5416 RVA: 0x00036F94 File Offset: 0x00035194
		internal void SetInput(byte logonIndex, byte handleTableIndex, ArraySegment<byte> data)
		{
			base.SetCommonInput(logonIndex, handleTableIndex);
			this.data = data;
		}

		// Token: 0x06001529 RID: 5417 RVA: 0x00036FA5 File Offset: 0x000351A5
		protected override void InternalSerializeInput(Writer writer, Encoding string8Encoding)
		{
			base.InternalSerializeInput(writer, string8Encoding);
			writer.WriteSizedBytesSegment(this.data, FieldLength.WordSize);
		}

		// Token: 0x0600152A RID: 5418 RVA: 0x00036FBC File Offset: 0x000351BC
		protected override void InternalParseOutput(Reader reader, Encoding string8Encoding)
		{
			base.InternalParseOutput(reader, string8Encoding);
			this.result = WriteStreamResult.Parse(reader);
		}

		// Token: 0x0600152B RID: 5419 RVA: 0x00036FD2 File Offset: 0x000351D2
		protected override IResultFactory GetDefaultResultFactory(IConnectionInformation connection, ArraySegment<byte> outputBuffer)
		{
			return RopWriteStream.resultFactory;
		}

		// Token: 0x0600152C RID: 5420 RVA: 0x00036FD9 File Offset: 0x000351D9
		protected override void InternalParseInput(Reader reader, ServerObjectHandleTable serverObjectHandleTable)
		{
			base.InternalParseInput(reader, serverObjectHandleTable);
			this.data = reader.ReadSizeAndByteArraySegment();
		}

		// Token: 0x0600152D RID: 5421 RVA: 0x00036FEF File Offset: 0x000351EF
		protected override void InternalSerializeOutput(Writer writer)
		{
			base.InternalSerializeOutput(writer);
			this.result.Serialize(writer);
		}

		// Token: 0x0600152E RID: 5422 RVA: 0x00037004 File Offset: 0x00035204
		protected override void InternalExecute(IServerObject serverObject, IRopHandler ropHandler, ArraySegment<byte> outputBuffer)
		{
			if ((long)outputBuffer.Count < RopWriteStream.resultFactory.SuccessfulResultMinimalSize)
			{
				throw new BufferTooSmallException();
			}
			this.result = ropHandler.WriteStream(serverObject, this.data, RopWriteStream.resultFactory);
		}

		// Token: 0x0600152F RID: 5423 RVA: 0x00037038 File Offset: 0x00035238
		internal override void AppendToString(StringBuilder stringBuilder)
		{
			base.AppendToString(stringBuilder);
			stringBuilder.Append(" Size=").Append(this.data.Count);
		}

		// Token: 0x04000B22 RID: 2850
		private const RopId RopType = RopId.WriteStream;

		// Token: 0x04000B23 RID: 2851
		private static WriteStreamResultFactory resultFactory = new WriteStreamResultFactory();

		// Token: 0x04000B24 RID: 2852
		private ArraySegment<byte> data;
	}
}
