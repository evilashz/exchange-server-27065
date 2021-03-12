using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Management.Automation;
using System.Security.AccessControl;
using System.Security.Principal;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000167 RID: 359
	public class SetupTaskBase : Task
	{
		// Token: 0x170003AD RID: 941
		// (get) Token: 0x06000D1B RID: 3355 RVA: 0x0003C640 File Offset: 0x0003A840
		// (set) Token: 0x06000D1C RID: 3356 RVA: 0x0003C657 File Offset: 0x0003A857
		[Parameter(ValueFromPipelineByPropertyName = true, Mandatory = false)]
		public string DomainController
		{
			get
			{
				return (string)base.Fields["DomainController"];
			}
			set
			{
				base.Fields["DomainController"] = value;
			}
		}

		// Token: 0x170003AE RID: 942
		// (get) Token: 0x06000D1D RID: 3357 RVA: 0x0003C66A File Offset: 0x0003A86A
		// (set) Token: 0x06000D1E RID: 3358 RVA: 0x0003C681 File Offset: 0x0003A881
		public virtual OrganizationIdParameter Organization
		{
			get
			{
				return (OrganizationIdParameter)base.Fields["Organization"];
			}
			set
			{
				base.Fields["Organization"] = value;
			}
		}

		// Token: 0x170003AF RID: 943
		// (get) Token: 0x06000D1F RID: 3359 RVA: 0x0003C694 File Offset: 0x0003A894
		protected OrganizationId OrganizationId
		{
			get
			{
				if (this.organization != null)
				{
					return this.organization.OrganizationId;
				}
				return OrganizationId.ForestWideOrgId;
			}
		}

		// Token: 0x170003B0 RID: 944
		// (get) Token: 0x06000D20 RID: 3360 RVA: 0x0003C6AF File Offset: 0x0003A8AF
		protected ADObjectId OrgContainerId
		{
			get
			{
				if (this.organization != null)
				{
					return this.organization.ConfigurationUnit;
				}
				return this.configurationSession.GetOrgContainerId();
			}
		}

		// Token: 0x06000D21 RID: 3361 RVA: 0x0003C6D0 File Offset: 0x0003A8D0
		protected void LogReadObject(ADRawEntry obj)
		{
			base.WriteVerbose(Strings.InfoReadObjectFromDC(obj.OriginatingServer, obj.Id.DistinguishedName));
		}

		// Token: 0x06000D22 RID: 3362 RVA: 0x0003C6EE File Offset: 0x0003A8EE
		protected override bool IsKnownException(Exception exception)
		{
			return base.IsKnownException(exception) || DataAccessHelper.IsDataAccessKnownException(exception) || exception is SecurityDescriptorAccessDeniedException;
		}

		// Token: 0x06000D23 RID: 3363 RVA: 0x0003C70C File Offset: 0x0003A90C
		protected void LogWriteObject(ADObject obj)
		{
			base.WriteVerbose(Strings.InfoWroteObjectToDC(obj.OriginatingServer, obj.DistinguishedName));
		}

		// Token: 0x06000D24 RID: 3364 RVA: 0x0003C728 File Offset: 0x0003A928
		protected override void InternalBeginProcessing()
		{
			base.InternalBeginProcessing();
			this.rootOrgSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(false, ConsistencyMode.PartiallyConsistent, ADSessionSettings.FromRootOrgScopeSet(), 168, "InternalBeginProcessing", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\DirectorySetup\\SetupTaskBase.cs");
			if (ADSessionSettings.GetProcessServerSettings() != null && this.Organization == null)
			{
				this.PrepareSessionsForRootOrg();
				return;
			}
			this.PrepareSessionsForTenant();
		}

		// Token: 0x06000D25 RID: 3365 RVA: 0x0003C780 File Offset: 0x0003A980
		protected T ResolveExchangeGroupGuid<T>(Guid wkg) where T : ADObject, new()
		{
			T t = default(T);
			try
			{
				t = this.rootDomainRecipientSession.ResolveWellKnownGuid<T>(wkg, this.configurationSession.ConfigurationNamingContext);
			}
			catch (ADReferralException)
			{
			}
			if (t == null)
			{
				bool useGlobalCatalog = this.recipientSession.UseGlobalCatalog;
				this.recipientSession.UseGlobalCatalog = true;
				try
				{
					t = this.recipientSession.ResolveWellKnownGuid<T>(wkg, this.configurationSession.ConfigurationNamingContext);
				}
				finally
				{
					this.recipientSession.UseGlobalCatalog = useGlobalCatalog;
				}
			}
			if (t != null)
			{
				this.LogReadObject(t);
			}
			return t;
		}

		// Token: 0x06000D26 RID: 3366 RVA: 0x0003C82C File Offset: 0x0003AA2C
		protected T ResolveHostedExchangeGroupGuid<T>(Guid wkg, OrganizationId orgId) where T : ADObject, new()
		{
			if (null == orgId)
			{
				throw new ArgumentNullException("orgId");
			}
			if (OrganizationId.ForestWideOrgId.Equals(orgId))
			{
				throw new ArgumentOutOfRangeException("orgId");
			}
			T t = this.orgDomainRecipientSession.ResolveWellKnownGuid<T>(wkg, orgId.ConfigurationUnit);
			if (t != null)
			{
				this.LogReadObject(t);
			}
			return t;
		}

		// Token: 0x06000D27 RID: 3367 RVA: 0x0003C890 File Offset: 0x0003AA90
		internal static void Save(ADRecipient o, IRecipientSession recipientSession)
		{
			if (o == null)
			{
				throw new ArgumentNullException("o");
			}
			IRecipientSession recipientSession2 = o.Session;
			if (recipientSession2 == null)
			{
				recipientSession2 = recipientSession;
			}
			recipientSession2.Save(o);
		}

		// Token: 0x06000D28 RID: 3368 RVA: 0x0003C8C0 File Offset: 0x0003AAC0
		protected void ReplaceAddressListACEs(ADObjectId addressBookContainerRoot, SecurityIdentifier originalSid, SecurityIdentifier[] replacementSids)
		{
			AddressBookBase[] array = this.configurationSession.Find<AddressBookBase>(addressBookContainerRoot, QueryScope.SubTree, null, null, 0);
			foreach (AddressBookBase addressBookBase in array)
			{
				bool flag = false;
				RawSecurityDescriptor rawSecurityDescriptor = addressBookBase.ReadSecurityDescriptor();
				ActiveDirectorySecurity activeDirectorySecurity = new ActiveDirectorySecurity();
				byte[] array3 = new byte[rawSecurityDescriptor.BinaryLength];
				rawSecurityDescriptor.GetBinaryForm(array3, 0);
				activeDirectorySecurity.SetSecurityDescriptorBinaryForm(array3);
				AuthorizationRuleCollection accessRules = activeDirectorySecurity.GetAccessRules(true, false, typeof(SecurityIdentifier));
				foreach (object obj in accessRules)
				{
					ActiveDirectoryAccessRule activeDirectoryAccessRule = (ActiveDirectoryAccessRule)obj;
					if (activeDirectoryAccessRule.IdentityReference == originalSid)
					{
						flag = true;
						activeDirectorySecurity.RemoveAccessRuleSpecific(activeDirectoryAccessRule);
						foreach (SecurityIdentifier identity in replacementSids)
						{
							ActiveDirectoryAccessRule rule = new ActiveDirectoryAccessRule(identity, activeDirectoryAccessRule.ActiveDirectoryRights, activeDirectoryAccessRule.AccessControlType, activeDirectoryAccessRule.ObjectType, activeDirectoryAccessRule.InheritanceType, activeDirectoryAccessRule.InheritedObjectType);
							activeDirectorySecurity.AddAccessRule(rule);
						}
					}
				}
				if (flag && base.ShouldProcess(addressBookBase.DistinguishedName, Strings.InfoProcessAction(addressBookBase.DistinguishedName), null))
				{
					addressBookBase.SaveSecurityDescriptor(new RawSecurityDescriptor(activeDirectorySecurity.GetSecurityDescriptorBinaryForm(), 0));
				}
			}
		}

		// Token: 0x06000D29 RID: 3369 RVA: 0x0003CA34 File Offset: 0x0003AC34
		protected bool GetADSplitPermissionMode(ADGroup ets, ADGroup ewp)
		{
			bool result = false;
			if (ets == null)
			{
				ets = this.ResolveExchangeGroupGuid<ADGroup>(WellKnownGuid.EtsWkGuid);
				if (ets == null)
				{
					base.ThrowTerminatingError(new ExTrustedSubsystemGroupNotFoundException(WellKnownGuid.EtsWkGuid), ErrorCategory.InvalidData, null);
				}
				this.LogReadObject(ets);
			}
			if (ewp == null)
			{
				ewp = this.ResolveExchangeGroupGuid<ADGroup>(WellKnownGuid.EwpWkGuid);
				if (ewp == null)
				{
					base.ThrowTerminatingError(new ExWindowsPermissionsGroupNotFoundException(WellKnownGuid.EwpWkGuid), ErrorCategory.InvalidData, null);
				}
				this.LogReadObject(ewp);
			}
			if (!ewp.Members.Contains(ets.Id))
			{
				result = true;
			}
			return result;
		}

		// Token: 0x06000D2A RID: 3370 RVA: 0x0003CAB4 File Offset: 0x0003ACB4
		private void ResolveOrganization()
		{
			ADSessionSettings sessionSettings = ADSessionSettings.FromOrganizationIdWithoutRbacScopes(this.rootOrgId, base.CurrentOrganizationId, base.ExecutingUserOrganizationId, true);
			IConfigurationSession tenantOrTopologyConfigurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(this.DomainController, true, ConsistencyMode.PartiallyConsistent, null, sessionSettings, ConfigScopes.TenantSubTree, 395, "ResolveOrganization", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\DirectorySetup\\SetupTaskBase.cs");
			tenantOrTopologyConfigurationSession.UseConfigNC = false;
			IEnumerable<ADOrganizationalUnit> objects = this.Organization.GetObjects<ADOrganizationalUnit>(null, tenantOrTopologyConfigurationSession);
			using (IEnumerator<ADOrganizationalUnit> enumerator = objects.GetEnumerator())
			{
				if (enumerator.MoveNext())
				{
					this.organization = enumerator.Current;
					if (enumerator.MoveNext())
					{
						base.ThrowTerminatingError(new ArgumentException(Strings.ErrorOrganizationNotUnique(this.Organization.ToString())), ErrorCategory.InvalidArgument, null);
					}
				}
				else
				{
					base.ThrowTerminatingError(new ArgumentException(Strings.ErrorOrganizationNotFound(this.Organization.ToString())), ErrorCategory.InvalidArgument, null);
				}
			}
			base.CurrentOrganizationId = this.organization.OrganizationId;
		}

		// Token: 0x06000D2B RID: 3371 RVA: 0x0003CBAC File Offset: 0x0003ADAC
		private void PrepareSessionsForRootOrg()
		{
			this.recipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(false, ConsistencyMode.PartiallyConsistent, ADSessionSettings.FromRootOrgScopeSet(), 435, "PrepareSessionsForRootOrg", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\DirectorySetup\\SetupTaskBase.cs");
			this.recipientSession.UseGlobalCatalog = false;
			this.configurationSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(false, ConsistencyMode.PartiallyConsistent, ADSessionSettings.FromRootOrgScopeSet(), 443, "PrepareSessionsForRootOrg", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\DirectorySetup\\SetupTaskBase.cs");
			this.domainConfigurationSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(false, ConsistencyMode.PartiallyConsistent, ADSessionSettings.FromRootOrgScopeSet(), 448, "PrepareSessionsForRootOrg", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\DirectorySetup\\SetupTaskBase.cs");
			this.domainConfigurationSession.UseConfigNC = false;
			this.ReadRootDomainFromDc(OrganizationId.ForestWideOrgId);
			this.rootDomainRecipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(this.rootDomain.OriginatingServer, false, ConsistencyMode.PartiallyConsistent, ADSessionSettings.FromAccountPartitionWideScopeSet(this.recipientSession.SessionSettings.PartitionId), 458, "PrepareSessionsForRootOrg", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\DirectorySetup\\SetupTaskBase.cs");
		}

		// Token: 0x06000D2C RID: 3372 RVA: 0x0003CC90 File Offset: 0x0003AE90
		private void PrepareSessionsForTenant()
		{
			this.rootOrgId = ADSystemConfigurationSession.GetRootOrgContainerIdForLocalForest();
			if (this.Organization != null)
			{
				this.ResolveOrganization();
				this.LogReadObject(this.organization);
				this.orgDomainRecipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(this.organization.OriginatingServer, false, ConsistencyMode.PartiallyConsistent, ADSessionSettings.FromAllTenantsObjectId(this.organization.Id), 479, "PrepareSessionsForTenant", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\DirectorySetup\\SetupTaskBase.cs");
			}
			if (this.organization != null)
			{
				this.rootOrgId = ((null != this.organization.Id.GetPartitionId() && this.organization.Id.GetPartitionId().ForestFQDN != null) ? ADSystemConfigurationSession.GetRootOrgContainerId(this.organization.Id.GetPartitionId().ForestFQDN, null, null) : ADSystemConfigurationSession.GetRootOrgContainerIdForLocalForest());
			}
			ADSessionSettings sessionSettings = ADSessionSettings.FromOrganizationIdWithoutRbacScopes(this.rootOrgId, base.CurrentOrganizationId, base.ExecutingUserOrganizationId, false);
			this.recipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(this.DomainController, false, ConsistencyMode.PartiallyConsistent, null, sessionSettings, ConfigScopes.TenantSubTree, 503, "PrepareSessionsForTenant", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\DirectorySetup\\SetupTaskBase.cs");
			this.recipientSession.UseGlobalCatalog = false;
			this.configurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(this.DomainController, false, ConsistencyMode.PartiallyConsistent, null, sessionSettings, 514, "PrepareSessionsForTenant", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\DirectorySetup\\SetupTaskBase.cs");
			this.domainConfigurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(this.DomainController, false, ConsistencyMode.PartiallyConsistent, null, sessionSettings, 521, "PrepareSessionsForTenant", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\DirectorySetup\\SetupTaskBase.cs");
			this.domainConfigurationSession.UseConfigNC = false;
			this.ReadRootDomainFromDc(base.CurrentOrganizationId);
			this.rootDomainRecipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(this.rootDomain.OriginatingServer, false, ConsistencyMode.PartiallyConsistent, ADSessionSettings.FromAccountPartitionRootOrgScopeSet(base.CurrentOrganizationId.PartitionId), 533, "PrepareSessionsForTenant", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\DirectorySetup\\SetupTaskBase.cs");
		}

		// Token: 0x06000D2D RID: 3373 RVA: 0x0003CE54 File Offset: 0x0003B054
		private void ReadRootDomainFromDc(OrganizationId orgId)
		{
			this.rootDomain = null;
			if (orgId.Equals(OrganizationId.ForestWideOrgId))
			{
				this.rootDomain = ADForest.GetLocalForest().FindRootDomain(true);
			}
			else
			{
				this.rootDomain = ADForest.GetForest(orgId.PartitionId).FindRootDomain(true);
			}
			if (this.rootDomain == null)
			{
				base.ThrowTerminatingError(new RootDomainNotFoundException(), ErrorCategory.InvalidData, null);
			}
			this.LogReadObject(this.rootDomain);
		}

		// Token: 0x04000677 RID: 1655
		internal IRecipientSession recipientSession;

		// Token: 0x04000678 RID: 1656
		internal IRecipientSession rootDomainRecipientSession;

		// Token: 0x04000679 RID: 1657
		internal IConfigurationSession configurationSession;

		// Token: 0x0400067A RID: 1658
		internal IConfigurationSession domainConfigurationSession;

		// Token: 0x0400067B RID: 1659
		private IRecipientSession orgDomainRecipientSession;

		// Token: 0x0400067C RID: 1660
		protected ADDomain rootDomain;

		// Token: 0x0400067D RID: 1661
		protected ADOrganizationalUnit organization;

		// Token: 0x0400067E RID: 1662
		private ADObjectId rootOrgId;

		// Token: 0x0400067F RID: 1663
		internal ITopologyConfigurationSession rootOrgSession;
	}
}
