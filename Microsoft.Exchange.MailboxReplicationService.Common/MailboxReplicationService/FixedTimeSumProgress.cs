using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Collections;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020001F4 RID: 500
	internal class FixedTimeSumProgress : FixedTimeSumBase
	{
		// Token: 0x06001572 RID: 5490 RVA: 0x0002FF08 File Offset: 0x0002E108
		public FixedTimeSumProgress(uint windowBucketLength, ushort numberOfBuckets) : base(windowBucketLength, numberOfBuckets, null)
		{
		}

		// Token: 0x06001573 RID: 5491 RVA: 0x0002FF28 File Offset: 0x0002E128
		public FixedTimeSumProgress(uint windowBucketLength, ushort numberOfBuckets, FixedTimeSumSlot[] input) : base(windowBucketLength, numberOfBuckets, null)
		{
			this.Deserialize(input);
		}

		// Token: 0x06001574 RID: 5492 RVA: 0x0002FF4D File Offset: 0x0002E14D
		internal void Add(uint value)
		{
			base.TryAdd(value);
		}

		// Token: 0x06001575 RID: 5493 RVA: 0x0002FF60 File Offset: 0x0002E160
		internal FixedTimeSumSlot[] Serialize()
		{
			List<FixedTimeSumBase.WindowBucket> list = new List<FixedTimeSumBase.WindowBucket>();
			if (this.currentBucket != null)
			{
				list.Add(this.currentBucket);
			}
			if (this.waitingBuckets != null && this.waitingBuckets.Count > 0)
			{
				list.AddRange((from bucket in this.waitingBuckets
				orderby bucket.ExpireAt descending
				select bucket).ToList<FixedTimeSumBase.WindowBucket>());
			}
			FixedTimeSumSlot[] array = new FixedTimeSumSlot[list.Count];
			for (int i = 0; i < list.Count; i++)
			{
				array[i] = new FixedTimeSumSlot
				{
					StartTimeInTicks = list[i].ExpireAt.Subtract(TimeSpan.FromMilliseconds(this.windowBucketLength)).Ticks,
					Value = list[i].Value
				};
			}
			return array;
		}

		// Token: 0x06001576 RID: 5494 RVA: 0x0003003C File Offset: 0x0002E23C
		private void Deserialize(FixedTimeSumSlot[] input)
		{
			if (input == null || input.Length == 0)
			{
				return;
			}
			DateTime utcNow = new DateTime(input[0].StartTimeInTicks, DateTimeKind.Utc);
			this.currentBucket = new FixedTimeSumBase.WindowBucket(utcNow, this.windowBucketLength);
			this.currentBucket.Add(utcNow, input[0].Value);
			this.value += input[0].Value;
			for (int i = 1; i < input.Length; i++)
			{
				FixedTimeSumBase.WindowBucket windowBucket = new FixedTimeSumBase.WindowBucket(new DateTime(input[i].StartTimeInTicks), this.windowBucketLength);
				windowBucket.Add(new DateTime(input[i].StartTimeInTicks, DateTimeKind.Utc), input[i].Value);
				this.waitingBuckets.Enqueue(windowBucket);
				this.value += input[i].Value;
			}
		}
	}
}
