using System;
using System.Linq;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.RpcClientAccess.Parser;

namespace Microsoft.Exchange.RpcClientAccess.Handler
{
	// Token: 0x02000033 RID: 51
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class SetReadFlagsSegmentedOperation : SegmentedRopOperation
	{
		// Token: 0x06000246 RID: 582 RVA: 0x00015364 File Offset: 0x00013564
		internal SetReadFlagsSegmentedOperation(ReferenceCount<CoreFolder> folder, SetReadFlagFlags setReadFlagFlags, StoreObjectId[] messageIds, int segmentSize) : base(RopId.SetReadFlags)
		{
			using (DisposeGuard disposeGuard = this.Guard())
			{
				this.folder = folder;
				this.folder.AddRef();
				this.setReadFlagFlags = setReadFlagFlags;
				if (messageIds != null)
				{
					this.setReadFlagsEnumerator = new IdsSegmentEnumerator(messageIds, segmentSize);
					base.TotalWork = messageIds.Length;
				}
				else
				{
					this.setReadFlagsEnumerator = new QuerySegmentEnumerator(this.folder.ReferencedObject, ItemQueryType.None, segmentSize);
					base.TotalWork = (int)this.folder.ReferencedObject.PropertyBag[FolderSchema.ItemCount];
				}
				disposeGuard.Success();
			}
		}

		// Token: 0x06000247 RID: 583 RVA: 0x00015420 File Offset: 0x00013620
		protected override SegmentOperationResult InternalDoNextBatchOperation()
		{
			TestInterceptor.Intercept(TestInterceptorLocation.SetReadFlagsSegmentedOperation_InternalDoNextBatchOperation, new object[0]);
			StoreObjectId[] nextBatchIds = this.setReadFlagsEnumerator.GetNextBatchIds();
			if (nextBatchIds.Length > 0)
			{
				bool flag;
				this.folder.ReferencedObject.SetReadFlags((int)this.setReadFlagFlags, (from id in nextBatchIds
				select id).ToArray<StoreId>(), out flag);
				return new SegmentOperationResult
				{
					CompletedWork = nextBatchIds.Length,
					OperationResult = (flag ? OperationResult.PartiallySucceeded : OperationResult.Succeeded),
					IsCompleted = false,
					Exception = null
				};
			}
			return SegmentedRopOperation.FinalResult;
		}

		// Token: 0x06000248 RID: 584 RVA: 0x000154C6 File Offset: 0x000136C6
		internal override RopResult CreateCompleteResult(object progressToken, IProgressResultFactory resultFactory)
		{
			if (base.ErrorCode == ErrorCode.None)
			{
				return ((SetReadFlagsResultFactory)resultFactory).CreateSuccessfulResult(base.IsPartiallyCompleted);
			}
			return ((SetReadFlagsResultFactory)resultFactory).CreateFailedResult(base.ErrorCode, base.IsPartiallyCompleted);
		}

		// Token: 0x06000249 RID: 585 RVA: 0x000154F9 File Offset: 0x000136F9
		internal override RopResult CreateCompleteResultForProgress(object progressToken, ProgressResultFactory progressResultFactory)
		{
			if (base.ErrorCode == ErrorCode.None)
			{
				return progressResultFactory.CreateSuccessfulSetReadFlagsResult(progressToken, base.IsPartiallyCompleted);
			}
			return progressResultFactory.CreateFailedSetReadFlagsResult(progressToken, base.ErrorCode, base.IsPartiallyCompleted);
		}

		// Token: 0x0600024A RID: 586 RVA: 0x00015524 File Offset: 0x00013724
		protected override void InternalDispose()
		{
			Util.DisposeIfPresent(this.setReadFlagsEnumerator);
			if (this.folder != null)
			{
				this.folder.Release();
			}
			base.InternalDispose();
		}

		// Token: 0x0600024B RID: 587 RVA: 0x0001554B File Offset: 0x0001374B
		protected override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<SetReadFlagsSegmentedOperation>(this);
		}

		// Token: 0x040000DD RID: 221
		private readonly ReferenceCount<CoreFolder> folder;

		// Token: 0x040000DE RID: 222
		private readonly SetReadFlagFlags setReadFlagFlags;

		// Token: 0x040000DF RID: 223
		private readonly SegmentEnumerator setReadFlagsEnumerator;
	}
}
