using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Threading
{
	// Token: 0x020004F8 RID: 1272
	// (Invoke) Token: 0x06003CD3 RID: 15571
	[SecurityCritical]
	[CLSCompliant(false)]
	[ComVisible(true)]
	public unsafe delegate void IOCompletionCallback(uint errorCode, uint numBytes, NativeOverlapped* pOVERLAP);
}
