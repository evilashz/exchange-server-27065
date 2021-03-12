using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.PublicFolder;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x0200034A RID: 842
	internal sealed class MoveFolder : MoveCopyFolderCommandBase
	{
		// Token: 0x060017BE RID: 6078 RVA: 0x0007F644 File Offset: 0x0007D844
		public MoveFolder(CallContext callContext, MoveFolderRequest request) : base(callContext, request)
		{
		}

		// Token: 0x060017BF RID: 6079 RVA: 0x0007F64E File Offset: 0x0007D84E
		protected override BaseInfoResponse CreateResponse()
		{
			return new MoveFolderResponse();
		}

		// Token: 0x060017C0 RID: 6080 RVA: 0x0007F658 File Offset: 0x0007D858
		protected override AggregateOperationResult DoOperation(StoreSession destinationSession, StoreSession sourceSession, StoreId sourceId)
		{
			return sourceSession.Move(destinationSession, this.destinationFolder.Id, new StoreId[]
			{
				sourceId
			});
		}

		// Token: 0x060017C1 RID: 6081 RVA: 0x0007F684 File Offset: 0x0007D884
		protected override ServiceResult<BaseFolderType> MoveCopyObject(IdAndSession idAndSession)
		{
			PublicFolderSession publicFolderSession = idAndSession.Session as PublicFolderSession;
			if (publicFolderSession == null || publicFolderSession.IsPrimaryHierarchySession)
			{
				return base.MoveCopyObject(idAndSession);
			}
			return this.RemoteMovePublicFolder(idAndSession);
		}

		// Token: 0x060017C2 RID: 6082 RVA: 0x0007F6B8 File Offset: 0x0007D8B8
		protected override void SubclassValidateOperation(StoreSession storeSession, IdAndSession idAndSession)
		{
			if ((idAndSession.Session is PublicFolderSession && this.destinationFolder.Session is MailboxSession) || (idAndSession.Session is MailboxSession && this.destinationFolder.Session is PublicFolderSession))
			{
				throw new ServiceInvalidOperationException((CoreResources.IDs)3206878473U);
			}
			if (idAndSession.Session is MailboxSession)
			{
				MailboxSession mailboxSession = idAndSession.Session as MailboxSession;
				DefaultFolderType defaultFolderType = mailboxSession.IsDefaultFolderType(idAndSession.Id);
				if (defaultFolderType != DefaultFolderType.None)
				{
					throw new MoveDistinguishedFolderException();
				}
			}
		}

		// Token: 0x060017C3 RID: 6083 RVA: 0x0007F744 File Offset: 0x0007D944
		private ServiceResult<BaseFolderType> RemoteMovePublicFolder(IdAndSession idAndSession)
		{
			ServiceResult<BaseFolderType> serviceResult = RemotePublicFolderOperations.MoveFolder(base.CallContext, idAndSession, this.destinationFolder.Id.ObjectId);
			if (serviceResult.Error != null)
			{
				return serviceResult;
			}
			StoreObjectId storeObjectId = StoreId.GetStoreObjectId(idAndSession.Id);
			bool flag = false;
			try
			{
				PublicFolderSyncJobRpc.SyncFolder(((PublicFolderSession)idAndSession.Session).MailboxPrincipal, storeObjectId.ProviderLevelItemId);
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
			ServiceResult<BaseFolderType> result;
			using (Folder folder = Folder.Bind(idAndSession.Session, storeObjectId))
			{
				BaseFolderType baseFolderType = BaseFolderType.CreateFromStoreObjectType(storeObjectId.ObjectType);
				baseFolderType.FolderId = base.GetServiceFolderIdFromStoreId(StoreId.GetStoreObjectId(folder.Id), idAndSession);
				result = new ServiceResult<BaseFolderType>(baseFolderType);
			}
			return result;
		}
	}
}
