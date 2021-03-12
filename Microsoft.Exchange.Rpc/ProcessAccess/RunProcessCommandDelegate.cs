using System;

namespace Microsoft.Exchange.Rpc.ProcessAccess
{
	// Token: 0x0200039B RID: 923
	// (Invoke) Token: 0x0600102F RID: 4143
	internal unsafe delegate void RunProcessCommandDelegate(void* hBinding, int inBytesLen, byte* pInBytes, int* pOutBytesLen, byte** ppOutBytes);
}
