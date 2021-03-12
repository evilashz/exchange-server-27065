using System;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Deployment;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000647 RID: 1607
	[Cmdlet("Clear", "RbacCoexistenceData")]
	public sealed class ClearRbacCoexistenceData : SetupTaskBase
	{
		// Token: 0x170010CD RID: 4301
		// (get) Token: 0x06003843 RID: 14403 RVA: 0x000E8CA0 File Offset: 0x000E6EA0
		// (set) Token: 0x06003844 RID: 14404 RVA: 0x000E8CA8 File Offset: 0x000E6EA8
		[ValidateNotNullOrEmpty]
		[Parameter]
		public override OrganizationIdParameter Organization
		{
			get
			{
				return base.Organization;
			}
			set
			{
				base.Organization = value;
			}
		}

		// Token: 0x06003845 RID: 14405 RVA: 0x000E8CD4 File Offset: 0x000E6ED4
		protected override void InternalProcessRecord()
		{
			base.InternalProcessRecord();
			RoleDefinition[] array;
			RoleNameMappingCollection roleNameMappingCollection;
			string[] array2;
			InstallCannedRbacRoles.CalculateRoleConfigurationForCurrentSKU(this.Organization, out array, out roleNameMappingCollection, out array2, out this.allAllowedRoleEntriesForSKU);
			List<ExchangeRole> list = new List<ExchangeRole>();
			List<ExchangeRole> list2 = new List<ExchangeRole>();
			bool flag = false;
			if (this.Organization != null)
			{
				ExchangeConfigurationUnit exchangeConfigUnitFromOrganizationId = OrganizationTaskHelper.GetExchangeConfigUnitFromOrganizationId(this.Organization, this.configurationSession, new Task.TaskErrorLoggingDelegate(base.WriteError), true);
				flag = !ServicePlanConfiguration.GetInstance().GetServicePlanSettings(exchangeConfigUnitFromOrganizationId.ProgramId, exchangeConfigUnitFromOrganizationId.OfferId).Organization.PerMBXPlanRoleAssignmentPolicyEnabled;
			}
			ADPagedReader<ExchangeRole> adpagedReader = this.configurationSession.FindAllPaged<ExchangeRole>();
			foreach (ExchangeRole exchangeRole in adpagedReader)
			{
				base.LogReadObject(exchangeRole);
				if (!exchangeRole.IsDeprecated)
				{
					if (exchangeRole.HasDownlevelData)
					{
						exchangeRole[ExchangeRoleSchema.InternalDownlevelRoleEntries] = null;
					}
					this.RemoveObsoleteEntriesAndParameters(exchangeRole);
					if (exchangeRole.RoleEntries.Count == 0)
					{
						base.WriteVerbose(Strings.VerboseDeprecatingRoleBecauseNoEntriesLeft(exchangeRole.Id.ToString()));
						exchangeRole.RoleState = RoleState.Deprecated;
					}
					this.configurationSession.Save(exchangeRole);
					base.LogWriteObject(exchangeRole);
				}
				if (flag && exchangeRole.IsEndUserRole)
				{
					exchangeRole.MailboxPlanIndex = string.Empty;
					this.configurationSession.Save(exchangeRole);
					base.WriteVerbose(TaskVerboseStringHelper.GetSaveObjectVerboseString(exchangeRole, this.configurationSession, typeof(ExchangeRole)));
					base.LogWriteObject(exchangeRole);
				}
				if (exchangeRole.IsDeprecated)
				{
					this.DeleteRoleAssigments(exchangeRole);
					if (exchangeRole.IsRootRole)
					{
						list.Add(exchangeRole);
					}
					else
					{
						list2.Add(exchangeRole);
					}
				}
			}
			foreach (ExchangeRole exchangeRole2 in list)
			{
				this.configurationSession.DeleteTree(exchangeRole2, delegate(ADTreeDeleteNotFinishedException de)
				{
					if (de != null)
					{
						base.WriteVerbose(de.LocalizedString);
					}
				});
				base.WriteVerbose(Strings.ProgressActivityRemovingManagementRoleTree(exchangeRole2.Id.ToString()));
			}
			foreach (ExchangeRole exchangeRole3 in list2)
			{
				ExchangeRole exchangeRole4 = this.configurationSession.Read<ExchangeRole>(exchangeRole3.Id);
				if (exchangeRole4 != null)
				{
					base.LogReadObject(exchangeRole4);
					this.configurationSession.DeleteTree(exchangeRole4, delegate(ADTreeDeleteNotFinishedException de)
					{
						if (de != null)
						{
							base.WriteVerbose(de.LocalizedString);
						}
					});
					base.WriteVerbose(Strings.ConfirmationMessageRemoveManagementRole(exchangeRole4.Id.ToString()));
				}
			}
		}

		// Token: 0x06003846 RID: 14406 RVA: 0x000E8FC0 File Offset: 0x000E71C0
		private void RemoveObsoleteEntriesAndParameters(ExchangeRole role)
		{
			if (role.RoleType == RoleType.UnScoped)
			{
				return;
			}
			List<RoleEntry> list = new List<RoleEntry>();
			foreach (RoleEntry roleEntry in role.RoleEntries)
			{
				RoleEntry roleEntry2 = roleEntry.FindAndIntersectWithMatchingParameters(this.allAllowedRoleEntriesForSKU);
				if (roleEntry2 != null)
				{
					list.Add(roleEntry2);
					if (roleEntry2 != roleEntry)
					{
						base.WriteVerbose(Strings.ConfirmationMessageRemoveManagementRoleEntry(roleEntry.ToString(), role.Id.ToString()));
						base.WriteVerbose(Strings.ConfirmationMessageNewManagementRoleEntry(roleEntry2.ToString(), role.Id.ToString()));
					}
				}
				else
				{
					base.WriteVerbose(Strings.ConfirmationMessageRemoveManagementRoleEntry(roleEntry.ToString(), role.Id.ToString()));
				}
			}
			role.RoleEntries = new MultiValuedProperty<RoleEntry>(list);
		}

		// Token: 0x06003847 RID: 14407 RVA: 0x000E90A8 File Offset: 0x000E72A8
		private void DeleteRoleAssigments(ExchangeRole deprecatedRole)
		{
			foreach (ExchangeRoleAssignment exchangeRoleAssignment in this.configurationSession.FindPaged<ExchangeRoleAssignment>(base.OrgContainerId, QueryScope.SubTree, new ComparisonFilter(ComparisonOperator.Equal, ExchangeRoleAssignmentSchema.Role, deprecatedRole.Id), null, 0))
			{
				base.LogReadObject(exchangeRoleAssignment);
				this.configurationSession.Delete(exchangeRoleAssignment);
				base.LogWriteObject(exchangeRoleAssignment);
			}
		}

		// Token: 0x040025C3 RID: 9667
		private RoleEntry[] allAllowedRoleEntriesForSKU;
	}
}
