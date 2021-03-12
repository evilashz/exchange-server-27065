using System;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x020001AE RID: 430
	internal interface IDiagnosticInfoProvider
	{
		// Token: 0x0600088D RID: 2189
		void GetDiagnosticData(long maxSize, out uint threadId, out uint requestId, out DiagnosticContextFlags flags, out byte[] data);
	}
}
