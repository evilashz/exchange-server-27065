using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.ServiceModel.Activation;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000538 RID: 1336
	[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
	public sealed class RoleAssignmentPolicies : DataSourceService, IRoleAssignmentPolicies, IDataSourceService<RoleAssignmentPolicyFilter, RoleAssignmentPolicyRow, RoleAssignmentPolicy, SetRoleAssignmentPolicy, NewRoleAssignmentPolicy>, IDataSourceService<RoleAssignmentPolicyFilter, RoleAssignmentPolicyRow, RoleAssignmentPolicy, SetRoleAssignmentPolicy, NewRoleAssignmentPolicy, BaseWebServiceParameters>, IEditListService<RoleAssignmentPolicyFilter, RoleAssignmentPolicyRow, RoleAssignmentPolicy, NewRoleAssignmentPolicy, BaseWebServiceParameters>, IGetListService<RoleAssignmentPolicyFilter, RoleAssignmentPolicyRow>, INewObjectService<RoleAssignmentPolicyRow, NewRoleAssignmentPolicy>, IRemoveObjectsService<BaseWebServiceParameters>, IEditObjectForListService<RoleAssignmentPolicy, SetRoleAssignmentPolicy, RoleAssignmentPolicyRow>, IGetObjectService<RoleAssignmentPolicy>, IGetObjectForListService<RoleAssignmentPolicyRow>
	{
		// Token: 0x06003F3A RID: 16186 RVA: 0x000BE464 File Offset: 0x000BC664
		[PrincipalPermission(SecurityAction.Demand, Role = "Get-RoleAssignmentPolicy@R:Organization")]
		public PowerShellResults<RoleAssignmentPolicyRow> GetList(RoleAssignmentPolicyFilter filter, SortOptions sort)
		{
			return base.GetList<RoleAssignmentPolicyRow, RoleAssignmentPolicyFilter>("Get-RoleAssignmentPolicy", filter, sort);
		}

		// Token: 0x06003F3B RID: 16187 RVA: 0x000BE474 File Offset: 0x000BC674
		public PowerShellResults<EndUserRoleRow> GetAssignedEndUserRoles(RoleAssignmentPolicyFilter filter, SortOptions sort)
		{
			PowerShellResults<RoleAssignmentPolicy> @object = this.GetObject(filter.Policy);
			PowerShellResults<EndUserRoleRow> powerShellResults = new PowerShellResults<EndUserRoleRow>
			{
				ErrorRecords = @object.ErrorRecords,
				Warnings = @object.Warnings
			};
			if (@object.SucceededWithValue)
			{
				EndUserRoles endUserRoles = new EndUserRoles();
				powerShellResults.MergeAll(endUserRoles.GetList(null, null));
				if (powerShellResults.Succeeded)
				{
					List<EndUserRoleRow> list = new List<EndUserRoleRow>();
					foreach (EndUserRoleRow endUserRoleRow in powerShellResults.Output)
					{
						if (@object.Value.AssignedEndUserRoles.Contains(endUserRoleRow.Identity))
						{
							list.Add(endUserRoleRow);
						}
					}
					powerShellResults.Output = list.ToArray();
				}
			}
			return powerShellResults;
		}

		// Token: 0x06003F3C RID: 16188 RVA: 0x000BE52D File Offset: 0x000BC72D
		[PrincipalPermission(SecurityAction.Demand, Role = "Get-RoleAssignmentPolicy@R:Organization")]
		public PowerShellResults<RoleAssignmentPolicyRow> GetObjectForList(Identity identity)
		{
			return base.GetObjectForList<RoleAssignmentPolicyRow>("Get-RoleAssignmentPolicy", identity);
		}

		// Token: 0x06003F3D RID: 16189 RVA: 0x000BE53C File Offset: 0x000BC73C
		[PrincipalPermission(SecurityAction.Demand, Role = "Dedicated+Get-ManagementRoleAssignment@R:Organization+Get-RoleAssignmentPolicy?Identity@R:Organization")]
		[PrincipalPermission(SecurityAction.Demand, Role = "MultiTenant+Get-MailboxPlan@R:Organization+Get-ManagementRoleAssignment@R:Organization+Get-RoleAssignmentPolicy?Identity@R:Organization")]
		[PrincipalPermission(SecurityAction.Demand, Role = "Enterprise+Get-ManagementRoleAssignment@R:Organization+Get-RoleAssignmentPolicy?Identity@R:Organization")]
		public PowerShellResults<RoleAssignmentPolicy> GetObject(Identity identity)
		{
			PowerShellResults<RoleAssignmentPolicy> @object = base.GetObject<RoleAssignmentPolicy>("Get-RoleAssignmentPolicy", identity);
			if (!@object.HasValue)
			{
				return @object;
			}
			RoleAssignmentPolicy value = @object.Value;
			@object.Output = new RoleAssignmentPolicy[]
			{
				value
			};
			return @object;
		}

		// Token: 0x06003F3E RID: 16190 RVA: 0x000BE57A File Offset: 0x000BC77A
		[PrincipalPermission(SecurityAction.Demand, Role = "New-RoleAssignmentPolicy@W:Organization")]
		public PowerShellResults<RoleAssignmentPolicyRow> NewObject(NewRoleAssignmentPolicy properties)
		{
			return base.NewObject<RoleAssignmentPolicyRow, NewRoleAssignmentPolicy>("New-RoleAssignmentPolicy", properties);
		}

		// Token: 0x06003F3F RID: 16191 RVA: 0x000BE588 File Offset: 0x000BC788
		[PrincipalPermission(SecurityAction.Demand, Role = "Remove-RoleAssignmentPolicy?Identity@W:Organization")]
		public PowerShellResults RemoveObjects(Identity[] identities, BaseWebServiceParameters parameters)
		{
			return base.RemoveObjects("Remove-RoleAssignmentPolicy", identities, parameters);
		}

		// Token: 0x06003F40 RID: 16192 RVA: 0x000BE598 File Offset: 0x000BC798
		[PrincipalPermission(SecurityAction.Demand, Role = "Dedicated+Get-ManagementRoleAssignment@R:Organization+Get-RoleAssignmentPolicy?Identity@R:Organization+Set-RoleAssignmentPolicy?Identity+Get-ManagementRole@R:Organization+New-ManagementRoleAssignment@W:Organization+Remove-ManagementRoleAssignment?Identity@W:Organization")]
		[PrincipalPermission(SecurityAction.Demand, Role = "MultiTenant+Get-MailboxPlan@R:Organization+Get-ManagementRoleAssignment@R:Organization+Get-RoleAssignmentPolicy?Identity@R:Organization+Get-ManagementRole@R:Organization+New-ManagementRoleAssignment@W:Organization+Remove-ManagementRoleAssignment?Identity@W:Organization")]
		[PrincipalPermission(SecurityAction.Demand, Role = "Enterprise+Get-ManagementRoleAssignment@R:Organization+Get-RoleAssignmentPolicy?Identity@R:Organization+Set-RoleAssignmentPolicy?Identity+Get-ManagementRole@R:Organization+New-ManagementRoleAssignment@W:Organization+Remove-ManagementRoleAssignment?Identity@W:Organization")]
		public PowerShellResults<RoleAssignmentPolicyRow> SetObject(Identity identity, SetRoleAssignmentPolicy properties)
		{
			properties.FaultIfNull();
			PowerShellResults<RoleAssignmentPolicyRow> powerShellResults;
			if (!string.IsNullOrEmpty(properties.Name) || !string.IsNullOrEmpty(properties.Description))
			{
				powerShellResults = base.SetObject<RoleAssignmentPolicyRow, SetRoleAssignmentPolicy>("Set-RoleAssignmentPolicy", identity, properties);
			}
			else
			{
				powerShellResults = base.GetObject<RoleAssignmentPolicyRow>("Get-RoleAssignmentPolicy", identity);
			}
			if (!powerShellResults.SucceededWithValue)
			{
				powerShellResults.Output = null;
				return powerShellResults;
			}
			PowerShellResults<RoleAssignmentPolicy> @object = this.GetObject(identity);
			if (@object.SucceededWithValue && properties.AssignedEndUserRoles != null)
			{
				this.UpdateRoleAssignments(@object, properties);
			}
			if (!@object.SucceededWithValue)
			{
				powerShellResults.MergeErrors<RoleAssignmentPolicy>(@object);
				powerShellResults.Output = null;
			}
			return powerShellResults;
		}

		// Token: 0x06003F41 RID: 16193 RVA: 0x000BE630 File Offset: 0x000BC830
		private void UpdateRoleAssignments(PowerShellResults<RoleAssignmentPolicy> result, SetRoleAssignmentPolicy properties)
		{
			RoleAssignmentPolicy value = result.Value;
			ManagementRoleAssignments managementRoleAssignments = new ManagementRoleAssignments();
			Delta<Identity> delta = value.AssignedEndUserRoles.CalculateDelta(properties.AssignedEndUserRoles);
			if (delta.RemovedObjects != null && delta.RemovedObjects.Count > 0)
			{
				result.MergeErrors(this.RemoveRoleAssignments(delta.RemovedObjects, value, managementRoleAssignments));
				if (result.Failed)
				{
					return;
				}
			}
			foreach (Identity role in delta.AddedObjects)
			{
				result.MergeErrors<ManagementRoleAssignment>(managementRoleAssignments.NewObject(new NewManagementRoleAssignment
				{
					Policy = value.Identity,
					Role = role
				}));
			}
		}

		// Token: 0x06003F42 RID: 16194 RVA: 0x000BE700 File Offset: 0x000BC900
		private PowerShellResults RemoveRoleAssignments(List<Identity> roles, RoleAssignmentPolicy policy, ManagementRoleAssignments service)
		{
			PowerShellResults<ManagementRoleAssignment> list = service.GetList(new ManagementRoleAssignmentFilter
			{
				RoleAssignee = policy.Identity
			}, null);
			if (list.Failed)
			{
				return list;
			}
			List<Identity> list2 = new List<Identity>();
			foreach (ManagementRoleAssignment managementRoleAssignment in list.Output)
			{
				if (roles.Contains(managementRoleAssignment.Role.ToIdentity()))
				{
					list2.Add(managementRoleAssignment.Identity);
				}
			}
			return service.RemoveObjects(list2.ToArray(), null);
		}

		// Token: 0x040028DF RID: 10463
		private const string Noun = "RoleAssignmentPolicy";

		// Token: 0x040028E0 RID: 10464
		internal const string GetCmdlet = "Get-RoleAssignmentPolicy";

		// Token: 0x040028E1 RID: 10465
		internal const string SetCmdlet = "Set-RoleAssignmentPolicy";

		// Token: 0x040028E2 RID: 10466
		internal const string NewCmdlet = "New-RoleAssignmentPolicy";

		// Token: 0x040028E3 RID: 10467
		internal const string RemoveCmdlet = "Remove-RoleAssignmentPolicy";

		// Token: 0x040028E4 RID: 10468
		internal const string ReadScope = "@R:Organization";

		// Token: 0x040028E5 RID: 10469
		internal const string WriteScope = "@W:Organization";

		// Token: 0x040028E6 RID: 10470
		private const string GetListRole = "Get-RoleAssignmentPolicy@R:Organization";

		// Token: 0x040028E7 RID: 10471
		private const string GetObjectForListRole = "Get-RoleAssignmentPolicy@R:Organization";

		// Token: 0x040028E8 RID: 10472
		private const string GetObjectRole_Enterprise = "Enterprise+Get-ManagementRoleAssignment@R:Organization+Get-RoleAssignmentPolicy?Identity@R:Organization";

		// Token: 0x040028E9 RID: 10473
		private const string GetObjectRole_MultiTenant = "MultiTenant+Get-MailboxPlan@R:Organization+Get-ManagementRoleAssignment@R:Organization+Get-RoleAssignmentPolicy?Identity@R:Organization";

		// Token: 0x040028EA RID: 10474
		private const string GetObjectRole_Dedicated = "Dedicated+Get-ManagementRoleAssignment@R:Organization+Get-RoleAssignmentPolicy?Identity@R:Organization";

		// Token: 0x040028EB RID: 10475
		private const string NewObjectRole = "New-RoleAssignmentPolicy@W:Organization";

		// Token: 0x040028EC RID: 10476
		private const string RemoveObjectRole = "Remove-RoleAssignmentPolicy?Identity@W:Organization";

		// Token: 0x040028ED RID: 10477
		private const string SetObjectRole_Enterprise = "Enterprise+Get-ManagementRoleAssignment@R:Organization+Get-RoleAssignmentPolicy?Identity@R:Organization+Set-RoleAssignmentPolicy?Identity+Get-ManagementRole@R:Organization+New-ManagementRoleAssignment@W:Organization+Remove-ManagementRoleAssignment?Identity@W:Organization";

		// Token: 0x040028EE RID: 10478
		private const string SetObjectRole_MultiTenant = "MultiTenant+Get-MailboxPlan@R:Organization+Get-ManagementRoleAssignment@R:Organization+Get-RoleAssignmentPolicy?Identity@R:Organization+Get-ManagementRole@R:Organization+New-ManagementRoleAssignment@W:Organization+Remove-ManagementRoleAssignment?Identity@W:Organization";

		// Token: 0x040028EF RID: 10479
		private const string SetObjectRole_Dedicated = "Dedicated+Get-ManagementRoleAssignment@R:Organization+Get-RoleAssignmentPolicy?Identity@R:Organization+Set-RoleAssignmentPolicy?Identity+Get-ManagementRole@R:Organization+New-ManagementRoleAssignment@W:Organization+Remove-ManagementRoleAssignment?Identity@W:Organization";
	}
}
