using System;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x0200035D RID: 861
	internal sealed class RopUpdateDeferredActionMessages : InputRop
	{
		// Token: 0x1700038F RID: 911
		// (get) Token: 0x060014E1 RID: 5345 RVA: 0x00036896 File Offset: 0x00034A96
		internal override RopId RopId
		{
			get
			{
				return RopId.UpdateDeferredActionMessages;
			}
		}

		// Token: 0x060014E2 RID: 5346 RVA: 0x0003689A File Offset: 0x00034A9A
		internal static Rop CreateRop()
		{
			return new RopUpdateDeferredActionMessages();
		}

		// Token: 0x060014E3 RID: 5347 RVA: 0x000368A1 File Offset: 0x00034AA1
		internal void SetInput(byte logonIndex, byte handleTableIndex, byte[] serverEntryId, byte[] clientEntryId)
		{
			base.SetCommonInput(logonIndex, handleTableIndex);
			this.serverEntryId = serverEntryId;
			this.clientEntryId = clientEntryId;
		}

		// Token: 0x060014E4 RID: 5348 RVA: 0x000368BA File Offset: 0x00034ABA
		protected override void InternalSerializeInput(Writer writer, Encoding string8Encoding)
		{
			base.InternalSerializeInput(writer, string8Encoding);
			writer.WriteSizedBytes(this.serverEntryId);
			writer.WriteSizedBytes(this.clientEntryId);
		}

		// Token: 0x060014E5 RID: 5349 RVA: 0x000368DC File Offset: 0x00034ADC
		protected override void InternalParseOutput(Reader reader, Encoding string8Encoding)
		{
			base.InternalParseOutput(reader, string8Encoding);
			this.result = RopResult.Parse(reader, new RopResult.ResultParserDelegate(StandardRopResult.ParseSuccessResult), new RopResult.ResultParserDelegate(StandardRopResult.ParseFailResult));
		}

		// Token: 0x060014E6 RID: 5350 RVA: 0x0003690A File Offset: 0x00034B0A
		protected override void InternalExecute(IServerObject sourceServerObject, IRopHandler ropHandler, ArraySegment<byte> outputBuffer)
		{
			this.result = ropHandler.UpdateDeferredActionMessages(sourceServerObject, this.serverEntryId, this.clientEntryId, RopUpdateDeferredActionMessages.resultFactory);
		}

		// Token: 0x060014E7 RID: 5351 RVA: 0x0003692A File Offset: 0x00034B2A
		protected override IResultFactory GetDefaultResultFactory(IConnectionInformation connection, ArraySegment<byte> outputBuffer)
		{
			return RopUpdateDeferredActionMessages.resultFactory;
		}

		// Token: 0x060014E8 RID: 5352 RVA: 0x00036931 File Offset: 0x00034B31
		protected override void InternalParseInput(Reader reader, ServerObjectHandleTable serverObjectHandleTable)
		{
			base.InternalParseInput(reader, serverObjectHandleTable);
			this.serverEntryId = reader.ReadSizeAndByteArray();
			this.clientEntryId = reader.ReadSizeAndByteArray();
		}

		// Token: 0x060014E9 RID: 5353 RVA: 0x00036953 File Offset: 0x00034B53
		protected override void InternalSerializeOutput(Writer writer)
		{
			base.InternalSerializeOutput(writer);
			this.result.Serialize(writer);
		}

		// Token: 0x04000B0B RID: 2827
		private const RopId RopType = RopId.UpdateDeferredActionMessages;

		// Token: 0x04000B0C RID: 2828
		private static UpdateDeferredActionMessagesResultFactory resultFactory = new UpdateDeferredActionMessagesResultFactory();

		// Token: 0x04000B0D RID: 2829
		private byte[] serverEntryId;

		// Token: 0x04000B0E RID: 2830
		private byte[] clientEntryId;
	}
}
