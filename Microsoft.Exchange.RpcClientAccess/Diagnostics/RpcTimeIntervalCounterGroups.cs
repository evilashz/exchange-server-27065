using System;
using System.Collections.Generic;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.RpcClientAccess.Diagnostics
{
	// Token: 0x0200003E RID: 62
	internal sealed class RpcTimeIntervalCounterGroups<T> where T : IRpcCounters, new()
	{
		// Token: 0x06000258 RID: 600 RVA: 0x000089E9 File Offset: 0x00006BE9
		public RpcTimeIntervalCounterGroups() : this(RpcTimeIntervalCounterGroups<T>.DefaultIntervalSize)
		{
		}

		// Token: 0x06000259 RID: 601 RVA: 0x000089F6 File Offset: 0x00006BF6
		public RpcTimeIntervalCounterGroups(TimeSpan timeIntervalDuration)
		{
			if (timeIntervalDuration == TimeSpan.Zero)
			{
				throw new ArgumentException("timeIntervalDuration must indicate a non-zero duration for the interval.");
			}
			this.timeIntervalDuration = timeIntervalDuration;
			this.timeIntervalCounterGroups = new Dictionary<ExDateTime, T>();
		}

		// Token: 0x0600025A RID: 602 RVA: 0x00008A28 File Offset: 0x00006C28
		public void IncrementCounter(ExDateTime timeStamp, IRpcCounterData counterData)
		{
			ExDateTime intervalEndForTimestamp = this.GetIntervalEndForTimestamp(timeStamp);
			T value;
			if (!this.timeIntervalCounterGroups.TryGetValue(intervalEndForTimestamp, out value))
			{
				value = ((default(T) == null) ? Activator.CreateInstance<T>() : default(T));
				this.timeIntervalCounterGroups.Add(intervalEndForTimestamp, value);
			}
			value.IncrementCounter(counterData);
		}

		// Token: 0x0600025B RID: 603 RVA: 0x00008A89 File Offset: 0x00006C89
		public ICollection<ExDateTime> GetTimeIntervals()
		{
			return this.timeIntervalCounterGroups.Keys;
		}

		// Token: 0x0600025C RID: 604 RVA: 0x00008A98 File Offset: 0x00006C98
		public string GetFormattedCounterDataForInterval(ExDateTime intervalEndTime)
		{
			T t;
			if (!this.timeIntervalCounterGroups.TryGetValue(intervalEndTime, out t))
			{
				throw new ArgumentException("The specified interval end time does not contain any associated counter data.");
			}
			return t.ToString();
		}

		// Token: 0x0600025D RID: 605 RVA: 0x00008ACD File Offset: 0x00006CCD
		public void ResetCounters()
		{
			this.timeIntervalCounterGroups.Clear();
		}

		// Token: 0x0600025E RID: 606 RVA: 0x00008ADC File Offset: 0x00006CDC
		private ExDateTime GetIntervalEndForTimestamp(ExDateTime timeStamp)
		{
			long num = (timeStamp.UtcTicks + this.timeIntervalDuration.Ticks) / this.timeIntervalDuration.Ticks;
			return new ExDateTime(ExTimeZone.UtcTimeZone, new DateTime(num * this.timeIntervalDuration.Ticks));
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x0600025F RID: 607 RVA: 0x00008B2E File Offset: 0x00006D2E
		public TimeSpan TimeIntervalDuration
		{
			get
			{
				return this.timeIntervalDuration;
			}
		}

		// Token: 0x040001E1 RID: 481
		private static readonly TimeSpan DefaultIntervalSize = TimeSpan.FromHours(1.0);

		// Token: 0x040001E2 RID: 482
		private readonly IDictionary<ExDateTime, T> timeIntervalCounterGroups;

		// Token: 0x040001E3 RID: 483
		private readonly TimeSpan timeIntervalDuration;
	}
}
