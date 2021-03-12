using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.LinkedFolder;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.RpcClientAccess;
using Microsoft.Exchange.RpcClientAccess.Parser;
using Microsoft.Mapi;

namespace Microsoft.Exchange.RpcClientAccess.Handler
{
	// Token: 0x02000028 RID: 40
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal abstract class DeleteMessagesSegmentedOperation : SegmentedRopOperation
	{
		// Token: 0x06000217 RID: 535 RVA: 0x000137B0 File Offset: 0x000119B0
		protected DeleteMessagesSegmentedOperation(ReferenceCount<CoreFolder> folder, DeleteItemFlags deleteItemFlags, StoreObjectId[] storeObjectIds, int segmentSize, TeamMailboxClientOperations teamMailboxClientOperations) : base(((deleteItemFlags & DeleteItemFlags.HardDelete) == DeleteItemFlags.HardDelete) ? RopId.HardDeleteMessages : RopId.DeleteMessages)
		{
			using (DisposeGuard disposeGuard = this.Guard())
			{
				this.folder = folder;
				this.folder.AddRef();
				this.deleteItemFlags = deleteItemFlags;
				this.deleteMessagesEnumerator = new IdsSegmentEnumerator(storeObjectIds, segmentSize);
				base.TotalWork = this.deleteMessagesEnumerator.Count;
				this.teamMailboxClientOperations = teamMailboxClientOperations;
				disposeGuard.Success();
			}
		}

		// Token: 0x06000218 RID: 536 RVA: 0x00013874 File Offset: 0x00011A74
		protected override SegmentOperationResult InternalDoNextBatchOperation()
		{
			StoreObjectId[] ids = this.deleteMessagesEnumerator.GetNextBatchIds();
			if (ids.Length > 0)
			{
				GroupOperationResult groupOperationResult;
				if (this.teamMailboxClientOperations != null)
				{
					groupOperationResult = TeamMailboxExecutionHelper.RunGroupOperationsWithExecutionLimitHandler(() => this.teamMailboxClientOperations.OnDeleteMessages(this.folder.ReferencedObject, ids), "TeamMailboxClientOperations.OnDeleteMessages");
				}
				else
				{
					groupOperationResult = this.folder.ReferencedObject.Session.Delete(this.deleteItemFlags, ids).GroupOperationResults[0];
					this.PostDeleteMessages(ref groupOperationResult);
				}
				TestInterceptor.InterceptValue<GroupOperationResult>(TestInterceptorLocation.DeleteMessagesSegmentedOperation_InternalDoNextBatchOperation, ref groupOperationResult);
				return new SegmentOperationResult
				{
					CompletedWork = ids.Length,
					OperationResult = groupOperationResult.OperationResult,
					Exception = groupOperationResult.Exception,
					IsCompleted = false
				};
			}
			if (this.teamMailboxClientOperations != null)
			{
				((MailboxSession)this.folder.ReferencedObject.Session).TryToSyncSiteMailboxNow();
			}
			return SegmentedRopOperation.FinalResult;
		}

		// Token: 0x06000219 RID: 537 RVA: 0x00013978 File Offset: 0x00011B78
		private void PostDeleteMessages(ref GroupOperationResult result)
		{
			if ((this.deleteItemFlags & DeleteItemFlags.HardDelete) == DeleteItemFlags.HardDelete && result.OperationResult == OperationResult.PartiallySucceeded && result.Exception is PartialCompletionException && result.Exception.InnerException is MapiExceptionPartialCompletion)
			{
				StoreSession session = this.folder.ReferencedObject.Session;
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
						storeObjectId = PublicFolderCOWSession.GetRecoverableItemsDeletionsFolderId(this.folder.ReferencedObject);
					}
				}
				if (storeObjectId != null && !this.folder.ReferencedObject.Id.ObjectId.Equals(storeObjectId))
				{
					ExTraceGlobals.FailedRopTracer.TraceDebug<int>((long)this.GetHashCode(), "Retry HardDelete from Dumpster with {0} items", result.ObjectIds.Count);
					StoreObjectId[] ids = SegmentedRopOperation.ConvertMessageIds(session.IdConverter, storeObjectId, result.ObjectIds);
					using (CoreFolder coreFolder = CoreFolder.Bind(session, storeObjectId))
					{
						GroupOperationResult groupOperationResult = coreFolder.Session.Delete(this.deleteItemFlags, ids).GroupOperationResults[0];
						if (groupOperationResult.OperationResult != OperationResult.Failed)
						{
							result = groupOperationResult;
						}
					}
				}
			}
		}

		// Token: 0x0600021A RID: 538 RVA: 0x00013AB8 File Offset: 0x00011CB8
		protected override void InternalDispose()
		{
			Util.DisposeIfPresent(this.deleteMessagesEnumerator);
			if (this.folder != null)
			{
				this.folder.Release();
			}
			base.InternalDispose();
		}

		// Token: 0x0600021B RID: 539 RVA: 0x00013ADF File Offset: 0x00011CDF
		protected override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<DeleteMessagesSegmentedOperation>(this);
		}

		// Token: 0x040000BA RID: 186
		private readonly ReferenceCount<CoreFolder> folder;

		// Token: 0x040000BB RID: 187
		private readonly DeleteItemFlags deleteItemFlags;

		// Token: 0x040000BC RID: 188
		private readonly IdsSegmentEnumerator deleteMessagesEnumerator;

		// Token: 0x040000BD RID: 189
		private readonly TeamMailboxClientOperations teamMailboxClientOperations;
	}
}
