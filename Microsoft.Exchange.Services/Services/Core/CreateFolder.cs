using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.PublicFolder;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.Services.Core.DataConverter;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x020002BD RID: 701
	internal sealed class CreateFolder : MultiStepServiceCommand<CreateFolderRequest, BaseFolderType>
	{
		// Token: 0x060012F7 RID: 4855 RVA: 0x0005C76C File Offset: 0x0005A96C
		public CreateFolder(CallContext callContext, CreateFolderRequest request) : base(callContext, request)
		{
			this.parentTargetFolderId = request.ParentFolderId.BaseFolderId;
			this.folders = request.Folders;
			this.responseShape = ServiceCommandBase.DefaultFolderResponseShape;
			ServiceCommandBase.ThrowIfNull(this.parentTargetFolderId, "this.parentTargetFolderId", "CreateFolder::Execute");
			ServiceCommandBase.ThrowIfNullOrEmpty<BaseFolderType>(this.folders, "this.folders", "CreateFolder::Execute");
			ServiceCommandBase.ThrowIfNull(this.responseShape, "this.responseShape", "CreateFolder::Execute");
		}

		// Token: 0x060012F8 RID: 4856 RVA: 0x0005C7E8 File Offset: 0x0005A9E8
		internal override IExchangeWebMethodResponse GetResponse()
		{
			CreateFolderResponse createFolderResponse = new CreateFolderResponse();
			createFolderResponse.BuildForResults<BaseFolderType>(base.Results);
			return createFolderResponse;
		}

		// Token: 0x17000257 RID: 599
		// (get) Token: 0x060012F9 RID: 4857 RVA: 0x0005C808 File Offset: 0x0005AA08
		internal override int StepCount
		{
			get
			{
				return base.Request.Folders.Length;
			}
		}

		// Token: 0x060012FA RID: 4858 RVA: 0x0005C818 File Offset: 0x0005AA18
		private static Folder CreateFolderBasedOnStoreObjectType(IdAndSession parentIdAndSession, StoreObjectType storeObjectType, BaseFolderType folder)
		{
			Folder result;
			if (storeObjectType == StoreObjectType.SearchFolder)
			{
				if (!(parentIdAndSession.Session is MailboxSession))
				{
					throw new InvalidFolderTypeForOperationException(CoreResources.IDs.ErrorCannotCreateSearchFolderInPublicFolder);
				}
				MailboxSession mailboxSession = parentIdAndSession.Session as MailboxSession;
				DefaultFolderType defaultFolderType = mailboxSession.IsDefaultFolderType(parentIdAndSession.Id);
				if (defaultFolderType == DefaultFolderType.SearchFolders)
				{
					result = OutlookSearchFolder.Create(mailboxSession, "New Outlook Search Folder");
				}
				else
				{
					result = SearchFolder.Create(mailboxSession, parentIdAndSession.Id);
				}
			}
			else if (folder.DistinguishedFolderIdSpecified)
			{
				if (!IdConverter.IsDefaultFolderCreateSupported(folder.DistinguishedFolderId))
				{
					throw new ServiceInvalidOperationException(CoreResources.IDs.ErrorCreateDistinguishedFolder);
				}
				MailboxSession mailboxSession2 = parentIdAndSession.Session as MailboxSession;
				DefaultFolderType defaultFolderTypeFromDistinguishedFolderIdNameType = IdConverter.GetDefaultFolderTypeFromDistinguishedFolderIdNameType(folder.DistinguishedFolderId);
				StoreObjectId storeObjectId;
				if (DefaultFolderType.AdminAuditLogs == defaultFolderTypeFromDistinguishedFolderIdNameType)
				{
					storeObjectId = mailboxSession2.GetAdminAuditLogsFolderId();
				}
				else
				{
					storeObjectId = mailboxSession2.GetDefaultFolderId(defaultFolderTypeFromDistinguishedFolderIdNameType);
				}
				if (storeObjectId != null)
				{
					throw new ServiceInvalidOperationException(CoreResources.IDs.ErrorFolderExists);
				}
				StoreObjectId folderId = mailboxSession2.CreateDefaultFolder(defaultFolderTypeFromDistinguishedFolderIdNameType);
				result = Folder.Bind(mailboxSession2, folderId);
				folder.PropertyBag.Remove(BaseFolderSchema.DistinguishedFolderId);
				folder.PropertyBag.Remove(BaseFolderSchema.DisplayName);
			}
			else
			{
				result = Folder.Create(parentIdAndSession.Session, parentIdAndSession.Id, storeObjectType);
			}
			return result;
		}

		// Token: 0x060012FB RID: 4859 RVA: 0x0005C944 File Offset: 0x0005AB44
		internal override void PreExecuteCommand()
		{
			try
			{
				this.parentIdAndSession = base.IdConverter.ConvertTargetFolderIdToIdAndHierarchySession(this.parentTargetFolderId, true);
			}
			catch (ObjectNotFoundException innerException)
			{
				throw new ParentFolderNotFoundException(innerException);
			}
		}

		// Token: 0x060012FC RID: 4860 RVA: 0x0005C984 File Offset: 0x0005AB84
		internal override ServiceResult<BaseFolderType> Execute()
		{
			PublicFolderSession publicFolderSession = this.parentIdAndSession.Session as PublicFolderSession;
			ServiceResult<BaseFolderType> result;
			if (publicFolderSession == null || publicFolderSession.IsPrimaryHierarchySession)
			{
				result = new ServiceResult<BaseFolderType>(this.CreateFolderFromRequestFolder(this.parentIdAndSession, this.folders[base.CurrentStep]));
			}
			else
			{
				result = this.RemoteCreatePublicFolder(this.parentIdAndSession, this.folders[base.CurrentStep]);
			}
			this.objectsChanged++;
			return result;
		}

		// Token: 0x060012FD RID: 4861 RVA: 0x0005CA00 File Offset: 0x0005AC00
		private BaseFolderType CreateFolderFromRequestFolder(IdAndSession parentIdAndSession, BaseFolderType folder)
		{
			BaseFolderType result = null;
			StoreObjectType storeObjectType = folder.StoreObjectType;
			this.ValidateCreate(storeObjectType, folder);
			using (Folder folder2 = CreateFolder.CreateFolderBasedOnStoreObjectType(parentIdAndSession, storeObjectType, folder))
			{
				if (folder2 is SearchFolder)
				{
					result = this.UpdateNewSearchFolder(parentIdAndSession, (SearchFolder)folder2, (SearchFolderType)folder);
				}
				else
				{
					result = this.UpdateNewFolder(parentIdAndSession, folder2, folder);
				}
			}
			return result;
		}

		// Token: 0x060012FE RID: 4862 RVA: 0x0005CA6C File Offset: 0x0005AC6C
		private void ValidateCreate(StoreObjectType storeObjectType, BaseFolderType folder)
		{
			if (storeObjectType != StoreObjectType.Folder)
			{
				this.ConfirmNoFolderClassOverride(folder);
			}
		}

		// Token: 0x060012FF RID: 4863 RVA: 0x0005CA79 File Offset: 0x0005AC79
		private void ConfirmNoFolderClassOverride(BaseFolderType folder)
		{
			if (!string.IsNullOrEmpty(folder.FolderClass))
			{
				throw new NoFolderClassOverrideException();
			}
		}

		// Token: 0x06001300 RID: 4864 RVA: 0x0005CA90 File Offset: 0x0005AC90
		private BaseFolderType UpdateNewSearchFolder(IdAndSession parentIdAndSession, SearchFolder xsoSearchFolder, SearchFolderType searchFolder)
		{
			SearchParametersType searchParameters = searchFolder.SearchParameters;
			if (searchParameters != null)
			{
				searchFolder.SearchParameters = null;
			}
			base.SetProperties(xsoSearchFolder, searchFolder);
			this.SaveXsoFolder(xsoSearchFolder);
			if (searchParameters != null)
			{
				SearchFolderType serviceObject = new SearchFolderType
				{
					SearchParameters = searchParameters
				};
				bool flag = false;
				try
				{
					base.SetProperties(xsoSearchFolder, serviceObject);
					OutlookSearchFolder outlookSearchFolder = xsoSearchFolder as OutlookSearchFolder;
					if (outlookSearchFolder != null)
					{
						outlookSearchFolder.MakeVisibleToOutlook(true);
					}
					this.SaveXsoFolder(xsoSearchFolder);
					flag = true;
				}
				finally
				{
					if (!flag)
					{
						ExTraceGlobals.CreateFolderCallTracer.TraceDebug((long)this.GetHashCode(), "[CreateFolder::UpdateNewSearchFolder] Set was not successful.  Deleting folder.");
						this.DeleteSearchFolder(xsoSearchFolder);
					}
				}
			}
			searchFolder.Clear();
			base.LoadServiceObject(searchFolder, xsoSearchFolder, parentIdAndSession, this.responseShape);
			return searchFolder;
		}

		// Token: 0x06001301 RID: 4865 RVA: 0x0005CB40 File Offset: 0x0005AD40
		private void DeleteSearchFolder(SearchFolder searchFolder)
		{
			AggregateOperationResult aggregateOperationResult;
			try
			{
				aggregateOperationResult = searchFolder.Session.Delete(DeleteItemFlags.HardDelete, new StoreId[]
				{
					searchFolder.Id
				});
			}
			catch (ObjectNotFoundException)
			{
				ExTraceGlobals.CreateFolderCallTracer.TraceDebug((long)this.GetHashCode(), "[CreateFolder::DeleteSearchFolder] Tried deleting bad search folder, but it wasn't there.  Ignoring.");
				return;
			}
			if (aggregateOperationResult.OperationResult != OperationResult.Succeeded && ExTraceGlobals.CreateFolderCallTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				LocalizedException exception = aggregateOperationResult.GroupOperationResults[0].Exception;
				string arg = (exception == null) ? "<NULL>" : string.Format(CultureInfo.InvariantCulture, "Class: {0}, Message: {1}", new object[]
				{
					exception.GetType().FullName,
					exception.Message
				});
				ExTraceGlobals.CreateFolderCallTracer.TraceDebug<string>((long)this.GetHashCode(), "[CreateFolder::DeleteSearchFolder] Attempted deleting search folder, but was not successful. Exception: {0}", arg);
			}
		}

		// Token: 0x06001302 RID: 4866 RVA: 0x0005CC10 File Offset: 0x0005AE10
		private BaseFolderType UpdateNewFolder(IdAndSession parentIdAndSession, Folder xsoFolder, BaseFolderType folder)
		{
			base.SetProperties(xsoFolder, folder);
			this.SaveXsoFolder(xsoFolder);
			folder.Clear();
			base.LoadServiceObject(folder, xsoFolder, parentIdAndSession, this.responseShape);
			return folder;
		}

		// Token: 0x06001303 RID: 4867 RVA: 0x0005CC38 File Offset: 0x0005AE38
		private ServiceResult<BaseFolderType> RemoteCreatePublicFolder(IdAndSession parentIdAndSession, BaseFolderType folderToBeCreated)
		{
			ServiceResult<BaseFolderType> serviceResult = RemotePublicFolderOperations.CreateFolder(base.CallContext, parentIdAndSession, folderToBeCreated);
			if (serviceResult.Error != null)
			{
				return serviceResult;
			}
			StoreObjectId storeObjectId = StoreId.EwsIdToFolderStoreObjectId(serviceResult.Value.FolderId.Id);
			bool flag = false;
			try
			{
				PublicFolderSyncJobRpc.SyncFolder(((PublicFolderSession)parentIdAndSession.Session).MailboxPrincipal, storeObjectId.ProviderLevelItemId);
			}
			catch (PublicFolderSyncTransientException)
			{
				flag = true;
			}
			catch (PublicFolderSyncPermanentException)
			{
				flag = true;
			}
			if (flag)
			{
				return new ServiceResult<BaseFolderType>(new ServiceError((CoreResources.IDs)2636256287U, ResponseCodeType.ErrorInternalServerError, 0, ExchangeVersion.Exchange2012));
			}
			ToServiceObjectPropertyList toServiceObjectPropertyList = XsoDataConverter.GetToServiceObjectPropertyList(parentIdAndSession.Id, parentIdAndSession.Session, this.responseShape, base.ParticipantResolver);
			ServiceResult<BaseFolderType> result;
			using (Folder xsoFolder = ServiceCommandBase.GetXsoFolder(parentIdAndSession.Session, storeObjectId, ref toServiceObjectPropertyList))
			{
				StoreObjectType storeObjectType = serviceResult.Value.StoreObjectType;
				BaseFolderType baseFolderType = BaseFolderType.CreateFromStoreObjectType(storeObjectType);
				base.LoadServiceObject(baseFolderType, xsoFolder, parentIdAndSession, this.responseShape);
				result = new ServiceResult<BaseFolderType>(baseFolderType);
			}
			return result;
		}

		// Token: 0x04000D21 RID: 3361
		private const string OutlookSearchFolderDisplayNamePlaceholder = "New Outlook Search Folder";

		// Token: 0x04000D22 RID: 3362
		private BaseFolderId parentTargetFolderId;

		// Token: 0x04000D23 RID: 3363
		private IList<BaseFolderType> folders;

		// Token: 0x04000D24 RID: 3364
		private FolderResponseShape responseShape;

		// Token: 0x04000D25 RID: 3365
		private IdAndSession parentIdAndSession;
	}
}
