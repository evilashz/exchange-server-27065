using System;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x02000310 RID: 784
	public struct WasclTags
	{
		// Token: 0x040014ED RID: 5357
		public const int General = 0;

		// Token: 0x040014EE RID: 5358
		public const int Core = 1;

		// Token: 0x040014EF RID: 5359
		public const int Verdict = 2;

		// Token: 0x040014F0 RID: 5360
		public const int ExternalCall = 3;

		// Token: 0x040014F1 RID: 5361
		public const int API = 4;

		// Token: 0x040014F2 RID: 5362
		public const int CryptoHelper = 5;

		// Token: 0x040014F3 RID: 5363
		public const int OSE = 6;

		// Token: 0x040014F4 RID: 5364
		public static Guid guid = new Guid("48076FB3-30FE-460B-975D-934742F529EA");
	}
}
