using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.MailboxTransport.ContentAggregation;
using Microsoft.Exchange.MailboxTransport.ContentAggregation.Schema;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Transport.Sync.Common;
using Microsoft.Exchange.Transport.Sync.Common.Logging;
using Microsoft.Exchange.Transport.Sync.Common.Subscription;
using Microsoft.Exchange.Transport.Sync.Common.Subscription.Imap;
using Microsoft.Exchange.Transport.Sync.Worker.Framework.Provider.IMAP.Client;

namespace Microsoft.Exchange.Transport.Sync.Worker.Framework.Provider.IMAP
{
	// Token: 0x020001E0 RID: 480
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class IMAPSyncStorageProviderState : SyncStorageProviderState, ISyncSourceSession
	{
		// Token: 0x06000F51 RID: 3921 RVA: 0x0002FE68 File Offset: 0x0002E068
		internal IMAPSyncStorageProviderState(ISyncWorkerData subscription, SyncLogSession syncLogSession, bool underRecovery) : this(AggregationConfiguration.Instance.MaxDownloadSizePerItem, AggregationConfiguration.Instance.GetMaxDownloadItemsPerConnection(subscription.AggregationType), (long)AggregationConfiguration.Instance.GetMaxDownloadSizePerConnection(subscription.AggregationType).ToBytes(), subscription, syncLogSession, underRecovery, new EventHandler<DownloadCompleteEventArgs>(FrameworkPerfCounterHandler.Instance.OnImapSyncDownloadCompletion))
		{
			this.clientState = new IMAPClientState(this.imapSubscription.IMAPServer, this.imapSubscription.IMAPPort, this.imapSubscription.IMAPLogOnName, this.imapSubscription.LogonPasswordSecured, this.imapSubscription.ImapPathPrefix, base.SyncLogSession, this.sessionId, this.imapSubscription.SubscriptionGuid, this.imapSubscription.IMAPAuthentication, this.imapSubscription.IMAPSecurity, this.imapSubscription.AggregationType, this.maxDownloadBytesAllowed, AggregationConfiguration.Instance.RemoteConnectionTimeout, new EventHandler<DownloadCompleteEventArgs>(base.InternalOnDownloadCompletion), new EventHandler<EventArgs>(FrameworkPerfCounterHandler.Instance.OnImapSyncMessageDownloadCompletion), new EventHandler<EventArgs>(FrameworkPerfCounterHandler.Instance.OnImapSyncMessageUploadCompletion), new EventHandler<RoundtripCompleteEventArgs>(this.OnRoundtripComplete));
			base.SyncLogSession.LogDebugging((TSLID)810UL, "Built IMAPSyncStorageProviderState", new object[0]);
		}

		// Token: 0x06000F52 RID: 3922 RVA: 0x0002FFA4 File Offset: 0x0002E1A4
		internal IMAPSyncStorageProviderState(ICommClient commClient, long maxSizePerItem, int maxItemsPerSession, long maxSizePerSession, ISyncWorkerData subscription, SyncLogSession syncLogSession, bool underRecovery, EventHandler<DownloadCompleteEventArgs> downloadCompleted) : this(maxSizePerItem, maxItemsPerSession, maxSizePerSession, subscription, syncLogSession, underRecovery, downloadCompleted)
		{
			this.clientState = new IMAPClientState(commClient, this.imapSubscription.IMAPLogOnName, this.imapSubscription.LogonPasswordSecured, this.imapSubscription.ImapPathPrefix, base.SyncLogSession, this.imapSubscription.IMAPAuthentication, this.imapSubscription.IMAPSecurity);
		}

		// Token: 0x06000F53 RID: 3923 RVA: 0x0003000C File Offset: 0x0002E20C
		private IMAPSyncStorageProviderState(long maxSizePerItem, int maxItemsPerSession, long maxSizePerSession, ISyncWorkerData subscription, SyncLogSession syncLogSession, bool underRecovery, EventHandler<DownloadCompleteEventArgs> downloadCompleted) : base(subscription, syncLogSession, underRecovery, downloadCompleted)
		{
			this.sessionId = SyncUtilities.GetNextSessionId();
			this.imapSubscription = (IMAPAggregationSubscription)subscription;
			this.maxDownloadSizePerItem = maxSizePerItem;
			this.maxDownloadBytesAllowed = maxSizePerSession;
			this.maxDownloadItemsPerConnection = maxItemsPerSession;
			this.cloudIdToFolderMap = new Dictionary<string, IMAPFolder>(5, StringComparer.Ordinal);
			this.cloudItemChangeMap = new Dictionary<string, string>(5);
		}

		// Token: 0x17000560 RID: 1376
		// (get) Token: 0x06000F54 RID: 3924 RVA: 0x00030074 File Offset: 0x0002E274
		internal string SyncWatermark
		{
			get
			{
				base.CheckDisposed();
				string result;
				this.ImapWatermark.Load(out result);
				return result;
			}
		}

		// Token: 0x17000561 RID: 1377
		// (get) Token: 0x06000F55 RID: 3925 RVA: 0x00030095 File Offset: 0x0002E295
		internal StringWatermark ImapWatermark
		{
			get
			{
				return (StringWatermark)base.BaseWatermark;
			}
		}

		// Token: 0x17000562 RID: 1378
		// (get) Token: 0x06000F56 RID: 3926 RVA: 0x000300A2 File Offset: 0x0002E2A2
		// (set) Token: 0x06000F57 RID: 3927 RVA: 0x000300B0 File Offset: 0x0002E2B0
		internal IDictionary<string, string> CloudItemChangeMap
		{
			get
			{
				base.CheckDisposed();
				return this.cloudItemChangeMap;
			}
			set
			{
				base.CheckDisposed();
				this.cloudItemChangeMap = value;
			}
		}

		// Token: 0x17000563 RID: 1379
		// (get) Token: 0x06000F58 RID: 3928 RVA: 0x000300BF File Offset: 0x0002E2BF
		// (set) Token: 0x06000F59 RID: 3929 RVA: 0x000300CD File Offset: 0x0002E2CD
		internal Dictionary<DefaultFolderType, string> PreprocessedDefaultMappings
		{
			get
			{
				base.CheckDisposed();
				return this.preprocessedDefaultMappings;
			}
			set
			{
				base.CheckDisposed();
				this.preprocessedDefaultMappings = value;
			}
		}

		// Token: 0x17000564 RID: 1380
		// (get) Token: 0x06000F5A RID: 3930 RVA: 0x000300DC File Offset: 0x0002E2DC
		// (set) Token: 0x06000F5B RID: 3931 RVA: 0x000300EA File Offset: 0x0002E2EA
		internal SortedList<string, SyncChangeEntry> SortedFolderAddsSyncChangeEntries
		{
			get
			{
				base.CheckDisposed();
				return this.sortedFolderAddsSyncChangeEntries;
			}
			set
			{
				base.CheckDisposed();
				this.sortedFolderAddsSyncChangeEntries = value;
			}
		}

		// Token: 0x17000565 RID: 1381
		// (get) Token: 0x06000F5C RID: 3932 RVA: 0x000300F9 File Offset: 0x0002E2F9
		// (set) Token: 0x06000F5D RID: 3933 RVA: 0x00030107 File Offset: 0x0002E307
		internal Exception LastSelectFailedFolderException
		{
			get
			{
				base.CheckDisposed();
				return this.lastSelectFailedFolderException;
			}
			set
			{
				base.CheckDisposed();
				this.lastSelectFailedFolderException = value;
			}
		}

		// Token: 0x17000566 RID: 1382
		// (get) Token: 0x06000F5E RID: 3934 RVA: 0x00030116 File Offset: 0x0002E316
		// (set) Token: 0x06000F5F RID: 3935 RVA: 0x00030124 File Offset: 0x0002E324
		internal long EstimatedMessageBytesToDownload
		{
			get
			{
				base.CheckDisposed();
				return this.estimatedMessageBytesToDownload;
			}
			set
			{
				base.CheckDisposed();
				this.estimatedMessageBytesToDownload = value;
			}
		}

		// Token: 0x17000567 RID: 1383
		// (get) Token: 0x06000F60 RID: 3936 RVA: 0x00030133 File Offset: 0x0002E333
		// (set) Token: 0x06000F61 RID: 3937 RVA: 0x00030141 File Offset: 0x0002E341
		internal int? LowestAttemptedSequenceNumber
		{
			get
			{
				base.CheckDisposed();
				return this.lowestAttemptedSequenceNumber;
			}
			set
			{
				base.CheckDisposed();
				this.lowestAttemptedSequenceNumber = value;
			}
		}

		// Token: 0x17000568 RID: 1384
		// (get) Token: 0x06000F62 RID: 3938 RVA: 0x00030150 File Offset: 0x0002E350
		// (set) Token: 0x06000F63 RID: 3939 RVA: 0x0003015E File Offset: 0x0002E35E
		internal string LastAppendMessageId
		{
			get
			{
				base.CheckDisposed();
				return this.lastAppendMessageId;
			}
			set
			{
				base.CheckDisposed();
				this.lastAppendMessageId = value;
			}
		}

		// Token: 0x17000569 RID: 1385
		// (get) Token: 0x06000F64 RID: 3940 RVA: 0x0003016D File Offset: 0x0002E36D
		// (set) Token: 0x06000F65 RID: 3941 RVA: 0x0003017B File Offset: 0x0002E37B
		internal Stream LastAppendMessageMimeStream
		{
			get
			{
				base.CheckDisposed();
				return this.lastAppendMessageMimeStream;
			}
			set
			{
				base.CheckDisposed();
				this.lastAppendMessageMimeStream = value;
			}
		}

		// Token: 0x1700056A RID: 1386
		// (get) Token: 0x06000F66 RID: 3942 RVA: 0x0003018A File Offset: 0x0002E38A
		// (set) Token: 0x06000F67 RID: 3943 RVA: 0x00030198 File Offset: 0x0002E398
		internal bool MoreItemsAvailable
		{
			get
			{
				base.CheckDisposed();
				return this.moreItemsAvailable;
			}
			set
			{
				base.CheckDisposed();
				this.moreItemsAvailable = value;
			}
		}

		// Token: 0x1700056B RID: 1387
		// (get) Token: 0x06000F68 RID: 3944 RVA: 0x000301A7 File Offset: 0x0002E3A7
		// (set) Token: 0x06000F69 RID: 3945 RVA: 0x000301B5 File Offset: 0x0002E3B5
		internal IEnumerator<string> CloudFolderEnumerator
		{
			get
			{
				base.CheckDisposed();
				return this.cloudFolderEnumerator;
			}
			set
			{
				base.CheckDisposed();
				this.cloudFolderEnumerator = value;
			}
		}

		// Token: 0x1700056C RID: 1388
		// (get) Token: 0x06000F6A RID: 3946 RVA: 0x000301C4 File Offset: 0x0002E3C4
		// (set) Token: 0x06000F6B RID: 3947 RVA: 0x000301D2 File Offset: 0x0002E3D2
		public bool CanTrackItemCount
		{
			get
			{
				base.CheckDisposed();
				return this.canTrackItemCount;
			}
			set
			{
				base.CheckDisposed();
				this.canTrackItemCount = value;
			}
		}

		// Token: 0x1700056D RID: 1389
		// (get) Token: 0x06000F6C RID: 3948 RVA: 0x000301E1 File Offset: 0x0002E3E1
		// (set) Token: 0x06000F6D RID: 3949 RVA: 0x000301EF File Offset: 0x0002E3EF
		internal IEnumerator<IMAPFolder> NewCloudFolderEnumerator
		{
			get
			{
				base.CheckDisposed();
				return this.newCloudFolderEnumerator;
			}
			set
			{
				base.CheckDisposed();
				this.newCloudFolderEnumerator = value;
			}
		}

		// Token: 0x1700056E RID: 1390
		// (get) Token: 0x06000F6E RID: 3950 RVA: 0x000301FE File Offset: 0x0002E3FE
		// (set) Token: 0x06000F6F RID: 3951 RVA: 0x0003020C File Offset: 0x0002E40C
		internal IEnumerator<IMAPFolder> CheckForChangesCloudFolderEnumerator
		{
			get
			{
				base.CheckDisposed();
				return this.checkForChangesCloudFolderEnumerator;
			}
			set
			{
				base.CheckDisposed();
				this.checkForChangesCloudFolderEnumerator = value;
			}
		}

		// Token: 0x1700056F RID: 1391
		// (get) Token: 0x06000F70 RID: 3952 RVA: 0x0003021B File Offset: 0x0002E41B
		// (set) Token: 0x06000F71 RID: 3953 RVA: 0x00030229 File Offset: 0x0002E429
		internal bool LightFetchDone
		{
			get
			{
				base.CheckDisposed();
				return this.lightFetchDone;
			}
			set
			{
				base.CheckDisposed();
				this.lightFetchDone = value;
			}
		}

		// Token: 0x17000570 RID: 1392
		// (get) Token: 0x06000F72 RID: 3954 RVA: 0x00030238 File Offset: 0x0002E438
		public string Protocol
		{
			get
			{
				base.CheckDisposed();
				return "IMAP";
			}
		}

		// Token: 0x17000571 RID: 1393
		// (get) Token: 0x06000F73 RID: 3955 RVA: 0x00030245 File Offset: 0x0002E445
		public string SessionId
		{
			get
			{
				base.CheckDisposed();
				return this.sessionId;
			}
		}

		// Token: 0x17000572 RID: 1394
		// (get) Token: 0x06000F74 RID: 3956 RVA: 0x00030253 File Offset: 0x0002E453
		public string Server
		{
			get
			{
				base.CheckDisposed();
				return this.imapSubscription.IMAPServer;
			}
		}

		// Token: 0x17000573 RID: 1395
		// (get) Token: 0x06000F75 RID: 3957 RVA: 0x0003026B File Offset: 0x0002E46B
		internal IMAPClientState ClientState
		{
			get
			{
				base.CheckDisposed();
				return this.clientState;
			}
		}

		// Token: 0x17000574 RID: 1396
		// (get) Token: 0x06000F76 RID: 3958 RVA: 0x00030279 File Offset: 0x0002E479
		internal int CurrentFolderListLevel
		{
			get
			{
				base.CheckDisposed();
				return this.currentFolderListLevel;
			}
		}

		// Token: 0x17000575 RID: 1397
		// (get) Token: 0x06000F77 RID: 3959 RVA: 0x00030287 File Offset: 0x0002E487
		internal bool AnyFoldersExistAtPreviousLevel
		{
			get
			{
				base.CheckDisposed();
				return this.anyFoldersExistAtPreviousLevel;
			}
		}

		// Token: 0x17000576 RID: 1398
		// (get) Token: 0x06000F78 RID: 3960 RVA: 0x00030295 File Offset: 0x0002E495
		internal IDictionary<string, IMAPFolder> CloudIdToFolder
		{
			get
			{
				base.CheckDisposed();
				return this.cloudIdToFolderMap;
			}
		}

		// Token: 0x17000577 RID: 1399
		// (get) Token: 0x06000F79 RID: 3961 RVA: 0x000302A3 File Offset: 0x0002E4A3
		// (set) Token: 0x06000F7A RID: 3962 RVA: 0x000302B1 File Offset: 0x0002E4B1
		internal IMAPFolder CurrentFolder
		{
			get
			{
				base.CheckDisposed();
				return this.currentFolder;
			}
			set
			{
				base.CheckDisposed();
				this.currentFolder = value;
			}
		}

		// Token: 0x17000578 RID: 1400
		// (get) Token: 0x06000F7B RID: 3963 RVA: 0x000302C0 File Offset: 0x0002E4C0
		// (set) Token: 0x06000F7C RID: 3964 RVA: 0x000302CE File Offset: 0x0002E4CE
		internal string PendingExpungeCloudFolderId
		{
			get
			{
				base.CheckDisposed();
				return this.pendingExpungeCloudFolderId;
			}
			set
			{
				base.CheckDisposed();
				this.pendingExpungeCloudFolderId = value;
			}
		}

		// Token: 0x17000579 RID: 1401
		// (get) Token: 0x06000F7D RID: 3965 RVA: 0x000302DD File Offset: 0x0002E4DD
		// (set) Token: 0x06000F7E RID: 3966 RVA: 0x000302EB File Offset: 0x0002E4EB
		internal IDictionary<string, string> MessageIdToUidMap
		{
			get
			{
				base.CheckDisposed();
				return this.messageIdToUidMap;
			}
			set
			{
				base.CheckDisposed();
				this.messageIdToUidMap = value;
			}
		}

		// Token: 0x1700057A RID: 1402
		// (get) Token: 0x06000F7F RID: 3967 RVA: 0x000302FA File Offset: 0x0002E4FA
		// (set) Token: 0x06000F80 RID: 3968 RVA: 0x00030308 File Offset: 0x0002E508
		internal IMAPSyncStorageProviderState.PostProcessor PostUidReconciliationCallback
		{
			get
			{
				base.CheckDisposed();
				return this.postUidReconciliationCallback;
			}
			set
			{
				base.CheckDisposed();
				this.postUidReconciliationCallback = value;
			}
		}

		// Token: 0x1700057B RID: 1403
		// (get) Token: 0x06000F81 RID: 3969 RVA: 0x00030317 File Offset: 0x0002E517
		// (set) Token: 0x06000F82 RID: 3970 RVA: 0x00030325 File Offset: 0x0002E525
		internal IEnumerator<SyncChangeEntry> ApplyChangesEnumerator
		{
			get
			{
				base.CheckDisposed();
				return this.applyChangesEnumerator;
			}
			set
			{
				base.CheckDisposed();
				this.applyChangesEnumerator = value;
			}
		}

		// Token: 0x1700057C RID: 1404
		// (get) Token: 0x06000F83 RID: 3971 RVA: 0x00030334 File Offset: 0x0002E534
		// (set) Token: 0x06000F84 RID: 3972 RVA: 0x00030342 File Offset: 0x0002E542
		internal int MaxDownloadItemsPerConnection
		{
			get
			{
				base.CheckDisposed();
				return this.maxDownloadItemsPerConnection;
			}
			set
			{
				base.CheckDisposed();
				this.maxDownloadItemsPerConnection = value;
			}
		}

		// Token: 0x1700057D RID: 1405
		// (get) Token: 0x06000F85 RID: 3973 RVA: 0x00030351 File Offset: 0x0002E551
		internal long MaxDownloadSizePerItem
		{
			get
			{
				base.CheckDisposed();
				return this.maxDownloadSizePerItem;
			}
		}

		// Token: 0x1700057E RID: 1406
		// (get) Token: 0x06000F86 RID: 3974 RVA: 0x0003035F File Offset: 0x0002E55F
		internal long MaxDownloadBytesAllowed
		{
			get
			{
				base.CheckDisposed();
				return this.maxDownloadBytesAllowed;
			}
		}

		// Token: 0x1700057F RID: 1407
		// (get) Token: 0x06000F87 RID: 3975 RVA: 0x0003036D File Offset: 0x0002E56D
		internal long TotalBytesSent
		{
			get
			{
				base.CheckDisposed();
				return this.ClientState.CommClient.TotalBytesSent;
			}
		}

		// Token: 0x17000580 RID: 1408
		// (get) Token: 0x06000F88 RID: 3976 RVA: 0x00030385 File Offset: 0x0002E585
		internal long TotalBytesReceived
		{
			get
			{
				base.CheckDisposed();
				return this.ClientState.CommClient.TotalBytesReceived;
			}
		}

		// Token: 0x17000581 RID: 1409
		// (get) Token: 0x06000F89 RID: 3977 RVA: 0x0003039D File Offset: 0x0002E59D
		// (set) Token: 0x06000F8A RID: 3978 RVA: 0x000303AB File Offset: 0x0002E5AB
		internal IMAPFolder LastSelectFailedFolder
		{
			get
			{
				base.CheckDisposed();
				return this.lastSelectFailedFolder;
			}
			set
			{
				base.CheckDisposed();
				this.lastSelectFailedFolder = value;
			}
		}

		// Token: 0x17000582 RID: 1410
		// (get) Token: 0x06000F8B RID: 3979 RVA: 0x000303BA File Offset: 0x0002E5BA
		// (set) Token: 0x06000F8C RID: 3980 RVA: 0x000303C8 File Offset: 0x0002E5C8
		internal IMAPMailFlags LastAppendMessageFlags
		{
			get
			{
				base.CheckDisposed();
				return this.lastAppendMessageFlags;
			}
			set
			{
				base.CheckDisposed();
				this.lastAppendMessageFlags = value;
			}
		}

		// Token: 0x17000583 RID: 1411
		// (get) Token: 0x06000F8D RID: 3981 RVA: 0x000303D7 File Offset: 0x0002E5D7
		// (set) Token: 0x06000F8E RID: 3982 RVA: 0x000303E5 File Offset: 0x0002E5E5
		internal Queue<IMAPFolder> ApplyDeleteFolders
		{
			get
			{
				base.CheckDisposed();
				return this.applyDeleteFolders;
			}
			set
			{
				base.CheckDisposed();
				this.applyDeleteFolders = value;
			}
		}

		// Token: 0x17000584 RID: 1412
		// (get) Token: 0x06000F8F RID: 3983 RVA: 0x000303F4 File Offset: 0x0002E5F4
		// (set) Token: 0x06000F90 RID: 3984 RVA: 0x00030402 File Offset: 0x0002E602
		internal SyncChangeEntry LastAppendMessageChange
		{
			get
			{
				base.CheckDisposed();
				return this.lastAppendMessageChange;
			}
			set
			{
				base.CheckDisposed();
				this.lastAppendMessageChange = value;
			}
		}

		// Token: 0x17000585 RID: 1413
		// (get) Token: 0x06000F91 RID: 3985 RVA: 0x00030411 File Offset: 0x0002E611
		// (set) Token: 0x06000F92 RID: 3986 RVA: 0x0003041F File Offset: 0x0002E61F
		internal bool RequiresLogOff
		{
			get
			{
				base.CheckDisposed();
				return this.requiresLogOff;
			}
			set
			{
				base.CheckDisposed();
				this.requiresLogOff = value;
			}
		}

		// Token: 0x17000586 RID: 1414
		// (get) Token: 0x06000F93 RID: 3987 RVA: 0x0003042E File Offset: 0x0002E62E
		// (set) Token: 0x06000F94 RID: 3988 RVA: 0x0003043C File Offset: 0x0002E63C
		internal bool IsListLevelsComplete
		{
			get
			{
				base.CheckDisposed();
				return this.isListLevelsComplete;
			}
			set
			{
				base.CheckDisposed();
				this.isListLevelsComplete = value;
			}
		}

		// Token: 0x17000587 RID: 1415
		// (get) Token: 0x06000F95 RID: 3989 RVA: 0x0003044B File Offset: 0x0002E64B
		// (set) Token: 0x06000F96 RID: 3990 RVA: 0x00030459 File Offset: 0x0002E659
		internal bool WasEnumerateEntered
		{
			get
			{
				base.CheckDisposed();
				return this.wasEnumerateEntered;
			}
			set
			{
				base.CheckDisposed();
				this.wasEnumerateEntered = value;
			}
		}

		// Token: 0x06000F97 RID: 3991 RVA: 0x00030468 File Offset: 0x0002E668
		internal void SetInitialFolderLevel()
		{
			base.CheckDisposed();
			this.currentFolderListLevel = 1;
			this.anyFoldersExistAtPreviousLevel = true;
		}

		// Token: 0x06000F98 RID: 3992 RVA: 0x0003047E File Offset: 0x0002E67E
		internal void AdvanceToNextFolderLevel(bool anyFoldersAtThisLevel)
		{
			base.CheckDisposed();
			this.currentFolderListLevel++;
			this.anyFoldersExistAtPreviousLevel = anyFoldersAtThisLevel;
		}

		// Token: 0x06000F99 RID: 3993 RVA: 0x0003049C File Offset: 0x0002E69C
		internal void StoreDefaultFolderMapping(DefaultFolderType folderType, string mailboxName)
		{
			base.CheckDisposed();
			int num = (int)folderType;
			string property = num.ToString(CultureInfo.InvariantCulture);
			if (base.StateStorage.ContainsProperty(property))
			{
				base.StateStorage.ChangePropertyValue(property, mailboxName);
				return;
			}
			base.StateStorage.AddProperty(property, mailboxName);
		}

		// Token: 0x06000F9A RID: 3994 RVA: 0x000304E8 File Offset: 0x0002E6E8
		internal string GetDefaultFolderMapping(DefaultFolderType folderType)
		{
			base.CheckDisposed();
			int num = (int)folderType;
			string property = num.ToString(CultureInfo.InvariantCulture);
			string result;
			base.StateStorage.TryGetPropertyValue(property, out result);
			return result;
		}

		// Token: 0x06000F9B RID: 3995 RVA: 0x0003051A File Offset: 0x0002E71A
		internal char GetSeparatorCharacter()
		{
			base.CheckDisposed();
			return this.GetSeparatorCharacter(this.CurrentFolder);
		}

		// Token: 0x06000F9C RID: 3996 RVA: 0x00030530 File Offset: 0x0002E730
		internal char GetSeparatorCharacter(IMAPFolder incomingFolder)
		{
			base.CheckDisposed();
			IMAPFolder imapfolder = incomingFolder;
			if (imapfolder != null && imapfolder.Mailbox.Separator != null)
			{
				base.SyncLogSession.LogDebugging((TSLID)1406UL, IMAPSyncStorageProvider.Tracer, "Using the specified mailbox {0} hierarchy separator '{1}'.", new object[]
				{
					imapfolder.Mailbox.Name,
					imapfolder.Mailbox.Separator.Value
				});
				return imapfolder.Mailbox.Separator.Value;
			}
			if (this.CloudIdToFolder.TryGetValue("INBOX", out imapfolder) && imapfolder.Mailbox.Separator != null)
			{
				base.SyncLogSession.LogDebugging((TSLID)1407UL, IMAPSyncStorageProvider.Tracer, "Using the INBOX mailbox hierarchy separator '{0}'.", new object[]
				{
					imapfolder.Mailbox.Separator.Value
				});
				return imapfolder.Mailbox.Separator.Value;
			}
			base.SyncLogSession.LogDebugging((TSLID)811UL, IMAPSyncStorageProvider.Tracer, "Using default hierarchy separator.", new object[0]);
			return IMAPFolder.DefaultHierarchySeparator;
		}

		// Token: 0x06000F9D RID: 3997 RVA: 0x00030674 File Offset: 0x0002E874
		internal char GetSeparatorCharacter(string cloudFolderId)
		{
			base.CheckDisposed();
			IMAPFolder incomingFolder = null;
			if (cloudFolderId != null && this.CloudIdToFolder.ContainsKey(cloudFolderId))
			{
				incomingFolder = this.CloudIdToFolder[cloudFolderId];
			}
			return this.GetSeparatorCharacter(incomingFolder);
		}

		// Token: 0x06000F9E RID: 3998 RVA: 0x000306B0 File Offset: 0x0002E8B0
		internal void AppendToSyncWatermark(string folderName, long? uidValidity, long? uidNext)
		{
			base.CheckDisposed();
			if (this.syncWatermarkBuilder == null)
			{
				this.syncWatermarkBuilder = new StringBuilder(100);
			}
			this.syncWatermarkBuilder.Append(folderName);
			this.syncWatermarkBuilder.Append(uidValidity);
			this.syncWatermarkBuilder.Append(uidNext);
		}

		// Token: 0x06000F9F RID: 3999 RVA: 0x00030709 File Offset: 0x0002E909
		internal string ComputeSyncWatermark()
		{
			base.CheckDisposed();
			if (this.syncWatermarkBuilder == null)
			{
				return SyncUtilities.ComputeSHA512Hash(string.Empty);
			}
			return SyncUtilities.ComputeSHA512Hash(this.syncWatermarkBuilder.ToString());
		}

		// Token: 0x06000FA0 RID: 4000 RVA: 0x00030734 File Offset: 0x0002E934
		internal void UpdateSubscriptionSyncWatermarkIfNeeded()
		{
			if (this.MoreItemsAvailable)
			{
				return;
			}
			string watermark = this.ComputeSyncWatermark();
			this.ImapWatermark.Save(watermark);
		}

		// Token: 0x06000FA1 RID: 4001 RVA: 0x00030760 File Offset: 0x0002E960
		internal void UpdateMailboxItemCountFromCurrentFolderData()
		{
			if (this.CurrentFolder.NumberOfMessages == null)
			{
				return;
			}
			if (base.CloudStatistics.TotalItemsInSourceMailbox == null || base.CloudStatistics.TotalItemsInSourceMailbox.Value == SyncUtilities.DataNotAvailable)
			{
				base.CloudStatistics.TotalItemsInSourceMailbox = new long?(this.CurrentFolder.NumberOfMessages.Value);
				return;
			}
			base.CloudStatistics.TotalItemsInSourceMailbox += this.CurrentFolder.NumberOfMessages.Value;
		}

		// Token: 0x06000FA2 RID: 4002 RVA: 0x00030824 File Offset: 0x0002EA24
		protected override void InternalDispose(bool disposing)
		{
			base.SyncLogSession.LogDebugging((TSLID)812UL, "InternalDispose({0})", new object[]
			{
				disposing
			});
			if (disposing && this.clientState != null)
			{
				this.clientState.Dispose();
				this.clientState = null;
			}
		}

		// Token: 0x06000FA3 RID: 4003 RVA: 0x0003087A File Offset: 0x0002EA7A
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<IMAPSyncStorageProviderState>(this);
		}

