using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020007C7 RID: 1991
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class RightsNotAllowedRecipient
	{
		// Token: 0x06004AC3 RID: 19139 RVA: 0x00139212 File Offset: 0x00137412
		internal RightsNotAllowedRecipient(PermissionSecurityPrincipal principal, MemberRights notAllowedRights)
		{
			Util.ThrowOnNullArgument(principal, "principal");
			this.principal = principal;
			this.notAllowedRights = notAllowedRights;
		}

		// Token: 0x1700154F RID: 5455
		// (get) Token: 0x06004AC4 RID: 19140 RVA: 0x00139233 File Offset: 0x00137433
		public PermissionSecurityPrincipal Principal
		{
			get
			{
				return this.principal;
			}
		}

		// Token: 0x17001550 RID: 5456
		// (get) Token: 0x06004AC5 RID: 19141 RVA: 0x0013923B File Offset: 0x0013743B
		public MemberRights NotAllowedRights
		{
			get
			{
				return this.notAllowedRights;
			}
		}

		// Token: 0x06004AC6 RID: 19142 RVA: 0x00139243 File Offset: 0x00137443
		public override string ToString()
		{
			return "Principal=" + this.Principal.ToString() + ", NotAllowedRights=" + this.NotAllowedRights.ToString();
		}

		// Token: 0x04002892 RID: 10386
		private PermissionSecurityPrincipal principal;

		// Token: 0x04002893 RID: 10387
		private MemberRights notAllowedRights;
	}
}
