using System;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000350 RID: 848
	internal sealed class RopSubmitMessage : InputRop
	{
		// Token: 0x17000385 RID: 901
		// (get) Token: 0x0600147A RID: 5242 RVA: 0x00035F80 File Offset: 0x00034180
		internal override RopId RopId
		{
			get
			{
				return RopId.SubmitMessage;
			}
		}

		// Token: 0x0600147B RID: 5243 RVA: 0x00035F84 File Offset: 0x00034184
		internal static Rop CreateRop()
		{
			return new RopSubmitMessage();
		}

		// Token: 0x0600147C RID: 5244 RVA: 0x00035F8B File Offset: 0x0003418B
		internal void SetInput(byte logonIndex, byte handleTableIndex, SubmitMessageFlags submitFlags)
		{
			base.SetCommonInput(logonIndex, handleTableIndex);
			this.submitFlags = submitFlags;
		}

		// Token: 0x0600147D RID: 5245 RVA: 0x00035F9C File Offset: 0x0003419C
		protected override void InternalSerializeInput(Writer writer, Encoding string8Encoding)
		{
			base.InternalSerializeInput(writer, string8Encoding);
			writer.WriteByte((byte)this.submitFlags);
		}

		// Token: 0x0600147E RID: 5246 RVA: 0x00035FB2 File Offset: 0x000341B2
		protected override void InternalParseOutput(Reader reader, Encoding string8Encoding)
		{
			base.InternalParseOutput(reader, string8Encoding);
			this.result = RopResult.Parse(reader, new RopResult.ResultParserDelegate(StandardRopResult.ParseSuccessResult), new RopResult.ResultParserDelegate(StandardRopResult.ParseFailResult));
		}

		// Token: 0x0600147F RID: 5247 RVA: 0x00035FE0 File Offset: 0x000341E0
		protected override IResultFactory GetDefaultResultFactory(IConnectionInformation connection, ArraySegment<byte> outputBuffer)
		{
			return RopSubmitMessage.resultFactory;
		}

		// Token: 0x06001480 RID: 5248 RVA: 0x00035FE7 File Offset: 0x000341E7
		protected override void InternalParseInput(Reader reader, ServerObjectHandleTable serverObjectHandleTable)
		{
			base.InternalParseInput(reader, serverObjectHandleTable);
			this.submitFlags = (SubmitMessageFlags)reader.ReadByte();
		}

		// Token: 0x06001481 RID: 5249 RVA: 0x00035FFD File Offset: 0x000341FD
		protected override void InternalSerializeOutput(Writer writer)
		{
			base.InternalSerializeOutput(writer);
			this.result.Serialize(writer);
		}

		// Token: 0x06001482 RID: 5250 RVA: 0x00036012 File Offset: 0x00034212
		protected override void InternalExecute(IServerObject serverObject, IRopHandler ropHandler, ArraySegment<byte> outputBuffer)
		{
			this.result = ropHandler.SubmitMessage(serverObject, this.submitFlags, RopSubmitMessage.resultFactory);
		}

		// Token: 0x06001483 RID: 5251 RVA: 0x0003602C File Offset: 0x0003422C
		internal override void AppendToString(StringBuilder stringBuilder)
		{
			base.AppendToString(stringBuilder);
			stringBuilder.Append(" Flags=").Append(this.submitFlags);
		}

		// Token: 0x04000AD1 RID: 2769
		private const RopId RopType = RopId.SubmitMessage;

		// Token: 0x04000AD2 RID: 2770
		private static SubmitMessageResultFactory resultFactory = new SubmitMessageResultFactory();

		// Token: 0x04000AD3 RID: 2771
		private SubmitMessageFlags submitFlags;
	}
}
