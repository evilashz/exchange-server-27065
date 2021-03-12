using System;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x020002F6 RID: 758
	public struct RusPublishingTags
	{
		// Token: 0x04001430 RID: 5168
		public const int Common = 0;

		// Token: 0x04001431 RID: 5169
		public const int PublisherWeb = 1;

		// Token: 0x04001432 RID: 5170
		public const int EngineUpdateDownloader = 2;

		// Token: 0x04001433 RID: 5171
		public const int EngineUpdatePublisher = 3;

		// Token: 0x04001434 RID: 5172
		public const int SignaturePollingJob = 4;

		// Token: 0x04001435 RID: 5173
		public const int EnginePackagingStep = 5;

		// Token: 0x04001436 RID: 5174
		public const int EngineTestingStep = 6;

		// Token: 0x04001437 RID: 5175
		public const int EngineCodeSignStep = 7;

		// Token: 0x04001438 RID: 5176
		public const int EngineCleanUpStep = 8;

		// Token: 0x04001439 RID: 5177
		public static Guid guid = new Guid("534d6f5a-8ca8-4c44-abd7-481335889364");
	}
}
