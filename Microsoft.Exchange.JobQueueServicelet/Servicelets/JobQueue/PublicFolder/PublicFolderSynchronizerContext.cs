using System;
using System.Globalization;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.PublicFolder;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ServiceHost.PublicFolder;
using Microsoft.Exchange.MailboxReplicationService;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Servicelets.JobQueue.PublicFolder
{
	// Token: 0x02000016 RID: 22
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class PublicFolderSynchronizerContext : DisposeTrackableBase
	{
		// Token: 0x060000C1 RID: 193 RVA: 0x00006658 File Offset: 0x00004858
		public PublicFolderSynchronizerContext(OrganizationId organizationId, Guid contentMailboxGuid, bool isSingleFolderSync, bool executeReconcileFolders, Guid correlationId)
		{
			if (executeReconcileFolders && isSingleFolderSync)
			{
				throw new ArgumentException("Reconcile Folders can't be executed wit single folder syncs", "executeReconcileFolders");
			}
			this.OrganizationId = (organizationId ?? OrganizationId.ForestWideOrgId);
			this.ContentMailboxGuid = contentMailboxGuid;
			this.isSingleFolderSync = isSingleFolderSync;
			this.correlationId = correlationId;
			this.executeReconcileFolders = executeReconcileFolders;
			if (ExEnvironment.IsTest)
			{
				TenantPublicFolderConfigurationCache.Instance.RemoveValue(organizationId);
			}
			this.isHierarchyReady = this.ReadIsHierarchyReadyFlag();
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060000C2 RID: 194 RVA: 0x000066E4 File Offset: 0x000048E4
		// (set) Token: 0x060000C3 RID: 195 RVA: 0x000066EC File Offset: 0x000048EC
		public OrganizationId OrganizationId { get; private set; }

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060000C4 RID: 196 RVA: 0x000066F5 File Offset: 0x000048F5
		// (set) Token: 0x060000C5 RID: 197 RVA: 0x000066FD File Offset: 0x000048FD
		public Guid ContentMailboxGuid { get; private set; }

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060000C6 RID: 198 RVA: 0x00006706 File Offset: 0x00004906
		public bool IsSingleFolderSync
		{
			get
			{
				return this.isSingleFolderSync;
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060000C7 RID: 199 RVA: 0x0000670E File Offset: 0x0000490E
		public bool ExecuteReconcileFolders
		{
			get
			{
				return this.executeReconcileFolders;
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060000C8 RID: 200 RVA: 0x00006716 File Offset: 0x00004916
		public bool IsLoggerInitialized
		{
			get
			{
				return this.logger != null;
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060000C9 RID: 201 RVA: 0x00006724 File Offset: 0x00004924
		public bool IsHierarchyReady
		{
			get
			{
				return this.isHierarchyReady;
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060000CA RID: 202 RVA: 0x0000672C File Offset: 0x0000492C
		public ISourceMailbox SourceMailbox
		{
			get
			{
				if (this.sourceMailbox == null)
				{
					this.sourceMailbox = this.GetSourceMailbox();
				}
				return this.sourceMailbox;
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060000CB RID: 203 RVA: 0x00006748 File Offset: 0x00004948
		public IDestinationMailbox DestinationMailbox
		{
			get
			{
				if (this.destinationMailbox == null)
				{
					ExchangePrincipal exchangePrincipal;
					if (!PublicFolderSession.TryGetPublicFolderMailboxPrincipal(this.OrganizationId, this.ContentMailboxGuid, false, out exchangePrincipal))
					{
						throw new StoragePermanentException(PublicFolderSession.GetNoPublicFoldersProvisionedError(this.OrganizationId));
					}
					this.destinationMailbox = new StorageDestinationMailbox(LocalMailboxFlags.None);
					this.destinationMailbox.Config(null, exchangePrincipal.MailboxInfo.MailboxGuid, exchangePrincipal.MailboxInfo.MailboxGuid, TenantPartitionHint.FromOrganizationId(this.OrganizationId), exchangePrincipal.MailboxInfo.GetDatabaseGuid(), MailboxType.DestMailboxIntraOrg, null);
					this.destinationMailbox.Connect(MailboxConnectFlags.PublicFolderHierarchyReplication);
					if (!this.destinationMailbox.MailboxExists())
					{
						using (PublicFolderSession.OpenAsAdmin(this.OrganizationId, null, this.ContentMailboxGuid, null, CultureInfo.CurrentCulture, "Client=PublicFolderSystem;Action=PublicFolderHierarchyReplication", null))
						{
						}
						this.destinationMailbox.Connect(MailboxConnectFlags.PublicFolderHierarchyReplication);
					}
				}
				return this.destinationMailbox;
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060000CC RID: 204 RVA: 0x0000683C File Offset: 0x00004A3C
		public WellKnownPublicFolders SourceWellKnownFolders
		{
			get
			{
				if (this.sourceWellKnownFolders == null)
				{
					this.sourceWellKnownFolders = new WellKnownPublicFolders(DataConverter<PropValueConverter, PropValue, PropValueData>.GetNative(this.SourceMailbox.GetProps(WellKnownPublicFolders.EntryIdPropTags)));
				}
				return this.sourceWellKnownFolders;
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060000CD RID: 205 RVA: 0x0000686C File Offset: 0x00004A6C
		public WellKnownPublicFolders DestinationWellKnownFolders
		{
			get
			{
				if (this.destinationWellKnownFolders == null)
				{
					this.destinationWellKnownFolders = new WellKnownPublicFolders(DataConverter<PropValueConverter, PropValue, PropValueData>.GetNative(this.DestinationMailbox.GetProps(WellKnownPublicFolders.EntryIdPropTags)));
				}
				return this.destinationWellKnownFolders;
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060000CE RID: 206 RVA: 0x0000689C File Offset: 0x00004A9C
		public Guid CorrelationId
		{
			get
			{
				return this.correlationId;
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060000CF RID: 207 RVA: 0x000068A4 File Offset: 0x00004AA4
		public FolderOperationCounter FolderOperationCount
		{
			get
			{
				return this.folderOperationCount;
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060000D0 RID: 208 RVA: 0x000068AC File Offset: 0x00004AAC
		public SyncStateCounter SyncStateCounter
		{
			get
			{
				return this.syncStateCounter;
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060000D1 RID: 209 RVA: 0x000068B4 File Offset: 0x00004AB4
		public PublicFolderSynchronizerLogger Logger
		{
			get
			{
				if (this.isSingleFolderSync)
				{
					return null;
				}
				if (this.logger == null)
				{
					this.logger = new PublicFolderSynchronizerLogger(this.DestinationMailboxSession, this.FolderOperationCount, this.CorrelationId);
				}
				return this.logger;
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060000D2 RID: 210 RVA: 0x000068EB File Offset: 0x00004AEB
		public LatencyInfo MRSProxyLatencyInfo
		{
			get
			{
				return ((RemoteMailbox)this.SourceMailbox).MrsProxyClient.LatencyInfo;
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060000D3 RID: 211 RVA: 0x00006904 File Offset: 0x00004B04
		public PublicFolderSession DestinationMailboxSession
		{
			get
			{
				if (this.destinationMailboxSession == null)
				{
					ExchangePrincipal publicFolderMailboxPrincipal;
					if (!PublicFolderSession.TryGetPublicFolderMailboxPrincipal(this.OrganizationId, this.ContentMailboxGuid, false, out publicFolderMailboxPrincipal))
					{
						throw new StoragePermanentException(PublicFolderSession.GetNoPublicFoldersProvisionedError(this.OrganizationId));
					}
					this.destinationMailboxSession = PublicFolderSession.OpenAsAdmin(null, publicFolderMailboxPrincipal, null, CultureInfo.CurrentCulture, "Client=PublicFolderSystem;Action=PublicFolderHierarchyReplication", null);
				}
				return this.destinationMailboxSession;
			}
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x00006960 File Offset: 0x00004B60
		public byte[] MapSourceToDestinationFolderId(byte[] folderId, out bool isWellKnownFolder)
		{
			WellKnownPublicFolders.FolderType? folderType;
			isWellKnownFolder = ((folderId.Length > 22) ? this.SourceWellKnownFolders.GetFolderType(folderId, out folderType) : this.SourceWellKnownFolders.TryGetFolderTypeFromLongTermId(folderId, out folderType));
			if (isWellKnownFolder)
			{
				return this.DestinationWellKnownFolders.GetFolderId(folderType.Value);
			}
			return this.DestinationMailbox.GetSessionSpecificEntryId(folderId);
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x000069B8 File Offset: 0x00004BB8
		public void ResetDestinationMailboxConnection()
		{
			if (this.destinationMailbox != null)
			{
				this.destinationMailbox.Dispose();
				this.destinationMailbox = null;
			}
			if (this.logger != null)
			{
				this.logger.Dispose();
				this.logger = null;
			}
			if (this.destinationMailboxSession != null)
			{
				this.destinationMailboxSession.Dispose();
				this.destinationMailboxSession = null;
			}
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x00006A13 File Offset: 0x00004C13
		public void ResetSourceMailboxConnection()
		{
			if (this.sourceMailbox != null)
			{
				this.sourceMailbox.Dispose();
				this.sourceMailbox = null;
			}
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x00006A30 File Offset: 0x00004C30
		public IRecipientSession GetADSession()
		{
			ADSessionSettings sessionSettings = this.OrganizationId.ToADSessionSettings();
			return DirectorySessionFactory.NonCacheSessionFactory.GetTenantOrRootOrgRecipientSession(false, ConsistencyMode.IgnoreInvalid, sessionSettings, 421, "GetADSession", "f:\\15.00.1497\\sources\\dev\\Management\\src\\ServiceHost\\Servicelets\\JobQueue\\Program\\PublicFolderSynchronizerContext.cs");
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x00006A65 File Offset: 0x00004C65
		public ADRecipient GetContentMailboxADRecipient(IRecipientSession session = null)
		{
			if (session == null)
			{
				session = this.GetADSession();
			}
			return session.FindByExchangeGuid(this.ContentMailboxGuid);
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x00006A7E File Offset: 0x00004C7E
		protected override void InternalDispose(bool disposing)
		{
			if (disposing)
			{
				this.ResetSourceMailboxConnection();
				this.ResetDestinationMailboxConnection();
			}
		}

		// Token: 0x060000DA RID: 218 RVA: 0x00006A8F File Offset: 0x00004C8F
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<PublicFolderSynchronizerContext>(this);
		}

		// Token: 0x060000DB RID: 219 RVA: 0x00006A98 File Offset: 0x00004C98
		private bool ReadIsHierarchyReadyFlag()
		{
			ADRecipient contentMailboxADRecipient = this.GetContentMailboxADRecipient(null);
			return contentMailboxADRecipient != null && (bool)contentMailboxADRecipient[ADRecipientSchema.IsHierarchyReady];
		}

		// Token: 0x060000DC RID: 220 RVA: 0x00006AC4 File Offset: 0x00004CC4
		private ISourceMailbox GetSourceMailbox()
		{
			ISourceMailbox result;
			using (DisposeGuard disposeGuard = default(DisposeGuard))
			{
				ExchangePrincipal exchangePrincipal;
				if (!PublicFolderSession.TryGetPublicFolderMailboxPrincipal(this.OrganizationId, PublicFolderSession.HierarchyMailboxGuidAlias, true, out exchangePrincipal))
				{
					throw new StoragePermanentException(PublicFolderSession.GetNoPublicFoldersProvisionedError(this.OrganizationId));
				}
				ISourceMailbox sourceMailbox = new RemoteSourceMailbox(exchangePrincipal.MailboxInfo.Location.ServerFqdn, null, null, this.isSingleFolderSync ? ProxyControlFlags.SkipWLMThrottling : ProxyControlFlags.None, PublicFolderSynchronizerContext.RequiredCapabilities, false, LocalMailboxFlags.None);
				disposeGuard.Add<ISourceMailbox>(sourceMailbox);
				TenantPartitionHint partitionHint = CommonUtils.GetPartitionHint(this.OrganizationId);
				if (this.Logger != null)
				{
					this.Logger.LogEvent(LogEventType.Verbose, string.Format("Connecting to Primary Hierarchy. [Mailbox:{0}; Server:{1}; Database:{2}; PartitionHint:{3}]", new object[]
					{
						exchangePrincipal.MailboxInfo.MailboxGuid,
						exchangePrincipal.MailboxInfo.Location.ServerFqdn,
						exchangePrincipal.MailboxInfo.GetDatabaseGuid(),
						partitionHint
					}));
				}
				sourceMailbox.Config(null, exchangePrincipal.MailboxInfo.MailboxGuid, exchangePrincipal.MailboxInfo.MailboxGuid, partitionHint, exchangePrincipal.MailboxInfo.GetDatabaseGuid(), MailboxType.SourceMailbox, null);
				sourceMailbox.Connect(MailboxConnectFlags.PublicFolderHierarchyReplication);
				disposeGuard.Success();
				result = sourceMailbox;
			}
			return result;
		}

		// Token: 0x0400007B RID: 123
		private static readonly Trace Tracer = ExTraceGlobals.PublicFolderSynchronizerTracer;

		// Token: 0x0400007C RID: 124
		private static readonly MRSProxyCapabilities[] RequiredCapabilities = new MRSProxyCapabilities[]
		{
			MRSProxyCapabilities.PublicFolderMigration
		};

		// Token: 0x0400007D RID: 125
		private static readonly BudgetKey BudgetKey = new UnthrottledBudgetKey("PublicFolderSync", BudgetType.ResourceTracking);

		// Token: 0x0400007E RID: 126
		private readonly bool isSingleFolderSync;

		// Token: 0x0400007F RID: 127
		private readonly bool executeReconcileFolders;

		// Token: 0x04000080 RID: 128
		private readonly FolderOperationCounter folderOperationCount = new FolderOperationCounter();

		// Token: 0x04000081 RID: 129
		private readonly SyncStateCounter syncStateCounter = new SyncStateCounter();

		// Token: 0x04000082 RID: 130
		private readonly Guid correlationId;

		// Token: 0x04000083 RID: 131
		private readonly bool isHierarchyReady;

		// Token: 0x04000084 RID: 132
		private ISourceMailbox sourceMailbox;

		// Token: 0x04000085 RID: 133
		private IDestinationMailbox destinationMailbox;

		// Token: 0x04000086 RID: 134
		private WellKnownPublicFolders sourceWellKnownFolders;

		// Token: 0x04000087 RID: 135
		private WellKnownPublicFolders destinationWellKnownFolders;

		// Token: 0x04000088 RID: 136
		private PublicFolderSynchronizerLogger logger;

		// Token: 0x04000089 RID: 137
		private PublicFolderSession destinationMailboxSession;
	}
}
