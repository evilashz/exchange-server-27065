using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000049 RID: 73
	[XmlType(TypeName = "Queue")]
	public class MRSQueueDiagnosticInfoXML : XMLSerializableBase
	{
		// Token: 0x17000114 RID: 276
		// (get) Token: 0x060003F0 RID: 1008 RVA: 0x00018DF8 File Offset: 0x00016FF8
		// (set) Token: 0x060003F1 RID: 1009 RVA: 0x00018E00 File Offset: 0x00017000
		[XmlAttribute(AttributeName = "Guid")]
		public Guid MdbGuid { get; set; }

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x060003F2 RID: 1010 RVA: 0x00018E09 File Offset: 0x00017009
		// (set) Token: 0x060003F3 RID: 1011 RVA: 0x00018E11 File Offset: 0x00017011
		[XmlAttribute(AttributeName = "DBName")]
		public string MdbName { get; set; }

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x060003F4 RID: 1012 RVA: 0x00018E1A File Offset: 0x0001701A
		// (set) Token: 0x060003F5 RID: 1013 RVA: 0x00018E22 File Offset: 0x00017022
		[XmlAttribute(AttributeName = "LastJobPickup")]
		public DateTime LastJobPickup { get; set; }

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x060003F6 RID: 1014 RVA: 0x00018E2B File Offset: 0x0001702B
		// (set) Token: 0x060003F7 RID: 1015 RVA: 0x00018E33 File Offset: 0x00017033
		[XmlAttribute(AttributeName = "LastInteractiveJobPickup")]
		public DateTime LastInteractiveJobPickup { get; set; }

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x060003F8 RID: 1016 RVA: 0x00018E3C File Offset: 0x0001703C
		// (set) Token: 0x060003F9 RID: 1017 RVA: 0x00018E44 File Offset: 0x00017044
		[XmlAttribute(AttributeName = "QueuedJobs")]
		public int QueuedJobsCount { get; set; }

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x060003FA RID: 1018 RVA: 0x00018E4D File Offset: 0x0001704D
		// (set) Token: 0x060003FB RID: 1019 RVA: 0x00018E55 File Offset: 0x00017055
		[XmlAttribute(AttributeName = "InProgressJobs")]
		public int InProgressJobsCount { get; set; }

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x060003FC RID: 1020 RVA: 0x00018E5E File Offset: 0x0001705E
		// (set) Token: 0x060003FD RID: 1021 RVA: 0x00018E66 File Offset: 0x00017066
		[XmlText]
		public string LastScanFailure { get; set; }

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x060003FE RID: 1022 RVA: 0x00018E6F File Offset: 0x0001706F
		// (set) Token: 0x060003FF RID: 1023 RVA: 0x00018E77 File Offset: 0x00017077
		[XmlAttribute(AttributeName = "MdbDiscovery")]
		public DateTime MdbDiscoveryTimestamp { get; set; }

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x06000400 RID: 1024 RVA: 0x00018E80 File Offset: 0x00017080
		// (set) Token: 0x06000401 RID: 1025 RVA: 0x00018E88 File Offset: 0x00017088
		[XmlAttribute(AttributeName = "LastScan")]
		public DateTime LastScanTimestamp { get; set; }

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x06000402 RID: 1026 RVA: 0x00018E91 File Offset: 0x00017091
		// (set) Token: 0x06000403 RID: 1027 RVA: 0x00018E99 File Offset: 0x00017099
		[XmlAttribute(AttributeName = "LastScanDurationMs")]
		public int LastScanDurationMs { get; set; }

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x06000404 RID: 1028 RVA: 0x00018EA2 File Offset: 0x000170A2
		// (set) Token: 0x06000405 RID: 1029 RVA: 0x00018EAA File Offset: 0x000170AA
		[XmlAttribute(AttributeName = "NextScan")]
		public DateTime NextRecommendedScan { get; set; }

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x06000406 RID: 1030 RVA: 0x00018EB3 File Offset: 0x000170B3
		// (set) Token: 0x06000407 RID: 1031 RVA: 0x00018EBB File Offset: 0x000170BB
		[XmlAttribute(AttributeName = "lastfinishtime")]
		public DateTime LastActiveJobFinishTime { get; set; }

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x06000408 RID: 1032 RVA: 0x00018EC4 File Offset: 0x000170C4
		// (set) Token: 0x06000409 RID: 1033 RVA: 0x00018ECC File Offset: 0x000170CC
		[XmlAttribute(AttributeName = "lastfinishjob")]
		public Guid LastActiveJobFinished { get; set; }

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x0600040A RID: 1034 RVA: 0x00018ED5 File Offset: 0x000170D5
		// (set) Token: 0x0600040B RID: 1035 RVA: 0x00018EDD File Offset: 0x000170DD
		[XmlArray("LastScanResults")]
		public List<JobPickupRec> LastScanResults { get; set; }

		// Token: 0x0600040C RID: 1036 RVA: 0x00018EE6 File Offset: 0x000170E6
		public bool IsEmpty()
		{
			return this.QueuedJobsCount == 0 && this.InProgressJobsCount == 0;
		}
	}
}
