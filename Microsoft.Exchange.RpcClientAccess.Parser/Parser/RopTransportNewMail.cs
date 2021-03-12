using System;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x0200035A RID: 858
	internal sealed class RopTransportNewMail : InputRop
	{
		// Token: 0x1700038C RID: 908
		// (get) Token: 0x060014C5 RID: 5317 RVA: 0x000365AE File Offset: 0x000347AE
		internal override RopId RopId
		{
			get
			{
				return RopId.TransportNewMail;
			}
		}

		// Token: 0x060014C6 RID: 5318 RVA: 0x000365B2 File Offset: 0x000347B2
		internal static Rop CreateRop()
		{
			return new RopTransportNewMail();
		}

		// Token: 0x060014C7 RID: 5319 RVA: 0x000365B9 File Offset: 0x000347B9
		internal void SetInput(byte logonIndex, byte handleTableIndex, StoreId folderId, StoreId messageId, string messageClass, MessageFlags messageFlags)
		{
			base.SetCommonInput(logonIndex, handleTableIndex);
			this.folderId = folderId;
			this.messageId = messageId;
			this.messageClass = messageClass;
			this.messageFlags = messageFlags;
		}

		// Token: 0x060014C8 RID: 5320 RVA: 0x000365E2 File Offset: 0x000347E2
		protected override void InternalSerializeInput(Writer writer, Encoding string8Encoding)
		{
			base.InternalSerializeInput(writer, string8Encoding);
			this.messageId.Serialize(writer);
			this.folderId.Serialize(writer);
			writer.WriteAsciiString(this.messageClass, StringFlags.IncludeNull);
			writer.WriteUInt32((uint)this.messageFlags);
		}

		// Token: 0x060014C9 RID: 5321 RVA: 0x0003661D File Offset: 0x0003481D
		protected override void InternalParseOutput(Reader reader, Encoding string8Encoding)
		{
			base.InternalParseOutput(reader, string8Encoding);
			this.result = RopResult.Parse(reader, new RopResult.ResultParserDelegate(StandardRopResult.ParseSuccessResult), new RopResult.ResultParserDelegate(StandardRopResult.ParseFailResult));
		}

		// Token: 0x060014CA RID: 5322 RVA: 0x0003664B File Offset: 0x0003484B
		protected override IResultFactory GetDefaultResultFactory(IConnectionInformation connection, ArraySegment<byte> outputBuffer)
		{
			return RopTransportNewMail.resultFactory;
		}

		// Token: 0x060014CB RID: 5323 RVA: 0x00036652 File Offset: 0x00034852
		protected override void InternalParseInput(Reader reader, ServerObjectHandleTable serverObjectHandleTable)
		{
			base.InternalParseInput(reader, serverObjectHandleTable);
			this.messageId = StoreId.Parse(reader);
			this.folderId = StoreId.Parse(reader);
			this.messageClass = reader.ReadAsciiString(StringFlags.IncludeNull);
			this.messageFlags = (MessageFlags)reader.ReadUInt32();
		}

		// Token: 0x060014CC RID: 5324 RVA: 0x0003668D File Offset: 0x0003488D
		protected override void InternalSerializeOutput(Writer writer)
		{
			base.InternalSerializeOutput(writer);
			this.result.Serialize(writer);
		}

		// Token: 0x060014CD RID: 5325 RVA: 0x000366A2 File Offset: 0x000348A2
		protected override void InternalExecute(IServerObject serverObject, IRopHandler ropHandler, ArraySegment<byte> outputBuffer)
		{
			this.result = ropHandler.TransportNewMail(serverObject, this.folderId, this.messageId, this.messageClass, this.messageFlags, RopTransportNewMail.resultFactory);
		}

		// Token: 0x060014CE RID: 5326 RVA: 0x000366D0 File Offset: 0x000348D0
		internal override void AppendToString(StringBuilder stringBuilder)
		{
			base.AppendToString(stringBuilder);
			stringBuilder.Append(" MID=").Append(this.messageId.ToString());
			stringBuilder.Append(" FID=").Append(this.folderId.ToString());
			stringBuilder.Append(" [").Append(this.messageClass).Append("]");
		}

		// Token: 0x04000B02 RID: 2818
		private const RopId RopType = RopId.TransportNewMail;

		// Token: 0x04000B03 RID: 2819
		private static TransportNewMailResultFactory resultFactory = new TransportNewMailResultFactory();

		// Token: 0x04000B04 RID: 2820
		private StoreId messageId;

		// Token: 0x04000B05 RID: 2821
		private StoreId folderId;

		// Token: 0x04000B06 RID: 2822
		private string messageClass;

		// Token: 0x04000B07 RID: 2823
		private MessageFlags messageFlags;
	}
}
