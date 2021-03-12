using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200021A RID: 538
	public sealed class RequestJobTimeTrackerXML : XMLSerializableBase
	{
		// Token: 0x17000AA2 RID: 2722
		// (get) Token: 0x06001BB2 RID: 7090 RVA: 0x0003B34B File Offset: 0x0003954B
		// (set) Token: 0x06001BB3 RID: 7091 RVA: 0x0003B353 File Offset: 0x00039553
		[XmlElement(ElementName = "CurrentState")]
		public string CurrentState { get; set; }

		// Token: 0x17000AA3 RID: 2723
		// (get) Token: 0x06001BB4 RID: 7092 RVA: 0x0003B35C File Offset: 0x0003955C
		// (set) Token: 0x06001BB5 RID: 7093 RVA: 0x0003B364 File Offset: 0x00039564
		[XmlElement(ElementName = "LastStateChangeTimeStamp")]
		public string LastStateChangeTimeStamp { get; set; }

		// Token: 0x17000AA4 RID: 2724
		// (get) Token: 0x06001BB6 RID: 7094 RVA: 0x0003B36D File Offset: 0x0003956D
		// (set) Token: 0x06001BB7 RID: 7095 RVA: 0x0003B375 File Offset: 0x00039575
		[XmlElement(ElementName = "Timestamp")]
		public List<RequestJobTimeTrackerXML.TimestampRec> Timestamps { get; set; }

		// Token: 0x17000AA5 RID: 2725
		// (get) Token: 0x06001BB8 RID: 7096 RVA: 0x0003B37E File Offset: 0x0003957E
		// (set) Token: 0x06001BB9 RID: 7097 RVA: 0x0003B386 File Offset: 0x00039586
		[XmlElement(ElementName = "Duration")]
		public List<RequestJobTimeTrackerXML.DurationRec> Durations { get; set; }

		// Token: 0x06001BBA RID: 7098 RVA: 0x0003B390 File Offset: 0x00039590
		public void AddTimestamp(RequestJobTimestamp mrt, DateTime value)
		{
			if (this.Timestamps == null)
			{
				this.Timestamps = new List<RequestJobTimeTrackerXML.TimestampRec>();
			}
			this.Timestamps.Add(new RequestJobTimeTrackerXML.TimestampRec
			{
				Type = mrt.ToString(),
				Timestamp = value
			});
		}

		// Token: 0x0200021B RID: 539
		public sealed class TimestampRec : XMLSerializableBase
		{
			// Token: 0x17000AA6 RID: 2726
			// (get) Token: 0x06001BBC RID: 7100 RVA: 0x0003B3E2 File Offset: 0x000395E2
			// (set) Token: 0x06001BBD RID: 7101 RVA: 0x0003B3EA File Offset: 0x000395EA
			[XmlAttribute(AttributeName = "Type")]
			public string Type { get; set; }

			// Token: 0x17000AA7 RID: 2727
			// (get) Token: 0x06001BBE RID: 7102 RVA: 0x0003B3F3 File Offset: 0x000395F3
			// (set) Token: 0x06001BBF RID: 7103 RVA: 0x0003B3FB File Offset: 0x000395FB
			[XmlAttribute(AttributeName = "Time")]
			public DateTime Timestamp { get; set; }
		}

		// Token: 0x0200021C RID: 540
		public sealed class DurationRec : XMLSerializableBase
		{
			// Token: 0x17000AA8 RID: 2728
			// (get) Token: 0x06001BC1 RID: 7105 RVA: 0x0003B40C File Offset: 0x0003960C
			// (set) Token: 0x06001BC2 RID: 7106 RVA: 0x0003B414 File Offset: 0x00039614
			[XmlAttribute(AttributeName = "State")]
			public string State { get; set; }

			// Token: 0x17000AA9 RID: 2729
			// (get) Token: 0x06001BC3 RID: 7107 RVA: 0x0003B41D File Offset: 0x0003961D
			// (set) Token: 0x06001BC4 RID: 7108 RVA: 0x0003B425 File Offset: 0x00039625
			[XmlAttribute(AttributeName = "Dur")]
			public string Duration { get; set; }

			// Token: 0x17000AAA RID: 2730
			// (get) Token: 0x06001BC5 RID: 7109 RVA: 0x0003B42E File Offset: 0x0003962E
			// (set) Token: 0x06001BC6 RID: 7110 RVA: 0x0003B436 File Offset: 0x00039636
			[XmlArray("PerMinute")]
			[XmlArrayItem("Minute")]
			public TimeSlotXML[] PerMinute { get; set; }

			// Token: 0x17000AAB RID: 2731
			// (get) Token: 0x06001BC7 RID: 7111 RVA: 0x0003B43F File Offset: 0x0003963F
			// (set) Token: 0x06001BC8 RID: 7112 RVA: 0x0003B447 File Offset: 0x00039647
			[XmlArrayItem("Hour")]
			[XmlArray("PerHour")]
			public TimeSlotXML[] PerHour { get; set; }

			// Token: 0x17000AAC RID: 2732
			// (get) Token: 0x06001BC9 RID: 7113 RVA: 0x0003B450 File Offset: 0x00039650
			// (set) Token: 0x06001BCA RID: 7114 RVA: 0x0003B458 File Offset: 0x00039658
			[XmlArray("PerDay")]
			[XmlArrayItem("Day")]
			public TimeSlotXML[] PerDay { get; set; }

			// Token: 0x17000AAD RID: 2733
			// (get) Token: 0x06001BCB RID: 7115 RVA: 0x0003B461 File Offset: 0x00039661
			// (set) Token: 0x06001BCC RID: 7116 RVA: 0x0003B469 File Offset: 0x00039669
			[XmlArray("PerMonth")]
			[XmlArrayItem("Month")]
			public TimeSlotXML[] PerMonth { get; set; }

			// Token: 0x17000AAE RID: 2734
			// (get) Token: 0x06001BCD RID: 7117 RVA: 0x0003B472 File Offset: 0x00039672
			// (set) Token: 0x06001BCE RID: 7118 RVA: 0x0003B47A File Offset: 0x0003967A
			[XmlElement(ElementName = "Duration")]
			public List<RequestJobTimeTrackerXML.DurationRec> ChildNodes { get; set; }
		}
	}
}
