using System;
using System.Security;

namespace System.Threading
{
	// Token: 0x020004F4 RID: 1268
	internal static class _ThreadPoolWaitCallback
	{
		// Token: 0x06003CC3 RID: 15555 RVA: 0x000E2ACD File Offset: 0x000E0CCD
		[SecurityCritical]
		internal static bool PerformWaitCallback()
		{
			return ThreadPoolWorkQueue.Dispatch();
		}
	}
}
