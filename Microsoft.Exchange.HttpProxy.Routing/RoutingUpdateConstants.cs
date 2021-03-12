using System;

namespace Microsoft.Exchange.HttpProxy.Routing
{
	// Token: 0x02000041 RID: 65
	public static class RoutingUpdateConstants
	{
		// Token: 0x04000062 RID: 98
		public const string RoutingEntryHeader = "X-RoutingEntry";

		// Token: 0x04000063 RID: 99
		public const string ExtraRoutingEntryHeader = "X-ExtraRoutingEntry";

		// Token: 0x04000064 RID: 100
		public const string LegacyRoutingEntryHeader = "X-LegacyRoutingEntry";

		// Token: 0x04000065 RID: 101
		public const string RoutingEntryUpdateHeader = "X-RoutingEntryUpdate";

		// Token: 0x04000066 RID: 102
		public const string ProtocolTypeAppSettingsKey = "RoutingUpdateModule.ProtocolType";
	}
}
