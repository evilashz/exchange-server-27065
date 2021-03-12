using System;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000312 RID: 786
	internal sealed class RopPrereadMessages : InputRop
	{
		// Token: 0x1700034B RID: 843
		// (get) Token: 0x06001271 RID: 4721 RVA: 0x00032704 File Offset: 0x00030904
		internal override RopId RopId
		{
			get
			{
				return RopId.PrereadMessages;
			}
		}

		// Token: 0x06001272 RID: 4722 RVA: 0x0003270B File Offset: 0x0003090B
		internal static Rop CreateRop()
		{
			return new RopPrereadMessages();
		}

		// Token: 0x06001273 RID: 4723 RVA: 0x00032712 File Offset: 0x00030912
		internal override void AppendToString(StringBuilder stringBuilder)
		{
			base.AppendToString(stringBuilder);
			stringBuilder.Append(" Messages={");
			Util.AppendToString<StoreIdPair>(stringBuilder, this.messages);
			stringBuilder.Append("}");
		}

		// Token: 0x06001274 RID: 4724 RVA: 0x0003273F File Offset: 0x0003093F
		internal void SetInput(byte logonIndex, byte handleTableIndex, StoreIdPair[] messages)
		{
			base.SetCommonInput(logonIndex, handleTableIndex);
			this.messages = messages;
		}

		// Token: 0x06001275 RID: 4725 RVA: 0x00032750 File Offset: 0x00030950
		protected override void InternalSerializeInput(Writer writer, Encoding string8Encoding)
		{
			base.InternalSerializeInput(writer, string8Encoding);
			writer.WriteCountedStoreIdPairs(this.messages);
		}

		// Token: 0x06001276 RID: 4726 RVA: 0x00032766 File Offset: 0x00030966
		protected override void InternalParseOutput(Reader reader, Encoding string8Encoding)
		{
			base.InternalParseOutput(reader, string8Encoding);
			this.result = RopResult.Parse(reader, new RopResult.ResultParserDelegate(StandardRopResult.ParseSuccessResult), new RopResult.ResultParserDelegate(StandardRopResult.ParseFailResult));
		}

		// Token: 0x06001277 RID: 4727 RVA: 0x00032794 File Offset: 0x00030994
		protected override IResultFactory GetDefaultResultFactory(IConnectionInformation connection, ArraySegment<byte> outputBuffer)
		{
			return RopPrereadMessages.resultFactory;
		}

		// Token: 0x06001278 RID: 4728 RVA: 0x0003279B File Offset: 0x0003099B
		protected override void InternalParseInput(Reader reader, ServerObjectHandleTable serverObjectHandleTable)
		{
			base.InternalParseInput(reader, serverObjectHandleTable);
			this.messages = reader.ReadSizeAndStoreIdPairArray();
		}

		// Token: 0x06001279 RID: 4729 RVA: 0x000327B1 File Offset: 0x000309B1
		protected override void InternalSerializeOutput(Writer writer)
		{
			base.InternalSerializeOutput(writer);
			this.result.Serialize(writer);
		}

		// Token: 0x0600127A RID: 4730 RVA: 0x000327C6 File Offset: 0x000309C6
		protected override void InternalExecute(IServerObject serverObject, IRopHandler ropHandler, ArraySegment<byte> outputBuffer)
		{
			this.result = ropHandler.PrereadMessages(serverObject, this.messages, RopPrereadMessages.resultFactory);
		}

		// Token: 0x040009ED RID: 2541
		private const RopId RopType = RopId.PrereadMessages;

		// Token: 0x040009EE RID: 2542
		private static PrereadMessagesResultFactory resultFactory = new PrereadMessagesResultFactory();

		// Token: 0x040009EF RID: 2543
		private StoreIdPair[] messages;
	}
}
