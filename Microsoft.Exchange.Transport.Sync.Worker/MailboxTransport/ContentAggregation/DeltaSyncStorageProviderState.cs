using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.MailboxTransport.ContentAggregation.Schema;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Net.LiveIDAuthentication;
using Microsoft.Exchange.Net.Logging;
using Microsoft.Exchange.Net.Protocols.DeltaSync;
using Microsoft.Exchange.Net.Protocols.DeltaSync.SettingsResponse;
using Microsoft.Exchange.Net.Protocols.DeltaSync.SyncResponse;
using Microsoft.Exchange.Transport.Sync.Common;
using Microsoft.Exchange.Transport.Sync.Common.Exceptions;
using Microsoft.Exchange.Transport.Sync.Common.Logging;
using Microsoft.Exchange.Transport.Sync.Common.Subscription;
using Microsoft.Exchange.Transport.Sync.Common.Subscription.DeltaSync;

namespace Microsoft.Exchange.MailboxTransport.ContentAggregation
{
	// Token: 0x0200005B RID: 91
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class DeltaSyncStorageProviderState : SyncStorageProviderState, ISyncSourceSession
	{
		// Token: 0x06000429 RID: 1065 RVA: 0x0001462C File Offset: 0x0001282C
		internal DeltaSyncStorageProviderState(ISyncWorkerData subscription, SyncLogSession syncLogSession, bool underRecovery, IWebProxy proxyServer, int remoteConnectionTimeout, EventHandler<DownloadCompleteEventArgs> downloadCompletedEventHandler, EventHandler<EventArgs> messageDownloadedEventHandler, EventHandler<EventArgs> messageUploadedEventHandler, int maxDownloadItemsPerConnection, long maxDownloadSizePerConnection, long maxDownloadSizePerItem, ProtocolLog httpProtocolLog, TimeSpan deltaSyncSettingsUpdateInterval, int maxDownloadItemsInFirstDeltaSyncConnection, DeltaSyncClientFactory deltaSyncClientFactory) : base(subscription, syncLogSession, underRecovery, downloadCompletedEventHandler)
		{
			if (this.DeltaSyncSubscription.LogonPasswordSecured != null && this.DeltaSyncSubscription.LogonPasswordSecured.Length > 0)
			{
				this.deltaSyncUserAccount = DeltaSyncUserAccount.CreateDeltaSyncUserForPassportAuth(this.DeltaSyncSubscription.LogonName, SyncUtilities.SecureStringToString(this.DeltaSyncSubscription.LogonPasswordSecured));
				if (!string.IsNullOrEmpty(this.DeltaSyncSubscription.AuthPolicy))
				{
					this.deltaSyncUserAccount.AuthPolicy = this.DeltaSyncSubscription.AuthPolicy;
				}
				if (!string.IsNullOrEmpty(this.DeltaSyncSubscription.AuthToken) && !string.IsNullOrEmpty(this.DeltaSyncSubscription.Puid))
				{
					this.deltaSyncUserAccount.AuthToken = new AuthenticationToken(this.DeltaSyncSubscription.AuthToken, this.DeltaSyncSubscription.AuthTokenExpirationTime, null, this.DeltaSyncSubscription.Puid);
				}
			}
			else
			{
				string puid = this.DeltaSyncSubscription.Puid ?? string.Empty;
				this.deltaSyncUserAccount = DeltaSyncUserAccount.CreateDeltaSyncUserForTrustedPartnerAuthWithPuid(this.DeltaSyncSubscription.LogonName, puid);
			}
			this.deltaSyncUserAccount.DeltaSyncServer = this.DeltaSyncSubscription.IncommingServerUrl;
			this.deltaSyncClient = deltaSyncClientFactory.Create(this.deltaSyncUserAccount, remoteConnectionTimeout, proxyServer, maxDownloadSizePerItem, httpProtocolLog, syncLogSession, new EventHandler<RoundtripCompleteEventArgs>(this.OnRoundtripComplete));
			this.deltaSyncClient.SubscribeDownloadCompletedEvent(new EventHandler<DownloadCompleteEventArgs>(base.InternalOnDownloadCompletion));
			this.MessageDownloaded += messageDownloadedEventHandler;
			this.MessageUploaded += messageUploadedEventHandler;
			this.maxEmailChangesEnumerated = ((maxDownloadItemsPerConnection > 0) ? maxDownloadItemsPerConnection : int.MaxValue);
			this.maxDownloadSizePerConnection = ((maxDownloadSizePerConnection > 0L) ? maxDownloadSizePerConnection : long.MaxValue);
			this.maxDownloadSizePerItem = ((maxDownloadSizePerItem > 0L) ? maxDownloadSizePerItem : long.MaxValue);
			this.maxDownloadItemsInFirstDeltaSyncConnection = maxDownloadItemsInFirstDeltaSyncConnection;
			this.deltaSyncSettingsUpdateInterval = deltaSyncSettingsUpdateInterval;
		}

		// Token: 0x14000009 RID: 9
		// (add) Token: 0x0600042A RID: 1066 RVA: 0x000147F8 File Offset: 0x000129F8
		// (remove) Token: 0x0600042B RID: 1067 RVA: 0x00014830 File Offset: 0x00012A30
		private event EventHandler<EventArgs> MessageDownloaded;

		// Token: 0x1400000A RID: 10
		// (add) Token: 0x0600042C RID: 1068 RVA: 0x00014868 File Offset: 0x00012A68
		// (remove) Token: 0x0600042D RID: 1069 RVA: 0x000148A0 File Offset: 0x00012AA0
		private event EventHandler<EventArgs> MessageUploaded;

		// Token: 0x17000172 RID: 370
		// (get) Token: 0x0600042E RID: 1070 RVA: 0x000148D5 File Offset: 0x00012AD5
		string ISyncSourceSession.Protocol
		{
			get
			{
				base.CheckDisposed();
				return "DeltaSync";
			}
		}

		// Token: 0x17000173 RID: 371
		// (get) Token: 0x0600042F RID: 1071 RVA: 0x000148E2 File Offset: 0x00012AE2
		string ISyncSourceSession.SessionId
		{
			get
			{
				base.CheckDisposed();
				return string.Empty;
			}
		}

		// Token: 0x17000174 RID: 372
		// (get) Token: 0x06000430 RID: 1072 RVA: 0x000148EF File Offset: 0x00012AEF
		string ISyncSourceSession.Server
		{
			get
			{
				base.CheckDisposed();
				return string.Empty;
			}
		}

		// Token: 0x17000175 RID: 373
		// (get) Token: 0x06000431 RID: 1073 RVA: 0x000148FC File Offset: 0x00012AFC
		internal static ConflictResolution ConflictResolution
		{
			get
			{
				return DeltaSyncStorageProviderState.conflictResolution;
			}
		}

		// Token: 0x17000176 RID: 374
		// (get) Token: 0x06000432 RID: 1074 RVA: 0x00014903 File Offset: 0x00012B03
		internal IDeltaSyncClient DeltaSyncClient
		{
			get
			{
				base.CheckDisposed();
				return this.deltaSyncClient;
			}
		}

		// Token: 0x17000177 RID: 375
		// (get) Token: 0x06000433 RID: 1075 RVA: 0x00014911 File Offset: 0x00012B11
		// (set) Token: 0x06000434 RID: 1076 RVA: 0x0001491F File Offset: 0x00012B1F
		internal DeltaSyncResultData DeltaSyncResultData
		{
			get
			{
				base.CheckDisposed();
				return this.deltaSyncResultData;
			}
			set
			{
				base.CheckDisposed();
				this.deltaSyncResultData = value;
			}
		}

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x06000435 RID: 1077 RVA: 0x0001492E File Offset: 0x00012B2E
		// (set) Token: 0x06000436 RID: 1078 RVA: 0x0001493C File Offset: 0x00012B3C
		internal string LatestFolderSyncKey
		{
			get
			{
				base.CheckDisposed();
				return this.latestFolderSyncKey;
			}
			set
			{
				base.CheckDisposed();
				this.latestFolderSyncKey = value;
			}
		}

		// Token: 0x17000179 RID: 377
		// (get) Token: 0x06000437 RID: 1079 RVA: 0x0001494B File Offset: 0x00012B4B
		// (set) Token: 0x06000438 RID: 1080 RVA: 0x00014959 File Offset: 0x00012B59
		internal string LatestEmailSyncKey
		{
			get
			{
				base.CheckDisposed();
				return this.latestEmailSyncKey;
			}
			set
			{
				base.CheckDisposed();
				this.latestEmailSyncKey = value;
			}
		}

		// Token: 0x1700017A RID: 378
		// (get) Token: 0x06000439 RID: 1081 RVA: 0x00014968 File Offset: 0x00012B68
		internal long MaxMessageSize
		{
			get
			{
				base.CheckDisposed();
				return this.DeltaSyncSubscription.MaxMessageSize;
			}
		}

		// Token: 0x1700017B RID: 379
		// (get) Token: 0x0600043A RID: 1082 RVA: 0x0001497C File Offset: 0x00012B7C
		internal bool HasLatestSettings
		{
			get
			{
				base.CheckDisposed();
				return this.LastSettingsSyncTime != null && ExDateTime.UtcNow - this.LastSettingsSyncTime.Value <= this.deltaSyncSettingsUpdateInterval;
			}
		}

		// Token: 0x1700017C RID: 380
		// (get) Token: 0x0600043B RID: 1083 RVA: 0x000149C4 File Offset: 0x00012BC4
		// (set) Token: 0x0600043C RID: 1084 RVA: 0x000149D2 File Offset: 0x00012BD2
		internal List<DeltaSyncOperation> DeltaSyncOperations
		{
			get
			{
				base.CheckDisposed();
				return this.deltaSyncOperations;
			}
			set
			{
				base.CheckDisposed();
				this.deltaSyncOperations = value;
			}
		}

		// Token: 0x1700017D RID: 381
		// (get) Token: 0x0600043D RID: 1085 RVA: 0x000149E1 File Offset: 0x00012BE1
		// (set) Token: 0x0600043E RID: 1086 RVA: 0x000149EF File Offset: 0x00012BEF
		internal Dictionary<string, SyncChangeEntry> EmailIdMapping
		{
			get
			{
				base.CheckDisposed();
				return this.emailIdMapping;
			}
			set
			{
				base.CheckDisposed();
				this.emailIdMapping = value;
			}
		}

		// Token: 0x1700017E RID: 382
		// (get) Token: 0x0600043F RID: 1087 RVA: 0x000149FE File Offset: 0x00012BFE
		// (set) Token: 0x06000440 RID: 1088 RVA: 0x00014A0C File Offset: 0x00012C0C
		internal Dictionary<string, SyncChangeEntry> FolderAddList
		{
			get
			{
				base.CheckDisposed();
				return this.folderAddList;
			}
			set
			{
				base.CheckDisposed();
				this.folderAddList = value;
			}
		}

		// Token: 0x1700017F RID: 383
		// (get) Token: 0x06000441 RID: 1089 RVA: 0x00014A1B File Offset: 0x00012C1B
		// (set) Token: 0x06000442 RID: 1090 RVA: 0x00014A29 File Offset: 0x00012C29
		internal Dictionary<StoreObjectId, string> NativeToTempFolderIdMapping
		{
			get
			{
				base.CheckDisposed();
				return this.nativeToTempFolderIdMapping;
			}
			set
			{
				base.CheckDisposed();
				this.nativeToTempFolderIdMapping = value;
			}
		}

		// Token: 0x17000180 RID: 384
		// (get) Token: 0x06000443 RID: 1091 RVA: 0x00014A38 File Offset: 0x00012C38
		// (set) Token: 0x06000444 RID: 1092 RVA: 0x00014A46 File Offset: 0x00012C46
		internal Dictionary<StoreObjectId, string> NativeToCloudFolderIdMapping
		{
			get
			{
				base.CheckDisposed();
				return this.nativeToCloudFolderIdMapping;
			}
			set
			{
				base.CheckDisposed();
				this.nativeToCloudFolderIdMapping = value;
			}
		}

		// Token: 0x17000181 RID: 385
		// (get) Token: 0x06000445 RID: 1093 RVA: 0x00014A55 File Offset: 0x00012C55
		// (set) Token: 0x06000446 RID: 1094 RVA: 0x00014A63 File Offset: 0x00012C63
		internal Dictionary<string, SyncChangeEntry> ChangeList
		{
			get
			{
				base.CheckDisposed();
				return this.changeList;
			}
			set
			{
				base.CheckDisposed();
				this.changeList = value;
			}
		}

		// Token: 0x17000182 RID: 386
		// (get) Token: 0x06000447 RID: 1095 RVA: 0x00014A72 File Offset: 0x00012C72
		// (set) Token: 0x06000448 RID: 1096 RVA: 0x00014A80 File Offset: 0x00012C80
		internal List<SyncChangeEntry> EmailAddList
		{
			get
			{
				base.CheckDisposed();
				return this.emailAddList;
			}
			set
			{
				base.CheckDisposed();
				this.emailAddList = value;
			}
		}

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x06000449 RID: 1097 RVA: 0x00014A8F File Offset: 0x00012C8F
		// (set) Token: 0x0600044A RID: 1098 RVA: 0x00014A9D File Offset: 0x00012C9D
		internal int EmailIndex
		{
			get
			{
				base.CheckDisposed();
				return this.emailIndex;
			}
			set
			{
				base.CheckDisposed();
				this.emailIndex = value;
			}
		}

		// Token: 0x17000184 RID: 388
		// (get) Token: 0x0600044B RID: 1099 RVA: 0x00014AAC File Offset: 0x00012CAC
		// (set) Token: 0x0600044C RID: 1100 RVA: 0x00014ABA File Offset: 0x00012CBA
		internal bool ChangesLoaded
		{
			get
			{
				base.CheckDisposed();
				return this.changesLoaded;
			}
			set
			{
				base.CheckDisposed();
				this.changesLoaded = value;
			}
		}

		// Token: 0x17000185 RID: 389
		// (get) Token: 0x0600044D RID: 1101 RVA: 0x00014AC9 File Offset: 0x00012CC9
		internal long MaxDownloadSizePerItem
		{
			get
			{
				base.CheckDisposed();
				return this.maxDownloadSizePerItem;
			}
		}

		// Token: 0x17000186 RID: 390
		// (get) Token: 0x0600044E RID: 1102 RVA: 0x00014AD7 File Offset: 0x00012CD7
		internal long MaxDownloadSizePerConnection
		{
			get
			{
				base.CheckDisposed();
				return this.maxDownloadSizePerConnection;
			}
		}

		// Token: 0x17000187 RID: 391
		// (get) Token: 0x0600044F RID: 1103 RVA: 0x00014AE8 File Offset: 0x00012CE8
		internal int MaxEmailChangesEnumeratedInThisSync
		{
			get
			{
				base.CheckDisposed();
				if (this.deltaSyncUserAccount.EmailSyncKey == DeltaSyncCommon.DefaultSyncKey)
				{
					base.SyncLogSession.LogVerbose((TSLID)3000UL, "First Sync Connection (EmailSyncKey = DefaultSyncKey) - using maxDownloadItemsInFirstDeltaSyncConnection {0} instead of maxEmailChangesEnumerated {1}.", new object[]
					{
						this.maxDownloadItemsInFirstDeltaSyncConnection,
						this.maxEmailChangesEnumerated
					});
					return this.maxDownloadItemsInFirstDeltaSyncConnection;
				}
				return this.maxEmailChangesEnumerated;
			}
		}

		// Token: 0x17000188 RID: 392
		// (get) Token: 0x06000450 RID: 1104 RVA: 0x00014B5E File Offset: 0x00012D5E
		// (set) Token: 0x06000451 RID: 1105 RVA: 0x00014B6C File Offset: 0x00012D6C
		internal int CloudItemsSynced
		{
			get
			{
				base.CheckDisposed();
				return this.cloudItemsSynced;
			}
			set
			{
				base.CheckDisposed();
				this.cloudItemsSynced = value;
			}
		}

		// Token: 0x17000189 RID: 393
		// (get) Token: 0x06000452 RID: 1106 RVA: 0x00014B7B File Offset: 0x00012D7B
		// (set) Token: 0x06000453 RID: 1107 RVA: 0x00014B89 File Offset: 0x00012D89
		internal bool CloudMoreItemsAvailable
		{
			get
			{
				base.CheckDisposed();
				return this.cloudMoreItemsAvailable;
			}
			set
			{
				base.CheckDisposed();
				this.cloudMoreItemsAvailable = value;
			}
		}

		// Token: 0x1700018A RID: 394
		// (get) Token: 0x06000454 RID: 1108 RVA: 0x00014B98 File Offset: 0x00012D98
		internal DeltaSyncAggregationSubscription DeltaSyncSubscription
		{
			get
			{
				base.CheckDisposed();
				return (DeltaSyncAggregationSubscription)base.Subscription;
			}
		}

		// Token: 0x1700018B RID: 395
		// (get) Token: 0x06000455 RID: 1109 RVA: 0x00014BAB File Offset: 0x00012DAB
		internal bool HasFolderAndEmailCollectionCached
		{
			get
			{
				base.CheckDisposed();
				return this.hasFolderAndEmailCollectionCached;
			}
		}

		// Token: 0x1700018C RID: 396
		// (get) Token: 0x06000456 RID: 1110 RVA: 0x00014BB9 File Offset: 0x00012DB9
		internal Collection CachedFolderCollection
		{
			get
			{
				base.CheckDisposed();
				return this.cachedFolderCollection;
			}
		}

		// Token: 0x1700018D RID: 397
		// (get) Token: 0x06000457 RID: 1111 RVA: 0x00014BC7 File Offset: 0x00012DC7
		internal Collection CachedEmailCollection
		{
			get
			{
				base.CheckDisposed();
				return this.cachedEmailCollection;
			}
		}

		// Token: 0x1700018E RID: 398
		// (get) Token: 0x06000458 RID: 1112 RVA: 0x00014BD5 File Offset: 0x00012DD5
		internal DeltaSyncWatermark DeltaSyncWatermark
		{
			get
			{
				return (DeltaSyncWatermark)base.BaseWatermark;
			}
		}

		// Token: 0x1700018F RID: 399
		// (get) Token: 0x06000459 RID: 1113 RVA: 0x00014BE4 File Offset: 0x00012DE4
		// (set) Token: 0x0600045A RID: 1114 RVA: 0x00014C24 File Offset: 0x00012E24
		private ExDateTime? LastSettingsSyncTime
		{
			get
			{
				string s;
				ExDateTime value;
				if (base.StateStorage.TryGetPropertyValue("LastSettingsSyncTime", out s) && ExDateTime.TryParse(ExTimeZone.UtcTimeZone, s, out value))
				{
					return new ExDateTime?(value);
				}
				return null;
			}
			set
			{
				if (value == null)
				{
					base.StateStorage.TryRemoveProperty("LastSettingsSyncTime");
					return;
				}
				string value2 = value.Value.ToUtc().ToString();
				if (base.StateStorage.ContainsProperty("LastSettingsSyncTime"))
				{
					base.StateStorage.ChangePropertyValue("LastSettingsSyncTime", value2);
					return;
				}
				base.StateStorage.AddProperty("LastSettingsSyncTime", value2);
			}
		}

		// Token: 0x0600045B RID: 1115 RVA: 0x00014C9F File Offset: 0x00012E9F
		public override string ToString()
		{
			return base.ToString() + ", " + this.deltaSyncUserAccount.ToString();
		}

		// Token: 0x0600045C RID: 1116 RVA: 0x00014CBC File Offset: 0x00012EBC
		internal void ForceGetSettingsInNextSync()
		{
			base.CheckDisposed();
			this.LastSettingsSyncTime = null;
		}

		// Token: 0x0600045D RID: 1117 RVA: 0x00014CDE File Offset: 0x00012EDE
		internal void RecordSettingsSyncTime()
		{
			base.CheckDisposed();
			this.LastSettingsSyncTime = new ExDateTime?(ExDateTime.UtcNow);
		}

		// Token: 0x0600045E RID: 1118 RVA: 0x00014CF8 File Offset: 0x00012EF8
		internal void UpdateWaterMark()
		{
			base.CheckDisposed();
			if (this.latestEmailSyncKey == null && this.latestFolderSyncKey == null)
			{
				throw new InvalidOperationException("At least one of the Sync Keys (EmailSyncKey,FolderSyncKey) should be not null");
			}
			string folderSyncKey = this.latestFolderSyncKey ?? DeltaSyncCommon.DefaultSyncKey;
			string emailSyncKey = this.latestEmailSyncKey ?? DeltaSyncCommon.DefaultSyncKey;
			this.DeltaSyncWatermark.Save(folderSyncKey, emailSyncKey);
		}

		// Token: 0x0600045F RID: 1119 RVA: 0x00014D54 File Offset: 0x00012F54
		internal void UpdateDeltaSyncClientWithWaterMark()
		{
			base.CheckDisposed();
			string text;
			string text2;
			this.DeltaSyncWatermark.Load(out text, out text2);
			this.deltaSyncUserAccount.FolderSyncKey = text;
			base.SyncLogSession.LogVerbose((TSLID)645UL, "DS Client Folder Sync Key Updated: [{0}]", new object[]
			{
				text
			});
			this.deltaSyncUserAccount.EmailSyncKey = text2;
			base.SyncLogSession.LogVerbose((TSLID)646UL, "DS Client Email Sync Key Updated: [{0}]", new object[]
			{
				text2
			});
		}

		// Token: 0x06000460 RID: 1120 RVA: 0x00014DDC File Offset: 0x00012FDC
		internal void CacheDeltaSyncServerUrl()
		{
			base.CheckDisposed();
			this.DeltaSyncSubscription.IncommingServerUrl = this.deltaSyncUserAccount.DeltaSyncServer;
		}

		// Token: 0x06000461 RID: 1121 RVA: 0x00014DFA File Offset: 0x00012FFA
		internal void CacheGetChangesResults(Collection folderCollection, Collection emailCollection)
		{
			base.CheckDisposed();
			SyncUtilities.ThrowIfArgumentNull("folderCollection", folderCollection);
			SyncUtilities.ThrowIfArgumentNull("emailCollection", emailCollection);
			this.cachedFolderCollection = folderCollection;
			this.cachedEmailCollection = emailCollection;
			this.hasFolderAndEmailCollectionCached = true;
		}

		// Token: 0x06000462 RID: 1122 RVA: 0x00014E2D File Offset: 0x0001302D
		internal void TriggerMessageDownloadedEvent(object sender, EventArgs eventArgs)
		{
			base.CheckDisposed();
			if (this.MessageDownloaded != null)
			{
				this.MessageDownloaded(sender, eventArgs);
			}
		}

		// Token: 0x06000463 RID: 1123 RVA: 0x00014E4A File Offset: 0x0001304A
		internal void TriggerMessageUploadedEvent(object sender, EventArgs eventArgs)
		{
			base.CheckDisposed();
			if (this.MessageUploaded != null)
			{
				this.MessageUploaded(sender, eventArgs);
			}
		}

		// Token: 0x06000464 RID: 1124 RVA: 0x00014E68 File Offset: 0x00013068
		internal void ClearApplyChangesState()
		{
			base.CheckDisposed();
			if (this.changesLoaded)
			{
				if (this.changeList != null)
				{
					DeltaSyncStorageProviderState.DisposeSyncObjects(this.changeList.Values);
					this.changeList.Clear();
					this.changeList = null;
				}
				if (this.folderAddList != null)
				{
					DeltaSyncStorageProviderState.DisposeSyncObjects(this.folderAddList.Values);
					this.folderAddList.Clear();
					this.folderAddList = null;
				}
				if (this.emailAddList != null)
				{
					DeltaSyncStorageProviderState.DisposeSyncObjects(this.emailAddList);
					this.emailAddList.Clear();
					this.emailAddList = null;
				}
				if (this.deltaSyncOperations != null)
				{
					this.deltaSyncOperations.Clear();
					this.deltaSyncOperations = null;
				}
				if (this.nativeToCloudFolderIdMapping != null)
				{
					this.nativeToCloudFolderIdMapping.Clear();
					this.nativeToCloudFolderIdMapping = null;
				}
				if (this.nativeToTempFolderIdMapping != null)
				{
					this.nativeToTempFolderIdMapping.Clear();
					this.nativeToTempFolderIdMapping = null;
				}
				if (this.emailIdMapping != null)
				{
					DeltaSyncStorageProviderState.DisposeSyncObjects(this.emailIdMapping.Values);
					this.emailIdMapping.Clear();
					this.emailIdMapping = null;
				}
				this.emailIndex = -1;
				base.ItemRetriever = null;
				base.ItemRetrieverState = null;
				this.changesLoaded = false;
			}
		}

		// Token: 0x06000465 RID: 1125 RVA: 0x00014F94 File Offset: 0x00013194
		internal void UpdateSubscriptionSettings(DeltaSyncSettings deltaSyncSettings)
		{
			base.CheckDisposed();
			this.DeltaSyncSubscription.MaxNumberOfEmailAdds = deltaSyncSettings.MaxNumberOfEmailAdds;
			this.DeltaSyncSubscription.MaxNumberOfFolderAdds = deltaSyncSettings.MaxNumberOfFolderAdds;
			this.DeltaSyncSubscription.MaxObjectInSync = deltaSyncSettings.MaxObjectsInSync;
			this.DeltaSyncSubscription.MinSettingPollInterval = deltaSyncSettings.MinSettingsPollInterval;
			this.DeltaSyncSubscription.MinSyncPollInterval = deltaSyncSettings.MinSyncPollInterval;
			this.DeltaSyncSubscription.SyncMultiplier = deltaSyncSettings.SyncMultiplier;
			this.DeltaSyncSubscription.MaxAttachments = deltaSyncSettings.MaxAttachments;
			this.DeltaSyncSubscription.MaxMessageSize = deltaSyncSettings.MaxMessageSize;
			this.DeltaSyncSubscription.MaxRecipients = deltaSyncSettings.MaxRecipients;
			switch (deltaSyncSettings.AccountStatus)
			{
			case AccountStatusType.OK:
				this.DeltaSyncSubscription.AccountStatus = DeltaSyncAccountStatus.Normal;
				return;
			case AccountStatusType.Blocked:
				this.DeltaSyncSubscription.AccountStatus = DeltaSyncAccountStatus.Blocked;
				return;
			case AccountStatusType.RequiresHIP:
				this.DeltaSyncSubscription.AccountStatus = DeltaSyncAccountStatus.HipRequired;
				return;
			default:
				throw new InvalidDataException(string.Format(CultureInfo.InvariantCulture, "unknown AccountStatus: {0}", new object[]
				{
					deltaSyncSettings.AccountStatus
				}));
			}
		}

		// Token: 0x06000466 RID: 1126 RVA: 0x000150B4 File Offset: 0x000132B4
		internal void UpdateSyncChangeEntry(SyncChangeEntry syncEntry, Exception exception)
		{
			if (exception is TransientException)
			{
				base.HasTransientSyncErrors = true;
				syncEntry.Exception = SyncTransientException.CreateItemLevelException(exception);
				return;
			}
			base.HasPermanentSyncErrors = true;
			syncEntry.Exception = SyncPermanentException.CreateItemLevelException(exception);
		}

		// Token: 0x06000467 RID: 1127 RVA: 0x000150E5 File Offset: 0x000132E5
		protected override void InternalDispose(bool disposing)
		{
			if (disposing && this.deltaSyncClient != null)
			{
				this.deltaSyncClient.Dispose();
				this.deltaSyncClient = null;
				this.ClearApplyChangesState();
			}
		}

		// Token: 0x06000468 RID: 1128 RVA: 0x0001510A File Offset: 0x0001330A
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<DeltaSyncStorageProviderState>(this);
		}

