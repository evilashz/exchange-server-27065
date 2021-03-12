using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Wcf.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x020009DF RID: 2527
	internal sealed class DeleteTaskFolderCommand : ServiceCommand<TaskFolderActionResponse>
	{
		// Token: 0x0600475C RID: 18268 RVA: 0x00100020 File Offset: 0x000FE220
		public DeleteTaskFolderCommand(CallContext callContext, Microsoft.Exchange.Services.Core.Types.ItemId itemId) : base(callContext)
		{
			this.itemId = itemId;
		}

		// Token: 0x0600475D RID: 18269 RVA: 0x00100030 File Offset: 0x000FE230
		protected override TaskFolderActionResponse InternalExecute()
		{
			if (this.itemId == null || string.IsNullOrEmpty(this.itemId.Id) || string.IsNullOrEmpty(this.itemId.ChangeKey))
			{
				ExTraceGlobals.DeleteTaskFolderCallTracer.TraceError<string, string>((long)this.GetHashCode(), "FolderId provided is invalid. FolderId.Id: '{0}' FolderId.ChangeKey: '{1}'", (this.itemId == null || this.itemId.Id == null) ? "is null" : this.itemId.Id, (this.itemId == null || this.itemId.ChangeKey == null) ? "is null" : this.itemId.ChangeKey);
				return new TaskFolderActionResponse(TaskFolderActionError.TaskFolderActionInvalidItemId);
			}
			TaskFolderActionResponse result;
			try
			{
				IdAndSession idAndSession = base.IdConverter.ConvertItemIdToIdAndSessionReadWrite(this.itemId);
				result = new DeleteTaskFolder(base.MailboxIdentityMailboxSession, idAndSession.Id).Execute();
			}
			catch (StoragePermanentException ex)
			{
				ExTraceGlobals.DeleteTaskFolderCallTracer.TraceError((long)this.GetHashCode(), "StoragePermanentException thrown while trying to delete task folder. ItemId.Id: {0}, ItemId.ChangeKey: {1} ExceptionInfo: {2}. CallStack: {3}", new object[]
				{
					this.itemId.Id,
					this.itemId.ChangeKey,
					ex.Message,
					ex.StackTrace
				});
				result = new TaskFolderActionFolderIdResponse(TaskFolderActionError.TaskFolderActionCannotDeleteTaskFolder);
			}
			catch (StorageTransientException ex2)
			{
				ExTraceGlobals.DeleteTaskFolderCallTracer.TraceError((long)this.GetHashCode(), "StorageTransientException thrown while trying to delete TaskFolder. ItemId.Id: {0}, ItemId.ChangeKey: {1} ExceptionInfo: {2}. CallStack: {3}", new object[]
				{
					this.itemId.Id,
					this.itemId.ChangeKey,
					ex2.Message,
					ex2.StackTrace
				});
				result = new TaskFolderActionFolderIdResponse(TaskFolderActionError.TaskFolderActionCannotDeleteTaskFolder);
			}
			return result;
		}

		// Token: 0x040028F0 RID: 10480
		private readonly Microsoft.Exchange.Services.Core.Types.ItemId itemId;
	}
}
