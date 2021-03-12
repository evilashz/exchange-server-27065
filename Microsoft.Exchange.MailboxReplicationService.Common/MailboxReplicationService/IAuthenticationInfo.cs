using System;
using System.Security.Principal;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020000CA RID: 202
	internal interface IAuthenticationInfo
	{
		// Token: 0x170002A9 RID: 681
		// (get) Token: 0x060007CA RID: 1994
		WindowsPrincipal WindowsPrincipal { get; }

		// Token: 0x170002AA RID: 682
		// (get) Token: 0x060007CB RID: 1995
		SecurityIdentifier Sid { get; }

		// Token: 0x170002AB RID: 683
		// (get) Token: 0x060007CC RID: 1996
		string PrincipalName { get; }

		// Token: 0x170002AC RID: 684
		// (get) Token: 0x060007CD RID: 1997
		bool IsCertificateAuthentication { get; }
	}
}
