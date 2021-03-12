using System;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x020002AF RID: 687
	internal sealed class RopFastTransferSourceCopyMessages : InputOutputRop
	{
		// Token: 0x170002E6 RID: 742
		// (get) Token: 0x06000F62 RID: 3938 RVA: 0x0002C9CA File Offset: 0x0002ABCA
		internal override RopId RopId
		{
			get
			{
				return RopId.FastTransferSourceCopyMessages;
			}
		}

		// Token: 0x06000F63 RID: 3939 RVA: 0x0002C9CE File Offset: 0x0002ABCE
		internal static Rop CreateRop()
		{
			return new RopFastTransferSourceCopyMessages();
		}

		// Token: 0x06000F64 RID: 3940 RVA: 0x0002C9D5 File Offset: 0x0002ABD5
		internal void SetInput(byte logonIndex, byte handleTableIndex, byte returnHandleTableIndex, StoreId[] messageIds, FastTransferCopyMessagesFlag flags, FastTransferSendOption sendOptions)
		{
			base.SetCommonInput(logonIndex, handleTableIndex, returnHandleTableIndex);
			this.messageIds = messageIds;
			this.flags = flags;
			this.sendOptions = sendOptions;
		}

		// Token: 0x06000F65 RID: 3941 RVA: 0x0002C9F8 File Offset: 0x0002ABF8
		protected override void InternalSerializeInput(Writer writer, Encoding string8Encoding)
		{
			base.InternalSerializeInput(writer, string8Encoding);
			writer.WriteCountedStoreIds(this.messageIds);
			writer.WriteByte((byte)this.flags);
			writer.WriteByte((byte)this.sendOptions);
		}

		// Token: 0x06000F66 RID: 3942 RVA: 0x0002CA26 File Offset: 0x0002AC26
		protected override void InternalParseOutput(Reader reader, Encoding string8Encoding)
		{
			base.InternalParseOutput(reader, string8Encoding);
			this.result = RopResult.Parse(reader, new RopResult.ResultParserDelegate(SuccessfulFastTransferSourceCopyMessagesResult.Parse), new RopResult.ResultParserDelegate(StandardRopResult.ParseFailResult));
		}

		// Token: 0x06000F67 RID: 3943 RVA: 0x0002CA54 File Offset: 0x0002AC54
		protected override IResultFactory GetDefaultResultFactory(IConnectionInformation connection, ArraySegment<byte> outputBuffer)
		{
			return RopFastTransferSourceCopyMessages.resultFactory;
		}

		// Token: 0x06000F68 RID: 3944 RVA: 0x0002CA5B File Offset: 0x0002AC5B
		protected override void InternalParseInput(Reader reader, ServerObjectHandleTable serverObjectHandleTable)
		{
			base.InternalParseInput(reader, serverObjectHandleTable);
			this.messageIds = reader.ReadSizeAndStoreIdArray();
			this.flags = (FastTransferCopyMessagesFlag)reader.ReadByte();
			this.sendOptions = (FastTransferSendOption)reader.ReadByte();
		}

		// Token: 0x06000F69 RID: 3945 RVA: 0x0002CA89 File Offset: 0x0002AC89
		protected override void InternalSerializeOutput(Writer writer)
		{
			base.InternalSerializeOutput(writer);
			this.result.Serialize(writer);
		}

		// Token: 0x06000F6A RID: 3946 RVA: 0x0002CA9E File Offset: 0x0002AC9E
		protected override void InternalExecute(IServerObject serverObject, IRopHandler ropHandler, ArraySegment<byte> outputBuffer)
		{
			this.result = ropHandler.FastTransferSourceCopyMessages(serverObject, this.messageIds, this.flags, this.sendOptions, RopFastTransferSourceCopyMessages.resultFactory);
		}

		// Token: 0x06000F6B RID: 3947 RVA: 0x0002CAC4 File Offset: 0x0002ACC4
		internal override void AppendToString(StringBuilder stringBuilder)
		{
			base.AppendToString(stringBuilder);
			stringBuilder.Append(" flags=").Append(this.flags.ToString());
			stringBuilder.Append(" sendOptions=").Append(this.sendOptions.ToString());
			stringBuilder.Append(" messageIds=[");
			Util.AppendToString<StoreId>(stringBuilder, this.messageIds);
			stringBuilder.Append("]");
		}

		// Token: 0x040007C2 RID: 1986
		private const RopId RopType = RopId.FastTransferSourceCopyMessages;

		// Token: 0x040007C3 RID: 1987
		private static FastTransferSourceCopyMessagesResultFactory resultFactory = new FastTransferSourceCopyMessagesResultFactory();

		// Token: 0x040007C4 RID: 1988
		private StoreId[] messageIds;

		// Token: 0x040007C5 RID: 1989
		private FastTransferCopyMessagesFlag flags;

		// Token: 0x040007C6 RID: 1990
		private FastTransferSendOption sendOptions;
	}
}
