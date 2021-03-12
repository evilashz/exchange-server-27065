using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.Services.Wcf.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x020009E0 RID: 2528
	internal class DeleteTaskFolder : TaskFolderActionBase<TaskFolderActionResponse>
	{
		// Token: 0x0600475E RID: 18270 RVA: 0x001001DC File Offset: 0x000FE3DC
		public DeleteTaskFolder(MailboxSession session, StoreId taskFolderEntryId) : base(session)
		{
			this.taskFolderEntryId = taskFolderEntryId;
		}

		// Token: 0x0600475F RID: 18271 RVA: 0x001001EC File Offset: 0x000FE3EC
		public override TaskFolderActionResponse Execute()
		{
			MailboxSession mailboxSession = base.MailboxSession;
			StoreObjectId storeObjectId = null;
			using (TaskGroupEntry taskGroupEntry = TaskGroupEntry.Bind(mailboxSession, this.taskFolderEntryId))
			{
				ExTraceGlobals.DeleteTaskFolderCallTracer.TraceDebug<string, VersionedId>((long)this.GetHashCode(), "Successfully bound to TaskFolderGroupEntry for deletion. TaskFolderName: {0} Id: {1}", (taskGroupEntry.FolderName == null) ? string.Empty : taskGroupEntry.FolderName, taskGroupEntry.Id);
				storeObjectId = taskGroupEntry.TaskFolderId;
				TaskFolderActionError taskFolderActionError = this.DeleteTaskFolderFolder(storeObjectId);
				if (taskFolderActionError != TaskFolderActionError.None)
				{
					return new TaskFolderActionResponse(taskFolderActionError);
				}
			}
			AggregateOperationResult aggregateOperationResult = mailboxSession.Delete(DeleteItemFlags.HardDelete, new StoreId[]
			{
				this.taskFolderEntryId
			});
			if (aggregateOperationResult.OperationResult != OperationResult.Succeeded)
			{
				ExTraceGlobals.DeleteTaskFolderCallTracer.TraceError<string, StoreId, OperationResult>((long)this.GetHashCode(), "Unable to delete TaskFolder group entry. TaskFolderId: '{0}'. TaskFolderNodeId: '{1}' Result: {2}", (storeObjectId == null) ? string.Empty : storeObjectId.ToBase64String(), this.taskFolderEntryId, aggregateOperationResult.OperationResult);
			}
			return new TaskFolderActionResponse();
		}

		// Token: 0x06004760 RID: 18272 RVA: 0x001002DC File Offset: 0x000FE4DC
		private TaskFolderActionError DeleteTaskFolderFolder(StoreObjectId taskFolderObjectId)
		{
			MailboxSession mailboxSession = base.MailboxSession;
			StoreObjectId defaultFolderId = mailboxSession.GetDefaultFolderId(DefaultFolderType.ToDoSearch);
			StoreObjectId defaultFolderId2 = mailboxSession.GetDefaultFolderId(DefaultFolderType.Tasks);
			if (defaultFolderId.Equals(taskFolderObjectId) || defaultFolderId2.Equals(taskFolderObjectId))
			{
				ExTraceGlobals.DeleteTaskFolderCallTracer.TraceError((long)this.GetHashCode(), "FolderId is the default TaskFolder");
				return TaskFolderActionError.TaskFolderActionFolderIdIsDefaultTaskFolder;
			}
			ExTraceGlobals.DeleteTaskFolderCallTracer.TraceDebug<StoreObjectId>((long)this.GetHashCode(), "Attempting to delete TaskFolder folder. Id: '{0}'", taskFolderObjectId);
			AggregateOperationResult aggregateOperationResult = mailboxSession.Delete(DeleteItemFlags.MoveToDeletedItems, new StoreId[]
			{
				taskFolderObjectId
			});
			if (aggregateOperationResult.OperationResult != OperationResult.Succeeded)
			{
				ExTraceGlobals.DeleteTaskFolderCallTracer.TraceError<StoreObjectId, OperationResult>((long)this.GetHashCode(), "Unable to delete task folder. Id: '{0}'. Result: {1}", taskFolderObjectId, aggregateOperationResult.OperationResult);
				return TaskFolderActionError.TaskFolderActionCannotDeleteTaskFolder;
			}
			return TaskFolderActionError.None;
		}

		// Token: 0x040028F1 RID: 10481
		private readonly StoreId taskFolderEntryId;
	}
}
