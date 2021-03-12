using System;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000314 RID: 788
	internal sealed class RopPublicFolderIsGhosted : InputRop
	{
		// Token: 0x1700034D RID: 845
		// (get) Token: 0x06001289 RID: 4745 RVA: 0x000328BD File Offset: 0x00030ABD
		internal override RopId RopId
		{
			get
			{
				return RopId.PublicFolderIsGhosted;
			}
		}

		// Token: 0x0600128A RID: 4746 RVA: 0x000328C1 File Offset: 0x00030AC1
		internal static Rop CreateRop()
		{
			return new RopPublicFolderIsGhosted();
		}

		// Token: 0x0600128B RID: 4747 RVA: 0x000328C8 File Offset: 0x00030AC8
		internal override void AppendToString(StringBuilder stringBuilder)
		{
			base.AppendToString(stringBuilder);
			stringBuilder.Append(" FID=").Append(this.folderId.ToString());
		}

		// Token: 0x0600128C RID: 4748 RVA: 0x000328F3 File Offset: 0x00030AF3
		internal void SetInput(byte logonIndex, byte handleTableIndex, StoreId folderId)
		{
			base.SetCommonInput(logonIndex, handleTableIndex);
			this.folderId = folderId;
		}

		// Token: 0x0600128D RID: 4749 RVA: 0x00032904 File Offset: 0x00030B04
		protected override void InternalSerializeInput(Writer writer, Encoding string8Encoding)
		{
			base.InternalSerializeInput(writer, string8Encoding);
			this.folderId.Serialize(writer);
		}

		// Token: 0x0600128E RID: 4750 RVA: 0x0003291A File Offset: 0x00030B1A
		protected override void InternalParseOutput(Reader reader, Encoding string8Encoding)
		{
			base.InternalParseOutput(reader, string8Encoding);
			this.result = RopResult.Parse(reader, new RopResult.ResultParserDelegate(SuccessfulPublicFolderIsGhostedResult.Parse), new RopResult.ResultParserDelegate(StandardRopResult.ParseFailResult));
		}

		// Token: 0x0600128F RID: 4751 RVA: 0x00032948 File Offset: 0x00030B48
		protected override IResultFactory GetDefaultResultFactory(IConnectionInformation connection, ArraySegment<byte> outputBuffer)
		{
			return RopPublicFolderIsGhosted.resultFactory;
		}

		// Token: 0x06001290 RID: 4752 RVA: 0x0003294F File Offset: 0x00030B4F
		protected override void InternalParseInput(Reader reader, ServerObjectHandleTable serverObjectHandleTable)
		{
			base.InternalParseInput(reader, serverObjectHandleTable);
			this.folderId = StoreId.Parse(reader);
		}

		// Token: 0x06001291 RID: 4753 RVA: 0x00032965 File Offset: 0x00030B65
		protected override void InternalSerializeOutput(Writer writer)
		{
			base.InternalSerializeOutput(writer);
			this.result.Serialize(writer);
		}

		// Token: 0x06001292 RID: 4754 RVA: 0x0003297A File Offset: 0x00030B7A
		protected override void InternalExecute(IServerObject serverObject, IRopHandler ropHandler, ArraySegment<byte> outputBuffer)
		{
			this.result = ropHandler.PublicFolderIsGhosted(serverObject, this.folderId, RopPublicFolderIsGhosted.resultFactory);
		}

		// Token: 0x040009F3 RID: 2547
		private const RopId RopType = RopId.PublicFolderIsGhosted;

		// Token: 0x040009F4 RID: 2548
		private static PublicFolderIsGhostedResultFactory resultFactory = new PublicFolderIsGhostedResultFactory();

		// Token: 0x040009F5 RID: 2549
		private StoreId folderId;
	}
}
