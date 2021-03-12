using System;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000284 RID: 644
	internal sealed class RopAbortSubmit : InputRop
	{
		// Token: 0x170002BA RID: 698
		// (get) Token: 0x06000DF9 RID: 3577 RVA: 0x0002A1FB File Offset: 0x000283FB
		internal override RopId RopId
		{
			get
			{
				return RopId.AbortSubmit;
			}
		}

		// Token: 0x06000DFA RID: 3578 RVA: 0x0002A1FF File Offset: 0x000283FF
		internal static Rop CreateRop()
		{
			return new RopAbortSubmit();
		}

		// Token: 0x06000DFB RID: 3579 RVA: 0x0002A206 File Offset: 0x00028406
		internal void SetInput(byte logonIndex, byte handleTableIndex, StoreId folderId, StoreId messageId)
		{
			base.SetCommonInput(logonIndex, handleTableIndex);
			this.folderId = folderId;
			this.messageId = messageId;
		}

		// Token: 0x06000DFC RID: 3580 RVA: 0x0002A21F File Offset: 0x0002841F
		protected override void InternalSerializeInput(Writer writer, Encoding string8Encoding)
		{
			base.InternalSerializeInput(writer, string8Encoding);
			this.folderId.Serialize(writer);
			this.messageId.Serialize(writer);
		}

		// Token: 0x06000DFD RID: 3581 RVA: 0x0002A241 File Offset: 0x00028441
		protected override void InternalParseOutput(Reader reader, Encoding string8Encoding)
		{
			base.InternalParseOutput(reader, string8Encoding);
			this.result = RopResult.Parse(reader, new RopResult.ResultParserDelegate(StandardRopResult.ParseSuccessResult), new RopResult.ResultParserDelegate(StandardRopResult.ParseFailResult));
		}

		// Token: 0x06000DFE RID: 3582 RVA: 0x0002A26F File Offset: 0x0002846F
		protected override IResultFactory GetDefaultResultFactory(IConnectionInformation connection, ArraySegment<byte> outputBuffer)
		{
			return RopAbortSubmit.resultFactory;
		}

		// Token: 0x06000DFF RID: 3583 RVA: 0x0002A276 File Offset: 0x00028476
		protected override void InternalParseInput(Reader reader, ServerObjectHandleTable serverObjectHandleTable)
		{
			base.InternalParseInput(reader, serverObjectHandleTable);
			this.folderId = StoreId.Parse(reader);
			this.messageId = StoreId.Parse(reader);
		}

		// Token: 0x06000E00 RID: 3584 RVA: 0x0002A298 File Offset: 0x00028498
		protected override void InternalSerializeOutput(Writer writer)
		{
			base.InternalSerializeOutput(writer);
			this.result.Serialize(writer);
		}

		// Token: 0x06000E01 RID: 3585 RVA: 0x0002A2AD File Offset: 0x000284AD
		protected override void InternalExecute(IServerObject serverObject, IRopHandler ropHandler, ArraySegment<byte> outputBuffer)
		{
			this.result = ropHandler.AbortSubmit(serverObject, this.folderId, this.messageId, RopAbortSubmit.resultFactory);
		}

		// Token: 0x06000E02 RID: 3586 RVA: 0x0002A2D0 File Offset: 0x000284D0
		internal override void AppendToString(StringBuilder stringBuilder)
		{
			base.AppendToString(stringBuilder);
			stringBuilder.Append(" FID=").Append(this.folderId.ToString());
			stringBuilder.Append(" MID=").Append(this.messageId.ToString());
		}

		// Token: 0x04000735 RID: 1845
		private const RopId RopType = RopId.AbortSubmit;

		// Token: 0x04000736 RID: 1846
		private static AbortSubmitResultFactory resultFactory = new AbortSubmitResultFactory();

		// Token: 0x04000737 RID: 1847
		private StoreId folderId;

		// Token: 0x04000738 RID: 1848
		private StoreId messageId;
	}
}
