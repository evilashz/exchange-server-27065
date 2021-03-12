using System;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;

namespace System.StubHelpers
{
	// Token: 0x0200056D RID: 1389
	[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
	[FriendAccessAllowed]
	internal static class InterfaceMarshaler
	{
		// Token: 0x060041C7 RID: 16839
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern IntPtr ConvertToNative(object objSrc, IntPtr itfMT, IntPtr classMT, int flags);

		// Token: 0x060041C8 RID: 16840
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern object ConvertToManaged(IntPtr pUnk, IntPtr itfMT, IntPtr classMT, int flags);

		// Token: 0x060041C9 RID: 16841
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall")]
		internal static extern void ClearNative(IntPtr pUnk);

		// Token: 0x060041CA RID: 16842
		[FriendAccessAllowed]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern object ConvertToManagedWithoutUnboxing(IntPtr pNative);
	}
}
