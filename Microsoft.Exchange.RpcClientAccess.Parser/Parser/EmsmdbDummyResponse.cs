using System;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x020001C1 RID: 449
	internal sealed class EmsmdbDummyResponse : MapiHttpOperationResponse
	{
		// Token: 0x06000988 RID: 2440 RVA: 0x0001E4E9 File Offset: 0x0001C6E9
		public EmsmdbDummyResponse(uint returnCode, ArraySegment<byte> auxiliaryBuffer) : base(returnCode, auxiliaryBuffer)
		{
		}

		// Token: 0x06000989 RID: 2441 RVA: 0x0001E4F3 File Offset: 0x0001C6F3
		public EmsmdbDummyResponse(Reader reader) : base(reader)
		{
		}
	}
}
