using System;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x020001DC RID: 476
	public sealed class OwaHttpHeader
	{
		// Token: 0x06000F59 RID: 3929 RVA: 0x0005F4FA File Offset: 0x0005D6FA
		private OwaHttpHeader()
		{
		}

		// Token: 0x04000A41 RID: 2625
		public const string Version = "X-OWA-Version";

		// Token: 0x04000A42 RID: 2626
		public const string ProxyVersion = "X-OWA-ProxyVersion";

		// Token: 0x04000A43 RID: 2627
		public const string ProxyUri = "X-OWA-ProxyUri";

		// Token: 0x04000A44 RID: 2628
		public const string ProxySid = "X-OWA-ProxySid";

		// Token: 0x04000A45 RID: 2629
		public const string ProxyCanary = "X-OWA-ProxyCanary";

		// Token: 0x04000A46 RID: 2630
		public const string EventResult = "X-OWA-EventResult";

		// Token: 0x04000A47 RID: 2631
		public const string OwaError = "X-OWA-Error";

		// Token: 0x04000A48 RID: 2632
		public const string ExplicitLogonUser = "X-OWA-ExplicitLogonUser";

		// Token: 0x04000A49 RID: 2633
		public const string UserActivity = "X-UserActivity";

		// Token: 0x04000A4A RID: 2634
		public const string ProxyWebPart = "X-OWA-ProxyWebPart";

		// Token: 0x04000A4B RID: 2635
		public const string PerfConsoleRowId = "X-OWA-PerfConsoleRowId";

		// Token: 0x04000A4C RID: 2636
		public const string IsaNoCompression = "X-NoCompression";

		// Token: 0x04000A4D RID: 2637
		public const string IsaNoBuffering = "X-NoBuffering";

		// Token: 0x04000A4E RID: 2638
		public const string PublishedAccessPath = "X-OWA-PublishedAccessPath";

		// Token: 0x04000A4F RID: 2639
		public const string DoNotCache = "X-OWA-DoNotCache";

		// Token: 0x04000A50 RID: 2640
		public const string Mailbox = "X-DiagInfoMailbox";

		// Token: 0x04000A51 RID: 2641
		public const string DomainController = "X-DiagInfoDomainController";

		// Token: 0x04000A52 RID: 2642
		public const string RpcLatency = "X-DiagInfoRpcLatency";

		// Token: 0x04000A53 RID: 2643
		public const string LdapLatency = "X-DiagInfoLdapLatency";

		// Token: 0x04000A54 RID: 2644
		public const string IisLatency = "X-DiagInfoIisLatency";

		// Token: 0x04000A55 RID: 2645
		public const string IsFromCafe = "X-IsFromCafe";
	}
}
