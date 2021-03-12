using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Reflection;
using System.Text;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.Management
{
	// Token: 0x02000A69 RID: 2665
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class PublicFolderDataProvider : XsoStoreDataProviderBase
	{
		// Token: 0x17001ADB RID: 6875
		// (get) Token: 0x0600617B RID: 24955 RVA: 0x0019B4BB File Offset: 0x001996BB
		public PublicFolderSessionCache PublicFolderSessionCache
		{
			get
			{
				return this.publicFolderSessionCache;
			}
		}

		// Token: 0x17001ADC RID: 6876
		// (get) Token: 0x0600617C RID: 24956 RVA: 0x0019B4C3 File Offset: 0x001996C3
		public OrganizationId CurrentOrganizationId
		{
			get
			{
				return this.currentOrganizationId;
			}
		}

		// Token: 0x0600617D RID: 24957 RVA: 0x0019B4CC File Offset: 0x001996CC
		public PublicFolderDataProvider(IConfigurationSession configurationSession, string action, Guid mailboxGuid)
		{
			using (DisposeGuard disposeGuard = this.Guard())
			{
				Util.ThrowOnNullArgument(configurationSession, "configurationSession");
				Util.ThrowOnNullOrEmptyArgument(action, "action");
				this.currentOrganizationId = configurationSession.GetOrgContainer().OrganizationId;
				TenantPublicFolderConfiguration value = TenantPublicFolderConfigurationCache.Instance.GetValue(this.currentOrganizationId);
				if (mailboxGuid == Guid.Empty)
				{
					Organization orgContainer = configurationSession.GetOrgContainer();
					if (orgContainer.DefaultPublicFolderMailbox.HierarchyMailboxGuid != value.GetHierarchyMailboxInformation().HierarchyMailboxGuid)
					{
						TenantPublicFolderConfigurationCache.Instance.RemoveValue(this.currentOrganizationId);
					}
				}
				else if (value.GetLocalMailboxRecipient(mailboxGuid) == null)
				{
					TenantPublicFolderConfigurationCache.Instance.RemoveValue(this.currentOrganizationId);
				}
				this.publicFolderSessionCache = new PublicFolderSessionCache(configurationSession.SessionSettings.CurrentOrganizationId, null, null, CultureInfo.InvariantCulture, string.Format("Client=Management;Action={0}", action), null, null, true);
				this.PublicFolderSession = this.publicFolderSessionCache.GetPublicFolderSession(mailboxGuid);
				disposeGuard.Success();
			}
		}

		// Token: 0x17001ADD RID: 6877
		// (get) Token: 0x0600617E RID: 24958 RVA: 0x0019B5E0 File Offset: 0x001997E0
		// (set) Token: 0x0600617F RID: 24959 RVA: 0x0019B5ED File Offset: 0x001997ED
		public PublicFolderSession PublicFolderSession
		{
			get
			{
				return (PublicFolderSession)base.StoreSession;
			}
			private set
			{
				base.StoreSession = value;
			}
		}

		// Token: 0x06006180 RID: 24960 RVA: 0x0019B5F8 File Offset: 0x001997F8
		protected override void InternalSave(ConfigurableObject instance)
		{
			PublicFolder publicFolder = instance as PublicFolder;
			if (publicFolder == null)
			{
				throw new NotSupportedException("Save: " + instance.GetType().FullName);
			}
			FolderSaveResult folderSaveResult = null;
			switch (publicFolder.ObjectState)
			{
			case ObjectState.New:
				try
				{
					using (Folder folder = Folder.Create(this.PublicFolderSession, publicFolder.InternalParentFolderIdentity, ObjectClass.GetObjectType(publicFolder.FolderClass), publicFolder.Name, CreateMode.CreateNew))
					{
						publicFolder.SaveDataToXso(folder, new ReadOnlyCollection<XsoDriverPropertyDefinition>(new List<XsoDriverPropertyDefinition>
						{
							PublicFolderSchema.Name,
							MailboxFolderSchema.InternalParentFolderIdentity
						}));
						MailboxFolderDataProvider.ValidateXsoObjectAndThrowForError(publicFolder.Name, folder, publicFolder.Schema);
						folderSaveResult = folder.Save();
						publicFolder.OrganizationId = this.PublicFolderSession.OrganizationId;
					}
					goto IL_157;
				}
				catch (ObjectExistedException innerException)
				{
					throw new ObjectExistedException(ServerStrings.ErrorFolderAlreadyExists(publicFolder.Name), innerException);
				}
				break;
			case ObjectState.Unchanged:
				goto IL_157;
			case ObjectState.Changed:
				break;
			case ObjectState.Deleted:
				goto IL_147;
			default:
				goto IL_157;
			}
			using (Folder folder2 = Folder.Bind(this.PublicFolderSession, publicFolder.InternalFolderIdentity, null))
			{
				publicFolder.SaveDataToXso(folder2, new ReadOnlyCollection<XsoDriverPropertyDefinition>(new List<XsoDriverPropertyDefinition>
				{
					MailboxFolderSchema.InternalParentFolderIdentity
				}));
				MailboxFolderDataProvider.ValidateXsoObjectAndThrowForError(publicFolder.Name, folder2, publicFolder.Schema);
				folderSaveResult = folder2.Save();
				goto IL_157;
			}
			IL_147:
			throw new InvalidOperationException(ServerStrings.ExceptionObjectHasBeenDeleted);
			IL_157:
			if (folderSaveResult != null && folderSaveResult.OperationResult != OperationResult.Succeeded && folderSaveResult.PropertyErrors.Length > 0)
			{
				foreach (PropertyError propertyError in folderSaveResult.PropertyErrors)
				{
					if (propertyError.PropertyErrorCode == PropertyErrorCode.FolderNameConflict)
					{
						throw new ObjectExistedException(ServerStrings.ErrorFolderAlreadyExists(publicFolder.Name));
					}
				}
				throw folderSaveResult.ToException(ServerStrings.ErrorFolderSave(instance.Identity.ToString(), folderSaveResult.ToString()));
			}
		}

		// Token: 0x06006181 RID: 24961 RVA: 0x0019B7F8 File Offset: 0x001999F8
		protected override void InternalDelete(ConfigurableObject instance)
		{
			PublicFolder publicFolder = instance as PublicFolder;
			if (publicFolder == null)
			{
				throw new NotSupportedException(ServerStrings.ExceptionIsNotPublicFolder(instance.GetType().FullName));
			}
			if (publicFolder.ObjectState == ObjectState.Deleted)
			{
				throw new InvalidOperationException(ServerStrings.ExceptionObjectHasBeenDeleted);
			}
			AggregateOperationResult aggregateOperationResult = this.PublicFolderSession.Delete(DeleteItemFlags.HardDelete, new StoreId[]
			{
				this.PublicFolderSession.IdConverter.GetSessionSpecificId(publicFolder.InternalFolderIdentity.ObjectId)
			});
			if (aggregateOperationResult != null && aggregateOperationResult.OperationResult != OperationResult.Succeeded)
			{
				StringBuilder stringBuilder = new StringBuilder();
				foreach (GroupOperationResult groupOperationResult in aggregateOperationResult.GroupOperationResults)
				{
					if (groupOperationResult.OperationResult != OperationResult.Succeeded && groupOperationResult.Exception != null)
					{
						stringBuilder.AppendLine(groupOperationResult.Exception.ToString());
					}
				}
				throw new StoragePermanentException(ServerStrings.ErrorFailedToDeletePublicFolder(publicFolder.Identity.ToString(), stringBuilder.ToString()));
			}
		}

		// Token: 0x06006182 RID: 24962 RVA: 0x0019C090 File Offset: 0x0019A290
		protected override IEnumerable<T> InternalFindPaged<T>(QueryFilter filter, ObjectId rootId, bool deepSearch, SortBy sortBy, int pageSize)
		{
			if (sortBy != null)
			{
				throw new NotSupportedException("sortBy");
			}
			if (rootId != null && !(rootId is PublicFolderId))
			{
				throw new NotSupportedException("rootId: " + rootId.GetType().FullName);
			}
			if (!typeof(PublicFolder).GetTypeInfo().IsAssignableFrom(typeof(T).GetTypeInfo()))
			{
				throw new NotSupportedException("FindPaged: " + typeof(T).FullName);
			}
			if (filter == null)
			{
				filter = PublicFolderDataProvider.nonHiddenFilter;
			}
			else
			{
				filter = new AndFilter(new QueryFilter[]
				{
					filter,
					PublicFolderDataProvider.nonHiddenFilter
				});
			}
			Dictionary<StoreObjectId, MapiFolderPath> knownFolderPathsCache = new Dictionary<StoreObjectId, MapiFolderPath>();
			knownFolderPathsCache.Add(this.PublicFolderSession.GetPublicFolderRootId(), MapiFolderPath.IpmSubtreeRoot);
			knownFolderPathsCache.Add(this.PublicFolderSession.GetIpmSubtreeFolderId(), MapiFolderPath.IpmSubtreeRoot);
			knownFolderPathsCache.Add(this.PublicFolderSession.GetNonIpmSubtreeFolderId(), MapiFolderPath.NonIpmSubtreeRoot);
			StoreObjectId xsoRootIdentity;
			MapiFolderPath xsoRootFolderPath;
			if (rootId == null)
			{
				xsoRootIdentity = this.PublicFolderSession.GetIpmSubtreeFolderId();
				xsoRootFolderPath = MapiFolderPath.IpmSubtreeRoot;
			}
			else
			{
				PublicFolderId publicFolderId = (PublicFolderId)rootId;
				if (publicFolderId.StoreObjectId == null)
				{
					StoreObjectId storeObjectId = this.ResolveStoreObjectIdFromFolderPath(publicFolderId.MapiFolderPath);
					if (storeObjectId == null)
					{
						yield break;
					}
					xsoRootIdentity = storeObjectId;
				}
				else
				{
					xsoRootIdentity = publicFolderId.StoreObjectId;
				}
				xsoRootFolderPath = publicFolderId.MapiFolderPath;
			}
			PublicFolder rootFolder = new PublicFolder();
			PropertyDefinition[] xsoProperties = rootFolder.Schema.AllDependentXsoProperties;
			Folder xsoFolder = Folder.Bind(this.PublicFolderSession, xsoRootIdentity, xsoProperties);
			rootFolder.LoadDataFromXso(this.PublicFolderSession.MailboxPrincipal.ObjectId, xsoFolder);
			rootFolder.SetDefaultFolderType(DefaultFolderType.None);
			rootFolder.OrganizationId = this.PublicFolderSession.OrganizationId;
			StoreObjectId receoverableItemsDeletionFolderId = PublicFolderCOWSession.GetRecoverableItemsDeletionsFolderId(xsoFolder);
			rootFolder.DumpsterEntryId = ((receoverableItemsDeletionFolderId != null) ? receoverableItemsDeletionFolderId.ToHexEntryId() : null);
			if (null == xsoRootFolderPath)
			{
				xsoRootFolderPath = MailboxFolderDataProvider.CalculateMailboxFolderPath(this.PublicFolderSession, rootFolder.InternalFolderIdentity.ObjectId, rootFolder.InternalParentFolderIdentity, rootFolder.Name, knownFolderPathsCache);
			}
			else if (!knownFolderPathsCache.ContainsKey(rootFolder.InternalFolderIdentity.ObjectId))
			{
				knownFolderPathsCache.Add(rootFolder.InternalFolderIdentity.ObjectId, xsoRootFolderPath);
			}
			rootFolder.FolderPath = xsoRootFolderPath;
			if (deepSearch)
			{
				yield return (T)((object)rootFolder);
			}
			QueryResult queryResults = xsoFolder.FolderQuery(deepSearch ? FolderQueryFlags.DeepTraversal : FolderQueryFlags.None, filter, null, new PropertyDefinition[]
			{
				FolderSchema.Id
			});
			for (;;)
			{
				object[][] folderRows = queryResults.GetRows((pageSize == 0) ? 1000 : pageSize);
				if (folderRows.Length <= 0)
				{
					break;
				}
				foreach (object[] row in folderRows)
				{
					PublicFolder onePublicFolder = new PublicFolder();
					using (Folder oneXsoFolder = Folder.Bind(this.PublicFolderSession, ((VersionedId)row[0]).ObjectId, xsoProperties))
					{
						onePublicFolder.LoadDataFromXso(this.PublicFolderSession.MailboxPrincipal.ObjectId, oneXsoFolder);
						onePublicFolder.SetDefaultFolderType(DefaultFolderType.None);
						onePublicFolder.OrganizationId = this.PublicFolderSession.OrganizationId;
						receoverableItemsDeletionFolderId = PublicFolderCOWSession.GetRecoverableItemsDeletionsFolderId(oneXsoFolder);
						onePublicFolder.DumpsterEntryId = ((receoverableItemsDeletionFolderId != null) ? receoverableItemsDeletionFolderId.ToHexEntryId() : null);
						onePublicFolder.FolderPath = MailboxFolderDataProvider.CalculateMailboxFolderPath(this.PublicFolderSession, onePublicFolder.InternalFolderIdentity.ObjectId, onePublicFolder.InternalParentFolderIdentity, onePublicFolder.Name, knownFolderPathsCache);
						yield return (T)((object)onePublicFolder);
					}
				}
			}
			yield break;
		}

		// Token: 0x06006183 RID: 24963 RVA: 0x0019C0D2 File Offset: 0x0019A2D2
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<PublicFolderDataProvider>(this);
		}

		// Token: 0x06006184 RID: 24964 RVA: 0x0019C0DC File Offset: 0x0019A2DC
		public StoreObjectId ResolveStoreObjectIdFromFolderPath(MapiFolderPath folderPath)
		{
			Util.ThrowOnNullArgument(folderPath, "folderPath");
			StoreObjectId storeObjectId;
			if (folderPath.IsNonIpmPath)
			{
				storeObjectId = this.PublicFolderSession.GetNonIpmSubtreeFolderId();
			}
			else
			{
				storeObjectId = this.PublicFolderSession.GetIpmSubtreeFolderId();
			}
			if (folderPath.Depth <= 0)
			{
				return storeObjectId;
			}
			foreach (string text in folderPath)
			{
				QueryFilter seekFilter = new TextFilter(FolderSchema.DisplayName, text, MatchOptions.FullString, MatchFlags.IgnoreCase);
				using (Folder folder = Folder.Bind(this.PublicFolderSession, storeObjectId, PublicFolderDataProvider.FolderQueryReturnColumns))
				{
					using (QueryResult queryResult = folder.FolderQuery(FolderQueryFlags.None, null, PublicFolderDataProvider.FolderQuerySorts, PublicFolderDataProvider.FolderQueryReturnColumns))
					{
						if (!queryResult.SeekToCondition(SeekReference.OriginBeginning, seekFilter, SeekToConditionFlags.AllowExtendedFilters))
						{
							return null;
						}
						object[][] rows = queryResult.GetRows(1);
						storeObjectId = ((VersionedId)rows[0][0]).ObjectId;
					}
				}
			}
			return storeObjectId;
		}

		// Token: 0x06006185 RID: 24965 RVA: 0x0019C1F8 File Offset: 0x0019A3F8
		protected override void InternalDispose(bool disposing)
		{
			if (disposing)
			{
				if (this.publicFolderSessionCache != null)
				{
					this.publicFolderSessionCache.Dispose();
					this.publicFolderSessionCache = null;
				}
				this.PublicFolderSession = null;
			}
			base.InternalDispose(disposing);
		}

		// Token: 0x04003749 RID: 14153
		private static readonly ComparisonFilter nonHiddenFilter = new ComparisonFilter(ComparisonOperator.NotEqual, FolderSchema.IsHidden, true);

		// Token: 0x0400374A RID: 14154
		private static readonly PropertyDefinition[] FolderQueryReturnColumns = new PropertyDefinition[]
		{
			FolderSchema.Id,
			FolderSchema.DisplayName
		};

		// Token: 0x0400374B RID: 14155
		private static readonly SortBy[] FolderQuerySorts = new SortBy[]
		{
			new SortBy(FolderSchema.DisplayName, SortOrder.Ascending)
		};

		// Token: 0x0400374C RID: 14156
		private OrganizationId currentOrganizationId;

		// Token: 0x0400374D RID: 14157
		private PublicFolderSessionCache publicFolderSessionCache;

		// Token: 0x02000A6A RID: 2666
		private enum FolderQueryReturnColumnIndex
		{
			// Token: 0x0400374F RID: 14159
			Id,
			// Token: 0x04003750 RID: 14160
			DisplayName
		}
	}
}
