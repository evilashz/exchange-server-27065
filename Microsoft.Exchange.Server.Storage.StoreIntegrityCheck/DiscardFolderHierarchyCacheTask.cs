using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ManagedStore.StoreIntegrityCheck;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.LogicalDataModel;
using Microsoft.Exchange.Server.Storage.StoreCommonServices;

namespace Microsoft.Exchange.Server.Storage.StoreIntegrityCheck
{
	// Token: 0x02000008 RID: 8
	public class DiscardFolderHierarchyCacheTask : IntegrityCheckTaskBase
	{
		// Token: 0x06000026 RID: 38 RVA: 0x00003EAA File Offset: 0x000020AA
		public DiscardFolderHierarchyCacheTask(IJobExecutionTracker tracker) : base(tracker)
		{
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000027 RID: 39 RVA: 0x00003EB3 File Offset: 0x000020B3
		public override string TaskName
		{
			get
			{
				return "DiscardFolderHierarchyCache";
			}
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00003F0C File Offset: 0x0000210C
		public override ErrorCode Execute(Context context, MailboxEntry mailboxEntry, bool detectOnly, Func<bool> shouldContinue)
		{
			if (ExTraceGlobals.OnlineIsintegTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				ExTraceGlobals.OnlineIsintegTracer.TraceDebug<Guid, bool>(0L, "DiscardFolderHierarchyCacheTask.Execute invoked on mailbox {0}, detect only = {1}", mailboxEntry.MailboxGuid, detectOnly);
			}
			if (!shouldContinue())
			{
				if (ExTraceGlobals.OnlineIsintegTracer.IsTraceEnabled(TraceType.ErrorTrace))
				{
					ExTraceGlobals.OnlineIsintegTracer.TraceError(0L, "Task aborted");
				}
				return ErrorCode.CreateExiting((LID)33036U);
			}
			ErrorCode errorCode = IntegrityCheckTaskBase.LockMailboxForOperation(context, mailboxEntry.MailboxNumber, delegate(MailboxState mailboxState)
			{
				ErrorCode noError;
				try
				{
					if (!detectOnly)
					{
						FolderHierarchy.DiscardFolderHierarchyCache(context, mailboxState);
					}
					noError = ErrorCode.NoError;
				}
				finally
				{
					context.Abort();
				}
				return noError;
			});
			if (ExTraceGlobals.OnlineIsintegTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				ExTraceGlobals.OnlineIsintegTracer.TraceDebug<Guid, ErrorCode>(0L, "DiscardFolderHierarchyCacheTask.Execute finished on mailbox {0} with error code {1}", mailboxEntry.MailboxGuid, errorCode);
			}
			return errorCode;
		}
	}
}
