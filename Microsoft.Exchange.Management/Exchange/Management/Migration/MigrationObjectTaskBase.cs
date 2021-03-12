using System;
using System.Collections.Generic;
using System.IO;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Authorization;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Management.Common;
using Microsoft.Exchange.Management.EventMessages;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.Migration;
using Microsoft.Exchange.Migration.Logging;
using Microsoft.Exchange.Transport.Sync.Migration.Rpc;

namespace Microsoft.Exchange.Management.Migration
{
	// Token: 0x020004D6 RID: 1238
	public abstract class MigrationObjectTaskBase<TIdentityParameter> : ObjectActionTenantADTask<TIdentityParameter, MigrationBatch> where TIdentityParameter : IIdentityParameter, new()
	{
		// Token: 0x17000CB8 RID: 3256
		// (get) Token: 0x06002AE7 RID: 10983 RVA: 0x000ABE44 File Offset: 0x000AA044
		// (set) Token: 0x06002AE8 RID: 10984 RVA: 0x000ABE91 File Offset: 0x000AA091
		[Parameter(Mandatory = false, ParameterSetName = "Identity", ValueFromPipeline = true, ValueFromPipelineByPropertyName = true, Position = 0)]
		public override TIdentityParameter Identity
		{
			get
			{
				TIdentityParameter tidentityParameter = (TIdentityParameter)((object)base.Fields["Identity"]);
				if (tidentityParameter == null)
				{
					tidentityParameter = ((default(TIdentityParameter) == null) ? Activator.CreateInstance<TIdentityParameter>() : default(TIdentityParameter));
				}
				return tidentityParameter;
			}
			set
			{
				base.Fields["Identity"] = value;
			}
		}

		// Token: 0x17000CB9 RID: 3257
		// (get) Token: 0x06002AE9 RID: 10985 RVA: 0x000ABEA9 File Offset: 0x000AA0A9
		// (set) Token: 0x06002AEA RID: 10986 RVA: 0x000ABEC0 File Offset: 0x000AA0C0
		[Parameter(Mandatory = false)]
		public OrganizationIdParameter Organization
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

		// Token: 0x17000CBA RID: 3258
		// (get) Token: 0x06002AEB RID: 10987 RVA: 0x000ABED3 File Offset: 0x000AA0D3
		// (set) Token: 0x06002AEC RID: 10988 RVA: 0x000ABEEA File Offset: 0x000AA0EA
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

		// Token: 0x17000CBB RID: 3259
		// (get) Token: 0x06002AED RID: 10989
		public abstract string Action { get; }

		// Token: 0x17000CBC RID: 3260
		// (get) Token: 0x06002AEE RID: 10990 RVA: 0x000ABEFD File Offset: 0x000AA0FD
		protected string TenantName
		{
			get
			{
				if (!(base.CurrentOrganizationId != null) || base.CurrentOrganizationId.OrganizationalUnit == null)
				{
					return string.Empty;
				}
				return base.CurrentOrganizationId.OrganizationalUnit.Name;
			}
		}

		// Token: 0x06002AEF RID: 10991 RVA: 0x000ABF30 File Offset: 0x000AA130
		internal static void RegisterMigrationBatch(Task task, MailboxSession mailboxSession, OrganizationId organizationId, bool failIfNotConnected, bool refresh = false)
		{
			string serverFqdn = mailboxSession.MailboxOwner.MailboxInfo.Location.ServerFqdn;
			Guid mdbGuid = mailboxSession.MdbGuid;
			string mailboxOwnerLegacyDN = mailboxSession.MailboxOwnerLegacyDN;
			ADObjectId organizationName = organizationId.OrganizationalUnit ?? new ADObjectId();
			int num = 2;
			for (int i = 1; i <= num; i++)
			{
				try
				{
					MigrationNotificationRpcStub migrationNotificationRpcStub = new MigrationNotificationRpcStub(serverFqdn);
					migrationNotificationRpcStub.RegisterMigrationBatch(new RegisterMigrationBatchArgs(mdbGuid, mailboxOwnerLegacyDN, organizationName, refresh));
					break;
				}
				catch (MigrationServiceRpcException ex)
				{
					ExManagementApplicationLogger.LogEvent(ManagementEventLogConstants.Tuple_MigrationServiceConnectionError, new string[]
					{
						serverFqdn,
						ex.Message
					});
					if (i == num && failIfNotConnected)
					{
						MigrationLogger.Log(MigrationEventType.Warning, MigrationLogger.GetDiagnosticInfo(ex, null), new object[0]);
						task.WriteError(new LocalizedException(Strings.MigrationOperationFailed, null), ExchangeErrorCategory.Client, null);
					}
				}
			}
		}

