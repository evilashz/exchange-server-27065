using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.MailboxTransport.ContentAggregation.Schema;
using Microsoft.Exchange.Transport.Sync.Common;
using Microsoft.Exchange.Transport.Sync.Common.Logging;
using Microsoft.Exchange.Transport.Sync.Common.Subscription;
using Microsoft.Exchange.Transport.Sync.Common.Subscription.Connect;
using Microsoft.Exchange.Transport.Sync.Common.Subscription.Pim;
using Microsoft.Exchange.Transport.Sync.Worker.Health;
using Microsoft.Exchange.Transport.Sync.Worker.Throttling;

namespace Microsoft.Exchange.MailboxTransport.ContentAggregation
{
	// Token: 0x02000208 RID: 520
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal abstract class NativeSyncStorageProviderState : SyncStorageProviderStateBase
	{
		// Token: 0x0600119A RID: 4506 RVA: 0x0003971C File Offset: 0x0003791C
		internal NativeSyncStorageProviderState(SyncMailboxSession syncMailboxSession, ISyncWorkerData subscription, INativeStateStorage stateStorage, SyncLogSession syncLogSession, bool underRecovery) : base(subscription, syncLogSession, underRecovery)
		{
			this.stateStorage = stateStorage;
			this.syncMailboxSession = syncMailboxSession;
			this.failedFolderCreations = new Dictionary<string, Exception>(1);
		}

		// Token: 0x17000612 RID: 1554
		// (get) Token: 0x0600119B RID: 4507 RVA: 0x00039743 File Offset: 0x00037943
		// (set) Token: 0x0600119C RID: 4508 RVA: 0x00039751 File Offset: 0x00037951
		internal Queue<SyncChangeEntry> ItemsToProcess
		{
			get
			{
				base.CheckDisposed();
				return this.itemsToProcess;
			}
			set
			{
				base.CheckDisposed();
				this.itemsToProcess = value;
			}
		}

		// Token: 0x17000613 RID: 1555
		// (get) Token: 0x0600119D RID: 4509 RVA: 0x00039760 File Offset: 0x00037960
		internal SyncMailboxSession SyncMailboxSession
		{
			get
			{
				base.CheckDisposed();
				return this.syncMailboxSession;
			}
		}

		// Token: 0x17000614 RID: 1556
		// (get) Token: 0x0600119E RID: 4510 RVA: 0x0003976E File Offset: 0x0003796E
		internal bool TryCancel
		{
			get
			{
				base.CheckDisposed();
				return this.tryCancel;
			}
		}

		// Token: 0x17000615 RID: 1557
		// (get) Token: 0x0600119F RID: 4511 RVA: 0x0003977C File Offset: 0x0003797C
		internal INativeStateStorage StateStorage
		{
			get
			{
				base.CheckDisposed();
				return this.stateStorage;
			}
		}

		// Token: 0x17000616 RID: 1558
		// (get) Token: 0x060011A0 RID: 4512 RVA: 0x0003978A File Offset: 0x0003798A
		// (set) Token: 0x060011A1 RID: 4513 RVA: 0x00039798 File Offset: 0x00037998
		internal long EnumeratedItemsSize
		{
			get
			{
				base.CheckDisposed();
				return this.enumeratedItemsSize;
			}
			set
			{
				base.CheckDisposed();
				this.enumeratedItemsSize = value;
			}
		}

		// Token: 0x17000617 RID: 1559
		// (get) Token: 0x060011A2 RID: 4514 RVA: 0x000397A7 File Offset: 0x000379A7
		internal InboundConversionOptions ScopedInboundConversionOptions
		{
			get
			{
				base.CheckDisposed();
				if (this.scopedInboundConversionOptions == null)
				{
					this.scopedInboundConversionOptions = XSOSyncContentConversion.GetScopedInboundConversionOptions(this.ScopedADRecipientSession);
				}
				return this.scopedInboundConversionOptions;
			}
		}

		// Token: 0x17000618 RID: 1560
		// (get) Token: 0x060011A3 RID: 4515 RVA: 0x000397CE File Offset: 0x000379CE
		internal OutboundConversionOptions ScopedOutboundConversionOptions
		{
			get
			{
				base.CheckDisposed();
				if (this.scopedOutboundConversionOptions == null)
				{
					this.scopedOutboundConversionOptions = XSOSyncContentConversion.GetScopedOutboundConversionOptions(this.ScopedADRecipientSession);
				}
				return this.scopedOutboundConversionOptions;
			}
		}

		// Token: 0x17000619 RID: 1561
		// (get) Token: 0x060011A4 RID: 4516 RVA: 0x000397F8 File Offset: 0x000379F8
		private IRecipientSession ScopedADRecipientSession
		{
			get
			{
				base.CheckDisposed();
				if (this.scopedIRecipientSession == null)
				{
					ADSessionSettings adSettings = ADSessionSettings.FromOrganizationIdWithoutRbacScopesServiceOnly(this.syncMailboxSession.MailboxSession.MailboxOwner.MailboxInfo.OrganizationId);
					ExchangePrincipal exchangePrincipal = ExchangePrincipal.FromMailboxGuid(adSettings, this.syncMailboxSession.MailboxSession.MailboxGuid, null);
					this.scopedIRecipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(null, null, this.syncMailboxSession.MailboxSession.PreferedCulture.LCID, true, ConsistencyMode.IgnoreInvalid, null, exchangePrincipal.MailboxInfo.OrganizationId.ToADSessionSettings(), 256, "ScopedADRecipientSession", "f:\\15.00.1497\\sources\\dev\\transportSync\\src\\Worker\\Framework\\SyncEngine\\NativeSyncStorageProviderState.cs");
				}
				return this.scopedIRecipientSession;
			}
		}

		// Token: 0x1700061A RID: 1562
		// (get) Token: 0x060011A5 RID: 4517 RVA: 0x0003989D File Offset: 0x00037A9D
		internal TimeSpan AverageBackoffTime
		{
			get
			{
				base.CheckDisposed();
				return this.connectionStatistics.AverageBackoffTime;
			}
		}

		// Token: 0x1700061B RID: 1563
		// (get) Token: 0x060011A6 RID: 4518 RVA: 0x000398B0 File Offset: 0x00037AB0
		internal ThrottlingStatistics ThrottlingStatistics
		{
			get
			{
				base.CheckDisposed();
				return this.connectionStatistics.ThrottlingStatistics;
			}
		}

		// Token: 0x060011A7 RID: 4519
		internal abstract StoreObjectId EnsureInboxFolder(SyncChangeEntry change);

		// Token: 0x060011A8 RID: 4520 RVA: 0x00039914 File Offset: 0x00037B14
		internal StoreObjectId EnsureDefaultFolder(DefaultFolderType defaultFolderType)
		{
			base.CheckDisposed();
			StoreObjectId folderId = null;
			SyncStoreLoadManager.ThrottleAndExecuteStoreCall(this.syncMailboxSession.MailboxSession, new EventHandler<RoundtripCompleteEventArgs>(this.OnRoundtripComplete), "GetDefaultFolderId", delegate
			{
				folderId = this.syncMailboxSession.MailboxSession.GetDefaultFolderId(defaultFolderType);
			});
			if (folderId == null)
			{
				SyncStoreLoadManager.ThrottleAndExecuteStoreCall(this.syncMailboxSession.MailboxSession, new EventHandler<RoundtripCompleteEventArgs>(this.OnRoundtripComplete), "CreateDefaultFolder", delegate
				{
					folderId = this.syncMailboxSession.MailboxSession.CreateDefaultFolder(defaultFolderType);
				});
			}
			return folderId;
		}

		// Token: 0x060011A9 RID: 4521 RVA: 0x000399B3 File Offset: 0x00037BB3
		internal virtual bool IsInboxFolderId(StoreObjectId itemId)
		{
			base.CheckDisposed();
			if (this.inboxFolderId == null)
			{
				this.inboxFolderId = this.EnsureDefaultFolder(DefaultFolderType.Inbox);
			}
			return object.Equals(this.inboxFolderId, itemId);
		}

		// Token: 0x060011AA RID: 4522 RVA: 0x000399DC File Offset: 0x00037BDC
		internal void SetTryCancel()
		{
			this.tryCancel = true;
		}

		// Token: 0x060011AB RID: 4523 RVA: 0x000399E5 File Offset: 0x00037BE5
		internal void AddFailedFolderCreation(string cloudId, Exception exception)
		{
			SyncUtilities.ThrowIfArgumentNull("exception", exception);
			if (this.failedFolderCreations.ContainsKey(cloudId))
			{
				return;
			}
			this.failedFolderCreations.Add(cloudId, exception);
		}

		// Token: 0x060011AC RID: 4524 RVA: 0x00039A0E File Offset: 0x00037C0E
		internal bool TryGetFailedFolderCreation(string cloudId, out Exception exception)
		{
			exception = null;
			return !string.IsNullOrEmpty(cloudId) && this.failedFolderCreations.TryGetValue(cloudId, out exception);
		}

		// Token: 0x060011AD RID: 4525 RVA: 0x00039A2F File Offset: 0x00037C2F
		internal bool TryRemoveFailedFolderCreation(string cloudId)
		{
			return this.failedFolderCreations.Remove(cloudId);
		}

		// Token: 0x060011AE RID: 4526 RVA: 0x00039A40 File Offset: 0x00037C40
		internal DefaultFolderType GetDefaultFolderTypeForSubscription()
		{
			DefaultFolderType result;
			switch (base.Subscription.FolderSupport)
			{
			case FolderSupport.InboxOnly:
				result = DefaultFolderType.Inbox;
				break;
			case FolderSupport.ContactsOnly:
				result = DefaultFolderType.Contacts;
				break;
			default:
				return DefaultFolderType.None;
			}
			return result;
		}

		// Token: 0x060011AF RID: 4527 RVA: 0x00039A74 File Offset: 0x00037C74
		internal StoreObjectId GetDefaultFolderId(SyncChangeEntry itemChange)
		{
			if (base.Subscription.AggregationType == AggregationType.PeopleConnection)
			{
				if (this.peopleConnectFolderRetriever == null)
				{
					base.SyncLogSession.LogVerbose((TSLID)1369UL, "Initializing PeopleConnectFolderRetriever.", new object[0]);
					this.peopleConnectFolderRetriever = new OscFolderCreator(this.SyncMailboxSession.MailboxSession);
				}
				IConnectSubscription connectSubscription = (IConnectSubscription)base.Subscription;
				base.SyncLogSession.LogDebugging((TSLID)1491UL, "Retrieving folder Id for network: {0}; user id: {1}.", new object[]
				{
					connectSubscription.Name,
					connectSubscription.UserId
				});
				return this.peopleConnectFolderRetriever.Create(connectSubscription.Name, connectSubscription.UserId).FolderId;
			}
			DefaultFolderType defaultFolderType = DefaultFolderType.None;
			if (itemChange.SchemaType == SchemaType.Folder && itemChange.SyncObject != null)
			{
				defaultFolderType = ((SyncFolder)itemChange.SyncObject).DefaultFolderType;
			}
			if (defaultFolderType == DefaultFolderType.None)
			{
				defaultFolderType = this.GetDefaultFolderTypeForSubscription();
			}
			DefaultFolderType defaultFolderType2 = defaultFolderType;
			StoreObjectId storeObjectId;
			if (defaultFolderType2 != DefaultFolderType.None && defaultFolderType2 != DefaultFolderType.Inbox)
			{
				if (defaultFolderType2 == DefaultFolderType.Root)
				{
					storeObjectId = null;
				}
				else
				{
					storeObjectId = this.EnsureDefaultFolder(defaultFolderType);
				}
			}
			else
			{
				storeObjectId = this.EnsureInboxFolder(itemChange);
			}
			base.SyncLogSession.LogVerbose((TSLID)1073UL, "Item {0} mapped to default folder {1} with id {2}.", new object[]
			{
				itemChange,
				defaultFolderType,
				storeObjectId
			});
			return storeObjectId;
		}

		// Token: 0x04000999 RID: 2457
		private const int EstimatedFolderFailedCreations = 1;

		// Token: 0x0400099A RID: 2458
		private readonly SyncMailboxSession syncMailboxSession;

		// Token: 0x0400099B RID: 2459
		private readonly INativeStateStorage stateStorage;

		// Token: 0x0400099C RID: 2460
		private readonly Dictionary<string, Exception> failedFolderCreations;

		// Token: 0x0400099D RID: 2461
		private Queue<SyncChangeEntry> itemsToProcess;

		// Token: 0x0400099E RID: 2462
		private bool tryCancel;

		// Token: 0x0400099F RID: 2463
		private long enumeratedItemsSize;

		// Token: 0x040009A0 RID: 2464
		private IRecipientSession scopedIRecipientSession;

		// Token: 0x040009A1 RID: 2465
		private InboundConversionOptions scopedInboundConversionOptions;

		// Token: 0x040009A2 RID: 2466
		private OutboundConversionOptions scopedOutboundConversionOptions;

		// Token: 0x040009A3 RID: 2467
		private StoreObjectId inboxFolderId;

		// Token: 0x040009A4 RID: 2468
		private OscFolderCreator peopleConnectFolderRetriever;
	}
}
