using System;
using System.Runtime.ConstrainedExecution;
using System.Security;
using System.Text;

namespace System.StubHelpers
{
	// Token: 0x02000562 RID: 1378
	[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
	internal static class UTF8BufferMarshaler
	{
		// Token: 0x060041AB RID: 16811 RVA: 0x000F49C0 File Offset: 0x000F2BC0
		[SecurityCritical]
		internal unsafe static IntPtr ConvertToNative(StringBuilder sb, IntPtr pNativeBuffer, int flags)
		{
			if (sb == null)
			{
				return IntPtr.Zero;
			}
			string text = sb.ToString();
			int num = Encoding.UTF8.GetByteCount(text);
			byte* ptr = (byte*)((void*)pNativeBuffer);
			num = text.GetBytesFromEncoding(ptr, num, Encoding.UTF8);
			ptr[num] = 0;
			return (IntPtr)((void*)ptr);
		}

		// Token: 0x060041AC RID: 16812 RVA: 0x000F4A0C File Offset: 0x000F2C0C
		[SecurityCritical]
		internal unsafe static void ConvertToManaged(StringBuilder sb, IntPtr pNative)
		{
			int num = StubHelpers.strlen((sbyte*)((void*)pNative));
			int num2 = Encoding.UTF8.GetCharCount((byte*)((void*)pNative), num);
			char[] array = new char[num2 + 1];
			array[num2] = '\0';
			fixed (char* ptr = array)
			{
				num2 = Encoding.UTF8.GetChars((byte*)((void*)pNative), num, ptr, num2);
				sb.ReplaceBufferInternal(ptr, num2);
			}
		}
	}
}
