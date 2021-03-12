using System;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;

namespace System.StubHelpers
{
	// Token: 0x0200056C RID: 1388
	[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
	internal static class DateMarshaler
	{
		// Token: 0x060041C5 RID: 16837
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern double ConvertToNative(DateTime managedDate);

		// Token: 0x060041C6 RID: 16838
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern long ConvertToManaged(double nativeDate);
	}
}
