using System;
using System.Security;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	// Token: 0x020009E5 RID: 2533
	[Guid("86ddd2d7-ad80-44f6-a12e-63698b52825d")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface IWinRTClassActivator
	{
		// Token: 0x0600649C RID: 25756
		[SecurityCritical]
		[return: MarshalAs(UnmanagedType.IInspectable)]
		object ActivateInstance([MarshalAs(UnmanagedType.HString)] string activatableClassId);

		// Token: 0x0600649D RID: 25757
		[SecurityCritical]
		IntPtr GetActivationFactory([MarshalAs(UnmanagedType.HString)] string activatableClassId, ref Guid iid);
	}
}
