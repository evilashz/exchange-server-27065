using System;
using System.Collections.Generic;
using System.Management.Automation;
using System.Net;
using System.Security.AccessControl;
using System.Security.Principal;
using Microsoft.Exchange.Configuration.Common.LocStrings;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Provisioning;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Mapi;
using Microsoft.Exchange.Data.Mapi.Common;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.ActiveManager;
using Microsoft.Exchange.Management.Common;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.Provisioning;

namespace Microsoft.Exchange.Management.MapiTasks
{
	// Token: 0x02000482 RID: 1154
	[Cmdlet("connect", "Mailbox", DefaultParameterSetName = "User", SupportsShouldProcess = true)]
	public sealed class ConnectMailbox : MapiObjectActionTask<StoreMailboxIdParameter, MailboxStatistics>
	{
		// Token: 0x17000C21 RID: 3105
		// (get) Token: 0x0600288C RID: 10380 RVA: 0x0009F1D0 File Offset: 0x0009D3D0
		private ITopologyConfigurationSession ResourceForestSession
		{
			get
			{
				if (this.resourceForestSession == null)
				{
					this.resourceForestSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(base.DomainController, true, ConsistencyMode.PartiallyConsistent, ADSessionSettings.FromRootOrgScopeSet(), 90, "ResourceForestSession", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\MapiTasks\\Mailbox\\ConnectMailbox.cs");
				}
				return this.resourceForestSession;
			}
		}

		// Token: 0x17000C22 RID: 3106
		// (get) Token: 0x0600288D RID: 10381 RVA: 0x0009F219 File Offset: 0x0009D419
		private ActiveManager ActiveManager
		{
			get
			{
				return RecipientTaskHelper.GetActiveManagerInstance();
			}
		}

		// Token: 0x17000C23 RID: 3107
		// (get) Token: 0x0600288E RID: 10382 RVA: 0x0009F220 File Offset: 0x0009D420
		// (set) Token: 0x0600288F RID: 10383 RVA: 0x0009F228 File Offset: 0x0009D428
		private new ServerIdParameter Server
		{
			get
			{
				return base.Server;
			}
			set
			{
				base.Server = value;
			}
		}

		// Token: 0x17000C24 RID: 3108
		// (get) Token: 0x06002890 RID: 10384 RVA: 0x0009F231 File Offset: 0x0009D431
		// (set) Token: 0x06002891 RID: 10385 RVA: 0x0009F239 File Offset: 0x0009D439
		[Parameter(Mandatory = true, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true, Position = 0)]
		public new StoreMailboxIdParameter Identity
		{
			get
			{
				return base.Identity;
			}
			set
			{
				base.Identity = value;
			}
		}

		// Token: 0x17000C25 RID: 3109
		// (get) Token: 0x06002892 RID: 10386 RVA: 0x0009F242 File Offset: 0x0009D442
		// (set) Token: 0x06002893 RID: 10387 RVA: 0x0009F259 File Offset: 0x0009D459
		[Parameter(Mandatory = true, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true, Position = 1)]
		public DatabaseIdParameter Database
		{
			get
			{
				return (DatabaseIdParameter)base.Fields["MailboxDatabase"];
			}
			set
			{
				base.Fields["MailboxDatabase"] = value;
			}
		}

		// Token: 0x17000C26 RID: 3110
		// (get) Token: 0x06002894 RID: 10388 RVA: 0x0009F26C File Offset: 0x0009D46C
		// (set) Token: 0x06002895 RID: 10389 RVA: 0x0009F283 File Offset: 0x0009D483
		[Parameter(Mandatory = true, ParameterSetName = "ValidateOnly")]
		public SwitchParameter ValidateOnly
		{
			get
			{
				return (SwitchParameter)base.Fields["ValidateOnly"];
			}
			set
			{
				base.Fields["ValidateOnly"] = value;
			}
		}

		// Token: 0x17000C27 RID: 3111
		// (get) Token: 0x06002896 RID: 10390 RVA: 0x0009F29B File Offset: 0x0009D49B
		// (set) Token: 0x06002897 RID: 10391 RVA: 0x0009F2B2 File Offset: 0x0009D4B2
		[Parameter(Mandatory = false, ParameterSetName = "Linked")]
		[Parameter(Mandatory = false, ParameterSetName = "Equipment")]
		[Parameter(Mandatory = false, ParameterSetName = "Shared")]
		[Parameter(Mandatory = false, ParameterSetName = "Room")]
		[Parameter(Mandatory = false, ParameterSetName = "User")]
		public UserIdParameter User
		{
			get
			{
				return (UserIdParameter)base.Fields["User"];
			}
			set
			{
				base.Fields["User"] = value;
			}
		}

		// Token: 0x17000C28 RID: 3112
		// (get) Token: 0x06002898 RID: 10392 RVA: 0x0009F2C5 File Offset: 0x0009D4C5
		// (set) Token: 0x06002899 RID: 10393 RVA: 0x0009F2DC File Offset: 0x0009D4DC
		[Parameter(Mandatory = false, ParameterSetName = "User")]
		public SwitchParameter AllowLegacyDNMismatch
		{
			get
			{
				return (SwitchParameter)base.Fields["AllowLegacyDNMismatch"];
			}
			set
			{
				base.Fields["AllowLegacyDNMismatch"] = value;
			}
		}

		// Token: 0x17000C29 RID: 3113
		// (get) Token: 0x0600289A RID: 10394 RVA: 0x0009F2F4 File Offset: 0x0009D4F4
		// (set) Token: 0x0600289B RID: 10395 RVA: 0x0009F30B File Offset: 0x0009D50B
		[Parameter(Mandatory = false, ParameterSetName = "User")]
		[Parameter(Mandatory = false, ParameterSetName = "Shared")]
		[Parameter(Mandatory = false, ParameterSetName = "Room")]
		[Parameter(Mandatory = false, ParameterSetName = "Linked")]
		[Parameter(Mandatory = false, ParameterSetName = "Equipment")]
		public string Alias
		{
			get
			{
				return (string)base.Fields["Alias"];
			}
			set
			{
				base.Fields["Alias"] = value;
			}
		}

		// Token: 0x17000C2A RID: 3114
		// (get) Token: 0x0600289C RID: 10396 RVA: 0x0009F31E File Offset: 0x0009D51E
		// (set) Token: 0x0600289D RID: 10397 RVA: 0x0009F335 File Offset: 0x0009D535
		[Parameter(Mandatory = true, ParameterSetName = "Room")]
		public SwitchParameter Room
		{
			get
			{
				return (SwitchParameter)base.Fields["Room"];
			}
			set
			{
				base.Fields["Room"] = value;
			}
		}

		// Token: 0x17000C2B RID: 3115
		// (get) Token: 0x0600289E RID: 10398 RVA: 0x0009F34D File Offset: 0x0009D54D
		// (set) Token: 0x0600289F RID: 10399 RVA: 0x0009F364 File Offset: 0x0009D564
		[Parameter(Mandatory = true, ParameterSetName = "Equipment")]
		public SwitchParameter Equipment
		{
			get
			{
				return (SwitchParameter)base.Fields["Equipment"];
			}
			set
			{
				base.Fields["Equipment"] = value;
			}
		}

		// Token: 0x17000C2C RID: 3116
		// (get) Token: 0x060028A0 RID: 10400 RVA: 0x0009F37C File Offset: 0x0009D57C
		// (set) Token: 0x060028A1 RID: 10401 RVA: 0x0009F393 File Offset: 0x0009D593
		[Parameter(Mandatory = true, ParameterSetName = "Shared")]
		public SwitchParameter Shared
		{
			get
			{
				return (SwitchParameter)base.Fields["Shared"];
			}
			set
			{
				base.Fields["Shared"] = value;
			}
		}

		// Token: 0x17000C2D RID: 3117
		// (get) Token: 0x060028A2 RID: 10402 RVA: 0x0009F3AB File Offset: 0x0009D5AB
		// (set) Token: 0x060028A3 RID: 10403 RVA: 0x0009F3C2 File Offset: 0x0009D5C2
		[Parameter(Mandatory = true, ParameterSetName = "Linked")]
		public UserIdParameter LinkedMasterAccount
		{
			get
			{
				return (UserIdParameter)base.Fields["LinkedMasterAccount"];
			}
			set
			{
				base.Fields["LinkedMasterAccount"] = value;
			}
		}

		// Token: 0x17000C2E RID: 3118
		// (get) Token: 0x060028A4 RID: 10404 RVA: 0x0009F3D5 File Offset: 0x0009D5D5
		// (set) Token: 0x060028A5 RID: 10405 RVA: 0x0009F3FB File Offset: 0x0009D5FB
		[Parameter(Mandatory = false)]
		public SwitchParameter Force
		{
			get
			{
				return (SwitchParameter)(base.Fields["Force"] ?? false);
			}
			set
			{
				base.Fields["Force"] = value;
			}
		}

		// Token: 0x17000C2F RID: 3119
		// (get) Token: 0x060028A6 RID: 10406 RVA: 0x0009F413 File Offset: 0x0009D613
		// (set) Token: 0x060028A7 RID: 10407 RVA: 0x0009F42A File Offset: 0x0009D62A
		[Parameter(Mandatory = true, ParameterSetName = "Linked")]
		public Fqdn LinkedDomainController
		{
			get
			{
				return (Fqdn)base.Fields["LinkedDomainController"];
			}
			set
			{
				base.Fields["LinkedDomainController"] = value;
			}
		}

		// Token: 0x17000C30 RID: 3120
		// (get) Token: 0x060028A8 RID: 10408 RVA: 0x0009F43D File Offset: 0x0009D63D
		// (set) Token: 0x060028A9 RID: 10409 RVA: 0x0009F454 File Offset: 0x0009D654
		[Parameter(Mandatory = false, ParameterSetName = "Linked")]
		public PSCredential LinkedCredential
		{
			get
			{
				return (PSCredential)base.Fields["LinkedCredential"];
			}
			set
			{
				base.Fields["LinkedCredential"] = value;
			}
		}

		// Token: 0x17000C31 RID: 3121
		// (get) Token: 0x060028AA RID: 10410 RVA: 0x0009F467 File Offset: 0x0009D667
		// (set) Token: 0x060028AB RID: 10411 RVA: 0x0009F47E File Offset: 0x0009D67E
		[Parameter(Mandatory = false, ParameterSetName = "Equipment")]
		[Parameter(Mandatory = false, ParameterSetName = "Linked")]
		[Parameter(Mandatory = false, ParameterSetName = "Shared")]
		[Parameter(Mandatory = false, ParameterSetName = "Room")]
		[Parameter(Mandatory = false, ParameterSetName = "User")]
		public MailboxPolicyIdParameter ManagedFolderMailboxPolicy
		{
			get
			{
				return (MailboxPolicyIdParameter)base.Fields["ManagedFolderMailboxPolicy"];
			}
			set
			{
				base.Fields["ManagedFolderMailboxPolicy"] = value;
			}
		}

		// Token: 0x17000C32 RID: 3122
		// (get) Token: 0x060028AC RID: 10412 RVA: 0x0009F491 File Offset: 0x0009D691
		// (set) Token: 0x060028AD RID: 10413 RVA: 0x0009F4A8 File Offset: 0x0009D6A8
		[Parameter(Mandatory = false, ParameterSetName = "Equipment")]
		[Parameter(Mandatory = false, ParameterSetName = "Linked")]
		[Parameter(Mandatory = false, ParameterSetName = "Room")]
		[Parameter(Mandatory = false, ParameterSetName = "User")]
		[Parameter(Mandatory = false, ParameterSetName = "Shared")]
		public MailboxPolicyIdParameter RetentionPolicy
		{
			get
			{
				return (MailboxPolicyIdParameter)base.Fields["RetentionPolicy"];
			}
			set
			{
				base.Fields["RetentionPolicy"] = value;
			}
		}

		// Token: 0x17000C33 RID: 3123
		// (get) Token: 0x060028AE RID: 10414 RVA: 0x0009F4BB File Offset: 0x0009D6BB
		// (set) Token: 0x060028AF RID: 10415 RVA: 0x0009F4E1 File Offset: 0x0009D6E1
		[Parameter(Mandatory = false, ParameterSetName = "Room")]
		[Parameter(Mandatory = false, ParameterSetName = "Equipment")]
		[Parameter(Mandatory = false, ParameterSetName = "Linked")]
		[Parameter(Mandatory = false, ParameterSetName = "Shared")]
		[Parameter(Mandatory = false, ParameterSetName = "User")]
		public SwitchParameter ManagedFolderMailboxPolicyAllowed
		{
			get
			{
				return (SwitchParameter)(base.Fields["ManagedFolderMailboxPolicyAllowed"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["ManagedFolderMailboxPolicyAllowed"] = value;
			}
		}

		// Token: 0x17000C34 RID: 3124
		// (get) Token: 0x060028B0 RID: 10416 RVA: 0x0009F4F9 File Offset: 0x0009D6F9
		// (set) Token: 0x060028B1 RID: 10417 RVA: 0x0009F510 File Offset: 0x0009D710
		[Parameter(Mandatory = false, ParameterSetName = "Linked")]
		[Parameter(Mandatory = false, ParameterSetName = "Room")]
		[Parameter(Mandatory = false, ParameterSetName = "Equipment")]
		[Parameter(Mandatory = false, ParameterSetName = "User")]
		[Parameter(Mandatory = false, ParameterSetName = "Shared")]
		public MailboxPolicyIdParameter ActiveSyncMailboxPolicy
		{
			get
			{
				return (MailboxPolicyIdParameter)base.Fields["ActiveSyncMailboxPolicy"];
			}
			set
			{
				base.Fields["ActiveSyncMailboxPolicy"] = value;
			}
		}

		// Token: 0x17000C35 RID: 3125
		// (get) Token: 0x060028B2 RID: 10418 RVA: 0x0009F523 File Offset: 0x0009D723
		// (set) Token: 0x060028B3 RID: 10419 RVA: 0x0009F53A File Offset: 0x0009D73A
		[Parameter(Mandatory = false, ParameterSetName = "User")]
		public AddressBookMailboxPolicyIdParameter AddressBookPolicy
		{
			get
			{
				return (AddressBookMailboxPolicyIdParameter)base.Fields[ADRecipientSchema.AddressBookPolicy];
			}
			set
			{
				base.Fields[ADRecipientSchema.AddressBookPolicy] = value;
			}
		}

		// Token: 0x17000C36 RID: 3126
		// (get) Token: 0x060028B4 RID: 10420 RVA: 0x0009F54D File Offset: 0x0009D74D
		// (set) Token: 0x060028B5 RID: 10421 RVA: 0x0009F573 File Offset: 0x0009D773
		[Parameter(Mandatory = false, ParameterSetName = "User")]
		public SwitchParameter Archive
		{
			get
			{
				return (SwitchParameter)(base.Fields["Archive"] ?? false);
			}
			set
			{
				base.Fields["Archive"] = value;
			}
		}

		// Token: 0x17000C37 RID: 3127
		// (get) Token: 0x060028B6 RID: 10422 RVA: 0x0009F58C File Offset: 0x0009D78C
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				if ("Room" == base.ParameterSetName)
				{
					return Strings.ConfirmationMessageConnectMailboxResource(this.Identity.ToString(), ExchangeResourceType.Room.ToString(), this.Database.ToString());
				}
				if ("Equipment" == base.ParameterSetName)
				{
					return Strings.ConfirmationMessageConnectMailboxResource(this.Identity.ToString(), ExchangeResourceType.Equipment.ToString(), this.Database.ToString());
				}
				if ("Linked" == base.ParameterSetName)
				{
					return Strings.ConfirmationMessageConnectMailboxLinked(this.Identity.ToString(), this.LinkedMasterAccount.ToString(), this.LinkedDomainController, this.Database.ToString());
				}
				if ("Shared" == base.ParameterSetName)
				{
					return Strings.ConfirmationMessageConnectMailboxShared(this.Identity.ToString(), this.Shared.ToString(), this.Database.ToString());
				}
				if ("ValidateOnly" == base.ParameterSetName)
				{
					return Strings.ConfirmationMessageConnectMailboxValidateOnly(this.Identity.ToString(), this.ValidateOnly.ToString(), this.Database.ToString());
				}
				return Strings.ConfirmationMessageConnectMailboxUser(this.Identity.ToString(), this.Database.ToString());
			}
		}

		// Token: 0x17000C38 RID: 3128
		// (get) Token: 0x060028B7 RID: 10423 RVA: 0x0009F6EE File Offset: 0x0009D8EE
		private IConfigurationSession TenantConfigurationSession
		{
			get
			{
				return this.tenantConfigurationSession;
			}
		}

		// Token: 0x17000C39 RID: 3129
		// (get) Token: 0x060028B8 RID: 10424 RVA: 0x0009F6F6 File Offset: 0x0009D8F6
		private IRecipientSession RecipientSession
		{
			get
			{
				return this.recipientSession;
			}
		}

		// Token: 0x17000C3A RID: 3130
		// (get) Token: 0x060028B9 RID: 10425 RVA: 0x0009F6FE File Offset: 0x0009D8FE
		private IRecipientSession GlobalCatalogSession
		{
			get
			{
				return this.globalCatalogSession;
			}
		}

		// Token: 0x17000C3B RID: 3131
		// (get) Token: 0x060028BA RID: 10426 RVA: 0x0009F706 File Offset: 0x0009D906
		private MailboxDatabase OwnerMailboxDatabase
		{
			get
			{
				return this.ownerMailboxDatabase;
			}
		}

		// Token: 0x17000C3C RID: 3132
		// (get) Token: 0x060028BB RID: 10427 RVA: 0x0009F70E File Offset: 0x0009D90E
		protected override ObjectId RootId
		{
			get
			{
				if (this.OwnerMailboxDatabase == null)
				{
					return null;
				}
				return MapiTaskHelper.ConvertDatabaseADObjectToDatabaseId(this.OwnerMailboxDatabase);
			}
		}

		// Token: 0x060028BC RID: 10428 RVA: 0x0009F728 File Offset: 0x0009D928
		protected override IConfigDataProvider CreateSession()
		{
			this.ownerMailboxDatabase = (MailboxDatabase)base.GetDataObject<MailboxDatabase>(this.Database, this.ResourceForestSession, null, new LocalizedString?(Strings.ErrorDatabaseNotFound(this.Database.ToString())), new LocalizedString?(Strings.ErrorDatabaseNotUnique(this.Database.ToString())));
			if (this.OwnerMailboxDatabase.Recovery)
			{
				base.WriteError(new MdbAdminTaskException(Strings.ErrorMailboxResidesInRDB(this.Identity.ToString())), ErrorCategory.InvalidArgument, this.Identity);
			}
			this.databaseLocationInfo = this.ActiveManager.GetServerForDatabase(this.OwnerMailboxDatabase.Guid);
			Server server = this.ownerMailboxDatabase.GetServer();
			if (!server.IsE15OrLater)
			{
				base.WriteError(new MdbAdminTaskException(Strings.ErrorMailboxDatabaseNotOnE15Server(this.Database.ToString())), ErrorCategory.InvalidArgument, this.Identity);
			}
			if (this.mapiAdministrationSession == null)
			{
				this.mapiAdministrationSession = new MapiAdministrationSession(server.ExchangeLegacyDN, Fqdn.Parse(server.Fqdn));
			}
			return this.mapiAdministrationSession;
		}

		// Token: 0x060028BD RID: 10429 RVA: 0x0009F828 File Offset: 0x0009DA28
		protected override IConfigurable PrepareDataObject()
		{
			TaskLogger.LogEnter();
			MailboxStatistics deletedStoreMailbox = MailboxTaskHelper.GetDeletedStoreMailbox(base.DataSession, this.Identity, this.RootId, this.Database, new Task.ErrorLoggerDelegate(base.WriteError));
			ADSessionSettings sessionSettings;
			if (deletedStoreMailbox.ExternalDirectoryOrganizationId == Guid.Empty)
			{
				sessionSettings = ADSessionSettings.FromRootOrgScopeSet();
			}
			else
			{
				sessionSettings = ADSessionSettings.FromExternalDirectoryOrganizationId(deletedStoreMailbox.ExternalDirectoryOrganizationId);
			}
			this.tenantConfigurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(base.DomainController, false, ConsistencyMode.PartiallyConsistent, sessionSettings, 470, "PrepareDataObject", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\MapiTasks\\Mailbox\\ConnectMailbox.cs");
			this.recipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(base.DomainController, false, ConsistencyMode.PartiallyConsistent, sessionSettings, 476, "PrepareDataObject", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\MapiTasks\\Mailbox\\ConnectMailbox.cs");
			this.recipientSession.UseGlobalCatalog = (base.ServerSettings.ViewEntireForest && null == base.DomainController);
			this.globalCatalogSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(base.DomainController, true, ConsistencyMode.PartiallyConsistent, sessionSettings, 486, "PrepareDataObject", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\MapiTasks\\Mailbox\\ConnectMailbox.cs");
			if (!this.globalCatalogSession.IsReadConnectionAvailable())
			{
				this.globalCatalogSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(ConsistencyMode.PartiallyConsistent, sessionSettings, 494, "PrepareDataObject", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\MapiTasks\\Mailbox\\ConnectMailbox.cs");
			}
			TaskLogger.LogExit();
			return deletedStoreMailbox;
		}

		// Token: 0x060028BE RID: 10430 RVA: 0x0009F970 File Offset: 0x0009DB70
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			base.InternalValidate();
			if ("Linked" == base.ParameterSetName)
			{
				try
				{
					NetworkCredential userForestCredential = (this.LinkedCredential == null) ? null : this.LinkedCredential.GetNetworkCredential();
					this.linkedUserSid = MailboxTaskHelper.GetAccountSidFromAnotherForest(this.LinkedMasterAccount, this.LinkedDomainController, userForestCredential, this.ResourceForestSession, new MailboxTaskHelper.GetUniqueObject(base.GetDataObject<ADUser>), new Task.ErrorLoggerDelegate(base.ThrowTerminatingError));
				}
				catch (PSArgumentException exception)
				{
					base.ThrowTerminatingError(exception, ErrorCategory.InvalidArgument, this.LinkedCredential);
				}
			}
			if (this.ManagedFolderMailboxPolicy != null)
			{
				ManagedFolderMailboxPolicy managedFolderMailboxPolicy = (ManagedFolderMailboxPolicy)base.GetDataObject<ManagedFolderMailboxPolicy>(this.ManagedFolderMailboxPolicy, this.TenantConfigurationSession, null, new LocalizedString?(Strings.ErrorManagedFolderMailboxPolicyNotFound(this.ManagedFolderMailboxPolicy.ToString())), new LocalizedString?(Strings.ErrorManagedFolderMailboxPolicyNotUnique(this.ManagedFolderMailboxPolicy.ToString())));
				this.elcPolicyId = (ADObjectId)managedFolderMailboxPolicy.Identity;
			}
			if (this.RetentionPolicy != null)
			{
				if (SharedConfiguration.IsDehydratedConfiguration(base.CurrentOrganizationId))
				{
					base.WriteError(new LocalizedException(Strings.ErrorLinkOpOnDehydratedTenant("RetentionPolicy")), ExchangeErrorCategory.Client, null);
				}
				RetentionPolicy retentionPolicy = (RetentionPolicy)base.GetDataObject<RetentionPolicy>(this.RetentionPolicy, this.TenantConfigurationSession, null, new LocalizedString?(Strings.ErrorRetentionPolicyNotFound(this.RetentionPolicy.ToString())), new LocalizedString?(Strings.ErrorRetentionPolicyNotUnique(this.RetentionPolicy.ToString())));
				this.retentionPolicyId = retentionPolicy.Id;
			}
			if (this.ActiveSyncMailboxPolicy != null)
			{
				MobileMailboxPolicy mobileMailboxPolicy = (MobileMailboxPolicy)base.GetDataObject<MobileMailboxPolicy>(this.ActiveSyncMailboxPolicy, this.TenantConfigurationSession, null, new LocalizedString?(Strings.ErrorMobileMailboxPolicyNotFound(this.ActiveSyncMailboxPolicy.ToString())), new LocalizedString?(Strings.ErrorMobileMailboxPolicyNotUnique(this.ActiveSyncMailboxPolicy.ToString())));
				this.mobilePolicyId = (ADObjectId)mobileMailboxPolicy.Identity;
			}
			if (this.AddressBookPolicy != null)
			{
				AddressBookMailboxPolicy addressBookMailboxPolicy = (AddressBookMailboxPolicy)base.GetDataObject<AddressBookMailboxPolicy>(this.AddressBookPolicy, this.TenantConfigurationSession, null, new LocalizedString?(Strings.ErrorAddressBookMailboxPolicyNotFound(this.AddressBookPolicy.ToString())), new LocalizedString?(Strings.ErrorAddressBookMailboxPolicyNotUnique(this.AddressBookPolicy.ToString())), ExchangeErrorCategory.Client);
				this.addressBookPolicyId = (ADObjectId)addressBookMailboxPolicy.Identity;
			}
			MailboxTaskHelper.ValidateMailboxIsDisconnected(this.GlobalCatalogSession, this.DataObject.MailboxGuid, new Task.TaskVerboseLoggingDelegate(base.WriteVerbose), new Task.ErrorLoggerDelegate(base.WriteError));
			if (!this.Archive)
			{
				ConnectMailbox.CheckLegacyDNNotInUse(this.DataObject.Identity, this.DataObject.LegacyDN, this.GlobalCatalogSession, new Task.ErrorLoggerDelegate(base.WriteError));
			}
			if (this.User != null)
			{
				this.userToConnect = (ADUser)base.GetDataObject<ADUser>(this.User, this.RecipientSession, null, new LocalizedString?(Strings.ErrorRecipientNotFound(this.User.ToString())), new LocalizedString?(Strings.ErrorRecipientNotUnique(this.User.ToString())));
				if (this.Archive)
				{
					ConnectMailbox.CheckUserForArchive(this.DataObject, this.GlobalCatalogSession, new Task.ErrorLoggerDelegate(base.WriteError), this.userToConnect, this.OwnerMailboxDatabase, this.AllowLegacyDNMismatch);
				}
				else if (RecipientType.User != this.userToConnect.RecipientType)
				{
					base.WriteError(new RecipientTaskException(Strings.ErrorNoMatchedUserTypeFound(RecipientType.User.ToString(), this.User.ToString(), this.userToConnect.RecipientType.ToString())), ErrorCategory.InvalidArgument, this.User);
				}
			}
			else if (!this.Archive)
			{
				if ("ValidateOnly" == base.ParameterSetName)
				{
					this.matchedUsers = this.FindMatchedUser(this.DataObject, null);
				}
				else
				{
					this.matchedUsers = this.FindMatchedUser(this.DataObject, new bool?("User" == base.ParameterSetName));
				}
				if ("ValidateOnly" != base.ParameterSetName)
				{
					if (this.matchedUsers.Length == 0)
					{
						base.WriteError(new MdbAdminTaskException(Strings.ErrorNoMatchedUserFound), ErrorCategory.InvalidArgument, this.Identity);
					}
					else if (this.matchedUsers.Length > 1)
					{
						this.WriteWarning(Strings.ErrorMultipleMatchedUser(this.Identity.ToString()));
						this.needListMatchingUser = true;
					}
					else
					{
						this.userToConnect = (ADUser)this.matchedUsers[0];
						this.userToConnect = (ADUser)this.RecipientSession.Read(this.userToConnect.Id);
						if (this.userToConnect == null)
						{
							base.WriteError(new MdbAdminTaskException(Strings.ErrorNoMatchedUserFound), ErrorCategory.InvalidArgument, this.Identity);
						}
						if (this.Archive)
						{
							ConnectMailbox.CheckUserForArchive(this.DataObject, this.GlobalCatalogSession, new Task.ErrorLoggerDelegate(base.WriteError), this.userToConnect, this.OwnerMailboxDatabase, this.AllowLegacyDNMismatch);
						}
					}
				}
			}
			else
			{
				this.userToConnect = this.FindArchiveUser(this.DataObject, this.RecipientSession, new Task.TaskErrorLoggingDelegate(base.WriteError));
				ConnectMailbox.CheckUserForArchive(this.DataObject, this.GlobalCatalogSession, new Task.ErrorLoggerDelegate(base.WriteError), this.userToConnect, this.OwnerMailboxDatabase, this.AllowLegacyDNMismatch);
			}
			if (this.userToConnect != null && !this.Archive)
			{
				if ("User" == base.ParameterSetName)
				{
					if ((this.userToConnect.UserAccountControl & UserAccountControlFlags.AccountDisabled) != UserAccountControlFlags.None && this.DataObject.MailboxType == StoreMailboxType.Private)
					{
						base.WriteError(new RecipientTaskException(Strings.ErrorAccountDisabledForUserMailbox), ErrorCategory.InvalidArgument, this.userToConnect);
					}
				}
				else if ((this.userToConnect.UserAccountControl & UserAccountControlFlags.AccountDisabled) == UserAccountControlFlags.None)
				{
					base.WriteError(new RecipientTaskException(Strings.ErrorAccountEnabledForNonUserMailbox), ErrorCategory.InvalidArgument, this.userToConnect);
				}
				if (!string.IsNullOrEmpty(this.Alias))
				{
					this.alias = this.Alias;
				}
				else
				{
					this.alias = RecipientTaskHelper.GenerateUniqueAlias(this.globalCatalogSession, this.userToConnect.OrganizationId, this.userToConnect.Name, new Task.TaskVerboseLoggingDelegate(base.WriteVerbose));
				}
			}
			if (this.Archive && this.userToConnect.ManagedFolderMailboxPolicy != null)
			{
				base.WriteError(new MdbAdminTaskException(Strings.ErrorNoArchiveWithManagedFolder(this.userToConnect.Name)), ErrorCategory.InvalidData, this.Identity);
			}
			if (this.DataObject.IsArchiveMailbox != null && this.Archive != this.DataObject.IsArchiveMailbox.Value)
			{
				if (this.Archive)
				{
					base.WriteError(new MdbAdminTaskException(Strings.ErrorDisconnectedMailboxNotArchive(this.Identity.ToString(), this.userToConnect.Name)), ErrorCategory.InvalidArgument, this.Identity);
				}
				else
				{
					base.WriteError(new MdbAdminTaskException(Strings.ErrorDisconnectedMailboxNotPrimary(this.Identity.ToString(), this.userToConnect.Name)), ErrorCategory.InvalidArgument, this.Identity);
				}
			}
			ADSessionSettings sessionSettings = ADSessionSettings.FromCustomScopeSet(base.ScopeSet, ADSystemConfigurationSession.GetRootOrgContainerIdForLocalForest(), base.CurrentOrganizationId, base.ExecutingUserOrganizationId, true);
			MapiTaskHelper.VerifyDatabaseIsWithinScope(sessionSettings, this.OwnerMailboxDatabase, new Task.ErrorLoggerDelegate(base.WriteError));
			TaskLogger.LogExit();
		}

		// Token: 0x060028BF RID: 10431 RVA: 0x000A00B8 File Offset: 0x0009E2B8
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter(new object[]
			{
				this.DataObject
			});
			if (base.ParameterSetName == "ValidateOnly" || this.needListMatchingUser)
			{
				base.WriteObject(this.matchedUsers, true);
			}
			else if (this.Archive)
			{
				if (!this.Force && this.User == null && !base.ShouldContinue(Strings.ComfirmConnectToMatchingUser(this.userToConnect.Identity.ToString(), this.userToConnect.Alias)))
				{
					TaskLogger.LogExit();
					return;
				}
				ConnectMailbox.ConnectArchiveCore(this.userToConnect, this.DataObject.MailboxGuid, base.ParameterSetName, this.RecipientSession, this.TenantConfigurationSession, (MapiAdministrationSession)base.DataSession, this.alias, this.linkedUserSid, this.databaseLocationInfo, this.OwnerMailboxDatabase, new Task.TaskVerboseLoggingDelegate(base.WriteVerbose), new Task.TaskWarningLoggingDelegate(this.WriteWarning));
			}
			else
			{
				Organization orgContainer = this.TenantConfigurationSession.GetOrgContainer();
				if (this.DataObject.MailboxType != StoreMailboxType.Private)
				{
					if (orgContainer.DefaultPublicFolderMailbox.HierarchyMailboxGuid == Guid.Empty)
					{
						if (this.DataObject.MailboxType == StoreMailboxType.PublicFolderSecondary)
						{
							this.WriteWarning(Strings.WarningPromotingSecondaryToPrimary);
						}
					}
					else if (this.DataObject.MailboxType == StoreMailboxType.PublicFolderPrimary)
					{
						this.WriteWarning(Strings.WarningConnectingPrimaryAsSecondary);
					}
				}
				if (!this.Force && this.User == null && !base.ShouldContinue(Strings.ComfirmConnectToMatchingUser(this.userToConnect.Identity.ToString(), this.alias)))
				{
					TaskLogger.LogExit();
					return;
				}
				if (this.elcPolicyId != null && !this.Force && !this.ManagedFolderMailboxPolicyAllowed.IsPresent && !base.ShouldContinue(Strings.ConfirmManagedFolderMailboxPolicyAllowed(this.userToConnect.Identity.ToString())))
				{
					TaskLogger.LogExit();
					return;
				}
				if (!base.IsProvisioningLayerAvailable)
				{
					base.WriteError(new InvalidOperationException(Strings.ErrorNoProvisioningHandlerAvailable), ErrorCategory.InvalidOperation, null);
				}
				ADObjectId roleAssignmentPolicyId = null;
				RoleAssignmentPolicy roleAssignmentPolicy = RecipientTaskHelper.FindDefaultRoleAssignmentPolicy(this.TenantConfigurationSession, new Task.ErrorLoggerDelegate(base.WriteError), Strings.ErrorDefaultRoleAssignmentPolicyNotUnique, Strings.ErrorDefaultRoleAssignmentPolicyNotFound);
				if (roleAssignmentPolicy != null)
				{
					roleAssignmentPolicyId = (ADObjectId)roleAssignmentPolicy.Identity;
				}
				ConnectMailbox.ConnectMailboxCore(this.userToConnect, this.DataObject.MailboxGuid, this.DataObject.MailboxType, this.DataObject.LegacyDN, base.ParameterSetName, true, this.RecipientSession, (MapiAdministrationSession)base.DataSession, this.alias, this.linkedUserSid, this.databaseLocationInfo, this.OwnerMailboxDatabase, this.elcPolicyId, this.retentionPolicyId, this.mobilePolicyId, this.addressBookPolicyId, new Task.TaskVerboseLoggingDelegate(base.WriteVerbose), new Task.TaskWarningLoggingDelegate(this.WriteWarning), roleAssignmentPolicyId, this);
				if (this.DataObject.MailboxType != StoreMailboxType.Private && orgContainer.DefaultPublicFolderMailbox.HierarchyMailboxGuid == Guid.Empty)
				{
					orgContainer.DefaultPublicFolderMailbox = orgContainer.DefaultPublicFolderMailbox.Clone();
					orgContainer.DefaultPublicFolderMailbox.SetHierarchyMailbox(this.DataObject.MailboxGuid, PublicFolderInformation.HierarchyType.MailboxGuid);
					this.TenantConfigurationSession.Save(orgContainer);
					MailboxTaskHelper.PrepopulateCacheForMailbox(this.OwnerMailboxDatabase, this.databaseLocationInfo.ServerFqdn, this.userToConnect.OrganizationId, this.DataObject.LegacyDN, this.DataObject.MailboxGuid, this.TenantConfigurationSession.LastUsedDc, new Task.TaskWarningLoggingDelegate(this.WriteWarning), new Task.TaskVerboseLoggingDelegate(base.WriteVerbose));
				}
			}
			TaskLogger.LogExit();
		}

		// Token: 0x060028C0 RID: 10432 RVA: 0x000A044D File Offset: 0x0009E64D
		protected override void ProvisioningUpdateConfigurationObject()
		{
		}

		// Token: 0x060028C1 RID: 10433 RVA: 0x000A0450 File Offset: 0x0009E650
		private ADRecipient[] FindMatchedUser(MailboxStatistics storeMailbox, bool? accountEnabled)
		{
			int num = storeMailbox.LegacyDN.ToUpperInvariant().LastIndexOf("/CN=");
			string propertyValue = storeMailbox.LegacyDN.Substring(num + "/CN=".Length);
			QueryFilter queryFilter = new ComparisonFilter(ComparisonOperator.Equal, ADMailboxRecipientSchema.SamAccountName, propertyValue);
			QueryFilter queryFilter2 = new ComparisonFilter(ComparisonOperator.Equal, ADMailboxRecipientSchema.SamAccountName, storeMailbox.DisplayName);
			QueryFilter queryFilter3 = new ComparisonFilter(ComparisonOperator.Equal, ADRecipientSchema.DisplayName, propertyValue);
			QueryFilter queryFilter4 = new ComparisonFilter(ComparisonOperator.Equal, ADRecipientSchema.DisplayName, storeMailbox.DisplayName);
			QueryFilter queryFilter5 = new ComparisonFilter(ComparisonOperator.Equal, ADRecipientSchema.RecipientType, RecipientType.User);
			AndFilter andFilter = new AndFilter(new QueryFilter[]
			{
				queryFilter5,
				new OrFilter(new QueryFilter[]
				{
					queryFilter,
					queryFilter2,
					queryFilter3,
					queryFilter4
				})
			});
			if (accountEnabled != null && storeMailbox.MailboxType == StoreMailboxType.Private)
			{
				QueryFilter queryFilter6 = new BitMaskAndFilter(ADUserSchema.UserAccountControl, 2UL);
				if (accountEnabled.Value)
				{
					queryFilter6 = new NotFilter(queryFilter6);
				}
				andFilter = new AndFilter(new QueryFilter[]
				{
					andFilter,
					queryFilter6
				});
			}
			return this.GlobalCatalogSession.Find(null, QueryScope.SubTree, andFilter, null, 0);
		}

		// Token: 0x060028C2 RID: 10434 RVA: 0x000A0580 File Offset: 0x0009E780
		internal static ADRecipient FindMailboxByLegacyDN(string legacyDN, IRecipientSession globalCatalogSession)
		{
			QueryFilter queryFilter = new ComparisonFilter(ComparisonOperator.Equal, ADRecipientSchema.LegacyExchangeDN, legacyDN);
			QueryFilter queryFilter2 = new ComparisonFilter(ComparisonOperator.Equal, ADRecipientSchema.RecipientType, RecipientType.UserMailbox);
			AndFilter filter = new AndFilter(new QueryFilter[]
			{
				queryFilter2,
				queryFilter
			});
			ADRecipient[] array = globalCatalogSession.Find(null, QueryScope.SubTree, filter, null, 1);
			if (array != null && array.Length > 0)
			{
				return array[0];
			}
			return null;
		}

		// Token: 0x060028C3 RID: 10435 RVA: 0x000A05E0 File Offset: 0x0009E7E0
		internal static void CheckLegacyDNNotInUse(MailboxId disconnectedMailboxIdentity, string disconnectedMailboxLegacyDN, IRecipientSession globalCatalogSession, Task.ErrorLoggerDelegate errorLogger)
		{
			ADRecipient adrecipient = ConnectMailbox.FindMailboxByLegacyDN(disconnectedMailboxLegacyDN, globalCatalogSession);
			if (adrecipient != null)
			{
				errorLogger(new MdbAdminTaskException(Strings.ErrorMailboxLegacyDNInUse(disconnectedMailboxLegacyDN, disconnectedMailboxIdentity.ToString(), adrecipient.DisplayName)), ExchangeErrorCategory.ServerOperation, disconnectedMailboxIdentity);
			}
		}

		// Token: 0x060028C4 RID: 10436 RVA: 0x000A061C File Offset: 0x0009E81C
		private ADUser FindArchiveUser(MailboxStatistics storeMailbox, IRecipientSession globalCatalogSession, Task.TaskErrorLoggingDelegate errorLogger)
		{
			ADRecipient adrecipient = ConnectMailbox.FindMailboxByLegacyDN(storeMailbox.LegacyDN, globalCatalogSession);
			if (adrecipient == null)
			{
				errorLogger(new MdbAdminTaskException(Strings.ErrorRecipientNotFound(storeMailbox.LegacyDN)), ErrorCategory.InvalidArgument, storeMailbox);
			}
			return (ADUser)adrecipient;
		}

		// Token: 0x060028C5 RID: 10437 RVA: 0x000A0658 File Offset: 0x0009E858
		private static void CheckUserForArchive(MailboxStatistics disconnectedMailbox, IRecipientSession globalCatalogSession, Task.ErrorLoggerDelegate errorLogger, ADUser user, MailboxDatabase database, bool allowLegacyDNmismatch)
		{
			if (!string.Equals(user.LegacyExchangeDN, disconnectedMailbox.LegacyDN, StringComparison.OrdinalIgnoreCase) && !allowLegacyDNmismatch)
			{
				errorLogger(new MdbAdminTaskException(Strings.ErrorArchiveLegacyDNDoesNotMatchUser(disconnectedMailbox.LegacyDN, user.LegacyExchangeDN)), ExchangeErrorCategory.Client, disconnectedMailbox);
			}
			if (user.ExchangeVersion.IsOlderThan(ExchangeObjectVersion.Exchange2010))
			{
				errorLogger(new MdbAdminTaskException(Strings.ErrorArchiveUserVersionTooOld(user.ExchangeVersion.ToString())), ExchangeErrorCategory.Client, disconnectedMailbox);
			}
		}

		// Token: 0x060028C6 RID: 10438 RVA: 0x000A06D4 File Offset: 0x0009E8D4
		private static RawSecurityDescriptor UpdateMailboxSecurityDescriptor(SecurityIdentifier userSid, ADUser userToConnect, MapiAdministrationSession mapiAdministrationSession, MailboxDatabase database, Guid deletedMailboxGuid, string parameterSetName, Task.TaskVerboseLoggingDelegate verboseLogger)
		{
			RawSecurityDescriptor rawSecurityDescriptor = null;
			try
			{
				rawSecurityDescriptor = mapiAdministrationSession.GetMailboxSecurityDescriptor(new MailboxId(MapiTaskHelper.ConvertDatabaseADObjectToDatabaseId(database), deletedMailboxGuid));
			}
			catch (Microsoft.Exchange.Data.Mapi.Common.MailboxNotFoundException)
			{
				rawSecurityDescriptor = new RawSecurityDescriptor(ControlFlags.DiscretionaryAclDefaulted | ControlFlags.SystemAclDefaulted | ControlFlags.SelfRelative, WindowsIdentity.GetCurrent().User, WindowsIdentity.GetCurrent().User, null, null);
				DiscretionaryAcl discretionaryAcl = new DiscretionaryAcl(true, true, 0);
				byte[] binaryForm = new byte[discretionaryAcl.BinaryLength];
				discretionaryAcl.GetBinaryForm(binaryForm, 0);
				rawSecurityDescriptor.DiscretionaryAcl = new RawAcl(binaryForm, 0);
			}
			bool flag = false;
			foreach (GenericAce genericAce in rawSecurityDescriptor.DiscretionaryAcl)
			{
				KnownAce knownAce = (KnownAce)genericAce;
				if (knownAce.SecurityIdentifier.IsWellKnown(WellKnownSidType.SelfSid))
				{
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				CommonAce ace = new CommonAce(AceFlags.ContainerInherit, AceQualifier.AccessAllowed, 131073, new SecurityIdentifier(WellKnownSidType.SelfSid, null), false, null);
				rawSecurityDescriptor.DiscretionaryAcl.InsertAce(0, ace);
			}
			rawSecurityDescriptor.SetFlags(rawSecurityDescriptor.ControlFlags | ControlFlags.SelfRelative);
			if ("Linked" == parameterSetName || "Shared" == parameterSetName || "Room" == parameterSetName || "Equipment" == parameterSetName)
			{
				RawSecurityDescriptor sd = userToConnect.ReadSecurityDescriptor();
				MailboxTaskHelper.GrantPermissionToLinkedUserAccount(userToConnect.MasterAccountSid, ref rawSecurityDescriptor, ref sd);
				verboseLogger(Strings.VerboseSaveADSecurityDescriptor(userToConnect.Id.ToString()));
				userToConnect.SaveSecurityDescriptor(sd);
			}
			mapiAdministrationSession.Administration.PurgeCachedMailboxObject(deletedMailboxGuid);
			return rawSecurityDescriptor;
		}

		// Token: 0x060028C7 RID: 10439 RVA: 0x000A084C File Offset: 0x0009EA4C
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.mapiAdministrationSession != null)
			{
				this.mapiAdministrationSession.Dispose();
				this.mapiAdministrationSession = null;
			}
			base.Dispose(disposing);
		}

		// Token: 0x060028C8 RID: 10440 RVA: 0x000A0874 File Offset: 0x0009EA74
		internal static void ConnectMailboxCore(ADUser userToConnect, Guid deletedMailboxGuid, StoreMailboxType mailboxType, string deletedMailboxLegacyDN, string parameterSetName, bool clearPropertiesBeforeConnecting, IRecipientSession recipientSession, MapiAdministrationSession mapiAdministrationSession, string alias, SecurityIdentifier linkedUserSid, DatabaseLocationInfo databaseLocationInfo, MailboxDatabase database, ADObjectId elcPolicyId, ADObjectId retentionPolicyId, ADObjectId mobilePolicyId, ADObjectId addressBookPolicyId, Task.TaskVerboseLoggingDelegate verboseLogger, Task.TaskWarningLoggingDelegate warningLogger, ADObjectId roleAssignmentPolicyId, Task task)
		{
			if (userToConnect.ExchangeVersion.IsOlderThan(ExchangeObjectVersion.Exchange2012))
			{
				verboseLogger(Strings.VerboseUpdatingVersion(userToConnect.Identity.ToString(), userToConnect.ExchangeVersion.ToString(), ExchangeObjectVersion.Exchange2012.ToString()));
				userToConnect.SetExchangeVersion(ExchangeObjectVersion.Exchange2012);
				recipientSession.Save(userToConnect);
				verboseLogger(Strings.VerboseADOperationSucceeded(userToConnect.Identity.ToString()));
				bool useGlobalCatalog = recipientSession.UseGlobalCatalog;
				try
				{
					recipientSession.UseGlobalCatalog = false;
					userToConnect = (ADUser)recipientSession.Read(userToConnect.Id);
				}
				finally
				{
					recipientSession.UseGlobalCatalog = useGlobalCatalog;
				}
			}
			if (clearPropertiesBeforeConnecting)
			{
				List<PropertyDefinition> list = new List<PropertyDefinition>(RecipientConstants.DisableMailbox_PropertiesToReset);
				MailboxTaskHelper.RemovePersistentProperties(list);
				list.Remove(ADObjectSchema.ExchangeVersion);
				MailboxTaskHelper.ClearExchangeProperties(userToConnect, list);
			}
			userToConnect.Alias = alias;
			if ("Linked" == parameterSetName)
			{
				userToConnect.MasterAccountSid = linkedUserSid;
			}
			else if ("Shared" == parameterSetName)
			{
				userToConnect.MasterAccountSid = new SecurityIdentifier(WellKnownSidType.SelfSid, null);
			}
			else if ("Room" == parameterSetName)
			{
				userToConnect.ResourceType = new ExchangeResourceType?(ExchangeResourceType.Room);
				userToConnect.MasterAccountSid = new SecurityIdentifier(WellKnownSidType.SelfSid, null);
			}
			else if ("Equipment" == parameterSetName)
			{
				userToConnect.ResourceType = new ExchangeResourceType?(ExchangeResourceType.Equipment);
				userToConnect.MasterAccountSid = new SecurityIdentifier(WellKnownSidType.SelfSid, null);
			}
			userToConnect.ServerLegacyDN = databaseLocationInfo.ServerLegacyDN;
			userToConnect.ExchangeGuid = deletedMailboxGuid;
			userToConnect.Database = (ADObjectId)database.Identity;
			userToConnect.LegacyExchangeDN = deletedMailboxLegacyDN.ToLowerInvariant();
			userToConnect.ManagedFolderMailboxPolicy = elcPolicyId;
			userToConnect.RetentionPolicy = retentionPolicyId;
			userToConnect.ActiveSyncMailboxPolicy = mobilePolicyId;
			userToConnect.UseDatabaseQuotaDefaults = new bool?(true);
			userToConnect.AddressBookPolicy = addressBookPolicyId;
			if (roleAssignmentPolicyId != null)
			{
				userToConnect.RoleAssignmentPolicy = roleAssignmentPolicyId;
			}
			if (mailboxType == StoreMailboxType.PublicFolderPrimary || mailboxType == StoreMailboxType.PublicFolderSecondary)
			{
				userToConnect.MasterAccountSid = new SecurityIdentifier(WellKnownSidType.SelfSid, null);
				userToConnect.UserAccountControl = (UserAccountControlFlags.AccountDisabled | UserAccountControlFlags.NormalAccount);
				userToConnect.ExchangeUserAccountControl |= UserAccountControlFlags.AccountDisabled;
				MailboxTaskHelper.StampMailboxRecipientTypes(userToConnect, "PublicFolder");
			}
			else
			{
				MailboxTaskHelper.StampMailboxRecipientTypes(userToConnect, parameterSetName);
			}
			if (MailboxTaskHelper.SupportsMailboxReleaseVersioning(userToConnect))
			{
				userToConnect.MailboxRelease = databaseLocationInfo.MailboxRelease;
			}
			userToConnect.EmailAddressPolicyEnabled = true;
			ProvisioningLayer.UpdateAffectedIConfigurable(task, RecipientTaskHelper.ConvertRecipientToPresentationObject(userToConnect), false);
			recipientSession.Save(userToConnect);
			verboseLogger(Strings.VerboseADOperationSucceeded(userToConnect.Identity.ToString()));
			ConnectMailbox.UpdateSDAndRefreshMailbox(mapiAdministrationSession, userToConnect, database, deletedMailboxGuid, parameterSetName, verboseLogger, warningLogger);
		}

		// Token: 0x060028C9 RID: 10441 RVA: 0x000A0AEC File Offset: 0x0009ECEC
		private static void ConnectArchiveCore(ADUser userToConnect, Guid deletedMailboxGuid, string parameterSetName, IRecipientSession recipientSession, IConfigurationSession configSession, MapiAdministrationSession mapiAdministrationSession, string alias, SecurityIdentifier linkedUserSid, DatabaseLocationInfo databaseLocationInfo, MailboxDatabase database, Task.TaskVerboseLoggingDelegate verboseLogger, Task.TaskWarningLoggingDelegate warningLogger)
		{
			userToConnect.ArchiveDatabase = (ADObjectId)database.Identity;
			userToConnect.ArchiveGuid = deletedMailboxGuid;
			userToConnect.ArchiveName = new MultiValuedProperty<string>(Strings.ArchiveNamePrefix + userToConnect.DisplayName);
			if (MailboxTaskHelper.SupportsMailboxReleaseVersioning(userToConnect))
			{
				userToConnect.ArchiveRelease = databaseLocationInfo.MailboxRelease;
			}
			userToConnect.ArchiveQuota = ProvisioningHelper.DefaultArchiveQuota;
			userToConnect.ArchiveWarningQuota = ProvisioningHelper.DefaultArchiveWarningQuota;
			MailboxTaskHelper.ApplyDefaultArchivePolicy(userToConnect, configSession);
			recipientSession.Save(userToConnect);
			verboseLogger(Strings.VerboseADOperationSucceeded(userToConnect.Identity.ToString()));
			ConnectMailbox.UpdateSDAndRefreshMailbox(mapiAdministrationSession, userToConnect, database, deletedMailboxGuid, parameterSetName, verboseLogger, warningLogger);
		}

		// Token: 0x060028CA RID: 10442 RVA: 0x000A0B94 File Offset: 0x0009ED94
		internal static void UpdateSDAndRefreshMailbox(MapiAdministrationSession mapiAdministrationSession, ADUser userToConnect, MailboxDatabase database, Guid mailboxGuid, string parameterSetName, Task.TaskVerboseLoggingDelegate verboseLogger, Task.TaskWarningLoggingDelegate warningLogger)
		{
			ConnectMailbox.UpdateMailboxSecurityDescriptor(userToConnect.Sid, userToConnect, mapiAdministrationSession, database, mailboxGuid, parameterSetName, verboseLogger);
			try
			{
				mapiAdministrationSession.ForceStoreToRefreshMailbox(new MailboxId(MapiTaskHelper.ConvertDatabaseADObjectToDatabaseId(database), mailboxGuid));
			}
			catch (FailedToRefreshMailboxException ex)
			{
				TaskLogger.Trace("An exception is caught and ignored when refreshing the mailbox '{0}'. Exception: {1}", new object[]
				{
					mailboxGuid,
					ex.Message
				});
				warningLogger(Strings.WarningReplicationLatency);
			}
			try
			{
				mapiAdministrationSession.SyncMailboxWithDS(new MailboxId(MapiTaskHelper.ConvertDatabaseADObjectToDatabaseId(database), mailboxGuid));
			}
			catch (DataSourceTransientException ex2)
			{
				TaskLogger.Trace("Swallowing exception {0} from mapi.net", new object[]
				{
					ex2
				});
				warningLogger(ex2.LocalizedString);
			}
			catch (DataSourceOperationException ex3)
			{
				TaskLogger.Trace("Swallowing exception {0} from mapi.net", new object[]
				{
					ex3
				});
				warningLogger(ex3.LocalizedString);
			}
			catch (ArgumentNullException ex4)
			{
				TaskLogger.Trace("Swallowing exception {0} from mapi.net", new object[]
				{
					ex4
				});
				warningLogger(Strings.ErrorNoDatabaseInfor);
			}
		}

		// Token: 0x04001E15 RID: 7701
		private const string NamePrefixInLegacyDN = "/CN=";

		// Token: 0x04001E16 RID: 7702
		internal const string ParameterSetValidateOnly = "ValidateOnly";

		// Token: 0x04001E17 RID: 7703
		private ADUser userToConnect;

		// Token: 0x04001E18 RID: 7704
		private ADRecipient[] matchedUsers;

		// Token: 0x04001E19 RID: 7705
		private bool needListMatchingUser;

		// Token: 0x04001E1A RID: 7706
		private SecurityIdentifier linkedUserSid;

		// Token: 0x04001E1B RID: 7707
		private ADObjectId elcPolicyId;

		// Token: 0x04001E1C RID: 7708
		private ADObjectId retentionPolicyId;

		// Token: 0x04001E1D RID: 7709
		private ADObjectId mobilePolicyId;

		// Token: 0x04001E1E RID: 7710
		private ADObjectId addressBookPolicyId;

		// Token: 0x04001E1F RID: 7711
		private string alias;

		// Token: 0x04001E20 RID: 7712
		private IConfigurationSession tenantConfigurationSession;

		// Token: 0x04001E21 RID: 7713
		private ITopologyConfigurationSession resourceForestSession;

		// Token: 0x04001E22 RID: 7714
		private IRecipientSession recipientSession;

		// Token: 0x04001E23 RID: 7715
		private MailboxDatabase ownerMailboxDatabase;

		// Token: 0x04001E24 RID: 7716
		private IRecipientSession globalCatalogSession;

		// Token: 0x04001E25 RID: 7717
		private MapiAdministrationSession mapiAdministrationSession;

		// Token: 0x04001E26 RID: 7718
		private DatabaseLocationInfo databaseLocationInfo;
	}
}
