using System;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x020001D9 RID: 473
	internal sealed class NspiModLinkAttResponse : MapiHttpOperationResponse
	{
		// Token: 0x06000A04 RID: 2564 RVA: 0x0001F36E File Offset: 0x0001D56E
		public NspiModLinkAttResponse(uint returnCode, ArraySegment<byte> auxiliaryBuffer) : base(returnCode, auxiliaryBuffer)
		{
		}

		// Token: 0x06000A05 RID: 2565 RVA: 0x0001F378 File Offset: 0x0001D578
		public NspiModLinkAttResponse(Reader reader) : base(reader)
		{
			base.ParseAuxiliaryBuffer(reader);
		}

		// Token: 0x06000A06 RID: 2566 RVA: 0x0001F388 File Offset: 0x0001D588
		public override void Serialize(Writer writer)
		{
			base.Serialize(writer);
			base.SerializeAuxiliaryBuffer(writer);
		}
	}
}
