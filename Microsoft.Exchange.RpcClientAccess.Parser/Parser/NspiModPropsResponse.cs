using System;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x020001DB RID: 475
	internal sealed class NspiModPropsResponse : MapiHttpOperationResponse
	{
		// Token: 0x06000A0E RID: 2574 RVA: 0x0001F4BB File Offset: 0x0001D6BB
		public NspiModPropsResponse(uint returnCode, ArraySegment<byte> auxiliaryBuffer) : base(returnCode, auxiliaryBuffer)
		{
		}

		// Token: 0x06000A0F RID: 2575 RVA: 0x0001F4C5 File Offset: 0x0001D6C5
		public NspiModPropsResponse(Reader reader) : base(reader)
		{
			base.ParseAuxiliaryBuffer(reader);
		}

		// Token: 0x06000A10 RID: 2576 RVA: 0x0001F4D5 File Offset: 0x0001D6D5
		public override void Serialize(Writer writer)
		{
			base.Serialize(writer);
			base.SerializeAuxiliaryBuffer(writer);
		}
	}
}
