using System;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000224 RID: 548
	[Serializable]
	public class HourlyTimeSlot : TimeSlot
	{
		// Token: 0x06001DAD RID: 7597 RVA: 0x0003D3AD File Offset: 0x0003B5AD
		public HourlyTimeSlot(uint capacity) : base(capacity, 3600000UL)
		{
		}

		// Token: 0x06001DAE RID: 7598 RVA: 0x0003D3BC File Offset: 0x0003B5BC
		public HourlyTimeSlot(ulong[] inputArray) : base(120U, inputArray, 3600000UL)
		{
		}

		// Token: 0x06001DAF RID: 7599 RVA: 0x0003D3CD File Offset: 0x0003B5CD
		public HourlyTimeSlot() : base(120U, 3600000UL)
		{
		}

		// Token: 0x06001DB0 RID: 7600 RVA: 0x0003D3E0 File Offset: 0x0003B5E0
		protected override uint GetElapsedSlotCount(DateTime lastUpdateTime, DateTime currentTime)
		{
			DateTime value = new DateTime(lastUpdateTime.Year, lastUpdateTime.Month, lastUpdateTime.Day, lastUpdateTime.Hour, 0, 0);
			DateTime dateTime = new DateTime(currentTime.Year, currentTime.Month, currentTime.Day, currentTime.Hour, 0, 0);
			return (uint)dateTime.Subtract(value).TotalHours;
		}

		// Token: 0x06001DB1 RID: 7601 RVA: 0x0003D448 File Offset: 0x0003B648
		protected override ulong GetLatestSlotMilliseconds(DateTime currentTime)
		{
			return (ulong)currentTime.Minute * 60000UL + (ulong)(currentTime.Second * 1000) + (ulong)currentTime.Millisecond;
		}

		// Token: 0x06001DB2 RID: 7602 RVA: 0x0003D474 File Offset: 0x0003B674
		protected override TimeSlotXML GetSlotXML(DateTime time, ulong value)
		{
			return new TimeSlotXML
			{
				StartTime = time.ToString("yyyy-MM-dd HH:00:00"),
				Value = value
			};
		}

		// Token: 0x04000C53 RID: 3155
		public const uint DefaultNumberOfHours = 120U;
	}
}
