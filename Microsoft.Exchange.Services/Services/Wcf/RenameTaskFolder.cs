using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Wcf.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x020009DE RID: 2526
	internal class RenameTaskFolder : TaskFolderActionBase<TaskFolderActionFolderIdResponse>
	{
		// Token: 0x06004759 RID: 18265 RVA: 0x000FFD60 File Offset: 0x000FDF60
		public RenameTaskFolder(MailboxSession session, StoreId taskFolderToRename, string newTaskFolderName) : base(session)
		{
			this.taskFolderToRename = taskFolderToRename;
			this.newTaskFolderName = ((newTaskFolderName != null) ? newTaskFolderName.Trim() : null);
		}

		// Token: 0x0600475A RID: 18266 RVA: 0x000FFD84 File Offset: 0x000FDF84
		public override TaskFolderActionFolderIdResponse Execute()
		{
			MailboxSession mailboxSession = base.MailboxSession;
			if (string.IsNullOrEmpty(this.newTaskFolderName))
			{
				ExTraceGlobals.RenameTaskFolderCallTracer.TraceError<string>((long)this.GetHashCode(), "New TaskFolder name provided is invalid. Name: '{0}'", (this.newTaskFolderName == null) ? "is null" : this.newTaskFolderName);
				return new TaskFolderActionFolderIdResponse(TaskFolderActionError.TaskFolderActionInvalidTaskFolderName);
			}
			FolderId folderId = null;
			Microsoft.Exchange.Services.Core.Types.ItemId taskFolderEntryId = null;
			using (TaskGroupEntry taskGroupEntry = TaskGroupEntry.Bind(mailboxSession, this.taskFolderToRename))
			{
				string folderName = taskGroupEntry.FolderName;
				ExTraceGlobals.RenameTaskFolderCallTracer.TraceDebug<VersionedId, string>((long)this.GetHashCode(), "Successfully bound to MEDS.TaskGroupEntry. Id: '{0}', OldTaskFolderName: '{1}'", taskGroupEntry.Id, (folderName == null) ? "is null" : folderName);
				TaskFolderActionError taskFolderActionError = this.RenameTaskFolderFolder(taskGroupEntry.TaskFolderId, out folderId);
				if (taskFolderActionError != TaskFolderActionError.None)
				{
					return new TaskFolderActionFolderIdResponse(taskFolderActionError);
				}
				ExTraceGlobals.RenameTaskFolderCallTracer.TraceDebug((long)this.GetHashCode(), "Updating TaskFolder group entry after renaming TaskFolder.");
				taskGroupEntry.FolderName = this.newTaskFolderName;
				ConflictResolutionResult conflictResolutionResult = taskGroupEntry.Save(SaveMode.NoConflictResolution);
				if (conflictResolutionResult.SaveStatus == SaveResult.IrresolvableConflict)
				{
					ExTraceGlobals.RenameTaskFolderCallTracer.TraceError((long)this.GetHashCode(), "Could not update TaskFolder group entry with new TaskFolder name.");
					return new TaskFolderActionFolderIdResponse(TaskFolderActionError.TaskFolderActionCannotRenameTaskFolderNode);
				}
				taskGroupEntry.Load();
				taskFolderEntryId = IdConverter.ConvertStoreItemIdToItemId(taskGroupEntry.Id, base.MailboxSession);
			}
			return new TaskFolderActionFolderIdResponse(folderId, taskFolderEntryId);
		}

		// Token: 0x0600475B RID: 18267 RVA: 0x000FFED0 File Offset: 0x000FE0D0
		private TaskFolderActionError RenameTaskFolderFolder(StoreObjectId taskFolderObjectId, out FolderId newTaskFolderId)
		{
			newTaskFolderId = null;
			MailboxSession mailboxSession = base.MailboxSession;
			StoreObjectId defaultFolderId = mailboxSession.GetDefaultFolderId(DefaultFolderType.ToDoSearch);
			StoreObjectId defaultFolderId2 = mailboxSession.GetDefaultFolderId(DefaultFolderType.Tasks);
			if (defaultFolderId.Equals(taskFolderObjectId) || defaultFolderId2.Equals(taskFolderObjectId))
			{
				ExTraceGlobals.RenameTaskFolderCallTracer.TraceError((long)this.GetHashCode(), "FolderId is the default task folder");
				return TaskFolderActionError.TaskFolderActionFolderIdIsDefaultTaskFolder;
			}
			ExTraceGlobals.RenameTaskFolderCallTracer.TraceDebug<StoreObjectId, string>((long)this.GetHashCode(), "Renaming task folder with Id: '{0}'. NewName: '{1}'", taskFolderObjectId, this.newTaskFolderName);
			using (Folder folder = Folder.Bind(mailboxSession, taskFolderObjectId))
			{
				ExTraceGlobals.RenameTaskFolderCallTracer.TraceDebug<string>((long)this.GetHashCode(), "Successfully bound to TaskFolder. Old TaskFolder name: '{0}'", (folder.DisplayName == null) ? "is null" : folder.DisplayName);
				folder.DisplayName = this.newTaskFolderName;
				FolderSaveResult folderSaveResult = folder.Save(SaveMode.NoConflictResolution);
				if (folderSaveResult.OperationResult == OperationResult.Failed)
				{
					ExTraceGlobals.RenameTaskFolderCallTracer.TraceError<string, StoreObjectId>((long)this.GetHashCode(), "Could not change TaskFolder folder name. NewName: '{0}', FolderId: '{1}'", this.newTaskFolderName, taskFolderObjectId);
					return TaskFolderActionError.TaskFolderActionCannotRename;
				}
				if (folderSaveResult.OperationResult == OperationResult.PartiallySucceeded && folderSaveResult.PropertyErrors.Length == 1 && folderSaveResult.PropertyErrors[0].PropertyDefinition == FolderSchema.DisplayName)
				{
					return TaskFolderActionError.TaskFolderActionTaskFolderAlreadyExists;
				}
				folder.Load();
				newTaskFolderId = IdConverter.GetFolderIdFromStoreId(folder.Id, new MailboxId(mailboxSession));
			}
			return TaskFolderActionError.None;
		}

		// Token: 0x040028EE RID: 10478
		private readonly StoreId taskFolderToRename;

		// Token: 0x040028EF RID: 10479
		private readonly string newTaskFolderName;
	}
}
