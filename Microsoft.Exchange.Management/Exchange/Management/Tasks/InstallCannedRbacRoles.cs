using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Deployment;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x0200064D RID: 1613
	[Cmdlet("Install", "CannedRbacRoles", DefaultParameterSetName = "Default")]
	public sealed class InstallCannedRbacRoles : InstallCannedRbacObjectsTaskBase
	{
		// Token: 0x170010D4 RID: 4308
		// (get) Token: 0x06003882 RID: 14466 RVA: 0x000EB70E File Offset: 0x000E990E
		// (set) Token: 0x06003883 RID: 14467 RVA: 0x000EB725 File Offset: 0x000E9925
		[Parameter(Mandatory = false)]
		public SwitchParameter IsFfo
		{
			get
			{
				return (SwitchParameter)base.Fields["IsFfo"];
			}
			set
			{
				base.Fields["IsFfo"] = value;
			}
		}

		// Token: 0x06003884 RID: 14468 RVA: 0x000EB740 File Offset: 0x000E9940
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			InstallCannedRbacRoles.isFfoEnvironment = (base.Fields.Contains("IsFfo") && this.IsFfo);
			this.configurationSession.SessionSettings.IsSharedConfigChecked = true;
			base.InternalProcessRecord();
			RoleDefinition[] roleDefinitions;
			RoleNameMappingCollection mapping;
			string[] rolesToRemove;
			InstallCannedRbacRoles.CalculateRoleConfigurationForCurrentSKU(this.Organization, base.ServicePlanSettings, out roleDefinitions, out mapping, out rolesToRemove, out this.allAllowedRoleEntriesForSKU);
			this.RemoveRolesAndAssignments(rolesToRemove);
			this.UpdateRolesInOrg(mapping, roleDefinitions, base.ServicePlanSettings);
			TaskLogger.LogExit();
		}

		// Token: 0x06003885 RID: 14469 RVA: 0x000EB7C5 File Offset: 0x000E99C5
		internal static void CalculateRoleConfigurationForCurrentSKU(OrganizationIdParameter organization, out RoleDefinition[] roles, out RoleNameMappingCollection nameMapping, out string[] rolesToRemove, out RoleEntry[] allAllowedRoleEntriesForSKU)
		{
			InstallCannedRbacRoles.CalculateRoleConfigurationForCurrentSKU(organization, null, out roles, out nameMapping, out rolesToRemove, out allAllowedRoleEntriesForSKU);
		}

		// Token: 0x06003886 RID: 14470 RVA: 0x000EB7D4 File Offset: 0x000E99D4
		internal static void CalculateRoleConfigurationForCurrentSKU(OrganizationIdParameter organization, ServicePlan servicePlanSettings, out RoleDefinition[] roles, out RoleNameMappingCollection nameMapping, out string[] rolesToRemove, out RoleEntry[] allAllowedRoleEntriesForSKU)
		{
			if (InstallCannedRbacRoles.isFfoEnvironment)
			{
				roles = InstallCannedRbacRoles.MergeRoleDefinitions(CannedEOPRoles_Datacenter.Definition, CannedWebServiceRoles_Datacenter.Definition);
				roles = InstallCannedRbacRoles.MergeRoleDefinitions(roles, CannedUCCRoles_Datacenter.Definition);
				nameMapping = InstallCannedRbacObjectsTaskBase.RoleNameMappingDatacenterR4;
				rolesToRemove = InstallCannedRbacObjectsTaskBase.ObsoleteRoleNamesDatacenter;
				allAllowedRoleEntriesForSKU = InstallCannedRbacRoles.MergeRoleEntries(AvailableEOPRoleEntries_Datacenter.RoleEntries, AvailableWebServiceRoleEntries_Datacenter.RoleEntries, AvailableUCCRoleEntries_Datacenter.RoleEntries);
				return;
			}
			if (organization == null)
			{
				if (Datacenter.IsMicrosoftHostedOnly(false))
				{
					roles = InstallCannedRbacRoles.MergeRoleDefinitions(CannedRoles_Datacenter.Definition, CannedWebServiceRoles_Datacenter.Definition);
					nameMapping = InstallCannedRbacObjectsTaskBase.RoleNameMappingDatacenterR4;
					rolesToRemove = InstallCannedRbacObjectsTaskBase.ObsoleteRoleNamesDatacenter;
					allAllowedRoleEntriesForSKU = InstallCannedRbacRoles.MergeRoleEntries(AvailableRoleEntries_Datacenter.RoleEntries, AvailableWebServiceRoleEntries_Datacenter.RoleEntries, null);
					return;
				}
				if (Datacenter.IsPartnerHostedOnly(false))
				{
					roles = InstallCannedRbacRoles.MergeRoleDefinitions(CannedRoles_Hosting.Definition, CannedWebServiceRoles_Hosting.Definition);
					nameMapping = InstallCannedRbacObjectsTaskBase.RoleNameMappingHostingR4;
					rolesToRemove = InstallCannedRbacObjectsTaskBase.ObsoleteRoleNamesHosting;
					allAllowedRoleEntriesForSKU = InstallCannedRbacRoles.MergeRoleEntries(AvailableRoleEntries_Hosting.RoleEntries, AvailableWebServiceRoleEntries_Hosting.RoleEntries, null);
					return;
				}
				if (Datacenter.IsDatacenterDedicated(false))
				{
					roles = InstallCannedRbacRoles.MergeRoleDefinitions(CannedRoles_Dedicated.Definition, CannedWebServiceRoles_Dedicated.Definition);
					nameMapping = InstallCannedRbacObjectsTaskBase.RoleNameMappingEnterpriseR4;
					rolesToRemove = InstallCannedRbacObjectsTaskBase.ObsoleteRoleNamesEnterprise;
					allAllowedRoleEntriesForSKU = InstallCannedRbacRoles.MergeRoleEntries(AvailableRoleEntries_Dedicated.RoleEntries, AvailableWebServiceRoleEntries_Dedicated.RoleEntries, null);
					return;
				}
				roles = InstallCannedRbacRoles.MergeRoleDefinitions(CannedRoles_Enterprise.Definition, CannedWebServiceRoles_Enterprise.Definition);
				nameMapping = InstallCannedRbacObjectsTaskBase.RoleNameMappingEnterpriseR4;
				rolesToRemove = InstallCannedRbacObjectsTaskBase.ObsoleteRoleNamesEnterprise;
				allAllowedRoleEntriesForSKU = InstallCannedRbacRoles.MergeRoleEntries(AvailableRoleEntries_Enterprise.RoleEntries, AvailableWebServiceRoleEntries_Enterprise.RoleEntries, null);
				return;
			}
			else
			{
				if (Datacenter.IsPartnerHostedOnly(false))
				{
					roles = InstallCannedRbacRoles.MergeRoleDefinitions(CannedRoles_HostedTenant.Definition, CannedWebServiceRoles_HostedTenant.Definition);
					rolesToRemove = InstallCannedRbacObjectsTaskBase.ObsoleteRoleNamesHostedTenant;
					if (servicePlanSettings != null)
					{
						List<string> enabledRoleGroupRoleAssignmentFeatures = servicePlanSettings.Organization.GetEnabledRoleGroupRoleAssignmentFeatures();
						RoleGroupRoleMapping[] definition = HostedTenant_RoleGroupDefinition.Definition;
						List<string> a;
						roles = InstallCannedRbacRoles.FilterOrgRolesByRoleGroupFilters(roles, enabledRoleGroupRoleAssignmentFeatures, definition, out a);
						rolesToRemove = InstallCannedRbacRoles.AppendIListToarray<string>(rolesToRemove, a);
					}
					nameMapping = InstallCannedRbacObjectsTaskBase.RoleNameMappingHostedTenantR4;
					allAllowedRoleEntriesForSKU = InstallCannedRbacRoles.MergeRoleEntries(AvailableRoleEntries_HostedTenant.RoleEntries, AvailableWebServiceRoleEntries_HostedTenant.RoleEntries, null);
					return;
				}
				roles = InstallCannedRbacRoles.MergeRoleDefinitions(CannedRoles_Tenant.Definition, CannedWebServiceRoles_Tenant.Definition);
				rolesToRemove = InstallCannedRbacObjectsTaskBase.ObsoleteRoleNamesTenant;
				if (servicePlanSettings != null)
				{
					List<string> enabledRoleGroupRoleAssignmentFeatures2 = servicePlanSettings.Organization.GetEnabledRoleGroupRoleAssignmentFeatures();
					RoleGroupRoleMapping[] definition2 = Tenant_RoleGroupDefinition.Definition;
					List<string> a2;
					roles = InstallCannedRbacRoles.FilterOrgRolesByRoleGroupFilters(roles, enabledRoleGroupRoleAssignmentFeatures2, definition2, out a2);
					rolesToRemove = InstallCannedRbacRoles.AppendIListToarray<string>(rolesToRemove, a2);
				}
				nameMapping = InstallCannedRbacObjectsTaskBase.RoleNameMappingTenantR4;
				allAllowedRoleEntriesForSKU = InstallCannedRbacRoles.MergeRoleEntries(AvailableRoleEntries_Tenant.RoleEntries, AvailableWebServiceRoleEntries_Tenant.RoleEntries, null);
				return;
			}
		}

		// Token: 0x06003887 RID: 14471 RVA: 0x000EBA38 File Offset: 0x000E9C38
		internal static RoleDefinition[] MergeRoleDefinitions(RoleDefinition[] cmdletRoleDefinitions, RoleDefinition[] webServiceRoleDefinitions)
		{
			List<RoleDefinition> list = cmdletRoleDefinitions.ToList<RoleDefinition>();
			List<RoleDefinition> list2 = new List<RoleDefinition>();
			for (int i = 0; i < webServiceRoleDefinitions.Length; i++)
			{
				RoleDefinition webServiceDef = webServiceRoleDefinitions[i];
				int num = list.FindIndex(delegate(RoleDefinition x)
				{
					string roleName = x.RoleName;
					RoleDefinition webServiceDef2 = webServiceDef;
					if (roleName.Equals(webServiceDef2.RoleName, StringComparison.OrdinalIgnoreCase))
					{
						RoleType roleType = x.RoleType;
						RoleDefinition webServiceDef3 = webServiceDef;
						return roleType == webServiceDef3.RoleType;
					}
					return false;
				});
				if (num >= 0)
				{
					RoleDefinition roleDefinition = list[num];
					List<RoleCmdlet> list3 = roleDefinition.Cmdlets.ToList<RoleCmdlet>();
					List<RoleCmdlet> list4 = list3;
					RoleDefinition webServiceDef4 = webServiceDef;
					list4.AddRange(webServiceDef4.Cmdlets);
					RoleDefinition value = new RoleDefinition(roleDefinition.RoleName, roleDefinition.ParentRoleName, roleDefinition.RoleType, list3.ToArray());
					list[num] = value;
				}
				else
				{
					list2.Add(webServiceDef);
				}
			}
			list.AddRange(list2);
			return list.ToArray();
		}

		// Token: 0x06003888 RID: 14472 RVA: 0x000EBB24 File Offset: 0x000E9D24
		internal static RoleEntry[] MergeRoleEntries(RoleEntry[] cmdletRoleEntries, RoleEntry[] webServiceRoleEntries, RoleEntry[] uccRoleEntries = null)
		{
			List<RoleEntry> list = cmdletRoleEntries.ToList<RoleEntry>();
			list.AddRange(webServiceRoleEntries);
			if (uccRoleEntries != null)
			{
				list.AddRange(uccRoleEntries);
			}
			list.Sort(RoleEntryComparer.Instance);
			return list.ToArray();
		}

		// Token: 0x06003889 RID: 14473 RVA: 0x000EBB5C File Offset: 0x000E9D5C
		private static T[] AppendIListToarray<T>(T[] a1, IList<T> a2)
		{
			if (a2 == null)
			{
				return a1;
			}
			if (a1 == null)
			{
				return a2.ToArray<T>();
			}
			List<T> list = new List<T>(a1.Length + a2.Count);
			list.AddRange(a1);
			foreach (T item in a2)
			{
				if (!list.Contains(item))
				{
					list.Add(item);
				}
			}
			return list.ToArray();
		}

		// Token: 0x0600388A RID: 14474 RVA: 0x000EBC00 File Offset: 0x000E9E00
		private void UpdateRolesInOrg(RoleNameMappingCollection mapping, RoleDefinition[] roleDefinitions, ServicePlan servicePlan)
		{
			TaskLogger.LogEnter();
			Container container = this.configurationSession.Read<Container>(this.rolesContainerId);
			if (container == null)
			{
				base.WriteError(new ContainerNotFoundException(this.rolesContainerId.DistinguishedName), ErrorCategory.ObjectNotFound, null);
			}
			base.LogReadObject(container);
			List<RoleDefinition> list = new List<RoleDefinition>();
			RoleUpgradeConfigurationSettings settings = new RoleUpgradeConfigurationSettings
			{
				AvailableRoleEntries = this.allAllowedRoleEntriesForSKU,
				ConfigurationSession = this.configurationSession,
				OrgContainerId = base.OrgContainerId,
				OrganizationId = ((null != base.CurrentOrganizationId) ? base.CurrentOrganizationId : base.ExecutingUserOrganizationId),
				Organization = this.Organization,
				RolesContainerId = this.rolesContainerId,
				ServicePlanSettings = base.ServicePlanSettings,
				WriteVerbose = new Task.TaskVerboseLoggingDelegate(base.WriteVerbose),
				WriteError = new Task.TaskErrorLoggingDelegate(base.WriteError),
				WriteWarning = new Task.TaskWarningLoggingDelegate(this.WriteWarning),
				LogReadObject = new RoleUpgradeConfigurationSettings.LogReadObjectDelegate(base.LogReadObject),
				LogWriteObject = new RoleUpgradeConfigurationSettings.LogWriteObjectDelegate(base.LogWriteObject),
				RemoveRoleAndAssignments = new RoleUpgradeConfigurationSettings.RemoveRoleAndAssignmentsDelegate(this.RemoveRoleAndAssignments),
				Task = this
			};
			this.RenameEndUserRolesForUnifiedRAP();
			using (List<RoleNameMapping>.Enumerator enumerator = mapping.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					RoleNameMapping map = enumerator.Current;
					if (!map.IsSplitting && !map.IsDeprecatedRole)
					{
						List<RoleDefinition> list2 = roleDefinitions.ToList<RoleDefinition>().FindAll((RoleDefinition x) => x.RoleName.Equals(map.NewName));
						if (list2 != null && list2.Count<RoleDefinition>() > 0)
						{
							RoleUpgrader roleUpgrader = RoleUpgraderFactory.GetRoleUpgrader(settings, map, list2[0]);
							roleUpgrader.UpdateRole(list2[0]);
						}
					}
				}
			}
			List<RoleDefinition> list3 = new List<RoleDefinition>();
			foreach (RoleDefinition roleDefinition in roleDefinitions)
			{
				if (!list.Contains(roleDefinition))
				{
					if (!string.IsNullOrEmpty(roleDefinition.ParentRoleName))
					{
						list3.Add(roleDefinition);
					}
					else
					{
						new List<RoleNameMapping>();
						List<RoleNameMapping> nonRenamingMappings = mapping.GetNonRenamingMappings(roleDefinition.RoleName);
						if (nonRenamingMappings != null)
						{
							using (List<RoleNameMapping>.Enumerator enumerator2 = nonRenamingMappings.GetEnumerator())
							{
								while (enumerator2.MoveNext())
								{
									RoleNameMapping roleNameMapping = enumerator2.Current;
									if (roleNameMapping.NewNames == null)
									{
										throw new InvalidOperationException("Invalid MAP:" + roleNameMapping.OldName);
									}
									RoleUpgrader roleUpgrader2 = RoleUpgraderFactory.GetRoleUpgrader(settings, roleNameMapping, roleDefinition);
									List<RoleDefinition> list4 = new List<RoleDefinition>();
									list4 = this.GetRoleDefinitionsByName(roleDefinitions, roleNameMapping.NewNames);
									if (roleNameMapping.IsSplitting)
									{
										list4.Add(roleDefinition);
									}
									roleUpgrader2.UpdateRoles(list4);
									foreach (RoleDefinition item in list4)
									{
										List<RoleNameMapping> nonRenamingMappings2 = mapping.GetNonRenamingMappings(item.RoleName);
										if (nonRenamingMappings2 == null)
										{
											list.Add(item);
										}
										else if (nonRenamingMappings2.Count == 1 && nonRenamingMappings2[0].Equals(roleNameMapping))
										{
											list.Add(item);
										}
									}
								}
								goto IL_363;
							}
						}
						RoleUpgrader roleUpgrader3 = RoleUpgraderFactory.GetRoleUpgrader(settings, null, roleDefinition);
						roleUpgrader3.UpdateRole(roleDefinition);
					}
				}
				IL_363:;
			}
			this.InstallCustomRoles(list3);
			TaskLogger.LogExit();
		}

		// Token: 0x0600388B RID: 14475 RVA: 0x000EBFB8 File Offset: 0x000EA1B8
		private void InstallCustomRoles(List<RoleDefinition> customRoles)
		{
			if (!this.IsBuildApplicableForCustomRoles())
			{
				return;
			}
			foreach (RoleDefinition customRoleDefinition in customRoles)
			{
				if (base.ServicePlanSettings != null)
				{
					if (customRoleDefinition.IsEndUserRole && !base.ServicePlanSettings.Organization.PerMBXPlanRoleAssignmentPolicyEnabled)
					{
						this.InstallCustomRole(customRoleDefinition, base.ServicePlanSettings.GetAggregatedMailboxPlanPermissions(), null, null);
					}
					else
					{
						if (customRoleDefinition.IsEndUserRole)
						{
							using (List<ServicePlan.MailboxPlan>.Enumerator enumerator2 = base.ServicePlanSettings.MailboxPlans.GetEnumerator())
							{
								while (enumerator2.MoveNext())
								{
									ServicePlan.MailboxPlan mailboxPlan = enumerator2.Current;
									string suffix = "_" + mailboxPlan.Name;
									this.InstallCustomRole(customRoleDefinition, mailboxPlan.GetEnabledPermissionFeatures(), suffix, mailboxPlan.MailboxPlanIndex);
								}
								continue;
							}
						}
						this.InstallCustomRole(customRoleDefinition, base.ServicePlanSettings.Organization.GetEnabledPermissionFeatures(), null, null);
					}
				}
				else
				{
					this.InstallCustomRole(customRoleDefinition, null, null, null);
				}
			}
		}

		// Token: 0x0600388C RID: 14476 RVA: 0x000EC0E4 File Offset: 0x000EA2E4
		private bool IsBuildApplicableForCustomRoles()
		{
			if (base.InvocationMode == InvocationMode.Install)
			{
				return true;
			}
			int objectVersion = this.configurationSession.GetOrgContainer().ObjectVersion;
			return objectVersion <= 13120;
		}

		// Token: 0x0600388D RID: 14477 RVA: 0x000EC118 File Offset: 0x000EA318
		private void InstallCustomRole(RoleDefinition customRoleDefinition, List<string> enabledPermissionFeatures, string suffix, string mailboxPlanIndex)
		{
			if (this.RoleNameExists(customRoleDefinition.RoleName + suffix))
			{
				return;
			}
			this.CreateCustomRole(customRoleDefinition, enabledPermissionFeatures, suffix, mailboxPlanIndex);
		}

		// Token: 0x0600388E RID: 14478 RVA: 0x000EC13C File Offset: 0x000EA33C
		private bool RoleNameExists(string roleName)
		{
			ComparisonFilter filter = new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.Name, roleName);
			ExchangeRole[] array = this.configurationSession.Find<ExchangeRole>(this.roleAssignmentContainerId.Parent, QueryScope.SubTree, filter, null, 1);
			return array.Length > 0;
		}

		// Token: 0x0600388F RID: 14479 RVA: 0x000EC178 File Offset: 0x000EA378
		private void CreateCustomRole(RoleDefinition customRoleDefinition, List<string> enabledPermissionFeatures, string suffix, string mailboxPlanIndex)
		{
			ADObjectId childId = this.rolesContainerId.GetChildId(customRoleDefinition.ParentRoleName + suffix);
			ExchangeRole exchangeRole = customRoleDefinition.GenerateRole(enabledPermissionFeatures, childId, suffix, mailboxPlanIndex);
			exchangeRole.OrganizationId = base.CurrentOrganizationId;
			this.configurationSession.Save(exchangeRole);
		}

		// Token: 0x06003890 RID: 14480 RVA: 0x000EC1C4 File Offset: 0x000EA3C4
		private static RoleDefinition[] FilterOrgRolesByRoleGroupFilters(RoleDefinition[] roles, List<string> enabledRoleGroupAssignmentFeatures, RoleGroupRoleMapping[] definitions, out List<string> extraRolesToRemove)
		{
			HashSet<RoleDefinition> hashSet = new HashSet<RoleDefinition>();
			foreach (RoleDefinition item in roles)
			{
				bool flag = !ExchangeRole.IsAdminRole(item.RoleType);
				if (flag)
				{
					hashSet.Add(item);
				}
				else
				{
					foreach (RoleGroupRoleMapping roleGroupRoleMapping in definitions)
					{
						foreach (RoleAssignmentDefinition roleAssignmentDefinition in roleGroupRoleMapping.Assignments)
						{
							if (roleAssignmentDefinition.RoleType == item.RoleType && roleAssignmentDefinition.SatisfyCondition(enabledRoleGroupAssignmentFeatures) && !hashSet.Contains(item))
							{
								hashSet.Add(item);
							}
						}
					}
				}
			}
			extraRolesToRemove = new List<string>();
			foreach (RoleDefinition item2 in roles)
			{
				if (!hashSet.Contains(item2))
				{
					extraRolesToRemove.Add(item2.RoleName);
				}
			}
			return hashSet.ToArray<RoleDefinition>();
		}

		// Token: 0x06003891 RID: 14481 RVA: 0x000EC2F0 File Offset: 0x000EA4F0
		private List<RoleDefinition> GetRoleDefinitionsByName(RoleDefinition[] roleDefinitions, List<string> roleDefinitionNames)
		{
			return roleDefinitions.ToList<RoleDefinition>().FindAll((RoleDefinition x) => roleDefinitionNames.Contains(x.RoleName));
		}

		// Token: 0x06003892 RID: 14482 RVA: 0x000EC324 File Offset: 0x000EA524
		private void RemoveRolesAndAssignments(string[] rolesToRemove)
		{
			foreach (string unescapedCommonName in rolesToRemove)
			{
				this.RemoveRoleAndAssignments(this.rolesContainerId.GetChildId(unescapedCommonName));
			}
		}

		// Token: 0x06003893 RID: 14483 RVA: 0x000EC368 File Offset: 0x000EA568
		private void RemoveRoleAndAssignments(ADObjectId roleId)
		{
			TaskLogger.LogEnter(new object[]
			{
				"roleId"
			});
			ExchangeRole[] array = this.configurationSession.Find<ExchangeRole>(roleId, QueryScope.SubTree, null, null, 0);
			if (array.Length > 0)
			{
				ExchangeRole exchangeRole = null;
				foreach (ExchangeRole exchangeRole2 in array)
				{
					base.LogReadObject(exchangeRole2);
					if (exchangeRole2.Id.Equals(roleId))
					{
						exchangeRole = exchangeRole2;
					}
					ADPagedReader<ExchangeRoleAssignment> adpagedReader = this.configurationSession.FindPaged<ExchangeRoleAssignment>(this.roleAssignmentContainerId, QueryScope.OneLevel, new ComparisonFilter(ComparisonOperator.Equal, ExchangeRoleAssignmentSchema.Role, exchangeRole2.Id), null, 0);
					foreach (ExchangeRoleAssignment exchangeRoleAssignment in adpagedReader)
					{
						base.LogReadObject(exchangeRoleAssignment);
						this.configurationSession.Delete(exchangeRoleAssignment);
						base.LogWriteObject(exchangeRoleAssignment);
					}
				}
				this.configurationSession.DeleteTree(exchangeRole, delegate(ADTreeDeleteNotFinishedException de)
				{
					if (de != null)
					{
						base.WriteVerbose(de.LocalizedString);
					}
				});
				base.LogWriteObject(exchangeRole);
			}
			TaskLogger.LogExit();
		}

		// Token: 0x06003894 RID: 14484 RVA: 0x000EC490 File Offset: 0x000EA690
		private void RenameEndUserRolesForUnifiedRAP()
		{
			if (base.ServicePlanSettings == null || base.ServicePlanSettings.Organization.PerMBXPlanRoleAssignmentPolicyEnabled || base.InvocationMode == InvocationMode.Install)
			{
				return;
			}
			ADPagedReader<ExchangeRole> adpagedReader = this.configurationSession.FindPaged<ExchangeRole>(this.rolesContainerId, QueryScope.OneLevel, null, null, 0);
			List<ExchangeRole> list = new List<ExchangeRole>();
			foreach (ExchangeRole exchangeRole in adpagedReader)
			{
				base.LogReadObject(exchangeRole);
				if (exchangeRole.IsEndUserRole && exchangeRole.IsRootRole)
				{
					list.Add(exchangeRole);
				}
			}
			this.RenameCannedEndUserRoles(list);
			adpagedReader = this.configurationSession.FindPaged<ExchangeRole>(this.rolesContainerId, QueryScope.SubTree, new ComparisonFilter(ComparisonOperator.Equal, ExchangeRoleSchema.IsEndUserRole, true), null, 0);
			list = new List<ExchangeRole>();
			foreach (ExchangeRole exchangeRole2 in adpagedReader)
			{
				base.LogReadObject(exchangeRole2);
				if (exchangeRole2.IsEndUserRole && exchangeRole2.Name.Contains("_DefaultMailboxPlan"))
				{
					list.Add(exchangeRole2);
				}
			}
			this.RenameCannedEndUserRoles(list);
		}

		// Token: 0x06003895 RID: 14485 RVA: 0x000EC5CC File Offset: 0x000EA7CC
		private void RenameCannedEndUserRoles(List<ExchangeRole> cannedRolesToRename)
		{
			foreach (ExchangeRole exchangeRole in cannedRolesToRename)
			{
				if (!exchangeRole.Name.Equals(exchangeRole.RoleType.ToString(), StringComparison.OrdinalIgnoreCase) && exchangeRole.Name.Contains("_DefaultMailboxPlan"))
				{
					string text = exchangeRole.IsRootRole ? exchangeRole.RoleType.ToString() : exchangeRole.Name.Replace("_DefaultMailboxPlan", string.Empty).Trim();
					QueryFilter filter = new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.Name, text);
					ExchangeRole[] array = this.configurationSession.FindPaged<ExchangeRole>(this.rolesContainerId, QueryScope.SubTree, filter, null, 0).ReadAllPages();
					if (array.Length != 0)
					{
						foreach (ExchangeRole exchangeRole2 in array)
						{
							if (exchangeRole2.IsRootRole || !exchangeRole2.IsEndUserRole)
							{
								base.WriteError(new TaskInvalidOperationException(Strings.ErrorUnableToRenameEndUserRoleNameAlreadyExist(exchangeRole.Name, exchangeRole.RoleType.ToString())), ExchangeErrorCategory.Context, null);
							}
							else if (!this.TryRenameDeriveEndUserRole(exchangeRole2))
							{
								base.WriteError(new TaskInvalidOperationException(Strings.ErrorUnableToRenameEndUserRoleNameAlreadyExist(exchangeRole.Name, exchangeRole.RoleType.ToString())), ExchangeErrorCategory.Context, null);
							}
						}
					}
					exchangeRole.Name = text;
					this.configurationSession.Save(exchangeRole);
					base.LogWriteObject(exchangeRole);
				}
			}
		}

		// Token: 0x06003896 RID: 14486 RVA: 0x000EC774 File Offset: 0x000EA974
		private bool TryRenameDeriveEndUserRole(ExchangeRole role)
		{
			if (role.IsRootRole || !role.IsEndUserRole)
			{
				throw new ArgumentException(string.Format("Invalid role {0}. Only NON root roles and endUser roles can be renamed.", role.Name));
			}
			string text = ("renamed_" + role.Name).Trim();
			if (text.Length > 64)
			{
				text = text.Substring(0, 64).Trim();
			}
			string text2 = text;
			if (text2.Length > 61)
			{
				text2 = text2.Substring(0, 61).Trim();
			}
			int num = 1;
			bool flag = false;
			for (;;)
			{
				QueryFilter filter = new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.Name, text);
				ExchangeRole[] array = this.configurationSession.FindPaged<ExchangeRole>(this.rolesContainerId, QueryScope.SubTree, filter, null, 0).ReadAllPages();
				if (array.Length == 0)
				{
					break;
				}
				text = text2 + "-" + num.ToString();
				num++;
				if (num >= 100)
				{
					goto IL_C5;
				}
			}
			flag = true;
			IL_C5:
			if (flag)
			{
				try
				{
					role.AllowEmptyRole = true;
					role.Name = text;
					this.configurationSession.Save(role);
					base.LogWriteObject(role);
				}
				catch (DataSourceOperationException ex)
				{
					this.WriteWarning(Strings.WarningRenamingRole(role.Name, ex.Message));
				}
				catch (DataValidationException ex2)
				{
					this.WriteWarning(Strings.WarningRenamingRole(role.Name, ex2.Message));
				}
			}
			return flag;
		}

		// Token: 0x040025DD RID: 9693
		private const string DefaultMailboxPlanSuffix = "_DefaultMailboxPlan";

		// Token: 0x040025DE RID: 9694
		internal static readonly List<string> DCProhibitedActions = new List<string>
		{
			"DestructiveAction",
			"DangerousAction"
		};

		// Token: 0x040025DF RID: 9695
		private static bool isFfoEnvironment = false;

		// Token: 0x040025E0 RID: 9696
		private RoleEntry[] allAllowedRoleEntriesForSKU;

		// Token: 0x040025E1 RID: 9697
		private static ExchangeResourceManager ResourceManager = ExchangeResourceManager.GetResourceManager("Microsoft.Exchange.Data.Directory.Strings", typeof(DirectoryStrings).Assembly);
	}
}
