using System;

namespace System.Threading.Tasks
{
	// Token: 0x02000529 RID: 1321
	internal class ParallelLoopState32 : ParallelLoopState
	{
		// Token: 0x06003F31 RID: 16177 RVA: 0x000EB870 File Offset: 0x000E9A70
		internal ParallelLoopState32(ParallelLoopStateFlags32 sharedParallelStateFlags) : base(sharedParallelStateFlags)
		{
			this.m_sharedParallelStateFlags = sharedParallelStateFlags;
		}

		// Token: 0x17000970 RID: 2416
		// (get) Token: 0x06003F32 RID: 16178 RVA: 0x000EB880 File Offset: 0x000E9A80
		// (set) Token: 0x06003F33 RID: 16179 RVA: 0x000EB888 File Offset: 0x000E9A88
		internal int CurrentIteration
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

		// Token: 0x17000971 RID: 2417
		// (get) Token: 0x06003F34 RID: 16180 RVA: 0x000EB891 File Offset: 0x000E9A91
		internal override bool InternalShouldExitCurrentIteration
		{
			get
			{
				return this.m_sharedParallelStateFlags.ShouldExitLoop(this.CurrentIteration);
			}
		}

		// Token: 0x17000972 RID: 2418
		// (get) Token: 0x06003F35 RID: 16181 RVA: 0x000EB8A4 File Offset: 0x000E9AA4
		internal override long? InternalLowestBreakIteration
		{
			get
			{
				return this.m_sharedParallelStateFlags.NullableLowestBreakIteration;
			}
		}

		// Token: 0x06003F36 RID: 16182 RVA: 0x000EB8B1 File Offset: 0x000E9AB1
		internal override void InternalBreak()
		{
			ParallelLoopState.Break(this.CurrentIteration, this.m_sharedParallelStateFlags);
		}

		// Token: 0x04001A24 RID: 6692
		private ParallelLoopStateFlags32 m_sharedParallelStateFlags;

		// Token: 0x04001A25 RID: 6693
		private int m_currentIteration;
	}
}
