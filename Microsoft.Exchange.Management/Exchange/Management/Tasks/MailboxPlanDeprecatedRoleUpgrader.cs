using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics.Components.Authorization;
using Microsoft.Exchange.Management.Deployment;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000655 RID: 1621
	internal class MailboxPlanDeprecatedRoleUpgrader : DeprecatedRoleUpgrader
	{
		// Token: 0x060038C4 RID: 14532 RVA: 0x000EE2DB File Offset: 0x000EC4DB
		protected MailboxPlanDeprecatedRoleUpgrader(RoleUpgradeConfigurationSettings settings, RoleNameMapping roleNameMapping) : base(settings, roleNameMapping)
		{
		}

		// Token: 0x060038C5 RID: 14533 RVA: 0x000EE2E8 File Offset: 0x000EC4E8
		public override void UpdateRoles(List<RoleDefinition> rolesDefinitions)
		{
			foreach (ServicePlan.MailboxPlan mailboxPlan in this.settings.ServicePlanSettings.MailboxPlans)
			{
				string suffix = "_" + mailboxPlan.Name;
				this.CreateOrUpdateRoles(this.roleNameMapping, rolesDefinitions, mailboxPlan.GetEnabledPermissionFeatures(), suffix, mailboxPlan.MailboxPlanIndex);
			}
		}

		// Token: 0x060038C6 RID: 14534 RVA: 0x000EE36C File Offset: 0x000EC56C
		public new static bool CanUpgrade(RoleUpgradeConfigurationSettings settings, RoleNameMapping roleNameMapping, RoleDefinition roleDefinition, out RoleUpgrader roleUpgrader)
		{
			ExTraceGlobals.AccessCheckTracer.TraceFunction<string>(20009L, "-->MailboxPlanDeprecatedRoleUpgrader.CanUpgrade: roleName = {0}", roleDefinition.RoleName);
			bool flag = false;
			roleUpgrader = null;
			if (settings.Organization != null && roleDefinition.IsEndUserRole && roleNameMapping != null && roleNameMapping.IsDeprecatedRole && settings.ServicePlanSettings.Organization.PerMBXPlanRoleAssignmentPolicyEnabled)
			{
				flag = true;
			}
			if (flag)
			{
				roleUpgrader = new MailboxPlanDeprecatedRoleUpgrader(settings, roleNameMapping);
			}
			ExTraceGlobals.AccessCheckTracer.TraceFunction<bool>(20008L, "<--MailboxPlanDeprecatedRoleUpgrader.CanUpgrade: canUpgrade = {0}", flag);
			return flag;
		}
	}
}
