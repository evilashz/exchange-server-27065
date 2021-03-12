using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Management.Automation.Runspaces;
using System.Security.Principal;
using System.Text;
using System.Threading;
using Microsoft.Exchange.Collections;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Configuration.Common.LocStrings;
using Microsoft.Exchange.Configuration.Core;
using Microsoft.Exchange.Configuration.ObjectModel.EventLog;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Directory.ValidationRules;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.CmdletInfra;
using Microsoft.Exchange.Diagnostics.Components.Authorization;
using Microsoft.Exchange.Extensions;
using Microsoft.Exchange.Provisioning;
using Microsoft.Exchange.Security.Authentication;
using Microsoft.Exchange.Security.Authorization;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.Configuration.Authorization
{
	// Token: 0x02000222 RID: 546
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class ExchangeRunspaceConfiguration : IDisposable
	{
		// Token: 0x17000389 RID: 905
		// (get) Token: 0x060012FE RID: 4862 RVA: 0x0003EC15 File Offset: 0x0003CE15
		// (set) Token: 0x060012FF RID: 4863 RVA: 0x0003EC1D File Offset: 0x0003CE1D
		public bool RestrictToFilteredCmdlet { get; protected set; }

		// Token: 0x1700038A RID: 906
		// (get) Token: 0x06001300 RID: 4864 RVA: 0x0003EC26 File Offset: 0x0003CE26
		// (set) Token: 0x06001301 RID: 4865 RVA: 0x0003EC2E File Offset: 0x0003CE2E
		private protected bool HasLinkedRoleGroups { protected get; private set; }

		// Token: 0x1700038B RID: 907
		// (get) Token: 0x06001302 RID: 4866 RVA: 0x0003EC37 File Offset: 0x0003CE37
		protected SerializedAccessToken UserAccessToken
		{
			get
			{
				return this.impersonatedUserAccessToken ?? this.logonAccessToken;
			}
		}

		// Token: 0x1700038C RID: 908
		// (get) Token: 0x06001303 RID: 4867 RVA: 0x0003EC49 File Offset: 0x0003CE49
		// (set) Token: 0x06001304 RID: 4868 RVA: 0x0003EC51 File Offset: 0x0003CE51
		internal bool IsPowerShellWebService { get; private set; }

		// Token: 0x1700038D RID: 909
		// (get) Token: 0x06001305 RID: 4869 RVA: 0x0003EC5A File Offset: 0x0003CE5A
		internal bool IsAppPasswordUsed
		{
			get
			{
				return this.settings != null && this.settings.UserToken != null && this.settings.UserToken.AppPasswordUsed;
			}
		}

		// Token: 0x1700038E RID: 910
		// (get) Token: 0x06001306 RID: 4870 RVA: 0x0003EC83 File Offset: 0x0003CE83
		internal List<RoleEntry> SortedRoleEntryFilter
		{
			get
			{
				return this.sortedRoleEntryFilter;
			}
		}

		// Token: 0x1700038F RID: 911
		// (get) Token: 0x06001307 RID: 4871 RVA: 0x0003EC8C File Offset: 0x0003CE8C
		internal string ServicePlanForLogging
		{
			get
			{
				if (this.servicePlanForLogging == null && null != this.ExecutingUserOrganizationId && this.ExecutingUserOrganizationId.OrganizationalUnit != null)
				{
					try
					{
						string name = this.ExecutingUserOrganizationId.OrganizationalUnit.Name;
						ITenantConfigurationSession tenantConfigurationSession = DirectorySessionFactory.Default.CreateTenantConfigurationSession(ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromTenantCUName(name), 639, "ServicePlanForLogging", "f:\\15.00.1497\\sources\\dev\\Configuration\\src\\ObjectModel\\rbac\\ExchangeRunspaceConfiguration.cs");
						ExchangeConfigurationUnit exchangeConfigurationUnitByNameOrAcceptedDomain = tenantConfigurationSession.GetExchangeConfigurationUnitByNameOrAcceptedDomain(name);
						if (exchangeConfigurationUnitByNameOrAcceptedDomain != null)
						{
							this.servicePlanForLogging = exchangeConfigurationUnitByNameOrAcceptedDomain.ServicePlan;
						}
					}
					catch (Exception ex)
					{
						this.servicePlanForLogging = ex.ToString();
					}
				}
				return this.servicePlanForLogging;
			}
		}

		// Token: 0x17000390 RID: 912
		// (get) Token: 0x06001308 RID: 4872 RVA: 0x0003ED30 File Offset: 0x0003CF30
		protected virtual bool ApplyValidationRules
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000391 RID: 913
		// (get) Token: 0x06001309 RID: 4873 RVA: 0x0003ED33 File Offset: 0x0003CF33
		// (set) Token: 0x0600130A RID: 4874 RVA: 0x0003ED3B File Offset: 0x0003CF3B
		protected bool NoCmdletAllowed { get; set; }

		// Token: 0x0600130B RID: 4875 RVA: 0x0003ED44 File Offset: 0x0003CF44
		protected ExchangeRunspaceConfiguration()
		{
		}

		// Token: 0x0600130C RID: 4876 RVA: 0x0003EDB2 File Offset: 0x0003CFB2
		public ExchangeRunspaceConfiguration(IIdentity identity) : this(identity, ExchangeRunspaceConfigurationSettings.GetDefaultInstance())
		{
		}

		// Token: 0x0600130D RID: 4877 RVA: 0x0003EDC0 File Offset: 0x0003CFC0
		public ExchangeRunspaceConfiguration(IIdentity logonIdentity, IIdentity impersonatedIdentity, IList<RoleType> roleTypeFilter, List<RoleEntry> sortedRoleEntryFilter, bool callerCheckedAccess) : this(logonIdentity, impersonatedIdentity, ExchangeRunspaceConfigurationSettings.GetDefaultInstance(), roleTypeFilter, sortedRoleEntryFilter, null, callerCheckedAccess)
		{
		}

		// Token: 0x0600130E RID: 4878 RVA: 0x0003EDD5 File Offset: 0x0003CFD5
		internal ExchangeRunspaceConfiguration(IIdentity identity, ExchangeRunspaceConfigurationSettings settings) : this(identity, null, settings, null, null, null, false)
		{
		}

		// Token: 0x0600130F RID: 4879 RVA: 0x0003EDE4 File Offset: 0x0003CFE4
		internal ExchangeRunspaceConfiguration(IIdentity logonIdentity, IIdentity impersonatedIdentity, ExchangeRunspaceConfigurationSettings settings, IList<RoleType> roleTypeFilter, List<RoleEntry> sortedRoleEntryFilter, IList<RoleType> logonUserRequiredRoleTypes, bool callerCheckedAccess) : this(logonIdentity, impersonatedIdentity, settings, roleTypeFilter, sortedRoleEntryFilter, logonUserRequiredRoleTypes, callerCheckedAccess, false, false, SnapinSet.Default)
		{
		}

		// Token: 0x06001310 RID: 4880 RVA: 0x0003EE08 File Offset: 0x0003D008
		internal ExchangeRunspaceConfiguration(IIdentity logonIdentity, IIdentity impersonatedIdentity, ExchangeRunspaceConfigurationSettings settings, IList<RoleType> roleTypeFilter, List<RoleEntry> sortedRoleEntryFilter, IList<RoleType> logonUserRequiredRoleTypes, bool callerCheckedAccess, SnapinSet snapinSet) : this(logonIdentity, impersonatedIdentity, settings, roleTypeFilter, sortedRoleEntryFilter, logonUserRequiredRoleTypes, callerCheckedAccess, false, false, snapinSet)
		{
		}

		// Token: 0x06001311 RID: 4881 RVA: 0x0003EE3C File Offset: 0x0003D03C
		internal ExchangeRunspaceConfiguration(IIdentity logonIdentity, IIdentity impersonatedIdentity, ExchangeRunspaceConfigurationSettings settings, IList<RoleType> roleTypeFilter, List<RoleEntry> sortedRoleEntryFilter, IList<RoleType> logonUserRequiredRoleTypes, bool callerCheckedAccess, bool isPowerShellWebService, bool noCmdletAllowed = false, SnapinSet snapinSet = SnapinSet.Default)
		{
			this.logonIdentityToDispose = (logonIdentity as IDisposable);
			this.impersonatedIdentityToDispose = (impersonatedIdentity as IDisposable);
			if (settings == null)
			{
				throw new ArgumentNullException("settings");
			}
			this.settings = settings;
			AuthZLogger.SafeAppendGenericInfo("ERC.SnapinCollection", snapinSet.ToString());
			switch (snapinSet)
			{
			case SnapinSet.OSP:
				this.snapinCollection = ExchangeRunspaceConfiguration.OspSnapins;
				break;
			case SnapinSet.Admin:
				this.snapinCollection = ExchangeRunspaceConfiguration.AdminSnapins;
				break;
			default:
				this.snapinCollection = ExchangeRunspaceConfiguration.ExchangeSnapins;
				break;
			}
			if (logonIdentity == null)
			{
				throw new ArgumentNullException("logonIdentity");
			}
			if (DelegatedPrincipal.DelegatedAuthenticationType.Equals(logonIdentity.AuthenticationType))
			{
				this.delegatedPrincipal = new DelegatedPrincipal(logonIdentity, null);
			}
			this.authenticationType = logonIdentity.AuthenticationType;
			this.roleTypeFilter = roleTypeFilter;
			this.sortedRoleEntryFilter = sortedRoleEntryFilter;
			this.logonUserRequiredRoleTypes = logonUserRequiredRoleTypes;
			this.callerCheckedAccess = callerCheckedAccess;
			this.IsPowerShellWebService = isPowerShellWebService;
			this.NoCmdletAllowed = noCmdletAllowed;
			string text;
			if (this.delegatedPrincipal != null)
			{
				text = this.delegatedPrincipal.ToString();
			}
			else
			{
				text = logonIdentity.GetSafeName(true);
			}
			string text2 = null;
			if (impersonatedIdentity != null)
			{
				this.Impersonated = true;
				text2 = impersonatedIdentity.GetSafeName(true);
				this.identityName = Strings.OnbehalfOf(text, text2);
				if (this.PartnerMode)
				{
					throw new ArgumentException("ParterMode is not allowed to create an impersonated runspace.");
				}
			}
			else
			{
				this.Impersonated = false;
				this.identityName = text;
				if (logonUserRequiredRoleTypes != null)
				{
					throw new ArgumentException("logonUserRequiredRoleTypes should only be used for creating impersonated RBAC runspace.");
				}
			}
			ExTraceGlobals.RunspaceConfigTracer.TraceDebug<string>((long)this.GetHashCode(), "Creating new ExchangeRunspaceConfiguration for IIdentity {0}", this.identityName);
			if (this.settings.UserToken == null)
			{
				IIdentity identity = this.Impersonated ? impersonatedIdentity : logonIdentity;
				this.settings.UserToken = UserTokenHelper.CreateDefaultUserTokenInERC(identity, this.delegatedPrincipal, this.Impersonated);
			}
			ADRawEntry adrawEntry = null;
			ADRawEntry adrawEntry2 = null;
			if (this.Impersonated)
			{
				adrawEntry = this.LoadExecutingUser(impersonatedIdentity, null);
				this.executingUser = adrawEntry;
			}
			else if (this.delegatedPrincipal == null)
			{
				adrawEntry2 = this.LoadExecutingUser(logonIdentity, ExchangeRunspaceConfiguration.userPropertyArray);
				this.executingUser = adrawEntry2;
			}
			List<ADObjectId> list;
			if (this.delegatedPrincipal != null)
			{
				OrganizationId orgId;
				this.TryGetGroupAccountsForDelegatedPrincipal(this.delegatedPrincipal, out list, out orgId, out this.ensureTargetIsMemberOfRIMMailboxUsersGroup);
				if (list.Count == 0)
				{
					AuthZLogger.SafeAppendGenericError("ERC.Ctor", "TryGetGroupAccountsForDelegatedPrincipal roleGoups.Count is 0", false);
					TaskLogger.LogRbacEvent(TaskEventLogConstants.Tuple_AccessDenied_NoGroupsResolvedForDelegatedAdmin, this.identityName, new object[]
					{
						this.identityName,
						this.delegatedPrincipal.DelegatedOrganization,
						this.delegatedPrincipal.Identity.Name
					});
					throw new CmdletAccessDeniedException(Strings.ErrorManagementObjectNotFound(this.identityName));
				}
				ExTraceGlobals.PublicCreationAPITracer.TraceDebug<string, int>((long)this.GetHashCode(), "Delegated admin authenticated. Creating stub user object for {0}, number of groups {1}", this.identityName, list.Count);
				ADUser aduser = this.CreateDelegatedADUser(this.delegatedPrincipal, orgId);
				if (this.Impersonated)
				{
					this.logonUser = aduser;
				}
				else
				{
					this.executingUser = aduser;
				}
			}
			else
			{
				this.tokenSids = this.GetGroupAccountsSIDs(logonIdentity);
				ExchangeRunspaceConfiguration.TryFindLinkedRoleGroupsBySidList(this.recipientSession, this.tokenSids, this.identityName, out list);
				this.HasLinkedRoleGroups = (list.Count > 0);
			}
			if (this.executingUser == null && this.delegatedPrincipal == null)
			{
				SecurityIdentifier securityIdentifier = this.Impersonated ? impersonatedIdentity.GetSecurityIdentifier() : logonIdentity.GetSecurityIdentifier();
				if (list.Count == 0)
				{
					AuthZLogger.SafeAppendGenericError("ERC.Ctor", "User Not found in AD and no linked role groups", false);
					TaskLogger.LogRbacEvent(TaskEventLogConstants.Tuple_AccessDenied_UserNotFoundBySid, null, new object[]
					{
						this.identityName,
						securityIdentifier,
						this.recipientSession.Source
					});
					throw new CmdletAccessDeniedException(Strings.ErrorManagementObjectNotFound(this.identityName));
				}
				AuthZLogger.SafeAppendGenericInfo("ERC.ExecutingUser", "Linked Role Group Only");
				ExTraceGlobals.PublicCreationAPITracer.TraceDebug<string, SecurityIdentifier>((long)this.GetHashCode(), "No user found, but found linked role groups associated with identity.  Creating stub user object for {0} with SID {1}", this.identityName, securityIdentifier);
				this.executingUser = this.CreateDelegatedADUser(this.identityName, securityIdentifier);
			}
			if (this.executingUser != null)
			{
				IEnumerable<ADPropertyDefinition> enumerable = ExchangeRunspaceConfiguration.userPropertyArray;
				if (this.executingUser.ObjectSchema != null)
				{
					enumerable = (from x in enumerable
					where x.IsCalculated
					select x).Union(from ADPropertyDefinition x in this.executingUser.ObjectSchema.AllProperties
					where x.IsCalculated
					select x);
				}
				foreach (ProviderPropertyDefinition providerPropertyDefinition in enumerable)
				{
					if (providerPropertyDefinition.IsCalculated)
					{
						object obj = this.executingUser[providerPropertyDefinition];
					}
				}
			}
			if (this.Impersonated)
			{
				if (this.delegatedPrincipal == null)
				{
					this.logonUser = this.LoadExecutingUser(logonIdentity, ExchangeRunspaceConfiguration.userPropertyArray);
					if (this.logonUser == null)
					{
						this.logonUser = this.CreateDelegatedADUser(this.identityName, logonIdentity.GetSecurityIdentifier());
					}
					if (!this.logonUser[ADObjectSchema.OrganizationId].Equals(this.executingUser[ADObjectSchema.OrganizationId]))
					{
						throw new CmdletAccessDeniedException(Strings.NotInSameOrg(this.identityName, text2));
					}
				}
				this.identityName = Strings.OnbehalfOf((this.logonUser != null && this.logonUser.Id != null) ? this.logonUser.Id.ToString() : text, (adrawEntry != null) ? adrawEntry.Id.ToString() : text2);
			}
			else
			{
				this.logonUser = this.executingUser;
				this.identityName = ((adrawEntry2 != null) ? adrawEntry2.Id.ToString() : this.identityName);
			}
			this.intersectRoleEntries = (this.Impersonated && !callerCheckedAccess);
			this.OpenMailboxAsAdmin = this.intersectRoleEntries;
			if (this.delegatedPrincipal == null)
			{
				this.logonAccessToken = this.PopulateGroupMemberships(logonIdentity);
			}
			if (this.Impersonated)
			{
				this.impersonatedUserAccessToken = this.PopulateGroupMemberships(impersonatedIdentity);
			}
			this.LoadRoleCmdletInfo(this.settings.TenantOrganization, roleTypeFilter, sortedRoleEntryFilter, logonUserRequiredRoleTypes, list);
			this.RefreshCombinedCmdletsAndScripts();
			this.RefreshVerboseDebugEnabledCmdlets(this.combinedCmdlets);
			AuthZLogger.SafeAppendGenericInfo("ERC.RoleAssignmentsCnt", this.RoleAssignments.Count.ToString());
			AuthZLogger.SafeAppendGenericInfo("ERC.AllRoleEntriesCnt", this.allRoleEntries.Count.ToString());
			if (this.Impersonated)
			{
				AuthZLogger.SafeAppendGenericInfo("ERC.Impersonated", "true");
				ExTraceGlobals.PublicCreationAPITracer.TraceDebug((long)this.GetHashCode(), "Created ExchangeRunspaceConfiguration for {3} on behalf of {0} with {1} role assingments and {2} entries. CallerCheckedAccess is {4}", new object[]
				{
					this.executingUser.Id,
					this.RoleAssignments.Count,
					this.allRoleEntries.Count,
					this.logonUser.Id,
					callerCheckedAccess
				});
			}
			else
			{
				ExTraceGlobals.PublicCreationAPITracer.TraceDebug<ADObjectId, int, int>((long)this.GetHashCode(), "Created ExchangeRunspaceConfiguration for {0} with {1} role assingments and {2} entries", this.executingUser.Id, this.RoleAssignments.Count, this.allRoleEntries.Count);
			}
			this.troubleshootingContext.TraceOperationCompletedAndUpdateContext();
		}

		// Token: 0x06001312 RID: 4882 RVA: 0x0003F5C4 File Offset: 0x0003D7C4
		private ADUser CreateDelegatedADUser(DelegatedPrincipal delegatedPrincipal, OrganizationId orgId)
		{
			PropertyBag propertyBag = new DelegatedPropertyBag();
			propertyBag[DelegatedObjectSchema.Identity] = new DelegatedObjectId(delegatedPrincipal.UserId, delegatedPrincipal.DelegatedOrganization);
			propertyBag[ADObjectSchema.Name] = delegatedPrincipal.GetUserName();
			propertyBag[ADUserSchema.UserPrincipalName] = delegatedPrincipal.UserId;
			propertyBag[ADRecipientSchema.DisplayName] = delegatedPrincipal.DisplayName;
			propertyBag[ADRecipientSchema.HiddenFromAddressListsEnabled] = true;
			if (orgId != null)
			{
				propertyBag[ADObjectSchema.OrganizationId] = orgId;
			}
			return new ADUser(this.recipientSession, propertyBag);
		}

		// Token: 0x06001313 RID: 4883 RVA: 0x0003F658 File Offset: 0x0003D858
		private ADUser CreateDelegatedADUser(string name, SecurityIdentifier sid)
		{
			PropertyBag propertyBag = new ADPropertyBag();
			propertyBag[ADObjectSchema.Name] = name.Substring(0, Math.Min(64, name.Length));
			propertyBag[ADRecipientSchema.DisplayName] = Strings.DelegatedAdminAccount(name).ToString();
			propertyBag[ADRecipientSchema.HiddenFromAddressListsEnabled] = true;
			if (sid != null)
			{
				propertyBag.DangerousSetValue(ADMailboxRecipientSchema.Sid, sid);
			}
			return new ADUser(this.recipientSession, propertyBag);
		}

		// Token: 0x17000392 RID: 914
		// (get) Token: 0x06001314 RID: 4884 RVA: 0x0003F6DB File Offset: 0x0003D8DB
		public ICollection<RoleType> RoleTypes
		{
			get
			{
				return this.allRoleTypes.Keys;
			}
		}

		// Token: 0x17000393 RID: 915
		// (get) Token: 0x06001315 RID: 4885 RVA: 0x0003F6E8 File Offset: 0x0003D8E8
		internal DelegatedPrincipal DelegatedPrincipal
		{
			get
			{
				return this.delegatedPrincipal;
			}
		}

		// Token: 0x17000394 RID: 916
		// (get) Token: 0x06001316 RID: 4886 RVA: 0x0003F6F0 File Offset: 0x0003D8F0
		internal virtual ObjectId ExecutingUserIdentity
		{
			get
			{
				return this.executingUser.Identity;
			}
		}

		// Token: 0x17000395 RID: 917
		// (get) Token: 0x06001317 RID: 4887 RVA: 0x0003F6FD File Offset: 0x0003D8FD
		internal virtual string ExecutingUserDisplayName
		{
			get
			{
				return (string)this.executingUser[ADRecipientSchema.DisplayName];
			}
		}

		// Token: 0x17000396 RID: 918
		// (get) Token: 0x06001318 RID: 4888 RVA: 0x0003F714 File Offset: 0x0003D914
		internal virtual string ExecutingUserPrincipalName
		{
			get
			{
				return (string)this.executingUser[ADUserSchema.UserPrincipalName];
			}
		}

		// Token: 0x17000397 RID: 919
		// (get) Token: 0x06001319 RID: 4889 RVA: 0x0003F72B File Offset: 0x0003D92B
		internal ADRawEntry ExecutingUser
		{
			get
			{
				return this.executingUser;
			}
		}

		// Token: 0x17000398 RID: 920
		// (get) Token: 0x0600131A RID: 4890 RVA: 0x0003F734 File Offset: 0x0003D934
		internal ADRecipientOrAddress ExecutingUserAsRecipient
		{
			get
			{
				if (this.executingUserAsRecipient == null)
				{
					Participant participant;
					if (this.delegatedPrincipal != null)
					{
						participant = new Participant(this.ExecutingUserDisplayName, this.delegatedPrincipal.UserId, "SMTP");
					}
					else if (!string.IsNullOrEmpty((string)this.executingUser[ADRecipientSchema.LegacyExchangeDN]))
					{
						participant = new Participant(this.executingUser);
					}
					else
					{
						participant = new Participant(this.ExecutingUserDisplayName, this.ExecutingUserPrincipalName, "SMTP");
					}
					this.executingUserAsRecipient = new ADRecipientOrAddress(participant);
				}
				return this.executingUserAsRecipient;
			}
		}

		// Token: 0x0600131B RID: 4891 RVA: 0x0003F7C4 File Offset: 0x0003D9C4
		public void Dispose()
		{
			if (this.logonIdentityToDispose != null)
			{
				this.logonIdentityToDispose.Dispose();
				this.logonIdentityToDispose = null;
			}
			if (this.impersonatedIdentityToDispose != null)
			{
				this.impersonatedIdentityToDispose.Dispose();
				this.impersonatedIdentityToDispose = null;
			}
			this.EnablePiiMap = false;
		}

		// Token: 0x0600131C RID: 4892 RVA: 0x0003F801 File Offset: 0x0003DA01
		internal virtual bool TryGetExecutingUserId(out ADObjectId executingUserId)
		{
			executingUserId = (ADObjectId)this.executingUser[ADObjectSchema.Id];
			return executingUserId != null;
		}

		// Token: 0x0600131D RID: 4893 RVA: 0x0003F822 File Offset: 0x0003DA22
		internal bool TryGetExecutingWindowsLiveId(out SmtpAddress executingWindowsLiveId)
		{
			executingWindowsLiveId = (SmtpAddress)this.executingUser[ADRecipientSchema.WindowsLiveID];
			return true;
		}

		// Token: 0x0600131E RID: 4894 RVA: 0x0003F840 File Offset: 0x0003DA40
		internal virtual bool TryGetExecutingUserSid(out SecurityIdentifier executingUserSid)
		{
			executingUserSid = (SecurityIdentifier)this.executingUser[IADSecurityPrincipalSchema.Sid];
			return executingUserSid != null;
		}

		// Token: 0x17000399 RID: 921
		// (get) Token: 0x0600131F RID: 4895 RVA: 0x0003F861 File Offset: 0x0003DA61
		internal virtual bool ExecutingUserIsAllowedRemotePowerShell
		{
			get
			{
				return (bool)this.executingUser[ADRecipientSchema.RemotePowerShellEnabled];
			}
		}

		// Token: 0x1700039A RID: 922
		// (get) Token: 0x06001320 RID: 4896 RVA: 0x0003F878 File Offset: 0x0003DA78
		internal virtual OrganizationId ExecutingUserOrganizationId
		{
			get
			{
				return (OrganizationId)this.executingUser[ADObjectSchema.OrganizationId];
			}
		}

		// Token: 0x1700039B RID: 923
		// (get) Token: 0x06001321 RID: 4897 RVA: 0x0003F88F File Offset: 0x0003DA8F
		internal virtual bool ExecutingUserIsAllowedECP
		{
			get
			{
				return (bool)this.executingUser[ADRecipientSchema.ECPEnabled];
			}
		}

		// Token: 0x1700039C RID: 924
		// (get) Token: 0x06001322 RID: 4898 RVA: 0x0003F8A6 File Offset: 0x0003DAA6
		internal virtual bool ExecutingUserIsAllowedOWA
		{
			get
			{
				return (bool)this.executingUser[ADRecipientSchema.OWAEnabled];
			}
		}

		// Token: 0x1700039D RID: 925
		// (get) Token: 0x06001323 RID: 4899 RVA: 0x0003F8BD File Offset: 0x0003DABD
		internal virtual bool ExecutingUserIsActiveSyncEnabled
		{
			get
			{
				return (bool)this.executingUser[ADUserSchema.ActiveSyncEnabled];
			}
		}

		// Token: 0x1700039E RID: 926
		// (get) Token: 0x06001324 RID: 4900 RVA: 0x0003F8D4 File Offset: 0x0003DAD4
		internal virtual bool ExecutingUserIsPopEnabled
		{
			get
			{
				return (bool)this.executingUser[ADRecipientSchema.PopEnabled];
			}
		}

		// Token: 0x1700039F RID: 927
		// (get) Token: 0x06001325 RID: 4901 RVA: 0x0003F8EB File Offset: 0x0003DAEB
		internal virtual bool ExecutingUserIsImapEnabled
		{
			get
			{
				return (bool)this.executingUser[ADRecipientSchema.ImapEnabled];
			}
		}

		// Token: 0x170003A0 RID: 928
		// (get) Token: 0x06001326 RID: 4902 RVA: 0x0003F902 File Offset: 0x0003DB02
		internal virtual bool ExecutingUserHasRetentionPolicy
		{
			get
			{
				return SharedConfiguration.ExecutingUserHasRetentionPolicy(this.executingUser, this.OrganizationId);
			}
		}

		// Token: 0x170003A1 RID: 929
		// (get) Token: 0x06001327 RID: 4903 RVA: 0x0003F915 File Offset: 0x0003DB15
		internal virtual bool ExecutingUserHasExternalOofOptions
		{
			get
			{
				return (ExternalOofOptions)this.executingUser[ADMailboxRecipientSchema.ExternalOofOptions] == ExternalOofOptions.External;
			}
		}

		// Token: 0x170003A2 RID: 930
		// (get) Token: 0x06001328 RID: 4904 RVA: 0x0003F930 File Offset: 0x0003DB30
		internal virtual bool ExecutingUserIsResource
		{
			get
			{
				RecipientTypeDetails recipientTypeDetails = (RecipientTypeDetails)this.executingUser[ADRecipientSchema.RecipientTypeDetails];
				return recipientTypeDetails == RecipientTypeDetails.RoomMailbox || recipientTypeDetails == RecipientTypeDetails.EquipmentMailbox || recipientTypeDetails == RecipientTypeDetails.LinkedRoomMailbox;
			}
		}

		// Token: 0x170003A3 RID: 931
		// (get) Token: 0x06001329 RID: 4905 RVA: 0x0003F96D File Offset: 0x0003DB6D
		internal virtual bool ExecutingUserIsMAPIEnabled
		{
			get
			{
				return (bool)this.executingUser[ADRecipientSchema.MAPIEnabled];
			}
		}

		// Token: 0x170003A4 RID: 932
		// (get) Token: 0x0600132A RID: 4906 RVA: 0x0003F984 File Offset: 0x0003DB84
		internal virtual bool ExecutingUserIsUmEnabled
		{
			get
			{
				return (bool)this.executingUser[ADUserSchema.UMEnabled];
			}
		}

		// Token: 0x170003A5 RID: 933
		// (get) Token: 0x0600132B RID: 4907 RVA: 0x0003F99B File Offset: 0x0003DB9B
		internal virtual SmtpAddress ExecutingUserPrimarySmtpAddress
		{
			get
			{
				return (SmtpAddress)this.executingUser[ADRecipientSchema.PrimarySmtpAddress];
			}
		}

		// Token: 0x170003A6 RID: 934
		// (get) Token: 0x0600132C RID: 4908 RVA: 0x0003F9B4 File Offset: 0x0003DBB4
		// (set) Token: 0x0600132D RID: 4909 RVA: 0x0003FA20 File Offset: 0x0003DC20
		internal virtual bool ExecutingUserIsUmConfigured
		{
			get
			{
				if (this.umConfigured == null)
				{
					if (this.ExecutingUserIsUmEnabled)
					{
						this.umConfigured = new bool?(null != UMMailbox.GetPrimaryExtension((ProxyAddressCollection)this.executingUser[ADRecipientSchema.EmailAddresses], ProxyAddressPrefix.UM));
					}
					else
					{
						this.umConfigured = new bool?(false);
					}
				}
				return this.umConfigured.Value;
			}
			set
			{
				if (!this.ExecutingUserIsUmEnabled && value)
				{
					ExTraceGlobals.AccessCheckTracer.TraceError<string>((long)this.GetHashCode(), "User {0} has to be UMEnabled before he could be UMConfigured.", this.executingUser.Id.DistinguishedName);
					throw new InvalidOperationException();
				}
				this.umConfigured = new bool?(value);
			}
		}

		// Token: 0x170003A7 RID: 935
		// (get) Token: 0x0600132E RID: 4910 RVA: 0x0003FA70 File Offset: 0x0003DC70
		internal virtual bool ExecutingUserIsHiddenFromGAL
		{
			get
			{
				return (bool)this.executingUser[ADRecipientSchema.HiddenFromAddressListsEnabled];
			}
		}

		// Token: 0x170003A8 RID: 936
		// (get) Token: 0x0600132F RID: 4911 RVA: 0x0003FA87 File Offset: 0x0003DC87
		internal bool ExecutingUserIsPiiApproved
		{
			get
			{
				return this.HasRoleOfType(RoleType.MailRecipientCreation) || this.HasRoleOfType(RoleType.PersonallyIdentifiableInformation);
			}
		}

		// Token: 0x170003A9 RID: 937
		// (get) Token: 0x06001330 RID: 4912 RVA: 0x0003FAA0 File Offset: 0x0003DCA0
		public bool NeedSuppressingPiiData
		{
			get
			{
				return this.AuthenticationType == "Kerberos" && !this.ExecutingUserIsPiiApproved && VariantConfiguration.InvariantNoFlightingSnapshot.CmdletInfra.PiiRedaction.Enabled;
			}
		}

		// Token: 0x170003AA RID: 938
		// (get) Token: 0x06001331 RID: 4913 RVA: 0x0003FAE0 File Offset: 0x0003DCE0
		// (set) Token: 0x06001332 RID: 4914 RVA: 0x0003FAE8 File Offset: 0x0003DCE8
		internal bool EnablePiiMap
		{
			get
			{
				return this.enablePiiMap;
			}
			set
			{
				this.enablePiiMap = value;
				if (this.enablePiiMap && string.IsNullOrEmpty(this.PiiMapId))
				{
					this.PiiMapId = Guid.NewGuid().ToString();
					return;
				}
				if (!this.enablePiiMap && !string.IsNullOrEmpty(this.PiiMapId))
				{
					this.PiiMapId = null;
				}
			}
		}

		// Token: 0x170003AB RID: 939
		// (get) Token: 0x06001333 RID: 4915 RVA: 0x0003FB47 File Offset: 0x0003DD47
		// (set) Token: 0x06001334 RID: 4916 RVA: 0x0003FB4F File Offset: 0x0003DD4F
		internal string PiiMapId
		{
			get
			{
				return this.piiMapId;
			}
			private set
			{
				if (this.piiMapId != null && this.piiMapId != value)
				{
					PiiMapManager.Instance.Remove(this.piiMapId);
				}
				this.piiMapId = value;
			}
		}

		// Token: 0x170003AC RID: 940
		// (get) Token: 0x06001335 RID: 4917 RVA: 0x0003FB7E File Offset: 0x0003DD7E
		internal bool ExecutingUserHasResetPasswordPermission
		{
			get
			{
				return this.HasRoleOfType(RoleType.ResetPassword);
			}
		}

		// Token: 0x170003AD RID: 941
		// (get) Token: 0x06001336 RID: 4918 RVA: 0x0003FB88 File Offset: 0x0003DD88
		// (set) Token: 0x06001337 RID: 4919 RVA: 0x0003FB90 File Offset: 0x0003DD90
		internal bool ExecutingUserLanguagesChanged { get; set; }

		// Token: 0x170003AE RID: 942
		// (get) Token: 0x06001338 RID: 4920 RVA: 0x0003FB9C File Offset: 0x0003DD9C
		internal virtual MultiValuedProperty<CultureInfo> ExecutingUserLanguages
		{
			get
			{
				MultiValuedProperty<CultureInfo> result;
				using (new MonitoredScope("ExchangeRunspaceConfiguration", "ExecutingUserLanguages.get", AuthZLogHelper.AuthZPerfMonitors))
				{
					if (this.ExecutingUserLanguagesChanged)
					{
						ExTraceGlobals.RunspaceConfigTracer.TraceDebug<ADObjectId>((long)this.GetHashCode(), "Fetching updated Languages value for the user: {0}", this.executingUser.Id);
						IRecipientSession tenantOrRootOrgRecipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(null, false, ConsistencyMode.FullyConsistent, null, this.OrganizationId.ToADSessionSettings(), ConfigScopes.TenantSubTree, 1653, "ExecutingUserLanguages", "f:\\15.00.1497\\sources\\dev\\Configuration\\src\\ObjectModel\\rbac\\ExchangeRunspaceConfiguration.cs");
						tenantOrRootOrgRecipientSession.UseGlobalCatalog = false;
						ADObjectId entryId;
						if (this.TryGetExecutingUserId(out entryId))
						{
							ADRawEntry adrawEntry = tenantOrRootOrgRecipientSession.ReadADRawEntry(entryId, new ADPropertyDefinition[]
							{
								ADOrgPersonSchema.Languages
							});
							if (adrawEntry != null)
							{
								this.executingUserLanguages = (MultiValuedProperty<CultureInfo>)adrawEntry[ADOrgPersonSchema.Languages];
								this.ExecutingUserLanguagesChanged = false;
							}
						}
					}
					this.executingUserLanguages = (this.executingUserLanguages ?? ((MultiValuedProperty<CultureInfo>)this.executingUser[ADOrgPersonSchema.Languages]));
					result = this.executingUserLanguages;
				}
				return result;
			}
		}

		// Token: 0x170003AF RID: 943
		// (get) Token: 0x06001339 RID: 4921 RVA: 0x0003FCAC File Offset: 0x0003DEAC
		internal virtual RecipientType ExecutingUserRecipientType
		{
			get
			{
				return (RecipientType)this.executingUser[ADRecipientSchema.RecipientType];
			}
		}

		// Token: 0x170003B0 RID: 944
		// (get) Token: 0x0600133A RID: 4922 RVA: 0x0003FCC3 File Offset: 0x0003DEC3
		internal virtual bool ExecutingUserIsPersonToPersonTextMessagingEnabled
		{
			get
			{
				return (bool)this.executingUser[ADRecipientSchema.IsPersonToPersonTextMessagingEnabled];
			}
		}

		// Token: 0x170003B1 RID: 945
		// (get) Token: 0x0600133B RID: 4923 RVA: 0x0003FCDA File Offset: 0x0003DEDA
		internal virtual bool ExecutingUserIsMachineToPersonTextMessagingEnabled
		{
			get
			{
				return (bool)this.executingUser[ADRecipientSchema.IsMachineToPersonTextMessagingEnabled];
			}
		}

		// Token: 0x170003B2 RID: 946
		// (get) Token: 0x0600133C RID: 4924 RVA: 0x0003FCF1 File Offset: 0x0003DEF1
		internal virtual bool ExecutingUserIsBposUser
		{
			get
			{
				return CapabilityHelper.HasBposSKUCapability((MultiValuedProperty<Capability>)this.executingUser[SharedPropertyDefinitions.PersistedCapabilities]);
			}
		}

		// Token: 0x170003B3 RID: 947
		// (get) Token: 0x0600133D RID: 4925 RVA: 0x0003FD0D File Offset: 0x0003DF0D
		internal virtual ADRawEntry LogonUser
		{
			get
			{
				return this.logonUser;
			}
		}

		// Token: 0x170003B4 RID: 948
		// (get) Token: 0x0600133E RID: 4926 RVA: 0x0003FD15 File Offset: 0x0003DF15
		internal virtual string LogonUserDisplayName
		{
			get
			{
				return (string)this.logonUser[ADRecipientSchema.DisplayName];
			}
		}

		// Token: 0x170003B5 RID: 949
		// (get) Token: 0x0600133F RID: 4927 RVA: 0x0003FD2C File Offset: 0x0003DF2C
		internal virtual RecipientType LogonUserRecipientType
		{
			get
			{
				return (RecipientType)this.logonUser[ADRecipientSchema.RecipientType];
			}
		}

		// Token: 0x170003B6 RID: 950
		// (get) Token: 0x06001340 RID: 4928 RVA: 0x0003FD43 File Offset: 0x0003DF43
		internal virtual SecurityIdentifier LogonUserSid
		{
			get
			{
				return (SecurityIdentifier)this.logonUser[IADSecurityPrincipalSchema.Sid];
			}
		}

		// Token: 0x170003B7 RID: 951
		// (get) Token: 0x06001341 RID: 4929 RVA: 0x0003FD5C File Offset: 0x0003DF5C
		internal virtual OrganizationId OrganizationId
		{
			get
			{
				if (this.organizationId != null && (OrganizationId)this.executingUser[ADObjectSchema.OrganizationId] != OrganizationId.ForestWideOrgId)
				{
					throw new AuthorizationException(Strings.ErrorInvalidStatePartnerOrgNotNull(this.executingUser.Id.ToString()));
				}
				return this.organizationId ?? ((OrganizationId)this.executingUser[ADObjectSchema.OrganizationId]);
			}
		}

		// Token: 0x170003B8 RID: 952
		// (get) Token: 0x06001342 RID: 4930 RVA: 0x0003FDD4 File Offset: 0x0003DFD4
		internal virtual OwaSegmentationSettings OwaSegmentationSettings
		{
			get
			{
				OwaSegmentationSettings result;
				using (new MonitoredScope("ExchangeRunspaceConfiguration", "OwaSegmentationSettings.get", AuthZLogHelper.AuthZPerfMonitors))
				{
					if (this.owaSegmentationSettings == null)
					{
						lock (this)
						{
							if (this.owaSegmentationSettings == null)
							{
								ExTraceGlobals.RunspaceConfigTracer.TraceDebug((long)this.GetHashCode(), "Creating a new configuration session to read OWA segmentation settings.");
								IConfigurationSession tenantOrTopologyConfigurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(true, ConsistencyMode.IgnoreInvalid, this.GetADSessionSettings(), 1809, "OwaSegmentationSettings", "f:\\15.00.1497\\sources\\dev\\Configuration\\src\\ObjectModel\\rbac\\ExchangeRunspaceConfiguration.cs");
								this.owaSegmentationSettings = OwaSegmentationSettings.GetInstance(tenantOrTopologyConfigurationSession, (ADObjectId)this.executingUser[ADUserSchema.OwaMailboxPolicy], this.OrganizationId);
							}
						}
					}
					result = this.owaSegmentationSettings;
				}
				return result;
			}
		}

		// Token: 0x170003B9 RID: 953
		// (get) Token: 0x06001343 RID: 4931 RVA: 0x0003FEB4 File Offset: 0x0003E0B4
		internal virtual IDictionary<ADObjectId, ManagementScope> ScopesCache
		{
			get
			{
				return this.allScopes;
			}
		}

		// Token: 0x170003BA RID: 954
		// (get) Token: 0x06001344 RID: 4932 RVA: 0x0003FEBC File Offset: 0x0003E0BC
		internal virtual ICollection<ExchangeRoleAssignment> RoleAssignments
		{
			get
			{
				return this.allRoleAssignments;
			}
		}

		// Token: 0x170003BB RID: 955
		// (get) Token: 0x06001345 RID: 4933 RVA: 0x0003FEC4 File Offset: 0x0003E0C4
		internal virtual bool IsDedicatedTenantAdmin
		{
			get
			{
				return this.isDedicatedTenantAdmin;
			}
		}

		// Token: 0x170003BC RID: 956
		// (get) Token: 0x06001346 RID: 4934 RVA: 0x0003FECC File Offset: 0x0003E0CC
		internal SerializedAccessToken SecurityAccessToken
		{
			get
			{
				return this.logonAccessToken;
			}
		}

		// Token: 0x170003BD RID: 957
		// (get) Token: 0x06001347 RID: 4935 RVA: 0x0003FED4 File Offset: 0x0003E0D4
		internal TroubleshootingContext TroubleshootingContext
		{
			get
			{
				return this.troubleshootingContext;
			}
		}

		// Token: 0x170003BE RID: 958
		// (get) Token: 0x06001348 RID: 4936 RVA: 0x0003FEDC File Offset: 0x0003E0DC
		internal ExchangeRunspaceConfigurationSettings ConfigurationSettings
		{
			get
			{
				return this.settings;
			}
		}

		// Token: 0x170003BF RID: 959
		// (get) Token: 0x06001349 RID: 4937 RVA: 0x0003FEE4 File Offset: 0x0003E0E4
		// (set) Token: 0x0600134A RID: 4938 RVA: 0x0003FEEC File Offset: 0x0003E0EC
		internal TypeTable TypeTable { get; set; }

		// Token: 0x170003C0 RID: 960
		// (get) Token: 0x0600134B RID: 4939 RVA: 0x0003FEF5 File Offset: 0x0003E0F5
		internal string IdentityName
		{
			get
			{
				return this.identityName;
			}
		}

		// Token: 0x170003C1 RID: 961
		// (get) Token: 0x0600134C RID: 4940 RVA: 0x0003FEFD File Offset: 0x0003E0FD
		// (set) Token: 0x0600134D RID: 4941 RVA: 0x0003FF05 File Offset: 0x0003E105
		internal bool Impersonated { get; private set; }

		// Token: 0x170003C2 RID: 962
		// (get) Token: 0x0600134E RID: 4942 RVA: 0x0003FF0E File Offset: 0x0003E10E
		// (set) Token: 0x0600134F RID: 4943 RVA: 0x0003FF16 File Offset: 0x0003E116
		internal bool OpenMailboxAsAdmin { get; private set; }

		// Token: 0x170003C3 RID: 963
		// (get) Token: 0x06001350 RID: 4944 RVA: 0x0003FF1F File Offset: 0x0003E11F
		internal bool PartnerMode
		{
			get
			{
				return !string.IsNullOrEmpty(this.ConfigurationSettings.TenantOrganization);
			}
		}

		// Token: 0x170003C4 RID: 964
		// (get) Token: 0x06001351 RID: 4945 RVA: 0x0003FF3C File Offset: 0x0003E13C
		internal virtual bool HasAdminRoles
		{
			get
			{
				if (this.allRoleTypes != null)
				{
					return this.allRoleTypes.Keys.Any((RoleType x) => ExchangeRole.IsAdminRole(x));
				}
				return false;
			}
		}

		// Token: 0x170003C5 RID: 965
		// (get) Token: 0x06001352 RID: 4946 RVA: 0x0003FF75 File Offset: 0x0003E175
		internal string AuthenticationType
		{
			get
			{
				return this.authenticationType;
			}
		}

		// Token: 0x06001353 RID: 4947 RVA: 0x00040010 File Offset: 0x0003E210
		private static ADObjectId GetRootOrgUSGContainerId(PartitionId partitionId)
		{
			ADObjectId orAdd;
			using (new MonitoredScope("ExchangeRunspaceConfiguration", "GetRootOrgUSGContainerId", AuthZLogHelper.AuthZPerfMonitors))
			{
				orAdd = ExchangeRunspaceConfiguration.rootOrgUSGContainers.GetOrAdd(partitionId.ForestFQDN, delegate(string param0)
				{
					IConfigurationSession configurationSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromAccountPartitionRootOrgScopeSet(partitionId), 1986, "GetRootOrgUSGContainerId", "f:\\15.00.1497\\sources\\dev\\Configuration\\src\\ObjectModel\\rbac\\ExchangeRunspaceConfiguration.cs");
					IRecipientSession recipientSession = DirectorySessionFactory.Default.CreateRootOrgRecipientSession(true, ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromAccountPartitionRootOrgScopeSet(partitionId), 1990, "GetRootOrgUSGContainerId", "f:\\15.00.1497\\sources\\dev\\Configuration\\src\\ObjectModel\\rbac\\ExchangeRunspaceConfiguration.cs");
					ADGroup adgroup = recipientSession.ResolveWellKnownGuid<ADGroup>(WellKnownGuid.ExSWkGuid, configurationSession.ConfigurationNamingContext);
					if (adgroup == null)
					{
						throw new ManagementObjectNotFoundException(DirectoryStrings.ExceptionADTopologyCannotFindWellKnownExchangeGroup);
					}
					return adgroup.Id.Parent;
				});
			}
			return orAdd;
		}

		// Token: 0x06001354 RID: 4948 RVA: 0x00040088 File Offset: 0x0003E288
		internal RBACContext GetRbacContext()
		{
			RBACContext result;
			if (this.DelegatedPrincipal != null)
			{
				result = new RBACContext(this.delegatedPrincipal, this.impersonatedUserAccessToken, this.roleTypeFilter, this.sortedRoleEntryFilter, this.logonUserRequiredRoleTypes, this.callerCheckedAccess);
			}
			else
			{
				result = new RBACContext(this.logonAccessToken, this.impersonatedUserAccessToken, this.roleTypeFilter, this.sortedRoleEntryFilter, this.logonUserRequiredRoleTypes, this.callerCheckedAccess);
			}
			return result;
		}

		// Token: 0x06001355 RID: 4949 RVA: 0x000400F8 File Offset: 0x0003E2F8
		public static bool TryStampQueryFilterOnManagementScope(ManagementScope managementScope)
		{
			QueryFilter queryFilter;
			string arg;
			if (!RBACHelper.TryConvertPowershellFilterIntoQueryFilter(managementScope.Filter, managementScope.ScopeRestrictionType, null, out queryFilter, out arg))
			{
				ExTraceGlobals.ADConfigTracer.TraceError<ADObjectId, string, string>((long)managementScope.GetHashCode(), "Scope {0} has invalid filter {1}:{2}", managementScope.Id, managementScope.Filter, arg);
				return false;
			}
			managementScope.QueryFilter = queryFilter;
			return true;
		}

		// Token: 0x06001356 RID: 4950 RVA: 0x0004014C File Offset: 0x0003E34C
		public static bool IsAllowedOrganizationForPartnerAccounts(OrganizationId organizationId)
		{
			if (organizationId != OrganizationId.ForestWideOrgId)
			{
				AuthZLogger.SafeAppendGenericError("ERC.IsAllowedOrganizationForPartnerAccounts", "User in Org " + organizationId.GetFriendlyName(), false);
				ExTraceGlobals.AccessDeniedTracer.TraceError<OrganizationId>(0L, "ERC.IsAllowedOrganizationForPartnerAccounts returns false because user belongs to a tenant organization {0}", organizationId);
				return false;
			}
			if (!Datacenter.IsMultiTenancyEnabled())
			{
				AuthZLogger.SafeAppendGenericError("ERC.IsAllowedOrganizationForPartnerAccounts", "Enterprise", false);
				ExTraceGlobals.AccessDeniedTracer.TraceError(0L, "ERC.IsAllowedOrganizationForPartnerAccounts returns false because it is an Enterprise installation");
				return false;
			}
			return true;
		}

		// Token: 0x06001357 RID: 4951 RVA: 0x000401C0 File Offset: 0x0003E3C0
		public InitialSessionState CreateInitialSessionState()
		{
			InitialSessionState result;
			using (new MonitoredScope("ExchangeRunspaceConfiguration", "CreateInitialSessionState", AuthZLogHelper.AuthZPerfMonitors))
			{
				ExTraceGlobals.PublicInstanceAPITracer.TraceDebug<string>(0L, "Entering ERC.CreateInitialSessionState({0})", this.identityName);
				InitialSessionState initialSessionState = InitialSessionStateBuilder.Build(this.combinedCmdlets, this.combinedScripts, this);
				ExTraceGlobals.PublicInstanceAPITracer.TraceDebug<string, int>((long)initialSessionState.GetHashCode(), "ERC.CreateInitialSessionState(identity,cmdlets) returns ISS for {0} with {1} commands", this.identityName, initialSessionState.Commands.Count);
				using (new MonitoredScope("ExchangeRunspaceConfiguration", "TraceOperationCompletedAndUpdateContext", AuthZLogHelper.AuthZPerfMonitors))
				{
					this.troubleshootingContext.TraceOperationCompletedAndUpdateContext();
				}
				result = initialSessionState;
			}
			return result;
		}

		// Token: 0x06001358 RID: 4952 RVA: 0x0004028C File Offset: 0x0003E48C
		public bool HasRoleOfType(RoleType roleType)
		{
			ExTraceGlobals.PublicInstanceAPITracer.TraceDebug<RoleType>((long)this.GetHashCode(), "Entering ERC.HasRoleOfType({0})", roleType);
			bool flag = this.allRoleTypes != null && this.allRoleTypes.Keys.Contains(roleType);
			this.TracePublicInstanceAPIResult(flag, "ERC.HasRoleOfType({0}) returns {1}", new object[]
			{
				roleType,
				flag
			});
			return flag;
		}

		// Token: 0x06001359 RID: 4953 RVA: 0x000402F4 File Offset: 0x0003E4F4
		public virtual bool IsCmdletAllowedInScope(string cmdletName, string[] paramNames, ScopeSet scopeSet)
		{
			if (string.IsNullOrEmpty(cmdletName))
			{
				throw new ArgumentNullException("cmdletName");
			}
			if (scopeSet == null)
			{
				throw new ArgumentNullException("scopeSet");
			}
			ExTraceGlobals.PublicInstanceAPITracer.TraceDebug<string, int>((long)this.GetHashCode(), "Entering ERC.IsCmdletAllowedInScope({0}, {1} params, ScopeSet)", cmdletName, (paramNames == null) ? 0 : paramNames.Length);
			int num;
			int num2;
			if (!this.LocateRoleEntriesForCmdlet(cmdletName, out num, out num2))
			{
				ExTraceGlobals.PublicInstanceAPITracer.TraceWarning<string, string>((long)this.GetHashCode(), "IsCmdletAllowedInScope({0}) returns false for user {1} because this cmdlet is not present in any of the assigned roles.", cmdletName, this.identityName);
				return false;
			}
			List<RoleEntry> list = new List<RoleEntry>(num2 - num + 1);
			for (int i = num; i <= num2; i++)
			{
				if (this.allRoleEntries[i].RoleAssignment.AllPresentScopesMatch(scopeSet))
				{
					list.Add(this.allRoleEntries[i].RoleEntry);
				}
			}
			if (list.Count == 0)
			{
				ExTraceGlobals.PublicInstanceAPITracer.TraceWarning<string, string>((long)this.GetHashCode(), "IsCmdletAllowedInScope({0}) returns false for user {1} because no role assignments with matching scopes were found.", cmdletName, this.identityName);
				return false;
			}
			bool flag = false;
			if (this.ApplyValidationRules && ValidationRuleFactory.HasApplicableValidationRules(cmdletName, this.executingUser))
			{
				if (scopeSet.RecipientReadScope is RbacScope && ((RbacScope)scopeSet.RecipientReadScope).ScopeType == ScopeType.Self)
				{
					flag = true;
				}
				flag = (flag || this.ContainsOnlySelfScopes(scopeSet.RecipientWriteScopes));
				ExTraceGlobals.PublicInstanceAPITracer.TraceDebug<string, bool>((long)this.GetHashCode(), "ERC.IsCmdletAllowedInScope({0}) isSelfScopePresent '{1}'.", cmdletName, flag);
			}
			if (!flag && paramNames.IsNullOrEmpty<string>())
			{
				ExTraceGlobals.PublicInstanceAPITracer.TraceDebug<string>((long)this.GetHashCode(), "ERC.IsCmdletAllowedInScope({0}) returns true because cmdlet was found and no params were requested", cmdletName);
				return true;
			}
			RoleEntry roleEntry = RoleEntry.MergeParameters(list);
			bool flag2 = true;
			if (flag)
			{
				ExTraceGlobals.PublicInstanceAPITracer.TraceDebug<string, RoleEntry>((long)this.GetHashCode(), "ERC.IsCmdletAllowedInScope({0}). MergedRoleEntry '{1}'.", cmdletName, roleEntry);
				if (roleEntry.Parameters.Count > 0)
				{
					roleEntry = this.TrimRoleEntryParametersWithValidationRules(roleEntry, false);
					flag2 = (roleEntry.Parameters.Count > 0);
				}
			}
			if (!paramNames.IsNullOrEmpty<string>())
			{
				flag2 = roleEntry.ContainsAllParameters(paramNames);
			}
			this.TracePublicInstanceAPIResult(flag2, "ERC.IsCmdletAllowedInScope({0}) returns {1} after successfully locating cmdlet in user's role and matching parameters", new object[]
			{
				cmdletName,
				flag2
			});
			return flag2;
		}

		// Token: 0x0600135A RID: 4954 RVA: 0x000404FC File Offset: 0x0003E6FC
		public virtual bool IsCmdletAllowedInScope(string cmdletName, string[] paramNames, ADRawEntry adObject, ScopeLocation scopeLocation)
		{
			if (string.IsNullOrEmpty(cmdletName))
			{
				throw new ArgumentNullException("cmdletName");
			}
			if (adObject == null)
			{
				throw new ArgumentNullException("adObject");
			}
			if (scopeLocation != ScopeLocation.RecipientRead && scopeLocation != ScopeLocation.RecipientWrite)
			{
				throw new NotSupportedException("Only ScopeLocation.RecipientRead and ScopeLocation.RecipientWrite is supported!");
			}
			ExTraceGlobals.PublicInstanceAPITracer.TraceDebug<string, int, string>((long)this.GetHashCode(), "Entering ERC.IsCmdletAllowedInScope({0}, {1} params, {2}, ScopeLocation)", cmdletName, paramNames.IsNullOrEmpty<string>() ? 0 : paramNames.Length, adObject.GetDistinguishedNameOrName());
			RoleEntry roleEntry = null;
			ScopeSet scopeSet = this.CalculateScopeSetForExchangeCmdlet(cmdletName, paramNames, this.OrganizationId, null, out roleEntry);
			if (scopeSet == null)
			{
				ExTraceGlobals.PublicInstanceAPITracer.TraceWarning<string, string>((long)this.GetHashCode(), "ERC.IsCmdletAllowedInScope({0}, {1}) returns FALSE because no valid scope has been calculated from the specified cmdlet and parameters combination.", cmdletName, adObject.GetDistinguishedNameOrName());
				return false;
			}
			switch (scopeLocation)
			{
			case ScopeLocation.RecipientRead:
				if (!ADSession.IsWithinScope(adObject, scopeSet.RecipientReadScope))
				{
					ExTraceGlobals.PublicInstanceAPITracer.TraceWarning((long)this.GetHashCode(), "ERC.IsCmdletAllowedInScope({0}, {1}) returns FALSE because {2} is not in the valid read scope for user {3}.", new object[]
					{
						cmdletName,
						(ADObjectId)adObject[ADObjectSchema.Id],
						(ADObjectId)adObject[ADObjectSchema.Id],
						this.identityName
					});
					return false;
				}
				break;
			case ScopeLocation.RecipientWrite:
			{
				ADScopeException ex;
				if (!ADSession.TryVerifyIsWithinScopes(adObject, scopeSet.RecipientReadScope, scopeSet.RecipientWriteScopes, scopeSet.ExclusiveRecipientScopes, scopeSet.ValidationRules, false, out ex))
				{
					ExTraceGlobals.PublicInstanceAPITracer.TraceWarning((long)this.GetHashCode(), "ERC.IsCmdletAllowedInScope({0}, {1}) returns FALSE because {2} is not in the valid read or write scope for user {3}. ADScopeException {4} is caught.", new object[]
					{
						cmdletName,
						(ADObjectId)adObject[ADObjectSchema.Id],
						(ADObjectId)adObject[ADObjectSchema.Id],
						this.identityName,
						ex
					});
					return false;
				}
				break;
			}
			default:
				throw new NotSupportedException("Only ScopeLocation.RecipientRead and ScopeLocation.RecipientWrite is supported!");
			}
			if (paramNames.IsNullOrEmpty<string>() && null != roleEntry && roleEntry.Parameters.Count != 0 && this.TrimRoleEntryParametersWithValidationRules(roleEntry, adObject, false).Parameters.Count == 0)
			{
				ExTraceGlobals.PublicInstanceAPITracer.TraceWarning<string, string>((long)this.GetHashCode(), "ERC.IsCmdletAllowedInScope({0}, {1}) returns FALSE. All possible parameters of '{0}' violates a Validation Rule on object '{1}'.", cmdletName, adObject.GetDistinguishedNameOrName());
				return false;
			}
			ExTraceGlobals.PublicInstanceAPITracer.TraceDebug<string, ADObjectId>((long)this.GetHashCode(), "ERC.IsCmdletAllowedInScope({0}, {1}) returning TRUE.", cmdletName, (ADObjectId)adObject[ADObjectSchema.Id]);
			return true;
		}

		// Token: 0x0600135B RID: 4955 RVA: 0x00040728 File Offset: 0x0003E928
		public ICollection<string> GetAllowedParamsForSetCmdlet(string cmdletName, ADRawEntry adObject, ScopeLocation scopeLocation)
		{
			if (string.IsNullOrEmpty(cmdletName))
			{
				throw new ArgumentNullException("cmdletName");
			}
			ExTraceGlobals.PublicInstanceAPITracer.TraceDebug<string, string, ScopeLocation>((long)this.GetHashCode(), "Entering ERC.GetAllowedParamsForSetCmdlet({0}, {1}, ScopeLocation {2})", cmdletName, (adObject != null) ? ((ADObjectId)adObject[ADObjectSchema.Id]).ToString() : string.Empty, scopeLocation);
			int num;
			int num2;
			if (!this.LocateRoleEntriesForCmdlet(cmdletName, out num, out num2))
			{
				ExTraceGlobals.PublicInstanceAPITracer.TraceWarning<string, string>((long)this.GetHashCode(), "GetAllowedParamsForSetCmdlet({0}) returns false for user {1} because this cmdlet is not present in any of the assigned roles.", cmdletName, this.identityName);
				return null;
			}
			List<RoleEntry> list = new List<RoleEntry>(num2 - num + 1);
			int i = num;
			while (i <= num2)
			{
				bool flag = true;
				if (adObject == null)
				{
					goto IL_156;
				}
				flag = false;
				RoleAssignmentScopeSet effectiveScopeSet = this.allRoleEntries[i].RoleAssignment.GetEffectiveScopeSet(this.allScopes, this.UserAccessToken);
				if (effectiveScopeSet != null)
				{
					ScopeSet scopeSet = new ScopeSet(effectiveScopeSet.RecipientReadScope, new ADScopeCollection[]
					{
						new ADScopeCollection(new ADScope[]
						{
							effectiveScopeSet.RecipientWriteScope
						})
					}, null, null);
					switch (scopeLocation)
					{
					case ScopeLocation.RecipientRead:
						if (ADSession.IsWithinScope(adObject, scopeSet.RecipientReadScope))
						{
							flag = true;
							goto IL_156;
						}
						goto IL_156;
					case ScopeLocation.RecipientWrite:
					{
						ADScopeException ex;
						if (ADSession.TryVerifyIsWithinScopes(adObject, scopeSet.RecipientReadScope, scopeSet.RecipientWriteScopes, scopeSet.ExclusiveRecipientScopes, scopeSet.ValidationRules, false, out ex))
						{
							flag = true;
							goto IL_156;
						}
						goto IL_156;
					}
					default:
						throw new NotSupportedException("Only ScopeLocation.DomainRead and ScopeLocation.DomainWrite is supported!");
					}
				}
				IL_171:
				i++;
				continue;
				IL_156:
				if (flag)
				{
					list.Add(this.allRoleEntries[i].RoleEntry);
					goto IL_171;
				}
				goto IL_171;
			}
			if (list.Count == 0)
			{
				ExTraceGlobals.PublicInstanceAPITracer.TraceWarning<string, string>((long)this.GetHashCode(), "GetAllowedParamsForSetCmdlet({0}) returns false for user {1} because no role assignments with matching scopes were found.", cmdletName, this.identityName);
				return null;
			}
			return RoleEntry.MergeParameters(list).Parameters;
		}

		// Token: 0x0600135C RID: 4956 RVA: 0x000408E4 File Offset: 0x0003EAE4
		public virtual bool IsApplicationImpersonationAllowed(ADUser impersonatedADUserObject)
		{
			if (impersonatedADUserObject == null)
			{
				throw new ArgumentNullException("impersonatedADUserObject");
			}
			ExTraceGlobals.PublicInstanceAPITracer.TraceDebug<ObjectId>((long)this.GetHashCode(), "Entering ERC.IsApplicationImpersonationAllowed({0})", impersonatedADUserObject.Identity);
			bool flag;
			if (this.PartnerMode)
			{
				flag = this.OrganizationId.Equals(impersonatedADUserObject.OrganizationId);
			}
			else
			{
				flag = this.IsCmdletAllowedInScope("Impersonate-ExchangeUser", ExchangeRunspaceConfiguration.emptyArray, impersonatedADUserObject, ScopeLocation.RecipientWrite);
			}
			if (flag && this.ensureTargetIsMemberOfRIMMailboxUsersGroup && this.OrganizationId != null && this.OrganizationId.ConfigurationUnit != null)
			{
				IRecipientSession recipientSession = (IRecipientSession)TaskHelper.UnderscopeSessionToOrganization(this.recipientSession, this.OrganizationId, true);
				flag = recipientSession.IsMemberOfGroupByWellKnownGuid(WellKnownGuid.RIMMailboxUsersGroupGuid, this.OrganizationId.ConfigurationUnit.DistinguishedName, impersonatedADUserObject.Id);
			}
			this.TracePublicInstanceAPIResult(flag, "ERC.IsApplicationImpersonationAllowed({0}) returns {1} in {2}Partner mode", new object[]
			{
				impersonatedADUserObject.Identity,
				flag,
				this.PartnerMode ? "" : "non-"
			});
			return flag;
		}

		// Token: 0x0600135D RID: 4957 RVA: 0x000409EC File Offset: 0x0003EBEC
		public bool IsVerboseEnabled(string cmdletName)
		{
			bool result;
			lock (this.loadRoleCmdletInfoSyncRoot)
			{
				result = this.verboseEnabledCmdlets.Contains(cmdletName);
			}
			return result;
		}

		// Token: 0x0600135E RID: 4958 RVA: 0x00040A34 File Offset: 0x0003EC34
		public bool IsDebugEnabled(string cmdletName)
		{
			bool result;
			lock (this.loadRoleCmdletInfoSyncRoot)
			{
				result = this.debugEnabledCmdlets.Contains(cmdletName);
			}
			return result;
		}

		// Token: 0x0600135F RID: 4959 RVA: 0x00040A7C File Offset: 0x0003EC7C
		internal bool TryGetGroupAccountsForDelegatedPrincipal(DelegatedPrincipal delegatedPrincipal, out List<ADObjectId> groups, out OrganizationId orgId, out bool ensureTargetIsMemberOfRIMMailboxUsersGroup)
		{
			bool result;
			using (new MonitoredScope("ExchangeRunspaceConfiguration", "TryGetGroupAccountsForDelegatedPrincipal", AuthZLogHelper.AuthZPerfMonitors))
			{
				ensureTargetIsMemberOfRIMMailboxUsersGroup = false;
				ADSessionSettings sessionSettings = null;
				groups = new List<ADObjectId>();
				orgId = null;
				if (delegatedPrincipal == null)
				{
					result = false;
				}
				else
				{
					string[] roles = delegatedPrincipal.Roles;
					try
					{
						sessionSettings = ADSessionSettings.FromAllTenantsPartitionId(ADAccountPartitionLocator.GetPartitionIdByAcceptedDomainName(delegatedPrincipal.DelegatedOrganization));
					}
					catch (CannotResolveTenantNameException arg)
					{
						AuthZLogger.SafeAppendGenericError("ERC.TryGetGroupAccountsForDelegatedPrincipal", delegatedPrincipal.DelegatedOrganization + " " + arg, false);
						ExTraceGlobals.RunspaceConfigTracer.TraceError<string>((long)delegatedPrincipal.GetHashCode(), "The target organization {0} for a delegated account should be correctly resolved at this stage.", delegatedPrincipal.DelegatedOrganization);
						return false;
					}
					ITenantConfigurationSession tenantConfigurationSession = DirectorySessionFactory.Default.CreateTenantConfigurationSession(true, ConsistencyMode.IgnoreInvalid, sessionSettings, 2670, "TryGetGroupAccountsForDelegatedPrincipal", "f:\\15.00.1497\\sources\\dev\\Configuration\\src\\ObjectModel\\rbac\\ExchangeRunspaceConfiguration.cs");
					ExchangeConfigurationUnit exchangeConfigurationUnitByNameOrAcceptedDomain = tenantConfigurationSession.GetExchangeConfigurationUnitByNameOrAcceptedDomain(delegatedPrincipal.DelegatedOrganization);
					if (exchangeConfigurationUnitByNameOrAcceptedDomain == null)
					{
						result = false;
					}
					else
					{
						orgId = exchangeConfigurationUnitByNameOrAcceptedDomain.OrganizationId;
						this.recipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromOrganizationIdWithoutRbacScopes(ADSystemConfigurationSession.GetRootOrgContainerIdForLocalForest(), exchangeConfigurationUnitByNameOrAcceptedDomain.OrganizationId, exchangeConfigurationUnitByNameOrAcceptedDomain.OrganizationId, false), 2690, "TryGetGroupAccountsForDelegatedPrincipal", "f:\\15.00.1497\\sources\\dev\\Configuration\\src\\ObjectModel\\rbac\\ExchangeRunspaceConfiguration.cs");
						LinkedPartnerGroupInformation[] array = new LinkedPartnerGroupInformation[roles.Length];
						for (int i = 0; i < array.Length; i++)
						{
							array[i] = new LinkedPartnerGroupInformation();
							array[i].LinkedPartnerOrganizationId = delegatedPrincipal.UserOrganizationId;
							array[i].LinkedPartnerGroupId = roles[i];
						}
						ADSessionSettings adsessionSettings = ADSessionSettings.FromOrganizationIdWithoutRbacScopes(ADSystemConfigurationSession.GetRootOrgContainerIdForLocalForest(), exchangeConfigurationUnitByNameOrAcceptedDomain.OrganizationId, exchangeConfigurationUnitByNameOrAcceptedDomain.OrganizationId, false);
						adsessionSettings.ForceADInTemplateScope = true;
						ITenantRecipientSession tenantRecipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(ConsistencyMode.IgnoreInvalid, adsessionSettings, 2713, "TryGetGroupAccountsForDelegatedPrincipal", "f:\\15.00.1497\\sources\\dev\\Configuration\\src\\ObjectModel\\rbac\\ExchangeRunspaceConfiguration.cs") as ITenantRecipientSession;
						if (tenantRecipientSession != null)
						{
							foreach (Result<ADRawEntry> result2 in tenantRecipientSession.ReadMultipleByLinkedPartnerId(array, ExchangeRunspaceConfiguration.delegatedGroupInfo))
							{
								if (result2.Data != null)
								{
									groups.Add((ADObjectId)result2.Data[ADObjectSchema.Id]);
									ADObjectId[] nestedSecurityGroupMembership = ExchangeRunspaceConfiguration.GetNestedSecurityGroupMembership(result2.Data, tenantRecipientSession, AssignmentMethod.SecurityGroup | AssignmentMethod.RoleGroup);
									if (nestedSecurityGroupMembership != null && nestedSecurityGroupMembership.Length > 0)
									{
										groups.AddRange(nestedSecurityGroupMembership);
									}
								}
								else
								{
									ExTraceGlobals.RunspaceConfigTracer.TraceDebug<LinkedPartnerGroupInformation[], string, string>(0L, "Failed to find delegated group '{0}' for user '{1}' with error '{2}'", array, delegatedPrincipal.UserId, (result2.Error != null) ? result2.Error.ToString() : string.Empty);
								}
							}
						}
						if (array.Length > 1)
						{
							groups = groups.Distinct(ADObjectIdEqualityComparer.Instance).ToList<ADObjectId>();
						}
						DNWithBinary dnwithBinary = TaskHelper.FindWellKnownObjectEntry(exchangeConfigurationUnitByNameOrAcceptedDomain.OtherWellKnownObjects, WellKnownGuid.RIMMailboxAdminsGroupGuid);
						if (dnwithBinary != null)
						{
							ADObjectId item = new ADObjectId(dnwithBinary.DistinguishedName);
							ensureTargetIsMemberOfRIMMailboxUsersGroup = groups.Contains(item);
						}
						result = true;
					}
				}
			}
			return result;
		}

		// Token: 0x06001360 RID: 4960 RVA: 0x00040D74 File Offset: 0x0003EF74
		internal static ADRawEntry TryFindComputer(SecurityIdentifier userSid)
		{
			ITopologyConfigurationSession topologyConfigurationSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromRootOrgScopeSet(), 2768, "TryFindComputer", "f:\\15.00.1497\\sources\\dev\\Configuration\\src\\ObjectModel\\rbac\\ExchangeRunspaceConfiguration.cs");
			topologyConfigurationSession.UseConfigNC = false;
			topologyConfigurationSession.UseGlobalCatalog = true;
			return topologyConfigurationSession.FindComputerBySid(userSid);
		}

		// Token: 0x06001361 RID: 4961 RVA: 0x00040DB8 File Offset: 0x0003EFB8
		internal static bool TryFindLinkedRoleGroupsBySidList(IRecipientSession recipientSession, ICollection<SecurityIdentifier> sids, string identityName, out List<ADObjectId> roleGroups)
		{
			bool result;
			using (new MonitoredScope("ExchangeRunspaceConfiguration", "TryFindLinkedRoleGroupsBySidList", AuthZLogHelper.AuthZPerfMonitors))
			{
				if (sids == null)
				{
					roleGroups = new List<ADObjectId>(0);
				}
				else
				{
					roleGroups = new List<ADObjectId>(sids.Count);
					foreach (SecurityIdentifier sId in sids)
					{
						IRootOrganizationRecipientSession rootOrganizationRecipientSession = DirectorySessionFactory.Default.CreateRootOrgRecipientSession(ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromAccountPartitionRootOrgScopeSet(recipientSession.SessionSettings.PartitionId), 2805, "TryFindLinkedRoleGroupsBySidList", "f:\\15.00.1497\\sources\\dev\\Configuration\\src\\ObjectModel\\rbac\\ExchangeRunspaceConfiguration.cs");
						ADObjectId rootOrgUSGContainerId = ExchangeRunspaceConfiguration.GetRootOrgUSGContainerId(recipientSession.SessionSettings.PartitionId);
						IEnumerable<ADGroup> enumerable = rootOrganizationRecipientSession.FindRoleGroupsByForeignGroupSid(rootOrgUSGContainerId, sId);
						foreach (ADGroup adgroup in enumerable)
						{
							ExTraceGlobals.RunspaceConfigTracer.TraceDebug<ObjectId, string>(0L, "Found linked RoleGroup '{0}' for '{1}'", adgroup.Identity, identityName);
							roleGroups.Add(adgroup.Id);
							ADObjectId[] nestedSecurityGroupMembership = ExchangeRunspaceConfiguration.GetNestedSecurityGroupMembership(adgroup, recipientSession, AssignmentMethod.SecurityGroup | AssignmentMethod.RoleGroup);
							if (nestedSecurityGroupMembership != null && nestedSecurityGroupMembership.Length > 0)
							{
								roleGroups.AddRange(nestedSecurityGroupMembership);
							}
						}
					}
					roleGroups = roleGroups.Distinct(ADObjectIdEqualityComparer.Instance).ToList<ADObjectId>();
				}
				result = (roleGroups.Count != 0);
			}
			return result;
		}

		// Token: 0x06001362 RID: 4962 RVA: 0x00040F5C File Offset: 0x0003F15C
		internal void LoadRoleCmdletInfo()
		{
			lock (this.loadRoleCmdletInfoSyncRoot)
			{
				List<ADObjectId> implicitRoleIds = new List<ADObjectId>(0);
				if (this.tokenSids != null)
				{
					ExchangeRunspaceConfiguration.TryFindLinkedRoleGroupsBySidList(this.recipientSession, this.tokenSids, this.identityName, out implicitRoleIds);
				}
				this.LoadRoleCmdletInfo((this.organizationId == null) ? null : this.organizationId.OrganizationalUnit.Name, null, null, null, implicitRoleIds);
				this.RefreshCombinedCmdletsAndScripts();
				this.RefreshVerboseDebugEnabledCmdlets(this.combinedCmdlets);
			}
		}

		// Token: 0x06001363 RID: 4963 RVA: 0x00040FFC File Offset: 0x0003F1FC
		internal ProvisioningBroker GetProvisioningBroker()
		{
			ProvisioningBroker provisioningBroker = this.provisioningBroker;
			if (provisioningBroker == null)
			{
				lock (this.provisioningBrokerSyncRoot)
				{
					if (this.provisioningBroker == null)
					{
						this.provisioningBroker = new ProvisioningBroker();
					}
					provisioningBroker = this.provisioningBroker;
				}
			}
			return provisioningBroker;
		}

		// Token: 0x06001364 RID: 4964 RVA: 0x0004105C File Offset: 0x0003F25C
		internal virtual ScopeSet CalculateScopeSetForExchangeCmdlet(string exchangeCmdletName, IList<string> parameters, OrganizationId organizationId, Task.ErrorLoggerDelegate writeError)
		{
			RoleEntry roleEntry;
			return this.CalculateScopeSetForExchangeCmdlet(exchangeCmdletName, parameters, organizationId, writeError, out roleEntry);
		}

		// Token: 0x06001365 RID: 4965 RVA: 0x00041078 File Offset: 0x0003F278
		protected virtual ScopeSet CalculateScopeSetForExchangeCmdlet(string exchangeCmdletName, IList<string> parameters, OrganizationId organizationId, Task.ErrorLoggerDelegate writeError, out RoleEntry exchangeCmdlet)
		{
			ScopeSet result;
			using (new MonitoredScope("ExchangeRunspaceConfiguration", "CalculateScopeSetForExchangeCmdlet", AuthZLogHelper.AuthZPerfMonitors))
			{
				if (string.IsNullOrEmpty(exchangeCmdletName))
				{
					throw new ArgumentNullException("exchangeCmdletName");
				}
				if (parameters == null)
				{
					throw new ArgumentNullException("parameters");
				}
				ExTraceGlobals.PublicInstanceAPITracer.TraceDebug<string>((long)this.GetHashCode(), "Entering ERC.CalculateScopeSetForExchangeCmdlet({0})", exchangeCmdletName);
				int num = 1;
				int num2 = -1;
				string text = null;
				bool flag = false;
				exchangeCmdlet = null;
				if (exchangeCmdletName.Equals("Impersonate-ExchangeUser", StringComparison.OrdinalIgnoreCase))
				{
					text = exchangeCmdletName;
					flag = this.LocateRoleEntriesForCmdlet(text, out num, out num2);
				}
				else
				{
					foreach (string str in this.snapinCollection)
					{
						text = str + "\\" + exchangeCmdletName;
						this.FaultInjection_ByPassRBACTestSnapinCheck(ref text, exchangeCmdletName);
						flag = this.LocateRoleEntriesForCmdlet(text, out num, out num2);
						if (flag)
						{
							break;
						}
					}
				}
				if (!flag)
				{
					ExTraceGlobals.AccessDeniedTracer.TraceError<string, object, string>((long)this.GetHashCode(), "CalculateScopeSetForExchangeCmdlet({0}) returns null (NO SCOPE) because this cmdlet is not in any roles assigned to user {1} ({2}).", exchangeCmdletName, this.executingUser[ADRecipientSchema.PrimarySmtpAddress], this.identityName);
					if (writeError != null)
					{
						AuthZLogger.SafeAppendGenericError("ERC.CalculateScopeSetForExchangeCmdlet", exchangeCmdletName + " Not Allowed for " + this.IdentityName, false);
						TaskLogger.LogRbacEvent(TaskEventLogConstants.Tuple_CmdletAccessDenied_InvalidCmdlet, this.IdentityName + exchangeCmdletName, new object[]
						{
							exchangeCmdletName,
							this.IdentityName
						});
						writeError(new CmdletAccessDeniedException(Strings.NoRoleEntriesFound(exchangeCmdletName)), (ExchangeErrorCategory)18, null);
					}
					result = null;
				}
				else
				{
					List<RoleEntryInfo> list = new List<RoleEntryInfo>(num2 - num + 1);
					bool flag2 = false;
					using (new MonitoredScope("ExchangeRunspaceConfiguration", "ValidationRuleFactory.HasApplicableValidationRules", AuthZLogHelper.AuthZPerfMonitors))
					{
						flag2 = (this.ApplyValidationRules && ValidationRuleFactory.HasApplicableValidationRules(text, this.executingUser));
					}
					HashSet<string> hashSet = null;
					if (parameters.Count == 0 && flag2)
					{
						hashSet = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
					}
					int i = num;
					while (i <= num2)
					{
						if (parameters.Count != 0 && !this.allRoleEntries[i].RoleEntry.ContainsAnyParameter(parameters))
						{
							goto IL_281;
						}
						RoleAssignmentScopeSet effectiveScopeSet = this.allRoleEntries[i].RoleAssignment.GetEffectiveScopeSet(this.allScopes, this.UserAccessToken);
						if (effectiveScopeSet != null)
						{
							list.Add(this.allRoleEntries[i]);
							this.allRoleEntries[i].ScopeSet = effectiveScopeSet;
							goto IL_281;
						}
						ExTraceGlobals.AccessDeniedTracer.TraceWarning<string, ADObjectId>((long)this.GetHashCode(), "CalculateScopeSetForExchangeCmdlet({0}) skips role assignment {1} because the required scope objects were not found.", exchangeCmdletName, this.allRoleEntries[i].RoleAssignment.Id);
						IL_2CF:
						i++;
						continue;
						IL_281:
						if (hashSet != null)
						{
							foreach (string item in this.allRoleEntries[i].RoleEntry.Parameters)
							{
								hashSet.Add(item);
							}
							goto IL_2CF;
						}
						goto IL_2CF;
					}
					if (list.Count == 0)
					{
						ExTraceGlobals.AccessDeniedTracer.TraceError<string, string>((long)this.GetHashCode(), "CalculateScopeSetForExchangeCmdlet({0}) returns null (NO SCOPE) for user {1} because no role assignments with matching parameters were found.", exchangeCmdletName, this.identityName);
						if (writeError != null)
						{
							AuthZLogger.SafeAppendGenericError("ERC.CalculateScopeSetForExchangeCmdlet", exchangeCmdletName + " Some of parameters NOT Allowed for " + this.IdentityName, false);
							TaskLogger.LogRbacEvent(TaskEventLogConstants.Tuple_CmdletAccessDenied_InvalidParameter, this.IdentityName + exchangeCmdletName, new object[]
							{
								exchangeCmdletName,
								this.IdentityName
							});
							writeError(new CmdletAccessDeniedException(Strings.NoRoleEntriesWithParametersFound(exchangeCmdletName)), (ExchangeErrorCategory)18, null);
						}
						result = null;
					}
					else
					{
						ScopeType[] maximumScopesCommonForAllParams = new ScopeType[]
						{
							ScopeType.None,
							ScopeType.None,
							ScopeType.None,
							ScopeType.None
						};
						ScopeType[,] maxParameterScopes = new ScopeType[(parameters.Count == 0) ? 1 : parameters.Count, 4];
						List<RoleAssignmentScopeSet>[] array = this.CalculateScopesForEachParameter(exchangeCmdletName, parameters, organizationId, writeError, num, num2, maximumScopesCommonForAllParams, maxParameterScopes);
						if (array == null)
						{
							result = null;
						}
						else
						{
							ScopeSet scopeSet = this.AggregateScopes(organizationId, array, maximumScopesCommonForAllParams, maxParameterScopes);
							if (flag2)
							{
								exchangeCmdlet = list[0].RoleEntry.Clone((parameters.Count == 0) ? hashSet.ToList<string>() : parameters);
							}
							IList<ValidationRule> list2 = this.ApplyValidationRules ? ValidationRuleFactory.GetApplicableValidationRules(text, parameters, this.executingUser) : null;
							if (list2 != null && list2.Count > 0)
							{
								ExTraceGlobals.PublicInstanceAPITracer.TraceDebug<string, int>((long)this.GetHashCode(), "ERC.CalculateScopeSetForExchangeCmdlet({0}) Adding {1} 'ValidationRules", exchangeCmdletName, list2.Count);
								List<ValidationRule> list3 = (List<ValidationRule>)scopeSet.ValidationRules;
								list3.AddRange(list2);
							}
							ExTraceGlobals.PublicInstanceAPITracer.TraceDebug<string, ScopeSet>((long)this.GetHashCode(), "ERC.CalculateScopeSetForExchangeCmdlet({0}) returns ScopeSet {1}", exchangeCmdletName, scopeSet);
							result = scopeSet;
						}
					}
				}
			}
			return result;
		}

		// Token: 0x06001366 RID: 4966 RVA: 0x00041574 File Offset: 0x0003F774
		internal static bool IsFeatureValidOnObject(string featureName, ADObject targetObject)
		{
			if (targetObject == null)
			{
				throw new ArgumentNullException("targetObject");
			}
			List<ValidationRule> validationRulesByFeature = ValidationRuleFactory.GetValidationRulesByFeature(featureName);
			return validationRulesByFeature == null || validationRulesByFeature.Count == 0 || ExchangeRunspaceConfiguration.EvaluateRulesOnObject(validationRulesByFeature, targetObject);
		}

		// Token: 0x06001367 RID: 4967 RVA: 0x000415AC File Offset: 0x0003F7AC
		internal static bool IsCmdletValidOnObject(string cmdletFullName, IList<string> parameters, ADObject targetObject)
		{
			if (targetObject == null)
			{
				throw new ArgumentNullException("targetObject");
			}
			IList<ValidationRule> applicableValidationRules = ValidationRuleFactory.GetApplicableValidationRules(cmdletFullName, parameters, ValidationRuleSkus.All);
			return ExchangeRunspaceConfiguration.EvaluateRulesOnObject(applicableValidationRules, targetObject);
		}

		// Token: 0x06001368 RID: 4968 RVA: 0x000415DC File Offset: 0x0003F7DC
		private static bool EvaluateRulesOnObject(IList<ValidationRule> rules, ADObject targetObject)
		{
			if (targetObject == null)
			{
				throw new ArgumentNullException("targetObject");
			}
			if (rules != null)
			{
				ExTraceGlobals.AccessCheckTracer.TraceDebug<string>((long)targetObject.GetHashCode(), "ERC.EvaluateRulesOnObject({0}) Validating rules.", targetObject.Identity.ToString());
				RuleValidationException ex = null;
				foreach (ValidationRule validationRule in rules)
				{
					if (!validationRule.TryValidate(targetObject, out ex))
					{
						ExTraceGlobals.AccessCheckTracer.TraceDebug<string, string>((long)targetObject.GetHashCode(), "ERC.EvaluateRulesOnObject({0}) returns false. RuleException '{1}'", validationRule.Name, ex.Message);
						return false;
					}
				}
			}
			ExTraceGlobals.AccessCheckTracer.TraceDebug<string>((long)targetObject.GetHashCode(), "ERC.EvaluateRulesOnObject({0}) returns true.", targetObject.ToString());
			return true;
		}

		// Token: 0x06001369 RID: 4969 RVA: 0x000416A4 File Offset: 0x0003F8A4
		internal void SwitchCurrentPartnerOrganizationAndReloadRoleCmdletInfo(string organizationName)
		{
			if (!this.PartnerMode)
			{
				throw new InvalidOperationException();
			}
			if (string.IsNullOrEmpty(organizationName))
			{
				throw new ArgumentException("organizationName");
			}
			IConfigurationSession tenantOrTopologyConfigurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromRootOrgScopeSet(), 3350, "SwitchCurrentPartnerOrganizationAndReloadRoleCmdletInfo", "f:\\15.00.1497\\sources\\dev\\Configuration\\src\\ObjectModel\\rbac\\ExchangeRunspaceConfiguration.cs");
			Dictionary<ADObjectId, ManagementScope> userAllScopes = new Dictionary<ADObjectId, ManagementScope>(this.allScopes);
			this.ReadAndCheckAllScopes(tenantOrTopologyConfigurationSession, userAllScopes, organizationName);
		}

		// Token: 0x0600136A RID: 4970 RVA: 0x00041708 File Offset: 0x0003F908
		internal bool IsScriptInUserRole(string scriptFullPath)
		{
			if (string.IsNullOrEmpty(scriptFullPath))
			{
				return false;
			}
			string scriptName;
			if (scriptFullPath.StartsWith(ConfigurationContext.Setup.RemoteScriptPath))
			{
				scriptName = scriptFullPath.Substring(ConfigurationContext.Setup.RemoteScriptPath.Length + 1);
			}
			else
			{
				if (!scriptFullPath.StartsWith(ConfigurationContext.Setup.TorusRemoteScriptPath))
				{
					return false;
				}
				scriptName = scriptFullPath.Substring(ConfigurationContext.Setup.TorusRemoteScriptPath.Length + 1);
			}
			return this.LocateRoleEntriesForScript(scriptName);
		}

		// Token: 0x0600136B RID: 4971 RVA: 0x0004176C File Offset: 0x0003F96C
		internal ADObjectId[] GetNestedSecurityGroupMembership()
		{
			return this.universalSecurityGroupsCache;
		}

		// Token: 0x0600136C RID: 4972 RVA: 0x00041774 File Offset: 0x0003F974
		internal void RefreshProvisioningBroker()
		{
			lock (this.provisioningBrokerSyncRoot)
			{
				this.provisioningBroker = null;
			}
		}

		// Token: 0x0600136D RID: 4973 RVA: 0x000417B8 File Offset: 0x0003F9B8
		protected virtual ICollection<SecurityIdentifier> GetGroupAccountsSIDs(IIdentity logonIdentity)
		{
			ICollection<SecurityIdentifier> collection = null;
			try
			{
				collection = logonIdentity.GetGroupAccountsSIDs();
				IdentityCache<ICollection<SecurityIdentifier>>.Current.Add(logonIdentity, collection);
			}
			catch (AuthzException ex)
			{
				if (!IdentityCache<ICollection<SecurityIdentifier>>.Current.TryGetValue(logonIdentity, out collection))
				{
					ExTraceGlobals.AccessDeniedTracer.TraceError<string, AuthzException>(0L, "Call to NativeMethods.AuthzInitializeContextFromSid() failed when initializing the ClientSecurityContext for user {0}. Exception: {1}", this.identityName, ex);
					AuthZLogger.SafeAppendGenericError("ERC.GetGroupAccountsSIDs", ex, new Func<Exception, bool>(KnownException.IsUnhandledException));
					TaskLogger.LogRbacEvent(TaskEventLogConstants.Tuple_AccessDenied_NativeCallFailed, null, new object[]
					{
						this.identityName,
						ex
					});
					throw new CmdletAccessDeniedException(Strings.ErrorManagementObjectNotFound(this.identityName));
				}
				AuthZLogger.SafeAppendGenericInfo("ERC.GetGroupAccountsSIDs", "Recovered from AuthzException");
			}
			return collection;
		}

		// Token: 0x0600136E RID: 4974 RVA: 0x00041874 File Offset: 0x0003FA74
		protected virtual SerializedAccessToken PopulateGroupMemberships(IIdentity identity)
		{
			SerializedAccessToken serializedAccessToken = null;
			if (!this.IsPowershellProcess())
			{
				try
				{
					serializedAccessToken = identity.GetAccessToken();
					IdentityCache<SerializedAccessToken>.Current.Add(identity, serializedAccessToken);
				}
				catch (AuthzException ex)
				{
					if (!IdentityCache<SerializedAccessToken>.Current.TryGetValue(identity, out serializedAccessToken))
					{
						ExTraceGlobals.AccessDeniedTracer.TraceError<string, AuthzException>(0L, "Call to NativeMethods.AuthzInitializeContextFromSid() failed when initializing the ClientSecurityContext for user {0}. Exception: {1}", this.identityName, ex);
						AuthZLogger.SafeAppendGenericError("ERC.PopulateGroupMemberships", ex, new Func<Exception, bool>(KnownException.IsUnhandledException));
						TaskLogger.LogRbacEvent(TaskEventLogConstants.Tuple_AccessDenied_NativeCallFailed, null, new object[]
						{
							this.identityName,
							ex
						});
						throw new CmdletAccessDeniedException(new LocalizedString(ex.Message));
					}
					AuthZLogger.SafeAppendGenericInfo("ERC.PopulateGroupMemberships", "Recovered from AuthzException");
				}
			}
			return serializedAccessToken;
		}

		// Token: 0x0600136F RID: 4975 RVA: 0x0004193C File Offset: 0x0003FB3C
		protected virtual ADRawEntry LoadExecutingUser(IIdentity identity, IList<PropertyDefinition> properties)
		{
			ADRawEntry result;
			using (new MonitoredScope("ExchangeRunspaceConfiguration", "LoadExecutingUser", AuthZLogHelper.AuthZPerfMonitors))
			{
				PartitionId partitionId = null;
				GenericSidIdentity genericSidIdentity = identity as GenericSidIdentity;
				if (genericSidIdentity != null && !string.IsNullOrEmpty(genericSidIdentity.PartitionId))
				{
					PartitionId.TryParse(genericSidIdentity.PartitionId, out partitionId);
				}
				ADSessionSettings sessionSettings;
				if (partitionId != null)
				{
					sessionSettings = ADSessionSettings.FromAllTenantsPartitionId(partitionId);
				}
				else
				{
					sessionSettings = ADSessionSettings.FromRootOrgScopeSet();
				}
				this.recipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(ConsistencyMode.IgnoreInvalid, sessionSettings, 3560, "LoadExecutingUser", "f:\\15.00.1497\\sources\\dev\\Configuration\\src\\ObjectModel\\rbac\\ExchangeRunspaceConfiguration.cs");
				SecurityIdentifier securityIdentifier = identity.GetSecurityIdentifier();
				ADRawEntry adrawEntry = this.recipientSession.FindUserBySid(securityIdentifier, properties);
				if (adrawEntry == null && partitionId == null && VariantConfiguration.InvariantNoFlightingSnapshot.CmdletInfra.ServiceAccountForest.Enabled)
				{
					adrawEntry = PartitionDataAggregator.FindUserBySid(securityIdentifier, properties, ref this.recipientSession);
				}
				if (adrawEntry != null && !UserIdParameter.AllowedRecipientTypes.Contains((RecipientType)adrawEntry[ADRecipientSchema.RecipientType]))
				{
					adrawEntry = null;
				}
				if (adrawEntry == null)
				{
					ADComputer adcomputer = (ADComputer)ExchangeRunspaceConfiguration.TryFindComputer(securityIdentifier);
					if (adcomputer == null)
					{
						ExTraceGlobals.AccessDeniedTracer.TraceWarning<SecurityIdentifier>(0L, "Neither User nor Computer {0} could not be found in AD", securityIdentifier);
						return null;
					}
					adrawEntry = adcomputer;
				}
				SecurityIdentifier securityIdentifier2 = (SecurityIdentifier)adrawEntry[IADSecurityPrincipalSchema.Sid];
				result = adrawEntry;
			}
			return result;
		}

		// Token: 0x06001370 RID: 4976 RVA: 0x00041AA8 File Offset: 0x0003FCA8
		protected virtual ManagementScope[] LoadExclusiveScopes()
		{
			ManagementScope[] result;
			using (new MonitoredScope("ExchangeRunspaceConfiguration", "LoadExclusiveScopes", AuthZLogHelper.AuthZPerfMonitors))
			{
				IConfigurationSession tenantOrTopologyConfigurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromOrganizationIdWithoutRbacScopes(ADSystemConfigurationSession.GetRootOrgContainerIdForLocalForest(), this.OrganizationId, this.OrganizationId, false), 3639, "LoadExclusiveScopes", "f:\\15.00.1497\\sources\\dev\\Configuration\\src\\ObjectModel\\rbac\\ExchangeRunspaceConfiguration.cs");
				result = tenantOrTopologyConfigurationSession.GetAllExclusiveScopes().ReadAllPages();
			}
			return result;
		}

		// Token: 0x06001371 RID: 4977 RVA: 0x00041B28 File Offset: 0x0003FD28
		protected virtual Result<ManagementScope>[] LoadScopes(IConfigurationSession session, ADObjectId[] scopeIds)
		{
			Result<ManagementScope>[] result;
			using (new MonitoredScope("ExchangeRunspaceConfiguration", "LoadScopes", AuthZLogHelper.AuthZPerfMonitors))
			{
				result = session.ReadMultiple<ManagementScope>(scopeIds);
			}
			return result;
		}

		// Token: 0x06001372 RID: 4978 RVA: 0x00041B70 File Offset: 0x0003FD70
		protected virtual Result<ExchangeRole>[] LoadRoles(IConfigurationSession session, List<ADObjectId> roleIds)
		{
			Result<ExchangeRole>[] result;
			using (new MonitoredScope("ExchangeRunspaceConfiguration", "LoadRoles", AuthZLogHelper.AuthZPerfMonitors))
			{
				result = session.ReadMultiple<ExchangeRole>(roleIds.ToArray());
			}
			return result;
		}

		// Token: 0x06001373 RID: 4979 RVA: 0x00041BBC File Offset: 0x0003FDBC
		protected virtual Result<ExchangeRoleAssignment>[] LoadRoleAssignments(IConfigurationSession session, List<ADObjectId> implicitRoleIds)
		{
			return this.LoadRoleAssignments(session, this.executingUser, implicitRoleIds);
		}

		// Token: 0x06001374 RID: 4980 RVA: 0x00041BCC File Offset: 0x0003FDCC
		protected virtual Result<ExchangeRoleAssignment>[] LoadRoleAssignments(IConfigurationSession session, ADRawEntry user, List<ADObjectId> implicitRoleIds)
		{
			Result<ExchangeRoleAssignment>[] result;
			using (new MonitoredScope("ExchangeRunspaceConfiguration", "LoadRoleAssignments", AuthZLogHelper.AuthZPerfMonitors))
			{
				ADObjectId[] nestedSecurityGroupMembership = this.GetNestedSecurityGroupMembership(user);
				ADObjectId[] array = new ADObjectId[nestedSecurityGroupMembership.Length + implicitRoleIds.Count];
				Array.Copy(nestedSecurityGroupMembership, array, nestedSecurityGroupMembership.Length);
				Array.Copy(implicitRoleIds.ToArray(), 0, array, nestedSecurityGroupMembership.Length, implicitRoleIds.Count);
				if (array.Length == 0)
				{
					ExTraceGlobals.AccessDeniedTracer.TraceError<string>((long)this.GetHashCode(), "Could not retrieve any SIDs for user {0} from AD, access denied", this.identityName);
					AuthZLogger.SafeAppendGenericError("ERC.LoadRoleAssignments", string.Format("Could not retrieve any SIDs for user {0} from AD", this.identityName), false);
					TaskLogger.LogRbacEvent(TaskEventLogConstants.Tuple_AccessDenied_NoRoleAssignments, null, new object[]
					{
						this.IdentityName,
						this.executingUser.OriginatingServer
					});
					throw new CmdletAccessDeniedException(Strings.NoRoleAssignmentsFound(this.identityName));
				}
				OrganizationId orgId = (OrganizationId)user[ADObjectSchema.OrganizationId];
				ADObjectId adobjectId = null;
				SharedConfiguration sharedConfiguration = null;
				using (new MonitoredScope("ExchangeRunspaceConfiguration", "SharedConfiguration.GetSharedConfiguration", AuthZLogHelper.AuthZPerfMonitors))
				{
					sharedConfiguration = SharedConfiguration.GetSharedConfiguration(orgId);
				}
				if (sharedConfiguration != null)
				{
					using (new MonitoredScope("ExchangeRunspaceConfiguration", "sharedConfig.GetSharedRoleGroupIds", AuthZLogHelper.AuthZPerfMonitors))
					{
						array = sharedConfiguration.GetSharedRoleGroupIds(array);
					}
					if (user[ADRecipientSchema.RoleAssignmentPolicy] == null)
					{
						using (new MonitoredScope("ExchangeRunspaceConfiguration", "sharedConfig.GetSharedRoleAssignmentPolicy", AuthZLogHelper.AuthZPerfMonitors))
						{
							adobjectId = sharedConfiguration.GetSharedRoleAssignmentPolicy();
						}
					}
					if (array.Length == 0 && adobjectId == null)
					{
						ExTraceGlobals.AccessDeniedTracer.TraceError<string>((long)this.GetHashCode(), "User {0} is not member of any RoleGroup and Role Assignment Policy its null. Access Denied", this.identityName);
						AuthZLogger.SafeAppendGenericError("ERC.LoadRoleAssignments", string.Format("User {0} is not member of any RoleGroup and Role Assignment Policy its null", this.identityName), false);
						TaskLogger.LogRbacEvent(TaskEventLogConstants.Tuple_AccessDenied_NoRoleAssignments, null, new object[]
						{
							this.IdentityName,
							this.executingUser.OriginatingServer
						});
						throw new CmdletAccessDeniedException(Strings.NoRoleAssignmentsFound(this.identityName));
					}
				}
				else
				{
					adobjectId = (ADObjectId)user[ADRecipientSchema.RoleAssignmentPolicy];
					if (adobjectId == null)
					{
						using (new MonitoredScope("ExchangeRunspaceConfiguration", "RBACHelper.GetDefaultRoleAssignmentPolicy", AuthZLogHelper.AuthZPerfMonitors))
						{
							adobjectId = RBACHelper.GetDefaultRoleAssignmentPolicy(orgId);
						}
					}
				}
				if (adobjectId != null)
				{
					if (ExchangeRunspaceConfiguration.IsComputer(this.executingUser))
					{
						ExTraceGlobals.ADConfigTracer.TraceDebug(0L, "Role assignment policy check skipped because current user is the computer user.");
					}
					else if (!this.Impersonated && this.delegatedPrincipal != null)
					{
						ExTraceGlobals.ADConfigTracer.TraceDebug(0L, "Role assignment policy check skipped because current user is a delegated admin.");
					}
					else
					{
						array = RBACHelper.AddElementToStaticArray(array, adobjectId);
					}
				}
				if (array.Length == 0)
				{
					ExTraceGlobals.AccessDeniedTracer.TraceError<string>((long)this.GetHashCode(), "Could not retrieve any SIDs for user {0} from AD, access denied - Right before FindRoleAssignmentsByUserIds", this.identityName);
					AuthZLogger.SafeAppendGenericError("ERC.LoadRoleAssignments", string.Format("Could not retrieve any SIDs for user {0} from AD, access denied - Right before FindRoleAssignmentsByUserIds", this.identityName), false);
					TaskLogger.LogRbacEvent(TaskEventLogConstants.Tuple_AccessDenied_NoRoleAssignments, null, new object[]
					{
						this.IdentityName,
						this.executingUser.OriginatingServer
					});
					throw new CmdletAccessDeniedException(Strings.NoRoleAssignmentsFound(this.identityName));
				}
				this.universalSecurityGroupsCache = array;
				using (new MonitoredScope("ExchangeRunspaceConfiguration", "session.FindRoleAssignmentsByUserIds", AuthZLogHelper.AuthZPerfMonitors))
				{
					result = session.FindRoleAssignmentsByUserIds(array, this.PartnerMode);
				}
			}
			return result;
		}

		// Token: 0x06001375 RID: 4981 RVA: 0x00041FBC File Offset: 0x000401BC
		private static bool IsComputer(ADRawEntry executingUser)
		{
			return ((MultiValuedProperty<string>)executingUser[ADObjectSchema.ObjectClass]).Contains("computer");
		}

		// Token: 0x06001376 RID: 4982 RVA: 0x00041FD8 File Offset: 0x000401D8
		private static bool SameScopeCollectionExists(List<ADScopeCollection> aggregatedScopes, List<ADScope> scopes)
		{
			for (int i = 0; i < aggregatedScopes.Count; i++)
			{
				if (aggregatedScopes[i].Count == scopes.Count)
				{
					bool flag = true;
					for (int j = 0; j < scopes.Count; j++)
					{
						if (!((RbacScope)scopes[j]).IsPresentInCollection(aggregatedScopes[i]))
						{
							flag = false;
							break;
						}
					}
					if (flag)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06001377 RID: 4983 RVA: 0x00042044 File Offset: 0x00040244
		private static void TrimAggregatedScopes(List<ADScopeCollection> aggregatedScopes, bool hiddenFromGAL)
		{
			int num = -1;
			bool flag = false;
			int i = 0;
			while (i < aggregatedScopes.Count)
			{
				ADScopeCollection adscopeCollection = aggregatedScopes[i];
				if (adscopeCollection.Count == 1)
				{
					RbacScope rbacScope = (RbacScope)adscopeCollection[0];
					bool flag2 = true;
					ScopeType scopeType = rbacScope.ScopeType;
					switch (scopeType)
					{
					case ScopeType.Organization:
						goto IL_4D;
					case ScopeType.MyGAL:
						num = i;
						break;
					default:
						if (scopeType == ScopeType.OrganizationConfig)
						{
							goto IL_4D;
						}
						flag2 = false;
						break;
					}
					IL_61:
					if (!flag2)
					{
						goto IL_65;
					}
					goto IL_AB;
					IL_4D:
					aggregatedScopes.RemoveAt(i);
					i--;
					goto IL_61;
				}
				goto IL_65;
				IL_AB:
				i++;
				continue;
				IL_65:
				if (flag)
				{
					goto IL_AB;
				}
				bool flag3 = true;
				for (int j = 0; j < adscopeCollection.Count; j++)
				{
					RbacScope rbacScope2 = (RbacScope)aggregatedScopes[i][j];
					if (!rbacScope2.IsScopeTypeSmallerThan(ScopeType.MyGAL, hiddenFromGAL))
					{
						flag3 = false;
						break;
					}
				}
				if (flag3)
				{
					flag = true;
					goto IL_AB;
				}
				goto IL_AB;
			}
			if (flag && num != -1)
			{
				aggregatedScopes.RemoveAt(num);
			}
		}

		// Token: 0x06001378 RID: 4984 RVA: 0x0004211C File Offset: 0x0004031C
		private bool IsPowershellProcess()
		{
			if (ExchangeRunspaceConfiguration.isPowershellProcess == null)
			{
				using (Process currentProcess = Process.GetCurrentProcess())
				{
					ExchangeRunspaceConfiguration.isPowershellProcess = new bool?(currentProcess.MainModule.ModuleName.Equals("Powershell.exe", StringComparison.OrdinalIgnoreCase));
				}
			}
			return ExchangeRunspaceConfiguration.isPowershellProcess.Value;
		}

		// Token: 0x06001379 RID: 4985 RVA: 0x00042184 File Offset: 0x00040384
		private void LoadRoleCmdletInfo(string organizationName, IList<RoleType> roleTypeFilter, List<RoleEntry> sortedRoleEntryFilter, IList<RoleType> logonUserRequiredRoleTypes, List<ADObjectId> implicitRoleIds)
		{
			using (new MonitoredScope("ExchangeRunspaceConfiguration", "LoadRoleCmdletInfo", AuthZLogHelper.AuthZPerfMonitors))
			{
				this.allScopes = null;
				this.allRoleEntries = null;
				this.allRoleTypes = null;
				this.allRoleAssignments = null;
				this.validationRuleCacheResults = new List<CachedValidationRuleResult>();
				IConfigurationSession session = null;
				using (new MonitoredScope("ExchangeRunspaceConfiguration", "GetTenantOrTopologyConfigurationSession", AuthZLogHelper.AuthZPerfMonitors))
				{
					session = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(ConsistencyMode.IgnoreInvalid, this.GetADSessionSettings(), 4036, "LoadRoleCmdletInfo", "f:\\15.00.1497\\sources\\dev\\Configuration\\src\\ObjectModel\\rbac\\ExchangeRunspaceConfiguration.cs");
				}
				if (this.intersectRoleEntries)
				{
					Dictionary<ADObjectId, ManagementScope> dictionary;
					List<RoleEntryInfo> userAllRoleEntries;
					Dictionary<RoleType, List<ADObjectId>> dictionary2;
					Microsoft.Exchange.Collections.ReadOnlyCollection<ExchangeRoleAssignment> readOnlyCollection;
					this.LoadRoleCmdletInfo(this.logonUser, this.executingUser, session, null, null, null, implicitRoleIds, RoleFilteringMode.DiscardEndUserRole, this.logonAccessToken, out dictionary, out userAllRoleEntries, out dictionary2, out readOnlyCollection);
					if (logonUserRequiredRoleTypes != null)
					{
						bool flag = false;
						foreach (RoleType item in dictionary2.Keys)
						{
							if ((roleTypeFilter == null || roleTypeFilter.Contains(item)) && logonUserRequiredRoleTypes.Contains(item))
							{
								flag = true;
								break;
							}
						}
						if (!flag)
						{
							throw new CmdletAccessDeniedException(Strings.NoRequiredRole(this.identityName));
						}
					}
					List<RoleEntry> list;
					List<RoleEntry> list2;
					this.CombineRoleEntries(userAllRoleEntries, out list, out list2);
					sortedRoleEntryFilter = list;
				}
				this.LoadRoleCmdletInfo(this.executingUser, null, session, organizationName, roleTypeFilter, sortedRoleEntryFilter, implicitRoleIds, this.intersectRoleEntries ? RoleFilteringMode.KeepOnlyEndUserRole : ((!this.Impersonated && this.delegatedPrincipal != null) ? RoleFilteringMode.DiscardEndUserRole : RoleFilteringMode.NoFiltering), this.UserAccessToken, out this.allScopes, out this.allRoleEntries, out this.allRoleTypes, out this.allRoleAssignments);
				if (this.executingUser == null)
				{
					this.ConfigurationSettings.VariantConfigurationSnapshot = null;
				}
				else
				{
					this.ConfigurationSettings.VariantConfigurationSnapshot = CmdletFlight.GetSnapshot(this.executingUser, this.ConfigurationSettings.AdditionalConstraints);
				}
				CmdletFlight.FilterCmdletsAndParams(this.ConfigurationSettings.VariantConfigurationSnapshot, this.allRoleEntries);
			}
		}

		// Token: 0x0600137A RID: 4986 RVA: 0x000423B8 File Offset: 0x000405B8
		private void LoadRoleCmdletInfo(ADRawEntry user, ADRawEntry userToVerifyInScope, IConfigurationSession session, string organizationName, IList<RoleType> roleTypeFilter, List<RoleEntry> sortedRoleEntryFilter, List<ADObjectId> implicitRoleIds, RoleFilteringMode roleFilteringMode, SerializedAccessToken securityAccessToken, out Dictionary<ADObjectId, ManagementScope> userAllScopes, out List<RoleEntryInfo> userAllRoleEntries, out Dictionary<RoleType, List<ADObjectId>> userAllRoleTypes, out Microsoft.Exchange.Collections.ReadOnlyCollection<ExchangeRoleAssignment> userAllRoleAssignments)
		{
			Result<ExchangeRoleAssignment>[] array = this.LoadRoleAssignments(session, user, implicitRoleIds);
			AuthZLogger.SafeAppendGenericInfo("RoleAssignments", this.GetUserFriendlyRoleAssignmentInfo(array));
			if (array.Length == 0)
			{
				ExTraceGlobals.AccessDeniedTracer.TraceError<object, string>((long)this.GetHashCode(), "User {0} ({1}) has no role assignments associated, access denied", user[ADRecipientSchema.PrimarySmtpAddress], this.identityName);
				AuthZLogger.SafeAppendGenericError("ERC.LoadRoleCmdletInfo", string.Format("User {0} ({1}) has no role assignments associated, access denied", user[ADRecipientSchema.PrimarySmtpAddress], this.identityName), false);
				TaskLogger.LogRbacEvent(TaskEventLogConstants.Tuple_AccessDenied_NoRoleAssignments, null, new object[]
				{
					this.IdentityName,
					session.Source
				});
				throw new CmdletAccessDeniedException(Strings.NoRoleAssignmentsFound(this.identityName));
			}
			List<ADObjectId> list = new List<ADObjectId>(array.Length);
			List<ExchangeRoleAssignment> list2 = new List<ExchangeRoleAssignment>(array.Length);
			List<ExchangeRoleAssignment> list3 = new List<ExchangeRoleAssignment>(array.Length);
			List<ExchangeRoleAssignment> list4 = new List<ExchangeRoleAssignment>(array.Length);
			userAllScopes = new Dictionary<ADObjectId, ManagementScope>();
			bool enabled = VariantConfiguration.InvariantNoFlightingSnapshot.CmdletInfra.CheckForDedicatedTenantAdminRoleNamePrefix.Enabled;
			AuthZLogger.SafeAppendGenericInfo("roleAssignmentResults.Length", array.Length.ToString());
			for (int i = 0; i < array.Length; i++)
			{
				ExchangeRoleAssignment data = array[i].Data;
				ExTraceGlobals.ADConfigTracer.TraceDebug((long)this.GetHashCode(), "Found {0} RoleAssignment {1} for role {2}: {3}", new object[]
				{
					data.RoleAssignmentDelegationType,
					data.Id,
					data.Role,
					data.Enabled ? "Enabled" : "Disabled"
				});
				if (data.Enabled)
				{
					if (data.CustomRecipientWriteScope != null)
					{
						userAllScopes[data.CustomRecipientWriteScope] = null;
					}
					if (data.CustomConfigWriteScope != null)
					{
						userAllScopes[data.CustomConfigWriteScope] = null;
					}
					if (data.RoleAssignmentDelegationType == RoleAssignmentDelegationType.Regular)
					{
						list4.Add(data);
						list.Add(data.Role);
					}
					else
					{
						list3.Add(data);
					}
				}
			}
			if (list4.Count == 0)
			{
				ExTraceGlobals.AccessDeniedTracer.TraceError<string>((long)this.GetHashCode(), "User {0} has no valid and enabled Regular role assignments associated, access denied", this.identityName);
				AuthZLogger.SafeAppendGenericError("ERC.LoadRoleCmdletInfo", string.Format("User {0} has no valid and enabled Regular role assignments associated, access denied", this.identityName), false);
				TaskLogger.LogRbacEvent(TaskEventLogConstants.Tuple_AccessDenied_NoValidEnabledRoleAssignments, null, new object[]
				{
					this.IdentityName,
					session.Source
				});
				throw new CmdletAccessDeniedException(Strings.NoRoleAssignmentsFound(this.identityName));
			}
			this.ReadAndCheckAllScopes(session, userAllScopes, organizationName);
			Result<ExchangeRole>[] array2 = this.LoadRoles(session, list);
			userAllRoleEntries = new List<RoleEntryInfo>(100);
			userAllRoleTypes = new Dictionary<RoleType, List<ADObjectId>>(array2.Length);
			bool flag = false;
			using (new MonitoredScope("ExchangeRunspaceConfiguration", "ProcessRoleAssignmentsAndAssociatedRoles", AuthZLogHelper.AuthZPerfMonitors))
			{
				AuthZLogger.SafeAppendGenericInfo("roleResults.Length", array2.Length.ToString());
				for (int j = 0; j < array2.Length; j++)
				{
					Result<ExchangeRole> result = array2[j];
					if (result.Data == null)
					{
						ExTraceGlobals.ADConfigTracer.TraceError<ADObjectId>((long)this.GetHashCode(), "Role {0} was not found", list[j]);
					}
					else
					{
						list4[j].IsFromEndUserRole = result.Data.IsEndUserRole;
						if (enabled && !this.isDedicatedTenantAdmin && result.Data.Name.StartsWith("SSA_", StringComparison.OrdinalIgnoreCase) && list4[j].RoleAssigneeType != RoleAssigneeType.RoleAssignmentPolicy)
						{
							this.isDedicatedTenantAdmin = true;
						}
						ExTraceGlobals.ADConfigTracer.TraceDebug<ADObjectId, RoleType>(0L, "Read Role {0} of type {1}", result.Data.Id, result.Data.RoleType);
						if (roleFilteringMode == RoleFilteringMode.DiscardEndUserRole)
						{
							if (result.Data.IsEndUserRole)
							{
								list4[j] = null;
								ExTraceGlobals.ADConfigTracer.TraceDebug<ADObjectId, RoleType>(0L, "RoleFilteringMode is DiscardEndUserRole. Role {0} of type {1} skipped because it is an end user role.", result.Data.Id, result.Data.RoleType);
								goto IL_83A;
							}
						}
						else if (roleFilteringMode == RoleFilteringMode.KeepOnlyEndUserRole && !result.Data.IsEndUserRole)
						{
							list4[j] = null;
							ExTraceGlobals.ADConfigTracer.TraceDebug<ADObjectId, RoleType>(0L, "RoleFilteringMode is KeepOnlyEndUserRole. Role {0} of type {1} skipped because it isn't an end user role.", result.Data.Id, result.Data.RoleType);
							goto IL_83A;
						}
						if (list4[j].RoleAssigneeType == RoleAssigneeType.RoleAssignmentPolicy && !result.Data.IsEndUserRole)
						{
							list4[j] = null;
							ExTraceGlobals.ADConfigTracer.TraceDebug<ADObjectId, RoleType>(0L, "Role {0} of type {1} skipped because it is assigned through RoleAssignmentPolicy but is not end-user role.", result.Data.Id, result.Data.RoleType);
						}
						else if (roleTypeFilter != null && !roleTypeFilter.Contains(result.Data.RoleType))
						{
							list4[j] = null;
							ExTraceGlobals.ADConfigTracer.TraceDebug<ADObjectId, RoleType>(0L, "Role {0} of type {1} skipped due to not existed in rolefilter.", result.Data.Id, result.Data.RoleType);
						}
						else
						{
							if (userToVerifyInScope != null)
							{
								if (result.Data.RoleType == RoleType.Custom || result.Data.RoleType >= RoleType.UnScoped)
								{
									list4[j] = null;
									ExTraceGlobals.ADConfigTracer.TraceDebug<ADObjectId, RoleType>((long)this.GetHashCode(), "Role {0} skipped because it is type {1}.", result.Data.Id, result.Data.RoleType);
									goto IL_83A;
								}
								RoleAssignmentScopeSet effectiveScopeSet = list4[j].GetEffectiveScopeSet(userAllScopes, securityAccessToken);
								if (effectiveScopeSet == null)
								{
									list4[j] = null;
									ExTraceGlobals.ADConfigTracer.TraceDebug<ADObjectId, RoleType>((long)this.GetHashCode(), "Role {0} of type {1} skipped because its effective scope set is null.", result.Data.Id, result.Data.RoleType);
									goto IL_83A;
								}
								OrganizationId organizationId = (OrganizationId)user[ADObjectSchema.OrganizationId];
								effectiveScopeSet.RecipientReadScope.PopulateRootAndFilter(organizationId, user);
								effectiveScopeSet.RecipientWriteScope.PopulateRootAndFilter(organizationId, user);
								ADScopeException ex;
								if (!ADSession.TryVerifyIsWithinScopes(userToVerifyInScope, effectiveScopeSet.RecipientReadScope, new ADScopeCollection[]
								{
									new ADScopeCollection(new ADScope[]
									{
										effectiveScopeSet.RecipientWriteScope
									})
								}, this.exclusiveRecipientScopesCollection, false, out ex))
								{
									list4[j] = null;
									ExTraceGlobals.ADConfigTracer.TraceDebug<ADObjectId, RoleType, string>((long)this.GetHashCode(), "Role {0} named {2} of type {1} skipped because its RecipientScope doesn't include the impersonated user.", result.Data.Id, result.Data.RoleType, result.Data.Name);
									goto IL_83A;
								}
							}
							flag = (RoleType.UnScoped != result.Data.RoleType && result.Data.ExchangeVersion.IsOlderThan(result.Data.MaximumSupportedExchangeObjectVersion) && (j == 0 || flag));
							int count = userAllRoleEntries.Count;
							foreach (RoleEntry roleEntry in result.Data.RoleEntries)
							{
								ExTraceGlobals.ADConfigTracer.TraceDebug<RoleEntry>((long)this.GetHashCode(), "Got Entry {0}", roleEntry);
								RoleEntry roleEntry2 = roleEntry;
								if (sortedRoleEntryFilter != null)
								{
									int num = sortedRoleEntryFilter.BinarySearch(roleEntry, RoleEntry.NameComparer);
									if (num < 0)
									{
										ExTraceGlobals.ADConfigTracer.TraceDebug<RoleEntry>((long)this.GetHashCode(), "Entry {0} skipped due to not existed in entry filter.", roleEntry);
										continue;
									}
									if (this.intersectRoleEntries)
									{
										roleEntry2 = roleEntry.IntersectParameters(sortedRoleEntryFilter[num]);
									}
								}
								if (roleEntry2.Parameters.Count != 0)
								{
									if (result.Data.IsEndUserRole)
									{
										roleEntry2 = this.TrimRoleEntryParametersWithValidationRules(roleEntry2, false);
										if (roleEntry2.Parameters.Count == 0)
										{
											ExTraceGlobals.ADConfigTracer.TraceDebug<RoleEntry, string>((long)this.GetHashCode(), "Entry '{0}' skipped due to validation rules filtering. Role '{1}'", roleEntry, result.Data.Name);
											continue;
										}
									}
									else
									{
										roleEntry2 = this.TrimRoleEntryParametersWithValidationRules(roleEntry2, true);
										if (roleEntry2.Parameters.Count == 0)
										{
											ExTraceGlobals.ADConfigTracer.TraceDebug<RoleEntry, string>((long)this.GetHashCode(), "Entry '{0}' skipped due to validation rules filtering. Role '{1}'", roleEntry, result.Data.Name);
											continue;
										}
									}
								}
								userAllRoleEntries.Add(new RoleEntryInfo(roleEntry2, list4[j]));
							}
							List<ADObjectId> list5 = null;
							if (!userAllRoleTypes.TryGetValue(result.Data.RoleType, out list5))
							{
								list5 = new List<ADObjectId>();
								userAllRoleTypes[result.Data.RoleType] = list5;
							}
							list5.Add(result.Data.Id);
						}
					}
					IL_83A:;
				}
			}
			if (flag)
			{
				throw new NonMigratedUserDeniedException(Strings.NonMigratedUserException(this.identityName));
			}
			if (userAllRoleEntries.Count == 0 && !this.NoCmdletAllowed)
			{
				throw new CmdletAccessDeniedException(Strings.NoRoleAssignmentsFound(this.identityName));
			}
			for (int k = list4.Count - 1; k >= 0; k--)
			{
				if (list4[k] == null)
				{
					list4.RemoveAt(k);
				}
			}
			list2.AddRange(list4);
			list2.AddRange(list3);
			if (ExchangeAuthorizationPlugin.ShouldShowFismaBanner)
			{
				userAllRoleEntries.Add(ExchangeRunspaceConfiguration.welcomeMessageScriptInitializer);
			}
			userAllRoleAssignments = new Microsoft.Exchange.Collections.ReadOnlyCollection<ExchangeRoleAssignment>(list2);
			userAllRoleEntries.Sort(RoleEntryInfo.NameAndInstanceHashCodeComparer);
			this.AddClientSpecificParameters(ref userAllRoleEntries);
			this.AddClientSpecificRoleEntries(ref userAllRoleEntries);
		}

		// Token: 0x0600137B RID: 4987 RVA: 0x00042D00 File Offset: 0x00040F00
		private void ReadAndCheckAllScopes(IConfigurationSession session, Dictionary<ADObjectId, ManagementScope> userAllScopes, string tenantOrganizationName)
		{
			using (new MonitoredScope("ExchangeRunspaceConfiguration", "ReadAndCheckAllScopes", AuthZLogHelper.AuthZPerfMonitors))
			{
				OrganizationId organizationId = null;
				int num = 0;
				if (userAllScopes.Keys.Count != 0)
				{
					ADObjectId[] array = new ADObjectId[userAllScopes.Keys.Count];
					userAllScopes.Keys.CopyTo(array, 0);
					Result<ManagementScope>[] array2 = this.LoadScopes(session, array);
					for (int i = 0; i < array2.Length; i++)
					{
						userAllScopes[array[i]] = null;
						Result<ManagementScope> result = array2[i];
						if (result.Data == null)
						{
							ExTraceGlobals.ADConfigTracer.TraceError<ADObjectId>((long)this.GetHashCode(), "Scope {0} was not found", array[i]);
							userAllScopes.Remove(array[i]);
						}
						else
						{
							ExTraceGlobals.ADConfigTracer.TraceDebug<ADObjectId>((long)this.GetHashCode(), "Read scope {0}", result.Data.Id);
							if (this.PartnerMode != (result.Data.ScopeRestrictionType == ScopeRestrictionType.PartnerDelegatedTenantScope))
							{
								ExTraceGlobals.ADConfigTracer.TraceError<ADObjectId, ScopeRestrictionType, string>((long)this.GetHashCode(), "We somehow read ManagementScope {0} of type {1} which is not allowed in {2}Partner mode. Ignoring the scope (and associated role assignments)", result.Data.Id, result.Data.ScopeRestrictionType, this.PartnerMode ? "" : "non-");
							}
							else if (ExchangeRunspaceConfiguration.TryStampQueryFilterOnManagementScope(result.Data))
							{
								userAllScopes[array[i]] = result.Data;
								num++;
							}
						}
					}
				}
				if (this.PartnerMode)
				{
					this.ValidateTenantOrganizationAndPartnerScopes(session, num, userAllScopes, tenantOrganizationName, out organizationId);
				}
				else
				{
					this.LoadAllExclusiveScopes(session, userAllScopes);
				}
				this.allScopes = userAllScopes;
				this.organizationId = organizationId;
			}
		}

		// Token: 0x0600137C RID: 4988 RVA: 0x00042EBC File Offset: 0x000410BC
		private void LoadAllExclusiveScopes(IConfigurationSession session, Dictionary<ADObjectId, ManagementScope> allScopes)
		{
			ManagementScope[] array = this.LoadExclusiveScopes();
			this.exclusiveRecipientScopes = new List<ADScope>();
			Dictionary<string, List<ADScope>> dictionary = new Dictionary<string, List<ADScope>>(2);
			ManagementScope[] array2 = array;
			int i = 0;
			while (i < array2.Length)
			{
				ManagementScope managementScope = array2[i];
				if (!allScopes.ContainsKey(managementScope.Id))
				{
					ExchangeRunspaceConfiguration.TryStampQueryFilterOnManagementScope(managementScope);
					allScopes[managementScope.Id] = managementScope;
				}
				switch (managementScope.ScopeRestrictionType)
				{
				case ScopeRestrictionType.RecipientScope:
				{
					RbacScope rbacScope = new RbacScope(ScopeType.ExclusiveRecipientScope, allScopes[managementScope.Id]);
					rbacScope.PopulateRootAndFilter(this.organizationId, this.executingUser);
					this.exclusiveRecipientScopes.Add(rbacScope);
					break;
				}
				case ScopeRestrictionType.ServerScope:
				{
					RbacScope rbacScope = new RbacScope(ScopeType.ExclusiveConfigScope, allScopes[managementScope.Id]);
					rbacScope.PopulateRootAndFilter(this.organizationId, this.executingUser);
					RBACHelper.AddValueToDictionaryList<ADScope>(dictionary, "Server", rbacScope);
					break;
				}
				case ScopeRestrictionType.PartnerDelegatedTenantScope:
					goto IL_111;
				case ScopeRestrictionType.DatabaseScope:
				{
					RbacScope rbacScope = new RbacScope(ScopeType.ExclusiveConfigScope, allScopes[managementScope.Id]);
					rbacScope.PopulateRootAndFilter(this.organizationId, this.executingUser);
					RBACHelper.AddValueToDictionaryList<ADScope>(dictionary, "Database", rbacScope);
					break;
				}
				default:
					goto IL_111;
				}
				IL_133:
				i++;
				continue;
				IL_111:
				ExTraceGlobals.ADConfigTracer.TraceError<ADObjectId, ScopeRestrictionType>((long)this.GetHashCode(), "We somehow read ManagementScope {0} of type {1} which is not allowed as an exclusive scope. Ignoring the scope.", managementScope.Id, managementScope.ScopeRestrictionType);
				goto IL_133;
			}
			if (this.exclusiveRecipientScopes == null || this.exclusiveRecipientScopes.Count == 0)
			{
				this.exclusiveRecipientScopesCollection = ADScopeCollection.Empty;
			}
			else
			{
				this.exclusiveRecipientScopesCollection = new ADScopeCollection(this.exclusiveRecipientScopes);
			}
			if (dictionary.Count > 0)
			{
				this.exclusiveObjectSpecificConfigScopes = new Dictionary<string, ADScopeCollection>(dictionary.Count);
				foreach (KeyValuePair<string, List<ADScope>> keyValuePair in dictionary)
				{
					this.exclusiveObjectSpecificConfigScopes[keyValuePair.Key] = new ADScopeCollection(keyValuePair.Value);
				}
			}
		}

		// Token: 0x0600137D RID: 4989 RVA: 0x000430B4 File Offset: 0x000412B4
		private void ValidateTenantOrganizationAndPartnerScopes(IConfigurationSession session, int validScopesCount, Dictionary<ADObjectId, ManagementScope> scopesDictionary, string organizationName, out OrganizationId orgId)
		{
			orgId = null;
			if (validScopesCount == 0)
			{
				ExTraceGlobals.AccessDeniedTracer.TraceError<string, string>((long)this.GetHashCode(), "No partner scopes found for {0} while requested partner access to {1}. Returning Access Denied", this.identityName, organizationName);
				AuthZLogger.SafeAppendGenericError("ERC.ValidateTenantOrganizationAndPartnerScopes", string.Format("No partner scopes found for {0} while requested partner access to {1}. Returning Access Denied", this.identityName, organizationName), false);
				TaskLogger.LogRbacEvent(TaskEventLogConstants.Tuple_AccessDenied_NoPartnerScopes, null, new object[]
				{
					this.identityName,
					session.Source
				});
				throw new CmdletAccessDeniedException(Strings.ErrorNoPartnerScopes(this.identityName));
			}
			ITenantConfigurationSession tenantConfigurationSession = DirectorySessionFactory.Default.CreateTenantConfigurationSession(ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromTenantCUName(organizationName), 4816, "ValidateTenantOrganizationAndPartnerScopes", "f:\\15.00.1497\\sources\\dev\\Configuration\\src\\ObjectModel\\rbac\\ExchangeRunspaceConfiguration.cs");
			ExchangeConfigurationUnit exchangeConfigurationUnitByNameOrAcceptedDomain = tenantConfigurationSession.GetExchangeConfigurationUnitByNameOrAcceptedDomain(organizationName);
			if (exchangeConfigurationUnitByNameOrAcceptedDomain == null)
			{
				ExTraceGlobals.AccessDeniedTracer.TraceError<string, string>((long)this.GetHashCode(), "Organization {0} not found (User {1} requested partner access to it). Returning Access Denied", organizationName, this.identityName);
				AuthZLogger.SafeAppendGenericError("ERC.ValidateTenantOrganizationAndPartnerScopes", string.Format("Organization {0} not found (User {1} requested partner access to it). Returning Access Denied", organizationName, this.identityName), false);
				TaskLogger.LogRbacEvent(TaskEventLogConstants.Tuple_AccessDenied_OrgNotFound, null, new object[]
				{
					this.identityName,
					organizationName,
					tenantConfigurationSession.Source
				});
				throw new CmdletAccessDeniedException(Strings.ErrorOrgNotFound(this.identityName, organizationName));
			}
			TenantOrganizationPresentationObject tenantOrganizationPresentationObject = new TenantOrganizationPresentationObject(exchangeConfigurationUnitByNameOrAcceptedDomain);
			List<ADObjectId> list = new List<ADObjectId>(scopesDictionary.Count);
			foreach (ManagementScope managementScope in scopesDictionary.Values)
			{
				if (OpathFilterEvaluator.FilterMatches(managementScope.QueryFilter, tenantOrganizationPresentationObject))
				{
					ExTraceGlobals.ADConfigTracer.TraceDebug((long)this.GetHashCode(), "ManagementScope {0} with filter {1} DID match organization {2}({3}), setting runspace OrganizationId to the requested value", new object[]
					{
						managementScope.Id,
						managementScope.Filter,
						organizationName,
						tenantOrganizationPresentationObject.Name
					});
				}
				else
				{
					ExTraceGlobals.ADConfigTracer.TraceWarning((long)this.GetHashCode(), "ManagementScope {0} with filter {1} did not match organization {2}({3})", new object[]
					{
						managementScope.Id,
						managementScope.Filter,
						organizationName,
						tenantOrganizationPresentationObject.Name
					});
					list.Add(managementScope.Id);
					validScopesCount--;
				}
			}
			foreach (ADObjectId key in list)
			{
				scopesDictionary[key] = null;
			}
			if (validScopesCount == 0)
			{
				ExTraceGlobals.AccessDeniedTracer.TraceError<string, string, string>((long)this.GetHashCode(), "No matching partner scopes found for organization {0}({1}) (User {2} requested partner access to it). Returning Access Denied", organizationName, tenantOrganizationPresentationObject.Name, this.identityName);
				AuthZLogger.SafeAppendGenericError("ERC.ValidateTenantOrganizationAndPartnerScopes", string.Format("No matching partner scopes found for organization {0}({1}) (User {2} requested partner access to it). Returning Access Denied", organizationName, tenantOrganizationPresentationObject.Name, this.identityName), false);
				TaskLogger.LogRbacEvent(TaskEventLogConstants.Tuple_AccessDenied_OrgOutOfPartnerScope, null, new object[]
				{
					this.identityName,
					organizationName
				});
				throw new CmdletAccessDeniedException(Strings.ErrorOrgOutOfPartnerScope(this.identityName, organizationName));
			}
			orgId = exchangeConfigurationUnitByNameOrAcceptedDomain.OrganizationId;
		}

		// Token: 0x0600137E RID: 4990 RVA: 0x000433C0 File Offset: 0x000415C0
		private void CombineRoleEntries(List<RoleEntryInfo> userAllRoleEntries, out List<RoleEntry> combinedCmdlets, out List<RoleEntry> combinedScripts)
		{
			using (new MonitoredScope("ExchangeRunspaceConfiguration", "CombineRoleEntries", AuthZLogHelper.AuthZPerfMonitors))
			{
				combinedCmdlets = new List<RoleEntry>(userAllRoleEntries.Count);
				combinedScripts = new List<RoleEntry>(5);
				List<RoleEntry> list = combinedScripts;
				int num = 0;
				for (int i = 1; i <= userAllRoleEntries.Count; i++)
				{
					if (i == userAllRoleEntries.Count || RoleEntry.CompareRoleEntriesByName(userAllRoleEntries[i - 1].RoleEntry, userAllRoleEntries[i].RoleEntry) != 0)
					{
						RoleEntry[] array = new RoleEntry[i - num];
						for (int j = num; j <= i - 1; j++)
						{
							array[j - num] = userAllRoleEntries[j].RoleEntry;
						}
						RoleEntry roleEntry = RoleEntry.MergeParameters(array);
						if (list == combinedScripts && userAllRoleEntries[i - 1].RoleEntry is CmdletRoleEntry)
						{
							ExTraceGlobals.ADConfigTracer.TraceDebug((long)this.GetHashCode(), "Switching to cmdlets");
							list = combinedCmdlets;
						}
						list.Add(roleEntry);
						ExTraceGlobals.ADConfigTracer.TraceDebug<RoleEntry>((long)this.GetHashCode(), "Added merged entry {0}", roleEntry);
						num = i;
					}
				}
			}
		}

		// Token: 0x0600137F RID: 4991 RVA: 0x000434EC File Offset: 0x000416EC
		protected bool LocateRoleEntriesForCmdlet(string cmdletName, out int firstIndex, out int lastIndex)
		{
			firstIndex = -1;
			lastIndex = -1;
			RoleEntryInfo roleInfoForCmdlet;
			try
			{
				roleInfoForCmdlet = RoleEntryInfo.GetRoleInfoForCmdlet(cmdletName);
			}
			catch (FormatException innerException)
			{
				throw new ArgumentException("cmdletName", innerException);
			}
			int num = this.allRoleEntries.BinarySearch(roleInfoForCmdlet, RoleEntryInfo.NameComparer);
			if (num < 0)
			{
				ExTraceGlobals.AccessCheckTracer.TraceWarning<string, string>((long)this.GetHashCode(), "LocateRoleEntriesForCmdlet({0}) returns false for user {1} because this cmdlet is not in role", cmdletName, this.identityName);
				return false;
			}
			firstIndex = num;
			lastIndex = num;
			while (firstIndex > 0)
			{
				if (RoleEntry.CompareRoleEntriesByName(this.allRoleEntries[firstIndex - 1].RoleEntry, roleInfoForCmdlet.RoleEntry) != 0)
				{
					break;
				}
				firstIndex--;
			}
			while (lastIndex < this.allRoleEntries.Count - 1 && RoleEntry.CompareRoleEntriesByName(this.allRoleEntries[lastIndex + 1].RoleEntry, roleInfoForCmdlet.RoleEntry) == 0)
			{
				lastIndex++;
			}
			return true;
		}

		// Token: 0x06001380 RID: 4992 RVA: 0x000435CC File Offset: 0x000417CC
		protected virtual ADObjectId[] GetNestedSecurityGroupMembership(ADRawEntry user)
		{
			ADObjectId[] nestedSecurityGroupMembership;
			using (new MonitoredScope("ExchangeRunspaceConfiguration", "GetNestedSecurityGroupMembership", AuthZLogHelper.AuthZPerfMonitors))
			{
				ADSessionSettings sessionSettings;
				using (new MonitoredScope("ExchangeRunspaceConfiguration", "FromRootOrgScopeSet", AuthZLogHelper.AuthZPerfMonitors))
				{
					sessionSettings = ADSessionSettings.FromRootOrgScopeSet();
				}
				using (new MonitoredScope("ExchangeRunspaceConfiguration", "FromAllTenantsOrRootOrgAutoDetect", AuthZLogHelper.AuthZPerfMonitors))
				{
					if (VariantConfiguration.InvariantNoFlightingSnapshot.CmdletInfra.ServiceAccountForest.Enabled && user != null && user.Id != null)
					{
						sessionSettings = ADSessionSettings.FromAllTenantsOrRootOrgAutoDetect(user.Id);
					}
				}
				IRecipientSession tenantOrRootOrgRecipientSession;
				using (new MonitoredScope("ExchangeRunspaceConfiguration", "GetNestedSecurityGroupMembership.GetTenantOrRootOrgRecipientSession", AuthZLogHelper.AuthZPerfMonitors))
				{
					tenantOrRootOrgRecipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(ConsistencyMode.IgnoreInvalid, sessionSettings, 5087, "GetNestedSecurityGroupMembership", "f:\\15.00.1497\\sources\\dev\\Configuration\\src\\ObjectModel\\rbac\\ExchangeRunspaceConfiguration.cs");
				}
				nestedSecurityGroupMembership = ExchangeRunspaceConfiguration.GetNestedSecurityGroupMembership(user, tenantOrRootOrgRecipientSession, AssignmentMethod.All);
			}
			return nestedSecurityGroupMembership;
		}

		// Token: 0x06001381 RID: 4993 RVA: 0x000436F8 File Offset: 0x000418F8
		protected static ADObjectId[] GetNestedSecurityGroupMembership(ADRawEntry user, IRecipientSession recipientSession, AssignmentMethod assignmentMethod)
		{
			List<string> list;
			using (new MonitoredScope("ExchangeRunspaceConfiguration", "recipientSession.GetTokenSids", AuthZLogHelper.AuthZPerfMonitors))
			{
				list = recipientSession.GetTokenSids(user, assignmentMethod);
			}
			if (list.Count == 0)
			{
				return new ADObjectId[0];
			}
			ADSessionSettings sessionSettings;
			using (new MonitoredScope("ExchangeRunspaceConfiguration", "FromAllTenantsOrRootOrgAutoDetectOrFromRootOrgScopeSet", AuthZLogHelper.AuthZPerfMonitors))
			{
				sessionSettings = ((VariantConfiguration.InvariantNoFlightingSnapshot.CmdletInfra.ServiceAccountForest.Enabled && user.Id != null && user.Id.DomainId != null) ? ADSessionSettings.FromAllTenantsOrRootOrgAutoDetect(user.Id) : ADSessionSettings.FromRootOrgScopeSet());
			}
			IRecipientSession tenantOrRootOrgRecipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(ConsistencyMode.IgnoreInvalid, sessionSettings, 5124, "GetNestedSecurityGroupMembership", "f:\\15.00.1497\\sources\\dev\\Configuration\\src\\ObjectModel\\rbac\\ExchangeRunspaceConfiguration.cs");
			tenantOrRootOrgRecipientSession.UseGlobalCatalog = true;
			ADObjectId[] result;
			using (new MonitoredScope("ExchangeRunspaceConfiguration", "gcSession.ResolveSidsToADObjectIds", AuthZLogHelper.AuthZPerfMonitors))
			{
				result = tenantOrRootOrgRecipientSession.ResolveSidsToADObjectIds(list.ToArray());
			}
			return result;
		}

		// Token: 0x06001382 RID: 4994 RVA: 0x00043824 File Offset: 0x00041A24
		private bool LocateRoleEntriesForScript(string scriptName)
		{
			RoleEntryInfo roleInfoForScript;
			try
			{
				roleInfoForScript = RoleEntryInfo.GetRoleInfoForScript(scriptName);
			}
			catch (FormatException innerException)
			{
				throw new ArgumentException("scriptName", innerException);
			}
			int num = this.allRoleEntries.BinarySearch(roleInfoForScript, RoleEntryInfo.NameComparer);
			if (num < 0)
			{
				ExTraceGlobals.AccessCheckTracer.TraceWarning<string, string>((long)this.GetHashCode(), "LocateRoleEntriesForScript({0}) returns false for user {1} because this script is not in role", scriptName, this.identityName);
				return false;
			}
			return true;
		}

		// Token: 0x06001383 RID: 4995 RVA: 0x00043890 File Offset: 0x00041A90
		private void TracePublicInstanceAPIResult(bool result, string format, params object[] parameters)
		{
			if (result)
			{
				ExTraceGlobals.PublicInstanceAPITracer.TraceDebug((long)this.GetHashCode(), format, parameters);
				return;
			}
			ExTraceGlobals.PublicInstanceAPITracer.TraceWarning((long)this.GetHashCode(), format, parameters);
		}

		// Token: 0x06001384 RID: 4996 RVA: 0x000438BC File Offset: 0x00041ABC
		private List<RoleAssignmentScopeSet>[] CalculateScopesForEachParameter(string exchangeCmdletName, IList<string> parameters, OrganizationId organizationId, Task.ErrorLoggerDelegate writeError, int firstIndex, int lastIndex, ScopeType[] maximumScopesCommonForAllParams, ScopeType[,] maxParameterScopes)
		{
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			bool flag4 = false;
			bool flag5 = false;
			bool flag6 = false;
			bool flag7 = parameters.Count == 0;
			if (flag7)
			{
				parameters = ExchangeRunspaceConfiguration.noParameterArray;
			}
			List<RoleAssignmentScopeSet>[] array = new List<RoleAssignmentScopeSet>[parameters.Count];
			for (int i = 0; i < parameters.Count; i++)
			{
				bool flag8 = false;
				bool flag9 = false;
				bool flag10 = false;
				bool flag11 = false;
				bool flag12 = false;
				bool flag13 = false;
				bool flag14 = false;
				bool flag15 = false;
				List<RoleAssignmentScopeSet> list = new List<RoleAssignmentScopeSet>();
				for (int j = firstIndex; j <= lastIndex; j++)
				{
					if (flag7 || this.allRoleEntries[j].RoleEntry.ContainsParameter(parameters[i]))
					{
						RoleAssignmentScopeSet scopeSet = this.allRoleEntries[j].ScopeSet;
						if (scopeSet != null)
						{
							list.Add(scopeSet);
							if (scopeSet.RecipientReadScope.ScopeType == ScopeType.Organization)
							{
								flag8 = true;
								flag10 = true;
							}
							if (scopeSet.RecipientWriteScope.ScopeType == ScopeType.Organization)
							{
								flag9 = true;
								flag11 = true;
							}
							if (scopeSet.RecipientReadScope.ScopeType == ScopeType.MyGAL)
							{
								flag10 = true;
							}
							if (scopeSet.RecipientWriteScope.ScopeType == ScopeType.MyGAL)
							{
								flag11 = true;
							}
							if (scopeSet.ConfigReadScope.ScopeType == ScopeType.OrganizationConfig)
							{
								flag13 = true;
							}
							if (scopeSet.ConfigWriteScope.ScopeType == ScopeType.OrganizationConfig)
							{
								flag14 = true;
							}
							if (scopeSet.RecipientWriteScope.ScopeType == ScopeType.ExclusiveRecipientScope)
							{
								flag12 = true;
							}
							if (scopeSet.ConfigWriteScope.ScopeType == ScopeType.ExclusiveConfigScope)
							{
								flag15 = true;
							}
						}
					}
				}
				if (list.Count == 0)
				{
					ExTraceGlobals.AccessDeniedTracer.TraceError<string, string, string>((long)this.GetHashCode(), "CalculateScopeSetForExchangeCmdlet({0}) returns null (NO SCOPE) for user {1} because no role assignments with parameter {2} were found.", exchangeCmdletName, this.identityName, parameters[i]);
					if (writeError != null)
					{
						AuthZLogger.SafeAppendGenericError("ERC.CalculateScopeSetForExchangeCmdlet", string.Format("CalculateScopeSetForExchangeCmdlet({0}) returns null (NO SCOPE) for user {1} because no role assignments with parameter {2} were found.", exchangeCmdletName, this.identityName, parameters[i]), false);
						TaskLogger.LogRbacEvent(TaskEventLogConstants.Tuple_CmdletAccessDenied_InvalidParameter, this.IdentityName + exchangeCmdletName, new object[]
						{
							exchangeCmdletName,
							this.IdentityName
						});
						writeError(new CmdletAccessDeniedException(Strings.NoRoleEntriesWithParameterFound(exchangeCmdletName, parameters[i])), (ExchangeErrorCategory)18, null);
					}
					return null;
				}
				if (flag12)
				{
					flag11 = false;
					flag9 = false;
				}
				if (flag15)
				{
					flag14 = false;
				}
				array[i] = list;
				flag = ((flag || i == 0) && flag8);
				flag2 = ((flag2 || i == 0) && flag9);
				flag3 = ((flag3 || i == 0) && flag10);
				flag4 = ((flag4 || i == 0) && flag11);
				flag5 = ((flag5 || i == 0) && flag13);
				flag6 = ((flag6 || i == 0) && flag14);
				maxParameterScopes[i, 0] = (flag8 ? ScopeType.Organization : (flag10 ? ScopeType.MyGAL : ScopeType.None));
				maxParameterScopes[i, 1] = (flag9 ? ScopeType.Organization : (flag11 ? ScopeType.MyGAL : ScopeType.None));
				maxParameterScopes[i, 2] = (flag13 ? ScopeType.OrganizationConfig : ScopeType.None);
				maxParameterScopes[i, 3] = (flag14 ? ScopeType.OrganizationConfig : ScopeType.None);
			}
			if (flag)
			{
				maximumScopesCommonForAllParams[0] = ScopeType.Organization;
			}
			else if (flag3)
			{
				maximumScopesCommonForAllParams[0] = ScopeType.MyGAL;
			}
			if (flag2)
			{
				maximumScopesCommonForAllParams[1] = ScopeType.Organization;
			}
			else if (flag4)
			{
				maximumScopesCommonForAllParams[1] = ScopeType.MyGAL;
			}
			if (flag5)
			{
				maximumScopesCommonForAllParams[2] = ScopeType.OrganizationConfig;
			}
			if (flag6)
			{
				maximumScopesCommonForAllParams[3] = ScopeType.OrganizationConfig;
			}
			return array;
		}

		// Token: 0x06001385 RID: 4997 RVA: 0x00043BD8 File Offset: 0x00041DD8
		private ScopeSet AggregateScopes(OrganizationId organizationId, List<RoleAssignmentScopeSet>[] parameterScopes, ScopeType[] maximumScopesCommonForAllParams, ScopeType[,] maxParameterScopes)
		{
			RbacScope rbacScope = new RbacScope(maximumScopesCommonForAllParams[2]);
			rbacScope.PopulateRootAndFilter(organizationId, this.executingUser);
			List<ADScopeCollection> combinableScopeCollections = this.AggregateScopeForLocation(ScopeLocation.RecipientRead, organizationId, parameterScopes, maximumScopesCommonForAllParams, maxParameterScopes, this.ExecutingUserIsHiddenFromGAL);
			List<ADScopeCollection> recipientWriteScopes = this.AggregateScopeForLocation(ScopeLocation.RecipientWrite, organizationId, parameterScopes, maximumScopesCommonForAllParams, maxParameterScopes, this.ExecutingUserIsHiddenFromGAL);
			ADScope recipientReadScope = ADScope.CombineScopeCollections(combinableScopeCollections);
			ADScope configWriteScope = null;
			Dictionary<string, IList<ADScopeCollection>> dictionary = null;
			if (rbacScope.ScopeType == ScopeType.None)
			{
				configWriteScope = ADScope.NoAccess;
			}
			else
			{
				bool flag = true;
				List<ADScopeCollection> list = this.AggregateScopeForLocation(ScopeLocation.ConfigWrite, organizationId, parameterScopes, maximumScopesCommonForAllParams, maxParameterScopes, this.ExecutingUserIsHiddenFromGAL);
				if (list.Count == 1 && list[0].Count == 1)
				{
					using (IEnumerator<ADScope> enumerator = list[0].GetEnumerator())
					{
						if (enumerator.MoveNext())
						{
							ADScope adscope = enumerator.Current;
							if (((RbacScope)adscope).ScopeType != ScopeType.CustomConfigScope)
							{
								flag = false;
								configWriteScope = adscope;
							}
						}
					}
				}
				if (flag)
				{
					dictionary = new Dictionary<string, IList<ADScopeCollection>>();
					Dictionary<string, List<ADScope>> dictionary2 = null;
					List<ADScope> list2 = null;
					foreach (ADScopeCollection adscopeCollection in list)
					{
						dictionary2 = new Dictionary<string, List<ADScope>>();
						list2 = new List<ADScope>();
						foreach (ADScope adscope2 in adscopeCollection)
						{
							RbacScope rbacScope2 = adscope2 as RbacScope;
							if (rbacScope2 != null)
							{
								string key;
								switch (rbacScope2.ScopeRestrictionType)
								{
								case ScopeRestrictionType.NotApplicable:
									list2.Add(rbacScope2);
									continue;
								case ScopeRestrictionType.DomainScope_Obsolete:
								case ScopeRestrictionType.RecipientScope:
								case ScopeRestrictionType.PartnerDelegatedTenantScope:
									continue;
								case ScopeRestrictionType.ServerScope:
									key = "Server";
									break;
								case ScopeRestrictionType.DatabaseScope:
									key = "Database";
									break;
								default:
									continue;
								}
								RBACHelper.AddValueToDictionaryList<ADScope>(dictionary2, key, rbacScope2);
							}
						}
						foreach (KeyValuePair<string, List<ADScope>> keyValuePair in dictionary2)
						{
							IList<ADScopeCollection> list3 = null;
							if (!dictionary.TryGetValue(keyValuePair.Key, out list3))
							{
								list3 = new List<ADScopeCollection>();
								dictionary.Add(keyValuePair.Key, list3);
							}
							if (list2.Count != 0)
							{
								List<ADScope> list4 = new List<ADScope>(keyValuePair.Value);
								list4.AddRange(list2);
								list3.Add(new ADScopeCollection(list4));
							}
							else
							{
								list3.Add(new ADScopeCollection(keyValuePair.Value));
							}
						}
					}
				}
			}
			return new ScopeSet(recipientReadScope, recipientWriteScopes, this.exclusiveRecipientScopes, rbacScope, configWriteScope, dictionary, this.exclusiveObjectSpecificConfigScopes);
		}

		// Token: 0x06001386 RID: 4998 RVA: 0x00043EC4 File Offset: 0x000420C4
		private List<ADScopeCollection> AggregateScopeForLocation(ScopeLocation scopeLocation, OrganizationId organizationId, List<RoleAssignmentScopeSet>[] parameterScopes, ScopeType[] maximumScopesCommonForAllParams, ScopeType[,] maxParameterScopes, bool hiddenFromGAL)
		{
			ScopeType scopeType = maximumScopesCommonForAllParams[(int)scopeLocation];
			if (scopeType != ScopeType.None && scopeType != ScopeType.MyGAL)
			{
				RbacScope rbacScope = new RbacScope(scopeType);
				rbacScope.PopulateRootAndFilter(organizationId, this.executingUser);
				return new List<ADScopeCollection>(1)
				{
					new ADScopeCollection(new ADScope[]
					{
						rbacScope
					})
				};
			}
			List<ADScopeCollection> list = new List<ADScopeCollection>();
			for (int i = 0; i < parameterScopes.Length; i++)
			{
				List<RoleAssignmentScopeSet> list2 = parameterScopes[i];
				List<ADScope> list3 = null;
				for (int j = 0; j < list2.Count; j++)
				{
					RbacScope rbacScope2 = list2[j][scopeLocation];
					if (rbacScope2.Exclusive || !rbacScope2.IsScopeTypeSmallerThan(maxParameterScopes[i, (int)scopeLocation], hiddenFromGAL))
					{
						if (list3 == null)
						{
							list3 = new List<ADScope>();
						}
						if (!rbacScope2.IsPresentInCollection(list3))
						{
							rbacScope2.PopulateRootAndFilter(organizationId, this.executingUser);
							list3.Add(rbacScope2);
						}
					}
				}
				if (!ExchangeRunspaceConfiguration.SameScopeCollectionExists(list, list3))
				{
					list.Add(new ADScopeCollection(list3));
				}
			}
			if (list.Count > 1)
			{
				ExchangeRunspaceConfiguration.TrimAggregatedScopes(list, hiddenFromGAL);
			}
			return list;
		}

		// Token: 0x06001387 RID: 4999 RVA: 0x00043FD4 File Offset: 0x000421D4
		private void AddClientSpecificRoleEntries(ref List<RoleEntryInfo> userAllRoleEntries)
		{
			ExchangeRunspaceConfigurationSettings.ExchangeApplication clientApplication = this.ConfigurationSettings.ClientApplication;
			if (clientApplication != ExchangeRunspaceConfigurationSettings.ExchangeApplication.EMC)
			{
				return;
			}
			this.AddEMCSpecificRoleEntries(ref userAllRoleEntries);
		}

		// Token: 0x06001388 RID: 5000 RVA: 0x00043FFC File Offset: 0x000421FC
		private void AddEMCSpecificRoleEntries(ref List<RoleEntryInfo> userAllRoleEntries)
		{
			foreach (RoleEntryInfo roleEntryInfo in ClientRoleEntries.EMCRequiredRoleEntries)
			{
				int num = userAllRoleEntries.BinarySearch(roleEntryInfo, RoleEntryInfo.NameComparer);
				if (num < 0)
				{
					userAllRoleEntries.Insert(~num, roleEntryInfo);
					ExTraceGlobals.PublicInstanceAPITracer.TraceDebug<RoleEntryInfo>((long)this.GetHashCode(), "ERC.AddEMCSpecificRoleEntries adding entry {0} to the current ExchangeRunspaceConfiguration.", roleEntryInfo);
				}
			}
		}

		// Token: 0x06001389 RID: 5001 RVA: 0x00044054 File Offset: 0x00042254
		private void AddClientSpecificParameters(ref List<RoleEntryInfo> userAllRoleEntries)
		{
			ExchangeRunspaceConfigurationSettings.ExchangeApplication clientApplication = this.ConfigurationSettings.ClientApplication;
			if (clientApplication != ExchangeRunspaceConfigurationSettings.ExchangeApplication.ECP && clientApplication != ExchangeRunspaceConfigurationSettings.ExchangeApplication.ReportingWebService && clientApplication != ExchangeRunspaceConfigurationSettings.ExchangeApplication.OSP)
			{
				return;
			}
			this.AddSpecificParameters(ClientRoleEntries.ECPRequiredParameters, ref userAllRoleEntries);
			this.AddSpecificParameters(ClientRoleEntries.EngineeringFundamentalsReportingRequiredParameters, ref userAllRoleEntries);
			this.AddSpecificParameters(ClientRoleEntries.TenantReportingRequiredParameters, ref userAllRoleEntries);
		}

		// Token: 0x0600138A RID: 5002 RVA: 0x000440A0 File Offset: 0x000422A0
		private void AddSpecificParameters(RoleEntry[] entryParametersToAdd, ref List<RoleEntryInfo> userAllRoleEntries)
		{
			if (entryParametersToAdd == null || entryParametersToAdd.Length == 0)
			{
				return;
			}
			int num = 0;
			int i = 0;
			RoleEntry roleEntry = entryParametersToAdd[num];
			int num2 = 0;
			int num3 = 0;
			bool flag = false;
			while (i < userAllRoleEntries.Count)
			{
				RoleEntry roleEntry2 = userAllRoleEntries[i].RoleEntry;
				if (RoleEntry.CompareRoleEntriesByName(roleEntry2, roleEntry) == 0)
				{
					if (num2 == 0)
					{
						num2 = i;
					}
					num3++;
					if (!roleEntry2.ContainsAllParameters(new List<string>(roleEntry.Parameters)))
					{
						flag = true;
						ExTraceGlobals.PublicInstanceAPITracer.TraceDebug<RoleEntry, RoleEntry>((long)this.GetHashCode(), "ERC.AddECPSpecificParameters adding parameters entry {0} to the existing entry {1}.", roleEntry2, roleEntry);
						userAllRoleEntries[i] = new RoleEntryInfo(RoleEntry.MergeParameters(new RoleEntry[]
						{
							roleEntry2,
							roleEntry
						}), userAllRoleEntries[i].RoleAssignment);
					}
					i++;
				}
				else if (RoleEntry.CompareRoleEntriesByName(roleEntry2, roleEntry) > 0)
				{
					if (flag)
					{
						userAllRoleEntries.Sort(num2, num3, RoleEntryInfo.NameAndInstanceHashCodeComparer);
						flag = false;
					}
					num2 = 0;
					num3 = 0;
					if (num + 1 >= entryParametersToAdd.Length)
					{
						return;
					}
					roleEntry = entryParametersToAdd[++num];
				}
				else
				{
					i++;
				}
			}
		}

		// Token: 0x0600138B RID: 5003 RVA: 0x000441A4 File Offset: 0x000423A4
		private bool ContainsOnlySelfScopes(IList<ADScopeCollection> recipientWriteScopes)
		{
			if (recipientWriteScopes.Count == 0)
			{
				return false;
			}
			foreach (ADScopeCollection adscopeCollection in recipientWriteScopes)
			{
				foreach (ADScope adscope in adscopeCollection)
				{
					RbacScope rbacScope = adscope as RbacScope;
					if (rbacScope == null || rbacScope.ScopeType != ScopeType.Self)
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x0600138C RID: 5004 RVA: 0x00044248 File Offset: 0x00042448
		private RoleEntry TrimRoleEntryParametersWithValidationRules(RoleEntry roleEntry, bool onlyOrgLevelValidationRules = false)
		{
			return this.TrimRoleEntryParametersWithValidationRules(roleEntry, this.executingUser, onlyOrgLevelValidationRules);
		}

		// Token: 0x0600138D RID: 5005 RVA: 0x00044258 File Offset: 0x00042458
		private RoleEntry TrimRoleEntryParametersWithValidationRules(RoleEntry roleEntry, ADRawEntry targetRulesUser, bool onlyOrgLevelValidationRules = false)
		{
			ExTraceGlobals.AccessCheckTracer.TraceDebug<RoleEntry, string>((long)this.GetHashCode(), "Entering ERC.TrimRoleEntryParametersWithValidationRules('{0}', '{1}').", roleEntry, targetRulesUser.GetDistinguishedNameOrName());
			IList<ValidationRule> list = null;
			if (this.ApplyValidationRules)
			{
				list = ValidationRuleFactory.GetApplicableValidationRules((roleEntry is CmdletRoleEntry) ? ((CmdletRoleEntry)roleEntry).FullName : roleEntry.Name, (IList<string>)roleEntry.Parameters, this.executingUser);
				if (onlyOrgLevelValidationRules)
				{
					List<ValidationRule> list2 = new List<ValidationRule>();
					foreach (ValidationRule validationRule in list)
					{
						if (validationRule is OrganizationValidationRule)
						{
							list2.Add(validationRule);
						}
					}
					list = list2;
				}
			}
			if (list != null && list.Count > 0)
			{
				List<string> list3 = new List<string>(roleEntry.Parameters);
				this.TrimParameterListWithValidationRules(targetRulesUser, list3, list);
				if (list3.Count != roleEntry.Parameters.Count)
				{
					return roleEntry.Clone(list3);
				}
			}
			ExTraceGlobals.AccessCheckTracer.TraceDebug<RoleEntry, string>((long)this.GetHashCode(), "ERC.TrimRoleEntryParametersWithValidationRules('{0}', '{1}'). Returns SAME ROLE ENTRY", roleEntry, targetRulesUser.GetDistinguishedNameOrName());
			return roleEntry;
		}

		// Token: 0x0600138E RID: 5006 RVA: 0x00044390 File Offset: 0x00042590
		private void TrimParameterListWithValidationRules(ADRawEntry objectToCheck, IList<string> parameters, IList<ValidationRule> validationRules)
		{
			bool flag = false;
			using (IEnumerator<ValidationRule> enumerator = validationRules.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					ValidationRule rule = enumerator.Current;
					if (objectToCheck.Id == null || this.executingUser.Id == null || !objectToCheck.Id.Equals(this.executingUser.Id))
					{
						goto IL_EB;
					}
					CachedValidationRuleResult cachedValidationRuleResult = null;
					try
					{
						this.validationRuleCacheResultsLock.EnterReadLock();
						cachedValidationRuleResult = this.validationRuleCacheResults.FirstOrDefault((CachedValidationRuleResult x) => x.RuleName.Equals(rule.Name, StringComparison.OrdinalIgnoreCase));
					}
					finally
					{
						this.validationRuleCacheResultsLock.ExitReadLock();
					}
					RuleValidationException ex;
					if (cachedValidationRuleResult == null)
					{
						flag = rule.TryValidate(objectToCheck, out ex);
						try
						{
							this.validationRuleCacheResultsLock.EnterWriteLock();
							this.validationRuleCacheResults.Add(new CachedValidationRuleResult(rule.Name, flag));
							goto IL_FB;
						}
						finally
						{
							this.validationRuleCacheResultsLock.ExitWriteLock();
						}
						goto IL_EB;
					}
					flag = cachedValidationRuleResult.EvaluationResult;
					IL_FB:
					if (!flag)
					{
						foreach (string text in rule.Parameters)
						{
							if (parameters.Count == 0)
							{
								return;
							}
							ExTraceGlobals.AccessCheckTracer.TraceDebug<string, string, string>((long)this.GetHashCode(), "ERC.TrimParameterListWithValidationRules('{0}'). Removing Parameter '{1}' per Validation Rule '{2}' failure.", objectToCheck.GetDistinguishedNameOrName(), text, rule.Name);
							parameters.Remove(text);
						}
						continue;
					}
					continue;
					IL_EB:
					flag = rule.TryValidate(objectToCheck, out ex);
					goto IL_FB;
				}
			}
		}

		// Token: 0x0600138F RID: 5007 RVA: 0x00044588 File Offset: 0x00042788
		private ADSessionSettings GetADSessionSettings()
		{
			if (!Datacenter.IsMultiTenancyEnabled())
			{
				ExTraceGlobals.RunspaceConfigTracer.TraceDebug((long)this.GetHashCode(), "ERC.GetADSessionSettings. Ent Sku, returning ADSessionSettings.FromRootOrgScopeSet().");
				return ADSessionSettings.FromRootOrgScopeSet();
			}
			ExTraceGlobals.RunspaceConfigTracer.TraceDebug((long)this.GetHashCode(), "ERC.GetADSessionSettings. OrganizationId {0} , DelegatedPrincipal {1} , HasLinkedRoleGroups {2}, \r\n                    LogonUser {3} OrgId {4}, \r\n                    Executing User {5} OrgId {6}", new object[]
			{
				(null == this.OrganizationId) ? "<null>" : this.OrganizationId.ToString(),
				(this.DelegatedPrincipal == null) ? "<null>" : this.DelegatedPrincipal.ToString(),
				this.HasLinkedRoleGroups,
				(this.executingUser == null) ? "<null>" : this.logonUser.ToString(),
				(this.executingUser != null && this.logonUser[ADObjectSchema.OrganizationId] != null) ? this.logonUser[ADObjectSchema.OrganizationId] : "<null>",
				(this.executingUser == null) ? "<null>" : this.executingUser.ToString(),
				(this.executingUser != null && this.executingUser[ADObjectSchema.OrganizationId] != null) ? this.executingUser[ADObjectSchema.OrganizationId] : "<null>"
			});
			if (null != this.OrganizationId)
			{
				if (this.OrganizationId.Equals(OrganizationId.ForestWideOrgId))
				{
					ExTraceGlobals.RunspaceConfigTracer.TraceDebug((long)this.GetHashCode(), "ERC.GetADSessionSettings. Multitenant Sku (First Org User), returning ADSessionSettings.FromRootOrgScopeSet().");
					return ADSessionSettings.FromRootOrgScopeSet();
				}
				if (this.PartnerMode)
				{
					ExTraceGlobals.RunspaceConfigTracer.TraceDebug((long)this.GetHashCode(), "ERC.GetADSessionSettings. Multitenant Sku (Partner Admin - First Org User), returning ADSessionSettings.FromRootOrgScopeSet().");
					return ADSessionSettings.FromRootOrgScopeSet();
				}
				ExTraceGlobals.RunspaceConfigTracer.TraceDebug((long)this.GetHashCode(), "ERC.GetADSessionSettings. Multitenant Sku (Tenant User), returning ADSessionSettings.FromAllTenantsPartitionId().");
				return ADSessionSettings.FromAllTenantsPartitionId(this.OrganizationId.PartitionId);
			}
			else
			{
				if (this.DelegatedPrincipal != null)
				{
					ExTraceGlobals.RunspaceConfigTracer.TraceDebug((long)this.GetHashCode(), "ERC.GetADSessionSettings. Multitenant Sku (Delegated Principal User), returning ADSessionSettings.FromAllTenantsPartitionId().");
					return ADSessionSettings.FromAllTenantsPartitionId(ADAccountPartitionLocator.GetPartitionIdByAcceptedDomainName(this.DelegatedPrincipal.DelegatedOrganization));
				}
				if (this.HasLinkedRoleGroups)
				{
					ExTraceGlobals.RunspaceConfigTracer.TraceDebug((long)this.GetHashCode(), "ERC.GetADSessionSettings. Multitenant Sku (Linked RoleGroups user), returning ADSessionSettings.FromRootOrgScopeSet().");
					return ADSessionSettings.FromRootOrgScopeSet();
				}
				if (this.logonUser != null && this.logonUser[ADObjectSchema.OrganizationId] != null)
				{
					ExTraceGlobals.RunspaceConfigTracer.TraceDebug((long)this.GetHashCode(), "ERC.GetADSessionSettings. Multitenant Sku (logon user org).");
					OrganizationId organizationId = (OrganizationId)this.logonUser[ADObjectSchema.OrganizationId];
					if (!OrganizationId.ForestWideOrgId.Equals(organizationId))
					{
						return ADSessionSettings.FromAllTenantsPartitionId(organizationId.PartitionId);
					}
					return ADSessionSettings.FromRootOrgScopeSet();
				}
				else
				{
					if (this.executingUser == null || this.executingUser[ADObjectSchema.OrganizationId] == null)
					{
						return ADSessionSettings.FromRootOrgScopeSet();
					}
					ExTraceGlobals.RunspaceConfigTracer.TraceDebug((long)this.GetHashCode(), "ERC.GetADSessionSettings. Multitenant Sku (executing user org Id {0}).");
					OrganizationId organizationId2 = (OrganizationId)this.logonUser[ADObjectSchema.OrganizationId];
					if (!OrganizationId.ForestWideOrgId.Equals(organizationId2))
					{
						return ADSessionSettings.FromAllTenantsPartitionId(organizationId2.PartitionId);
					}
					return ADSessionSettings.FromRootOrgScopeSet();
				}
			}
		}

		// Token: 0x06001390 RID: 5008 RVA: 0x00044878 File Offset: 0x00042A78
		protected void RefreshCombinedCmdletsAndScripts()
		{
			using (new MonitoredScope("ExchangeRunspaceConfiguration", "RefreshCombinedCmdletsAndScripts", AuthZLogHelper.AuthZPerfMonitors))
			{
				this.combinedCmdlets = null;
				this.combinedScripts = null;
				this.CombineRoleEntries(this.allRoleEntries, out this.combinedCmdlets, out this.combinedScripts);
				bool flag = this.intersectRoleEntries;
				this.combinedCmdlets.Add(ExchangeRunspaceConfiguration.getHelpRoleEntry);
				this.combinedCmdlets.Add(ExchangeRunspaceConfiguration.getCommandRoleEntry);
				if (this.IsPowerShellWebService)
				{
					this.combinedCmdlets.Add(ExchangeRunspaceConfiguration.convertToExchangeXmlEntry);
				}
				if (this.settings.IsProxy)
				{
					for (int i = 0; i < this.combinedCmdlets.Count; i++)
					{
						CmdletRoleEntry cmdletRoleEntry = this.combinedCmdlets[i] as CmdletRoleEntry;
						if (cmdletRoleEntry != null)
						{
							IList<RoleEntry> list = new List<RoleEntry>(2);
							list.Add(cmdletRoleEntry);
							list.Add(new CmdletRoleEntry(cmdletRoleEntry.Name, cmdletRoleEntry.PSSnapinName, ClientRoleEntries.ParametersForProxy));
							this.combinedCmdlets[i] = RoleEntry.MergeParameters(list);
						}
					}
				}
			}
		}

		// Token: 0x06001391 RID: 5009 RVA: 0x00044998 File Offset: 0x00042B98
		protected void RefreshVerboseDebugEnabledCmdlets(IEnumerable<RoleEntry> cmdlets)
		{
			this.verboseEnabledCmdlets.Clear();
			this.debugEnabledCmdlets.Clear();
			foreach (RoleEntry roleEntry in cmdlets)
			{
				if (roleEntry.Parameters.Contains("Verbose", StringComparer.OrdinalIgnoreCase))
				{
					this.verboseEnabledCmdlets.Add(roleEntry.Name);
				}
				if (roleEntry.Parameters.Contains("Debug", StringComparer.OrdinalIgnoreCase))
				{
					this.debugEnabledCmdlets.Add(roleEntry.Name);
				}
			}
		}

		// Token: 0x06001392 RID: 5010 RVA: 0x00044A44 File Offset: 0x00042C44
		private void FaultInjection_ByPassRBACTestSnapinCheck(ref string cmdletFullName, string exchangeCmdletName)
		{
			if (ExTraceGlobals.FaultInjectionTracer.IsTraceEnabled(TraceType.FaultInjection))
			{
				string empty = string.Empty;
				string empty2 = string.Empty;
				ExTraceGlobals.FaultInjectionTracer.TraceTest<string>(4257623357U, ref empty);
				ExTraceGlobals.FaultInjectionTracer.TraceTest<string>(3385208125U, ref empty2);
				if (!string.IsNullOrEmpty(empty) && !string.IsNullOrEmpty(empty2) && empty2.IndexOf(exchangeCmdletName, StringComparison.OrdinalIgnoreCase) >= 0)
				{
					cmdletFullName = empty + "\\" + exchangeCmdletName;
				}
			}
		}

		// Token: 0x06001393 RID: 5011 RVA: 0x00044AB8 File Offset: 0x00042CB8
		internal string GetRBACInformationSummary()
		{
			if (string.IsNullOrEmpty(this.rbacInformationSummary))
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.AppendFormat("ServicePlan:{0};", this.ServicePlanForLogging);
				stringBuilder.AppendFormat("IsAdmin:{0};", this.HasAdminRoles);
				this.rbacInformationSummary = stringBuilder.ToString();
			}
			return this.rbacInformationSummary;
		}

		// Token: 0x06001394 RID: 5012 RVA: 0x00044B14 File Offset: 0x00042D14
		private string GetUserFriendlyRoleAssignmentInfo(Result<ExchangeRoleAssignment>[] roleAssignmentResults)
		{
			Dictionary<RoleAssigneeType, HashSet<string>> dictionary = new Dictionary<RoleAssigneeType, HashSet<string>>(16);
			foreach (Result<ExchangeRoleAssignment> result in roleAssignmentResults)
			{
				ExchangeRoleAssignment data = result.Data;
				if (!dictionary.ContainsKey(data.RoleAssigneeType))
				{
					dictionary.Add(data.RoleAssigneeType, new HashSet<string>());
				}
				if (data.RoleAssigneeType == RoleAssigneeType.User)
				{
					dictionary[data.RoleAssigneeType].Add(string.Format("{0}[CRW:{1} CCW:{2} RRS:{3} RWS{4} CRS:{5} CWS:{6}]", new object[]
					{
						data.Role.Name,
						data.CustomRecipientWriteScope,
						data.CustomConfigWriteScope,
						data.RecipientReadScope,
						data.RecipientWriteScope,
						data.ConfigReadScope,
						data.ConfigWriteScope
					}));
				}
				else
				{
					dictionary[data.RoleAssigneeType].Add(data.User.Name);
				}
			}
			StringBuilder stringBuilder = new StringBuilder();
			foreach (RoleAssigneeType roleAssigneeType in dictionary.Keys)
			{
				if (roleAssigneeType.Equals(RoleAssigneeType.User))
				{
					stringBuilder.AppendFormat("Roles{{", new object[0]);
				}
				else
				{
					stringBuilder.AppendFormat("{0}{{", roleAssigneeType.ToString());
				}
				foreach (string arg in dictionary[roleAssigneeType])
				{
					stringBuilder.AppendFormat("{0}:", arg);
				}
				stringBuilder.AppendFormat("}}", new object[0]);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x040004C3 RID: 1219
		internal const string ExchangeSetupPSSnapinName = "Microsoft.Exchange.Management.PowerShell.Setup";

		// Token: 0x040004C4 RID: 1220
		internal const string ExchangeAdminPSSnapinName = "Microsoft.Exchange.Management.PowerShell.E2010";

		// Token: 0x040004C5 RID: 1221
		internal const string ExchangeCentralAdminPSSnapinName = "Microsoft.Exchange.Management.PowerShell.CentralAdmin";

		// Token: 0x040004C6 RID: 1222
		internal const string ExchangeFfoCentralAdminPSSnapinName = "Microsoft.Exchange.Management.PowerShell.FfoCentralAdmin";

		// Token: 0x040004C7 RID: 1223
		internal const string ExchangeFfoAntiSpamPSSnapinName = "Microsoft.Forefront.AntiSpam.PowerShell";

		// Token: 0x040004C8 RID: 1224
		internal const string ExchangeFfoManagementPSSnapinName = "Microsoft.Forefront.Management.PowerShell";

		// Token: 0x040004C9 RID: 1225
		internal const string ExchangeSupportPSSnapinName = "Microsoft.Exchange.Management.PowerShell.Support";

		// Token: 0x040004CA RID: 1226
		internal const string ExchangeOspPSSnapinName = "Microsoft.Exchange.Monitoring.ServiceHealth";

		// Token: 0x040004CB RID: 1227
		internal const string ExchangeOspRptPSSnapinName = "Microsoft.Exchange.Monitoring.Reporting";

		// Token: 0x040004CC RID: 1228
		internal const string ExchangeDataCenterCommonPSSnapinName = "Microsoft.Datacenter.Common";

		// Token: 0x040004CD RID: 1229
		internal const string ExchangeMonitoringAzureManagementPSSnapinName = "Microsoft.Exchange.Datacenter.Management.ActiveMonitoring";

		// Token: 0x040004CE RID: 1230
		internal const string ExchangeNewCapacityPSSnapinName = "Microsoft.Exchange.ServiceManagement.NewCapacityPSSnapIn";

		// Token: 0x040004CF RID: 1231
		internal const string DataMiningConfigurationPSSnapinName = "Microsoft.Datacenter.DataMining.Configuration.Powershell";

		// Token: 0x040004D0 RID: 1232
		internal const string ReportExchangePSSnapinName = "Microsoft.Datacenter.Reporting.Exchange.Powershell";

		// Token: 0x040004D1 RID: 1233
		internal const string ReportLyncPSSnapinName = "Microsoft.Datacenter.Reporting.Lync.Powershell";

		// Token: 0x040004D2 RID: 1234
		internal const string GenericExchangeSnapinName = "Exchange";

		// Token: 0x040004D3 RID: 1235
		internal const string SupportLyncPSSnapinName = "Microsoft.Datacenter.Support.Lync.Powershell";

		// Token: 0x040004D4 RID: 1236
		internal const string TorusPSSnapinName = "Microsoft.Office.Datacenter.Torus.Cmdlets";

		// Token: 0x040004D5 RID: 1237
		internal const string ImpersonationMethod = "Impersonate-ExchangeUser";

		// Token: 0x040004D6 RID: 1238
		internal const string DedicatedTenantAdminRoleNamePrefix = "SSA_";

		// Token: 0x040004D7 RID: 1239
		private const int estimatedRoleEntryCount = 100;

		// Token: 0x040004D8 RID: 1240
		private const int estimatedParameterCount = 10;

		// Token: 0x040004D9 RID: 1241
		private const int estimatedScriptCount = 5;

		// Token: 0x040004DA RID: 1242
		internal static readonly System.Collections.ObjectModel.ReadOnlyCollection<string> ExchangeSnapins = new System.Collections.ObjectModel.ReadOnlyCollection<string>(new string[]
		{
			"Microsoft.Exchange.Management.PowerShell.E2010",
			"Microsoft.Exchange.Management.PowerShell.Setup",
			"Microsoft.Exchange.Management.PowerShell.CentralAdmin",
			"Microsoft.Exchange.Management.PowerShell.FfoCentralAdmin",
			"Microsoft.Forefront.AntiSpam.PowerShell",
			"Microsoft.Forefront.Management.PowerShell",
			"Exchange",
			"Microsoft.Exchange.Management.PowerShell.Support",
			"Microsoft.Exchange.Monitoring.ServiceHealth",
			"Microsoft.Exchange.Monitoring.Reporting",
			"Microsoft.Datacenter.Common",
			"Microsoft.Exchange.Datacenter.Management.ActiveMonitoring",
			"Microsoft.Exchange.ServiceManagement.NewCapacityPSSnapIn",
			"Microsoft.Datacenter.DataMining.Configuration.Powershell",
			"Microsoft.Datacenter.Reporting.Exchange.Powershell",
			"Microsoft.Datacenter.Reporting.Lync.Powershell",
			"Microsoft.Office.Datacenter.Torus.Cmdlets"
		});

		// Token: 0x040004DB RID: 1243
		internal static readonly System.Collections.ObjectModel.ReadOnlyCollection<string> AdminSnapins = new System.Collections.ObjectModel.ReadOnlyCollection<string>(new string[]
		{
			"Microsoft.Exchange.Management.PowerShell.E2010"
		});

		// Token: 0x040004DC RID: 1244
		internal static readonly System.Collections.ObjectModel.ReadOnlyCollection<string> OspSnapins = new System.Collections.ObjectModel.ReadOnlyCollection<string>(new string[]
		{
			"Microsoft.Exchange.Management.PowerShell.E2010",
			"Microsoft.Exchange.Management.PowerShell.CentralAdmin",
			"Microsoft.Exchange.Monitoring.ServiceHealth",
			"Microsoft.Exchange.Monitoring.Reporting",
			"Microsoft.Datacenter.Common",
			"Microsoft.Exchange.Datacenter.Management.ActiveMonitoring",
			"Microsoft.Datacenter.DataMining.Configuration.Powershell",
			"Microsoft.Datacenter.Reporting.Exchange.Powershell",
			"Microsoft.Datacenter.Reporting.Lync.Powershell",
			"Microsoft.Datacenter.Support.Lync.Powershell"
		});

		// Token: 0x040004DD RID: 1245
		private static RoleEntryInfo welcomeMessageScriptInitializer = new RoleEntryInfo(new ScriptRoleEntry("ExchangeFismaBannerMessage.ps1", null));

		// Token: 0x040004DE RID: 1246
		private static ADPropertyDefinition[] ercUserPropertyArray = new ADPropertyDefinition[]
		{
			ADObjectSchema.RawName,
			ADObjectSchema.Name,
			ADObjectSchema.Id,
			ADRecipientSchema.WindowsLiveID,
			ADObjectSchema.ExchangeVersion,
			IADSecurityPrincipalSchema.Sid,
			ADRecipientSchema.MasterAccountSid,
			ADObjectSchema.OrganizationId,
			ADRecipientSchema.DisplayName,
			ADRecipientSchema.AddressListMembership,
			ADRecipientSchema.RemotePowerShellEnabled,
			ADRecipientSchema.ECPEnabled,
			ADUserSchema.OwaMailboxPolicy,
			ADUserSchema.RetentionPolicy,
			ADRecipientSchema.OWAEnabled,
			ADRecipientSchema.EmailAddresses,
			ADUserSchema.ActiveSyncEnabled,
			ADMailboxRecipientSchema.ExternalOofOptions,
			ADRecipientSchema.MAPIEnabled,
			ADUserSchema.UMEnabled,
			ADRecipientSchema.MailboxPlan,
			ADRecipientSchema.RoleAssignmentPolicy,
			ADObjectSchema.ObjectClass,
			ADOrgPersonSchema.Languages,
			ADRecipientSchema.ThrottlingPolicy,
			ADRecipientSchema.HiddenFromAddressListsEnabled,
			ADRecipientSchema.PrimarySmtpAddress,
			ADRecipientSchema.RecipientType,
			ADRecipientSchema.IsPersonToPersonTextMessagingEnabled,
			ADRecipientSchema.IsMachineToPersonTextMessagingEnabled,
			ADUserSchema.PersistedCapabilities,
			ADRecipientSchema.RecipientTypeDetails,
			ADRecipientSchema.UsageLocation,
			ADRecipientSchema.RecipientTypeDetails,
			ADUserSchema.UserPrincipalName,
			ADRecipientSchema.LegacyExchangeDN,
			ADRecipientSchema.ExternalDirectoryObjectId,
			SharedPropertyDefinitions.PersistedCapabilities
		};

		// Token: 0x040004DF RID: 1247
		private static IEnumerable<ADPropertyDefinition> userFilterSchemaProperties = ObjectSchema.GetInstance<ClientAccessRulesRecipientFilterSchema>().AllProperties.OfType<ADPropertyDefinition>();

		// Token: 0x040004E0 RID: 1248
		internal static ADPropertyDefinition[] userPropertyArray = ExchangeRunspaceConfiguration.ercUserPropertyArray.Union(ExchangeRunspaceConfiguration.userFilterSchemaProperties).ToArray<ADPropertyDefinition>();

		// Token: 0x040004E1 RID: 1249
		internal static PropertyDefinition[] delegatedGroupInfo = new PropertyDefinition[]
		{
			ADObjectSchema.RawName,
			ADObjectSchema.Name,
			ADGroupSchema.LinkedPartnerGroupAndOrganizationId,
			ADObjectSchema.Id,
			ADObjectSchema.ExchangeVersion
		};

		// Token: 0x040004E2 RID: 1250
		private static readonly string[] noParameterArray = new string[]
		{
			string.Empty
		};

		// Token: 0x040004E3 RID: 1251
		private static readonly string[] emptyArray = new string[0];

		// Token: 0x040004E4 RID: 1252
		private static readonly CmdletRoleEntry getHelpRoleEntry = new CmdletRoleEntry("Get-Help", "Microsoft.PowerShell.Core", new string[]
		{
			"Name",
			"Path",
			"Category",
			"Component",
			"Functionality",
			"Role",
			"Detailed",
			"Full",
			"Examples",
			"Parameter",
			"Online",
			"Verbose",
			"Debug",
			"ErrorAction",
			"WarningAction",
			"ErrorVariable",
			"WarningVariable",
			"OutVariable",
			"OutBuffer"
		});

		// Token: 0x040004E5 RID: 1253
		private static readonly CmdletRoleEntry getCommandRoleEntry = new CmdletRoleEntry("Get-Command", "Microsoft.PowerShell.Core", new string[]
		{
			"Name",
			"Verb",
			"Noun",
			"Module",
			"CommandType",
			"TotalCount",
			"Syntax",
			"ArgumentList",
			"Verbose",
			"Debug",
			"ErrorAction",
			"WarningAction",
			"ErrorVariable",
			"WarningVariable",
			"OutVariable",
			"OutBuffer"
		});

		// Token: 0x040004E6 RID: 1254
		private static readonly CmdletRoleEntry convertToExchangeXmlEntry = new CmdletRoleEntry("ConvertTo-ExchangeXml", "Microsoft.Exchange.Management.PowerShell.E2010", new string[]
		{
			"InputObject"
		});

		// Token: 0x040004E7 RID: 1255
		private static bool? isPowershellProcess;

		// Token: 0x040004E8 RID: 1256
		private static ConcurrentDictionary<string, ADObjectId> rootOrgUSGContainers = new ConcurrentDictionary<string, ADObjectId>(StringComparer.OrdinalIgnoreCase);

		// Token: 0x040004E9 RID: 1257
		protected string identityName;

		// Token: 0x040004EA RID: 1258
		private readonly string authenticationType;

		// Token: 0x040004EB RID: 1259
		protected DelegatedPrincipal delegatedPrincipal;

		// Token: 0x040004EC RID: 1260
		protected ADRawEntry executingUser;

		// Token: 0x040004ED RID: 1261
		private ADRawEntry logonUser;

		// Token: 0x040004EE RID: 1262
		private OrganizationId organizationId;

		// Token: 0x040004EF RID: 1263
		protected IRecipientSession recipientSession;

		// Token: 0x040004F0 RID: 1264
		private System.Collections.ObjectModel.ReadOnlyCollection<string> snapinCollection;

		// Token: 0x040004F1 RID: 1265
		protected ExchangeRunspaceConfigurationSettings settings;

		// Token: 0x040004F2 RID: 1266
		private ICollection<SecurityIdentifier> tokenSids;

		// Token: 0x040004F3 RID: 1267
		protected List<RoleEntryInfo> allRoleEntries;

		// Token: 0x040004F4 RID: 1268
		protected Dictionary<RoleType, List<ADObjectId>> allRoleTypes;

		// Token: 0x040004F5 RID: 1269
		protected Dictionary<ADObjectId, ManagementScope> allScopes;

		// Token: 0x040004F6 RID: 1270
		protected List<RoleEntry> combinedCmdlets;

		// Token: 0x040004F7 RID: 1271
		private List<RoleEntry> combinedScripts;

		// Token: 0x040004F8 RID: 1272
		private IDisposable logonIdentityToDispose;

		// Token: 0x040004F9 RID: 1273
		private IDisposable impersonatedIdentityToDispose;

		// Token: 0x040004FA RID: 1274
		protected Microsoft.Exchange.Collections.ReadOnlyCollection<ExchangeRoleAssignment> allRoleAssignments;

		// Token: 0x040004FB RID: 1275
		private bool intersectRoleEntries;

		// Token: 0x040004FC RID: 1276
		private List<ADScope> exclusiveRecipientScopes;

		// Token: 0x040004FD RID: 1277
		private Dictionary<string, ADScopeCollection> exclusiveObjectSpecificConfigScopes;

		// Token: 0x040004FE RID: 1278
		protected ADScopeCollection exclusiveRecipientScopesCollection;

		// Token: 0x040004FF RID: 1279
		private SerializedAccessToken logonAccessToken;

		// Token: 0x04000500 RID: 1280
		private TroubleshootingContext troubleshootingContext = new TroubleshootingContext(ExWatson.AppName + ".Powershell");

		// Token: 0x04000501 RID: 1281
		private SerializedAccessToken impersonatedUserAccessToken;

		// Token: 0x04000502 RID: 1282
		private ProvisioningBroker provisioningBroker;

		// Token: 0x04000503 RID: 1283
		private object provisioningBrokerSyncRoot = new object();

		// Token: 0x04000504 RID: 1284
		private bool? umConfigured;

		// Token: 0x04000505 RID: 1285
		private OwaSegmentationSettings owaSegmentationSettings;

		// Token: 0x04000506 RID: 1286
		private MultiValuedProperty<CultureInfo> executingUserLanguages;

		// Token: 0x04000507 RID: 1287
		private ADRecipientOrAddress executingUserAsRecipient;

		// Token: 0x04000508 RID: 1288
		private ReaderWriterLockSlim validationRuleCacheResultsLock = new ReaderWriterLockSlim();

		// Token: 0x04000509 RID: 1289
		private ICollection<CachedValidationRuleResult> validationRuleCacheResults;

		// Token: 0x0400050A RID: 1290
		private ADObjectId[] universalSecurityGroupsCache;

		// Token: 0x0400050B RID: 1291
		private bool ensureTargetIsMemberOfRIMMailboxUsersGroup;

		// Token: 0x0400050C RID: 1292
		private object loadRoleCmdletInfoSyncRoot = new object();

		// Token: 0x0400050D RID: 1293
		private HashSet<string> verboseEnabledCmdlets = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

		// Token: 0x0400050E RID: 1294
		private HashSet<string> debugEnabledCmdlets = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

		// Token: 0x0400050F RID: 1295
		private bool enablePiiMap;

		// Token: 0x04000510 RID: 1296
		private string piiMapId;

		// Token: 0x04000511 RID: 1297
		private bool isDedicatedTenantAdmin;

		// Token: 0x04000512 RID: 1298
		private string rbacInformationSummary;

		// Token: 0x04000513 RID: 1299
		private string servicePlanForLogging;

		// Token: 0x04000514 RID: 1300
		private IList<RoleType> roleTypeFilter;

		// Token: 0x04000515 RID: 1301
		private List<RoleEntry> sortedRoleEntryFilter;

		// Token: 0x04000516 RID: 1302
		private IList<RoleType> logonUserRequiredRoleTypes;

		// Token: 0x04000517 RID: 1303
		private bool callerCheckedAccess;
	}
}
