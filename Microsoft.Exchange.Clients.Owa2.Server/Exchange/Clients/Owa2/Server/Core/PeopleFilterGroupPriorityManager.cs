using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x02000216 RID: 534
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class PeopleFilterGroupPriorityManager
	{
		// Token: 0x0600149D RID: 5277 RVA: 0x00049129 File Offset: 0x00047329
		public PeopleFilterGroupPriorityManager(IMailboxSession session, IXSOFactory xsoFactory)
		{
			if (session == null)
			{
				throw new ArgumentNullException("session");
			}
			if (xsoFactory == null)
			{
				throw new ArgumentNullException("xsoFactory");
			}
			this.session = session;
			this.xsoFactory = xsoFactory;
		}

		// Token: 0x170004F9 RID: 1273
		// (get) Token: 0x0600149E RID: 5278 RVA: 0x0004915B File Offset: 0x0004735B
		private StoreObjectId DefaultContactsFolderId
		{
			get
			{
				if (this.defaultContactsFolderId == null)
				{
					this.defaultContactsFolderId = this.session.GetDefaultFolderId(DefaultFolderType.Contacts);
				}
				return this.defaultContactsFolderId;
			}
		}

		// Token: 0x170004FA RID: 1274
		// (get) Token: 0x0600149F RID: 5279 RVA: 0x0004917D File Offset: 0x0004737D
		private StoreObjectId QuickContactsFolderId
		{
			get
			{
				if (this.quickContactsFolderId == null)
				{
					this.quickContactsFolderId = this.session.GetDefaultFolderId(DefaultFolderType.QuickContacts);
				}
				return this.quickContactsFolderId;
			}
		}

		// Token: 0x060014A0 RID: 5280 RVA: 0x000491A0 File Offset: 0x000473A0
		public static void SetSortGroupPriorityOnFolder(IStorePropertyBag folder, int sortGroupPriority)
		{
			folder[FolderSchema.PeopleHubSortGroupPriority] = sortGroupPriority;
			folder[FolderSchema.PeopleHubSortGroupPriorityVersion] = 2;
		}

		// Token: 0x060014A1 RID: 5281 RVA: 0x000491C4 File Offset: 0x000473C4
		public int DetermineSortGroupPriority(IStorePropertyBag folder)
		{
			StoreObjectId objectId = ((VersionedId)folder.TryGetProperty(FolderSchema.Id)).ObjectId;
			int valueOrDefault = folder.GetValueOrDefault<int>(FolderSchema.PeopleHubSortGroupPriorityVersion, -1);
			int num = folder.GetValueOrDefault<int>(FolderSchema.PeopleHubSortGroupPriority, -1);
			bool valueOrDefault2 = folder.GetValueOrDefault<bool>(FolderSchema.IsPeopleConnectSyncFolder, false);
			if (valueOrDefault == 2 && num >= 0)
			{
				return num;
			}
			if (object.Equals(objectId, this.DefaultContactsFolderId))
			{
				num = 2;
			}
			else if (object.Equals(objectId, this.QuickContactsFolderId))
			{
				num = 4;
			}
			else if (valueOrDefault2)
			{
				num = 3;
			}
			else
			{
				num = 10;
			}
			using (IFolder folder2 = this.xsoFactory.BindToFolder(this.session, objectId))
			{
				PeopleFilterGroupPriorityManager.SetSortGroupPriorityOnFolder(folder2, num);
				folder2.Save();
			}
			return num;
		}

		// Token: 0x060014A2 RID: 5282 RVA: 0x0004928C File Offset: 0x0004748C
		public StoreObjectId[] GetMyContactFolderIds()
		{
			ContactFoldersEnumerator contactFoldersEnumerator = new ContactFoldersEnumerator(this.session, this.xsoFactory, ContactFoldersEnumeratorOptions.SkipHiddenFolders | ContactFoldersEnumeratorOptions.SkipDeletedFolders, PeopleFilterGroupPriorityManager.RequiredFolderProperties);
			List<StoreObjectId> list = new List<StoreObjectId>(10);
			foreach (IStorePropertyBag storePropertyBag in contactFoldersEnumerator)
			{
				StoreObjectId objectId = ((VersionedId)storePropertyBag.TryGetProperty(FolderSchema.Id)).ObjectId;
				int sortGroupPriority = this.DetermineSortGroupPriority(storePropertyBag);
				if (PeopleFilterGroupPriorities.ShouldBeIncludedInMyContactsFolder(sortGroupPriority))
				{
					list.Add(objectId);
				}
			}
			return list.ToArray();
		}

		// Token: 0x04000B2F RID: 2863
		public static readonly PropertyDefinition[] RequiredFolderProperties = new PropertyDefinition[]
		{
			FolderSchema.Id,
			StoreObjectSchema.ParentItemId,
			FolderSchema.PeopleHubSortGroupPriority,
			FolderSchema.PeopleHubSortGroupPriorityVersion,
			FolderSchema.IsPeopleConnectSyncFolder,
			FolderSchema.ExtendedFolderFlags
		};

		// Token: 0x04000B30 RID: 2864
		private readonly IMailboxSession session;

		// Token: 0x04000B31 RID: 2865
		private readonly IXSOFactory xsoFactory;

		// Token: 0x04000B32 RID: 2866
		private StoreObjectId defaultContactsFolderId;

		// Token: 0x04000B33 RID: 2867
		private StoreObjectId quickContactsFolderId;
	}
}
