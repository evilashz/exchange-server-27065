using System;
using System.Collections.Generic;
using System.Management.Automation;
using System.Resources;
using Microsoft.Exchange.Core;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.Management;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x0200044C RID: 1100
	[ObjectScope(new ConfigScopes[]
	{
		ConfigScopes.TenantLocal,
		ConfigScopes.TenantSubTree
	})]
	[Serializable]
	public class ExchangeRole : ADConfigurationObject
	{
		// Token: 0x17000E4E RID: 3662
		// (get) Token: 0x06003194 RID: 12692 RVA: 0x000C7862 File Offset: 0x000C5A62
		internal override bool IsShareable
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000E4F RID: 3663
		// (get) Token: 0x06003195 RID: 12693 RVA: 0x000C7865 File Offset: 0x000C5A65
		internal override ADObjectSchema Schema
		{
			get
			{
				return ExchangeRole.schema;
			}
		}

		// Token: 0x17000E50 RID: 3664
		// (get) Token: 0x06003196 RID: 12694 RVA: 0x000C786C File Offset: 0x000C5A6C
		internal override string MostDerivedObjectClass
		{
			get
			{
				return ExchangeRole.mostDerivedClass;
			}
		}

		// Token: 0x17000E51 RID: 3665
		// (get) Token: 0x06003197 RID: 12695 RVA: 0x000C7873 File Offset: 0x000C5A73
		internal override ExchangeObjectVersion MaximumSupportedExchangeObjectVersion
		{
			get
			{
				return ExchangeRoleSchema.CurrentRoleVersion;
			}
		}

		// Token: 0x17000E52 RID: 3666
		// (get) Token: 0x06003198 RID: 12696 RVA: 0x000C787A File Offset: 0x000C5A7A
		// (set) Token: 0x06003199 RID: 12697 RVA: 0x000C7891 File Offset: 0x000C5A91
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<RoleEntry> RoleEntries
		{
			get
			{
				return (MultiValuedProperty<RoleEntry>)this.propertyBag[ExchangeRoleSchema.RoleEntries];
			}
			internal set
			{
				this.propertyBag[ExchangeRoleSchema.RoleEntries] = value;
			}
		}

		// Token: 0x17000E53 RID: 3667
		// (get) Token: 0x0600319A RID: 12698 RVA: 0x000C78A4 File Offset: 0x000C5AA4
		// (set) Token: 0x0600319B RID: 12699 RVA: 0x000C78B6 File Offset: 0x000C5AB6
		public RoleType RoleType
		{
			get
			{
				return (RoleType)this[ExchangeRoleSchema.RoleType];
			}
			internal set
			{
				this[ExchangeRoleSchema.RoleType] = value;
			}
		}

		// Token: 0x17000E54 RID: 3668
		// (get) Token: 0x0600319C RID: 12700 RVA: 0x000C78C9 File Offset: 0x000C5AC9
		public ScopeType ImplicitRecipientReadScope
		{
			get
			{
				return (ScopeType)this[ExchangeRoleSchema.ImplicitRecipientReadScope];
			}
		}

		// Token: 0x17000E55 RID: 3669
		// (get) Token: 0x0600319D RID: 12701 RVA: 0x000C78DB File Offset: 0x000C5ADB
		public ScopeType ImplicitRecipientWriteScope
		{
			get
			{
				return (ScopeType)this[ExchangeRoleSchema.ImplicitRecipientWriteScope];
			}
		}

		// Token: 0x17000E56 RID: 3670
		// (get) Token: 0x0600319E RID: 12702 RVA: 0x000C78ED File Offset: 0x000C5AED
		public ScopeType ImplicitConfigReadScope
		{
			get
			{
				return (ScopeType)this[ExchangeRoleSchema.ImplicitConfigReadScope];
			}
		}

		// Token: 0x17000E57 RID: 3671
		// (get) Token: 0x0600319F RID: 12703 RVA: 0x000C78FF File Offset: 0x000C5AFF
		public ScopeType ImplicitConfigWriteScope
		{
			get
			{
				return (ScopeType)this[ExchangeRoleSchema.ImplicitConfigWriteScope];
			}
		}

		// Token: 0x17000E58 RID: 3672
		// (get) Token: 0x060031A0 RID: 12704 RVA: 0x000C7911 File Offset: 0x000C5B11
		public bool IsRootRole
		{
			get
			{
				return (bool)this[ExchangeRoleSchema.IsRootRole];
			}
		}

		// Token: 0x17000E59 RID: 3673
		// (get) Token: 0x060031A1 RID: 12705 RVA: 0x000C7923 File Offset: 0x000C5B23
		public bool IsEndUserRole
		{
			get
			{
				return (bool)this[ExchangeRoleSchema.IsEndUserRole];
			}
		}

		// Token: 0x17000E5A RID: 3674
		// (get) Token: 0x060031A2 RID: 12706 RVA: 0x000C7935 File Offset: 0x000C5B35
		internal bool IsPartnerApplicationRole
		{
			get
			{
				return ExchangeRole.IsPartnerApplicationRoleType(this.RoleType);
			}
		}

		// Token: 0x17000E5B RID: 3675
		// (get) Token: 0x060031A3 RID: 12707 RVA: 0x000C7942 File Offset: 0x000C5B42
		// (set) Token: 0x060031A4 RID: 12708 RVA: 0x000C7954 File Offset: 0x000C5B54
		public string MailboxPlanIndex
		{
			get
			{
				return (string)this[ExchangeRoleSchema.MailboxPlanIndex];
			}
			internal set
			{
				this[ExchangeRoleSchema.MailboxPlanIndex] = value;
			}
		}

		// Token: 0x17000E5C RID: 3676
		// (get) Token: 0x060031A5 RID: 12709 RVA: 0x000C7962 File Offset: 0x000C5B62
		// (set) Token: 0x060031A6 RID: 12710 RVA: 0x000C7974 File Offset: 0x000C5B74
		public string Description
		{
			get
			{
				return (string)this[ExchangeRoleSchema.Description];
			}
			set
			{
				this[ExchangeRoleSchema.Description] = value;
			}
		}

		// Token: 0x17000E5D RID: 3677
		// (get) Token: 0x060031A7 RID: 12711 RVA: 0x000C7982 File Offset: 0x000C5B82
		public ADObjectId Parent
		{
			get
			{
				return (ADObjectId)this[ExchangeRoleSchema.Parent];
			}
		}

		// Token: 0x17000E5E RID: 3678
		// (get) Token: 0x060031A8 RID: 12712 RVA: 0x000C7994 File Offset: 0x000C5B94
		// (set) Token: 0x060031A9 RID: 12713 RVA: 0x000C79A6 File Offset: 0x000C5BA6
		internal RoleState RoleState
		{
			get
			{
				return (RoleState)this[ExchangeRoleSchema.RoleState];
			}
			set
			{
				this[ExchangeRoleSchema.RoleState] = value;
			}
		}

		// Token: 0x17000E5F RID: 3679
		// (get) Token: 0x060031AA RID: 12714 RVA: 0x000C79B9 File Offset: 0x000C5BB9
		public bool IsDeprecated
		{
			get
			{
				return (bool)this[ExchangeRoleSchema.IsDeprecated];
			}
		}

		// Token: 0x17000E60 RID: 3680
		// (get) Token: 0x060031AB RID: 12715 RVA: 0x000C79CB File Offset: 0x000C5BCB
		internal bool HasDownlevelData
		{
			get
			{
				return (bool)this[ExchangeRoleSchema.HasDownlevelData];
			}
		}

		// Token: 0x17000E61 RID: 3681
		// (get) Token: 0x060031AC RID: 12716 RVA: 0x000C79DD File Offset: 0x000C5BDD
		internal bool IsUnscopedTopLevel
		{
			get
			{
				return this.IsRootRole && this.IsUnscoped;
			}
		}

		// Token: 0x17000E62 RID: 3682
		// (get) Token: 0x060031AD RID: 12717 RVA: 0x000C79EF File Offset: 0x000C5BEF
		internal bool IsUnscoped
		{
			get
			{
				return this.RoleType == RoleType.UnScoped;
			}
		}

		// Token: 0x17000E63 RID: 3683
		// (get) Token: 0x060031AE RID: 12718 RVA: 0x000C79FE File Offset: 0x000C5BFE
		// (set) Token: 0x060031AF RID: 12719 RVA: 0x000C7A06 File Offset: 0x000C5C06
		internal bool AllowEmptyRole
		{
			get
			{
				return this.allowEmptyRole;
			}
			set
			{
				this.allowEmptyRole = value;
			}
		}

		// Token: 0x060031B0 RID: 12720 RVA: 0x000C7A10 File Offset: 0x000C5C10
		static ExchangeRole()
		{
			ExchangeRole.EndUserRoleTypes = new RoleType[]
			{
				RoleType.MyOptions,
				RoleType.MyFacebookEnabled,
				RoleType.MyLinkedInEnabled,
				RoleType.MyBaseOptions,
				RoleType.MyMailSubscriptions,
				RoleType.MyContactInformation,
				RoleType.MyProfileInformation,
				RoleType.MyRetentionPolicies,
				RoleType.MyTextMessaging,
				RoleType.MyVoiceMail,
				RoleType.MyDistributionGroups,
				RoleType.MyDistributionGroupMembership,
				RoleType.MyDiagnostics,
				RoleType.MyMailboxDelegation,
				RoleType.MyTeamMailboxes,
				RoleType.MyMarketplaceApps,
				RoleType.MyCustomApps,
				RoleType.MyReadWriteMailboxApps
			};
			Array.Sort<RoleType>(ExchangeRole.EndUserRoleTypes);
			ExchangeRole.PartnerApplicationRoleTypes = new RoleType[]
			{
				RoleType.UserApplication,
				RoleType.ArchiveApplication,
				RoleType.LegalHoldApplication,
				RoleType.OfficeExtensionApplication,
				RoleType.TeamMailboxLifecycleApplication,
				RoleType.MailboxSearchApplication
			};
			Array.Sort<RoleType>(ExchangeRole.PartnerApplicationRoleTypes);
			ExchangeRole.ScopeSets = new Dictionary<RoleType, SimpleScopeSet<ScopeType>>(68);
			ExchangeRole.ScopeSets[RoleType.Custom] = new SimpleScopeSet<ScopeType>(ScopeType.NotApplicable, ScopeType.NotApplicable, ScopeType.NotApplicable, ScopeType.NotApplicable);
			ExchangeRole.ScopeSets[RoleType.UnScoped] = new SimpleScopeSet<ScopeType>(ScopeType.NotApplicable, ScopeType.NotApplicable, ScopeType.NotApplicable, ScopeType.NotApplicable);
			ExchangeRole.ScopeSets[RoleType.MyBaseOptions] = new SimpleScopeSet<ScopeType>(ScopeType.Self, ScopeType.Self, ScopeType.OrganizationConfig, ScopeType.OrganizationConfig);
			ExchangeRole.ScopeSets[RoleType.MyDiagnostics] = new SimpleScopeSet<ScopeType>(ScopeType.Self, ScopeType.Self, ScopeType.OrganizationConfig, ScopeType.OrganizationConfig);
			ExchangeRole.ScopeSets[RoleType.MyMailSubscriptions] = new SimpleScopeSet<ScopeType>(ScopeType.Self, ScopeType.Self, ScopeType.OrganizationConfig, ScopeType.OrganizationConfig);
			ExchangeRole.ScopeSets[RoleType.MyContactInformation] = new SimpleScopeSet<ScopeType>(ScopeType.Self, ScopeType.Self, ScopeType.OrganizationConfig, ScopeType.OrganizationConfig);
			ExchangeRole.ScopeSets[RoleType.MyProfileInformation] = new SimpleScopeSet<ScopeType>(ScopeType.Self, ScopeType.Self, ScopeType.OrganizationConfig, ScopeType.OrganizationConfig);
			ExchangeRole.ScopeSets[RoleType.MyRetentionPolicies] = new SimpleScopeSet<ScopeType>(ScopeType.Self, ScopeType.Self, ScopeType.OrganizationConfig, ScopeType.OrganizationConfig);
			ExchangeRole.ScopeSets[RoleType.MyTextMessaging] = new SimpleScopeSet<ScopeType>(ScopeType.Self, ScopeType.Self, ScopeType.OrganizationConfig, ScopeType.OrganizationConfig);
			ExchangeRole.ScopeSets[RoleType.MyMarketplaceApps] = new SimpleScopeSet<ScopeType>(ScopeType.Self, ScopeType.Self, ScopeType.OrganizationConfig, ScopeType.OrganizationConfig);
			ExchangeRole.ScopeSets[RoleType.MyCustomApps] = new SimpleScopeSet<ScopeType>(ScopeType.Self, ScopeType.Self, ScopeType.OrganizationConfig, ScopeType.OrganizationConfig);
			ExchangeRole.ScopeSets[RoleType.MyReadWriteMailboxApps] = new SimpleScopeSet<ScopeType>(ScopeType.Self, ScopeType.Self, ScopeType.OrganizationConfig, ScopeType.OrganizationConfig);
			ExchangeRole.ScopeSets[RoleType.MyTeamMailboxes] = new SimpleScopeSet<ScopeType>(ScopeType.Organization, ScopeType.Organization, ScopeType.OrganizationConfig, ScopeType.OrganizationConfig);
			ExchangeRole.ScopeSets[RoleType.MyVoiceMail] = new SimpleScopeSet<ScopeType>(ScopeType.Self, ScopeType.Self, ScopeType.OrganizationConfig, ScopeType.OrganizationConfig);
			ExchangeRole.ScopeSets[RoleType.DistributionGroupManagement] = new SimpleScopeSet<ScopeType>(ScopeType.Organization, ScopeType.Organization, ScopeType.None, ScopeType.None);
			ExchangeRole.ScopeSets[RoleType.MyDistributionGroups] = new SimpleScopeSet<ScopeType>(ScopeType.MyGAL, ScopeType.MyDistributionGroups, ScopeType.OrganizationConfig, ScopeType.None);
			ExchangeRole.ScopeSets[RoleType.MyDistributionGroupMembership] = new SimpleScopeSet<ScopeType>(ScopeType.MyGAL, ScopeType.MyGAL, ScopeType.None, ScopeType.None);
			ExchangeRole.ScopeSets[RoleType.HelpdeskRecipientManagement] = new SimpleScopeSet<ScopeType>(ScopeType.Organization, ScopeType.Organization, ScopeType.OrganizationConfig, ScopeType.OrganizationConfig);
			ExchangeRole.ScopeSets[RoleType.GALSynchronizationManagement] = new SimpleScopeSet<ScopeType>(ScopeType.Organization, ScopeType.Organization, ScopeType.OrganizationConfig, ScopeType.OrganizationConfig);
			ExchangeRole.ScopeSets[RoleType.ApplicationImpersonation] = new SimpleScopeSet<ScopeType>(ScopeType.Organization, ScopeType.Organization, ScopeType.None, ScopeType.None);
			ExchangeRole.ScopeSets[RoleType.PartnerDelegatedTenantManagement] = new SimpleScopeSet<ScopeType>(ScopeType.Organization, ScopeType.Organization, ScopeType.OrganizationConfig, ScopeType.OrganizationConfig);
			ExchangeRole.ScopeSets[RoleType.CentralAdminManagement] = new SimpleScopeSet<ScopeType>(ScopeType.Organization, ScopeType.Organization, ScopeType.OrganizationConfig, ScopeType.OrganizationConfig);
			ExchangeRole.ScopeSets[RoleType.ViewOnlyCentralAdminManagement] = new SimpleScopeSet<ScopeType>(ScopeType.Organization, ScopeType.None, ScopeType.OrganizationConfig, ScopeType.None);
			ExchangeRole.ScopeSets[RoleType.ViewOnlyCentralAdminSupport] = new SimpleScopeSet<ScopeType>(ScopeType.Organization, ScopeType.None, ScopeType.OrganizationConfig, ScopeType.None);
			ExchangeRole.ScopeSets[RoleType.UnScopedRoleManagement] = new SimpleScopeSet<ScopeType>(ScopeType.Organization, ScopeType.Organization, ScopeType.OrganizationConfig, ScopeType.OrganizationConfig);
			ExchangeRole.ScopeSets[RoleType.LawEnforcementRequests] = new SimpleScopeSet<ScopeType>(ScopeType.Organization, ScopeType.Organization, ScopeType.OrganizationConfig, ScopeType.OrganizationConfig);
			ExchangeRole.ScopeSets[RoleType.ViewOnlyRoleManagement] = new SimpleScopeSet<ScopeType>(ScopeType.Organization, ScopeType.None, ScopeType.OrganizationConfig, ScopeType.None);
			ExchangeRole.ScopeSets[RoleType.Reporting] = new SimpleScopeSet<ScopeType>(ScopeType.Organization, ScopeType.None, ScopeType.OrganizationConfig, ScopeType.None);
			ExchangeRole.ScopeSets[RoleType.PersonallyIdentifiableInformation] = new SimpleScopeSet<ScopeType>(ScopeType.Organization, ScopeType.None, ScopeType.None, ScopeType.None);
			ExchangeRole.ScopeSets[RoleType.MailRecipients] = new SimpleScopeSet<ScopeType>(ScopeType.Organization, ScopeType.Organization, ScopeType.OrganizationConfig, ScopeType.OrganizationConfig);
			ExchangeRole.ScopeSets[RoleType.FederatedSharing] = new SimpleScopeSet<ScopeType>(ScopeType.Organization, ScopeType.Organization, ScopeType.OrganizationConfig, ScopeType.OrganizationConfig);
			ExchangeRole.ScopeSets[RoleType.DatabaseAvailabilityGroups] = new SimpleScopeSet<ScopeType>(ScopeType.Organization, ScopeType.Organization, ScopeType.OrganizationConfig, ScopeType.OrganizationConfig);
			ExchangeRole.ScopeSets[RoleType.Databases] = new SimpleScopeSet<ScopeType>(ScopeType.Organization, ScopeType.Organization, ScopeType.OrganizationConfig, ScopeType.OrganizationConfig);
			ExchangeRole.ScopeSets[RoleType.PublicFolders] = new SimpleScopeSet<ScopeType>(ScopeType.Organization, ScopeType.Organization, ScopeType.OrganizationConfig, ScopeType.OrganizationConfig);
			ExchangeRole.ScopeSets[RoleType.AddressLists] = new SimpleScopeSet<ScopeType>(ScopeType.Organization, ScopeType.Organization, ScopeType.OrganizationConfig, ScopeType.OrganizationConfig);
			ExchangeRole.ScopeSets[RoleType.RecipientPolicies] = new SimpleScopeSet<ScopeType>(ScopeType.Organization, ScopeType.Organization, ScopeType.OrganizationConfig, ScopeType.OrganizationConfig);
			ExchangeRole.ScopeSets[RoleType.DisasterRecovery] = new SimpleScopeSet<ScopeType>(ScopeType.Organization, ScopeType.Organization, ScopeType.OrganizationConfig, ScopeType.OrganizationConfig);
			ExchangeRole.ScopeSets[RoleType.Monitoring] = new SimpleScopeSet<ScopeType>(ScopeType.Organization, ScopeType.Organization, ScopeType.OrganizationConfig, ScopeType.OrganizationConfig);
			ExchangeRole.ScopeSets[RoleType.DatabaseCopies] = new SimpleScopeSet<ScopeType>(ScopeType.Organization, ScopeType.Organization, ScopeType.OrganizationConfig, ScopeType.OrganizationConfig);
			ExchangeRole.ScopeSets[RoleType.UnifiedMessaging] = new SimpleScopeSet<ScopeType>(ScopeType.Organization, ScopeType.Organization, ScopeType.OrganizationConfig, ScopeType.OrganizationConfig);
			ExchangeRole.ScopeSets[RoleType.Journaling] = new SimpleScopeSet<ScopeType>(ScopeType.Organization, ScopeType.Organization, ScopeType.OrganizationConfig, ScopeType.OrganizationConfig);
			ExchangeRole.ScopeSets[RoleType.RemoteAndAcceptedDomains] = new SimpleScopeSet<ScopeType>(ScopeType.Organization, ScopeType.Organization, ScopeType.OrganizationConfig, ScopeType.OrganizationConfig);
			ExchangeRole.ScopeSets[RoleType.EmailAddressPolicies] = new SimpleScopeSet<ScopeType>(ScopeType.Organization, ScopeType.Organization, ScopeType.OrganizationConfig, ScopeType.OrganizationConfig);
			ExchangeRole.ScopeSets[RoleType.TransportRules] = new SimpleScopeSet<ScopeType>(ScopeType.Organization, ScopeType.Organization, ScopeType.OrganizationConfig, ScopeType.OrganizationConfig);
			ExchangeRole.ScopeSets[RoleType.SendConnectors] = new SimpleScopeSet<ScopeType>(ScopeType.Organization, ScopeType.Organization, ScopeType.OrganizationConfig, ScopeType.OrganizationConfig);
			ExchangeRole.ScopeSets[RoleType.EdgeSubscriptions] = new SimpleScopeSet<ScopeType>(ScopeType.Organization, ScopeType.Organization, ScopeType.OrganizationConfig, ScopeType.OrganizationConfig);
			ExchangeRole.ScopeSets[RoleType.OrganizationTransportSettings] = new SimpleScopeSet<ScopeType>(ScopeType.Organization, ScopeType.Organization, ScopeType.OrganizationConfig, ScopeType.OrganizationConfig);
			ExchangeRole.ScopeSets[RoleType.ExchangeServers] = new SimpleScopeSet<ScopeType>(ScopeType.Organization, ScopeType.Organization, ScopeType.OrganizationConfig, ScopeType.OrganizationConfig);
			ExchangeRole.ScopeSets[RoleType.ExchangeVirtualDirectories] = new SimpleScopeSet<ScopeType>(ScopeType.Organization, ScopeType.Organization, ScopeType.OrganizationConfig, ScopeType.OrganizationConfig);
			ExchangeRole.ScopeSets[RoleType.ExchangeServerCertificates] = new SimpleScopeSet<ScopeType>(ScopeType.Organization, ScopeType.Organization, ScopeType.OrganizationConfig, ScopeType.OrganizationConfig);
			ExchangeRole.ScopeSets[RoleType.POP3AndIMAP4Protocols] = new SimpleScopeSet<ScopeType>(ScopeType.Organization, ScopeType.Organization, ScopeType.OrganizationConfig, ScopeType.OrganizationConfig);
			ExchangeRole.ScopeSets[RoleType.ReceiveConnectors] = new SimpleScopeSet<ScopeType>(ScopeType.Organization, ScopeType.Organization, ScopeType.OrganizationConfig, ScopeType.OrganizationConfig);
			ExchangeRole.ScopeSets[RoleType.UMMailboxes] = new SimpleScopeSet<ScopeType>(ScopeType.Organization, ScopeType.Organization, ScopeType.OrganizationConfig, ScopeType.OrganizationConfig);
			ExchangeRole.ScopeSets[RoleType.UserOptions] = new SimpleScopeSet<ScopeType>(ScopeType.Organization, ScopeType.Organization, ScopeType.OrganizationConfig, ScopeType.OrganizationConfig);
			ExchangeRole.ScopeSets[RoleType.SecurityGroupCreationAndMembership] = new SimpleScopeSet<ScopeType>(ScopeType.Organization, ScopeType.Organization, ScopeType.OrganizationConfig, ScopeType.OrganizationConfig);
			ExchangeRole.ScopeSets[RoleType.MailRecipientCreation] = new SimpleScopeSet<ScopeType>(ScopeType.Organization, ScopeType.Organization, ScopeType.OrganizationConfig, ScopeType.OrganizationConfig);
			ExchangeRole.ScopeSets[RoleType.MessageTracking] = new SimpleScopeSet<ScopeType>(ScopeType.Organization, ScopeType.Organization, ScopeType.OrganizationConfig, ScopeType.OrganizationConfig);
			ExchangeRole.ScopeSets[RoleType.RoleManagement] = new SimpleScopeSet<ScopeType>(ScopeType.Organization, ScopeType.Organization, ScopeType.OrganizationConfig, ScopeType.OrganizationConfig);
			ExchangeRole.ScopeSets[RoleType.ViewOnlyRecipients] = new SimpleScopeSet<ScopeType>(ScopeType.Organization, ScopeType.None, ScopeType.OrganizationConfig, ScopeType.None);
			ExchangeRole.ScopeSets[RoleType.ViewOnlyConfiguration] = new SimpleScopeSet<ScopeType>(ScopeType.Organization, ScopeType.None, ScopeType.OrganizationConfig, ScopeType.None);
			ExchangeRole.ScopeSets[RoleType.DistributionGroups] = new SimpleScopeSet<ScopeType>(ScopeType.Organization, ScopeType.Organization, ScopeType.OrganizationConfig, ScopeType.OrganizationConfig);
			ExchangeRole.ScopeSets[RoleType.MailEnabledPublicFolders] = new SimpleScopeSet<ScopeType>(ScopeType.Organization, ScopeType.Organization, ScopeType.OrganizationConfig, ScopeType.OrganizationConfig);
			ExchangeRole.ScopeSets[RoleType.MoveMailboxes] = new SimpleScopeSet<ScopeType>(ScopeType.Organization, ScopeType.Organization, ScopeType.OrganizationConfig, ScopeType.OrganizationConfig);
			ExchangeRole.ScopeSets[RoleType.ResetPassword] = new SimpleScopeSet<ScopeType>(ScopeType.Organization, ScopeType.Organization, ScopeType.OrganizationConfig, ScopeType.OrganizationConfig);
			ExchangeRole.ScopeSets[RoleType.AuditLogs] = new SimpleScopeSet<ScopeType>(ScopeType.Organization, ScopeType.Organization, ScopeType.OrganizationConfig, ScopeType.OrganizationConfig);
			ExchangeRole.ScopeSets[RoleType.RetentionManagement] = new SimpleScopeSet<ScopeType>(ScopeType.Organization, ScopeType.Organization, ScopeType.OrganizationConfig, ScopeType.OrganizationConfig);
			ExchangeRole.ScopeSets[RoleType.SupportDiagnostics] = new SimpleScopeSet<ScopeType>(ScopeType.Organization, ScopeType.Organization, ScopeType.OrganizationConfig, ScopeType.OrganizationConfig);
			ExchangeRole.ScopeSets[RoleType.MailboxSearch] = new SimpleScopeSet<ScopeType>(ScopeType.Organization, ScopeType.Organization, ScopeType.None, ScopeType.None);
			ExchangeRole.ScopeSets[RoleType.LegalHold] = new SimpleScopeSet<ScopeType>(ScopeType.Organization, ScopeType.Organization, ScopeType.OrganizationConfig, ScopeType.None);
			ExchangeRole.ScopeSets[RoleType.MailTips] = new SimpleScopeSet<ScopeType>(ScopeType.Organization, ScopeType.Organization, ScopeType.OrganizationConfig, ScopeType.OrganizationConfig);
			ExchangeRole.ScopeSets[RoleType.ActiveDirectoryPermissions] = new SimpleScopeSet<ScopeType>(ScopeType.Organization, ScopeType.Organization, ScopeType.OrganizationConfig, ScopeType.OrganizationConfig);
			ExchangeRole.ScopeSets[RoleType.UMPrompts] = new SimpleScopeSet<ScopeType>(ScopeType.Organization, ScopeType.Organization, ScopeType.OrganizationConfig, ScopeType.OrganizationConfig);
			ExchangeRole.ScopeSets[RoleType.Migration] = new SimpleScopeSet<ScopeType>(ScopeType.Organization, ScopeType.Organization, ScopeType.OrganizationConfig, ScopeType.OrganizationConfig);
			ExchangeRole.ScopeSets[RoleType.DataCenterOperations] = new SimpleScopeSet<ScopeType>(ScopeType.Organization, ScopeType.Organization, ScopeType.OrganizationConfig, ScopeType.OrganizationConfig);
			ExchangeRole.ScopeSets[RoleType.TransportHygiene] = new SimpleScopeSet<ScopeType>(ScopeType.Organization, ScopeType.Organization, ScopeType.OrganizationConfig, ScopeType.OrganizationConfig);
			ExchangeRole.ScopeSets[RoleType.TransportQueues] = new SimpleScopeSet<ScopeType>(ScopeType.Organization, ScopeType.Organization, ScopeType.OrganizationConfig, ScopeType.OrganizationConfig);
			ExchangeRole.ScopeSets[RoleType.Supervision] = new SimpleScopeSet<ScopeType>(ScopeType.Organization, ScopeType.Organization, ScopeType.OrganizationConfig, ScopeType.OrganizationConfig);
			ExchangeRole.ScopeSets[RoleType.CmdletExtensionAgents] = new SimpleScopeSet<ScopeType>(ScopeType.Organization, ScopeType.Organization, ScopeType.OrganizationConfig, ScopeType.OrganizationConfig);
			ExchangeRole.ScopeSets[RoleType.OrganizationConfiguration] = new SimpleScopeSet<ScopeType>(ScopeType.Organization, ScopeType.Organization, ScopeType.OrganizationConfig, ScopeType.OrganizationConfig);
			ExchangeRole.ScopeSets[RoleType.OrganizationClientAccess] = new SimpleScopeSet<ScopeType>(ScopeType.Organization, ScopeType.Organization, ScopeType.OrganizationConfig, ScopeType.OrganizationConfig);
			ExchangeRole.ScopeSets[RoleType.ExchangeConnectors] = new SimpleScopeSet<ScopeType>(ScopeType.Organization, ScopeType.Organization, ScopeType.OrganizationConfig, ScopeType.OrganizationConfig);
			ExchangeRole.ScopeSets[RoleType.TransportAgents] = new SimpleScopeSet<ScopeType>(ScopeType.Organization, ScopeType.Organization, ScopeType.OrganizationConfig, ScopeType.OrganizationConfig);
			ExchangeRole.ScopeSets[RoleType.InformationRightsManagement] = new SimpleScopeSet<ScopeType>(ScopeType.Organization, ScopeType.Organization, ScopeType.OrganizationConfig, ScopeType.OrganizationConfig);
			ExchangeRole.ScopeSets[RoleType.MailboxImportExport] = new SimpleScopeSet<ScopeType>(ScopeType.Organization, ScopeType.Organization, ScopeType.OrganizationConfig, ScopeType.OrganizationConfig);
			ExchangeRole.ScopeSets[RoleType.ViewOnlyAuditLogs] = new SimpleScopeSet<ScopeType>(ScopeType.Organization, ScopeType.None, ScopeType.OrganizationConfig, ScopeType.None);
			ExchangeRole.ScopeSets[RoleType.DataCenterDestructiveOperations] = new SimpleScopeSet<ScopeType>(ScopeType.Organization, ScopeType.Organization, ScopeType.OrganizationConfig, ScopeType.OrganizationConfig);
			ExchangeRole.ScopeSets[RoleType.NetworkingManagement] = new SimpleScopeSet<ScopeType>(ScopeType.Organization, ScopeType.Organization, ScopeType.OrganizationConfig, ScopeType.OrganizationConfig);
			ExchangeRole.ScopeSets[RoleType.WorkloadManagement] = new SimpleScopeSet<ScopeType>(ScopeType.Organization, ScopeType.Organization, ScopeType.OrganizationConfig, ScopeType.OrganizationConfig);
			ExchangeRole.ScopeSets[RoleType.CentralAdminCredentialManagement] = new SimpleScopeSet<ScopeType>(ScopeType.Organization, ScopeType.Organization, ScopeType.OrganizationConfig, ScopeType.OrganizationConfig);
			ExchangeRole.ScopeSets[RoleType.TeamMailboxes] = new SimpleScopeSet<ScopeType>(ScopeType.Organization, ScopeType.Organization, ScopeType.OrganizationConfig, ScopeType.OrganizationConfig);
			ExchangeRole.ScopeSets[RoleType.ActiveMonitoring] = new SimpleScopeSet<ScopeType>(ScopeType.Organization, ScopeType.Organization, ScopeType.OrganizationConfig, ScopeType.OrganizationConfig);
			ExchangeRole.ScopeSets[RoleType.DataLossPrevention] = new SimpleScopeSet<ScopeType>(ScopeType.Organization, ScopeType.Organization, ScopeType.OrganizationConfig, ScopeType.OrganizationConfig);
			ExchangeRole.ScopeSets[RoleType.OrgMarketplaceApps] = new SimpleScopeSet<ScopeType>(ScopeType.Organization, ScopeType.Organization, ScopeType.OrganizationConfig, ScopeType.OrganizationConfig);
			ExchangeRole.ScopeSets[RoleType.OrgCustomApps] = new SimpleScopeSet<ScopeType>(ScopeType.Organization, ScopeType.Organization, ScopeType.OrganizationConfig, ScopeType.OrganizationConfig);
			ExchangeRole.ScopeSets[RoleType.UserApplication] = new SimpleScopeSet<ScopeType>(ScopeType.Organization, ScopeType.Organization, ScopeType.OrganizationConfig, ScopeType.OrganizationConfig);
			ExchangeRole.ScopeSets[RoleType.ArchiveApplication] = new SimpleScopeSet<ScopeType>(ScopeType.Organization, ScopeType.Organization, ScopeType.OrganizationConfig, ScopeType.OrganizationConfig);
			ExchangeRole.ScopeSets[RoleType.LegalHoldApplication] = new SimpleScopeSet<ScopeType>(ScopeType.Organization, ScopeType.Organization, ScopeType.OrganizationConfig, ScopeType.OrganizationConfig);
			ExchangeRole.ScopeSets[RoleType.OfficeExtensionApplication] = new SimpleScopeSet<ScopeType>(ScopeType.Self, ScopeType.Self, ScopeType.OrganizationConfig, ScopeType.OrganizationConfig);
			ExchangeRole.ScopeSets[RoleType.TeamMailboxLifecycleApplication] = new SimpleScopeSet<ScopeType>(ScopeType.Self, ScopeType.Self, ScopeType.OrganizationConfig, ScopeType.OrganizationConfig);
			ExchangeRole.ScopeSets[RoleType.MailboxSearchApplication] = new SimpleScopeSet<ScopeType>(ScopeType.Organization, ScopeType.Organization, ScopeType.OrganizationConfig, ScopeType.OrganizationConfig);
			ExchangeRole.ScopeSets[RoleType.PublicFolderReplication] = new SimpleScopeSet<ScopeType>(ScopeType.Organization, ScopeType.Organization, ScopeType.OrganizationConfig, ScopeType.OrganizationConfig);
			ExchangeRole.ScopeSets[RoleType.OrganizationManagement] = new SimpleScopeSet<ScopeType>(ScopeType.Organization, ScopeType.Organization, ScopeType.OrganizationConfig, ScopeType.OrganizationConfig);
			ExchangeRole.ScopeSets[RoleType.ViewOnlyOrganizationManagement] = new SimpleScopeSet<ScopeType>(ScopeType.Organization, ScopeType.None, ScopeType.OrganizationConfig, ScopeType.None);
			ExchangeRole.ScopeSets[RoleType.RecipientManagement] = new SimpleScopeSet<ScopeType>(ScopeType.Organization, ScopeType.Organization, ScopeType.OrganizationConfig, ScopeType.OrganizationConfig);
			ExchangeRole.ScopeSets[RoleType.UmManagement] = new SimpleScopeSet<ScopeType>(ScopeType.Organization, ScopeType.Organization, ScopeType.OrganizationConfig, ScopeType.OrganizationConfig);
			ExchangeRole.ScopeSets[RoleType.RecordsManagement] = new SimpleScopeSet<ScopeType>(ScopeType.Organization, ScopeType.Organization, ScopeType.OrganizationConfig, ScopeType.OrganizationConfig);
			ExchangeRole.ScopeSets[RoleType.UmRecipientManagement] = new SimpleScopeSet<ScopeType>(ScopeType.Organization, ScopeType.Organization, ScopeType.OrganizationConfig, ScopeType.OrganizationConfig);
			ExchangeRole.ScopeSets[RoleType.UMPromptManagement] = new SimpleScopeSet<ScopeType>(ScopeType.Organization, ScopeType.Organization, ScopeType.OrganizationConfig, ScopeType.OrganizationConfig);
			ExchangeRole.ScopeSets[RoleType.DiscoveryManagement] = new SimpleScopeSet<ScopeType>(ScopeType.Organization, ScopeType.Organization, ScopeType.None, ScopeType.None);
			ExchangeRole.ScopeSets[RoleType.MyOptions] = new SimpleScopeSet<ScopeType>(ScopeType.Self, ScopeType.Self, ScopeType.OrganizationConfig, ScopeType.OrganizationConfig);
			ExchangeRole.ScopeSets[RoleType.MyFacebookEnabled] = new SimpleScopeSet<ScopeType>(ScopeType.Self, ScopeType.Self, ScopeType.OrganizationConfig, ScopeType.OrganizationConfig);
			ExchangeRole.ScopeSets[RoleType.MyLinkedInEnabled] = new SimpleScopeSet<ScopeType>(ScopeType.Self, ScopeType.Self, ScopeType.OrganizationConfig, ScopeType.OrganizationConfig);
			ExchangeRole.ScopeSets[RoleType.MyMailboxDelegation] = new SimpleScopeSet<ScopeType>(ScopeType.MyGAL, ScopeType.MailboxICanDelegate, ScopeType.OrganizationConfig, ScopeType.None);
			ExchangeRole.ScopeSets[RoleType.ExchangeCrossServiceIntegration] = new SimpleScopeSet<ScopeType>(ScopeType.Organization, ScopeType.Organization, ScopeType.OrganizationConfig, ScopeType.OrganizationConfig);
			ExchangeRole.ScopeSets[RoleType.AccessToCustomerDataDCOnly] = new SimpleScopeSet<ScopeType>(ScopeType.Organization, ScopeType.Organization, ScopeType.OrganizationConfig, ScopeType.OrganizationConfig);
			ExchangeRole.ScopeSets[RoleType.DatacenterOperationsDCOnly] = new SimpleScopeSet<ScopeType>(ScopeType.Organization, ScopeType.Organization, ScopeType.OrganizationConfig, ScopeType.OrganizationConfig);
		}

		// Token: 0x060031B1 RID: 12721 RVA: 0x000C8574 File Offset: 0x000C6774
		protected override void ValidateRead(List<ValidationError> errors)
		{
			base.ValidateRead(errors);
			if (!base.ExchangeVersion.IsOlderThan(ExchangeRoleSchema.RoleType.VersionAdded))
			{
				MultiValuedProperty<RoleEntry> roleEntries = this.RoleEntries;
				if (!base.ExchangeVersion.IsOlderThan(ExchangeRoleSchema.Exchange2009_R4DF5) && roleEntries.Count == 0 && !this.IsDeprecated && !this.AllowEmptyRole)
				{
					if (RoleType.UnScoped != this.RoleType)
					{
						errors.Add(new ObjectValidationError(DirectoryStrings.NoRoleEntriesFound, this.Identity, base.OriginatingServer));
					}
					else if (!this.IsRootRole)
					{
						errors.Add(new ObjectValidationError(DirectoryStrings.NoRoleEntriesCmdletOrScriptFound, this.Identity, base.OriginatingServer));
					}
				}
				object obj = null;
				if (!this.propertyBag.TryGetField(ExchangeRoleSchema.RoleType, ref obj) || obj == null)
				{
					errors.Add(new PropertyValidationError(DataStrings.PropertyIsMandatory, ExchangeRoleSchema.RoleType, null));
				}
				foreach (RoleEntry roleEntry in roleEntries)
				{
					ManagementRoleEntryType managementRoleEntryType;
					if (roleEntry is CmdletRoleEntry)
					{
						managementRoleEntryType = ManagementRoleEntryType.Cmdlet;
					}
					else if (roleEntry is ScriptRoleEntry)
					{
						managementRoleEntryType = ManagementRoleEntryType.Script;
					}
					else if (roleEntry is ApplicationPermissionRoleEntry)
					{
						managementRoleEntryType = ManagementRoleEntryType.ApplicationPermission;
					}
					else
					{
						if (!(roleEntry is WebServiceRoleEntry))
						{
							continue;
						}
						managementRoleEntryType = ManagementRoleEntryType.WebService;
					}
					if ((managementRoleEntryType == ManagementRoleEntryType.Script && this.RoleType != RoleType.Custom && this.RoleType != RoleType.UnScoped) || (managementRoleEntryType == ManagementRoleEntryType.ApplicationPermission && this.RoleType != RoleType.ApplicationImpersonation && this.RoleType != RoleType.PersonallyIdentifiableInformation))
					{
						errors.Add(new ObjectValidationError(DirectoryStrings.InvalidRoleEntryType(roleEntry.ToString(), this.RoleType, managementRoleEntryType), this.Identity, base.OriginatingServer));
					}
				}
			}
		}

		// Token: 0x060031B2 RID: 12722 RVA: 0x000C8728 File Offset: 0x000C6928
		protected override void ValidateWrite(List<ValidationError> errors)
		{
			base.ValidateWrite(errors);
			if ((int)this[ExchangeRoleSchema.RoleFlags] == 0)
			{
				errors.Add(new PropertyValidationError(DataStrings.PropertyIsMandatory, ExchangeRoleSchema.RoleFlags, 0));
			}
		}

		// Token: 0x060031B3 RID: 12723 RVA: 0x000C8760 File Offset: 0x000C6960
		internal static object HasDownlevelDataGetter(IPropertyBag propertyBag)
		{
			MultiValuedProperty<RoleEntry> multiValuedProperty = (MultiValuedProperty<RoleEntry>)propertyBag[ExchangeRoleSchema.InternalDownlevelRoleEntries];
			return ((ExchangeObjectVersion)propertyBag[ADObjectSchema.ExchangeVersion]).IsSameVersion(ExchangeRoleSchema.CurrentRoleVersion) && multiValuedProperty != null && multiValuedProperty.Count > 0;
		}

		// Token: 0x060031B4 RID: 12724 RVA: 0x000C87B4 File Offset: 0x000C69B4
		internal static object IsRootRoleGetter(IPropertyBag propertyBag)
		{
			ADObjectId roleId = (ADObjectId)propertyBag[ADObjectSchema.Id];
			return ExchangeRole.IsRootRoleInternal(roleId);
		}

		// Token: 0x060031B5 RID: 12725 RVA: 0x000C87E0 File Offset: 0x000C69E0
		internal static bool IsRootRoleInternal(ADObjectId roleId)
		{
			return roleId != null && (roleId.Parent.Name.Equals("Roles", StringComparison.OrdinalIgnoreCase) && roleId.Parent.Parent.Name.Equals("RBAC", StringComparison.OrdinalIgnoreCase)) && roleId.DistinguishedName.LastIndexOf("CN=Roles,CN=RBAC", StringComparison.OrdinalIgnoreCase) == roleId.DistinguishedName.IndexOf("CN=Roles,CN=RBAC", StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x060031B6 RID: 12726 RVA: 0x000C884D File Offset: 0x000C6A4D
		internal void StampIsEndUserRole()
		{
			this[ExchangeRoleSchema.IsEndUserRole] = (Array.BinarySearch<RoleType>(ExchangeRole.EndUserRoleTypes, this.RoleType) >= 0);
		}

		// Token: 0x060031B7 RID: 12727 RVA: 0x000C8878 File Offset: 0x000C6A78
		internal static object MailboxPlanIndexGetter(IPropertyBag propertyBag)
		{
			string text = (string)propertyBag[ExchangeRoleSchema.RawMailboxPlanIndex];
			RoleType roleType = (RoleType)propertyBag[ExchangeRoleSchema.RoleType];
			string name = ((ADObjectId)propertyBag[ADObjectSchema.Id]).Name;
			if (!ExchangeRoleSchema.RawMailboxPlanIndex.DefaultValue.Equals(text))
			{
				return text;
			}
			if (propertyBag[ADObjectSchema.ConfigurationUnit] != null && Array.BinarySearch<RoleType>(ExchangeRole.EndUserRoleTypes, roleType) >= 0 && (bool)ExchangeRole.IsRootRoleGetter(propertyBag) && !name.Equals(roleType.ToString(), StringComparison.OrdinalIgnoreCase) && !name.Replace(" ", string.Empty).Equals(roleType.ToString(), StringComparison.OrdinalIgnoreCase))
			{
				return "0";
			}
			return ExchangeRoleSchema.RawMailboxPlanIndex.DefaultValue;
		}

		// Token: 0x060031B8 RID: 12728 RVA: 0x000C8940 File Offset: 0x000C6B40
		internal static void MailboxPlanIndexSetter(object value, IPropertyBag propertyBag)
		{
			propertyBag[ExchangeRoleSchema.RawMailboxPlanIndex] = value;
		}

		// Token: 0x060031B9 RID: 12729 RVA: 0x000C8950 File Offset: 0x000C6B50
		internal static object DescriptionGetter(IPropertyBag propertyBag)
		{
			string text = (string)propertyBag[ExchangeRoleSchema.RawDescription];
			if (ExchangeRoleSchema.RawDescription.DefaultValue.Equals(text))
			{
				RoleType roleType = (RoleType)propertyBag[ExchangeRoleSchema.RoleType];
				if (Enum.IsDefined(typeof(RoleType), roleType))
				{
					try
					{
						if ((bool)ExchangeRole.IsRootRoleGetter(propertyBag))
						{
							text = ExchangeRole.resourceManager.GetString("RoleTypeDescription_" + roleType.ToString());
						}
						else
						{
							ADObjectId adobjectId = (ADObjectId)propertyBag[ADObjectSchema.Id];
							int num = adobjectId.Name.IndexOf("_");
							string str = (num == -1) ? adobjectId.Name : adobjectId.Name.Substring(0, num);
							string @string = ExchangeRole.resourceManager.GetString("CustomRoleDescription_" + str);
							if (!string.IsNullOrEmpty(@string))
							{
								text = @string;
							}
						}
					}
					catch (MissingManifestResourceException)
					{
					}
				}
			}
			return text;
		}

		// Token: 0x060031BA RID: 12730 RVA: 0x000C8A54 File Offset: 0x000C6C54
		internal static void DescriptionSetter(object value, IPropertyBag propertyBag)
		{
			propertyBag[ExchangeRoleSchema.RawDescription] = value;
		}

		// Token: 0x060031BB RID: 12731 RVA: 0x000C8A62 File Offset: 0x000C6C62
		internal static object ParentGetter(IPropertyBag propertyBag)
		{
			if ((bool)ExchangeRole.IsRootRoleGetter(propertyBag))
			{
				return null;
			}
			return ((ADObjectId)propertyBag[ADObjectSchema.Id]).Parent;
		}

		// Token: 0x060031BC RID: 12732 RVA: 0x000C8ABC File Offset: 0x000C6CBC
		internal void ApplyChangesToDownlevelData(ExchangeRole parentRole)
		{
			if (parentRole == null)
			{
				throw new ArgumentNullException("parentRole");
			}
			if (!this.HasDownlevelData)
			{
				return;
			}
			MultiValuedProperty<RoleEntry> multiValuedProperty = (MultiValuedProperty<RoleEntry>)this[ExchangeRoleSchema.InternalDownlevelRoleEntries];
			object[] removed = this.RoleEntries.Removed;
			for (int i = 0; i < removed.Length; i++)
			{
				RoleEntry roleEntry = (RoleEntry)removed[i];
				RoleEntry downlevelEntryToFind = roleEntry.MapToPreviousVersion();
				RoleEntry roleEntry2 = multiValuedProperty.Find((RoleEntry dre) => 0 == RoleEntry.CompareRoleEntriesByName(dre, downlevelEntryToFind));
				if (roleEntry2 != null)
				{
					multiValuedProperty.Remove(roleEntry2);
				}
			}
			object[] added = this.RoleEntries.Added;
			for (int j = 0; j < added.Length; j++)
			{
				RoleEntry roleEntry3 = (RoleEntry)added[j];
				RoleEntry downlevelEntryToFind = roleEntry3.MapToPreviousVersion();
				MultiValuedProperty<RoleEntry> multiValuedProperty2 = (MultiValuedProperty<RoleEntry>)parentRole[ExchangeRoleSchema.InternalDownlevelRoleEntries];
				RoleEntry roleEntry4 = multiValuedProperty2.Find((RoleEntry dre) => 0 == RoleEntry.CompareRoleEntriesByName(dre, downlevelEntryToFind));
				if (!(roleEntry4 == null))
				{
					List<string> list = new List<string>();
					foreach (string newParameter in roleEntry3.Parameters)
					{
						string text = roleEntry3.MapParameterToPreviousVersion(newParameter);
						if (roleEntry4.ContainsParameter(text))
						{
							list.Add(text);
						}
					}
					RoleEntry item = downlevelEntryToFind.Clone(list);
					multiValuedProperty.Add(item);
				}
			}
		}

		// Token: 0x060031BD RID: 12733 RVA: 0x000C8C4C File Offset: 0x000C6E4C
		internal static bool IsAdminRole(RoleType roleToCheck)
		{
			return Array.BinarySearch<RoleType>(ExchangeRole.EndUserRoleTypes, roleToCheck) < 0;
		}

		// Token: 0x060031BE RID: 12734 RVA: 0x000C8C5C File Offset: 0x000C6E5C
		internal static bool IsPartnerApplicationRoleType(RoleType roleToCheck)
		{
			return Array.BinarySearch<RoleType>(ExchangeRole.PartnerApplicationRoleTypes, roleToCheck) >= 0;
		}

		// Token: 0x060031BF RID: 12735 RVA: 0x000C8C70 File Offset: 0x000C6E70
		internal void StampImplicitScopes()
		{
			SimpleScopeSet<ScopeType> simpleScopeSet = ExchangeRole.ScopeSets[this.RoleType];
			this[ExchangeRoleSchema.ImplicitRecipientReadScope] = simpleScopeSet.RecipientReadScope;
			this[ExchangeRoleSchema.ImplicitRecipientWriteScope] = simpleScopeSet.RecipientWriteScope;
			this[ExchangeRoleSchema.ImplicitConfigReadScope] = simpleScopeSet.ConfigReadScope;
			this[ExchangeRoleSchema.ImplicitConfigWriteScope] = simpleScopeSet.ConfigWriteScope;
		}

		// Token: 0x060031C0 RID: 12736 RVA: 0x000C8CE6 File Offset: 0x000C6EE6
		internal SimpleScopeSet<ScopeType> GetImplicitScopeSet()
		{
			return ExchangeRole.ScopeSets[this.RoleType];
		}

		// Token: 0x040021E6 RID: 8678
		private const string RolesToken = "Roles";

		// Token: 0x040021E7 RID: 8679
		private const string RbacToken = "RBAC";

		// Token: 0x040021E8 RID: 8680
		private const string RdnRolesContainer = "CN=Roles,CN=RBAC";

		// Token: 0x040021E9 RID: 8681
		internal static readonly ADObjectId RdnContainer = new ADObjectId("CN=Roles,CN=RBAC");

		// Token: 0x040021EA RID: 8682
		private static ExchangeRoleSchema schema = ObjectSchema.GetInstance<ExchangeRoleSchema>();

		// Token: 0x040021EB RID: 8683
		private static string mostDerivedClass = "msExchRole";

		// Token: 0x040021EC RID: 8684
		internal static readonly RoleType[] EndUserRoleTypes;

		// Token: 0x040021ED RID: 8685
		internal static readonly RoleType[] PartnerApplicationRoleTypes;

		// Token: 0x040021EE RID: 8686
		private static ExchangeResourceManager resourceManager = ExchangeResourceManager.GetResourceManager(typeof(CoreStrings).FullName, typeof(CoreStrings).Assembly);

		// Token: 0x040021EF RID: 8687
		internal static Dictionary<RoleType, SimpleScopeSet<ScopeType>> ScopeSets;

		// Token: 0x040021F0 RID: 8688
		private bool allowEmptyRole;
	}
}
