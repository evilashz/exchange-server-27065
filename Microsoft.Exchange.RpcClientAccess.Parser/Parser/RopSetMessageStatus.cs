using System;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x0200033B RID: 827
	internal sealed class RopSetMessageStatus : InputRop
	{
		// Token: 0x17000370 RID: 880
		// (get) Token: 0x060013CB RID: 5067 RVA: 0x00034D6A File Offset: 0x00032F6A
		internal override RopId RopId
		{
			get
			{
				return RopId.SetMessageStatus;
			}
		}

		// Token: 0x060013CC RID: 5068 RVA: 0x00034D6E File Offset: 0x00032F6E
		internal static Rop CreateRop()
		{
			return new RopSetMessageStatus();
		}

		// Token: 0x060013CD RID: 5069 RVA: 0x00034D75 File Offset: 0x00032F75
		internal void SetInput(byte logonIndex, byte handleTableIndex, StoreId messageId, MessageStatusFlags status, MessageStatusFlags statusMask)
		{
			base.SetCommonInput(logonIndex, handleTableIndex);
			this.messageId = messageId;
			this.status = status;
			this.statusMask = statusMask;
		}

		// Token: 0x060013CE RID: 5070 RVA: 0x00034D96 File Offset: 0x00032F96
		protected override void InternalSerializeInput(Writer writer, Encoding string8Encoding)
		{
			base.InternalSerializeInput(writer, string8Encoding);
			this.messageId.Serialize(writer);
			writer.WriteUInt32((uint)this.status);
			writer.WriteUInt32((uint)this.statusMask);
		}

		// Token: 0x060013CF RID: 5071 RVA: 0x00034DC4 File Offset: 0x00032FC4
		protected override void InternalParseOutput(Reader reader, Encoding string8Encoding)
		{
			base.InternalParseOutput(reader, string8Encoding);
			this.result = RopResult.Parse(reader, new RopResult.ResultParserDelegate(SuccessfulSetMessageStatusResult.Parse), new RopResult.ResultParserDelegate(StandardRopResult.ParseFailResult));
		}

		// Token: 0x060013D0 RID: 5072 RVA: 0x00034DF2 File Offset: 0x00032FF2
		protected override IResultFactory GetDefaultResultFactory(IConnectionInformation connection, ArraySegment<byte> outputBuffer)
		{
			return RopSetMessageStatus.resultFactory;
		}

		// Token: 0x060013D1 RID: 5073 RVA: 0x00034DF9 File Offset: 0x00032FF9
		protected override void InternalParseInput(Reader reader, ServerObjectHandleTable serverObjectHandleTable)
		{
			base.InternalParseInput(reader, serverObjectHandleTable);
			this.messageId = StoreId.Parse(reader);
			this.status = (MessageStatusFlags)reader.ReadUInt32();
			this.statusMask = (MessageStatusFlags)reader.ReadUInt32();
		}

		// Token: 0x060013D2 RID: 5074 RVA: 0x00034E27 File Offset: 0x00033027
		protected override void InternalSerializeOutput(Writer writer)
		{
			base.InternalSerializeOutput(writer);
			this.result.Serialize(writer);
		}

		// Token: 0x060013D3 RID: 5075 RVA: 0x00034E3C File Offset: 0x0003303C
		protected override void InternalExecute(IServerObject serverObject, IRopHandler ropHandler, ArraySegment<byte> outputBuffer)
		{
			this.result = ropHandler.SetMessageStatus(serverObject, this.messageId, this.status, this.statusMask, RopSetMessageStatus.resultFactory);
		}

		// Token: 0x060013D4 RID: 5076 RVA: 0x00034E64 File Offset: 0x00033064
		internal override void AppendToString(StringBuilder stringBuilder)
		{
			base.AppendToString(stringBuilder);
			stringBuilder.Append(" MID=").Append(this.messageId.ToString());
			stringBuilder.Append(" Status=").Append(this.status);
			stringBuilder.Append(" Mask=").Append(this.statusMask);
		}

		// Token: 0x04000A83 RID: 2691
		private const RopId RopType = RopId.SetMessageStatus;

		// Token: 0x04000A84 RID: 2692
		private static SetMessageStatusResultFactory resultFactory = new SetMessageStatusResultFactory();

		// Token: 0x04000A85 RID: 2693
		private StoreId messageId;

		// Token: 0x04000A86 RID: 2694
		private MessageStatusFlags status;

		// Token: 0x04000A87 RID: 2695
		private MessageStatusFlags statusMask;
	}
}
