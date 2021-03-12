using System;
using System.Security.AccessControl;
using System.Security.Principal;

namespace Microsoft.Exchange.Security.Authorization
{
	// Token: 0x0200064C RID: 1612
	internal class PrincipalPermissionPair
	{
		// Token: 0x06001D39 RID: 7481 RVA: 0x00035610 File Offset: 0x00033810
		public PrincipalPermissionPair(SecurityIdentifier principalSid, Permission rights, AccessControlType accessControlType)
		{
			this.principal = principalSid;
			this.permission = rights;
			this.accessControlType = accessControlType;
		}

		// Token: 0x170007F3 RID: 2035
		// (get) Token: 0x06001D3A RID: 7482 RVA: 0x0003562D File Offset: 0x0003382D
		public SecurityIdentifier Principal
		{
			get
			{
				return this.principal;
			}
		}

		// Token: 0x170007F4 RID: 2036
		// (get) Token: 0x06001D3B RID: 7483 RVA: 0x00035635 File Offset: 0x00033835
		public Permission Permission
		{
			get
			{
				return this.permission;
			}
		}

		// Token: 0x170007F5 RID: 2037
		// (get) Token: 0x06001D3C RID: 7484 RVA: 0x0003563D File Offset: 0x0003383D
		public AccessControlType AccessControlType
		{
			get
			{
				return this.accessControlType;
			}
		}

		// Token: 0x04001D95 RID: 7573
		private SecurityIdentifier principal;

		// Token: 0x04001D96 RID: 7574
		private Permission permission;

		// Token: 0x04001D97 RID: 7575
		private AccessControlType accessControlType;
	}
}
