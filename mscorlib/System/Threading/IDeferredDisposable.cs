using System;
using System.Security;

namespace System.Threading
{
	// Token: 0x020004DD RID: 1245
	internal interface IDeferredDisposable
	{
		// Token: 0x06003BA3 RID: 15267
		[SecurityCritical]
		void OnFinalRelease(bool disposed);
	}
}
