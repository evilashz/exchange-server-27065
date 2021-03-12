using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x020001B4 RID: 436
	internal interface IRopDriver : IDisposable
	{
		// Token: 0x060008AB RID: 2219
		void Execute(IList<ArraySegment<byte>> inputBufferArray, ArraySegment<byte> outputBuffer, out int outputSize, AuxiliaryData auxiliaryData, bool isFake, out byte[] fakeOut);

		// Token: 0x060008AC RID: 2220
		ServerObjectMap CreateLogon(byte logonIndex, LogonFlags logonFlags);

		// Token: 0x060008AD RID: 2221
		bool TryGetServerObjectMap(byte logonIndex, out ServerObjectMap serverObjectMap, out ErrorCode errorCode);

		// Token: 0x060008AE RID: 2222
		bool TryGetServerObject(byte logonIndex, ServerObjectHandle handle, out IServerObject serverObject, out ErrorCode errorCode);

		// Token: 0x060008AF RID: 2223
		void ReleaseHandle(byte logonIndex, ServerObjectHandle handleToRelease);

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x060008B0 RID: 2224
		IRopHandler RopHandler { get; }
	}
}
