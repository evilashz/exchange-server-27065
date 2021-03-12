using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Security.Permissions;
using System.ServiceModel;
using System.ServiceModel.Activation;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.ControlPanel.WebControls;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020004DF RID: 1247
	[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
	public sealed class AdminRoleGroups : DataSourceService, IRoleGroups, IDataSourceService<AdminRoleGroupFilter, AdminRoleGroupRow, AdminRoleGroupObject, SetAdminRoleGroupParameter, NewAdminRoleGroupParameter>, IDataSourceService<AdminRoleGroupFilter, AdminRoleGroupRow, AdminRoleGroupObject, SetAdminRoleGroupParameter, NewAdminRoleGroupParameter, BaseWebServiceParameters>, IEditListService<AdminRoleGroupFilter, AdminRoleGroupRow, AdminRoleGroupObject, NewAdminRoleGroupParameter, BaseWebServiceParameters>, IGetListService<AdminRoleGroupFilter, AdminRoleGroupRow>, INewObjectService<AdminRoleGroupRow, NewAdminRoleGroupParameter>, IRemoveObjectsService<BaseWebServiceParameters>, IEditObjectForListService<AdminRoleGroupObject, SetAdminRoleGroupParameter, AdminRoleGroupRow>, IGetObjectService<AdminRoleGroupObject>, IGetObjectForListService<AdminRoleGroupRow>
	{
		// Token: 0x06003CD6 RID: 15574 RVA: 0x000B65D3 File Offset: 0x000B47D3
		[PrincipalPermission(SecurityAction.Demand, Role = "Get-RoleGroup?ResultSize&Filter@R:Organization")]
		public PowerShellResults<AdminRoleGroupRow> GetList(AdminRoleGroupFilter filter, SortOptions sort)
		{
			return base.GetList<AdminRoleGroupRow, AdminRoleGroupFilter>("Get-RoleGroup", filter, sort, "Name");
		}

		// Token: 0x06003CD7 RID: 15575 RVA: 0x000B65E7 File Offset: 0x000B47E7
		[PrincipalPermission(SecurityAction.Demand, Role = "Get-RoleGroup?Identity@R:Organization")]
		public PowerShellResults<AdminRoleGroupObject> GetObject(Identity identity)
		{
			return base.GetObject<AdminRoleGroupObject>(new PSCommand().AddCommand("Get-RoleGroup").AddParameter("ReadFromDomainController"), identity);
		}

		// Token: 0x06003CD8 RID: 15576 RVA: 0x000B6609 File Offset: 0x000B4809
		[PrincipalPermission(SecurityAction.Demand, Role = "Get-RoleGroup?Identity@R:Organization")]
		public PowerShellResults<AdminRoleGroupRow> GetObjectForList(Identity identity)
		{
			return base.GetObjectForList<AdminRoleGroupRow>(new PSCommand().AddCommand("Get-RoleGroup").AddParameter("ReadFromDomainController"), identity);
		}

		// Token: 0x06003CD9 RID: 15577 RVA: 0x000B662C File Offset: 0x000B482C
		[PrincipalPermission(SecurityAction.Demand, Role = "Get-RoleGroup?Identity@R:Organization")]
		public PowerShellResults<AdminRoleGroupObject> GetObjectForNew(Identity identity)
		{
			PowerShellResults<AdminRoleGroupObject> @object = base.GetObject<AdminRoleGroupObject>("Get-RoleGroup", identity);
			if (@object.SucceededWithValue && !@object.Value.RoleGroup.IsReadOnly)
			{
				AdminRoleGroupObject value = @object.Value;
				string text = Strings.CopyOf(value.Name);
				if (text.Length > 64)
				{
					text = text.Substring(0, 64).Trim();
				}
				value.Name = text;
				value.Members.Clear();
			}
			return @object;
		}

		// Token: 0x06003CDA RID: 15578 RVA: 0x000B66A4 File Offset: 0x000B48A4
		[PrincipalPermission(SecurityAction.Demand, Role = "New-RoleGroup@W:Organization")]
		public PowerShellResults<AdminRoleGroupRow> NewObject(NewAdminRoleGroupParameter properties)
		{
			properties.FaultIfNull();
			PowerShellResults<AdminRoleGroupRow> powerShellResults = new PowerShellResults<AdminRoleGroupRow>();
			if (properties.IsScopeModified)
			{
				if (properties.IsOrganizationalUnit)
				{
					if (string.IsNullOrEmpty(properties.ManagementScopeId))
					{
						throw new FaultException(Strings.InvalidOrganizationalUnit(properties.ManagementScopeId));
					}
					OrganizationalUnits organizationalUnits = new OrganizationalUnits();
					Identity identity = new Identity(properties.ManagementScopeId, properties.ManagementScopeId);
					PowerShellResults<ExtendedOrganizationalUnit> powerShellResults2 = powerShellResults.MergeErrors<ExtendedOrganizationalUnit>(organizationalUnits.GetObject(identity));
					if (powerShellResults.Failed)
					{
						return powerShellResults;
					}
					ExtendedOrganizationalUnit value = powerShellResults2.Value;
					properties.RecipientOrganizationalUnitScope = new Identity(value.Id, value.Name);
				}
				else
				{
					PowerShellResults<ManagementScopeRow> managementScope = this.GetManagementScope(properties.ManagementScopeId, powerShellResults);
					if (powerShellResults.Failed)
					{
						return powerShellResults;
					}
					if (managementScope != null && managementScope.SucceededWithValue)
					{
						ManagementScopeRow value2 = managementScope.Value;
						if (value2.ScopeRestrictionType == ScopeRestrictionType.RecipientScope)
						{
							properties.RecipientWriteScope = value2.Identity;
						}
						else if (value2.ScopeRestrictionType == ScopeRestrictionType.ServerScope)
						{
							properties.ConfigWriteScope = value2.Identity;
						}
					}
				}
			}
			powerShellResults = base.NewObject<AdminRoleGroupRow, NewAdminRoleGroupParameter>("New-RoleGroup", properties);
			if (powerShellResults.Succeeded && powerShellResults.HasWarnings)
			{
				powerShellResults.Warnings = null;
			}
			return powerShellResults;
		}

		// Token: 0x06003CDB RID: 15579 RVA: 0x000B67CC File Offset: 0x000B49CC
		[PrincipalPermission(SecurityAction.Demand, Role = "Remove-RoleGroup?Identity@W:Organization")]
		public PowerShellResults RemoveObjects(Identity[] identities, BaseWebServiceParameters parameters)
		{
			return base.RemoveObjects("Remove-RoleGroup", identities, parameters);
		}

		// Token: 0x06003CDC RID: 15580 RVA: 0x000B67DC File Offset: 0x000B49DC
		[PrincipalPermission(SecurityAction.Demand, Role = "FFO+Get-RoleGroup?Identity@R:Organization")]
		[PrincipalPermission(SecurityAction.Demand, Role = "Get-RoleGroup?Identity@R:Organization+Set-RoleGroup?Identity")]
		public PowerShellResults<AdminRoleGroupRow> SetObject(Identity identity, SetAdminRoleGroupParameter properties)
		{
			properties.FaultIfNull();
			PowerShellResults<AdminRoleGroupRow> powerShellResults = new PowerShellResults<AdminRoleGroupRow>();
			powerShellResults = this.HandleManagementRoleAssignments(identity, properties, powerShellResults);
			if (powerShellResults.Failed)
			{
				return powerShellResults;
			}
			this.UpdateRoleGroupMembers(identity, properties.Members, powerShellResults);
			if (powerShellResults.Failed)
			{
				return powerShellResults;
			}
			powerShellResults = this.SetRoleGroup(identity, properties, powerShellResults);
			if (powerShellResults.HasWarnings)
			{
				powerShellResults.Warnings = null;
			}
			return powerShellResults;
		}

		// Token: 0x06003CDD RID: 15581 RVA: 0x000B683C File Offset: 0x000B4A3C
		private void UpdateRoleGroupMembers(Identity identity, Identity[] members, PowerShellResults<AdminRoleGroupRow> results)
		{
			if (members != null)
			{
				RoleGroupMembers roleGroupMembers = new RoleGroupMembers();
				results.MergeErrors<RoleGroupMembersRow>(roleGroupMembers.SetObject(identity, new SetRoleGroupMembersParameter
				{
					Members = members
				}));
			}
		}

		// Token: 0x06003CDE RID: 15582 RVA: 0x000B686E File Offset: 0x000B4A6E
		private PowerShellResults<AdminRoleGroupRow> SetRoleGroup(Identity identity, SetAdminRoleGroupParameter properties, PowerShellResults<AdminRoleGroupRow> results)
		{
			if (properties.Description != null || properties.Name != null)
			{
				results = base.SetObject<AdminRoleGroupObject, SetAdminRoleGroupParameter, AdminRoleGroupRow>("Set-RoleGroup", identity, properties);
			}
			return results;
		}

		// Token: 0x06003CDF RID: 15583 RVA: 0x000B6890 File Offset: 0x000B4A90
		private PowerShellResults<AdminRoleGroupRow> HandleManagementRoleAssignments(Identity identity, SetAdminRoleGroupParameter properties, PowerShellResults<AdminRoleGroupRow> results)
		{
			if (!properties.IsRolesModified && !properties.IsScopeModified)
			{
				return results;
			}
			if (properties.IsOrganizationalUnit)
			{
				this.GetOrganizationalUnit(properties, results);
			}
			this.GetOriginalObject(identity, properties, results);
			if (results.Failed)
			{
				return results;
			}
			Identity[] roles = properties.Roles;
			IEnumerable<Identity> newCollection = (roles != null) ? ((IEnumerable<Identity>)roles) : properties.OriginalObject.RoleIdentities;
			Delta<Identity> delta = properties.OriginalObject.RoleIdentities.CalculateDelta(newCollection);
			if (delta.UnchangedObjects.Count == 0 && delta.AddedObjects.Count == 0)
			{
				throw new FaultException(Strings.NoRoles);
			}
			if (!properties.IsScopeModified)
			{
				this.PrepareObjectsForRoleAssignmentWithoutScope(identity, properties, results);
			}
			else
			{
				this.PrepareObjectsForForRoleAssignmentWithScope(identity, properties, results);
			}
			if (results.Failed)
			{
				return results;
			}
			return this.UpdateRoleAssignments(delta, identity, properties, results);
		}

		// Token: 0x06003CE0 RID: 15584 RVA: 0x000B6960 File Offset: 0x000B4B60
		private void PrepareObjectsForForRoleAssignmentWithScope(Identity identity, SetAdminRoleGroupParameter properties, PowerShellResults<AdminRoleGroupRow> results)
		{
			if (!properties.IsOrganizationalUnit)
			{
				PowerShellResults<ManagementScopeRow> managementScope = this.GetManagementScope(properties.ManagementScopeId, results);
				if (results.Failed)
				{
					return;
				}
				if (managementScope != null && managementScope.SucceededWithValue)
				{
					properties.ManagementScopeRow = managementScope.Value;
				}
			}
		}

		// Token: 0x06003CE1 RID: 15585 RVA: 0x000B69A4 File Offset: 0x000B4BA4
		private void PrepareObjectsForRoleAssignmentWithoutScope(Identity identity, SetAdminRoleGroupParameter properties, PowerShellResults<AdminRoleGroupRow> results)
		{
			if (properties.OriginalObject.IsOrganizationalUnit)
			{
				PowerShellResults<ExtendedOrganizationalUnit> organizationalUnit = this.GetOrganizationalUnit(properties.OriginalObject.ManagementScopeId, results);
				if (results.Failed)
				{
					return;
				}
				if (organizationalUnit.SucceededWithValue)
				{
					properties.OrganizationalUnitRow = organizationalUnit.Value;
					return;
				}
			}
			else
			{
				PowerShellResults<ManagementScopeRow> managementScope = this.GetManagementScope(properties.OriginalObject.ManagementScopeId, results);
				if (results.Failed)
				{
					return;
				}
				if (managementScope != null && managementScope.SucceededWithValue)
				{
					properties.ManagementScopeRow = managementScope.Value;
				}
			}
		}

		// Token: 0x06003CE2 RID: 15586 RVA: 0x000B6A24 File Offset: 0x000B4C24
		private PowerShellResults<AdminRoleGroupRow> UpdateRoleAssignments(Delta<Identity> delta, Identity identity, SetAdminRoleGroupParameter properties, PowerShellResults<AdminRoleGroupRow> results)
		{
			ManagementScopeRow managementScopeRow = properties.ManagementScopeRow;
			ManagementRoleAssignments roleAssignmentsWebService = new ManagementRoleAssignments();
			results = this.SetRoleAssignments(delta.UnchangedObjects, roleAssignmentsWebService, identity, managementScopeRow, properties.OrganizationalUnitRow, results);
			if (results.Failed)
			{
				return results;
			}
			results = this.AddRoleAssignments(delta.AddedObjects, roleAssignmentsWebService, identity, managementScopeRow, properties.OrganizationalUnitRow, results);
			if (results.Failed)
			{
				return results;
			}
			results = this.RemoveRoleAssignments(delta.RemovedObjects, roleAssignmentsWebService, identity, results);
			if (results.Failed)
			{
				return results;
			}
			return results;
		}

		// Token: 0x06003CE3 RID: 15587 RVA: 0x000B6AAC File Offset: 0x000B4CAC
		private PowerShellResults<AdminRoleGroupRow> AddRoleAssignments(IEnumerable<Identity> addedRoles, ManagementRoleAssignments roleAssignmentsWebService, Identity identity, ManagementScopeRow scopeRow, ExtendedOrganizationalUnit ouRow, PowerShellResults<AdminRoleGroupRow> results)
		{
			if (addedRoles != null)
			{
				foreach (Identity identity2 in addedRoles)
				{
					NewManagementRoleAssignment newManagementRoleAssignment = new NewManagementRoleAssignment();
					newManagementRoleAssignment.Role = identity2;
					newManagementRoleAssignment.SecurityGroup = identity.RawIdentity;
					this.SetScopeInfoInParameter(identity2, newManagementRoleAssignment, scopeRow, ouRow, results);
					if (results.Failed)
					{
						return results;
					}
					results.MergeErrors<ManagementRoleAssignment>(roleAssignmentsWebService.NewObject(newManagementRoleAssignment));
				}
				return results;
			}
			return results;
		}

		// Token: 0x06003CE4 RID: 15588 RVA: 0x000B6B50 File Offset: 0x000B4D50
		private PowerShellResults<AdminRoleGroupRow> RemoveRoleAssignments(IEnumerable<Identity> removedRoles, ManagementRoleAssignments roleAssignmentsWebService, Identity identity, PowerShellResults<AdminRoleGroupRow> results)
		{
			if (removedRoles != null)
			{
				foreach (Identity roleIdentity in removedRoles)
				{
					PowerShellResults<ManagementRoleAssignment> roleAssignments = this.GetRoleAssignments(roleIdentity, identity, roleAssignmentsWebService);
					if (roleAssignments.Failed)
					{
						results.MergeErrors<ManagementRoleAssignment>(roleAssignments);
						return results;
					}
					if (roleAssignments != null && roleAssignments.Output != null)
					{
						ManagementRoleAssignment[] output = roleAssignments.Output;
						IEnumerable<Identity> source = from entry in output
						where entry.DelegationType == RoleAssignmentDelegationType.Regular
						select entry.Identity;
						results.MergeErrors(roleAssignmentsWebService.RemoveObjects(source.ToArray<Identity>(), null));
						if (results.Failed)
						{
							return results;
						}
					}
				}
				return results;
			}
			return results;
		}

		// Token: 0x06003CE5 RID: 15589 RVA: 0x000B6C48 File Offset: 0x000B4E48
		private PowerShellResults<AdminRoleGroupRow> SetRoleAssignments(IEnumerable<Identity> unchangedRoles, ManagementRoleAssignments roleAssignmentsWebService, Identity identity, ManagementScopeRow scopeRow, ExtendedOrganizationalUnit ouRow, PowerShellResults<AdminRoleGroupRow> results)
		{
			if ((ouRow != null || scopeRow != null) && unchangedRoles != null)
			{
				foreach (Identity roleIdentity in unchangedRoles)
				{
					PowerShellResults<ManagementRoleAssignment> roleAssignments = this.GetRoleAssignments(roleIdentity, identity, roleAssignmentsWebService);
					if (roleAssignments.Failed)
					{
						results.MergeErrors<ManagementRoleAssignment>(roleAssignments);
						return results;
					}
					SetManagementRoleAssignment properties = this.SetScopeInfoInParameter(roleIdentity, null, scopeRow, ouRow, results);
					if (results.Failed)
					{
						return results;
					}
					ManagementRoleAssignment[] output = roleAssignments.Output;
					foreach (ManagementRoleAssignment managementRoleAssignment in output)
					{
						if (managementRoleAssignment.DelegationType == RoleAssignmentDelegationType.Regular)
						{
							results.MergeErrors<ManagementRoleAssignment>(roleAssignmentsWebService.SetObject(managementRoleAssignment.Identity, properties));
							if (results.Failed)
							{
								return results;
							}
						}
					}
				}
				return results;
			}
			return results;
		}

		// Token: 0x06003CE6 RID: 15590 RVA: 0x000B6D40 File Offset: 0x000B4F40
		private void GetOriginalObject(Identity identity, SetAdminRoleGroupParameter properties, PowerShellResults<AdminRoleGroupRow> results)
		{
			PowerShellResults<AdminRoleGroupObject> powerShellResults = results.MergeErrors<AdminRoleGroupObject>(base.GetObject<AdminRoleGroupObject>(new PSCommand().AddCommand("Get-RoleGroup").AddParameter("ReadFromDomainController"), identity));
			if (powerShellResults.SucceededWithValue)
			{
				properties.OriginalObject = powerShellResults.Value;
			}
		}

		// Token: 0x06003CE7 RID: 15591 RVA: 0x000B6D88 File Offset: 0x000B4F88
		private void GetOrganizationalUnit(SetAdminRoleGroupParameter properties, PowerShellResults<AdminRoleGroupRow> results)
		{
			if (string.IsNullOrEmpty(properties.ManagementScopeId))
			{
				throw new FaultException(Strings.InvalidOrganizationalUnit(properties.ManagementScopeId));
			}
			PowerShellResults<ExtendedOrganizationalUnit> organizationalUnit = this.GetOrganizationalUnit(properties.ManagementScopeId, results);
			if (results.Failed)
			{
				throw new FaultException(Strings.InvalidOrganizationalUnit(properties.ManagementScopeId));
			}
			if (organizationalUnit.SucceededWithValue)
			{
				properties.OrganizationalUnitRow = organizationalUnit.Value;
			}
		}

		// Token: 0x06003CE8 RID: 15592 RVA: 0x000B6DF8 File Offset: 0x000B4FF8
		private PowerShellResults<ExtendedOrganizationalUnit> GetOrganizationalUnit(string ou, PowerShellResults<AdminRoleGroupRow> results)
		{
			OrganizationalUnits organizationalUnits = new OrganizationalUnits();
			Identity identity = new Identity(ou, ou);
			return results.MergeErrors<ExtendedOrganizationalUnit>(organizationalUnits.GetObject(identity));
		}

		// Token: 0x06003CE9 RID: 15593 RVA: 0x000B6E24 File Offset: 0x000B5024
		private PowerShellResults<ManagementScopeRow> GetManagementScope(string managementScope, PowerShellResults<AdminRoleGroupRow> results)
		{
			PowerShellResults<ManagementScopeRow> result = null;
			if (!string.IsNullOrEmpty(managementScope) && !ManagementScopeRow.IsDefaultScope(managementScope))
			{
				ManagementScopes managementScopes = new ManagementScopes();
				Identity identity = new Identity(managementScope, managementScope);
				result = results.MergeErrors<ManagementScopeRow>(managementScopes.GetObject(identity));
			}
			return result;
		}

		// Token: 0x06003CEA RID: 15594 RVA: 0x000B6E60 File Offset: 0x000B5060
		private PowerShellResults<ManagementRoleAssignment> GetRoleAssignments(Identity roleIdentity, Identity roleAssignee, ManagementRoleAssignments roleAssignmentsWebService)
		{
			return roleAssignmentsWebService.GetList(new ManagementRoleAssignmentFilter
			{
				Role = roleIdentity,
				Delegating = false,
				RoleAssignee = roleAssignee
			}, null);
		}

		// Token: 0x06003CEB RID: 15595 RVA: 0x000B6E90 File Offset: 0x000B5090
		private PowerShellResults<ManagementRoleObject> GetManagementRole(Identity roleIdentity, PowerShellResults<AdminRoleGroupRow> results)
		{
			ManagementRoles managementRoles = new ManagementRoles();
			return results.MergeErrors<ManagementRoleObject>(managementRoles.GetObject(roleIdentity));
		}

		// Token: 0x06003CEC RID: 15596 RVA: 0x000B6EB0 File Offset: 0x000B50B0
		private SetManagementRoleAssignment SetScopeInfoInParameter(Identity roleIdentity, SetManagementRoleAssignment param, ManagementScopeRow scopeRow, ExtendedOrganizationalUnit ouRow, PowerShellResults<AdminRoleGroupRow> results)
		{
			SetManagementRoleAssignment setManagementRoleAssignment = param;
			if (setManagementRoleAssignment == null)
			{
				setManagementRoleAssignment = new SetManagementRoleAssignment();
			}
			if (ouRow != null || scopeRow != null)
			{
				if (scopeRow != null && scopeRow.ScopeRestrictionType == ScopeRestrictionType.ServerScope)
				{
					setManagementRoleAssignment.RecipientWriteScope = null;
					setManagementRoleAssignment.ConfigWriteScope = scopeRow.Identity;
				}
				else
				{
					if (!Util.IsDataCenter)
					{
						if (scopeRow != null && scopeRow.ScopeRestrictionType == ScopeRestrictionType.DatabaseScope)
						{
							setManagementRoleAssignment.ConfigWriteScope = scopeRow.Identity;
						}
						else
						{
							setManagementRoleAssignment.ConfigWriteScope = null;
						}
					}
					if (ouRow != null)
					{
						setManagementRoleAssignment.OrganizationalUnit = new Identity(ouRow.Id, ouRow.Name);
					}
					else if (scopeRow != null && scopeRow.ScopeRestrictionType == ScopeRestrictionType.RecipientScope)
					{
						setManagementRoleAssignment.RecipientWriteScope = scopeRow.Identity;
					}
				}
			}
			return setManagementRoleAssignment;
		}

		// Token: 0x040027C3 RID: 10179
		internal const string GetCmdlet = "Get-RoleGroup";

		// Token: 0x040027C4 RID: 10180
		internal const string SetCmdlet = "Set-RoleGroup";

		// Token: 0x040027C5 RID: 10181
		internal const string NewCmdlet = "New-RoleGroup";

		// Token: 0x040027C6 RID: 10182
		internal const string RemoveCmdlet = "Remove-RoleGroup";

		// Token: 0x040027C7 RID: 10183
		internal const int NameMaxLength = 64;

		// Token: 0x040027C8 RID: 10184
		internal const string ReadScope = "@R:Organization";

		// Token: 0x040027C9 RID: 10185
		internal const string WriteScope = "@W:Organization";

		// Token: 0x040027CA RID: 10186
		private const string GetListRole = "Get-RoleGroup?ResultSize&Filter@R:Organization";

		// Token: 0x040027CB RID: 10187
		private const string GetObjectRole = "Get-RoleGroup?Identity@R:Organization";

		// Token: 0x040027CC RID: 10188
		private const string NewObjectRole = "New-RoleGroup@W:Organization";

		// Token: 0x040027CD RID: 10189
		private const string RemoveObjectsRole = "Remove-RoleGroup?Identity@W:Organization";

		// Token: 0x040027CE RID: 10190
		private const string SetObjectRole = "Get-RoleGroup?Identity@R:Organization+Set-RoleGroup?Identity";
	}
}
