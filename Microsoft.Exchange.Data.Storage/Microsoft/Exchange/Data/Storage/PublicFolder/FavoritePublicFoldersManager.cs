using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;

namespace Microsoft.Exchange.Data.Storage.PublicFolder
{
	// Token: 0x02000942 RID: 2370
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class FavoritePublicFoldersManager : IFavoritePublicFoldersManager
	{
		// Token: 0x06005847 RID: 22599 RVA: 0x0016AD3F File Offset: 0x00168F3F
		public FavoritePublicFoldersManager(IMailboxSession mailboxSession)
		{
			this.mailboxSession = mailboxSession;
		}

		// Token: 0x06005848 RID: 22600 RVA: 0x0016AD4E File Offset: 0x00168F4E
		public IEnumerable<IFavoritePublicFolder> EnumerateCalendarFolders()
		{
			return this.InternalEnumerateFavorites(FolderTreeDataSection.Calendar);
		}

		// Token: 0x06005849 RID: 22601 RVA: 0x0016AD57 File Offset: 0x00168F57
		public IEnumerable<IFavoritePublicFolder> EnumerateContactsFolders()
		{
			return this.InternalEnumerateFavorites(FolderTreeDataSection.Contacts);
		}

		// Token: 0x0600584A RID: 22602 RVA: 0x0016AD60 File Offset: 0x00168F60
		public IEnumerable<IFavoritePublicFolder> EnumerateMailAndPostsFolders()
		{
			return this.InternalEnumerateFavorites(FolderTreeDataSection.First);
		}

		// Token: 0x0600584B RID: 22603 RVA: 0x0016AD69 File Offset: 0x00168F69
		protected virtual IFolder InternalBindToCommonViewsFolder()
		{
			return Folder.Bind((MailboxSession)this.mailboxSession, DefaultFolderType.CommonViews);
		}

		// Token: 0x0600584C RID: 22604 RVA: 0x0016B07C File Offset: 0x0016927C
		private IEnumerable<IFavoritePublicFolder> InternalEnumerateFavorites(FolderTreeDataSection folderTreeDataSection)
		{
			using (IFolder commonViewsFolder = this.InternalBindToCommonViewsFolder())
			{
				using (IQueryResult queryResult = commonViewsFolder.IItemQuery(ItemQueryType.Associated, null, FavoritePublicFoldersManager.FavoriteFolderEntrySortBy, FavoritePublicFoldersManager.FavoriteFolderEntryProperties))
				{
					if (queryResult.SeekToCondition(SeekReference.OriginBeginning, FavoritePublicFoldersManager.ItemClassFilter, SeekToConditionFlags.AllowExtendedFilters))
					{
						object[][] rowsFetched = null;
						bool shouldContinue = false;
						bool mightBeMoreRows = false;
						do
						{
							rowsFetched = queryResult.GetRows(10000, out mightBeMoreRows);
							foreach (object[] row in rowsFetched)
							{
								IFavoritePublicFolder favoritePublicFolder = null;
								if (this.TryFetchFavoritePublicFolder(row, (int)folderTreeDataSection, out favoritePublicFolder, out shouldContinue))
								{
									yield return favoritePublicFolder;
								}
							}
						}
						while (rowsFetched.Length > 0 && shouldContinue);
					}
				}
			}
			yield break;
		}

		// Token: 0x0600584D RID: 22605 RVA: 0x0016B0A0 File Offset: 0x001692A0
		private bool TryFetchFavoritePublicFolder(object[] row, int folderTreeDataSectionValue, out IFavoritePublicFolder favoritePublicFolder, out bool shouldContinue)
		{
			favoritePublicFolder = null;
			shouldContinue = true;
			StoreId storeId = FavoritePublicFoldersManager.InternalGetItemPropertyOrDefault<StoreId>(row, null, FavoritePublicFoldersManager.FavoriteFolderEntryPropertyIndex.Id);
			if (storeId == null)
			{
				FavoritePublicFoldersManager.Tracer.TraceError((long)this.GetHashCode(), "TryFetchFavoritePublicFolder: detected item with null id");
				return false;
			}
			FavoritePublicFoldersManager.Tracer.TraceDebug<string>((long)this.GetHashCode(), "TryFetchFavoritePublicFolder: processing item '{0}'", storeId.ToBase64String());
			string text = FavoritePublicFoldersManager.InternalGetItemPropertyOrDefault<string>(row, string.Empty, FavoritePublicFoldersManager.FavoriteFolderEntryPropertyIndex.ItemClass);
			if (!ObjectClass.IsFolderTreeData(text))
			{
				shouldContinue = false;
				FavoritePublicFoldersManager.Tracer.TraceDebug<string>((long)this.GetHashCode(), "TryFetchFavoritePublicFolder: not the expected item class '{0}', stopping enumeration", text);
				return false;
			}
			int num = FavoritePublicFoldersManager.InternalGetItemPropertyOrDefault<int>(row, -1, FavoritePublicFoldersManager.FavoriteFolderEntryPropertyIndex.GroupSection);
			if (folderTreeDataSectionValue != num)
			{
				FavoritePublicFoldersManager.Tracer.TraceDebug<int, int>((long)this.GetHashCode(), "TryFetchFavoritePublicFolder: not the expected group section (expected '{0}', actual '{1}'), skipping", folderTreeDataSectionValue, num);
				return false;
			}
			int num2 = FavoritePublicFoldersManager.InternalGetItemPropertyOrDefault<int>(row, -1, FavoritePublicFoldersManager.FavoriteFolderEntryPropertyIndex.Type);
			if (num2 != 0 && num2 != 2 && num2 != 1)
			{
				FavoritePublicFoldersManager.Tracer.TraceDebug<int>((long)this.GetHashCode(), "TryFetchFavoritePublicFolder: not the expected folder type '{0}', skipping", num2);
				return false;
			}
			byte[] array = FavoritePublicFoldersManager.InternalGetItemPropertyOrDefault<byte[]>(row, null, FavoritePublicFoldersManager.FavoriteFolderEntryPropertyIndex.NodeEntryId);
			if (array == null)
			{
				FavoritePublicFoldersManager.Tracer.TraceError((long)this.GetHashCode(), "TryFetchFavoritePublicFolder: folderEntryId was null");
				return false;
			}
			StoreObjectId storeObjectId = null;
			try
			{
				storeObjectId = StoreObjectId.FromProviderSpecificId(array);
				if (!storeObjectId.IsPublicFolderType())
				{
					FavoritePublicFoldersManager.Tracer.TraceDebug<string>((long)this.GetHashCode(), "TryFetchFavoritePublicFolder: folderStoreObjectId is not of the expected type (actual '{0}'), skipping", (storeObjectId == null) ? "null" : storeObjectId.ToHexEntryId());
					return false;
				}
				storeObjectId = StoreObjectId.FromLegacyFavoritePublicFolderId(storeObjectId);
			}
			catch (CorruptDataException arg)
			{
				FavoritePublicFoldersManager.Tracer.TraceError<CorruptDataException, string>((long)this.GetHashCode(), "GetFavorites: Exception {0} encountered trying to retrieve navigation node entry ID for folder entry id {1}", arg, (array == null) ? "null" : Convert.ToBase64String(array));
				return false;
			}
			string displayName = FavoritePublicFoldersManager.InternalGetItemPropertyOrDefault<string>(row, string.Empty, FavoritePublicFoldersManager.FavoriteFolderEntryPropertyIndex.Subject);
			favoritePublicFolder = new FavoritePublicFolder(storeObjectId, displayName);
			return true;
		}

		// Token: 0x0600584E RID: 22606 RVA: 0x0016B250 File Offset: 0x00169450
		private static T InternalGetItemPropertyOrDefault<T>(object[] row, T defaultValue, FavoritePublicFoldersManager.FavoriteFolderEntryPropertyIndex propertyIndex)
		{
			T result = defaultValue;
			if (propertyIndex >= FavoritePublicFoldersManager.FavoriteFolderEntryPropertyIndex.ItemClass && propertyIndex < (FavoritePublicFoldersManager.FavoriteFolderEntryPropertyIndex)row.Length)
			{
				object obj = row[(int)propertyIndex];
				if (obj == null || typeof(T).IsAssignableFrom(obj.GetType()))
				{
					result = (T)((object)obj);
				}
			}
			return result;
		}

		// Token: 0x0400300C RID: 12300
		private static readonly Trace Tracer = ExTraceGlobals.FavoritePublicFoldersTracer;

		// Token: 0x0400300D RID: 12301
		private static readonly SortBy[] FavoriteFolderEntrySortBy = new SortBy[]
		{
			new SortBy(StoreObjectSchema.ItemClass, SortOrder.Ascending),
			new SortBy(FolderTreeDataSchema.GroupSection, SortOrder.Ascending),
			new SortBy(FolderTreeDataSchema.Ordinal, SortOrder.Ascending)
		};

		// Token: 0x0400300E RID: 12302
		private static readonly PropertyDefinition[] FavoriteFolderEntryProperties = new PropertyDefinition[]
		{
			StoreObjectSchema.ItemClass,
			FolderTreeDataSchema.GroupSection,
			FolderTreeDataSchema.Ordinal,
			FolderTreeDataSchema.Type,
			ItemSchema.Id,
			FavoriteFolderEntrySchema.NodeEntryId,
			ItemSchema.Subject
		};

		// Token: 0x0400300F RID: 12303
		private static readonly QueryFilter ItemClassFilter = new TextFilter(StoreObjectSchema.ItemClass, "IPM.Microsoft.WunderBar.Link", MatchOptions.Prefix, MatchFlags.IgnoreCase);

		// Token: 0x04003010 RID: 12304
		private readonly IMailboxSession mailboxSession;

		// Token: 0x02000943 RID: 2371
		private enum FavoriteFolderEntryPropertyIndex
		{
			// Token: 0x04003012 RID: 12306
			ItemClass,
			// Token: 0x04003013 RID: 12307
			GroupSection,
			// Token: 0x04003014 RID: 12308
			Ordinal,
			// Token: 0x04003015 RID: 12309
			Type,
			// Token: 0x04003016 RID: 12310
			Id,
			// Token: 0x04003017 RID: 12311
			NodeEntryId,
			// Token: 0x04003018 RID: 12312
			Subject
		}
	}
}
