using System;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000223 RID: 547
	[Serializable]
	public class PerMinuteTimeSlot : TimeSlot
	{
		// Token: 0x06001DA7 RID: 7591 RVA: 0x0003D2C4 File Offset: 0x0003B4C4
		public PerMinuteTimeSlot(uint capacity) : base(capacity, 60000UL)
		{
		}

		// Token: 0x06001DA8 RID: 7592 RVA: 0x0003D2D3 File Offset: 0x0003B4D3
		public PerMinuteTimeSlot(ulong[] inputArray) : base(120U, inputArray, 60000UL)
		{
		}

		// Token: 0x06001DA9 RID: 7593 RVA: 0x0003D2E4 File Offset: 0x0003B4E4
		public PerMinuteTimeSlot() : base(120U, 60000UL)
		{
		}

		// Token: 0x06001DAA RID: 7594 RVA: 0x0003D2F4 File Offset: 0x0003B4F4
		protected override uint GetElapsedSlotCount(DateTime lastUpdateTime, DateTime currentTime)
		{
			DateTime value = new DateTime(lastUpdateTime.Year, lastUpdateTime.Month, lastUpdateTime.Day, lastUpdateTime.Hour, lastUpdateTime.Minute, 0);
			DateTime dateTime = new DateTime(currentTime.Year, currentTime.Month, currentTime.Day, currentTime.Hour, currentTime.Minute, 0);
			return (uint)dateTime.Subtract(value).TotalMinutes;
		}

		// Token: 0x06001DAB RID: 7595 RVA: 0x0003D368 File Offset: 0x0003B568
		protected override ulong GetLatestSlotMilliseconds(DateTime currentTime)
		{
			return (ulong)(currentTime.Second * 1000 + currentTime.Millisecond);
		}

		// Token: 0x06001DAC RID: 7596 RVA: 0x0003D380 File Offset: 0x0003B580
		protected override TimeSlotXML GetSlotXML(DateTime time, ulong value)
		{
			return new TimeSlotXML
			{
				StartTime = time.ToString("yyyy-MM-dd HH:mm:00"),
				Value = value
			};
		}

		// Token: 0x04000C52 RID: 3154
		public const uint DefaultNumberOfMinutes = 120U;
	}
}
