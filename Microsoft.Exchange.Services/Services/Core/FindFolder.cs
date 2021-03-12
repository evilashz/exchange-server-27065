using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.Services.Core.Search;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x020002EA RID: 746
	internal sealed class FindFolder : MultiStepServiceCommand<FindFolderRequest, FindFolderParentWrapper>
	{
		// Token: 0x060014E2 RID: 5346 RVA: 0x000693B5 File Offset: 0x000675B5
		public FindFolder(CallContext callContext, FindFolderRequest request) : base(callContext, request)
		{
		}

		// Token: 0x060014E3 RID: 5347 RVA: 0x000693C0 File Offset: 0x000675C0
		internal override void PreExecuteCommand()
		{
			this.responseShape = Global.ResponseShapeResolver.GetResponseShape<FolderResponseShape>(base.Request.ShapeName, base.Request.FolderShape, base.CallContext.FeaturesManager);
			this.paging = base.Request.Paging;
			this.restriction = base.Request.Restriction;
			this.rootFolders = base.Request.ParentFolderIds;
			this.traversalType = base.Request.Traversal;
			this.returnParentFolder = base.Request.ReturnParentFolder;
			this.requiredFolders = base.Request.RequiredFolders;
			this.foldersToMoveToTop = base.Request.FoldersToMoveToTop;
			if (base.Request.MailboxGuid != Guid.Empty)
			{
				this.mailboxId = new MailboxId(base.Request.MailboxGuid);
			}
			else
			{
				ServiceCommandBase.ThrowIfNullOrEmpty<BaseFolderId>(this.rootFolders, "rootFolders", "FindFolder::PreExecuteCommand");
			}
			ServiceCommandBase.ThrowIfNull(this.responseShape, "responseShape", "FindFolder::PreExecuteCommand");
			BasePagingType.Validate(this.paging);
			int num;
			if ((this.paging == null || !this.paging.BudgetInducedTruncationAllowed) && !CallContext.Current.Budget.CanAllocateFoundObjects(1U, out num))
			{
				ExceededFindCountLimitException.Throw();
			}
			this.classDeterminer = PropertyListForViewRowDeterminer.BuildForFolders(this.responseShape);
			if (this.paging != null && !(this.paging is IndexedPageView) && !(this.paging is FractionalPageView))
			{
				this.paging = null;
			}
			ServiceObjectToFilterConverter serviceObjectToFilterConverter = new ServiceObjectToFilterConverter();
			this.query = ((this.restriction != null && this.restriction.Item != null) ? serviceObjectToFilterConverter.Convert(this.restriction.Item) : null);
			this.query = BasePagingType.ApplyQueryAppend(this.query, this.paging);
		}

		// Token: 0x1700028A RID: 650
		// (get) Token: 0x060014E4 RID: 5348 RVA: 0x0006958D File Offset: 0x0006778D
		internal override int StepCount
		{
			get
			{
				if (this.rootFolders == null)
				{
					return 1;
				}
				return this.rootFolders.Count;
			}
		}

		// Token: 0x060014E5 RID: 5349 RVA: 0x000695A4 File Offset: 0x000677A4
		internal override IExchangeWebMethodResponse GetResponse()
		{
			return FindFolderResponse.CreateResponseForFindFolder(base.Results, new FindFolderResponse.CreateFindFolderResponse(FindFolderResponse.CreateResponse));
		}

		// Token: 0x060014E6 RID: 5350 RVA: 0x000695C0 File Offset: 0x000677C0
		internal override ServiceResult<FindFolderParentWrapper> Execute()
		{
			IdAndSession folderIdAndSession;
			if (this.rootFolders != null)
			{
				folderIdAndSession = base.IdConverter.ConvertFolderIdToIdAndSession(this.rootFolders[base.CurrentStep], IdConverter.ConvertOption.IgnoreChangeKey | IdConverter.ConvertOption.AllowKnownExternalUsers | (base.Request.IsHierarchicalOperation ? IdConverter.ConvertOption.IsHierarchicalOperation : IdConverter.ConvertOption.None));
			}
			else
			{
				folderIdAndSession = base.IdConverter.ConvertDefaultFolderType(DefaultFolderType.Root, this.mailboxId, false);
			}
			return this.FindFoldersInParent(folderIdAndSession);
		}

		// Token: 0x060014E7 RID: 5351 RVA: 0x00069624 File Offset: 0x00067824
		private ServiceResult<FindFolderParentWrapper> FindFoldersInParent(IdAndSession folderIdAndSession)
		{
			this.ValidateFindFolderOperation(folderIdAndSession);
			PropertyDefinition[] propertiesToFetch = this.classDeterminer.GetPropertiesToFetch();
			PropertyDefinition[] array = this.returnParentFolder ? propertiesToFetch : null;
			Folder folder = null;
			bool flag = false;
			ServiceResult<FindFolderParentWrapper> result;
			try
			{
				using (Folder folder2 = Folder.Bind(folderIdAndSession.Session, folderIdAndSession.Id, array))
				{
					folder = folder2;
					if (this.traversalType == FolderQueryTraversal.SoftDeleted && folderIdAndSession.Session.IsPublicFolderSession)
					{
						flag = true;
						StoreObjectId recoverableItemsDeletionsFolderId = PublicFolderCOWSession.GetRecoverableItemsDeletionsFolderId(folder2);
						if (recoverableItemsDeletionsFolderId == null)
						{
							throw new ServiceInvalidOperationException((CoreResources.IDs)3395659933U);
						}
						folder = Folder.Bind(folderIdAndSession.Session, recoverableItemsDeletionsFolderId);
						this.traversalType = FolderQueryTraversal.Shallow;
					}
					FindFolderParentWrapper findFolderParentWrapper = this.ExecuteFindFolderQuery(folderIdAndSession, propertiesToFetch, folder, this.traversalType, this.paging, this.returnParentFolder);
					if (this.ShouldReturnRequiredDistinguishedFolders(folderIdAndSession, findFolderParentWrapper))
					{
						List<DistinguishedFolderIdName> missingRequiredFolders = this.GetMissingRequiredFolders(findFolderParentWrapper);
						if (missingRequiredFolders.Count > 0)
						{
							List<BaseFolderType> list = this.LoadMissingRequiredFolders(array, missingRequiredFolders);
							if (list.Count > 0)
							{
								List<BaseFolderType> list2 = new List<BaseFolderType>(findFolderParentWrapper.Folders);
								list2.AddRange(list);
								findFolderParentWrapper.Folders = list2.ToArray();
							}
						}
					}
					if (this.foldersToMoveToTop != null && this.foldersToMoveToTop.Length > 0)
					{
						findFolderParentWrapper.Folders = FindFolderResponseProcessor.ShiftDefaultFoldersToTop(findFolderParentWrapper.Folders, this.foldersToMoveToTop);
					}
					result = new ServiceResult<FindFolderParentWrapper>(findFolderParentWrapper);
				}
			}
			catch (FilterNotSupportedException ex)
			{
				throw new UnsupportedPathForQueryException(ex.Properties[0], ex);
			}
			finally
			{
				if (flag && folder != null)
				{
					folder.Dispose();
				}
			}
			return result;
		}

		// Token: 0x060014E8 RID: 5352 RVA: 0x000697E0 File Offset: 0x000679E0
		private FindFolderParentWrapper ExecuteFindFolderQuery(IdAndSession folderIdAndSession, PropertyDefinition[] propsToFetch, Folder rootFolder, FolderQueryTraversal folderTraversal, BasePagingType folderPaging, bool includeParentFolder)
		{
			FindFolderParentWrapper result = null;
			using (QueryResult queryResult = rootFolder.FolderQuery((FolderQueryFlags)folderTraversal, this.query, null, propsToFetch))
			{
				BasePageResult basePageResult = BasePagingType.ApplyPostQueryPaging(queryResult, folderPaging);
				BaseFolderType[] array = basePageResult.View.ConvertToFolderObjects(propsToFetch, this.classDeterminer, folderIdAndSession);
				if (folderIdAndSession.Session is MailboxSession)
				{
					string clientInfoString = folderIdAndSession.Session.ClientInfoString;
					if ((clientInfoString.Equals("Client=OWA", StringComparison.OrdinalIgnoreCase) || clientInfoString.StartsWith("Client=OWA;", StringComparison.OrdinalIgnoreCase)) && folderIdAndSession.Id.Equals(folderIdAndSession.Session.GetDefaultFolderId(DefaultFolderType.Root)))
					{
						array = this.AddFromFavoriteSendersFolder(array);
					}
				}
				BaseFolderType baseFolderType = null;
				if (includeParentFolder)
				{
					StoreObjectId storeObjectId = StoreId.GetStoreObjectId(folderIdAndSession.Id);
					baseFolderType = BaseFolderType.CreateFromStoreObjectType(storeObjectId.ObjectType);
					ServiceCommandBase.LoadServiceObject(baseFolderType, rootFolder, folderIdAndSession, this.responseShape, null);
				}
				result = new FindFolderParentWrapper(array, baseFolderType, basePageResult);
			}
			return result;
		}

		// Token: 0x060014E9 RID: 5353 RVA: 0x000698D0 File Offset: 0x00067AD0
		private bool ShouldReturnRequiredDistinguishedFolders(IdAndSession folderIdAndSession, FindFolderParentWrapper findFolderResponse)
		{
			return this.requiredFolders != null && this.requiredFolders.Length > 0 && !findFolderResponse.IncludesLastItemInRange && findFolderResponse.TotalItemsInView > Global.FindCountLimit && this.traversalType == FolderQueryTraversal.Deep && folderIdAndSession.Id.Equals(folderIdAndSession.Session.SafeGetDefaultFolderId(DefaultFolderType.Root));
		}

		// Token: 0x060014EA RID: 5354 RVA: 0x00069928 File Offset: 0x00067B28
		private List<BaseFolderType> LoadMissingRequiredFolders(PropertyDefinition[] folderPropsToReturn, List<DistinguishedFolderIdName> requiredFoldersToBind)
		{
			List<BaseFolderType> list = new List<BaseFolderType>();
			foreach (DistinguishedFolderIdName distinguishedFolderIdName in requiredFoldersToBind)
			{
				try
				{
					IdAndSession idAndSession = base.IdConverter.ConvertFolderIdToIdAndSessionReadOnly(new DistinguishedFolderId
					{
						Id = distinguishedFolderIdName
					});
					using (Folder folder = Folder.Bind(idAndSession.Session, idAndSession.Id, folderPropsToReturn))
					{
						BaseFolderType baseFolderType = BaseFolderType.CreateFromStoreObjectType(idAndSession.GetAsStoreObjectId().ObjectType);
						ServiceCommandBase.LoadServiceObject(baseFolderType, folder, idAndSession, this.responseShape, null);
						list.Add(baseFolderType);
					}
				}
				catch (FolderNotFoundException)
				{
					ExTraceGlobals.ExceptionTracer.TraceError<DistinguishedFolderIdName>((long)this.GetHashCode(), "BindAndLoadRequiredFolders: Default folder {0} does not exist.", distinguishedFolderIdName);
				}
			}
			return list;
		}

		// Token: 0x060014EB RID: 5355 RVA: 0x00069A18 File Offset: 0x00067C18
		private List<DistinguishedFolderIdName> GetMissingRequiredFolders(FindFolderParentWrapper findFolderResponse)
		{
			List<DistinguishedFolderIdName> list = new List<DistinguishedFolderIdName>(this.requiredFolders);
			foreach (BaseFolderType baseFolderType in findFolderResponse.Folders)
			{
				if (!string.IsNullOrEmpty(baseFolderType.DistinguishedFolderId))
				{
					DistinguishedFolderIdName item = EnumUtilities.Parse<DistinguishedFolderIdName>(baseFolderType.DistinguishedFolderId);
					list.Remove(item);
				}
			}
			return list;
		}

		// Token: 0x060014EC RID: 5356 RVA: 0x00069A71 File Offset: 0x00067C71
		private void ValidateFindFolderOperation(IdAndSession folderIdAndSession)
		{
			if (folderIdAndSession.Session is PublicFolderSession && this.traversalType == FolderQueryTraversal.Deep)
			{
				throw new ServiceInvalidOperationException((CoreResources.IDs)4039615479U);
			}
		}

		// Token: 0x060014ED RID: 5357 RVA: 0x00069A9C File Offset: 0x00067C9C
		private BaseFolderType[] AddFromFavoriteSendersFolder(BaseFolderType[] foundFolders)
		{
			ExTraceGlobals.FindFolderCallTracer.TraceFunction((long)this.GetHashCode(), "FindFolder.AddFromFavoriteSendersFolder");
			try
			{
				if (base.CallContext.AccessingPrincipal != null && base.CallContext.AccessingPrincipal.GetConfiguration().OwaClientServer.PeopleCentricTriage.Enabled)
				{
					string text = base.CallContext.OwaExplicitLogonUser;
					if (string.IsNullOrWhiteSpace(text))
					{
						text = base.CallContext.GetEffectiveAccessingSmtpAddress();
					}
					IdAndSession idAndSession = base.IdConverter.ConvertDefaultFolderType(DefaultFolderType.FromFavoriteSenders, text);
					using (SearchFolder searchFolder = SearchFolder.Bind(idAndSession.Session, idAndSession.Id, this.classDeterminer.GetPropertiesToFetch()))
					{
						if (!searchFolder.IsPopulated())
						{
							ExTraceGlobals.FindFolderCallTracer.TraceDebug((long)this.GetHashCode(), "FromFavoriteSenders folder not yet populated.  Skipped.");
							return foundFolders;
						}
						SearchFolderType searchFolderType = new SearchFolderType();
						ServiceCommandBase.LoadServiceObject(searchFolderType, searchFolder, idAndSession, this.responseShape, null);
						int newSize = (foundFolders == null) ? 1 : (foundFolders.Length + 1);
						Array.Resize<BaseFolderType>(ref foundFolders, newSize);
						foundFolders[foundFolders.Length - 1] = searchFolderType;
					}
				}
			}
			catch (WrongServerVersionException arg)
			{
				ExTraceGlobals.FindFolderCallTracer.TraceError<WrongServerVersionException>(0L, "Unable to add from favorites folder due to WrongServerVersionException. Message: {0}", arg);
			}
			catch (StorageTransientException arg2)
			{
				ExTraceGlobals.FindFolderCallTracer.TraceError<StorageTransientException>(0L, "Unable to add from favorites folder due to StorageTransientException. Message: {0}", arg2);
			}
			catch (StoragePermanentException arg3)
			{
				ExTraceGlobals.FindFolderCallTracer.TraceError<StoragePermanentException>(0L, "Unable to add from favorites folder due to StoragePermanentException. Message: {0}", arg3);
			}
			return foundFolders;
		}

		// Token: 0x04000E23 RID: 3619
		private FolderResponseShape responseShape;

		// Token: 0x04000E24 RID: 3620
		private BasePagingType paging;

		// Token: 0x04000E25 RID: 3621
		private RestrictionType restriction;

		// Token: 0x04000E26 RID: 3622
		private IList<BaseFolderId> rootFolders;

		// Token: 0x04000E27 RID: 3623
		private FolderQueryTraversal traversalType;

		// Token: 0x04000E28 RID: 3624
		private PropertyListForViewRowDeterminer classDeterminer;

		// Token: 0x04000E29 RID: 3625
		private QueryFilter query;

		// Token: 0x04000E2A RID: 3626
		private bool returnParentFolder;

		// Token: 0x04000E2B RID: 3627
		private DistinguishedFolderIdName[] requiredFolders;

		// Token: 0x04000E2C RID: 3628
		private DistinguishedFolderIdName[] foldersToMoveToTop;

		// Token: 0x04000E2D RID: 3629
		private MailboxId mailboxId;
	}
}
