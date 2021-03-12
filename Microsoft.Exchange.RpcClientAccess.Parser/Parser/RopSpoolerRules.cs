using System;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x0200034E RID: 846
	internal sealed class RopSpoolerRules : InputRop
	{
		// Token: 0x17000383 RID: 899
		// (get) Token: 0x0600146D RID: 5229 RVA: 0x00035E75 File Offset: 0x00034075
		internal override RopId RopId
		{
			get
			{
				return RopId.SpoolerRules;
			}
		}

		// Token: 0x17000384 RID: 900
		// (get) Token: 0x0600146E RID: 5230 RVA: 0x00035E79 File Offset: 0x00034079
		internal override byte InputHandleTableIndex
		{
			get
			{
				return this.realHandleTableIndex;
			}
		}

		// Token: 0x0600146F RID: 5231 RVA: 0x00035E81 File Offset: 0x00034081
		internal static Rop CreateRop()
		{
			return new RopSpoolerRules();
		}

		// Token: 0x06001470 RID: 5232 RVA: 0x00035E88 File Offset: 0x00034088
		internal override void AppendToString(StringBuilder stringBuilder)
		{
			base.AppendToString(stringBuilder);
			stringBuilder.Append(" FID=").Append(this.folderId.ToString());
		}

		// Token: 0x06001471 RID: 5233 RVA: 0x00035EB3 File Offset: 0x000340B3
		internal void SetInput(byte logonIndex, byte handleTableIndex, byte realHandleTableIndex, StoreId folderId)
		{
			this.realHandleTableIndex = realHandleTableIndex;
			this.folderId = folderId;
		}

		// Token: 0x06001472 RID: 5234 RVA: 0x00035EC4 File Offset: 0x000340C4
		protected override void InternalParseOutput(Reader reader, Encoding string8Encoding)
		{
			base.InternalParseOutput(reader, string8Encoding);
			this.result = RopResult.Parse(reader, new RopResult.ResultParserDelegate(SuccessfulSpoolerRulesResult.Parse), new RopResult.ResultParserDelegate(StandardRopResult.ParseFailResult));
		}

		// Token: 0x06001473 RID: 5235 RVA: 0x00035EF2 File Offset: 0x000340F2
		protected override void InternalSerializeInput(Writer writer, Encoding string8Encoding)
		{
			base.InternalSerializeInput(writer, string8Encoding);
			writer.WriteByte(this.realHandleTableIndex);
			this.folderId.Serialize(writer);
		}

		// Token: 0x06001474 RID: 5236 RVA: 0x00035F14 File Offset: 0x00034114
		protected override IResultFactory GetDefaultResultFactory(IConnectionInformation connection, ArraySegment<byte> outputBuffer)
		{
			return RopSpoolerRules.resultFactory;
		}

		// Token: 0x06001475 RID: 5237 RVA: 0x00035F1B File Offset: 0x0003411B
		protected override void InternalParseInput(Reader reader, ServerObjectHandleTable serverObjectHandleTable)
		{
			base.InternalParseInput(reader, serverObjectHandleTable);
			this.realHandleTableIndex = reader.ReadByte();
			this.folderId = StoreId.Parse(reader);
		}

		// Token: 0x06001476 RID: 5238 RVA: 0x00035F3D File Offset: 0x0003413D
		protected override void InternalSerializeOutput(Writer writer)
		{
			base.InternalSerializeOutput(writer);
			this.result.Serialize(writer);
		}

		// Token: 0x06001477 RID: 5239 RVA: 0x00035F52 File Offset: 0x00034152
		protected override void InternalExecute(IServerObject serverObject, IRopHandler ropHandler, ArraySegment<byte> outputBuffer)
		{
			this.result = ropHandler.SpoolerRules(serverObject, this.folderId, RopSpoolerRules.resultFactory);
		}

		// Token: 0x04000AC8 RID: 2760
		private const RopId RopType = RopId.SpoolerRules;

		// Token: 0x04000AC9 RID: 2761
		private static SpoolerRulesResultFactory resultFactory = new SpoolerRulesResultFactory();

		// Token: 0x04000ACA RID: 2762
		private byte realHandleTableIndex;

		// Token: 0x04000ACB RID: 2763
		private StoreId folderId;
	}
}
