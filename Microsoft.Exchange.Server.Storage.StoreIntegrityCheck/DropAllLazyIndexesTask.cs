using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ManagedStore.StoreIntegrityCheck;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.LazyIndexing;
using Microsoft.Exchange.Server.Storage.StoreCommonServices;

namespace Microsoft.Exchange.Server.Storage.StoreIntegrityCheck
{
	// Token: 0x02000009 RID: 9
	public class DropAllLazyIndexesTask : IntegrityCheckTaskBase
	{
		// Token: 0x06000029 RID: 41 RVA: 0x00003FD2 File Offset: 0x000021D2
		public DropAllLazyIndexesTask(IJobExecutionTracker tracker) : base(tracker)
		{
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600002A RID: 42 RVA: 0x00003FDB File Offset: 0x000021DB
		public override string TaskName
		{
			get
			{
				return "DropAllLazyIndexes";
			}
		}

		// Token: 0x0600002B RID: 43 RVA: 0x0000406C File Offset: 0x0000226C
		public override ErrorCode Execute(Context context, MailboxEntry mailboxEntry, bool detectOnly, Func<bool> shouldContinue)
		{
			if (ExTraceGlobals.OnlineIsintegTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				ExTraceGlobals.OnlineIsintegTracer.TraceDebug<Guid, bool>(0L, "DropAllLazyIndexesTask.Execute invoked on mailbox {0}, detect only = {1}", mailboxEntry.MailboxGuid, detectOnly);
			}
			if (!shouldContinue())
			{
				if (ExTraceGlobals.OnlineIsintegTracer.IsTraceEnabled(TraceType.ErrorTrace))
				{
					ExTraceGlobals.OnlineIsintegTracer.TraceError(0L, "Task aborted");
				}
				return ErrorCode.CreateExiting((LID)64956U);
			}
			ErrorCode errorCode = IntegrityCheckTaskBase.LockMailboxForOperation(context, mailboxEntry.MailboxNumber, delegate(MailboxState mailboxState)
			{
				ErrorCode noError;
				try
				{
					if (!detectOnly)
					{
						bool flag;
						LogicalIndexCache.CleanupLogicalIndexes(context, mailboxState, TimeSpan.Zero, out flag);
						if (!flag)
						{
							if (ExTraceGlobals.OnlineIsintegTracer.IsTraceEnabled(TraceType.ErrorTrace))
							{
								ExTraceGlobals.OnlineIsintegTracer.TraceError(0L, "Cleanup not completed");
							}
							return ErrorCode.CreateExiting((LID)45852U);
						}
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
				ExTraceGlobals.OnlineIsintegTracer.TraceDebug<Guid, ErrorCode>(0L, "DropAllLazyIndexesTask.Execute finished on mailbox {0} with error code {1}", mailboxEntry.MailboxGuid, errorCode);
			}
			return errorCode;
		}
	}
}
