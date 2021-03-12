using System;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000296 RID: 662
	internal sealed class RopDeleteAttachment : InputRop
	{
		// Token: 0x170002CF RID: 719
		// (get) Token: 0x06000EA0 RID: 3744 RVA: 0x0002B509 File Offset: 0x00029709
		internal override RopId RopId
		{
			get
			{
				return RopId.DeleteAttachment;
			}
		}

		// Token: 0x06000EA1 RID: 3745 RVA: 0x0002B50D File Offset: 0x0002970D
		internal static Rop CreateRop()
		{
			return new RopDeleteAttachment();
		}

		// Token: 0x06000EA2 RID: 3746 RVA: 0x0002B514 File Offset: 0x00029714
		internal void SetInput(byte logonIndex, byte handleTableIndex, uint attachmentNumber)
		{
			base.SetCommonInput(logonIndex, handleTableIndex);
			this.attachmentNumber = attachmentNumber;
		}

		// Token: 0x06000EA3 RID: 3747 RVA: 0x0002B525 File Offset: 0x00029725
		protected override void InternalSerializeInput(Writer writer, Encoding string8Encoding)
		{
			base.InternalSerializeInput(writer, string8Encoding);
			writer.WriteUInt32(this.attachmentNumber);
		}

		// Token: 0x06000EA4 RID: 3748 RVA: 0x0002B53B File Offset: 0x0002973B
		protected override void InternalParseOutput(Reader reader, Encoding string8Encoding)
		{
			base.InternalParseOutput(reader, string8Encoding);
			this.result = RopResult.Parse(reader, new RopResult.ResultParserDelegate(StandardRopResult.ParseSuccessResult), new RopResult.ResultParserDelegate(StandardRopResult.ParseFailResult));
		}

		// Token: 0x06000EA5 RID: 3749 RVA: 0x0002B569 File Offset: 0x00029769
		protected override IResultFactory GetDefaultResultFactory(IConnectionInformation connection, ArraySegment<byte> outputBuffer)
		{
			return RopDeleteAttachment.resultFactory;
		}

		// Token: 0x06000EA6 RID: 3750 RVA: 0x0002B570 File Offset: 0x00029770
		protected override void InternalParseInput(Reader reader, ServerObjectHandleTable serverObjectHandleTable)
		{
			base.InternalParseInput(reader, serverObjectHandleTable);
			this.attachmentNumber = reader.ReadUInt32();
		}

		// Token: 0x06000EA7 RID: 3751 RVA: 0x0002B586 File Offset: 0x00029786
		protected override void InternalSerializeOutput(Writer writer)
		{
			base.InternalSerializeOutput(writer);
			this.result.Serialize(writer);
		}

		// Token: 0x06000EA8 RID: 3752 RVA: 0x0002B59B File Offset: 0x0002979B
		protected override void InternalExecute(IServerObject serverObject, IRopHandler ropHandler, ArraySegment<byte> outputBuffer)
		{
			this.result = ropHandler.DeleteAttachment(serverObject, this.attachmentNumber, RopDeleteAttachment.resultFactory);
		}

		// Token: 0x06000EA9 RID: 3753 RVA: 0x0002B5B5 File Offset: 0x000297B5
		internal override void AppendToString(StringBuilder stringBuilder)
		{
			base.AppendToString(stringBuilder);
			stringBuilder.Append(" Number=").Append(this.attachmentNumber);
		}

		// Token: 0x04000779 RID: 1913
		private const RopId RopType = RopId.DeleteAttachment;

		// Token: 0x0400077A RID: 1914
		private static DeleteAttachmentResultFactory resultFactory = new DeleteAttachmentResultFactory();

		// Token: 0x0400077B RID: 1915
		private uint attachmentNumber;
	}
}
