using System;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x0200030C RID: 780
	internal sealed class RopOpenAttachment : InputOutputRop
	{
		// Token: 0x17000344 RID: 836
		// (get) Token: 0x0600122B RID: 4651 RVA: 0x00031F01 File Offset: 0x00030101
		internal override RopId RopId
		{
			get
			{
				return RopId.OpenAttachment;
			}
		}

		// Token: 0x0600122C RID: 4652 RVA: 0x00031F05 File Offset: 0x00030105
		internal static Rop CreateRop()
		{
			return new RopOpenAttachment();
		}

		// Token: 0x0600122D RID: 4653 RVA: 0x00031F0C File Offset: 0x0003010C
		internal void SetInput(byte logonIndex, byte handleTableIndex, byte returnHandleTableIndex, OpenMode openMode, uint attachmentNumber)
		{
			base.SetCommonInput(logonIndex, handleTableIndex, returnHandleTableIndex);
			this.openMode = openMode;
			this.attachmentNumber = attachmentNumber;
		}

		// Token: 0x0600122E RID: 4654 RVA: 0x00031F27 File Offset: 0x00030127
		protected override void InternalSerializeInput(Writer writer, Encoding string8Encoding)
		{
			base.InternalSerializeInput(writer, string8Encoding);
			writer.WriteByte((byte)this.openMode);
			writer.WriteUInt32(this.attachmentNumber);
		}

		// Token: 0x0600122F RID: 4655 RVA: 0x00031F49 File Offset: 0x00030149
		protected override void InternalParseOutput(Reader reader, Encoding string8Encoding)
		{
			base.InternalParseOutput(reader, string8Encoding);
			this.result = RopResult.Parse(reader, new RopResult.ResultParserDelegate(SuccessfulOpenAttachmentResult.Parse), new RopResult.ResultParserDelegate(StandardRopResult.ParseFailResult));
		}

		// Token: 0x06001230 RID: 4656 RVA: 0x00031F77 File Offset: 0x00030177
		protected override IResultFactory GetDefaultResultFactory(IConnectionInformation connection, ArraySegment<byte> outputBuffer)
		{
			return RopOpenAttachment.resultFactory;
		}

		// Token: 0x06001231 RID: 4657 RVA: 0x00031F80 File Offset: 0x00030180
		protected override void InternalParseInput(Reader reader, ServerObjectHandleTable serverObjectHandleTable)
		{
			base.InternalParseInput(reader, serverObjectHandleTable);
			this.openMode = (OpenMode)reader.ReadByte();
			this.attachmentNumber = reader.ReadUInt32();
			if (this.attachmentNumber > 1024U)
			{
				throw new BufferParseException(string.Format("Attachment number is greater then maximum attachment number allowed. Maximum: {0}. Actual: {1}", 1024U, this.attachmentNumber));
			}
		}

		// Token: 0x06001232 RID: 4658 RVA: 0x00031FDF File Offset: 0x000301DF
		protected override void InternalSerializeOutput(Writer writer)
		{
			base.InternalSerializeOutput(writer);
			this.result.Serialize(writer);
		}

		// Token: 0x06001233 RID: 4659 RVA: 0x00031FF4 File Offset: 0x000301F4
		protected override void InternalExecute(IServerObject serverObject, IRopHandler ropHandler, ArraySegment<byte> outputBuffer)
		{
			this.result = ropHandler.OpenAttachment(serverObject, this.openMode, this.attachmentNumber, RopOpenAttachment.resultFactory);
		}

		// Token: 0x06001234 RID: 4660 RVA: 0x00032014 File Offset: 0x00030214
		internal override void AppendToString(StringBuilder stringBuilder)
		{
			base.AppendToString(stringBuilder);
			stringBuilder.Append(" OpenMode=").Append(this.openMode);
			stringBuilder.Append(" Number=").Append(this.attachmentNumber);
		}

		// Token: 0x040009D5 RID: 2517
		private const RopId RopType = RopId.OpenAttachment;

		// Token: 0x040009D6 RID: 2518
		private const uint MaxAttachmentNumber = 1024U;

		// Token: 0x040009D7 RID: 2519
		private static OpenAttachmentResultFactory resultFactory = new OpenAttachmentResultFactory();

		// Token: 0x040009D8 RID: 2520
		private OpenMode openMode;

		// Token: 0x040009D9 RID: 2521
		private uint attachmentNumber;
	}
}
