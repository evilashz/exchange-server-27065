using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000ED8 RID: 3800
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal static class TimeConstants
	{
		// Token: 0x040057E6 RID: 22502
		public const int MinutesPerDay = 1440;

		// Token: 0x040057E7 RID: 22503
		public const int MinutesPerHour = 60;

		// Token: 0x040057E8 RID: 22504
		public static readonly TimeSpan OneDay = new TimeSpan(1, 0, 0, 0, 0);
	}
}
