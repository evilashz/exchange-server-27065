using System;
using System.Collections.Concurrent;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Threading;

namespace Microsoft.Exchange.Net
{
	// Token: 0x02000766 RID: 1894
	internal class AsyncLog
	{
		// Token: 0x0600255D RID: 9565 RVA: 0x0004E726 File Offset: 0x0004C926
		public AsyncLog(string filePrefix, LogHeaderFormatter headerFormatter, string logComponent)
		{
			this.backingLog = new Log(filePrefix, headerFormatter, logComponent);
			this.component = logComponent;
		}

		// Token: 0x0600255E RID: 9566 RVA: 0x0004E764 File Offset: 0x0004C964
		public void Configure(string path, TimeSpan maxAge, long maxDirectorySize, long maxLogFileSize, int bufferSize, TimeSpan streamFlushInterval, TimeSpan backgroundWriteInterval)
		{
			if (backgroundWriteInterval < AsyncLog.MinimumAsyncLogInterval)
			{
				throw new ArgumentException(string.Format("The backgroundWriteInterval must exceed {0}.", AsyncLog.MinimumAsyncLogInterval), "backgroundWriteInterval");
			}
			this.backingLog.Configure(path, maxAge, maxDirectorySize, maxLogFileSize, bufferSize, streamFlushInterval);
			if (this.logWriterTimer != null)
			{
				this.logWriterTimer.Change(streamFlushInterval, streamFlushInterval);
				return;
			}
			this.logWriterTimer = new GuardedTimer(delegate(object state)
			{
				this.WriteRequests(false);
			}, null, backgroundWriteInterval, backgroundWriteInterval);
			this.enabled = true;
		}

		// Token: 0x0600255F RID: 9567 RVA: 0x0004E7F8 File Offset: 0x0004C9F8
		public void Append(LogRowFormatter row, int timestampField)
		{
			if (!this.enabled)
			{
				throw new InvalidOperationException("The log must first be configured before it can be written to.");
			}
			if (this.flushRequired || this.pendingRequests.Count > 50000)
			{
				this.WriteRequests(true);
			}
			this.pendingRequests.Enqueue(new AsyncLog.AppendRequest
			{
				Row = new LogRowFormatter(row),
				TimeStampField = timestampField,
				TimeStamp = DateTime.UtcNow
			});
		}

		// Token: 0x06002560 RID: 9568 RVA: 0x0004E86C File Offset: 0x0004CA6C
		public void WriteRequests(bool forcedFlush = false)
		{
			try
			{
				lock (this.loggingLock)
				{
					if (forcedFlush && this.pendingRequests.Count > 50000)
					{
						Log.EventLog.LogEvent(CommonEventLogConstants.Tuple_PendingLoggingRequestsReachedMaximum, this.backingLog.LogDirectory.FullName, new object[]
						{
							this.component,
							50000
						});
						this.flushRequired = true;
					}
					AsyncLog.AppendRequest appendRequest;
					while (this.pendingRequests.TryDequeue(out appendRequest))
					{
						this.backingLog.Append(appendRequest.Row, appendRequest.TimeStampField, appendRequest.TimeStamp);
					}
					this.flushRequired = false;
				}
			}
			catch (Exception)
			{
				throw;
			}
		}

		// Token: 0x06002561 RID: 9569 RVA: 0x0004E948 File Offset: 0x0004CB48
		public void Flush()
		{
			if (!this.enabled)
			{
				throw new InvalidOperationException("The log must first be configured before it can be flushed.");
			}
			this.WriteRequests(false);
			this.backingLog.Flush();
		}

		// Token: 0x06002562 RID: 9570 RVA: 0x0004E96F File Offset: 0x0004CB6F
		public void Close()
		{
			if (this.enabled)
			{
				this.logWriterTimer.Dispose(true);
				this.logWriterTimer = null;
				this.Flush();
				this.enabled = false;
			}
			this.backingLog.Close();
		}

		// Token: 0x040022B7 RID: 8887
		private const int MaximumQueueSize = 50000;

		// Token: 0x040022B8 RID: 8888
		private static readonly TimeSpan MinimumAsyncLogInterval = TimeSpan.FromMilliseconds(100.0);

		// Token: 0x040022B9 RID: 8889
		private readonly Log backingLog;

		// Token: 0x040022BA RID: 8890
		private readonly string component;

		// Token: 0x040022BB RID: 8891
		private ConcurrentQueue<AsyncLog.AppendRequest> pendingRequests = new ConcurrentQueue<AsyncLog.AppendRequest>();

		// Token: 0x040022BC RID: 8892
		private GuardedTimer logWriterTimer;

		// Token: 0x040022BD RID: 8893
		private bool enabled;

		// Token: 0x040022BE RID: 8894
		private bool flushRequired;

		// Token: 0x040022BF RID: 8895
		private object loggingLock = new object();

		// Token: 0x02000767 RID: 1895
		private class AppendRequest
		{
			// Token: 0x170009DB RID: 2523
			// (get) Token: 0x06002565 RID: 9573 RVA: 0x0004E9B9 File Offset: 0x0004CBB9
			// (set) Token: 0x06002566 RID: 9574 RVA: 0x0004E9C1 File Offset: 0x0004CBC1
			internal LogRowFormatter Row { get; set; }

			// Token: 0x170009DC RID: 2524
			// (get) Token: 0x06002567 RID: 9575 RVA: 0x0004E9CA File Offset: 0x0004CBCA
			// (set) Token: 0x06002568 RID: 9576 RVA: 0x0004E9D2 File Offset: 0x0004CBD2
			internal int TimeStampField { get; set; }

			// Token: 0x170009DD RID: 2525
			// (get) Token: 0x06002569 RID: 9577 RVA: 0x0004E9DB File Offset: 0x0004CBDB
			// (set) Token: 0x0600256A RID: 9578 RVA: 0x0004E9E3 File Offset: 0x0004CBE3
			internal DateTime TimeStamp { get; set; }
		}
	}
}
