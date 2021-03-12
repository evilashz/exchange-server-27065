using System;
using Microsoft.Exchange.Diagnostics.Components.Authorization;
using Microsoft.Exchange.Management.Deployment;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000653 RID: 1619
	internal class MailboxPlanRoleUpgrader : NonDeprecatedRoleUpgrader
	{
		// Token: 0x060038B5 RID: 14517 RVA: 0x000ED797 File Offset: 0x000EB997
		protected MailboxPlanRoleUpgrader(RoleUpgradeConfigurationSettings settings, RoleNameMapping roleNameMapping) : base(settings, roleNameMapping)
		{
		}

		// Token: 0x060038B6 RID: 14518 RVA: 0x000ED7A4 File Offset: 0x000EB9A4
		public override void UpdateRole(RoleDefinition definition)
		{
			foreach (ServicePlan.MailboxPlan mailboxPlan in this.settings.ServicePlanSettings.MailboxPlans)
			{
				string suffix = "_" + mailboxPlan.Name;
				base.CreateOrUpdateRole(this.roleNameMapping, definition, mailboxPlan.GetEnabledPermissionFeatures(), suffix, mailboxPlan.MailboxPlanIndex);
			}
		}

		// Token: 0x060038B7 RID: 14519 RVA: 0x000ED828 File Offset: 0x000EBA28
		public new static bool CanUpgrade(RoleUpgradeConfigurationSettings settings, RoleNameMapping roleNameMapping, RoleDefinition roleDefinition, out RoleUpgrader roleUpgrader)
		{
			ExTraceGlobals.AccessCheckTracer.TraceFunction<string>(20007L, "-->MailboxPlanRoleUpgrader.CanUpgrade: roleName = {0}", roleDefinition.RoleName);
			bool flag = false;
			roleUpgrader = null;
			if (settings.Organization != null && roleDefinition.IsEndUserRole && (roleNameMapping == null || (!roleNameMapping.IsDeprecatedRole && !roleNameMapping.IsSplitting)) && settings.ServicePlanSettings.Organization.PerMBXPlanRoleAssignmentPolicyEnabled)
			{
				flag = true;
			}
			if (flag)
			{
				roleUpgrader = new MailboxPlanRoleUpgrader(settings, roleNameMapping);
			}
			ExTraceGlobals.AccessCheckTracer.TraceFunction<bool>(20007L, "<--MailboxPlanRoleUpgrader.CanUpgrade: canUpgrade = {0}", flag);
			return flag;
		}
	}
}
