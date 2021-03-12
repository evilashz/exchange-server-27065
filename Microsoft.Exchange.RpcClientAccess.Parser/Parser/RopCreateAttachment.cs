using System;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x0200028F RID: 655
	internal sealed class RopCreateAttachment : InputOutputRop
	{
		// Token: 0x170002CA RID: 714
		// (get) Token: 0x06000E69 RID: 3689 RVA: 0x0002AD9E File Offset: 0x00028F9E
		internal override RopId RopId
		{
			get
			{
				return RopId.CreateAttachment;
			}
		}

		// Token: 0x06000E6A RID: 3690 RVA: 0x0002ADA2 File Offset: 0x00028FA2
		internal static Rop CreateRop()
		{
			return new RopCreateAttachment();
		}

		// Token: 0x06000E6B RID: 3691 RVA: 0x0002ADA9 File Offset: 0x00028FA9
		internal void SetInput(byte logonIndex, byte handleTableIndex, byte returnHandleTableIndex)
		{
			base.SetCommonInput(logonIndex, handleTableIndex, returnHandleTableIndex);
		}

		// Token: 0x06000E6C RID: 3692 RVA: 0x0002ADB4 File Offset: 0x00028FB4
		protected override void InternalParseOutput(Reader reader, Encoding string8Encoding)
		{
			base.InternalParseOutput(reader, string8Encoding);
			this.result = RopResult.Parse(reader, new RopResult.ResultParserDelegate(SuccessfulCreateAttachmentResult.Parse), new RopResult.ResultParserDelegate(StandardRopResult.ParseFailResult));
		}

		// Token: 0x06000E6D RID: 3693 RVA: 0x0002ADE2 File Offset: 0x00028FE2
		protected override IResultFactory GetDefaultResultFactory(IConnectionInformation connection, ArraySegment<byte> outputBuffer)
		{
			return RopCreateAttachment.resultFactory;
		}

		// Token: 0x06000E6E RID: 3694 RVA: 0x0002ADE9 File Offset: 0x00028FE9
		protected override void InternalSerializeOutput(Writer writer)
		{
			base.InternalSerializeOutput(writer);
			this.result.Serialize(writer);
		}

		// Token: 0x06000E6F RID: 3695 RVA: 0x0002ADFE File Offset: 0x00028FFE
		protected override void InternalExecute(IServerObject serverObject, IRopHandler ropHandler, ArraySegment<byte> outputBuffer)
		{
			this.result = ropHandler.CreateAttachment(serverObject, RopCreateAttachment.resultFactory);
		}

		// Token: 0x04000757 RID: 1879
		private const RopId RopType = RopId.CreateAttachment;

		// Token: 0x04000758 RID: 1880
		private static CreateAttachmentResultFactory resultFactory = new CreateAttachmentResultFactory();
	}
}
