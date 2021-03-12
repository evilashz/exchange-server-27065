using System;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000220 RID: 544
	public sealed class TransferProgressTrackerXML : XMLSerializableBase
	{
		// Token: 0x17000B84 RID: 2948
		// (get) Token: 0x06001D76 RID: 7542 RVA: 0x0003CD68 File Offset: 0x0003AF68
		// (set) Token: 0x06001D77 RID: 7543 RVA: 0x0003CD70 File Offset: 0x0003AF70
		[XmlElement(ElementName = "TotalBytesTransferred")]
		public ulong TotalBytesTransferred { get; set; }

		// Token: 0x17000B85 RID: 2949
		// (get) Token: 0x06001D78 RID: 7544 RVA: 0x0003CD79 File Offset: 0x0003AF79
		// (set) Token: 0x06001D79 RID: 7545 RVA: 0x0003CD81 File Offset: 0x0003AF81
		[XmlElement(ElementName = "TotalItemsTransferred")]
		public ulong TotalItemsTransferred { get; set; }

		// Token: 0x06001D7A RID: 7546 RVA: 0x0003CD8A File Offset: 0x0003AF8A
		public TransferProgressTrackerXML()
		{
		}

		// Token: 0x06001D7B RID: 7547 RVA: 0x0003CD94 File Offset: 0x0003AF94
		public TransferProgressTrackerXML(TransferProgressTracker tracker, bool showTimeSlots = false)
		{
			this.TotalBytesTransferred = tracker.BytesTransferred;
			this.TotalItemsTransferred = tracker.ItemsTransferred;
			if (showTimeSlots)
			{
				this.PerMinuteBytes = this.Convert(tracker.PerMinuteBytes);
				this.PerMinuteItems = this.Convert(tracker.PerMinuteItems);
				this.PerHourBytes = this.Convert(tracker.PerHourBytes);
				this.PerHourItems = this.Convert(tracker.PerHourItems);
				this.PerDayBytes = this.Convert(tracker.PerDayBytes);
				this.PerDayItems = this.Convert(tracker.PerDayItems);
				this.PerMonthBytes = this.Convert(tracker.PerMonthBytes);
				this.PerMonthItems = this.Convert(tracker.PerMonthItems);
			}
		}

		// Token: 0x06001D7C RID: 7548 RVA: 0x0003CE98 File Offset: 0x0003B098
		private TimeSlotXML[] Convert(FixedTimeSumSlot[] slots)
		{
			return Array.ConvertAll<FixedTimeSumSlot, TimeSlotXML>(slots, (FixedTimeSumSlot s) => new TimeSlotXML
			{
				StartTime = new DateTime(s.StartTimeInTicks, DateTimeKind.Utc).ToString("O"),
				Value = (ulong)s.Value
			});
		}

		// Token: 0x17000B86 RID: 2950
		// (get) Token: 0x06001D7D RID: 7549 RVA: 0x0003CEBD File Offset: 0x0003B0BD
		// (set) Token: 0x06001D7E RID: 7550 RVA: 0x0003CEC5 File Offset: 0x0003B0C5
		[XmlArray("PerMinuteBytes")]
		[XmlArrayItem("Minute")]
		public TimeSlotXML[] PerMinuteBytes { get; set; }

		// Token: 0x17000B87 RID: 2951
		// (get) Token: 0x06001D7F RID: 7551 RVA: 0x0003CECE File Offset: 0x0003B0CE
		// (set) Token: 0x06001D80 RID: 7552 RVA: 0x0003CED6 File Offset: 0x0003B0D6
		[XmlArrayItem("Hour")]
		[XmlArray("PerHourBytes")]
		public TimeSlotXML[] PerHourBytes { get; set; }

		// Token: 0x17000B88 RID: 2952
		// (get) Token: 0x06001D81 RID: 7553 RVA: 0x0003CEDF File Offset: 0x0003B0DF
		// (set) Token: 0x06001D82 RID: 7554 RVA: 0x0003CEE7 File Offset: 0x0003B0E7
		[XmlArray("PerDayBytes")]
		[XmlArrayItem("Day")]
		public TimeSlotXML[] PerDayBytes { get; set; }

		// Token: 0x17000B89 RID: 2953
		// (get) Token: 0x06001D83 RID: 7555 RVA: 0x0003CEF0 File Offset: 0x0003B0F0
		// (set) Token: 0x06001D84 RID: 7556 RVA: 0x0003CEF8 File Offset: 0x0003B0F8
		[XmlArray("PerMonthBytes")]
		[XmlArrayItem("Month")]
		public TimeSlotXML[] PerMonthBytes { get; set; }

		// Token: 0x17000B8A RID: 2954
		// (get) Token: 0x06001D85 RID: 7557 RVA: 0x0003CF01 File Offset: 0x0003B101
		// (set) Token: 0x06001D86 RID: 7558 RVA: 0x0003CF09 File Offset: 0x0003B109
		[XmlArray("PerMinuteItems")]
		[XmlArrayItem("Minute")]
		public TimeSlotXML[] PerMinuteItems { get; set; }

		// Token: 0x17000B8B RID: 2955
		// (get) Token: 0x06001D87 RID: 7559 RVA: 0x0003CF12 File Offset: 0x0003B112
		// (set) Token: 0x06001D88 RID: 7560 RVA: 0x0003CF1A File Offset: 0x0003B11A
		[XmlArray("PerHourItems")]
		[XmlArrayItem("Hour")]
		public TimeSlotXML[] PerHourItems { get; set; }

		// Token: 0x17000B8C RID: 2956
		// (get) Token: 0x06001D89 RID: 7561 RVA: 0x0003CF23 File Offset: 0x0003B123
		// (set) Token: 0x06001D8A RID: 7562 RVA: 0x0003CF2B File Offset: 0x0003B12B
		[XmlArrayItem("Day")]
		[XmlArray("PerDayItems")]
		public TimeSlotXML[] PerDayItems { get; set; }

		// Token: 0x17000B8D RID: 2957
		// (get) Token: 0x06001D8B RID: 7563 RVA: 0x0003CF34 File Offset: 0x0003B134
		// (set) Token: 0x06001D8C RID: 7564 RVA: 0x0003CF3C File Offset: 0x0003B13C
		[XmlArray("PerMonthItems")]
		[XmlArrayItem("Month")]
		public TimeSlotXML[] PerMonthItems { get; set; }
	}
}
