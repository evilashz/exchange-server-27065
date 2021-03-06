using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Exchange.Collections;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000057 RID: 87
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class Folder : StoreObject, IFolder, IStoreObject, IStorePropertyBag, IPropertyBag, IReadOnlyPropertyBag, IDisposable
	{
		// Token: 0x0600066E RID: 1646 RVA: 0x00035A13 File Offset: 0x00033C13
		internal Folder(CoreFolder coreFolder) : base(coreFolder, false)
		{
		}

		// Token: 0x0600066F RID: 1647 RVA: 0x00035A1D File Offset: 0x00033C1D
		protected static bool ExtendedFlagsContains(int? extendedFolderFlags, ExtendedFolderFlags flag)
		{
			return extendedFolderFlags != null && (flag & (ExtendedFolderFlags)extendedFolderFlags.Value) != (ExtendedFolderFlags)0;
		}

		// Token: 0x06000670 RID: 1648 RVA: 0x00035A39 File Offset: 0x00033C39
		public static Folder Create(StoreSession session, StoreId parentFolderId, StoreObjectType folderType)
		{
			Util.ThrowOnNullArgument(session, "session");
			Util.ThrowOnNullArgument(parentFolderId, "parentFolderId");
			EnumValidator.ThrowIfInvalid<StoreObjectType>(folderType, Folder.FolderTypes);
			ExTraceGlobals.StorageTracer.Information(0L, "Folder::Create.");
			return Folder.InternalCreateFolder(session, parentFolderId, folderType, null, CreateMode.CreateNew, false);
		}

		// Token: 0x06000671 RID: 1649 RVA: 0x00035A78 File Offset: 0x00033C78
		public static Folder Create(StoreSession session, StoreId parentFolderId, StoreObjectType foldertype, string displayName, CreateMode createMode)
		{
			Util.ThrowOnNullArgument(session, "session");
			Util.ThrowOnNullArgument(parentFolderId, "parentFolderId");
			EnumValidator.ThrowIfInvalid<StoreObjectType>(foldertype, Folder.FolderTypes);
			Util.ThrowOnNullArgument(displayName, "displayName");
			EnumValidator.ThrowIfInvalid<CreateMode>(createMode, "createMode");
			ExTraceGlobals.StorageTracer.Information(0L, "Folder::Create.");
			return Folder.InternalCreateFolder(session, parentFolderId, foldertype, displayName, createMode, false);
		}

		// Token: 0x06000672 RID: 1650 RVA: 0x00035ADA File Offset: 0x00033CDA
		public static Folder CreateSecure(StoreSession session, StoreId parentFolderId, StoreObjectType folderType)
		{
			Util.ThrowOnNullArgument(session, "session");
			Util.ThrowOnNullArgument(parentFolderId, "parentFolderId");
			EnumValidator.ThrowIfInvalid<StoreObjectType>(folderType, Folder.FolderTypes);
			ExTraceGlobals.StorageTracer.Information(0L, "Folder::CreateSecure.");
			return Folder.InternalCreateFolder(session, parentFolderId, folderType, null, CreateMode.CreateNew, true);
		}

		// Token: 0x06000673 RID: 1651 RVA: 0x00035B1C File Offset: 0x00033D1C
		public static Folder CreateSecure(StoreSession session, StoreId parentFolderId, StoreObjectType foldertype, string displayName, CreateMode createMode)
		{
			Util.ThrowOnNullArgument(session, "session");
			Util.ThrowOnNullArgument(parentFolderId, "parentFolderId");
			EnumValidator.ThrowIfInvalid<StoreObjectType>(foldertype, Folder.FolderTypes);
			Util.ThrowOnNullArgument(displayName, "displayName");
			EnumValidator.ThrowIfInvalid<CreateMode>(createMode, "createMode");
			ExTraceGlobals.StorageTracer.Information(0L, "Folder::CreateSecure.");
			return Folder.InternalCreateFolder(session, parentFolderId, foldertype, displayName, createMode, true);
		}

		// Token: 0x06000674 RID: 1652 RVA: 0x00035B7E File Offset: 0x00033D7E
		public static Folder Bind(StoreSession session, StoreId folderId)
		{
			return Folder.Bind(session, folderId, null);
		}

		// Token: 0x06000675 RID: 1653 RVA: 0x00035B88 File Offset: 0x00033D88
		public static Folder Bind(StoreSession session, StoreId folderId, params PropertyDefinition[] propsToReturn)
		{
			return Folder.Bind(session, folderId, (ICollection<PropertyDefinition>)propsToReturn);
		}

		// Token: 0x06000676 RID: 1654 RVA: 0x00035B97 File Offset: 0x00033D97
		public static Folder Bind(StoreSession session, StoreId folderId, ICollection<PropertyDefinition> propsToReturn)
		{
			Util.ThrowOnNullArgument(session, "session");
			Util.ThrowOnNullArgument(folderId, "folderId");
			return Folder.InternalBind<Folder>(session, folderId, propsToReturn);
		}

		// Token: 0x06000677 RID: 1655 RVA: 0x00035BB7 File Offset: 0x00033DB7
		public static Folder Bind(StoreSession session, DefaultFolderType defaultFolderType)
		{
			return Folder.Bind(session, defaultFolderType, null);
		}

		// Token: 0x06000678 RID: 1656 RVA: 0x00035BC1 File Offset: 0x00033DC1
		public static Folder Bind(StoreSession session, DefaultFolderType defaultFolderType, params PropertyDefinition[] propsToReturn)
		{
			return Folder.Bind(session, defaultFolderType, (ICollection<PropertyDefinition>)propsToReturn);
		}

		// Token: 0x06000679 RID: 1657 RVA: 0x00035BD0 File Offset: 0x00033DD0
		public static Folder Bind(StoreSession session, DefaultFolderType defaultFolderType, ICollection<PropertyDefinition> propsToReturn)
		{
			ObjectNotFoundException ex = null;
			StoreObjectId folderId = session.SafeGetDefaultFolderId(defaultFolderType);
			for (int i = 0; i < 2; i++)
			{
				try
				{
					return Folder.Bind(session, folderId, propsToReturn);
				}
				catch (ObjectNotFoundException ex2)
				{
					ex = ex2;
					ExTraceGlobals.StorageTracer.Information<DefaultFolderType>(0L, "Folder::Bind(defaultFolderType): attempting to recreate {0}.", defaultFolderType);
					if (!session.TryFixDefaultFolderId(defaultFolderType, out folderId))
					{
						throw;
					}
				}
			}
			throw ex;
		}

		// Token: 0x0600067A RID: 1658 RVA: 0x00035C38 File Offset: 0x00033E38
		public static bool IsFolderId(StoreId id)
		{
			if (id == null)
			{
				ExTraceGlobals.StorageTracer.TraceError<string>(0L, "StoreSession::IsFolderId. The containerId cannot be null. Argument = {0}.", "id");
				throw new ArgumentNullException(ServerStrings.ExNullParameter("id", 1));
			}
			StoreObjectId storeObjectId = StoreId.GetStoreObjectId(id);
			return storeObjectId.ObjectType == StoreObjectType.Folder || storeObjectId.ProviderLevelItemId == null || IdConverter.IsFolderId(storeObjectId);
		}

		// Token: 0x0600067B RID: 1659 RVA: 0x00035C97 File Offset: 0x00033E97
		public static QueryFilter GetClutterBasedViewFilter(bool isClutter)
		{
			if (!isClutter)
			{
				return Folder.DefaultNoClutterFilter;
			}
			return Folder.DefaultClutterFilter;
		}

		// Token: 0x0600067C RID: 1660 RVA: 0x00035CA7 File Offset: 0x00033EA7
		public override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<Folder>(this);
		}

		// Token: 0x0600067D RID: 1661 RVA: 0x00035CB0 File Offset: 0x00033EB0
		private static bool IsFlagsValid(ExtendedFolderFlags? flags, bool hasPermissions)
		{
			if (hasPermissions)
			{
				if (flags != null && (flags.Value & ExtendedFolderFlags.SharedViaExchange) != (ExtendedFolderFlags)0)
				{
					ExTraceGlobals.StorageTracer.Information(0L, "Folder::IsFlagsValid. ExtendedFolderFlags already contains SharedViaExchange.");
					return true;
				}
			}
			else if (flags == null || (flags.Value & ExtendedFolderFlags.SharedViaExchange) == (ExtendedFolderFlags)0)
			{
				ExTraceGlobals.StorageTracer.Information(0L, "Folder::IsFlagsValid. ExtendedFolderFlags doesn't contain SharedViaExchange.");
				return true;
			}
			return false;
		}

		// Token: 0x0600067E RID: 1662 RVA: 0x00035D18 File Offset: 0x00033F18
		private static MapiMessage InternalCreateMapiMessage(MapiFolder mapiFolder, CreateMessageType mapiMessageType, StoreSession session)
		{
			EnumValidator.ThrowIfInvalid<CreateMessageType>(mapiMessageType);
			object thisObject = null;
			bool flag = false;
			MapiMessage result;
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
				switch (mapiMessageType)
				{
				case CreateMessageType.Associated:
					result = mapiFolder.CreateAssociatedMessage();
					break;
				case CreateMessageType.Aggregated:
					result = mapiFolder.CreateMessage(CreateMessageFlags.ContentAggregation);
					break;
				default:
					result = mapiFolder.CreateMessage(CreateMessageFlags.DeferredErrors);
					break;
				}
			}
			catch (MapiPermanentException ex)
			{
				throw StorageGlobals.TranslateMapiException(ServerStrings.MapiCannotCreateMessage, ex, session, thisObject, "{0}. MapiException = {1}.", new object[]
				{
					string.Format("Folder::InternalCreateMapiMessage failed.", new object[0]),
					ex
				});
			}
			catch (MapiRetryableException ex2)
			{
				throw StorageGlobals.TranslateMapiException(ServerStrings.MapiCannotCreateMessage, ex2, session, thisObject, "{0}. MapiException = {1}.", new object[]
				{
					string.Format("Folder::InternalCreateMapiMessage failed.", new object[0]),
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
			return result;
		}

		// Token: 0x0600067F RID: 1663 RVA: 0x00035E6C File Offset: 0x0003406C
		private static MapiMessage InternalCreateMapiMessageForDelivery(MapiFolder mapiFolder, CreateMessageType mapiMessageType, StoreSession session, string internetMessageId, ExDateTime? clientSubmitTime)
		{
			EnumValidator.ThrowIfInvalid<CreateMessageType>(mapiMessageType);
			DateTime? clientSubmitTime2 = null;
			if (clientSubmitTime != null)
			{
				clientSubmitTime2 = new DateTime?((DateTime)clientSubmitTime.Value.ToUtc());
			}
			bool flag = false;
			MapiMessage mapiMessage = null;
			try
			{
				object thisObject = null;
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
					if (mapiMessageType == CreateMessageType.Aggregated)
					{
						mapiMessage = mapiFolder.CreateMessageForDelivery(CreateMessageFlags.ContentAggregation | CreateMessageFlags.DeferredErrors, internetMessageId, clientSubmitTime2);
					}
					else
					{
						mapiMessage = mapiFolder.CreateMessageForDelivery(CreateMessageFlags.DeferredErrors, internetMessageId, clientSubmitTime2);
					}
				}
				catch (MapiPermanentException ex)
				{
					throw StorageGlobals.TranslateMapiException(ServerStrings.MapiCannotCreateMessage, ex, session, thisObject, "{0}. MapiException = {1}.", new object[]
					{
						string.Format("Folder::InternalCreateMapiMessageForDelivery failed.", new object[0]),
						ex
					});
				}
				catch (MapiRetryableException ex2)
				{
					throw StorageGlobals.TranslateMapiException(ServerStrings.MapiCannotCreateMessage, ex2, session, thisObject, "{0}. MapiException = {1}.", new object[]
					{
						string.Format("Folder::InternalCreateMapiMessageForDelivery failed.", new object[0]),
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
				flag = true;
			}
			finally
			{
				if (!flag && mapiMessage != null)
				{
					mapiMessage.Dispose();
					mapiMessage = null;
				}
			}
			return mapiMessage;
		}

		// Token: 0x06000680 RID: 1664 RVA: 0x00035FFC File Offset: 0x000341FC
		private static Folder InternalCreateFolder(StoreSession session, StoreId parentId, StoreObjectType itemType, string displayName, CreateMode createMode, bool isSecure)
		{
			Util.ThrowOnNullArgument(session, "session");
			Util.ThrowOnNullArgument(parentId, "parentId");
			EnumValidator.ThrowIfInvalid<StoreObjectType>(itemType, Folder.FolderTypes);
			EnumValidator.ThrowIfInvalid<CreateMode>(createMode, "createMode");
			Folder folder = null;
			CoreFolder coreFolder = null;
			bool flag = false;
			Folder result;
			try
			{
				FolderCreateInfo folderCreateInfo = FolderCreateInfo.GetFolderCreateInfo(itemType);
				coreFolder = CoreFolder.InternalCreate(session, parentId, itemType == StoreObjectType.SearchFolder || itemType == StoreObjectType.OutlookSearchFolder, displayName, createMode, isSecure, folderCreateInfo);
				if (folderCreateInfo.ContainerClass != null)
				{
					coreFolder.PropertyBag[StoreObjectSchema.ContainerClass] = folderCreateInfo.ContainerClass;
				}
				folder = folderCreateInfo.Creator(coreFolder);
				flag = true;
				result = folder;
			}
			finally
			{
				if (!flag)
				{
					Util.DisposeIfPresent(folder);
					Util.DisposeIfPresent(coreFolder);
				}
			}
			return result;
		}

		// Token: 0x06000681 RID: 1665 RVA: 0x000360B0 File Offset: 0x000342B0
		private static T InternalBind<T>(Folder.CoreFolderBindDelegate coreFolderBindDelegate) where T : Folder
		{
			CoreFolder coreFolder = null;
			Folder folder = null;
			T t = default(T);
			bool flag = false;
			T result;
			try
			{
				coreFolder = coreFolderBindDelegate();
				FolderCreateInfo folderCreateInfo = FolderCreateInfo.GetFolderCreateInfo(((ICoreObject)coreFolder).StoreObjectId.ObjectType);
				folder = folderCreateInfo.Creator(coreFolder);
				t = folder.DownCastStoreObject<T>();
				flag = true;
				result = t;
			}
			finally
			{
				if (!flag)
				{
					Util.DisposeIfPresent(t);
					Util.DisposeIfPresent(folder);
					Util.DisposeIfPresent(coreFolder);
				}
			}
			return result;
		}

		// Token: 0x06000682 RID: 1666 RVA: 0x00036178 File Offset: 0x00034378
		private static AggregateOperationResult ExecuteOperationOnObjects(Folder.ActOnFolderDelegate actOnFolderDelegate, Folder.ActOnItemsDelegate actOnItemsDelegate, params StoreId[] sourceObjectIds)
		{
			List<StoreObjectId> list = new List<StoreObjectId>(sourceObjectIds.Length);
			List<StoreObjectId> list2 = new List<StoreObjectId>(sourceObjectIds.Length);
			Folder.GroupItemsAndFolders(sourceObjectIds, list, list2);
			List<GroupOperationResult> groupOperationResults = new List<GroupOperationResult>();
			using (List<StoreObjectId>.Enumerator enumerator = list2.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					StoreObjectId sourcefolderId = enumerator.Current;
					Folder.ExecuteGroupOperationAndAggregateResults(groupOperationResults, new StoreObjectId[]
					{
						sourcefolderId
					}, () => actOnFolderDelegate(sourcefolderId));
				}
			}
			if (list.Count > 0)
			{
				StoreObjectId[] sourceItemIdArray = list.ToArray();
				Folder.ExecuteGroupOperationAndAggregateResults(groupOperationResults, sourceItemIdArray, () => actOnItemsDelegate(sourceItemIdArray));
			}
			return Folder.CreateAggregateOperationResult(groupOperationResults);
		}

		// Token: 0x06000683 RID: 1667 RVA: 0x00036284 File Offset: 0x00034484
		private static void GroupItemsAndFolders(StoreId[] sourceObjectIds, List<StoreObjectId> sourceItemIds, List<StoreObjectId> sourceFolderIds)
		{
			foreach (StoreId id in sourceObjectIds)
			{
				StoreObjectId storeObjectId = StoreId.GetStoreObjectId(id);
				if (IdConverter.IsFolderId(storeObjectId))
				{
					sourceFolderIds.Add(storeObjectId);
				}
				else
				{
					sourceItemIds.Add(storeObjectId);
				}
			}
		}

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x06000684 RID: 1668 RVA: 0x000362C4 File Offset: 0x000344C4
		// (set) Token: 0x06000685 RID: 1669 RVA: 0x000362E1 File Offset: 0x000344E1
		public string DisplayName
		{
			get
			{
				this.CheckDisposed("DisplayName::get");
				return base.GetValueOrDefault<string>(InternalSchema.DisplayName, string.Empty);
			}
			set
			{
				this.CheckDisposed("DisplayName::set");
				this[InternalSchema.DisplayName] = value;
			}
		}

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x06000686 RID: 1670 RVA: 0x000362FA File Offset: 0x000344FA
		// (set) Token: 0x06000687 RID: 1671 RVA: 0x00036317 File Offset: 0x00034517
		public string Description
		{
			get
			{
				this.CheckDisposed("Description::get");
				return base.GetValueOrDefault<string>(InternalSchema.Description, string.Empty);
			}
			set
			{
				this.CheckDisposed("Description::set");
				this[InternalSchema.Description] = value;
			}
		}

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x06000688 RID: 1672 RVA: 0x00036330 File Offset: 0x00034530
		public int ItemCount
		{
			get
			{
				this.CheckDisposed("ItemCount::get");
				return base.GetValueOrDefault<int>(InternalSchema.ItemCount);
			}
		}

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x06000689 RID: 1673 RVA: 0x00036348 File Offset: 0x00034548
		public bool HasSubfolders
		{
			get
			{
				this.CheckDisposed("HasSubfolders::get");
				return base.GetValueOrDefault<bool>(InternalSchema.HasChildren);
			}
		}

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x0600068A RID: 1674 RVA: 0x00036360 File Offset: 0x00034560
		public override Schema Schema
		{
			get
			{
				this.CheckDisposed("Schema::get");
				return FolderSchema.Instance;
			}
		}

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x0600068B RID: 1675 RVA: 0x00036372 File Offset: 0x00034572
		public int SubfolderCount
		{
			get
			{
				this.CheckDisposed("SubfolderCount::get");
				return base.GetValueOrDefault<int>(InternalSchema.ChildCount);
			}
		}

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x0600068C RID: 1676 RVA: 0x0003638A File Offset: 0x0003458A
		// (set) Token: 0x0600068D RID: 1677 RVA: 0x000363A7 File Offset: 0x000345A7
		public override string ClassName
		{
			get
			{
				this.CheckDisposed("ClassName::get");
				return base.GetValueOrDefault<string>(InternalSchema.ContainerClass, string.Empty);
			}
			set
			{
				this.CheckDisposed("ClassName::set");
				this[InternalSchema.ContainerClass] = value;
			}
		}

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x0600068E RID: 1678 RVA: 0x000363C0 File Offset: 0x000345C0
		public bool IsExchangeCrossOrgShareFolder
		{
			get
			{
				this.CheckDisposed("IsExchangeCrossOrgShareFolder::get");
				return this.ExtendedFlagsContains(ExtendedFolderFlags.ExchangeCrossOrgShareFolder);
			}
		}

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x0600068F RID: 1679 RVA: 0x000363D8 File Offset: 0x000345D8
		public bool IsExchangeConsumerShareFolder
		{
			get
			{
				this.CheckDisposed("IsExchangeConsumerShareFolder::get");
				return this.ExtendedFlagsContains(ExtendedFolderFlags.ExchangeConsumerShareFolder);
			}
		}

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x06000690 RID: 1680 RVA: 0x000363F0 File Offset: 0x000345F0
		// (set) Token: 0x06000691 RID: 1681 RVA: 0x0003640D File Offset: 0x0003460D
		public ExDateTime LastAttemptedSyncTime
		{
			get
			{
				this.CheckDisposed("LastAttemptedSyncTime::get");
				return base.GetValueOrDefault<ExDateTime>(FolderSchema.SubscriptionLastAttemptedSyncTime, ExDateTime.MinValue);
			}
			set
			{
				this.CheckDisposed("LastAttemptedSyncTime::set");
				this[FolderSchema.SubscriptionLastAttemptedSyncTime] = value;
			}
		}

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x06000692 RID: 1682 RVA: 0x0003642B File Offset: 0x0003462B
		// (set) Token: 0x06000693 RID: 1683 RVA: 0x00036448 File Offset: 0x00034648
		public ExDateTime LastSuccessfulSyncTime
		{
			get
			{
				this.CheckDisposed("LastSuccessfulSyncTime::get");
				return base.GetValueOrDefault<ExDateTime>(FolderSchema.SubscriptionLastSuccessfulSyncTime, ExDateTime.MinValue);
			}
			set
			{
				this.CheckDisposed("LastSuccessfulSyncTime::set");
				this[FolderSchema.SubscriptionLastSuccessfulSyncTime] = value;
			}
		}

		// Token: 0x06000694 RID: 1684 RVA: 0x00036466 File Offset: 0x00034666
		public virtual FolderSaveResult Save()
		{
			return this.Save(SaveMode.NoConflictResolution);
		}

		// Token: 0x06000695 RID: 1685 RVA: 0x00036470 File Offset: 0x00034670
		public FolderSaveResult Save(SaveMode saveMode)
		{
			this.CheckDisposed("Save");
			EnumValidator.ThrowIfInvalid<SaveMode>(saveMode, "saveMode");
			if (saveMode == SaveMode.ResolveConflicts)
			{
				throw new NotSupportedException("Folder does not support resolving conflicts.");
			}
			this.CheckFolderIsShared();
			ExTraceGlobals.StorageTracer.Information<int, SaveMode>((long)this.GetHashCode(), "Folder::Save. HashCode = {0}, saveMode = {1}.", this.GetHashCode(), saveMode);
			FolderSaveResult result = this.CoreFolder.Save(saveMode);
			this.SavePermissions();
			return result;
		}

		// Token: 0x06000696 RID: 1686 RVA: 0x000364D8 File Offset: 0x000346D8
		private void CheckFolderIsShared()
		{
			if (this.IsPermissionChangePending)
			{
				bool flag = false;
				using (IEnumerator<Permission> enumerator = this.PermissionTable.GetEnumerator())
				{
					if (enumerator.MoveNext())
					{
						Permission permission = enumerator.Current;
						flag = true;
					}
				}
				ExTraceGlobals.StorageTracer.Information<bool>((long)this.GetHashCode(), "Folder::CheckFolderIsShared. HasPermissions = {0}.", flag);
				ExtendedFolderFlags? valueAsNullable = base.GetValueAsNullable<ExtendedFolderFlags>(FolderSchema.ExtendedFolderFlags);
				Trace storageTracer = ExTraceGlobals.StorageTracer;
				long id = (long)this.GetHashCode();
				string formatString = "Folder::CheckFolderIsShared. Get ExtendedFolderFlags = {0}.";
				object[] array = new object[1];
				object[] array2 = array;
				int num = 0;
				ExtendedFolderFlags? extendedFolderFlags = valueAsNullable;
				array2[num] = ((extendedFolderFlags != null) ? extendedFolderFlags.GetValueOrDefault() : "<null>");
				storageTracer.Information(id, formatString, array);
				if (Folder.IsFlagsValid(valueAsNullable, flag))
				{
					return;
				}
				ExtendedFolderFlags extendedFolderFlags2;
				if (flag)
				{
					extendedFolderFlags2 = ExtendedFolderFlags.SharedViaExchange;
					if (valueAsNullable != null)
					{
						extendedFolderFlags2 |= valueAsNullable.Value;
					}
				}
				else
				{
					extendedFolderFlags2 = (valueAsNullable.Value & ~ExtendedFolderFlags.SharedViaExchange);
				}
				ExTraceGlobals.StorageTracer.Information<ExtendedFolderFlags>((long)this.GetHashCode(), "Folder::CheckFolderIsShared. Set ExtendedFolderFlags = {0}.", extendedFolderFlags2);
				this[FolderSchema.ExtendedFolderFlags] = extendedFolderFlags2;
			}
		}

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x06000697 RID: 1687 RVA: 0x000365F4 File Offset: 0x000347F4
		private bool IsPermissionChangePending
		{
			get
			{
				return this.PermissionTable != null && this.PermissionTable.IsDirty;
			}
		}

		// Token: 0x06000698 RID: 1688 RVA: 0x0003660B File Offset: 0x0003480B
		private void SavePermissions()
		{
			if (this.IsPermissionChangePending)
			{
				this.InternalSavePermissions(this.PermissionTable);
			}
			this.PermissionTable = null;
		}

		// Token: 0x06000699 RID: 1689 RVA: 0x00036628 File Offset: 0x00034828
		protected virtual void InternalSavePermissions(PermissionTable permissionsTable)
		{
			permissionsTable.Save(this.CoreFolder);
		}

		// Token: 0x0600069A RID: 1690 RVA: 0x00036636 File Offset: 0x00034836
		public IQueryResult IFolderQuery(FolderQueryFlags queryFlags, QueryFilter queryFilter, SortBy[] sortColumns, params PropertyDefinition[] dataColumns)
		{
			return this.FolderQuery(queryFlags, queryFilter, sortColumns, dataColumns);
		}

		// Token: 0x0600069B RID: 1691 RVA: 0x00036643 File Offset: 0x00034843
		public QueryResult FolderQuery(FolderQueryFlags queryFlags, QueryFilter queryFilter, SortBy[] sortColumns, params PropertyDefinition[] dataColumns)
		{
			this.CheckDisposed("FolderQuery");
			return this.CoreFolder.QueryExecutor.FolderQuery(queryFlags, queryFilter, sortColumns, dataColumns);
		}

		// Token: 0x0600069C RID: 1692 RVA: 0x00036665 File Offset: 0x00034865
		public QueryResult ItemQuery(ItemQueryType queryFlags, QueryFilter queryFilter, SortBy[] sortColumns, params PropertyDefinition[] dataColumns)
		{
			return this.ItemQuery(queryFlags, queryFilter, sortColumns, (ICollection<PropertyDefinition>)dataColumns);
		}

		// Token: 0x0600069D RID: 1693 RVA: 0x00036677 File Offset: 0x00034877
		public IQueryResult IItemQuery(ItemQueryType queryFlags, QueryFilter queryFilter, SortBy[] sortColumns, ICollection<PropertyDefinition> dataColumns)
		{
			return this.ItemQuery(queryFlags, queryFilter, sortColumns, dataColumns);
		}

		// Token: 0x0600069E RID: 1694 RVA: 0x00036684 File Offset: 0x00034884
		public QueryResult ItemQuery(ItemQueryType queryFlags, QueryFilter queryFilter, SortBy[] sortColumns, ICollection<PropertyDefinition> dataColumns)
		{
			this.CheckDisposed("ItemQuery");
			return this.CoreFolder.QueryExecutor.ItemQuery(queryFlags, queryFilter, sortColumns, dataColumns);
		}

		// Token: 0x0600069F RID: 1695 RVA: 0x000366A6 File Offset: 0x000348A6
		public QueryResult ConversationItemQuery(QueryFilter queryFilter, SortBy[] sortColumns, params PropertyDefinition[] dataColumns)
		{
			return this.ConversationItemQuery(queryFilter, sortColumns, (ICollection<PropertyDefinition>)dataColumns);
		}

		// Token: 0x060006A0 RID: 1696 RVA: 0x000366B8 File Offset: 0x000348B8
		public QueryResult ConversationItemQuery(QueryFilter queryFilter, SortBy[] sortColumns, ICollection<PropertyDefinition> dataColumns)
		{
			this.CheckDisposed("ConversationItemQuery");
			if (!(base.Session is MailboxSession))
			{
				throw new InvalidOperationException("ConversationItemQuery can only be performed on folders in MailboxSession");
			}
			if (sortColumns != null)
			{
				foreach (SortBy sortBy in sortColumns)
				{
					if (!ConversationItemSchema.Instance.AllProperties.Contains(sortBy.ColumnDefinition))
					{
						throw new ConversationItemQueryException(new LocalizedString("ConversationItemQuery can only take propertyDefinition from ConversationItemSchema"), sortBy.ColumnDefinition);
					}
				}
			}
			foreach (PropertyDefinition propertyDefinition in dataColumns)
			{
				if (!ConversationItemSchema.Instance.AllProperties.Contains(propertyDefinition) && !ItemSchema.InstanceKey.Equals(propertyDefinition) && !StoreObjectSchema.LastModifiedTime.Equals(propertyDefinition))
				{
					throw new ConversationItemQueryException(new LocalizedString("ConversationItemQuery can only take propertyDefinition from ConversationItemSchema"), propertyDefinition);
				}
			}
			return this.CoreFolder.QueryExecutor.InternalItemQuery(ContentsTableFlags.ShowConversations, queryFilter, sortColumns, QueryExclusionType.Row, dataColumns, null);
		}

		// Token: 0x060006A1 RID: 1697 RVA: 0x000367C4 File Offset: 0x000349C4
		public QueryResult ConversationMembersQuery(QueryFilter queryFilter, SortBy[] sortColumns, ICollection<PropertyDefinition> dataColumns)
		{
			this.CheckDisposed("ConversationMembersQuery");
			if (!(base.Session is MailboxSession))
			{
				throw new InvalidOperationException("ConversationMembersQuery can only be performed on folders in MailboxSession");
			}
			return this.CoreFolder.QueryExecutor.InternalItemQuery(ContentsTableFlags.ShowConversationMembers, queryFilter, sortColumns, QueryExclusionType.Row, dataColumns, null);
		}

		// Token: 0x060006A2 RID: 1698 RVA: 0x00036810 File Offset: 0x00034A10
		public IQueryResult IConversationMembersQuery(QueryFilter queryFilter, SortBy[] sortColumns, ICollection<PropertyDefinition> dataColumns)
		{
			return this.ConversationMembersQuery(queryFilter, sortColumns, dataColumns);
		}

		// Token: 0x060006A3 RID: 1699 RVA: 0x0003681B File Offset: 0x00034A1B
		public IQueryResult IConversationItemQuery(QueryFilter queryFilter, SortBy[] sortColumns, ICollection<PropertyDefinition> dataColumns)
		{
			return this.ConversationItemQuery(queryFilter, sortColumns, dataColumns);
		}

		// Token: 0x060006A4 RID: 1700 RVA: 0x00036826 File Offset: 0x00034A26
		public GroupedQueryResult GroupedItemQuery(QueryFilter queryFilter, PropertyDefinition groupBy, GroupSort groupSort, SortBy[] sortColumns, params PropertyDefinition[] dataColumns)
		{
			this.CheckDisposed("GroupedItemQuery");
			return this.CoreFolder.QueryExecutor.GroupedItemQuery(queryFilter, groupBy, groupSort, sortColumns, dataColumns);
		}

		// Token: 0x060006A5 RID: 1701 RVA: 0x0003684A File Offset: 0x00034A4A
		public QueryResult GroupedItemQuery(QueryFilter queryFilter, ItemQueryType queryFlags, GroupByAndOrder[] groupBy, int expandCount, SortBy[] sortColumns, params PropertyDefinition[] dataColumns)
		{
			this.CheckDisposed("GroupedItemQuery");
			return this.CoreFolder.QueryExecutor.GroupedItemQuery(queryFilter, queryFlags, groupBy, expandCount, sortColumns, dataColumns);
		}

		// Token: 0x060006A6 RID: 1702 RVA: 0x000368B8 File Offset: 0x00034AB8
		public IQueryResult AggregatedItemQuery(QueryFilter queryFilter, QueryFilter aggregationFilter, SortBy[] sortColumns, ICollection<PropertyDefinition> dataColumns, AggregationExtension aggregationExtension, Schema schema)
		{
			Util.ThrowOnNullArgument(dataColumns, "dataColumns");
			Util.ThrowOnNullArgument(aggregationExtension, "aggregationExtension");
			Util.ThrowOnNullArgument(schema, "schema");
			this.CheckDisposed("AggregatedItemQuery");
			if (!(base.Session is MailboxSession))
			{
				throw new InvalidOperationException("AggregatedItemQuery can only be performed on folders in MailboxSession");
			}
			if (sortColumns != null && sortColumns.Any((SortBy sortBy) => !schema.AllProperties.Contains(sortBy.ColumnDefinition)))
			{
				throw new ArgumentException("AggregatedItemQuery can only take sort columns from given schema", "sortColumns");
			}
			ICollection<PropertyDefinition> collection = null;
			if (aggregationFilter == null)
			{
				collection = dataColumns;
			}
			else
			{
				IEnumerable<PropertyDefinition> enumerable = aggregationFilter.FilterProperties();
				int capacity = dataColumns.Count + enumerable.Count<PropertyDefinition>();
				collection = new List<PropertyDefinition>(capacity);
				foreach (PropertyDefinition item in dataColumns)
				{
					collection.Add(item);
				}
				foreach (PropertyDefinition item2 in enumerable)
				{
					if (!collection.Contains(item2))
					{
						collection.Add(item2);
					}
				}
			}
			if (collection.Any((PropertyDefinition column) => !schema.AllProperties.Contains(column) && column != ItemSchema.InstanceKey))
			{
				throw new ArgumentException("AggregatedItemQuery can only take data columns from given schema", "dataColumns");
			}
			ContentsTableFlags flags = ContentsTableFlags.ShowConversations | ContentsTableFlags.ExpandedConversationView;
			ConversationMembersQueryResult conversationMembersQueryResult = (ConversationMembersQueryResult)this.CoreFolder.QueryExecutor.InternalItemQuery(flags, queryFilter, sortColumns, QueryExclusionType.Row, collection, aggregationExtension);
			if (aggregationFilter != null)
			{
				return AggregationQueryResult.FromQueryResult(conversationMembersQueryResult, aggregationFilter, dataColumns.Count);
			}
			return conversationMembersQueryResult;
		}

		// Token: 0x060006A7 RID: 1703 RVA: 0x00036A5C File Offset: 0x00034C5C
		public IQueryResult PersonItemQuery(QueryFilter queryFilter, QueryFilter aggregationFilter, SortBy[] sortColumns, ICollection<PropertyDefinition> dataColumns, AggregationExtension aggregationExtension)
		{
			ArgumentValidator.ThrowIfNull("aggregationExtension", aggregationExtension);
			return this.AggregatedItemQuery(queryFilter, aggregationFilter, sortColumns, dataColumns, aggregationExtension, PersonSchema.Instance);
		}

		// Token: 0x060006A8 RID: 1704 RVA: 0x00036A7C File Offset: 0x00034C7C
		public IQueryResult PersonItemQuery(QueryFilter queryFilter, QueryFilter aggregationFilter, SortBy[] sortColumns, ICollection<PropertyDefinition> dataColumns)
		{
			return this.PersonItemQuery(queryFilter, aggregationFilter, sortColumns, dataColumns, new PeopleAggregationExtension((MailboxSession)base.Session));
		}

		// Token: 0x060006A9 RID: 1705 RVA: 0x00036A99 File Offset: 0x00034C99
		public IQueryResult PersonItemQuery(SortBy[] sortColumns, ICollection<PropertyDefinition> dataColumns)
		{
			return this.PersonItemQuery(null, null, sortColumns, dataColumns);
		}

		// Token: 0x060006AA RID: 1706 RVA: 0x00036AA5 File Offset: 0x00034CA5
		public IQueryResult PersonItemQuery(ICollection<PropertyDefinition> dataColumns)
		{
			return this.PersonItemQuery(null, null, null, dataColumns);
		}

		// Token: 0x060006AB RID: 1707 RVA: 0x00036AB1 File Offset: 0x00034CB1
		public IQueryResult AggregatedConversationQuery(QueryFilter queryFilter, SortBy[] sortColumns, ICollection<PropertyDefinition> dataColumns, AggregationExtension aggregationExtension)
		{
			return this.AggregatedItemQuery(queryFilter, null, sortColumns, dataColumns, aggregationExtension, AggregatedConversationSchema.Instance);
		}

		// Token: 0x060006AC RID: 1708 RVA: 0x00036AC4 File Offset: 0x00034CC4
		public GroupOperationResult CopyItems(StoreId destinationFolderId, params StoreId[] sourceItemIds)
		{
			return this.CopyItems(base.Session, destinationFolderId, sourceItemIds);
		}

		// Token: 0x060006AD RID: 1709 RVA: 0x00036AD4 File Offset: 0x00034CD4
		public GroupOperationResult CopyItems(StoreSession destinationSession, StoreId destinationFolderId, params StoreId[] sourceItemIds)
		{
			return this.CopyItems(destinationSession, destinationFolderId, true, sourceItemIds);
		}

		// Token: 0x060006AE RID: 1710 RVA: 0x00036AE0 File Offset: 0x00034CE0
		public GroupOperationResult CopyItems(StoreSession destinationSession, StoreId destinationFolderId, bool updateSource, params StoreId[] sourceItemIds)
		{
			this.CheckDisposed("CopyItems");
			ExTraceGlobals.StorageTracer.Information<int>((long)this.GetHashCode(), "Folder::CopyItems. HashCode = {0}.", this.GetHashCode());
			if (sourceItemIds == null)
			{
				throw new ArgumentNullException("sourceItemIds");
			}
			if (destinationFolderId == null)
			{
				throw new ArgumentNullException("destinationFolderId");
			}
			if (destinationSession == null)
			{
				throw new ArgumentNullException("destinationSession");
			}
			GroupOperationResult result;
			using (Folder folder = Folder.Bind(destinationSession, destinationFolderId))
			{
				result = this.CopyItems(folder, Folder.StoreIdsToStoreObjectIds(sourceItemIds), null, null);
			}
			return result;
		}

		// Token: 0x060006AF RID: 1711 RVA: 0x00036B78 File Offset: 0x00034D78
		public GroupOperationResult MoveItems(StoreId destinationFolderId, params StoreId[] sourceItemIds)
		{
			return this.MoveItems(base.Session, destinationFolderId, sourceItemIds);
		}

		// Token: 0x060006B0 RID: 1712 RVA: 0x00036B88 File Offset: 0x00034D88
		public GroupOperationResult MoveItems(StoreSession destinationSession, StoreId destinationFolderId, params StoreId[] sourceItemIds)
		{
			this.CheckDisposed("MoveItems");
			ExTraceGlobals.StorageTracer.Information<int>((long)this.GetHashCode(), "Folder::MoveItems. HashCode = {0}.", this.GetHashCode());
			if (sourceItemIds == null)
			{
				throw new ArgumentNullException("sourceItemIds");
			}
			if (destinationFolderId == null)
			{
				throw new ArgumentNullException("destinationFolderId");
			}
			if (destinationSession == null)
			{
				throw new ArgumentNullException("destinationSession");
			}
			GroupOperationResult result;
			using (Folder folder = Folder.Bind(destinationSession, destinationFolderId))
			{
				result = this.MoveItems(folder, Folder.StoreIdsToStoreObjectIds(sourceItemIds), null, null);
			}
			return result;
		}

		// Token: 0x060006B1 RID: 1713 RVA: 0x00036C1C File Offset: 0x00034E1C
		public GroupOperationResult CopyFolder(StoreId destinationFolderId, StoreId sourceFolderId)
		{
			return this.CopyFolder(base.Session, destinationFolderId, sourceFolderId);
		}

		// Token: 0x060006B2 RID: 1714 RVA: 0x00036C2C File Offset: 0x00034E2C
		public GroupOperationResult CopyFolder(StoreSession destinationSession, StoreId destinationFolderId, StoreId sourceFolderId)
		{
			this.CheckDisposed("CopyFolder");
			ExTraceGlobals.StorageTracer.Information<int>((long)this.GetHashCode(), "Folder::CopyFolder. HashCode = {0}.", this.GetHashCode());
			if (sourceFolderId == null)
			{
				throw new ArgumentNullException("sourceFolderId");
			}
			if (destinationFolderId == null)
			{
				throw new ArgumentNullException("destinationFolderId");
			}
			if (destinationSession == null)
			{
				throw new ArgumentNullException("destinationSession");
			}
			GroupOperationResult result;
			using (Folder folder = Folder.Bind(destinationSession, destinationFolderId))
			{
				result = this.CoreFolder.CopyFolder(folder.CoreFolder, CopySubObjects.Copy, StoreId.GetStoreObjectId(sourceFolderId));
			}
			return result;
		}

		// Token: 0x060006B3 RID: 1715 RVA: 0x00036CC8 File Offset: 0x00034EC8
		public GroupOperationResult MoveFolder(StoreId destinationFolderId, StoreId sourceFolderId)
		{
			return this.MoveFolder(base.Session, destinationFolderId, sourceFolderId);
		}

		// Token: 0x060006B4 RID: 1716 RVA: 0x00036CD8 File Offset: 0x00034ED8
		public GroupOperationResult MoveFolder(StoreSession destinationSession, StoreId destinationFolderId, StoreId sourceFolderId)
		{
			this.CheckDisposed("MoveFolder");
			ExTraceGlobals.StorageTracer.Information<int>((long)this.GetHashCode(), "Folder::MoveFolder. HashCode = {0}.", this.GetHashCode());
			if (sourceFolderId == null)
			{
				throw new ArgumentNullException("sourceFolderId");
			}
			if (destinationFolderId == null)
			{
				throw new ArgumentNullException("destinationFolderId");
			}
			if (destinationSession == null)
			{
				throw new ArgumentNullException("destinationSession");
			}
			GroupOperationResult result;
			using (Folder folder = Folder.Bind(destinationSession, destinationFolderId))
			{
				result = this.CoreFolder.MoveFolder(folder.CoreFolder, StoreId.GetStoreObjectId(sourceFolderId));
			}
			return result;
		}

		// Token: 0x060006B5 RID: 1717 RVA: 0x00036D74 File Offset: 0x00034F74
		public AggregateOperationResult CopyObjects(StoreId destinationFolderId, params StoreId[] sourceObjectIds)
		{
			return this.CopyObjects(base.Session, destinationFolderId, sourceObjectIds);
		}

		// Token: 0x060006B6 RID: 1718 RVA: 0x00036D84 File Offset: 0x00034F84
		public AggregateOperationResult CopyObjects(StoreSession destinationSession, StoreId destinationFolderId, params StoreId[] sourceObjectIds)
		{
			return this.CopyObjects(destinationSession, destinationFolderId, false, sourceObjectIds);
		}

		// Token: 0x060006B7 RID: 1719 RVA: 0x00036DEC File Offset: 0x00034FEC
		public AggregateOperationResult CopyObjects(StoreSession destinationSession, StoreId destinationFolderId, bool returnNewIds, params StoreId[] sourceObjectIds)
		{
			Folder.<>c__DisplayClassf CS$<>8__locals1 = new Folder.<>c__DisplayClassf();
			CS$<>8__locals1.returnNewIds = returnNewIds;
			CS$<>8__locals1.<>4__this = this;
			this.CheckDisposed("CopyObjects");
			ExTraceGlobals.StorageTracer.Information<int>((long)this.GetHashCode(), "Folder::CopyObjects. HashCode = {0}.", this.GetHashCode());
			if (sourceObjectIds == null)
			{
				throw new ArgumentNullException("sourceObjectIds");
			}
			if (destinationFolderId == null)
			{
				throw new ArgumentNullException("destinationFolderId");
			}
			if (destinationSession == null)
			{
				throw new ArgumentNullException("destinationSession");
			}
			AggregateOperationResult result;
			using (Folder destinationFolder = Folder.Bind(destinationSession, destinationFolderId))
			{
				result = Folder.ExecuteOperationOnObjects((StoreObjectId sourceFolderId) => CS$<>8__locals1.<>4__this.CoreFolder.CopyFolder(destinationFolder.CoreFolder, CopySubObjects.Copy, sourceFolderId), (StoreObjectId[] sourceItemIds) => CS$<>8__locals1.<>4__this.CopyItems(destinationFolder, sourceItemIds, null, null, CS$<>8__locals1.returnNewIds), sourceObjectIds);
			}
			return result;
		}

		// Token: 0x060006B8 RID: 1720 RVA: 0x00036ED0 File Offset: 0x000350D0
		public AggregateOperationResult MoveObjects(StoreId destinationFolderId, params StoreId[] sourceObjectIds)
		{
			return this.MoveObjects(destinationFolderId, null, sourceObjectIds);
		}

		// Token: 0x060006B9 RID: 1721 RVA: 0x00036EEE File Offset: 0x000350EE
		public AggregateOperationResult MoveObjects(StoreId destinationFolderId, DeleteItemFlags? deleteFlags, params StoreId[] sourceObjectIds)
		{
			return this.MoveObjects(base.Session, destinationFolderId, deleteFlags, sourceObjectIds);
		}

		// Token: 0x060006BA RID: 1722 RVA: 0x00036F00 File Offset: 0x00035100
		public AggregateOperationResult MoveObjects(StoreSession destinationSession, StoreId destinationFolderId, params StoreId[] sourceObjectIds)
		{
			return this.MoveObjects(destinationSession, destinationFolderId, null, sourceObjectIds);
		}

		// Token: 0x060006BB RID: 1723 RVA: 0x00036F1F File Offset: 0x0003511F
		public AggregateOperationResult MoveObjects(StoreSession destinationSession, StoreId destinationFolderId, DeleteItemFlags? deleteFlags, params StoreId[] sourceObjectIds)
		{
			return this.MoveObjects(destinationSession, destinationFolderId, false, deleteFlags, sourceObjectIds);
		}

		// Token: 0x060006BC RID: 1724 RVA: 0x00036F30 File Offset: 0x00035130
		public AggregateOperationResult MoveObjects(StoreSession destinationSession, StoreId destinationFolderId, bool returnNewIds, params StoreId[] sourceObjectIds)
		{
			return this.MoveObjects(destinationSession, destinationFolderId, returnNewIds, null, sourceObjectIds);
		}

		// Token: 0x060006BD RID: 1725 RVA: 0x00036FB8 File Offset: 0x000351B8
		public AggregateOperationResult MoveObjects(StoreSession destinationSession, StoreId destinationFolderId, bool returnNewIds, DeleteItemFlags? deleteFlags, params StoreId[] sourceObjectIds)
		{
			Folder.<>c__DisplayClass17 CS$<>8__locals1 = new Folder.<>c__DisplayClass17();
			CS$<>8__locals1.returnNewIds = returnNewIds;
			CS$<>8__locals1.deleteFlags = deleteFlags;
			CS$<>8__locals1.<>4__this = this;
			this.CheckDisposed("MoveObjects");
			ExTraceGlobals.StorageTracer.Information<int>((long)this.GetHashCode(), "Folder::MoveObjects. HashCode = {0}.", this.GetHashCode());
			if (sourceObjectIds == null)
			{
				throw new ArgumentNullException("sourceObjectIds");
			}
			if (destinationFolderId == null)
			{
				throw new ArgumentNullException("destinationFolderId");
			}
			if (destinationSession == null)
			{
				throw new ArgumentNullException("destinationSession");
			}
			AggregateOperationResult result;
			using (Folder destinationFolder = Folder.Bind(destinationSession, destinationFolderId))
			{
				result = Folder.ExecuteOperationOnObjects((StoreObjectId sourceFolderId) => CS$<>8__locals1.<>4__this.CoreFolder.MoveFolder(destinationFolder.CoreFolder, sourceFolderId), (StoreObjectId[] sourceItemIds) => CS$<>8__locals1.<>4__this.MoveItems(destinationFolder, sourceItemIds, null, null, CS$<>8__locals1.returnNewIds, CS$<>8__locals1.deleteFlags), sourceObjectIds);
			}
			return result;
		}

		// Token: 0x060006BE RID: 1726 RVA: 0x000370A4 File Offset: 0x000352A4
		internal static void CheckPairedArray<T>(T[] propertyDefinitions, object[] values, string errorMessage) where T : PropertyDefinition
		{
			if (propertyDefinitions == null || values == null)
			{
				throw new ArgumentException(errorMessage);
			}
			if (propertyDefinitions.Length != values.Length)
			{
				throw new ArgumentException(errorMessage);
			}
		}

		// Token: 0x060006BF RID: 1727 RVA: 0x000370C4 File Offset: 0x000352C4
		public GroupOperationResult UnsafeMoveItemsAndSetProperties(StoreId destinationFolderId, StoreId[] sourceItemIds, PropertyDefinition[] propertyDefinitions, object[] values)
		{
			this.CheckDisposed("UnsafeMoveItemsAndSetProperties");
			ExTraceGlobals.StorageTracer.Information<int>((long)this.GetHashCode(), "Folder::UnsafeMoveItemsAndSetProperties. HashCode = {0}.", this.GetHashCode());
			if (sourceItemIds == null)
			{
				throw new ArgumentNullException("sourceItemIds");
			}
			if (destinationFolderId == null)
			{
				throw new ArgumentNullException("destinationFolderId");
			}
			Folder.CheckPairedArray<PropertyDefinition>(propertyDefinitions, values, ServerStrings.PropertyDefinitionsValuesNotMatch);
			GroupOperationResult result;
			using (Folder folder = Folder.Bind(base.Session, destinationFolderId))
			{
				result = this.MoveItems(folder, Folder.StoreIdsToStoreObjectIds(sourceItemIds), propertyDefinitions, values);
			}
			return result;
		}

		// Token: 0x060006C0 RID: 1728 RVA: 0x00037164 File Offset: 0x00035364
		public GroupOperationResult UnsafeCopyItemsAndSetProperties(StoreId destinationFolderId, StoreId[] sourceItemIds, PropertyDefinition[] propertyDefinitions, object[] values)
		{
			this.CheckDisposed("UnsafeCopyItemsAndSetProperties");
			ExTraceGlobals.StorageTracer.Information<int>((long)this.GetHashCode(), "Folder::UnsafeCopyItemsAndSetProperties. HashCode = {0}.", this.GetHashCode());
			if (sourceItemIds == null)
			{
				throw new ArgumentNullException("sourceItemIds");
			}
			if (destinationFolderId == null)
			{
				throw new ArgumentNullException("destinationFolderId");
			}
			Folder.CheckPairedArray<PropertyDefinition>(propertyDefinitions, values, ServerStrings.PropertyDefinitionsValuesNotMatch);
			GroupOperationResult result;
			using (Folder folder = Folder.Bind(base.Session, destinationFolderId))
			{
				result = this.CopyItems(folder, Folder.StoreIdsToStoreObjectIds(sourceItemIds), propertyDefinitions, values);
			}
			return result;
		}

		// Token: 0x060006C1 RID: 1729 RVA: 0x00037204 File Offset: 0x00035404
		public AggregateOperationResult DeleteObjects(DeleteItemFlags deleteFlags, params StoreId[] ids)
		{
			return this.DeleteObjects(deleteFlags, false, ids);
		}

		// Token: 0x060006C2 RID: 1730 RVA: 0x00037248 File Offset: 0x00035448
		public AggregateOperationResult DeleteObjects(DeleteItemFlags deleteFlags, bool returnNewItemIds, params StoreId[] ids)
		{
			this.CheckDisposed("DeleteObjects");
			base.Session.CheckDeleteItemFlags(deleteFlags);
			ExTraceGlobals.StorageTracer.Information((long)this.GetHashCode(), "Folder::DeleteObjects.");
			if ((deleteFlags & DeleteItemFlags.SuppressReadReceipt) != DeleteItemFlags.None && (deleteFlags & DeleteItemFlags.MoveToDeletedItems) == DeleteItemFlags.None)
			{
				this.CoreFolder.ClearNotReadNotificationPending(ids);
			}
			List<OccurrenceStoreObjectId> list = new List<OccurrenceStoreObjectId>();
			List<StoreId> list2 = new List<StoreId>();
			Folder.GroupOccurrencesAndObjectIds(ids, list2, list);
			List<GroupOperationResult> list3 = new List<GroupOperationResult>();
			AggregateOperationResult aggregateOperationResult;
			if ((deleteFlags & DeleteItemFlags.MoveToDeletedItems) == DeleteItemFlags.MoveToDeletedItems)
			{
				aggregateOperationResult = this.MoveObjects(base.Session, ((MailboxSession)base.Session).GetDefaultFolderId(DefaultFolderType.DeletedItems), returnNewItemIds, new DeleteItemFlags?(deleteFlags), list2.ToArray());
			}
			else
			{
				aggregateOperationResult = this.InternalDeleteObjects(deleteFlags, list2.ToArray());
			}
			list3.AddRange(aggregateOperationResult.GroupOperationResults);
			using (List<OccurrenceStoreObjectId>.Enumerator enumerator = list.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					OccurrenceStoreObjectId occurrenceId = enumerator.Current;
					Folder.ExecuteGroupOperationAndAggregateResults(list3, new StoreObjectId[]
					{
						occurrenceId
					}, () => this.Session.DeleteCalendarOccurrence(deleteFlags, occurrenceId));
				}
			}
			return Folder.CreateAggregateOperationResult(list3);
		}

		// Token: 0x060006C3 RID: 1731 RVA: 0x000373CC File Offset: 0x000355CC
		public GroupOperationResult DeleteAllObjects(DeleteItemFlags flags)
		{
			bool deleteAssociated = false;
			return this.DeleteAllObjects(flags, deleteAssociated);
		}

		// Token: 0x060006C4 RID: 1732 RVA: 0x000373E4 File Offset: 0x000355E4
		public GroupOperationResult DeleteAllObjects(DeleteItemFlags flags, bool deleteAssociated)
		{
			this.CheckDisposed("DeleteAllObjects");
			base.Session.CheckDeleteItemFlags(flags);
			ExTraceGlobals.StorageTracer.Information<DeleteItemFlags>((long)this.GetHashCode(), "Folder::DeleteAllObjects. flags = {0}.", flags);
			if ((flags & DeleteItemFlags.SuppressReadReceipt) != DeleteItemFlags.None && (flags & DeleteItemFlags.MoveToDeletedItems) == DeleteItemFlags.None)
			{
				this.CoreFolder.ClearNotReadNotificationPending();
			}
			EmptyFolderFlags emptyFolderFlags;
			switch (CoreFolder.NormalizeDeleteFlags(flags))
			{
			case DeleteItemFlags.SoftDelete:
				emptyFolderFlags = EmptyFolderFlags.None;
				break;
			case DeleteItemFlags.HardDelete:
				emptyFolderFlags = EmptyFolderFlags.HardDelete;
				break;
			default:
				ExTraceGlobals.StorageTracer.TraceError((long)this.GetHashCode(), "Folder::DeleteAllObjects. DeleteItemFlags is not supported in DeleteAllItems API.");
				throw new NotSupportedException(ServerStrings.Ex12NotSupportedDeleteItemFlags((int)flags));
			}
			if (deleteAssociated)
			{
				emptyFolderFlags |= EmptyFolderFlags.DeleteAssociatedMessages;
			}
			return this.CoreFolder.EmptyFolder(false, emptyFolderFlags);
		}

		// Token: 0x060006C5 RID: 1733 RVA: 0x00037498 File Offset: 0x00035698
		public AggregateOperationResult DeleteAllItems(DeleteItemFlags flags)
		{
			return this.DeleteAllItems(flags, null);
		}

		// Token: 0x060006C6 RID: 1734 RVA: 0x000374B8 File Offset: 0x000356B8
		public AggregateOperationResult DeleteAllItems(DeleteItemFlags flags, DateTime? createdBefore)
		{
			this.CheckDisposed("DeleteAllItems");
			base.Session.CheckDeleteItemFlags(flags);
			ExTraceGlobals.StorageTracer.Information<DeleteItemFlags>((long)this.GetHashCode(), "Folder::DeleteAllItems. flags = {0}.", flags);
			List<GroupOperationResult> list = new List<GroupOperationResult>();
			QueryFilter queryFilter = null;
			if (createdBefore != null)
			{
				queryFilter = new AndFilter(new QueryFilter[]
				{
					new ComparisonFilter(ComparisonOperator.LessThan, ItemSchema.ReceivedTime, createdBefore.Value),
					new ExistsFilter(ItemSchema.ReceivedTime)
				});
			}
			using (QueryResult queryResult = this.ItemQuery(ItemQueryType.None, queryFilter, null, new PropertyDefinition[]
			{
				ItemSchema.Id
			}))
			{
				object[][] rows;
				do
				{
					rows = queryResult.GetRows(200);
					if (rows.Length != 0)
					{
						VersionedId[] array = new VersionedId[rows.Length];
						for (int i = 0; i < rows.Length; i++)
						{
							array[i] = Microsoft.Exchange.Data.Storage.PropertyBag.CheckPropertyValue<VersionedId>(ItemSchema.Id, rows[i][0]);
						}
						AggregateOperationResult aggregateOperationResult = this.DeleteObjects(flags, array);
						list.AddRange(aggregateOperationResult.GroupOperationResults);
					}
				}
				while (rows.Length != 0);
			}
			return Folder.CreateAggregateOperationResult(list);
		}

		// Token: 0x060006C7 RID: 1735 RVA: 0x000375E0 File Offset: 0x000357E0
		public void MarkAllAsRead(bool suppressReadReceipts)
		{
			this.CheckDisposed("MarkAllAsRead");
			this.InternalSetReadFlags(suppressReadReceipts, true, new StoreId[0]);
		}

		// Token: 0x060006C8 RID: 1736 RVA: 0x000375FB File Offset: 0x000357FB
		public void MarkAllAsUnread(bool suppressReadReceipts)
		{
			this.CheckDisposed("MarkAllAsUnRead");
			this.InternalSetReadFlags(suppressReadReceipts, false, new StoreId[0]);
		}

		// Token: 0x060006C9 RID: 1737 RVA: 0x00037616 File Offset: 0x00035816
		public void MarkAsRead(bool suppressReadReceipts, params StoreId[] ids)
		{
			this.MarkAsRead(suppressReadReceipts, false, ids);
		}

		// Token: 0x060006CA RID: 1738 RVA: 0x00037624 File Offset: 0x00035824
		public void MarkAsRead(bool suppressReadReceipts, bool suppressNotReadReceipts, params StoreId[] ids)
		{
			this.CheckDisposed("MarkAsRead");
			if (ids == null)
			{
				ExTraceGlobals.StorageTracer.TraceError<string>((long)this.GetHashCode(), "Folder::MarkAsRead. The parameter cannot be null. Parameter = {0}.", "itemIds");
				throw new ArgumentNullException(ServerStrings.ExNullParameter("itemIds", 1));
			}
			if (ids.Length != 0)
			{
				this.InternalSetReadFlags(suppressReadReceipts, true, suppressNotReadReceipts, ids);
			}
		}

		// Token: 0x060006CB RID: 1739 RVA: 0x00037680 File Offset: 0x00035880
		public void MarkAsUnread(bool suppressReadReceipts, params StoreId[] ids)
		{
			this.CheckDisposed("MarkAsUnRead");
			if (ids == null)
			{
				ExTraceGlobals.StorageTracer.TraceError<string>((long)this.GetHashCode(), "Folder::MarkAsUnRead. The parameter cannot be null. Parameter = {0}.", "itemIds");
				throw new ArgumentNullException(ServerStrings.ExNullParameter("itemIds", 1));
			}
			if (ids.Length != 0)
			{
				this.InternalSetReadFlags(suppressReadReceipts, false, ids);
			}
		}

		// Token: 0x060006CC RID: 1740 RVA: 0x000376DC File Offset: 0x000358DC
		public MessageStatusFlags SetItemStatus(StoreObjectId itemId, MessageStatusFlags status, MessageStatusFlags statusMask)
		{
			this.CheckDisposed("SetItemStatus");
			EnumValidator.ThrowIfInvalid<MessageStatusFlags>(status, "status");
			EnumValidator.ThrowIfInvalid<MessageStatusFlags>(statusMask, "statusMask");
			MessageStatusFlags result;
			this.CoreFolder.SetItemStatus(itemId, status, statusMask, out result);
			return result;
		}

		// Token: 0x060006CD RID: 1741 RVA: 0x0003771B File Offset: 0x0003591B
		public void SetItemFlags(StoreObjectId itemId, MessageFlags flags, MessageFlags flagsMask)
		{
			this.CheckDisposed("SetItemFlags");
			EnumValidator.ThrowIfInvalid<MessageFlags>(flags, "flags");
			EnumValidator.ThrowIfInvalid<MessageFlags>(flagsMask, "flagsMask");
			this.CoreFolder.SetItemFlags(itemId, flags, flagsMask);
		}

		// Token: 0x060006CE RID: 1742 RVA: 0x0003774C File Offset: 0x0003594C
		internal void ClearNotReadNotificationPending(StoreId[] itemIds)
		{
			this.CheckDisposed("ClearNotReadNotificationPending");
			this.CoreFolder.ClearNotReadNotificationPending(itemIds);
		}

		// Token: 0x060006CF RID: 1743 RVA: 0x00037768 File Offset: 0x00035968
		public PermissionSet GetPermissionSet()
		{
			this.CheckDisposed("GetPermissionSet");
			if (this.PermissionTable == null)
			{
				if (base.CoreObject.Origin == Origin.Existing)
				{
					this.PermissionTable = PermissionTable.Load(new Func<PermissionTable, PermissionSet>(this.CreatePermissionSet), this.CoreFolder);
				}
				else
				{
					this.PermissionTable = PermissionTable.Create(new Func<PermissionTable, PermissionSet>(this.CreatePermissionSet));
				}
			}
			return this.PermissionTable.PermissionSet;
		}

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x060006D0 RID: 1744 RVA: 0x000377D8 File Offset: 0x000359D8
		protected CoreFolder CoreFolder
		{
			get
			{
				return (CoreFolder)base.CoreObject;
			}
		}

		// Token: 0x060006D1 RID: 1745 RVA: 0x000377E5 File Offset: 0x000359E5
		internal IModifyTable GetRuleTable()
		{
			return this.CoreFolder.GetRuleTable();
		}

		// Token: 0x060006D2 RID: 1746 RVA: 0x000377F2 File Offset: 0x000359F2
		internal IModifyTable GetRuleTable(IModifyTableRestriction modifyTableRestriction)
		{
			return this.CoreFolder.GetRuleTable(modifyTableRestriction);
		}

		// Token: 0x060006D3 RID: 1747 RVA: 0x00037800 File Offset: 0x00035A00
		internal static bool IsNotConfigurationFolder(ValidationContext context, IValidatablePropertyBag validatablePropertyBag)
		{
			VersionedId versionedId = validatablePropertyBag.TryGetProperty(InternalSchema.FolderId) as VersionedId;
			if (versionedId == null)
			{
				return true;
			}
			if (!context.Session.IsPublicFolderSession)
			{
				return !((MailboxSession)context.Session).InternalIsConfigurationFolder(versionedId.ObjectId);
			}
			return !((PublicFolderSession)context.Session).GetPublicFolderRootId().Equals(versionedId.ObjectId);
		}

		// Token: 0x060006D4 RID: 1748 RVA: 0x00037868 File Offset: 0x00035A68
		internal static bool DoesFolderHaveFixedDisplayName(ValidationContext context, IValidatablePropertyBag validatablePropertyBag)
		{
			VersionedId versionedId = validatablePropertyBag.TryGetProperty(InternalSchema.FolderId) as VersionedId;
			MailboxSession mailboxSession = context.Session as MailboxSession;
			if (mailboxSession != null && versionedId != null && !mailboxSession.IsMoveUser)
			{
				DefaultFolderType defaultFolderType = mailboxSession.IsDefaultFolderType(versionedId);
				return defaultFolderType != DefaultFolderType.None && defaultFolderType != DefaultFolderType.QuickContacts;
			}
			return false;
		}

		// Token: 0x060006D5 RID: 1749 RVA: 0x000378B7 File Offset: 0x00035AB7
		internal QueryResult ItemQueryInternal(ItemQueryType queryFlags, QueryFilter queryFilter, SortBy[] sortColumns, QueryExclusionType queryExclusionType, ICollection<PropertyDefinition> dataColumns)
		{
			return this.CoreFolder.QueryExecutor.InternalItemQuery(QueryExecutor.ItemQueryTypeToContentsTableFlags(queryFlags), queryFilter, sortColumns, queryExclusionType, dataColumns, null);
		}

		// Token: 0x060006D6 RID: 1750 RVA: 0x000378D8 File Offset: 0x00035AD8
		internal static MapiFolder DeferBind(StoreSession storeSession, StoreId folderId)
		{
			if (!Folder.IsFolderId(folderId))
			{
				throw new ArgumentException(ServerStrings.InvalidFolderId(folderId.ToBase64String()));
			}
			MapiProp mapiProp = null;
			bool flag = false;
			MapiFolder result;
			try
			{
				object thisObject = null;
				bool flag2 = false;
				try
				{
					if (storeSession != null)
					{
						storeSession.BeginMapiCall();
						storeSession.BeginServerHealthCall();
						flag2 = true;
					}
					if (StorageGlobals.MapiTestHookBeforeCall != null)
					{
						StorageGlobals.MapiTestHookBeforeCall(MethodBase.GetCurrentMethod());
					}
					mapiProp = storeSession.GetMapiProp(StoreId.GetStoreObjectId(folderId));
				}
				catch (MapiPermanentException ex)
				{
					throw StorageGlobals.TranslateMapiException(ServerStrings.MapiCannotCreateMessage, ex, storeSession, thisObject, "{0}. MapiException = {1}.", new object[]
					{
						string.Format("Folder::DeferBind.", new object[0]),
						ex
					});
				}
				catch (MapiRetryableException ex2)
				{
					throw StorageGlobals.TranslateMapiException(ServerStrings.MapiCannotCreateMessage, ex2, storeSession, thisObject, "{0}. MapiException = {1}.", new object[]
					{
						string.Format("Folder::DeferBind.", new object[0]),
						ex2
					});
				}
				finally
				{
					try
					{
						if (storeSession != null)
						{
							storeSession.EndMapiCall();
							if (flag2)
							{
								storeSession.EndServerHealthCall();
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
				MapiFolder mapiFolder = (MapiFolder)mapiProp;
				flag = true;
				result = mapiFolder;
			}
			finally
			{
				if (!flag && mapiProp != null)
				{
					mapiProp.Dispose();
					mapiProp = null;
				}
			}
			return result;
		}

		// Token: 0x060006D7 RID: 1751 RVA: 0x00037A7C File Offset: 0x00035C7C
		internal static T InternalBind<T>(StoreSession storeSession, MapiFolder mapiFolder, StoreObjectId folderObjectId, byte[] changeKey, PropertyDefinition[] propsToReturn) where T : Folder
		{
			return Folder.InternalBind<T>(() => CoreFolder.InternalBind(storeSession, mapiFolder, folderObjectId, changeKey, propsToReturn));
		}

		// Token: 0x060006D8 RID: 1752 RVA: 0x00037AE8 File Offset: 0x00035CE8
		internal static T InternalBind<T>(StoreSession storeSession, StoreId folderId, ICollection<PropertyDefinition> propsToReturn) where T : Folder
		{
			return Folder.InternalBind<T>(() => CoreFolder.InternalBind(storeSession, folderId, false, propsToReturn));
		}

		// Token: 0x060006D9 RID: 1753 RVA: 0x00037B24 File Offset: 0x00035D24
		internal static bool IsFolderType(StoreObjectType type)
		{
			bool flag = type == StoreObjectType.ShortcutFolder;
			return (type != StoreObjectType.Unknown && type <= StoreObjectType.OutlookSearchFolder) || flag;
		}

		// Token: 0x060006DA RID: 1754 RVA: 0x00037B6C File Offset: 0x00035D6C
		internal MessageItem CreateMessageInternal(CreateMessageType type)
		{
			EnumValidator.AssertValid<CreateMessageType>(type);
			ItemCreateInfo messageItemInfo = ItemCreateInfo.MessageItemInfo;
			return ItemBuilder.CreateNewItem<MessageItem>(base.Session, messageItemInfo, () => Folder.InternalCreateMapiMessage(this.MapiFolder, type, this.Session));
		}

		// Token: 0x060006DB RID: 1755 RVA: 0x00037BB8 File Offset: 0x00035DB8
		internal static MapiMessage InternalCreateMapiMessage(StoreSession store, StoreId folderId, CreateMessageType itemType)
		{
			PublicFolderSession publicFolderSession = store as PublicFolderSession;
			if (publicFolderSession != null)
			{
				using (CoreFolder coreFolder = CoreFolder.Bind(store, folderId, new PropertyDefinition[]
				{
					CoreFolderSchema.ReplicaList
				}))
				{
					using (MapiFolder mapiFolder = (MapiFolder)Microsoft.Exchange.Data.Storage.CoreObject.GetPersistablePropertyBag(coreFolder).MapiProp)
					{
						return Folder.InternalCreateMapiMessage(mapiFolder, itemType, store);
					}
				}
			}
			MapiMessage result;
			using (MapiFolder mapiFolder2 = Folder.DeferBind(store, folderId))
			{
				result = Folder.InternalCreateMapiMessage(mapiFolder2, itemType, store);
			}
			return result;
		}

		// Token: 0x060006DC RID: 1756 RVA: 0x00037C9C File Offset: 0x00035E9C
		internal MessageItem CreateMessageForDeliveryInternal(CreateMessageType type, string internetMessageId, ExDateTime? clientSubmitTime)
		{
			EnumValidator.AssertValid<CreateMessageType>(type);
			ItemCreateInfo messageItemInfo = ItemCreateInfo.MessageItemInfo;
			return ItemBuilder.CreateNewItem<MessageItem>(base.Session, messageItemInfo, () => Folder.InternalCreateMapiMessageForDelivery(this.MapiFolder, type, this.Session, internetMessageId, clientSubmitTime));
		}

		// Token: 0x060006DD RID: 1757 RVA: 0x00037CF4 File Offset: 0x00035EF4
		internal static MapiMessage InternalCreateMapiMessageForDelivery(StoreSession store, StoreId folderId, CreateMessageType itemType, string internetMessageId, ExDateTime? clientSubmitTime)
		{
			bool flag = false;
			MapiMessage mapiMessage = null;
			try
			{
				using (MapiFolder mapiFolder = Folder.DeferBind(store, folderId))
				{
					mapiMessage = Folder.InternalCreateMapiMessageForDelivery(mapiFolder, itemType, store, internetMessageId, clientSubmitTime);
				}
				flag = true;
			}
			finally
			{
				if (!flag && mapiMessage != null)
				{
					mapiMessage.Dispose();
					mapiMessage = null;
				}
			}
			return mapiMessage;
		}

		// Token: 0x060006DE RID: 1758 RVA: 0x00037D58 File Offset: 0x00035F58
		internal virtual PermissionSet CreatePermissionSet(PermissionTable permissionTable)
		{
			return new PermissionSet(permissionTable);
		}

		// Token: 0x060006DF RID: 1759 RVA: 0x00037D60 File Offset: 0x00035F60
		internal void InternalSetReadFlags(bool suppressReceipts, bool markAsRead, params StoreId[] itemIds)
		{
			this.InternalSetReadFlags(suppressReceipts, markAsRead, itemIds, false, false);
		}

		// Token: 0x060006E0 RID: 1760 RVA: 0x00037D6D File Offset: 0x00035F6D
		internal void InternalSetReadFlags(bool suppressReceipts, bool markAsRead, bool suppressNotReadReceipts, params StoreId[] itemIds)
		{
			this.InternalSetReadFlags(suppressReceipts, markAsRead, itemIds, false, suppressNotReadReceipts);
		}

		// Token: 0x060006E1 RID: 1761 RVA: 0x00037D84 File Offset: 0x00035F84
		internal void InternalSetReadFlags(bool suppressReceipts, bool markAsRead, StoreId[] itemIds, bool throwIfWarning, bool suppressNotReadReceipts = false)
		{
			SetReadFlags setReadFlags = markAsRead ? SetReadFlags.None : SetReadFlags.ClearRead;
			if (suppressReceipts)
			{
				if (markAsRead)
				{
					setReadFlags |= SetReadFlags.SuppressReceipt;
				}
				if (!markAsRead || suppressNotReadReceipts)
				{
					this.CoreFolder.InternalSetReadFlags(SetReadFlags.ClearRnPending | SetReadFlags.CleanNrnPending, itemIds, throwIfWarning);
				}
			}
			this.CoreFolder.InternalSetReadFlags(setReadFlags, itemIds, throwIfWarning);
			ICollection<StoreObjectId> itemIds2 = (from id in itemIds
			select StoreId.GetStoreObjectId(id)).ToArray<StoreObjectId>();
			if (base.Session.ActivitySession != null)
			{
				if (markAsRead)
				{
					base.Session.ActivitySession.CaptureMarkAsRead(itemIds2);
					return;
				}
				base.Session.ActivitySession.CaptureMarkAsUnread(itemIds2);
			}
		}

		// Token: 0x060006E2 RID: 1762 RVA: 0x00037E28 File Offset: 0x00036028
		internal StoreObjectId FindChildFolderByName(string childFolderName)
		{
			StoreObjectId result;
			using (QueryResult queryResult = this.FolderQuery(FolderQueryFlags.None, null, null, new PropertyDefinition[]
			{
				FolderSchema.Id,
				FolderSchema.DisplayName
			}))
			{
				queryResult.SeekToCondition(SeekReference.OriginBeginning, new ComparisonFilter(ComparisonOperator.Equal, FolderSchema.DisplayName, childFolderName));
				object[][] rows = queryResult.GetRows(1);
				if (rows.Length > 0)
				{
					result = ((VersionedId)rows[0][0]).ObjectId;
				}
				else
				{
					result = null;
				}
			}
			return result;
		}

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x060006E3 RID: 1763 RVA: 0x00037EAC File Offset: 0x000360AC
		// (set) Token: 0x060006E4 RID: 1764 RVA: 0x00037EB4 File Offset: 0x000360B4
		private PermissionTable PermissionTable
		{
			get
			{
				return this.permissionTable;
			}
			set
			{
				this.permissionTable = value;
			}
		}

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x060006E5 RID: 1765 RVA: 0x00037EBD File Offset: 0x000360BD
		public MapiFolder MapiFolder
		{
			get
			{
				this.CheckDisposed("MapiFolder::get");
				return (MapiFolder)base.MapiProp;
			}
		}

		// Token: 0x17000145 RID: 325
		// (get) Token: 0x060006E6 RID: 1766 RVA: 0x00037ED5 File Offset: 0x000360D5
		private StoreObjectPropertyBag FolderPropertyBag
		{
			get
			{
				return (StoreObjectPropertyBag)PersistablePropertyBag.GetPersistablePropertyBag(this.CoreFolder.PropertyBag);
			}
		}

		// Token: 0x060006E7 RID: 1767 RVA: 0x00037F1C File Offset: 0x0003611C
		private AggregateOperationResult InternalDeleteObjects(DeleteItemFlags flags, StoreId[] sourceObjectIds)
		{
			this.CheckDisposed("InternalDeleteObjects");
			EnumValidator.AssertValid<DeleteItemFlags>(flags);
			if (sourceObjectIds == null)
			{
				throw new ArgumentNullException("sourceObjectIds");
			}
			return Folder.ExecuteOperationOnObjects((StoreObjectId sourceFolderId) => this.InternalDeleteFolder(flags, sourceFolderId), (StoreObjectId[] sourceItemIds) => this.InternalDeleteItems(flags, sourceItemIds), sourceObjectIds);
		}

		// Token: 0x060006E8 RID: 1768 RVA: 0x00037F88 File Offset: 0x00036188
		internal GroupOperationResult InternalDeleteItems(DeleteItemFlags flags, StoreObjectId[] ids)
		{
			this.CheckDisposed("InternalDeleteItems");
			EnumValidator.AssertValid<DeleteItemFlags>(flags);
			this.CoreFolder.ValidateItemIds(ids, delegate(StoreObjectId storeObjectId)
			{
				this.CheckItemBelongsToThisFolder(storeObjectId);
			});
			return this.CoreFolder.DeleteItems(flags, ids);
		}

		// Token: 0x060006E9 RID: 1769 RVA: 0x00037FC0 File Offset: 0x000361C0
		private GroupOperationResult InternalDeleteFolder(DeleteItemFlags flags, StoreObjectId folderId)
		{
			this.CheckDisposed("InternalDeleteFolder");
			EnumValidator.AssertValid<DeleteItemFlags>(flags);
			this.CoreFolder.CheckIsNotDefaultFolder(folderId, Folder.canDeleteDefaultFolders);
			return this.CoreFolder.DeleteFolder(flags, folderId);
		}

		// Token: 0x060006EA RID: 1770 RVA: 0x00037FF1 File Offset: 0x000361F1
		internal static bool CanDeleteFolder(StoreSession session, StoreObjectId folderId)
		{
			return CoreFolder.CheckIsNotDefaultFolder(session, folderId, Folder.canDeleteDefaultFolders);
		}

		// Token: 0x060006EB RID: 1771 RVA: 0x00038000 File Offset: 0x00036200
		internal static void ExecuteGroupOperationAndAggregateResults(List<GroupOperationResult> groupOperationResults, StoreObjectId[] sourceObjectIds, Folder.GroupOperationDelegate groupOperationDelegate)
		{
			GroupOperationResult item;
			try
			{
				item = groupOperationDelegate();
			}
			catch (StoragePermanentException storageException)
			{
				item = new GroupOperationResult(OperationResult.Failed, sourceObjectIds, storageException);
			}
			catch (StorageTransientException storageException2)
			{
				item = new GroupOperationResult(OperationResult.Failed, sourceObjectIds, storageException2);
			}
			groupOperationResults.Add(item);
		}

		// Token: 0x060006EC RID: 1772 RVA: 0x00038054 File Offset: 0x00036254
		internal static AggregateOperationResult CreateAggregateOperationResult(List<GroupOperationResult> groupOperationResults)
		{
			int num = 0;
			int num2 = 0;
			bool flag = false;
			foreach (GroupOperationResult groupOperationResult in groupOperationResults)
			{
				if (groupOperationResult.OperationResult == OperationResult.Succeeded)
				{
					num++;
				}
				else
				{
					if (groupOperationResult.OperationResult != OperationResult.Failed)
					{
						flag = true;
						break;
					}
					num2++;
				}
			}
			OperationResult operationResult;
			if (num == groupOperationResults.Count)
			{
				operationResult = OperationResult.Succeeded;
			}
			else if (flag || num > 0)
			{
				operationResult = OperationResult.PartiallySucceeded;
			}
			else
			{
				operationResult = OperationResult.Failed;
			}
			return new AggregateOperationResult(operationResult, groupOperationResults.ToArray());
		}

		// Token: 0x060006ED RID: 1773 RVA: 0x000380F0 File Offset: 0x000362F0
		internal static void GroupOccurrencesAndObjectIds(StoreId[] sourceObjectIds, List<StoreId> sourceObjectIdList, List<OccurrenceStoreObjectId> sourceOccurrenceIdList)
		{
			for (int i = 0; i < sourceObjectIds.Length; i++)
			{
				if (sourceObjectIds[i] == null)
				{
					throw new ArgumentException(ServerStrings.ExNullItemIdParameter(0));
				}
				OccurrenceStoreObjectId occurrenceStoreObjectId = StoreId.GetStoreObjectId(sourceObjectIds[i]) as OccurrenceStoreObjectId;
				if (occurrenceStoreObjectId != null)
				{
					sourceOccurrenceIdList.Add(occurrenceStoreObjectId);
				}
				else
				{
					sourceObjectIdList.Add(sourceObjectIds[i]);
				}
			}
		}

		// Token: 0x060006EE RID: 1774 RVA: 0x00038144 File Offset: 0x00036344
		internal static void CoreObjectUpdateRetentionProperties(CoreFolder coreFolder)
		{
			MailboxSession mailboxSession = coreFolder.Session as MailboxSession;
			if (mailboxSession == null || COWSettings.IsMrmAction(mailboxSession))
			{
				return;
			}
			PolicyTagHelper.ApplyPolicyToFolder(mailboxSession, coreFolder);
		}

		// Token: 0x060006EF RID: 1775 RVA: 0x00038170 File Offset: 0x00036370
		internal GroupOperationResult CopyItems(Folder destinationFolder, StoreObjectId[] sourceItemIds, PropertyDefinition[] propertyDefinitions, object[] propertyValues)
		{
			return this.CopyItems(destinationFolder, sourceItemIds, propertyDefinitions, propertyValues, false);
		}

		// Token: 0x060006F0 RID: 1776 RVA: 0x00038187 File Offset: 0x00036387
		internal GroupOperationResult CopyItems(Folder destinationFolder, StoreObjectId[] sourceItemIds, PropertyDefinition[] propertyDefinitions, object[] propertyValues, bool returnNewIds)
		{
			this.CheckDisposed("CopyItems");
			return this.CoreFolder.CopyItems(destinationFolder.CoreFolder, sourceItemIds, propertyDefinitions, propertyValues, delegate(StoreObjectId storeObjectId)
			{
				this.CheckItemBelongsToThisFolder(storeObjectId);
			}, returnNewIds);
		}

		// Token: 0x060006F1 RID: 1777 RVA: 0x000381B7 File Offset: 0x000363B7
		internal GroupOperationResult MoveItems(Folder destinationFolder, StoreObjectId[] sourceItemIds, PropertyDefinition[] propertyDefinitions, object[] propertyValues)
		{
			return this.MoveItems(destinationFolder, sourceItemIds, propertyDefinitions, propertyValues, false);
		}

		// Token: 0x060006F2 RID: 1778 RVA: 0x000381C8 File Offset: 0x000363C8
		public GroupOperationResult MoveItems(Folder destinationFolder, StoreObjectId[] sourceItemIds, PropertyDefinition[] propertyDefinitions, object[] propertyValues, bool returnNewIds)
		{
			return this.MoveItems(destinationFolder, sourceItemIds, propertyDefinitions, propertyValues, returnNewIds, null);
		}

		// Token: 0x060006F3 RID: 1779 RVA: 0x000381F4 File Offset: 0x000363F4
		public GroupOperationResult MoveItems(Folder destinationFolder, StoreObjectId[] sourceItemIds, PropertyDefinition[] propertyDefinitions, object[] propertyValues, bool returnNewIds, DeleteItemFlags? deleteFlags)
		{
			this.CheckDisposed("MoveItems");
			Util.ThrowOnNullArgument(destinationFolder, "destinationFolder");
			return this.CoreFolder.MoveItems(destinationFolder.CoreFolder, sourceItemIds, propertyDefinitions, propertyValues, delegate(StoreObjectId storeObjectId)
			{
				this.CheckItemBelongsToThisFolder(storeObjectId);
			}, returnNewIds, deleteFlags);
		}

		// Token: 0x060006F4 RID: 1780 RVA: 0x00038231 File Offset: 0x00036431
		public bool IsContentAvailable()
		{
			this.CheckDisposed("IsContentAvailable");
			return this.CoreFolder.IsContentAvailable();
		}

		// Token: 0x060006F5 RID: 1781 RVA: 0x00038249 File Offset: 0x00036449
		internal static bool CanDeleteDefaultFolder(DefaultFolderType defaultFolderType)
		{
			return ((IList)Folder.canDeleteDefaultFolders).Contains(defaultFolderType);
		}

		// Token: 0x060006F6 RID: 1782 RVA: 0x0003825B File Offset: 0x0003645B
		internal static byte[] GetFolderProviderLevelItemId(StoreObjectId sourceFolderId)
		{
			if (!IdConverter.IsFolderId(sourceFolderId))
			{
				throw new ArgumentException(ServerStrings.ExInvalidFolderId, "sourceFolderId");
			}
			if (sourceFolderId is OccurrenceStoreObjectId)
			{
				throw new ArgumentException(ServerStrings.ExCannotMoveOrCopyOccurrenceItem(sourceFolderId));
			}
			return sourceFolderId.ProviderLevelItemId;
		}

		// Token: 0x060006F7 RID: 1783 RVA: 0x0003829C File Offset: 0x0003649C
		internal static StoreObjectId[] StoreIdsToStoreObjectIds(StoreId[] storeIds)
		{
			StoreObjectId[] array = new StoreObjectId[storeIds.Length];
			for (int i = 0; i < storeIds.Length; i++)
			{
				array[i] = StoreId.GetStoreObjectId(storeIds[i]);
			}
			return array;
		}

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x060006F8 RID: 1784 RVA: 0x000382CC File Offset: 0x000364CC
		// (set) Token: 0x060006F9 RID: 1785 RVA: 0x000382D3 File Offset: 0x000364D3
		internal static DefaultFolderType[] CanDeleteDefaultFolders
		{
			get
			{
				return Folder.canDeleteDefaultFolders;
			}
			set
			{
				Folder.canDeleteDefaultFolders = value;
			}
		}

		// Token: 0x060006FA RID: 1786 RVA: 0x000382DC File Offset: 0x000364DC
		protected virtual void CheckItemBelongsToThisFolder(StoreObjectId storeObjectId)
		{
			byte[] providerLevelItemId = base.Session.IdConverter.GetSessionSpecificId(base.StoreObjectId).ProviderLevelItemId;
			byte[] providerLevelItemId2 = base.Session.IdConverter.GetSessionSpecificId(base.Session.GetParentFolderId(storeObjectId)).ProviderLevelItemId;
			if (!ArrayComparer<byte>.Comparer.Equals(providerLevelItemId, providerLevelItemId2))
			{
				throw new ArgumentException(ServerStrings.ExItemDoesNotBelongToCurrentFolder(storeObjectId));
			}
		}

		// Token: 0x060006FB RID: 1787 RVA: 0x00038346 File Offset: 0x00036546
		protected bool ExtendedFlagsContains(ExtendedFolderFlags flag)
		{
			return Folder.ExtendedFlagsContains(base.GetValueAsNullable<int>(FolderSchema.ExtendedFolderFlags), flag);
		}

		// Token: 0x060006FC RID: 1788 RVA: 0x00038359 File Offset: 0x00036559
		public PublicFolderContentMailboxInfo GetContentMailboxInfo()
		{
			return this.CoreFolder.GetContentMailboxInfo();
		}

		// Token: 0x040001D5 RID: 469
		private const int MaxDeleteItemCount = 200;

		// Token: 0x040001D6 RID: 470
		public const int MaxRows = 10000;

		// Token: 0x040001D7 RID: 471
		private static readonly StoreObjectType[] FolderTypes = new StoreObjectType[]
		{
			StoreObjectType.SearchFolder,
			StoreObjectType.OutlookSearchFolder,
			StoreObjectType.CalendarFolder,
			StoreObjectType.ContactsFolder,
			StoreObjectType.TasksFolder,
			StoreObjectType.JournalFolder,
			StoreObjectType.NotesFolder,
			StoreObjectType.ShortcutFolder,
			StoreObjectType.Folder
		};

		// Token: 0x040001D8 RID: 472
		private static readonly QueryFilter DefaultClutterFilter = new ComparisonFilter(ComparisonOperator.Equal, ItemSchema.IsClutter, true);

		// Token: 0x040001D9 RID: 473
		private static readonly QueryFilter DefaultNoClutterFilter = new ComparisonFilter(ComparisonOperator.NotEqual, ItemSchema.IsClutter, true);

		// Token: 0x040001DA RID: 474
		private static DefaultFolderType[] canDeleteDefaultFolders = new DefaultFolderType[]
		{
			DefaultFolderType.ElcRoot,
			DefaultFolderType.UMVoicemail,
			DefaultFolderType.UMFax,
			DefaultFolderType.Drafts,
			DefaultFolderType.JunkEmail,
			DefaultFolderType.Tasks,
			DefaultFolderType.RecipientCache,
			DefaultFolderType.QuickContacts,
			DefaultFolderType.ImContactList,
			DefaultFolderType.OrganizationalContacts,
			DefaultFolderType.PushNotificationRoot,
			DefaultFolderType.BirthdayCalendar
		};

		// Token: 0x040001DB RID: 475
		private PermissionTable permissionTable;

		// Token: 0x02000058 RID: 88
		// (Invoke) Token: 0x06000703 RID: 1795
		private delegate CoreFolder CoreFolderBindDelegate();

		// Token: 0x02000059 RID: 89
		// (Invoke) Token: 0x06000707 RID: 1799
		internal delegate GroupOperationResult GroupOperationDelegate();

		// Token: 0x0200005A RID: 90
		// (Invoke) Token: 0x0600070B RID: 1803
		private delegate GroupOperationResult ActOnItemsDelegate(StoreObjectId[] sourceItemIds);

		// Token: 0x0200005B RID: 91
		// (Invoke) Token: 0x0600070F RID: 1807
		private delegate GroupOperationResult ActOnFolderDelegate(StoreObjectId sourceFolderId);
	}
}
