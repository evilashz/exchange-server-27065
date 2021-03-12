using System;

namespace Microsoft.Exchange.ExchangeSystem
{
	// Token: 0x02000067 RID: 103
	internal class TimeLibConsts
	{
		// Token: 0x040001C3 RID: 451
		internal static TimeSpan MaxBias = TimeSpan.FromHours(24.0);

		// Token: 0x040001C4 RID: 452
		internal static DateTime MinSystemDateTimeValue = DateTime.SpecifyKind(DateTime.MinValue.Add(TimeLibConsts.MaxBias), DateTimeKind.Utc);

		// Token: 0x040001C5 RID: 453
		internal static DateTime MaxSystemDateTimeValue = DateTime.SpecifyKind(DateTime.MaxValue.Subtract(TimeLibConsts.MaxBias), DateTimeKind.Utc);

		// Token: 0x040001C6 RID: 454
		internal static TimeSpan MaxIncValue;
	}
}
