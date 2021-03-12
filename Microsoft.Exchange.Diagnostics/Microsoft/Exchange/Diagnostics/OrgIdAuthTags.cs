using System;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x020002E2 RID: 738
	public struct OrgIdAuthTags
	{
		// Token: 0x04001367 RID: 4967
		public const int OrgIdAuthentication = 0;

		// Token: 0x04001368 RID: 4968
		public const int OrgIdBasicAuth = 1;

		// Token: 0x04001369 RID: 4969
		public const int OrgIdConfiguration = 2;

		// Token: 0x0400136A RID: 4970
		public const int OrgIdUserValidation = 3;

		// Token: 0x0400136B RID: 4971
		public static Guid guid = new Guid("BD7A7CA1-6659-4EB0-A477-8F89F9A7D983");
	}
}
