using System;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000226 RID: 550
	[Serializable]
	public class MonthlyTimeSlot : TimeSlot
	{
		// Token: 0x06001DB9 RID: 7609 RVA: 0x0003D599 File Offset: 0x0003B799
		public MonthlyTimeSlot(uint capacity) : base(capacity, (ulong)-1702967296)
		{
		}

		// Token: 0x06001DBA RID: 7610 RVA: 0x0003D5A8 File Offset: 0x0003B7A8
		public MonthlyTimeSlot(ulong[] inputArray) : base(24U, inputArray, (ulong)-1702967296)
		{
		}

		// Token: 0x06001DBB RID: 7611 RVA: 0x0003D5B9 File Offset: 0x0003B7B9
		public MonthlyTimeSlot() : base(24U, (ulong)-1702967296)
		{
		}

		// Token: 0x06001DBC RID: 7612 RVA: 0x0003D5C9 File Offset: 0x0003B7C9
		protected override uint GetElapsedSlotCount(DateTime lastUpdateTime, DateTime currentTime)
		{
			return (uint)(currentTime.Year * 12 + currentTime.Month - (lastUpdateTime.Year * 12 + lastUpdateTime.Month));
		}

		// Token: 0x06001DBD RID: 7613 RVA: 0x0003D5F0 File Offset: 0x0003B7F0
		protected override ulong GetLatestSlotMilliseconds(DateTime currentTime)
		{
			return (ulong)((long)(currentTime.Day - 1) * 86400000L + (long)((ulong)currentTime.Hour * 3600000UL) + (long)((ulong)currentTime.Minute * 60000UL) + (long)((ulong)(currentTime.Second * 1000)) + (long)((ulong)currentTime.Millisecond));
		}

		// Token: 0x06001DBE RID: 7614 RVA: 0x0003D648 File Offset: 0x0003B848
		protected override TimeSlotXML GetSlotXML(DateTime time, ulong value)
		{
			return new TimeSlotXML
			{
				StartTime = time.ToString("yyyy-MM-01"),
				Value = value
			};
		}

		// Token: 0x04000C55 RID: 3157
		public const uint DefaultNumberOfMonths = 24U;
	}
}
