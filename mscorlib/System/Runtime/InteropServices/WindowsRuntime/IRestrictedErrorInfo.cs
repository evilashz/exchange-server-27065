using System;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	// Token: 0x020009B6 RID: 2486
	[Guid("82BA7092-4C88-427D-A7BC-16DD93FEB67E")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface IRestrictedErrorInfo
	{
		// Token: 0x06006366 RID: 25446
		void GetErrorDetails([MarshalAs(UnmanagedType.BStr)] out string description, out int error, [MarshalAs(UnmanagedType.BStr)] out string restrictedDescription, [MarshalAs(UnmanagedType.BStr)] out string capabilitySid);

		// Token: 0x06006367 RID: 25447
		void GetReference([MarshalAs(UnmanagedType.BStr)] out string reference);
	}
}
