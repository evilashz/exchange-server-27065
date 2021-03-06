using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.DirectoryServices.Protocols;
using System.Linq;
using System.Management.Automation;
using System.Security.AccessControl;
using System.Security.Principal;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x020002C6 RID: 710
	[Cmdlet("initialize", "ExchangeUniversalGroups", SupportsShouldProcess = true)]
	public sealed class InitializeExchangeUniversalGroups : SetupTaskBase
	{
		// Token: 0x17000763 RID: 1891
		// (get) Token: 0x060018EC RID: 6380 RVA: 0x0006E498 File Offset: 0x0006C698
		// (set) Token: 0x060018ED RID: 6381 RVA: 0x0006E4AF File Offset: 0x0006C6AF
		[Parameter(Mandatory = false)]
		public bool? ActiveDirectorySplitPermissions
		{
			get
			{
				return (bool?)base.Fields["ActiveDirectorySplitPermissions"];
			}
			set
			{
				base.Fields["ActiveDirectorySplitPermissions"] = value;
			}
		}

		// Token: 0x060018EE RID: 6382 RVA: 0x0006E4C7 File Offset: 0x0006C6C7
		protected override bool IsKnownException(Exception exception)
		{
			return base.IsKnownException(exception) || exception is UnwillingToPerformException;
		}

		// Token: 0x060018EF RID: 6383 RVA: 0x0006E4E0 File Offset: 0x0006C6E0
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			RoleGroupCollection roleGroupCollection = InitializeExchangeUniversalGroups.RoleGroupsToCreate();
			bool flag = false;
			foreach (RoleGroupDefinition roleGroupDefinition in roleGroupCollection)
			{
				roleGroupDefinition.ADGroup = base.ResolveExchangeGroupGuid<ADGroup>(roleGroupDefinition.RoleGroupGuid);
				if (roleGroupDefinition.ADGroup == null)
				{
					flag = true;
				}
			}
			ADGroup adgroup = base.ResolveExchangeGroupGuid<ADGroup>(WellKnownGuid.ExSWkGuid);
			ADGroup adgroup2 = base.ResolveExchangeGroupGuid<ADGroup>(WellKnownGuid.E3iWkGuid);
			ADGroup adgroup3 = base.ResolveExchangeGroupGuid<ADGroup>(WellKnownGuid.EtsWkGuid);
			ADGroup adgroup4 = base.ResolveExchangeGroupGuid<ADGroup>(WellKnownGuid.EwpWkGuid);
			this.adSplitPermissionMode = false;
			if (this.ActiveDirectorySplitPermissions != null)
			{
				if (this.ActiveDirectorySplitPermissions.Value)
				{
					this.adSplitPermissionMode = true;
				}
				else
				{
					this.adSplitPermissionMode = false;
				}
			}
			else if (adgroup3 == null)
			{
				this.adSplitPermissionMode = false;
			}
			else if (adgroup4 == null)
			{
				this.adSplitPermissionMode = false;
			}
			else if (!adgroup4.Members.Contains(adgroup3.Id))
			{
				this.adSplitPermissionMode = true;
			}
			else
			{
				this.adSplitPermissionMode = false;
			}
			ADOrganizationalUnit adorganizationalUnit = this.FindExchangeUSGContainer("Microsoft Exchange Protected Groups", this.domainConfigurationSession, this.rootDomain.Id);
			if (this.adSplitPermissionMode && adorganizationalUnit == null)
			{
				adorganizationalUnit = this.CreateExchangeUSGContainer("Microsoft Exchange Protected Groups", this.domainConfigurationSession, this.rootDomain.Id);
				if (adorganizationalUnit == null)
				{
					base.WriteError(new USGContainerNotFoundException("Microsoft Exchange Protected Groups", this.rootDomain.DistinguishedName), ErrorCategory.ObjectNotFound, null);
				}
			}
			ADOrganizationalUnit adorganizationalUnit2 = null;
			if (flag || adgroup == null || adgroup2 == null || adgroup3 == null || (!this.adSplitPermissionMode && adgroup4 == null))
			{
				adorganizationalUnit2 = this.CreateExchangeUSGContainer("Microsoft Exchange Security Groups", this.domainConfigurationSession, this.rootDomain.Id);
				if (adorganizationalUnit2 == null)
				{
					base.WriteError(new USGContainerNotFoundException("Microsoft Exchange Security Groups", this.rootDomain.DistinguishedName), ErrorCategory.ObjectNotFound, null);
				}
			}
			else
			{
				adorganizationalUnit2 = this.FindExchangeUSGContainer("Microsoft Exchange Security Groups", this.domainConfigurationSession, this.rootDomain.Id);
			}
			this.CreateAndValidateRoleGroups(adorganizationalUnit2, roleGroupCollection);
			this.CreateGroup(adorganizationalUnit2, "Exchange Servers", 0, WellKnownGuid.ExSWkGuid, Strings.ExchangeServersUSGDescription);
			this.CreateGroup(adorganizationalUnit2, "Exchange Trusted Subsystem", 0, WellKnownGuid.EtsWkGuid, Strings.ExchangeTrustedSubsystemDescription);
			this.CreateGroup(adorganizationalUnit2, "Managed Availability Servers", 0, WellKnownGuid.MaSWkGuid, Strings.ManagedAvailabilityServersUSGDescription);
			if (this.adSplitPermissionMode)
			{
				this.CreateOrMoveEWPGroup(adgroup4, adorganizationalUnit);
			}
			else
			{
				this.CreateOrMoveEWPGroup(adgroup4, adorganizationalUnit2);
				if (adorganizationalUnit != null)
				{
					this.domainConfigurationSession.Delete(adorganizationalUnit);
					base.LogWriteObject(adorganizationalUnit);
				}
			}
			this.CreateGroup(adorganizationalUnit2, "ExchangeLegacyInterop", 0, WellKnownGuid.E3iWkGuid, Strings.ExchangeInteropUSGDescription);
			if (adgroup == null)
			{
				adgroup = base.ResolveExchangeGroupGuid<ADGroup>(WellKnownGuid.ExSWkGuid);
				if (adgroup == null)
				{
					base.WriteError(new ExSGroupNotFoundException(WellKnownGuid.ExSWkGuid), ErrorCategory.InvalidData, null);
				}
			}
			base.LogReadObject(adgroup);
			ADGroup adgroup5 = base.ResolveExchangeGroupGuid<ADGroup>(WellKnownGuid.MaSWkGuid);
			if (adgroup5 == null)
			{
				base.WriteError(new MaSGroupNotFoundException(WellKnownGuid.MaSWkGuid), ErrorCategory.InvalidData, null);
			}
			base.LogReadObject(adgroup5);
			InitializeExchangeUniversalGroups.AddMember(adgroup, this.rootDomainRecipientSession, adgroup5, new WriteVerboseDelegate(base.WriteVerbose));
			if (adgroup2 == null)
			{
				adgroup2 = base.ResolveExchangeGroupGuid<ADGroup>(WellKnownGuid.E3iWkGuid);
				if (adgroup2 == null)
				{
					base.WriteError(new E2k3InteropGroupNotFoundException(WellKnownGuid.E3iWkGuid), ErrorCategory.InvalidData, null);
				}
			}
			base.LogReadObject(adgroup2);
			bool etsExisted = adgroup3 != null;
			if (adgroup3 == null)
			{
				adgroup3 = base.ResolveExchangeGroupGuid<ADGroup>(WellKnownGuid.EtsWkGuid);
				if (adgroup3 == null)
				{
					base.WriteError(new ExTrustedSubsystemGroupNotFoundException(WellKnownGuid.EtsWkGuid), ErrorCategory.InvalidData, null);
				}
			}
			base.LogReadObject(adgroup3);
			bool ewpExisted = adgroup4 != null;
			if (adgroup4 == null)
			{
				adgroup4 = base.ResolveExchangeGroupGuid<ADGroup>(WellKnownGuid.EwpWkGuid);
				if (adgroup4 == null)
				{
					base.WriteError(new ExWindowsPermissionsGroupNotFoundException(WellKnownGuid.EwpWkGuid), ErrorCategory.InvalidData, null);
				}
			}
			base.LogReadObject(adgroup4);
			this.GrantWriteMembershipPermission(adgroup3.Sid, adorganizationalUnit2);
			this.FixExchangeTrustedSubsystemGroupMembership(adgroup3, adgroup4, adgroup, roleGroupCollection.GetADGroupByGuid(WellKnownGuid.EmaWkGuid), etsExisted, ewpExisted);
			WindowsPrincipal windowsPrincipal = new WindowsPrincipal(WindowsIdentity.GetCurrent());
			string name = windowsPrincipal.Identity.Name;
			string[] array = name.Split(new char[]
			{
				'\\'
			}, 2);
			ADCrossRef[] domainPartitions = ADForest.GetLocalForest(base.DomainController).GetDomainPartitions();
			if (domainPartitions == null || domainPartitions.Length == 0)
			{
				base.WriteError(new DomainsNotFoundException(), ErrorCategory.InvalidData, null);
			}
			List<SecurityIdentifier> list = new List<SecurityIdentifier>();
			foreach (ADCrossRef adcrossRef in domainPartitions)
			{
				Exception ex = null;
				try
				{
					this.domainConfigurationSession.DomainController = null;
					ADDomain addomain = this.domainConfigurationSession.Read<ADDomain>(adcrossRef.NCName);
					base.LogReadObject(addomain);
					SecurityIdentifier item = new SecurityIdentifier(WellKnownSidType.AccountDomainAdminsSid, addomain.Sid);
					list.Add(item);
				}
				catch (ADExternalException ex2)
				{
					ex = ex2;
				}
				catch (ADTransientException ex3)
				{
					ex = ex3;
				}
				if (ex != null)
				{
					this.WriteWarning(Strings.DomainNotReachableWarning(adcrossRef.DnsRoot[0]));
				}
			}
			this.domainConfigurationSession.DomainController = null;
			ADGroup adgroupByGuid = roleGroupCollection.GetADGroupByGuid(WellKnownGuid.EoaWkGuid);
			ADGroup adgroupByGuid2 = roleGroupCollection.GetADGroupByGuid(WellKnownGuid.EpaWkGuid);
			ActiveDirectoryAccessRule activeDirectoryAccessRule = new ActiveDirectoryAccessRule(adgroupByGuid.Sid, ActiveDirectoryRights.GenericAll, AccessControlType.Allow, ActiveDirectorySecurityInheritance.All);
			List<ActiveDirectoryAccessRule> list2 = new List<ActiveDirectoryAccessRule>();
			list2.Add(activeDirectoryAccessRule);
			Guid schemaPropertyGuid = DirectoryCommon.GetSchemaPropertyGuid(this.configurationSession, "member");
			foreach (SecurityIdentifier identity in list)
			{
				list2.Add(new ActiveDirectoryAccessRule(identity, ActiveDirectoryRights.ReadProperty | ActiveDirectoryRights.WriteProperty, AccessControlType.Allow, schemaPropertyGuid, ActiveDirectorySecurityInheritance.All));
			}
			DirectoryCommon.SetAces(new Task.TaskVerboseLoggingDelegate(base.WriteVerbose), null, adgroup, list2.ToArray());
			try
			{
				DirectoryCommon.SetAces(new Task.TaskVerboseLoggingDelegate(base.WriteVerbose), null, adgroupByGuid, new ActiveDirectoryAccessRule[]
				{
					activeDirectoryAccessRule
				});
			}
			catch (ADOperationException ex4)
			{
				this.WriteWarning(Strings.UnableToGrantFullControlOnEOA(adgroupByGuid.Id.ToString(), adgroupByGuid.Id.ToString(), ex4.Message));
			}
			if (adgroupByGuid2 != null)
			{
				DirectoryCommon.SetAces(new Task.TaskVerboseLoggingDelegate(base.WriteVerbose), null, adgroupByGuid2, new ActiveDirectoryAccessRule[]
				{
					activeDirectoryAccessRule
				});
			}
			if (adorganizationalUnit2 != null)
			{
				base.WriteVerbose(Strings.InfoSetAces(adorganizationalUnit2.Id.DistinguishedName));
				DirectoryCommon.SetAces(new Task.TaskVerboseLoggingDelegate(base.WriteVerbose), null, adorganizationalUnit2, new ActiveDirectoryAccessRule[]
				{
					activeDirectoryAccessRule
				});
			}
			DirectoryCommon.SetAces(new Task.TaskVerboseLoggingDelegate(base.WriteVerbose), null, adgroup2, new ActiveDirectoryAccessRule[]
			{
				activeDirectoryAccessRule
			});
			bool useGlobalCatalog = this.recipientSession.UseGlobalCatalog;
			this.recipientSession.UseGlobalCatalog = true;
			try
			{
				ADRecipient adrecipient = (ADRecipient)this.recipientSession.FindByAccountName<ADRecipient>(array[0], array[1]);
				if (adrecipient != null)
				{
					TaskLogger.Trace("Adding user {0} ({1}), to group {2}.", new object[]
					{
						name,
						adrecipient.DistinguishedName,
						adgroupByGuid.DistinguishedName
					});
					InitializeExchangeUniversalGroups.AddMember(adrecipient, this.rootDomainRecipientSession, adgroupByGuid, new WriteVerboseDelegate(base.WriteVerbose));
				}
				else
				{
					TaskLogger.Trace("Didn't find user {0})", new object[]
					{
						name
					});
				}
			}
			catch (ADOperationException ex5)
			{
				base.WriteVerbose(new LocalizedString(ex5.Message));
			}
			this.recipientSession.UseGlobalCatalog = useGlobalCatalog;
			TaskLogger.LogExit();
		}

		// Token: 0x060018F0 RID: 6384 RVA: 0x0006EC70 File Offset: 0x0006CE70
		protected override void InternalBeginProcessing()
		{
			TaskLogger.LogEnter();
			base.InternalBeginProcessing();
			this.configurationSession.DomainController = this.rootDomain.OriginatingServer;
			this.configContainer = this.configurationSession.Read<ConfigurationContainer>(this.configurationSession.ConfigurationNamingContext);
			if (this.configContainer == null)
			{
				base.ThrowTerminatingError(new ConfigurationContainerNotFoundException(), ErrorCategory.InvalidData, null);
			}
			base.LogReadObject(this.configContainer);
			this.exchangeConfigContainer = this.configurationSession.GetExchangeConfigurationContainer();
			if (this.exchangeConfigContainer == null)
			{
				base.ThrowTerminatingError(new MicrosoftExchangeContainerNotFoundException(), ErrorCategory.InvalidData, null);
			}
			base.LogReadObject(this.exchangeConfigContainer);
			TaskLogger.LogExit();
		}

		// Token: 0x060018F1 RID: 6385 RVA: 0x0006ED14 File Offset: 0x0006CF14
		internal static void AddMember(ADObject obj, IRecipientSession session, ADGroup destGroup, WriteVerboseDelegate writeVerbose)
		{
			if (destGroup.Members.Contains(obj.Id))
			{
				writeVerbose(Strings.InfoAlreadyIsMemberOfGroup(obj.DistinguishedName, destGroup.DistinguishedName));
				return;
			}
			destGroup.Members.Add(obj.Id);
			SetupTaskBase.Save(destGroup, session);
		}

		// Token: 0x060018F2 RID: 6386 RVA: 0x0006ED64 File Offset: 0x0006CF64
		internal static void RemoveMember(ADObject obj, IRecipientSession session, ADGroup destGroup, WriteVerboseDelegate writeVerbose)
		{
			if (destGroup.Members.Contains(obj.Id))
			{
				destGroup.Members.Remove(obj.Id);
				SetupTaskBase.Save(destGroup, session);
				return;
			}
			writeVerbose(Strings.InfoIsNotMemberOfGroup(obj.DistinguishedName, destGroup.DistinguishedName));
		}

		// Token: 0x060018F3 RID: 6387 RVA: 0x0006EDB8 File Offset: 0x0006CFB8
		private ADGroup CreateOrMoveEWPGroup(ADGroup ewp, ADOrganizationalUnit usgContainer)
		{
			if (ewp == null)
			{
				return this.CreateGroup(usgContainer, "Exchange Windows Permissions", 0, WellKnownGuid.EwpWkGuid, Strings.ExchangeWindowsPermissionsDescription);
			}
			if (!ewp.Id.IsDescendantOf(usgContainer.Id))
			{
				ewp.SetId(usgContainer.Id.GetChildId("CN", ewp.Id.Rdn.UnescapedName));
				InitializeExchangeUniversalGroups.SaveGroup(this.rootDomainRecipientSession, usgContainer.Id, ewp);
				TaskLogger.Trace(Strings.InfoMovedGroup(ewp.DistinguishedName, ewp.Id.Parent.ToDNString(), usgContainer.Id.ToDNString()));
			}
			return ewp;
		}

		// Token: 0x060018F4 RID: 6388 RVA: 0x0006EE5C File Offset: 0x0006D05C
		private ADGroup CreateGroup(ADOrganizationalUnit usgContainer, string groupName, int groupId, Guid wkGuid, string groupDescription)
		{
			return this.CreateGroup(usgContainer, groupName, groupId, wkGuid, groupDescription, GroupTypeFlags.Universal | GroupTypeFlags.SecurityEnabled, false);
		}

		// Token: 0x060018F5 RID: 6389 RVA: 0x0006EE74 File Offset: 0x0006D074
		private void FixExchangeTrustedSubsystemGroupMembership(ADGroup ets, ADGroup ewp, ADGroup exs, ADGroup ema, bool etsExisted, bool ewpExisted)
		{
			if (!ewpExisted && etsExisted)
			{
				ets.Members.Remove(exs.Id);
				ADPagedReader<Server> adpagedReader = ((ITopologyConfigurationSession)this.configurationSession).FindAllServersWithVersionNumber(Server.E14MinVersion);
				bool useGlobalCatalog = this.domainConfigurationSession.UseGlobalCatalog;
				this.domainConfigurationSession.UseGlobalCatalog = true;
				foreach (Server server in adpagedReader)
				{
					ADComputer adcomputer = ((ITopologyConfigurationSession)this.domainConfigurationSession).FindComputerByHostName(server.Fqdn);
					if (adcomputer == null)
					{
						this.WriteWarning(Strings.ErrorCannotFindComputerObjectByServerFqdnNeedManualAdd(server.Fqdn));
					}
					else if (ets.Members.Contains(adcomputer.Id))
					{
						base.WriteVerbose(Strings.InfoAlreadyIsMemberOfGroup(adcomputer.DistinguishedName, ets.DistinguishedName));
					}
					else
					{
						ets.Members.Add(adcomputer.Id);
					}
				}
				this.domainConfigurationSession.UseGlobalCatalog = useGlobalCatalog;
				SetupTaskBase.Save(ets, this.rootDomainRecipientSession);
			}
			if (this.adSplitPermissionMode)
			{
				InitializeExchangeUniversalGroups.RemoveMember(ets, this.rootDomainRecipientSession, ewp, new WriteVerboseDelegate(base.WriteVerbose));
			}
			else
			{
				InitializeExchangeUniversalGroups.AddMember(ets, this.rootDomainRecipientSession, ewp, new WriteVerboseDelegate(base.WriteVerbose));
			}
			if (ema.Members.Contains(ets.Id))
			{
				ema.Members.Remove(ets.Id);
				SetupTaskBase.Save(ema, this.rootDomainRecipientSession);
			}
		}

		// Token: 0x060018F6 RID: 6390 RVA: 0x0006EFFC File Offset: 0x0006D1FC
		private ADGroup CreateGroup(ADOrganizationalUnit usgContainer, string groupName, int groupId, Guid wkGuid, string groupDescription, GroupTypeFlags groupType, bool createAsRoleGroup)
		{
			ADRecipient adrecipient = base.ResolveExchangeGroupGuid<ADRecipient>(wkGuid);
			DNWithBinary dnwithBinary = null;
			if (adrecipient != null)
			{
				base.LogReadObject(adrecipient);
				if (adrecipient.RecipientType != RecipientType.Group)
				{
					base.WriteError(new InvalidWKObjectTargetException(wkGuid.ToString(), "CN=Microsoft Exchange,CN=Services," + this.configurationSession.ConfigurationNamingContext.DistinguishedName, adrecipient.Id.DistinguishedName, groupType.ToString()), ErrorCategory.NotSpecified, null);
				}
				ADGroup adgroup = adrecipient as ADGroup;
				base.LogReadObject(adgroup);
				if ((adgroup.GroupType & groupType) != groupType)
				{
					base.WriteError(new InvalidWKObjectTargetException(wkGuid.ToString(), "CN=Microsoft Exchange,CN=Services," + this.configurationSession.ConfigurationNamingContext.DistinguishedName, adgroup.Id.DistinguishedName, groupType.ToString()), ErrorCategory.NotSpecified, null);
				}
				if (createAsRoleGroup && adgroup.RecipientTypeDetails != RecipientTypeDetails.RoleGroup)
				{
					base.WriteError(new InvalidWKObjectTargetException(wkGuid.ToString(), "CN=Microsoft Exchange,CN=Services," + this.configurationSession.ConfigurationNamingContext.DistinguishedName, adgroup.Id.DistinguishedName, RecipientTypeDetails.RoleGroup.ToString()), ErrorCategory.NotSpecified, null);
				}
				base.WriteVerbose(Strings.InfoGroupAlreadyPresent(adgroup.Id.DistinguishedName));
				dnwithBinary = DirectoryCommon.FindWellKnownObjectEntry(this.exchangeConfigContainer.OtherWellKnownObjects, wkGuid);
				if (dnwithBinary == null)
				{
					dnwithBinary = this.CreateWKGuid(this.exchangeConfigContainer, adgroup.Id, wkGuid);
				}
				if (createAsRoleGroup)
				{
					InitializeExchangeUniversalGroups.UpgradeRoleGroupLocalization(adgroup, groupId, groupDescription, this.rootDomainRecipientSession);
				}
				return adgroup;
			}
			ADContainer adcontainer = this.exchangeConfigContainer;
			dnwithBinary = DirectoryCommon.FindWellKnownObjectEntry(adcontainer.OtherWellKnownObjects, wkGuid);
			if (dnwithBinary == null)
			{
				adcontainer = this.configContainer;
				dnwithBinary = DirectoryCommon.FindWellKnownObjectEntry(adcontainer.OtherWellKnownObjects, wkGuid);
			}
			if (dnwithBinary != null)
			{
				base.WriteError(new InvalidWKObjectException(dnwithBinary.ToString(), adcontainer.DistinguishedName), ErrorCategory.NotSpecified, null);
			}
			ADGroup adgroup2 = null;
			try
			{
				if (createAsRoleGroup)
				{
					adgroup2 = InitializeExchangeUniversalGroups.CreateUniqueRoleGroup(this.rootDomainRecipientSession, this.rootDomain.Id, usgContainer.Id, groupName, groupId, groupDescription, OrganizationId.ForestWideOrgId);
				}
				else
				{
					adgroup2 = InitializeExchangeUniversalGroups.CreateUniqueChildSG(this.rootDomainRecipientSession, this.rootDomain.Id, usgContainer.Id, groupName, groupDescription, groupType, OrganizationId.ForestWideOrgId);
				}
				dnwithBinary = this.CreateWKGuid(this.exchangeConfigContainer, adgroup2.Id, wkGuid);
			}
			finally
			{
				if (adgroup2 == null && dnwithBinary != null)
				{
					this.exchangeConfigContainer.OtherWellKnownObjects.Remove(dnwithBinary);
					this.domainConfigurationSession.Save(this.exchangeConfigContainer);
					base.LogWriteObject(this.exchangeConfigContainer);
				}
				else if (adgroup2 != null && dnwithBinary == null)
				{
					this.rootDomainRecipientSession.Delete(adgroup2);
					base.LogWriteObject(adgroup2);
					adgroup2 = null;
				}
			}
			return adgroup2;
		}

		// Token: 0x060018F7 RID: 6391 RVA: 0x0006F2D8 File Offset: 0x0006D4D8
		private ADOrganizationalUnit CreateExchangeUSGContainer(string name, IConfigurationSession session, ADObjectId domain)
		{
			ADOrganizationalUnit adorganizationalUnit = this.FindExchangeUSGContainer(name, session, domain);
			if (adorganizationalUnit == null)
			{
				ADOrganizationalUnit adorganizationalUnit2 = new ADOrganizationalUnit();
				adorganizationalUnit2.SetId(domain.GetChildId("OU", name));
				session.Save(adorganizationalUnit2);
				adorganizationalUnit = this.FindExchangeUSGContainer(name, session, domain);
			}
			base.LogReadObject(adorganizationalUnit);
			return adorganizationalUnit;
		}

		// Token: 0x060018F8 RID: 6392 RVA: 0x0006F324 File Offset: 0x0006D524
		private ADOrganizationalUnit FindExchangeUSGContainer(string name, IConfigurationSession session, ADObjectId domain)
		{
			ADOrganizationalUnit[] array = session.Find<ADOrganizationalUnit>(domain, QueryScope.OneLevel, new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.Name, name), null, 1);
			if (array == null || array.Length == 0)
			{
				return null;
			}
			return array[0];
		}

		// Token: 0x060018F9 RID: 6393 RVA: 0x0006F358 File Offset: 0x0006D558
		internal static void UpgradeRoleGroupLocalization(ADGroup group, int groupId, string groupDescription, IRecipientSession session)
		{
			if ((int)group[ADGroupSchema.RoleGroupTypeId] != 0)
			{
				return;
			}
			group[ADGroupSchema.RoleGroupTypeId] = groupId;
			MultiValuedProperty<string> multiValuedProperty = (MultiValuedProperty<string>)group[ADRecipientSchema.Description];
			if (!string.Equals((multiValuedProperty.Count > 0) ? multiValuedProperty[0] : string.Empty, groupDescription, StringComparison.Ordinal))
			{
				group.LocalizationDisabled = true;
			}
			session.Save(group);
		}

		// Token: 0x060018FA RID: 6394 RVA: 0x0006F3C8 File Offset: 0x0006D5C8
		private static string Concat(string prefix, string suffix, int maxlen)
		{
			if (suffix.Length > maxlen)
			{
				return suffix.Substring(0, maxlen);
			}
			if (prefix.Length + suffix.Length > maxlen)
			{
				prefix = prefix.Substring(0, maxlen - suffix.Length);
			}
			return prefix + suffix;
		}

		// Token: 0x060018FB RID: 6395 RVA: 0x0006F404 File Offset: 0x0006D604
		internal static string FindUniqueCN(IRecipientSession session, ADObjectId parentId, string cnOrig)
		{
			string suffix = "";
			for (int i = 1; i < 100; i++)
			{
				string text = InitializeExchangeUniversalGroups.Concat(cnOrig, suffix, 64);
				if (session.Read(parentId.GetChildId(text)) == null)
				{
					return text;
				}
				suffix = i.ToString();
			}
			return InitializeExchangeUniversalGroups.Concat(cnOrig, Guid.NewGuid().ToString("N"), 64);
		}

		// Token: 0x060018FC RID: 6396 RVA: 0x0006F464 File Offset: 0x0006D664
		internal static string FindUniqueSamAccountName(IRecipientSession session, ADObjectId dom, string samOrig)
		{
			string suffix = "";
			for (int i = 1; i < 100; i++)
			{
				string text = InitializeExchangeUniversalGroups.Concat(samOrig, suffix, 256);
				QueryFilter filter = new ComparisonFilter(ComparisonOperator.Equal, IADSecurityPrincipalSchema.SamAccountName, text);
				ADRawEntry[] array = session.Find(dom, QueryScope.SubTree, filter, null, 1);
				if (array.Length == 0)
				{
					return text;
				}
				suffix = i.ToString();
			}
			return InitializeExchangeUniversalGroups.Concat(samOrig, Guid.NewGuid().ToString("N"), 256);
		}

		// Token: 0x060018FD RID: 6397 RVA: 0x0006F4D9 File Offset: 0x0006D6D9
		internal static ADGroup CreateUniqueChildUSG(IRecipientSession session, ADObjectId dom, ADObjectId containerId, string groupNameOrig, string groupDescription, OrganizationId orgId)
		{
			return InitializeExchangeUniversalGroups.CreateUniqueChildSG(session, dom, containerId, groupNameOrig, groupDescription, GroupTypeFlags.Universal | GroupTypeFlags.SecurityEnabled, orgId);
		}

		// Token: 0x060018FE RID: 6398 RVA: 0x0006F4F0 File Offset: 0x0006D6F0
		internal static ADGroup CreateUniqueChildSG(IRecipientSession session, ADObjectId dom, ADObjectId containerId, string groupNameOrig, string groupDescription, GroupTypeFlags groupType, OrganizationId orgId)
		{
			string groupSam = InitializeExchangeUniversalGroups.FindUniqueSamAccountName(session, dom, groupNameOrig);
			return InitializeExchangeUniversalGroups.CreateUniqueChildSG(session, dom, containerId, groupNameOrig, groupDescription, groupSam, groupType, orgId);
		}

		// Token: 0x060018FF RID: 6399 RVA: 0x0006F518 File Offset: 0x0006D718
		internal static ADGroup CreateUniqueChildSG(IRecipientSession session, ADObjectId dom, ADObjectId containerId, string groupNameOrig, string groupDescription, string groupSam, GroupTypeFlags groupType, OrganizationId orgId)
		{
			string commonName = InitializeExchangeUniversalGroups.FindUniqueCN(session, containerId, groupNameOrig);
			ADGroup adgroup = new ADGroup(session, commonName, containerId, groupType);
			MultiValuedProperty<string> multiValuedProperty = new MultiValuedProperty<string>();
			multiValuedProperty.Add(groupDescription);
			adgroup[ADRecipientSchema.Description] = multiValuedProperty;
			adgroup.SamAccountName = groupSam;
			adgroup.OrganizationId = orgId;
			InitializeExchangeUniversalGroups.SaveGroup(session, containerId, adgroup);
			TaskLogger.Trace(Strings.InfoCreatedGroup(adgroup.DistinguishedName));
			return adgroup;
		}

		// Token: 0x06001900 RID: 6400 RVA: 0x0006F57C File Offset: 0x0006D77C
		private static void SaveGroup(IRecipientSession session, ADObjectId containerId, ADGroup groupToSave)
		{
			try
			{
				SetupTaskBase.Save(groupToSave, session);
			}
			catch (ADOperationException ex)
			{
				DirectoryOperationException ex2 = ex.InnerException as DirectoryOperationException;
				if (ex2 != null && ex2.Response != null && ex2.Response.ResultCode == ResultCode.UnwillingToPerform)
				{
					throw new UnwillingToPerformException(groupToSave.Name, containerId.DistinguishedName, ex);
				}
				throw;
			}
		}

		// Token: 0x06001901 RID: 6401 RVA: 0x0006F5E0 File Offset: 0x0006D7E0
		private DNWithBinary CreateWKGuid(ADContainer container, ADObjectId dn, Guid wkGuid)
		{
			DNWithBinary dnwithBinary = new DNWithBinary(dn.DistinguishedName, wkGuid.ToByteArray());
			container.OtherWellKnownObjects.Add(dnwithBinary);
			this.configurationSession.Save(container);
			TaskLogger.Trace(Strings.InfoCreatedWKGuid(wkGuid.ToString(), dn.DistinguishedName, container.DistinguishedName));
			return dnwithBinary;
		}

		// Token: 0x06001902 RID: 6402 RVA: 0x0006F63C File Offset: 0x0006D83C
		internal static ADGroup CreateUniqueRoleGroup(IRecipientSession session, ADObjectId dom, ADObjectId containerId, string groupNameOrig, int groupId, string groupDescription, OrganizationId orgId)
		{
			string groupSam = InitializeExchangeUniversalGroups.FindUniqueSamAccountName(session, dom, groupNameOrig);
			return InitializeExchangeUniversalGroups.CreateUniqueRoleGroup(session, dom, containerId, groupNameOrig, groupId, groupDescription, groupSam, null, orgId);
		}

		// Token: 0x06001903 RID: 6403 RVA: 0x0006F664 File Offset: 0x0006D864
		internal static ADGroup CreateUniqueRoleGroup(IRecipientSession session, ADObjectId dom, ADObjectId containerId, string groupNameOrig, int groupId, string groupDescription, string groupSam, List<ADObjectId> manageBy, OrganizationId orgId)
		{
			string commonName = InitializeExchangeUniversalGroups.FindUniqueCN(session, containerId, groupNameOrig);
			ADGroup adgroup = new ADGroup(session, commonName, containerId, GroupTypeFlags.Universal | GroupTypeFlags.SecurityEnabled);
			adgroup.RecipientTypeDetails = RecipientTypeDetails.RoleGroup;
			adgroup.SamAccountName = groupSam;
			adgroup.OrganizationId = orgId;
			adgroup[ADRecipientSchema.Description] = new MultiValuedProperty<string>(groupDescription);
			adgroup[ADGroupSchema.RoleGroupTypeId] = groupId;
			if (manageBy != null && manageBy.Count > 0)
			{
				adgroup.ManagedBy = new MultiValuedProperty<ADObjectId>(manageBy);
			}
			InitializeExchangeUniversalGroups.SaveGroup(session, containerId, adgroup);
			TaskLogger.Trace(Strings.InfoCreatedGroup(adgroup.DistinguishedName));
			return adgroup;
		}

		// Token: 0x06001904 RID: 6404 RVA: 0x0006F720 File Offset: 0x0006D920
		internal static RoleGroupCollection RoleGroupsToCreate()
		{
			RoleGroupCollection roleGroupCollection = new RoleGroupCollection();
			Datacenter.ExchangeSku exchangeSku = Datacenter.GetExchangeSku();
			RoleGroupRoleMapping[] definition;
			if (Datacenter.IsMicrosoftHostedOnly(false))
			{
				definition = Datacenter_RoleGroupDefinition.Definition;
			}
			else if (Datacenter.IsPartnerHostedOnly(false))
			{
				definition = Hosting_RoleGroupDefinition.Definition;
			}
			else if (Datacenter.IsDatacenterDedicated(false))
			{
				definition = Dedicated_RoleGroupDefinition.Definition;
			}
			else
			{
				definition = Enterprise_RoleGroupDefinition.Definition;
			}
			using (List<RoleGroupDefinition>.Enumerator enumerator = RoleGroupDefinitions.RoleGroups.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					RoleGroupDefinition roleGroup = enumerator.Current;
					if (roleGroup.AlwaysCreateOnSku != null && roleGroup.AlwaysCreateOnSku.Contains(exchangeSku))
					{
						roleGroupCollection.Add(new RoleGroupDefinition(roleGroup));
					}
					else
					{
						RoleGroupRoleMapping roleGroupRoleMapping = definition.FirstOrDefault((RoleGroupRoleMapping x) => x.RoleGroup.Equals(roleGroup.Name, StringComparison.OrdinalIgnoreCase));
						if (roleGroupRoleMapping != null)
						{
							roleGroupCollection.Add(new RoleGroupDefinition(roleGroup));
						}
					}
				}
			}
			return roleGroupCollection;
		}

		// Token: 0x06001905 RID: 6405 RVA: 0x0006F82C File Offset: 0x0006DA2C
		private void CreateAndValidateRoleGroups(ADOrganizationalUnit usgContainer, RoleGroupCollection roleGroups)
		{
			foreach (RoleGroupDefinition roleGroup in roleGroups)
			{
				this.CreateRoleGroup(usgContainer, roleGroup);
			}
			WindowsPrincipal windowsPrincipal = new WindowsPrincipal(WindowsIdentity.GetCurrent());
			string name = windowsPrincipal.Identity.Name;
			string[] array = name.Split(new char[]
			{
				'\\'
			}, 2);
			ADRecipient adrecipient = (ADRecipient)this.recipientSession.FindByAccountName<ADRecipient>(array[0], array[1]);
			if (adrecipient != null)
			{
				TaskLogger.Trace("Didn't find user {0})", new object[]
				{
					name
				});
			}
			ADGroup adgroup = base.ResolveExchangeGroupGuid<ADGroup>(WellKnownGuid.EoaWkGuid);
			if (adgroup == null)
			{
				base.WriteError(new ExOrgAdminSGroupNotFoundException(WellKnownGuid.EoaWkGuid), ErrorCategory.ObjectNotFound, null);
			}
			else if (adrecipient != null)
			{
				base.LogReadObject(adrecipient);
				TaskLogger.Trace("User {0} is being set as member of RoleGroup {1}", new object[]
				{
					adrecipient.DistinguishedName,
					adgroup.Name
				});
				InitializeExchangeUniversalGroups.AddMember(adrecipient, this.rootDomainRecipientSession, adgroup, new WriteVerboseDelegate(base.WriteVerbose));
			}
			foreach (RoleGroupDefinition roleGroupDefinition in roleGroups)
			{
				if (roleGroupDefinition.ADGroup == null)
				{
					roleGroupDefinition.ADGroup = base.ResolveExchangeGroupGuid<ADGroup>(roleGroupDefinition.RoleGroupGuid);
					if (roleGroupDefinition.ADGroup == null)
					{
						base.WriteError(roleGroupDefinition.GuidNotFoundException, ErrorCategory.ObjectNotFound, null);
					}
					base.LogReadObject(roleGroupDefinition.ADGroup);
				}
				if (roleGroupDefinition.ADGroup != null && !roleGroupDefinition.ADGroup.ManagedBy.Contains(adgroup.Id))
				{
					roleGroupDefinition.ADGroup.ManagedBy.Add(adgroup.Id);
					this.rootDomainRecipientSession.Save(roleGroupDefinition.ADGroup);
					base.LogWriteObject(roleGroupDefinition.ADGroup);
				}
			}
		}

		// Token: 0x06001906 RID: 6406 RVA: 0x0006FA34 File Offset: 0x0006DC34
		private void CreateRoleGroup(ADOrganizationalUnit usgContainer, RoleGroupDefinition roleGroup)
		{
			ADGroup adgroup = this.CreateGroup(usgContainer, roleGroup.Name, roleGroup.Id, roleGroup.RoleGroupGuid, roleGroup.Description, GroupTypeFlags.Universal | GroupTypeFlags.SecurityEnabled, true);
			if (adgroup == null)
			{
				base.WriteError(roleGroup.GuidNotFoundException, ErrorCategory.ObjectNotFound, null);
			}
			base.LogReadObject(adgroup);
			foreach (Guid wkg in roleGroup.E12USG)
			{
				ADGroup adgroup2 = base.ResolveExchangeGroupGuid<ADGroup>(wkg);
				if (adgroup2 != null && adgroup2.RecipientType == RecipientType.Group)
				{
					base.LogReadObject(adgroup2);
					TaskLogger.Trace("Adding old USG {0} as member of RG {1}", new object[]
					{
						adgroup2.Name,
						adgroup.Name
					});
					InitializeExchangeUniversalGroups.AddMember(adgroup2, this.rootDomainRecipientSession, adgroup, new WriteVerboseDelegate(base.WriteVerbose));
				}
			}
		}

		// Token: 0x06001907 RID: 6407 RVA: 0x0006FB1C File Offset: 0x0006DD1C
		private void GrantWriteMembershipPermission(SecurityIdentifier sid, ADOrganizationalUnit container)
		{
			List<ActiveDirectoryAccessRule> list = new List<ActiveDirectoryAccessRule>();
			List<ActiveDirectoryAccessRule> list2 = new List<ActiveDirectoryAccessRule>();
			Guid schemaPropertyGuid = DirectoryCommon.GetSchemaPropertyGuid(this.configurationSession, "member");
			Guid schemaClassGuid = DirectoryCommon.GetSchemaClassGuid(this.configurationSession, "group");
			list2.Add(new ActiveDirectoryAccessRule(sid, ActiveDirectoryRights.WriteProperty, AccessControlType.Allow, schemaPropertyGuid, ActiveDirectorySecurityInheritance.Descendents, schemaClassGuid));
			list2.Add(new ActiveDirectoryAccessRule(sid, ActiveDirectoryRights.CreateChild, AccessControlType.Allow, schemaClassGuid, ActiveDirectorySecurityInheritance.All));
			list.Add(new ActiveDirectoryAccessRule(sid, ActiveDirectoryRights.DeleteChild, AccessControlType.Allow, schemaClassGuid, ActiveDirectorySecurityInheritance.All));
			list2.Add(new ActiveDirectoryAccessRule(sid, ActiveDirectoryRights.Delete, AccessControlType.Allow, ActiveDirectorySecurityInheritance.Descendents, schemaClassGuid));
			DirectoryCommon.RemoveAces(new Task.TaskVerboseLoggingDelegate(base.WriteVerbose), null, null, container, list.ToArray());
			DirectoryCommon.SetAces(new Task.TaskVerboseLoggingDelegate(base.WriteVerbose), null, container, list2.ToArray());
		}

		// Token: 0x04000AE5 RID: 2789
		private const GroupTypeFlags USG_GROUPTYPE_FLAGS = GroupTypeFlags.Universal | GroupTypeFlags.SecurityEnabled;

		// Token: 0x04000AE6 RID: 2790
		private const GroupTypeFlags GLOBAL_SG_GROUPTYPE_FLAGS = GroupTypeFlags.Global | GroupTypeFlags.SecurityEnabled;

		// Token: 0x04000AE7 RID: 2791
		internal const string exchangeUSGContainerName = "Microsoft Exchange Security Groups";

		// Token: 0x04000AE8 RID: 2792
		internal const string exchangeProtectedUSGContainerName = "Microsoft Exchange Protected Groups";

		// Token: 0x04000AE9 RID: 2793
		private const string ExchangeTrustedSubsystem = "Exchange Trusted Subsystem";

		// Token: 0x04000AEA RID: 2794
		private const string ExchangeWindowsPermissions = "Exchange Windows Permissions";

		// Token: 0x04000AEB RID: 2795
		private const string ExchangeServers = "Exchange Servers";

		// Token: 0x04000AEC RID: 2796
		private const string ManagedAvailabilityServers = "Managed Availability Servers";

		// Token: 0x04000AED RID: 2797
		private const string ExchangeLegacyInterop = "ExchangeLegacyInterop";

		// Token: 0x04000AEE RID: 2798
		private ConfigurationContainer configContainer;

		// Token: 0x04000AEF RID: 2799
		private ExchangeConfigurationContainer exchangeConfigContainer;

		// Token: 0x04000AF0 RID: 2800
		private bool adSplitPermissionMode;
	}
}
