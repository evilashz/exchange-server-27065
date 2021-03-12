using System;

namespace System.Runtime.ConstrainedExecution
{
	// Token: 0x020006FD RID: 1789
	[Serializable]
	public enum Consistency
	{
		// Token: 0x04002375 RID: 9077
		MayCorruptProcess,
		// Token: 0x04002376 RID: 9078
		MayCorruptAppDomain,
		// Token: 0x04002377 RID: 9079
		MayCorruptInstance,
		// Token: 0x04002378 RID: 9080
		WillNotCorruptState
	}
}
