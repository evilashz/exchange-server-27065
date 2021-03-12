using System;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x0200032F RID: 815
	internal sealed class RopSeekRow : InputRop
	{
		// Token: 0x17000364 RID: 868
		// (get) Token: 0x06001360 RID: 4960 RVA: 0x000342BD File Offset: 0x000324BD
		internal override RopId RopId
		{
			get
			{
				return RopId.SeekRow;
			}
		}

		// Token: 0x06001361 RID: 4961 RVA: 0x000342C1 File Offset: 0x000324C1
		internal static Rop CreateRop()
		{
			return new RopSeekRow();
		}

		// Token: 0x06001362 RID: 4962 RVA: 0x000342C8 File Offset: 0x000324C8
		internal void SetInput(byte logonIndex, byte handleTableIndex, BookmarkOrigin bookmarkOrigin, int rowCount, bool wantMoveCount)
		{
			base.SetCommonInput(logonIndex, handleTableIndex);
			this.bookmarkOrigin = bookmarkOrigin;
			this.rowCount = rowCount;
			this.wantMoveCount = wantMoveCount;
		}

		// Token: 0x06001363 RID: 4963 RVA: 0x000342E9 File Offset: 0x000324E9
		protected override void InternalSerializeInput(Writer writer, Encoding string8Encoding)
		{
			base.InternalSerializeInput(writer, string8Encoding);
			writer.WriteByte((byte)this.bookmarkOrigin);
			writer.WriteInt32(this.rowCount);
			writer.WriteBool(this.wantMoveCount, 1);
		}

		// Token: 0x06001364 RID: 4964 RVA: 0x00034318 File Offset: 0x00032518
		protected override void InternalParseOutput(Reader reader, Encoding string8Encoding)
		{
			base.InternalParseOutput(reader, string8Encoding);
			this.result = RopResult.Parse(reader, new RopResult.ResultParserDelegate(SuccessfulSeekRowResult.Parse), new RopResult.ResultParserDelegate(StandardRopResult.ParseFailResult));
		}

		// Token: 0x06001365 RID: 4965 RVA: 0x00034346 File Offset: 0x00032546
		protected override IResultFactory GetDefaultResultFactory(IConnectionInformation connection, ArraySegment<byte> outputBuffer)
		{
			return RopSeekRow.resultFactory;
		}

		// Token: 0x06001366 RID: 4966 RVA: 0x0003434D File Offset: 0x0003254D
		protected override void InternalParseInput(Reader reader, ServerObjectHandleTable serverObjectHandleTable)
		{
			base.InternalParseInput(reader, serverObjectHandleTable);
			this.bookmarkOrigin = (BookmarkOrigin)reader.ReadByte();
			this.rowCount = reader.ReadInt32();
			this.wantMoveCount = reader.ReadBool();
		}

		// Token: 0x06001367 RID: 4967 RVA: 0x0003437B File Offset: 0x0003257B
		protected override void InternalSerializeOutput(Writer writer)
		{
			base.InternalSerializeOutput(writer);
			this.result.Serialize(writer);
		}

		// Token: 0x06001368 RID: 4968 RVA: 0x00034390 File Offset: 0x00032590
		protected override void InternalExecute(IServerObject serverObject, IRopHandler ropHandler, ArraySegment<byte> outputBuffer)
		{
			this.result = ropHandler.SeekRow(serverObject, this.bookmarkOrigin, this.rowCount, this.wantMoveCount, RopSeekRow.resultFactory);
		}

		// Token: 0x06001369 RID: 4969 RVA: 0x000343B8 File Offset: 0x000325B8
		internal override void AppendToString(StringBuilder stringBuilder)
		{
			base.AppendToString(stringBuilder);
			stringBuilder.Append(" Origin=").Append(this.bookmarkOrigin);
			stringBuilder.Append(" Count=").Append(this.rowCount);
			stringBuilder.Append(" WantCount=").Append(this.wantMoveCount);
		}

		// Token: 0x04000A58 RID: 2648
		private const RopId RopType = RopId.SeekRow;

		// Token: 0x04000A59 RID: 2649
		private static SeekRowResultFactory resultFactory = new SeekRowResultFactory();

		// Token: 0x04000A5A RID: 2650
		private BookmarkOrigin bookmarkOrigin;

		// Token: 0x04000A5B RID: 2651
		private int rowCount;

		// Token: 0x04000A5C RID: 2652
		private bool wantMoveCount;
	}
}
