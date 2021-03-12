using System;
using System.Runtime.ConstrainedExecution;
using System.Security;
using Microsoft.Win32;

namespace System.StubHelpers
{
	// Token: 0x02000565 RID: 1381
	[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
	internal static class AnsiBSTRMarshaler
	{
		// Token: 0x060041B3 RID: 16819 RVA: 0x000F4C5C File Offset: 0x000F2E5C
		[SecurityCritical]
		internal static IntPtr ConvertToNative(int flags, string strManaged)
		{
			if (strManaged == null)
			{
				return IntPtr.Zero;
			}
			int length = strManaged.Length;
			StubHelpers.CheckStringLength(length);
			byte[] str = null;
			int len = 0;
			if (length > 0)
			{
				str = AnsiCharMarshaler.DoAnsiConversion(strManaged, (flags & 255) != 0, flags >> 8 != 0, out len);
			}
			return Win32Native.SysAllocStringByteLen(str, (uint)len);
		}

		// Token: 0x060041B4 RID: 16820 RVA: 0x000F4CA7 File Offset: 0x000F2EA7
		[SecurityCritical]
		internal unsafe static string ConvertToManaged(IntPtr bstr)
		{
			if (IntPtr.Zero == bstr)
			{
				return null;
			}
			return new string((sbyte*)((void*)bstr));
		}

		// Token: 0x060041B5 RID: 16821 RVA: 0x000F4CC3 File Offset: 0x000F2EC3
		[SecurityCritical]
		internal static void ClearNative(IntPtr pNative)
		{
			if (IntPtr.Zero != pNative)
			{
				Win32Native.SysFreeString(pNative);
			}
		}
	}
}