		// Token: 0x06002AF0 RID: 10992 RVA: 0x000AC010 File Offset: 0x000AA210
		internal static ADUser ResolvePartitionMailbox(MailboxIdParameter partitionMailboxIdentity, IRecipientSession tenantGlobalCatalogSession, ADServerSettings serverSettings, DataAccessHelper.CategorizedGetDataObjectDelegate getDataObject, Task.ErrorLoggerDelegate writeError, bool datacenterFirstOrg)
		{
			ADUser aduser;
			if (partitionMailboxIdentity != null)
			{
				ADObjectId rootID = null;
				if (datacenterFirstOrg)
				{
					rootID = ADSystemConfigurationSession.GetFirstOrgUsersContainerId();
				}
				aduser = (ADUser)getDataObject(partitionMailboxIdentity, tenantGlobalCatalogSession, rootID, null, new LocalizedString?(Strings.MigrationPartitionMailboxNotFound), new LocalizedString?(Strings.MigrationPartitionMailboxAmbiguous), ExchangeErrorCategory.Client);
				if (!aduser.PersistedCapabilities.Contains(Capability.OrganizationCapabilityMigration))
				{
					writeError(new MigrationPartitionMailboxInvalidException(aduser.Alias), ExchangeErrorCategory.Client, partitionMailboxIdentity);
				}
			}
			else
			{
				List<ADUser> organizationMailboxesByCapability = OrganizationMailbox.GetOrganizationMailboxesByCapability(tenantGlobalCatalogSession, OrganizationCapability.Migration);
				if (organizationMailboxesByCapability == null || organizationMailboxesByCapability.Count == 0)
				{
					organizationMailboxesByCapability = OrganizationMailbox.GetOrganizationMailboxesByCapability(tenantGlobalCatalogSession, OrganizationCapability.Management);
				}
				if (organizationMailboxesByCapability == null || organizationMailboxesByCapability.Count == 0)
				{
					writeError(new MigrationPartitionMailboxNotFoundException(), ExchangeErrorCategory.Client, null);
				}
				else if (organizationMailboxesByCapability.Count > 1)
				{
					writeError(new MigrationPartitionMailboxAmbiguousException(), ExchangeErrorCategory.Client, null);
				}
				aduser = organizationMailboxesByCapability[0];
			}
			if (aduser.RecipientTypeDetails != RecipientTypeDetails.ArbitrationMailbox || aduser.Database == null)
			{
				writeError(new MigrationPartitionMailboxInvalidException(aduser.Alias), ExchangeErrorCategory.Client, partitionMailboxIdentity);
			}
			return aduser;
		}

		// Token: 0x06002AF1 RID: 10993 RVA: 0x000AC110 File Offset: 0x000AA310
		internal static MigrationJob GetAndValidateMigrationJob(Task task, MigrationBatchDataProvider batchProvider, MigrationBatchIdParameter identity, bool skipCorrupt, bool failIfNotFound = true)
		{
			MigrationObjectTaskBase<TIdentityParameter>.ValidateIdentity(task, batchProvider, identity);
			return MigrationObjectTaskBase<TIdentityParameter>.GetMigrationJobByBatchId(task, batchProvider, identity.MigrationBatchId, skipCorrupt, failIfNotFound);
		}

