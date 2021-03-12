using System;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x0200030E RID: 782
	internal sealed class RopOpenEmbeddedMessage : InputOutputRop
	{
		// Token: 0x17000346 RID: 838
		// (get) Token: 0x06001242 RID: 4674 RVA: 0x00032126 File Offset: 0x00030326
		internal override RopId RopId
		{
			get
			{
				return RopId.OpenEmbeddedMessage;
			}
		}

		// Token: 0x06001243 RID: 4675 RVA: 0x0003212A File Offset: 0x0003032A
		internal static Rop CreateRop()
		{
			return new RopOpenEmbeddedMessage();
		}

		// Token: 0x06001244 RID: 4676 RVA: 0x00032131 File Offset: 0x00030331
		internal void SetInput(byte logonIndex, byte handleTableIndex, byte returnHandleTableIndex, ushort codePageId, OpenMode openMode)
		{
			base.SetCommonInput(logonIndex, handleTableIndex, returnHandleTableIndex);
			this.codePageId = codePageId;
			this.openMode = openMode;
		}

		// Token: 0x06001245 RID: 4677 RVA: 0x0003214C File Offset: 0x0003034C
		protected override void InternalSerializeInput(Writer writer, Encoding string8Encoding)
		{
			base.InternalSerializeInput(writer, string8Encoding);
			writer.WriteUInt16(this.codePageId);
			writer.WriteByte((byte)this.openMode);
		}

		// Token: 0x06001246 RID: 4678 RVA: 0x00032184 File Offset: 0x00030384
		protected override void InternalParseOutput(Reader reader, Encoding string8Encoding)
		{
			base.InternalParseOutput(reader, string8Encoding);
			this.result = RopResult.Parse(reader, (Reader readerParameter) => SuccessfulOpenEmbeddedMessageResult.Parse(readerParameter, string8Encoding), new RopResult.ResultParserDelegate(StandardRopResult.ParseFailResult));
		}

		// Token: 0x06001247 RID: 4679 RVA: 0x000321CF File Offset: 0x000303CF
		protected override IResultFactory GetDefaultResultFactory(IConnectionInformation connection, ArraySegment<byte> outputBuffer)
		{
			return new OpenEmbeddedMessageResultFactory(outputBuffer.Count);
		}

		// Token: 0x06001248 RID: 4680 RVA: 0x000321DD File Offset: 0x000303DD
		protected override void InternalParseInput(Reader reader, ServerObjectHandleTable serverObjectHandleTable)
		{
			base.InternalParseInput(reader, serverObjectHandleTable);
			this.codePageId = reader.ReadUInt16();
			this.openMode = (OpenMode)reader.ReadByte();
		}

		// Token: 0x06001249 RID: 4681 RVA: 0x000321FF File Offset: 0x000303FF
		protected override void InternalSerializeOutput(Writer writer)
		{
			base.InternalSerializeOutput(writer);
			this.result.Serialize(writer);
		}

		// Token: 0x0600124A RID: 4682 RVA: 0x00032214 File Offset: 0x00030414
		protected override void InternalExecute(IServerObject serverObject, IRopHandler ropHandler, ArraySegment<byte> outputBuffer)
		{
			OpenEmbeddedMessageResultFactory resultFactory = new OpenEmbeddedMessageResultFactory(outputBuffer.Count);
			this.result = ropHandler.OpenEmbeddedMessage(serverObject, this.codePageId, this.openMode, resultFactory);
		}

		// Token: 0x0600124B RID: 4683 RVA: 0x00032248 File Offset: 0x00030448
		internal override void AppendToString(StringBuilder stringBuilder)
		{
			base.AppendToString(stringBuilder);
			stringBuilder.Append(" CPID=").Append(this.codePageId);
			stringBuilder.Append(" OpenMode=").Append(this.openMode);
		}

		// Token: 0x040009DD RID: 2525
		private const RopId RopType = RopId.OpenEmbeddedMessage;

		// Token: 0x040009DE RID: 2526
		private ushort codePageId;

		// Token: 0x040009DF RID: 2527
		private OpenMode openMode;
	}
}
