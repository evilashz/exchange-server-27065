using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.Services.Core.DataConverter;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x020002B5 RID: 693
	internal abstract class MoveCopyFolderCommandBase : MoveCopyCommandBase<BaseMoveCopyFolderRequest, BaseFolderType>
	{
		// Token: 0x060012AB RID: 4779 RVA: 0x0005B3E3 File Offset: 0x000595E3
		public MoveCopyFolderCommandBase(CallContext callContext, BaseMoveCopyFolderRequest request) : base(callContext, request)
		{
		}

		// Token: 0x060012AC RID: 4780 RVA: 0x0005B3F0 File Offset: 0x000595F0
		protected override void PrepareCommandMembers()
		{
			ExTraceGlobals.MoveCopyFolderCommandBaseCallTracer.TraceDebug((long)this.GetHashCode(), "MoveCopyFolderCommandBase.PrepareCommandMembers called");
			this.objectIds = base.Request.Ids;
			this.destinationFolderId = base.Request.ToFolderId.BaseFolderId;
			this.responseShape = ServiceCommandBase.DefaultFolderResponseShape;
			ServiceCommandBase.ThrowIfNullOrEmpty<ServiceObjectId>(this.objectIds, "objectIds", "MoveCopyFolderCommandBase::PrepareCommandMembers");
			ServiceCommandBase.ThrowIfNull(this.destinationFolderId, "destinationFolderId", "MoveCopyFolderCommandBase::PrepareCommandMembers");
			ServiceCommandBase.ThrowIfNull(this.responseShape, "responseShape", "MoveCopyFolderCommandBase::PrepareCommandMembers");
		}

		// Token: 0x060012AD RID: 4781 RVA: 0x0005B484 File Offset: 0x00059684
		protected override StoreObject BindObjectFromRequest(StoreSession storeSession, StoreId storeId)
		{
			ToXmlPropertyList propertyList = XsoDataConverter.GetPropertyList(storeId, storeSession, this.responseShape);
			return ServiceCommandBase.GetXsoFolder(storeSession, storeId, propertyList);
		}

		// Token: 0x060012AE RID: 4782 RVA: 0x0005B4A8 File Offset: 0x000596A8
		protected override ServiceResult<BaseFolderType> PrepareResult(StoreObject originalStoreObject, GroupOperationResult[] groupOperationResults)
		{
			StoreSession session = this.destinationFolder.Session;
			StoreId resultingObjectId = this.GetResultingObjectId(originalStoreObject);
			StoreObjectId storeObjectId = StoreId.GetStoreObjectId(resultingObjectId);
			IdAndSession idAndSession = new IdAndSession(resultingObjectId, session);
			BaseFolderType baseFolderType = BaseFolderType.CreateFromStoreObjectType(storeObjectId.ObjectType);
			baseFolderType.FolderId = base.GetServiceFolderIdFromStoreId(storeObjectId, idAndSession);
			return new ServiceResult<BaseFolderType>(baseFolderType);
		}

		// Token: 0x060012AF RID: 4783 RVA: 0x0005B4FC File Offset: 0x000596FC
		private StoreId GetResultingObjectId(StoreObject originalObject)
		{
			Folder folder = originalObject as Folder;
			ComparisonFilter queryFilter = new ComparisonFilter(ComparisonOperator.Equal, FolderSchema.DisplayName, folder.DisplayName);
			StoreId result;
			using (QueryResult queryResult = this.destinationFolder.FolderQuery(FolderQueryFlags.None, queryFilter, null, new PropertyDefinition[]
			{
				FolderSchema.Id
			}))
			{
				if (queryResult.EstimatedRowCount == 0)
				{
					throw new MoveCopyException();
				}
				object[][] rows = queryResult.GetRows(1);
				result = (VersionedId)rows[0][0];
			}
			return result;
		}

		// Token: 0x060012B0 RID: 4784 RVA: 0x0005B584 File Offset: 0x00059784
		protected override IdAndSession GetIdAndSession(ServiceObjectId objectId)
		{
			BaseFolderId folderId = objectId as BaseFolderId;
			return base.IdConverter.ConvertFolderIdToIdAndSession(folderId, IdConverter.ConvertOption.IgnoreChangeKey | IdConverter.ConvertOption.AllowKnownExternalUsers | (base.Request.IsHierarchicalOperation ? IdConverter.ConvertOption.IsHierarchicalOperation : IdConverter.ConvertOption.None));
		}

		// Token: 0x04000D10 RID: 3344
		private ResponseShape responseShape;
	}
}
