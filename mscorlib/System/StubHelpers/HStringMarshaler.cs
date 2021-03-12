using System;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security;

namespace System.StubHelpers
{
	// Token: 0x02000569 RID: 1385
	[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
	internal static class HStringMarshaler
	{
		// Token: 0x060041BB RID: 16827 RVA: 0x000F4D48 File Offset: 0x000F2F48
		[SecurityCritical]
		internal unsafe static IntPtr ConvertToNative(string managed)
		{
			if (!Environment.IsWinRTSupported)
			{
				throw new PlatformNotSupportedException(Environment.GetResourceString("PlatformNotSupported_WinRT"));
			}
			if (managed == null)
			{
				throw new ArgumentNullException();
			}
			IntPtr result;
			int errorCode = UnsafeNativeMethods.WindowsCreateString(managed, managed.Length, &result);
			Marshal.ThrowExceptionForHR(errorCode, new IntPtr(-1));
			return result;
		}

		// Token: 0x060041BC RID: 16828 RVA: 0x000F4D94 File Offset: 0x000F2F94
		[SecurityCritical]
		internal unsafe static IntPtr ConvertToNativeReference(string managed, [Out] HSTRING_HEADER* hstringHeader)
		{
			if (!Environment.IsWinRTSupported)
			{
				throw new PlatformNotSupportedException(Environment.GetResourceString("PlatformNotSupported_WinRT"));
			}
			if (managed == null)
			{
				throw new ArgumentNullException();
			}
			char* ptr = managed;
			if (ptr != null)
			{
				ptr += RuntimeHelpers.OffsetToStringData / 2;
			}
			IntPtr result;
			int errorCode = UnsafeNativeMethods.WindowsCreateStringReference(ptr, managed.Length, hstringHeader, &result);
			Marshal.ThrowExceptionForHR(errorCode, new IntPtr(-1));
			return result;
		}

		// Token: 0x060041BD RID: 16829 RVA: 0x000F4DEF File Offset: 0x000F2FEF
		[SecurityCritical]
		internal static string ConvertToManaged(IntPtr hstring)
		{
			if (!Environment.IsWinRTSupported)
			{
				throw new PlatformNotSupportedException(Environment.GetResourceString("PlatformNotSupported_WinRT"));
			}
			return WindowsRuntimeMarshal.HStringToString(hstring);
		}

		// Token: 0x060041BE RID: 16830 RVA: 0x000F4E0E File Offset: 0x000F300E
		[SecurityCritical]
		internal static void ClearNative(IntPtr hstring)
		{
			if (hstring != IntPtr.Zero)
			{
				UnsafeNativeMethods.WindowsDeleteString(hstring);
			}
		}
	}
}
