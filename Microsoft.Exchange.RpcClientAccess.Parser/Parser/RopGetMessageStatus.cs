using System;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x020002CA RID: 714
	internal sealed class RopGetMessageStatus : InputRop
	{
		// Token: 0x170002FF RID: 767
		// (get) Token: 0x06001047 RID: 4167 RVA: 0x0002E518 File Offset: 0x0002C718
		internal override RopId RopId
		{
			get
			{
				return RopId.GetMessageStatus;
			}
		}

		// Token: 0x06001048 RID: 4168 RVA: 0x0002E51C File Offset: 0x0002C71C
		internal static Rop CreateRop()
		{
			return new RopGetMessageStatus();
		}

		// Token: 0x06001049 RID: 4169 RVA: 0x0002E523 File Offset: 0x0002C723
		internal void SetInput(byte logonIndex, byte handleTableIndex, StoreId messageId)
		{
			base.SetCommonInput(logonIndex, handleTableIndex);
			this.messageId = messageId;
		}

		// Token: 0x0600104A RID: 4170 RVA: 0x0002E534 File Offset: 0x0002C734
		protected override void InternalSerializeInput(Writer writer, Encoding string8Encoding)
		{
			base.InternalSerializeInput(writer, string8Encoding);
			this.messageId.Serialize(writer);
		}

		// Token: 0x0600104B RID: 4171 RVA: 0x0002E54A File Offset: 0x0002C74A
		protected override void InternalParseOutput(Reader reader, Encoding string8Encoding)
		{
			base.InternalParseOutput(reader, string8Encoding);
			this.result = RopResult.Parse(reader, new RopResult.ResultParserDelegate(SuccessfulGetMessageStatusResult.Parse), new RopResult.ResultParserDelegate(StandardRopResult.ParseFailResult));
		}

		// Token: 0x0600104C RID: 4172 RVA: 0x0002E578 File Offset: 0x0002C778
		protected override IResultFactory GetDefaultResultFactory(IConnectionInformation connection, ArraySegment<byte> outputBuffer)
		{
			return RopGetMessageStatus.resultFactory;
		}

		// Token: 0x0600104D RID: 4173 RVA: 0x0002E57F File Offset: 0x0002C77F
		protected override void InternalParseInput(Reader reader, ServerObjectHandleTable serverObjectHandleTable)
		{
			base.InternalParseInput(reader, serverObjectHandleTable);
			this.messageId = StoreId.Parse(reader);
		}

		// Token: 0x0600104E RID: 4174 RVA: 0x0002E595 File Offset: 0x0002C795
		protected override void InternalSerializeOutput(Writer writer)
		{
			base.InternalSerializeOutput(writer);
			this.result.Serialize(writer);
		}

		// Token: 0x0600104F RID: 4175 RVA: 0x0002E5AA File Offset: 0x0002C7AA
		protected override void InternalExecute(IServerObject serverObject, IRopHandler ropHandler, ArraySegment<byte> outputBuffer)
		{
			this.result = ropHandler.GetMessageStatus(serverObject, this.messageId, RopGetMessageStatus.resultFactory);
		}

		// Token: 0x06001050 RID: 4176 RVA: 0x0002E5C4 File Offset: 0x0002C7C4
		internal override void AppendToString(StringBuilder stringBuilder)
		{
			base.AppendToString(stringBuilder);
			stringBuilder.Append(" MID=").Append(this.messageId.ToString());
		}

		// Token: 0x04000820 RID: 2080
		private const RopId RopType = RopId.GetMessageStatus;

		// Token: 0x04000821 RID: 2081
		private static GetMessageStatusResultFactory resultFactory = new GetMessageStatusResultFactory();

		// Token: 0x04000822 RID: 2082
		private StoreId messageId;
	}
}
