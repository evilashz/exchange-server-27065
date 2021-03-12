using System;
using System.Security;
using Microsoft.Win32.SafeHandles;

namespace System.Threading
{
	// Token: 0x02000507 RID: 1287
	[__DynamicallyInvokable]
	public static class WaitHandleExtensions
	{
		// Token: 0x06003D86 RID: 15750 RVA: 0x000E4844 File Offset: 0x000E2A44
		[SecurityCritical]
		[__DynamicallyInvokable]
		public static SafeWaitHandle GetSafeWaitHandle(this WaitHandle waitHandle)
		{
			if (waitHandle == null)
			{
				throw new ArgumentNullException("waitHandle");
			}
			return waitHandle.SafeWaitHandle;
		}

		// Token: 0x06003D87 RID: 15751 RVA: 0x000E485A File Offset: 0x000E2A5A
		[SecurityCritical]
		[__DynamicallyInvokable]
		public static void SetSafeWaitHandle(this WaitHandle waitHandle, SafeWaitHandle value)
		{
			if (waitHandle == null)
			{
				throw new ArgumentNullException("waitHandle");
			}
			waitHandle.SafeWaitHandle = value;
		}
	}
}
