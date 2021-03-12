using System;

namespace System.Threading.Tasks
{
	// Token: 0x0200052B RID: 1323
	internal class ParallelLoopStateFlags
	{
		// Token: 0x17000976 RID: 2422
		// (get) Token: 0x06003F3D RID: 16189 RVA: 0x000EB918 File Offset: 0x000E9B18
		internal int LoopStateFlags
		{
			get
			{
				return this.m_LoopStateFlags;
			}
		}

		// Token: 0x06003F3E RID: 16190 RVA: 0x000EB924 File Offset: 0x000E9B24
		internal bool AtomicLoopStateUpdate(int newState, int illegalStates)
		{
			int num = 0;
			return this.AtomicLoopStateUpdate(newState, illegalStates, ref num);
		}

		// Token: 0x06003F3F RID: 16191 RVA: 0x000EB940 File Offset: 0x000E9B40
		internal bool AtomicLoopStateUpdate(int newState, int illegalStates, ref int oldState)
		{
			SpinWait spinWait = default(SpinWait);
			for (;;)
			{
				oldState = this.m_LoopStateFlags;
				if ((oldState & illegalStates) != 0)
				{
					break;
				}
				if (Interlocked.CompareExchange(ref this.m_LoopStateFlags, oldState | newState, oldState) == oldState)
				{
					return true;
				}
				spinWait.SpinOnce();
			}
			return false;
		}

		// Token: 0x06003F40 RID: 16192 RVA: 0x000EB986 File Offset: 0x000E9B86
		internal void SetExceptional()
		{
			this.AtomicLoopStateUpdate(ParallelLoopStateFlags.PLS_EXCEPTIONAL, ParallelLoopStateFlags.PLS_NONE);
		}

		// Token: 0x06003F41 RID: 16193 RVA: 0x000EB999 File Offset: 0x000E9B99
		internal void Stop()
		{
			if (!this.AtomicLoopStateUpdate(ParallelLoopStateFlags.PLS_STOPPED, ParallelLoopStateFlags.PLS_BROKEN))
			{
				throw new InvalidOperationException(Environment.GetResourceString("ParallelState_Stop_InvalidOperationException_StopAfterBreak"));
			}
		}

		// Token: 0x06003F42 RID: 16194 RVA: 0x000EB9BD File Offset: 0x000E9BBD
		internal bool Cancel()
		{
			return this.AtomicLoopStateUpdate(ParallelLoopStateFlags.PLS_CANCELED, ParallelLoopStateFlags.PLS_NONE);
		}

		// Token: 0x04001A28 RID: 6696
		internal static int PLS_NONE;

		// Token: 0x04001A29 RID: 6697
		internal static int PLS_EXCEPTIONAL = 1;

		// Token: 0x04001A2A RID: 6698
		internal static int PLS_BROKEN = 2;

		// Token: 0x04001A2B RID: 6699
		internal static int PLS_STOPPED = 4;

		// Token: 0x04001A2C RID: 6700
		internal static int PLS_CANCELED = 8;

		// Token: 0x04001A2D RID: 6701
		private volatile int m_LoopStateFlags = ParallelLoopStateFlags.PLS_NONE;
	}
}
