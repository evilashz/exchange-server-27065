using System;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000225 RID: 549
	[Serializable]
	public class DailyTimeSlot : TimeSlot
	{
		// Token: 0x06001DB3 RID: 7603 RVA: 0x0003D4A1 File Offset: 0x0003B6A1
		public DailyTimeSlot(uint capacity) : base(capacity, 86400000UL)
		{
		}

		// Token: 0x06001DB4 RID: 7604 RVA: 0x0003D4B0 File Offset: 0x0003B6B0
		public DailyTimeSlot(ulong[] inputArray) : base(90U, inputArray, 86400000UL)
		{
		}

		// Token: 0x06001DB5 RID: 7605 RVA: 0x0003D4C1 File Offset: 0x0003B6C1
		public DailyTimeSlot() : base(90U, 86400000UL)
		{
		}

		// Token: 0x06001DB6 RID: 7606 RVA: 0x0003D4D4 File Offset: 0x0003B6D4
		protected override uint GetElapsedSlotCount(DateTime lastUpdateTime, DateTime currentTime)
		{
			DateTime value = new DateTime(lastUpdateTime.Year, lastUpdateTime.Month, lastUpdateTime.Day, 0, 0, 0);
			DateTime dateTime = new DateTime(currentTime.Year, currentTime.Month, currentTime.Day, 0, 0, 0);
			return (uint)dateTime.Subtract(value).TotalDays;
		}

		// Token: 0x06001DB7 RID: 7607 RVA: 0x0003D530 File Offset: 0x0003B730
		protected override ulong GetLatestSlotMilliseconds(DateTime currentTime)
		{
			return (ulong)currentTime.Hour * 3600000UL + (ulong)currentTime.Minute * 60000UL + (ulong)(currentTime.Second * 1000) + (ulong)currentTime.Millisecond;
		}

		// Token: 0x06001DB8 RID: 7608 RVA: 0x0003D56C File Offset: 0x0003B76C
		protected override TimeSlotXML GetSlotXML(DateTime time, ulong value)
		{
			return new TimeSlotXML
			{
				StartTime = time.ToString("yyyy-MM-dd"),
				Value = value
			};
		}

		// Token: 0x04000C54 RID: 3156
		public const uint DefaultNumberOfDays = 90U;
	}
}
