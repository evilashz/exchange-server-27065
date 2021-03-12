using System;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x020002CD RID: 717
	internal sealed class RopGetOwningServers : InputRop
	{
		// Token: 0x17000302 RID: 770
		// (get) Token: 0x0600106B RID: 4203 RVA: 0x0002E84D File Offset: 0x0002CA4D
		internal override RopId RopId
		{
			get
			{
				return RopId.GetOwningServers;
			}
		}

		// Token: 0x0600106C RID: 4204 RVA: 0x0002E851 File Offset: 0x0002CA51
		internal static Rop CreateRop()
		{
			return new RopGetOwningServers();
		}

		// Token: 0x0600106D RID: 4205 RVA: 0x0002E858 File Offset: 0x0002CA58
		internal override void AppendToString(StringBuilder stringBuilder)
		{
			base.AppendToString(stringBuilder);
			stringBuilder.Append(" FID=").Append(this.folderId.ToString());
		}

		// Token: 0x0600106E RID: 4206 RVA: 0x0002E883 File Offset: 0x0002CA83
		internal void SetInput(byte logonIndex, byte handleTableIndex, StoreId folderId)
		{
			base.SetCommonInput(logonIndex, handleTableIndex);
			this.folderId = folderId;
		}

		// Token: 0x0600106F RID: 4207 RVA: 0x0002E894 File Offset: 0x0002CA94
		protected override void InternalSerializeInput(Writer writer, Encoding string8Encoding)
		{
			base.InternalSerializeInput(writer, string8Encoding);
			this.folderId.Serialize(writer);
		}

		// Token: 0x06001070 RID: 4208 RVA: 0x0002E8AA File Offset: 0x0002CAAA
		protected override void InternalParseOutput(Reader reader, Encoding string8Encoding)
		{
			base.InternalParseOutput(reader, string8Encoding);
			this.result = RopResult.Parse(reader, new RopResult.ResultParserDelegate(SuccessfulGetOwningServersResult.Parse), new RopResult.ResultParserDelegate(StandardRopResult.ParseFailResult));
		}

		// Token: 0x06001071 RID: 4209 RVA: 0x0002E8D8 File Offset: 0x0002CAD8
		protected override IResultFactory GetDefaultResultFactory(IConnectionInformation connection, ArraySegment<byte> outputBuffer)
		{
			return RopGetOwningServers.resultFactory;
		}

		// Token: 0x06001072 RID: 4210 RVA: 0x0002E8DF File Offset: 0x0002CADF
		protected override void InternalParseInput(Reader reader, ServerObjectHandleTable serverObjectHandleTable)
		{
			base.InternalParseInput(reader, serverObjectHandleTable);
			this.folderId = StoreId.Parse(reader);
		}

		// Token: 0x06001073 RID: 4211 RVA: 0x0002E8F5 File Offset: 0x0002CAF5
		protected override void InternalSerializeOutput(Writer writer)
		{
			base.InternalSerializeOutput(writer);
			this.result.Serialize(writer);
		}

		// Token: 0x06001074 RID: 4212 RVA: 0x0002E90A File Offset: 0x0002CB0A
		protected override void InternalExecute(IServerObject serverObject, IRopHandler ropHandler, ArraySegment<byte> outputBuffer)
		{
			this.result = ropHandler.GetOwningServers(serverObject, this.folderId, RopGetOwningServers.resultFactory);
		}

		// Token: 0x0400082A RID: 2090
		private const RopId RopType = RopId.GetOwningServers;

		// Token: 0x0400082B RID: 2091
		private static GetOwningServersResultFactory resultFactory = new GetOwningServersResultFactory();

		// Token: 0x0400082C RID: 2092
		private StoreId folderId;
	}
}
