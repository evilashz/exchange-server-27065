using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.GroupMailbox
{
	// Token: 0x0200001F RID: 31
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class PermissionEntry
	{
		// Token: 0x06000110 RID: 272 RVA: 0x000087C1 File Offset: 0x000069C1
		public PermissionEntry(PermissionSecurityPrincipal userSecurityPrincipal, MemberRights userRights)
		{
			this.userSecurityPrincipal = userSecurityPrincipal;
			this.userRights = userRights;
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000111 RID: 273 RVA: 0x000087D7 File Offset: 0x000069D7
		public PermissionSecurityPrincipal UserSecurityPrincipal
		{
			get
			{
				return this.userSecurityPrincipal;
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000112 RID: 274 RVA: 0x000087DF File Offset: 0x000069DF
		public MemberRights UserRights
		{
			get
			{
				return this.userRights;
			}
		}

		// Token: 0x04000073 RID: 115
		private PermissionSecurityPrincipal userSecurityPrincipal;

		// Token: 0x04000074 RID: 116
		private MemberRights userRights;
	}
}
