using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000763 RID: 1891
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class PrincipalNotAllowedByPolicyException : StoragePermanentException
	{
		// Token: 0x06004876 RID: 18550 RVA: 0x0013104A File Offset: 0x0012F24A
		public PrincipalNotAllowedByPolicyException(PermissionSecurityPrincipal principal) : base(ServerStrings.PrincipalNotAllowedByPolicy((principal == null) ? string.Empty : principal.ToString()))
		{
			if (principal == null)
			{
				throw new ArgumentNullException("principal");
			}
			this.principal = principal;
		}

		// Token: 0x170014EB RID: 5355
		// (get) Token: 0x06004877 RID: 18551 RVA: 0x0013107C File Offset: 0x0012F27C
		public PermissionSecurityPrincipal Principal
		{
			get
			{
				return this.principal;
			}
		}

		// Token: 0x04002757 RID: 10071
		private PermissionSecurityPrincipal principal;
	}
}