		// Token: 0x06002AF2 RID: 10994 RVA: 0x000AC138 File Offset: 0x000AA338
		internal static void ValidateIdentity(Task task, MigrationBatchDataProvider batchProvider, MigrationBatchIdParameter identity)
		{
			if (!batchProvider.MigrationSession.Config.IsSupported(MigrationFeature.MultiBatch))
			{
				return;
			}
			if (identity == null || identity.MigrationBatchId == null || identity.MigrationBatchId.Name == MigrationBatchId.Any.ToString())
			{
				task.WriteError(new MigrationPermanentException(Strings.MigrationBatchIdMissing), (ErrorCategory)1000, null);
			}
		}

		// Token: 0x06002AF3 RID: 10995 RVA: 0x000AC196 File Offset: 0x000AA396
		internal static SubmittedByUserAdminType GetUserType(ExchangeRunspaceConfiguration configuration, OrganizationId organizationId)
		{
			if (configuration == null)
			{
				return SubmittedByUserAdminType.DataCenterAdmin;
			}
			if (organizationId == OrganizationId.ForestWideOrgId)
			{
				if (configuration.PartnerMode)
				{
					return SubmittedByUserAdminType.Partner;
				}
				return SubmittedByUserAdminType.DataCenterAdmin;
			}
			else
			{
				if (configuration.DelegatedPrincipal != null)
				{
					return SubmittedByUserAdminType.PartnerTenant;
				}
				return SubmittedByUserAdminType.TenantAdmin;
			}
		}

		// Token: 0x06002AF4 RID: 10996 RVA: 0x000AC1C4 File Offset: 0x000AA3C4
		internal static LocalizedException ProcessCsv(IMigrationDataProvider dataProvider, MigrationBatch batch, MigrationCsvSchemaBase schema, byte[] csvData)
		{
			MigrationBatchCsvProcessor migrationBatchCsvProcessor = (batch.MigrationType == MigrationType.PublicFolder) ? new PublicFolderMigrationBatchCsvProcessor((PublicFolderMigrationCsvSchema)schema, dataProvider) : new MigrationBatchCsvProcessor(schema);
			return migrationBatchCsvProcessor.ProcessCsv(batch, csvData);
		}

		// Token: 0x06002AF5 RID: 10997 RVA: 0x000AC23C File Offset: 0x000AA43C
		internal static void StartJob(Task task, MigrationBatchDataProvider batchProvider, MigrationJob job, MultiValuedProperty<SmtpAddress> notificationEmails, MigrationBatchFlags batchFlags)
		{
			MigrationHelper.RunUpdateOperation(delegate
			{
				job.StartJob(batchProvider.MailboxProvider, notificationEmails, batchFlags, null);
			});
		}

		// Token: 0x06002AF6 RID: 10998 RVA: 0x000AC280 File Offset: 0x000AA480
		internal static MultiValuedProperty<SmtpAddress> GetUpdatedNotificationEmails(Task task, IRecipientSession recipientSession, IEnumerable<SmtpAddress> originalEmails)
		{
			MultiValuedProperty<SmtpAddress> multiValuedProperty = new MultiValuedProperty<SmtpAddress>();
			ADObjectId entryId;
			if (task.TryGetExecutingUserId(out entryId))
			{
				ADUser aduser = (ADUser)recipientSession.Read(entryId);
				if (aduser != null)
				{
					SmtpAddress windowsEmailAddress = aduser.WindowsEmailAddress;
					if (windowsEmailAddress.Length > 0)
					{
						multiValuedProperty.Add(windowsEmailAddress);
					}
				}
			}
			else if (task.ExchangeRunspaceConfig.DelegatedPrincipal != null)
			{
				multiValuedProperty.Add(new SmtpAddress(task.ExchangeRunspaceConfig.DelegatedPrincipal.UserId));
			}
			if (originalEmails == null && multiValuedProperty.Count <= 0)
			{
				return null;
			}
			if (originalEmails != null)
			{
				foreach (SmtpAddress item in originalEmails)
				{
					if (!multiValuedProperty.Contains(item))
					{
						multiValuedProperty.Add(item);
					}
				}
			}
			return multiValuedProperty;
		}

		// Token: 0x06002AF7 RID: 10999 RVA: 0x000AC350 File Offset: 0x000AA550
		internal static void WriteJobNotFoundError(Task task, string identity)
		{
			MigrationObjectTaskBase<TIdentityParameter>.WriteJobNotFoundError(task, identity, null);
		}

