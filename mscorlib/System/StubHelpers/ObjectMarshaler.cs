using System;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;

namespace System.StubHelpers
{
	// Token: 0x0200056A RID: 1386
	[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
	internal static class ObjectMarshaler
	{
		// Token: 0x060041BF RID: 16831
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void ConvertToNative(object objSrc, IntPtr pDstVariant);

		// Token: 0x060041C0 RID: 16832
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern object ConvertToManaged(IntPtr pSrcVariant);

		// Token: 0x060041C1 RID: 16833
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void ClearNative(IntPtr pVariant);
	}
}
