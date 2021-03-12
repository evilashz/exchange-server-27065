using System;

namespace System.Threading.Tasks
{
	// Token: 0x0200052A RID: 1322
	internal class ParallelLoopState64 : ParallelLoopState
	{
		// Token: 0x06003F37 RID: 16183 RVA: 0x000EB8C4 File Offset: 0x000E9AC4
		internal ParallelLoopState64(ParallelLoopStateFlags64 sharedParallelStateFlags) : base(sharedParallelStateFlags)
		{
			this.m_sharedParallelStateFlags = sharedParallelStateFlags;
		}

		// Token: 0x17000973 RID: 2419
		// (get) Token: 0x06003F38 RID: 16184 RVA: 0x000EB8D4 File Offset: 0x000E9AD4
		// (set) Token: 0x06003F39 RID: 16185 RVA: 0x000EB8DC File Offset: 0x000E9ADC
		internal long CurrentIteration
		{
			get
			{
				return this.m_currentIteration;
			}
			set
			{
				this.m_currentIteration = value;
			}
		}

		// Token: 0x17000974 RID: 2420
		// (get) Token: 0x06003F3A RID: 16186 RVA: 0x000EB8E5 File Offset: 0x000E9AE5
		internal override bool InternalShouldExitCurrentIteration
		{
			get
			{
				return this.m_sharedParallelStateFlags.ShouldExitLoop(this.CurrentIteration);
			}
		}

		// Token: 0x17000975 RID: 2421
		// (get) Token: 0x06003F3B RID: 16187 RVA: 0x000EB8F8 File Offset: 0x000E9AF8
		internal override long? InternalLowestBreakIteration
		{
			get
			{
				return this.m_sharedParallelStateFlags.NullableLowestBreakIteration;
			}
		}

		// Token: 0x06003F3C RID: 16188 RVA: 0x000EB905 File Offset: 0x000E9B05
		internal override void InternalBreak()
		{
			ParallelLoopState.Break(this.CurrentIteration, this.m_sharedParallelStateFlags);
		}

		// Token: 0x04001A26 RID: 6694
		private ParallelLoopStateFlags64 m_sharedParallelStateFlags;

		// Token: 0x04001A27 RID: 6695
		private long m_currentIteration;
	}
}
