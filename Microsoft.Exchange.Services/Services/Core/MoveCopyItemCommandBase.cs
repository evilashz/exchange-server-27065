using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x020002B7 RID: 695
	internal abstract class MoveCopyItemCommandBase : MoveCopyCommandBase<BaseMoveCopyItemRequest, ItemType>
	{
		// Token: 0x060012B5 RID: 4789 RVA: 0x0005B624 File Offset: 0x00059824
		public MoveCopyItemCommandBase(CallContext callContext, BaseMoveCopyItemRequest request) : base(callContext, request)
		{
		}

		// Token: 0x060012B6 RID: 4790 RVA: 0x0005B630 File Offset: 0x00059830
		protected override void PrepareCommandMembers()
		{
			ExTraceGlobals.MoveCopyItemCommandBaseCallTracer.TraceDebug((long)this.GetHashCode(), "MoveCopyItemCommandBase.PrepareCommandMembers called");
			this.objectIds = base.Request.Ids;
			this.destinationFolderId = base.Request.ToFolderId.BaseFolderId;
			ServiceCommandBase.ThrowIfNullOrEmpty<ServiceObjectId>(this.objectIds, "objectIds", "PrepareCommandMembers::Execute");
			ServiceCommandBase.ThrowIfNull(this.destinationFolderId, "destinationFolderId", "PrepareCommandMembers::Execute");
		}

		// Token: 0x060012B7 RID: 4791 RVA: 0x0005B6A4 File Offset: 0x000598A4
		protected override StoreObject BindObjectFromRequest(StoreSession storeSession, StoreId storeId)
		{
			Item xsoItem = ServiceCommandBase.GetXsoItem(storeSession, storeId, new PropertyDefinition[]
			{
				MessageItemSchema.Flags
			});
			if (ServiceCommandBase.IsAssociated(xsoItem))
			{
				xsoItem.Dispose();
				throw new ServiceInvalidOperationException((CoreResources.IDs)3859804741U);
			}
			return xsoItem;
		}

		// Token: 0x060012B8 RID: 4792 RVA: 0x0005B6E8 File Offset: 0x000598E8
		protected override ServiceResult<ItemType> PrepareResult(StoreObject storeObject, GroupOperationResult[] groupOperationResults)
		{
			if (groupOperationResults == null || groupOperationResults.Length != 1 || groupOperationResults[0].ResultObjectIds == null || groupOperationResults[0].ResultObjectIds.Count != 1)
			{
				return new ServiceResult<ItemType>(null);
			}
			StoreId storeId = IdConverter.CombineStoreObjectIdWithChangeKey(groupOperationResults[0].ResultObjectIds[0], groupOperationResults[0].ResultChangeKeys[0]);
			StoreObjectId storeObjectId = StoreId.GetStoreObjectId(storeId);
			IdAndSession idAndSession = new IdAndSession(storeId, this.destinationFolder.Id, this.destinationFolder.Session);
			ItemType itemType = ItemType.CreateFromStoreObjectType(storeObjectId.ObjectType);
			itemType.ItemId = base.GetServiceItemIdFromStoreId(storeId, idAndSession);
			return new ServiceResult<ItemType>(itemType);
		}

		// Token: 0x060012B9 RID: 4793 RVA: 0x0005B788 File Offset: 0x00059988
		protected override IdAndSession GetIdAndSession(ServiceObjectId objectId)
		{
			BaseItemId baseItemId = objectId as BaseItemId;
			return base.IdConverter.ConvertItemIdToIdAndSessionReadOnly(baseItemId);
		}

		// Token: 0x060012BA RID: 4794 RVA: 0x0005B7A8 File Offset: 0x000599A8
		protected override Folder GetDestinationFolder()
		{
			IdAndSession idAndSession = null;
			try
			{
				idAndSession = base.IdConverter.ConvertTargetFolderIdToIdAndContentSession(this.destinationFolderId, true);
			}
			catch (ObjectNotFoundException innerException)
			{
				throw new ToFolderNotFoundException(innerException);
			}
			return Folder.Bind(idAndSession.Session, idAndSession.Id, null);
		}
	}
}
