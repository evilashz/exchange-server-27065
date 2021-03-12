using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Storage.LinkedFolder;
using Microsoft.Exchange.Data.Storage.MailboxRules;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000037 RID: 55
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class CoreItem : CoreObject, ICoreItem, ICoreObject, ICoreState, IValidatable, IDisposeTrackable, IDisposable, ILocationIdentifierController
	{
		// Token: 0x060004D4 RID: 1236 RVA: 0x00028E58 File Offset: 0x00027058
		internal CoreItem(StoreSession session, PersistablePropertyBag propertyBag, StoreObjectId storeObjectId, byte[] changeKey, Origin origin, ItemLevel itemLevel, ICollection<PropertyDefinition> prefetchProperties, ItemBindOption itemBindOption) : base(session, propertyBag, storeObjectId, changeKey, origin, itemLevel, prefetchProperties)
		{
			this.itemBindOption = itemBindOption;
			this.locationIdentifierHelperInstance = new LocationIdentifierHelper();
			this.locationIdentifierHelperInstance.ResetChangeList();
			propertyBag.OnLocationIdentifierReached = (Action<uint>)Delegate.Combine(propertyBag.OnLocationIdentifierReached, new Action<uint>(this.locationIdentifierHelperInstance.SetLocationIdentifier));
			propertyBag.OnNamedLocationIdentifierReached = (Action<uint, LastChangeAction>)Delegate.Combine(propertyBag.OnNamedLocationIdentifierReached, new Action<uint, LastChangeAction>(this.locationIdentifierHelperInstance.SetLocationIdentifier));
			this.charsetDetector = new ItemCharsetDetector(this);
			if (session != null && session.PreferredInternetCodePageForShiftJis != 0)
			{
				this.charsetDetector.DetectionOptions.PreferredInternetCodePageForShiftJis = session.PreferredInternetCodePageForShiftJis;
				this.charsetDetector.DetectionOptions.RequiredCoverage = session.RequiredCoverage;
			}
		}

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x060004D5 RID: 1237 RVA: 0x00028F30 File Offset: 0x00027130
		// (remove) Token: 0x060004D6 RID: 1238 RVA: 0x00028F50 File Offset: 0x00027150
		public event Action BeforeSend
		{
			add
			{
				this.CheckDisposed(null);
				this.beforeSendEventHandler = (Action)Delegate.Combine(this.beforeSendEventHandler, value);
			}
			remove
			{
				this.beforeSendEventHandler = (Action)Delegate.Remove(this.beforeSendEventHandler, value);
			}
		}

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x060004D7 RID: 1239 RVA: 0x00028F69 File Offset: 0x00027169
		// (remove) Token: 0x060004D8 RID: 1240 RVA: 0x00028F89 File Offset: 0x00027189
		public event Action BeforeFlush
		{
			add
			{
				this.CheckDisposed(null);
				this.beforeFlushEventHandler = (Action)Delegate.Combine(this.beforeFlushEventHandler, value);
			}
			remove
			{
				this.beforeFlushEventHandler = (Action)Delegate.Remove(this.beforeFlushEventHandler, value);
			}
		}

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x060004D9 RID: 1241 RVA: 0x00028FA4 File Offset: 0x000271A4
		public bool IsReadOnly
		{
			get
			{
				AcrPropertyBag acrPropertyBag = base.PropertyBag as AcrPropertyBag;
				return acrPropertyBag != null && acrPropertyBag.IsReadOnly;
			}
		}

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x060004DA RID: 1242 RVA: 0x00028FC8 File Offset: 0x000271C8
		public bool IsMoveUser
		{
			get
			{
				return base.Session != null && base.Session.IsMoveUser;
			}
		}

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x060004DB RID: 1243 RVA: 0x00028FDF File Offset: 0x000271DF
		// (set) Token: 0x060004DC RID: 1244 RVA: 0x00028FEE File Offset: 0x000271EE
		public ICoreItem TopLevelItem
		{
			get
			{
				this.CheckDisposed(null);
				return this.topLevelItem;
			}
			set
			{
				this.CheckDisposed(null);
				this.topLevelItem = value;
			}
		}

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x060004DD RID: 1245 RVA: 0x00028FFE File Offset: 0x000271FE
		public bool IsFlushNeeded
		{
			get
			{
				return base.IsDirty || this.IsRecipientCollectionDirty;
			}
		}

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x060004DE RID: 1246 RVA: 0x00029010 File Offset: 0x00027210
		public override bool IsDirty
		{
			get
			{
				return base.IsDirty || this.IsRecipientCollectionDirty || this.IsAttachmentCollectionDirty;
			}
		}

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x060004DF RID: 1247 RVA: 0x0002902A File Offset: 0x0002722A
		public bool IsLegallyDirty
		{
			get
			{
				return this.legallyDirtyProperties != null && this.legallyDirtyProperties.Count<string>() > 0;
			}
		}

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x060004E0 RID: 1248 RVA: 0x00029044 File Offset: 0x00027244
		// (set) Token: 0x060004E1 RID: 1249 RVA: 0x00029058 File Offset: 0x00027258
		public PropertyBagSaveFlags SaveFlags
		{
			get
			{
				this.CheckDisposed(null);
				return CoreObject.GetPersistablePropertyBag(this).SaveFlags;
			}
			set
			{
				this.CheckDisposed(null);
				EnumValidator.ThrowIfInvalid<PropertyBagSaveFlags>(value, "value");
				CoreObject.GetPersistablePropertyBag(this).SaveFlags = value;
			}
		}

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x060004E2 RID: 1250 RVA: 0x00029078 File Offset: 0x00027278
		public CoreAttachmentCollection AttachmentCollection
		{
			get
			{
				this.CheckDisposed(null);
				if (this.attachmentCollection == null)
				{
					((ICoreItem)this).OpenAttachmentCollection();
				}
				return this.attachmentCollection;
			}
		}

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x060004E3 RID: 1251 RVA: 0x00029095 File Offset: 0x00027295
		public CoreRecipientCollection Recipients
		{
			get
			{
				return ((ICoreItem)this).GetRecipientCollection(true);
			}
		}

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x060004E4 RID: 1252 RVA: 0x0002909E File Offset: 0x0002729E
		public Body Body
		{
			get
			{
				this.CheckDisposed(null);
				if (this.body == null)
				{
					this.body = new Body(this);
				}
				return this.body;
			}
		}

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x060004E5 RID: 1253 RVA: 0x000290C1 File Offset: 0x000272C1
		public ItemCharsetDetector CharsetDetector
		{
			get
			{
				this.CheckDisposed(null);
				return this.charsetDetector;
			}
		}

		// Token: 0x17000113 RID: 275
		// (set) Token: 0x060004E6 RID: 1254 RVA: 0x000290D0 File Offset: 0x000272D0
		public int PreferredInternetCodePageForShiftJis
		{
			set
			{
				this.CheckDisposed(null);
				this.charsetDetector.DetectionOptions.PreferredInternetCodePageForShiftJis = value;
			}
		}

		// Token: 0x17000114 RID: 276
		// (set) Token: 0x060004E7 RID: 1255 RVA: 0x000290EA File Offset: 0x000272EA
		public int RequiredCoverage
		{
			set
			{
				this.CheckDisposed(null);
				this.charsetDetector.DetectionOptions.RequiredCoverage = value;
			}
		}

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x060004E8 RID: 1256 RVA: 0x00029104 File Offset: 0x00027304
		public LocationIdentifierHelper LocationIdentifierHelperInstance
		{
			get
			{
				this.CheckDisposed(null);
				return this.locationIdentifierHelperInstance;
			}
		}

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x060004E9 RID: 1257 RVA: 0x00029113 File Offset: 0x00027313
		public Dictionary<ItemSchema, object> CoreObjectUpdateContext
		{
			get
			{
				if (this.coreObjectUpdateContext == null)
				{
					this.coreObjectUpdateContext = new Dictionary<ItemSchema, object>();
				}
				return this.coreObjectUpdateContext;
			}
		}

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x060004EA RID: 1258 RVA: 0x0002912E File Offset: 0x0002732E
		MapiMessage ICoreItem.MapiMessage
		{
			get
			{
				return (MapiMessage)CoreObject.GetPersistablePropertyBag(this).MapiProp;
			}
		}

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x060004EB RID: 1259 RVA: 0x00029140 File Offset: 0x00027340
		bool ICoreItem.IsAttachmentCollectionLoaded
		{
			get
			{
				this.CheckDisposed(null);
				return this.attachmentCollection != null && this.attachmentCollection.IsInitialized;
			}
		}

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x060004EC RID: 1260 RVA: 0x0002915E File Offset: 0x0002735E
		bool ICoreItem.AreOptionalAutoloadPropertiesLoaded
		{
			get
			{
				return (this.itemBindOption & ItemBindOption.LoadRequiredPropertiesOnly) != ItemBindOption.LoadRequiredPropertiesOnly;
			}
		}

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x060004ED RID: 1261 RVA: 0x0002916E File Offset: 0x0002736E
		internal bool IsRecipientCollectionLoaded
		{
			get
			{
				return this.recipients != null;
			}
		}

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x060004EE RID: 1262 RVA: 0x0002917C File Offset: 0x0002737C
		protected override StorePropertyDefinition IdProperty
		{
			get
			{
				return CoreItemSchema.Id;
			}
		}

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x060004EF RID: 1263 RVA: 0x00029183 File Offset: 0x00027383
		private IAttachmentProvider AttachmentProvider
		{
			get
			{
				this.CheckDisposed(null);
				if (this.attachmentProvider == null)
				{
					if (((ICoreObject)this).Session != null)
					{
						this.attachmentProvider = new MapiAttachmentProvider();
					}
					else
					{
						this.attachmentProvider = new InMemoryAttachmentProvider();
					}
				}
				return this.attachmentProvider;
			}
		}

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x060004F0 RID: 1264 RVA: 0x000291BA File Offset: 0x000273BA
		private bool IsRecipientCollectionDirty
		{
			get
			{
				return this.IsRecipientCollectionLoaded && this.recipients.IsDirty;
			}
		}

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x060004F1 RID: 1265 RVA: 0x000291D1 File Offset: 0x000273D1
		private bool IsAttachmentCollectionDirty
		{
			get
			{
				return ((ICoreItem)this).IsAttachmentCollectionLoaded && this.attachmentCollection.IsDirty;
			}
		}

		// Token: 0x060004F2 RID: 1266 RVA: 0x000291E8 File Offset: 0x000273E8
		public static CoreItem Bind(StoreSession session, StoreId storeId)
		{
			return CoreItem.Bind(session, storeId, null);
		}

		// Token: 0x060004F3 RID: 1267 RVA: 0x000291F4 File Offset: 0x000273F4
		public static CoreItem Bind(StoreSession session, StoreId storeId, ICollection<PropertyDefinition> propsToReturn)
		{
			Util.ThrowOnNullArgument(session, "session");
			Util.ThrowOnNullArgument(storeId, "storeId");
			if (!IdConverter.IsMessageId(StoreId.GetStoreObjectId(storeId)))
			{
				throw new ArgumentException(ServerStrings.ExInvalidItemId);
			}
			StoreObjectType storeObjectType = StoreId.GetStoreObjectId(storeId).ObjectType;
			if (storeObjectType == StoreObjectType.Unknown)
			{
				storeObjectType = StoreObjectType.Message;
			}
			ItemCreateInfo itemCreateInfo = ItemCreateInfo.GetItemCreateInfo(storeObjectType);
			propsToReturn = InternalSchema.Combine<PropertyDefinition>(itemCreateInfo.Schema.AutoloadProperties, propsToReturn);
			CoreItem result = ItemBuilder.CoreItemBind(session, storeId, null, ItemBindOption.None, propsToReturn, ref storeObjectType);
			session.MessagesWereDownloaded = true;
			return result;
		}

		// Token: 0x060004F4 RID: 1268 RVA: 0x00029298 File Offset: 0x00027498
		public static CoreItem Create(StoreSession session, StoreId parentFolderId, CreateMessageType createMessageType)
		{
			Util.ThrowOnNullArgument(session, "session");
			Util.ThrowOnNullArgument(parentFolderId, "parentFolderId");
			EnumValidator.ThrowIfInvalid<CreateMessageType>(createMessageType, "createMessageType");
			session.CheckSystemFolderAccess(StoreId.GetStoreObjectId(parentFolderId));
			return ItemBuilder.CreateNewCoreItem(session, ItemCreateInfo.MessageItemInfo, true, () => Folder.InternalCreateMapiMessage(session, parentFolderId, createMessageType));
		}

		// Token: 0x060004F5 RID: 1269 RVA: 0x00029334 File Offset: 0x00027534
		public static ImportResult Import(ContentsSynchronizationUploadContext context, CreateMessageType createMessageType, bool failOnConflict, VersionedId itemId, ExDateTime lastModificationTime, byte[] predecessorChangeList, out CoreItem coreItem)
		{
			Util.ThrowOnNullArgument(context, "context");
			EnumValidator.ThrowIfInvalid<CreateMessageType>(createMessageType, "createMessageType");
			Util.ThrowOnNullArgument(itemId, "itemId");
			Util.ThrowOnNullArgument(predecessorChangeList, "predecessorChangeList");
			object[] propertyValues = new object[]
			{
				context.Session.IdConverter.GetLongTermIdFromId(itemId.ObjectId),
				itemId.ChangeKeyAsByteArray(),
				lastModificationTime,
				predecessorChangeList
			};
			coreItem = null;
			MapiMessage mapiMessage = null;
			bool flag = false;
			ImportResult result;
			try
			{
				ImportResult importResult = context.ImportChange(createMessageType, failOnConflict, CoreItem.importChangeProperties, propertyValues, out mapiMessage);
				if (mapiMessage != null)
				{
					coreItem = ItemBuilder.CreateNewCoreItem(context.Session, ItemCreateInfo.GenericItemInfo, itemId, false, () => mapiMessage);
					mapiMessage = null;
				}
				flag = true;
				result = importResult;
			}
			finally
			{
				if (!flag)
				{
					if (coreItem != null)
					{
						coreItem.Dispose();
						coreItem = null;
					}
					if (mapiMessage != null)
					{
						mapiMessage.Dispose();
						mapiMessage = null;
					}
				}
			}
			return result;
		}

		// Token: 0x060004F6 RID: 1270 RVA: 0x00029460 File Offset: 0x00027660
		public static ImportResult Move(ContentsSynchronizationUploadContext destinationContext, StoreObjectId sourceItemId, byte[] sourcePredecessorChangeList, VersionedId destinationItemId)
		{
			Util.ThrowOnNullArgument(destinationContext, "destinationContext");
			Util.ThrowOnNullArgument(sourceItemId, "sourceItemId");
			Util.ThrowOnNullArgument(sourcePredecessorChangeList, "sourcePredecessorChangeList");
			Util.ThrowOnNullArgument(destinationItemId, "destinationItemId");
			FolderChangeOperation operation = CoreFolder.IsMovingToDeletedItems(destinationContext.Session, destinationContext.SyncRootFolderId) ? FolderChangeOperation.MoveToDeletedItems : FolderChangeOperation.Move;
			TeamMailboxClientOperations.ThrowIfInvalidItemOperation(destinationContext.Session, new StoreObjectId[]
			{
				destinationItemId.ObjectId
			});
			StoreObjectId[] array = new StoreObjectId[]
			{
				sourceItemId
			};
			StoreObjectId parentIdFromMessageId = IdConverter.GetParentIdFromMessageId(sourceItemId);
			byte[] array2 = destinationItemId.ChangeKeyAsByteArray();
			GroupOperationResult result = null;
			ImportResult result2;
			using (CallbackContext callbackContext = new CallbackContext(destinationContext.Session))
			{
				try
				{
					destinationContext.Session.OnBeforeFolderChange(operation, FolderChangeOperationFlags.IncludeAll, destinationContext.Session, destinationContext.Session, parentIdFromMessageId, destinationContext.SyncRootFolderId, array, callbackContext);
					try
					{
						result2 = destinationContext.ImportMove(destinationContext.Session.IdConverter.GetLongTermIdFromId(destinationContext.Session.IdConverter.GetFidFromId(sourceItemId)), destinationContext.Session.IdConverter.GetLongTermIdFromId(sourceItemId), sourcePredecessorChangeList, destinationContext.Session.IdConverter.GetLongTermIdFromId(destinationItemId.ObjectId), array2);
					}
					catch (StoragePermanentException storageException)
					{
						result = new GroupOperationResult(OperationResult.Failed, array, storageException);
						throw;
					}
					catch (StorageTransientException storageException2)
					{
						result = new GroupOperationResult(OperationResult.Failed, array, storageException2);
						throw;
					}
					LocalizedException storageException3;
					if (CoreItem.ImportOperationSucceeded(result2, out storageException3))
					{
						result = new GroupOperationResult(OperationResult.Succeeded, array, null, new StoreObjectId[]
						{
							destinationItemId.ObjectId
						}, new byte[][]
						{
							array2
						});
					}
					else
					{
						result = new GroupOperationResult(OperationResult.Failed, array, storageException3);
					}
				}
				finally
				{
					destinationContext.Session.OnAfterFolderChange(operation, FolderChangeOperationFlags.IncludeAll, destinationContext.Session, destinationContext.Session, parentIdFromMessageId, destinationContext.SyncRootFolderId, array, result, callbackContext);
				}
			}
			return result2;
		}

		// Token: 0x060004F7 RID: 1271 RVA: 0x00029678 File Offset: 0x00027878
		public static OperationResult Delete(ContentsSynchronizationUploadContext context, DeleteItemFlags deleteItemFlags, IList<StoreObjectId> itemIds)
		{
			Util.ThrowOnNullArgument(context, "context");
			Util.ThrowOnNullArgument(itemIds, "itemIds");
			EnumValidator.ThrowIfInvalid<DeleteItemFlags>(deleteItemFlags, "deleteItemFlags");
			TeamMailboxClientOperations.ThrowIfInvalidItemOperation(context.Session, itemIds);
			FolderChangeOperation operation = ((deleteItemFlags & DeleteItemFlags.HardDelete) == DeleteItemFlags.HardDelete) ? FolderChangeOperation.HardDelete : FolderChangeOperation.SoftDelete;
			FolderChangeOperationFlags flags = FolderChangeOperationFlags.IncludeAssociated | FolderChangeOperationFlags.IncludeItems;
			GroupOperationResult groupOperationResult = null;
			using (CallbackContext callbackContext = new CallbackContext(context.Session))
			{
				try
				{
					context.CoreFolder.ClearNotReadNotificationPending(itemIds.ToArray<StoreObjectId>());
					bool flag = context.Session.OnBeforeFolderChange(operation, flags, context.Session, null, context.SyncRootFolderId, null, itemIds, callbackContext);
					if (flag)
					{
						groupOperationResult = context.Session.GetCallbackResults();
					}
					else
					{
						context.ImportDeletes(deleteItemFlags, context.Session.IdConverter.GetSourceKeys(itemIds, new Predicate<StoreObjectId>(IdConverter.IsMessageId)));
						groupOperationResult = new GroupOperationResult(OperationResult.Succeeded, itemIds, null);
					}
				}
				catch (StoragePermanentException storageException)
				{
					groupOperationResult = new GroupOperationResult(OperationResult.Failed, itemIds, storageException);
					throw;
				}
				catch (StorageTransientException storageException2)
				{
					groupOperationResult = new GroupOperationResult(OperationResult.Failed, itemIds, storageException2);
					throw;
				}
				finally
				{
					context.Session.OnAfterFolderChange(operation, flags, context.Session, null, context.SyncRootFolderId, null, itemIds, groupOperationResult, callbackContext);
				}
			}
			return groupOperationResult.OperationResult;
		}

		// Token: 0x060004F8 RID: 1272 RVA: 0x000297C4 File Offset: 0x000279C4
		public static void SetReadFlag(ContentsSynchronizationUploadContext context, bool isRead, ICollection<StoreObjectId> itemIds)
		{
			Util.ThrowOnNullArgument(context, "context");
			Util.ThrowOnNullArgument(itemIds, "itemIds");
			TeamMailboxClientOperations.ThrowIfInvalidItemOperation(context.Session, itemIds);
			context.ImportReadStateChange(isRead, context.Session.IdConverter.GetSourceKeys(itemIds, new Predicate<StoreObjectId>(IdConverter.IsMessageId)));
			if (context.Session.ActivitySession != null)
			{
				if (isRead)
				{
					context.Session.ActivitySession.CaptureMarkAsRead(itemIds);
					return;
				}
				context.Session.ActivitySession.CaptureMarkAsUnread(itemIds);
			}
		}

		// Token: 0x060004F9 RID: 1273 RVA: 0x0002984C File Offset: 0x00027A4C
		public void OpenAsReadWrite()
		{
			AcrPropertyBag acrPropertyBag = base.PropertyBag as AcrPropertyBag;
			if (acrPropertyBag != null)
			{
				acrPropertyBag.OpenAsReadWrite();
			}
			this.ResetAttachmentCollection();
			if (this.attachmentCollection != null)
			{
				this.attachmentCollection.OpenAsReadWrite();
			}
		}

		// Token: 0x060004FA RID: 1274 RVA: 0x00029888 File Offset: 0x00027A88
		public ConflictResolutionResult Save(SaveMode saveMode)
		{
			this.CheckDisposed(null);
			ConflictResolutionResult result;
			using (CallbackContext callbackContext = new CallbackContext(base.Session))
			{
				ConflictResolutionResult conflictResolutionResult = this.InternalFlush(saveMode, CoreItemOperation.Save, callbackContext);
				if (conflictResolutionResult.SaveStatus != SaveResult.Success && conflictResolutionResult.SaveStatus != SaveResult.SuccessWithConflictResolution)
				{
					result = conflictResolutionResult;
				}
				else
				{
					ConflictResolutionResult conflictResolutionResult2 = this.InternalSave(saveMode, callbackContext);
					if (conflictResolutionResult2.SaveStatus != SaveResult.Success)
					{
						result = conflictResolutionResult2;
					}
					else
					{
						result = conflictResolutionResult;
					}
				}
			}
			return result;
		}

		// Token: 0x060004FB RID: 1275 RVA: 0x000298FC File Offset: 0x00027AFC
		public ConflictResolutionResult Flush(SaveMode saveMode)
		{
			this.CheckDisposed(null);
			ConflictResolutionResult result;
			using (CallbackContext callbackContext = new CallbackContext(base.Session))
			{
				ConflictResolutionResult conflictResolutionResult = ((ICoreItem)this).InternalFlush(saveMode, callbackContext);
				this.Body.ResetBodyFormat();
				result = conflictResolutionResult;
			}
			return result;
		}

		// Token: 0x060004FC RID: 1276 RVA: 0x00029950 File Offset: 0x00027B50
		public void Submit()
		{
			this.CheckDisposed(null);
			this.Submit(SubmitMessageFlags.None);
		}

		// Token: 0x060004FD RID: 1277 RVA: 0x00029960 File Offset: 0x00027B60
		public void Submit(SubmitMessageFlags submitFlags)
		{
			this.CheckDisposed(null);
			EnumValidator.ThrowIfInvalid<SubmitMessageFlags>(submitFlags, "submitFlags");
			SubmitMessageExFlags submitMessageExFlags = SubmitMessageExFlags.None;
			if ((submitFlags & SubmitMessageFlags.Preprocess) != SubmitMessageFlags.None)
			{
				submitMessageExFlags |= SubmitMessageExFlags.Preprocess;
			}
			if ((submitFlags & SubmitMessageFlags.NeedsSpooler) != SubmitMessageFlags.None)
			{
				submitMessageExFlags |= SubmitMessageExFlags.NeedsSpooler;
			}
			if ((submitFlags & SubmitMessageFlags.IgnoreSendAsRight) != SubmitMessageFlags.None)
			{
				submitMessageExFlags |= SubmitMessageExFlags.IgnoreSendAsRight;
			}
			using (CallbackContext callbackContext = new CallbackContext(base.Session))
			{
				base.Session.OnBeforeItemChange(ItemChangeOperation.Submit, base.Session, null, this, callbackContext);
				this.OnBeforeSend();
				ConflictResolutionResult result = ConflictResolutionResult.Success;
				try
				{
					StoreSession session = base.Session;
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
						((ICoreItem)this).MapiMessage.SubmitMessageEx(submitMessageExFlags);
					}
					catch (MapiPermanentException ex)
					{
						throw StorageGlobals.TranslateMapiException(ServerStrings.MapiCannotSubmitMessage, ex, session, this, "{0}. MapiException = {1}.", new object[]
						{
							string.Format("CoreItem::Submit. The submit flags = {0}.", submitMessageExFlags),
							ex
						});
					}
					catch (MapiRetryableException ex2)
					{
						throw StorageGlobals.TranslateMapiException(ServerStrings.MapiCannotSubmitMessage, ex2, session, this, "{0}. MapiException = {1}.", new object[]
						{
							string.Format("CoreItem::Submit. The submit flags = {0}.", submitMessageExFlags),
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
				catch (Exception)
				{
					result = new ConflictResolutionResult(SaveResult.IrresolvableConflict, null);
					throw;
				}
				finally
				{
					base.PropertyBag.Clear();
					base.Session.OnAfterItemChange(ItemChangeOperation.Submit, base.Session, null, this, result, callbackContext);
				}
			}
		}

		// Token: 0x060004FE RID: 1278 RVA: 0x00029B9C File Offset: 0x00027D9C
		public void TransportSend(out PropertyDefinition[] propertyDefinitions, out object[] propertyValues)
		{
			this.CheckDisposed(null);
			PersistablePropertyBag persistablePropertyBag = CoreObject.GetPersistablePropertyBag(this);
			if (persistablePropertyBag == null)
			{
				throw new NotSupportedException("The item is not sendable.");
			}
			using (CallbackContext callbackContext = new CallbackContext(base.Session))
			{
				base.Session.OnBeforeItemChange(ItemChangeOperation.Submit, base.Session, null, this, callbackContext);
				this.OnBeforeSend();
				ConflictResolutionResult result = ConflictResolutionResult.Success;
				try
				{
					PropValue[] mapiPropValues = null;
					StoreSession session = base.Session;
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
						((ICoreItem)this).MapiMessage.TransportSendMessage(out mapiPropValues);
					}
					catch (MapiPermanentException ex)
					{
						throw StorageGlobals.TranslateMapiException(ServerStrings.MapiCannotTransportSendMessage, ex, session, this, "{0}. MapiException = {1}.", new object[]
						{
							string.Format("CoreItem::TransportSend.", new object[0]),
							ex
						});
					}
					catch (MapiRetryableException ex2)
					{
						throw StorageGlobals.TranslateMapiException(ServerStrings.MapiCannotTransportSendMessage, ex2, session, this, "{0}. MapiException = {1}.", new object[]
						{
							string.Format("CoreItem::TransportSend.", new object[0]),
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
					NativeStorePropertyDefinition[] array;
					PropTag[] array2;
					PropertyTagCache.ResolveAndFilterPropertyValues(NativeStorePropertyDefinition.TypeCheckingFlag.DoNotCreateInvalidType, base.Session, persistablePropertyBag.MapiProp, persistablePropertyBag.ExTimeZone, mapiPropValues, out array, out array2, out propertyValues);
					propertyDefinitions = new PropertyDefinition[array.Length];
					for (int i = 0; i < propertyDefinitions.Length; i++)
					{
						propertyDefinitions[i] = array[i];
					}
				}
				catch (Exception)
				{
					result = new ConflictResolutionResult(SaveResult.IrresolvableConflict, null);
					throw;
				}
				finally
				{
					base.PropertyBag.Clear();
					base.Session.OnAfterItemChange(ItemChangeOperation.Submit, base.Session, null, this, result, callbackContext);
				}
			}
		}

		// Token: 0x060004FF RID: 1279 RVA: 0x00029E18 File Offset: 0x00028018
		public void SetReadFlag(int flags, out bool hasChanged)
		{
			this.CheckDisposed(null);
			int num = (int)base.PropertyBag.TryGetProperty(CoreItemSchema.Flags);
			this.SetReadFlag(flags, true);
			MapiMessage mapiMessage = ((ICoreItem)this).MapiMessage;
			int num2 = 0;
			StoreSession session = base.Session;
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
				num2 = mapiMessage.GetProp(PropTag.MessageFlags).GetInt();
			}
			catch (MapiPermanentException ex)
			{
				throw StorageGlobals.TranslateMapiException(ServerStrings.MapiCannotSetReadFlags, ex, session, this, "{0}. MapiException = {1}.", new object[]
				{
					string.Format("Unable to set read flag on a message. Flags: {0}", flags),
					ex
				});
			}
			catch (MapiRetryableException ex2)
			{
				throw StorageGlobals.TranslateMapiException(ServerStrings.MapiCannotSetReadFlags, ex2, session, this, "{0}. MapiException = {1}.", new object[]
				{
					string.Format("Unable to set read flag on a message. Flags: {0}", flags),
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
			if (!this.IsReadOnly)
			{
				base.PropertyBag[CoreItemSchema.Flags] = ((num2 & 769) | (num & -770));
			}
			hasChanged = (num2 != num);
		}

		// Token: 0x06000500 RID: 1280 RVA: 0x00029FB0 File Offset: 0x000281B0
		public void SetReadFlag(int flags, bool deferErrors)
		{
			this.CheckDisposed(null);
			MapiMessage mapiMessage = ((ICoreItem)this).MapiMessage;
			StoreSession session = base.Session;
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
				if (deferErrors)
				{
					mapiMessage.SetReadFlag((SetReadFlags)(flags | 8));
				}
				else
				{
					mapiMessage.SetReadFlag((SetReadFlags)flags);
				}
			}
			catch (MapiPermanentException ex)
			{
				throw StorageGlobals.TranslateMapiException(ServerStrings.MapiCannotSetReadFlags, ex, session, this, "{0}. MapiException = {1}.", new object[]
				{
					string.Format("Unable to set read flag on a message. Flags: {0}", flags),
					ex
				});
			}
			catch (MapiRetryableException ex2)
			{
				throw StorageGlobals.TranslateMapiException(ServerStrings.MapiCannotSetReadFlags, ex2, session, this, "{0}. MapiException = {1}.", new object[]
				{
					string.Format("Unable to set read flag on a message. Flags: {0}", flags),
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
			if (base.Session.ActivitySession != null)
			{
				if ((112 & flags) != 0)
				{
					return;
				}
				if ((4 & flags) == 4)
				{
					base.Session.ActivitySession.CaptureMarkAsUnread(this);
					return;
				}
				base.Session.ActivitySession.CaptureMarkAsRead(this);
			}
		}

		// Token: 0x06000501 RID: 1281 RVA: 0x0002A128 File Offset: 0x00028328
		public PropertyError[] CopyItem(ICoreItem destinationItem, CopyPropertiesFlags copyPropertiesFlags, CopySubObjects copySubObjects, NativeStorePropertyDefinition[] excludeProperties)
		{
			this.CheckDisposed(null);
			Util.ThrowOnNullArgument(destinationItem, "destinationItem");
			Util.ThrowOnNullArgument(excludeProperties, "excludeProperties");
			EnumValidator.ThrowIfInvalid<CopyPropertiesFlags>(copyPropertiesFlags, "copyPropertiesFlags");
			EnumValidator.ThrowIfInvalid<CopySubObjects>(copySubObjects, "copySubObjects");
			PropertyError[] result = CoreObject.MapiCopyTo(((ICoreItem)this).MapiMessage, destinationItem.MapiMessage, base.Session, destinationItem.Session, copyPropertiesFlags, copySubObjects, excludeProperties);
			destinationItem.Reload();
			destinationItem.SetIrresolvableChange();
			return result;
		}

		// Token: 0x06000502 RID: 1282 RVA: 0x0002A198 File Offset: 0x00028398
		public PropertyError[] CopyProperties(ICoreItem destinationItem, CopyPropertiesFlags copyPropertiesFlags, NativeStorePropertyDefinition[] includeProperties)
		{
			this.CheckDisposed(null);
			Util.ThrowOnNullArgument(destinationItem, "destinationItem");
			Util.ThrowOnNullArgument(includeProperties, "includeProperties");
			EnumValidator.ThrowIfInvalid<CopyPropertiesFlags>(copyPropertiesFlags, "copyPropertiesFlags");
			PropertyError[] result = CoreObject.MapiCopyProps(((ICoreItem)this).MapiMessage, destinationItem.MapiMessage, base.Session, destinationItem.Session, copyPropertiesFlags, includeProperties);
			destinationItem.Reload();
			destinationItem.SetIrresolvableChange();
			return result;
		}

		// Token: 0x06000503 RID: 1283 RVA: 0x0002A1FA File Offset: 0x000283FA
		public void ResetLegallyDirtyProperties()
		{
			this.CheckDisposed(null);
			this.legallyDirtyProperties = null;
		}

		// Token: 0x06000504 RID: 1284 RVA: 0x0002A20C File Offset: 0x0002840C
		public List<string> GetLegallyDirtyProperties()
		{
			this.CheckDisposed(null);
			List<string> result;
			lock (this)
			{
				if (this.legallyDirtyProperties == null)
				{
					result = null;
				}
				else
				{
					result = new List<string>(this.legallyDirtyProperties);
				}
			}
			return result;
		}

		// Token: 0x06000505 RID: 1285 RVA: 0x0002A264 File Offset: 0x00028464
		public bool IsLegallyDirtyProperty(string property)
		{
			return this.legallyDirtyProperties != null && this.legallyDirtyProperties.Contains(property);
		}

		// Token: 0x06000506 RID: 1286 RVA: 0x0002A27C File Offset: 0x0002847C
		public void PopulateLegallyDirtyProperties(StoreSession session, bool verifyLegallyDirty)
		{
			this.CheckDisposed(null);
			Util.ThrowOnNullArgument(session, "session");
			List<string> collection;
			bool flag = COWDumpster.IsItemLegallyDirty(session, this, verifyLegallyDirty, out collection);
			lock (this)
			{
				if (!flag)
				{
					this.legallyDirtyProperties = null;
				}
				else
				{
					this.legallyDirtyProperties = new HashSet<string>(collection, StringComparer.OrdinalIgnoreCase);
				}
			}
		}

		// Token: 0x06000507 RID: 1287 RVA: 0x0002A2EC File Offset: 0x000284EC
		CoreRecipientCollection ICoreItem.GetRecipientCollection(bool forceOpen)
		{
			this.CheckDisposed(null);
			if (!this.IsRecipientCollectionLoaded && forceOpen)
			{
				this.recipients = new CoreRecipientCollection(this);
			}
			return this.recipients;
		}

		// Token: 0x06000508 RID: 1288 RVA: 0x0002A312 File Offset: 0x00028512
		void ICoreItem.OpenAttachmentCollection()
		{
			if (this.attachmentCollection == null)
			{
				this.InternalOpenAttachmentCollection(this, false);
			}
		}

		// Token: 0x06000509 RID: 1289 RVA: 0x0002A324 File Offset: 0x00028524
		void ICoreItem.OpenAttachmentCollection(ICoreItem collectionOwner)
		{
			if (this.attachmentCollection == null)
			{
				this.InternalOpenAttachmentCollection(collectionOwner, true);
			}
		}

		// Token: 0x0600050A RID: 1290 RVA: 0x0002A336 File Offset: 0x00028536
		void ICoreItem.DisposeAttachmentCollection()
		{
			this.CheckDisposed(null);
			if (this.attachmentCollection != null)
			{
				this.attachmentCollection.Dispose();
				this.attachmentCollection = null;
				this.AttachmentProvider.SetCollection(null);
			}
		}

		// Token: 0x0600050B RID: 1291 RVA: 0x0002A365 File Offset: 0x00028565
		public ConflictResolutionResult InternalFlush(SaveMode saveMode, CallbackContext callbackContext)
		{
			return this.InternalFlush(saveMode, CoreItemOperation.Save, callbackContext);
		}

		// Token: 0x0600050C RID: 1292 RVA: 0x0002A370 File Offset: 0x00028570
		public ConflictResolutionResult InternalFlush(SaveMode saveMode, CoreItemOperation operation, CallbackContext callbackContext)
		{
			this.CheckDisposed(null);
			EnumValidator.ThrowIfInvalid<SaveMode>(saveMode, "saveMode");
			TeamMailboxClientOperations.ThrowIfInvalidItemOperation(base.Session, this);
			this.OnBeforeFlush();
			this.CoreObjectUpdate(operation);
			if (base.Session != null)
			{
				if (base.Origin == Origin.Existing)
				{
					bool flag = false;
					bool verifyLegallyDirty = false;
					if (base.Session is MailboxSession)
					{
						flag = (((MailboxSession)base.Session).CowSession != null);
						verifyLegallyDirty = (flag && ((MailboxSession)base.Session).COWSettings.LegalHoldEnabled());
					}
					else if (base.Session is PublicFolderSession)
					{
						flag = (((PublicFolderSession)base.Session).CowSession != null);
					}
					if (flag)
					{
						this.PopulateLegallyDirtyProperties(base.Session, verifyLegallyDirty);
					}
				}
				base.Session.OnBeforeItemChange((base.Origin == Origin.New) ? ItemChangeOperation.Create : ItemChangeOperation.Update, base.Session, ((ICoreObject)this).StoreObjectId, this, callbackContext);
			}
			PersistablePropertyBag persistablePropertyBag = CoreObject.GetPersistablePropertyBag(this);
			if (persistablePropertyBag == null || !persistablePropertyBag.Context.IsValidationDisabled)
			{
				base.ValidateCoreObject();
			}
			if (((IValidatable)this).ValidateAllProperties)
			{
				this.Body.ValidateBody();
				if (ObjectClass.IsSmime(this.ClassName()) && !ObjectClass.IsSmimeClearSigned(this.ClassName()))
				{
					foreach (StorePropertyDefinition propertyDefinition in Body.BodyProps)
					{
						base.PropertyBag.Delete(propertyDefinition);
					}
				}
			}
			((ICoreItem)this).SaveRecipients();
			if (this.attachmentCollection != null && this.AttachmentCollection.IsDirty)
			{
				((ICoreItem)this).SetIrresolvableChange();
			}
			ConflictResolutionResult conflictResolutionResult = ConflictResolutionResult.Success;
			try
			{
				if (this.ShouldApplyACR(saveMode))
				{
					conflictResolutionResult = ((AcrPropertyBag)base.PropertyBag).FlushChangesWithAcr(saveMode);
				}
				else
				{
					CoreObject.GetPersistablePropertyBag(this).FlushChanges();
				}
			}
			finally
			{
				if (conflictResolutionResult.SaveStatus != SaveResult.Success && conflictResolutionResult.SaveStatus != SaveResult.SuccessWithConflictResolution && base.Session != null)
				{
					base.Session.OnAfterItemChange((base.Origin == Origin.New) ? ItemChangeOperation.Create : ItemChangeOperation.Update, base.Session, null, this, conflictResolutionResult, callbackContext);
				}
			}
			return conflictResolutionResult;
		}

		// Token: 0x0600050D RID: 1293 RVA: 0x0002A578 File Offset: 0x00028778
		void ICoreItem.SaveRecipients()
		{
			this.CheckDisposed(null);
			if (this.IsRecipientCollectionDirty)
			{
				((ICoreItem)this).SetIrresolvableChange();
				this.recipients.Save();
			}
		}

		// Token: 0x0600050E RID: 1294 RVA: 0x0002A59A File Offset: 0x0002879A
		void ICoreItem.AbandonRecipientChanges()
		{
			this.CheckDisposed(null);
			Util.DisposeIfPresent(this.recipients);
			this.recipients = null;
		}

		// Token: 0x0600050F RID: 1295 RVA: 0x0002A5B8 File Offset: 0x000287B8
		void ICoreItem.Reload()
		{
			this.CheckDisposed(null);
			((ILocationIdentifierController)this).LocationIdentifierHelperInstance.SetLocationIdentifier(56351U, LastChangeAction.Reload);
			((ICoreObject)this).PropertyBag.Reload();
			((ICoreItem)this).AbandonRecipientChanges();
			this.ResetAttachmentCollection();
			((ICoreItem)this).Body.Reset();
		}

		// Token: 0x06000510 RID: 1296 RVA: 0x0002A604 File Offset: 0x00028804
		void ICoreItem.SetIrresolvableChange()
		{
			this.CheckDisposed(null);
			AcrPropertyBag acrPropertyBag = base.PropertyBag as AcrPropertyBag;
			if (acrPropertyBag != null)
			{
				acrPropertyBag.SetIrresolvableChange();
			}
		}

		// Token: 0x06000511 RID: 1297 RVA: 0x0002A630 File Offset: 0x00028830
		public void GetCharsetDetectionData(StringBuilder stringBuilder, CharsetDetectionDataFlags flags)
		{
			this.CheckDisposed(null);
			Util.ThrowOnNullArgument(stringBuilder, "stringBuilder");
			EnumValidator.ThrowIfInvalid<CharsetDetectionDataFlags>(flags, "flags");
			bool isComplete = (flags & CharsetDetectionDataFlags.Complete) == CharsetDetectionDataFlags.Complete;
			this.GetPropertyCharsetDetectionData(stringBuilder, isComplete);
			this.GetAttachmentCharsetDetectionData(stringBuilder, isComplete);
			this.GetRecipientCharsetDetectionData(stringBuilder, isComplete);
			this.context.GetContextCharsetDetectionData(stringBuilder, flags);
		}

		// Token: 0x06000512 RID: 1298 RVA: 0x0002A686 File Offset: 0x00028886
		void ICoreItem.SetCoreItemContext(ICoreItemContext context)
		{
			this.CheckDisposed(null);
			this.context = context;
		}

		// Token: 0x06000513 RID: 1299 RVA: 0x0002A696 File Offset: 0x00028896
		internal static void CopyItemContentExcept(ICoreItem sourceItem, ICoreItem destinationItem, HashSet<NativeStorePropertyDefinition> excludeProperties)
		{
			CoreItem.CopyItemContent(sourceItem, destinationItem, null, excludeProperties);
		}

		// Token: 0x06000514 RID: 1300 RVA: 0x0002A6A1 File Offset: 0x000288A1
		internal static void CopyItemContent(ICoreItem sourceItem, ICoreItem destinationItem)
		{
			CoreItem.CopyItemContent(sourceItem, destinationItem, null, null);
		}

		// Token: 0x06000515 RID: 1301 RVA: 0x0002A6AC File Offset: 0x000288AC
		internal static void CopyItemContent(ICoreItem sourceItem, ICoreItem destinationItem, IEnumerable<NativeStorePropertyDefinition> properties)
		{
			CoreItem.CopyItemContent(sourceItem, destinationItem, properties, null);
		}

		// Token: 0x06000516 RID: 1302 RVA: 0x0002A6B8 File Offset: 0x000288B8
		private static void CopyItemContent(ICoreItem sourceItem, ICoreItem destinationItem, IEnumerable<NativeStorePropertyDefinition> properties, HashSet<NativeStorePropertyDefinition> excludeProperties)
		{
			if (properties == null)
			{
				sourceItem.PropertyBag.Load(CoreObjectSchema.AllPropertiesOnStore);
				properties = CoreObject.GetPersistablePropertyBag(sourceItem).AllNativeProperties;
			}
			foreach (NativeStorePropertyDefinition nativeStorePropertyDefinition in properties)
			{
				if (excludeProperties == null || !excludeProperties.Contains(nativeStorePropertyDefinition))
				{
					PersistablePropertyBag.CopyProperty(CoreObject.GetPersistablePropertyBag(sourceItem), nativeStorePropertyDefinition, CoreObject.GetPersistablePropertyBag(destinationItem));
				}
			}
			destinationItem.Recipients.CopyRecipientsFrom(sourceItem.Recipients);
			CoreAttachmentCollection.CloneAttachmentCollection(sourceItem, destinationItem);
			CoreObject.GetPersistablePropertyBag(destinationItem).SaveFlags |= (PropertyBagSaveFlags.IgnoreMapiComputedErrors | PropertyBagSaveFlags.IgnoreUnresolvedHeaders);
		}

		// Token: 0x06000517 RID: 1303 RVA: 0x0002A764 File Offset: 0x00028964
		protected override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<CoreItem>(this);
		}

		// Token: 0x06000518 RID: 1304 RVA: 0x0002A76C File Offset: 0x0002896C
		protected override void InternalDispose(bool disposing)
		{
			if (disposing)
			{
				if (this.recipients != null)
				{
					this.recipients.Dispose();
					this.recipients = null;
				}
				if (this.attachmentCollection != null)
				{
					this.attachmentCollection.Dispose();
					this.attachmentCollection = null;
				}
				if (this.attachmentProvider != null)
				{
					this.attachmentProvider.Dispose();
					this.attachmentProvider = null;
				}
			}
			base.InternalDispose(disposing);
		}

		// Token: 0x06000519 RID: 1305 RVA: 0x0002A7D4 File Offset: 0x000289D4
		protected override Schema GetSchema()
		{
			string valueOrDefault = base.PropertyBag.GetValueOrDefault<string>(CoreItemSchema.ItemClass, string.Empty);
			Schema schema = ObjectClass.GetSchema(valueOrDefault);
			if (!(schema is ItemSchema))
			{
				schema = ItemSchema.Instance;
			}
			ICollection<PropertyDefinition> propsToLoad = ((this.itemBindOption & ItemBindOption.LoadRequiredPropertiesOnly) == ItemBindOption.LoadRequiredPropertiesOnly) ? schema.RequiredAutoloadProperties : schema.AutoloadProperties;
			base.PropertyBag.Load(propsToLoad);
			return schema;
		}

		// Token: 0x0600051A RID: 1306 RVA: 0x0002A834 File Offset: 0x00028A34
		protected override void ValidateContainedObjects(IList<StoreObjectValidationError> validationErrors)
		{
			base.ValidateContainedObjects(validationErrors);
			if (this.IsRecipientCollectionLoaded)
			{
				foreach (CoreRecipient coreRecipient in this.recipients)
				{
					ValidationContext validationContext = new ValidationContext(base.Session);
					((IValidatable)coreRecipient).Validate(validationContext, validationErrors);
				}
			}
		}

		// Token: 0x0600051B RID: 1307 RVA: 0x0002A8A0 File Offset: 0x00028AA0
		private static bool ImportOperationSucceeded(ImportResult result, out LocalizedException exception)
		{
			exception = null;
			switch (result)
			{
			case ImportResult.Success:
			case ImportResult.SyncClientChangeNewer:
			case ImportResult.SyncIgnore:
				return true;
			}
			exception = new ImportException(ServerStrings.CannotImportMessageMove, result);
			return false;
		}

		// Token: 0x0600051C RID: 1308 RVA: 0x0002A8DC File Offset: 0x00028ADC
		private void SetClientAndServerIPs()
		{
			if (base.Session != null && base.PropertyBag != null)
			{
				IPAddress clientIPAddress = base.Session.ClientIPAddress;
				IPAddress serverIPAddress = base.Session.ServerIPAddress;
				if (clientIPAddress.Equals(serverIPAddress) && clientIPAddress.Equals(IPAddress.IPv6Loopback))
				{
					return;
				}
				base.PropertyBag.SetProperty(CoreItemSchema.XMsExchOrganizationOriginalClientIPAddress, clientIPAddress.ToString());
				base.PropertyBag.SetProperty(CoreItemSchema.XMsExchOrganizationOriginalServerIPAddress, serverIPAddress.ToString());
			}
		}

		// Token: 0x0600051D RID: 1309 RVA: 0x0002A954 File Offset: 0x00028B54
		private void InternalOpenAttachmentCollection(ICoreItem owner, bool forceReadOnly)
		{
			bool valueOrDefault = owner.PropertyBag.GetValueOrDefault<bool>(CoreItemSchema.MapiHasAttachment, true);
			this.attachmentCollection = new CoreAttachmentCollection(this.AttachmentProvider, owner, forceReadOnly, valueOrDefault);
			this.AttachmentProvider.SetCollection(this.attachmentCollection);
		}

		// Token: 0x0600051E RID: 1310 RVA: 0x0002A998 File Offset: 0x00028B98
		private void ResetAttachmentCollection()
		{
			this.CheckDisposed(null);
			if (this.attachmentCollection != null)
			{
				this.attachmentCollection.Reset();
			}
		}

		// Token: 0x0600051F RID: 1311 RVA: 0x0002A9B4 File Offset: 0x00028BB4
		private void CoreObjectUpdate(CoreItemOperation operation)
		{
			this.coreObjectUpdateSchema = (ItemSchema)this.GetSchema();
			this.coreObjectUpdateSchema.CoreObjectUpdate(this, operation);
		}

		// Token: 0x06000520 RID: 1312 RVA: 0x0002A9D4 File Offset: 0x00028BD4
		private void CoreObjectUpdateComplete(SaveResult saveResult)
		{
			if (this.coreObjectUpdateSchema != null)
			{
				this.coreObjectUpdateSchema.CoreObjectUpdateComplete(this, saveResult);
				this.coreObjectUpdateSchema = null;
			}
		}

		// Token: 0x06000521 RID: 1313 RVA: 0x0002A9F2 File Offset: 0x00028BF2
		private bool ShouldApplyACR(SaveMode saveMode)
		{
			return base.PropertyBag is AcrPropertyBag && saveMode != SaveMode.NoConflictResolution && saveMode != SaveMode.NoConflictResolutionForceSave;
		}

		// Token: 0x06000522 RID: 1314 RVA: 0x0002AA0E File Offset: 0x00028C0E
		private void OnBeforeFlush()
		{
			if (this.beforeFlushEventHandler != null)
			{
				this.beforeFlushEventHandler();
			}
		}

		// Token: 0x06000523 RID: 1315 RVA: 0x0002AA38 File Offset: 0x00028C38
		private void OnBeforeSend()
		{
			if (!base.Session.CheckSubmissionQuota(this.Recipients.Count))
			{
				throw new SubmissionQuotaExceededException(ServerStrings.ExSubmissionQuotaExceeded);
			}
			this.EnsureNoUnresolvedRecipients();
			if (!base.PropertyBag.GetValueOrDefault<bool>(CoreItemSchema.IsResend))
			{
				this.Recipients.FilterRecipients((CoreRecipient recipient) => recipient.RecipientItemType != RecipientItemType.P1);
			}
			if (PropertyError.IsPropertyNotFound(base.PropertyBag.TryGetProperty(CoreItemSchema.DavSubmitData)) && this.Recipients.Count == 0)
			{
				throw new InvalidRecipientsException(ServerStrings.ExCantSubmitWithoutRecipients, null);
			}
			this.SetClientAndServerIPs();
			bool isFlushNeeded = this.IsFlushNeeded;
			if (this.beforeSendEventHandler != null)
			{
				this.beforeSendEventHandler();
			}
			if (this.IsFlushNeeded || !isFlushNeeded)
			{
				using (CallbackContext callbackContext = new CallbackContext(base.Session))
				{
					ConflictResolutionResult conflictResolutionResult = this.InternalFlush(SaveMode.ResolveConflicts, CoreItemOperation.Send, callbackContext);
					if (conflictResolutionResult.SaveStatus == SaveResult.IrresolvableConflict)
					{
						throw new SaveConflictException(ServerStrings.MapiCannotSubmitMessage, conflictResolutionResult);
					}
				}
			}
		}

		// Token: 0x06000524 RID: 1316 RVA: 0x0002AB4C File Offset: 0x00028D4C
		private void EnsureNoUnresolvedRecipients()
		{
			foreach (CoreRecipient coreRecipient in this.Recipients)
			{
				if (string.IsNullOrEmpty(coreRecipient.PropertyBag.GetValueOrDefault<string>(RecipientSchema.EmailAddrType)))
				{
					throw new InvalidRecipientsException(ServerStrings.ExUnresolvedRecipient(coreRecipient.PropertyBag.GetValueOrDefault<string>(RecipientSchema.EmailDisplayName)), null);
				}
			}
		}

		// Token: 0x06000525 RID: 1317 RVA: 0x0002ABC8 File Offset: 0x00028DC8
		private void GetPropertyCharsetDetectionData(StringBuilder stringBuilder, bool isComplete)
		{
			foreach (StorePropertyDefinition propertyDefinition in this.GetSchema().DetectCodepageProperties)
			{
				if (isComplete || base.PropertyBag.IsPropertyDirty(propertyDefinition))
				{
					object obj = base.PropertyBag.TryGetProperty(propertyDefinition);
					if (obj is string)
					{
						stringBuilder.AppendLine(obj as string);
					}
					else if (obj is string[])
					{
						foreach (string value in obj as string[])
						{
							stringBuilder.AppendLine(value);
						}
					}
				}
			}
		}

		// Token: 0x06000526 RID: 1318 RVA: 0x0002AC7C File Offset: 0x00028E7C
		private void GetAttachmentCharsetDetectionData(StringBuilder stringBuilder, bool isComplete)
		{
			if ((isComplete || (((ICoreItem)this).IsAttachmentCollectionLoaded && this.AttachmentCollection.IsDirty)) && (base.Origin != Origin.New || ((ICoreItem)this).IsAttachmentCollectionLoaded))
			{
				this.AttachmentCollection.GetCharsetDetectionData(stringBuilder);
			}
		}

		// Token: 0x06000527 RID: 1319 RVA: 0x0002ACB4 File Offset: 0x00028EB4
		private void GetRecipientCharsetDetectionData(StringBuilder stringBuilder, bool isComplete)
		{
			((ICoreItem)this).GetRecipientCollection(false);
			if (isComplete || this.recipients != null)
			{
				foreach (CoreRecipient coreRecipient in this.Recipients)
				{
					coreRecipient.GetCharsetDetectionData(stringBuilder);
				}
			}
		}

		// Token: 0x06000528 RID: 1320 RVA: 0x0002AD14 File Offset: 0x00028F14
		public ConflictResolutionResult InternalSave(SaveMode saveMode, CallbackContext callbackContext)
		{
			this.CheckDisposed(null);
			EnumValidator.ThrowIfInvalid<SaveMode>(saveMode, "saveMode");
			TeamMailboxClientOperations.ThrowIfInvalidItemOperation(base.Session, this);
			ConflictResolutionResult conflictResolutionResult = ConflictResolutionResult.Failure;
			using (MailboxEvaluationResult mailboxEvaluationResult = (base.Session != null) ? base.Session.EvaluateFolderRules(this, null) : null)
			{
				if (base.Session != null)
				{
					base.Session.OnBeforeItemSave((base.Origin == Origin.New) ? ItemChangeOperation.Create : ItemChangeOperation.Update, base.Session, ((ICoreObject)this).StoreObjectId, this, callbackContext);
					callbackContext.ItemOperationAuditInfo = new ItemAuditInfo((base.Id == null) ? null : base.Id.ObjectId, null, null, base.PropertyBag.TryGetProperty(CoreItemSchema.Subject) as string, base.PropertyBag.TryGetProperty(ItemSchema.From) as Participant, base.PropertyBag.GetValueOrDefault<bool>(InternalSchema.IsAssociated), this.GetLegallyDirtyProperties());
				}
				try
				{
					bool flag = false;
					if (base.Session != null)
					{
						FolderRuleEvaluationStatus folderRuleEvaluationStatus = base.Session.ExecuteFolderRulesOnBefore(mailboxEvaluationResult);
						if (FolderRuleEvaluationStatus.InterruptWithException == folderRuleEvaluationStatus)
						{
							throw new StoragePermanentException(ServerStrings.FolderRuleCannotSaveItem);
						}
						if (FolderRuleEvaluationStatus.InterruptSilently == folderRuleEvaluationStatus)
						{
							flag = true;
							conflictResolutionResult = ConflictResolutionResult.SuccessWithoutSaving;
						}
					}
					if (!flag)
					{
						if (this.ShouldApplyACR(saveMode))
						{
							conflictResolutionResult = ((AcrPropertyBag)base.PropertyBag).SaveChangesWithAcr(saveMode);
							if (conflictResolutionResult.SaveStatus == SaveResult.SuccessWithConflictResolution)
							{
								((ICoreItem)this).AbandonRecipientChanges();
							}
						}
						else
						{
							bool force = (saveMode & SaveMode.NoConflictResolutionForceSave) == SaveMode.NoConflictResolutionForceSave;
							CoreObject.GetPersistablePropertyBag(this).SaveChanges(force);
							conflictResolutionResult = ConflictResolutionResult.Success;
						}
						this.CoreObjectUpdateComplete(conflictResolutionResult.SaveStatus);
						this.LocationIdentifierHelperInstance.ResetChangeList();
					}
					if (this.attachmentCollection != null)
					{
						this.attachmentCollection.OnAfterCoreItemSave(conflictResolutionResult.SaveStatus);
					}
				}
				finally
				{
					if (base.Session != null && SaveResult.IrresolvableConflict != conflictResolutionResult.SaveStatus)
					{
						base.Session.ExecuteFolderRulesOnAfter(mailboxEvaluationResult);
					}
					if (base.Session != null)
					{
						base.Session.OnAfterItemSave((base.Origin == Origin.New) ? ItemChangeOperation.Create : ItemChangeOperation.Update, base.Session, null, this, conflictResolutionResult, callbackContext);
					}
				}
			}
			base.Origin = Origin.Existing;
			((ICoreObject)this).ResetId();
			this.Body.Reset();
			return conflictResolutionResult;
		}

		// Token: 0x0400014E RID: 334
		private static readonly IList<PropertyDefinition> importChangeProperties = new ReadOnlyCollection<PropertyDefinition>(new PropertyDefinition[]
		{
			CoreObjectSchema.SourceKey,
			CoreObjectSchema.ChangeKey,
			CoreObjectSchema.LastModifiedTime,
			CoreObjectSchema.PredecessorChangeList
		});

		// Token: 0x0400014F RID: 335
		private readonly ItemCharsetDetector charsetDetector;

		// Token: 0x04000150 RID: 336
		private ItemBindOption itemBindOption;

		// Token: 0x04000151 RID: 337
		private CoreAttachmentCollection attachmentCollection;

		// Token: 0x04000152 RID: 338
		private IAttachmentProvider attachmentProvider;

		// Token: 0x04000153 RID: 339
		private CoreRecipientCollection recipients;

		// Token: 0x04000154 RID: 340
		private ICoreItem topLevelItem;

		// Token: 0x04000155 RID: 341
		private Action beforeSendEventHandler;

		// Token: 0x04000156 RID: 342
		private Action beforeFlushEventHandler;

		// Token: 0x04000157 RID: 343
		private Body body;

		// Token: 0x04000158 RID: 344
		private ICoreItemContext context = NullCoreItemContext.Instance;

		// Token: 0x04000159 RID: 345
		private Dictionary<ItemSchema, object> coreObjectUpdateContext;

		// Token: 0x0400015A RID: 346
		private ItemSchema coreObjectUpdateSchema;

		// Token: 0x0400015B RID: 347
		private LocationIdentifierHelper locationIdentifierHelperInstance;

		// Token: 0x0400015C RID: 348
		private HashSet<string> legallyDirtyProperties;
	}
}
