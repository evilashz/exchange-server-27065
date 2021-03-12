using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage.PublicFolder;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000097 RID: 151
	internal class PublicFolderMigrationJob : MoveBaseJob
	{
		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x06000774 RID: 1908 RVA: 0x00033A77 File Offset: 0x00031C77
		// (set) Token: 0x06000775 RID: 1909 RVA: 0x00033A7F File Offset: 0x00031C7F
		public SourceMailboxWrapper SourceDatabasesWrapper { get; private set; }

		// Token: 0x170001B7 RID: 439
		// (get) Token: 0x06000776 RID: 1910 RVA: 0x00033A88 File Offset: 0x00031C88
		protected override bool ReachedThePointOfNoReturn
		{
			get
			{
				return base.SyncStage >= SyncStage.Cleanup;
			}
		}

		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x06000777 RID: 1911 RVA: 0x00033A97 File Offset: 0x00031C97
		// (set) Token: 0x06000778 RID: 1912 RVA: 0x00033A9F File Offset: 0x00031C9F
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

		// Token: 0x06000779 RID: 1913 RVA: 0x00033AB4 File Offset: 0x00031CB4
		public override void Initialize(TransactionalRequestJob migrationRequestJob)
		{
			MrsTracer.Service.Function("PublicFolderMigrationJob.Initialize: SourceDatabase=\"{0}\", Flags={1}", new object[]
			{
				migrationRequestJob.SourceDatabase,
				migrationRequestJob.Flags
			});
			TenantPublicFolderConfigurationCache.Instance.RemoveValue(migrationRequestJob.OrganizationId);
			this.publicFolderConfiguration = TenantPublicFolderConfigurationCache.Instance.GetValue(migrationRequestJob.OrganizationId);
			this.publicFolderMigrators = new Dictionary<Guid, PublicFolderMigrator>();
			base.Initialize(migrationRequestJob);
			if (this.publicFolderConfiguration.GetHierarchyMailboxInformation().Type != PublicFolderInformation.HierarchyType.InTransitMailboxGuid)
			{
				return;
			}
			LocalizedString sourceTracingID = LocalizedString.Empty;
			MailboxCopierFlags mailboxCopierFlags = MailboxCopierFlags.PublicFolderMigration;
			bool flag = migrationRequestJob.RequestStyle == RequestStyle.CrossOrg;
			if (flag)
			{
				sourceTracingID = MrsStrings.RPCHTTPPublicFoldersId(migrationRequestJob.RemoteMailboxServerLegacyDN);
				mailboxCopierFlags |= MailboxCopierFlags.CrossOrg;
			}
			Guid hierarchyMailboxGuid = this.publicFolderConfiguration.GetHierarchyMailboxInformation().HierarchyMailboxGuid;
			PublicFolderMigrator value = new PublicFolderMigrator(this.publicFolderConfiguration, migrationRequestJob.FolderToMailboxMap, hierarchyMailboxGuid, mailboxCopierFlags | MailboxCopierFlags.Root, migrationRequestJob, this, sourceTracingID);
			this.publicFolderMigrators[hierarchyMailboxGuid] = value;
			foreach (FolderToMailboxMapping folderToMailboxMapping in migrationRequestJob.FolderToMailboxMap)
			{
				Guid mailboxGuid = folderToMailboxMapping.MailboxGuid;
				if (mailboxGuid != hierarchyMailboxGuid && !this.publicFolderMigrators.ContainsKey(mailboxGuid))
				{
					PublicFolderMigrator value2 = new PublicFolderMigrator(this.publicFolderConfiguration, migrationRequestJob.FolderToMailboxMap, mailboxGuid, mailboxCopierFlags, migrationRequestJob, this, sourceTracingID);
					this.publicFolderMigrators[mailboxGuid] = value2;
				}
			}
		}

		// Token: 0x0600077A RID: 1914 RVA: 0x00033C2C File Offset: 0x00031E2C
		protected override void ConfigureProviders(bool continueAfterConfiguringProviders)
		{
			this.InternalConfigureProviders(continueAfterConfiguringProviders);
		}

		// Token: 0x0600077B RID: 1915 RVA: 0x00033C35 File Offset: 0x00031E35
		protected override void UnconfigureProviders()
		{
			if (this.SourceDatabasesWrapper != null)
			{
				this.SourceDatabasesWrapper.Dispose();
				this.SourceDatabasesWrapper = null;
			}
		}

		// Token: 0x0600077C RID: 1916 RVA: 0x00033C54 File Offset: 0x00031E54
		public override List<MailboxCopierBase> GetAllCopiers()
		{
			List<MailboxCopierBase> list = new List<MailboxCopierBase>();
			if (this.publicFolderConfiguration.GetHierarchyMailboxInformation().Type == PublicFolderInformation.HierarchyType.InTransitMailboxGuid)
			{
				Guid hierarchyMailboxGuid = this.publicFolderConfiguration.GetHierarchyMailboxInformation().HierarchyMailboxGuid;
				list.Add(this.publicFolderMigrators[hierarchyMailboxGuid]);
				foreach (PublicFolderMigrator publicFolderMigrator in this.publicFolderMigrators.Values)
				{
					if (publicFolderMigrator.TargetMailboxGuid != hierarchyMailboxGuid)
					{
						list.Add(publicFolderMigrator);
					}
				}
			}
			return list;
		}

		// Token: 0x0600077D RID: 1917 RVA: 0x00033CF8 File Offset: 0x00031EF8
		protected override void ScheduleContentVerification(List<FolderSizeRec> verificationResults)
		{
		}

		// Token: 0x0600077E RID: 1918 RVA: 0x00033CFA File Offset: 0x00031EFA
		protected override void VerifyFolderContents(MailboxCopierBase mbxCtx, FolderRecWrapper folderRecWrapper, List<FolderSizeRec> verificationResults)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600077F RID: 1919 RVA: 0x00033D04 File Offset: 0x00031F04
		public override void ValidateAndPopulateRequestJob(List<ReportEntry> entries)
		{
			this.InternalConfigureProviders(false);
			MailboxServerInformation mailboxServerInformation;
			MailboxInformation mailboxInformation;
			PublicFolderMigrationJob.ConnectAndValidateSource(this.sourceDatabases, out mailboxServerInformation, out mailboxInformation);
			base.CachedRequestJob.TimeTracker.SetTimestamp(RequestJobTimestamp.LastSuccessfulSourceConnection, new DateTime?(DateTime.UtcNow));
			base.CachedRequestJob.TimeTracker.SetTimestamp(RequestJobTimestamp.SourceConnectionFailure, null);
			MailboxServerInformation mailboxServerInformation2 = null;
			MailboxInformation mailboxInformation2 = null;
			foreach (PublicFolderMigrator publicFolderMigrator in this.publicFolderMigrators.Values)
			{
				MailboxServerInformation mailboxServerInformation3;
				MailboxInformation mailboxInformation3;
				PublicFolderMigrationJob.ConnectAndValidateDestination(publicFolderMigrator.DestMailbox, MailboxConnectFlags.ValidateOnly, out mailboxServerInformation3, out mailboxInformation3);
				bool flag = publicFolderMigrator.TargetMailboxGuid == this.publicFolderConfiguration.GetHierarchyMailboxInformation().HierarchyMailboxGuid;
				if (flag)
				{
					MrsTracer.Service.Debug("PublicFolderMigrator for hierarchy mailbox created, with dumpster creation by {0}.", new object[]
					{
						publicFolderMigrator.DestMailbox.IsCapabilitySupported(MRSProxyCapabilities.CanStoreCreatePFDumpster) ? "Store" : "MRS"
					});
					mailboxServerInformation2 = mailboxServerInformation3;
					mailboxInformation2 = mailboxInformation3;
				}
			}
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

		// Token: 0x06000780 RID: 1920 RVA: 0x00033EBC File Offset: 0x000320BC
		protected override bool ValidateTargetMailbox(MailboxInformation mailboxInfo, out LocalizedString moveFinishedReason)
		{
			Organization orgContainer = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(true, ConsistencyMode.PartiallyConsistent, ADSessionSettings.FromOrganizationIdWithoutRbacScopesServiceOnly(this.publicFolderConfiguration.OrganizationId), 314, "ValidateTargetMailbox", "f:\\15.00.1497\\sources\\dev\\mrs\\src\\Service\\MRSJobs\\PublicFolderMigrationJob.cs").GetOrgContainer();
			moveFinishedReason = MrsStrings.ReportTargetPublicFolderDeploymentUnlocked;
			return orgContainer.DefaultPublicFolderMailbox.Type == PublicFolderInformation.HierarchyType.InTransitMailboxGuid;
		}

		// Token: 0x06000781 RID: 1921 RVA: 0x00033F14 File Offset: 0x00032114
		private static void ConnectAndValidateSource(ISourceMailbox sourceDatabase, out MailboxServerInformation sourceMailboxServerInfo, out MailboxInformation sourceDatabaseInfo)
		{
			sourceDatabase.Connect(MailboxConnectFlags.None);
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

		// Token: 0x06000782 RID: 1922 RVA: 0x00033F67 File Offset: 0x00032167
		private static void ConnectAndValidateDestination(IDestinationMailbox destinationMailboxes, MailboxConnectFlags connectFlags, out MailboxServerInformation destinationHierarchyMailboxServerInfo, out MailboxInformation destinationHierarchyMailboxInfo)
		{
			destinationMailboxes.Connect(connectFlags);
			destinationHierarchyMailboxServerInfo = destinationMailboxes.GetMailboxServerInformation();
			destinationHierarchyMailboxInfo = destinationMailboxes.GetMailboxInformation();
		}

		// Token: 0x06000783 RID: 1923 RVA: 0x00033F80 File Offset: 0x00032180
		protected override void MigrateSecurityDescriptors()
		{
		}

		// Token: 0x06000784 RID: 1924 RVA: 0x00033F82 File Offset: 0x00032182
		protected override void UpdateMovedMailbox()
		{
		}

		// Token: 0x06000785 RID: 1925 RVA: 0x00033F84 File Offset: 0x00032184
		protected override void UpdateRequestOnSave(TransactionalRequestJob rj, UpdateRequestOnSaveType updateType)
		{
		}

		// Token: 0x06000786 RID: 1926 RVA: 0x00034060 File Offset: 0x00032260
		protected override void OnMoveCompleted(MailboxCopierBase mbxCtx)
		{
			CommonUtils.CatchKnownExceptions(delegate
			{
				Guid hierarchyMailboxGuid = this.publicFolderConfiguration.GetHierarchyMailboxInformation().HierarchyMailboxGuid;
				if (mbxCtx.TargetMailboxGuid != hierarchyMailboxGuid)
				{
					MrsTracer.Service.Debug("Attempting to start hierarchy sync to content mailbox {0}.", new object[]
					{
						mbxCtx.TargetTracingID
					});
					PublicFolderSyncJobRpc.StartSyncHierarchy(this.publicFolderConfiguration.OrganizationId, mbxCtx.TargetMailboxGuid, mbxCtx.TargetServerInfo.MailboxServerName, true);
				}
			}, delegate(Exception e)
			{
				MrsTracer.Service.Error("Failed to start hierarchy sync to content mailbox {0} - {1}", new object[]
				{
					mbxCtx.TargetTracingID,
					e
				});
			});
		}

		// Token: 0x06000787 RID: 1927 RVA: 0x0003409E File Offset: 0x0003229E
		protected override void PostMoveCleanupSourceMailbox(MailboxCopierBase mbxCtx)
		{
			mbxCtx.SyncState.CompletedCleanupTasks |= PostMoveCleanupStatusFlags.SourceMailboxCleanup;
		}

		// Token: 0x06000788 RID: 1928 RVA: 0x000340B3 File Offset: 0x000322B3
		protected override void PostMoveMarkRehomeOnRelatedRequests(MailboxCopierBase mbxCtx)
		{
			mbxCtx.SyncState.CompletedCleanupTasks |= PostMoveCleanupStatusFlags.SetRelatedRequestsRehome;
		}

		// Token: 0x06000789 RID: 1929 RVA: 0x000340CC File Offset: 0x000322CC
		protected override void CleanupDestinationMailbox(MailboxCopierBase mbxCtx, bool moveIsSuccessful)
		{
			if (mbxCtx.IsRoot && moveIsSuccessful)
			{
				IConfigurationSession tenantOrTopologyConfigurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(false, ConsistencyMode.PartiallyConsistent, ADSessionSettings.FromOrganizationIdWithoutRbacScopesServiceOnly(this.publicFolderConfiguration.OrganizationId), 450, "CleanupDestinationMailbox", "f:\\15.00.1497\\sources\\dev\\mrs\\src\\Service\\MRSJobs\\PublicFolderMigrationJob.cs");
				Organization orgContainer = tenantOrTopologyConfigurationSession.GetOrgContainer();
				orgContainer.DefaultPublicFolderMailbox = orgContainer.DefaultPublicFolderMailbox.Clone();
				orgContainer.DefaultPublicFolderMailbox.SetHierarchyMailbox(orgContainer.DefaultPublicFolderMailbox.HierarchyMailboxGuid, PublicFolderInformation.HierarchyType.MailboxGuid);
				tenantOrTopologyConfigurationSession.Save(orgContainer);
			}
		}

		// Token: 0x0600078A RID: 1930 RVA: 0x000341D0 File Offset: 0x000323D0
		protected override MailboxChangesManifest EnumerateSourceHierarchyChanges(MailboxCopierBase mbxCtx, bool catchup, SyncContext syncContext)
		{
			if (catchup)
			{
				if (!mbxCtx.IsRoot)
				{
					return null;
				}
				return base.EnumerateSourceHierarchyChanges(mbxCtx, catchup, syncContext);
			}
			else
			{
				if (mbxCtx.IsRoot)
				{
					return base.EnumerateSourceHierarchyChanges(mbxCtx, catchup, syncContext);
				}
				FolderMap destinationFolderMap = base.GetRootMailboxContext().GetDestinationFolderMap(GetFolderMapFlags.None);
				EntryIdMap<FolderRecWrapper> primaryMailboxFolderRecWrappers = new EntryIdMap<FolderRecWrapper>();
				destinationFolderMap.EnumerateFolderHierarchy(EnumHierarchyFlags.NormalFolders | EnumHierarchyFlags.RootFolder, delegate(FolderRecWrapper primaryFolderRecWrapper, FolderMap.EnumFolderContext enumFolderContext)
				{
					if (mbxCtx.IsContentAvailableInTargetMailbox(primaryFolderRecWrapper))
					{
						byte[] key = (byte[])primaryFolderRecWrapper.FolderRec[PropTag.LTID];
						primaryMailboxFolderRecWrappers[key] = primaryFolderRecWrapper;
					}
				});
				EntryIdMap<FolderRecWrapper> secondaryMailboxFolderRecWrappers = new EntryIdMap<FolderRecWrapper>();
				syncContext.TargetFolderMap.EnumerateFolderHierarchy(EnumHierarchyFlags.NormalFolders | EnumHierarchyFlags.RootFolder, delegate(FolderRecWrapper secondaryFolderRecWrapper, FolderMap.EnumFolderContext enumFolderContext)
				{
					if (mbxCtx.IsContentAvailableInTargetMailbox(secondaryFolderRecWrapper))
					{
						byte[] key = (byte[])secondaryFolderRecWrapper.FolderRec[PropTag.LTID];
						secondaryMailboxFolderRecWrappers[key] = secondaryFolderRecWrapper;
					}
				});
				MailboxChangesManifest mailboxChangesManifest = new MailboxChangesManifest();
				mailboxChangesManifest.ChangedFolders = new List<byte[]>();
				mailboxChangesManifest.DeletedFolders = new List<byte[]>();
				foreach (KeyValuePair<byte[], FolderRecWrapper> keyValuePair in primaryMailboxFolderRecWrappers)
				{
					FolderRecWrapper folderRecWrapper;
					if (!secondaryMailboxFolderRecWrappers.TryGetValue(keyValuePair.Key, out folderRecWrapper) || folderRecWrapper.FolderRec.LastModifyTimestamp != keyValuePair.Value.FolderRec.LastModifyTimestamp)
					{
						mailboxChangesManifest.ChangedFolders.Add(mbxCtx.SourceMailbox.GetSessionSpecificEntryId(keyValuePair.Key));
					}
				}
				foreach (KeyValuePair<byte[], FolderRecWrapper> keyValuePair2 in secondaryMailboxFolderRecWrappers)
				{
					FolderRecWrapper folderRecWrapper2;
					if (!primaryMailboxFolderRecWrappers.TryGetValue(keyValuePair2.Key, out folderRecWrapper2))
					{
						mailboxChangesManifest.DeletedFolders.Add(mbxCtx.SourceMailbox.GetSessionSpecificEntryId(keyValuePair2.Key));
					}
				}
				return mailboxChangesManifest;
			}
		}

		// Token: 0x0600078B RID: 1931 RVA: 0x000343B4 File Offset: 0x000325B4
		protected override void InternalDispose(bool calledFromDispose)
		{
			if (calledFromDispose)
			{
				if (this.SourceDatabasesWrapper != null)
				{
					this.SourceDatabasesWrapper.Dispose();
					this.SourceDatabasesWrapper = null;
				}
				if (this.publicFolderMigrators != null)
				{
					foreach (PublicFolderMigrator publicFolderMigrator in this.publicFolderMigrators.Values)
					{
						publicFolderMigrator.UnconfigureProviders();
					}
					this.publicFolderMigrators.Clear();
				}
			}
			base.InternalDispose(calledFromDispose);
		}

		// Token: 0x0600078C RID: 1932 RVA: 0x00034444 File Offset: 0x00032644
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<PublicFolderMigrationJob>(this);
		}

		// Token: 0x0600078D RID: 1933 RVA: 0x0003444C File Offset: 0x0003264C
		private void InternalConfigureProviders(bool continueAfterConfiguringProviders)
		{
			if (this.publicFolderConfiguration.GetHierarchyMailboxInformation().Type != PublicFolderInformation.HierarchyType.InTransitMailboxGuid)
			{
				throw new PublicFolderMailboxesNotProvisionedForMigrationException();
			}
			RequestStatisticsBase cachedRequestJob = base.CachedRequestJob;
			bool flag = cachedRequestJob.RequestStyle == RequestStyle.CrossOrg;
			LocalMailboxFlags localMailboxFlags = LocalMailboxFlags.LegacyPublicFolders;
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
			foreach (MailboxCopierBase mailboxCopierBase in this.GetAllCopiers())
			{
				PublicFolderMigrator publicFolderMigrator = (PublicFolderMigrator)mailboxCopierBase;
				publicFolderMigrator.SetSourceDatabasesWrapper(this.SourceDatabasesWrapper);
			}
			base.ConfigureProviders(continueAfterConfiguringProviders);
			MailboxCopierBase rootMailboxContext = base.GetRootMailboxContext();
			foreach (MailboxCopierBase mailboxCopierBase2 in this.GetAllCopiers())
			{
				PublicFolderMigrator publicFolderMigrator2 = (PublicFolderMigrator)mailboxCopierBase2;
				if (!publicFolderMigrator2.IsRoot)
				{
					publicFolderMigrator2.SetHierarchyMailbox(rootMailboxContext.DestMailbox);
				}
				if (flag)
				{
					publicFolderMigrator2.ConfigTranslators(new PrincipalTranslator(this.SourceDatabasesWrapper.PrincipalMapper, publicFolderMigrator2.DestMailboxWrapper.PrincipalMapper), null);
				}
			}
		}

		// Token: 0x04000303 RID: 771
		private TenantPublicFolderConfiguration publicFolderConfiguration;

		// Token: 0x04000304 RID: 772
		private MapiSourceMailbox sourceDatabases;

		// Token: 0x04000305 RID: 773
		private Dictionary<Guid, PublicFolderMigrator> publicFolderMigrators;
	}
}
