using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x0200064E RID: 1614
	internal static class RoleUpgraderFactory
	{
		// Token: 0x0600389A RID: 14490 RVA: 0x000EC920 File Offset: 0x000EAB20
		public static RoleUpgrader GetRoleUpgrader(RoleUpgradeConfigurationSettings settings, RoleNameMapping roleNameMapping, RoleDefinition roleDefinition)
		{
			RoleUpgrader result = null;
			foreach (RoleUpgraderFactory.CanUpgradeRoleDelegate canUpgradeRoleDelegate in RoleUpgraderFactory.knownUpgrades)
			{
				if (canUpgradeRoleDelegate(settings, roleNameMapping, roleDefinition, out result))
				{
					break;
				}
			}
			return result;
		}

		// Token: 0x040025E2 RID: 9698
		private static List<RoleUpgraderFactory.CanUpgradeRoleDelegate> knownUpgrades = new List<RoleUpgraderFactory.CanUpgradeRoleDelegate>(4)
		{
			new RoleUpgraderFactory.CanUpgradeRoleDelegate(NonDeprecatedRoleUpgrader.CanUpgrade),
			new RoleUpgraderFactory.CanUpgradeRoleDelegate(MailboxPlanRoleUpgrader.CanUpgrade),
			new RoleUpgraderFactory.CanUpgradeRoleDelegate(DeprecatedRoleUpgrader.CanUpgrade),
			new RoleUpgraderFactory.CanUpgradeRoleDelegate(MailboxPlanDeprecatedRoleUpgrader.CanUpgrade),
			new RoleUpgraderFactory.CanUpgradeRoleDelegate(SplitRoleUpgrader.CanUpgrade)
		};

		// Token: 0x0200064F RID: 1615
		// (Invoke) Token: 0x0600389D RID: 14493
		private delegate bool CanUpgradeRoleDelegate(RoleUpgradeConfigurationSettings settings, RoleNameMapping roleNameMapping, RoleDefinition roleDefinition, out RoleUpgrader roleUpgrader);
	}
}