		// Token: 0x06000469 RID: 1129 RVA: 0x00015114 File Offset: 0x00013314
		private static void DisposeSyncObjects(ICollection<SyncChangeEntry> collection)
		{
			if (collection != null)
			{
				foreach (SyncChangeEntry syncChangeEntry in collection)
				{
					if (syncChangeEntry.SyncObject != null)
					{
						syncChangeEntry.SyncObject.Dispose();
						syncChangeEntry.SyncObject = null;
					}
				}
			}
		}

		// Token: 0x04000203 RID: 515
		private const string DeltaSyncComponentId = "DeltaSync";

		// Token: 0x04000204 RID: 516
		private const string LastSettingsSyncTimePropertyName = "LastSettingsSyncTime";

		// Token: 0x04000205 RID: 517
		private static ConflictResolution conflictResolution = ConflictResolution.ServerWins;

		// Token: 0x04000206 RID: 518
		private readonly int maxDownloadItemsInFirstDeltaSyncConnection;

		// Token: 0x04000207 RID: 519
		private IDeltaSyncClient deltaSyncClient;

		// Token: 0x04000208 RID: 520
		private DeltaSyncResultData deltaSyncResultData;

		// Token: 0x04000209 RID: 521
		private DeltaSyncUserAccount deltaSyncUserAccount;

