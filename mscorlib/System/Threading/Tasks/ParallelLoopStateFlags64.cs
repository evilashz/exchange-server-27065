using System;

namespace System.Threading.Tasks
{
	// Token: 0x0200052D RID: 1325
	internal class ParallelLoopStateFlags64 : ParallelLoopStateFlags
	{
		// Token: 0x17000979 RID: 2425
		// (get) Token: 0x06003F4A RID: 16202 RVA: 0x000EBAE3 File Offset: 0x000E9CE3
		internal long LowestBreakIteration
		{
			get
			{
				if (IntPtr.Size >= 8)
				{
					return this.m_lowestBreakIteration;
				}
				return Interlocked.Read(ref this.m_lowestBreakIteration);
			}
		}

		// Token: 0x1700097A RID: 2426
		// (get) Token: 0x06003F4B RID: 16203 RVA: 0x000EBB00 File Offset: 0x000E9D00
		internal long? NullableLowestBreakIteration
		{
			get
			{
				if (this.m_lowestBreakIteration == 9223372036854775807L)
				{
					return null;
				}
				if (IntPtr.Size >= 8)
				{
					return new long?(this.m_lowestBreakIteration);
				}
				return new long?(Interlocked.Read(ref this.m_lowestBreakIteration));
			}
		}

		// Token: 0x06003F4C RID: 16204 RVA: 0x000EBB4C File Offset: 0x000E9D4C
		internal bool ShouldExitLoop(long CallerIteration)
		{
			int loopStateFlags = base.LoopStateFlags;
			return loopStateFlags != ParallelLoopStateFlags.PLS_NONE && ((loopStateFlags & (ParallelLoopStateFlags.PLS_EXCEPTIONAL | ParallelLoopStateFlags.PLS_STOPPED | ParallelLoopStateFlags.PLS_CANCELED)) != 0 || ((loopStateFlags & ParallelLoopStateFlags.PLS_BROKEN) != 0 && CallerIteration > this.LowestBreakIteration));
		}

		// Token: 0x06003F4D RID: 16205 RVA: 0x000EBB98 File Offset: 0x000E9D98
		internal bool ShouldExitLoop()
		{
			int loopStateFlags = base.LoopStateFlags;
			return loopStateFlags != ParallelLoopStateFlags.PLS_NONE && (loopStateFlags & (ParallelLoopStateFlags.PLS_EXCEPTIONAL | ParallelLoopStateFlags.PLS_CANCELED)) != 0;
		}

		// Token: 0x04001A2F RID: 6703
		internal long m_lowestBreakIteration = long.MaxValue;
	}
}
