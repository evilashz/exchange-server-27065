using System;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000361 RID: 865
	internal sealed class RopWriteCommitStream : InputRop
	{
		// Token: 0x17000393 RID: 915
		// (get) Token: 0x0600150F RID: 5391 RVA: 0x00036C40 File Offset: 0x00034E40
		internal override RopId RopId
		{
			get
			{
				return RopId.WriteCommitStream;
			}
		}

		// Token: 0x06001510 RID: 5392 RVA: 0x00036C47 File Offset: 0x00034E47
		internal static Rop CreateRop()
		{
			return new RopWriteCommitStream();
		}

		// Token: 0x06001511 RID: 5393 RVA: 0x00036C4E File Offset: 0x00034E4E
		internal void SetInput(byte logonIndex, byte handleTableIndex, byte[] data)
		{
			base.SetCommonInput(logonIndex, handleTableIndex);
			this.data = data;
		}

		// Token: 0x06001512 RID: 5394 RVA: 0x00036C5F File Offset: 0x00034E5F
		protected override void InternalSerializeInput(Writer writer, Encoding string8Encoding)
		{
			base.InternalSerializeInput(writer, string8Encoding);
			writer.WriteSizedBytes(this.data);
		}

		// Token: 0x06001513 RID: 5395 RVA: 0x00036C75 File Offset: 0x00034E75
		protected override void InternalParseOutput(Reader reader, Encoding string8Encoding)
		{
			base.InternalParseOutput(reader, string8Encoding);
			this.result = WriteCommitStreamResult.Parse(reader);
		}

		// Token: 0x06001514 RID: 5396 RVA: 0x00036C8B File Offset: 0x00034E8B
		protected override IResultFactory GetDefaultResultFactory(IConnectionInformation connection, ArraySegment<byte> outputBuffer)
		{
			return RopWriteCommitStream.resultFactory;
		}

		// Token: 0x06001515 RID: 5397 RVA: 0x00036C92 File Offset: 0x00034E92
		protected override void InternalParseInput(Reader reader, ServerObjectHandleTable serverObjectHandleTable)
		{
			base.InternalParseInput(reader, serverObjectHandleTable);
			this.data = reader.ReadSizeAndByteArray();
		}

		// Token: 0x06001516 RID: 5398 RVA: 0x00036CA8 File Offset: 0x00034EA8
		protected override void InternalSerializeOutput(Writer writer)
		{
			base.InternalSerializeOutput(writer);
			this.result.Serialize(writer);
		}

		// Token: 0x06001517 RID: 5399 RVA: 0x00036CBD File Offset: 0x00034EBD
		protected override void InternalExecute(IServerObject serverObject, IRopHandler ropHandler, ArraySegment<byte> outputBuffer)
		{
			if ((long)outputBuffer.Count < RopWriteCommitStream.resultFactory.SuccessfulResultMinimalSize)
			{
				throw new BufferTooSmallException();
			}
			this.result = ropHandler.WriteCommitStream(serverObject, this.data, RopWriteCommitStream.resultFactory);
		}

		// Token: 0x04000B18 RID: 2840
		private const RopId RopType = RopId.WriteCommitStream;

		// Token: 0x04000B19 RID: 2841
		private static WriteCommitStreamResultFactory resultFactory = new WriteCommitStreamResultFactory();

		// Token: 0x04000B1A RID: 2842
		private byte[] data;
	}
}
