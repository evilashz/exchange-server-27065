using System;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x020001BF RID: 447
	internal sealed class EmsmdbDisconnectResponse : MapiHttpOperationResponse
	{
		// Token: 0x06000982 RID: 2434 RVA: 0x0001E4A7 File Offset: 0x0001C6A7
		public EmsmdbDisconnectResponse(uint returnCode, ArraySegment<byte> auxiliaryBuffer) : base(returnCode, auxiliaryBuffer)
		{
		}

		// Token: 0x06000983 RID: 2435 RVA: 0x0001E4B1 File Offset: 0x0001C6B1
		public EmsmdbDisconnectResponse(Reader reader) : base(reader)
		{
			base.ParseAuxiliaryBuffer(reader);
		}

		// Token: 0x06000984 RID: 2436 RVA: 0x0001E4C1 File Offset: 0x0001C6C1
		public override void Serialize(Writer writer)
		{
			base.Serialize(writer);
			base.SerializeAuxiliaryBuffer(writer);
		}
	}
}
