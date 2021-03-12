using System;
using System.Threading;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.StoreDriverSubmission;
using Microsoft.Exchange.MailboxTransport.StoreDriver;
using Microsoft.Exchange.Transport;

namespace Microsoft.Exchange.MailboxTransport.Submission.StoreDriverSubmission
{
	// Token: 0x02000034 RID: 52
	internal sealed class SubmissionThreadLimiter : DisposeTrackableBase
	{
		// Token: 0x170000AA RID: 170
		// (get) Token: 0x0600020B RID: 523 RVA: 0x0000C477 File Offset: 0x0000A677
		public static int ConcurrentSubmissions
		{
			get
			{
				return SubmissionThreadLimiter.concurrentSubmissions;
			}
		}

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x0600020C RID: 524 RVA: 0x0000C47E File Offset: 0x0000A67E
		public static SubmissionDatabaseThreadMap DatabaseThreadMap
		{
			get
			{
				return SubmissionThreadLimiter.databaseThreadMap;
			}
		}

		// Token: 0x0600020D RID: 525 RVA: 0x0000C488 File Offset: 0x0000A688
		public void BeginSubmission(ulong id, string server, string database)
		{
			int num = Interlocked.Increment(ref SubmissionThreadLimiter.concurrentSubmissions);
			if (num > Components.Configuration.LocalServer.MaxConcurrentMailboxSubmissions)
			{
				StoreDriverSubmission.LogEvent(MSExchangeStoreDriverSubmissionEventLogConstants.Tuple_TooManySubmissionThreads, null, new object[]
				{
					Components.Configuration.LocalServer.MaxConcurrentMailboxSubmissions
				});
				string message = string.Format("Total thread count exceeded limit of {0} threads.", Components.Configuration.LocalServer.MaxConcurrentMailboxSubmissions);
				throw new ThreadLimitExceededException(message);
			}
			if (SubmissionThreadLimiter.databaseThreadMap.ThreadLimit > 0)
			{
				SubmissionThreadLimiter.databaseThreadMap.CheckAndIncrement(database, id, database);
				this.databaseThreadMapEntry = database;
			}
		}

		// Token: 0x0600020E RID: 526 RVA: 0x0000C523 File Offset: 0x0000A723
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<SubmissionThreadLimiter>(this);
		}

		// Token: 0x0600020F RID: 527 RVA: 0x0000C52B File Offset: 0x0000A72B
		protected override void InternalDispose(bool disposing)
		{
			Interlocked.Decrement(ref SubmissionThreadLimiter.concurrentSubmissions);
			if (!disposing)
			{
				return;
			}
			if (this.databaseThreadMapEntry != null)
			{
				SubmissionThreadLimiter.databaseThreadMap.Decrement(this.databaseThreadMapEntry);
			}
		}

		// Token: 0x040000EE RID: 238
		private static readonly Trace diag = ExTraceGlobals.SubmissionConnectionPoolTracer;

		// Token: 0x040000EF RID: 239
		private static readonly SubmissionDatabaseThreadMap databaseThreadMap = new SubmissionDatabaseThreadMap(SubmissionThreadLimiter.diag);

		// Token: 0x040000F0 RID: 240
		private static int concurrentSubmissions;

		// Token: 0x040000F1 RID: 241
		private string databaseThreadMapEntry;
	}
}
