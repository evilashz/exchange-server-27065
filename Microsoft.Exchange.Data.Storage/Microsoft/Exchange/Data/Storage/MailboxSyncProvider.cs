using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Microsoft.Exchange.Data.Storage.Conversations;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020000B2 RID: 178
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class MailboxSyncProvider : ISyncProvider, IDisposeTrackable, IDisposable
	{
		// Token: 0x06000C00 RID: 3072 RVA: 0x0005229C File Offset: 0x0005049C
		public MailboxSyncProvider(Folder folder, bool trackReadFlagChanges, bool trackAssociatedMessageChanges, bool returnNewestFirst, bool trackConversations, bool allowTableRestrict, ISyncLogger syncLogger = null) : this(folder, trackReadFlagChanges, trackAssociatedMessageChanges, returnNewestFirst, trackConversations, allowTableRestrict, true, syncLogger)
		{
		}

		// Token: 0x06000C01 RID: 3073 RVA: 0x000522BC File Offset: 0x000504BC
		public MailboxSyncProvider(Folder folder, bool trackReadFlagChanges, bool trackAssociatedMessageChanges, bool returnNewestFirst, bool trackConversations, bool allowTableRestrict, bool disposeFolder, ISyncLogger syncLogger = null)
		{
			this.itemQueryOptimizationFilter = MailboxSyncProvider.falseFilterInstance;
			base..ctor();
			this.folder = folder;
			this.SyncLogger = (syncLogger ?? TracingLogger.Singleton);
			this.allowTableRestrict = allowTableRestrict;
			this.trackReadFlagChanges = trackReadFlagChanges;
			this.trackAssociatedMessageChanges = trackAssociatedMessageChanges;
			this.returnNewestChangesFirst = returnNewestFirst;
			this.trackConversations = trackConversations;
			this.UseSortOrder = true;
			this.disposeTracker = this.GetDisposeTracker();
			this.disposeFolder = disposeFolder;
		}

		// Token: 0x06000C02 RID: 3074 RVA: 0x00052333 File Offset: 0x00050533
		protected MailboxSyncProvider()
		{
			this.itemQueryOptimizationFilter = MailboxSyncProvider.falseFilterInstance;
			base..ctor();
			StorageGlobals.TraceConstructIDisposable(this);
			this.disposeTracker = this.GetDisposeTracker();
		}

		// Token: 0x17000248 RID: 584
		// (get) Token: 0x06000C03 RID: 3075 RVA: 0x00052358 File Offset: 0x00050558
		public bool IsTrackingConversations
		{
			get
			{
				this.CheckDisposed("get_IsTrackingConversations");
				return this.trackConversations;
			}
		}

		// Token: 0x17000249 RID: 585
		// (get) Token: 0x06000C04 RID: 3076 RVA: 0x0005236B File Offset: 0x0005056B
		// (set) Token: 0x06000C05 RID: 3077 RVA: 0x00052373 File Offset: 0x00050573
		public ISyncLogger SyncLogger { get; private set; }

		// Token: 0x1700024A RID: 586
		// (get) Token: 0x06000C06 RID: 3078 RVA: 0x0005237C File Offset: 0x0005057C
		// (set) Token: 0x06000C07 RID: 3079 RVA: 0x0005238F File Offset: 0x0005058F
		public QueryFilter ItemQueryOptimizationFilter
		{
			get
			{
				this.CheckDisposed("get_ItemQueryOptimizationFilter");
				return this.itemQueryOptimizationFilter;
			}
			set
			{
				this.CheckDisposed("set_ItemQueryOptimizationFilter");
				if (value != null && !(value is ComparisonFilter))
				{
					throw new InvalidOperationException();
				}
				this.itemQueryOptimizationFilter = value;
			}
		}

		// Token: 0x1700024B RID: 587
		// (get) Token: 0x06000C08 RID: 3080 RVA: 0x000523B4 File Offset: 0x000505B4
		// (set) Token: 0x06000C09 RID: 3081 RVA: 0x000523C7 File Offset: 0x000505C7
		public QueryFilter IcsPropertyGroupFilter
		{
			get
			{
				this.CheckDisposed("get_IcsPropertyGroupFilter");
				return this.icsPropertyGroupFilter;
			}
			set
			{
				this.CheckDisposed("set_IcsPropertyGroupFilter");
				this.icsPropertyGroupFilter = value;
			}
		}

		// Token: 0x1700024C RID: 588
		// (get) Token: 0x06000C0A RID: 3082 RVA: 0x000523DB File Offset: 0x000505DB
		// (set) Token: 0x06000C0B RID: 3083 RVA: 0x000523EE File Offset: 0x000505EE
		public bool UseSortOrder
		{
			get
			{
				this.CheckDisposed("get_UseSortOrder");
				return this.useSortOrder;
			}
			set
			{
				this.CheckDisposed("set_UseSortOrder");
				this.useSortOrder = value;
			}
		}

		// Token: 0x1700024D RID: 589
		// (get) Token: 0x06000C0C RID: 3084 RVA: 0x00052402 File Offset: 0x00050602
		internal static PropertyDefinition[] QueryColumns
		{
			get
			{
				return MailboxSyncProvider.queryColumns;
			}
		}

		// Token: 0x1700024E RID: 590
		// (get) Token: 0x06000C0D RID: 3085 RVA: 0x00052409 File Offset: 0x00050609
		internal FolderSync FolderSync
		{
			get
			{
				return this.folderSync;
			}
		}

		// Token: 0x1700024F RID: 591
		// (get) Token: 0x06000C0E RID: 3086 RVA: 0x00052411 File Offset: 0x00050611
		internal Folder Folder
		{
			get
			{
				return this.folder;
			}
		}

		// Token: 0x06000C0F RID: 3087 RVA: 0x00052419 File Offset: 0x00050619
		public static MailboxSyncProvider Bind(Folder folder, bool trackReadFlagChanges, bool trackAssociatedMessageChanges, bool returnNewestChangesFirst, bool trackConversations, bool allowTableRestrict, ISyncLogger syncLogger = null)
		{
			return new MailboxSyncProvider(folder, trackReadFlagChanges, trackAssociatedMessageChanges, returnNewestChangesFirst, trackConversations, allowTableRestrict, syncLogger);
		}

		// Token: 0x06000C10 RID: 3088 RVA: 0x0005242A File Offset: 0x0005062A
		public static MailboxSyncProvider Bind(Folder folder, bool trackReadFlagChanges, bool trackAssociatedMessageChanges, bool returnNewestChangesFirst, bool trackConversations, bool allowTableRestrict, bool disposeFolder, ISyncLogger syncLogger = null)
		{
			return new MailboxSyncProvider(folder, trackReadFlagChanges, trackAssociatedMessageChanges, returnNewestChangesFirst, trackConversations, allowTableRestrict, disposeFolder, syncLogger);
		}

		// Token: 0x06000C11 RID: 3089 RVA: 0x00052440 File Offset: 0x00050640
		private static bool IcsStateEquals(byte[] stateA, byte[] stateB)
		{
			if (stateA == null && stateB == null)
			{
				return true;
			}
			if (stateA == null || stateB == null)
			{
				return false;
			}
			if (object.ReferenceEquals(stateA, stateB))
			{
				return true;
			}
			if (stateA.Length != stateB.Length)
			{
				return false;
			}
			for (int i = 0; i < stateA.Length; i++)
			{
				if (stateA[i] != stateB[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000C12 RID: 3090 RVA: 0x0005248B File Offset: 0x0005068B
		public virtual DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<MailboxSyncProvider>(this);
		}

		// Token: 0x06000C13 RID: 3091 RVA: 0x00052493 File Offset: 0x00050693
		public void SuppressDisposeTracker()
		{
			if (this.disposeTracker != null)
			{
				this.disposeTracker.Suppress();
			}
		}

		// Token: 0x06000C14 RID: 3092 RVA: 0x000524A8 File Offset: 0x000506A8
		public void BindToFolderSync(FolderSync folderSync)
		{
			this.CheckDisposed("BindToFolderSync");
			this.folderSync = folderSync;
		}

		// Token: 0x06000C15 RID: 3093 RVA: 0x000524BC File Offset: 0x000506BC
		public virtual ISyncWatermark CreateNewWatermark()
		{
			this.CheckDisposed("CreateNewWatermark");
			return MailboxSyncWatermark.Create();
		}

		// Token: 0x06000C16 RID: 3094 RVA: 0x000524D0 File Offset: 0x000506D0
		public virtual OperationResult DeleteItems(params ISyncItemId[] syncIds)
		{
			this.CheckDisposed("DeleteItems");
			StoreObjectId[] array = new StoreObjectId[syncIds.Length];
			for (int i = 0; i < syncIds.Length; i++)
			{
				array[i] = (StoreObjectId)syncIds[i].NativeId;
			}
			return this.folder.Session.Delete(DeleteItemFlags.SoftDelete, array).OperationResult;
		}

		// Token: 0x06000C17 RID: 3095 RVA: 0x00052526 File Offset: 0x00050726
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000C18 RID: 3096 RVA: 0x00052535 File Offset: 0x00050735
		public void DisposeNewOperationsCachedData()
		{
			this.CheckDisposed("DisposeNewOperationsCachedData");
			if (this.syncQueryResult != null)
			{
				this.syncQueryResult.Dispose();
				this.syncQueryResult = null;
			}
		}

		// Token: 0x06000C19 RID: 3097 RVA: 0x0005255C File Offset: 0x0005075C
		public ISyncItem GetItem(ISyncItemId id, params PropertyDefinition[] specifiedPrefetchProperties)
		{
			this.CheckDisposed("GetItem");
			PropertyDefinition[] array;
			if (specifiedPrefetchProperties != null && specifiedPrefetchProperties.Length != 0)
			{
				array = new PropertyDefinition[specifiedPrefetchProperties.Length + MailboxSyncProvider.defaultPrefetchProperties.Length];
				specifiedPrefetchProperties.CopyTo(array, 0);
				MailboxSyncProvider.defaultPrefetchProperties.CopyTo(array, specifiedPrefetchProperties.Length);
			}
			else
			{
				array = MailboxSyncProvider.defaultPrefetchProperties;
			}
			return this.GetItem(this.BindToItemWithItemClass((StoreObjectId)id.NativeId, array));
		}

		// Token: 0x06000C1A RID: 3098 RVA: 0x000525C8 File Offset: 0x000507C8
		public ISyncWatermark GetMaxItemWatermark(ISyncWatermark currentWatermark)
		{
			this.CheckDisposed("GetMaxItemWatermark");
			MailboxSyncWatermark mailboxSyncWatermark = MailboxSyncWatermark.Create();
			mailboxSyncWatermark.IcsState = this.CatchUpIcsState((MailboxSyncWatermark)currentWatermark);
			using (QueryResult queryResult = this.folder.ItemQuery(ItemQueryType.None, null, MailboxSyncProvider.sortByArticleIdDescending, MailboxSyncProvider.queryColumns))
			{
				object[][] rows = queryResult.GetRows(1);
				if (rows.Length != 0)
				{
					StoreObjectId objectId = ((VersionedId)rows[0][1]).ObjectId;
					mailboxSyncWatermark.ChangeNumber = (int)rows[0][0];
				}
			}
			return mailboxSyncWatermark;
		}

		// Token: 0x06000C1B RID: 3099 RVA: 0x0005265C File Offset: 0x0005085C
		public virtual bool GetNewOperations(ISyncWatermark minSyncWatermark, ISyncWatermark maxSyncWatermark, bool enumerateDeletes, int numOperations, QueryFilter filter, Dictionary<ISyncItemId, ServerManifestEntry> newServerManifest)
		{
			if (numOperations < 0 && numOperations != -1)
			{
				throw new ArgumentException("numOperations is not valid, value = " + numOperations);
			}
			this.CheckDisposed("GetNewOperations");
			MailboxSyncWatermark minWatermark = minSyncWatermark as MailboxSyncWatermark;
			MailboxSyncWatermark maxWatermark = maxSyncWatermark as MailboxSyncWatermark;
			if (filter != null)
			{
				this.SyncLogger.Information<int>(ExTraceGlobals.SyncProcessTracer, (long)this.GetHashCode(), "MailboxSyncProvider.GetNewOperations. numOperations = {0} With filter", numOperations);
				return this.GetNewOperationsWithFilter(minWatermark, maxWatermark, numOperations, filter, newServerManifest);
			}
			this.SyncLogger.Information<int>(ExTraceGlobals.SyncProcessTracer, (long)this.GetHashCode(), "MailboxSyncProvider.GetNewOperations. numOperations = {0} with ICS", numOperations);
			return this.IcsGetNewOperations(minWatermark, enumerateDeletes, numOperations, newServerManifest);
		}

		// Token: 0x06000C1C RID: 3100 RVA: 0x00052700 File Offset: 0x00050900
		public List<IConversationTreeNode> GetInFolderItemsForConversation(ConversationId conversationId)
		{
			this.CheckDisposed("GetInFolderItemsForConversation");
			if (conversationId == null)
			{
				return null;
			}
			List<IConversationTreeNode> list = null;
			Conversation conversation = Conversation.Load((MailboxSession)this.folder.Session, conversationId, MailboxSyncProvider.conversationPrefetchProperties);
			if (conversation == null)
			{
				return list;
			}
			list = new List<IConversationTreeNode>(conversation.ConversationTree.Count);
			StoreObjectId objectId = this.folder.Id.ObjectId;
			foreach (IConversationTreeNode conversationTreeNode in conversation.ConversationTree)
			{
				StoreObjectId id = (StoreObjectId)conversationTreeNode.StorePropertyBags[0].TryGetProperty(StoreObjectSchema.ParentItemId);
				if (objectId.Equals(id))
				{
					list.Add(conversationTreeNode);
				}
			}
			return list;
		}

		// Token: 0x06000C1D RID: 3101 RVA: 0x000527D0 File Offset: 0x000509D0
		public virtual ISyncItemId CreateISyncItemIdForNewItem(StoreObjectId itemId)
		{
			this.CheckDisposed("CreateISyncItemIdForNewItem");
			if (itemId == null)
			{
				throw new ArgumentNullException("itemId");
			}
			return MailboxSyncItemId.CreateForNewItem(itemId);
		}

		// Token: 0x06000C1E RID: 3102 RVA: 0x000527F4 File Offset: 0x000509F4
		internal static ServerManifestEntry CreateItemDeleteManifestEntry(ISyncItemId syncItemId)
		{
			return new ServerManifestEntry(syncItemId)
			{
				ChangeType = ChangeType.Delete
			};
		}

		// Token: 0x06000C1F RID: 3103 RVA: 0x00052810 File Offset: 0x00050A10
		protected virtual ISyncItem GetItem(Item item)
		{
			return MailboxSyncItem.Bind(item);
		}

		// Token: 0x06000C20 RID: 3104 RVA: 0x00052818 File Offset: 0x00050A18
		protected void CheckDisposed(string methodName)
		{
			if (this.disposed)
			{
				StorageGlobals.TraceFailedCheckDisposed(this, methodName);
				throw new ObjectDisposedException(base.GetType().ToString());
			}
		}

		// Token: 0x06000C21 RID: 3105 RVA: 0x0005283C File Offset: 0x00050A3C
		public ServerManifestEntry CreateItemChangeManifestEntry(ISyncItemId syncItemId, ISyncWatermark watermark)
		{
			return new ServerManifestEntry(syncItemId)
			{
				Watermark = watermark,
				ChangeType = ChangeType.Add
			};
		}

		// Token: 0x06000C22 RID: 3106 RVA: 0x00052860 File Offset: 0x00050A60
		internal ServerManifestEntry CreateReadFlagChangeManifestEntry(ISyncItemId syncItemId, bool read)
		{
			if (this.folderSync != null && !this.folderSync.ClientHasItem(syncItemId))
			{
				return null;
			}
			return new ServerManifestEntry(syncItemId)
			{
				ChangeType = ChangeType.ReadFlagChange,
				IsRead = read
			};
		}

		// Token: 0x06000C23 RID: 3107 RVA: 0x0005289C File Offset: 0x00050A9C
		protected virtual void InternalDispose(bool disposing)
		{
			if (this.manifest != null)
			{
				this.manifest.Dispose();
				this.manifest = null;
			}
			if (disposing)
			{
				if (this.folder != null && this.disposeFolder)
				{
					this.folder.Dispose();
					this.folder = null;
				}
				if (this.disposeTracker != null)
				{
					this.disposeTracker.Dispose();
				}
			}
		}

		// Token: 0x06000C24 RID: 3108 RVA: 0x000528FC File Offset: 0x00050AFC
		private Item BindToItemWithItemClass(StoreObjectId id, ICollection<PropertyDefinition> properties)
		{
			switch (id.ObjectType)
			{
			case StoreObjectType.Message:
				return MessageItem.Bind(this.folder.Session, id, properties);
			case StoreObjectType.MeetingRequest:
				return MeetingRequest.Bind(this.folder.Session, id, properties);
			case StoreObjectType.MeetingResponse:
				return MeetingResponse.Bind(this.folder.Session, id, properties);
			case StoreObjectType.MeetingCancellation:
				return MeetingCancellation.Bind(this.folder.Session, id, properties);
			case StoreObjectType.Contact:
				return Contact.Bind(this.folder.Session, id, properties);
			case StoreObjectType.DistributionList:
				return DistributionList.Bind(this.folder.Session, id, properties);
			case StoreObjectType.Task:
				return Task.Bind(this.folder.Session, id, true, properties);
			case StoreObjectType.Post:
				return PostItem.Bind(this.folder.Session, id, properties);
			case StoreObjectType.Report:
				return ReportMessage.Bind(this.folder.Session, id, properties);
			}
			return Item.Bind(this.folder.Session, id, properties);
		}

		// Token: 0x06000C25 RID: 3109 RVA: 0x00052A38 File Offset: 0x00050C38
		private ManifestConfigFlags GetConfigFlags()
		{
			ManifestConfigFlags manifestConfigFlags = ManifestConfigFlags.Normal;
			if (this.returnNewestChangesFirst)
			{
				manifestConfigFlags |= ManifestConfigFlags.OrderByDeliveryTime;
			}
			if (!this.trackReadFlagChanges)
			{
				manifestConfigFlags |= ManifestConfigFlags.NoReadUnread;
			}
			if (this.trackAssociatedMessageChanges)
			{
				manifestConfigFlags |= ManifestConfigFlags.Associated;
			}
			if (this.trackConversations)
			{
				manifestConfigFlags |= ManifestConfigFlags.Conversations;
			}
			return manifestConfigFlags;
		}

		// Token: 0x06000C26 RID: 3110 RVA: 0x00052A80 File Offset: 0x00050C80
		private byte[] CatchUpIcsState(MailboxSyncWatermark currentWatermark)
		{
			byte[] result = null;
			MapiFolder mapiFolder = this.folder.MapiFolder;
			MemoryStream memoryStream = null;
			MemoryStream memoryStream2 = null;
			try
			{
				if (currentWatermark.IcsState == null)
				{
					memoryStream = new MemoryStream();
				}
				else
				{
					memoryStream = new MemoryStream(currentWatermark.IcsState);
				}
				StoreSession session = this.folder.Session;
				bool flag = false;
				try
				{
					if (session != null)
					{
						session.BeginMapiCall();
						session.BeginServerHealthCall();
						flag = true;
					}
					if (StorageGlobals.MapiTestHookBeforeCall != null)
					{
						StorageGlobals.MapiTestHookBeforeCall(MethodBase.GetCurrentMethod());
					}
					try
					{
						using (MapiManifest mapiManifest = mapiFolder.CreateExportManifest())
						{
							IcsCallback iMapiManifestCallback = new IcsCallback(this, null, 0, null);
							mapiManifest.Configure(this.GetConfigFlags() | ManifestConfigFlags.Catchup, null, memoryStream, iMapiManifestCallback, IcsCallback.PropTags);
							mapiManifest.Synchronize();
							memoryStream2 = new MemoryStream();
							mapiManifest.GetState(memoryStream2);
							result = memoryStream2.ToArray();
						}
					}
					catch (MapiExceptionCorruptData innerException)
					{
						throw new CorruptSyncStateException(ServerStrings.ExSyncStateCorrupted("ICS"), innerException);
					}
					catch (CorruptDataException innerException2)
					{
						throw new CorruptSyncStateException(ServerStrings.ExSyncStateCorrupted("ICS"), innerException2);
					}
				}
				catch (MapiPermanentException ex)
				{
					throw StorageGlobals.TranslateMapiException(ServerStrings.ICSSynchronizationFailed, ex, session, this, "{0}. MapiException = {1}.", new object[]
					{
						string.Format("MailboxSyncProvider::CatchUpIcsState()", new object[0]),
						ex
					});
				}
				catch (MapiRetryableException ex2)
				{
					throw StorageGlobals.TranslateMapiException(ServerStrings.ICSSynchronizationFailed, ex2, session, this, "{0}. MapiException = {1}.", new object[]
					{
						string.Format("MailboxSyncProvider::CatchUpIcsState()", new object[0]),
						ex2
					});
				}
				finally
				{
					try
					{
						if (session != null)
						{
							session.EndMapiCall();
							if (flag)
							{
								session.EndServerHealthCall();
							}
						}
					}
					finally
					{
						if (StorageGlobals.MapiTestHookAfterCall != null)
						{
							StorageGlobals.MapiTestHookAfterCall(MethodBase.GetCurrentMethod());
						}
					}
				}
			}
			finally
			{
				Util.DisposeIfPresent(memoryStream);
				Util.DisposeIfPresent(memoryStream2);
			}
			return result;
		}

		// Token: 0x06000C27 RID: 3111 RVA: 0x00052D00 File Offset: 0x00050F00
		private void Dispose(bool disposing)
		{
			StorageGlobals.TraceDispose(this, this.disposed, disposing);
			if (!this.disposed)
			{
				this.disposed = true;
				this.InternalDispose(disposing);
			}
		}

		// Token: 0x06000C28 RID: 3112 RVA: 0x00052D28 File Offset: 0x00050F28
		private bool GetNewOperationsWithFilter(MailboxSyncWatermark minWatermark, MailboxSyncWatermark maxWatermark, int numOperations, QueryFilter filter, Dictionary<ISyncItemId, ServerManifestEntry> newServerManifest)
		{
			this.SyncLogger.Information(ExTraceGlobals.SyncProcessTracer, (long)this.GetHashCode(), "MailboxSyncProvider.GetNewOperationsWithFilter. numOperations {0}, newServerManifest count {1}, minWatermark is New? {2}. Starting change number {3}", new object[]
			{
				numOperations,
				newServerManifest.Count,
				minWatermark.IsNew,
				minWatermark.ChangeNumber
			});
			bool result = false;
			ComparisonFilter comparisonFilter = null;
			if (!minWatermark.IsNew)
			{
				comparisonFilter = new ComparisonFilter(ComparisonOperator.GreaterThan, InternalSchema.ArticleId, minWatermark.RawChangeNumber);
			}
			if (this.syncQueryResult == null)
			{
				this.syncQueryResult = MailboxSyncQueryProcessor.ItemQuery(this.folder, ItemQueryType.None, filter, this.itemQueryOptimizationFilter, MailboxSyncProvider.sortByArticleIdAscending, MailboxSyncProvider.queryColumns, this.allowTableRestrict, this.UseSortOrder);
			}
			else if (comparisonFilter == null)
			{
				this.syncQueryResult.SeekToOffset(SeekReference.OriginBeginning, 0);
			}
			if (comparisonFilter != null)
			{
				this.syncQueryResult.SeekToCondition(SeekReference.OriginBeginning, comparisonFilter);
			}
			bool flag = false;
			while (!flag)
			{
				int num;
				if (numOperations == -1)
				{
					num = 10000;
				}
				else
				{
					int num2 = numOperations - newServerManifest.Count;
					num = num2 + 1;
				}
				if (num < 0)
				{
					throw new InvalidOperationException(ServerStrings.ExNumberOfRowsToFetchInvalid(num.ToString()));
				}
				object[][] rows = this.syncQueryResult.GetRows(num);
				flag = (this.syncQueryResult.CurrentRow == this.syncQueryResult.EstimatedRowCount);
				this.SyncLogger.Information<int, int, bool>(ExTraceGlobals.SyncProcessTracer, (long)this.GetHashCode(), "MailboxSyncProvider.GetNewOperationsWithFilter. Requested {0} rows, Received {1} rows, All fetched? {2}", num, rows.Length, flag);
				for (int i = 0; i < rows.Length; i++)
				{
					try
					{
						StoreObjectId objectId = ((VersionedId)rows[i][1]).ObjectId;
						int changeNumber = (int)rows[i][0];
						MailboxSyncWatermark mailboxSyncWatermark = (MailboxSyncWatermark)this.CreateNewWatermark();
						mailboxSyncWatermark.UpdateWithChangeNumber(changeNumber, (bool)rows[i][2]);
						if (maxWatermark == null || maxWatermark.CompareTo(mailboxSyncWatermark) >= 0)
						{
							ISyncItemId syncItemId = this.CreateISyncItemIdForNewItem(objectId);
							ServerManifestEntry serverManifestEntry = this.CreateItemChangeManifestEntry(syncItemId, mailboxSyncWatermark);
							serverManifestEntry.ConversationId = (rows[i][3] as ConversationId);
							byte[] bytes = rows[i][4] as byte[];
							ConversationIndex index;
							if (ConversationIndex.TryCreate(bytes, out index) && index != ConversationIndex.Empty && index.Components != null && index.Components.Count == 1)
							{
								serverManifestEntry.FirstMessageInConversation = true;
							}
							if (rows[i][5] is ExDateTime)
							{
								serverManifestEntry.FilterDate = new ExDateTime?((ExDateTime)rows[i][5]);
							}
							serverManifestEntry.MessageClass = (rows[i][6] as string);
							if (numOperations != -1 && newServerManifest.Count >= numOperations)
							{
								result = true;
								goto IL_2B9;
							}
							newServerManifest[serverManifestEntry.Id] = serverManifestEntry;
							minWatermark.ChangeNumber = changeNumber;
						}
					}
					catch
					{
						throw;
					}
				}
			}
			IL_2B9:
			this.SyncLogger.Information<int>(ExTraceGlobals.SyncProcessTracer, (long)this.GetHashCode(), "MailboxSyncProvider.GetNewOperationsWithFilter. Ending change number {0}", minWatermark.ChangeNumber);
			return result;
		}

		// Token: 0x06000C29 RID: 3113 RVA: 0x00053030 File Offset: 0x00051230
		private bool IcsGetNewOperations(MailboxSyncWatermark minWatermark, bool enumerateDeletes, int numOperations, Dictionary<ISyncItemId, ServerManifestEntry> newServerManifest)
		{
			this.SyncLogger.Information<int, int>(ExTraceGlobals.SyncProcessTracer, (long)this.GetHashCode(), "MailboxSyncProvider.IcsGetNewOperations.  NumOperations {0}, newServerManifest.Count {1}", numOperations, newServerManifest.Count);
			bool flag = false;
			MemoryStream memoryStream = null;
			try
			{
				StoreSession session = this.folder.Session;
				bool flag2 = false;
				try
				{
					if (session != null)
					{
						session.BeginMapiCall();
						session.BeginServerHealthCall();
						flag2 = true;
					}
					if (StorageGlobals.MapiTestHookBeforeCall != null)
					{
						StorageGlobals.MapiTestHookBeforeCall(MethodBase.GetCurrentMethod());
					}
					try
					{
						flag = this.EnsureMapiManifest(minWatermark, numOperations, newServerManifest);
						if (flag)
						{
							this.SyncLogger.Information(ExTraceGlobals.SyncProcessTracer, (long)this.GetHashCode(), "MailboxSyncProvider.IcsGetNewOperations.  EnsureMapiManifest returned true (moreAvailable).  Bailing out.");
							return flag;
						}
						memoryStream = new MemoryStream();
						ManifestStatus manifestStatus = ManifestStatus.Yielded;
						while (manifestStatus == ManifestStatus.Yielded)
						{
							manifestStatus = this.manifest.Synchronize();
							switch (manifestStatus)
							{
							case ManifestStatus.Done:
							case ManifestStatus.Yielded:
								this.manifest.GetState(memoryStream);
								minWatermark.IcsState = memoryStream.ToArray();
								this.icsState = minWatermark.IcsState;
								break;
							case ManifestStatus.Stopped:
								this.extraServerManifestEntry = this.icsCallback.ExtraServerManiferEntry;
								this.SyncLogger.Information<string>(ExTraceGlobals.SyncProcessTracer, (long)this.GetHashCode(), "MailboxSyncProvider::IcsGetNewOperations Got additional change, id = {0}", (this.extraServerManifestEntry == null) ? "NULL" : this.extraServerManifestEntry.Id.ToString());
								break;
							}
						}
						flag = this.icsCallback.MoreAvailable;
						this.SyncLogger.Information<bool>(ExTraceGlobals.SyncProcessTracer, (long)this.GetHashCode(), "MailboxSyncProvider.IcsGetNewOperations.  More available after ICS sync? {0}", flag);
					}
					catch (MapiExceptionCorruptData innerException)
					{
						throw new CorruptSyncStateException(ServerStrings.ExSyncStateCorrupted("ICS"), innerException);
					}
					catch (CorruptDataException innerException2)
					{
						throw new CorruptSyncStateException(ServerStrings.ExSyncStateCorrupted("ICS"), innerException2);
					}
				}
				catch (MapiPermanentException ex)
				{
					throw StorageGlobals.TranslateMapiException(ServerStrings.ICSSynchronizationFailed, ex, session, this, "{0}. MapiException = {1}.", new object[]
					{
						string.Format("MailboxSyncProvider::IcsGetNewOperations()", new object[0]),
						ex
					});
				}
				catch (MapiRetryableException ex2)
				{
					throw StorageGlobals.TranslateMapiException(ServerStrings.ICSSynchronizationFailed, ex2, session, this, "{0}. MapiException = {1}.", new object[]
					{
						string.Format("MailboxSyncProvider::IcsGetNewOperations()", new object[0]),
						ex2
					});
				}
				finally
				{
					try
					{
						if (session != null)
						{
							session.EndMapiCall();
							if (flag2)
							{
								session.EndServerHealthCall();
							}
						}
					}
					finally
					{
						if (StorageGlobals.MapiTestHookAfterCall != null)
						{
							StorageGlobals.MapiTestHookAfterCall(MethodBase.GetCurrentMethod());
						}
					}
				}
			}
			finally
			{
				Util.DisposeIfPresent(memoryStream);
			}
			return flag;
		}

		// Token: 0x06000C2A RID: 3114 RVA: 0x00053330 File Offset: 0x00051530
		private bool EnsureMapiManifest(MailboxSyncWatermark minWatermark, int numOperations, Dictionary<ISyncItemId, ServerManifestEntry> newServerManifest)
		{
			MemoryStream memoryStream = null;
			MapiFolder mapiFolder = this.folder.MapiFolder;
			if (this.manifest != null)
			{
				if (MailboxSyncProvider.IcsStateEquals(this.icsState, minWatermark.IcsState))
				{
					if (this.extraServerManifestEntry != null)
					{
						if (numOperations == 0)
						{
							return true;
						}
						newServerManifest.Add(this.extraServerManifestEntry.Id, this.extraServerManifestEntry);
						if (this.extraServerManifestEntry.Watermark != null)
						{
							minWatermark.ChangeNumber = ((MailboxSyncWatermark)this.extraServerManifestEntry.Watermark).ChangeNumber;
						}
						this.SyncLogger.Information<ISyncItemId>(ExTraceGlobals.SyncProcessTracer, (long)this.GetHashCode(), "MailboxSyncProvider::EnsureMapiManifest Adding cached change, id = {0}", this.extraServerManifestEntry.Id);
						this.extraServerManifestEntry = null;
					}
					this.icsCallback.Bind(minWatermark, numOperations, newServerManifest);
					this.SyncLogger.Information<int, int>(ExTraceGlobals.SyncProcessTracer, (long)this.GetHashCode(), "MailboxSyncProvider::EnsureMapiManifest Reusing ICS manifest, numOperations = {0}, newServerManifest.Count = {1}", numOperations, newServerManifest.Count);
					return false;
				}
				this.manifest.Dispose();
				this.manifest = null;
				this.SyncLogger.Information<int, int>(ExTraceGlobals.SyncProcessTracer, (long)this.GetHashCode(), "MailboxSyncProvider::EnsureMapiManifest Tossed old ICS manifest, numOperations = {0}, newServerManifest.Count = {1}", numOperations, newServerManifest.Count);
			}
			try
			{
				memoryStream = ((minWatermark.IcsState == null) ? new MemoryStream() : new MemoryStream(minWatermark.IcsState));
				this.manifest = mapiFolder.CreateExportManifest();
				this.icsCallback = new IcsCallback(this, newServerManifest, numOperations, minWatermark);
				Restriction restriction = (this.icsPropertyGroupFilter == null) ? null : FilterRestrictionConverter.CreateRestriction(this.folder.Session, this.folder.Session.ExTimeZone, this.folder.MapiFolder, this.icsPropertyGroupFilter);
				this.manifest.Configure(this.GetConfigFlags(), restriction, memoryStream, this.icsCallback, IcsCallback.PropTags);
				this.SyncLogger.Information<int, int>(ExTraceGlobals.SyncProcessTracer, (long)this.GetHashCode(), "MailboxSyncProvider::EnsureMapiManifest Created new ICS manifest, numOperations = {0}, newServerManifest.Count = {1}", numOperations, newServerManifest.Count);
			}
			finally
			{
				Util.DisposeIfPresent(memoryStream);
			}
			return false;
		}

		// Token: 0x04000349 RID: 841
		protected const int MaxNumOperations = 10240;

		// Token: 0x0400034A RID: 842
		private static readonly SortBy[] sortByArticleIdAscending = new SortBy[]
		{
			new SortBy(InternalSchema.ArticleId, SortOrder.Ascending)
		};

		// Token: 0x0400034B RID: 843
		private static readonly SortBy[] sortByArticleIdDescending = new SortBy[]
		{
			new SortBy(InternalSchema.ArticleId, SortOrder.Descending)
		};

		// Token: 0x0400034C RID: 844
		protected static readonly PropertyDefinition[] queryColumns = new PropertyDefinition[]
		{
			InternalSchema.ArticleId,
			InternalSchema.ItemId,
			MessageItemSchema.IsRead,
			ItemSchema.ConversationId,
			ItemSchema.ConversationIndex,
			ItemSchema.ReceivedTime,
			InternalSchema.ItemClass
		};

		// Token: 0x0400034D RID: 845
		private static readonly PropertyDefinition[] conversationPrefetchProperties = new PropertyDefinition[]
		{
			ItemSchema.Id,
			StoreObjectSchema.ParentItemId,
			ItemSchema.ConversationId,
			StoreObjectSchema.ItemClass
		};

		// Token: 0x0400034E RID: 846
		private static readonly PropertyDefinition[] defaultPrefetchProperties = new PropertyDefinition[]
		{
			InternalSchema.ArticleId
		};

		// Token: 0x0400034F RID: 847
		private static readonly QueryFilter falseFilterInstance = new FalseFilter();

		// Token: 0x04000350 RID: 848
		private readonly DisposeTracker disposeTracker;

		// Token: 0x04000351 RID: 849
		private readonly bool trackConversations;

		// Token: 0x04000352 RID: 850
		private readonly bool disposeFolder;

		// Token: 0x04000353 RID: 851
		private bool allowTableRestrict;

		// Token: 0x04000354 RID: 852
		protected Folder folder;

		// Token: 0x04000355 RID: 853
		protected bool trackAssociatedMessageChanges;

		// Token: 0x04000356 RID: 854
		private FolderSync folderSync;

		// Token: 0x04000357 RID: 855
		private bool disposed;

		// Token: 0x04000358 RID: 856
		private MailboxSyncQueryProcessor.IQueryResult syncQueryResult;

		// Token: 0x04000359 RID: 857
		private bool trackReadFlagChanges;

		// Token: 0x0400035A RID: 858
		protected bool returnNewestChangesFirst;

		// Token: 0x0400035B RID: 859
		private QueryFilter itemQueryOptimizationFilter;

		// Token: 0x0400035C RID: 860
		private QueryFilter icsPropertyGroupFilter;

		// Token: 0x0400035D RID: 861
		private MapiManifest manifest;

		// Token: 0x0400035E RID: 862
		private byte[] icsState;

		// Token: 0x0400035F RID: 863
		private ServerManifestEntry extraServerManifestEntry;

		// Token: 0x04000360 RID: 864
		private IcsCallback icsCallback;

		// Token: 0x04000361 RID: 865
		private bool useSortOrder;

		// Token: 0x020000B3 RID: 179
		protected enum QueryColumnsEnum
		{
			// Token: 0x04000364 RID: 868
			ArticleId,
			// Token: 0x04000365 RID: 869
			Id,
			// Token: 0x04000366 RID: 870
			IsRead,
			// Token: 0x04000367 RID: 871
			ConversationId,
			// Token: 0x04000368 RID: 872
			ConversationIndex,
			// Token: 0x04000369 RID: 873
			ReceivedTime,
			// Token: 0x0400036A RID: 874
			ItemClass
		}
	}
}
