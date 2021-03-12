using System;
using System.Runtime.ConstrainedExecution;
using System.Security;

namespace System.StubHelpers
{
	// Token: 0x0200057A RID: 1402
	[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
	internal static class HResultExceptionMarshaler
	{
		// Token: 0x06004209 RID: 16905 RVA: 0x000F5866 File Offset: 0x000F3A66
		internal static int ConvertToNative(Exception ex)
		{
			if (!Environment.IsWinRTSupported)
			{
				throw new PlatformNotSupportedException(Environment.GetResourceString("PlatformNotSupported_WinRT"));
			}
			if (ex == null)
			{
				return 0;
			}
			return ex._HResult;
		}

		// Token: 0x0600420A RID: 16906 RVA: 0x000F588C File Offset: 0x000F3A8C
		[SecuritySafeCritical]
		internal static Exception ConvertToManaged(int hr)
		{
			if (!Environment.IsWinRTSupported)
			{
				throw new PlatformNotSupportedException(Environment.GetResourceString("PlatformNotSupported_WinRT"));
			}
			Exception result = null;
			if (hr < 0)
			{
				result = StubHelpers.InternalGetCOMHRExceptionObject(hr, IntPtr.Zero, null, true);
			}
			return result;
		}
	}
}
