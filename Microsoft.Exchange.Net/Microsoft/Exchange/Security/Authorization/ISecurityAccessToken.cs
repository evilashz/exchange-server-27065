using System;

namespace Microsoft.Exchange.Security.Authorization
{
	// Token: 0x02000027 RID: 39
	public interface ISecurityAccessToken
	{
		// Token: 0x17000036 RID: 54
		// (get) Token: 0x06000101 RID: 257
		// (set) Token: 0x06000102 RID: 258
		string UserSid { get; set; }

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000103 RID: 259
		// (set) Token: 0x06000104 RID: 260
		SidStringAndAttributes[] GroupSids { get; set; }

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06000105 RID: 261
		// (set) Token: 0x06000106 RID: 262
		SidStringAndAttributes[] RestrictedGroupSids { get; set; }
	}
}
