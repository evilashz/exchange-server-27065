using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Exchange.Configuration.Authorization;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.RbacTasks
{
	// Token: 0x0200066A RID: 1642
	internal static class RoleGroupCommon
	{
		// Token: 0x060039C5 RID: 14789 RVA: 0x000F3E8C File Offset: 0x000F208C
		internal static ADObjectId RoleGroupContainerId(IRecipientSession globalCatalogSession, IConfigurationSession configurationSession)
		{
			if (configurationSession.ConfigurationNamingContext == null)
			{
				return null;
			}
			ADGroup adgroup = globalCatalogSession.ResolveWellKnownGuid<ADGroup>(RoleGroup.OrganizationManagement_InitInfo.WellKnownGuid, configurationSession.ConfigurationNamingContext);
			if (adgroup == null)
			{
				throw new ManagementObjectNotFoundException(DirectoryStrings.ExceptionADTopologyCannotFindWellKnownExchangeGroup);
			}
			return adgroup.Id.Parent;
		}

		// Token: 0x060039C6 RID: 14790 RVA: 0x000F3ED8 File Offset: 0x000F20D8
		internal static ADObjectId GetRootOrgUsgContainerId(IConfigurationSession configurationSession, ADServerSettings adServerSettings, IRecipientSession globalCatalogSession, OrganizationId organizationId)
		{
			IRecipientSession tenantOrRootOrgRecipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(adServerSettings.PreferredGlobalCatalog(globalCatalogSession.SessionSettings.PartitionId.ForestFQDN), true, ConsistencyMode.PartiallyConsistent, null, ADSessionSettings.FromAccountPartitionRootOrgScopeSet(globalCatalogSession.SessionSettings.PartitionId), 110, "GetRootOrgUsgContainerId", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\RBAC\\RoleGroup\\RoleGroupCommon.cs");
			return RoleGroupCommon.RoleGroupContainerId(tenantOrRootOrgRecipientSession, configurationSession);
		}

		// Token: 0x060039C7 RID: 14791 RVA: 0x000F3F34 File Offset: 0x000F2134
		internal static RoleGroup PopulateRoleAssignmentsAndConvert(ADGroup group, IConfigurationSession configurationSession)
		{
			Result<ExchangeRoleAssignment>[] roleAssignmentResults = null;
			if (RoleGroup.ContainsRoleAssignments(group))
			{
				return new RoleGroup(group, roleAssignmentResults);
			}
			SharedConfiguration sharedConfiguration = SharedConfiguration.GetSharedConfiguration(group.OrganizationId);
			RoleGroup roleGroup;
			if (sharedConfiguration != null)
			{
				if (group.RoleGroupType == RoleGroupType.PartnerLinked)
				{
					roleGroup = new RoleGroup(group, null);
				}
				else
				{
					ADObjectId sharedConfiguration2 = null;
					ADObjectId[] sharedRoleGroupIds;
					if (group.IsModified(ADObjectSchema.Name))
					{
						sharedRoleGroupIds = sharedConfiguration.GetSharedRoleGroupIds(new ADObjectId[]
						{
							group.OriginalId
						});
					}
					else
					{
						sharedRoleGroupIds = sharedConfiguration.GetSharedRoleGroupIds(new ADObjectId[]
						{
							group.Id
						});
					}
					if (sharedRoleGroupIds != null && sharedRoleGroupIds.Length != 0)
					{
						IConfigurationSession tenantOrTopologyConfigurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(ConsistencyMode.PartiallyConsistent, sharedConfiguration.GetSharedConfigurationSessionSettings(), 171, "PopulateRoleAssignmentsAndConvert", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\RBAC\\RoleGroup\\RoleGroupCommon.cs");
						roleAssignmentResults = tenantOrTopologyConfigurationSession.FindRoleAssignmentsByUserIds(new ADObjectId[]
						{
							sharedRoleGroupIds[0]
						}, false);
						sharedConfiguration2 = sharedConfiguration.SharedConfigurationCU.Id;
					}
					roleGroup = new RoleGroup(group, roleAssignmentResults);
					roleGroup.SharedConfiguration = sharedConfiguration2;
				}
			}
			else
			{
				configurationSession.SessionSettings.IsSharedConfigChecked = true;
				roleAssignmentResults = configurationSession.FindRoleAssignmentsByUserIds(new ADObjectId[]
				{
					group.Id
				}, false);
				roleGroup = new RoleGroup(group, roleAssignmentResults);
			}
			return roleGroup;
		}

		// Token: 0x060039C8 RID: 14792 RVA: 0x000F405C File Offset: 0x000F225C
		internal static string NamesFromObjects(IEnumerable objects)
		{
			if (objects == null)
			{
				return string.Empty;
			}
			StringBuilder stringBuilder = new StringBuilder();
			bool flag = false;
			MultiValuedPropertyBase multiValuedPropertyBase = objects as MultiValuedPropertyBase;
			if (multiValuedPropertyBase != null && multiValuedPropertyBase.Count == 0)
			{
				return RoleGroupCommon.DumpMultiValuedPropertyWithChanges(multiValuedPropertyBase);
			}
			foreach (object obj in objects)
			{
				if (flag)
				{
					stringBuilder.Append(", ");
				}
				flag = true;
				if (obj is ADObject)
				{
					stringBuilder.Append(((ADObject)obj).Name);
				}
				else if (obj is ADObjectId)
				{
					stringBuilder.Append(((ADObjectId)obj).Name);
				}
				else
				{
					if (!(obj is SecurityPrincipalIdParameter))
					{
						throw new ArgumentException("objects");
					}
					stringBuilder.Append(((SecurityPrincipalIdParameter)obj).ToString());
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060039C9 RID: 14793 RVA: 0x000F4150 File Offset: 0x000F2350
		private static string DumpMultiValuedPropertyWithChanges(MultiValuedPropertyBase mvp)
		{
			StringBuilder stringBuilder = new StringBuilder();
			object[] added = mvp.Added;
			object[] removed = mvp.Removed;
			if (added.Length > 0)
			{
				stringBuilder.Append(MultiValuedProperty<string>.AddKeys[0]);
				stringBuilder.Append("=");
				stringBuilder.Append(RoleGroupCommon.NamesFromObjects(added));
				stringBuilder.Append(";");
			}
			if (removed.Length > 0)
			{
				stringBuilder.Append(MultiValuedProperty<string>.RemoveKeys[0]);
				stringBuilder.Append("=");
				stringBuilder.Append(RoleGroupCommon.NamesFromObjects(removed));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060039CA RID: 14794 RVA: 0x000F41E0 File Offset: 0x000F23E0
		internal static void ValidateExecutingUserHasGroupManagementRights(ADObjectId user, ADGroup group, ExchangeRunspaceConfiguration exchangeRunspaceConfiguration, Task.ErrorLoggerDelegate writeError)
		{
			List<ADObjectId> list = new List<ADObjectId>();
			if (exchangeRunspaceConfiguration != null)
			{
				list.AddRange(exchangeRunspaceConfiguration.GetNestedSecurityGroupMembership());
			}
			if (user != null && !list.Contains(user))
			{
				list.Add(user);
			}
			RecipientTaskHelper.ValidateUserIsGroupManager((list != null) ? list.ToArray() : new ADObjectId[0], group, writeError, false, null);
		}

		// Token: 0x060039CB RID: 14795 RVA: 0x000F4230 File Offset: 0x000F2430
		internal static void StampWellKnownObjectGuid(IConfigurationSession writableConfigSession, OrganizationId organizationId, string distinguishedName, Guid wellKnownObjectGuid)
		{
			bool flag = false;
			ExchangeConfigurationUnit exchangeConfigurationUnit = writableConfigSession.Read<ExchangeConfigurationUnit>(organizationId.ConfigurationUnit);
			DNWithBinary dnwithBinary = DirectoryCommon.FindWellKnownObjectEntry(exchangeConfigurationUnit.OtherWellKnownObjects, wellKnownObjectGuid);
			if (dnwithBinary != null)
			{
				if (dnwithBinary.DistinguishedName.Equals(distinguishedName, StringComparison.OrdinalIgnoreCase))
				{
					flag = true;
				}
				else
				{
					exchangeConfigurationUnit.OtherWellKnownObjects.Remove(dnwithBinary);
				}
			}
			if (!flag)
			{
				dnwithBinary = new DNWithBinary(distinguishedName, wellKnownObjectGuid.ToByteArray());
				exchangeConfigurationUnit.OtherWellKnownObjects.Add(dnwithBinary);
				writableConfigSession.Save(exchangeConfigurationUnit);
			}
		}

		// Token: 0x060039CC RID: 14796 RVA: 0x000F42C4 File Offset: 0x000F24C4
		internal static bool IsPrecannedRoleGroup(ADGroup roleGroup, IConfigurationSession configurationSession, params Guid[] cannedRoleGroups)
		{
			if (roleGroup == null)
			{
				throw new ArgumentNullException("roleGroup");
			}
			if (configurationSession == null)
			{
				throw new ArgumentNullException("configurationSession");
			}
			ADSessionSettings sessionSettings = ADSessionSettings.FromAllTenantsOrRootOrgAutoDetect(roleGroup.OrganizationId);
			IConfigurationSession tenantOrTopologyConfigurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(configurationSession.DomainController, true, configurationSession.ConsistencyMode, configurationSession.NetworkCredential, sessionSettings, 351, "IsPrecannedRoleGroup", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\RBAC\\RoleGroup\\RoleGroupCommon.cs");
			List<DNWithBinary> source = roleGroup.OrganizationId.Equals(OrganizationId.ForestWideOrgId) ? tenantOrTopologyConfigurationSession.GetExchangeConfigurationContainer().OtherWellKnownObjects.ToList<DNWithBinary>() : tenantOrTopologyConfigurationSession.Read<ExchangeConfigurationUnit>(roleGroup.OrganizationId.ConfigurationUnit).OtherWellKnownObjects.ToList<DNWithBinary>();
			List<RoleGroupDefinition> list;
			if (cannedRoleGroups != null && cannedRoleGroups.Length > 0)
			{
				list = (from rgDefinition in RoleGroupDefinitions.RoleGroups
				where cannedRoleGroups.Contains(rgDefinition.RoleGroupGuid)
				select rgDefinition).ToList<RoleGroupDefinition>();
			}
			else
			{
				list = RoleGroupDefinitions.RoleGroups.ToList<RoleGroupDefinition>();
			}
			foreach (RoleGroupDefinition roleGroupDefinition in list)
			{
				DNWithBinary value = new DNWithBinary(roleGroup.Id.DistinguishedName, roleGroupDefinition.RoleGroupGuid.ToByteArray());
				if (source.Contains(value))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x04002637 RID: 9783
		internal static RecipientTypeDetails[] OwnerRecipientTypeDetails = new RecipientTypeDetails[]
		{
			RecipientTypeDetails.UserMailbox,
			RecipientTypeDetails.LegacyMailbox,
			RecipientTypeDetails.SharedMailbox,
			RecipientTypeDetails.TeamMailbox,
			RecipientTypeDetails.MailUser,
			RecipientTypeDetails.LinkedMailbox,
			RecipientTypeDetails.User,
			RecipientTypeDetails.UniversalSecurityGroup,
			RecipientTypeDetails.RoleGroup,
			RecipientTypeDetails.MailUniversalSecurityGroup,
			RecipientTypeDetails.LinkedUser
		};

		// Token: 0x04002638 RID: 9784
		private static PropertyDefinition[] RoleAssignments = new PropertyDefinition[]
		{
			RoleGroupSchema.RoleAssignments
		};
	}
}
