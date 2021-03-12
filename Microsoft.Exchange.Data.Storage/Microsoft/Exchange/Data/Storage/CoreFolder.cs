using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Storage.LinkedFolder;
using Microsoft.Exchange.Data.Storage.PublicFolder;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000031 RID: 49
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class CoreFolder : CoreObject
	{
		// Token: 0x06000440 RID: 1088 RVA: 0x00026140 File Offset: 0x00024340
		internal CoreFolder(StoreSession session, FolderSchema schema, PersistablePropertyBag propertyBag, StoreObjectId storeObjectId, byte[] changeKey, Origin origin, ICollection<PropertyDefinition> prefetchProperties) : base(session, propertyBag, storeObjectId, changeKey, origin, ItemLevel.TopLevel, prefetchProperties)
		{
			using (DisposeGuard disposeGuard = this.Guard())
			{
				this.schema = schema;
				this.queryExecutor = new QueryExecutor(base.Session, new QueryExecutor.GetMapiFolderDelegate(this.GetMapiFolder));
				disposeGuard.Success();
			}
		}

		// Token: 0x06000441 RID: 1089 RVA: 0x000261B0 File Offset: 0x000243B0
		protected override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<CoreFolder>(this);
		}

		// Token: 0x06000442 RID: 1090 RVA: 0x000261B8 File Offset: 0x000243B8
		public static CoreFolder Bind(StoreSession session, StoreId folderId)
		{
			return CoreFolder.Bind(session, folderId, null);
		}

		// Token: 0x06000443 RID: 1091 RVA: 0x000261C2 File Offset: 0x000243C2
		public static CoreFolder Bind(StoreSession session, StoreId folderId, params PropertyDefinition[] propsToReturn)
		{
			return CoreFolder.Bind(session, folderId, false, (ICollection<PropertyDefinition>)propsToReturn);
		}

		// Token: 0x06000444 RID: 1092 RVA: 0x000261D2 File Offset: 0x000243D2
		public static CoreFolder Bind(StoreSession session, StoreId folderId, bool allowSoftDeleted, params PropertyDefinition[] propsToReturn)
		{
			return CoreFolder.Bind(session, folderId, allowSoftDeleted, (ICollection<PropertyDefinition>)propsToReturn);
		}

		// Token: 0x06000445 RID: 1093 RVA: 0x000261E2 File Offset: 0x000243E2
		public static CoreFolder Bind(StoreSession session, StoreId folderId, ICollection<PropertyDefinition> propsToReturn)
		{
			return CoreFolder.Bind(session, folderId, false, propsToReturn);
		}

		// Token: 0x06000446 RID: 1094 RVA: 0x000261ED File Offset: 0x000243ED
		public static CoreFolder Bind(StoreSession session, StoreId folderId, bool allowSoftDeleted, ICollection<PropertyDefinition> propsToReturn)
		{
			Util.ThrowOnNullArgument(session, "session");
			Util.ThrowOnNullArgument(folderId, "folderId");
			return CoreFolder.InternalBind(session, folderId, allowSoftDeleted, propsToReturn);
		}

		// Token: 0x06000447 RID: 1095 RVA: 0x0002620E File Offset: 0x0002440E
		public static CoreFolder Create(StoreSession session, StoreId parentId, bool isSearchFolder, string displayName, CreateMode createMode)
		{
			Util.ThrowOnNullArgument(session, "session");
			Util.ThrowOnNullArgument(parentId, "parentId");
			Util.ThrowOnNullOrEmptyArgument(displayName, "displayName");
			EnumValidator.ThrowIfInvalid<CreateMode>(createMode, "createMode");
			return CoreFolder.InternalCreate(session, parentId, isSearchFolder, displayName, createMode, false, FolderCreateInfo.GenericFolderInfo);
		}

		// Token: 0x06000448 RID: 1096 RVA: 0x0002624E File Offset: 0x0002444E
		public static CoreFolder CreateSecure(StoreSession session, StoreId parentId, bool isSearchFolder, string displayName, CreateMode createMode)
		{
			Util.ThrowOnNullArgument(session, "session");
			Util.ThrowOnNullArgument(parentId, "parentId");
			Util.ThrowOnNullOrEmptyArgument(displayName, "displayName");
			EnumValidator.ThrowIfInvalid<CreateMode>(createMode, "createMode");
			return CoreFolder.InternalCreate(session, parentId, isSearchFolder, displayName, createMode, true, FolderCreateInfo.GenericFolderInfo);
		}

		// Token: 0x06000449 RID: 1097 RVA: 0x00026290 File Offset: 0x00024490
		public static CoreFolder Import(HierarchySynchronizationUploadContext context, StoreObjectId parentFolderId, VersionedId folderId, ExDateTime lastModificationTime, byte[] predecessorChangeList, string displayName)
		{
			Util.ThrowOnNullArgument(context, "context");
			Util.ThrowOnNullArgument(parentFolderId, "parentFolderId");
			Util.ThrowOnNullArgument(folderId, "folderId");
			Util.ThrowOnNullArgument(predecessorChangeList, "predecessorChangeList");
			Util.ThrowOnNullOrEmptyArgument(displayName, "displayName");
			context.Session.ValidateOperation(FolderChangeOperation.Create, parentFolderId);
			CoreFolder coreFolder = null;
			FolderPropertyBag folderPropertyBag = null;
			FolderCreateInfo folderCreateInfo = (folderId.ObjectId.ObjectType == StoreObjectType.SearchFolder) ? FolderCreateInfo.SearchFolderInfo : FolderCreateInfo.GenericFolderInfo;
			bool flag = false;
			CoreFolder result;
			try
			{
				folderPropertyBag = new FolderImportPropertyBag(context, parentFolderId, StoreId.GetStoreObjectId(folderId), folderCreateInfo.Schema.AutoloadProperties);
				coreFolder = new CoreFolder(context.Session, folderCreateInfo.Schema, folderPropertyBag, null, null, Origin.New, folderCreateInfo.Schema.AutoloadProperties);
				coreFolder.PropertyBag[CoreObjectSchema.ParentSourceKey] = context.Session.IdConverter.GetLongTermIdFromId(parentFolderId);
				coreFolder.PropertyBag[CoreObjectSchema.SourceKey] = context.Session.IdConverter.GetLongTermIdFromId(folderId.ObjectId);
				coreFolder.PropertyBag[CoreObjectSchema.LastModifiedTime] = lastModificationTime;
				coreFolder.PropertyBag[CoreObjectSchema.ChangeKey] = folderId.ChangeKeyAsByteArray();
				coreFolder.PropertyBag[CoreObjectSchema.PredecessorChangeList] = predecessorChangeList;
				coreFolder.PropertyBag[CoreFolderSchema.DisplayName] = displayName;
				flag = true;
				result = coreFolder;
			}
			finally
			{
				if (!flag)
				{
					Util.DisposeIfPresent(coreFolder);
					Util.DisposeIfPresent(folderPropertyBag);
				}
			}
			return result;
		}

		// Token: 0x0600044A RID: 1098 RVA: 0x00026400 File Offset: 0x00024600
		public static OperationResult Delete(HierarchySynchronizationUploadContext context, DeleteItemFlags deleteItemFlags, IList<StoreObjectId> folderIds)
		{
			Util.ThrowOnNullArgument(context, "context");
			Util.ThrowOnNullArgument(folderIds, "folderIds");
			EnumValidator.ThrowIfInvalid<DeleteItemFlags>(deleteItemFlags, "deleteItemFlags");
			TeamMailboxClientOperations.ThrowIfInvalidFolderOperation(context.Session, folderIds);
			FolderChangeOperation folderChangeOperation = ((deleteItemFlags & DeleteItemFlags.HardDelete) == DeleteItemFlags.HardDelete) ? FolderChangeOperation.HardDelete : FolderChangeOperation.SoftDelete;
			foreach (StoreObjectId folderId in folderIds)
			{
				context.Session.ValidateOperation(folderChangeOperation, folderId);
			}
			FolderChangeOperationFlags folderChangeOperationFlags = CoreFolder.GetFolderChangeOperationFlags(FolderChangeOperationFlags.IncludeAll, deleteItemFlags);
			GroupOperationResult groupOperationResult = null;
			using (CallbackContext callbackContext = new CallbackContext(context.Session))
			{
				try
				{
					bool flag = context.Session.OnBeforeFolderChange(folderChangeOperation, folderChangeOperationFlags, context.Session, null, null, null, folderIds, callbackContext);
					if (flag)
					{
						groupOperationResult = context.Session.GetCallbackResults();
					}
					else
					{
						context.ImportDeletes(deleteItemFlags, context.Session.IdConverter.GetSourceKeys(folderIds, new Predicate<StoreObjectId>(IdConverter.IsFolderId)));
						groupOperationResult = new GroupOperationResult(OperationResult.Succeeded, folderIds, null);
					}
				}
				catch (StoragePermanentException storageException)
				{
					groupOperationResult = new GroupOperationResult(OperationResult.Failed, folderIds, storageException);
					throw;
				}
				catch (StorageTransientException storageException2)
				{
					groupOperationResult = new GroupOperationResult(OperationResult.Failed, folderIds, storageException2);
					throw;
				}
				finally
				{
					context.Session.OnAfterFolderChange(folderChangeOperation, folderChangeOperationFlags, context.Session, null, null, null, folderIds, groupOperationResult, callbackContext);
				}
			}
			return groupOperationResult.OperationResult;
		}

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x0600044B RID: 1099 RVA: 0x0002657C File Offset: 0x0002477C
		public QueryExecutor QueryExecutor
		{
			get
			{
				this.CheckDisposed(null);
				return this.queryExecutor;
			}
		}

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x0600044C RID: 1100 RVA: 0x0002658B File Offset: 0x0002478B
		// (set) Token: 0x0600044D RID: 1101 RVA: 0x0002659F File Offset: 0x0002479F
		public PropertyBagSaveFlags SaveFlags
		{
			get
			{
				this.CheckDisposed(null);
				return this.FolderPropertyBag.SaveFlags;
			}
			set
			{
				this.CheckDisposed(null);
				EnumValidator.ThrowIfInvalid<PropertyBagSaveFlags>(value, "value");
				this.FolderPropertyBag.SaveFlags = value;
			}
		}

		// Token: 0x0600044E RID: 1102 RVA: 0x000265BF File Offset: 0x000247BF
		public IModifyTable GetRuleTable()
		{
			return this.GetRuleTable(new RuleTableRestriction(this));
		}

		// Token: 0x0600044F RID: 1103 RVA: 0x000265CD File Offset: 0x000247CD
		public IModifyTable GetRuleTable(IModifyTableRestriction modifyTableRestriction)
		{
			return new PropertyTable(this, CoreFolderSchema.MapiRulesTable, ModifyTableOptions.None, modifyTableRestriction);
		}

		// Token: 0x06000450 RID: 1104 RVA: 0x000265DC File Offset: 0x000247DC
		public SearchFolderCriteria GetSearchCriteria(bool needsConvertToSmartFilter)
		{
			this.CheckDisposed(null);
			Restriction restriction = null;
			byte[][] array = null;
			SearchState searchState = SearchState.None;
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
				this.GetMapiFolder().GetSearchCriteria(out restriction, out array, out searchState);
			}
			catch (MapiPermanentException ex)
			{
				throw StorageGlobals.TranslateMapiException(ServerStrings.ExCannotGetSearchCriteria, ex, session, this, "{0}. MapiException = {1}.", new object[]
				{
					string.Format("CoreFolder::GetSearchCriteria.", new object[0]),
					ex
				});
			}
			catch (MapiRetryableException ex2)
			{
				throw StorageGlobals.TranslateMapiException(ServerStrings.ExCannotGetSearchCriteria, ex2, session, this, "{0}. MapiException = {1}.", new object[]
				{
					string.Format("CoreFolder::GetSearchCriteria.", new object[0]),
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
			Restriction.CountRestriction countRestriction = null;
			if (restriction is Restriction.CountRestriction)
			{
				countRestriction = (Restriction.CountRestriction)restriction;
				restriction = countRestriction.Restriction;
			}
			QueryFilter searchQuery = FilterRestrictionConverter.CreateFilter(base.Session, this.GetMapiFolder(), restriction, needsConvertToSmartFilter);
			StoreId[] array2 = new StoreObjectId[array.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array2[i] = StoreObjectId.FromProviderSpecificId(array[i]);
			}
			SearchFolderCriteria searchFolderCriteria = new SearchFolderCriteria(searchQuery, array2, searchState);
			if (countRestriction != null)
			{
				searchFolderCriteria.MaximumResultsCount = new int?(countRestriction.Count);
			}
			return searchFolderCriteria;
		}

		// Token: 0x06000451 RID: 1105 RVA: 0x00026798 File Offset: 0x00024998
		public void SetSearchCriteria(SearchFolderCriteria searchFolderCriteria, SetSearchCriteriaFlags setSearchCriteriaFlags)
		{
			this.CheckDisposed(null);
			EnumValidator.ThrowIfInvalid<SetSearchCriteriaFlags>(setSearchCriteriaFlags);
			Util.ThrowOnNullArgument(searchFolderCriteria, "searchFolderCriteria");
			SearchCriteriaFlags flags = MapiUtil.SetSearchCriteriaFlagsToMapiSearchCriteriaFlags(setSearchCriteriaFlags);
			Restriction restriction = null;
			if (searchFolderCriteria.SearchQuery != null)
			{
				restriction = FilterRestrictionConverter.CreateRestriction(base.Session, base.Session.ExTimeZone, this.GetMapiFolder(), searchFolderCriteria.SearchQuery);
				if (searchFolderCriteria.MaximumResultsCount != null)
				{
					restriction = new Restriction.CountRestriction(searchFolderCriteria.MaximumResultsCount.Value, restriction);
				}
			}
			byte[][] entryIds = null;
			if (searchFolderCriteria.FolderScope != null && searchFolderCriteria.FolderScope.Length > 0)
			{
				entryIds = StoreId.StoreIdsToEntryIds(searchFolderCriteria.FolderScope);
			}
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
				this.GetMapiFolder().SetSearchCriteria(restriction, entryIds, flags);
			}
			catch (MapiPermanentException ex)
			{
				throw StorageGlobals.TranslateMapiException(ServerStrings.ExCannotSetSearchCriteria, ex, session, this, "{0}. MapiException = {1}.", new object[]
				{
					string.Format("CoreFolder::SetSearchCriteria.", new object[0]),
					ex
				});
			}
			catch (MapiRetryableException ex2)
			{
				throw StorageGlobals.TranslateMapiException(ServerStrings.ExCannotSetSearchCriteria, ex2, session, this, "{0}. MapiException = {1}.", new object[]
				{
					string.Format("CoreFolder::SetSearchCriteria.", new object[0]),
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

		// Token: 0x06000452 RID: 1106 RVA: 0x00026958 File Offset: 0x00024B58
		public FolderSaveResult Save(SaveMode saveMode)
		{
			this.CheckDisposed(null);
			EnumValidator.ThrowIfInvalid<SaveMode>(saveMode, "saveMode");
			TeamMailboxClientOperations.ThrowIfInvalidFolderOperation(base.Session, this);
			if (saveMode == SaveMode.ResolveConflicts)
			{
				throw new NotSupportedException("Folder does not support resolving conflicts.");
			}
			this.CoreObjectUpdate();
			base.ValidateCoreObject();
			this.OnBeforeFolderSave();
			bool needVersionCheck = saveMode == SaveMode.FailOnAnyConflict;
			FolderSaveResult result = this.FolderPropertyBag.SaveFolderPropertyBag(needVersionCheck);
			this.OnAfterFolderSave();
			base.Origin = Origin.Existing;
			((ICoreObject)this).ResetId();
			return result;
		}

		// Token: 0x06000453 RID: 1107 RVA: 0x000269CC File Offset: 0x00024BCC
		public void SetItemStatus(StoreObjectId itemId, MessageStatusFlags status, MessageStatusFlags statusMask, out MessageStatusFlags oldStatus)
		{
			this.CheckDisposed(null);
			Util.ThrowOnNullArgument(itemId, "itemId");
			EnumValidator.ThrowIfInvalid<MessageStatusFlags>(status, "status");
			EnumValidator.ThrowIfInvalid<MessageStatusFlags>(statusMask, "statusMask");
			TeamMailboxClientOperations.ThrowIfInvalidFolderOperation(base.Session, this);
			MessageStatusFlags bitsSet = statusMask & status;
			MessageStatusFlags bitsClear = statusMask & ~status;
			MapiFolder mapiFolder = this.GetMapiFolder();
			MessageStatus messageStatus;
			CoreFolder.InternalSetItemStatus(mapiFolder, base.Session, this, itemId, (MessageStatus)bitsSet, (MessageStatus)bitsClear, out messageStatus);
			oldStatus = (MessageStatusFlags)messageStatus;
		}

		// Token: 0x06000454 RID: 1108 RVA: 0x00026A34 File Offset: 0x00024C34
		public void SetItemFlags(StoreObjectId itemId, MessageFlags flags, MessageFlags flagsMask)
		{
			this.CheckDisposed(null);
			Util.ThrowOnNullArgument(itemId, "itemId");
			EnumValidator.ThrowIfInvalid<MessageFlags>(flags, "flags");
			EnumValidator.ThrowIfInvalid<MessageFlags>(flagsMask, "flagsMask");
			TeamMailboxClientOperations.ThrowIfInvalidFolderOperation(base.Session, this);
			MessageFlags bitsSet = flagsMask & flags;
			MessageFlags bitsClear = flagsMask & ~flags;
			MapiFolder mapiFolder = this.GetMapiFolder();
			CoreFolder.InternalSetItemFlags(mapiFolder, base.Session, this, itemId, (MessageFlags)bitsSet, (MessageFlags)bitsClear);
		}

		// Token: 0x06000455 RID: 1109 RVA: 0x00026A98 File Offset: 0x00024C98
		public void SetReadFlags(int flags, StoreId[] itemIds, out bool partialCompletion)
		{
			this.CheckDisposed(null);
			TeamMailboxClientOperations.ThrowIfInvalidFolderOperation(base.Session, this);
			try
			{
				this.InternalSetReadFlags((SetReadFlags)flags, itemIds);
				partialCompletion = false;
			}
			catch (PartialCompletionException)
			{
				partialCompletion = true;
			}
		}

		// Token: 0x06000456 RID: 1110 RVA: 0x00026ADC File Offset: 0x00024CDC
		public IMapiFxProxy GetFxProxyCollector()
		{
			return this.GetMapiFolder().GetFxProxyCollector();
		}

		// Token: 0x06000457 RID: 1111 RVA: 0x00026AE9 File Offset: 0x00024CE9
		public HierarchyManifestProvider GetHierarchyManifest(ManifestConfigFlags flags, StorageIcsState state, PropertyDefinition[] includePropertyDefinitions, PropertyDefinition[] excludePropertyDefinitions)
		{
			this.CheckDisposed(null);
			return new HierarchyManifestProvider(this, flags, state, includePropertyDefinitions, excludePropertyDefinitions);
		}

		// Token: 0x06000458 RID: 1112 RVA: 0x00026AFD File Offset: 0x00024CFD
		public HierarchySynchronizationUploadContext GetHierarchySynchronizationUploadContext(StorageIcsState state)
		{
			this.CheckDisposed(null);
			return new HierarchySynchronizationUploadContext(this, state);
		}

		// Token: 0x06000459 RID: 1113 RVA: 0x00026B0D File Offset: 0x00024D0D
		public ContentManifestProvider GetContentManifest(ManifestConfigFlags flags, QueryFilter filter, StorageIcsState state, PropertyDefinition[] includePropertyDefinitions)
		{
			this.CheckDisposed(null);
			return new ContentManifestProvider(this, flags, filter, state, includePropertyDefinitions);
		}

		// Token: 0x0600045A RID: 1114 RVA: 0x00026B21 File Offset: 0x00024D21
		public ContentsSynchronizerProvider GetContentsSynchronizer(SynchronizerConfigFlags flags, QueryFilter filter, StorageIcsState state, PropertyDefinition[] includePropertyDefinitions, PropertyDefinition[] excludePropertyDefinitions, int bufferSize)
		{
			this.CheckDisposed(null);
			return new ContentsSynchronizerProvider(this, flags, filter, state, includePropertyDefinitions, excludePropertyDefinitions, bufferSize);
		}

		// Token: 0x0600045B RID: 1115 RVA: 0x00026B39 File Offset: 0x00024D39
		public HierarchySynchronizerProvider GetHierarchySynchronizer(SynchronizerConfigFlags flags, QueryFilter filter, StorageIcsState state, PropertyDefinition[] includePropertyDefinitions, PropertyDefinition[] excludePropertyDefinitions, int bufferSize)
		{
			this.CheckDisposed(null);
			return new HierarchySynchronizerProvider(this, flags, filter, state, includePropertyDefinitions, excludePropertyDefinitions, bufferSize);
		}

		// Token: 0x0600045C RID: 1116 RVA: 0x00026B51 File Offset: 0x00024D51
		public ContentsSynchronizationUploadContext GetContentsSynchronizationUploadContext(StorageIcsState state)
		{
			this.CheckDisposed(null);
			return new ContentsSynchronizationUploadContext(this, state);
		}

		// Token: 0x0600045D RID: 1117 RVA: 0x00026B61 File Offset: 0x00024D61
		public GroupOperationResult CopyItems(CoreFolder destinationFolder, StoreObjectId[] sourceItemIds, PropertyDefinition[] propertyDefinitions, object[] propertyValues, CoreFolder.ItemIdValidatorDelegate itemIdValidator)
		{
			return this.CopyItems(destinationFolder, sourceItemIds, propertyDefinitions, propertyValues, itemIdValidator, false);
		}

		// Token: 0x0600045E RID: 1118 RVA: 0x00026B71 File Offset: 0x00024D71
		public GroupOperationResult CopyItems(CoreFolder destinationFolder, StoreObjectId[] sourceItemIds, PropertyDefinition[] propertyDefinitions, object[] propertyValues, CoreFolder.ItemIdValidatorDelegate itemIdValidator, bool returnNewIds)
		{
			return this.CopyItems(destinationFolder, sourceItemIds, propertyDefinitions, propertyValues, itemIdValidator, returnNewIds, true);
		}

		// Token: 0x0600045F RID: 1119 RVA: 0x00026B84 File Offset: 0x00024D84
		public GroupOperationResult CopyItems(CoreFolder destinationFolder, StoreObjectId[] sourceItemIds, PropertyDefinition[] propertyDefinitions, object[] propertyValues, CoreFolder.ItemIdValidatorDelegate itemIdValidator, bool returnNewIds, bool updateSource)
		{
			this.CheckDisposed(null);
			this.ValidateItemIds(sourceItemIds, itemIdValidator);
			return this.InternalMoveOrCopyItems(destinationFolder, sourceItemIds, propertyDefinitions, propertyValues, false, returnNewIds, null, updateSource);
		}

		// Token: 0x06000460 RID: 1120 RVA: 0x00026BBA File Offset: 0x00024DBA
		public GroupOperationResult MoveItems(CoreFolder destinationFolder, StoreObjectId[] sourceItemIds, PropertyDefinition[] propertyDefinitions, object[] propertyValues, CoreFolder.ItemIdValidatorDelegate itemIdValidator)
		{
			return this.MoveItems(destinationFolder, sourceItemIds, propertyDefinitions, propertyValues, itemIdValidator, false);
		}

		// Token: 0x06000461 RID: 1121 RVA: 0x00026BCC File Offset: 0x00024DCC
		public GroupOperationResult MoveItems(CoreFolder destinationFolder, StoreObjectId[] sourceItemIds, PropertyDefinition[] propertyDefinitions, object[] propertyValues, CoreFolder.ItemIdValidatorDelegate itemIdValidator, bool returnNewIds)
		{
			return this.MoveItems(destinationFolder, sourceItemIds, propertyDefinitions, propertyValues, itemIdValidator, returnNewIds, null);
		}

		// Token: 0x06000462 RID: 1122 RVA: 0x00026BF4 File Offset: 0x00024DF4
		public GroupOperationResult MoveItems(CoreFolder destinationFolder, StoreObjectId[] sourceItemIds, PropertyDefinition[] propertyDefinitions, object[] propertyValues, CoreFolder.ItemIdValidatorDelegate itemIdValidator, bool returnNewIds, DeleteItemFlags? deleteFlags)
		{
			this.CheckDisposed(null);
			this.ValidateItemIds(sourceItemIds, itemIdValidator);
			if (destinationFolder.StoreObjectId.Equals(this.StoreObjectId) && propertyValues == null)
			{
				return new GroupOperationResult(OperationResult.Succeeded, sourceItemIds, null);
			}
			return this.InternalMoveOrCopyItems(destinationFolder, sourceItemIds, propertyDefinitions, propertyValues, true, returnNewIds, deleteFlags, true);
		}

		// Token: 0x06000463 RID: 1123 RVA: 0x00026C68 File Offset: 0x00024E68
		public GroupOperationResult DeleteItems(DeleteItemFlags deleteItemFlags, StoreObjectId[] ids)
		{
			this.CheckDisposed(null);
			this.ResolveStoreObjectType(ids);
			EnumValidator.ThrowIfInvalid<DeleteItemFlags>(deleteItemFlags, "deleteItemFlags");
			Util.ThrowOnNullArgument(ids, "ids");
			TeamMailboxClientOperations.ThrowIfInvalidFolderOperation(base.Session, this);
			byte[][] entryIDs = CoreFolder.StoreObjectIdsToEntryIds(base.Session, ids);
			DeleteMessagesFlags mapiDeleteFlags = CoreFolder.MapiDeleteFlagsFromXsoDeleteFlags(deleteItemFlags);
			FolderChangeOperation operation = ((deleteItemFlags & DeleteItemFlags.HardDelete) == DeleteItemFlags.HardDelete) ? FolderChangeOperation.HardDelete : FolderChangeOperation.SoftDelete;
			FolderChangeOperationFlags folderChangeOperationFlags = CoreFolder.GetFolderChangeOperationFlags(FolderChangeOperationFlags.IncludeAll, deleteItemFlags);
			GroupOperationResult result = null;
			using (CallbackContext callbackContext = new CallbackContext(base.Session))
			{
				try
				{
					bool flag = base.Session.OnBeforeFolderChange(operation, folderChangeOperationFlags, base.Session, null, this.StoreObjectId, null, ids, callbackContext);
					if (flag)
					{
						result = base.Session.GetCallbackResults();
					}
					else
					{
						result = this.ExecuteMapiGroupOperation("DeleteItems", ids, delegate()
						{
							this.GetMapiFolder().DeleteMessages(mapiDeleteFlags, entryIDs);
						});
					}
				}
				finally
				{
					base.Session.OnAfterFolderChange(operation, folderChangeOperationFlags, base.Session, null, this.StoreObjectId, null, ids, result, callbackContext);
				}
			}
			return result;
		}

		// Token: 0x06000464 RID: 1124 RVA: 0x00026DC4 File Offset: 0x00024FC4
		public GroupOperationResult DeleteFolder(DeleteFolderFlags storageDeleteFlags, StoreObjectId folderId)
		{
			this.CheckDisposed(null);
			EnumValidator.ThrowIfInvalid<DeleteFolderFlags>(storageDeleteFlags, "storageDeleteFlags");
			Util.ThrowOnNullArgument(folderId, "folderId");
			TeamMailboxClientOperations.ThrowIfInvalidFolderOperation(base.Session, new StoreObjectId[]
			{
				folderId
			});
			DeleteFolderFlags mapiDeleteFlags = DeleteFolderFlags.None;
			FolderChangeOperationFlags folderChangeOperationFlags = FolderChangeOperationFlags.None;
			if ((storageDeleteFlags & DeleteFolderFlags.DeleteMessages) != DeleteFolderFlags.None)
			{
				mapiDeleteFlags |= DeleteFolderFlags.DeleteMessages;
				folderChangeOperationFlags |= (FolderChangeOperationFlags.IncludeAssociated | FolderChangeOperationFlags.IncludeItems);
			}
			if ((storageDeleteFlags & DeleteFolderFlags.DeleteSubFolders) != DeleteFolderFlags.None)
			{
				mapiDeleteFlags |= DeleteFolderFlags.DelSubFolders;
				folderChangeOperationFlags |= FolderChangeOperationFlags.IncludeSubFolders;
			}
			if ((storageDeleteFlags & DeleteFolderFlags.HardDelete) != DeleteFolderFlags.None)
			{
				mapiDeleteFlags |= DeleteFolderFlags.ForceHardDelete;
			}
			FolderChangeOperation folderChangeOperation = ((storageDeleteFlags & DeleteFolderFlags.HardDelete) != DeleteFolderFlags.None) ? FolderChangeOperation.HardDelete : FolderChangeOperation.SoftDelete;
			base.Session.ValidateOperation(folderChangeOperation, folderId);
			GroupOperationResult result = null;
			using (CallbackContext callbackContext = new CallbackContext(base.Session))
			{
				try
				{
					bool flag = base.Session.OnBeforeFolderChange(folderChangeOperation, folderChangeOperationFlags, base.Session, null, this.StoreObjectId, null, new StoreObjectId[]
					{
						folderId
					}, callbackContext);
					if (flag)
					{
						result = base.Session.GetCallbackResults();
					}
					else
					{
						result = this.ExecuteMapiGroupOperation("DeleteFolder", new StoreObjectId[]
						{
							folderId
						}, delegate()
						{
							this.GetMapiFolder().DeleteFolder(Folder.GetFolderProviderLevelItemId(folderId), mapiDeleteFlags);
						});
					}
				}
				finally
				{
					base.Session.OnAfterFolderChange(folderChangeOperation, folderChangeOperationFlags, base.Session, null, this.StoreObjectId, null, new StoreObjectId[]
					{
						folderId
					}, result, callbackContext);
				}
			}
			return result;
		}

		// Token: 0x06000465 RID: 1125 RVA: 0x00026F84 File Offset: 0x00025184
		public GroupOperationResult DeleteFolder(DeleteItemFlags deleteItemFlags, StoreObjectId folderId)
		{
			this.CheckDisposed(null);
			EnumValidator.ThrowIfInvalid<DeleteItemFlags>(deleteItemFlags, "deleteItemFlags");
			Util.ThrowOnNullArgument(folderId, "folderId");
			TeamMailboxClientOperations.ThrowIfInvalidFolderOperation(base.Session, new StoreObjectId[]
			{
				folderId
			});
			DeleteFolderFlags deleteFolderFlags = DeleteFolderFlags.DeleteMessages | DeleteFolderFlags.DeleteSubFolders;
			if ((deleteItemFlags & DeleteItemFlags.HardDelete) == DeleteItemFlags.HardDelete)
			{
				deleteFolderFlags |= DeleteFolderFlags.HardDelete;
			}
			return this.DeleteFolder(deleteFolderFlags, folderId);
		}

		// Token: 0x06000466 RID: 1126 RVA: 0x00026FFC File Offset: 0x000251FC
		public GroupOperationResult EmptyFolder(bool reportProgress, EmptyFolderFlags storageEmptyFlags)
		{
			this.CheckDisposed(null);
			EnumValidator.ThrowIfInvalid<EmptyFolderFlags>(storageEmptyFlags, "storageEmptyFlags");
			TeamMailboxClientOperations.ThrowIfInvalidFolderOperation(base.Session, this);
			base.Session.ValidateOperation(FolderChangeOperation.Empty, this.StoreObjectId);
			bool flag = (storageEmptyFlags & EmptyFolderFlags.DeleteAssociatedMessages) != EmptyFolderFlags.None;
			bool flag2 = (storageEmptyFlags & EmptyFolderFlags.HardDelete) != EmptyFolderFlags.None;
			FolderChangeOperation operation = flag2 ? FolderChangeOperation.HardDelete : FolderChangeOperation.SoftDelete;
			FolderChangeOperationFlags folderChangeOperationFlags = FolderChangeOperationFlags.IncludeItems | FolderChangeOperationFlags.IncludeSubFolders | FolderChangeOperationFlags.EmptyFolder;
			EmptyFolderFlags mapiEmptyFolderFlags = EmptyFolderFlags.None;
			if (flag)
			{
				mapiEmptyFolderFlags |= EmptyFolderFlags.DeleteAssociatedMessages;
				folderChangeOperationFlags |= FolderChangeOperationFlags.IncludeAssociated;
			}
			if ((storageEmptyFlags & EmptyFolderFlags.Force) != EmptyFolderFlags.None)
			{
				mapiEmptyFolderFlags |= EmptyFolderFlags.Force;
			}
			if (flag2)
			{
				mapiEmptyFolderFlags |= EmptyFolderFlags.ForceHardDelete;
			}
			GroupOperationResult result = null;
			using (CallbackContext callbackContext = new CallbackContext(base.Session))
			{
				try
				{
					bool flag3 = base.Session.OnBeforeFolderChange(operation, folderChangeOperationFlags, base.Session, null, this.StoreObjectId, null, new StoreObjectId[]
					{
						this.StoreObjectId
					}, callbackContext);
					if (flag3)
					{
						result = base.Session.GetCallbackResults();
					}
					else
					{
						result = this.ExecuteMapiGroupOperation("EmptyFolder", new StoreObjectId[]
						{
							this.StoreObjectId
						}, delegate()
						{
							this.GetMapiFolder().EmptyFolder(mapiEmptyFolderFlags);
						});
					}
				}
				finally
				{
					base.Session.OnAfterFolderChange(operation, folderChangeOperationFlags, base.Session, null, this.StoreObjectId, null, new StoreObjectId[]
					{
						this.StoreObjectId
					}, result, callbackContext);
				}
			}
			return result;
		}

		// Token: 0x06000467 RID: 1127 RVA: 0x000271A4 File Offset: 0x000253A4
		public PropertyError[] CopyFolder(CoreFolder destinationFolder, CopyPropertiesFlags copyPropertiesFlags, CopySubObjects copySubObjects, NativeStorePropertyDefinition[] excludeProperties)
		{
			this.CheckDisposed(null);
			Util.ThrowOnNullArgument(destinationFolder, "destinationFolder");
			Util.ThrowOnNullArgument(excludeProperties, "excludeProperties");
			EnumValidator.ThrowIfInvalid<CopyPropertiesFlags>(copyPropertiesFlags, "copyPropertiesFlags");
			EnumValidator.ThrowIfInvalid<CopySubObjects>(copySubObjects, "copySubObjects");
			TeamMailboxClientOperations.ThrowIfInvalidFolderOperation(destinationFolder.Session, destinationFolder);
			base.Session.ValidateOperation(FolderChangeOperation.Copy, this.StoreObjectId);
			PropertyError[] array = null;
			LocalizedException ex = null;
			StoreObjectId[] array2 = new StoreObjectId[]
			{
				this.StoreObjectId
			};
			byte[] entryId = base.PropertyBag[CoreObjectSchema.ParentEntryId] as byte[];
			StoreObjectId sourceFolderId = StoreObjectId.FromProviderSpecificId(entryId, StoreObjectType.Folder);
			FolderChangeOperationFlags flags = FolderChangeOperationFlags.IncludeAll;
			if (destinationFolder.IsPermissionChangeBlocked())
			{
				int num = excludeProperties.Length;
				int num2 = num + CoreFolder.permissionChangeRelatedProperties.Length;
				Array.Resize<NativeStorePropertyDefinition>(ref excludeProperties, num2);
				for (int i = num; i < num2; i++)
				{
					excludeProperties[i] = CoreFolder.permissionChangeRelatedProperties[i - num];
				}
			}
			PropertyError[] result;
			using (CallbackContext callbackContext = new CallbackContext(base.Session))
			{
				try
				{
					base.Session.OnBeforeFolderChange(FolderChangeOperation.Copy, flags, base.Session, base.Session, sourceFolderId, destinationFolder.StoreObjectId, array2, callbackContext);
					array = CoreObject.MapiCopyTo(this.FolderPropertyBag.MapiProp, destinationFolder.FolderPropertyBag.MapiProp, base.Session, destinationFolder.Session, copyPropertiesFlags, copySubObjects, excludeProperties);
					result = array;
				}
				catch (StoragePermanentException ex2)
				{
					ex = ex2;
					throw;
				}
				catch (StorageTransientException ex3)
				{
					ex = ex3;
					throw;
				}
				finally
				{
					OperationResult operationResult = OperationResult.Succeeded;
					if (ex != null)
					{
						operationResult = OperationResult.Failed;
					}
					else if (array != null && array.Length != 0)
					{
						operationResult = OperationResult.PartiallySucceeded;
					}
					GroupOperationResult result2 = new GroupOperationResult(operationResult, array2, ex);
					base.Session.OnAfterFolderChange(FolderChangeOperation.Copy, flags, base.Session, base.Session, sourceFolderId, destinationFolder.StoreObjectId, array2, result2, callbackContext);
				}
			}
			return result;
		}

		// Token: 0x06000468 RID: 1128 RVA: 0x00027384 File Offset: 0x00025584
		public GroupOperationResult MoveFolder(CoreFolder destinationFolder, StoreObjectId sourceFolderId)
		{
			return this.MoveFolder(destinationFolder, sourceFolderId, null);
		}

		// Token: 0x06000469 RID: 1129 RVA: 0x000273C8 File Offset: 0x000255C8
		public GroupOperationResult MoveFolder(CoreFolder destinationFolder, StoreObjectId sourceFolderId, string newFolderName)
		{
			this.CheckDisposed(null);
			if (!base.Session.IsMoveUser)
			{
				this.CheckIsNotDefaultFolder(sourceFolderId, new DefaultFolderType[0]);
			}
			TeamMailboxClientOperations.ThrowIfInvalidFolderOperation(destinationFolder.Session, destinationFolder);
			base.Session.ValidateOperation(FolderChangeOperation.Move, sourceFolderId);
			FolderChangeOperation operation = FolderChangeOperation.Move;
			StoreObjectId storeObjectId = destinationFolder.StoreObjectId;
			StoreObjectId[] itemIds = new StoreObjectId[]
			{
				sourceFolderId
			};
			GroupOperationResult result = null;
			FolderChangeOperationFlags flags = FolderChangeOperationFlags.IncludeAll;
			using (CallbackContext callbackContext = new CallbackContext(base.Session))
			{
				try
				{
					if (base.Session != null)
					{
						MailboxSession mailboxSession = base.Session as MailboxSession;
						if (mailboxSession != null && mailboxSession.IsDefaultFolderType(storeObjectId) == DefaultFolderType.DeletedItems)
						{
							operation = FolderChangeOperation.MoveToDeletedItems;
						}
						base.Session.OnBeforeFolderChange(operation, flags, base.Session, destinationFolder.Session, this.StoreObjectId, storeObjectId, itemIds, callbackContext);
					}
					result = this.ExecuteMapiGroupOperation("MoveFolder", new StoreObjectId[]
					{
						sourceFolderId
					}, delegate()
					{
						this.GetMapiFolder().CopyFolder(CopyFolderFlags.FolderMove, destinationFolder.GetMapiFolder(), Folder.GetFolderProviderLevelItemId(sourceFolderId), newFolderName);
					});
				}
				finally
				{
					if (base.Session != null)
					{
						base.Session.OnAfterFolderChange(operation, flags, base.Session, destinationFolder.Session, this.StoreObjectId, destinationFolder.Id.ObjectId, itemIds, result, callbackContext);
					}
				}
			}
			return result;
		}

		// Token: 0x0600046A RID: 1130 RVA: 0x00027580 File Offset: 0x00025780
		public GroupOperationResult CopyFolder(CoreFolder destinationFolder, CopySubObjects copySubObjects, StoreObjectId sourceFolderId)
		{
			return this.CopyFolder(destinationFolder, copySubObjects, sourceFolderId, null);
		}

		// Token: 0x0600046B RID: 1131 RVA: 0x000275C8 File Offset: 0x000257C8
		public GroupOperationResult CopyFolder(CoreFolder destinationFolder, CopySubObjects copySubObjects, StoreObjectId sourceFolderId, string newFolderName)
		{
			this.CheckDisposed(null);
			EnumValidator.ThrowIfInvalid<CopySubObjects>(copySubObjects);
			base.Session.ValidateOperation(FolderChangeOperation.Copy, sourceFolderId);
			TeamMailboxClientOperations.ThrowIfInvalidFolderOperation(destinationFolder.Session, destinationFolder);
			GroupOperationResult result = null;
			CopyFolderFlags mapiCopyFolderFlags;
			switch (copySubObjects)
			{
			case CopySubObjects.Copy:
				mapiCopyFolderFlags = CopyFolderFlags.CopySubfolders;
				break;
			case CopySubObjects.DoNotCopy:
				mapiCopyFolderFlags = CopyFolderFlags.None;
				break;
			default:
				throw new ArgumentOutOfRangeException("copySubObjects");
			}
			FolderChangeOperationFlags flags = FolderChangeOperationFlags.IncludeAll;
			using (CallbackContext callbackContext = new CallbackContext(base.Session))
			{
				try
				{
					if (base.Session != null)
					{
						base.Session.OnBeforeFolderChange(FolderChangeOperation.Copy, flags, base.Session, base.Session, this.StoreObjectId, destinationFolder.StoreObjectId, new StoreObjectId[]
						{
							sourceFolderId
						}, callbackContext);
					}
					result = this.ExecuteMapiGroupOperation("CopyFolder", new StoreObjectId[]
					{
						sourceFolderId
					}, delegate()
					{
						this.GetMapiFolder().CopyFolder(mapiCopyFolderFlags, destinationFolder.GetMapiFolder(), Folder.GetFolderProviderLevelItemId(sourceFolderId), newFolderName);
					});
				}
				finally
				{
					if (base.Session != null)
					{
						base.Session.OnAfterFolderChange(FolderChangeOperation.Copy, flags, base.Session, base.Session, this.StoreObjectId, destinationFolder.StoreObjectId, new StoreObjectId[]
						{
							sourceFolderId
						}, result, callbackContext);
					}
				}
			}
			return result;
		}

		// Token: 0x0600046C RID: 1132 RVA: 0x00027770 File Offset: 0x00025970
		internal void CheckIsNotDefaultFolder(StoreObjectId sourceFolderId, params DefaultFolderType[] allowedDefaultFolders)
		{
			if (!CoreFolder.CheckIsNotDefaultFolder(base.Session, sourceFolderId, allowedDefaultFolders))
			{
				ExTraceGlobals.StorageTracer.TraceDebug<StoreObjectId>((long)this.GetHashCode(), "Folder::CheckIsNotDefaultFolder. Failed to move/delete a folder. It was a default folder. FolderId = {0}", sourceFolderId);
				throw new CannotMoveDefaultFolderException(ServerStrings.ExCannotMoveOrDeleteDefaultFolders);
			}
		}

		// Token: 0x0600046D RID: 1133 RVA: 0x000277A4 File Offset: 0x000259A4
		internal static bool CheckIsNotDefaultFolder(StoreSession session, StoreObjectId sourceFolderId, params DefaultFolderType[] allowedDefaultFolders)
		{
			MailboxSession mailboxSession = session as MailboxSession;
			if (mailboxSession != null)
			{
				DefaultFolderType defaultFolderType = mailboxSession.IsDefaultFolderType(sourceFolderId);
				if (defaultFolderType != DefaultFolderType.None && Array.IndexOf<DefaultFolderType>(allowedDefaultFolders, defaultFolderType) == -1)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600046E RID: 1134 RVA: 0x000277E8 File Offset: 0x000259E8
		public PropertyError[] CopyProperties(CoreFolder destinationFolder, CopyPropertiesFlags copyPropertiesFlags, NativeStorePropertyDefinition[] includeProperties)
		{
			this.CheckDisposed(null);
			Util.ThrowOnNullArgument(destinationFolder, "destinationFolder");
			Util.ThrowOnNullArgument(includeProperties, "includeProperties");
			EnumValidator.ThrowIfInvalid<CopyPropertiesFlags>(copyPropertiesFlags, "copyPropertiesFlags");
			TeamMailboxClientOperations.ThrowIfInvalidFolderOperation(destinationFolder.Session, destinationFolder);
			destinationFolder.Session.ValidateOperation(FolderChangeOperation.Update, destinationFolder.StoreObjectId);
			if (Array.Exists<NativeStorePropertyDefinition>(includeProperties, (NativeStorePropertyDefinition x) => Array.IndexOf<NativeStorePropertyDefinition>(CoreFolder.permissionChangeRelatedProperties, x) != -1) && destinationFolder.IsPermissionChangeBlocked())
			{
				HashSet<NativeStorePropertyDefinition> hashSet = new HashSet<NativeStorePropertyDefinition>(includeProperties);
				hashSet.ExceptWith(CoreFolder.permissionChangeRelatedProperties);
				includeProperties = new NativeStorePropertyDefinition[hashSet.Count];
				hashSet.CopyTo(includeProperties);
			}
			return CoreObject.MapiCopyProps(this.FolderPropertyBag.MapiProp, destinationFolder.FolderPropertyBag.MapiProp, base.Session, destinationFolder.Session, copyPropertiesFlags, includeProperties);
		}

		// Token: 0x0600046F RID: 1135 RVA: 0x000278B7 File Offset: 0x00025AB7
		public bool IsContentAvailable()
		{
			this.CheckDisposed(null);
			return CoreFolder.IsContentAvailable(base.Session, this.GetContentMailboxInfo());
		}

		// Token: 0x06000470 RID: 1136 RVA: 0x000278D1 File Offset: 0x00025AD1
		public PublicFolderContentMailboxInfo GetContentMailboxInfo()
		{
			this.CheckDisposed(null);
			return CoreFolder.GetContentMailboxInfo(base.PropertyBag.GetValueOrDefault<string[]>(CoreFolderSchema.ReplicaList, Array<string>.Empty));
		}

		// Token: 0x06000471 RID: 1137 RVA: 0x000278F4 File Offset: 0x00025AF4
		public static bool IsContentAvailable(StoreSession session, PublicFolderContentMailboxInfo contentMailboxInfo)
		{
			Util.ThrowOnNullArgument(session, "session");
			Util.ThrowOnNullArgument(contentMailboxInfo, "contentMailboxInfo");
			return session is MailboxSession || !contentMailboxInfo.IsValid || contentMailboxInfo.MailboxGuid == session.MailboxGuid;
		}

		// Token: 0x06000472 RID: 1138 RVA: 0x0002792F File Offset: 0x00025B2F
		public static PublicFolderContentMailboxInfo GetContentMailboxInfo(string[] replicaList)
		{
			Util.ThrowOnNullArgument(replicaList, "replicaList");
			return new PublicFolderContentMailboxInfo((replicaList.Length > 0) ? replicaList[0] : null);
		}

		// Token: 0x06000473 RID: 1139 RVA: 0x0002794D File Offset: 0x00025B4D
		internal void ClearNotReadNotificationPending()
		{
			this.ClearNotReadNotificationPending(null);
		}

		// Token: 0x06000474 RID: 1140 RVA: 0x00027956 File Offset: 0x00025B56
		internal void ClearNotReadNotificationPending(StoreId[] itemIds)
		{
			this.InternalSetReadFlags(Microsoft.Mapi.SetReadFlags.DeferredErrors | Microsoft.Mapi.SetReadFlags.CleanNrnPending, itemIds);
		}

		// Token: 0x06000475 RID: 1141 RVA: 0x00027961 File Offset: 0x00025B61
		internal void OnBeforeFolderSave()
		{
			if (!base.Session.IsMoveUser && base.Origin == Origin.Existing)
			{
				base.Session.ValidateOperation(FolderChangeOperation.Update, this.StoreObjectId);
			}
		}

		// Token: 0x06000476 RID: 1142 RVA: 0x0002798C File Offset: 0x00025B8C
		internal void OnAfterFolderSave()
		{
			PublicFolderSession publicFolderSession = base.Session as PublicFolderSession;
			if (publicFolderSession == null || !publicFolderSession.IsPrimaryHierarchySession || publicFolderSession.IsMoveUser)
			{
				return;
			}
			StoreObjectId storeObjectId = null;
			try
			{
				base.PropertyBag.Load(null);
				storeObjectId = this.StoreObjectId;
				if (storeObjectId.ObjectType != StoreObjectType.SearchFolder)
				{
					this.InternalSyncHierarchyToContentMailbox(publicFolderSession, storeObjectId);
				}
			}
			catch (StorageTransientException arg)
			{
				ExTraceGlobals.StorageTracer.TraceDebug<string, StorageTransientException>((long)this.GetHashCode(), "CoreFolder::OnAfterChangeForPublicFolder. Ignoring Error. Failed to sync properties on content folder. FolderId = {0}. Exception = {1}", (storeObjectId == null) ? "Unknown" : storeObjectId.ToHexEntryId(), arg);
			}
			catch (StoragePermanentException arg2)
			{
				ExTraceGlobals.StorageTracer.TraceDebug<string, StoragePermanentException>((long)this.GetHashCode(), "CoreFolder::OnAfterChangeForPublicFolder. Ignoring Error. Failed to sync properties on content folder. FolderId = {0}. Exception = {1}", (storeObjectId == null) ? "Unknown" : storeObjectId.ToHexEntryId(), arg2);
			}
		}

		// Token: 0x06000477 RID: 1143 RVA: 0x00027A54 File Offset: 0x00025C54
		internal static void InternalSetItemStatus(MapiFolder mapiFolder, StoreSession session, object currentObject, StoreObjectId itemId, MessageStatus bitsSet, MessageStatus bitsClear)
		{
			MessageStatus messageStatus;
			CoreFolder.InternalSetItemStatus(mapiFolder, session, currentObject, itemId, bitsSet, bitsClear, out messageStatus);
		}

		// Token: 0x06000478 RID: 1144 RVA: 0x00027A70 File Offset: 0x00025C70
		internal static void InternalSetItemStatus(MapiFolder mapiFolder, StoreSession session, object currentObject, StoreObjectId itemId, MessageStatus bitsSet, MessageStatus bitsClear, out MessageStatus oldStatus)
		{
			MessageStatus messageStatus = MessageStatus.None;
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
				mapiFolder.SetMessageStatus(itemId.ProviderLevelItemId, bitsSet, bitsClear, out messageStatus);
			}
			catch (MapiPermanentException ex)
			{
				throw StorageGlobals.TranslateMapiException(ServerStrings.CannotSetMessageFlagStatus, ex, session, currentObject, "{0}. MapiException = {1}.", new object[]
				{
					string.Format("CoreFolder::InternalSetItemStatus.", new object[0]),
					ex
				});
			}
			catch (MapiRetryableException ex2)
			{
				throw StorageGlobals.TranslateMapiException(ServerStrings.CannotSetMessageFlagStatus, ex2, session, currentObject, "{0}. MapiException = {1}.", new object[]
				{
					string.Format("CoreFolder::InternalSetItemStatus.", new object[0]),
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
			oldStatus = messageStatus;
		}

		// Token: 0x06000479 RID: 1145 RVA: 0x00027B9C File Offset: 0x00025D9C
		internal static void InternalSetItemFlags(MapiFolder mapiFolder, StoreSession session, object currentObject, StoreObjectId itemId, MessageFlags bitsSet, MessageFlags bitsClear)
		{
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
				mapiFolder.SetMessageFlags(itemId.ProviderLevelItemId, bitsSet, bitsClear);
			}
			catch (MapiPermanentException ex)
			{
				throw StorageGlobals.TranslateMapiException(ServerStrings.CannotSetMessageFlags, ex, session, currentObject, "{0}. MapiException = {1}.", new object[]
				{
					string.Format("CoreFolder::InternalSetItemFlags.", new object[0]),
					ex
				});
			}
			catch (MapiRetryableException ex2)
			{
				throw StorageGlobals.TranslateMapiException(ServerStrings.CannotSetMessageFlags, ex2, session, currentObject, "{0}. MapiException = {1}.", new object[]
				{
					string.Format("CoreFolder::InternalSetItemFlags.", new object[0]),
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

		// Token: 0x0600047A RID: 1146 RVA: 0x00027CBC File Offset: 0x00025EBC
		internal void InternalSetReadFlags(SetReadFlags flags, params StoreId[] itemIds)
		{
			this.InternalSetReadFlags(flags, itemIds, false);
		}

		// Token: 0x0600047B RID: 1147 RVA: 0x00027CC8 File Offset: 0x00025EC8
		internal void InternalSetReadFlags(SetReadFlags flags, StoreId[] itemIds, bool throwIfWarning)
		{
			MapiFolder mapiFolder = this.GetMapiFolder();
			StoreSession session = base.Session;
			object thisObject = null;
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
				bool allowWarnings = mapiFolder.AllowWarnings;
				try
				{
					mapiFolder.AllowWarnings = true;
					if (itemIds != null && itemIds.Length > 0)
					{
						mapiFolder.SetReadFlags(flags, StoreId.StoreIdsToEntryIds(itemIds), throwIfWarning);
					}
					else
					{
						mapiFolder.SetReadFlagsOnAllMessages(flags, throwIfWarning);
					}
				}
				finally
				{
					mapiFolder.AllowWarnings = allowWarnings;
				}
			}
			catch (MapiPermanentException ex)
			{
				throw StorageGlobals.TranslateMapiException(ServerStrings.MapiCannotSetReadFlags, ex, session, thisObject, "{0}. MapiException = {1}.", new object[]
				{
					string.Format("Failed to set read flags on messages in a folder", new object[0]),
					ex
				});
			}
			catch (MapiRetryableException ex2)
			{
				throw StorageGlobals.TranslateMapiException(ServerStrings.MapiCannotSetReadFlags, ex2, session, thisObject, "{0}. MapiException = {1}.", new object[]
				{
					string.Format("Failed to set read flags on messages in a folder", new object[0]),
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

		// Token: 0x0600047C RID: 1148 RVA: 0x00027E30 File Offset: 0x00026030
		protected override Schema GetSchema()
		{
			return this.schema;
		}

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x0600047D RID: 1149 RVA: 0x00027E38 File Offset: 0x00026038
		protected override StorePropertyDefinition IdProperty
		{
			get
			{
				return CoreFolderSchema.Id;
			}
		}

		// Token: 0x0600047E RID: 1150 RVA: 0x00027E3F File Offset: 0x0002603F
		internal bool IsMailEnabled()
		{
			this.CheckDisposed(null);
			base.PropertyBag.Load(CoreFolder.MailEnabledFolderProperty);
			return base.PropertyBag.GetValueOrDefault<bool>(InternalSchema.MailEnabled);
		}

		// Token: 0x0600047F RID: 1151 RVA: 0x00027E68 File Offset: 0x00026068
		internal bool IsPermissionChangeBlocked()
		{
			base.PropertyBag.Load(new NativeStorePropertyDefinition[]
			{
				CoreFolderSchema.PermissionChangeBlocked
			});
			return base.PropertyBag.GetValueAsNullable<bool>(CoreFolderSchema.PermissionChangeBlocked) != null;
		}

		// Token: 0x06000480 RID: 1152 RVA: 0x00027EA8 File Offset: 0x000260A8
		internal static CoreFolder InternalBind(StoreSession storeSession, MapiFolder mapiFolder, StoreObjectId folderObjectId, byte[] changeKey, ICollection<PropertyDefinition> propsToReturn)
		{
			Util.ThrowOnNullArgument(storeSession, "storeSession");
			Util.ThrowOnNullArgument(mapiFolder, "mapiFolder");
			Util.ThrowOnNullArgument(folderObjectId, "folderObjectId");
			storeSession.CheckSystemFolderAccess(folderObjectId);
			propsToReturn = InternalSchema.Combine<PropertyDefinition>(FolderSchema.Instance.AutoloadProperties, propsToReturn);
			CoreFolder coreFolder = null;
			bool success = false;
			using (CallbackContext callbackContext = new CallbackContext(storeSession))
			{
				storeSession.OnBeforeFolderBind(folderObjectId, callbackContext);
				using (DisposeGuard disposeGuard = default(DisposeGuard))
				{
					try
					{
						FolderPropertyBag folderPropertyBag = new FolderPropertyBag(storeSession, mapiFolder, propsToReturn);
						disposeGuard.Add<FolderPropertyBag>(folderPropertyBag);
						StoreObjectType storeObjectType = InternalSchema.FolderId.GetStoreObjectType(folderPropertyBag);
						if (folderObjectId.ObjectType != storeObjectType)
						{
							folderObjectId = StoreObjectId.FromProviderSpecificId(folderObjectId.ProviderLevelItemId, storeObjectType);
						}
						FolderCreateInfo folderCreateInfo = FolderCreateInfo.GetFolderCreateInfo(storeObjectType);
						coreFolder = new CoreFolder(storeSession, folderCreateInfo.Schema, folderPropertyBag, folderObjectId, changeKey, Origin.Existing, propsToReturn);
						disposeGuard.Add<CoreFolder>(coreFolder);
						success = true;
					}
					finally
					{
						storeSession.OnAfterFolderBind(folderObjectId, coreFolder, success, callbackContext);
					}
					disposeGuard.Success();
				}
			}
			return coreFolder;
		}

		// Token: 0x06000481 RID: 1153 RVA: 0x00027FC8 File Offset: 0x000261C8
		internal static CoreFolder InternalBind(StoreSession session, StoreId folderId, bool allowSoftDeleted, ICollection<PropertyDefinition> propsToReturn)
		{
			Util.ThrowOnNullArgument(session, "session");
			Util.ThrowOnNullArgument(folderId, "folderId");
			if (!Folder.IsFolderId(folderId))
			{
				throw new ArgumentException(ServerStrings.InvalidFolderId(folderId.ToBase64String()));
			}
			StoreObjectId storeObjectId;
			byte[] changeKey;
			StoreId.SplitStoreObjectIdAndChangeKey(folderId, out storeObjectId, out changeKey);
			CoreFolder result;
			using (DisposeGuard disposeGuard = default(DisposeGuard))
			{
				OpenEntryFlags openEntryFlags = OpenEntryFlags.BestAccess | OpenEntryFlags.DeferredErrors;
				if (allowSoftDeleted)
				{
					openEntryFlags |= OpenEntryFlags.ShowSoftDeletes;
				}
				MapiProp mapiProp = session.GetMapiProp(storeObjectId, openEntryFlags);
				disposeGuard.Add<MapiProp>(mapiProp);
				MapiFolder mapiFolder = (MapiFolder)mapiProp;
				disposeGuard.Add<MapiFolder>(mapiFolder);
				CoreFolder coreFolder = CoreFolder.InternalBind(session, mapiFolder, storeObjectId, changeKey, propsToReturn);
				disposeGuard.Add<CoreFolder>(coreFolder);
				disposeGuard.Success();
				result = coreFolder;
			}
			return result;
		}

		// Token: 0x06000482 RID: 1154 RVA: 0x00028098 File Offset: 0x00026298
		internal static CoreFolder InternalCreate(StoreSession session, StoreId parentId, bool isSearchFolder, string displayName, CreateMode createMode, bool isSecure, FolderCreateInfo folderCreateInfo)
		{
			Util.ThrowOnNullArgument(session, "session");
			Util.ThrowOnNullArgument(parentId, "parentId");
			EnumValidator.ThrowIfInvalid<CreateMode>(createMode, "createMode");
			Util.ThrowOnNullArgument(folderCreateInfo, "folderCreateInfo");
			session.ValidateOperation(FolderChangeOperation.Create, StoreId.GetStoreObjectId(parentId));
			PublicFolderSession publicFolderSession = session as PublicFolderSession;
			if (isSearchFolder && publicFolderSession != null && !publicFolderSession.IsSystemOperation())
			{
				throw new NoSupportException(ServerStrings.CannotCreateSearchFoldersInPublicStore);
			}
			if (session.BlockFolderCreation && !createMode.HasFlag(CreateMode.OverrideFolderCreationBlock))
			{
				throw new FolderCreationBlockedException();
			}
			CoreFolder result;
			using (DisposeGuard disposeGuard = default(DisposeGuard))
			{
				FolderPropertyBag folderPropertyBag = new FolderCreatePropertyBag(session, StoreId.GetStoreObjectId(parentId), isSearchFolder, createMode, isSecure, folderCreateInfo.Schema.AutoloadProperties);
				disposeGuard.Add<FolderPropertyBag>(folderPropertyBag);
				if (!string.IsNullOrEmpty(displayName))
				{
					folderPropertyBag[CoreFolderSchema.DisplayName] = displayName;
				}
				CoreFolder coreFolder = new CoreFolder(session, folderCreateInfo.Schema, folderPropertyBag, null, null, Origin.New, folderCreateInfo.Schema.AutoloadProperties);
				disposeGuard.Add<CoreFolder>(coreFolder);
				disposeGuard.Success();
				result = coreFolder;
			}
			return result;
		}

		// Token: 0x06000483 RID: 1155 RVA: 0x000281BC File Offset: 0x000263BC
		internal static DeleteItemFlags NormalizeDeleteFlags(DeleteItemFlags flags)
		{
			return flags & DeleteItemFlags.NormalizedDeleteFlags;
		}

		// Token: 0x06000484 RID: 1156 RVA: 0x000281C4 File Offset: 0x000263C4
		private static FolderChangeOperationFlags GetFolderChangeOperationFlags(FolderChangeOperationFlags baseFlags, DeleteItemFlags deleteFlags)
		{
			if ((deleteFlags & DeleteItemFlags.DeclineCalendarItemWithoutResponse) == DeleteItemFlags.DeclineCalendarItemWithoutResponse)
			{
				return baseFlags | FolderChangeOperationFlags.DeclineCalendarItemWithoutResponse;
			}
			if ((deleteFlags & DeleteItemFlags.DeclineCalendarItemWithResponse) == DeleteItemFlags.DeclineCalendarItemWithResponse)
			{
				return baseFlags | FolderChangeOperationFlags.DeclineCalendarItemWithResponse;
			}
			if ((deleteFlags & DeleteItemFlags.CancelCalendarItem) == DeleteItemFlags.CancelCalendarItem)
			{
				return baseFlags | FolderChangeOperationFlags.CancelCalendarItem;
			}
			if ((deleteFlags & DeleteItemFlags.DeleteAllClutter) == DeleteItemFlags.DeleteAllClutter)
			{
				return baseFlags | FolderChangeOperationFlags.DeleteAllClutter | FolderChangeOperationFlags.EmptyFolder;
			}
			if ((deleteFlags & DeleteItemFlags.EmptyFolder) == DeleteItemFlags.EmptyFolder)
			{
				return baseFlags | FolderChangeOperationFlags.EmptyFolder;
			}
			return baseFlags;
		}

		// Token: 0x06000485 RID: 1157 RVA: 0x00028240 File Offset: 0x00026440
		private static DeleteMessagesFlags MapiDeleteFlagsFromXsoDeleteFlags(DeleteItemFlags flags)
		{
			DeleteItemFlags deleteItemFlags = CoreFolder.NormalizeDeleteFlags(flags);
			DeleteMessagesFlags result = DeleteMessagesFlags.None;
			if (deleteItemFlags == DeleteItemFlags.HardDelete)
			{
				result = DeleteMessagesFlags.ForceHardDelete;
			}
			return result;
		}

		// Token: 0x06000486 RID: 1158 RVA: 0x00028260 File Offset: 0x00026460
		private void ResolveStoreObjectType(ICollection<StoreObjectId> sourceIds)
		{
			foreach (StoreObjectId storeObjectId in sourceIds)
			{
				if (storeObjectId.ObjectType == StoreObjectType.Unknown)
				{
					try
					{
						StoreObjectId parentIdFromMessageId = IdConverter.GetParentIdFromMessageId(storeObjectId);
						bool flag;
						if (!base.Session.IsContactFolder.TryGetValue(parentIdFromMessageId, out flag))
						{
							using (CoreFolder coreFolder = CoreFolder.Bind(base.Session, parentIdFromMessageId))
							{
								flag = (coreFolder.StoreObjectId.ObjectType == StoreObjectType.ContactsFolder);
							}
							base.Session.IsContactFolder.Add(parentIdFromMessageId, flag);
						}
						if (flag)
						{
							using (ICoreItem coreItem = CoreItem.Bind(base.Session, storeObjectId))
							{
								storeObjectId.UpdateItemType(coreItem.StoreObjectId.ObjectType);
							}
						}
					}
					catch (StoragePermanentException)
					{
					}
					catch (StorageTransientException)
					{
					}
				}
			}
		}

		// Token: 0x06000487 RID: 1159 RVA: 0x00028378 File Offset: 0x00026578
		private bool SourceIdsContainContactsOrDistributionsLists(ICollection<StoreObjectId> sourceIds)
		{
			foreach (StoreObjectId storeObjectId in sourceIds)
			{
				if (storeObjectId.ObjectType == StoreObjectType.DistributionList || storeObjectId.ObjectType == StoreObjectType.Contact)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x06000488 RID: 1160 RVA: 0x000283D4 File Offset: 0x000265D4
		private StoreObjectId StoreObjectId
		{
			get
			{
				return ((ICoreObject)this).StoreObjectId;
			}
		}

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x06000489 RID: 1161 RVA: 0x000283DC File Offset: 0x000265DC
		private FolderPropertyBag FolderPropertyBag
		{
			get
			{
				this.CheckDisposed(null);
				return (FolderPropertyBag)base.PropertyBag;
			}
		}

		// Token: 0x0600048A RID: 1162 RVA: 0x000283F0 File Offset: 0x000265F0
		private MapiFolder GetMapiFolder()
		{
			this.CheckDisposed(null);
			return (MapiFolder)CoreObject.GetPersistablePropertyBag(this).MapiProp;
		}

		// Token: 0x0600048B RID: 1163 RVA: 0x0002840C File Offset: 0x0002660C
		private GroupOperationResult ExecuteMapiGroupOperationMethod(string operationAttempted, StoreObjectId[] sourceObjectIds, CoreFolder.MapiGroupOperationWithResult mapiGroupOperationCall)
		{
			byte[][] array = null;
			byte[][] array2 = null;
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
				mapiGroupOperationCall(out array, out array2);
			}
			catch (MapiPermanentException ex)
			{
				throw StorageGlobals.TranslateMapiException(ServerStrings.MapiCopyMessagesFailed, ex, session, this, "{0}. MapiException = {1}.", new object[]
				{
					string.Format("Failed to {0}", operationAttempted),
					ex
				});
			}
			catch (MapiRetryableException ex2)
			{
				throw StorageGlobals.TranslateMapiException(ServerStrings.MapiCopyMessagesFailed, ex2, session, this, "{0}. MapiException = {1}.", new object[]
				{
					string.Format("Failed to {0}", operationAttempted),
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
			if ((array != null || array2 != null) && array.Length != array2.Length)
			{
				throw new InvalidOperationException("Unexpected result returned from MAPI. Different number of new entryids and change numbers");
			}
			if (array != null && array2 != null && array.Length == sourceObjectIds.Length)
			{
				StoreObjectId[] array3 = new StoreObjectId[sourceObjectIds.Length];
				for (int i = 0; i < array3.Length; i++)
				{
					array3[i] = StoreObjectId.FromProviderSpecificId(array[i], sourceObjectIds[i].ObjectType);
				}
				return new GroupOperationResult(OperationResult.Succeeded, sourceObjectIds, null, array3, array2);
			}
			return new GroupOperationResult(OperationResult.Succeeded, sourceObjectIds, null);
		}

		// Token: 0x0600048C RID: 1164 RVA: 0x0002859C File Offset: 0x0002679C
		private GroupOperationResult ExecuteMapiGroupOperationMethod(string operationAttempted, StoreObjectId[] sourceObjectIds, CoreFolder.MapiGroupOperation mapiGroupOperationCall)
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
				mapiGroupOperationCall();
			}
			catch (MapiPermanentException ex)
			{
				throw StorageGlobals.TranslateMapiException(ServerStrings.MapiCopyMessagesFailed, ex, session, this, "{0}. MapiException = {1}.", new object[]
				{
					string.Format("Failed to {0}", operationAttempted),
					ex
				});
			}
			catch (MapiRetryableException ex2)
			{
				throw StorageGlobals.TranslateMapiException(ServerStrings.MapiCopyMessagesFailed, ex2, session, this, "{0}. MapiException = {1}.", new object[]
				{
					string.Format("Failed to {0}", operationAttempted),
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
			return new GroupOperationResult(OperationResult.Succeeded, sourceObjectIds, null);
		}

		// Token: 0x0600048D RID: 1165 RVA: 0x000286B4 File Offset: 0x000268B4
		private GroupOperationResult ExecuteMapiGroupOperationInternal(StoreObjectId[] sourceObjectIds, CoreFolder.MapiGroupOperationInternalMethod method)
		{
			foreach (StoreObjectId id in sourceObjectIds)
			{
				base.Session.CheckSystemFolderAccess(id);
			}
			MapiFolder mapiFolder = this.GetMapiFolder();
			bool allowWarnings = mapiFolder.AllowWarnings;
			GroupOperationResult result;
			try
			{
				mapiFolder.AllowWarnings = true;
				result = method();
			}
			catch (PartialCompletionException storageException)
			{
				result = new GroupOperationResult(OperationResult.PartiallySucceeded, sourceObjectIds, storageException);
			}
			catch (ObjectExistedException storageException2)
			{
				result = new GroupOperationResult(OperationResult.Failed, sourceObjectIds, storageException2);
			}
			catch (FolderCycleException storageException3)
			{
				result = new GroupOperationResult(OperationResult.Failed, sourceObjectIds, storageException3);
			}
			finally
			{
				mapiFolder.AllowWarnings = allowWarnings;
			}
			return result;
		}

		// Token: 0x0600048E RID: 1166 RVA: 0x0002879C File Offset: 0x0002699C
		private GroupOperationResult ExecuteMapiGroupOperation(string operationAttempted, StoreObjectId[] sourceObjectIds, CoreFolder.MapiGroupOperationWithResult mapiGroupOperationCall)
		{
			return this.ExecuteMapiGroupOperationInternal(sourceObjectIds, () => this.ExecuteMapiGroupOperationMethod(operationAttempted, sourceObjectIds, mapiGroupOperationCall));
		}

		// Token: 0x0600048F RID: 1167 RVA: 0x0002880C File Offset: 0x00026A0C
		private GroupOperationResult ExecuteMapiGroupOperation(string operationAttempted, StoreObjectId[] sourceObjectIds, CoreFolder.MapiGroupOperation mapiGroupOperationCall)
		{
			return this.ExecuteMapiGroupOperationInternal(sourceObjectIds, () => this.ExecuteMapiGroupOperationMethod(operationAttempted, sourceObjectIds, mapiGroupOperationCall));
		}

		// Token: 0x06000490 RID: 1168 RVA: 0x00028853 File Offset: 0x00026A53
		private PropValue[] OptionalXsoPropertiesToPropValues(PropertyDefinition[] propertyDefinitions, object[] propertyValues)
		{
			if (propertyDefinitions == null)
			{
				return Array<PropValue>.Empty;
			}
			return MapiPropertyBag.MapiPropValuesFromXsoProperties(base.Session, this.GetMapiFolder(), propertyDefinitions, propertyValues);
		}

		// Token: 0x06000491 RID: 1169 RVA: 0x00028874 File Offset: 0x00026A74
		internal static byte[][] StoreObjectIdsToEntryIds(StoreSession storeSession, StoreObjectId[] storeObjectIds)
		{
			byte[][] array = new byte[storeObjectIds.Length][];
			for (int i = 0; i < storeObjectIds.Length; i++)
			{
				array[i] = storeSession.IdConverter.GetSessionSpecificId(storeObjectIds[i]).ProviderLevelItemId;
			}
			return array;
		}

		// Token: 0x06000492 RID: 1170 RVA: 0x000288B0 File Offset: 0x00026AB0
		internal void ValidateItemIds(StoreObjectId[] storeObjectIds, CoreFolder.ItemIdValidatorDelegate itemIdValidator)
		{
			foreach (StoreObjectId storeObjectId in storeObjectIds)
			{
				if (IdConverter.IsFolderId(storeObjectId))
				{
					throw new ArgumentException(ServerStrings.ExInvalidItemId);
				}
				if (storeObjectId is OccurrenceStoreObjectId)
				{
					throw new ArgumentException(ServerStrings.ExCannotMoveOrCopyOccurrenceItem(storeObjectId));
				}
				if (itemIdValidator != null)
				{
					itemIdValidator(storeObjectId);
				}
			}
		}

		// Token: 0x06000493 RID: 1171 RVA: 0x0002890C File Offset: 0x00026B0C
		internal static bool IsMovingToDeletedItems(StoreSession session, StoreObjectId destinationFolderId)
		{
			MailboxSession mailboxSession = session as MailboxSession;
			return mailboxSession != null && mailboxSession.IsDefaultFolderType(destinationFolderId) == DefaultFolderType.DeletedItems;
		}

		// Token: 0x06000494 RID: 1172 RVA: 0x000289F0 File Offset: 0x00026BF0
		internal GroupOperationResult InternalMoveOrCopyItems(CoreFolder destinationFolder, StoreObjectId[] sourceItemIds, PropertyDefinition[] propertyDefinitions, object[] propertyValues, bool move, bool returnNewIds, DeleteItemFlags? deleteFlags, bool updateSource)
		{
			this.CheckDisposed(null);
			bool flag = returnNewIds;
			this.ResolveStoreObjectType(sourceItemIds);
			TeamMailboxClientOperations.ThrowIfInvalidFolderOperation(destinationFolder.Session, destinationFolder);
			if (base.Session == destinationFolder.Session && (this.SourceIdsContainContactsOrDistributionsLists(sourceItemIds) || (base.Session.ActivitySession != null && move)))
			{
				returnNewIds = true;
			}
			PropValue[] propValues = this.OptionalXsoPropertiesToPropValues(propertyDefinitions, propertyValues);
			StoreObjectId[] array = sourceItemIds;
			Set<StoreObjectId> set = null;
			GroupOperationResult groupOperationResult = null;
			if (move && sourceItemIds.Length > 1)
			{
				set = new Set<StoreObjectId>(sourceItemIds.Length);
				for (int i = 0; i < sourceItemIds.Length; i++)
				{
					set.SafeAdd(sourceItemIds[i]);
				}
				array = set.ToArray();
			}
			FolderChangeOperationFlags folderChangeOperationFlags = FolderChangeOperationFlags.IncludeAll;
			FolderChangeOperation operation;
			if (!move)
			{
				operation = FolderChangeOperation.Copy;
			}
			else if (CoreFolder.IsMovingToDeletedItems(base.Session, destinationFolder.Id.ObjectId))
			{
				operation = FolderChangeOperation.MoveToDeletedItems;
				if (deleteFlags != null)
				{
					folderChangeOperationFlags = CoreFolder.GetFolderChangeOperationFlags(folderChangeOperationFlags, deleteFlags.Value);
				}
			}
			else
			{
				operation = FolderChangeOperation.Move;
				if (deleteFlags != null && (deleteFlags.Value & DeleteItemFlags.ClutterActionByUserOverride) != DeleteItemFlags.None)
				{
					folderChangeOperationFlags |= FolderChangeOperationFlags.ClutterActionByUserOverride;
				}
			}
			ICollection<StoreObjectId> itemIds = null;
			ICollection<StoreObjectId> collection2;
			if (set != null)
			{
				ICollection<StoreObjectId> collection = set;
				collection2 = collection;
			}
			else
			{
				collection2 = sourceItemIds;
			}
			itemIds = collection2;
			using (CallbackContext callbackContext = new CallbackContext(base.Session))
			{
				try
				{
					base.Session.OnBeforeFolderChange(operation, folderChangeOperationFlags, base.Session, destinationFolder.Session, this.StoreObjectId, destinationFolder.StoreObjectId, itemIds, callbackContext);
					byte[][] entryIds = CoreFolder.StoreObjectIdsToEntryIds(base.Session, array);
					CopyMessagesFlags flags = move ? CopyMessagesFlags.Move : CopyMessagesFlags.None;
					if (!move && !updateSource)
					{
						flags |= CopyMessagesFlags.DontUpdateSource;
					}
					if (returnNewIds)
					{
						groupOperationResult = this.ExecuteMapiGroupOperation("InternalMoveOrCopyItems", array, delegate(out byte[][] newEntryIds, out byte[][] newChangeNumbers)
						{
							this.GetMapiFolder().CopyMessages(flags, destinationFolder.GetMapiFolder(), propValues, entryIds, out newEntryIds, out newChangeNumbers);
						});
					}
					else if (propValues.Length > 0)
					{
						groupOperationResult = this.ExecuteMapiGroupOperation("InternalMoveOrCopyItems", array, delegate()
						{
							this.GetMapiFolder().CopyMessages(flags, destinationFolder.GetMapiFolder(), propValues, entryIds);
						});
					}
					else
					{
						groupOperationResult = this.ExecuteMapiGroupOperation("InternalMoveOrCopyItems", array, delegate()
						{
							this.GetMapiFolder().CopyMessages(flags, destinationFolder.GetMapiFolder(), entryIds);
						});
					}
				}
				finally
				{
					base.Session.OnAfterFolderChange(operation, folderChangeOperationFlags, base.Session, destinationFolder.Session, this.StoreObjectId, destinationFolder.StoreObjectId, itemIds, groupOperationResult, callbackContext);
				}
			}
			if (flag != returnNewIds)
			{
				groupOperationResult = new GroupOperationResult(groupOperationResult.OperationResult, groupOperationResult.ObjectIds, groupOperationResult.Exception);
			}
			return groupOperationResult;
		}

		// Token: 0x06000495 RID: 1173 RVA: 0x00028CE4 File Offset: 0x00026EE4
		private void CoreObjectUpdate()
		{
			((FolderSchema)this.GetSchema()).CoreObjectUpdate(this);
		}

		// Token: 0x06000496 RID: 1174 RVA: 0x00028CF7 File Offset: 0x00026EF7
		public IModifyTable GetPermissionTableDoNotLoadEntries(ModifyTableOptions options)
		{
			return this.GetPermissionTable(options, false, false);
		}

		// Token: 0x06000497 RID: 1175 RVA: 0x00028D02 File Offset: 0x00026F02
		public IModifyTable GetPermissionTable(ModifyTableOptions options)
		{
			return this.GetPermissionTable(options, false);
		}

		// Token: 0x06000498 RID: 1176 RVA: 0x00028D0C File Offset: 0x00026F0C
		internal IModifyTable GetPermissionTable(ModifyTableOptions options, bool useSecurityDescriptorOnly)
		{
			return this.GetPermissionTable(options, false, true);
		}

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x06000499 RID: 1177 RVA: 0x00028D17 File Offset: 0x00026F17
		internal AclTableIdMap AclTableIdMap
		{
			get
			{
				if (this.aclTableIdMap == null)
				{
					this.aclTableIdMap = new AclTableIdMap();
				}
				return this.aclTableIdMap;
			}
		}

		// Token: 0x0600049A RID: 1178 RVA: 0x00028D32 File Offset: 0x00026F32
		private IModifyTable GetPermissionTable(ModifyTableOptions options, bool useSecurityDescriptorOnly, bool loadTableEntries)
		{
			EnumValidator.ThrowIfInvalid<ModifyTableOptions>(options, "options");
			if (base.Session.IsE15Session)
			{
				return new AclModifyTable(this, options, new MapiAclTableRestriction(this), useSecurityDescriptorOnly, loadTableEntries);
			}
			return new PropertyTable(this, CoreFolderSchema.MapiAclTable, options, new MapiAclTableRestriction(this));
		}

		// Token: 0x0600049B RID: 1179 RVA: 0x00028D70 File Offset: 0x00026F70
		private void InternalSyncHierarchyToContentMailbox(PublicFolderSession publicFolderSession, StoreObjectId folderId)
		{
			PublicFolderContentMailboxInfo contentMailboxInfo = this.GetContentMailboxInfo();
			Guid guid = contentMailboxInfo.IsValid ? contentMailboxInfo.MailboxGuid : Guid.Empty;
			ExchangePrincipal contentMailboxPrincipal;
			if (guid != Guid.Empty && guid != publicFolderSession.MailboxGuid && PublicFolderSession.TryGetPublicFolderMailboxPrincipal(publicFolderSession.OrganizationId, guid, false, out contentMailboxPrincipal))
			{
				PublicFolderSyncJobRpc.SyncFolder(contentMailboxPrincipal, folderId.ProviderLevelItemId);
			}
		}

		// Token: 0x04000147 RID: 327
		private readonly FolderSchema schema;

		// Token: 0x04000148 RID: 328
		private readonly QueryExecutor queryExecutor;

		// Token: 0x04000149 RID: 329
		private static readonly PropertyDefinition[] MailEnabledFolderProperty = new PropertyDefinition[]
		{
			InternalSchema.MailEnabled
		};

		// Token: 0x0400014A RID: 330
		private static NativeStorePropertyDefinition[] permissionChangeRelatedProperties = new NativeStorePropertyDefinition[]
		{
			CoreFolderSchema.PermissionChangeBlocked,
			CoreFolderSchema.RawSecurityDescriptor,
			CoreFolderSchema.RawFreeBusySecurityDescriptor
		};

		// Token: 0x0400014B RID: 331
		private static StorePropertyDefinition[] propertiesToSyncForPublicFolders = new StorePropertyDefinition[]
		{
			InternalSchema.OverallAgeLimit,
			InternalSchema.RetentionAgeLimit,
			InternalSchema.PfOverHardQuotaLimit,
			InternalSchema.PfStorageQuota,
			InternalSchema.PfMsgSizeLimit,
			InternalSchema.DisablePerUserRead
		};

		// Token: 0x0400014C RID: 332
		private AclTableIdMap aclTableIdMap;

		// Token: 0x02000032 RID: 50
		// (Invoke) Token: 0x0600049F RID: 1183
		internal delegate void MapiGroupOperation();

		// Token: 0x02000033 RID: 51
		// (Invoke) Token: 0x060004A3 RID: 1187
		internal delegate void MapiGroupOperationWithResult(out byte[][] newEntryIds, out byte[][] newChangeNumbers);

		// Token: 0x02000034 RID: 52
		// (Invoke) Token: 0x060004A7 RID: 1191
		private delegate GroupOperationResult MapiGroupOperationInternalMethod();

		// Token: 0x02000035 RID: 53
		// (Invoke) Token: 0x060004AB RID: 1195
		internal delegate void ItemIdValidatorDelegate(StoreObjectId storeObjectIds);
	}
}
