using System;

namespace Microsoft.Exchange.Transport.Agent.AntiSpam.Common
{
	// Token: 0x0200000D RID: 13
	internal static class SessionProperties
	{
		// Token: 0x04000037 RID: 55
		public const string IsAuthenticated = "Microsoft.Exchange.IsAuthenticated";

		// Token: 0x04000038 RID: 56
		public const string IsOnAllowList = "Microsoft.Exchange.IsOnAllowList";

		// Token: 0x04000039 RID: 57
		public const string IsOnDenyList = "Microsoft.Exchange.IsOnDenyList";

		// Token: 0x0400003A RID: 58
		public const string IsOnBlockList = "Microsoft.Exchange.IsOnBlockList";

		// Token: 0x0400003B RID: 59
		public const string BypassedRecipients = "Microsoft.Exchange.BypassedRecipients";

		// Token: 0x0400003C RID: 60
		public const string IsOnSafeList = "Microsoft.Exchange.IsOnSafeList";

		// Token: 0x0400003D RID: 61
		public const string IsOnBlockListErrorMessage = "Microsoft.Exchange.IsOnBlockListErrorMessage";

		// Token: 0x0400003E RID: 62
		public const string IsOnBlockListProvider = "Microsoft..Exchange.IsOnBlockListProvider";

		// Token: 0x0400003F RID: 63
		public const string OnHeloOverriddenSenderAddress = "Microsoft.Exchange.Hygiene.TenantAttribution.OverriddenSenderAddress";

		// Token: 0x04000040 RID: 64
		public const string OnConnectOverriddenSenderAddress = "Microsoft.Forefront.Antispam.IPFilter.OverriddenSenderAddress";

		// Token: 0x04000041 RID: 65
		public const string IsShadowMessage = "Microsoft.Exchange.IsShadow";
	}
}
