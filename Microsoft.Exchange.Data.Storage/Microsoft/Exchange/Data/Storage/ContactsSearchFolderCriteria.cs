using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020004D9 RID: 1241
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class ContactsSearchFolderCriteria
	{
		// Token: 0x0600361B RID: 13851 RVA: 0x000DA6DD File Offset: 0x000D88DD
		private ContactsSearchFolderCriteria(DefaultFolderType defaultFolderType, DefaultFolderType[] scopeDefaultFolderTypes)
		{
			this.defaultFolderType = defaultFolderType;
			this.scopeDefaultFolderTypes = scopeDefaultFolderTypes;
		}

		// Token: 0x0600361C RID: 13852 RVA: 0x000DA6F3 File Offset: 0x000D88F3
		public static void ApplyOneTimeSearchFolderCriteria(IXSOFactory xsoFactory, IMailboxSession mailboxSession, ISearchFolder searchFolder, SearchFolderCriteria searchFolderCriteria)
		{
			ContactsSearchFolderCriteria.ApplySearchFolderCriteria(xsoFactory, mailboxSession, searchFolder, searchFolderCriteria, new Action<SearchFolderCriteria>(searchFolder.ApplyOneTimeSearch));
		}

		// Token: 0x0600361D RID: 13853 RVA: 0x000DA70B File Offset: 0x000D890B
		public static void ApplyContinuousSearchFolderCriteria(IXSOFactory xsoFactory, IMailboxSession mailboxSession, ISearchFolder searchFolder, SearchFolderCriteria searchFolderCriteria)
		{
			ContactsSearchFolderCriteria.ApplySearchFolderCriteria(xsoFactory, mailboxSession, searchFolder, searchFolderCriteria, new Action<SearchFolderCriteria>(searchFolder.ApplyContinuousSearch));
		}

		// Token: 0x0600361E RID: 13854 RVA: 0x000DA724 File Offset: 0x000D8924
		public static void WaitForSearchFolderPopulation(IXSOFactory xsoFactory, IMailboxSession mailboxSession, ISearchFolder searchFolder)
		{
			if (ContactsSearchFolderCriteria.IsSearchFolderPopulated(searchFolder))
			{
				ContactsSearchFolderCriteria.Tracer.TraceDebug((long)mailboxSession.GetHashCode(), "Search folder is already populated. No wait required.");
				return;
			}
			ContactsSearchFolderCriteria.Tracer.TraceDebug((long)mailboxSession.GetHashCode(), "Waiting for search folder to complete current population.");
			searchFolder.Load();
			using (SearchFolderAsyncSearch searchFolderAsyncSearch = new SearchFolderAsyncSearch((MailboxSession)mailboxSession, searchFolder.Id.ObjectId, null, null))
			{
				if (ContactsSearchFolderCriteria.IsSearchFolderPopulated(searchFolder))
				{
					ContactsSearchFolderCriteria.Tracer.TraceDebug((long)mailboxSession.GetHashCode(), "Search folder population completed.");
				}
				else
				{
					bool arg = searchFolderAsyncSearch.AsyncResult.AsyncWaitHandle.WaitOne(ContactsSearchFolderCriteria.SearchInProgressTimeout);
					ContactsSearchFolderCriteria.Tracer.TraceDebug<bool>((long)mailboxSession.GetHashCode(), "Done waiting, search folder population completed: {0}.", arg);
				}
			}
		}

		// Token: 0x0600361F RID: 13855 RVA: 0x000DA7F0 File Offset: 0x000D89F0
		private static void ApplySearchFolderCriteria(IXSOFactory xsoFactory, IMailboxSession mailboxSession, ISearchFolder searchFolder, SearchFolderCriteria searchFolderCriteria, Action<SearchFolderCriteria> applySearchCriteriaAction)
		{
			ArgumentValidator.ThrowIfNull("xsoFactory", xsoFactory);
			ArgumentValidator.ThrowIfNull("mailboxSession", mailboxSession);
			ArgumentValidator.ThrowIfNull("searchFolder", searchFolder);
			ArgumentValidator.ThrowIfNull("searchFolderCriteria", searchFolderCriteria);
			ContactsSearchFolderCriteria.Tracer.TraceDebug<SearchFolderCriteria>((long)mailboxSession.GetHashCode(), "Applying search folder criteria: {0}", searchFolderCriteria);
			try
			{
				applySearchCriteriaAction(searchFolderCriteria);
			}
			catch (ObjectNotFoundException)
			{
				List<StoreObjectId> list = new List<StoreObjectId>(searchFolderCriteria.FolderScope.Length);
				foreach (StoreId storeId in searchFolderCriteria.FolderScope)
				{
					list.Add((StoreObjectId)storeId);
				}
				searchFolderCriteria.FolderScope = ContactsSearchFolderCriteria.RemoveDeletedFoldersFromCollection(xsoFactory, mailboxSession, list);
				applySearchCriteriaAction(searchFolderCriteria);
			}
		}

		// Token: 0x06003620 RID: 13856 RVA: 0x000DA8C0 File Offset: 0x000D8AC0
		public static StoreObjectId[] RemoveDeletedFoldersFromCollection(IXSOFactory xsoFactory, IMailboxSession mailboxSession, IEnumerable<StoreObjectId> folderIds)
		{
			ArgumentValidator.ThrowIfNull("xsoFactory", xsoFactory);
			ArgumentValidator.ThrowIfNull("mailboxSession", mailboxSession);
			ArgumentValidator.ThrowIfNull("folderIds", folderIds);
			List<StoreObjectId> list = new List<StoreObjectId>(folderIds);
			list.RemoveAll((StoreObjectId folderId) => ContactsSearchFolderCriteria.IsDeletedFolder(mailboxSession, folderId));
			return list.ToArray();
		}

		// Token: 0x06003621 RID: 13857 RVA: 0x000DA920 File Offset: 0x000D8B20
		public static StoreObjectId[] GetMyContactExtendedFolders(IMailboxSession mailboxSession, StoreObjectId[] myContactsFolders, bool forceCreate)
		{
			ArgumentValidator.ThrowIfNull("mailboxSession", mailboxSession);
			ArgumentValidator.ThrowIfNull("myContactsFolders", myContactsFolders);
			StoreObjectId[] folderIds = ContactsSearchFolderCriteria.GetFolderIds(mailboxSession, forceCreate, ContactsSearchFolderCriteria.BaseScopeForMyContactExtended);
			HashSet<StoreObjectId> hashSet = new HashSet<StoreObjectId>(StoreId.EqualityComparer);
			hashSet.UnionWith(folderIds);
			hashSet.UnionWith(myContactsFolders);
			StoreObjectId[] array = new StoreObjectId[hashSet.Count];
			hashSet.CopyTo(array);
			return array;
		}

		// Token: 0x06003622 RID: 13858 RVA: 0x000DA980 File Offset: 0x000D8B80
		public static void UpdateFolderScope(IXSOFactory xsoFactory, IMailboxSession mailboxSession, ISearchFolder searchFolder, StoreObjectId[] folderScope)
		{
			ArgumentValidator.ThrowIfNull("xsoFactory", xsoFactory);
			ArgumentValidator.ThrowIfNull("mailboxSession", mailboxSession);
			ArgumentValidator.ThrowIfNull("searchFolder", searchFolder);
			ArgumentValidator.ThrowIfNull("folderScope", folderScope);
			SearchFolderCriteria searchFolderCriteria;
			try
			{
				searchFolderCriteria = searchFolder.GetSearchCriteria();
			}
			catch (ObjectNotInitializedException)
			{
				searchFolderCriteria = null;
			}
			if (searchFolderCriteria != null && ContactsSearchFolderCriteria.MatchFolderScope(searchFolderCriteria.FolderScope, folderScope))
			{
				return;
			}
			SearchFolderCriteria searchFolderCriteria2 = ContactsSearchFolderCriteria.CreateSearchCriteria(folderScope);
			ContactsSearchFolderCriteria.Tracer.TraceDebug<SearchFolderCriteria, SearchFolderCriteria>((long)searchFolder.GetHashCode(), "Updating MyContactsFolder Search Criteria since it is different from the current one. Current:{0}, New:{1}.", searchFolderCriteria, searchFolderCriteria2);
			ContactsSearchFolderCriteria.ApplyContinuousSearchFolderCriteria(xsoFactory, mailboxSession, searchFolder, searchFolderCriteria2);
			ContactsSearchFolderCriteria.WaitForSearchFolderPopulation(xsoFactory, mailboxSession, searchFolder);
		}

		// Token: 0x06003623 RID: 13859 RVA: 0x000DAA1C File Offset: 0x000D8C1C
		public static SearchFolderCriteria CreateSearchCriteria(StoreObjectId[] folderScope)
		{
			return new SearchFolderCriteria(ContactsSearchFolderCriteria.QueryFilter, folderScope)
			{
				DeepTraversal = false
			};
		}

		// Token: 0x170010D0 RID: 4304
		// (get) Token: 0x06003624 RID: 13860 RVA: 0x000DAA3D File Offset: 0x000D8C3D
		public DefaultFolderType[] ScopeDefaultFolderTypes
		{
			get
			{
				return this.scopeDefaultFolderTypes;
			}
		}

		// Token: 0x06003625 RID: 13861 RVA: 0x000DAA45 File Offset: 0x000D8C45
		public StoreObjectId[] GetDefaultFolderScope(IMailboxSession session, bool forceCreate)
		{
			ArgumentValidator.ThrowIfNull("session", session);
			return ContactsSearchFolderCriteria.GetFolderIds(session, forceCreate, this.scopeDefaultFolderTypes);
		}

		// Token: 0x06003626 RID: 13862 RVA: 0x000DAA60 File Offset: 0x000D8C60
		public StoreObjectId[] GetExistingDefaultFolderScope(DefaultFolderContext context)
		{
			ArgumentValidator.ThrowIfNull("context", context);
			List<StoreObjectId> list = new List<StoreObjectId>(this.scopeDefaultFolderTypes.Length);
			foreach (DefaultFolderType defaultFolderType in this.scopeDefaultFolderTypes)
			{
				StoreObjectId storeObjectId = context[defaultFolderType];
				if (storeObjectId != null)
				{
					list.Add(storeObjectId);
				}
			}
			return list.ToArray();
		}

		// Token: 0x06003627 RID: 13863 RVA: 0x000DAABC File Offset: 0x000D8CBC
		public void UpdateFolderScope(IXSOFactory xsoFactory, IMailboxSession mailboxSession, StoreObjectId[] scope)
		{
			ArgumentValidator.ThrowIfNull("xsoFactory", xsoFactory);
			ArgumentValidator.ThrowIfNull("mailboxSession", mailboxSession);
			ArgumentValidator.ThrowIfNull("scope", scope);
			StoreObjectId defaultFolderId = mailboxSession.GetDefaultFolderId(this.defaultFolderType);
			if (defaultFolderId == null)
			{
				mailboxSession.CreateDefaultFolder(this.defaultFolderType);
				defaultFolderId = mailboxSession.GetDefaultFolderId(this.defaultFolderType);
			}
			using (ISearchFolder searchFolder = xsoFactory.BindToSearchFolder(mailboxSession, defaultFolderId))
			{
				ContactsSearchFolderCriteria.UpdateFolderScope(xsoFactory, mailboxSession, searchFolder, scope);
			}
		}

		// Token: 0x06003628 RID: 13864 RVA: 0x000DAB44 File Offset: 0x000D8D44
		private static StoreObjectId[] GetFolderIds(IMailboxSession session, bool forceCreate, DefaultFolderType[] scopeDefaultFolderTypes)
		{
			List<StoreObjectId> list = new List<StoreObjectId>(scopeDefaultFolderTypes.Length);
			foreach (DefaultFolderType arg in scopeDefaultFolderTypes)
			{
				StoreObjectId defaultFolderId = session.GetDefaultFolderId(arg);
				if (forceCreate && defaultFolderId == null)
				{
					ContactsSearchFolderCriteria.Tracer.TraceDebug<DefaultFolderType>(0L, "Default folder {0} not yet created. Explicitly creating it now.", arg);
					session.CreateDefaultFolder(arg);
					defaultFolderId = session.GetDefaultFolderId(arg);
				}
				if (defaultFolderId != null)
				{
					list.Add(defaultFolderId);
				}
			}
			return list.ToArray();
		}

		// Token: 0x06003629 RID: 13865 RVA: 0x000DABB4 File Offset: 0x000D8DB4
		private static bool IsDeletedFolder(IStoreSession session, StoreObjectId folderId)
		{
			try
			{
				StoreObjectId parentFolderId = session.GetParentFolderId(folderId);
				if (parentFolderId != null)
				{
					return false;
				}
			}
			catch (ObjectNotFoundException)
			{
			}
			ContactsSearchFolderCriteria.Tracer.TraceDebug<StoreObjectId>(0L, "Folder with does not exist, removed from search folder criteria: {0}", folderId);
			return true;
		}

		// Token: 0x0600362A RID: 13866 RVA: 0x000DABFC File Offset: 0x000D8DFC
		private static bool MatchFolderScope(StoreId[] existingFolderScope, StoreId[] newFolderScope)
		{
			HashSet<StoreId> hashSet = new HashSet<StoreId>(existingFolderScope, StoreId.EqualityComparer);
			HashSet<StoreId> equals = new HashSet<StoreId>(newFolderScope, StoreId.EqualityComparer);
			return hashSet.SetEquals(equals);
		}

		// Token: 0x0600362B RID: 13867 RVA: 0x000DAC28 File Offset: 0x000D8E28
		private static bool IsSearchFolderPopulated(ISearchFolder folder)
		{
			SearchFolderCriteria searchCriteria = folder.GetSearchCriteria();
			return (searchCriteria.SearchState & SearchState.Rebuild) != SearchState.Rebuild;
		}

		// Token: 0x04001D0C RID: 7436
		private static readonly Trace Tracer = ExTraceGlobals.MyContactsFolderTracer;

		// Token: 0x04001D0D RID: 7437
		private static readonly TimeSpan SearchInProgressTimeout = TimeSpan.FromSeconds(30.0);

		// Token: 0x04001D0E RID: 7438
		private static readonly QueryFilter QueryFilter = new OrFilter(new QueryFilter[]
		{
			new ComparisonFilter(ComparisonOperator.Equal, StoreObjectSchema.ItemClass, "IPM.Contact"),
			new ComparisonFilter(ComparisonOperator.Equal, StoreObjectSchema.ItemClass, "IPM.DistList")
		});

		// Token: 0x04001D0F RID: 7439
		private readonly DefaultFolderType defaultFolderType;

		// Token: 0x04001D10 RID: 7440
		private readonly DefaultFolderType[] scopeDefaultFolderTypes;

		// Token: 0x04001D11 RID: 7441
		private static readonly DefaultFolderType[] BaseScopeForMyContactExtended = new DefaultFolderType[]
		{
			DefaultFolderType.QuickContacts,
			DefaultFolderType.RecipientCache
		};

		// Token: 0x04001D12 RID: 7442
		public static readonly ContactsSearchFolderCriteria MyContacts = new ContactsSearchFolderCriteria(DefaultFolderType.MyContacts, new DefaultFolderType[]
		{
			DefaultFolderType.Contacts,
			DefaultFolderType.QuickContacts
		});

		// Token: 0x04001D13 RID: 7443
		public static readonly ContactsSearchFolderCriteria MyContactsExtended = new ContactsSearchFolderCriteria(DefaultFolderType.MyContactsExtended, new DefaultFolderType[]
		{
			DefaultFolderType.Contacts,
			DefaultFolderType.QuickContacts,
			DefaultFolderType.RecipientCache
		});
	}
}