		// Token: 0x06002AF8 RID: 11000 RVA: 0x000AC35A File Offset: 0x000AA55A
		internal static void WriteJobNotFoundError(Task task, string jobName, Exception ex)
		{
			MigrationUtil.ThrowOnNullOrEmptyArgument(jobName, "jobName");
			task.WriteError(new MigrationBatchNotFoundException(jobName, ex), ExchangeErrorCategory.Client, null);
		}

		// Token: 0x06002AF9 RID: 11001 RVA: 0x000AC37C File Offset: 0x000AA57C
		internal MigrationJob GetAndValidateMigrationJob(bool skipCorrupt)
		{
			MigrationBatchDataProvider batchProvider = (MigrationBatchDataProvider)base.DataSession;
			return MigrationObjectTaskBase<TIdentityParameter>.GetAndValidateMigrationJob(this, batchProvider, (MigrationBatchIdParameter)((object)this.Identity), skipCorrupt, true);
		}

		// Token: 0x06002AFA RID: 11002 RVA: 0x000AC3AE File Offset: 0x000AA5AE
		internal MultiValuedProperty<SmtpAddress> GetUpdatedNotificationEmails(MultiValuedProperty<SmtpAddress> originalEmails)
		{
			return MigrationObjectTaskBase<TIdentityParameter>.GetUpdatedNotificationEmails(this, base.TenantGlobalCatalogSession, originalEmails);
		}

		// Token: 0x06002AFB RID: 11003 RVA: 0x000AC3C0 File Offset: 0x000AA5C0
		protected override void InternalBeginProcessing()
		{
			if (this.Organization != null)
			{
				base.CurrentOrganizationId = this.ResolveCurrentOrganization();
			}
			else if (!MapiTaskHelper.IsDatacenter)
			{
				base.CurrentOrganizationId = OrganizationId.ForestWideOrgId;
			}
			this.partitionMailbox = MigrationObjectTaskBase<TIdentityParameter>.ResolvePartitionMailbox(this.Partition, base.TenantGlobalCatalogSession, base.ServerSettings, new DataAccessHelper.CategorizedGetDataObjectDelegate(base.GetDataObject<ADUser>), new Task.ErrorLoggerDelegate(base.WriteError), base.CurrentOrganizationId == OrganizationId.ForestWideOrgId && MapiTaskHelper.IsDatacenter);
			base.InternalBeginProcessing();
		}

		// Token: 0x06002AFC RID: 11004 RVA: 0x000AC44C File Offset: 0x000AA64C
		protected override void InternalProcessRecord()
		{
			base.InternalProcessRecord();
			MigrationBatchDataProvider migrationBatchDataProvider = (MigrationBatchDataProvider)base.DataSession;
			migrationBatchDataProvider.MailboxProvider.FlushReport(migrationBatchDataProvider.MigrationJob.ReportData);
		}

		// Token: 0x06002AFD RID: 11005 RVA: 0x000AC484 File Offset: 0x000AA684
		protected override IConfigDataProvider CreateSession()
		{
			MigrationLogger.Initialize();
			MigrationLogContext.Current.Source = base.MyInvocation.MyCommand.Name;
			MigrationLogContext.Current.Organization = base.CurrentOrganizationId.OrganizationalUnit;
			return MigrationBatchDataProvider.CreateDataProvider(this.Action, base.TenantGlobalCatalogSession, null, this.partitionMailbox);
		}

		// Token: 0x06002AFE RID: 11006 RVA: 0x000AC4E8 File Offset: 0x000AA6E8
		protected override void InternalValidate()
		{
			MigrationBatchDataProvider migrationBatchDataProvider = (MigrationBatchDataProvider)base.DataSession;
			IMigrationADProvider adprovider = migrationBatchDataProvider.MailboxProvider.ADProvider;
			if (!adprovider.IsMigrationEnabled)
			{
				this.WriteError(new MigrationPermanentException(Strings.MigrationNotEnabledForTenant(this.TenantName)));
			}
			base.InternalValidate();
		}

