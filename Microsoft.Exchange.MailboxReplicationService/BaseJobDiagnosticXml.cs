using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200008B RID: 139
	[XmlType(TypeName = "job")]
	public class BaseJobDiagnosticXml : XMLSerializableBase
	{
		// Token: 0x17000194 RID: 404
		// (get) Token: 0x060006DC RID: 1756 RVA: 0x0002FB38 File Offset: 0x0002DD38
		// (set) Token: 0x060006DD RID: 1757 RVA: 0x0002FB40 File Offset: 0x0002DD40
		[XmlAttribute(AttributeName = "PickupTime")]
		public DateTime JobPickupTimestamp { get; set; }

		// Token: 0x17000195 RID: 405
		// (get) Token: 0x060006DE RID: 1758 RVA: 0x0002FB49 File Offset: 0x0002DD49
		// (set) Token: 0x060006DF RID: 1759 RVA: 0x0002FB51 File Offset: 0x0002DD51
		[XmlAttribute(AttributeName = "Guid")]
		public Guid RequestGuid { get; set; }

		// Token: 0x17000196 RID: 406
		// (get) Token: 0x060006E0 RID: 1760 RVA: 0x0002FB5A File Offset: 0x0002DD5A
		// (set) Token: 0x060006E1 RID: 1761 RVA: 0x0002FB62 File Offset: 0x0002DD62
		[XmlAttribute(AttributeName = "Queue")]
		public string RequestQueue { get; set; }

		// Token: 0x17000197 RID: 407
		// (get) Token: 0x060006E2 RID: 1762 RVA: 0x0002FB6B File Offset: 0x0002DD6B
		// (set) Token: 0x060006E3 RID: 1763 RVA: 0x0002FB73 File Offset: 0x0002DD73
		[XmlAttribute(AttributeName = "Type")]
		public string RequestType { get; set; }

		// Token: 0x17000198 RID: 408
		// (get) Token: 0x060006E4 RID: 1764 RVA: 0x0002FB7C File Offset: 0x0002DD7C
		// (set) Token: 0x060006E5 RID: 1765 RVA: 0x0002FB84 File Offset: 0x0002DD84
		[XmlAttribute(AttributeName = "RetryCount")]
		public int RetryCount { get; set; }

		// Token: 0x17000199 RID: 409
		// (get) Token: 0x060006E6 RID: 1766 RVA: 0x0002FB8D File Offset: 0x0002DD8D
		// (set) Token: 0x060006E7 RID: 1767 RVA: 0x0002FB95 File Offset: 0x0002DD95
		[XmlAttribute(AttributeName = "TotalRetryCount")]
		public int TotalRetryCount { get; set; }

		// Token: 0x1700019A RID: 410
		// (get) Token: 0x060006E8 RID: 1768 RVA: 0x0002FB9E File Offset: 0x0002DD9E
		// (set) Token: 0x060006E9 RID: 1769 RVA: 0x0002FBA6 File Offset: 0x0002DDA6
		[XmlAttribute(AttributeName = "SyncStage")]
		public SyncStage SyncStage { get; set; }

		// Token: 0x1700019B RID: 411
		// (get) Token: 0x060006EA RID: 1770 RVA: 0x0002FBAF File Offset: 0x0002DDAF
		// (set) Token: 0x060006EB RID: 1771 RVA: 0x0002FBB7 File Offset: 0x0002DDB7
		[XmlAttribute(AttributeName = "ThrottledBy")]
		public string CurrentlyThrottledResource { get; set; }

		// Token: 0x1700019C RID: 412
		// (get) Token: 0x060006EC RID: 1772 RVA: 0x0002FBC0 File Offset: 0x0002DDC0
		// (set) Token: 0x060006ED RID: 1773 RVA: 0x0002FBC8 File Offset: 0x0002DDC8
		[XmlAttribute(AttributeName = "ThrottledByMetricType")]
		public int CurrentlyThrottledResourceMetricType { get; set; }

		// Token: 0x1700019D RID: 413
		// (get) Token: 0x060006EE RID: 1774 RVA: 0x0002FBD1 File Offset: 0x0002DDD1
		// (set) Token: 0x060006EF RID: 1775 RVA: 0x0002FBD9 File Offset: 0x0002DDD9
		[XmlAttribute(AttributeName = "ThrottledSince")]
		public DateTime ThrottledSince { get; set; }

		// Token: 0x1700019E RID: 414
		// (get) Token: 0x060006F0 RID: 1776 RVA: 0x0002FBE2 File Offset: 0x0002DDE2
		// (set) Token: 0x060006F1 RID: 1777 RVA: 0x0002FBEA File Offset: 0x0002DDEA
		[XmlAttribute(AttributeName = "BadItemsEncountered")]
		public int BadItemsEncountered { get; set; }

		// Token: 0x1700019F RID: 415
		// (get) Token: 0x060006F2 RID: 1778 RVA: 0x0002FBF3 File Offset: 0x0002DDF3
		// (set) Token: 0x060006F3 RID: 1779 RVA: 0x0002FBFB File Offset: 0x0002DDFB
		[XmlAttribute(AttributeName = "LargeItemsEncountered")]
		public int LargeItemsEncountered { get; set; }

		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x060006F4 RID: 1780 RVA: 0x0002FC04 File Offset: 0x0002DE04
		// (set) Token: 0x060006F5 RID: 1781 RVA: 0x0002FC0C File Offset: 0x0002DE0C
		[XmlAttribute(AttributeName = "MissingItemsEncountered")]
		public int MissingItemsEncountered { get; set; }

		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x060006F6 RID: 1782 RVA: 0x0002FC15 File Offset: 0x0002DE15
		// (set) Token: 0x060006F7 RID: 1783 RVA: 0x0002FC1D File Offset: 0x0002DE1D
		[XmlAttribute(AttributeName = "LastProgressTimestamp")]
		public DateTime LastProgressTimestamp { get; set; }

		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x060006F8 RID: 1784 RVA: 0x0002FC26 File Offset: 0x0002DE26
		// (set) Token: 0x060006F9 RID: 1785 RVA: 0x0002FC2E File Offset: 0x0002DE2E
		[XmlElement("Progress")]
		public BaseJobDiagnosticXml.JobTransferProgress JobTransferProgressRec { get; set; }

		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x060006FA RID: 1786 RVA: 0x0002FC37 File Offset: 0x0002DE37
		// (set) Token: 0x060006FB RID: 1787 RVA: 0x0002FC3F File Offset: 0x0002DE3F
		[XmlAttribute(AttributeName = "TimeTrackerCurrentState")]
		public string TimeTrackerCurrentState { get; set; }

		// Token: 0x040002E0 RID: 736
		[XmlArray("Reservations")]
		public List<BaseJobDiagnosticXml.ReservationRec> ReservationRecs;

		// Token: 0x040002E1 RID: 737
		[XmlArray("Warnings")]
		public List<string> Warnings;

		// Token: 0x0200008C RID: 140
		[XmlType(TypeName = "Progress")]
		public class JobTransferProgress : XMLSerializableBase
		{
			// Token: 0x170001A4 RID: 420
			// (get) Token: 0x060006FC RID: 1788 RVA: 0x0002FC48 File Offset: 0x0002DE48
			// (set) Token: 0x060006FD RID: 1789 RVA: 0x0002FC50 File Offset: 0x0002DE50
			[XmlAttribute(AttributeName = "MsgsWritten")]
			public int MessagesWritten { get; set; }

			// Token: 0x170001A5 RID: 421
			// (get) Token: 0x060006FE RID: 1790 RVA: 0x0002FC59 File Offset: 0x0002DE59
			// (set) Token: 0x060006FF RID: 1791 RVA: 0x0002FC61 File Offset: 0x0002DE61
			[XmlAttribute(AttributeName = "MsgSizeWritten")]
			public ulong MessageSizeWritten { get; set; }

			// Token: 0x170001A6 RID: 422
			// (get) Token: 0x06000700 RID: 1792 RVA: 0x0002FC6A File Offset: 0x0002DE6A
			// (set) Token: 0x06000701 RID: 1793 RVA: 0x0002FC72 File Offset: 0x0002DE72
			[XmlAttribute(AttributeName = "TotalMsgs")]
			public int TotalMessages { get; set; }

			// Token: 0x170001A7 RID: 423
			// (get) Token: 0x06000702 RID: 1794 RVA: 0x0002FC7B File Offset: 0x0002DE7B
			// (set) Token: 0x06000703 RID: 1795 RVA: 0x0002FC83 File Offset: 0x0002DE83
			[XmlAttribute(AttributeName = "TotalMsgByteSize")]
			public ulong TotalMessageByteSize { get; set; }

			// Token: 0x170001A8 RID: 424
			// (get) Token: 0x06000704 RID: 1796 RVA: 0x0002FC8C File Offset: 0x0002DE8C
			// (set) Token: 0x06000705 RID: 1797 RVA: 0x0002FC94 File Offset: 0x0002DE94
			[XmlAttribute(AttributeName = "OverallProgress")]
			public int OverallProgress { get; set; }

			// Token: 0x170001A9 RID: 425
			// (get) Token: 0x06000706 RID: 1798 RVA: 0x0002FC9D File Offset: 0x0002DE9D
			// (set) Token: 0x06000707 RID: 1799 RVA: 0x0002FCA5 File Offset: 0x0002DEA5
			[XmlElement("ThroughputProgressTracker")]
			public TransferProgressTrackerXML ThroughputProgressTracker { get; set; }
		}

		// Token: 0x0200008D RID: 141
		[XmlType(TypeName = "ReservationRec")]
		public class ReservationRec : XMLSerializableBase
		{
			// Token: 0x170001AA RID: 426
			// (get) Token: 0x06000709 RID: 1801 RVA: 0x0002FCB6 File Offset: 0x0002DEB6
			// (set) Token: 0x0600070A RID: 1802 RVA: 0x0002FCBE File Offset: 0x0002DEBE
			[XmlAttribute(AttributeName = "Id")]
			public Guid Id { get; set; }

			// Token: 0x170001AB RID: 427
			// (get) Token: 0x0600070B RID: 1803 RVA: 0x0002FCC7 File Offset: 0x0002DEC7
			// (set) Token: 0x0600070C RID: 1804 RVA: 0x0002FCCF File Offset: 0x0002DECF
			[XmlAttribute(AttributeName = "Flags")]
			public string Flags { get; set; }

			// Token: 0x170001AC RID: 428
			// (get) Token: 0x0600070D RID: 1805 RVA: 0x0002FCD8 File Offset: 0x0002DED8
			// (set) Token: 0x0600070E RID: 1806 RVA: 0x0002FCE0 File Offset: 0x0002DEE0
			[XmlAttribute(AttributeName = "Resource")]
			public Guid ResourceId { get; set; }
		}
	}
}
