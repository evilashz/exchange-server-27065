using System;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;

namespace System.StubHelpers
{
	// Token: 0x02000570 RID: 1392
	[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
	internal static class MngdNativeArrayMarshaler
	{
		// Token: 0x060041D1 RID: 16849
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void CreateMarshaler(IntPtr pMarshalState, IntPtr pMT, int dwFlags);

		// Token: 0x060041D2 RID: 16850
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void ConvertSpaceToNative(IntPtr pMarshalState, ref object pManagedHome, IntPtr pNativeHome);

		// Token: 0x060041D3 RID: 16851
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void ConvertContentsToNative(IntPtr pMarshalState, ref object pManagedHome, IntPtr pNativeHome);

		// Token: 0x060041D4 RID: 16852
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void ConvertSpaceToManaged(IntPtr pMarshalState, ref object pManagedHome, IntPtr pNativeHome, int cElements);

		// Token: 0x060041D5 RID: 16853
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void ConvertContentsToManaged(IntPtr pMarshalState, ref object pManagedHome, IntPtr pNativeHome);

		// Token: 0x060041D6 RID: 16854
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void ClearNative(IntPtr pMarshalState, IntPtr pNativeHome, int cElements);

		// Token: 0x060041D7 RID: 16855
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void ClearNativeContents(IntPtr pMarshalState, IntPtr pNativeHome, int cElements);
	}
}
