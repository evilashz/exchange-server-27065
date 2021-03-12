using System;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x02000070 RID: 112
	internal static class TimeProvider
	{
		// Token: 0x17000061 RID: 97
		// (get) Token: 0x060001F6 RID: 502 RVA: 0x00006A7C File Offset: 0x00004C7C
		public static DateTime UtcNow
		{
			get
			{
				if (TimeProvider.CurrentProvider != null)
				{
					return TimeProvider.CurrentProvider.UtcNow;
				}
				return DateTime.UtcNow;
			}
		}

		// Token: 0x060001F7 RID: 503 RVA: 0x00006A98 File Offset: 0x00004C98
		public static ITimeProvider SetProvider(ITimeProvider provider)
		{
			ITimeProvider currentProvider = TimeProvider.CurrentProvider;
			TimeProvider.CurrentProvider = provider;
			return currentProvider;
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x060001F8 RID: 504 RVA: 0x00006AB2 File Offset: 0x00004CB2
		// (set) Token: 0x060001F9 RID: 505 RVA: 0x00006AB9 File Offset: 0x00004CB9
		public static ITimeProvider CurrentProvider { get; private set; }
	}
}
