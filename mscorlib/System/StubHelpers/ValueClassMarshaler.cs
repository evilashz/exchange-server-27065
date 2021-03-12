using System;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Security;

namespace System.StubHelpers
{
	// Token: 0x0200056B RID: 1387
	[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
	internal static class ValueClassMarshaler
	{
		// Token: 0x060041C2 RID: 16834
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void ConvertToNative(IntPtr dst, IntPtr src, IntPtr pMT, ref CleanupWorkList pCleanupWorkList);

		// Token: 0x060041C3 RID: 16835
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void ConvertToManaged(IntPtr dst, IntPtr src, IntPtr pMT);

		// Token: 0x060041C4 RID: 16836
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void ClearNative(IntPtr dst, IntPtr pMT);
	}
}
