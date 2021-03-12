using System;
using System.Runtime.ConstrainedExecution;

namespace System.StubHelpers
{
	// Token: 0x02000566 RID: 1382
	[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
	internal static class WSTRBufferMarshaler
	{
		// Token: 0x060041B6 RID: 16822 RVA: 0x000F4CD8 File Offset: 0x000F2ED8
		internal static IntPtr ConvertToNative(string strManaged)
		{
			return IntPtr.Zero;
		}

		// Token: 0x060041B7 RID: 16823 RVA: 0x000F4CDF File Offset: 0x000F2EDF
		internal static string ConvertToManaged(IntPtr bstr)
		{
			return null;
		}

		// Token: 0x060041B8 RID: 16824 RVA: 0x000F4CE2 File Offset: 0x000F2EE2
		internal static void ClearNative(IntPtr pNative)
		{
		}
	}
}
