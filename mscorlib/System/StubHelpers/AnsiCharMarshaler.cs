using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;

namespace System.StubHelpers
{
	// Token: 0x0200055F RID: 1375
	[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
	internal static class AnsiCharMarshaler
	{
		// Token: 0x060041A2 RID: 16802 RVA: 0x000F4770 File Offset: 0x000F2970
		[SecurityCritical]
		internal unsafe static byte[] DoAnsiConversion(string str, bool fBestFit, bool fThrowOnUnmappableChar, out int cbLength)
		{
			byte[] array = new byte[(str.Length + 1) * Marshal.SystemMaxDBCSCharSize];
			fixed (byte* ptr = array)
			{
				cbLength = str.ConvertToAnsi(ptr, array.Length, fBestFit, fThrowOnUnmappableChar);
			}
			return array;
		}

		// Token: 0x060041A3 RID: 16803 RVA: 0x000F47BC File Offset: 0x000F29BC
		[SecurityCritical]
		internal unsafe static byte ConvertToNative(char managedChar, bool fBestFit, bool fThrowOnUnmappableChar)
		{
			int num = 2 * Marshal.SystemMaxDBCSCharSize;
			byte* ptr = stackalloc byte[checked(unchecked((UIntPtr)num) * 1)];
			int num2 = managedChar.ToString().ConvertToAnsi(ptr, num, fBestFit, fThrowOnUnmappableChar);
			return *ptr;
		}

		// Token: 0x060041A4 RID: 16804 RVA: 0x000F47EC File Offset: 0x000F29EC
		internal static char ConvertToManaged(byte nativeChar)
		{
			byte[] bytes = new byte[]
			{
				nativeChar
			};
			string @string = Encoding.Default.GetString(bytes);
			return @string[0];
		}
	}
}
