using System;

namespace Microsoft.Exchange.InfoWorker.Common.Availability
{
	// Token: 0x0200007C RID: 124
	internal static class ErrorMessages
	{
		// Token: 0x04000215 RID: 533
		internal const string DescAuthzInitClientContextFailed = "Initialization of Authz Client Context failed with Win32 error code {0}.";

		// Token: 0x04000216 RID: 534
		internal const string DescAuthzGetInformationFromContextFailed = "AuthzGetInformationFromContext failed with Win32 error code {0}.";

		// Token: 0x04000217 RID: 535
		internal const string DescAuthzInitializeContextFromSidFailed = "AuthzInitializeContextFromSid failed with Win32 error code {0}.";

		// Token: 0x04000218 RID: 536
		internal const string DescAuthzAddSidsToContextFailed = "AuthzAddSidsToContext failed with Win32 error code {0}.";

		// Token: 0x04000219 RID: 537
		internal const string DescAuthzAccessCheckFailed = "AuthZ access check failed with Win32 error code {0}.";
	}
}
