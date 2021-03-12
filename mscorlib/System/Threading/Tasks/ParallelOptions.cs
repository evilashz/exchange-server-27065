using System;

namespace System.Threading.Tasks
{
	// Token: 0x02000523 RID: 1315
	[__DynamicallyInvokable]
	public class ParallelOptions
	{
		// Token: 0x06003EED RID: 16109 RVA: 0x000E983D File Offset: 0x000E7A3D
		[__DynamicallyInvokable]
		public ParallelOptions()
		{
			this.m_scheduler = TaskScheduler.Default;
			this.m_maxDegreeOfParallelism = -1;
			this.m_cancellationToken = CancellationToken.None;
		}

		// Token: 0x17000965 RID: 2405
		// (get) Token: 0x06003EEE RID: 16110 RVA: 0x000E9862 File Offset: 0x000E7A62
		// (set) Token: 0x06003EEF RID: 16111 RVA: 0x000E986A File Offset: 0x000E7A6A
		[__DynamicallyInvokable]
		public TaskScheduler TaskScheduler
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_scheduler;
			}
			[__DynamicallyInvokable]
			set
			{
				this.m_scheduler = value;
			}
		}

		// Token: 0x17000966 RID: 2406
		// (get) Token: 0x06003EF0 RID: 16112 RVA: 0x000E9873 File Offset: 0x000E7A73
		internal TaskScheduler EffectiveTaskScheduler
		{
			get
			{
				if (this.m_scheduler == null)
				{
					return TaskScheduler.Current;
				}
				return this.m_scheduler;
			}
		}

		// Token: 0x17000967 RID: 2407
		// (get) Token: 0x06003EF1 RID: 16113 RVA: 0x000E9889 File Offset: 0x000E7A89
		// (set) Token: 0x06003EF2 RID: 16114 RVA: 0x000E9891 File Offset: 0x000E7A91
		[__DynamicallyInvokable]
		public int MaxDegreeOfParallelism
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_maxDegreeOfParallelism;
			}
			[__DynamicallyInvokable]
			set
			{
				if (value == 0 || value < -1)
				{
					throw new ArgumentOutOfRangeException("MaxDegreeOfParallelism");
				}
				this.m_maxDegreeOfParallelism = value;
			}
		}

		// Token: 0x17000968 RID: 2408
		// (get) Token: 0x06003EF3 RID: 16115 RVA: 0x000E98AC File Offset: 0x000E7AAC
		// (set) Token: 0x06003EF4 RID: 16116 RVA: 0x000E98B4 File Offset: 0x000E7AB4
		[__DynamicallyInvokable]
		public CancellationToken CancellationToken
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_cancellationToken;
			}
			[__DynamicallyInvokable]
			set
			{
				this.m_cancellationToken = value;
			}
		}

		// Token: 0x17000969 RID: 2409
		// (get) Token: 0x06003EF5 RID: 16117 RVA: 0x000E98C0 File Offset: 0x000E7AC0
		internal int EffectiveMaxConcurrencyLevel
		{
			get
			{
				int num = this.MaxDegreeOfParallelism;
				int maximumConcurrencyLevel = this.EffectiveTaskScheduler.MaximumConcurrencyLevel;
				if (maximumConcurrencyLevel > 0 && maximumConcurrencyLevel != 2147483647)
				{
					num = ((num == -1) ? maximumConcurrencyLevel : Math.Min(maximumConcurrencyLevel, num));
				}
				return num;
			}
		}

		// Token: 0x04001A0F RID: 6671
		private TaskScheduler m_scheduler;

		// Token: 0x04001A10 RID: 6672
		private int m_maxDegreeOfParallelism;

		// Token: 0x04001A11 RID: 6673
		private CancellationToken m_cancellationToken;
	}
}
