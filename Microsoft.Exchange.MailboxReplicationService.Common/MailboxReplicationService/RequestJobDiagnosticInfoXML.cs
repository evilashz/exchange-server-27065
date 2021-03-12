using System;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000206 RID: 518
	[XmlType(TypeName = "DiagnosticInfo")]
	public sealed class RequestJobDiagnosticInfoXML : XMLSerializableBase
	{
		// Token: 0x17000A6E RID: 2670
		// (get) Token: 0x06001AD8 RID: 6872 RVA: 0x00035E82 File Offset: 0x00034082
		// (set) Token: 0x06001AD9 RID: 6873 RVA: 0x00035E8A File Offset: 0x0003408A
		[XmlAttribute(AttributeName = "PoisonCount")]
		public int PoisonCount { get; set; }

		// Token: 0x17000A6F RID: 2671
		// (get) Token: 0x06001ADA RID: 6874 RVA: 0x00035E93 File Offset: 0x00034093
		// (set) Token: 0x06001ADB RID: 6875 RVA: 0x00035E9B File Offset: 0x0003409B
		[XmlAttribute(AttributeName = "LastPickupTime")]
		public DateTime LastPickupTime { get; set; }

		// Token: 0x17000A70 RID: 2672
		// (get) Token: 0x06001ADC RID: 6876 RVA: 0x00035EA4 File Offset: 0x000340A4
		// (set) Token: 0x06001ADD RID: 6877 RVA: 0x00035EAC File Offset: 0x000340AC
		[XmlAttribute(AttributeName = "DoNotPickUntil")]
		public DateTime DoNotPickUntil { get; set; }

		// Token: 0x17000A71 RID: 2673
		// (get) Token: 0x06001ADE RID: 6878 RVA: 0x00035EB5 File Offset: 0x000340B5
		// (set) Token: 0x06001ADF RID: 6879 RVA: 0x00035EBD File Offset: 0x000340BD
		[XmlAttribute(AttributeName = "LastProgressTime")]
		public DateTime LastProgressTime { get; set; }

		// Token: 0x17000A72 RID: 2674
		// (get) Token: 0x06001AE0 RID: 6880 RVA: 0x00035EC6 File Offset: 0x000340C6
		// (set) Token: 0x06001AE1 RID: 6881 RVA: 0x00035ECE File Offset: 0x000340CE
		[XmlAttribute(AttributeName = "IsCanceled")]
		public bool IsCanceled { get; set; }

		// Token: 0x17000A73 RID: 2675
		// (get) Token: 0x06001AE2 RID: 6882 RVA: 0x00035ED7 File Offset: 0x000340D7
		// (set) Token: 0x06001AE3 RID: 6883 RVA: 0x00035EDF File Offset: 0x000340DF
		[XmlAttribute(AttributeName = "RetryCount")]
		public int RetryCount { get; set; }

		// Token: 0x17000A74 RID: 2676
		// (get) Token: 0x06001AE4 RID: 6884 RVA: 0x00035EE8 File Offset: 0x000340E8
		// (set) Token: 0x06001AE5 RID: 6885 RVA: 0x00035EF0 File Offset: 0x000340F0
		[XmlAttribute(AttributeName = "TotalRetryCount")]
		public int TotalRetryCount { get; set; }

		// Token: 0x17000A75 RID: 2677
		// (get) Token: 0x06001AE6 RID: 6886 RVA: 0x00035EF9 File Offset: 0x000340F9
		// (set) Token: 0x06001AE7 RID: 6887 RVA: 0x00035F01 File Offset: 0x00034101
		[XmlAttribute(AttributeName = "DomainController")]
		public string DomainController { get; set; }

		// Token: 0x17000A76 RID: 2678
		// (get) Token: 0x06001AE8 RID: 6888 RVA: 0x00035F0A File Offset: 0x0003410A
		// (set) Token: 0x06001AE9 RID: 6889 RVA: 0x00035F12 File Offset: 0x00034112
		[XmlElement(ElementName = "JobPickupFailureMessage")]
		public string JobPickupFailureMessage { get; set; }

		// Token: 0x17000A77 RID: 2679
		// (get) Token: 0x06001AEA RID: 6890 RVA: 0x00035F1B File Offset: 0x0003411B
		// (set) Token: 0x06001AEB RID: 6891 RVA: 0x00035F23 File Offset: 0x00034123
		[XmlElement(ElementName = "SkippedItems")]
		public SkippedItemCounts SkippedItems { get; set; }

		// Token: 0x17000A78 RID: 2680
		// (get) Token: 0x06001AEC RID: 6892 RVA: 0x00035F2C File Offset: 0x0003412C
		// (set) Token: 0x06001AED RID: 6893 RVA: 0x00035F34 File Offset: 0x00034134
		[XmlElement(ElementName = "FailureHistory")]
		public FailureHistory FailureHistory { get; set; }

		// Token: 0x17000A79 RID: 2681
		// (get) Token: 0x06001AEE RID: 6894 RVA: 0x00035F3D File Offset: 0x0003413D
		// (set) Token: 0x06001AEF RID: 6895 RVA: 0x00035F45 File Offset: 0x00034145
		[XmlElement(ElementName = "TimeTracker")]
		public RequestJobTimeTrackerXML TimeTracker { get; set; }

		// Token: 0x17000A7A RID: 2682
		// (get) Token: 0x06001AF0 RID: 6896 RVA: 0x00035F4E File Offset: 0x0003414E
		// (set) Token: 0x06001AF1 RID: 6897 RVA: 0x00035F56 File Offset: 0x00034156
		[XmlElement(ElementName = "ProgressTracker")]
		public TransferProgressTrackerXML ProgressTracker { get; set; }
	}
}
