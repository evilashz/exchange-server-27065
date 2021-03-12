using System;
using System.Security;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	// Token: 0x020009C7 RID: 2503
	internal static class UnsafeNativeMethods
	{
		// Token: 0x060063A6 RID: 25510
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
		[DllImport("api-ms-win-core-winrt-error-l1-1-1.dll", PreserveSig = false)]
		internal static extern IRestrictedErrorInfo GetRestrictedErrorInfo();

		// Token: 0x060063A7 RID: 25511
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
		[DllImport("api-ms-win-core-winrt-error-l1-1-1.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool RoOriginateLanguageException(int error, [MarshalAs(UnmanagedType.HString)] string message, IntPtr languageException);

		// Token: 0x060063A8 RID: 25512
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
		[DllImport("api-ms-win-core-winrt-error-l1-1-1.dll", PreserveSig = false)]
		internal static extern void RoReportUnhandledError(IRestrictedErrorInfo error);

		// Token: 0x060063A9 RID: 25513
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
		[DllImport("api-ms-win-core-winrt-string-l1-1-0.dll", CallingConvention = CallingConvention.StdCall)]
		internal unsafe static extern int WindowsCreateString([MarshalAs(UnmanagedType.LPWStr)] string sourceString, int length, [Out] IntPtr* hstring);

		// Token: 0x060063AA RID: 25514
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
		[DllImport("api-ms-win-core-winrt-string-l1-1-0.dll", CallingConvention = CallingConvention.StdCall)]
		internal unsafe static extern int WindowsCreateStringReference(char* sourceString, int length, [Out] HSTRING_HEADER* hstringHeader, [Out] IntPtr* hstring);

		// Token: 0x060063AB RID: 25515
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
		[DllImport("api-ms-win-core-winrt-string-l1-1-0.dll", CallingConvention = CallingConvention.StdCall)]
		internal static extern int WindowsDeleteString(IntPtr hstring);

		// Token: 0x060063AC RID: 25516
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
		[DllImport("api-ms-win-core-winrt-string-l1-1-0.dll", CallingConvention = CallingConvention.StdCall)]
		internal unsafe static extern char* WindowsGetStringRawBuffer(IntPtr hstring, [Out] uint* length);
	}
}
