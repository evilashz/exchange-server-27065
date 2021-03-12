using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics.Components.Authorization;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000652 RID: 1618
	internal class NonDeprecatedRoleUpgrader : RoleUpgrader
	{
		// Token: 0x060038B1 RID: 14513 RVA: 0x000ED665 File Offset: 0x000EB865
		protected NonDeprecatedRoleUpgrader(RoleUpgradeConfigurationSettings settings, RoleNameMapping roleNameMapping) : base(settings, roleNameMapping)
		{
		}

		// Token: 0x060038B2 RID: 14514 RVA: 0x000ED670 File Offset: 0x000EB870
		public override void UpdateRole(RoleDefinition definition)
		{
			List<string> enabledPermissionFeatures = null;
			if (this.settings.ServicePlanSettings != null)
			{
				if (definition.IsEndUserRole && !this.settings.ServicePlanSettings.Organization.PerMBXPlanRoleAssignmentPolicyEnabled)
				{
					enabledPermissionFeatures = this.settings.ServicePlanSettings.GetAggregatedMailboxPlanPermissions();
				}
				else if (!definition.IsEndUserRole)
				{
					enabledPermissionFeatures = this.settings.ServicePlanSettings.Organization.GetEnabledPermissionFeatures();
				}
			}
			base.CreateOrUpdateRole(this.roleNameMapping, definition, enabledPermissionFeatures);
		}

		// Token: 0x060038B3 RID: 14515 RVA: 0x000ED6ED File Offset: 0x000EB8ED
		public override void UpdateRoles(List<RoleDefinition> rolesDefinitions)
		{
			throw new NotImplementedException("NonDeprecatedRoleUpgrader feature not implemented");
		}

		// Token: 0x060038B4 RID: 14516 RVA: 0x000ED6FC File Offset: 0x000EB8FC
		public static bool CanUpgrade(RoleUpgradeConfigurationSettings settings, RoleNameMapping roleNameMapping, RoleDefinition roleDefinition, out RoleUpgrader roleUpgrader)
		{
			ExTraceGlobals.AccessCheckTracer.TraceFunction<string>(20006L, "-->NonDeprecatedRoleUpgrader.CanUpgrade: roleName = {0}", roleDefinition.RoleName);
			bool flag = false;
			roleUpgrader = null;
			if (settings.Organization == null)
			{
				flag = true;
			}
			else if (!roleDefinition.IsEndUserRole || !settings.ServicePlanSettings.Organization.PerMBXPlanRoleAssignmentPolicyEnabled)
			{
				flag = true;
			}
			flag = (flag && (roleNameMapping == null || (!roleNameMapping.IsDeprecatedRole && !roleNameMapping.IsSplitting)));
			if (flag)
			{
				roleUpgrader = new NonDeprecatedRoleUpgrader(settings, roleNameMapping);
			}
			ExTraceGlobals.AccessCheckTracer.TraceFunction<bool>(20006L, "<--NonDeprecatedRoleUpgrader.CanUpgrade: canUpgrade = {0}", flag);
			return flag;
		}
	}
}
