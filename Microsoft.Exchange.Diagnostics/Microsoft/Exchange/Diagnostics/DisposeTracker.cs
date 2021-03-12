using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.ConstrainedExecution;
using System.Threading;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x0200001F RID: 31
	[CLSCompliant(true)]
	public abstract class DisposeTracker : CriticalFinalizerObject, IDisposable
	{
		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000079 RID: 121 RVA: 0x000032E4 File Offset: 0x000014E4
		// (set) Token: 0x0600007A RID: 122 RVA: 0x000032EB File Offset: 0x000014EB
		public static bool Suppressed
		{
			get
			{
				return DisposeTracker.suppressed;
			}
			set
			{
				DisposeTracker.suppressed = value;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600007B RID: 123 RVA: 0x000032F3 File Offset: 0x000014F3
		// (set) Token: 0x0600007C RID: 124 RVA: 0x000032FA File Offset: 0x000014FA
		public static Action<string, string> OnLeakDetected { get; set; }

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600007D RID: 125 RVA: 0x00003302 File Offset: 0x00001502
		// (set) Token: 0x0600007E RID: 126 RVA: 0x0000330A File Offset: 0x0000150A
		public bool HasCollectedStackTrace { get; protected set; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600007F RID: 127 RVA: 0x00003313 File Offset: 0x00001513
		// (set) Token: 0x06000080 RID: 128 RVA: 0x0000331B File Offset: 0x0000151B
		internal IList<WatsonExtraDataReportAction> ExtraDataList { get; private set; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000081 RID: 129 RVA: 0x00003324 File Offset: 0x00001524
		// (set) Token: 0x06000082 RID: 130 RVA: 0x0000332C File Offset: 0x0000152C
		protected StackTrace StackTrace
		{
			get
			{
				return this.stackTrace;
			}
			set
			{
				this.stackTrace = value;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000083 RID: 131 RVA: 0x00003335 File Offset: 0x00001535
		protected bool WasProperlyDisposed
		{
			get
			{
				return this.wasProperlyDisposed;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000084 RID: 132 RVA: 0x0000333D File Offset: 0x0000153D
		protected bool StackTraceWasReset
		{
			get
			{
				return this.stackTraceWasReset;
			}
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00003345 File Offset: 0x00001545
		public static DisposeTracker Get<T>(T obj) where T : IDisposable
		{
			if (DisposeTrackerOptions.Enabled || DisposeTracker.OnLeakDetected != null)
			{
				return new DisposeTrackerObject<T>();
			}
			return DisposeTrackerNullObject.Instance;
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00003360 File Offset: 0x00001560
		public static void ForceLeakExposure()
		{
			for (int i = 0; i <= GC.MaxGeneration; i++)
			{
				GC.Collect(i, GCCollectionMode.Forced, true);
			}
			for (int j = GC.MaxGeneration; j >= 0; j--)
			{
				GC.Collect(j, GCCollectionMode.Forced, true);
			}
			GC.WaitForPendingFinalizers();
		}

		// Token: 0x06000087 RID: 135 RVA: 0x000033A2 File Offset: 0x000015A2
		public static void ResetType<T>(T obj) where T : IDisposable
		{
			DisposeTrackerObject<T>.Reset();
		}

		// Token: 0x06000088 RID: 136 RVA: 0x000033A9 File Offset: 0x000015A9
		public void Suppress()
		{
			this.stackTrace = null;
		}

		// Token: 0x06000089 RID: 137 RVA: 0x000033B2 File Offset: 0x000015B2
		public void SetReportedStacktraceToCurrentLocation()
		{
			if (this.stackTrace != null)
			{
				this.stackTrace = new StackTrace(true);
				this.stackTraceWasReset = true;
			}
		}

		// Token: 0x0600008A RID: 138 RVA: 0x000033CF File Offset: 0x000015CF
		public void Dispose()
		{
			this.wasProperlyDisposed = true;
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600008B RID: 139 RVA: 0x000033E5 File Offset: 0x000015E5
		public void AddExtraData(string extraData)
		{
			if (this.ExtraDataList == null)
			{
				this.ExtraDataList = new List<WatsonExtraDataReportAction>();
			}
			this.ExtraDataList.Add(new WatsonExtraDataReportAction(extraData));
		}

		// Token: 0x0600008C RID: 140 RVA: 0x0000340C File Offset: 0x0000160C
		public void AddExtraDataWithStackTrace(string header)
		{
			if (this.HasCollectedStackTrace)
			{
				StackTrace stackTrace = new StackTrace(1, DisposeTrackerOptions.UseFullSymbols);
				this.AddExtraData(string.Format(CultureInfo.InvariantCulture, "{0}:{1}{2}", new object[]
				{
					header,
					Environment.NewLine,
					stackTrace
				}));
			}
		}

		// Token: 0x0600008D RID: 141 RVA: 0x0000345A File Offset: 0x0000165A
		protected static bool CanCollectAnotherStackTraceYet()
		{
			return (ulong)(Environment.TickCount - (int)DisposeTracker.lastStacktraceTicks) >= (ulong)((long)DisposeTrackerOptions.ThrottleMilliseconds);
		}

		// Token: 0x0600008E RID: 142 RVA: 0x00003473 File Offset: 0x00001673
		protected static void RecordStackTraceTimestamp()
		{
			DisposeTracker.lastStacktraceTicks = (uint)Environment.TickCount;
		}

		// Token: 0x0600008F RID: 143 RVA: 0x0000347F File Offset: 0x0000167F
		protected static bool CheckAndUpdateIfCanWatsonThisSecond()
		{
			if (Environment.TickCount - (int)DisposeTracker.lastWatsonTicks >= 1000)
			{
				DisposeTracker.lastWatsonTicks = (uint)Environment.TickCount;
				DisposeTracker.watsonCount = 1L;
				return true;
			}
			return Interlocked.Increment(ref DisposeTracker.watsonCount) < (long)DisposeTrackerOptions.MaximumWatsonsPerSecond;
		}

		// Token: 0x06000090 RID: 144
		protected abstract void Dispose(bool disposing);

		// Token: 0x04000083 RID: 131
		internal const string IsTypeRiskyFieldName = "isTypeRisky";

		// Token: 0x04000084 RID: 132
		private static uint lastStacktraceTicks;

		// Token: 0x04000085 RID: 133
		private static uint lastWatsonTicks;

		// Token: 0x04000086 RID: 134
		private static long watsonCount;

		// Token: 0x04000087 RID: 135
		private static bool suppressed;

		// Token: 0x04000088 RID: 136
		private StackTrace stackTrace;

		// Token: 0x04000089 RID: 137
		private bool stackTraceWasReset;

		// Token: 0x0400008A RID: 138
		private bool wasProperlyDisposed;
	}
}
