using System;
using System.Security.Principal;

namespace Microsoft.Exchange.Security.Authorization
{
	// Token: 0x02000024 RID: 36
	public interface ISecurityAccessTokenEx
	{
		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060000F4 RID: 244
		SecurityIdentifier UserSid { get; }

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060000F5 RID: 245
		SidBinaryAndAttributes[] GroupSids { get; }

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060000F6 RID: 246
		SidBinaryAndAttributes[] RestrictedGroupSids { get; }
	}
}
