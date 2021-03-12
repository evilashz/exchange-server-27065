using System;
using System.Threading;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.InfoWorker.Common;

namespace Microsoft.Exchange.InfoWorker.Common.Search
{
	// Token: 0x0200022D RID: 557
	internal class ResponseThrottler
	{
		// Token: 0x06000F35 RID: 3893 RVA: 0x00043F87 File Offset: 0x00042187
		internal ResponseThrottler() : this(null)
		{
		}

		// Token: 0x06000F36 RID: 3894 RVA: 0x00043F90 File Offset: 0x00042190
		internal ResponseThrottler(WaitHandle abortHandle)
		{
			this.random = new Random();
			this.BackOffDelay = this.random.Next(1, 11);
			this.abortHandle = abortHandle;
		}

		// Token: 0x170003E2 RID: 994
		// (get) Token: 0x06000F37 RID: 3895 RVA: 0x00043FBE File Offset: 0x000421BE
		// (set) Token: 0x06000F38 RID: 3896 RVA: 0x00043FC6 File Offset: 0x000421C6
		internal int BackOffDelay
		{
			get
			{
				return this.backOffDelay;
			}
			set
			{
				this.backOffDelay = value;
			}
		}

		// Token: 0x170003E3 RID: 995
		// (get) Token: 0x06000F39 RID: 3897 RVA: 0x00043FCF File Offset: 0x000421CF
		// (set) Token: 0x06000F3A RID: 3898 RVA: 0x00043FD7 File Offset: 0x000421D7
		internal WaitHandle AbortHandle
		{
			get
			{
				return this.abortHandle;
			}
			set
			{
				this.abortHandle = value;
			}
		}

		// Token: 0x170003E4 RID: 996
		// (get) Token: 0x06000F3B RID: 3899 RVA: 0x00043FE0 File Offset: 0x000421E0
		internal static int MaxRunningCopySearches
		{
			get
			{
				return SearchMailboxExecuter.GetSettingsValue("MaximumCopySearches", 4);
			}
		}

		// Token: 0x170003E5 RID: 997
		// (get) Token: 0x06000F3C RID: 3900 RVA: 0x00043FED File Offset: 0x000421ED
		internal static int MaxRunningSearches
		{
			get
			{
				return SearchMailboxExecuter.GetSettingsValue("MaximumRunningSearches", 8);
			}
		}

		// Token: 0x170003E6 RID: 998
		// (get) Token: 0x06000F3D RID: 3901 RVA: 0x00043FFA File Offset: 0x000421FA
		internal static int MaxThreadLimitPerEstimateSearch
		{
			get
			{
				return SearchMailboxExecuter.GetSettingsValue("MaximumThreadLimitPerEstimateSearch", 32);
			}
		}

		// Token: 0x170003E7 RID: 999
		// (get) Token: 0x06000F3E RID: 3902 RVA: 0x00044008 File Offset: 0x00042208
		internal static int MaxThreadLimitPerServerEstimateSearch
		{
			get
			{
				return SearchMailboxExecuter.GetSettingsValue("MaximumThreadLimitPerServerEstimateSearch", 6);
			}
		}

		// Token: 0x170003E8 RID: 1000
		// (get) Token: 0x06000F3F RID: 3903 RVA: 0x00044015 File Offset: 0x00042215
		internal static int MaxThreadLimitPerCopySearch
		{
			get
			{
				return SearchMailboxExecuter.GetSettingsValue("MaximumThreadLimitPerCopySearch", 1);
			}
		}

		// Token: 0x170003E9 RID: 1001
		// (get) Token: 0x06000F40 RID: 3904 RVA: 0x00044022 File Offset: 0x00042222
		internal static int MaxThreadLimitPerServerCopySearch
		{
			get
			{
				return SearchMailboxExecuter.GetSettingsValue("MaximumThreadLimitPerServerCopySearch", 1);
			}
		}

		// Token: 0x170003EA RID: 1002
		// (get) Token: 0x06000F41 RID: 3905 RVA: 0x0004402F File Offset: 0x0004222F
		internal static int MaxBulkSize
		{
			get
			{
				return SearchMailboxExecuter.GetSettingsValue("MaximumBulkSize", 128);
			}
		}

		// Token: 0x06000F42 RID: 3906 RVA: 0x00044040 File Offset: 0x00042240
		internal void BackOffFromStore(MailboxSession storeSession)
		{
			bool flag = false;
			lock (storeSession)
			{
				flag = storeSession.IsInBackoffState;
			}
			if (flag)
			{
				ResponseThrottler.Tracer.TraceDebug<MailboxSession, int>((long)this.GetHashCode(), "The store {0} is busy, backing off {1} milli-seconds", storeSession, this.BackOffDelay);
				this.Backoff();
				return;
			}
			this.ResetBackoffDelay();
		}

		// Token: 0x06000F43 RID: 3907 RVA: 0x000440AC File Offset: 0x000422AC
		private void Backoff()
		{
			if (this.AbortHandle != null)
			{
				bool flag = this.AbortHandle.WaitOne(this.BackOffDelay, false);
				if (flag)
				{
					throw new BackoffAbortedException();
				}
			}
			else
			{
				Thread.Sleep(this.BackOffDelay);
			}
			if (this.BackOffDelay < 1024)
			{
				this.BackOffDelay <<= 2;
			}
		}

		// Token: 0x06000F44 RID: 3908 RVA: 0x00044103 File Offset: 0x00042303
		private void ResetBackoffDelay()
		{
			this.BackOffDelay = this.random.Next(1, 11);
		}

		// Token: 0x04000A6D RID: 2669
		internal const int MinInitBackOffDelay = 1;

		// Token: 0x04000A6E RID: 2670
		internal const int MaxInitBackOffDelay = 10;

		// Token: 0x04000A6F RID: 2671
		internal const int BackOffShift = 2;

		// Token: 0x04000A70 RID: 2672
		internal const int MaxBackOffDelay = 1024;

		// Token: 0x04000A71 RID: 2673
		internal const int MaxBackoffRetry = 20;

		// Token: 0x04000A72 RID: 2674
		internal const int MaxNonCriticalFailsPerMailbox = 3;

		// Token: 0x04000A73 RID: 2675
		internal const int SearchFolderTimeOutSecs = 180;

		// Token: 0x04000A74 RID: 2676
		protected static readonly Trace Tracer = ExTraceGlobals.SearchTracer;

		// Token: 0x04000A75 RID: 2677
		private Random random;

		// Token: 0x04000A76 RID: 2678
		private int backOffDelay;

		// Token: 0x04000A77 RID: 2679
		private WaitHandle abortHandle;
	}
}
