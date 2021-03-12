using System;

namespace System.Threading.Tasks
{
	// Token: 0x0200052C RID: 1324
	internal class ParallelLoopStateFlags32 : ParallelLoopStateFlags
	{
		// Token: 0x17000977 RID: 2423
		// (get) Token: 0x06003F45 RID: 16197 RVA: 0x000EB9FE File Offset: 0x000E9BFE
		internal int LowestBreakIteration
		{
			get
			{
				return this.m_lowestBreakIteration;
			}
		}

		// Token: 0x17000978 RID: 2424
		// (get) Token: 0x06003F46 RID: 16198 RVA: 0x000EBA08 File Offset: 0x000E9C08
		internal long? NullableLowestBreakIteration
		{
			get
			{
				if (this.m_lowestBreakIteration == 2147483647)
				{
					return null;
				}
				long value = (long)this.m_lowestBreakIteration;
				if (IntPtr.Size >= 8)
				{
					return new long?(value);
				}
				return new long?(Interlocked.Read(ref value));
			}
		}

		// Token: 0x06003F47 RID: 16199 RVA: 0x000EBA54 File Offset: 0x000E9C54
		internal bool ShouldExitLoop(int CallerIteration)
		{
			int loopStateFlags = base.LoopStateFlags;
			return loopStateFlags != ParallelLoopStateFlags.PLS_NONE && ((loopStateFlags & (ParallelLoopStateFlags.PLS_EXCEPTIONAL | ParallelLoopStateFlags.PLS_STOPPED | ParallelLoopStateFlags.PLS_CANCELED)) != 0 || ((loopStateFlags & ParallelLoopStateFlags.PLS_BROKEN) != 0 && CallerIteration > this.LowestBreakIteration));
		}

		// Token: 0x06003F48 RID: 16200 RVA: 0x000EBAA0 File Offset: 0x000E9CA0
		internal bool ShouldExitLoop()
		{
			int loopStateFlags = base.LoopStateFlags;
			return loopStateFlags != ParallelLoopStateFlags.PLS_NONE && (loopStateFlags & (ParallelLoopStateFlags.PLS_EXCEPTIONAL | ParallelLoopStateFlags.PLS_CANCELED)) != 0;
		}

		// Token: 0x04001A2E RID: 6702
		internal volatile int m_lowestBreakIteration = int.MaxValue;
	}
}
