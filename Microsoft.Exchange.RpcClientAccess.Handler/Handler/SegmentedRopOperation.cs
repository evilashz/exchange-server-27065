using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.RpcClientAccess;
using Microsoft.Exchange.Diagnostics.WorkloadManagement;
using Microsoft.Exchange.RpcClientAccess.Diagnostics;
using Microsoft.Exchange.RpcClientAccess.Parser;

namespace Microsoft.Exchange.RpcClientAccess.Handler
{
	// Token: 0x02000026 RID: 38
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal abstract class SegmentedRopOperation : BaseObject
	{
		// Token: 0x060001F0 RID: 496 RVA: 0x00011AAF File Offset: 0x0000FCAF
		protected SegmentedRopOperation(RopId ropId)
		{
			this.ropId = ropId;
			this.referencedActivityScope = ReferencedActivityScope.Current;
			if (this.referencedActivityScope != null)
			{
				this.referencedActivityScope.AddRef();
			}
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x00011AEC File Offset: 0x0000FCEC
		internal static bool SafeSegmentExecution(SegmentedRopOperation segmentedRopOperation, Action execution)
		{
			Exception ex2;
			try
			{
				execution();
				return true;
			}
			catch (StoragePermanentException ex)
			{
				ex2 = ex;
			}
			catch (StorageTransientException ex3)
			{
				ex2 = ex3;
			}
			ExTraceGlobals.FailedRopTracer.TraceDebug<object, MethodInfo, Exception>((long)segmentedRopOperation.GetHashCode(), "Received exception in {0}{1}. XsoException = {2}", execution.Target, execution.Method, ex2);
			Exception ex4;
			SegmentedRopOperation.TranslateSegmentedOperationException(ex2, out ex4, out segmentedRopOperation.errorCode);
			if (ex4 != null)
			{
				segmentedRopOperation.exception = ex4;
			}
			return false;
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x060001F2 RID: 498 RVA: 0x00011B6C File Offset: 0x0000FD6C
		public int CompletedWork
		{
			get
			{
				return this.completedWork;
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x060001F3 RID: 499 RVA: 0x00011B74 File Offset: 0x0000FD74
		// (set) Token: 0x060001F4 RID: 500 RVA: 0x00011B7C File Offset: 0x0000FD7C
		public int TotalWork
		{
			get
			{
				return this.totalWork;
			}
			protected set
			{
				this.totalWork = value;
			}
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x00011B85 File Offset: 0x0000FD85
		internal bool SafeSegmentExecution(Action execution)
		{
			return SegmentedRopOperation.SafeSegmentExecution(this, execution);
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x00011B94 File Offset: 0x0000FD94
		internal bool DoNextBatchOperation()
		{
			IActivityScope currentActivityScope = ActivityContext.GetCurrentActivityScope();
			SegmentOperationResult segmentOperationResult;
			Exception ex;
			ErrorCode error;
			bool flag;
			try
			{
				if (this.referencedActivityScope != null)
				{
					ActivityContext.SetThreadScope(this.referencedActivityScope.ActivityScope);
				}
				flag = ExceptionTranslator.TryExecuteCatchAndTranslateExceptions<SegmentOperationResult>(new Func<SegmentOperationResult>(this.InternalDoNextBatchOperation), (SegmentOperationResult unused) => ErrorCode.None, true, out segmentOperationResult, out ex, out error);
			}
			finally
			{
				ActivityContext.SetThreadScope(currentActivityScope);
			}
			if (!flag)
			{
				this.CalculateAggregateResult(OperationResult.Failed, error);
				RopHandlerHelper.TraceRopResult(this.ropId, ex, error);
				this.errorCode = error;
				return false;
			}
			if (segmentOperationResult.OperationResult != SegmentOperationResult.NeutralOperationResult)
			{
				this.CalculateAggregateResult(segmentOperationResult);
			}
			this.completedWork += segmentOperationResult.CompletedWork;
			return !segmentOperationResult.IsCompleted;
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x060001F7 RID: 503 RVA: 0x00011C68 File Offset: 0x0000FE68
		internal OperationResult AggregatedResult
		{
			get
			{
				OperationResult? operationResult = this.aggregatedResult;
				if (operationResult == null)
				{
					return OperationResult.Succeeded;
				}
				return operationResult.GetValueOrDefault();
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x060001F8 RID: 504 RVA: 0x00011C8E File Offset: 0x0000FE8E
		// (set) Token: 0x060001F9 RID: 505 RVA: 0x00011C96 File Offset: 0x0000FE96
		internal ErrorCode ErrorCode
		{
			get
			{
				return this.errorCode;
			}
			set
			{
				this.errorCode = value;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x060001FA RID: 506 RVA: 0x00011C9F File Offset: 0x0000FE9F
		internal Exception Exception
		{
			get
			{
				return this.exception;
			}
		}

		// Token: 0x060001FB RID: 507
		internal abstract RopResult CreateCompleteResult(object progressToken, IProgressResultFactory resultFactory);

		// Token: 0x060001FC RID: 508
		internal abstract RopResult CreateCompleteResultForProgress(object progressToken, ProgressResultFactory resultFactory);

		// Token: 0x060001FD RID: 509 RVA: 0x00011CA8 File Offset: 0x0000FEA8
		protected static StoreObjectId[] ConvertMessageIds(IdConverter converter, StoreObjectId parentFolderId, IList<StoreObjectId> objectIds)
		{
			StoreId storeId = new StoreId(converter.GetFidFromId(parentFolderId));
			StoreObjectId[] array = new StoreObjectId[objectIds.Count];
			int num = 0;
			foreach (StoreObjectId messageStoreObjectId in objectIds)
			{
				array[num++] = converter.CreateMessageId(storeId, converter.GetMidFromMessageId(messageStoreObjectId));
			}
			return array;
		}

		// Token: 0x060001FE RID: 510 RVA: 0x00011D28 File Offset: 0x0000FF28
		protected static int EstimateWork(CoreFolder rootFolder, bool includeAssociatedMessage, List<StoreObjectId> subfolderIds)
		{
			int num = 0;
			using (QueryResult queryResult = rootFolder.QueryExecutor.FolderQuery(FolderQueryFlags.DeepTraversal | FolderQueryFlags.SuppressNotificationsOnMyActions, null, null, SegmentedRopOperation.properties))
			{
				object[][] rows;
				do
				{
					rows = queryResult.GetRows(int.MaxValue);
					foreach (object[] array2 in rows)
					{
						StoreId storeId = array2[0] as StoreId;
						if (storeId != null)
						{
							int num2 = (array2[1] is int) ? ((int)array2[1]) : 0;
							num += num2;
							int num3 = (array2[2] is int) ? ((int)array2[2]) : 0;
							num += num3;
							if (includeAssociatedMessage)
							{
								int num4 = (array2[3] is int) ? ((int)array2[3]) : 0;
								num += num4;
							}
							if (subfolderIds != null)
							{
								subfolderIds.Add(StoreId.GetStoreObjectId(storeId));
							}
						}
					}
				}
				while (rows.Length != 0);
			}
			num += (int)rootFolder.PropertyBag[FolderSchema.ChildCount];
			num += (int)rootFolder.PropertyBag[FolderSchema.ItemCount];
			if (includeAssociatedMessage)
			{
				num += (int)rootFolder.PropertyBag[FolderSchema.AssociatedItemCount];
			}
			return num;
		}

		// Token: 0x060001FF RID: 511
		protected abstract SegmentOperationResult InternalDoNextBatchOperation();

		// Token: 0x06000200 RID: 512 RVA: 0x00011E60 File Offset: 0x00010060
		protected void CalculateAggregateResult(SegmentOperationResult segmentOperationResult)
		{
			ErrorCode errorCode = ErrorCode.None;
			if (segmentOperationResult.Exception != null)
			{
				Exception ex;
				SegmentedRopOperation.TranslateSegmentedOperationException(segmentOperationResult.Exception, out ex, out errorCode);
				if (errorCode == ErrorCode.PartialCompletion)
				{
					errorCode = ErrorCode.None;
				}
				if (ex != null)
				{
					this.exception = ex;
				}
			}
			this.CalculateAggregateResult(segmentOperationResult.OperationResult, errorCode);
		}

		// Token: 0x06000201 RID: 513 RVA: 0x00011EAC File Offset: 0x000100AC
		protected void CalculateAggregateResult(OperationResult operationResult, ErrorCode error)
		{
			if (this.aggregatedResult == null)
			{
				this.aggregatedResult = new OperationResult?(operationResult);
				return;
			}
			OperationResult operationResult2 = this.aggregatedResult.Value;
			switch (operationResult)
			{
			case OperationResult.Succeeded:
				if (operationResult2 == OperationResult.Failed)
				{
					operationResult2 = OperationResult.PartiallySucceeded;
				}
				break;
			case OperationResult.Failed:
				if (operationResult2 == OperationResult.Succeeded)
				{
					operationResult2 = OperationResult.PartiallySucceeded;
				}
				break;
			case OperationResult.PartiallySucceeded:
				operationResult2 = OperationResult.PartiallySucceeded;
				break;
			}
			this.aggregatedResult = new OperationResult?(operationResult2);
			if (error != ErrorCode.None)
			{
				this.errorCode = error;
			}
		}

		// Token: 0x06000202 RID: 514 RVA: 0x00011F20 File Offset: 0x00010120
		protected void DetectCopyMoveLoop(ReferenceCount<CoreFolder> destinationFolderReferenceCount, List<StoreObjectId> subFolderIds)
		{
			VersionedId id = destinationFolderReferenceCount.ReferencedObject.Id;
			StoreObjectId storeObjectId = StoreId.GetStoreObjectId(id);
			bool flag = subFolderIds.Contains(storeObjectId);
			if (flag)
			{
				throw new RopExecutionException("Copy/move folder cycle detected", (ErrorCode)2147747339U);
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000203 RID: 515 RVA: 0x00011F5B File Offset: 0x0001015B
		protected RopId RopId
		{
			get
			{
				return this.ropId;
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000204 RID: 516 RVA: 0x00011F64 File Offset: 0x00010164
		protected bool IsPartiallyCompleted
		{
			get
			{
				return this.aggregatedResult == OperationResult.PartiallySucceeded;
			}
		}

		// Token: 0x06000205 RID: 517 RVA: 0x00011F8B File Offset: 0x0001018B
		protected override void InternalDispose()
		{
			if (this.referencedActivityScope != null)
			{
				this.referencedActivityScope.Release();
			}
			base.InternalDispose();
		}

		// Token: 0x06000206 RID: 518 RVA: 0x00011FA8 File Offset: 0x000101A8
		private static void TranslateSegmentedOperationException(Exception xsoException, out Exception momtException, out ErrorCode errorCode)
		{
			momtException = null;
			errorCode = ErrorCode.None;
			if (xsoException == null)
			{
				return;
			}
			try
			{
				errorCode = ExceptionTranslator.ErrorFromXsoException(xsoException);
			}
			catch (SessionDeadException ex)
			{
				momtException = ex;
			}
			catch (ClientBackoffException ex2)
			{
				momtException = ex2;
			}
		}

		// Token: 0x06000207 RID: 519 RVA: 0x00011FF4 File Offset: 0x000101F4
		public override string ToString()
		{
			return string.Format("RopId:{0}; Work: {1}/{2}; Error:{3}", new object[]
			{
				this.ropId,
				this.CompletedWork,
				this.TotalWork,
				this.errorCode
			});
		}

		// Token: 0x040000A9 RID: 169
		private static readonly StorePropertyDefinition[] properties = new StorePropertyDefinition[]
		{
			FolderSchema.Id,
			FolderSchema.ChildCount,
			FolderSchema.ItemCount,
			FolderSchema.AssociatedItemCount
		};

		// Token: 0x040000AA RID: 170
		private readonly RopId ropId;

		// Token: 0x040000AB RID: 171
		private readonly ReferencedActivityScope referencedActivityScope;

		// Token: 0x040000AC RID: 172
		private OperationResult? aggregatedResult = null;

		// Token: 0x040000AD RID: 173
		private ErrorCode errorCode;

		// Token: 0x040000AE RID: 174
		private Exception exception;

		// Token: 0x040000AF RID: 175
		private int completedWork;

		// Token: 0x040000B0 RID: 176
		private int totalWork;

		// Token: 0x040000B1 RID: 177
		protected static readonly SegmentOperationResult FinalResult = new SegmentOperationResult
		{
			CompletedWork = 0,
			IsCompleted = true,
			OperationResult = SegmentOperationResult.NeutralOperationResult,
			Exception = null
		};

		// Token: 0x040000B2 RID: 178
		protected static readonly SegmentOperationResult FailedSegmentResult = new SegmentOperationResult
		{
			CompletedWork = 0,
			IsCompleted = false,
			OperationResult = OperationResult.Failed,
			Exception = null
		};
	}
}
