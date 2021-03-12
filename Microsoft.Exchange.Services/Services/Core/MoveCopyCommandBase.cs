using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x020002B4 RID: 692
	internal abstract class MoveCopyCommandBase<RequestType, ResponseType> : MultiStepServiceCommand<RequestType, ResponseType>, IDisposeTrackable, IDisposable where RequestType : BaseRequest
	{
		// Token: 0x06001298 RID: 4760 RVA: 0x0005B1D0 File Offset: 0x000593D0
		public MoveCopyCommandBase(CallContext callContext, RequestType request) : base(callContext, request)
		{
			this.PrepareCommandMembers();
			this.disposeTracker = this.GetDisposeTracker();
		}

		// Token: 0x06001299 RID: 4761 RVA: 0x0005B1EC File Offset: 0x000593EC
		public virtual DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<MoveCopyCommandBase<RequestType, ResponseType>>(this);
		}

		// Token: 0x0600129A RID: 4762 RVA: 0x0005B1F4 File Offset: 0x000593F4
		public void SuppressDisposeTracker()
		{
			if (this.disposeTracker != null)
			{
				this.disposeTracker.Suppress();
			}
		}

		// Token: 0x0600129B RID: 4763 RVA: 0x0005B209 File Offset: 0x00059409
		public void Dispose()
		{
			if (this.disposeTracker != null)
			{
				this.disposeTracker.Dispose();
			}
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600129C RID: 4764 RVA: 0x0005B22C File Offset: 0x0005942C
		internal override IExchangeWebMethodResponse GetResponse()
		{
			BaseInfoResponse baseInfoResponse = this.CreateResponse();
			baseInfoResponse.BuildForResults<ResponseType>(base.Results);
			return baseInfoResponse;
		}

		// Token: 0x0600129D RID: 4765 RVA: 0x0005B24D File Offset: 0x0005944D
		internal override void PreExecuteCommand()
		{
			this.destinationFolder = this.GetDestinationFolder();
		}

		// Token: 0x0600129E RID: 4766 RVA: 0x0005B25C File Offset: 0x0005945C
		protected virtual Folder GetDestinationFolder()
		{
			IdAndSession idAndSession = null;
			try
			{
				idAndSession = base.IdConverter.ConvertTargetFolderIdToIdAndHierarchySession(this.destinationFolderId, true);
			}
			catch (ObjectNotFoundException innerException)
			{
				throw new ToFolderNotFoundException(innerException);
			}
			return Folder.Bind(idAndSession.Session, idAndSession.Id, null);
		}

		// Token: 0x0600129F RID: 4767
		protected abstract IdAndSession GetIdAndSession(ServiceObjectId objectId);

		// Token: 0x060012A0 RID: 4768 RVA: 0x0005B2B0 File Offset: 0x000594B0
		internal override ServiceResult<ResponseType> Execute()
		{
			IdAndSession idAndSession = this.GetIdAndSession(this.objectIds[base.CurrentStep]);
			if (idAndSession.GetAsStoreObjectId().ObjectType == StoreObjectType.CalendarItemOccurrence)
			{
				throw new CalendarExceptionCannotMoveOrCopyOccurrence();
			}
			this.ValidateOperation(this.destinationFolder.Session, idAndSession);
			return this.MoveCopyObject(idAndSession);
		}

		// Token: 0x060012A1 RID: 4769 RVA: 0x0005B304 File Offset: 0x00059504
		protected virtual ServiceResult<ResponseType> MoveCopyObject(IdAndSession idAndSession)
		{
			ServiceResult<ResponseType> result = null;
			using (StoreObject storeObject = this.BindObjectFromRequest(idAndSession.Session, idAndSession.Id))
			{
				AggregateOperationResult aggregateOperationResult = this.DoOperation(this.destinationFolder.Session, idAndSession.Session, idAndSession.Id);
				if (aggregateOperationResult.OperationResult != OperationResult.Succeeded)
				{
					throw new MoveCopyException();
				}
				result = this.PrepareResult(storeObject, aggregateOperationResult.GroupOperationResults);
			}
			this.objectsChanged++;
			return result;
		}

		// Token: 0x060012A2 RID: 4770 RVA: 0x0005B38C File Offset: 0x0005958C
		private void ValidateOperation(StoreSession storeSession, IdAndSession idAndSession)
		{
			this.SubclassValidateOperation(storeSession, idAndSession);
		}

		// Token: 0x060012A3 RID: 4771 RVA: 0x0005B396 File Offset: 0x00059596
		protected virtual void SubclassValidateOperation(StoreSession storeSession, IdAndSession idAndSession)
		{
		}

		// Token: 0x1700024E RID: 590
		// (get) Token: 0x060012A4 RID: 4772 RVA: 0x0005B398 File Offset: 0x00059598
		internal override int StepCount
		{
			get
			{
				return this.objectIds.Count;
			}
		}

		// Token: 0x060012A5 RID: 4773 RVA: 0x0005B3A5 File Offset: 0x000595A5
		private void Dispose(bool fromDispose)
		{
			if (this.disposeTracker != null)
			{
				this.disposeTracker.Dispose();
			}
			if (!this.disposed)
			{
				if (this.destinationFolder != null)
				{
					this.destinationFolder.Dispose();
					this.destinationFolder = null;
				}
				this.disposed = true;
			}
		}

		// Token: 0x060012A6 RID: 4774
		protected abstract BaseInfoResponse CreateResponse();

		// Token: 0x060012A7 RID: 4775
		protected abstract void PrepareCommandMembers();

		// Token: 0x060012A8 RID: 4776
		protected abstract AggregateOperationResult DoOperation(StoreSession destinationSession, StoreSession sourceSession, StoreId sourceId);

		// Token: 0x060012A9 RID: 4777
		protected abstract StoreObject BindObjectFromRequest(StoreSession storeSession, StoreId storeId);

		// Token: 0x060012AA RID: 4778
		protected abstract ServiceResult<ResponseType> PrepareResult(StoreObject storeObject, GroupOperationResult[] groupOperationResults);

		// Token: 0x04000D0B RID: 3339
		private readonly DisposeTracker disposeTracker;

		// Token: 0x04000D0C RID: 3340
		protected IList<ServiceObjectId> objectIds;

		// Token: 0x04000D0D RID: 3341
		protected BaseFolderId destinationFolderId;

		// Token: 0x04000D0E RID: 3342
		protected Folder destinationFolder;

		// Token: 0x04000D0F RID: 3343
		private bool disposed;
	}
}
