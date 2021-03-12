using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.Services.Core;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000A43 RID: 2627
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange", Name = "FavoriteFolderCollection")]
	public class FavoriteFolderCollection
	{
		// Token: 0x06004A38 RID: 19000 RVA: 0x001036D6 File Offset: 0x001018D6
		private FavoriteFolderCollection(MailboxSession session, FolderTreeDataSection folderTreeSection)
		{
			this.favoriteFolders = new List<FavoriteFolderNode>();
			this.distinctFolderIds = new List<string>();
			this.session = session;
			this.folderTreeSection = folderTreeSection;
		}

		// Token: 0x170010BE RID: 4286
		// (get) Token: 0x06004A39 RID: 19001 RVA: 0x00103702 File Offset: 0x00101902
		[DataMember]
		public BaseFolderType[] FavoriteFolders
		{
			get
			{
				return this.GetFolderList();
			}
		}

		// Token: 0x06004A3A RID: 19002 RVA: 0x0010370C File Offset: 0x0010190C
		internal static FavoriteFolderCollection GetFavoritesCollection(MailboxSession session, FolderTreeDataSection folderTreeSection)
		{
			FavoriteFolderCollection favoriteFolderCollection = new FavoriteFolderCollection(session, folderTreeSection);
			favoriteFolderCollection.LoadCommonViewAssociatedMessages();
			return favoriteFolderCollection;
		}

		// Token: 0x06004A3B RID: 19003 RVA: 0x00103728 File Offset: 0x00101928
		internal bool InitializeDefaultFavorites()
		{
			if (this.favoriteFolders.Count > 0)
			{
				return false;
			}
			byte[] array = null;
			foreach (DefaultFolderType defaultFolderType in FavoriteFolderCollection.favoriteDefaultFolders)
			{
				using (Folder folder = Folder.Bind(this.session, defaultFolderType, FavoriteFolderCollection.folderProperties))
				{
					this.AppendFavoriteFolder(folder, ref array);
				}
			}
			return true;
		}

		// Token: 0x06004A3C RID: 19004 RVA: 0x001037A0 File Offset: 0x001019A0
		internal UpdateFavoriteFolderResponse AddFavoriteFolder(BaseFolderType folder, FolderId targetFolderId, MoveFavoriteFolderType? moveType, IdConverter idConverter)
		{
			if (targetFolderId != null && moveType == null)
			{
				ExTraceGlobals.UpdateFavoriteFolderCallTracer.TraceError((long)this.GetHashCode(), CoreResources.UpdateFavoritesMoveTypeMustBeSet);
				return new UpdateFavoriteFolderResponse(CoreResources.UpdateFavoritesMoveTypeMustBeSet);
			}
			FavoriteFolderNode favoriteFolderNode = this.FindFavoriteFolderNodeFromFolderId(folder.FolderId);
			if (favoriteFolderNode != null)
			{
				string text = string.Format(CoreResources.UpdateFavoritesFolderAlreadyInFavorites, folder.FolderId.Id);
				ExTraceGlobals.UpdateFavoriteFolderCallTracer.TraceError((long)this.GetHashCode(), text);
				return new UpdateFavoriteFolderResponse(text);
			}
			FolderTreeDataType folderTreeDataType = (folder is SearchFolderType) ? FolderTreeDataType.SmartFolder : FolderTreeDataType.NormalFolder;
			StoreObjectId storeObjectId = StoreId.EwsIdToFolderStoreObjectId(folder.FolderId.Id);
			if (storeObjectId.IsLegacyPublicFolderType())
			{
				this.CreateShortcutMessageIfAbsent(folder, true);
				StoreObjectId defaultFolderId = this.session.GetDefaultFolderId(DefaultFolderType.LegacyShortcuts);
				using (Folder folder2 = Folder.Create(this.session, defaultFolderId, StoreObjectType.Folder, folder.DisplayName, CreateMode.OpenIfExists))
				{
					folder2[StoreObjectSchema.ContainerClass] = "IPF.Note";
					folder2.Save();
				}
				storeObjectId = StoreObjectId.ToLegacyFavoritePublicFolderId(storeObjectId);
			}
			FavoriteFolderEntry favoriteFolderEntry = FavoriteFolderCollection.CreateFavoriteFolderEntryBasedOnFolderId(this.session, folder.FolderId, storeObjectId, folderTreeDataType);
			favoriteFolderEntry.FolderDisplayName = folder.DisplayName;
			this.SetOrdinalValue(favoriteFolderEntry, targetFolderId, moveType);
			if (this.SaveFavoriteMessage(favoriteFolderEntry))
			{
				return new UpdateFavoriteFolderResponse
				{
					WasSuccessful = true
				};
			}
			return new UpdateFavoriteFolderResponse(string.Format(CoreResources.UpdateFavoritesUnableToAddFolderToFavorites, folder.FolderId.Id));
		}

		// Token: 0x06004A3D RID: 19005 RVA: 0x00103924 File Offset: 0x00101B24
		internal UpdateFavoriteFolderResponse RemoveFavoriteFolder(BaseFolderType folder)
		{
			FavoriteFolderNode favoriteFolderNode = this.FindFavoriteFolderNodeFromFolderId(folder.FolderId);
			if (favoriteFolderNode == null)
			{
				string text = string.Format(CoreResources.UpdateFavoritesFavoriteNotFound, folder.FolderId.Id);
				ExTraceGlobals.UpdateFavoriteFolderCallTracer.TraceError((long)this.GetHashCode(), text);
				return new UpdateFavoriteFolderResponse(text);
			}
			AggregateOperationResult aggregateOperationResult = this.session.Delete(DeleteItemFlags.HardDelete, new StoreId[]
			{
				favoriteFolderNode.NavigationNodeId
			});
			if (aggregateOperationResult.OperationResult != OperationResult.Succeeded)
			{
				string text2 = string.Format(CoreResources.UpdateFavoritesUnableToDeleteFavoriteEntry, folder.FolderId.Id, favoriteFolderNode.NavigationNodeId, aggregateOperationResult.OperationResult);
				ExTraceGlobals.UpdateFavoriteFolderCallTracer.TraceError((long)this.GetHashCode(), text2);
				return new UpdateFavoriteFolderResponse(text2);
			}
			return new UpdateFavoriteFolderResponse
			{
				WasSuccessful = true
			};
		}

		// Token: 0x06004A3E RID: 19006 RVA: 0x001039F8 File Offset: 0x00101BF8
		internal UpdateFavoriteFolderResponse MoveFavoriteFolder(BaseFolderType folder, FolderId targetFolderId, MoveFavoriteFolderType? moveType)
		{
			if (targetFolderId == null && moveType == null)
			{
				ExTraceGlobals.UpdateFavoriteFolderCallTracer.TraceError((long)this.GetHashCode(), CoreResources.UpdateFavoritesInvalidMoveFavoriteRequest);
				return new UpdateFavoriteFolderResponse(CoreResources.UpdateFavoritesInvalidMoveFavoriteRequest);
			}
			FavoriteFolderNode favoriteFolderNode = this.FindFavoriteFolderNodeFromFolderId(folder.FolderId);
			if (favoriteFolderNode != null)
			{
				using (FavoriteFolderEntry favoriteFolderEntry = FavoriteFolderEntry.Bind(this.session, favoriteFolderNode.NavigationNodeId))
				{
					favoriteFolderEntry.FolderDisplayName = folder.DisplayName;
					this.SetOrdinalValue(favoriteFolderEntry, targetFolderId, moveType);
					if (this.SaveFavoriteMessage(favoriteFolderEntry))
					{
						return new UpdateFavoriteFolderResponse
						{
							WasSuccessful = true
						};
					}
					return new UpdateFavoriteFolderResponse(string.Format(CoreResources.UpdateFavoritesUnableToMoveFavorite, folder.FolderId.Id));
				}
			}
			string text = string.Format(CoreResources.UpdateFavoritesFavoriteNotFound, folder.FolderId.Id);
			ExTraceGlobals.UpdateFavoriteFolderCallTracer.TraceError((long)this.GetHashCode(), text);
			return new UpdateFavoriteFolderResponse(text);
		}

		// Token: 0x06004A3F RID: 19007 RVA: 0x00103B04 File Offset: 0x00101D04
		internal UpdateFavoriteFolderResponse RenameFavoriteFolder(BaseFolderType folder)
		{
			FavoriteFolderNode favoriteFolderNode = this.FindFavoriteFolderNodeFromFolderId(folder.FolderId);
			if (favoriteFolderNode != null)
			{
				if (folder.FolderId.IsPublicFolderId())
				{
					StoreObjectId storeId = this.CreateShortcutMessageIfAbsent(folder, false);
					string shortcutFolderName = null;
					using (ShortcutMessage shortcutMessage = ShortcutMessage.Bind(this.session, storeId, new PropertyDefinition[]
					{
						ShortcutMessageSchema.FavoriteDisplayAlias,
						ShortcutMessageSchema.FavoriteDisplayName
					}))
					{
						shortcutMessage.OpenAsReadWrite();
						if (shortcutMessage.FavoriteDisplayName.Equals(string.Empty))
						{
							shortcutFolderName = shortcutMessage.FavoriteDisplayAlias;
						}
						else
						{
							shortcutFolderName = shortcutMessage.FavoriteDisplayName;
							shortcutMessage.FavoriteDisplayName = string.Empty;
						}
						shortcutMessage.FavoriteDisplayAlias = folder.DisplayName;
						shortcutMessage.Save(SaveMode.ResolveConflicts);
					}
					StoreObjectId storeObjectId = this.FindShortcutFolderUsingName(shortcutFolderName);
					if (storeObjectId != null)
					{
						Folder folder2 = Folder.Bind(this.session, storeObjectId);
						folder2.DisplayName = folder.DisplayName;
						folder2.Save();
					}
				}
				using (FavoriteFolderEntry favoriteFolderEntry = FavoriteFolderEntry.Bind(this.session, favoriteFolderNode.NavigationNodeId))
				{
					favoriteFolderEntry.FolderDisplayName = folder.DisplayName;
					if (this.SaveFavoriteMessage(favoriteFolderEntry))
					{
						return new UpdateFavoriteFolderResponse
						{
							WasSuccessful = true
						};
					}
					return new UpdateFavoriteFolderResponse(string.Format(CoreResources.UpdateFavoritesUnableToRenameFavorite, folder.FolderId.Id));
				}
			}
			string text = string.Format(CoreResources.UpdateFavoritesFavoriteNotFound, folder.FolderId.Id);
			ExTraceGlobals.UpdateFavoriteFolderCallTracer.TraceError((long)this.GetHashCode(), text);
			return new UpdateFavoriteFolderResponse(text);
		}

		// Token: 0x06004A40 RID: 19008 RVA: 0x00103CB0 File Offset: 0x00101EB0
		private bool AppendFavoriteFolder(Folder folder, ref byte[] lastNodeOrdinal)
		{
			FolderTreeDataType dataType = (folder is SearchFolder) ? FolderTreeDataType.SmartFolder : FolderTreeDataType.NormalFolder;
			FavoriteFolderEntry favoriteFolderEntry = FavoriteFolderEntry.Create(this.session, folder.Id.ObjectId, dataType);
			favoriteFolderEntry.FolderDisplayName = folder.DisplayName;
			favoriteFolderEntry.SetNodeOrdinal(lastNodeOrdinal, null);
			lastNodeOrdinal = favoriteFolderEntry.NodeOrdinal;
			return this.SaveFavoriteMessage(favoriteFolderEntry);
		}

		// Token: 0x06004A41 RID: 19009 RVA: 0x00103D08 File Offset: 0x00101F08
		private void LoadCommonViewAssociatedMessages()
		{
			using (Folder folder = Folder.Bind(this.session, DefaultFolderType.CommonViews))
			{
				using (QueryResult queryResult = folder.ItemQuery(ItemQueryType.Associated, null, FavoriteFolderCollection.SortByWhenQuerying, FavoriteFolderCollection.AllProperties))
				{
					this.FetchRowsAndProcessFavorites(queryResult, 10000);
				}
			}
		}

		// Token: 0x06004A42 RID: 19010 RVA: 0x00103D78 File Offset: 0x00101F78
		private void FetchRowsAndProcessFavorites(QueryResult queryResult, int rowCount)
		{
			int num = 0;
			object[][] rows;
			do
			{
				rows = queryResult.GetRows(rowCount - num);
				this.ProcessFavoritesList(rows);
				num += rows.Length;
			}
			while (rows.Length > 0 && num < rowCount);
		}

		// Token: 0x06004A43 RID: 19011 RVA: 0x00103DAC File Offset: 0x00101FAC
		private void ProcessFavoritesList(object[][] rows)
		{
			for (int i = 0; i < rows.Length; i++)
			{
				string itemPropertyOrDefault = Util.GetItemPropertyOrDefault<string>(rows[i], StoreObjectSchema.ItemClass, string.Empty, FavoriteFolderCollection.PropertyMap);
				if (ObjectClass.IsFolderTreeData(itemPropertyOrDefault))
				{
					if (this.folderTreeSection != FolderTreeDataSection.None)
					{
						int itemPropertyOrDefault2 = Util.GetItemPropertyOrDefault<int>(rows[i], NavigationNodeSchema.GroupSection, -1, FavoriteFolderCollection.PropertyMap);
						if (itemPropertyOrDefault2 != (int)this.folderTreeSection)
						{
							goto IL_1D0;
						}
					}
					FolderType folderType;
					switch (Util.GetItemPropertyOrDefault<int>(rows[i], NavigationNodeSchema.Type, -1, FavoriteFolderCollection.PropertyMap))
					{
					case 0:
					case 2:
						folderType = new FolderType();
						break;
					case 1:
						folderType = new SearchFolderType();
						break;
					default:
						goto IL_1D0;
					}
					byte[] itemPropertyOrDefault3 = Util.GetItemPropertyOrDefault<byte[]>(rows[i], NavigationNodeSchema.NodeEntryId, null, FavoriteFolderCollection.PropertyMap);
					if (itemPropertyOrDefault3 != null)
					{
						try
						{
							StoreObjectId storeObjectId = StoreObjectId.FromProviderSpecificId(itemPropertyOrDefault3);
							if (storeObjectId.IsLegacyPublicFolderType())
							{
								storeObjectId = StoreObjectId.FromLegacyFavoritePublicFolderId(storeObjectId);
								ConcatenatedIdAndChangeKey concatenatedIdForPublicFolder = IdConverter.GetConcatenatedIdForPublicFolder(storeObjectId);
								folderType.FolderId = new FolderId(concatenatedIdForPublicFolder.Id, concatenatedIdForPublicFolder.ChangeKey);
							}
							else
							{
								folderType.FolderId = IdConverter.GetFolderIdFromStoreId(storeObjectId, new MailboxId(this.session));
							}
							if (this.distinctFolderIds.Contains(folderType.FolderId.Id))
							{
								goto IL_1D0;
							}
							this.distinctFolderIds.Add(folderType.FolderId.Id);
						}
						catch (CorruptDataException arg)
						{
							ExTraceGlobals.GetFavoritesCallTracer.TraceDebug<CorruptDataException, string>(0L, "GetFavorites: Exception {0} encountered trying to retrieve navigation node entry ID for folder entry id {1}", arg, Convert.ToBase64String(itemPropertyOrDefault3));
							goto IL_1D0;
						}
						folderType.DisplayName = Util.GetItemPropertyOrDefault<string>(rows[i], ItemSchema.Subject, string.Empty, FavoriteFolderCollection.PropertyMap);
						VersionedId itemPropertyOrDefault4 = Util.GetItemPropertyOrDefault<VersionedId>(rows[i], ItemSchema.Id, null, FavoriteFolderCollection.PropertyMap);
						byte[] itemPropertyOrDefault5 = Util.GetItemPropertyOrDefault<byte[]>(rows[i], NavigationNodeSchema.Ordinal, null, FavoriteFolderCollection.PropertyMap);
						FavoriteFolderNode item = new FavoriteFolderNode
						{
							NavigationNodeId = itemPropertyOrDefault4,
							NodeOrdinal = itemPropertyOrDefault5,
							Folder = folderType
						};
						this.favoriteFolders.Add(item);
					}
				}
				IL_1D0:;
			}
		}

		// Token: 0x06004A44 RID: 19012 RVA: 0x00103FA8 File Offset: 0x001021A8
		private BaseFolderType[] GetFolderList()
		{
			BaseFolderType[] array = new BaseFolderType[this.favoriteFolders.Count];
			for (int i = 0; i < this.favoriteFolders.Count; i++)
			{
				array[i] = this.favoriteFolders[i].Folder;
			}
			return array;
		}

		// Token: 0x06004A45 RID: 19013 RVA: 0x00103FF4 File Offset: 0x001021F4
		private FavoriteFolderNode FindFavoriteFolderNodeFromFolderId(FolderId folderId)
		{
			if (folderId == null)
			{
				return null;
			}
			for (int i = 0; i < this.favoriteFolders.Count; i++)
			{
				FavoriteFolderNode favoriteFolderNode = this.favoriteFolders[i];
				if (string.Equals(favoriteFolderNode.Folder.FolderId.Id, folderId.Id, StringComparison.Ordinal))
				{
					return favoriteFolderNode;
				}
			}
			return null;
		}

		// Token: 0x06004A46 RID: 19014 RVA: 0x0010404A File Offset: 0x0010224A
		private byte[] GetLastFolderNodeOrdinalInList()
		{
			if (this.favoriteFolders.Count == 0)
			{
				return null;
			}
			return this.favoriteFolders[this.favoriteFolders.Count - 1].NodeOrdinal;
		}

		// Token: 0x06004A47 RID: 19015 RVA: 0x00104078 File Offset: 0x00102278
		private byte[] GetNextFolderOrdinal(FolderId folderId)
		{
			int i = 0;
			while (i < this.favoriteFolders.Count)
			{
				if (string.Equals(this.favoriteFolders[i].Folder.FolderId.Id, folderId.Id, StringComparison.OrdinalIgnoreCase))
				{
					if (i == this.favoriteFolders.Count - 1)
					{
						return null;
					}
					return this.favoriteFolders[i + 1].NodeOrdinal;
				}
				else
				{
					i++;
				}
			}
			return null;
		}

		// Token: 0x06004A48 RID: 19016 RVA: 0x001040EC File Offset: 0x001022EC
		private byte[] GetPreviousFolderOrdinal(FolderId folderId)
		{
			int i = 0;
			while (i < this.favoriteFolders.Count)
			{
				if (string.Equals(this.favoriteFolders[i].Folder.FolderId.Id, folderId.Id, StringComparison.OrdinalIgnoreCase))
				{
					if (i == 0)
					{
						return null;
					}
					return this.favoriteFolders[i - 1].NodeOrdinal;
				}
				else
				{
					i++;
				}
			}
			return null;
		}

		// Token: 0x06004A49 RID: 19017 RVA: 0x00104154 File Offset: 0x00102354
		private void SetOrdinalValue(FavoriteFolderEntry message, FolderId targetFolderId, MoveFavoriteFolderType? moveType)
		{
			FavoriteFolderNode favoriteFolderNode = this.FindFavoriteFolderNodeFromFolderId(targetFolderId);
			if (favoriteFolderNode == null)
			{
				message.SetNodeOrdinal(this.GetLastFolderNodeOrdinalInList(), null);
				return;
			}
			if (moveType == MoveFavoriteFolderType.AfterTarget)
			{
				message.SetNodeOrdinal(favoriteFolderNode.NodeOrdinal, this.GetNextFolderOrdinal(targetFolderId));
				return;
			}
			if (moveType == MoveFavoriteFolderType.BeforeTarget)
			{
				message.SetNodeOrdinal(this.GetPreviousFolderOrdinal(targetFolderId), favoriteFolderNode.NodeOrdinal);
			}
		}

		// Token: 0x06004A4A RID: 19018 RVA: 0x001041D0 File Offset: 0x001023D0
		private bool SaveFavoriteMessage(FavoriteFolderEntry message)
		{
			bool result = true;
			try
			{
				message.Save(SaveMode.NoConflictResolution);
			}
			catch (StorageTransientException ex)
			{
				ExTraceGlobals.UpdateFavoriteFolderCallTracer.TraceDebug<string>((long)this.GetHashCode(), "FavoriteFolderCollection.SaveFavoriteMessage. Unable to save favorite message. Exception: {0}.", ex.Message);
				result = false;
			}
			catch (StoragePermanentException ex2)
			{
				ExTraceGlobals.UpdateFavoriteFolderCallTracer.TraceDebug<string>((long)this.GetHashCode(), "FavoriteFolderCollection.SaveFavoriteMessage. Unable to save favorite message. Exception: {0}.", ex2.Message);
				result = false;
			}
			finally
			{
				message.Dispose();
			}
			return result;
		}

		// Token: 0x06004A4B RID: 19019 RVA: 0x00104260 File Offset: 0x00102460
		private StoreObjectId CreateShortcutMessageIfAbsent(BaseFolderType folder, bool verifyDiplayName)
		{
			byte[] longTermFolderId = StoreId.EwsIdToFolderStoreObjectId(folder.FolderId.Id).LongTermFolderId;
			StoreObjectId result;
			using (Folder folder2 = Folder.Bind(this.session, DefaultFolderType.LegacyShortcuts))
			{
				QueryFilter queryFilter = new ComparisonFilter(ComparisonOperator.Equal, ShortcutMessageSchema.FavPublicSourceKey, longTermFolderId);
				if (verifyDiplayName)
				{
					queryFilter = new AndFilter(new QueryFilter[]
					{
						queryFilter,
						new ComparisonFilter(ComparisonOperator.Equal, ShortcutMessageSchema.FavoriteDisplayName, folder.DisplayName)
					});
				}
				queryFilter = new AndFilter(new QueryFilter[]
				{
					queryFilter,
					new ComparisonFilter(ComparisonOperator.Equal, ItemSchema.FavLevelMask, 1)
				});
				queryFilter = new AndFilter(new QueryFilter[]
				{
					queryFilter,
					new ComparisonFilter(ComparisonOperator.Equal, StoreObjectSchema.ItemClass, "IPM.Note")
				});
				using (QueryResult queryResult = folder2.ItemQuery(ItemQueryType.None, queryFilter, null, FavoriteFolderCollection.ShortcutMessageProperties))
				{
					StoreObjectId itemEntryIdIfValid = this.GetItemEntryIdIfValid(queryResult);
					if (itemEntryIdIfValid == null)
					{
						using (ShortcutMessage shortcutMessage = ShortcutMessage.Create(this.session, longTermFolderId, folder.DisplayName))
						{
							shortcutMessage.Save(SaveMode.ResolveConflicts);
							shortcutMessage.Load(new PropertyDefinition[]
							{
								ItemSchema.Id
							});
							return shortcutMessage.StoreObjectId;
						}
					}
					result = itemEntryIdIfValid;
				}
			}
			return result;
		}

		// Token: 0x06004A4C RID: 19020 RVA: 0x001043F0 File Offset: 0x001025F0
		private StoreObjectId FindShortcutFolderUsingName(string shortcutFolderName)
		{
			StoreObjectId storeObjectId;
			using (Folder folder = Folder.Bind(this.session, DefaultFolderType.LegacyShortcuts))
			{
				QueryFilter queryFilter = new ComparisonFilter(ComparisonOperator.Equal, FolderSchema.DisplayName, shortcutFolderName);
				QueryResult queryResult = folder.FolderQuery(FolderQueryFlags.None, queryFilter, null, new PropertyDefinition[]
				{
					FolderSchema.Id
				});
				object[][] rows = queryResult.GetRows(1);
				StoreId id = null;
				if (rows.Length > 0)
				{
					id = ((rows[0][0] is StoreId) ? ((StoreId)rows[0][0]) : null);
				}
				storeObjectId = StoreId.GetStoreObjectId(id);
			}
			return storeObjectId;
		}

		// Token: 0x06004A4D RID: 19021 RVA: 0x00104488 File Offset: 0x00102688
		private StoreObjectId GetItemEntryIdIfValid(QueryResult queryResult)
		{
			object[][] rows = queryResult.GetRows(1);
			StoreId id = null;
			if (rows.Length > 0)
			{
				id = Util.GetItemPropertyOrDefault<StoreId>(rows[0], ItemSchema.Id, null, FavoriteFolderCollection.PropertyMap);
			}
			return StoreId.GetStoreObjectId(id);
		}

		// Token: 0x06004A4E RID: 19022 RVA: 0x001044C0 File Offset: 0x001026C0
		private static FavoriteFolderEntry CreateFavoriteFolderEntryBasedOnFolderId(MailboxSession session, FolderId folderId, StoreObjectId storeFolderId, FolderTreeDataType folderTreeDataType)
		{
			StoreObjectType storeObjectType = StoreObjectType.Unknown;
			if (folderId != null && !string.IsNullOrEmpty(folderId.ChangeKey))
			{
				storeObjectType = IdConverter.GetObjectTypeFromChangeKey(folderId.ChangeKey);
			}
			FavoriteFolderType favoriteFolderType;
			switch (storeObjectType)
			{
			case StoreObjectType.CalendarFolder:
				favoriteFolderType = FavoriteFolderType.Calendar;
				break;
			case StoreObjectType.ContactsFolder:
				favoriteFolderType = FavoriteFolderType.Contact;
				break;
			default:
				favoriteFolderType = FavoriteFolderType.Mail;
				break;
			}
			return FavoriteFolderEntry.Create(session, storeFolderId, folderTreeDataType, favoriteFolderType);
		}

		// Token: 0x04002A56 RID: 10838
		private static readonly SortBy[] SortByWhenQuerying = new SortBy[]
		{
			new SortBy(StoreObjectSchema.ItemClass, SortOrder.Ascending),
			new SortBy(NavigationNodeSchema.GroupSection, SortOrder.Ascending),
			new SortBy(NavigationNodeSchema.Ordinal, SortOrder.Ascending)
		};

		// Token: 0x04002A57 RID: 10839
		private static readonly PropertyDefinition[] AllProperties = new PropertyDefinition[]
		{
			ItemSchema.Id,
			StoreObjectSchema.ItemClass,
			ItemSchema.Subject,
			NavigationNodeSchema.GroupSection,
			NavigationNodeSchema.NodeEntryId,
			NavigationNodeSchema.Ordinal,
			NavigationNodeSchema.Type
		};

		// Token: 0x04002A58 RID: 10840
		private static readonly PropertyDefinition[] ShortcutMessageProperties = new PropertyDefinition[]
		{
			ItemSchema.Id,
			StoreObjectSchema.ItemClass,
			ItemSchema.FavLevelMask,
			ShortcutMessageSchema.FavoriteDisplayName,
			ShortcutMessageSchema.FavPublicSourceKey
		};

		// Token: 0x04002A59 RID: 10841
		private static readonly Dictionary<PropertyDefinition, int> PropertyMap = Util.LoadPropertyMap(FavoriteFolderCollection.AllProperties);

		// Token: 0x04002A5A RID: 10842
		private static readonly PropertyDefinition[] folderProperties = new PropertyDefinition[]
		{
			FolderSchema.Id,
			FolderSchema.DisplayName
		};

		// Token: 0x04002A5B RID: 10843
		private static readonly DefaultFolderType[] favoriteDefaultFolders = new DefaultFolderType[]
		{
			DefaultFolderType.Inbox,
			DefaultFolderType.SentItems,
			DefaultFolderType.Drafts
		};

		// Token: 0x04002A5C RID: 10844
		private List<FavoriteFolderNode> favoriteFolders;

		// Token: 0x04002A5D RID: 10845
		private List<string> distinctFolderIds;

		// Token: 0x04002A5E RID: 10846
		private MailboxSession session;

		// Token: 0x04002A5F RID: 10847
		private readonly FolderTreeDataSection folderTreeSection;
	}
}