		// Token: 0x04000869 RID: 2153
		internal const int DefaultChangeListSize = 5;

		// Token: 0x0400086A RID: 2154
		internal const int DefaultFolderListSize = 5;

		// Token: 0x0400086B RID: 2155
		internal const string OrphanedCloudVersion = "ORPHANED";

		// Token: 0x0400086C RID: 2156
		internal const int NumFetchBatchSize = 100;

		// Token: 0x0400086D RID: 2157
		internal const string ImapVersionString = "imap4rev1";

		// Token: 0x0400086E RID: 2158
		private const string IMAPProtocolName = "IMAP";

		// Token: 0x0400086F RID: 2159
		private const int InitialSyncWatermarkCapacity = 100;

		// Token: 0x04000870 RID: 2160
		internal static readonly string LastMessageSyncKey = "LastMessageSyncKey";

		// Token: 0x04000871 RID: 2161
		private string sessionId;

		// Token: 0x04000872 RID: 2162
		private IMAPAggregationSubscription imapSubscription;

		// Token: 0x04000873 RID: 2163
		private IMAPClientState clientState;

		// Token: 0x04000874 RID: 2164
		private int maxDownloadItemsPerConnection;

		// Token: 0x04000875 RID: 2165
		private long maxDownloadSizePerItem;

