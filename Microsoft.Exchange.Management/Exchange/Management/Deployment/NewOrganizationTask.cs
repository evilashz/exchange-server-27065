using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Management.Automation;
using System.Security;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Sync;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Management.Common;
using Microsoft.Exchange.Management.SystemConfigurationTasks;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x02000211 RID: 529
	[ClassAccessLevel(AccessLevel.Consumer)]
	[Cmdlet("New", "Organization", SupportsShouldProcess = true, DefaultParameterSetName = "DatacenterParameterSet")]
	public sealed class NewOrganizationTask : ManageOrganizationTaskBase
	{
		// Token: 0x060011EC RID: 4588 RVA: 0x0004EEE6 File Offset: 0x0004D0E6
		public NewOrganizationTask()
		{
			base.Fields["InstallationMode"] = InstallationModes.Install;
			base.Fields["PrepareOrganization"] = true;
		}

		// Token: 0x1700057E RID: 1406
		// (get) Token: 0x060011ED RID: 4589 RVA: 0x0004EF25 File Offset: 0x0004D125
		private ServicePlanConfiguration ServicePlanConfig
		{
			get
			{
				if (this.servicePlanConfig == null)
				{
					this.servicePlanConfig = ServicePlanConfiguration.GetInstance();
				}
				return this.servicePlanConfig;
			}
		}

		// Token: 0x1700057F RID: 1407
		// (get) Token: 0x060011EE RID: 4590 RVA: 0x0004EF40 File Offset: 0x0004D140
		protected override LocalizedString Description
		{
			get
			{
				return Strings.NewOrganizationDescription;
			}
		}

		// Token: 0x17000580 RID: 1408
		// (get) Token: 0x060011EF RID: 4591 RVA: 0x0004EF47 File Offset: 0x0004D147
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageNewOrganizationNoPath(this.Name, this.DomainName);
			}
		}

		// Token: 0x17000581 RID: 1409
		// (get) Token: 0x060011F0 RID: 4592 RVA: 0x0004EF5A File Offset: 0x0004D15A
		// (set) Token: 0x060011F1 RID: 4593 RVA: 0x0004EF71 File Offset: 0x0004D171
		[Parameter(Mandatory = true, Position = 0, ParameterSetName = "DatacenterParameterSet")]
		[Parameter(Mandatory = false, ParameterSetName = "SharedConfigurationParameterSet")]
		public string Name
		{
			get
			{
				return (string)base.Fields["TenantName"];
			}
			set
			{
				base.Fields["TenantName"] = MailboxTaskHelper.GetNameOfAcceptableLengthForMultiTenantMode(value, out this.nameWarning);
			}
		}

		// Token: 0x17000582 RID: 1410
		// (get) Token: 0x060011F2 RID: 4594 RVA: 0x0004EF8F File Offset: 0x0004D18F
		// (set) Token: 0x060011F3 RID: 4595 RVA: 0x0004EFA6 File Offset: 0x0004D1A6
		[Parameter(Mandatory = true, ParameterSetName = "DatacenterParameterSet")]
		[Parameter(Mandatory = false, ParameterSetName = "SharedConfigurationParameterSet")]
		public SmtpDomain DomainName
		{
			get
			{
				return (SmtpDomain)base.Fields["TenantDomainName"];
			}
			set
			{
				base.Fields["TenantDomainName"] = value;
			}
		}

		// Token: 0x17000583 RID: 1411
		// (get) Token: 0x060011F4 RID: 4596 RVA: 0x0004EFB9 File Offset: 0x0004D1B9
		// (set) Token: 0x060011F5 RID: 4597 RVA: 0x0004EFD0 File Offset: 0x0004D1D0
		[Parameter(Mandatory = false)]
		public WindowsLiveId Administrator
		{
			get
			{
				return (WindowsLiveId)base.Fields["TenantAdministrator"];
			}
			set
			{
				base.Fields["TenantAdministrator"] = value;
			}
		}

		// Token: 0x17000584 RID: 1412
		// (get) Token: 0x060011F6 RID: 4598 RVA: 0x0004EFE3 File Offset: 0x0004D1E3
		// (set) Token: 0x060011F7 RID: 4599 RVA: 0x0004EFFA File Offset: 0x0004D1FA
		[Parameter(Mandatory = false)]
		public NetID AdministratorNetID
		{
			get
			{
				return (NetID)base.Fields["TenantAdministratorNetID"];
			}
			set
			{
				base.Fields["TenantAdministratorNetID"] = value;
			}
		}

		// Token: 0x17000585 RID: 1413
		// (get) Token: 0x060011F8 RID: 4600 RVA: 0x0004F00D File Offset: 0x0004D20D
		// (set) Token: 0x060011F9 RID: 4601 RVA: 0x0004F015 File Offset: 0x0004D215
		[Parameter(Mandatory = false)]
		public SecureString AdministratorPassword
		{
			get
			{
				return this.tenantAdministratorPassword;
			}
			set
			{
				this.tenantAdministratorPassword = value;
			}
		}

		// Token: 0x17000586 RID: 1414
		// (get) Token: 0x060011FA RID: 4602 RVA: 0x0004F01E File Offset: 0x0004D21E
		// (set) Token: 0x060011FB RID: 4603 RVA: 0x0004F035 File Offset: 0x0004D235
		[Parameter(Mandatory = true)]
		public string ProgramId
		{
			get
			{
				return (string)base.Fields["TenantProgramId"];
			}
			set
			{
				base.Fields["TenantProgramId"] = value;
			}
		}

		// Token: 0x17000587 RID: 1415
		// (get) Token: 0x060011FC RID: 4604 RVA: 0x0004F048 File Offset: 0x0004D248
		// (set) Token: 0x060011FD RID: 4605 RVA: 0x0004F05F File Offset: 0x0004D25F
		[Parameter(Mandatory = true)]
		public string OfferId
		{
			get
			{
				return (string)base.Fields["TenantOfferId"];
			}
			set
			{
				base.Fields["TenantOfferId"] = value;
			}
		}

		// Token: 0x17000588 RID: 1416
		// (get) Token: 0x060011FE RID: 4606 RVA: 0x0004F072 File Offset: 0x0004D272
		// (set) Token: 0x060011FF RID: 4607 RVA: 0x0004F089 File Offset: 0x0004D289
		[Parameter(Mandatory = false)]
		public string Location
		{
			get
			{
				return (string)base.Fields["TenantLocation"];
			}
			set
			{
				base.Fields["TenantLocation"] = value;
			}
		}

		// Token: 0x17000589 RID: 1417
		// (get) Token: 0x06001200 RID: 4608 RVA: 0x0004F09C File Offset: 0x0004D29C
		// (set) Token: 0x06001201 RID: 4609 RVA: 0x0004F0B3 File Offset: 0x0004D2B3
		[Parameter(Mandatory = false)]
		public Guid ExternalDirectoryOrganizationId
		{
			get
			{
				return (Guid)base.Fields["TenantExternalDirectoryOrganizationId"];
			}
			set
			{
				base.Fields["TenantExternalDirectoryOrganizationId"] = value;
			}
		}

		// Token: 0x1700058A RID: 1418
		// (get) Token: 0x06001202 RID: 4610 RVA: 0x0004F0CB File Offset: 0x0004D2CB
		// (set) Token: 0x06001203 RID: 4611 RVA: 0x0004F0EC File Offset: 0x0004D2EC
		[Parameter(Mandatory = false)]
		public bool IsDirSyncRunning
		{
			get
			{
				return (bool)(base.Fields["TenantIsDirSyncRunning"] ?? false);
			}
			set
			{
				base.Fields["TenantIsDirSyncRunning"] = value;
			}
		}

		// Token: 0x1700058B RID: 1419
		// (get) Token: 0x06001204 RID: 4612 RVA: 0x0004F104 File Offset: 0x0004D304
		// (set) Token: 0x06001205 RID: 4613 RVA: 0x0004F11B File Offset: 0x0004D31B
		[Parameter(Mandatory = false)]
		public string DirSyncStatus
		{
			get
			{
				return (string)base.Fields["TenantDirSyncStatus"];
			}
			set
			{
				base.Fields["TenantDirSyncStatus"] = value;
			}
		}

		// Token: 0x1700058C RID: 1420
		// (get) Token: 0x06001206 RID: 4614 RVA: 0x0004F12E File Offset: 0x0004D32E
		// (set) Token: 0x06001207 RID: 4615 RVA: 0x0004F145 File Offset: 0x0004D345
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<string> CompanyTags
		{
			get
			{
				return (MultiValuedProperty<string>)base.Fields["TenantCompanyTags"];
			}
			set
			{
				base.Fields["TenantCompanyTags"] = value;
			}
		}

		// Token: 0x1700058D RID: 1421
		// (get) Token: 0x06001208 RID: 4616 RVA: 0x0004F158 File Offset: 0x0004D358
		// (set) Token: 0x06001209 RID: 4617 RVA: 0x0004F16F File Offset: 0x0004D36F
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<Capability> PersistedCapabilities
		{
			get
			{
				return (MultiValuedProperty<Capability>)base.Fields["TenantPersistedCapabilities"];
			}
			set
			{
				base.Fields["TenantPersistedCapabilities"] = value;
			}
		}

		// Token: 0x1700058E RID: 1422
		// (get) Token: 0x0600120A RID: 4618 RVA: 0x0004F182 File Offset: 0x0004D382
		// (set) Token: 0x0600120B RID: 4619 RVA: 0x0004F18A File Offset: 0x0004D38A
		[Parameter(Mandatory = false)]
		public bool OutBoundOnly { get; set; }

		// Token: 0x1700058F RID: 1423
		// (get) Token: 0x0600120C RID: 4620 RVA: 0x0004F193 File Offset: 0x0004D393
		// (set) Token: 0x0600120D RID: 4621 RVA: 0x0004F1B4 File Offset: 0x0004D3B4
		[Parameter(Mandatory = false)]
		public AuthenticationType AuthenticationType
		{
			get
			{
				return (AuthenticationType)(base.Fields["AuthenticationType"] ?? AuthenticationType.Managed);
			}
			set
			{
				base.Fields["AuthenticationType"] = value;
			}
		}

		// Token: 0x17000590 RID: 1424
		// (get) Token: 0x0600120E RID: 4622 RVA: 0x0004F1CC File Offset: 0x0004D3CC
		// (set) Token: 0x0600120F RID: 4623 RVA: 0x0004F1ED File Offset: 0x0004D3ED
		[Parameter(Mandatory = false)]
		public LiveIdInstanceType LiveIdInstanceType
		{
			get
			{
				return (LiveIdInstanceType)(base.Fields["LiveIdInstanceType"] ?? LiveIdInstanceType.Consumer);
			}
			set
			{
				base.Fields["LiveIdInstanceType"] = value;
			}
		}

		// Token: 0x17000591 RID: 1425
		// (get) Token: 0x06001210 RID: 4624 RVA: 0x0004F205 File Offset: 0x0004D405
		// (set) Token: 0x06001211 RID: 4625 RVA: 0x0004F22B File Offset: 0x0004D42B
		[Parameter(Mandatory = false, ParameterSetName = "DatacenterParameterSet")]
		public SwitchParameter HotmailMigration
		{
			get
			{
				return (SwitchParameter)(base.Fields["HotmailMigration"] ?? false);
			}
			set
			{
				base.Fields["HotmailMigration"] = value;
			}
		}

		// Token: 0x17000592 RID: 1426
		// (get) Token: 0x06001212 RID: 4626 RVA: 0x0004F243 File Offset: 0x0004D443
		// (set) Token: 0x06001213 RID: 4627 RVA: 0x0004F25A File Offset: 0x0004D45A
		[Parameter(Mandatory = false)]
		public string DirSyncServiceInstance
		{
			get
			{
				return (string)base.Fields["TenantDirSyncServiceInstance"];
			}
			set
			{
				base.Fields["TenantDirSyncServiceInstance"] = value;
			}
		}

		// Token: 0x17000593 RID: 1427
		// (get) Token: 0x06001214 RID: 4628 RVA: 0x0004F26D File Offset: 0x0004D46D
		// (set) Token: 0x06001215 RID: 4629 RVA: 0x0004F293 File Offset: 0x0004D493
		[Parameter(Mandatory = true, ParameterSetName = "SharedConfigurationParameterSet")]
		public SwitchParameter CreateSharedConfiguration
		{
			get
			{
				return (SwitchParameter)(base.Fields[ManageOrganizationTaskBase.ParameterCreateSharedConfig] ?? false);
			}
			set
			{
				base.Fields[ManageOrganizationTaskBase.ParameterCreateSharedConfig] = value;
			}
		}

		// Token: 0x17000594 RID: 1428
		// (get) Token: 0x06001216 RID: 4630 RVA: 0x0004F2AB File Offset: 0x0004D4AB
		// (set) Token: 0x06001217 RID: 4631 RVA: 0x0004F2C2 File Offset: 0x0004D4C2
		[Parameter(Mandatory = false)]
		public AccountPartitionIdParameter AccountPartition
		{
			get
			{
				return (AccountPartitionIdParameter)base.Fields["AccountPartition"];
			}
			set
			{
				base.Fields["AccountPartition"] = value;
			}
		}

		// Token: 0x17000595 RID: 1429
		// (get) Token: 0x06001218 RID: 4632 RVA: 0x0004F2D5 File Offset: 0x0004D4D5
		// (set) Token: 0x06001219 RID: 4633 RVA: 0x0004F2EC File Offset: 0x0004D4EC
		[Parameter(Mandatory = false)]
		public byte[] RMSOnlineConfig
		{
			get
			{
				return (byte[])base.Fields[NewOrganizationTask.ParameterRMSOnlineConfig];
			}
			set
			{
				base.Fields[NewOrganizationTask.ParameterRMSOnlineConfig] = value;
			}
		}

		// Token: 0x17000596 RID: 1430
		// (get) Token: 0x0600121A RID: 4634 RVA: 0x0004F2FF File Offset: 0x0004D4FF
		// (set) Token: 0x0600121B RID: 4635 RVA: 0x0004F316 File Offset: 0x0004D516
		[Parameter(Mandatory = false)]
		public Hashtable RMSOnlineKeys
		{
			get
			{
				return (Hashtable)base.Fields[NewOrganizationTask.ParameterRMSOnlineKeys];
			}
			set
			{
				base.Fields[NewOrganizationTask.ParameterRMSOnlineKeys] = value;
			}
		}

		// Token: 0x17000597 RID: 1431
		// (get) Token: 0x0600121C RID: 4636 RVA: 0x0004F329 File Offset: 0x0004D529
		// (set) Token: 0x0600121D RID: 4637 RVA: 0x0004F330 File Offset: 0x0004D530
		private new Fqdn DomainController
		{
			get
			{
				throw new NotSupportedException();
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x0600121E RID: 4638 RVA: 0x0004F338 File Offset: 0x0004D538
		private ITenantConfigurationSession WritableAllTenantsSessionForPartition(PartitionId partitionId)
		{
			return (partitionId == this.partition) ? this.WritableAllTenantsSession : DirectorySessionFactory.Default.CreateTenantConfigurationSession(false, ConsistencyMode.PartiallyConsistent, ADSessionSettings.FromAllTenantsPartitionId(partitionId), 469, "WritableAllTenantsSessionForPartition", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\Deployment\\NewOrganizationTask.cs");
		}

		// Token: 0x17000598 RID: 1432
		// (get) Token: 0x0600121F RID: 4639 RVA: 0x0004F380 File Offset: 0x0004D580
		private ITenantConfigurationSession WritableAllTenantsSession
		{
			get
			{
				if (this.writableAllTenantsSession == null)
				{
					this.writableAllTenantsSession = DirectorySessionFactory.Default.CreateTenantConfigurationSession((base.ServerSettings == null) ? null : base.ServerSettings.PreferredGlobalCatalog(this.partition.ForestFQDN), false, ConsistencyMode.PartiallyConsistent, ADSessionSettings.FromAllTenantsPartitionId(this.partition), 485, "WritableAllTenantsSession", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\Deployment\\NewOrganizationTask.cs");
				}
				return this.writableAllTenantsSession;
			}
		}

		// Token: 0x06001220 RID: 4640 RVA: 0x0004F3ED File Offset: 0x0004D5ED
		protected override ITaskModuleFactory CreateTaskModuleFactory()
		{
			return new NewOrganizationTaskModuleFactory();
		}

		// Token: 0x06001221 RID: 4641 RVA: 0x0004F3F4 File Offset: 0x0004D5F4
		private static PartitionId ChoosePartition(string name, bool createSharedConfiguration, Task.TaskErrorLoggingDelegate errorLogger)
		{
			PartitionId[] allAccountPartitionIdsEnabledForProvisioning = ADAccountPartitionLocator.GetAllAccountPartitionIdsEnabledForProvisioning();
			if (createSharedConfiguration)
			{
				if (allAccountPartitionIdsEnabledForProvisioning.Length == 1)
				{
					return allAccountPartitionIdsEnabledForProvisioning[0];
				}
				if (string.IsNullOrEmpty(name))
				{
					return PartitionId.LocalForest;
				}
				errorLogger(new ArgumentException(Strings.ErrorAccountPartitionRequired), ErrorCategory.InvalidArgument, null);
			}
			return ADAccountPartitionLocator.SelectAccountPartitionForNewTenant(name);
		}

		// Token: 0x06001222 RID: 4642 RVA: 0x0004F440 File Offset: 0x0004D640
		private static PartitionId ResolvePartitionId(AccountPartitionIdParameter accountPartitionIdParameter, Task.TaskErrorLoggingDelegate errorLogger)
		{
			PartitionId result = null;
			LocalizedString? localizedString;
			IEnumerable<AccountPartition> objects = accountPartitionIdParameter.GetObjects<AccountPartition>(null, DirectorySessionFactory.Default.CreateTopologyConfigurationSession(ConsistencyMode.PartiallyConsistent, ADSessionSettings.SessionSettingsFactory.Default.FromRootOrgScopeSet(), 548, "ResolvePartitionId", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\Deployment\\NewOrganizationTask.cs"), null, out localizedString);
			Exception ex = null;
			using (IEnumerator<AccountPartition> enumerator = objects.GetEnumerator())
			{
				if (enumerator.MoveNext())
				{
					AccountPartition accountPartition = enumerator.Current;
					if (!accountPartition.TryGetPartitionId(out result))
					{
						ex = new NotSupportedException(Strings.ErrorCorruptedPartition(accountPartitionIdParameter.ToString()));
					}
					else if (enumerator.MoveNext())
					{
						ex = new ManagementObjectAmbiguousException(Strings.ErrorObjectNotUnique(accountPartitionIdParameter.ToString()));
					}
					if (accountPartition.IsSecondary)
					{
						ex = new ArgumentException(Strings.ErrorSecondaryPartitionNotEnabledForProvisioning(accountPartitionIdParameter.RawIdentity));
					}
				}
				else
				{
					ex = new ManagementObjectNotFoundException(localizedString ?? Strings.ErrorObjectNotFound(accountPartitionIdParameter.ToString()));
				}
			}
			if (ex != null)
			{
				errorLogger(ex, ErrorCategory.InvalidArgument, accountPartitionIdParameter);
			}
			return result;
		}

		// Token: 0x06001223 RID: 4643 RVA: 0x0004F54C File Offset: 0x0004D74C
		private void WriteWrappedError(Exception exception, ErrorCategory category, object target)
		{
			OrganizationValidationException exception2 = new OrganizationValidationException(Strings.ErrorValidationException(exception.ToString()), exception);
			base.WriteError(exception2, category, target);
		}

		// Token: 0x06001224 RID: 4644 RVA: 0x0004F574 File Offset: 0x0004D774
		protected override void InternalBeginProcessing()
		{
			TaskLogger.LogEnter();
			if (this.nameWarning != LocalizedString.Empty)
			{
				this.WriteWarning(this.nameWarning);
			}
			base.InternalBeginProcessing();
			if (this.Administrator != null)
			{
				OrganizationTaskHelper.ValidateParamString("Administrator", this.Administrator.ToString(), new Task.TaskErrorLoggingDelegate(base.WriteError));
			}
			if (this.AdministratorNetID != null)
			{
				OrganizationTaskHelper.ValidateParamString("AdministratorNetID", this.AdministratorNetID.ToString(), new Task.TaskErrorLoggingDelegate(base.WriteError));
			}
			if (this.AdministratorNetID != null && this.Administrator != null && this.Administrator.NetId != null && !this.AdministratorNetID.Equals(this.Administrator.NetId))
			{
				base.WriteError(new RecipientTaskException(Strings.ErrorNetIDValuesDoNotMatch(this.AdministratorNetID.ToString(), this.Administrator.NetId.ToString())), ErrorCategory.InvalidArgument, null);
			}
			if (this.AdministratorNetID != null && this.Administrator == null)
			{
				this.Administrator = new WindowsLiveId(this.AdministratorNetID.ToString());
			}
			if (base.Fields.IsModified("TenantDirSyncServiceInstance") && !string.IsNullOrEmpty(this.DirSyncServiceInstance) && !ServiceInstanceId.IsValidServiceInstanceId(this.DirSyncServiceInstance))
			{
				base.WriteError(new InvalidServiceInstanceIdException(this.DirSyncServiceInstance), ExchangeErrorCategory.Client, null);
			}
			if (this.ServicePlanConfig.IsPilotOffer(this.ProgramId, this.OfferId) && !this.CreateSharedConfiguration)
			{
				base.WriteError(new ArgumentException(Strings.ErrorPilotServicePlanCanBeUsedToCreateSharedOrgsOnly(this.ProgramId, this.OfferId)), (ErrorCategory)1000, null);
			}
			Exception ex = null;
			string text = null;
			if (base.Fields["TenantExternalDirectoryOrganizationId"] == null && !this.CreateSharedConfiguration)
			{
				base.Fields["TenantExternalDirectoryOrganizationId"] = Guid.NewGuid();
			}
			try
			{
				bool flag = this.ServicePlanConfig.TryGetHydratedOfferId(this.ProgramId, this.OfferId, out text);
				if (!this.CreateSharedConfiguration && this.Name == null)
				{
					base.WriteError(new ArgumentException(Strings.ErrorNameNotSet), (ErrorCategory)1000, null);
				}
				this.partition = ((this.AccountPartition != null) ? NewOrganizationTask.ResolvePartitionId(this.AccountPartition, new Task.TaskErrorLoggingDelegate(base.WriteError)) : NewOrganizationTask.ChoosePartition(this.Name, this.CreateSharedConfiguration, new Task.TaskErrorLoggingDelegate(base.WriteError)));
				if (this.CreateSharedConfiguration && flag)
				{
					this.OfferId = text;
					this.shouldCreateSCT = NewOrganizationTask.ShouldCreateSharedConfiguration(this.ProgramId, this.OfferId, this.partition, out this.sctConfigUnit);
				}
				string text2 = this.ServicePlanConfig.ResolveServicePlanName(this.ProgramId, this.OfferId);
				this.servicePlanSettings = this.ServicePlanConfig.GetServicePlanSettings(text2);
				bool flag2 = this.ServicePlanConfig.IsTemplateTenantServicePlan(this.servicePlanSettings);
				if (flag2)
				{
					this.shouldCreateSCT = NewOrganizationTask.ShouldCreateTenantTemplate(this.ProgramId, this.OfferId, this.partition, out this.sctConfigUnit);
				}
				if (this.CreateSharedConfiguration)
				{
					if (!this.shouldCreateSCT)
					{
						this.WriteWarning(Strings.WarningSharedConfigurationAlreadyExists(this.ProgramId, this.OfferId));
						return;
					}
					if (string.IsNullOrEmpty(this.Name))
					{
						this.Name = (flag2 ? TemplateTenantConfiguration.CreateSharedConfigurationName(this.ProgramId, this.OfferId) : SharedConfiguration.CreateSharedConfigurationName(this.ProgramId, this.OfferId));
					}
					if (this.DomainName == null)
					{
						this.DomainName = (flag2 ? TemplateTenantConfiguration.CreateSharedConfigurationDomainName(this.Name) : SharedConfiguration.CreateSharedConfigurationDomainName(this.Name));
					}
				}
				OrganizationTaskHelper.ValidateParamString("Name", this.Name, new Task.TaskErrorLoggingDelegate(base.WriteError), true);
				ADOrganizationalUnit adorganizationalUnit = new ADOrganizationalUnit();
				adorganizationalUnit[ADObjectSchema.Name] = this.Name;
				base.InternalIsSharedConfigServicePlan = this.ServicePlanConfig.IsSharedConfigurationAllowedForServicePlan(this.servicePlanSettings);
				if (this.CreateSharedConfiguration && !base.InternalIsSharedConfigServicePlan && !this.ServicePlanConfig.IsHydratedOffer(text2))
				{
					base.WriteError(new SharedConfigurationValidationException(Strings.ErrorServicePlanDoesntAllowSharedConfiguration(this.ProgramId, this.OfferId)), (ErrorCategory)1000, null);
				}
				else if (!flag && base.InternalIsSharedConfigServicePlan)
				{
					text = this.OfferId;
				}
				if (this.CreateSharedConfiguration)
				{
					base.InternalCreateSharedConfiguration = true;
				}
				else if (!this.CreateSharedConfiguration && base.InternalIsSharedConfigServicePlan)
				{
					SharedConfigurationInfo sharedConfigurationInfo = SharedConfigurationInfo.FromInstalledVersion(this.ProgramId, text);
					base.InternalSharedConfigurationId = SharedConfiguration.FindOneSharedConfigurationId(sharedConfigurationInfo, this.partition);
					if (base.InternalSharedConfigurationId == null)
					{
						base.WriteError(new SharedConfigurationValidationException(Strings.ErrorSharedConfigurationNotFound(this.ProgramId, text, sharedConfigurationInfo.CurrentVersion.ToString())), (ErrorCategory)1000, null);
					}
					base.InternalCreateSharedConfiguration = false;
				}
				List<ValidationError> list = new List<ValidationError>();
				list.AddRange(ServicePlan.ValidateFileSchema(text2));
				list.AddRange(this.servicePlanSettings.Validate());
				if (list.Count != 0)
				{
					ex = new ArgumentException(Strings.ErrorServicePlanInconsistent(text2, this.ProgramId, this.OfferId, ValidationError.CombineErrorDescriptions(list)));
				}
			}
			catch (ArgumentException ex2)
			{
				ex = ex2;
			}
			catch (IOException ex3)
			{
				ex = ex3;
			}
			if (ex != null)
			{
				base.WriteError(ex, ErrorCategory.InvalidArgument, null);
			}
			base.InternalLocalStaticConfigEnabled = (!this.servicePlanSettings.Organization.AdvancedHydrateableObjectsSharedEnabled || this.CreateSharedConfiguration);
			base.InternalLocalHydrateableConfigEnabled = (!this.servicePlanSettings.Organization.CommonHydrateableObjectsSharedEnabled || this.CreateSharedConfiguration);
			TaskLogger.LogExit();
		}

		// Token: 0x06001225 RID: 4645 RVA: 0x0004FB60 File Offset: 0x0004DD60
		protected override void SetRunspaceVariables()
		{
			base.SetRunspaceVariables();
			if (this.servicePlanSettings != null)
			{
				this.monadConnection.RunspaceProxy.SetVariable(NewOrganizationTask.ServicePlanSettingsVarName, this.servicePlanSettings);
			}
			if (this.tenantAdministratorPassword != null)
			{
				this.monadConnection.RunspaceProxy.SetVariable(NewOrganizationTask.TenantAdministratorPasswordVarName, this.tenantAdministratorPassword);
			}
			this.monadConnection.RunspaceProxy.SetVariable(NewOrganizationTask.ParameterRMSOnlineConfig, this.RMSOnlineConfig);
			this.monadConnection.RunspaceProxy.SetVariable(NewOrganizationTask.ParameterRMSOnlineKeys, this.RMSOnlineKeys);
		}

		// Token: 0x06001226 RID: 4646 RVA: 0x0004FBF0 File Offset: 0x0004DDF0
		protected override void PopulateContextVariables()
		{
			base.PopulateContextVariables();
			if (this.servicePlanSettings != null)
			{
				base.Fields[NewOrganizationTask.ServicePlanVarName] = this.servicePlanSettings.Name;
			}
			base.Fields["PreferredServer"] = base.ServerSettings.PreferredGlobalCatalog(this.partition.ForestFQDN);
			base.Fields["OrganizationHierarchicalPath"] = this.Name;
			base.Fields["AuthenticationType"] = this.AuthenticationType;
			base.Fields["LiveIdInstanceType"] = this.LiveIdInstanceType;
			base.Fields["OutBoundOnly"] = this.OutBoundOnly;
			base.Fields["Partition"] = this.partition.PartitionObjectId;
		}

		// Token: 0x06001227 RID: 4647 RVA: 0x0004FCD4 File Offset: 0x0004DED4
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			base.InternalValidate();
			if (this.CreateSharedConfiguration && !this.shouldCreateSCT)
			{
				return;
			}
			if (this.ProgramId.IndexOf(".") != -1 || this.OfferId.IndexOf(".") != -1)
			{
				base.WriteError(new ArgumentException(Strings.ErrorParametersCannotHaveEmbeddedDot), ErrorCategory.InvalidArgument, null);
			}
			this.CheckForDuplicateExistingOrganization();
			NewAcceptedDomain.ValidateDomainName(new AcceptedDomain
			{
				DomainName = new SmtpDomainWithSubdomains(this.DomainName, false),
				DomainType = AcceptedDomainType.Authoritative
			}, new Task.TaskErrorLoggingDelegate(this.WriteWrappedError));
			bool flag = this.ServicePlanConfig.IsTemplateTenantServicePlan(this.servicePlanSettings);
			bool flag2 = TemplateTenantConfiguration.IsTemplateTenantName(this.Name);
			if (flag)
			{
				if (this.partition != PartitionId.LocalForest)
				{
					this.WriteWarning(Strings.ErrorLocalAccountPartitionRequiredForTT);
				}
				if (!this.CreateSharedConfiguration)
				{
					base.WriteError(new ArgumentException(Strings.CreateSharedConfigurationRequiredForTT), ErrorCategory.InvalidArgument, null);
				}
				if (!flag2)
				{
					base.WriteError(new ArgumentException(Strings.CalculatedNameRequiredForTT(TemplateTenantConfiguration.TopLevelDomain)), ErrorCategory.InvalidArgument, null);
				}
			}
			else if (flag2)
			{
				base.WriteError(new ArgumentException(Strings.TTNameWithNonTTServiceplan(TemplateTenantConfiguration.TopLevelDomain)), ErrorCategory.InvalidArgument, null);
			}
			TaskLogger.LogExit();
		}

		// Token: 0x06001228 RID: 4648 RVA: 0x0004FE20 File Offset: 0x0004E020
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			if (this.CreateSharedConfiguration && !this.shouldCreateSCT)
			{
				if (this.sctConfigUnit != null)
				{
					TenantOrganizationPresentationObject sendToPipeline = new TenantOrganizationPresentationObject(this.sctConfigUnit);
					base.WriteObject(sendToPipeline);
				}
				return;
			}
			try
			{
				base.InternalProcessRecord();
			}
			catch (Exception)
			{
				this.CleanupOrganization(this.partition);
				throw;
			}
			bool flag = !base.HasErrors;
			if (flag)
			{
				ITenantConfigurationSession tenantConfigurationSession = this.WritableAllTenantsSession;
				tenantConfigurationSession.UseConfigNC = false;
				ADObjectId childId = tenantConfigurationSession.GetHostedOrganizationsRoot().GetChildId("OU", this.Name);
				ADOrganizationalUnit adorganizationalUnit = tenantConfigurationSession.Read<ADOrganizationalUnit>(childId);
				tenantConfigurationSession.UseConfigNC = true;
				ExchangeConfigurationUnit dataObject = tenantConfigurationSession.Read<ExchangeConfigurationUnit>(adorganizationalUnit.ConfigurationUnit);
				TenantOrganizationPresentationObject sendToPipeline2 = new TenantOrganizationPresentationObject(dataObject);
				base.WriteObject(sendToPipeline2);
			}
			else
			{
				this.CleanupOrganization(this.partition);
			}
			TaskLogger.LogExit();
		}

		// Token: 0x06001229 RID: 4649 RVA: 0x0004FF00 File Offset: 0x0004E100
		private void CleanupOrganization(PartitionId partitionId)
		{
			ITenantConfigurationSession tenantConfigurationSession = this.WritableAllTenantsSessionForPartition(partitionId);
			ADObjectId childId = tenantConfigurationSession.GetHostedOrganizationsRoot().GetChildId("OU", this.Name);
			ADOrganizationalUnit adorganizationalUnit = this.ReadADOrganizationalUnit(tenantConfigurationSession, childId);
			Container objectToDelete = null;
			if (adorganizationalUnit != null && adorganizationalUnit.ConfigurationUnit != null)
			{
				tenantConfigurationSession.UseConfigNC = true;
				ExchangeConfigurationUnit exchangeConfigurationUnit = tenantConfigurationSession.Read<ExchangeConfigurationUnit>(adorganizationalUnit.ConfigurationUnit);
				if (exchangeConfigurationUnit != null)
				{
					if (ExchangeConfigurationUnit.IsOrganizationActive(exchangeConfigurationUnit.OrganizationStatus))
					{
						base.WriteError(new OrganizationExistsException(Strings.ErrorDuplicateActiveOrganizationExists(this.Name)), ErrorCategory.InvalidArgument, null);
					}
					try
					{
						exchangeConfigurationUnit.ExternalDirectoryOrganizationId = string.Empty;
						tenantConfigurationSession.Save(exchangeConfigurationUnit);
					}
					catch (Exception)
					{
					}
					objectToDelete = this.ReadContainer(tenantConfigurationSession, adorganizationalUnit.ConfigurationUnit.Parent);
				}
			}
			try
			{
				this.DeleteTree(tenantConfigurationSession, adorganizationalUnit, false);
			}
			catch (Exception)
			{
			}
			try
			{
				this.DeleteTree(tenantConfigurationSession, objectToDelete, true);
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x0600122A RID: 4650 RVA: 0x0004FFF4 File Offset: 0x0004E1F4
		private static bool ShouldCreateSharedConfiguration(string programId, string offerId, PartitionId partitionId, out ExchangeConfigurationUnit sctConfigUnit)
		{
			sctConfigUnit = null;
			ADDriverContext processADContext = ADSessionSettings.GetProcessADContext();
			if (processADContext == null || processADContext.Mode != ContextMode.Setup)
			{
				return true;
			}
			SharedConfigurationInfo sci = SharedConfigurationInfo.FromInstalledVersion(programId, offerId);
			sctConfigUnit = SharedConfiguration.FindOneSharedConfiguration(sci, partitionId);
			return sctConfigUnit == null;
		}

		// Token: 0x0600122B RID: 4651 RVA: 0x00050030 File Offset: 0x0004E230
		private static bool ShouldCreateTenantTemplate(string programId, string offerId, PartitionId partitionId, out ExchangeConfigurationUnit sctConfigUnit)
		{
			sctConfigUnit = null;
			ADDriverContext processADContext = ADSessionSettings.GetProcessADContext();
			if (processADContext == null || processADContext.Mode != ContextMode.Setup)
			{
				return true;
			}
			ADPagedReader<ExchangeConfigurationUnit> adpagedReader = TemplateTenantConfiguration.FindAllTempateTenants(programId, offerId, partitionId);
			foreach (ExchangeConfigurationUnit exchangeConfigurationUnit in adpagedReader)
			{
				if (exchangeConfigurationUnit.SharedConfigurationInfo != null && ((IComparable)TemplateTenantConfiguration.RequiredTemplateTenantVersion).CompareTo(exchangeConfigurationUnit.SharedConfigurationInfo.CurrentVersion) <= 0)
				{
					sctConfigUnit = exchangeConfigurationUnit;
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600122C RID: 4652 RVA: 0x000500C8 File Offset: 0x0004E2C8
		private void CheckForDuplicateExistingOrganization()
		{
			ITenantConfigurationSession tenantConfigurationSession = this.WritableAllTenantsSessionForPartition(this.partition);
			ADObjectId childId = tenantConfigurationSession.GetHostedOrganizationsRoot().GetChildId("OU", this.Name);
			ADOrganizationalUnit oufromOrganizationId = OrganizationTaskHelper.GetOUFromOrganizationId(new OrganizationIdParameter(childId), tenantConfigurationSession, null, false);
			if (oufromOrganizationId == null)
			{
				this.CleanupOrganization(this.partition);
				return;
			}
			if (OrganizationTaskHelper.CanProceedWithOrganizationTask(new OrganizationIdParameter(this.Name), tenantConfigurationSession, NewOrganizationTask.ignorableFlagsOnStatusTimeout, new Task.TaskErrorLoggingDelegate(base.WriteError)))
			{
				this.CleanupOrganization(this.partition);
				return;
			}
			base.WriteError(new OrganizationPendingOperationException(Strings.ErrorDuplicateNonActiveOrganizationExists(this.Name)), ErrorCategory.InvalidArgument, null);
		}

		// Token: 0x0600122D RID: 4653 RVA: 0x00050164 File Offset: 0x0004E364
		private void DeleteContainer(ADConfigurationObject container, bool useConfigNC)
		{
			if (container != null)
			{
				PartitionId partitionId = new PartitionId(container.Id.PartitionGuid);
				ITenantConfigurationSession session = this.WritableAllTenantsSessionForPartition(partitionId);
				this.DeleteTree(session, container, useConfigNC);
			}
		}

		// Token: 0x0600122E RID: 4654 RVA: 0x00050198 File Offset: 0x0004E398
		private ADOrganizationalUnit ReadADOrganizationalUnit(ITenantConfigurationSession session, ADObjectId objectId)
		{
			ADOrganizationalUnit result = null;
			bool useConfigNC = session.UseConfigNC;
			session.UseConfigNC = false;
			try
			{
				result = session.Read<ADOrganizationalUnit>(objectId);
			}
			finally
			{
				session.UseConfigNC = useConfigNC;
			}
			return result;
		}

		// Token: 0x0600122F RID: 4655 RVA: 0x000501D8 File Offset: 0x0004E3D8
		private Container ReadContainer(ITenantConfigurationSession session, ADObjectId objectId)
		{
			Container result = null;
			bool useConfigNC = session.UseConfigNC;
			session.UseConfigNC = true;
			try
			{
				result = session.Read<Container>(objectId);
			}
			finally
			{
				session.UseConfigNC = useConfigNC;
			}
			return result;
		}

		// Token: 0x06001230 RID: 4656 RVA: 0x00050218 File Offset: 0x0004E418
		private void DeleteTree(ITenantConfigurationSession session, ADConfigurationObject objectToDelete, bool useConfigNC)
		{
			if (objectToDelete != null)
			{
				bool useConfigNC2 = session.UseConfigNC;
				session.UseConfigNC = useConfigNC;
				try
				{
					session.DeleteTree(objectToDelete, null);
				}
				finally
				{
					session.UseConfigNC = useConfigNC2;
				}
			}
		}

		// Token: 0x040007C1 RID: 1985
		private const string SharedConfigurationParameterSet = "SharedConfigurationParameterSet";

		// Token: 0x040007C2 RID: 1986
		private const string DatacenterParameterSet = "DatacenterParameterSet";

		// Token: 0x040007C3 RID: 1987
		private ITenantConfigurationSession writableAllTenantsSession;

		// Token: 0x040007C4 RID: 1988
		private ServicePlan servicePlanSettings;

		// Token: 0x040007C5 RID: 1989
		private bool shouldCreateSCT;

		// Token: 0x040007C6 RID: 1990
		private ExchangeConfigurationUnit sctConfigUnit;

		// Token: 0x040007C7 RID: 1991
		private ServicePlanConfiguration servicePlanConfig;

		// Token: 0x040007C8 RID: 1992
		internal static readonly string ServicePlanSettingsVarName = "ServicePlanSettings";

		// Token: 0x040007C9 RID: 1993
		internal static readonly string ServicePlanVarName = "TenantServicePlanName";

		// Token: 0x040007CA RID: 1994
		internal static readonly string TenantAdministratorPasswordVarName = "TenantAdministratorPassword";

		// Token: 0x040007CB RID: 1995
		internal static readonly string ParameterRMSOnlineConfig = "RMSOnlineConfig";

		// Token: 0x040007CC RID: 1996
		internal static readonly string ParameterRMSOnlineKeys = "RMSOnlineKeys";

		// Token: 0x040007CD RID: 1997
		private LocalizedString nameWarning = LocalizedString.Empty;

		// Token: 0x040007CE RID: 1998
		private static OrganizationStatus[] ignorableFlagsOnStatusTimeout = new OrganizationStatus[]
		{
			OrganizationStatus.PendingCompletion
		};

		// Token: 0x040007CF RID: 1999
		private SecureString tenantAdministratorPassword;

		// Token: 0x040007D0 RID: 2000
		private PartitionId partition;
	}
}
