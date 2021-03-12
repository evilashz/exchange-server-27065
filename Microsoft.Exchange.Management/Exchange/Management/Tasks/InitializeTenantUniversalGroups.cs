using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Management.Automation;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Deployment;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x020002CA RID: 714
	[Cmdlet("Initialize", "TenantUniversalGroups", SupportsShouldProcess = true)]
	public sealed class InitializeTenantUniversalGroups : SetupTaskBase
	{
		// Token: 0x17000767 RID: 1895
		// (get) Token: 0x06001919 RID: 6425 RVA: 0x000702AC File Offset: 0x0006E4AC
		// (set) Token: 0x0600191A RID: 6426 RVA: 0x000702B4 File Offset: 0x0006E4B4
		[Parameter(Mandatory = true)]
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

		// Token: 0x17000768 RID: 1896
		// (get) Token: 0x0600191B RID: 6427 RVA: 0x000702BD File Offset: 0x0006E4BD
		// (set) Token: 0x0600191C RID: 6428 RVA: 0x000702D4 File Offset: 0x0006E4D4
		[Parameter(Mandatory = true)]
		public ServicePlan ServicePlanSettings
		{
			get
			{
				return (ServicePlan)base.Fields["ServicePlanSettings"];
			}
			set
			{
				base.Fields["ServicePlanSettings"] = value;
			}
		}

		// Token: 0x17000769 RID: 1897
		// (get) Token: 0x0600191D RID: 6429 RVA: 0x000702E7 File Offset: 0x0006E4E7
		// (set) Token: 0x0600191E RID: 6430 RVA: 0x000702FE File Offset: 0x0006E4FE
		[Parameter(Mandatory = false)]
		public bool IsDehydrated
		{
			get
			{
				return (bool)base.Fields["IsDehydrated"];
			}
			set
			{
				base.Fields["IsDehydrated"] = value;
			}
		}

		// Token: 0x0600191F RID: 6431 RVA: 0x00070316 File Offset: 0x0006E516
		protected override bool IsKnownException(Exception exception)
		{
			return base.IsKnownException(exception) || exception is UnwillingToPerformException;
		}

		// Token: 0x06001920 RID: 6432 RVA: 0x00070330 File Offset: 0x0006E530
		protected override void InternalBeginProcessing()
		{
			TaskLogger.LogEnter();
			base.InternalBeginProcessing();
			this.configurationSession.DomainController = this.organization.OriginatingServer;
			this.orgDomainRecipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(this.organization.OriginatingServer, false, ConsistencyMode.PartiallyConsistent, ADSessionSettings.FromAllTenantsObjectId(this.organization.Id), 126, "InternalBeginProcessing", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\DirectorySetup\\InitializeTenantUniversalGroups.cs");
			this.domainConfigurationSession.DomainController = this.organization.OriginatingServer;
			this.rootDomain = null;
			this.rootDomainRecipientSession = null;
			this.configurationUnit = this.configurationSession.Read<ExchangeConfigurationUnit>(this.organization.ConfigurationUnit);
			if (this.configurationUnit == null)
			{
				base.ThrowTerminatingError(new ArgumentException(Strings.ErrorOrganizationNotFound(this.organization.ConfigurationUnit.ToString())), ErrorCategory.InvalidData, null);
			}
			base.LogReadObject(this.configurationUnit);
			TaskLogger.LogExit();
		}

		// Token: 0x06001921 RID: 6433 RVA: 0x00070418 File Offset: 0x0006E618
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			RoleGroupCollection roleGroupsToCreate = this.GetRoleGroupsToCreate();
			this.CreateAndValidateRoleGroups(this.organization.Id, roleGroupsToCreate);
			this.RemoveInvalidRoleGroups(this.GetInvalidRoleGroups());
			TaskLogger.LogExit();
		}

		// Token: 0x06001922 RID: 6434 RVA: 0x00070454 File Offset: 0x0006E654
		private ADGroup CreateGroup(OrganizationId orgId, ADObjectId usgContainerId, string groupName, int groupId, Guid wkGuid, string groupDescription, GroupTypeFlags groupType, List<ADObjectId> manageBy)
		{
			ADGroup adgroup = null;
			DNWithBinary dnwithBinary = DirectoryCommon.FindWellKnownObjectEntry(this.configurationUnit.OtherWellKnownObjects, wkGuid);
			if (null != dnwithBinary)
			{
				ADObjectId adobjectId = new ADObjectId(dnwithBinary.DistinguishedName);
				if (adobjectId.IsDeleted)
				{
					base.WriteError(new InvalidWKObjectException(dnwithBinary.ToString(), orgId.ConfigurationUnit.DistinguishedName), ErrorCategory.InvalidData, null);
				}
				ADRecipient adrecipient = this.orgDomainRecipientSession.Read(adobjectId);
				if (adrecipient == null)
				{
					base.WriteError(new InvalidWKObjectException(dnwithBinary.ToString(), orgId.ConfigurationUnit.DistinguishedName), ErrorCategory.InvalidData, null);
				}
				base.LogReadObject(adrecipient);
				if (adrecipient.RecipientType != RecipientType.Group)
				{
					base.WriteError(new InvalidWKObjectTargetException(wkGuid.ToString(), orgId.ConfigurationUnit.ToString(), adgroup.Id.DistinguishedName, groupType.ToString()), ErrorCategory.InvalidData, null);
				}
				adgroup = (adrecipient as ADGroup);
				InitializeExchangeUniversalGroups.UpgradeRoleGroupLocalization(adgroup, groupId, groupDescription, this.orgDomainRecipientSession);
				if ((adgroup.GroupType & groupType) != groupType)
				{
					base.WriteVerbose(Strings.InfoChangingGroupType(adgroup.Id.DistinguishedName, groupType.ToString()));
					adgroup.GroupType = groupType;
					adgroup.RecipientTypeDetails = RecipientTypeDetails.RoleGroup;
					this.orgDomainRecipientSession.Save(adgroup);
					base.LogWriteObject(adgroup);
				}
				else
				{
					base.WriteVerbose(Strings.InfoGroupAlreadyPresent(adgroup.Id.DistinguishedName));
				}
				return adgroup;
			}
			ADGroup adgroup2 = null;
			try
			{
				string groupSam = groupName + "{" + Guid.NewGuid().ToString("N") + "}";
				adgroup2 = InitializeExchangeUniversalGroups.CreateUniqueRoleGroup(this.orgDomainRecipientSession, orgId.OrganizationalUnit.DomainId, usgContainerId, groupName, groupId, groupDescription, groupSam, manageBy, orgId);
				dnwithBinary = this.CreateWKGuid(adgroup2.Id, wkGuid);
			}
			finally
			{
				if (adgroup2 == null && dnwithBinary != null)
				{
					this.configurationUnit.OtherWellKnownObjects.Remove(dnwithBinary);
					this.configurationSession.Save(this.configurationUnit);
					base.LogWriteObject(this.configurationUnit);
				}
				else if (adgroup2 != null && dnwithBinary == null)
				{
					this.orgDomainRecipientSession.Delete(adgroup2);
					base.LogWriteObject(adgroup2);
					adgroup2 = null;
				}
			}
			return adgroup2;
		}

		// Token: 0x06001923 RID: 6435 RVA: 0x0007068C File Offset: 0x0006E88C
		private ADOrganizationalUnit CreateHostedExchangeSGContainer(IConfigurationSession session, OrganizationId orgId)
		{
			ADOrganizationalUnit adorganizationalUnit = this.FindHostedExchangeSGContainer(session, orgId);
			if (adorganizationalUnit == null)
			{
				ADOrganizationalUnit adorganizationalUnit2 = new ADOrganizationalUnit();
				adorganizationalUnit2.SetId(orgId.OrganizationalUnit.GetChildId("OU", "Hosted Organization Security Groups"));
				ADObject adobject = adorganizationalUnit2;
				adobject.OrganizationId = orgId;
				session.Save(adorganizationalUnit2);
				adorganizationalUnit = this.FindHostedExchangeSGContainer(session, orgId);
			}
			base.LogReadObject(adorganizationalUnit);
			return adorganizationalUnit;
		}

		// Token: 0x06001924 RID: 6436 RVA: 0x000706E8 File Offset: 0x0006E8E8
		private ADOrganizationalUnit FindHostedExchangeSGContainer(IConfigurationSession session, OrganizationId orgId)
		{
			ADOrganizationalUnit[] array = session.Find<ADOrganizationalUnit>(orgId.OrganizationalUnit, QueryScope.OneLevel, new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.Name, "Hosted Organization Security Groups"), null, 1);
			if (array == null || array.Length == 0)
			{
				return null;
			}
			return array[0];
		}

		// Token: 0x06001925 RID: 6437 RVA: 0x00070724 File Offset: 0x0006E924
		private DNWithBinary CreateWKGuid(ADObjectId dn, Guid wkGuid)
		{
			DNWithBinary dnwithBinary = new DNWithBinary(dn.DistinguishedName, wkGuid.ToByteArray());
			this.configurationUnit.OtherWellKnownObjects.Add(dnwithBinary);
			this.configurationSession.Save(this.configurationUnit);
			base.LogWriteObject(this.configurationUnit);
			TaskLogger.Trace(Strings.InfoCreatedWKGuid(wkGuid.ToString(), dn.DistinguishedName, this.configurationUnit.DistinguishedName));
			return dnwithBinary;
		}

		// Token: 0x1700076A RID: 1898
		// (get) Token: 0x06001926 RID: 6438 RVA: 0x0007079B File Offset: 0x0006E99B
		internal static string ExchangePasswordSettingsSG
		{
			get
			{
				return "Hosted Organization Password Settings";
			}
		}

		// Token: 0x1700076B RID: 1899
		// (get) Token: 0x06001927 RID: 6439 RVA: 0x000707A2 File Offset: 0x0006E9A2
		internal static string ExchangeSGContainerName
		{
			get
			{
				return "Hosted Organization Security Groups";
			}
		}

		// Token: 0x06001928 RID: 6440 RVA: 0x000707A9 File Offset: 0x0006E9A9
		private static bool IsDehydrateable(RoleGroupDefinition roleGroupDefinition)
		{
			return false;
		}

		// Token: 0x06001929 RID: 6441 RVA: 0x000707E8 File Offset: 0x0006E9E8
		private RoleGroupCollection GetRoleGroupsToCreate()
		{
			bool flag = Datacenter.IsPartnerHostedOnly(false);
			RoleGroupCollection roleGroupCollection = new RoleGroupCollection();
			List<string> enabledRoleGroupRoleAssignmentFeatures = this.ServicePlanSettings.Organization.GetEnabledRoleGroupRoleAssignmentFeatures();
			using (List<RoleGroupDefinition>.Enumerator enumerator = RoleGroupDefinitions.RoleGroups.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					RoleGroupDefinition roleGroupDefinition = enumerator.Current;
					RoleGroupRoleMapping roleGroupRoleMapping;
					if (flag)
					{
						roleGroupRoleMapping = HostedTenant_RoleGroupDefinition.Definition.FirstOrDefault((RoleGroupRoleMapping x) => x.RoleGroup.Equals(roleGroupDefinition.Name, StringComparison.OrdinalIgnoreCase));
					}
					else
					{
						roleGroupRoleMapping = Tenant_RoleGroupDefinition.Definition.FirstOrDefault((RoleGroupRoleMapping x) => x.RoleGroup.Equals(roleGroupDefinition.Name, StringComparison.OrdinalIgnoreCase));
					}
					if (roleGroupRoleMapping != null && roleGroupRoleMapping.GetRolesAssignments(enabledRoleGroupRoleAssignmentFeatures).Count > 0 && (flag || !this.ServicePlanSettings.Organization.ShareableConfigurationEnabled || !this.IsDehydrated || !InitializeTenantUniversalGroups.IsDehydrateable(roleGroupDefinition)))
					{
						roleGroupCollection.Add(new RoleGroupDefinition(roleGroupDefinition));
					}
				}
			}
			return roleGroupCollection;
		}

		// Token: 0x0600192A RID: 6442 RVA: 0x0007093C File Offset: 0x0006EB3C
		private RoleGroupCollection GetInvalidRoleGroups()
		{
			bool flag = Datacenter.IsPartnerHostedOnly(false);
			RoleGroupCollection roleGroupCollection = new RoleGroupCollection();
			List<string> enabledRoleGroupRoleAssignmentFeatures = this.ServicePlanSettings.Organization.GetEnabledRoleGroupRoleAssignmentFeatures();
			using (List<RoleGroupDefinition>.Enumerator enumerator = RoleGroupDefinitions.RoleGroups.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					RoleGroupDefinition roleGroupDefinition = enumerator.Current;
					RoleGroupRoleMapping roleGroupRoleMapping;
					if (flag)
					{
						roleGroupRoleMapping = HostedTenant_RoleGroupDefinition.Definition.FirstOrDefault((RoleGroupRoleMapping x) => x.RoleGroup.Equals(roleGroupDefinition.Name, StringComparison.OrdinalIgnoreCase));
					}
					else
					{
						roleGroupRoleMapping = Tenant_RoleGroupDefinition.Definition.FirstOrDefault((RoleGroupRoleMapping x) => x.RoleGroup.Equals(roleGroupDefinition.Name, StringComparison.OrdinalIgnoreCase));
					}
					if (roleGroupRoleMapping != null && roleGroupRoleMapping.GetRolesAssignments(enabledRoleGroupRoleAssignmentFeatures).Count == 0)
					{
						roleGroupCollection.Add(new RoleGroupDefinition(roleGroupDefinition));
					}
				}
			}
			return roleGroupCollection;
		}

		// Token: 0x0600192B RID: 6443 RVA: 0x00070A50 File Offset: 0x0006EC50
		private void CreateAndValidateRoleGroups(ADObjectId usgContainerId, RoleGroupCollection roleGroups)
		{
			RoleGroupDefinition roleGroupDefinition = roleGroups.FirstOrDefault((RoleGroupDefinition x) => x.RoleGroupGuid.Equals(RoleGroup.OrganizationManagement_InitInfo.WellKnownGuid));
			if (roleGroupDefinition == null)
			{
				base.WriteError(new ExOrgAdminSGroupNotFoundException(WellKnownGuid.EoaWkGuid), ErrorCategory.ObjectNotFound, null);
			}
			roleGroups.Remove(roleGroupDefinition);
			roleGroupDefinition.ADGroup = this.CreateRoleGroup(usgContainerId, roleGroupDefinition, null);
			if (!roleGroupDefinition.ADGroup.ManagedBy.Contains(roleGroupDefinition.ADGroup.Id))
			{
				roleGroupDefinition.ADGroup.ManagedBy.Add(roleGroupDefinition.ADGroup.Id);
				this.orgDomainRecipientSession.Save(roleGroupDefinition.ADGroup);
				base.LogWriteObject(roleGroupDefinition.ADGroup);
			}
			List<ADObjectId> manageBy = new List<ADObjectId>
			{
				roleGroupDefinition.ADGroup.Id
			};
			foreach (RoleGroupDefinition roleGroupDefinition2 in roleGroups)
			{
				roleGroupDefinition2.ADGroup = this.CreateRoleGroup(usgContainerId, roleGroupDefinition2, manageBy);
			}
			roleGroups.Add(roleGroupDefinition);
		}

		// Token: 0x0600192C RID: 6444 RVA: 0x00070B70 File Offset: 0x0006ED70
		private ADGroup CreateRoleGroup(ADObjectId usgContainerId, RoleGroupDefinition roleGroup, List<ADObjectId> manageBy)
		{
			ADGroup adgroup = this.CreateGroup(this.organization.OrganizationId, usgContainerId, roleGroup.Name, roleGroup.Id, roleGroup.RoleGroupGuid, roleGroup.Description, GroupTypeFlags.Universal | GroupTypeFlags.SecurityEnabled, manageBy);
			if (adgroup == null)
			{
				base.WriteError(roleGroup.GuidNotFoundException, ErrorCategory.ObjectNotFound, null);
			}
			base.LogWriteObject(adgroup);
			return adgroup;
		}

		// Token: 0x0600192D RID: 6445 RVA: 0x00070BC8 File Offset: 0x0006EDC8
		private void RemoveInvalidRoleGroups(RoleGroupCollection roleGroups)
		{
			foreach (RoleGroupDefinition roleGroupDefinition in roleGroups)
			{
				DNWithBinary dnwithBinary = DirectoryCommon.FindWellKnownObjectEntry(this.configurationUnit.OtherWellKnownObjects, roleGroupDefinition.RoleGroupGuid);
				if (null != dnwithBinary)
				{
					this.configurationUnit.OtherWellKnownObjects.Remove(dnwithBinary);
					this.configurationSession.Save(this.configurationUnit);
					base.LogWriteObject(this.configurationUnit);
					ADObjectId adobjectId = new ADObjectId(dnwithBinary.DistinguishedName);
					foreach (ExchangeRoleAssignment exchangeRoleAssignment in this.configurationSession.FindPaged<ExchangeRoleAssignment>(base.OrgContainerId.GetDescendantId(ExchangeRoleAssignment.RdnContainer), QueryScope.OneLevel, new ComparisonFilter(ComparisonOperator.Equal, ExchangeRoleAssignmentSchema.User, adobjectId), null, 0))
					{
						this.configurationSession.Delete(exchangeRoleAssignment);
						base.LogWriteObject(exchangeRoleAssignment);
					}
					if (!adobjectId.IsDeleted)
					{
						ADRecipient adrecipient = this.orgDomainRecipientSession.Read(adobjectId);
						base.LogReadObject(adrecipient);
						this.orgDomainRecipientSession.Delete(adrecipient);
						base.LogWriteObject(adrecipient);
					}
				}
			}
		}

		// Token: 0x0600192E RID: 6446 RVA: 0x00070D30 File Offset: 0x0006EF30
		[Conditional("DEBUG")]
		private void ValidateCreatedRoleGroups(RoleGroupCollection roleGroups)
		{
			foreach (RoleGroupDefinition roleGroupDefinition in roleGroups)
			{
				base.ResolveHostedExchangeGroupGuid<ADGroup>(roleGroupDefinition.RoleGroupGuid, this.organization.OrganizationId);
			}
		}

		// Token: 0x04000AF7 RID: 2807
		private const GroupTypeFlags GSG_GROUPTYPE_FLAGS = GroupTypeFlags.Global | GroupTypeFlags.SecurityEnabled;

		// Token: 0x04000AF8 RID: 2808
		private const GroupTypeFlags USG_GROUPTYPE_FLAGS = GroupTypeFlags.Universal | GroupTypeFlags.SecurityEnabled;

		// Token: 0x04000AF9 RID: 2809
		private const string exchangeSGContainerName = "Hosted Organization Security Groups";

		// Token: 0x04000AFA RID: 2810
		private const string exchangeAllMailboxesDescription = "This group contains all the user mailboxes that live in the organization. All members of the group have self-service role assignment. This group should not be deleted.";

		// Token: 0x04000AFB RID: 2811
		private const string exchangePasswordSettingsSG = "Hosted Organization Password Settings";

		// Token: 0x04000AFC RID: 2812
		private const string exchangePasswordSettingsDescription = "This group contains all the user mailboxes that live in the organization. All members of the group have password settings object assignment. This group should not be deleted.";

		// Token: 0x04000AFD RID: 2813
		private ExchangeConfigurationUnit configurationUnit;

		// Token: 0x04000AFE RID: 2814
		private IRecipientSession orgDomainRecipientSession;
	}
}