		// Token: 0x06002AFF RID: 11007 RVA: 0x000AC531 File Offset: 0x000AA731
		protected override bool IsKnownException(Exception exception)
		{
			return MigrationBatchDataProvider.IsKnownException(exception) || base.IsKnownException(exception);
		}

		// Token: 0x06002B00 RID: 11008 RVA: 0x000AC544 File Offset: 0x000AA744
		protected override void InternalStateReset()
		{
			this.DisposeSession();
			base.InternalStateReset();
		}

		// Token: 0x06002B01 RID: 11009 RVA: 0x000AC554 File Offset: 0x000AA754
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

		// Token: 0x06002B02 RID: 11010 RVA: 0x000AC594 File Offset: 0x000AA794
		protected void WriteError(LocalizedException exception)
		{
			MigrationLogger.Log(MigrationEventType.Warning, MigrationLogger.GetDiagnosticInfo(exception, null), new object[0]);
			base.WriteError(exception, (ErrorCategory)1000, null);
		}

		// Token: 0x06002B03 RID: 11011 RVA: 0x000AC5B8 File Offset: 0x000AA7B8
		internal static MigrationJob GetMigrationJobByBatchId(Task task, MigrationBatchDataProvider batchProvider, MigrationBatchId migrationBatchId, bool skipCorrupt, bool failIfNotFound = true)
		{
			MigrationJob migrationJob = null;
			Exception ex = null;
			try
			{
				migrationJob = batchProvider.JobCache.GetJob(migrationBatchId);
			}
			catch (PropertyErrorException ex2)
			{
				ex = ex2;
			}
			catch (InvalidDataException ex3)
			{
				ex = ex3;
			}
			if (ex != null)
			{
				MigrationLogger.Log(MigrationEventType.Warning, MigrationLogger.GetDiagnosticInfo(ex, null), new object[0]);
				task.WriteError(new MigrationPermanentException(Strings.MigrationJobCorrupted, ex), ExchangeErrorCategory.Client, null);
			}
			if (migrationJob != null && migrationJob.Status == MigrationJobStatus.Corrupted && skipCorrupt)
			{
				migrationJob = null;
			}
			if (migrationJob == null && failIfNotFound)
			{
				MigrationObjectTaskBase<TIdentityParameter>.WriteJobNotFoundError(task, migrationBatchId.ToString());
			}
			return migrationJob;
		}

		// Token: 0x06002B04 RID: 11012 RVA: 0x000AC654 File Offset: 0x000AA854
		private OrganizationId ResolveCurrentOrganization()
		{
			ADObjectId rootOrgContainerId = ADSystemConfigurationSession.GetRootOrgContainerId(base.DomainController, string.IsNullOrEmpty(base.DomainController) ? null : base.NetCredential);
			ADSessionSettings sessionSettings = ADSessionSettings.FromOrganizationIdWithoutRbacScopes(rootOrgContainerId, base.CurrentOrganizationId, base.ExecutingUserOrganizationId, true);
			IConfigurationSession tenantOrTopologyConfigurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(base.DomainController, true, ConsistencyMode.PartiallyConsistent, null, sessionSettings, 695, "ResolveCurrentOrganization", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\Migration\\MigrationObjectTaskBase.cs");
			tenantOrTopologyConfigurationSession.UseConfigNC = false;
			ADOrganizationalUnit adorganizationalUnit = (ADOrganizationalUnit)base.GetDataObject<ADOrganizationalUnit>(this.Organization, tenantOrTopologyConfigurationSession, null, new LocalizedString?(Strings.ErrorOrganizationNotFound(this.Organization.ToString())), new LocalizedString?(Strings.ErrorOrganizationNotUnique(this.Organization.ToString())));
			return adorganizationalUnit.OrganizationId;
		}

		// Token: 0x06002B05 RID: 11013 RVA: 0x000AC715 File Offset: 0x000AA915
		private void DisposeSession()
		{
			if (base.DataSession is IDisposable)
			{
				MigrationLogger.Close();
				((IDisposable)base.DataSession).Dispose();
			}
		}

		// Token: 0x04002005 RID: 8197
		private bool disposed;

		// Token: 0x04002006 RID: 8198
		protected ADUser partitionMailbox;
	}
}
