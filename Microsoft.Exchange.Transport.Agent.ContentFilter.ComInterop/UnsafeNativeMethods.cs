using System;
using System.Runtime.InteropServices;
using System.Security;

namespace Microsoft.Exchange.Data.Transport.Interop
{
	// Token: 0x02000008 RID: 8
	[ComVisible(false)]
	[SuppressUnmanagedCodeSecurity]
	internal static class UnsafeNativeMethods
	{
		// Token: 0x0600002A RID: 42
		[DllImport("ole32.dll", PreserveSig = false)]
		[return: MarshalAs(UnmanagedType.IUnknown)]
		internal static extern object CoCreateInstance([MarshalAs(UnmanagedType.LPStruct)] Guid rclsid, [MarshalAs(UnmanagedType.IUnknown)] object pUnkOuter, uint dwClsContext, [MarshalAs(UnmanagedType.LPStruct)] Guid riid);
	}
}
