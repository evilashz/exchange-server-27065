using System;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;

namespace System.StubHelpers
{
	// Token: 0x02000573 RID: 1395
	[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
	internal static class MngdRefCustomMarshaler
	{
		// Token: 0x060041EF RID: 16879
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void CreateMarshaler(IntPtr pMarshalState, IntPtr pCMHelper);

		// Token: 0x060041F0 RID: 16880
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void ConvertContentsToNative(IntPtr pMarshalState, ref object pManagedHome, IntPtr pNativeHome);

		// Token: 0x060041F1 RID: 16881
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void ConvertContentsToManaged(IntPtr pMarshalState, ref object pManagedHome, IntPtr pNativeHome);

		// Token: 0x060041F2 RID: 16882
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void ClearNative(IntPtr pMarshalState, ref object pManagedHome, IntPtr pNativeHome);

		// Token: 0x060041F3 RID: 16883
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void ClearManaged(IntPtr pMarshalState, ref object pManagedHome, IntPtr pNativeHome);
	}
}
