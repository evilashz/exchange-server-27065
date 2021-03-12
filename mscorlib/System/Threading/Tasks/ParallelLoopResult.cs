using System;

namespace System.Threading.Tasks
{
	// Token: 0x0200052E RID: 1326
	[__DynamicallyInvokable]
	public struct ParallelLoopResult
	{
		// Token: 0x1700097B RID: 2427
		// (get) Token: 0x06003F4F RID: 16207 RVA: 0x000EBBDD File Offset: 0x000E9DDD
		[__DynamicallyInvokable]
		public bool IsCompleted
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_completed;
			}
		}

		// Token: 0x1700097C RID: 2428
		// (get) Token: 0x06003F50 RID: 16208 RVA: 0x000EBBE5 File Offset: 0x000E9DE5
		[__DynamicallyInvokable]
		public long? LowestBreakIteration
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_lowestBreakIteration;
			}
		}

		// Token: 0x04001A30 RID: 6704
		internal bool m_completed;

		// Token: 0x04001A31 RID: 6705
		internal long? m_lowestBreakIteration;
	}
}
