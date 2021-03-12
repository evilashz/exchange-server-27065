using System;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000332 RID: 818
	internal sealed class RopSeekRowBookmark : InputRop
	{
		// Token: 0x17000368 RID: 872
		// (get) Token: 0x0600137F RID: 4991 RVA: 0x000345F9 File Offset: 0x000327F9
		internal override RopId RopId
		{
			get
			{
				return RopId.SeekRowBookmark;
			}
		}

		// Token: 0x06001380 RID: 4992 RVA: 0x000345FD File Offset: 0x000327FD
		internal static Rop CreateRop()
		{
			return new RopSeekRowBookmark();
		}

		// Token: 0x06001381 RID: 4993 RVA: 0x00034604 File Offset: 0x00032804
		internal void SetInput(byte logonIndex, byte handleTableIndex, byte[] bookmark, int rowCount, bool wantMoveCount)
		{
			base.SetCommonInput(logonIndex, handleTableIndex);
			this.bookmark = bookmark;
			this.rowCount = rowCount;
			this.wantMoveCount = wantMoveCount;
		}

		// Token: 0x06001382 RID: 4994 RVA: 0x00034625 File Offset: 0x00032825
		protected override void InternalSerializeInput(Writer writer, Encoding string8Encoding)
		{
			base.InternalSerializeInput(writer, string8Encoding);
			writer.WriteSizedBytes(this.bookmark);
			writer.WriteInt32(this.rowCount);
			writer.WriteBool(this.wantMoveCount, 1);
		}

		// Token: 0x06001383 RID: 4995 RVA: 0x00034654 File Offset: 0x00032854
		protected override void InternalParseOutput(Reader reader, Encoding string8Encoding)
		{
			base.InternalParseOutput(reader, string8Encoding);
			this.result = RopResult.Parse(reader, new RopResult.ResultParserDelegate(SuccessfulSeekRowBookmarkResult.Parse), new RopResult.ResultParserDelegate(StandardRopResult.ParseFailResult));
		}

		// Token: 0x06001384 RID: 4996 RVA: 0x00034682 File Offset: 0x00032882
		protected override IResultFactory GetDefaultResultFactory(IConnectionInformation connection, ArraySegment<byte> outputBuffer)
		{
			return RopSeekRowBookmark.resultFactory;
		}

		// Token: 0x06001385 RID: 4997 RVA: 0x00034689 File Offset: 0x00032889
		protected override void InternalParseInput(Reader reader, ServerObjectHandleTable serverObjectHandleTable)
		{
			base.InternalParseInput(reader, serverObjectHandleTable);
			this.bookmark = reader.ReadSizeAndByteArray();
			this.rowCount = reader.ReadInt32();
			this.wantMoveCount = reader.ReadBool();
		}

		// Token: 0x06001386 RID: 4998 RVA: 0x000346B7 File Offset: 0x000328B7
		protected override void InternalSerializeOutput(Writer writer)
		{
			base.InternalSerializeOutput(writer);
			this.result.Serialize(writer);
		}

		// Token: 0x06001387 RID: 4999 RVA: 0x000346CC File Offset: 0x000328CC
		protected override void InternalExecute(IServerObject serverObject, IRopHandler ropHandler, ArraySegment<byte> outputBuffer)
		{
			this.result = ropHandler.SeekRowBookmark(serverObject, this.bookmark, this.rowCount, this.wantMoveCount, RopSeekRowBookmark.resultFactory);
		}

		// Token: 0x04000A63 RID: 2659
		private const RopId RopType = RopId.SeekRowBookmark;

		// Token: 0x04000A64 RID: 2660
		private static SeekRowBookmarkResultFactory resultFactory = new SeekRowBookmarkResultFactory();

		// Token: 0x04000A65 RID: 2661
		private byte[] bookmark;

		// Token: 0x04000A66 RID: 2662
		private int rowCount;

		// Token: 0x04000A67 RID: 2663
		private bool wantMoveCount;
	}
}
