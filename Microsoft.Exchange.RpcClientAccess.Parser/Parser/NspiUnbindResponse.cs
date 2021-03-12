using System;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x020001E7 RID: 487
	internal sealed class NspiUnbindResponse : MapiHttpOperationResponse
	{
		// Token: 0x06000A56 RID: 2646 RVA: 0x0001FF0B File Offset: 0x0001E10B
		public NspiUnbindResponse(uint returnCode, ArraySegment<byte> auxiliaryBuffer) : base(returnCode, auxiliaryBuffer)
		{
		}

		// Token: 0x06000A57 RID: 2647 RVA: 0x0001FF15 File Offset: 0x0001E115
		public NspiUnbindResponse(Reader reader) : base(reader)
		{
			base.ParseAuxiliaryBuffer(reader);
		}

		// Token: 0x06000A58 RID: 2648 RVA: 0x0001FF25 File Offset: 0x0001E125
		public override void Serialize(Writer writer)
		{
			base.Serialize(writer);
			base.SerializeAuxiliaryBuffer(writer);
		}
	}
}
