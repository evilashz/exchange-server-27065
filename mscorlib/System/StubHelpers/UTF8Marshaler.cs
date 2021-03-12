using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using Microsoft.Win32;

namespace System.StubHelpers
{
	// Token: 0x02000561 RID: 1377
	[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
	internal static class UTF8Marshaler
	{
		// Token: 0x060041A8 RID: 16808 RVA: 0x000F48F4 File Offset: 0x000F2AF4
		[SecurityCritical]
		internal unsafe static IntPtr ConvertToNative(int flags, string strManaged, IntPtr pNativeBuffer)
		{
			if (strManaged == null)
			{
				return IntPtr.Zero;
			}
			StubHelpers.CheckStringLength(strManaged.Length);
			byte* ptr = (byte*)((void*)pNativeBuffer);
			int num;
			if (ptr != null)
			{
				num = (strManaged.Length + 1) * 3;
				num = strManaged.GetBytesFromEncoding(ptr, num, Encoding.UTF8);
			}
			else
			{
				num = Encoding.UTF8.GetByteCount(strManaged);
				ptr = (byte*)((void*)Marshal.AllocCoTaskMem(num + 1));
				strManaged.GetBytesFromEncoding(ptr, num, Encoding.UTF8);
			}
			ptr[num] = 0;
			return (IntPtr)((void*)ptr);
		}

		// Token: 0x060041A9 RID: 16809 RVA: 0x000F4970 File Offset: 0x000F2B70
		[SecurityCritical]
		internal unsafe static string ConvertToManaged(IntPtr cstr)
		{
			if (IntPtr.Zero == cstr)
			{
				return null;
			}
			int byteLength = StubHelpers.strlen((sbyte*)((void*)cstr));
			return string.CreateStringFromEncoding((byte*)((void*)cstr), byteLength, Encoding.UTF8);
		}

		// Token: 0x060041AA RID: 16810 RVA: 0x000F49A9 File Offset: 0x000F2BA9
		[SecurityCritical]
		internal static void ClearNative(IntPtr pNative)
		{
			if (pNative != IntPtr.Zero)
			{
				Win32Native.CoTaskMemFree(pNative);
			}
		}

		// Token: 0x04001B1B RID: 6939
		private const int MAX_UTF8_CHAR_SIZE = 3;
	}
}
