using System;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x0200030B RID: 779
	public struct Notifications_BrokerTags
	{
		// Token: 0x040014B9 RID: 5305
		public const int Client = 0;

		// Token: 0x040014BA RID: 5306
		public const int Service = 1;

		// Token: 0x040014BB RID: 5307
		public const int MailboxChange = 2;

		// Token: 0x040014BC RID: 5308
		public const int Subscriptions = 3;

		// Token: 0x040014BD RID: 5309
		public const int RemoteConduit = 4;

		// Token: 0x040014BE RID: 5310
		public const int Generator = 5;

		// Token: 0x040014BF RID: 5311
		public static Guid guid = new Guid("f16b990e-bd72-4a46-b231-b1ed417eaa17");
	}
}
