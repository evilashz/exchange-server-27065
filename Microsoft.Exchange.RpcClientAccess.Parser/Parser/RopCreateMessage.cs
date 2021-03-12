using System;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000294 RID: 660
	internal sealed class RopCreateMessage : InputOutputRop
	{
		// Token: 0x170002CD RID: 717
		// (get) Token: 0x06000E88 RID: 3720 RVA: 0x0002B20D File Offset: 0x0002940D
		internal override RopId RopId
		{
			get
			{
				return RopId.CreateMessage;
			}
		}

		// Token: 0x06000E89 RID: 3721 RVA: 0x0002B210 File Offset: 0x00029410
		internal static Rop CreateRop()
		{
			return new RopCreateMessage();
		}

		// Token: 0x06000E8A RID: 3722 RVA: 0x0002B217 File Offset: 0x00029417
		internal void SetInput(byte logonIndex, byte handleTableIndex, byte returnHandleTableIndex, ushort codePageId, StoreId folderId, bool createAssociated)
		{
			base.SetCommonInput(logonIndex, handleTableIndex, returnHandleTableIndex);
			this.codePageId = codePageId;
			this.folderId = folderId;
			this.createAssociated = createAssociated;
		}

		// Token: 0x06000E8B RID: 3723 RVA: 0x0002B23A File Offset: 0x0002943A
		protected override void InternalSerializeInput(Writer writer, Encoding string8Encoding)
		{
			base.InternalSerializeInput(writer, string8Encoding);
			writer.WriteUInt16(this.codePageId);
			this.folderId.Serialize(writer);
			writer.WriteBool(this.createAssociated);
		}

		// Token: 0x06000E8C RID: 3724 RVA: 0x0002B268 File Offset: 0x00029468
		protected override void InternalParseOutput(Reader reader, Encoding string8Encoding)
		{
			base.InternalParseOutput(reader, string8Encoding);
			this.result = RopResult.Parse(reader, new RopResult.ResultParserDelegate(SuccessfulCreateMessageResult.Parse), new RopResult.ResultParserDelegate(StandardRopResult.ParseFailResult));
		}

		// Token: 0x06000E8D RID: 3725 RVA: 0x0002B296 File Offset: 0x00029496
		protected override IResultFactory GetDefaultResultFactory(IConnectionInformation connection, ArraySegment<byte> outputBuffer)
		{
			return RopCreateMessage.resultFactory;
		}

		// Token: 0x06000E8E RID: 3726 RVA: 0x0002B29D File Offset: 0x0002949D
		protected override void InternalParseInput(Reader reader, ServerObjectHandleTable serverObjectHandleTable)
		{
			base.InternalParseInput(reader, serverObjectHandleTable);
			this.codePageId = reader.ReadUInt16();
			this.folderId = StoreId.Parse(reader);
			this.createAssociated = reader.ReadBool();
		}

		// Token: 0x06000E8F RID: 3727 RVA: 0x0002B2CB File Offset: 0x000294CB
		protected override void InternalSerializeOutput(Writer writer)
		{
			base.InternalSerializeOutput(writer);
			this.result.Serialize(writer);
		}

		// Token: 0x06000E90 RID: 3728 RVA: 0x0002B2E0 File Offset: 0x000294E0
		protected override void InternalExecute(IServerObject serverObject, IRopHandler ropHandler, ArraySegment<byte> outputBuffer)
		{
			this.result = ropHandler.CreateMessage(serverObject, this.codePageId, this.folderId, this.createAssociated, RopCreateMessage.resultFactory);
		}

		// Token: 0x06000E91 RID: 3729 RVA: 0x0002B308 File Offset: 0x00029508
		internal override void AppendToString(StringBuilder stringBuilder)
		{
			base.AppendToString(stringBuilder);
			stringBuilder.Append(" CPID=").Append(this.codePageId);
			stringBuilder.Append(" FID=").Append(this.folderId.ToString());
			stringBuilder.Append(" Associated=").Append(this.createAssociated);
		}

		// Token: 0x0400076F RID: 1903
		private const RopId RopType = RopId.CreateMessage;

		// Token: 0x04000770 RID: 1904
		private static CreateMessageResultFactory resultFactory = new CreateMessageResultFactory();

		// Token: 0x04000771 RID: 1905
		private ushort codePageId;

		// Token: 0x04000772 RID: 1906
		private StoreId folderId;

		// Token: 0x04000773 RID: 1907
		private bool createAssociated;
	}
}
