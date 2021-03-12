using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Microsoft.Exchange.MapiHttp
{
	// Token: 0x0200003A RID: 58
	internal class DelayTimer
	{
		// Token: 0x06000211 RID: 529 RVA: 0x0000C5A8 File Offset: 0x0000A7A8
		private DelayTimer()
		{
			int num = (int)this.maxDelayPeriod.TotalSeconds / this.bucketTimeInSeconds + 1;
			this.buckets = new TaskCompletionSource<bool>[num];
			for (int i = 0; i < num; i++)
			{
				this.buckets[i] = new TaskCompletionSource<bool>();
			}
			Task.Factory.StartNew<Task>(async delegate()
			{
				await this.BackgroundTimerAsync();
			});
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x06000212 RID: 530 RVA: 0x0000C645 File Offset: 0x0000A845
		public static DelayTimer Instance
		{
			get
			{
				return DelayTimer.instance.Value;
			}
		}

		// Token: 0x06000213 RID: 531 RVA: 0x0000C654 File Offset: 0x0000A854
		public Task<bool> GetTimerTask(TimeSpan delayTime)
		{
			if (delayTime > this.maxDelayPeriod)
			{
				throw new ArgumentOutOfRangeException("delayTime", "Requested delay is more than the maximum delay time allowed");
			}
			if (delayTime < TimeSpan.Zero)
			{
				throw new ArgumentOutOfRangeException("delayTime", "Requested delay time cannot be negative");
			}
			int num = ((int)delayTime.TotalSeconds + (this.bucketTimeInSeconds - 1)) / this.bucketTimeInSeconds;
			int num2 = (this.currentBucketIndex + num) % this.buckets.Length;
			return this.buckets[num2].Task;
		}

		// Token: 0x06000214 RID: 532 RVA: 0x0000C854 File Offset: 0x0000AA54
		[DebuggerStepThrough]
		private Task BackgroundTimerAsync()
		{
			DelayTimer.<BackgroundTimerAsync>d__5 <BackgroundTimerAsync>d__;
			<BackgroundTimerAsync>d__.<>4__this = this;
			<BackgroundTimerAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<BackgroundTimerAsync>d__.<>1__state = -1;
			AsyncTaskMethodBuilder <>t__builder = <BackgroundTimerAsync>d__.<>t__builder;
			<>t__builder.Start<DelayTimer.<BackgroundTimerAsync>d__5>(ref <BackgroundTimerAsync>d__);
			return <BackgroundTimerAsync>d__.<>t__builder.Task;
		}

		// Token: 0x040000EE RID: 238
		private static Lazy<DelayTimer> instance = new Lazy<DelayTimer>(() => new DelayTimer());

		// Token: 0x040000EF RID: 239
		private readonly int bucketTimeInSeconds = (int)TimeSpan.FromSeconds(2.0).TotalSeconds;

		// Token: 0x040000F0 RID: 240
		private readonly TimeSpan maxDelayPeriod = TimeSpan.FromMinutes(1.0);

		// Token: 0x040000F1 RID: 241
		private readonly TaskCompletionSource<bool>[] buckets;

		// Token: 0x040000F2 RID: 242
		private int currentBucketIndex;
	}
}
