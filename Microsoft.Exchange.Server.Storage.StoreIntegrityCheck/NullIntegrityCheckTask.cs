using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ManagedStore.StoreIntegrityCheck;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.StoreCommonServices;

namespace Microsoft.Exchange.Server.Storage.StoreIntegrityCheck
{
	// Token: 0x0200002D RID: 45
	public class NullIntegrityCheckTask : IntegrityCheckTaskBase
	{
		// Token: 0x060000F3 RID: 243 RVA: 0x00007C55 File Offset: 0x00005E55
		public NullIntegrityCheckTask(TaskId unsupportedTaskId, IJobExecutionTracker tracker) : base(tracker)
		{
			this.unsupportedTaskId = unsupportedTaskId;
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x060000F4 RID: 244 RVA: 0x00007C65 File Offset: 0x00005E65
		public override string TaskName
		{
			get
			{
				return "NotSupportedIntegrityCheckTask";
			}
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x00007C6C File Offset: 0x00005E6C
		public override ErrorCode Execute(Context context, MailboxEntry mailboxEntry, bool detectOnly, Func<bool> shouldContinue)
		{
			if (ExTraceGlobals.OnlineIsintegTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				ExTraceGlobals.OnlineIsintegTracer.TraceDebug<Guid, TaskId>(0L, "Unsupported task invoked on mailbox {0}, task id= {1}", mailboxEntry.MailboxGuid, this.unsupportedTaskId);
			}
			TaskId taskId = this.unsupportedTaskId;
			switch (taskId)
			{
			case TaskId.FolderView:
			case TaskId.ProvisionedFolder:
			case TaskId.ReplState:
			case TaskId.MessagePtagCn:
				return ErrorCode.NoError;
			case TaskId.AggregateCounts:
				break;
			default:
				switch (taskId)
				{
				case TaskId.Extension1:
				case TaskId.Extension2:
				case TaskId.Extension3:
				case TaskId.Extension4:
				case TaskId.Extension5:
					return ErrorCode.NoError;
				}
				break;
			}
			return ErrorCode.CreateNotSupported((LID)44124U);
		}

		// Token: 0x040000A9 RID: 169
		private readonly TaskId unsupportedTaskId;
	}
}
