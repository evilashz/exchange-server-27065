using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020001A4 RID: 420
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class SearchPreviewItemType
	{
		// Token: 0x170005D0 RID: 1488
		// (get) Token: 0x060011B9 RID: 4537 RVA: 0x00024560 File Offset: 0x00022760
		// (set) Token: 0x060011BA RID: 4538 RVA: 0x00024568 File Offset: 0x00022768
		public ItemIdType Id
		{
			get
			{
				return this.idField;
			}
			set
			{
				this.idField = value;
			}
		}

		// Token: 0x170005D1 RID: 1489
		// (get) Token: 0x060011BB RID: 4539 RVA: 0x00024571 File Offset: 0x00022771
		// (set) Token: 0x060011BC RID: 4540 RVA: 0x00024579 File Offset: 0x00022779
		public PreviewItemMailboxType Mailbox
		{
			get
			{
				return this.mailboxField;
			}
			set
			{
				this.mailboxField = value;
			}
		}

		// Token: 0x170005D2 RID: 1490
		// (get) Token: 0x060011BD RID: 4541 RVA: 0x00024582 File Offset: 0x00022782
		// (set) Token: 0x060011BE RID: 4542 RVA: 0x0002458A File Offset: 0x0002278A
		public ItemIdType ParentId
		{
			get
			{
				return this.parentIdField;
			}
			set
			{
				this.parentIdField = value;
			}
		}

		// Token: 0x170005D3 RID: 1491
		// (get) Token: 0x060011BF RID: 4543 RVA: 0x00024593 File Offset: 0x00022793
		// (set) Token: 0x060011C0 RID: 4544 RVA: 0x0002459B File Offset: 0x0002279B
		public string ItemClass
		{
			get
			{
				return this.itemClassField;
			}
			set
			{
				this.itemClassField = value;
			}
		}

		// Token: 0x170005D4 RID: 1492
		// (get) Token: 0x060011C1 RID: 4545 RVA: 0x000245A4 File Offset: 0x000227A4
		// (set) Token: 0x060011C2 RID: 4546 RVA: 0x000245AC File Offset: 0x000227AC
		public string UniqueHash
		{
			get
			{
				return this.uniqueHashField;
			}
			set
			{
				this.uniqueHashField = value;
			}
		}

		// Token: 0x170005D5 RID: 1493
		// (get) Token: 0x060011C3 RID: 4547 RVA: 0x000245B5 File Offset: 0x000227B5
		// (set) Token: 0x060011C4 RID: 4548 RVA: 0x000245BD File Offset: 0x000227BD
		public string SortValue
		{
			get
			{
				return this.sortValueField;
			}
			set
			{
				this.sortValueField = value;
			}
		}

		// Token: 0x170005D6 RID: 1494
		// (get) Token: 0x060011C5 RID: 4549 RVA: 0x000245C6 File Offset: 0x000227C6
		// (set) Token: 0x060011C6 RID: 4550 RVA: 0x000245CE File Offset: 0x000227CE
		public string OwaLink
		{
			get
			{
				return this.owaLinkField;
			}
			set
			{
				this.owaLinkField = value;
			}
		}

		// Token: 0x170005D7 RID: 1495
		// (get) Token: 0x060011C7 RID: 4551 RVA: 0x000245D7 File Offset: 0x000227D7
		// (set) Token: 0x060011C8 RID: 4552 RVA: 0x000245DF File Offset: 0x000227DF
		public string Sender
		{
			get
			{
				return this.senderField;
			}
			set
			{
				this.senderField = value;
			}
		}

		// Token: 0x170005D8 RID: 1496
		// (get) Token: 0x060011C9 RID: 4553 RVA: 0x000245E8 File Offset: 0x000227E8
		// (set) Token: 0x060011CA RID: 4554 RVA: 0x000245F0 File Offset: 0x000227F0
		[XmlArrayItem("SmtpAddress", IsNullable = false)]
		public string[] ToRecipients
		{
			get
			{
				return this.toRecipientsField;
			}
			set
			{
				this.toRecipientsField = value;
			}
		}

		// Token: 0x170005D9 RID: 1497
		// (get) Token: 0x060011CB RID: 4555 RVA: 0x000245F9 File Offset: 0x000227F9
		// (set) Token: 0x060011CC RID: 4556 RVA: 0x00024601 File Offset: 0x00022801
		[XmlArrayItem("SmtpAddress", IsNullable = false)]
		public string[] CcRecipients
		{
			get
			{
				return this.ccRecipientsField;
			}
			set
			{
				this.ccRecipientsField = value;
			}
		}

		// Token: 0x170005DA RID: 1498
		// (get) Token: 0x060011CD RID: 4557 RVA: 0x0002460A File Offset: 0x0002280A
		// (set) Token: 0x060011CE RID: 4558 RVA: 0x00024612 File Offset: 0x00022812
		[XmlArrayItem("SmtpAddress", IsNullable = false)]
		public string[] BccRecipients
		{
			get
			{
				return this.bccRecipientsField;
			}
			set
			{
				this.bccRecipientsField = value;
			}
		}

		// Token: 0x170005DB RID: 1499
		// (get) Token: 0x060011CF RID: 4559 RVA: 0x0002461B File Offset: 0x0002281B
		// (set) Token: 0x060011D0 RID: 4560 RVA: 0x00024623 File Offset: 0x00022823
		public DateTime CreatedTime
		{
			get
			{
				return this.createdTimeField;
			}
			set
			{
				this.createdTimeField = value;
			}
		}

		// Token: 0x170005DC RID: 1500
		// (get) Token: 0x060011D1 RID: 4561 RVA: 0x0002462C File Offset: 0x0002282C
		// (set) Token: 0x060011D2 RID: 4562 RVA: 0x00024634 File Offset: 0x00022834
		[XmlIgnore]
		public bool CreatedTimeSpecified
		{
			get
			{
				return this.createdTimeFieldSpecified;
			}
			set
			{
				this.createdTimeFieldSpecified = value;
			}
		}

		// Token: 0x170005DD RID: 1501
		// (get) Token: 0x060011D3 RID: 4563 RVA: 0x0002463D File Offset: 0x0002283D
		// (set) Token: 0x060011D4 RID: 4564 RVA: 0x00024645 File Offset: 0x00022845
		public DateTime ReceivedTime
		{
			get
			{
				return this.receivedTimeField;
			}
			set
			{
				this.receivedTimeField = value;
			}
		}

		// Token: 0x170005DE RID: 1502
		// (get) Token: 0x060011D5 RID: 4565 RVA: 0x0002464E File Offset: 0x0002284E
		// (set) Token: 0x060011D6 RID: 4566 RVA: 0x00024656 File Offset: 0x00022856
		[XmlIgnore]
		public bool ReceivedTimeSpecified
		{
			get
			{
				return this.receivedTimeFieldSpecified;
			}
			set
			{
				this.receivedTimeFieldSpecified = value;
			}
		}

		// Token: 0x170005DF RID: 1503
		// (get) Token: 0x060011D7 RID: 4567 RVA: 0x0002465F File Offset: 0x0002285F
		// (set) Token: 0x060011D8 RID: 4568 RVA: 0x00024667 File Offset: 0x00022867
		public DateTime SentTime
		{
			get
			{
				return this.sentTimeField;
			}
			set
			{
				this.sentTimeField = value;
			}
		}

		// Token: 0x170005E0 RID: 1504
		// (get) Token: 0x060011D9 RID: 4569 RVA: 0x00024670 File Offset: 0x00022870
		// (set) Token: 0x060011DA RID: 4570 RVA: 0x00024678 File Offset: 0x00022878
		[XmlIgnore]
		public bool SentTimeSpecified
		{
			get
			{
				return this.sentTimeFieldSpecified;
			}
			set
			{
				this.sentTimeFieldSpecified = value;
			}
		}

		// Token: 0x170005E1 RID: 1505
		// (get) Token: 0x060011DB RID: 4571 RVA: 0x00024681 File Offset: 0x00022881
		// (set) Token: 0x060011DC RID: 4572 RVA: 0x00024689 File Offset: 0x00022889
		public string Subject
		{
			get
			{
				return this.subjectField;
			}
			set
			{
				this.subjectField = value;
			}
		}

		// Token: 0x170005E2 RID: 1506
		// (get) Token: 0x060011DD RID: 4573 RVA: 0x00024692 File Offset: 0x00022892
		// (set) Token: 0x060011DE RID: 4574 RVA: 0x0002469A File Offset: 0x0002289A
		public long Size
		{
			get
			{
				return this.sizeField;
			}
			set
			{
				this.sizeField = value;
			}
		}

		// Token: 0x170005E3 RID: 1507
		// (get) Token: 0x060011DF RID: 4575 RVA: 0x000246A3 File Offset: 0x000228A3
		// (set) Token: 0x060011E0 RID: 4576 RVA: 0x000246AB File Offset: 0x000228AB
		[XmlIgnore]
		public bool SizeSpecified
		{
			get
			{
				return this.sizeFieldSpecified;
			}
			set
			{
				this.sizeFieldSpecified = value;
			}
		}

		// Token: 0x170005E4 RID: 1508
		// (get) Token: 0x060011E1 RID: 4577 RVA: 0x000246B4 File Offset: 0x000228B4
		// (set) Token: 0x060011E2 RID: 4578 RVA: 0x000246BC File Offset: 0x000228BC
		public string Preview
		{
			get
			{
				return this.previewField;
			}
			set
			{
				this.previewField = value;
			}
		}

		// Token: 0x170005E5 RID: 1509
		// (get) Token: 0x060011E3 RID: 4579 RVA: 0x000246C5 File Offset: 0x000228C5
		// (set) Token: 0x060011E4 RID: 4580 RVA: 0x000246CD File Offset: 0x000228CD
		public ImportanceChoicesType Importance
		{
			get
			{
				return this.importanceField;
			}
			set
			{
				this.importanceField = value;
			}
		}

		// Token: 0x170005E6 RID: 1510
		// (get) Token: 0x060011E5 RID: 4581 RVA: 0x000246D6 File Offset: 0x000228D6
		// (set) Token: 0x060011E6 RID: 4582 RVA: 0x000246DE File Offset: 0x000228DE
		[XmlIgnore]
		public bool ImportanceSpecified
		{
			get
			{
				return this.importanceFieldSpecified;
			}
			set
			{
				this.importanceFieldSpecified = value;
			}
		}

		// Token: 0x170005E7 RID: 1511
		// (get) Token: 0x060011E7 RID: 4583 RVA: 0x000246E7 File Offset: 0x000228E7
		// (set) Token: 0x060011E8 RID: 4584 RVA: 0x000246EF File Offset: 0x000228EF
		public bool Read
		{
			get
			{
				return this.readField;
			}
			set
			{
				this.readField = value;
			}
		}

		// Token: 0x170005E8 RID: 1512
		// (get) Token: 0x060011E9 RID: 4585 RVA: 0x000246F8 File Offset: 0x000228F8
		// (set) Token: 0x060011EA RID: 4586 RVA: 0x00024700 File Offset: 0x00022900
		[XmlIgnore]
		public bool ReadSpecified
		{
			get
			{
				return this.readFieldSpecified;
			}
			set
			{
				this.readFieldSpecified = value;
			}
		}

		// Token: 0x170005E9 RID: 1513
		// (get) Token: 0x060011EB RID: 4587 RVA: 0x00024709 File Offset: 0x00022909
		// (set) Token: 0x060011EC RID: 4588 RVA: 0x00024711 File Offset: 0x00022911
		public bool HasAttachment
		{
			get
			{
				return this.hasAttachmentField;
			}
			set
			{
				this.hasAttachmentField = value;
			}
		}

		// Token: 0x170005EA RID: 1514
		// (get) Token: 0x060011ED RID: 4589 RVA: 0x0002471A File Offset: 0x0002291A
		// (set) Token: 0x060011EE RID: 4590 RVA: 0x00024722 File Offset: 0x00022922
		[XmlIgnore]
		public bool HasAttachmentSpecified
		{
			get
			{
				return this.hasAttachmentFieldSpecified;
			}
			set
			{
				this.hasAttachmentFieldSpecified = value;
			}
		}

		// Token: 0x170005EB RID: 1515
		// (get) Token: 0x060011EF RID: 4591 RVA: 0x0002472B File Offset: 0x0002292B
		// (set) Token: 0x060011F0 RID: 4592 RVA: 0x00024733 File Offset: 0x00022933
		public NonEmptyArrayOfExtendedPropertyType ExtendedProperties
		{
			get
			{
				return this.extendedPropertiesField;
			}
			set
			{
				this.extendedPropertiesField = value;
			}
		}

		// Token: 0x04000C27 RID: 3111
		private ItemIdType idField;

		// Token: 0x04000C28 RID: 3112
		private PreviewItemMailboxType mailboxField;

		// Token: 0x04000C29 RID: 3113
		private ItemIdType parentIdField;

		// Token: 0x04000C2A RID: 3114
		private string itemClassField;

		// Token: 0x04000C2B RID: 3115
		private string uniqueHashField;

		// Token: 0x04000C2C RID: 3116
		private string sortValueField;

		// Token: 0x04000C2D RID: 3117
		private string owaLinkField;

		// Token: 0x04000C2E RID: 3118
		private string senderField;

		// Token: 0x04000C2F RID: 3119
		private string[] toRecipientsField;

		// Token: 0x04000C30 RID: 3120
		private string[] ccRecipientsField;

		// Token: 0x04000C31 RID: 3121
		private string[] bccRecipientsField;

		// Token: 0x04000C32 RID: 3122
		private DateTime createdTimeField;

		// Token: 0x04000C33 RID: 3123
		private bool createdTimeFieldSpecified;

		// Token: 0x04000C34 RID: 3124
		private DateTime receivedTimeField;

		// Token: 0x04000C35 RID: 3125
		private bool receivedTimeFieldSpecified;

		// Token: 0x04000C36 RID: 3126
		private DateTime sentTimeField;

		// Token: 0x04000C37 RID: 3127
		private bool sentTimeFieldSpecified;

		// Token: 0x04000C38 RID: 3128
		private string subjectField;

		// Token: 0x04000C39 RID: 3129
		private long sizeField;

		// Token: 0x04000C3A RID: 3130
		private bool sizeFieldSpecified;

		// Token: 0x04000C3B RID: 3131
		private string previewField;

		// Token: 0x04000C3C RID: 3132
		private ImportanceChoicesType importanceField;

		// Token: 0x04000C3D RID: 3133
		private bool importanceFieldSpecified;

		// Token: 0x04000C3E RID: 3134
		private bool readField;

		// Token: 0x04000C3F RID: 3135
		private bool readFieldSpecified;

		// Token: 0x04000C40 RID: 3136
		private bool hasAttachmentField;

		// Token: 0x04000C41 RID: 3137
		private bool hasAttachmentFieldSpecified;

		// Token: 0x04000C42 RID: 3138
		private NonEmptyArrayOfExtendedPropertyType extendedPropertiesField;
	}
}
