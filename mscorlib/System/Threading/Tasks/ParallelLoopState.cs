using System;
using System.Diagnostics;
using System.Security.Permissions;

namespace System.Threading.Tasks
{
	// Token: 0x02000528 RID: 1320
	[DebuggerDisplay("ShouldExitCurrentIteration = {ShouldExitCurrentIteration}")]
	[__DynamicallyInvokable]
	[HostProtection(SecurityAction.LinkDemand, Synchronization = true, ExternalThreading = true)]
	public class ParallelLoopState
	{
		// Token: 0x06003F25 RID: 16165 RVA: 0x000EB6D0 File Offset: 0x000E98D0
		internal ParallelLoopState(ParallelLoopStateFlags fbase)
		{
			this.m_flagsBase = fbase;
		}

		// Token: 0x1700096A RID: 2410
		// (get) Token: 0x06003F26 RID: 16166 RVA: 0x000EB6DF File Offset: 0x000E98DF
		internal virtual bool InternalShouldExitCurrentIteration
		{
			get
			{
				throw new NotSupportedException(Environment.GetResourceString("ParallelState_NotSupportedException_UnsupportedMethod"));
			}
		}

		// Token: 0x1700096B RID: 2411
		// (get) Token: 0x06003F27 RID: 16167 RVA: 0x000EB6F0 File Offset: 0x000E98F0
		[__DynamicallyInvokable]
		public bool ShouldExitCurrentIteration
		{
			[__DynamicallyInvokable]
			get
			{
				return this.InternalShouldExitCurrentIteration;
			}
		}

		// Token: 0x1700096C RID: 2412
		// (get) Token: 0x06003F28 RID: 16168 RVA: 0x000EB6F8 File Offset: 0x000E98F8
		[__DynamicallyInvokable]
		public bool IsStopped
		{
			[__DynamicallyInvokable]
			get
			{
				return (this.m_flagsBase.LoopStateFlags & ParallelLoopStateFlags.PLS_STOPPED) != 0;
			}
		}

		// Token: 0x1700096D RID: 2413
		// (get) Token: 0x06003F29 RID: 16169 RVA: 0x000EB70E File Offset: 0x000E990E
		[__DynamicallyInvokable]
		public bool IsExceptional
		{
			[__DynamicallyInvokable]
			get
			{
				return (this.m_flagsBase.LoopStateFlags & ParallelLoopStateFlags.PLS_EXCEPTIONAL) != 0;
			}
		}

		// Token: 0x1700096E RID: 2414
		// (get) Token: 0x06003F2A RID: 16170 RVA: 0x000EB724 File Offset: 0x000E9924
		internal virtual long? InternalLowestBreakIteration
		{
			get
			{
				throw new NotSupportedException(Environment.GetResourceString("ParallelState_NotSupportedException_UnsupportedMethod"));
			}
		}

		// Token: 0x1700096F RID: 2415
		// (get) Token: 0x06003F2B RID: 16171 RVA: 0x000EB735 File Offset: 0x000E9935
		[__DynamicallyInvokable]
		public long? LowestBreakIteration
		{
			[__DynamicallyInvokable]
			get
			{
				return this.InternalLowestBreakIteration;
			}
		}

		// Token: 0x06003F2C RID: 16172 RVA: 0x000EB73D File Offset: 0x000E993D
		[__DynamicallyInvokable]
		public void Stop()
		{
			this.m_flagsBase.Stop();
		}

		// Token: 0x06003F2D RID: 16173 RVA: 0x000EB74A File Offset: 0x000E994A
		internal virtual void InternalBreak()
		{
			throw new NotSupportedException(Environment.GetResourceString("ParallelState_NotSupportedException_UnsupportedMethod"));
		}

		// Token: 0x06003F2E RID: 16174 RVA: 0x000EB75B File Offset: 0x000E995B
		[__DynamicallyInvokable]
		public void Break()
		{
			this.InternalBreak();
		}

		// Token: 0x06003F2F RID: 16175 RVA: 0x000EB764 File Offset: 0x000E9964
		internal static void Break(int iteration, ParallelLoopStateFlags32 pflags)
		{
			int pls_NONE = ParallelLoopStateFlags.PLS_NONE;
			if (pflags.AtomicLoopStateUpdate(ParallelLoopStateFlags.PLS_BROKEN, ParallelLoopStateFlags.PLS_STOPPED | ParallelLoopStateFlags.PLS_EXCEPTIONAL | ParallelLoopStateFlags.PLS_CANCELED, ref pls_NONE))
			{
				int lowestBreakIteration = pflags.m_lowestBreakIteration;
				if (iteration < lowestBreakIteration)
				{
					SpinWait spinWait = default(SpinWait);
					while (Interlocked.CompareExchange(ref pflags.m_lowestBreakIteration, iteration, lowestBreakIteration) != lowestBreakIteration)
					{
						spinWait.SpinOnce();
						lowestBreakIteration = pflags.m_lowestBreakIteration;
						if (iteration > lowestBreakIteration)
						{
							break;
						}
					}
				}
				return;
			}
			if ((pls_NONE & ParallelLoopStateFlags.PLS_STOPPED) != 0)
			{
				throw new InvalidOperationException(Environment.GetResourceString("ParallelState_Break_InvalidOperationException_BreakAfterStop"));
			}
		}

		// Token: 0x06003F30 RID: 16176 RVA: 0x000EB7EC File Offset: 0x000E99EC
		internal static void Break(long iteration, ParallelLoopStateFlags64 pflags)
		{
			int pls_NONE = ParallelLoopStateFlags.PLS_NONE;
			if (pflags.AtomicLoopStateUpdate(ParallelLoopStateFlags.PLS_BROKEN, ParallelLoopStateFlags.PLS_STOPPED | ParallelLoopStateFlags.PLS_EXCEPTIONAL | ParallelLoopStateFlags.PLS_CANCELED, ref pls_NONE))
			{
				long lowestBreakIteration = pflags.LowestBreakIteration;
				if (iteration < lowestBreakIteration)
				{
					SpinWait spinWait = default(SpinWait);
					while (Interlocked.CompareExchange(ref pflags.m_lowestBreakIteration, iteration, lowestBreakIteration) != lowestBreakIteration)
					{
						spinWait.SpinOnce();
						lowestBreakIteration = pflags.LowestBreakIteration;
						if (iteration > lowestBreakIteration)
						{
							break;
						}
					}
				}
				return;
			}
			if ((pls_NONE & ParallelLoopStateFlags.PLS_STOPPED) != 0)
			{
				throw new InvalidOperationException(Environment.GetResourceString("ParallelState_Break_InvalidOperationException_BreakAfterStop"));
			}
		}

		// Token: 0x04001A23 RID: 6691
		private ParallelLoopStateFlags m_flagsBase;
	}
}
