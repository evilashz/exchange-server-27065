using System;
using System.Runtime.ConstrainedExecution;
using System.Security;

namespace System.StubHelpers
{
	// Token: 0x02000568 RID: 1384
	[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
	internal static class DateTimeOffsetMarshaler
	{
		// Token: 0x060041B9 RID: 16825 RVA: 0x000F4CE4 File Offset: 0x000F2EE4
		[SecurityCritical]
		internal static void ConvertToNative(ref DateTimeOffset managedDTO, out DateTimeNative dateTime)
		{
			long utcTicks = managedDTO.UtcTicks;
			dateTime.UniversalTime = utcTicks - 504911232000000000L;
		}

		// Token: 0x060041BA RID: 16826 RVA: 0x000F4D0C File Offset: 0x000F2F0C
		[SecurityCritical]
		internal static void ConvertToManaged(out DateTimeOffset managedLocalDTO, ref DateTimeNative nativeTicks)
		{
			long ticks = 504911232000000000L + nativeTicks.UniversalTime;
			DateTimeOffset dateTimeOffset = new DateTimeOffset(ticks, TimeSpan.Zero);
			managedLocalDTO = dateTimeOffset.ToLocalTime(true);
		}

		// Token: 0x04001B1D RID: 6941
		private const long ManagedUtcTicksAtNativeZero = 504911232000000000L;
	}
}
