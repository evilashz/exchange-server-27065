using System;
using System.Xml.Serialization;
using Microsoft.Exchange.Connections.Eas.Model.Request.AirSyncBase;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Connections.Eas.Model.Request.ItemOperations
{
	// Token: 0x020000A3 RID: 163
	[XmlType(Namespace = "ItemOperations", TypeName = "Schema")]
	[ClassAccessLevel(AccessLevel.Implementation)]
	public class Schema
	{
		// Token: 0x17000180 RID: 384
		// (get) Token: 0x060003F5 RID: 1013 RVA: 0x0000A5AB File Offset: 0x000087AB
		// (set) Token: 0x060003F6 RID: 1014 RVA: 0x0000A5B3 File Offset: 0x000087B3
		[XmlElement(ElementName = "Attachments", Namespace = "AirSyncBase")]
		public Attachment Attachments { get; set; }

		// Token: 0x17000181 RID: 385
		// (get) Token: 0x060003F7 RID: 1015 RVA: 0x0000A5BC File Offset: 0x000087BC
		// (set) Token: 0x060003F8 RID: 1016 RVA: 0x0000A5C4 File Offset: 0x000087C4
		[XmlElement(ElementName = "Body", Namespace = "AirSyncBase")]
		public Body Body { get; set; }

		// Token: 0x17000182 RID: 386
		// (get) Token: 0x060003F9 RID: 1017 RVA: 0x0000A5CD File Offset: 0x000087CD
		// (set) Token: 0x060003FA RID: 1018 RVA: 0x0000A5D5 File Offset: 0x000087D5
		[XmlElement(ElementName = "CC", Namespace = "Email")]
		public string CC { get; set; }

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x060003FB RID: 1019 RVA: 0x0000A5DE File Offset: 0x000087DE
		// (set) Token: 0x060003FC RID: 1020 RVA: 0x0000A5E6 File Offset: 0x000087E6
		[XmlElement(ElementName = "DateReceived", Namespace = "Email")]
		public string DateReceived { get; set; }

		// Token: 0x17000184 RID: 388
		// (get) Token: 0x060003FD RID: 1021 RVA: 0x0000A5EF File Offset: 0x000087EF
		// (set) Token: 0x060003FE RID: 1022 RVA: 0x0000A5F7 File Offset: 0x000087F7
		[XmlElement(ElementName = "DisplayTo", Namespace = "Email")]
		public string DisplayTo { get; set; }

		// Token: 0x17000185 RID: 389
		// (get) Token: 0x060003FF RID: 1023 RVA: 0x0000A600 File Offset: 0x00008800
		// (set) Token: 0x06000400 RID: 1024 RVA: 0x0000A608 File Offset: 0x00008808
		[XmlElement(ElementName = "From", Namespace = "Email")]
		public string From { get; set; }

		// Token: 0x17000186 RID: 390
		// (get) Token: 0x06000401 RID: 1025 RVA: 0x0000A611 File Offset: 0x00008811
		// (set) Token: 0x06000402 RID: 1026 RVA: 0x0000A619 File Offset: 0x00008819
		[XmlElement(ElementName = "Importance", Namespace = "Email")]
		public byte? Importance { get; set; }

		// Token: 0x17000187 RID: 391
		// (get) Token: 0x06000403 RID: 1027 RVA: 0x0000A622 File Offset: 0x00008822
		// (set) Token: 0x06000404 RID: 1028 RVA: 0x0000A62A File Offset: 0x0000882A
		[XmlElement(ElementName = "InternetCPID", Namespace = "Email")]
		public int? InternetCpid { get; set; }

		// Token: 0x17000188 RID: 392
		// (get) Token: 0x06000405 RID: 1029 RVA: 0x0000A633 File Offset: 0x00008833
		// (set) Token: 0x06000406 RID: 1030 RVA: 0x0000A63B File Offset: 0x0000883B
		[XmlElement(ElementName = "MeetingRequest", Namespace = "Email")]
		public string MeetingRequest { get; set; }

		// Token: 0x17000189 RID: 393
		// (get) Token: 0x06000407 RID: 1031 RVA: 0x0000A644 File Offset: 0x00008844
		// (set) Token: 0x06000408 RID: 1032 RVA: 0x0000A64C File Offset: 0x0000884C
		[XmlElement(ElementName = "MessageClass", Namespace = "Email")]
		public string MessageClass { get; set; }

		// Token: 0x1700018A RID: 394
		// (get) Token: 0x06000409 RID: 1033 RVA: 0x0000A655 File Offset: 0x00008855
		// (set) Token: 0x0600040A RID: 1034 RVA: 0x0000A65D File Offset: 0x0000885D
		[XmlElement(ElementName = "Read", Namespace = "Email")]
		public byte? Read { get; set; }

		// Token: 0x1700018B RID: 395
		// (get) Token: 0x0600040B RID: 1035 RVA: 0x0000A666 File Offset: 0x00008866
		// (set) Token: 0x0600040C RID: 1036 RVA: 0x0000A66E File Offset: 0x0000886E
		[XmlElement(ElementName = "ReplyTo", Namespace = "Email")]
		public string ReplyTo { get; set; }

		// Token: 0x1700018C RID: 396
		// (get) Token: 0x0600040D RID: 1037 RVA: 0x0000A677 File Offset: 0x00008877
		// (set) Token: 0x0600040E RID: 1038 RVA: 0x0000A67F File Offset: 0x0000887F
		[XmlElement(ElementName = "Subject", Namespace = "Email")]
		public string Subject { get; set; }

		// Token: 0x1700018D RID: 397
		// (get) Token: 0x0600040F RID: 1039 RVA: 0x0000A688 File Offset: 0x00008888
		// (set) Token: 0x06000410 RID: 1040 RVA: 0x0000A690 File Offset: 0x00008890
		[XmlElement(ElementName = "ThreadTopic", Namespace = "Email")]
		public string ThreadTopic { get; set; }

		// Token: 0x1700018E RID: 398
		// (get) Token: 0x06000411 RID: 1041 RVA: 0x0000A699 File Offset: 0x00008899
		// (set) Token: 0x06000412 RID: 1042 RVA: 0x0000A6A1 File Offset: 0x000088A1
		[XmlElement(ElementName = "To", Namespace = "Email")]
		public string To { get; set; }

		// Token: 0x1700018F RID: 399
		// (get) Token: 0x06000413 RID: 1043 RVA: 0x0000A6AC File Offset: 0x000088AC
		[XmlIgnore]
		public bool ImportanceSpecified
		{
			get
			{
				return this.Importance != null;
			}
		}

		// Token: 0x17000190 RID: 400
		// (get) Token: 0x06000414 RID: 1044 RVA: 0x0000A6C8 File Offset: 0x000088C8
		[XmlIgnore]
		public bool InternetCpidSpecified
		{
			get
			{
				return this.InternetCpid != null;
			}
		}

		// Token: 0x17000191 RID: 401
		// (get) Token: 0x06000415 RID: 1045 RVA: 0x0000A6E4 File Offset: 0x000088E4
		[XmlIgnore]
		public bool ReadSpecified
		{
			get
			{
				return this.Read != null;
			}
		}
	}
}
