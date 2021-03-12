using System;
using System.Security.Principal;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020000CB RID: 203
	internal struct AdminRoleDefinition
	{
		// Token: 0x060007CE RID: 1998 RVA: 0x0000CC06 File Offset: 0x0000AE06
		public AdminRoleDefinition(SecurityIdentifier sid, string roleName)
		{
			this.Sid = sid;
			this.RoleName = roleName;
		}

		// Token: 0x040004A1 RID: 1185
		public SecurityIdentifier Sid;

		// Token: 0x040004A2 RID: 1186
		public string RoleName;
	}
}
