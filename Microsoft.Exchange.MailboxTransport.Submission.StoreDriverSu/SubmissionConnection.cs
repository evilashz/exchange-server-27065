using System;
using System.Threading;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.StoreDriverSubmission;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.MailboxTransport.StoreDriver;
using Microsoft.Exchange.Transport;
using Microsoft.Exchange.Transport.Logging.ConnectionLog;

namespace Microsoft.Exchange.MailboxTransport.Submission.StoreDriverSubmission
{
	// Token: 0x0200000A RID: 10
	internal sealed class SubmissionConnection
	{
		// Token: 0x06000025 RID: 37 RVA: 0x00003820 File Offset: 0x00001A20
		internal SubmissionConnection(string key, SubmissionConnectionPool pool, string server, string database)
		{
			this.key = key;
			this.pool = pool;
			this.server = server;
			this.database = database;
			this.id = SessionId.GetNextSessionId();
			TraceHelper.SubmissionConnectionTracer.TracePass<int, string>(TraceHelper.MessageProbeActivityId, (long)this.GetHashCode(), "SubmissionConnection.ctor: Thread {0}, Created: {1}.", Thread.CurrentThread.ManagedThreadId, this.ToString());
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000026 RID: 38 RVA: 0x00003886 File Offset: 0x00001A86
		public ulong Id
		{
			get
			{
				return this.id;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000027 RID: 39 RVA: 0x0000388E File Offset: 0x00001A8E
		public string Key
		{
			get
			{
				return this.key;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000028 RID: 40 RVA: 0x00003896 File Offset: 0x00001A96
		public bool IsInUse
		{
			get
			{
				return this.referenceCount > 0;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000029 RID: 41 RVA: 0x000038A1 File Offset: 0x00001AA1
		public int Failures
		{
			get
			{
				return this.failures;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600002A RID: 42 RVA: 0x000038A9 File Offset: 0x00001AA9
		public bool HasReachedSubmissionLimit
		{
			get
			{
				return this.totalSubmissions >= 20L;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600002B RID: 43 RVA: 0x000038B9 File Offset: 0x00001AB9
		public bool TimeoutElapsed
		{
			get
			{
				return ExDateTime.UtcNow > this.timeoutDeadline;
			}
		}

		// Token: 0x0600002C RID: 44 RVA: 0x000038CC File Offset: 0x00001ACC
		public void SubmissionSuccessful(long size, int recipients)
		{
			Interlocked.Increment(ref this.successfulSubmissions);
			Interlocked.Add(ref this.bytes, size);
			Interlocked.Add(ref this.recipients, (long)recipients);
			TraceHelper.SubmissionConnectionTracer.TracePass<int, string>(TraceHelper.MessageProbeActivityId, (long)this.GetHashCode(), "SubmissionConnection.SubmissionSuccessful: Thread {0}, Connection: {1}.", Thread.CurrentThread.ManagedThreadId, this.ToString());
			this.ReleaseReference();
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00003934 File Offset: 0x00001B34
		public void SubmissionAborted(string reason)
		{
			ConnectionLog.MapiSubmissionAborted(this.id, this.database, reason);
			TraceHelper.SubmissionConnectionTracer.TracePass<int, string, string>(TraceHelper.MessageProbeActivityId, (long)this.GetHashCode(), "SubmissionConnection.SubmissionAborted: Thread {0}, Connection: {1}, Reason: {1}", Thread.CurrentThread.ManagedThreadId, this.ToString(), reason);
			this.ReleaseReference();
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00003988 File Offset: 0x00001B88
		public void SubmissionFailed(string description)
		{
			Interlocked.Increment(ref this.failures);
			ConnectionLog.MapiSubmissionFailed(this.id, this.database, description);
			TraceHelper.SubmissionConnectionTracer.TracePass<int, string, string>(TraceHelper.MessageProbeActivityId, (long)this.GetHashCode(), "SubmissionConnection.SubmissionFailed: Thread {0}, Connection: {1}, Description: {2}", Thread.CurrentThread.ManagedThreadId, this.ToString(), description);
			this.ReleaseReference();
		}

		// Token: 0x0600002F RID: 47 RVA: 0x000039E8 File Offset: 0x00001BE8
		public override string ToString()
		{
			return string.Format("ID={0}, Key={1}, references={2}, totalSubmissions={3}, successfulSubmissions={4}, failures={5}, recipients={6}, bytes={7}", new object[]
			{
				this.id,
				this.key,
				this.referenceCount,
				this.totalSubmissions,
				this.successfulSubmissions,
				this.failures,
				this.recipients,
				this.bytes
			});
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00003A74 File Offset: 0x00001C74
		public void StartConnection()
		{
			ConnectionLog.MapiSubmissionConnectionStart(this.id, this.database, this.server);
			TraceHelper.SubmissionConnectionTracer.TracePass<int, string>(TraceHelper.MessageProbeActivityId, (long)this.GetHashCode(), "SubmissionConnection.StartConnection: Thread {0}, Connection: {1}.", Thread.CurrentThread.ManagedThreadId, this.ToString());
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00003AC3 File Offset: 0x00001CC3
		public void TimeoutExpired()
		{
			this.LogConnectionStopped(true);
			TraceHelper.SubmissionConnectionTracer.TracePass<int, string>(TraceHelper.MessageProbeActivityId, (long)this.GetHashCode(), "SubmissionConnection.TimeoutExpired: Thread {0}, Connection: {1}.", Thread.CurrentThread.ManagedThreadId, this.ToString());
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00003AF8 File Offset: 0x00001CF8
		public void SubmissionStarted()
		{
			Interlocked.Increment(ref this.referenceCount);
			Interlocked.Increment(ref this.totalSubmissions);
			this.timeoutDeadline = ExDateTime.UtcNow.Add(SubmissionConnection.timeoutInterval);
			TraceHelper.SubmissionConnectionTracer.TracePass<int, string>(TraceHelper.MessageProbeActivityId, (long)this.GetHashCode(), "SubmissionConnection.SubmissionStarted: Thread {0}, Connection: {1}.", Thread.CurrentThread.ManagedThreadId, this.ToString());
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00003B60 File Offset: 0x00001D60
		private void ReleaseReference()
		{
			long num = (long)Interlocked.Decrement(ref this.referenceCount);
			TraceHelper.SubmissionConnectionTracer.TracePass<int, string>(TraceHelper.MessageProbeActivityId, (long)this.GetHashCode(), "SubmissionConnection.ReleaseReference: Thread {0}, Connection: {1}.", Thread.CurrentThread.ManagedThreadId, this.ToString());
			if (num == 0L && this.pool.CanStopConnection(this))
			{
				TraceHelper.SubmissionConnectionTracer.TracePass<int, string>(TraceHelper.MessageProbeActivityId, (long)this.GetHashCode(), "SubmissionConnection.ReleaseReference: Thread {0}, stopping connection: {1}.", Thread.CurrentThread.ManagedThreadId, this.ToString());
				this.LogConnectionStopped(false);
			}
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00003BEC File Offset: 0x00001DEC
		private void LogConnectionStopped(bool idle)
		{
			ConnectionLog.MapiSubmissionConnectionStop(this.id, this.database, this.successfulSubmissions, 0L, this.bytes, this.recipients, this.failures, this.HasReachedSubmissionLimit, idle);
		}

		// Token: 0x0400000A RID: 10
		private const int MaxSubmissionsPerConnection = 20;

		// Token: 0x0400000B RID: 11
		private static readonly TimeSpan timeoutInterval = TimeSpan.FromSeconds(5.0);

		// Token: 0x0400000C RID: 12
		private static readonly Trace diag = ExTraceGlobals.SubmissionConnectionTracer;

		// Token: 0x0400000D RID: 13
		private readonly string key;

		// Token: 0x0400000E RID: 14
		private readonly SubmissionConnectionPool pool;

		// Token: 0x0400000F RID: 15
		private readonly string database;

		// Token: 0x04000010 RID: 16
		private readonly string server;

		// Token: 0x04000011 RID: 17
		private readonly ulong id;

		// Token: 0x04000012 RID: 18
		private int referenceCount;

		// Token: 0x04000013 RID: 19
		private long totalSubmissions;

		// Token: 0x04000014 RID: 20
		private long successfulSubmissions;

		// Token: 0x04000015 RID: 21
		private long bytes;

		// Token: 0x04000016 RID: 22
		private long recipients;

		// Token: 0x04000017 RID: 23
		private int failures;

		// Token: 0x04000018 RID: 24
		private ExDateTime timeoutDeadline;
	}
}
