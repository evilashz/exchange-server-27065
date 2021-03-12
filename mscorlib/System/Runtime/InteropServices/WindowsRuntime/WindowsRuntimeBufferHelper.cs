using System;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Security;
using System.Threading;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	// Token: 0x020009E7 RID: 2535
	[FriendAccessAllowed]
	internal static class WindowsRuntimeBufferHelper
	{
		// Token: 0x060064A3 RID: 25763
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[DllImport("QCall")]
		private unsafe static extern void StoreOverlappedPtrInCCW(ObjectHandleOnStack windowsRuntimeBuffer, NativeOverlapped* overlapped);

		// Token: 0x060064A4 RID: 25764 RVA: 0x001550F1 File Offset: 0x001532F1
		[FriendAccessAllowed]
		[SecurityCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		internal unsafe static void StoreOverlappedInCCW(object windowsRuntimeBuffer, NativeOverlapped* overlapped)
		{
			WindowsRuntimeBufferHelper.StoreOverlappedPtrInCCW(JitHelpers.GetObjectHandleOnStack<object>(ref windowsRuntimeBuffer), overlapped);
		}
	}
}
