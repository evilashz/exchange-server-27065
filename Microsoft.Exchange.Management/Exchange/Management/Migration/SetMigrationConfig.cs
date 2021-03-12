using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.Migration;
using Microsoft.Exchange.Migration.Logging;

namespace Microsoft.Exchange.Management.Migration
{
	// Token: 0x020004ED RID: 1261
	[Cmdlet("Set", "MigrationConfig", SupportsShouldProcess = true, DefaultParameterSetName = "Identity")]
	public sealed class SetMigrationConfig : SetTenantADTaskBase<MigrationConfigIdParameter, MigrationConfig, MigrationConfig>
	{
		// Token: 0x17000D3B RID: 3387
		// (get) Token: 0x06002C96 RID: 11414 RVA: 0x000B2AE4 File Offset: 0x000B0CE4
		// (set) Token: 0x06002C97 RID: 11415 RVA: 0x000B2AEC File Offset: 0x000B0CEC
		[Parameter(Mandatory = false, ParameterSetName = "Identity", ValueFromPipelineByPropertyName = true, ValueFromPipeline = true, Position = 0)]
		public override MigrationConfigIdParameter Identity
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

		// Token: 0x17000D3C RID: 3388
		// (get) Token: 0x06002C98 RID: 11416 RVA: 0x000B2AF5 File Offset: 0x000B0CF5
		// (set) Token: 0x06002C99 RID: 11417 RVA: 0x000B2B0C File Offset: 0x000B0D0C
		[Parameter(Mandatory = false)]
		public MailboxIdParameter Partition
		{
			get
			{
				return (MailboxIdParameter)base.Fields["Partition"];
			}
			set
			{
				base.Fields["Partition"] = value;
			}
		}

		// Token: 0x17000D3D RID: 3389
		// (get) Token: 0x06002C9A RID: 11418 RVA: 0x000B2B1F File Offset: 0x000B0D1F
		// (set) Token: 0x06002C9B RID: 11419 RVA: 0x000B2B36 File Offset: 0x000B0D36
		[Parameter(Mandatory = false)]
		public Unlimited<int> MaxConcurrentMigrations
		{
			get
			{
				return (Unlimited<int>)base.Fields["MaxConcurrentMigrations"];
			}
			set
			{
				base.Fields["MaxConcurrentMigrations"] = value;
			}
		}

		// Token: 0x17000D3E RID: 3390
		// (get) Token: 0x06002C9C RID: 11420 RVA: 0x000B2B4E File Offset: 0x000B0D4E
		// (set) Token: 0x06002C9D RID: 11421 RVA: 0x000B2B65 File Offset: 0x000B0D65
		[Parameter(Mandatory = false)]
		[ValidateRange(0, 2147483647)]
		public int MaxNumberOfBatches
		{
			get
			{
				return (int)base.Fields["MaxNumberOfBatches"];
			}
			set
			{
				base.Fields["MaxNumberOfBatches"] = value;
			}
		}

		// Token: 0x17000D3F RID: 3391
		// (get) Token: 0x06002C9E RID: 11422 RVA: 0x000B2B7D File Offset: 0x000B0D7D
		// (set) Token: 0x06002C9F RID: 11423 RVA: 0x000B2B94 File Offset: 0x000B0D94
		[Parameter(Mandatory = false)]
		public MigrationFeature Features
		{
			get
			{
				return (MigrationFeature)base.Fields["MigrationFeature"];
			}
			set
			{
				base.Fields["MigrationFeature"] = value;
			}
		}

		// Token: 0x06002CA0 RID: 11424 RVA: 0x000B2BAC File Offset: 0x000B0DAC
		protected override bool IsKnownException(Exception exception)
		{
			return MigrationSessionDataProvider.IsKnownException(exception) || base.IsKnownException(exception);
		}

