using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x020002CC RID: 716
	internal abstract class DeleteCommandBase<RequestType, ResponseType> : MultiStepServiceCommand<RequestType, ResponseType> where RequestType : BaseRequest
	{
		// Token: 0x060013DB RID: 5083 RVA: 0x0006361C File Offset: 0x0006181C
		public DeleteCommandBase(CallContext callContext, RequestType request) : base(callContext, request)
		{
		}

		// Token: 0x060013DC RID: 5084
		protected abstract StoreObject GetStoreObject(IdAndSession idAndSession);

		// Token: 0x060013DD RID: 5085
		protected abstract IdAndSession GetIdAndSession(ServiceObjectId objectId);

		// Token: 0x060013DE RID: 5086 RVA: 0x00063640 File Offset: 0x00061840
		internal override ServiceResult<ResponseType> Execute()
		{
			DeleteItemRequest deleteItemRequest = base.Request as DeleteItemRequest;
			if (deleteItemRequest != null && deleteItemRequest.Ids != null && deleteItemRequest.Ids.Length > base.CurrentStep && base.LogItemId())
			{
				RequestDetailsLoggerBase<RequestDetailsLogger>.SafeAppendGenericInfo(RequestDetailsLogger.Current, "PreDeleteId: ", string.Format("{0}:{1}", deleteItemRequest.Ids[base.CurrentStep].GetId(), deleteItemRequest.Ids[base.CurrentStep].GetChangeKey()));
			}
			IdAndSession idAndSession = null;
			try
			{
				idAndSession = this.GetIdAndSession(this.objectIds[base.CurrentStep]);
			}
			catch (ObjectNotFoundException)
			{
				if (!this.IgnoreObjectNotFoundError)
				{
					ExTraceGlobals.DeleteItemCallTracer.TraceDebug((long)this.GetHashCode(), "DeleteCommandBase.Execute: ObjectNotFoundException caught. Will be rethrown.");
					throw;
				}
				ExTraceGlobals.DeleteItemCallTracer.TraceDebug((long)this.GetHashCode(), "DeleteCommandBase.Execute: ObjectNotFoundException caught and ignored.");
				return this.CreateEmptyResponseType();
			}
			bool returnNewItemIds = false;
			if (deleteItemRequest != null)
			{
				returnNewItemIds = deleteItemRequest.ReturnMovedItemIds;
			}
			ServiceResult<ResponseType> result;
			using (DelegateSessionHandleWrapper delegateSessionHandleWrapper = base.GetDelegateSessionHandleWrapper(idAndSession))
			{
				if (delegateSessionHandleWrapper != null)
				{
					idAndSession = new IdAndSession(idAndSession.Id, delegateSessionHandleWrapper.Handle.MailboxSession);
				}
				result = this.DeleteObject(idAndSession, returnNewItemIds);
			}
			this.objectsChanged++;
			return result;
		}

		// Token: 0x1700026D RID: 621
		// (get) Token: 0x060013DF RID: 5087 RVA: 0x00063794 File Offset: 0x00061994
		internal override int StepCount
		{
			get
			{
				return this.objectIds.Count;
			}
		}

		// Token: 0x060013E0 RID: 5088 RVA: 0x000637A4 File Offset: 0x000619A4
		protected virtual ServiceResult<ResponseType> DeleteObject(IdAndSession idAndSession, bool returnNewItemIds)
		{
			ExTraceGlobals.ServiceCommandBaseCallTracer.TraceDebug<StoreId, DisposalType>(0L, "DeleteObject called for storeObjectId '{0}' using disposalType '{1}'", idAndSession.Id, this.disposalType);
			DeleteItemFlags deleteItemFlags = (DeleteItemFlags)this.disposalType;
			LocalizedException ex = null;
			ServiceResult<ResponseType> serviceResult = null;
			try
			{
				using (StoreObject storeObject = this.GetStoreObject(idAndSession))
				{
					this.ValidateOperation(storeObject);
					serviceResult = this.TryExecuteDelete(storeObject, deleteItemFlags, idAndSession, out ex, returnNewItemIds);
					if (serviceResult == null)
					{
						if (ex is CannotMoveDefaultFolderException)
						{
							throw new DeleteDistinguishedFolderException(ex);
						}
						throw new DeleteItemsException(ex);
					}
				}
			}
			catch (ObjectNotFoundException)
			{
				if (!this.IgnoreObjectNotFoundError)
				{
					ExTraceGlobals.DeleteItemCallTracer.TraceDebug((long)this.GetHashCode(), "DeleteCommandBase.DeleteObject: ObjectNotFoundException caught. Will be rethrown.");
					throw;
				}
				ExTraceGlobals.DeleteItemCallTracer.TraceDebug((long)this.GetHashCode(), "DeleteCommandBase.DeleteObject: ObjectNotFoundException caught and ignored.");
			}
			return serviceResult;
		}

		// Token: 0x060013E1 RID: 5089
		protected abstract ServiceResult<ResponseType> TryExecuteDelete(StoreObject storeObject, DeleteItemFlags deleteItemFlags, IdAndSession idAndSession, out LocalizedException localizedException, bool returnNewItemIds);

		// Token: 0x060013E2 RID: 5090
		protected abstract ServiceResult<ResponseType> CreateEmptyResponseType();

		// Token: 0x060013E3 RID: 5091 RVA: 0x00063874 File Offset: 0x00061A74
		protected AggregateOperationResult ExecuteDelete(StoreObject storeObject, DeleteItemFlags deleteItemFlags, IdAndSession idAndSession, bool returnNewItemIds)
		{
			return idAndSession.DeleteRootXsoItem(storeObject.ParentId, deleteItemFlags, returnNewItemIds);
		}

		// Token: 0x060013E4 RID: 5092 RVA: 0x00063885 File Offset: 0x00061A85
		private void ValidateOperation(StoreObject storeObject)
		{
			this.ExecuteCommandValidations(storeObject);
			if (storeObject.Session is PublicFolderSession && this.disposalType == DisposalType.MoveToDeletedItems)
			{
				throw new ServiceInvalidOperationException((storeObject is Folder) ? CoreResources.IDs.ErrorCannotMovePublicFolderOnDelete : CoreResources.IDs.ErrorCannotMovePublicFolderItemOnDelete);
			}
		}

		// Token: 0x060013E5 RID: 5093
		protected abstract void ExecuteCommandValidations(StoreObject storeObject);

		// Token: 0x1700026E RID: 622
		// (get) Token: 0x060013E6 RID: 5094 RVA: 0x000638C3 File Offset: 0x00061AC3
		// (set) Token: 0x060013E7 RID: 5095 RVA: 0x000638C6 File Offset: 0x00061AC6
		internal virtual bool IgnoreObjectNotFoundError
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		// Token: 0x04000D76 RID: 3446
		protected IList<ServiceObjectId> objectIds;

		// Token: 0x04000D77 RID: 3447
		protected DisposalType disposalType;

		// Token: 0x04000D78 RID: 3448
		protected CalendarItemOperationType.CreateOrDelete? sendMeetingCancellations = null;

		// Token: 0x04000D79 RID: 3449
		protected AffectedTaskOccurrencesType? affectedTaskOccurrences = null;
	}
}
