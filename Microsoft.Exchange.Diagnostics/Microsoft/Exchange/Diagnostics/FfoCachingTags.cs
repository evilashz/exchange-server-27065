using System;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x020002F3 RID: 755
	public struct FfoCachingTags
	{
		// Token: 0x04001415 RID: 5141
		public const int PrimingThread = 0;

		// Token: 0x04001416 RID: 5142
		public const int CachingProvider = 1;

		// Token: 0x04001417 RID: 5143
		public const int CompositeProvider = 2;

		// Token: 0x04001418 RID: 5144
		public const int PrimingStateLocalCache = 3;

		// Token: 0x04001419 RID: 5145
		public static Guid guid = new Guid("880B0BC2-765E-4B89-82A0-9FFBBA7B8BE1");
	}
}