		// Token: 0x06002CA1 RID: 11425 RVA: 0x000B2BC0 File Offset: 0x000B0DC0
		protected override IConfigDataProvider CreateSession()
		{
			base.CurrentOrganizationId = this.ResolveCurrentOrganization();
			this.partitionMailbox = MigrationObjectTaskBase<MigrationConfigIdParameter>.ResolvePartitionMailbox(this.Partition, base.TenantGlobalCatalogSession, base.ServerSettings, new DataAccessHelper.CategorizedGetDataObjectDelegate(base.GetDataObject<ADUser>), new Task.ErrorLoggerDelegate(base.WriteError), base.CurrentOrganizationId == OrganizationId.ForestWideOrgId && MapiTaskHelper.IsDatacenter);
			TenantPartitionHint partitionHint = TenantPartitionHint.FromOrganizationId(base.CurrentOrganizationId);
			MigrationLogger.Initialize();
			MigrationLogContext.Current.Source = "Set-MigrationConfig";
			MigrationLogContext.Current.Organization = base.CurrentOrganizationId.OrganizationalUnit;
			return MigrationSessionDataProvider.CreateDataProvider("SetMigrationConfig", MigrationHelperBase.CreateRecipientSession(partitionHint), this.partitionMailbox);
		}

		// Token: 0x06002CA2 RID: 11426 RVA: 0x000B2C74 File Offset: 0x000B0E74
		protected override void InternalValidate()
		{
			MigrationSessionDataProvider migrationSessionDataProvider = (MigrationSessionDataProvider)base.DataSession;
			if (this.IsFieldSet("MigrationFeature"))
			{
				migrationSessionDataProvider.MigrationSession.CheckFeaturesAvailableToUpgrade(this.Features);
			}
			if (this.IsFieldSet("MaxConcurrentMigrations"))
			{
				ValidationError validationError = MigrationConstraints.MaxConcurrentMigrationsConstraint.Validate(this.MaxConcurrentMigrations, MigrationBatchMessageSchema.MigrationJobMaxConcurrentMigrations, null);
				if (validationError != null)
				{
					this.WriteError(new MigrationMaxConcurrentConnectionsVerificationFailedException(this.MaxConcurrentMigrations.Value.ToString(), MigrationConstraints.MaxConcurrentMigrationsConstraint.MinimumValue.ToString(), MigrationConstraints.MaxConcurrentMigrationsConstraint.MaximumValue.ToString()));
				}
			}
			base.InternalValidate();
		}

