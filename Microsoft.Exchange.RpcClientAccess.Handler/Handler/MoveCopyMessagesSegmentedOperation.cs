using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.LinkedFolder;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.RpcClientAccess;
using Microsoft.Exchange.RpcClientAccess.Parser;
using Microsoft.Mapi;

namespace Microsoft.Exchange.RpcClientAccess.Handler
{
	// Token: 0x02000030 RID: 48
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class MoveCopyMessagesSegmentedOperation : SegmentedRopOperation
	{
		// Token: 0x06000238 RID: 568 RVA: 0x00014C60 File Offset: 0x00012E60
		internal MoveCopyMessagesSegmentedOperation(ReferenceCount<CoreFolder> sourceFolder, ReferenceCount<CoreFolder> destinationFolder, bool isCopy, StoreObjectId[] storeObjectIds, int segmentSize, TeamMailboxClientOperations teamMailboxClientOperations) : base(RopId.MoveCopyMessages)
		{
			using (DisposeGuard disposeGuard = this.Guard())
			{
				this.sourceFolder = sourceFolder;
				this.sourceFolder.AddRef();
				this.destinationFolder = destinationFolder;
				this.destinationFolder.AddRef();
				this.isCopy = isCopy;
				ExTraceGlobals.FaultInjectionTracer.TraceTest<int>(3903204669U, ref segmentSize);
				this.moveCopyMessagesEnumerator = new IdsSegmentEnumerator(storeObjectIds, segmentSize);
				base.TotalWork = this.moveCopyMessagesEnumerator.Count;
				this.teamMailboxClientOperations = teamMailboxClientOperations;
				disposeGuard.Success();
			}
		}

		// Token: 0x06000239 RID: 569 RVA: 0x00014D64 File Offset: 0x00012F64
		protected override SegmentOperationResult InternalDoNextBatchOperation()
		{
			StoreObjectId[] storeObjectIds = this.moveCopyMessagesEnumerator.GetNextBatchIds();
			if (storeObjectIds.Length > 0)
			{
				GroupOperationResult groupOperationResult;
				if (this.teamMailboxClientOperations != null)
				{
					groupOperationResult = TeamMailboxExecutionHelper.RunGroupOperationsWithExecutionLimitHandler(() => this.teamMailboxClientOperations.OnMoveCopyMessages(this.sourceFolder.ReferencedObject, this.destinationFolder.ReferencedObject, storeObjectIds, this.isCopy), "TeamMailboxClientOperations.OnMoveCopyMessages");
				}
				else if (this.isCopy)
				{
					groupOperationResult = this.sourceFolder.ReferencedObject.CopyItems(this.destinationFolder.ReferencedObject, storeObjectIds, null, null, null);
					this.PostCopyMessages(ref groupOperationResult);
				}
				else
				{
					groupOperationResult = this.sourceFolder.ReferencedObject.MoveItems(this.destinationFolder.ReferencedObject, storeObjectIds, null, null, null);
				}
				return new SegmentOperationResult
				{
					CompletedWork = storeObjectIds.Length,
					OperationResult = groupOperationResult.OperationResult,
					Exception = groupOperationResult.Exception,
					IsCompleted = (storeObjectIds.Length == 0)
				};
			}
			if (this.teamMailboxClientOperations != null)
			{
				((MailboxSession)this.destinationFolder.ReferencedObject.Session).TryToSyncSiteMailboxNow();
			}
			return SegmentedRopOperation.FinalResult;
		}

		// Token: 0x0600023A RID: 570 RVA: 0x00014E8E File Offset: 0x0001308E
		internal override RopResult CreateCompleteResult(object progressToken, IProgressResultFactory resultFactory)
		{
			if (base.ErrorCode == ErrorCode.None)
			{
				return ((MoveCopyMessagesResultFactory)resultFactory).CreateSuccessfulResult(base.IsPartiallyCompleted);
			}
			return ((MoveCopyMessagesResultFactory)resultFactory).CreateFailedResult(base.ErrorCode, base.IsPartiallyCompleted);
		}

		// Token: 0x0600023B RID: 571 RVA: 0x00014EC1 File Offset: 0x000130C1
		internal override RopResult CreateCompleteResultForProgress(object progressToken, ProgressResultFactory progressResultFactory)
		{
			if (base.ErrorCode == ErrorCode.None)
			{
				return progressResultFactory.CreateSuccessfulMoveCopyMessagesResult(progressToken, base.IsPartiallyCompleted);
			}
			return progressResultFactory.CreateFailedMoveCopyMessagesResult(progressToken, base.ErrorCode, base.IsPartiallyCompleted);
		}

		// Token: 0x0600023C RID: 572 RVA: 0x00014EEC File Offset: 0x000130EC
		private void PostCopyMessages(ref GroupOperationResult result)
		{
			if (this.sourceFolder.ReferencedObject.Id.Equals(this.destinationFolder.ReferencedObject.Id) && result.OperationResult == OperationResult.PartiallySucceeded && result.Exception is PartialCompletionException && result.Exception.InnerException is MapiExceptionPartialCompletion)
			{
				StoreSession session = this.sourceFolder.ReferencedObject.Session;
				StoreObjectId storeObjectId = null;
				MailboxSession mailboxSession = session as MailboxSession;
				if (mailboxSession != null)
				{
					storeObjectId = mailboxSession.GetDefaultFolderId(DefaultFolderType.RecoverableItemsDeletions);
				}
				else
				{
					PublicFolderSession publicFolderSession = session as PublicFolderSession;
					if (publicFolderSession != null)
					{
						storeObjectId = PublicFolderCOWSession.GetRecoverableItemsDeletionsFolderId(this.sourceFolder.ReferencedObject);
					}
				}
				if (storeObjectId != null && !this.sourceFolder.ReferencedObject.Id.ObjectId.Equals(storeObjectId))
				{
					StoreObjectId[] sourceItemIds = SegmentedRopOperation.ConvertMessageIds(session.IdConverter, storeObjectId, result.ObjectIds);
					using (CoreFolder coreFolder = CoreFolder.Bind(session, storeObjectId))
					{
						ExTraceGlobals.FailedRopTracer.TraceDebug<int>((long)this.GetHashCode(), "Retry CopyItems from Dumpster with {0} items", result.ObjectIds.Count);
						GroupOperationResult groupOperationResult = coreFolder.CopyItems(this.destinationFolder.ReferencedObject, sourceItemIds, null, null, null);
						if (groupOperationResult.OperationResult != OperationResult.Failed)
						{
							result = groupOperationResult;
						}
					}
				}
			}
		}

		// Token: 0x0600023D RID: 573 RVA: 0x00015044 File Offset: 0x00013244
		protected override void InternalDispose()
		{
			if (this.sourceFolder != null)
			{
				this.sourceFolder.Release();
			}
			if (this.destinationFolder != null)
			{
				this.destinationFolder.Release();
			}
			Util.DisposeIfPresent(this.moveCopyMessagesEnumerator);
			base.InternalDispose();
		}

		// Token: 0x0600023E RID: 574 RVA: 0x0001507F File Offset: 0x0001327F
		protected override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<MoveCopyMessagesSegmentedOperation>(this);
		}

		// Token: 0x040000CE RID: 206
		private readonly TeamMailboxClientOperations teamMailboxClientOperations;

		// Token: 0x040000CF RID: 207
		private readonly ReferenceCount<CoreFolder> sourceFolder;

		// Token: 0x040000D0 RID: 208
		private readonly ReferenceCount<CoreFolder> destinationFolder;

		// Token: 0x040000D1 RID: 209
		private readonly bool isCopy;

		// Token: 0x040000D2 RID: 210
		private readonly IdsSegmentEnumerator moveCopyMessagesEnumerator;
	}
}
