using System;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x020002EC RID: 748
	internal sealed class RopImportReads : InputRop
	{
		// Token: 0x17000320 RID: 800
		// (get) Token: 0x0600116D RID: 4461 RVA: 0x00030135 File Offset: 0x0002E335
		internal override RopId RopId
		{
			get
			{
				return RopId.ImportReads;
			}
		}

		// Token: 0x0600116E RID: 4462 RVA: 0x0003013C File Offset: 0x0002E33C
		internal static Rop CreateRop()
		{
			return new RopImportReads();
		}

		// Token: 0x0600116F RID: 4463 RVA: 0x00030143 File Offset: 0x0002E343
		internal void SetInput(byte logonIndex, byte handleTableIndex, MessageReadState[] messageReadStates)
		{
			base.SetCommonInput(logonIndex, handleTableIndex);
			this.messageReadStates = messageReadStates;
		}

		// Token: 0x06001170 RID: 4464 RVA: 0x00030154 File Offset: 0x0002E354
		protected override void InternalSerializeInput(Writer writer, Encoding string8Encoding)
		{
			base.InternalSerializeInput(writer, string8Encoding);
			writer.WriteSizedMessageReadStates(this.messageReadStates);
		}

		// Token: 0x06001171 RID: 4465 RVA: 0x0003016A File Offset: 0x0002E36A
		protected override void InternalParseOutput(Reader reader, Encoding string8Encoding)
		{
			base.InternalParseOutput(reader, string8Encoding);
			this.result = RopResult.Parse(reader, new RopResult.ResultParserDelegate(StandardRopResult.ParseSuccessResult), new RopResult.ResultParserDelegate(StandardRopResult.ParseFailResult));
		}

		// Token: 0x06001172 RID: 4466 RVA: 0x00030198 File Offset: 0x0002E398
		protected override void InternalExecute(IServerObject sourceServerObject, IRopHandler ropHandler, ArraySegment<byte> outputBuffer)
		{
			this.result = ropHandler.ImportReads(sourceServerObject, this.messageReadStates, RopImportReads.resultFactory);
		}

		// Token: 0x06001173 RID: 4467 RVA: 0x000301B2 File Offset: 0x0002E3B2
		protected override IResultFactory GetDefaultResultFactory(IConnectionInformation connection, ArraySegment<byte> outputBuffer)
		{
			return RopImportReads.resultFactory;
		}

		// Token: 0x06001174 RID: 4468 RVA: 0x000301B9 File Offset: 0x0002E3B9
		protected override void InternalParseInput(Reader reader, ServerObjectHandleTable serverObjectHandleTable)
		{
			base.InternalParseInput(reader, serverObjectHandleTable);
			this.messageReadStates = reader.ReadSizeAndMessageReadStateArray();
		}

		// Token: 0x06001175 RID: 4469 RVA: 0x000301CF File Offset: 0x0002E3CF
		protected override void InternalSerializeOutput(Writer writer)
		{
			base.InternalSerializeOutput(writer);
			this.result.Serialize(writer);
		}

		// Token: 0x0400093C RID: 2364
		private const RopId RopType = RopId.ImportReads;

		// Token: 0x0400093D RID: 2365
		private static ImportReadsResultFactory resultFactory = new ImportReadsResultFactory();

		// Token: 0x0400093E RID: 2366
		private MessageReadState[] messageReadStates;
	}
}