		// Token: 0x17000D40 RID: 3392
		// (get) Token: 0x06002CA3 RID: 11427 RVA: 0x000B2D30 File Offset: 0x000B0F30
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageSetMigrationConfig(this.Identity.ToString());
			}
		}

		// Token: 0x06002CA4 RID: 11428 RVA: 0x000B2D42 File Offset: 0x000B0F42
		protected override bool IsObjectStateChanged()
		{
			return this.changed;
		}

		// Token: 0x06002CA5 RID: 11429 RVA: 0x000B2D4C File Offset: 0x000B0F4C
		protected override IConfigurable PrepareDataObject()
		{
			MigrationSessionDataProvider migrationSessionDataProvider = (MigrationSessionDataProvider)base.DataSession;
			return migrationSessionDataProvider.MigrationSession.GetMigrationConfig(migrationSessionDataProvider.MailboxProvider);
		}

		// Token: 0x06002CA6 RID: 11430 RVA: 0x000B2D76 File Offset: 0x000B0F76
		protected override void InternalStateReset()
		{
			this.changed = false;
			this.DisposeSession();
			base.InternalStateReset();
		}

		// Token: 0x06002CA7 RID: 11431 RVA: 0x000B2D8C File Offset: 0x000B0F8C
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (!this.disposed)
				{
					if (disposing)
					{
						this.DisposeSession();
					}
					this.disposed = true;
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		// Token: 0x06002CA8 RID: 11432 RVA: 0x000B2DCC File Offset: 0x000B0FCC
		protected override void InternalProcessRecord()
		{
			MigrationSessionDataProvider migrationSessionDataProvider = (MigrationSessionDataProvider)base.DataSession;
			IMigrationDataProvider mailboxProvider = migrationSessionDataProvider.MailboxProvider;
			if (this.IsFieldSet("MigrationFeature") && migrationSessionDataProvider.MigrationSession.EnableFeatures(mailboxProvider, this.Features))
			{
				this.changed = true;
			}
			MigrationConfig migrationConfig = migrationSessionDataProvider.MigrationSession.GetMigrationConfig(mailboxProvider);
			if (this.IsFieldSet("MaxNumberOfBatches") && this.MaxNumberOfBatches != migrationConfig.MaxNumberOfBatches)
			{
				migrationConfig.MaxNumberOfBatches = this.MaxNumberOfBatches;
				this.changed = true;
			}
			if (this.IsFieldSet("MaxConcurrentMigrations") && this.MaxConcurrentMigrations != migrationConfig.MaxConcurrentMigrations)
			{
				migrationConfig.MaxConcurrentMigrations = this.MaxConcurrentMigrations;
				this.changed = true;
			}
			if (this.changed)
			{
				migrationSessionDataProvider.MigrationSession.SetMigrationConfig(mailboxProvider, migrationConfig);
			}
			base.InternalProcessRecord();
		}

		// Token: 0x06002CA9 RID: 11433 RVA: 0x000B2E9D File Offset: 0x000B109D
		private bool IsFieldSet(string fieldName)
		{
			return base.Fields.IsChanged(fieldName) || base.Fields.IsModified(fieldName);
		}

		// Token: 0x06002CAA RID: 11434 RVA: 0x000B2EBC File Offset: 0x000B10BC
		private OrganizationId ResolveCurrentOrganization()
		{
			if (this.Identity == null)
			{
				this.Identity = new MigrationConfigIdParameter();
				return OrganizationId.ForestWideOrgId;
			}
			if (this.Identity.Id != null)
			{
				return this.Identity.Id.OrganizationId;
			}
			ADObjectId rootOrgContainerIdForLocalForest = ADSystemConfigurationSession.GetRootOrgContainerIdForLocalForest();
			ADSessionSettings sessionSettings = ADSessionSettings.FromOrganizationIdWithoutRbacScopes(rootOrgContainerIdForLocalForest, base.CurrentOrganizationId, base.ExecutingUserOrganizationId, true);
			IConfigurationSession tenantOrTopologyConfigurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(base.DomainController, true, ConsistencyMode.PartiallyConsistent, null, sessionSettings, 386, "ResolveCurrentOrganization", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\Migration\\SetMigrationConfig.cs");
			tenantOrTopologyConfigurationSession.UseConfigNC = false;
			ADOrganizationalUnit adorganizationalUnit = (ADOrganizationalUnit)base.GetDataObject<ADOrganizationalUnit>(this.Identity.OrganizationIdentifier, tenantOrTopologyConfigurationSession, null, new LocalizedString?(Strings.ErrorOrganizationNotFound(this.Identity.OrganizationIdentifier.ToString())), new LocalizedString?(Strings.ErrorOrganizationNotUnique(this.Identity.OrganizationIdentifier.ToString())));
			OrganizationId organizationId = adorganizationalUnit.OrganizationId;
			this.Identity.Id = new MigrationConfigId(organizationId);
			return organizationId;
		}

		// Token: 0x06002CAB RID: 11435 RVA: 0x000B2FB4 File Offset: 0x000B11B4
		private void DisposeSession()
		{
			IDisposable disposable = base.DataSession as IDisposable;
			if (disposable != null)
			{
				MigrationLogger.Close();
				disposable.Dispose();
			}
		}

		// Token: 0x06002CAC RID: 11436 RVA: 0x000B2FDB File Offset: 0x000B11DB
		private void WriteError(LocalizedException exception)
		{
			MigrationLogger.Log(MigrationEventType.Warning, MigrationLogger.GetDiagnosticInfo(exception, null), new object[0]);
			base.WriteError(exception, (ErrorCategory)1000, null);
		}

		// Token: 0x04002060 RID: 8288
		private const string ParameterNameMaxConcurrentMigrations = "MaxConcurrentMigrations";

		// Token: 0x04002061 RID: 8289
		private const string ParameterNameMaxNumberOfBatches = "MaxNumberOfBatches";

		// Token: 0x04002062 RID: 8290
		private const string ParameterNameFeatures = "MigrationFeature";

		// Token: 0x04002063 RID: 8291
		private bool changed;

		// Token: 0x04002064 RID: 8292
		private bool disposed;

		// Token: 0x04002065 RID: 8293
		private ADUser partitionMailbox;
	}
}
