using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ManagedStore.StoreCommonServices;
using Microsoft.Exchange.Server.Storage.Common;

namespace Microsoft.Exchange.Server.Storage.StoreCommonServices
{
	// Token: 0x02000080 RID: 128
	public class ChunkedEnumerable : IChunked
	{
		// Token: 0x060004A2 RID: 1186 RVA: 0x0001D1C9 File Offset: 0x0001B3C9
		public ChunkedEnumerable(string workDescription, IEnumerable<bool> steps, ILockName mailboxLockName, TimeSpan minTimeBetweenInterrupts, TimeSpan maxTimeBetweenInterrupts)
		{
			this.workDescription = workDescription;
			this.steps = steps;
			this.mailboxLockName = mailboxLockName;
			this.minMillisecondsBetweenInterrupts = (int)minTimeBetweenInterrupts.TotalMilliseconds;
			this.maxMillisecondsBetweenInterrupts = (int)maxTimeBetweenInterrupts.TotalMilliseconds;
		}

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x060004A3 RID: 1187 RVA: 0x0001D202 File Offset: 0x0001B402
		public bool MustYield
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060004A4 RID: 1188 RVA: 0x0001D205 File Offset: 0x0001B405
		internal static IDisposable SetInterruptAfterEachStepTestHook(bool value)
		{
			return ChunkedEnumerable.interruptAfterEachStepTestHook.SetTestHook(value);
		}

		// Token: 0x060004A5 RID: 1189 RVA: 0x0001D214 File Offset: 0x0001B414
		public bool DoChunk(Context context)
		{
			if (this.steps != null)
			{
				if (this.stepEnumerator == null)
				{
					if (ExTraceGlobals.ChunkingTracer.IsTraceEnabled(TraceType.DebugTrace))
					{
						ExTraceGlobals.ChunkingTracer.TraceDebug<string>(0L, "Starting chunked {0}", this.workDescription);
					}
					this.stepEnumerator = this.steps.GetEnumerator();
				}
				if (ExTraceGlobals.ChunkingTracer.IsTraceEnabled(TraceType.DebugTrace))
				{
					ExTraceGlobals.ChunkingTracer.TraceDebug<string>(0L, "Starting {0} chunk", this.workDescription);
				}
				int tickCount = Environment.TickCount;
				int num = tickCount + this.maxMillisecondsBetweenInterrupts;
				int num2 = tickCount + this.minMillisecondsBetweenInterrupts;
				while (this.stepEnumerator.MoveNext())
				{
					tickCount = Environment.TickCount;
					if (ChunkedEnumerable.interruptAfterEachStepTestHook.Value || tickCount - num >= 0 || (tickCount - num2 >= 0 && LockManager.HasContention(this.mailboxLockName)))
					{
						return false;
					}
				}
				this.Dispose(context);
				if (ExTraceGlobals.ChunkingTracer.IsTraceEnabled(TraceType.DebugTrace))
				{
					ExTraceGlobals.ChunkingTracer.TraceDebug<string>(0L, "Finished chunked {0}", this.workDescription);
				}
			}
			return true;
		}

		// Token: 0x060004A6 RID: 1190 RVA: 0x0001D30D File Offset: 0x0001B50D
		public void Dispose(Context context)
		{
			if (this.steps != null)
			{
				if (this.stepEnumerator != null)
				{
					this.stepEnumerator.Dispose();
					this.stepEnumerator = null;
				}
				this.steps = null;
			}
		}

		// Token: 0x040003BD RID: 957
		private static readonly Hookable<bool> interruptAfterEachStepTestHook = Hookable<bool>.Create(true, false);

		// Token: 0x040003BE RID: 958
		private readonly string workDescription;

		// Token: 0x040003BF RID: 959
		private readonly ILockName mailboxLockName;

		// Token: 0x040003C0 RID: 960
		private readonly int minMillisecondsBetweenInterrupts;

		// Token: 0x040003C1 RID: 961
		private readonly int maxMillisecondsBetweenInterrupts;

		// Token: 0x040003C2 RID: 962
		private IEnumerable<bool> steps;

		// Token: 0x040003C3 RID: 963
		private IEnumerator<bool> stepEnumerator;
	}
}
