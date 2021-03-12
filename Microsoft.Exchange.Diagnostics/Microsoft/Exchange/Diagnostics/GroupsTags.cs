using System;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x020002FF RID: 767
	public struct GroupsTags
	{
		// Token: 0x04001455 RID: 5205
		public const int GroupNotificationStorage = 0;

		// Token: 0x04001456 RID: 5206
		public const int UnseenItemsReader = 1;

		// Token: 0x04001457 RID: 5207
		public const int COWGroupMessageEscalation = 2;

		// Token: 0x04001458 RID: 5208
		public const int COWGroupMessageWSPublishing = 3;

		// Token: 0x04001459 RID: 5209
		public static Guid guid = new Guid("1E4EC963-CD8B-4D26-A28B-832E3EA645CA");
	}
}
