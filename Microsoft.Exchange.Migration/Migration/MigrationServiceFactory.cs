using System;
using System.Globalization;
using System.Net;
using System.Threading;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Directory.SystemConfiguration.ConfigurationSettings;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.MailboxReplicationService;
using Microsoft.Exchange.Migration.DataAccessLayer;
using Microsoft.Exchange.Migration.Logging;
using Microsoft.Exchange.Migration.Test;
using Microsoft.Exchange.Rpc.MigrationService;
using Microsoft.Exchange.Transport.Sync.Migration.Rpc;
using Microsoft.Exchange.VariantConfiguration;
using Microsoft.Exchange.WebServices.Data;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x02000023 RID: 35
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class MigrationServiceFactory
	{
		// Token: 0x06000169 RID: 361 RVA: 0x00007596 File Offset: 0x00005796
		protected MigrationServiceFactory()
		{
			this.cryptoAdapter = new Lazy<ICryptoAdapter>(new Func<ICryptoAdapter>(MigrationServiceFactory.CreateCryptoAdapter), LazyThreadSafetyMode.PublicationOnly);
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x0600016A RID: 362 RVA: 0x000075B6 File Offset: 0x000057B6
		// (set) Token: 0x0600016B RID: 363 RVA: 0x000075BD File Offset: 0x000057BD
		public static MigrationServiceFactory Instance
		{
			get
			{
				return MigrationServiceFactory.instance;
			}
			protected set
			{
				MigrationServiceFactory.instance = value;
			}
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x0600016C RID: 364 RVA: 0x000075C5 File Offset: 0x000057C5
		public virtual bool ShouldLog
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600016D RID: 365 RVA: 0x000075C8 File Offset: 0x000057C8
		public virtual string GetLocalServerFqdn()
		{
			return LocalServer.GetServer().Fqdn;
		}

		// Token: 0x0600016E RID: 366 RVA: 0x000075D4 File Offset: 0x000057D4
		public virtual IMigrationProxyRpc GetMigrationProxyRpcClient(string server)
		{
			return new MigrationProxyRpcClient(server);
		}

		// Token: 0x0600016F RID: 367 RVA: 0x000075DC File Offset: 0x000057DC
		public virtual IMigrationDataProvider CreateProviderForMigrationMailbox(TenantPartitionHint tenantPartitionHint, string migrationMailboxLegacyDN)
		{
			return MigrationDataProvider.CreateProviderForMigrationMailbox(tenantPartitionHint, migrationMailboxLegacyDN);
		}

		// Token: 0x06000170 RID: 368 RVA: 0x000075E5 File Offset: 0x000057E5
		public virtual IMigrationDataProvider CreateProviderForSystemMailbox(Guid mdbGuid)
		{
			return MigrationDataProvider.CreateProviderForSystemMailbox(mdbGuid);
		}

		// Token: 0x06000171 RID: 369 RVA: 0x000075ED File Offset: 0x000057ED
		public virtual JobProcessor CreateJobProcessor(MigrationJob job)
		{
			return JobProcessor.CreateJobProcessor(job);
		}

		// Token: 0x06000172 RID: 370 RVA: 0x000075F5 File Offset: 0x000057F5
		public virtual IStepHandler CreateStepHandler(MigrationWorkflowPosition position, IMigrationDataProvider dataProvider, MigrationJob migrationJob)
		{
			return MigrationWorkflowPosition.CreateStepHandler(position, dataProvider, migrationJob);
		}

		// Token: 0x06000173 RID: 371 RVA: 0x000075FF File Offset: 0x000057FF
		public virtual ISnapshotId GetStepSnapshotId(MigrationWorkflowPosition position, MigrationJobItem jobItem)
		{
			return MigrationWorkflowPosition.GetStepSnapshotId(position, jobItem);
		}

		// Token: 0x06000174 RID: 372 RVA: 0x00007608 File Offset: 0x00005808
		public virtual MigrationWorkflow GetMigrationWorkflow(MigrationType type)
		{
			switch (type)
			{
			case MigrationType.XO1:
			case MigrationType.ExchangeOutlookAnywhere:
				return new MigrationWorkflow(MigrationWorkflow.DefaultProvisionAndMigrateWorkflowSteps);
			}
			return new MigrationWorkflow(MigrationWorkflow.DefaultMigrationWorkflowSteps);
		}

		// Token: 0x06000175 RID: 373 RVA: 0x00007644 File Offset: 0x00005844
		public virtual IMigrationDataRowProvider GetMigrationDataRowProvider(MigrationJob job, IMigrationDataProvider dataProvider)
		{
			if (job.OriginalJobId != null)
			{
				return new MigrationPreexistingBatchCsvDataRowProvider(job, dataProvider);
			}
			MigrationType migrationType = job.MigrationType;
			if (migrationType <= MigrationType.ExchangeRemoteMove)
			{
				switch (migrationType)
				{
				case MigrationType.IMAP:
					return new IMAPCSVDataRowProvider(job, dataProvider);
				case MigrationType.XO1:
					return new XO1CSVDataRowProvider(job, dataProvider);
				case MigrationType.IMAP | MigrationType.XO1:
					goto IL_93;
				case MigrationType.ExchangeOutlookAnywhere:
					if (job.IsStaged)
					{
						return new NspiCsvMigrationDataRowProvider(job, dataProvider, false);
					}
					return new NspiMigrationDataRowProvider((ExchangeOutlookAnywhereEndpoint)job.SourceEndpoint, job, false);
				default:
					if (migrationType != MigrationType.ExchangeRemoteMove)
					{
						goto IL_93;
					}
					break;
				}
			}
			else if (migrationType != MigrationType.ExchangeLocalMove)
			{
				if (migrationType != MigrationType.PSTImport)
				{
					goto IL_93;
				}
				return new PSTCSVDataRowProvider(job, dataProvider);
			}
			return new MoveCsvDataRowProvider(job, dataProvider);
			IL_93:
			throw new NotSupportedException("Type " + job.MigrationType + " is unknown.");
		}

		// Token: 0x06000176 RID: 374 RVA: 0x00007703 File Offset: 0x00005903
		public virtual IAutodiscoverService GetAutodiscoverService(ExchangeVersion exchangeVersion, NetworkCredential credentials)
		{
			MigrationUtil.ThrowOnNullArgument(credentials, "credentials");
			return new AutodiscoverProxyService(exchangeVersion, credentials);
		}

		// Token: 0x06000177 RID: 375 RVA: 0x00007717 File Offset: 0x00005917
		public virtual IMigrationAutodiscoverClient GetAutodiscoverClient()
		{
			if (MigrationTestIntegration.Instance.IsMigrationProxyRpcClientEnabled)
			{
				return new MigrationExchangeProxyRpcClient();
			}
			return new MigrationAutodiscoverClient();
		}

		// Token: 0x06000178 RID: 376 RVA: 0x00007730 File Offset: 0x00005930
		public virtual IMigrationNspiClient GetNspiClient(ReportData reportData)
		{
			if (MigrationTestIntegration.Instance.IsMigrationProxyRpcClientEnabled)
			{
				return new MigrationExchangeProxyRpcClient();
			}
			return new MigrationNspiClient(reportData);
		}

		// Token: 0x06000179 RID: 377 RVA: 0x0000774A File Offset: 0x0000594A
		public virtual IMigrationService GetMigrationServiceClient(string serverName)
		{
			return new MigrationServiceRpcStub(serverName);
		}

		// Token: 0x0600017A RID: 378 RVA: 0x00007752 File Offset: 0x00005952
		public virtual IMigrationNotification GetMigrationNotificationClient(string serverName)
		{
			return new MigrationNotificationRpcStub(serverName);
		}

		// Token: 0x0600017B RID: 379 RVA: 0x0000775A File Offset: 0x0000595A
		internal virtual IMigrationRunspaceProxy CreateRunspaceForDatacenterAdmin(OrganizationId organizationId)
		{
			return MigrationRunspaceProxy.CreateRunspaceForDatacenterAdmin(organizationId);
		}

		// Token: 0x0600017C RID: 380 RVA: 0x00007762 File Offset: 0x00005962
		internal virtual IMigrationMrsClient GetMigrationMrsClient()
		{
			return new MigrationExchangeProxyRpcClient();
		}

		// Token: 0x0600017D RID: 381 RVA: 0x00007769 File Offset: 0x00005969
		internal virtual ICryptoAdapter GetCryptoAdapter()
		{
			return this.cryptoAdapter.Value;
		}

		// Token: 0x0600017E RID: 382 RVA: 0x00007778 File Offset: 0x00005978
		internal virtual IAsyncNotificationAdapter GetAsyncNotificationAdapter()
		{
			if (!ConfigBase<MigrationServiceConfigSchema>.GetConfig<bool>("MigrationAsyncNotificationEnabled"))
			{
				return AsyncNotificationAdapter.Empty;
			}
			return AsyncNotificationAdapter.Instance;
		}

		// Token: 0x0600017F RID: 383 RVA: 0x000077A0 File Offset: 0x000059A0
		internal virtual SubscriptionAccessorBase GetSubscriptionAccessor(IMigrationDataProvider dataProvider, MigrationType migrationType, string jobName, bool isPAW, bool legacyManualSyncs = false)
		{
			SubscriptionAccessorBase subscriptionAccessorBase;
			if (TestSubscriptionProxyAccessor.TryCreate(out subscriptionAccessorBase))
			{
				MigrationLogger.Log(MigrationEventType.Verbose, "MigrationServiceFactory:: overriding normal accessor with test accessor {0}", new object[]
				{
					subscriptionAccessorBase
				});
				return subscriptionAccessorBase;
			}
			return SubscriptionAccessorBase.CreateAccessor(dataProvider, migrationType, jobName, isPAW, legacyManualSyncs);
		}

		// Token: 0x06000180 RID: 384 RVA: 0x000077DC File Offset: 0x000059DC
		internal virtual MailboxSession.MailboxItemCountsAndSizes GetMailboxCountsAndSizes(Guid mailboxGuid, IMigrationADProvider adProvider)
		{
			ExchangePrincipal exchangePrincipalFromMbxGuid = adProvider.GetExchangePrincipalFromMbxGuid(mailboxGuid);
			MailboxSession.MailboxItemCountsAndSizes result;
			using (MailboxSession mailboxSession = MailboxSession.OpenAsSystemService(exchangePrincipalFromMbxGuid, CultureInfo.InvariantCulture, "Client=MSExchangeSimpleMigration;Privilege:OpenAsSystemService"))
			{
				result = mailboxSession.ReadMailboxCountsAndSizes();
			}
			return result;
		}

		// Token: 0x06000181 RID: 385 RVA: 0x00007828 File Offset: 0x00005A28
		internal virtual void PublishNotification(string notification, string message)
		{
			EventNotificationItem eventNotificationItem = new EventNotificationItem(ExchangeComponent.MailboxMigration.Name, ExchangeComponent.MailboxMigration.Name, notification, message, ResultSeverityLevel.Error);
			eventNotificationItem.Publish(false);
		}

		// Token: 0x06000182 RID: 386 RVA: 0x00007859 File Offset: 0x00005A59
		internal virtual IUpgradeConstraintAdapter GetUpgradeConstraintAdapter(MigrationSession migrationSession)
		{
			MigrationUtil.ThrowOnNullArgument(migrationSession, "migrationSession");
			if (migrationSession.IsSupported(MigrationFeature.UpgradeBlock))
			{
				return new OrganizationUpgradeConstraintAdapter();
			}
			return new NullUpgradeConstraintAdapter();
		}

		// Token: 0x06000183 RID: 387 RVA: 0x0000787C File Offset: 0x00005A7C
		internal virtual IMigrationEmailHandler CreateEmailHandler(IMigrationDataProvider dataProvider)
		{
			IMigrationEmailHandler result;
			if (MigrationEmailHandlerProxy.TryCreate(MigrationTestIntegration.Instance.ReportMessageEndpoint, out result))
			{
				return result;
			}
			return new MigrationDataProviderEmailHandler(dataProvider);
		}

		// Token: 0x06000184 RID: 388 RVA: 0x000078A4 File Offset: 0x00005AA4
		internal virtual bool IsMultiTenantEnabled()
		{
			return VariantConfiguration.InvariantNoFlightingSnapshot.Global.MultiTenancy.Enabled;
		}

		// Token: 0x06000185 RID: 389 RVA: 0x000078C8 File Offset: 0x00005AC8
		private static ICryptoAdapter CreateCryptoAdapter()
		{
			int config = ConfigBase<MigrationServiceConfigSchema>.GetConfig<int>("MigrationUseDKMForEncryption");
			bool flag;
			if (config > 0)
			{
				MigrationLogger.Log(MigrationEventType.Information, "Setting encryption type based on MigrationUseDKMForEncryption configuration setting = {0}.", new object[]
				{
					config
				});
				flag = (config == 1);
			}
			else
			{
				MigrationLogger.Log(MigrationEventType.Information, "Setting encryption type based on runtime check", new object[0]);
				flag = VariantConfiguration.InvariantNoFlightingSnapshot.Global.DistributedKeyManagement.Enabled;
			}
			if (flag)
			{
				MigrationLogger.Log(MigrationEventType.Information, "Using DKM encryption (in Datacenter).", new object[0]);
				return new DkmAdapter();
			}
			MigrationLogger.Log(MigrationEventType.Information, "Using non-DKM encryption (in Enterprise).", new object[0]);
			return new CryptoAdapter();
		}

		// Token: 0x04000098 RID: 152
		private static MigrationServiceFactory instance = new MigrationServiceFactory();

		// Token: 0x04000099 RID: 153
		private readonly Lazy<ICryptoAdapter> cryptoAdapter;
	}
}