		// Token: 0x04000876 RID: 2166
		private long maxDownloadBytesAllowed;

		// Token: 0x04000877 RID: 2167
		private bool canTrackItemCount;

		// Token: 0x04000878 RID: 2168
		private Dictionary<DefaultFolderType, string> preprocessedDefaultMappings;

		// Token: 0x04000879 RID: 2169
		private long estimatedMessageBytesToDownload;

		// Token: 0x0400087A RID: 2170
		private SortedList<string, SyncChangeEntry> sortedFolderAddsSyncChangeEntries;

		// Token: 0x0400087B RID: 2171
		private int currentFolderListLevel;

		// Token: 0x0400087C RID: 2172
		private bool anyFoldersExistAtPreviousLevel;

		// Token: 0x0400087D RID: 2173
		private string pendingExpungeCloudFolderId;

		// Token: 0x0400087E RID: 2174
		private IDictionary<string, IMAPFolder> cloudIdToFolderMap;

		// Token: 0x0400087F RID: 2175
		private IDictionary<string, string> cloudItemChangeMap;

		// Token: 0x04000880 RID: 2176
		private IDictionary<string, string> messageIdToUidMap;

		// Token: 0x04000881 RID: 2177
		private IMAPSyncStorageProviderState.PostProcessor postUidReconciliationCallback;

