using System;
using System.Security.Principal;

namespace Microsoft.Exchange.Protocols.Smtp
{
	// Token: 0x02000409 RID: 1033
	internal static class SmtpConstants
	{
		// Token: 0x0400175E RID: 5982
		public const int DefaultMaxConnectionRatePerMinute = 1200;

		// Token: 0x0400175F RID: 5983
		public const int MaxCommandLineSize = 4000;

		// Token: 0x04001760 RID: 5984
		public const int MinTLSCipherStrength = 128;

		// Token: 0x04001761 RID: 5985
		public const int BreadcrumbsLength = 64;

		// Token: 0x04001762 RID: 5986
		public const int MaxCommandLength = 32768;

		// Token: 0x04001763 RID: 5987
		public const string AnonymousName = "anonymous";

		// Token: 0x04001764 RID: 5988
		public const string PartnerName = "partner";

		// Token: 0x04001765 RID: 5989
		public static readonly TimeSpan EventTimeout = TimeSpan.FromMinutes(10.0);

		// Token: 0x04001766 RID: 5990
		public static readonly SecurityIdentifier AnonymousSecurityIdentifier = new SecurityIdentifier(WellKnownSidType.AnonymousSid, null);
	}
}
