using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020004FB RID: 1275
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class MyContactFolders
	{
		// Token: 0x06003768 RID: 14184 RVA: 0x000DEE9C File Offset: 0x000DD09C
		internal MyContactFolders(StoreObjectId[] folderIds)
		{
			this.folderIds = folderIds;
		}

		// Token: 0x06003769 RID: 14185 RVA: 0x000DEEAB File Offset: 0x000DD0AB
		public MyContactFolders(IXSOFactory xsoFactory, IMailboxSession mailboxSession)
		{
			this.xsoFactory = xsoFactory;
			this.mailboxSession = mailboxSession;
		}

		// Token: 0x1700114A RID: 4426
		// (get) Token: 0x0600376A RID: 14186 RVA: 0x000DEEC1 File Offset: 0x000DD0C1
		public StoreObjectId[] Value
		{
			get
			{
				this.EnsureFolderIds(false);
				return this.folderIds;
			}
		}

		// Token: 0x0600376B RID: 14187 RVA: 0x000DEED0 File Offset: 0x000DD0D0
		public bool Contains(StoreObjectId folderId)
		{
			this.EnsureFolderIds(false);
			return this.folderIds.Contains(folderId);
		}

		// Token: 0x0600376C RID: 14188 RVA: 0x000DEEFC File Offset: 0x000DD0FC
		public void Add(StoreObjectId newFolderId)
		{
			this.EnsureFolderIds(true);
			if (!Array.Exists<StoreObjectId>(this.folderIds, (StoreObjectId folderId) => folderId.Equals(newFolderId)))
			{
				MyContactFolders.Tracer.TraceDebug<StoreObjectId, ArrayTracer<StoreObjectId>>((long)this.GetHashCode(), "Adding folder '{0}' to the list of MyContactFolders: {1}", newFolderId, new ArrayTracer<StoreObjectId>(this.folderIds));
				List<StoreObjectId> list = new List<StoreObjectId>(this.folderIds.Length + 1);
				list.AddRange(this.folderIds);
				list.Add(newFolderId);
				this.Set(list.ToArray());
			}
		}

		// Token: 0x0600376D RID: 14189 RVA: 0x000DEF94 File Offset: 0x000DD194
		public void Set(StoreObjectId[] newFolderIds)
		{
			MyContactFolders.Tracer.TraceDebug<ArrayTracer<StoreObjectId>>((long)this.GetHashCode(), "Updating folder ids with list of folders: {0}", new ArrayTracer<StoreObjectId>(newFolderIds));
			using (IFolder folder = this.xsoFactory.BindToFolder(this.mailboxSession, DefaultFolderType.Configuration, new PropertyDefinition[]
			{
				InternalSchema.MyContactsFolders
			}))
			{
				this.SetFolderIds(folder, newFolderIds);
				this.UpdateFolderIdsProperty(folder, newFolderIds);
			}
			this.folderIds = newFolderIds;
		}

		// Token: 0x0600376E RID: 14190 RVA: 0x000DF014 File Offset: 0x000DD214
		private void UpdateFolderIdsProperty(IFolder rootFolder, StoreObjectId[] folderIds)
		{
			MyContactFolders.Tracer.TraceDebug<string, ArrayTracer<StoreObjectId>>((long)this.GetHashCode(), "Updating folder ids property {0} on root folder with new list of folder ids: {1}", InternalSchema.MyContactsFolders.Name, new ArrayTracer<StoreObjectId>(folderIds));
			rootFolder[InternalSchema.MyContactsFolders] = folderIds;
			rootFolder.Save(SaveMode.NoConflictResolutionForceSave);
		}

		// Token: 0x0600376F RID: 14191 RVA: 0x000DF050 File Offset: 0x000DD250
		private void EnsureFolderIds(bool reload)
		{
			if (this.folderIds == null || reload)
			{
				MyContactFolders.Tracer.TraceDebug((long)this.GetHashCode(), "Loading folder ids from the mailbox");
				using (IFolder folder = this.xsoFactory.BindToFolder(this.mailboxSession, DefaultFolderType.Configuration, new PropertyDefinition[]
				{
					InternalSchema.MyContactsFolders
				}))
				{
					StoreObjectId[] valueOrDefault = folder.GetValueOrDefault<StoreObjectId[]>(InternalSchema.MyContactsFolders, null);
					if (valueOrDefault == null)
					{
						valueOrDefault = this.GetFolderIds();
						this.UpdateFolderIdsProperty(folder, valueOrDefault);
					}
					else if (!MyContactFolders.AreSame(valueOrDefault, this.folderIds))
					{
						this.UpdateFolderIdsProperty(folder, valueOrDefault);
					}
					this.folderIds = valueOrDefault;
				}
			}
		}

		// Token: 0x06003770 RID: 14192 RVA: 0x000DF100 File Offset: 0x000DD300
		private static bool AreSame(StoreObjectId[] a, StoreObjectId[] b)
		{
			if (a == null || b == null)
			{
				return false;
			}
			if (a.Length != b.Length)
			{
				return false;
			}
			Array.Sort<StoreObjectId>(a);
			Array.Sort<StoreObjectId>(b);
			for (int i = 0; i < a.Length; i++)
			{
				if (!a[i].Equals(b[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06003771 RID: 14193 RVA: 0x000DF14C File Offset: 0x000DD34C
		private StoreObjectId[] GetFolderIds()
		{
			StoreObjectId[] array = this.GetFolderIdsFromSearchFolder(DefaultFolderType.MyContacts);
			if (array == null)
			{
				MyContactFolders.Tracer.TraceDebug((long)this.GetHashCode(), "Unable to get folder ids from the MyContacts search folder, instead loading folder ids from the default folders in the mailbox");
				array = ContactsSearchFolderCriteria.MyContacts.GetDefaultFolderScope(this.mailboxSession, true);
			}
			return array;
		}

		// Token: 0x06003772 RID: 14194 RVA: 0x000DF190 File Offset: 0x000DD390
		private void SetFolderIds(IFolder rootFolder, StoreObjectId[] newFolderIds)
		{
			newFolderIds = ContactsSearchFolderCriteria.RemoveDeletedFoldersFromCollection(this.xsoFactory, this.mailboxSession, newFolderIds);
			ContactsSearchFolderCriteria.MyContacts.UpdateFolderScope(this.xsoFactory, this.mailboxSession, newFolderIds);
			ContactsSearchFolderCriteria.MyContactsExtended.UpdateFolderScope(this.xsoFactory, this.mailboxSession, ContactsSearchFolderCriteria.GetMyContactExtendedFolders(this.mailboxSession, newFolderIds, true));
		}

		// Token: 0x06003773 RID: 14195 RVA: 0x000DF1EC File Offset: 0x000DD3EC
		private StoreObjectId[] GetFolderIdsFromSearchFolder(DefaultFolderType searchFolderId)
		{
			SearchFolderCriteria searchFolderCriteria = null;
			try
			{
				using (ISearchFolder searchFolder = this.xsoFactory.BindToSearchFolder(this.mailboxSession, searchFolderId))
				{
					searchFolderCriteria = searchFolder.GetSearchCriteria();
				}
			}
			catch (ObjectNotInitializedException arg)
			{
				MyContactFolders.Tracer.TraceError<DefaultFolderType, ObjectNotInitializedException>((long)this.GetHashCode(), "Unable to load folder ids from the search folder {0} due ObjectNotInitializedException: {1}", searchFolderId, arg);
				return null;
			}
			catch (ObjectNotFoundException arg2)
			{
				MyContactFolders.Tracer.TraceError<DefaultFolderType, ObjectNotFoundException>((long)this.GetHashCode(), "Unable to load folder ids from the search folder {0} due ObjectNotFoundException: {1}", searchFolderId, arg2);
				return null;
			}
			if (searchFolderCriteria == null)
			{
				MyContactFolders.Tracer.TraceError<DefaultFolderType>((long)this.GetHashCode(), "There is no search folder criteria in the search folder {0}", searchFolderId);
				return null;
			}
			if (searchFolderCriteria.FolderScope == null)
			{
				MyContactFolders.Tracer.TraceError<DefaultFolderType>((long)this.GetHashCode(), "There is no folder scope in the search folder {0}", searchFolderId);
				return null;
			}
			if (searchFolderCriteria.FolderScope.Length == 0)
			{
				MyContactFolders.Tracer.TraceError<DefaultFolderType>((long)this.GetHashCode(), "The folder scope in the search folder {0} is empty", searchFolderId);
				return null;
			}
			StoreObjectId[] array = new StoreObjectId[searchFolderCriteria.FolderScope.Length];
			for (int i = 0; i < searchFolderCriteria.FolderScope.Length; i++)
			{
				array[i] = (StoreObjectId)searchFolderCriteria.FolderScope[i];
			}
			MyContactFolders.Tracer.TraceDebug<DefaultFolderType, ArrayTracer<StoreObjectId>>((long)this.GetHashCode(), "Loaded folder ids from the scope of search folder {0}: {1}", searchFolderId, new ArrayTracer<StoreObjectId>(array));
			return array;
		}

		// Token: 0x04001D60 RID: 7520
		private static readonly Trace Tracer = ExTraceGlobals.MyContactsFolderTracer;

		// Token: 0x04001D61 RID: 7521
		private readonly IXSOFactory xsoFactory;

		// Token: 0x04001D62 RID: 7522
		private readonly IMailboxSession mailboxSession;

		// Token: 0x04001D63 RID: 7523
		private StoreObjectId[] folderIds;
	}
}
