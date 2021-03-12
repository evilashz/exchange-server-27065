using System;
using System.Security;

namespace System.Threading
{
	// Token: 0x020004F5 RID: 1269
	internal interface IThreadPoolWorkItem
	{
		// Token: 0x06003CC4 RID: 15556
		[SecurityCritical]
		void ExecuteWorkItem();

		// Token: 0x06003CC5 RID: 15557
		[SecurityCritical]
		void MarkAborted(ThreadAbortException tae);
	}
}
