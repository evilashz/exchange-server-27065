using System;

namespace Microsoft.Exchange.MailboxTransport.StoreDriverDelivery
{
	// Token: 0x02000012 RID: 18
	internal enum AccessCheckResult
	{
		// Token: 0x04000089 RID: 137
		Allowed,
		// Token: 0x0400008A RID: 138
		NotAllowedAnonymous,
		// Token: 0x0400008B RID: 139
		NotAllowedAuthenticated,
		// Token: 0x0400008C RID: 140
		NotAllowedInternalSystemError
	}
}
