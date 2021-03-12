using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.WorkloadManagement;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000098 RID: 152
	internal class PublicFolderMailboxMigrationJob : MoveBaseJob
	{
		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x0600078F RID: 1935 RVA: 0x0003466C File Offset: 0x0003286C
		// (set) Token: 0x06000790 RID: 1936 RVA: 0x00034674 File Offset: 0x00032874
		public SourceMailboxWrapper SourceDatabasesWrapper { get; private set; }

		// Token: 0x170001BA RID: 442
		// (get) Token: 0x06000791 RID: 1937 RVA: 0x0003467D File Offset: 0x0003287D
		protected override bool ReachedThePointOfNoReturn
		{
			get
			{
				return base.SyncStage >= SyncStage.CreatingInitialSyncCheckpoint;
			}
		}

		// Token: 0x170001BB RID: 443
		// (get) Token: 0x06000792 RID: 1938 RVA: 0x0003468B File Offset: 0x0003288B
		// (set) Token: 0x06000793 RID: 1939 RVA: 0x00034693 File Offset: 0x00032893
		public override bool IsOnlineMove
		{
			get
			{
				return base.IsOnlineMove;
			}
			protected set
			{
				if (!value)
				{
					throw new OfflinePublicFolderMigrationNotSupportedException();
				}
				base.IsOnlineMove = value;
			}
		}

		// Token: 0x06000794 RID: 1940 RVA: 0x000346A8 File Offset: 0x000328A8
		protected override bool CanBeCanceledOrSuspended()
		{
			Organization orgContainer = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(true, ConsistencyMode.PartiallyConsistent, ADSessionSettings.FromOrganizationIdWithoutRbacScopesServiceOnly(this.publicFolderConfiguration.OrganizationId), 85, "CanBeCanceledOrSuspended", "f:\\15.00.1497\\sources\\dev\\mrs\\src\\Service\\MRSJobs\\PublicFolderMailboxMigrationJob.cs").GetOrgContainer();
			return orgContainer.DefaultPublicFolderMailbox.Type == PublicFolderInformation.HierarchyType.InTransitMailboxGuid && !this.ReachedThePointOfNoReturn;
		}

		// Token: 0x06000795 RID: 1941 RVA: 0x000346FC File Offset: 0x000328FC
		public override void Initialize(TransactionalRequestJob migrationRequestJob)
		{
			MrsTracer.Service.Function("PublicFolderMailboxMigrationJob.Initialize: SourceDatabase=\"{0}\", Flags={1}", new object[]
			{
				migrationRequestJob.SourceDatabase,
				migrationRequestJob.Flags
			});
			TenantPublicFolderConfigurationCache.Instance.RemoveValue(migrationRequestJob.OrganizationId);
			this.publicFolderConfiguration = TenantPublicFolderConfigurationCache.Instance.GetValue(migrationRequestJob.OrganizationId);
			base.Initialize(migrationRequestJob);
			LocalizedString sourceTracingID = LocalizedString.Empty;
			MailboxCopierFlags mailboxCopierFlags = MailboxCopierFlags.PublicFolderMigration;
			bool flag = migrationRequestJob.RequestStyle == RequestStyle.CrossOrg;
			if (flag)
			{
				sourceTracingID = MrsStrings.RPCHTTPPublicFoldersId(migrationRequestJob.RemoteMailboxServerLegacyDN);
				mailboxCopierFlags |= MailboxCopierFlags.CrossOrg;
			}
			this.publicFolderMailboxMigrator = new PublicFolderMailboxMigrator(this.publicFolderConfiguration, migrationRequestJob.FolderToMailboxMap, migrationRequestJob.TargetExchangeGuid, mailboxCopierFlags | MailboxCopierFlags.Root, migrationRequestJob, this, sourceTracingID);
		}

		// Token: 0x06000796 RID: 1942 RVA: 0x000347B0 File Offset: 0x000329B0
		protected override void ConfigureProviders(bool continueAfterConfiguringProviders)
		{
			this.InternalConfigureProviders(continueAfterConfiguringProviders);
		}

		// Token: 0x06000797 RID: 1943 RVA: 0x000347B9 File Offset: 0x000329B9
		protected override void UnconfigureProviders()
		{
			if (this.SourceDatabasesWrapper != null)
			{
				this.SourceDatabasesWrapper.Dispose();
				this.SourceDatabasesWrapper = null;
			}
		}

		// Token: 0x06000798 RID: 1944 RVA: 0x000347D8 File Offset: 0x000329D8
		public override List<MailboxCopierBase> GetAllCopiers()
		{
			return new List<MailboxCopierBase>
			{
				this.publicFolderMailboxMigrator
			};
		}

		// Token: 0x06000799 RID: 1945 RVA: 0x000347F8 File Offset: 0x000329F8
		protected override void ScheduleContentVerification(List<FolderSizeRec> verificationResults)
		{
		}

		// Token: 0x0600079A RID: 1946 RVA: 0x000347FA File Offset: 0x000329FA
		protected override void VerifyFolderContents(MailboxCopierBase mbxCtx, FolderRecWrapper folderRecWrapper, List<FolderSizeRec> verificationResults)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600079B RID: 1947 RVA: 0x00034804 File Offset: 0x00032A04
		public override void ValidateAndPopulateRequestJob(List<ReportEntry> entries)
		{
			this.InternalConfigureProviders(false);
			MailboxServerInformation mailboxServerInformation;
			MailboxInformation mailboxInformation;
			PublicFolderMailboxMigrationJob.ConnectAndValidateSource(this.sourceDatabases, out mailboxServerInformation, out mailboxInformation);
			base.CachedRequestJob.TimeTracker.SetTimestamp(RequestJobTimestamp.LastSuccessfulSourceConnection, new DateTime?(DateTime.UtcNow));
			base.CachedRequestJob.TimeTracker.SetTimestamp(RequestJobTimestamp.SourceConnectionFailure, null);
			MailboxServerInformation mailboxServerInformation2;
			MailboxInformation mailboxInformation2;
			PublicFolderMailboxMigrationJob.ConnectAndValidateDestination(this.publicFolderMailboxMigrator.DestMailbox, MailboxConnectFlags.ValidateOnly, out mailboxServerInformation2, out mailboxInformation2);
			base.CachedRequestJob.TimeTracker.SetTimestamp(RequestJobTimestamp.LastSuccessfulTargetConnection, new DateTime?(DateTime.UtcNow));
			base.CachedRequestJob.TimeTracker.SetTimestamp(RequestJobTimestamp.TargetConnectionFailure, null);
			if (mailboxInformation != null && mailboxInformation.ServerVersion != 0)
			{
				base.CachedRequestJob.SourceVersion = mailboxInformation.ServerVersion;
				base.CachedRequestJob.SourceServer = ((mailboxServerInformation != null) ? mailboxServerInformation.MailboxServerName : null);
			}
			if (mailboxInformation2 != null && mailboxInformation2.ServerVersion != 0)
			{
				base.CachedRequestJob.TargetVersion = mailboxInformation2.ServerVersion;
				base.CachedRequestJob.TargetServer = ((mailboxServerInformation2 != null) ? mailboxServerInformation2.MailboxServerName : null);
			}
		}

		// Token: 0x0600079C RID: 1948 RVA: 0x00034914 File Offset: 0x00032B14
		protected override bool ValidateTargetMailbox(MailboxInformation mailboxInfo, out LocalizedString moveFinishedReason)
		{
			Organization orgContainer = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(true, ConsistencyMode.PartiallyConsistent, ADSessionSettings.FromOrganizationIdWithoutRbacScopesServiceOnly(this.publicFolderConfiguration.OrganizationId), 249, "ValidateTargetMailbox", "f:\\15.00.1497\\sources\\dev\\mrs\\src\\Service\\MRSJobs\\PublicFolderMailboxMigrationJob.cs").GetOrgContainer();
			moveFinishedReason = MrsStrings.ReportTargetPublicFolderDeploymentUnlocked;
			return orgContainer.DefaultPublicFolderMailbox.Type == PublicFolderInformation.HierarchyType.InTransitMailboxGuid;
		}

		// Token: 0x0600079D RID: 1949 RVA: 0x0003496C File Offset: 0x00032B6C
		private static void ConnectAndValidateSource(ISourceMailbox sourceDatabase, out MailboxServerInformation sourceMailboxServerInfo, out MailboxInformation sourceDatabaseInfo)
		{
			sourceDatabase.Connect(MailboxConnectFlags.None);
			ParallelPublicFolderMigrationVersionChecker.ThrowIfMinimumRequiredVersionNotInstalled(((MapiSourceMailbox)sourceDatabase).ServerVersion);
			sourceMailboxServerInfo = sourceDatabase.GetMailboxServerInformation();
			sourceDatabaseInfo = sourceDatabase.GetMailboxInformation();
			MrsTracer.Service.Debug("Switching source public folder database {0} to SyncSource mode.", new object[]
			{
				sourceDatabaseInfo
			});
			bool flag;
			sourceDatabase.SetInTransitStatus(InTransitStatus.SyncSource, out flag);
			if (!flag)
			{
				throw new OfflinePublicFolderMigrationNotSupportedException();
			}
		}

		// Token: 0x0600079E RID: 1950 RVA: 0x000349CF File Offset: 0x00032BCF
		private static void ConnectAndValidateDestination(IDestinationMailbox destinationMailboxes, MailboxConnectFlags connectFlags, out MailboxServerInformation destinationHierarchyMailboxServerInfo, out MailboxInformation destinationHierarchyMailboxInfo)
		{
			destinationMailboxes.Connect(connectFlags);
			destinationHierarchyMailboxServerInfo = destinationMailboxes.GetMailboxServerInformation();
			destinationHierarchyMailboxInfo = destinationMailboxes.GetMailboxInformation();
		}

		// Token: 0x0600079F RID: 1951 RVA: 0x000349E8 File Offset: 0x00032BE8
		protected override void FinalSync()
		{
			base.StartDataGuaranteeWait();
			base.ScheduleWorkItem(new Action(base.WaitForMailboxChangesToReplicate), WorkloadType.Unknown);
		}

		// Token: 0x060007A0 RID: 1952 RVA: 0x00034A03 File Offset: 0x00032C03
		protected override void MigrateSecurityDescriptors()
		{
		}

		// Token: 0x060007A1 RID: 1953 RVA: 0x00034A05 File Offset: 0x00032C05
		protected override void UpdateMovedMailbox()
		{
		}

		// Token: 0x060007A2 RID: 1954 RVA: 0x00034A07 File Offset: 0x00032C07
		protected override void UpdateRequestOnSave(TransactionalRequestJob rj, UpdateRequestOnSaveType updateType)
		{
		}

		// Token: 0x060007A3 RID: 1955 RVA: 0x00034A09 File Offset: 0x00032C09
		protected override void OnMoveCompleted(MailboxCopierBase mbxCtx)
		{
		}

		// Token: 0x060007A4 RID: 1956 RVA: 0x00034A0B File Offset: 0x00032C0B
		protected override void PostMoveCleanupSourceMailbox(MailboxCopierBase mbxCtx)
		{
			mbxCtx.SyncState.CompletedCleanupTasks |= PostMoveCleanupStatusFlags.SourceMailboxCleanup;
		}

		// Token: 0x060007A5 RID: 1957 RVA: 0x00034A20 File Offset: 0x00032C20
		protected override void PostMoveMarkRehomeOnRelatedRequests(MailboxCopierBase mbxCtx)
		{
			mbxCtx.SyncState.CompletedCleanupTasks |= PostMoveCleanupStatusFlags.SetRelatedRequestsRehome;
		}

		// Token: 0x060007A6 RID: 1958 RVA: 0x00034A36 File Offset: 0x00032C36
		protected override void CleanupDestinationMailbox(MailboxCopierBase mbxCtx, bool moveIsSuccessful)
		{
		}

		// Token: 0x060007A7 RID: 1959 RVA: 0x00034A38 File Offset: 0x00032C38
		protected override void InternalDispose(bool calledFromDispose)
		{
			if (calledFromDispose)
			{
				if (this.SourceDatabasesWrapper != null)
				{
					this.SourceDatabasesWrapper.Dispose();
					this.SourceDatabasesWrapper = null;
				}
				if (this.publicFolderMailboxMigrator != null)
				{
					this.publicFolderMailboxMigrator.UnconfigureProviders();
					this.publicFolderMailboxMigrator = null;
				}
			}
			base.InternalDispose(calledFromDispose);
		}

		// Token: 0x060007A8 RID: 1960 RVA: 0x00034A78 File Offset: 0x00032C78
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<PublicFolderMailboxMigrationJob>(this);
		}

		// Token: 0x060007A9 RID: 1961 RVA: 0x00034A80 File Offset: 0x00032C80
		private void InternalConfigureProviders(bool continueAfterConfiguringProviders)
		{
			RequestStatisticsBase cachedRequestJob = base.CachedRequestJob;
			bool flag = cachedRequestJob.RequestStyle == RequestStyle.CrossOrg;
			LocalMailboxFlags localMailboxFlags = LocalMailboxFlags.LegacyPublicFolders | LocalMailboxFlags.ParallelPublicFolderMigration;
			if (flag)
			{
				localMailboxFlags |= LocalMailboxFlags.PureMAPI;
			}
			this.sourceDatabases = new MapiSourceMailbox(localMailboxFlags);
			if (flag)
			{
				this.sourceDatabases.ConfigRPCHTTP(cachedRequestJob.RemoteMailboxLegacyDN, null, cachedRequestJob.RemoteMailboxServerLegacyDN, cachedRequestJob.OutlookAnywhereHostName, cachedRequestJob.RemoteCredential, true, cachedRequestJob.AuthenticationMethod != null && cachedRequestJob.AuthenticationMethod.Value == AuthenticationMethod.Ntlm);
			}
			else
			{
				((IMailbox)this.sourceDatabases).Config(base.GetReservation(cachedRequestJob.SourceDatabase.ObjectGuid, ReservationFlags.Read), cachedRequestJob.SourceDatabase.ObjectGuid, cachedRequestJob.SourceDatabase.ObjectGuid, CommonUtils.GetPartitionHint(cachedRequestJob.OrganizationId), cachedRequestJob.SourceDatabase.ObjectGuid, MailboxType.SourceMailbox, null);
			}
			LocalizedString tracingId = flag ? MrsStrings.RPCHTTPPublicFoldersId(cachedRequestJob.RemoteMailboxLegacyDN) : MrsStrings.PublicFoldersId(cachedRequestJob.OrganizationId.ToString());
			this.sourceDatabases.ConfigPublicFolders(cachedRequestJob.SourceDatabase);
			this.SourceDatabasesWrapper = new SourceMailboxWrapper(this.sourceDatabases, MailboxWrapperFlags.Source, tracingId);
			this.publicFolderMailboxMigrator.SetSourceDatabasesWrapper(this.SourceDatabasesWrapper);
			base.ConfigureProviders(continueAfterConfiguringProviders);
			if (flag)
			{
				this.publicFolderMailboxMigrator.ConfigTranslators(new PrincipalTranslator(this.SourceDatabasesWrapper.PrincipalMapper, this.publicFolderMailboxMigrator.DestMailboxWrapper.PrincipalMapper), null);
			}
		}

		// Token: 0x04000307 RID: 775
		private TenantPublicFolderConfiguration publicFolderConfiguration;

		// Token: 0x04000308 RID: 776
		private MapiSourceMailbox sourceDatabases;

		// Token: 0x04000309 RID: 777
		private PublicFolderMailboxMigrator publicFolderMailboxMigrator;
	}
}
