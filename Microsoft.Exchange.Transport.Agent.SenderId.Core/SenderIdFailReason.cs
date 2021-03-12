using System;

namespace Microsoft.Exchange.SenderId
{
	// Token: 0x02000009 RID: 9
	internal enum SenderIdFailReason
	{
		// Token: 0x04000018 RID: 24
		None = 1,
		// Token: 0x04000019 RID: 25
		NotPermitted,
		// Token: 0x0400001A RID: 26
		MalformedDomain,
		// Token: 0x0400001B RID: 27
		DomainDoesNotExist
	}
}
