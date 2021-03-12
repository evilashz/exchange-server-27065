using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Wcf.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x020009DB RID: 2523
	internal sealed class CreateTaskFolderCommand : ServiceCommand<TaskFolderActionFolderIdResponse>
	{
		// Token: 0x06004753 RID: 18259 RVA: 0x000FF6C2 File Offset: 0x000FD8C2
		public CreateTaskFolderCommand(CallContext callContext, string newTaskFolderName, string parentGroupGuid) : base(callContext)
		{
			this.newTaskFolderName = newTaskFolderName;
			this.parentGroupGuid = parentGroupGuid;
		}

		// Token: 0x06004754 RID: 18260 RVA: 0x000FF6DC File Offset: 0x000FD8DC
		protected override TaskFolderActionFolderIdResponse InternalExecute()
		{
			TaskFolderActionFolderIdResponse result;
			try
			{
				result = new CreateTaskFolder(base.MailboxIdentityMailboxSession, this.newTaskFolderName, this.parentGroupGuid).Execute();
			}
			catch (StoragePermanentException ex)
			{
				ExTraceGlobals.CreateTaskFolderCallTracer.TraceError((long)this.GetHashCode(), "StoragePermanentException thrown while trying to create TaskFolder with name: {0}. ParentGroupGuid: {1}, ExceptionInfo: {2}. CallStack: {3}", new object[]
				{
					(this.newTaskFolderName == null) ? "is null" : this.newTaskFolderName,
					(this.parentGroupGuid == null) ? "is null" : this.parentGroupGuid,
					ex.Message,
					ex.StackTrace
				});
				result = new TaskFolderActionFolderIdResponse(TaskFolderActionError.TaskFolderActionUnableToCreateTaskFolder);
			}
			catch (StorageTransientException ex2)
			{
				ExTraceGlobals.CreateTaskFolderCallTracer.TraceError((long)this.GetHashCode(), "StorageTransientException thrown while trying to create TaskFolder with name: {0}. ParentGroupGuid: {1}, ExceptionInfo: {2}. CallStack: {3}", new object[]
				{
					(this.newTaskFolderName == null) ? "is null" : this.newTaskFolderName,
					(this.parentGroupGuid == null) ? "is null" : this.parentGroupGuid,
					ex2.Message,
					ex2.StackTrace
				});
				result = new TaskFolderActionFolderIdResponse(TaskFolderActionError.TaskFolderActionUnableToCreateTaskFolder);
			}
			return result;
		}

		// Token: 0x040028E8 RID: 10472
		private readonly string newTaskFolderName;

		// Token: 0x040028E9 RID: 10473
		private readonly string parentGroupGuid;
	}
}
