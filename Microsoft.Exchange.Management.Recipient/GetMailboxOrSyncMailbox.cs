using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Authorization;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.ProvisioningCache;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x0200004C RID: 76
	public abstract class GetMailboxOrSyncMailbox : GetMailboxBase<MailboxIdParameter>
	{
		// Token: 0x1700019C RID: 412
		// (get) Token: 0x06000487 RID: 1159 RVA: 0x000145A4 File Offset: 0x000127A4
		protected override RecipientTypeDetails[] InternalRecipientTypeDetails
		{
			get
			{
				if (this.Arbitration)
				{
					return GetUser.ArbitrationAllowedRecipientTypeDetails;
				}
				if (this.PublicFolder)
				{
					return GetUser.PublicFolderAllowedRecipientTypeDetails;
				}
				if (this.Monitoring)
				{
					return GetUser.MonitoringAllowedRecipientTypeDetails;
				}
				if (this.AuditLog)
				{
					return GetUser.AuditLogAllowedRecipientTypeDetails;
				}
				return this.RecipientTypeDetails ?? RecipientConstants.GetMailboxOrSyncMailbox_AllowedRecipientTypeDetails;
			}
		}

		// Token: 0x1700019D RID: 413
		// (get) Token: 0x06000488 RID: 1160 RVA: 0x0001460C File Offset: 0x0001280C
		protected override string SystemAddressListRdn
		{
			get
			{
				bool flag = false;
				bool flag2 = false;
				if (this.RecipientTypeDetails != null && Array.IndexOf<RecipientTypeDetails>(this.RecipientTypeDetails, Microsoft.Exchange.Data.Directory.Recipient.RecipientTypeDetails.TeamMailbox) >= 0)
				{
					flag2 = true;
					flag = (this.RecipientTypeDetails.Length == 1);
				}
				if (this.Arbitration.IsPresent || this.Monitoring.IsPresent || this.AuditLog.IsPresent || flag2)
				{
					return null;
				}
				if (flag)
				{
					return "TeamMailboxes(VLV)";
				}
				if (this.PublicFolder.IsPresent)
				{
					return "PublicFolderMailboxes(VLV)";
				}
				return "All Mailboxes(VLV)";
			}
		}

		// Token: 0x1700019E RID: 414
		// (get) Token: 0x06000489 RID: 1161 RVA: 0x000146A8 File Offset: 0x000128A8
		protected override QueryFilter InternalFilter
		{
			get
			{
				QueryFilter queryFilter = null;
				if (this.scopeObject is MailboxPlan)
				{
					queryFilter = new ComparisonFilter(ComparisonOperator.Equal, ADRecipientSchema.MailboxPlan, ((MailboxPlan)this.scopeObject).Id);
				}
				else if (this.scopeObject is Server)
				{
					queryFilter = new ComparisonFilter(ComparisonOperator.Equal, ADMailboxRecipientSchema.ServerLegacyDN, ((Server)this.scopeObject).ExchangeLegacyDN);
				}
				else if (this.scopeObject is MailboxDatabase)
				{
					queryFilter = new ComparisonFilter(ComparisonOperator.Equal, ADMailboxRecipientSchema.Database, ((MailboxDatabase)this.scopeObject).Id);
				}
				return QueryFilter.AndTogether(new QueryFilter[]
				{
					base.InternalFilter,
					queryFilter,
					this.Archive.IsPresent ? GetMailboxOrSyncMailbox.ArchiveFilter : null,
					this.RemoteArchive.IsPresent ? GetMailboxOrSyncMailbox.RemoteArchiveFilter : null,
					this.SoftDeletedMailbox.IsPresent ? GetMailboxOrSyncMailbox.SoftDeletedMailboxFilter : null,
					this.InactiveMailboxOnly.IsPresent ? GetMailboxOrSyncMailbox.InactiveMailboxFilter : null,
					this.AuxMailbox.IsPresent ? GetMailboxOrSyncMailbox.AuxMailboxFilter : new NotFilter(GetMailboxOrSyncMailbox.AuxMailboxFilter)
				});
			}
		}

		// Token: 0x1700019F RID: 415
		// (get) Token: 0x0600048A RID: 1162 RVA: 0x000147E3 File Offset: 0x000129E3
		internal override ObjectSchema FilterableObjectSchema
		{
			get
			{
				return ObjectSchema.GetInstance<MailboxSchema>();
			}
		}

		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x0600048B RID: 1163 RVA: 0x000147EA File Offset: 0x000129EA
		// (set) Token: 0x0600048C RID: 1164 RVA: 0x00014801 File Offset: 0x00012A01
		[Parameter]
		[ValidateNotNullOrEmpty]
		public RecipientTypeDetails[] RecipientTypeDetails
		{
			get
			{
				return (RecipientTypeDetails[])base.Fields["RecipientTypeDetails"];
			}
			set
			{
				base.VerifyValues<RecipientTypeDetails>(RecipientConstants.GetMailboxOrSyncMailbox_AllowedRecipientTypeDetails, value);
				base.Fields["RecipientTypeDetails"] = value;
			}
		}

		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x0600048D RID: 1165 RVA: 0x00014820 File Offset: 0x00012A20
		// (set) Token: 0x0600048E RID: 1166 RVA: 0x00014837 File Offset: 0x00012A37
		[ValidateNotNullOrEmpty]
		[Parameter(ParameterSetName = "MailboxPlanSet", ValueFromPipeline = true)]
		public MailboxPlanIdParameter MailboxPlan
		{
			get
			{
				return (MailboxPlanIdParameter)base.Fields["MailboxPlan"];
			}
			set
			{
				base.Fields["MailboxPlan"] = value;
			}
		}

		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x0600048F RID: 1167 RVA: 0x0001484A File Offset: 0x00012A4A
		// (set) Token: 0x06000490 RID: 1168 RVA: 0x00014861 File Offset: 0x00012A61
		[ValidateNotNullOrEmpty]
		[Parameter(ParameterSetName = "ServerSet", ValueFromPipeline = true)]
		public ServerIdParameter Server
		{
			get
			{
				return (ServerIdParameter)base.Fields["Server"];
			}
			set
			{
				base.Fields["Server"] = value;
			}
		}

		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x06000491 RID: 1169 RVA: 0x00014874 File Offset: 0x00012A74
		// (set) Token: 0x06000492 RID: 1170 RVA: 0x0001488B File Offset: 0x00012A8B
		[ValidateNotNullOrEmpty]
		[Parameter(ParameterSetName = "DatabaseSet", ValueFromPipeline = true)]
		public DatabaseIdParameter Database
		{
			get
			{
				return (DatabaseIdParameter)base.Fields["Database"];
			}
			set
			{
				base.Fields["Database"] = value;
			}
		}

		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x06000493 RID: 1171 RVA: 0x0001489E File Offset: 0x00012A9E
		// (set) Token: 0x06000494 RID: 1172 RVA: 0x000148C4 File Offset: 0x00012AC4
		[Parameter(Mandatory = false)]
		public SwitchParameter Arbitration
		{
			get
			{
				return (SwitchParameter)(base.Fields["Arbitration"] ?? false);
			}
			set
			{
				base.Fields["Arbitration"] = value;
			}
		}

		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x06000495 RID: 1173 RVA: 0x000148DC File Offset: 0x00012ADC
		// (set) Token: 0x06000496 RID: 1174 RVA: 0x00014902 File Offset: 0x00012B02
		[Parameter(Mandatory = false)]
		public SwitchParameter PublicFolder
		{
			get
			{
				return (SwitchParameter)(base.Fields["PublicFolder"] ?? false);
			}
			set
			{
				base.Fields["PublicFolder"] = value;
			}
		}

		// Token: 0x170001A6 RID: 422
		// (get) Token: 0x06000497 RID: 1175 RVA: 0x0001491A File Offset: 0x00012B1A
		// (set) Token: 0x06000498 RID: 1176 RVA: 0x00014940 File Offset: 0x00012B40
		[Parameter(Mandatory = false)]
		public SwitchParameter AuxMailbox
		{
			get
			{
				return (SwitchParameter)(base.Fields["AuxMailbox"] ?? false);
			}
			set
			{
				base.Fields["AuxMailbox"] = value;
			}
		}

		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x06000499 RID: 1177 RVA: 0x00014958 File Offset: 0x00012B58
		// (set) Token: 0x0600049A RID: 1178 RVA: 0x0001497E File Offset: 0x00012B7E
		[Parameter(Mandatory = false)]
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

		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x0600049B RID: 1179 RVA: 0x00014996 File Offset: 0x00012B96
		// (set) Token: 0x0600049C RID: 1180 RVA: 0x000149BC File Offset: 0x00012BBC
		[Parameter(Mandatory = false)]
		public SwitchParameter RemoteArchive
		{
			get
			{
				return (SwitchParameter)(base.Fields["RemoteArchive"] ?? false);
			}
			set
			{
				base.Fields["RemoteArchive"] = value;
			}
		}

		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x0600049D RID: 1181 RVA: 0x000149D4 File Offset: 0x00012BD4
		// (set) Token: 0x0600049E RID: 1182 RVA: 0x000149DC File Offset: 0x00012BDC
		[Parameter(Mandatory = false)]
		public SwitchParameter SoftDeletedMailbox
		{
			get
			{
				return base.SoftDeletedObject;
			}
			set
			{
				base.SoftDeletedObject = value;
			}
		}

		// Token: 0x170001AA RID: 426
		// (get) Token: 0x0600049F RID: 1183 RVA: 0x000149E5 File Offset: 0x00012BE5
		// (set) Token: 0x060004A0 RID: 1184 RVA: 0x00014A0B File Offset: 0x00012C0B
		[Parameter(Mandatory = false)]
		public SwitchParameter IncludeSoftDeletedMailbox
		{
			get
			{
				return (SwitchParameter)(base.Fields["IncludeSoftDeletedMailbox"] ?? false);
			}
			set
			{
				base.Fields["IncludeSoftDeletedMailbox"] = value;
			}
		}

		// Token: 0x170001AB RID: 427
		// (get) Token: 0x060004A1 RID: 1185 RVA: 0x00014A23 File Offset: 0x00012C23
		// (set) Token: 0x060004A2 RID: 1186 RVA: 0x00014A49 File Offset: 0x00012C49
		[Parameter(Mandatory = false)]
		public SwitchParameter InactiveMailboxOnly
		{
			get
			{
				return (SwitchParameter)(base.Fields["InactiveMailboxOnly"] ?? false);
			}
			set
			{
				base.Fields["InactiveMailboxOnly"] = value;
			}
		}

		// Token: 0x170001AC RID: 428
		// (get) Token: 0x060004A3 RID: 1187 RVA: 0x00014A61 File Offset: 0x00012C61
		// (set) Token: 0x060004A4 RID: 1188 RVA: 0x00014A87 File Offset: 0x00012C87
		[Parameter(Mandatory = false)]
		public SwitchParameter IncludeInactiveMailbox
		{
			get
			{
				return (SwitchParameter)(base.Fields["IncludeInactiveMailbox"] ?? false);
			}
			set
			{
				base.Fields["IncludeInactiveMailbox"] = value;
			}
		}

		// Token: 0x170001AD RID: 429
		// (get) Token: 0x060004A5 RID: 1189 RVA: 0x00014A9F File Offset: 0x00012C9F
		// (set) Token: 0x060004A6 RID: 1190 RVA: 0x00014AC5 File Offset: 0x00012CC5
		[Parameter(Mandatory = false)]
		public SwitchParameter Monitoring
		{
			get
			{
				return (SwitchParameter)(base.Fields["Monitoring"] ?? false);
			}
			set
			{
				base.Fields["Monitoring"] = value;
			}
		}

		// Token: 0x170001AE RID: 430
		// (get) Token: 0x060004A7 RID: 1191 RVA: 0x00014ADD File Offset: 0x00012CDD
		// (set) Token: 0x060004A8 RID: 1192 RVA: 0x00014B03 File Offset: 0x00012D03
		[Parameter(Mandatory = false)]
		public SwitchParameter AuditLog
		{
			get
			{
				return (SwitchParameter)(base.Fields["AuditLog"] ?? false);
			}
			set
			{
				base.Fields["AuditLog"] = value;
			}
		}

		// Token: 0x060004A9 RID: 1193 RVA: 0x00014B1C File Offset: 0x00012D1C
		protected override void InternalBeginProcessing()
		{
			base.OptionalIdentityData.AdditionalFilter = QueryFilter.AndTogether(new QueryFilter[]
			{
				base.OptionalIdentityData.AdditionalFilter,
				this.Archive.IsPresent ? GetMailboxOrSyncMailbox.ArchiveFilter : null,
				this.RemoteArchive.IsPresent ? GetMailboxOrSyncMailbox.RemoteArchiveFilter : null,
				this.SoftDeletedMailbox.IsPresent ? GetMailboxOrSyncMailbox.SoftDeletedMailboxFilter : null,
				this.InactiveMailboxOnly.IsPresent ? GetMailboxOrSyncMailbox.InactiveMailboxFilter : null,
				this.AuxMailbox.IsPresent ? GetMailboxOrSyncMailbox.AuxMailboxFilter : new NotFilter(GetMailboxOrSyncMailbox.AuxMailboxFilter)
			});
			base.InternalBeginProcessing();
		}

		// Token: 0x060004AA RID: 1194 RVA: 0x00014BE8 File Offset: 0x00012DE8
		protected override IConfigDataProvider CreateSession()
		{
			IRecipientSession recipientSession = (IRecipientSession)base.CreateSession();
			ADObjectId searchRoot = recipientSession.SearchRoot;
			if (this.SoftDeletedMailbox.IsPresent && base.CurrentOrganizationId != null && base.CurrentOrganizationId.OrganizationalUnit != null)
			{
				searchRoot = new ADObjectId("OU=Soft Deleted Objects," + base.CurrentOrganizationId.OrganizationalUnit.DistinguishedName);
			}
			if (base.ParameterSetName == "DatabaseSet" || base.ParameterSetName == "ServerSet" || base.ParameterSetName == "MailboxPlanSet" || this.SoftDeletedMailbox.IsPresent || this.IncludeSoftDeletedMailbox.IsPresent)
			{
				if (this.SoftDeletedMailbox.IsPresent || this.IncludeSoftDeletedMailbox.IsPresent)
				{
					recipientSession.SessionSettings.IncludeSoftDeletedObjects = true;
				}
				IRecipientSession tenantOrRootOrgRecipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(recipientSession.DomainController, searchRoot, recipientSession.Lcid, recipientSession.ReadOnly, recipientSession.ConsistencyMode, recipientSession.NetworkCredential, recipientSession.SessionSettings, ConfigScopes.TenantSubTree, 417, "CreateSession", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\RecipientTasks\\mailbox\\GetMailbox.cs");
				tenantOrRootOrgRecipientSession.EnforceDefaultScope = recipientSession.EnforceDefaultScope;
				tenantOrRootOrgRecipientSession.UseGlobalCatalog = recipientSession.UseGlobalCatalog;
				tenantOrRootOrgRecipientSession.LinkResolutionServer = recipientSession.LinkResolutionServer;
				recipientSession = tenantOrRootOrgRecipientSession;
			}
			if (this.IncludeInactiveMailbox.IsPresent)
			{
				recipientSession = SoftDeletedTaskHelper.CreateTenantOrRootOrgRecipientSessionIncludeInactiveMailbox(recipientSession, base.CurrentOrganizationId);
			}
			else if (this.InactiveMailboxOnly.IsPresent)
			{
				recipientSession = SoftDeletedTaskHelper.CreateTenantOrRootOrgRecipientSessionInactiveMailboxOnly(recipientSession, base.CurrentOrganizationId);
			}
			return recipientSession;
		}

		// Token: 0x060004AB RID: 1195 RVA: 0x00014D84 File Offset: 0x00012F84
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			base.InternalValidate();
			if (base.HasErrors)
			{
				return;
			}
			if (this.MailboxPlan != null)
			{
				this.scopeObject = new MailboxPlan((ADUser)base.GetDataObject<ADUser>(this.MailboxPlan, base.TenantGlobalCatalogSession, null, new LocalizedString?(Strings.ErrorMailboxPlanNotFound(this.MailboxPlan.ToString())), new LocalizedString?(Strings.ErrorMailboxPlanNotUnique(this.MailboxPlan.ToString()))));
			}
			else if (this.Server != null)
			{
				this.scopeObject = (Server)base.GetDataObject<Server>(this.Server, base.GlobalConfigSession, null, new LocalizedString?(Strings.ErrorServerNotFound(this.Server.ToString())), new LocalizedString?(Strings.ErrorServerNotUnique(this.Server.ToString())));
			}
			else if (this.Database != null)
			{
				if (MapiTaskHelper.IsDatacenter && base.AccountPartition == null)
				{
					this.WriteWarning(Strings.ImplicitPartitionIdSupplied(base.SessionSettings.PartitionId.ToString()));
				}
				this.Database.AllowInvalid = true;
				this.Database.AllowLegacy = true;
				this.scopeObject = (MailboxDatabase)base.GetDataObject<MailboxDatabase>(this.Database, base.GlobalConfigSession, null, new LocalizedString?(Strings.ErrorDatabaseNotFound(this.Database.ToString())), new LocalizedString?(Strings.ErrorDatabaseNotUnique(this.Database.ToString())));
			}
			base.CheckExclusiveParameters(new object[]
			{
				"IncludeSoftDeletedMailbox",
				"SoftDeletedMailbox",
				"IncludeInactiveMailbox",
				"InactiveMailboxOnly"
			});
			TaskLogger.LogExit();
		}

		// Token: 0x060004AC RID: 1196 RVA: 0x00014F44 File Offset: 0x00013144
		protected override IConfigurable ConvertDataObjectToPresentationObject(IConfigurable dataObject)
		{
			if (dataObject == null)
			{
				return null;
			}
			ADUser user = (ADUser)dataObject;
			SharedConfiguration sharedConfig = null;
			if (SharedConfiguration.IsDehydratedConfiguration(user.OrganizationId) || (SharedConfiguration.GetSharedConfigurationState(user.OrganizationId) & SharedTenantConfigurationState.Static) != SharedTenantConfigurationState.UnSupported)
			{
				sharedConfig = base.ProvisioningCache.TryAddAndGetOrganizationData<SharedConfiguration>(CannedProvisioningCacheKeys.MailboxSharedConfigCacheKey, user.OrganizationId, () => SharedConfiguration.GetSharedConfiguration(user.OrganizationId));
			}
			if (null != user.MasterAccountSid)
			{
				user.LinkedMasterAccount = SecurityPrincipalIdParameter.GetFriendlyUserName(user.MasterAccountSid, new Task.TaskVerboseLoggingDelegate(base.WriteVerbose));
				user.ResetChangeTracking();
			}
			Mailbox mailbox = new Mailbox(user);
			mailbox.propertyBag.SetField(MailboxSchema.Database, ADObjectIdResolutionHelper.ResolveDN(mailbox.Database));
			if (sharedConfig != null)
			{
				mailbox.SharedConfiguration = sharedConfig.SharedConfigId.ConfigurationUnit;
				if (mailbox.RoleAssignmentPolicy == null)
				{
					mailbox.RoleAssignmentPolicy = base.ProvisioningCache.TryAddAndGetOrganizationData<ADObjectId>(CannedProvisioningCacheKeys.MailboxRoleAssignmentPolicyCacheKey, user.OrganizationId, () => sharedConfig.GetSharedRoleAssignmentPolicy());
				}
			}
			else if (mailbox.RoleAssignmentPolicy == null && !mailbox.ExchangeVersion.IsOlderThan(MailboxSchema.RoleAssignmentPolicy.VersionAdded))
			{
				ADObjectId defaultRoleAssignmentPolicy = RBACHelper.GetDefaultRoleAssignmentPolicy(user.OrganizationId);
				if (defaultRoleAssignmentPolicy != null)
				{
					mailbox.RoleAssignmentPolicy = defaultRoleAssignmentPolicy;
				}
			}
			if (mailbox.SharingPolicy == null && !mailbox.propertyBag.IsReadOnlyProperty(MailboxSchema.SharingPolicy))
			{
				mailbox.SharingPolicy = base.GetDefaultSharingPolicyId(user, sharedConfig);
			}
			if (mailbox.RetentionPolicy == null && mailbox.ShouldUseDefaultRetentionPolicy && !mailbox.propertyBag.IsReadOnlyProperty(MailboxSchema.RetentionPolicy))
			{
				mailbox.RetentionPolicy = base.GetDefaultRetentionPolicyId(user, sharedConfig);
			}
			if (mailbox.Database != null && mailbox.UseDatabaseRetentionDefaults)
			{
				this.SetDefaultRetentionValues(mailbox);
			}
			mailbox.AdminDisplayVersion = Microsoft.Exchange.Data.Directory.SystemConfiguration.Server.GetServerVersion(mailbox.ServerName);
			return mailbox;
		}

		// Token: 0x060004AD RID: 1197 RVA: 0x00015204 File Offset: 0x00013404
		private void SetDefaultRetentionValues(Mailbox mailbox)
		{
			bool flag = mailbox.propertyBag.IsReadOnlyProperty(MailboxSchema.RetainDeletedItemsFor);
			bool flag2 = mailbox.propertyBag.IsReadOnlyProperty(MailboxSchema.RetainDeletedItemsUntilBackup);
			if (flag && flag2)
			{
				return;
			}
			MailboxDatabase mailboxDatabase;
			if (this.Database != null)
			{
				mailboxDatabase = (MailboxDatabase)this.scopeObject;
			}
			else
			{
				DatabaseIdParameter databaseIdParam = new DatabaseIdParameter(mailbox.Database);
				string subKey = databaseIdParam.ToString();
				mailboxDatabase = base.ProvisioningCache.TryAddAndGetGlobalDictionaryValue<MailboxDatabase, string>(CannedProvisioningCacheKeys.MailboxDatabaseForDefaultRetentionValuesCacheKey, subKey, () => (MailboxDatabase)this.GetDataObject<MailboxDatabase>(databaseIdParam, this.GlobalConfigSession, null, new LocalizedString?(Strings.ErrorDatabaseNotFound(mailbox.Database.ToString())), new LocalizedString?(Strings.ErrorDatabaseNotUnique(mailbox.Database.ToString()))));
			}
			if (!flag)
			{
				mailbox.RetainDeletedItemsFor = mailboxDatabase.DeletedItemRetention;
			}
			if (!flag2)
			{
				mailbox.RetainDeletedItemsUntilBackup = mailboxDatabase.RetainDeletedItemsUntilBackup;
			}
		}

		// Token: 0x0400011C RID: 284
		internal static readonly QueryFilter RemoteArchiveFilter = new AndFilter(new QueryFilter[]
		{
			new ComparisonFilter(ComparisonOperator.Equal, ADUserSchema.ArchiveStatus, ArchiveStatusFlags.Active),
			new BitMaskAndFilter(ADUserSchema.RemoteRecipientType, 2UL)
		});

		// Token: 0x0400011D RID: 285
		private static readonly QueryFilter SoftDeletedMailboxFilter = new ComparisonFilter(ComparisonOperator.Equal, ADRecipientSchema.IsSoftDeletedByRemove, true);

		// Token: 0x0400011E RID: 286
		private static readonly QueryFilter InactiveMailboxFilter = new BitMaskAndFilter(ADRecipientSchema.RecipientSoftDeletedStatus, 8UL);

		// Token: 0x0400011F RID: 287
		private static readonly QueryFilter ArchiveFilter = new ExistsFilter(ADUserSchema.ArchiveGuid);

		// Token: 0x04000120 RID: 288
		private static readonly QueryFilter AuxMailboxFilter = new BitMaskAndFilter(ADRecipientSchema.ProvisioningFlags, 131072UL);

		// Token: 0x04000121 RID: 289
		private ADObject scopeObject;
	}
}
