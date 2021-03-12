using System;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000310 RID: 784
	internal sealed class RopOpenMessage : InputOutputRop
	{
		// Token: 0x17000348 RID: 840
		// (get) Token: 0x06001259 RID: 4697 RVA: 0x000323C6 File Offset: 0x000305C6
		internal override RopId RopId
		{
			get
			{
				return RopId.OpenMessage;
			}
		}

		// Token: 0x0600125A RID: 4698 RVA: 0x000323C9 File Offset: 0x000305C9
		internal static Rop CreateRop()
		{
			return new RopOpenMessage();
		}

		// Token: 0x0600125B RID: 4699 RVA: 0x000323D0 File Offset: 0x000305D0
		internal void SetInput(byte logonIndex, byte handleTableIndex, byte returnHandleTableIndex, ushort codePageId, StoreId folderId, OpenMode openMode, StoreId messageId)
		{
			base.SetCommonInput(logonIndex, handleTableIndex, returnHandleTableIndex);
			this.codePageId = codePageId;
			this.folderId = folderId;
			this.openMode = openMode;
			this.messageId = messageId;
		}

		// Token: 0x0600125C RID: 4700 RVA: 0x000323FB File Offset: 0x000305FB
		protected override void InternalSerializeInput(Writer writer, Encoding string8Encoding)
		{
			base.InternalSerializeInput(writer, string8Encoding);
			writer.WriteUInt16(this.codePageId);
			this.folderId.Serialize(writer);
			writer.WriteByte((byte)this.openMode);
			this.messageId.Serialize(writer);
		}

		// Token: 0x0600125D RID: 4701 RVA: 0x0003244C File Offset: 0x0003064C
		protected override void InternalParseOutput(Reader reader, Encoding string8Encoding)
		{
			base.InternalParseOutput(reader, string8Encoding);
			this.result = RopResult.Parse(reader, (Reader readerParameter) => SuccessfulOpenMessageResult.Parse(readerParameter, string8Encoding), new RopResult.ResultParserDelegate(StandardRopResult.ParseFailResult));
		}

		// Token: 0x0600125E RID: 4702 RVA: 0x00032497 File Offset: 0x00030697
		protected override IResultFactory GetDefaultResultFactory(IConnectionInformation connection, ArraySegment<byte> outputBuffer)
		{
			return new OpenMessageResultFactory(outputBuffer.Count);
		}

		// Token: 0x0600125F RID: 4703 RVA: 0x000324A5 File Offset: 0x000306A5
		protected override void InternalParseInput(Reader reader, ServerObjectHandleTable serverObjectHandleTable)
		{
			base.InternalParseInput(reader, serverObjectHandleTable);
			this.codePageId = reader.ReadUInt16();
			this.folderId = StoreId.Parse(reader);
			this.openMode = (OpenMode)reader.ReadByte();
			this.messageId = StoreId.Parse(reader);
		}

		// Token: 0x06001260 RID: 4704 RVA: 0x000324DF File Offset: 0x000306DF
		protected override void InternalSerializeOutput(Writer writer)
		{
			base.InternalSerializeOutput(writer);
			this.result.Serialize(writer);
		}

		// Token: 0x06001261 RID: 4705 RVA: 0x000324F4 File Offset: 0x000306F4
		protected override void InternalExecute(IServerObject serverObject, IRopHandler ropHandler, ArraySegment<byte> outputBuffer)
		{
			OpenMessageResultFactory resultFactory = new OpenMessageResultFactory(outputBuffer.Count);
			this.result = ropHandler.OpenMessage(serverObject, this.codePageId, this.folderId, this.openMode, this.messageId, resultFactory);
		}

		// Token: 0x06001262 RID: 4706 RVA: 0x00032534 File Offset: 0x00030734
		internal override void AppendToString(StringBuilder stringBuilder)
		{
			base.AppendToString(stringBuilder);
			stringBuilder.Append(" CPID=").Append(this.codePageId);
			stringBuilder.Append(" FID=").Append(this.folderId.ToString());
			stringBuilder.Append(" MID=").Append(this.messageId.ToString());
			stringBuilder.Append(" OpenMode=").Append(this.openMode);
		}

		// Token: 0x040009E4 RID: 2532
		private const RopId RopType = RopId.OpenMessage;

		// Token: 0x040009E5 RID: 2533
		private ushort codePageId;

		// Token: 0x040009E6 RID: 2534
		private StoreId folderId;

		// Token: 0x040009E7 RID: 2535
		private OpenMode openMode;

		// Token: 0x040009E8 RID: 2536
		private StoreId messageId;
	}
}
