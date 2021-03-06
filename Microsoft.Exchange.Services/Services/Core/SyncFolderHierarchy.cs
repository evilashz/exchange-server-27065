using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.Services.Core.DataConverter;
using Microsoft.Exchange.Services.Core.Search;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x0200038C RID: 908
	internal sealed class SyncFolderHierarchy : SingleStepServiceCommand<SyncFolderHierarchyRequest, SyncFolderHierarchyChangesType>
	{
		// Token: 0x0600195C RID: 6492 RVA: 0x0008F1F0 File Offset: 0x0008D3F0
		static SyncFolderHierarchy()
		{
			Array.Copy(DeviceSyncStateMetadata.NullSyncPropertiesFolders, 0, SyncFolderHierarchy.PreLoadFolderProperties, 0, DeviceSyncStateMetadata.NullSyncPropertiesFolders.Length);
			Array.Copy(FolderSyncStateMetadata.IPMFolderNullSyncProperties, 0, SyncFolderHierarchy.PreLoadFolderProperties, DeviceSyncStateMetadata.NullSyncPropertiesFolders.Length, FolderSyncStateMetadata.IPMFolderNullSyncProperties.Length);
			Array.Copy(SyncFolderHierarchy.PropertiesUsedByOWA, 0, SyncFolderHierarchy.PreLoadFolderProperties, DeviceSyncStateMetadata.NullSyncPropertiesFolders.Length + FolderSyncStateMetadata.IPMFolderNullSyncProperties.Length, SyncFolderHierarchy.PropertiesUsedByOWA.Length);
		}

		// Token: 0x0600195D RID: 6493 RVA: 0x0008F2D0 File Offset: 0x0008D4D0
		public SyncFolderHierarchy(CallContext callContext, SyncFolderHierarchyRequest request) : base(callContext, request)
		{
			this.responseShape = Global.ResponseShapeResolver.GetResponseShape<FolderResponseShape>(base.Request.ShapeName, base.Request.FolderShape, base.CallContext.FeaturesManager);
			this.syncStateBase64String = base.Request.SyncState;
			this.rootFolderId = ((base.Request.SyncFolderId == null || base.Request.SyncFolderId.BaseFolderId == null) ? null : base.Request.SyncFolderId.BaseFolderId);
			this.returnRootFolder = base.Request.ReturnRootFolder;
			this.returnPeopleIKnowFolder = base.Request.ReturnPeopleIKnowFolder;
			this.foldersToMoveToTop = base.Request.FolderToMoveToTop;
			this.determiner = PropertyListForViewRowDeterminer.BuildForFolders(this.responseShape);
			ServiceCommandBase.ThrowIfNull(this.responseShape, "responseShape", "SyncFolderHierarchy");
		}

		// Token: 0x0600195E RID: 6494 RVA: 0x0008F3B8 File Offset: 0x0008D5B8
		internal override IExchangeWebMethodResponse GetResponse()
		{
			BaseInfoResponse baseInfoResponse = new SyncFolderHierarchyResponse();
			baseInfoResponse.ProcessServiceResult<SyncFolderHierarchyChangesType>(base.Result);
			return baseInfoResponse;
		}

		// Token: 0x0600195F RID: 6495 RVA: 0x0008F3D8 File Offset: 0x0008D5D8
		internal override ServiceResult<SyncFolderHierarchyChangesType> Execute()
		{
			IdAndSession idAndSession;
			if (this.rootFolderId == null)
			{
				string effectiveAccessingSmtpAddress = base.CallContext.GetEffectiveAccessingSmtpAddress();
				idAndSession = base.IdConverter.ConvertDefaultFolderType(DefaultFolderType.Root, effectiveAccessingSmtpAddress);
			}
			else
			{
				idAndSession = base.IdConverter.ConvertFolderIdToIdAndSessionReadOnly(this.rootFolderId);
			}
			string syncState = string.Empty;
			if (!(idAndSession.Session is MailboxSession))
			{
				throw new ServiceInvalidOperationException((CoreResources.IDs)2674546476U);
			}
			MailboxSession mailboxSession = (MailboxSession)idAndSession.Session;
			StoreObjectId asStoreObjectId = idAndSession.GetAsStoreObjectId();
			MailboxSyncProviderFactory mailboxSyncProviderFactory = new MailboxSyncProviderFactory(mailboxSession, asStoreObjectId);
			SyncFolderHierarchyChangesType syncFolderHierarchyChangesType = new SyncFolderHierarchyChangesType();
			bool includesLastFolderInRange = true;
			using (ISyncProvider syncProvider = mailboxSyncProviderFactory.CreateSyncProvider(null))
			{
				ServicesFolderHierarchySyncState servicesFolderHierarchySyncState = new ServicesFolderHierarchySyncState(mailboxSession, asStoreObjectId, syncProvider, this.syncStateBase64String);
				List<IStorePropertyBag> list = new List<IStorePropertyBag>();
				this.TryInitializeRowsDictionary();
				HierarchySyncOperations hierarchySyncOperations = null;
				SearchFolder searchFolder = null;
				FolderHierarchySync folderHierarchySync;
				try
				{
					this.ReadIPMSubTreeFolderHierarchy(idAndSession, list);
					if (this.returnPeopleIKnowFolder && base.CallContext.AccessingPrincipal != null && base.CallContext.AccessingPrincipal.GetConfiguration().OwaClientServer.PeopleCentricTriage.Enabled)
					{
						searchFolder = this.ReadPeopleIKnowFolderProperties(list);
					}
					folderHierarchySync = servicesFolderHierarchySyncState.GetFolderHierarchySync();
					hierarchySyncOperations = folderHierarchySync.EnumerateServerOperations(asStoreObjectId, false, list, null);
				}
				finally
				{
					if (searchFolder != null)
					{
						searchFolder.Dispose();
					}
				}
				for (int i = 0; i < hierarchySyncOperations.Count; i++)
				{
					HierarchySyncOperation hierSyncOperation = hierarchySyncOperations[i];
					SyncFolderHierarchyChangeBase change;
					if (this.TryBuildFolderHierarchyChangeElement(idAndSession, hierSyncOperation, out change))
					{
						syncFolderHierarchyChangesType.AddChange(change);
					}
				}
				if (this.foldersToMoveToTop != null && this.foldersToMoveToTop.Length > 0)
				{
					syncFolderHierarchyChangesType.Changes = SyncFolderHierarchyResponseProcessor.ShiftChangesToDefaultFoldersToTop(syncFolderHierarchyChangesType.Changes, this.foldersToMoveToTop);
				}
				if (this.returnRootFolder)
				{
					StoreObjectId storeObjectId = StoreId.GetStoreObjectId(idAndSession.Id);
					syncFolderHierarchyChangesType.RootFolder = this.GetFolder(idAndSession, storeObjectId);
				}
				this.objectsChanged = syncFolderHierarchyChangesType.Count;
				includesLastFolderInRange = !hierarchySyncOperations.MoreAvailable;
				folderHierarchySync.AcknowledgeServerOperations();
				syncState = servicesFolderHierarchySyncState.SerializeAsBase64String();
			}
			syncFolderHierarchyChangesType.SyncState = syncState;
			syncFolderHierarchyChangesType.IncludesLastFolderInRange = includesLastFolderInRange;
			return new ServiceResult<SyncFolderHierarchyChangesType>(syncFolderHierarchyChangesType);
		}

		// Token: 0x06001960 RID: 6496 RVA: 0x0008F614 File Offset: 0x0008D814
		private bool TryBuildFolderHierarchyChangeElement(IdAndSession idAndSession, HierarchySyncOperation hierSyncOperation, out SyncFolderHierarchyChangeBase change)
		{
			StoreObjectId itemId = hierSyncOperation.ItemId;
			change = null;
			switch (hierSyncOperation.ChangeType)
			{
			case ChangeType.Add:
				change = this.GetFolderChange(idAndSession, itemId, false);
				goto IL_94;
			case ChangeType.Change:
				change = this.GetFolderChange(idAndSession, itemId, true);
				goto IL_94;
			case ChangeType.Delete:
			case ChangeType.SoftDelete:
			{
				ConcatenatedIdAndChangeKey concatenatedId = IdConverter.GetConcatenatedId(itemId, idAndSession, null);
				FolderId folderId = new FolderId(concatenatedId.Id, concatenatedId.ChangeKey);
				change = new SyncFolderHierarchyDeleteType(folderId);
				goto IL_94;
			}
			case ChangeType.OutOfFilter:
				goto IL_94;
			}
			ExTraceGlobals.SyncFolderHierarchyCallTracer.TraceDebug<ChangeType>((long)this.GetHashCode(), "SyncFolderHierarchy.TryBuildFolderHierarchyChangeElement unknown sync change type {0}", hierSyncOperation.ChangeType);
			IL_94:
			return change != null;
		}

		// Token: 0x06001961 RID: 6497 RVA: 0x0008F6C0 File Offset: 0x0008D8C0
		private BaseFolderType GetFolder(IdAndSession idAndSession, StoreObjectId folderId)
		{
			if (this.rowDictionary != null)
			{
				IStorePropertyBag propertyBag = null;
				if (this.rowDictionary.TryGetValue(folderId, out propertyBag))
				{
					IDictionary<PropertyDefinition, object> dictionary = new Dictionary<PropertyDefinition, object>();
					foreach (PropertyDefinition propertyDefinition in SyncFolderHierarchy.PreLoadFolderProperties)
					{
						object value;
						if (FolderHierarchySync.TryGetPropertyFromBag<object>(propertyBag, propertyDefinition, null, out value))
						{
							dictionary[propertyDefinition] = value;
						}
					}
					StoreObjectType storeObjectType;
					ToServiceObjectForPropertyBagPropertyList toServiceObjectPropertyList = this.determiner.GetToServiceObjectPropertyList(dictionary, out storeObjectType);
					BaseFolderType baseFolderType = BaseFolderType.CreateFromStoreObjectType(storeObjectType);
					toServiceObjectPropertyList.ConvertPropertiesToServiceObject(baseFolderType, dictionary, idAndSession);
					return baseFolderType;
				}
			}
			ToServiceObjectPropertyList toServiceObjectPropertyList2 = XsoDataConverter.GetToServiceObjectPropertyList(folderId, idAndSession.Session, this.responseShape, base.ParticipantResolver);
			toServiceObjectPropertyList2.IgnoreCorruptPropertiesWhenRendering = true;
			BaseFolderType result;
			try
			{
				using (Folder xsoFolder = ServiceCommandBase.GetXsoFolder(idAndSession.Session, folderId, ref toServiceObjectPropertyList2))
				{
					BaseFolderType baseFolderType2 = BaseFolderType.CreateFromStoreObjectType(folderId.ObjectType);
					ServiceCommandBase.LoadServiceObject(baseFolderType2, xsoFolder, idAndSession, this.responseShape, toServiceObjectPropertyList2);
					result = baseFolderType2;
				}
			}
			catch (ObjectNotFoundException)
			{
				ExTraceGlobals.SyncFolderHierarchyCallTracer.TraceError((long)this.GetHashCode(), "SyncFolderHierarchy.GetFolderXml detected deleted folder on Add/Change sync operation");
				result = null;
			}
			return result;
		}

		// Token: 0x06001962 RID: 6498 RVA: 0x0008F7E8 File Offset: 0x0008D9E8
		private SyncFolderHierarchyChangeBase GetFolderChange(IdAndSession idAndSession, StoreObjectId folderId, bool isUpdate)
		{
			SyncFolderHierarchyCreateOrUpdateType result = null;
			BaseFolderType folder = this.GetFolder(idAndSession, folderId);
			if (folder != null)
			{
				result = new SyncFolderHierarchyCreateOrUpdateType(folder, isUpdate);
			}
			return result;
		}

		// Token: 0x06001963 RID: 6499 RVA: 0x0008F80C File Offset: 0x0008DA0C
		private void TryInitializeRowsDictionary()
		{
			bool flag = false;
			foreach (PropertyDefinition propertyDefinition in this.determiner.GetPropertiesToFetch())
			{
				bool flag2 = false;
				int num = 0;
				while (!flag2 && num < SyncFolderHierarchy.PreLoadFolderProperties.Length)
				{
					flag2 = (SyncFolderHierarchy.PreLoadFolderProperties[num] == propertyDefinition);
					num++;
				}
				if (!flag2)
				{
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				this.rowDictionary = new Dictionary<StoreObjectId, IStorePropertyBag>();
			}
		}

		// Token: 0x06001964 RID: 6500 RVA: 0x0008F878 File Offset: 0x0008DA78
		private void ReadIPMSubTreeFolderHierarchy(IdAndSession rootFolderIdAndSession, List<IStorePropertyBag> resultRows)
		{
			using (Folder folder = Folder.Bind(rootFolderIdAndSession.Session, rootFolderIdAndSession.Id))
			{
				using (QueryResult queryResult = folder.FolderQuery(FolderQueryFlags.DeepTraversal, null, null, SyncFolderHierarchy.PreLoadFolderProperties))
				{
					for (;;)
					{
						IStorePropertyBag[] propertyBags = queryResult.GetPropertyBags(10000);
						if (propertyBags == null || propertyBags.Length == 0)
						{
							break;
						}
						foreach (IStorePropertyBag storePropertyBag in propertyBags)
						{
							VersionedId versionedId = null;
							if (FolderHierarchySync.TryGetPropertyFromBag<VersionedId>(storePropertyBag, FolderSchema.Id, null, out versionedId) && versionedId != null)
							{
								resultRows.Add(storePropertyBag);
								if (this.rowDictionary != null)
								{
									this.rowDictionary[versionedId.ObjectId] = storePropertyBag;
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x06001965 RID: 6501 RVA: 0x0008F948 File Offset: 0x0008DB48
		private SearchFolder ReadPeopleIKnowFolderProperties(List<IStorePropertyBag> resultRows)
		{
			ExTraceGlobals.SyncFolderHierarchyCallTracer.TraceFunction((long)this.GetHashCode(), "SyncFolderHierarchy.ReadPeopleIKnowFolderProperties");
			IdAndSession idAndSession = base.IdConverter.ConvertDefaultFolderType(DefaultFolderType.FromFavoriteSenders, base.CallContext.GetEffectiveAccessingSmtpAddress());
			bool flag = false;
			SearchFolder searchFolder = null;
			SearchFolder result;
			try
			{
				searchFolder = SearchFolder.Bind(idAndSession.Session, idAndSession.Id, SyncFolderHierarchy.PreLoadFolderProperties);
				if (!searchFolder.IsPopulated())
				{
					ExTraceGlobals.SyncFolderHierarchyCallTracer.TraceDebug((long)this.GetHashCode(), "FromFavoriteSenders folder not yet populated.  Skipped.");
					searchFolder.Dispose();
					searchFolder = null;
					flag = true;
					result = null;
				}
				else
				{
					resultRows.Add(searchFolder.PropertyBag.AsIStorePropertyBag());
					if (this.rowDictionary != null)
					{
						this.rowDictionary[searchFolder.Id.ObjectId] = searchFolder.PropertyBag.AsIStorePropertyBag();
					}
					flag = true;
					result = searchFolder;
				}
			}
			finally
			{
				if (!flag && searchFolder != null)
				{
					searchFolder.Dispose();
					searchFolder = null;
				}
			}
			return result;
		}

		// Token: 0x04001102 RID: 4354
		private readonly bool returnRootFolder;

		// Token: 0x04001103 RID: 4355
		private readonly bool returnPeopleIKnowFolder;

		// Token: 0x04001104 RID: 4356
		private FolderResponseShape responseShape;

		// Token: 0x04001105 RID: 4357
		private string syncStateBase64String;

		// Token: 0x04001106 RID: 4358
		private BaseFolderId rootFolderId;

		// Token: 0x04001107 RID: 4359
		private PropertyListForViewRowDeterminer determiner;

		// Token: 0x04001108 RID: 4360
		private Dictionary<StoreObjectId, IStorePropertyBag> rowDictionary;

		// Token: 0x04001109 RID: 4361
		private DistinguishedFolderIdName[] foldersToMoveToTop;

		// Token: 0x0400110A RID: 4362
		private static readonly PropertyDefinition[] PropertiesUsedByOWA = new PropertyDefinition[]
		{
			FolderSchema.ItemCount,
			FolderSchema.ChildCount,
			StoreObjectSchema.RetentionFlags,
			StoreObjectSchema.PolicyTag,
			StoreObjectSchema.RetentionPeriod,
			StoreObjectSchema.ArchiveTag,
			StoreObjectSchema.ArchivePeriod,
			FolderSchema.UnreadCount,
			StoreObjectSchema.EffectiveRights
		};

		// Token: 0x0400110B RID: 4363
		private static readonly PropertyDefinition[] PreLoadFolderProperties = new PropertyDefinition[DeviceSyncStateMetadata.NullSyncPropertiesFolders.Length + FolderSyncStateMetadata.IPMFolderNullSyncProperties.Length + SyncFolderHierarchy.PropertiesUsedByOWA.Length];
	}
}
