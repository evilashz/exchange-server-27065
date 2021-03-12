using System;

namespace Microsoft.Exchange.Security.Authorization
{
	// Token: 0x0200001C RID: 28
	internal enum AuthzContextInformation
	{
		// Token: 0x04000083 RID: 131
		UserSid = 1,
		// Token: 0x04000084 RID: 132
		GroupSids,
		// Token: 0x04000085 RID: 133
		RestrictedSids,
		// Token: 0x04000086 RID: 134
		Privileges,
		// Token: 0x04000087 RID: 135
		ExpirationTime,
		// Token: 0x04000088 RID: 136
		ServerContext,
		// Token: 0x04000089 RID: 137
		Identifier
	}
}
