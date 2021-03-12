using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.WorkloadManagement
{
	// Token: 0x02000030 RID: 48
	internal abstract class LogEventIf
	{
		// Token: 0x060001A3 RID: 419 RVA: 0x00007AB0 File Offset: 0x00005CB0
		public LogEventIf(TimeSpan windowBucketLength, ushort numberOfBuckets, ushort minimumBucketCountToEvent)
		{
			if (windowBucketLength <= TimeSpan.Zero)
			{
				throw new ArgumentOutOfRangeException("windowBucketLength", windowBucketLength, "WindowBucketLength must be greater than zero.");
			}
			if (numberOfBuckets == 0)
			{
				throw new ArgumentOutOfRangeException("numberOfBuckets", numberOfBuckets, "NumberOfBuckets must be greater than zero.");
			}
			if (minimumBucketCountToEvent > numberOfBuckets || minimumBucketCountToEvent == 0)
			{
				throw new ArgumentOutOfRangeException("minimumBucketCountToEvent", minimumBucketCountToEvent, "MinimumBucketCountToEvent must be > 0 and <= numberOfBuckets.");
			}
			this.waitingBuckets = new Queue<LogEventIf.WindowBucket>((int)numberOfBuckets);
			this.windowBucketLength = windowBucketLength;
			this.lastUpdate = TimeProvider.UtcNow;
			this.MinimumBucketCountToEvent = (int)minimumBucketCountToEvent;
			this.totalWindowLength = TimeSpan.FromSeconds(windowBucketLength.TotalSeconds * (double)numberOfBuckets);
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x060001A4 RID: 420 RVA: 0x00007B6C File Offset: 0x00005D6C
		// (set) Token: 0x060001A5 RID: 421 RVA: 0x00007B74 File Offset: 0x00005D74
		public Action<bool, DateTime> OnBucketExpire { get; set; }

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x060001A6 RID: 422 RVA: 0x00007B7D File Offset: 0x00005D7D
		// (set) Token: 0x060001A7 RID: 423 RVA: 0x00007B85 File Offset: 0x00005D85
		public Func<LogEventIf, bool> OnLogEvent { get; set; }

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x060001A8 RID: 424 RVA: 0x00007B8E File Offset: 0x00005D8E
		// (set) Token: 0x060001A9 RID: 425 RVA: 0x00007B96 File Offset: 0x00005D96
		public int MinimumBucketCountToEvent { get; private set; }

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x060001AA RID: 426 RVA: 0x00007B9F File Offset: 0x00005D9F
		// (set) Token: 0x060001AB RID: 427 RVA: 0x00007BA7 File Offset: 0x00005DA7
		public int Sum { get; private set; }

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x060001AC RID: 428 RVA: 0x00007BB0 File Offset: 0x00005DB0
		public int BucketCount
		{
			get
			{
				this.Update();
				return this.BucketCountNonUpdating;
			}
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x060001AD RID: 429 RVA: 0x00007BBE File Offset: 0x00005DBE
		private int BucketCountNonUpdating
		{
			get
			{
				return this.waitingBuckets.Count + ((this.currentBucket == null) ? 0 : 1);
			}
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x060001AE RID: 430 RVA: 0x00007BD8 File Offset: 0x00005DD8
		private bool IsEmpty
		{
			get
			{
				return this.BucketCount == 0;
			}
		}

		// Token: 0x060001AF RID: 431 RVA: 0x00007BE4 File Offset: 0x00005DE4
		public void Clear()
		{
			lock (this.instanceLock)
			{
				this.waitingBuckets.Clear();
				this.currentBucket = null;
				this.Sum = 0;
				this.lastUpdate = TimeProvider.UtcNow;
			}
		}

		// Token: 0x060001B0 RID: 432 RVA: 0x00007C44 File Offset: 0x00005E44
		public void Set(bool value)
		{
			lock (this.instanceLock)
			{
				DateTime utcNow = TimeProvider.UtcNow;
				this.VerifyTime(ref utcNow);
				this.InternalUpdate(utcNow);
				bool flag2;
				LogEventIf.WindowBucket orCreateCurrentBucket = this.GetOrCreateCurrentBucket(utcNow, out flag2);
				if (flag2 || (value && !orCreateCurrentBucket.Value))
				{
					orCreateCurrentBucket.Value = value;
				}
			}
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x00007CB8 File Offset: 0x00005EB8
		public void Update()
		{
			lock (this.instanceLock)
			{
				DateTime utcNow = TimeProvider.UtcNow;
				this.VerifyTime(ref utcNow);
				this.InternalUpdate(utcNow);
			}
		}

		// Token: 0x060001B2 RID: 434
		protected abstract void InternalLogEvent();

		// Token: 0x060001B3 RID: 435 RVA: 0x00007D08 File Offset: 0x00005F08
		private void InternalUpdate(DateTime utcNow)
		{
			if (this.currentBucket != null && utcNow >= this.currentBucket.CreationTimeUtc + this.windowBucketLength)
			{
				int count = this.waitingBuckets.Count;
				this.waitingBuckets.Enqueue(this.currentBucket);
				this.Sum += (this.currentBucket.Value ? 1 : 0);
				this.currentBucket = null;
			}
			while (this.waitingBuckets.Count > 0)
			{
				LogEventIf.WindowBucket windowBucket = this.waitingBuckets.Peek();
				if (!(utcNow >= windowBucket.ExpireTimeUtc))
				{
					break;
				}
				if (TimeProvider.UtcNow - this.lastEventTime >= this.totalWindowLength && this.BucketCountNonUpdating >= this.MinimumBucketCountToEvent && this.Sum == 0)
				{
					this.LogEvent();
				}
				int count2 = this.waitingBuckets.Count;
				this.waitingBuckets.Dequeue();
				int num = count2 - 1;
				if (num > 0)
				{
					this.Sum -= (windowBucket.Value ? 1 : 0);
				}
				else
				{
					this.Sum = 0;
				}
				if (this.OnBucketExpire != null)
				{
					this.OnBucketExpire(windowBucket.Value, windowBucket.ExpireTimeUtc);
				}
			}
		}

		// Token: 0x060001B4 RID: 436 RVA: 0x00007E50 File Offset: 0x00006050
		private void LogEvent()
		{
			bool flag = true;
			if (this.OnLogEvent != null)
			{
				flag = this.OnLogEvent(this);
			}
			this.lastEventTime = TimeProvider.UtcNow;
			if (flag)
			{
				this.InternalLogEvent();
			}
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x00007E88 File Offset: 0x00006088
		private LogEventIf.WindowBucket GetOrCreateCurrentBucket(DateTime currentUtc, out bool isNew)
		{
			LogEventIf.WindowBucket result;
			lock (this.instanceLock)
			{
				this.VerifyTime(ref currentUtc);
				isNew = (this.currentBucket == null);
				if (isNew)
				{
					this.currentBucket = new LogEventIf.WindowBucket(currentUtc, currentUtc.Add(this.totalWindowLength));
				}
				result = this.currentBucket;
			}
			return result;
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x00007EFC File Offset: 0x000060FC
		private bool VerifyTime(ref DateTime currentUtc)
		{
			if (currentUtc < this.lastUpdate)
			{
				currentUtc = this.lastUpdate;
				return false;
			}
			this.lastUpdate = currentUtc;
			return true;
		}

		// Token: 0x040000DC RID: 220
		private readonly TimeSpan windowBucketLength;

		// Token: 0x040000DD RID: 221
		private readonly TimeSpan totalWindowLength;

		// Token: 0x040000DE RID: 222
		private Queue<LogEventIf.WindowBucket> waitingBuckets;

		// Token: 0x040000DF RID: 223
		private LogEventIf.WindowBucket currentBucket;

		// Token: 0x040000E0 RID: 224
		private DateTime lastUpdate;

		// Token: 0x040000E1 RID: 225
		private object instanceLock = new object();

		// Token: 0x040000E2 RID: 226
		private DateTime lastEventTime = DateTime.MinValue;

		// Token: 0x02000031 RID: 49
		private class WindowBucket
		{
			// Token: 0x060001B7 RID: 439 RVA: 0x00007F2C File Offset: 0x0000612C
			public WindowBucket(DateTime currentTime, DateTime expireTime)
			{
				this.CreationTimeUtc = currentTime;
				this.ExpireTimeUtc = expireTime;
			}

			// Token: 0x17000094 RID: 148
			// (get) Token: 0x060001B8 RID: 440 RVA: 0x00007F42 File Offset: 0x00006142
			// (set) Token: 0x060001B9 RID: 441 RVA: 0x00007F4A File Offset: 0x0000614A
			internal DateTime ExpireTimeUtc { get; private set; }

			// Token: 0x17000095 RID: 149
			// (get) Token: 0x060001BA RID: 442 RVA: 0x00007F53 File Offset: 0x00006153
			// (set) Token: 0x060001BB RID: 443 RVA: 0x00007F5B File Offset: 0x0000615B
			internal DateTime CreationTimeUtc { get; private set; }

			// Token: 0x17000096 RID: 150
			// (get) Token: 0x060001BC RID: 444 RVA: 0x00007F64 File Offset: 0x00006164
			// (set) Token: 0x060001BD RID: 445 RVA: 0x00007F6C File Offset: 0x0000616C
			internal bool Value { get; set; }
		}
	}
}
