using System;

namespace Microsoft.Exchange.Security.Authentication
{
	// Token: 0x02000047 RID: 71
	internal static class CookieNames
	{
		// Token: 0x02000048 RID: 72
		public static class Common
		{
			// Token: 0x040001D7 RID: 471
			public const string BackEndServerCookieName = "X-BackEndCookie";
		}

		// Token: 0x02000049 RID: 73
		public static class Owa
		{
			// Token: 0x040001D8 RID: 472
			public const string DFPOWAVdirCookie = "X-DFPOWA-Vdir";
		}

		// Token: 0x0200004A RID: 74
		public static class Ecp
		{
			// Token: 0x040001D9 RID: 473
			internal const string TargetServerParameter = "TargetServer";

			// Token: 0x040001DA RID: 474
			internal const string VersionParameter = "ExchClientVer";
		}

		// Token: 0x0200004B RID: 75
		public static class Fba
		{
			// Token: 0x040001DB RID: 475
			internal const string CadataCookie = "cadata";

			// Token: 0x040001DC RID: 476
			internal const string CadataKeyCookie = "cadataKey";

			// Token: 0x040001DD RID: 477
			internal const string CadataIVCookie = "cadataIV";

			// Token: 0x040001DE RID: 478
			internal const string CadataTTLCookie = "cadataTTL";

			// Token: 0x040001DF RID: 479
			internal const string CadataSigCookie = "cadataSig";
		}
	}
}
