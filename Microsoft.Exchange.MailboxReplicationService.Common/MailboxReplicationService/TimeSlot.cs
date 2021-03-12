using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000222 RID: 546
	[Serializable]
	public abstract class TimeSlot
	{
		// Token: 0x17000B90 RID: 2960
		// (get) Token: 0x06001D93 RID: 7571 RVA: 0x0003CF6F File Offset: 0x0003B16F
		// (set) Token: 0x06001D94 RID: 7572 RVA: 0x0003CF77 File Offset: 0x0003B177
		private protected ulong[] Values { protected get; private set; }

		// Token: 0x17000B91 RID: 2961
		// (get) Token: 0x06001D95 RID: 7573 RVA: 0x0003CF80 File Offset: 0x0003B180
		// (set) Token: 0x06001D96 RID: 7574 RVA: 0x0003CF88 File Offset: 0x0003B188
		private protected uint Head { protected get; private set; }

		// Token: 0x17000B92 RID: 2962
		// (get) Token: 0x06001D97 RID: 7575 RVA: 0x0003CF91 File Offset: 0x0003B191
		// (set) Token: 0x06001D98 RID: 7576 RVA: 0x0003CF99 File Offset: 0x0003B199
		private protected ulong MillisecondsPerSlot { protected get; private set; }

		// Token: 0x06001D99 RID: 7577 RVA: 0x0003CFA2 File Offset: 0x0003B1A2
		public TimeSlot(uint capacity, ulong millisecondsPerSlot)
		{
			this.Values = new ulong[capacity];
			this.MillisecondsPerSlot = millisecondsPerSlot;
		}

		// Token: 0x06001D9A RID: 7578 RVA: 0x0003CFC0 File Offset: 0x0003B1C0
		public TimeSlot(uint capacity, ulong[] inputArray, ulong millisecondsPerSlot)
		{
			if (inputArray != null)
			{
				capacity = Math.Max(capacity, (uint)inputArray.Length);
				this.Values = new ulong[capacity];
				Array.Copy(inputArray, this.Values, inputArray.Length);
			}
			else
			{
				this.Values = new ulong[capacity];
			}
			this.MillisecondsPerSlot = millisecondsPerSlot;
		}

		// Token: 0x17000B93 RID: 2963
		// (get) Token: 0x06001D9B RID: 7579 RVA: 0x0003D013 File Offset: 0x0003B213
		public uint Capacity
		{
			get
			{
				return (uint)this.Values.Length;
			}
		}

		// Token: 0x17000B94 RID: 2964
		public ulong this[uint index]
		{
			get
			{
				return this.Values[(int)((UIntPtr)((index + this.Head) % this.Capacity))];
			}
			private set
			{
				this.Values[(int)((UIntPtr)((index + this.Head) % this.Capacity))] = value;
			}
		}

		// Token: 0x06001D9E RID: 7582 RVA: 0x0003D050 File Offset: 0x0003B250
		public override string ToString()
		{
			return string.Join<ulong>("|", this.ToArray());
		}

		// Token: 0x06001D9F RID: 7583 RVA: 0x0003D064 File Offset: 0x0003B264
		public void AddTime(TimeSpan duration)
		{
			DateTime utcNow = TimeProvider.UtcNow;
			ulong num = this.GetLatestSlotMilliseconds(utcNow);
			TimeSpan.FromMilliseconds(num);
			if (num >= duration.TotalMilliseconds)
			{
				num = (ulong)duration.TotalMilliseconds;
			}
			this.Values[(int)((UIntPtr)this.Head)] = this.Values[(int)((UIntPtr)this.Head)] + num;
			ulong num2 = (ulong)(duration.TotalMilliseconds - num);
			if (num2 > 0UL)
			{
				uint num3 = (uint)(num2 / this.MillisecondsPerSlot);
				if (num3 >= this.Capacity - 1U)
				{
					num3 = this.Capacity - 1U;
				}
				else
				{
					ulong num4 = num2 % this.MillisecondsPerSlot;
					uint num5 = (this.Head + num3 + 1U) % this.Capacity;
					this.Values[(int)((UIntPtr)num5)] += num4;
				}
				for (uint num6 = 1U; num6 < num3 + 1U; num6 += 1U)
				{
					this.Values[(int)((UIntPtr)((this.Head + num6) % this.Capacity))] = this.MillisecondsPerSlot;
				}
			}
		}

		// Token: 0x06001DA0 RID: 7584 RVA: 0x0003D15C File Offset: 0x0003B35C
		public void Refresh(DateTime lastUpdateTime)
		{
			DateTime utcNow = TimeProvider.UtcNow;
			uint elapsedSlotCount = this.GetElapsedSlotCount(lastUpdateTime, utcNow);
			for (uint num = 0U; num < elapsedSlotCount; num += 1U)
			{
				if (this.Head == 0U)
				{
					this.Head = this.Capacity - 1U;
				}
				else
				{
					this.Head = (this.Head - 1U) % this.Capacity;
				}
				this.Values[(int)((UIntPtr)this.Head)] = 0UL;
			}
		}

		// Token: 0x06001DA1 RID: 7585
		protected abstract uint GetElapsedSlotCount(DateTime lastUpdateTime, DateTime currentTime);

		// Token: 0x06001DA2 RID: 7586
		protected abstract ulong GetLatestSlotMilliseconds(DateTime currentTime);

		// Token: 0x06001DA3 RID: 7587
		protected abstract TimeSlotXML GetSlotXML(DateTime time, ulong value);

		// Token: 0x06001DA4 RID: 7588 RVA: 0x0003D1C4 File Offset: 0x0003B3C4
		public void PopulateFrom(TimeSlot ts1, TimeSlot ts2)
		{
			for (uint num = 0U; num < this.Capacity; num += 1U)
			{
				this.Values[(int)((UIntPtr)num)] = ts1[num] + ts2[num];
			}
		}

		// Token: 0x06001DA5 RID: 7589 RVA: 0x0003D1FC File Offset: 0x0003B3FC
		public ulong[] ToArray()
		{
			uint capacity = this.Capacity;
			int num = -1;
			for (int i = (int)(this.Capacity - 1U); i >= 0; i--)
			{
				if (this[(uint)i] != 0UL)
				{
					num = i;
					break;
				}
			}
			List<ulong> list = new List<ulong>();
			uint num2 = 0U;
			while ((ulong)num2 <= (ulong)((long)num))
			{
				list.Add(this[num2]);
				num2 += 1U;
			}
			return list.ToArray();
		}

		// Token: 0x06001DA6 RID: 7590 RVA: 0x0003D25C File Offset: 0x0003B45C
		public TimeSlotXML[] GetDiagnosticXML()
		{
			DateTime utcNow = TimeProvider.UtcNow;
			ulong[] array = this.ToArray();
			List<TimeSlotXML> list = new List<TimeSlotXML>(array.Length);
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i] > 0UL)
				{
					DateTime time = utcNow.Subtract(TimeSpan.FromMilliseconds((long)i * (long)this.MillisecondsPerSlot));
					list.Add(this.GetSlotXML(time, array[i]));
				}
			}
			return list.ToArray();
		}

		// Token: 0x04000C4B RID: 3147
		protected const ulong MillisecondsPerMonth = 2592000000UL;

		// Token: 0x04000C4C RID: 3148
		protected const ulong MillisecondsPerDay = 86400000UL;

		// Token: 0x04000C4D RID: 3149
		protected const ulong MillisecondsPerHour = 3600000UL;

		// Token: 0x04000C4E RID: 3150
		protected const ulong MillisecondsPerMinute = 60000UL;
	}
}
