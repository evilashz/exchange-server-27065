using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x020002CF RID: 719
	internal sealed class DeleteItemBatch : ItemBatchCommandBase<DeleteItemRequest, DeleteItemResponseMessage>
	{
		// Token: 0x060013FF RID: 5119 RVA: 0x00063FDF File Offset: 0x000621DF
		public DeleteItemBatch(CallContext callContext, DeleteItemRequest request) : base(callContext, request)
		{
		}

		// Token: 0x17000270 RID: 624
		// (get) Token: 0x06001400 RID: 5120 RVA: 0x00063FE9 File Offset: 0x000621E9
		internal override int StepCount
		{
			get
			{
				return base.Request.Ids.Length;
			}
		}

		// Token: 0x06001401 RID: 5121 RVA: 0x00063FF8 File Offset: 0x000621F8
		internal override ServiceResult<DeleteItemResponseMessage> Execute()
		{
			ExTraceGlobals.DeleteItemCallTracer.TraceDebug((long)this.GetHashCode(), "DeleteItemBatch.Execute called");
			if (base.Request.Ids != null && base.Request.Ids.Length > base.CurrentStep && base.LogItemId())
			{
				RequestDetailsLoggerBase<RequestDetailsLogger>.SafeAppendGenericInfo(RequestDetailsLogger.Current, "PreDeleteId: ", string.Format("{0}:{1}", base.Request.Ids[base.CurrentStep].GetId(), base.Request.Ids[base.CurrentStep].GetChangeKey()));
			}
			if (base.CurrentStep == 0)
			{
				if (this.TryToDeleteItems())
				{
					this.objectsChanged += base.Results.Count<ServiceResult<DeleteItemResponseMessage>>();
					base.LogCommandOptimizationToIIS(true);
				}
				else
				{
					this.deleteItemCommand = new DeleteItem(base.CallContext, base.Request);
					this.deleteItemCommand.IgnoreObjectNotFoundError = true;
					this.deleteItemCommand.PreExecuteCommand();
				}
			}
			if (this.deleteItemCommand != null)
			{
				this.deleteItemCommand.CurrentStep = base.CurrentStep;
				return this.deleteItemCommand.Execute();
			}
			if (this.ServiceResults == null)
			{
				return new ServiceResult<DeleteItemResponseMessage>(new DeleteItemResponseMessage());
			}
			return this.ServiceResults[base.CurrentStep];
		}

		// Token: 0x06001402 RID: 5122 RVA: 0x00064130 File Offset: 0x00062330
		internal override void PostExecuteCommand()
		{
			if (this.deleteItemCommand != null)
			{
				this.deleteItemCommand.PostExecuteCommand();
			}
		}

		// Token: 0x06001403 RID: 5123 RVA: 0x00064148 File Offset: 0x00062348
		private bool TryToDeleteItems()
		{
			bool suppressReadReceipts = base.Request.SuppressReadReceipts;
			List<StoreId> itemStoreIds = base.Request.ItemStoreIds;
			bool returnMovedItemIds = base.Request.ReturnMovedItemIds;
			BaseItemId baseItemId = base.Request.Ids.First<BaseItemId>();
			IdAndSession idAndSession = base.IdConverter.ConvertItemIdToIdAndSessionReadOnly(baseItemId);
			if (base.Request.DeleteType == DisposalType.MoveToDeletedItems && idAndSession.Session is PublicFolderSession)
			{
				return false;
			}
			if (base.VerifyItemsCanBeBatched(itemStoreIds, idAndSession, null, ref suppressReadReceipts))
			{
				DeleteItemFlags deleteItemFlags = (DeleteItemFlags)base.Request.DeleteType;
				if (suppressReadReceipts)
				{
					deleteItemFlags |= DeleteItemFlags.SuppressReadReceipt;
				}
				AggregateOperationResult aggregateOperationResult = idAndSession.Session.Delete(deleteItemFlags, returnMovedItemIds, itemStoreIds.ToArray());
				if (returnMovedItemIds && (deleteItemFlags & DeleteItemFlags.MoveToDeletedItems) != DeleteItemFlags.None && aggregateOperationResult != null && aggregateOperationResult.GroupOperationResults != null && aggregateOperationResult.GroupOperationResults.Length > 0 && aggregateOperationResult.GroupOperationResults[0].OperationResult == OperationResult.Succeeded && aggregateOperationResult.GroupOperationResults[0].ResultObjectIds != null && aggregateOperationResult.GroupOperationResults[0].ResultObjectIds.Count > 0)
				{
					this.ServiceResults = new ServiceResult<DeleteItemResponseMessage>[aggregateOperationResult.GroupOperationResults[0].ResultObjectIds.Count];
					for (int i = 0; i < aggregateOperationResult.GroupOperationResults[0].ResultObjectIds.Count; i++)
					{
						DeleteItemResponseMessage deleteItemResponseMessage = new DeleteItemResponseMessage();
						StoreId storeItemId = IdConverter.CombineStoreObjectIdWithChangeKey(aggregateOperationResult.GroupOperationResults[0].ResultObjectIds[i], aggregateOperationResult.GroupOperationResults[0].ResultChangeKeys[i]);
						ItemId movedItemId = IdConverter.ConvertStoreItemIdToItemId(storeItemId, idAndSession.Session);
						deleteItemResponseMessage.MovedItemId = movedItemId;
						this.ServiceResults[i] = new ServiceResult<DeleteItemResponseMessage>(deleteItemResponseMessage);
					}
				}
				if (aggregateOperationResult != null && aggregateOperationResult.OperationResult == OperationResult.Succeeded)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001404 RID: 5124 RVA: 0x00064324 File Offset: 0x00062524
		internal override IExchangeWebMethodResponse GetResponse()
		{
			DeleteItemResponse deleteItemResponse = new DeleteItemResponse();
			deleteItemResponse.AddResponses(base.Results);
			return deleteItemResponse;
		}

		// Token: 0x17000271 RID: 625
		// (get) Token: 0x06001405 RID: 5125 RVA: 0x00064344 File Offset: 0x00062544
		// (set) Token: 0x06001406 RID: 5126 RVA: 0x0006434C File Offset: 0x0006254C
		internal ServiceResult<DeleteItemResponseMessage>[] ServiceResults { get; set; }

		// Token: 0x04000D7C RID: 3452
		private DeleteItem deleteItemCommand;
	}
}