		// Token: 0x0400020A RID: 522
		private string latestEmailSyncKey;

		// Token: 0x0400020B RID: 523
		private string latestFolderSyncKey;

		// Token: 0x0400020C RID: 524
		private Dictionary<string, SyncChangeEntry> changeList;

		// Token: 0x0400020D RID: 525
		private Dictionary<string, SyncChangeEntry> folderAddList;

		// Token: 0x0400020E RID: 526
		private Dictionary<StoreObjectId, string> nativeToCloudFolderIdMapping;

		// Token: 0x0400020F RID: 527
		private Dictionary<StoreObjectId, string> nativeToTempFolderIdMapping;

		// Token: 0x04000210 RID: 528
		private List<SyncChangeEntry> emailAddList;

		// Token: 0x04000211 RID: 529
		private List<DeltaSyncOperation> deltaSyncOperations;

		// Token: 0x04000212 RID: 530
		private Dictionary<string, SyncChangeEntry> emailIdMapping;

		// Token: 0x04000213 RID: 531
		private int emailIndex;

		// Token: 0x04000214 RID: 532
		private int maxEmailChangesEnumerated;

		// Token: 0x04000215 RID: 533
		private long maxDownloadSizePerConnection;

		// Token: 0x04000216 RID: 534
		private long maxDownloadSizePerItem;

		// Token: 0x04000217 RID: 535
		private int cloudItemsSynced;

		// Token: 0x04000218 RID: 536
		private bool cloudMoreItemsAvailable;

		// Token: 0x04000219 RID: 537
		private bool changesLoaded;

		// Token: 0x0400021A RID: 538
		private TimeSpan deltaSyncSettingsUpdateInterval;

		// Token: 0x0400021B RID: 539
		private bool hasFolderAndEmailCollectionCached;

		// Token: 0x0400021C RID: 540
		private Collection cachedFolderCollection;

		// Token: 0x0400021D RID: 541
		private Collection cachedEmailCollection;
	}
}
