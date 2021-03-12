using System;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Security;

namespace System.StubHelpers
{
	// Token: 0x0200056E RID: 1390
	[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
	internal static class UriMarshaler
	{
		// Token: 0x060041CB RID: 16843
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern string GetRawUriFromNative(IntPtr pUri);

		// Token: 0x060041CC RID: 16844
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal unsafe static extern IntPtr CreateNativeUriInstanceHelper(char* rawUri, int strLen);

		// Token: 0x060041CD RID: 16845 RVA: 0x000F4E24 File Offset: 0x000F3024
		[SecurityCritical]
		internal unsafe static IntPtr CreateNativeUriInstance(string rawUri)
		{
			char* ptr = rawUri;
			if (ptr != null)
			{
				ptr += RuntimeHelpers.OffsetToStringData / 2;
			}
			return UriMarshaler.CreateNativeUriInstanceHelper(ptr, rawUri.Length);
		}
	}
}
