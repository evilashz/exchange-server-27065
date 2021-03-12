using System;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;

namespace System.StubHelpers
{
	// Token: 0x02000571 RID: 1393
	[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
	internal static class MngdSafeArrayMarshaler
	{
		// Token: 0x060041D8 RID: 16856
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void CreateMarshaler(IntPtr pMarshalState, IntPtr pMT, int iRank, int dwFlags);

		// Token: 0x060041D9 RID: 16857
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void ConvertSpaceToNative(IntPtr pMarshalState, ref object pManagedHome, IntPtr pNativeHome);

		// Token: 0x060041DA RID: 16858
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void ConvertContentsToNative(IntPtr pMarshalState, ref object pManagedHome, IntPtr pNativeHome, object pOriginalManaged);

		// Token: 0x060041DB RID: 16859
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void ConvertSpaceToManaged(IntPtr pMarshalState, ref object pManagedHome, IntPtr pNativeHome);

		// Token: 0x060041DC RID: 16860
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void ConvertContentsToManaged(IntPtr pMarshalState, ref object pManagedHome, IntPtr pNativeHome);

		// Token: 0x060041DD RID: 16861
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void ClearNative(IntPtr pMarshalState, ref object pManagedHome, IntPtr pNativeHome);
	}
}