		// Token: 0x04000882 RID: 2178
		private IMAPFolder currentFolder;

		// Token: 0x04000883 RID: 2179
		private IMAPFolder lastSelectFailedFolder;

		// Token: 0x04000884 RID: 2180
		private Exception lastSelectFailedFolderException;

		// Token: 0x04000885 RID: 2181
		private int? lowestAttemptedSequenceNumber;

		// Token: 0x04000886 RID: 2182
		private bool lightFetchDone;

		// Token: 0x04000887 RID: 2183
		private string lastAppendMessageId;

		// Token: 0x04000888 RID: 2184
		private IMAPMailFlags lastAppendMessageFlags;

		// Token: 0x04000889 RID: 2185
		private Stream lastAppendMessageMimeStream;

		// Token: 0x0400088A RID: 2186
		private IEnumerator<SyncChangeEntry> applyChangesEnumerator;

		// Token: 0x0400088B RID: 2187
		private IEnumerator<string> cloudFolderEnumerator;

		// Token: 0x0400088C RID: 2188
		private IEnumerator<IMAPFolder> newCloudFolderEnumerator;

		// Token: 0x0400088D RID: 2189
		private IEnumerator<IMAPFolder> checkForChangesCloudFolderEnumerator;

		// Token: 0x0400088E RID: 2190
		private Queue<IMAPFolder> applyDeleteFolders;

		// Token: 0x0400088F RID: 2191
		private SyncChangeEntry lastAppendMessageChange;

		// Token: 0x04000890 RID: 2192
		private bool moreItemsAvailable;

		// Token: 0x04000891 RID: 2193
		private bool requiresLogOff;

		// Token: 0x04000892 RID: 2194
		private StringBuilder syncWatermarkBuilder;

		// Token: 0x04000893 RID: 2195
		private bool isListLevelsComplete;

		// Token: 0x04000894 RID: 2196
		private bool wasEnumerateEntered;

		// Token: 0x020001E1 RID: 481
		// (Invoke) Token: 0x06000FA6 RID: 4006
		internal delegate void PostProcessor(IAsyncResult curOp, Exception exceptionDuringReconciliation);
	}
}
