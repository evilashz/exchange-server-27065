using System;
using System.Diagnostics;
using System.Threading;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Win32;

namespace Microsoft.Exchange.AnchorService
{
	// Token: 0x02000026 RID: 38
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class OperationTracker : DisposeTrackableBase
	{
		// Token: 0x060001B2 RID: 434 RVA: 0x00006950 File Offset: 0x00004B50
		private OperationTracker(string context, ILogger logger)
		{
			ThreadTimes.GetFromCurrentThread(out this.startingKernelTime, out this.startingUserTime);
			this.context = context;
			this.logger = logger;
			this.timeTracker = Stopwatch.StartNew();
			this.logger.Log(MigrationEventType.Instrumentation, "BEGIN: [TID:{0}] [{1} ms] [{2} ms Kernel] [{3} ms User] - {4}", new object[]
			{
				Thread.CurrentThread.ManagedThreadId,
				this.timeTracker.ElapsedMilliseconds,
				this.startingKernelTime.TotalMilliseconds,
				this.startingUserTime.TotalMilliseconds,
				this.context
			});
		}

		// Token: 0x060001B3 RID: 435 RVA: 0x000069FC File Offset: 0x00004BFC
		public static OperationTracker Create(ILogger logger, string contextFormat, params object[] contextArgs)
		{
			string text = (contextArgs.Length == 0) ? contextFormat : string.Format(contextFormat, contextArgs);
			return new OperationTracker(text, logger);
		}

		// Token: 0x060001B4 RID: 436 RVA: 0x00006A20 File Offset: 0x00004C20
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<OperationTracker>(this);
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x00006A28 File Offset: 0x00004C28
		protected override void InternalDispose(bool disposing)
		{
			this.timeTracker.Stop();
			TimeSpan t;
			TimeSpan t2;
			ThreadTimes.GetFromCurrentThread(out t, out t2);
			this.logger.Log(MigrationEventType.Instrumentation, "END: [TID:{0}] [{1} ms] [{2} ms Kernel] [{3} ms User] - {4}", new object[]
			{
				Thread.CurrentThread.ManagedThreadId,
				this.timeTracker.ElapsedMilliseconds,
				(t - this.startingKernelTime).TotalMilliseconds,
				(t2 - this.startingUserTime).TotalMilliseconds,
				this.context
			});
		}

		// Token: 0x0400007C RID: 124
		private readonly string context;

		// Token: 0x0400007D RID: 125
		private readonly ILogger logger;

		// Token: 0x0400007E RID: 126
		private readonly Stopwatch timeTracker;

		// Token: 0x0400007F RID: 127
		private readonly TimeSpan startingUserTime;

		// Token: 0x04000080 RID: 128
		private readonly TimeSpan startingKernelTime;
	}
}
