using System;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020003A9 RID: 937
	internal class RoleTypeConstraint
	{
		// Token: 0x06003178 RID: 12664 RVA: 0x000989A2 File Offset: 0x00096BA2
		public RoleTypeConstraint(Predicate<RoleType> predicate)
		{
			if (predicate == null)
			{
				throw new ArgumentNullException("predicate");
			}
			this.predicate = predicate;
		}

		// Token: 0x06003179 RID: 12665 RVA: 0x000989BF File Offset: 0x00096BBF
		public bool Validate(RoleType roleType)
		{
			return this.predicate(roleType);
		}

		// Token: 0x04002400 RID: 9216
		private Predicate<RoleType> predicate;

		// Token: 0x04002401 RID: 9217
		public static RoleTypeConstraint AdminRoleTypeConstraint = new RoleTypeConstraint((RoleType x) => ExchangeRole.IsAdminRole(x));

		// Token: 0x04002402 RID: 9218
		public static RoleTypeConstraint EndUserRoleTypeConstraint = new RoleTypeConstraint((RoleType x) => !ExchangeRole.IsAdminRole(x));
	}
}
